using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.WinSvc;
using Org.TP;
using Org.GS;

namespace Org.FM
{
  public class FileMgmtTaskDispatcher : TaskDispatcherBase
  {
    private bool _reportAlignment = false;

    public FileMgmtTaskDispatcher()
    {
      _reportAlignment = g.AppConfig.GetBoolean("ReportAlignment");
    }

    public async override Task<TaskResult> DispatchTaskAsync(ITaskProcessor tp, TaskRequest taskRequest)
    {
      try
      {
        base.ContinueTask = true;

        taskRequest.TimeDispatched = DateTime.Now;
        TaskResult taskResult;

        tp.TaskRequest = taskRequest;
        tp.NotifyMessage += base.NotifyMessageMethod;
        taskResult = await tp.ProcessTaskAsync(CheckContinue);
        taskResult.EndDateTime = DateTime.Now;

        tp.NotifyMessage -= base.NotifyMessageMethod;

        return taskResult;
      }
      catch (Exception ex)
      {
        TaskResult taskResult = new TaskResult();
        taskResult.TaskName = taskRequest.TaskName;
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Message = "An error has occurred during " + taskResult.TaskName + " processing - see the following exception message." + g.crlf2 + ex.Message;
        taskResult.NotificationMessage = taskResult.Message;
        taskResult.Header = taskResult.Message;
        taskResult.Exception = ex;

        return taskResult;
      }
    }
  }
}

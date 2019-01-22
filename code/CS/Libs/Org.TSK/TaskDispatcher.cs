using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Org.WinSvc;
using Org.GS;
using Org.GS.Configuration;
using Org.TP;

namespace Org.TSK
{
  public class TaskDispatcher : TaskDispatcherBase
  {
    private bool _reportAlignment = false;
    public Dictionary<string, ITaskProcessorFactory> _taskProcessorFactories;

    public TaskDispatcher(Dictionary<string, ITaskProcessorFactory> taskProcessorFactories)
    {
      _taskProcessorFactories = taskProcessorFactories;
      _reportAlignment = g.AppConfig.GetBoolean("ReportAlignment");
    }

    public async override Task<TaskResult>DispatchTaskAsync(ITaskProcessor taskProcessor, TaskRequest taskRequest)
    {
      try
      {
        this.ContinueTask = true;

        taskRequest.TimeDispatched = DateTime.Now;
        TaskResult taskResult;
        taskProcessor.TaskRequest = taskRequest;
        taskProcessor.NotifyMessage += base.NotifyMessageMethod;
        taskResult = await taskProcessor.ProcessTaskAsync(CheckContinue);
        taskResult.EndDateTime = DateTime.Now;

        taskProcessor.NotifyMessage -= base.NotifyMessageMethod;

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

    public TaskResult DispatchTask(ITaskProcessor taskProcessor, TaskRequest taskRequest)
    {
      TaskResult taskResult = null;

      try
      {
        this.ContinueTask = true;

        taskRequest.TimeDispatched = DateTime.Now;
        taskProcessor.TaskRequest = taskRequest;
        taskProcessor.NotifyMessage += base.NotifyMessageMethod;
        taskResult = taskProcessor.ProcessTaskAsync(CheckContinue).Result;
        taskResult.EndDateTime = DateTime.Now;

        taskProcessor.NotifyMessage -= base.NotifyMessageMethod;

        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult = new TaskResult();
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



using Org.GS;
using Org.GS.Logging;
using Org.TP.Concrete;
using System;
using TPL = System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.QBX.Tasks
{
  public class QBXTransLoad : TaskProcessorBase
  {
    public override int EntityId { get { return 528; } }
    private Logger _logger;

    public override async TPL.Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();
      CheckContinue = checkContinue;
      _logger = new Logger();
      _logger.ModuleId = g.AppInfo.ModuleCode;

      try
      {
        return await TPL.Task.Run<TaskResult>(() =>
        {
          this.Initialize();
          taskResult.IsDryRun = IsDryRun;

          //base.AssertParmExistence("StatementType");

          // do some work

          if (IsDryRun)
            taskResult.NoWorkDone = true;

          return taskResult.Success();
        }).ContinueWith(r =>
        {
          if (r.Status == TPL.TaskStatus.RanToCompletion)
            return r.Result;
          if (r.Exception != null)
            return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing." + g.crlf + r.Exception.ToReport(), r.Exception);
          else
            return taskResult.Failed("The async task completed with status '" + r.Status.ToString() + "' during " + base.TaskRequest.TaskName + " task processing.");
        });
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing." + g.crlf + ex.ToReport(), ex);
      }
    }
  }
}

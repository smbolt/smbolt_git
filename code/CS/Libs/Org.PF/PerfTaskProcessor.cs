using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.TP.Concrete;
using Org.TP;
using Org.GS.Logging;
using Org.GS;

namespace Org.PF
{
  public class PerfTaskProcessor : TaskProcessorBase
  {
    public override int EntityId {
      get {
        return 522;
      }
    }
    private Logger _logger;


    public override async Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();
      CheckContinue = checkContinue;
      _logger = new Logger();
      _logger.ModuleId = g.AppInfo.ModuleCode;

      try
      {
        return await Task.Run<TaskResult>(() =>
        {
          this.Initialize();
          IsDryRun = base.GetParmValue("IsDryRun").ToBoolean();

          taskResult.TaskResultStatus = TaskResultStatus.Success;

          if (IsDryRun)
            taskResult.NoWorkDone = true;

          return taskResult;
        });
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing.", ex);
      }
    }

  }
}

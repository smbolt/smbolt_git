using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.TP.Concrete;
using Org.FTP;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS;

namespace Org.FTP.Tasks
{
  public class FtpTaskProcessor : TaskProcessorBase
  {
    public override int EntityId {
      get {
        return 999;
      }
    }
    private Logger _logger;
    private ConfigFtpSpec _configFtpSpec;

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
          taskResult.IsDryRun = IsDryRun;

          if (!ValidateFtpSpec(taskResult, _logger))
            return taskResult;


          if (IsDryRun)
            _logger.Log("Task processor is configured for a 'Dry Run'", 6106, this.EntityId);

          var commandSetTaskResult = ExecuteCommandSet(_configFtpSpec);

          switch(commandSetTaskResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              taskResult.TaskResultStatus = TaskResultStatus.Success;
              taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, commandSetTaskResult);
              break;

            default:
              taskResult.TaskResultStatus = TaskResultStatus.Failed;
              taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, commandSetTaskResult);
              break;
          }

          return taskResult;
        });
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing." + g.crlf + ex.ToReport(), ex);
      }
    }

    private TaskResult ExecuteCommandSet(ConfigFtpSpec configFtpSpec)
    {
      try
      {
        using (var ftpEngine = new FtpEngine(configFtpSpec))
        {
          var remoteFolder = ftpEngine.GetDirectoryList();


        }

        return new TaskResult().Success();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while executing the FTP Command Set.", ex);
      }
    }

    private bool ValidateFtpSpec(TaskResult taskResult, Logger logger)
    {
      _configFtpSpec = GetParmValue("ConfigFtpSpec") as ConfigFtpSpec;

      if (_configFtpSpec == null)
      {
        int code = 9999;
        string message = DryRunIndicator + "The required ConfigFtpSpec is null (code " + code.ToString() + ").";
        logger.Log(message, code, this.EntityId);
        taskResult.Failed(message, code);
        return false;
      }

      if (!_configFtpSpec.IsReadyToConnect())
      {
        int code = 9999;
        string message = DryRunIndicator + "ConfigFtpSpec is not ready to connect (code " + code.ToString() + ").";
        logger.Log(message, code, this.EntityId);
        taskResult.Failed(message, code);
        return false;
      }

      return true;
    }
  }
}

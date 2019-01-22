using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.AX
{
  public class AxEngine : IDisposable
  {
    public bool IsDryRun { get; set; }

    public TaskResult RunAxProfile(AxProfile profile)
    {
      TaskResult taskResult = new TaskResult("RunAxProfile");

      try
      {
        profile.Initialize();

        this.IsDryRun = profile.IsDryRun;

        foreach (var axAction in profile.Values.Where(a => a.IsActive))
        {
          var childTaskResult = axAction.Run(this.IsDryRun);
          taskResult.TaskResultSet.AddTaskResult(childTaskResult);

          if (childTaskResult.TaskResultStatus == TaskResultStatus.Failed)
            return taskResult.Failed(childTaskResult.Message);
        }

        taskResult.TaskResultStatus = taskResult.TaskResultSet.GetWorstResult();
        return taskResult;
      }
      catch (Exception ex)
      {
        return taskResult.Failed("RunAxProfile named '" + profile.Name + "' failed with an exception.", ex);
      }
    }

    public void Dispose()
    {
    }
  }
}

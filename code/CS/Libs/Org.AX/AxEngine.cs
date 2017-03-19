using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.AX
{
  public class AxEngine
  {
    public TaskResult RunAxProfile(AxProfileSet profileSet, string profileName, bool isDryRun)
    {
      TaskResult taskResult = new TaskResult("RunAxProfile");

      try
      {
        if (profileSet == null)
          return taskResult.Failed("AxProfileSet is null.");

        if (!profileSet.ContainsKey(profileName))
        {
          string noProfileMessage = "AxProfile set does not contain an AxProfile named '" + profileName + ".";
          taskResult.Data = noProfileMessage;
          return taskResult.Failed(noProfileMessage);
        }

        var profile = profileSet[profileName];

        foreach (var axion in profile.Values.Where(a => a.IsActive))
        {
          var axionTaskResult = axion.Run(isDryRun);
          taskResult.TaskResultSet.AddTaskResult(axionTaskResult);

          if (axionTaskResult.TaskResultStatus == TaskResultStatus.Failed)
            return taskResult.Failed(axionTaskResult.Message);
        }

        taskResult.TaskResultStatus = taskResult.TaskResultSet.GetWorstResult();
        return taskResult;
      }
      catch (Exception ex)
      {
        return taskResult.Failed("RunAxProfile named '" + profileName + "' failed with an exception.", ex);
      }
    }

    public void Dispose()
    {
    }
  }
}

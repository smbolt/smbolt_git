using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.WinSvc
{
  public class EngineMonitorParms
  {
    public bool EngineMonitorActive {
      get;
      set;
    }
    public int EngineMonitorIntervalSeconds {
      get;
      set;
    }
    public bool EngineMonitorAttemptRestart {
      get;
      set;
    }
    public bool EngineMonitorNotifyOnRestartAttempts {
      get;
      set;
    }
    public int EngineMonitorNotifyLimit {
      get;
      set;
    }
    public int EngineMonitorNotifyCount {
      get;
      set;
    }
    public int EngineMonitorResetNotifyLimitHours {
      get;
      set;
    }
    public bool EngineMonitorRunDependenciesChecks {
      get;
      set;
    }
    public int EngineMonitorDependencyCheckLimit {
      get;
      set;
    }
    public int EngineMonitorDependencyCheckCount {
      get;
      set;
    }
    public int EngineMonitorDependencyRetryIntervalSeconds {
      get;
      set;
    }
    public DateTime? LastMainLoopRestart {
      get;
      set;
    }
    public bool LogNormalMonitoringActivity {
      get;
      set;
    }

    public EngineMonitorParms()
    {
      this.EngineMonitorActive = true;
      this.EngineMonitorIntervalSeconds = 600;
      this.EngineMonitorAttemptRestart = true;
      this.EngineMonitorNotifyOnRestartAttempts = true;
      this.EngineMonitorNotifyLimit = 5;
      this.EngineMonitorNotifyCount = 0;
      this.EngineMonitorResetNotifyLimitHours = 12;
      this.EngineMonitorRunDependenciesChecks = true;
      this.EngineMonitorDependencyCheckLimit = 9999999;
      this.EngineMonitorDependencyCheckCount = 0;
      this.EngineMonitorDependencyRetryIntervalSeconds = 600;
      this.LastMainLoopRestart = null;
      this.LogNormalMonitoringActivity = false;
    }

    public bool PerformTryRestartNotification()
    {
      try
      {
        if (!this.EngineMonitorNotifyOnRestartAttempts)
          return false;

        if (this.EngineMonitorNotifyCount > this.EngineMonitorNotifyLimit)
          return false;

        return true;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to perform notifications in the main loop restart process after the " +
                            "main loop monitoring process discovered that the main loop was faulted.", ex);
      }
    }

    public bool ContinueToRetryDependenciesCheck()
    {
      this.EngineMonitorDependencyCheckCount++;

      if (this.EngineMonitorDependencyCheckCount > this.EngineMonitorDependencyCheckLimit)
        return false;

      return true;
    }

    public void EnsureMinimumValues()
    {
      if (this.EngineMonitorIntervalSeconds < 60)
        this.EngineMonitorIntervalSeconds = 60;

      if (this.EngineMonitorDependencyRetryIntervalSeconds < 60)
        this.EngineMonitorDependencyRetryIntervalSeconds = 60;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class WinServiceParms
  {
    public string WindowsServiceName { get; set; }
    public int EntityId { get; set; }
    public int SleepInterval { get; set; }
    public bool IsRunningAsWindowsService { get; set; }
    public string ConfigLogSpecPrefix { get; set; }
    public string ConfigNotifySpecPrefix { get; set; }
    public int MaxWaitIntervalMilliseconds { get; set; }
    public bool InDiagnosticsMode { get; set; }
    public bool SuppressNonErrorOutput { get; set; }
    public bool SuppressTaskReload { get; set; }
    public bool RunWebService { get; set; }
    public bool UseAlerter { get; set; }
    public string AlerterPath { get; set; }

    public WinServiceParms()
    {
      this.WindowsServiceName = "WindowsServiceNameNotSet";
      this.EntityId = 0;
      this.SleepInterval = 1000; 
      this.IsRunningAsWindowsService = true;
      this.ConfigLogSpecPrefix = "Default";
      this.ConfigNotifySpecPrefix = "Default";
      this.MaxWaitIntervalMilliseconds = 1000;
      this.InDiagnosticsMode = false;
      this.SuppressNonErrorOutput = true;
      this.SuppressTaskReload = false;
      this.RunWebService = false;
      this.UseAlerter = false;
      this.AlerterPath = String.Empty;
    }
  }
}

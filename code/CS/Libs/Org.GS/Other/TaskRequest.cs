using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.Configuration;

namespace Org.GS
{
  public enum TaskRequestType
  {
    ScheduledTask,
    OtherJob,
    OnDemand
  }

  [Serializable]
  public class TaskRequest
  {
    public string TaskName {
      get;
      set;
    }
    public string ProcessorName {
      get;
      set;
    }
    public string ProcessorVersion {
      get;
      set;
    }
    public string ProcessorNameAndVersion {
      get {
        return this.ProcessorName + "_" + this.ProcessorVersion;
      }
    }
    public string CatalogName {
      get;
      set;
    }
    public string CatalogEntry {
      get;
      set;
    }
    public string AssemblyName {
      get;
      set;
    }
    public string ObjectTypeName {
      get;
      set;
    }
    public TaskRequestType TaskRequestType {
      get;
      set;
    }

    public bool TrackHistory {
      get;
      set;
    }
    public int? RunId {
      get;
      set;
    }
    public int ScheduledTaskId {
      get;
      set;
    }
    public int ProcessorTypeId {
      get;
      set;
    }
    public ParmSet ParmSet {
      get;
      set;
    }
    public DateTime ScheduledRunDateTime {
      get;
      set;
    }
    public DateTime TimeDispatched {
      get;
      set;
    }
    public DateTime TimeCompleted {
      get;
      set;
    }
    public string TaskRequestId {
      get;
      set;
    }
    public bool RunUntilTask {
      get;
      set;
    }
    public int? RunUntilPeriodContextId {
      get;
      set;
    }
    public int? RunUntilOffsetMinutes {
      get;
      set;
    }
    public bool SuppressNotificationsOnSuccess {
      get;
      set;
    }
    public bool AllowConcurrent {
      get;
      set;
    }
    public bool IsDryRun {
      get;
      set;
    }
    public bool IsActive {
      get;
      set;
    }
    public object Object {
      get;
      set;
    }
    public TimeSpan RemainingTimeTilSchedule {
      get {
        return Get_RemainingTimeTilSchedule();
      }
    }
    public string RemainingTimeTilScheduleFmt {
      get {
        return Get_RemainingTimeTilScheduleFmt();
      }
    }
    public string Report {
      get {
        return Get_Report();
      }
    }

    public TaskRequest(int scheduledTaskId, string taskName, string processorName, string processorVersion, int processorTypeId,
                       string assemblyName, string catalogName, string catalogEntry, string objectTypeName,
                       TaskRequestType taskRequestType, ParmSet parmSet, DateTime scheduledRunDateTime, bool trackHistory, bool runUntilTask,
                       int? runUntilPeriodContextId, int? runUntilOffsetMinutes, bool suppressNotificationsOnSuccess)
    {
      this.ScheduledTaskId = scheduledTaskId;
      this.TaskName = taskName;
      this.ProcessorName = processorName;
      this.ProcessorVersion = processorVersion;
      this.ProcessorTypeId = processorTypeId;
      this.AssemblyName = assemblyName;
      this.CatalogName = catalogName;
      this.CatalogEntry = catalogEntry;
      this.ObjectTypeName = objectTypeName;
      this.TaskRequestType = taskRequestType;
      this.TrackHistory = trackHistory;
      this.RunId = null;
      this.ParmSet = parmSet;
      this.ScheduledRunDateTime = scheduledRunDateTime;
      this.TimeDispatched = DateTime.MinValue;
      this.TimeCompleted = DateTime.MinValue;
      this.TaskRequestId = String.Empty;
      this.RunUntilTask = runUntilTask;
      this.RunUntilPeriodContextId = runUntilPeriodContextId;
      this.RunUntilOffsetMinutes = runUntilOffsetMinutes;
      this.SuppressNotificationsOnSuccess = suppressNotificationsOnSuccess;
      this.AllowConcurrent = false;
      this.IsDryRun = GetIsDryRun();
      this.Object = null;
    }

    public TaskRequest(int scheduledTaskId, string taskName, string processorName, string processorVersion, int processorTypeId,
                       TaskRequestType taskRequestType, ParmSet parmSet, DateTime scheduledRunDateTime, bool trackHistory, bool runUntilTask,
                       int? runUntilPeriodContextId, int? runUntilOffsetMinutes, bool suppressNotificationsOnSuccess)
    {
      this.ScheduledTaskId = scheduledTaskId;
      this.TaskName = taskName;
      this.ProcessorName = processorName;
      this.ProcessorVersion = processorVersion;
      this.ProcessorTypeId = processorTypeId;
      this.AssemblyName = String.Empty;
      this.CatalogName = String.Empty;
      this.CatalogEntry = String.Empty;
      this.ObjectTypeName = String.Empty;
      this.TaskRequestType = taskRequestType;
      this.TrackHistory = trackHistory;
      this.RunId = null;
      this.ParmSet = parmSet;
      this.ScheduledRunDateTime = scheduledRunDateTime;
      this.TimeDispatched = DateTime.MinValue;
      this.TimeCompleted = DateTime.MinValue;
      this.TaskRequestId = String.Empty;
      this.RunUntilTask = runUntilTask;
      this.RunUntilPeriodContextId = runUntilPeriodContextId;
      this.RunUntilOffsetMinutes = runUntilOffsetMinutes;
      this.SuppressNotificationsOnSuccess = suppressNotificationsOnSuccess;
      this.AllowConcurrent = false;
      this.IsDryRun = GetIsDryRun();
      this.Object = null;
    }

    private bool GetIsDryRun()
    {
      bool isDryRun = false;

      foreach (var parm in this.ParmSet)
      {
        if (parm.ParameterName == "IsDryRun")
        {
          isDryRun = parm.ParameterValue.ToString().ToBoolean();
          break;
        }
      }
      return isDryRun;
    }

    private TimeSpan Get_RemainingTimeTilSchedule()
    {
      if (this.ScheduledRunDateTime < DateTime.Now)
        return new TimeSpan(0);

      return this.ScheduledRunDateTime - DateTime.Now;
    }

    private string Get_RemainingTimeTilScheduleFmt()
    {
      if (this.ScheduledRunDateTime < DateTime.Now)
        return "0 seconds";

      TimeSpan ts = this.ScheduledRunDateTime - DateTime.Now;
      return ts.TotalSeconds.ToString("####.000") + " seconds.";
    }

    public string GetParmReport()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("TASK PARAMETERS REPORT" + g.crlf2 +
                "Task: " + this.TaskName + " at " + this.ScheduledRunDateTime + g.crlf2 +
                "Parameter Name".PadTo(40) + "Data Type".PadTo(50) + "Parameter Value" + g.crlf +
                "--------------------------------------------------------------------------------------------------------------------------------------" + g.crlf);

      foreach (var parm in this.ParmSet)
      {
        sb.Append(parm.ParameterName.PadTo(40) + parm.ParameterType.ToString().PadTo(50));

        switch (parm.ParameterType.Name)
        {
          case "String":
          case "Int32":
            sb.Append(parm.ParameterValue);
            break;

          case "ConfigDbSpec":
            var configDbSpec = parm.ParameterValue as ConfigDbSpec;
            sb.Append(configDbSpec.ConnectionString);
            break;

          case "ConfigWsSpec":
            var configWsSpec = parm.ParameterValue as ConfigWsSpec;
            sb.Append(configWsSpec.WebServiceEndpoint);
            break;

          case "Dictionary`2":
            var dict = parm.ParameterValue as Dictionary<string, string>;
            sb.Remove(sb.Length - 50, 50);
            sb.Append("Dictionary<string, string>");
            foreach (var kvp in dict)
            {
              sb.Append(g.crlf + String.Empty.PadTo(2) + kvp.Key.PadTo(40) + "  " + kvp.Value);
            }
            break;

          case "List`1":
            var list = parm.ParameterValue as List<string>;
            sb.Remove(sb.Length - 50, 50);
            sb.Append("List<string>");
            foreach (var item in list)
            {
              sb.Append(g.crlf + String.Empty.PadTo(2) + item.PadTo(40));
            }
            break;

          default:
            sb.Append("[Type:" + parm.ParameterType.Name + "] " + parm.ParameterValue.ToString());
            break;
        }
        sb.Append(g.crlf);
      }

      return sb.ToString();
    }

    private string Get_Report()
    {
      var sb = new StringBuilder();
      sb.Append("TaskName:" + this.TaskName + " (" + this.ScheduledTaskId.ToString() + ")" + g.crlf +
                "  Scheduled at:" + this.ScheduledRunDateTime.ToString("MM/dd/yyyy HH:mm:ss.fff") + g.crlf +
                "  Parms:" + g.crlf + this.GetParmReport());

      string report = sb.ToString();
      return report;
    }
  }
}

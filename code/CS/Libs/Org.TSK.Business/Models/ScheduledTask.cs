using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;
using Org.TSK;
using Bus = Org.TSK.Business;

namespace Org.TSK.Business.Models
{
  public class ScheduledTask
  {
    private ConfigDbSpec _configDbSpec;
    public int ScheduledTaskId {
      get;
      set;
    }
    public string TaskName {
      get;
      set;
    }
    public int? TaskGroupId {
      get;
      set;
    }
    public string TaskGroupName {
      get;
      set;
    }
    public int ProcessorTypeId {
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
    public DateTime BaseDateTime {
      get;
      set;
    }
    public bool IsManaged {
      get;
      set;
    }
    public Nullable<int> TaskNumber {
      get;
      set;
    }
    public string AssemblyName {
      get;
      set;
    }
    public string AssemblyLocation {
      get;
      set;
    }
    public string CatalogName {
      get;
      set;
    }
    public string CatalogEntry {
      get;
      set;
    }
    public string ObjectTypeName {
      get;
      set;
    }
    public string ClassName {
      get;
      set;
    }
    public string StoredProcedureName {
      get;
      set;
    }
    public bool IsActive {
      get;
      set;
    }
    public bool RunUntilTask {
      get;
      set;
    }
    public bool RunUntilOverride {
      get;
      set;
    }
    public int? RunUntilPeriodContextID {
      get;
      set;
    }
    public int? RunUntilOffsetMinutes {
      get;
      set;
    }
    public bool IsLongRunning {
      get;
      set;
    }
    public bool TrackHistory {
      get;
      set;
    }
    public bool SuppressNotificationsOnSuccess {
      get;
      set;
    }
    public int? ActiveScheduleId {
      get;
      set;
    }
    public string CreatedBy {
      get;
      set;
    }
    public DateTime CreatedDate {
      get;
      set;
    }
    public string ModifiedBy {
      get;
      set;
    }
    public DateTime? ModifiedDate {
      get;
      set;
    }
    public TimeIntervalSet RunUntilExclusionIntervals {
      get;
      set;
    }
    public ScheduledRunSet ScheduledRunSet {
      get;
      set;
    }
    public TaskSchedule TaskSchedule {
      get;
      set;
    }
    public ParmSet ParmSet {
      get;
      set;
    }
    public TaskAssignmentSet TaskAssignmentSet {
      get;
      set;
    }
    public CurrentPeriod CurrentPeriod {
      get {
        return Get_CurrentPeriod();
      }
    }

    public ScheduledTask()
    {
      this.ParmSet = new ParmSet();
      this.TaskAssignmentSet = new TaskAssignmentSet();
    }

    public ScheduledTask(Bus.Models.ScheduledTask scheduledTask, DateTime baseDateTime, ConfigDbSpec configDbSpec)
    {
      _configDbSpec = configDbSpec;
      DateTime bdtWork = baseDateTime.AddSeconds(1);

      this.BaseDateTime = new DateTime(bdtWork.Year, bdtWork.Month, bdtWork.Day, bdtWork.Hour, bdtWork.Minute, bdtWork.Second);
      //this.Load(scheduledTask);
      this.ScheduledRunSet = null;
    }
    public ScheduledTask(DateTime baseDateTime, ConfigDbSpec configDbSpec)
    {
      _configDbSpec = configDbSpec;
      DateTime bdtWork = baseDateTime.AddSeconds(1);

      this.BaseDateTime = new DateTime(bdtWork.Year, bdtWork.Month, bdtWork.Day, bdtWork.Hour, bdtWork.Minute, bdtWork.Second);
      this.ScheduledRunSet = null;
      this.ModifiedDate = DateTime.MinValue;
    }

    public ScheduledTask(TaskConfig taskConfig, DateTime baseDateTime)
    {
      DateTime bdtWork = baseDateTime.AddSeconds(1);

      this.BaseDateTime = new DateTime(bdtWork.Year, bdtWork.Month, bdtWork.Day, bdtWork.Hour, bdtWork.Minute, bdtWork.Second);
      this.Load(taskConfig);
      this.ScheduledRunSet = null;
    }

    public ScheduledRunSet GetScheduledRunSet(DateTime intervalStart, DateTime intervalEnd, bool scheduleOnceNow, bool viewOnly)
    {
      try
      {
        ScheduledRunSet runSet = new ScheduledRunSet(this, this.TaskSchedule);

        if (!scheduleOnceNow)
          LoadRunUntilExclusionIntervals();

        foreach (var e in this.TaskSchedule.TaskScheduleElements.Where(t => t.IsActive || scheduleOnceNow))
        {
          ScheduleDateControl sdc = new ScheduleDateControl(e);
          List<DateTime> scheduleRunDateTimes = new List<DateTime>();

          ScheduledRun scheduledRun = null;

          if (scheduleOnceNow)
          {
            scheduledRun = new ScheduledRun(this, this.TaskSchedule, e, DateTime.Now.AddSeconds(5), ScheduledRunType.RunOnFrequency);
            runSet.Add(scheduledRun.ScheduledRunDateTime, scheduledRun);
            continue;
          }

          switch (e.TaskExecutionType)
          {
            case TaskExecutionType.RunOnFrequency:
              scheduleRunDateTimes.AddRange(sdc.GetScheduleOnFrequency(intervalStart, intervalEnd, this.RunUntilExclusionIntervals, this.RunUntilOverride, false));
              foreach (var runDateTime in scheduleRunDateTimes)
              {
                scheduledRun = new ScheduledRun(this, this.TaskSchedule, e, runDateTime, ScheduledRunType.RunOnFrequency);
                runSet.Add(scheduledRun.ScheduledRunDateTime, scheduledRun);
              }
              break;

            case TaskExecutionType.RunImmediate:
              if (!e.RunImmediateHasBeenScheduled)
              {
                DateTime timeToRun = this.BaseDateTime.AddSeconds(5);
                scheduledRun = new ScheduledRun(this, this.TaskSchedule, e, timeToRun, ScheduledRunType.RunImmediate);
                runSet.Add(scheduledRun.ScheduledRunDateTime, scheduledRun);
                e.RunImmediateHasBeenScheduled = true;
              }
              break;

            case TaskExecutionType.RunImmediateAndOnFrequency:
              // Add the RunImmediate ScheduledRun
              scheduledRun = new ScheduledRun(this, this.TaskSchedule, e, this.BaseDateTime, ScheduledRunType.RunImmediate);
              runSet.Add(scheduledRun.ScheduledRunDateTime, scheduledRun);

              // Add the OnFrequency ScheduledRuns
              scheduleRunDateTimes.AddRange(sdc.GetScheduleOnFrequency(intervalStart, intervalEnd, this.RunUntilExclusionIntervals, this.RunUntilOverride, true));
              foreach (var runDateTime in scheduleRunDateTimes)
              {
                scheduledRun = new ScheduledRun(this, this.TaskSchedule, e, runDateTime, ScheduledRunType.RunOnFrequency);
                if (!runSet.ContainsKey(scheduledRun.ScheduledRunDateTime))
                {
                  runSet.Add(scheduledRun.ScheduledRunDateTime, scheduledRun);
                }
              }
              break;

            // RunOnceAt
            case TaskExecutionType.RunOnceAt:
              if (!e.RunAtHasBeenScheduled)
              {
                // Add the RunAt ScheduledRun
                if (!e.StartDateTime.HasValue)
                  throw new Exception("RunAt task does not have valid StartDateTime property in Task '" + this.TaskName + ", " +
                                      "Schedule '" + this.TaskSchedule.ScheduleName + "', ScheduleElementId '" + e.TaskScheduleElementId.ToString() + "'.");
                scheduledRun = new ScheduledRun(this, this.TaskSchedule, e, e.StartDateTime.Value, ScheduledRunType.RunAt);
                runSet.Add(scheduledRun.ScheduledRunDateTime, scheduledRun);
                e.RunAtHasBeenScheduled = true;
              }
              break;

            // RunAtAndOnFrequency
            case TaskExecutionType.RunAtAndOnFrequency:
              // add the RunAt ScheduledRun
              if (!e.RunAtHasBeenScheduled)
              {
                if (!e.StartDateTime.HasValue)
                  throw new Exception("RunAt task does not have valid StartDateTime property in Task '" + this.TaskName + ", " +
                                      "Schedule '" + this.TaskSchedule.ScheduleName + "', ScheduleElementId '" + e.TaskScheduleElementId.ToString() + "'.");
                scheduledRun = new ScheduledRun(this, this.TaskSchedule, e, e.StartDateTime.Value, ScheduledRunType.RunAt);

                if (scheduledRun.ScheduledRunDateTime > DateTime.Now)
                {
                  runSet.Add(scheduledRun.ScheduledRunDateTime, scheduledRun);
                  e.RunAtHasBeenScheduled = true;
                }
              }

              // Add the OnFrequency ScheduledRuns
              scheduleRunDateTimes.AddRange(sdc.GetScheduleOnFrequency(intervalStart, intervalEnd, this.RunUntilExclusionIntervals, this.RunUntilOverride, true));
              foreach (var runDateTime in scheduleRunDateTimes)
              {
                scheduledRun = new ScheduledRun(this, this.TaskSchedule, e, runDateTime, ScheduledRunType.RunOnFrequency);
                if (!runSet.ContainsKey(scheduledRun.ScheduledRunDateTime))
                  runSet.Add(scheduledRun.ScheduledRunDateTime, scheduledRun);
              }

              e.RunAtHasBeenScheduled = true;
              break;
          }
        }

        if (!scheduleOnceNow && !viewOnly && this.RunUntilOverride && runSet.Count > 0)
        {
          using (var repo = new TaskRepository(_configDbSpec))
          {
            repo.TurnOffRunUntilOverride(this.ScheduledTaskId);
          }

          this.RunUntilOverride = false;
        }

        return runSet;
      }
      catch (Exception ex)
      {
        string msg = ex.Message;
        throw new Exception("An exception occurred attempting to build the ScheduleRunSet for task '" + this.TaskName + "'.", ex);
      }
    }

    public void LoadRunUntilExclusionIntervals()
    {
      this.RunUntilExclusionIntervals = new TimeIntervalSet();

      try
      {
        //if (this.RunUntilOverride)
        //{
        //  this.RunUntilExclusionIntervals = new TimeIntervalSet();
        //  return;
        //}
        if (this.RunUntilTask)
        {
          using (var repo = new TaskRepository(_configDbSpec))
          {
            this.RunUntilExclusionIntervals = repo.GetRunUntilExclusionIntervals(this);
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get RunUntilExclusionIntervals for task '" + this.TaskName + "'.", ex);
      }
    }

    private void Load(Bus.Models.ScheduledTask t)
    {
      this.ScheduledTaskId = t.ScheduledTaskId;
      this.TaskName = t.TaskName;
      this.AssemblyLocation = t.AssemblyLocation;
      this.ClassName = t.ClassName;
      this.StoredProcedureName = t.StoredProcedureName;
      this.IsActive = t.IsActive;
      this.IsLongRunning = t.IsLongRunning;
      this.ActiveScheduleId = t.ActiveScheduleId;

      this.ParmSet = new ParmSet();
      List<string> parmSetsToAdd = new List<string>();

      //foreach (var scheduledTaskParameter in t.ScheduledTaskParameters)
      //{
      //  var parm = new Parm();
      //  parm.ParameterId = scheduledTaskParameter.ParameterId;
      //  parm.ScheduledTaskId = scheduledTaskParameter.ScheduledTaskId;
      //  parm.ParameterSetName = scheduledTaskParameter.ParameterSetName;
      //  parm.ParameterName = scheduledTaskParameter.ParameterName;
      //  parm.ParameterValue = scheduledTaskParameter.ParameterValue != null ? scheduledTaskParameter.ParameterValue : String.Empty;
      //  if (parm.ParameterValue != null && parm.ParameterValue.ToString().IsNotBlank())
      //  {
      //    if (parm.ParameterValue.ToString().Trim().StartsWith("ParmSet="))
      //      parmSetsToAdd.Add(parm.ParameterValue.ToString().Trim().Replace("ParmSet=", String.Empty));
      //  }
      //  parm.ParameterType = scheduledTaskParameter.DataType.ToType();
      //  //if (parm.ParameterType == null && scheduledTaskParameter.DataType.IsNotBlank())
      //  //  parm.ParameterType

      //  this.ParmSet.Add(parm);
      //}

      //if (parmSetsToAdd.Count > 0)
      //{
      //  using (var context = new TaskSchedulingEntities(_configDbSpec))
      //  {
      //    // The parmSetsToAdd entries can contain a named parameter set such as "ZirmedSftpParms" in which case
      //    // all parameters in the set will be added to the resulting collection.  Alternately if the parmSetToAdd
      //    // entry uses the "dottted name convention" such as "ZirmedSftpParms.FileExchangeRoot", then only the
      //    // 'FileExchangeRoot' paramater from the 'ZirmedSftpParms' set will be included in the resulting colletion.

      //    foreach (string parmSetName in parmSetsToAdd)
      //    {
      //      bool getAllParmsInSet = true;
      //      string setName = parmSetName;
      //      string parmName = String.Empty;

      //      bool getSimpleItem = false;
      //      bool buildComplexObject = false;
      //      object complexObject = null;
      //      Type complexType = null;

      //      if (parmSetName.Contains("."))
      //      {
      //        getSimpleItem = true;
      //        string[] tokens = parmSetName.Split(Constants.DotDelimiter, StringSplitOptions.RemoveEmptyEntries);
      //        if (tokens.Length == 2)
      //        {
      //          setName = tokens[0].Trim();
      //          parmName = tokens[1].Trim();
      //          getAllParmsInSet = false;
      //        }
      //      }

      //      Parm parmToReplace = null;
      //      if (!getSimpleItem)
      //      {
      //        parmToReplace = this.ParmSet.Where(p => p.ParameterName == setName).FirstOrDefault();
      //        if (parmToReplace != null)
      //        {
      //          complexType = parmToReplace.ParameterType;
      //          if (complexType != null)
      //          {
      //            complexObject = Activator.CreateInstance(complexType);
      //            if (complexObject != null)
      //              buildComplexObject = true;
      //          }
      //        }
      //      }

      //      var parmSet = context.ScheduledTaskParameters.Where(p => p.ParameterSetName == setName);

      //      foreach (var parm in parmSet)
      //      {
      //        // If the dotted name notation was used (i.e. 'SetName.ParameterName') and the parm
      //        // within the set being processed does not match the name specified (i.e. 'ParameterName')
      //        // then do not include this parameter in the resulting set.

      //        if (!getAllParmsInSet && parm.ParameterName != parmName)
      //          continue;

      //        if (buildComplexObject)
      //        {
      //          string objectType = complexObject.GetType().Name;

      //          switch (objectType)
      //          {
      //            case "Dictionary`2":
      //              Dictionary<string, string> dict = (Dictionary<string, string>) complexObject;
      //              if (!dict.ContainsKey(parm.ParameterName))
      //                dict.Add(parm.ParameterName, parm.ParameterValue);
      //              break;

      //            case "List`1":
      //              List<string> list = (List<string>)complexObject;
      //              list.Add(parm.ParameterValue);
      //              break;

      //            case "ConfigDbSpec":
      //            case "ConfigFtpSpec":
      //            case "ConfigWsSpec":
      //            case "ConfigSyncSpec":
      //            case "ConfigSmtpSpec":
      //              PropertyInfo pi = complexType.GetProperty(parm.ParameterName);
      //              if (pi != null)
      //              {
      //                string propType = pi.PropertyType.Name;
      //                switch (propType)
      //                {
      //                  case "String":
      //                    pi.SetValue(complexObject, parm.ParameterValue.ObjectToTrimmedString());
      //                    break;

      //                  case "Int32":
      //                    pi.SetValue(complexObject, parm.ParameterValue.ToInt32());
      //                    break;

      //                  case "Boolean":
      //                    pi.SetValue(complexObject, parm.ParameterValue.ToBoolean());
      //                    break;

      //                  case "DatabaseType":
      //                    pi.SetValue(complexObject, g.ToEnum<DatabaseType>(parm.ParameterValue, DatabaseType.SqlServer));
      //                    break;

      //                  case "WebServiceBinding":
      //                    pi.SetValue(complexObject, g.ToEnum<WebServiceBinding>(parm.ParameterValue, WebServiceBinding.BasicHttp));
      //                    break;

      //                  default:
      //                    throw new Exception("Invalid value specified '" + parm.ParameterValue + "' for the schedule task parameter named '" +
      //                                         parm.ParameterName + "' in parameter set named '" + parm.ParameterSetName + "' while attempting to " +
      //                                         "build scheduled task parameters for the scheduled task named '" + t.TaskName + "' (ScheduleTaskId = '" +
      //                                         t.ScheduledTaskId.ToString() + ".");
      //                }

      //              }
      //              break;
      //          }

      //          continue;
      //        }

      //        var setParm = new Parm();
      //        setParm.ParameterId = parm.ParameterId;
      //        setParm.ScheduledTaskId = parm.ScheduledTaskId;
      //        setParm.ParameterSetName = parm.ParameterSetName;
      //        setParm.ParameterName = parm.ParameterName;
      //        setParm.ParameterValue = parm.ParameterValue;
      //        setParm.ParameterType = parm.DataType.ToType();
      //        this.ParmSet.Add(setParm);
      //      }

      //      if (buildComplexObject)
      //        parmToReplace.ParameterValue = complexObject;
      //    }
      //  }
      //}

      //Entity.TaskSchedule sched = t.TaskSchedules.Where(s => s.TaskScheduleId == t.ActiveScheduleId).FirstOrDefault();
      //if (sched != null)
      //{
      //  TaskSchedule taskSchedule = new TaskSchedule();
      //  taskSchedule.TaskScheduleId = sched.TaskScheduleId;
      //  taskSchedule.ScheduledTaskId = sched.ScheduledTaskId;
      //  taskSchedule.ScheduleName = sched.ScheduleName;
      //  taskSchedule.IsActive = sched.IsActive;
      //  taskSchedule.TaskScheduleElements = new List<TaskScheduleElement>();

      //  foreach (var e in sched.TaskScheduleElements)
      //  {
      //    TaskScheduleElement taskScheduleElement = new TaskScheduleElement();
      //    taskScheduleElement.ScheduledTask = this;
      //    taskScheduleElement.TaskSchedule = taskSchedule;
      //    taskScheduleElement.TaskScheduleElementId = e.TaskScheduleElementId;
      //    taskScheduleElement.TaskScheduleId = e.TaskScheduleId;
      //    taskScheduleElement.IsActive = e.IsActive;
      //    taskScheduleElement.TaskExecutionType = g.ToEnum<TaskExecutionType>(e.TaskScheduleExecutionTypeId, TaskExecutionType.NotSet);
      //    taskScheduleElement.FrequencySeconds = e.FrequencySeconds;
      //    taskScheduleElement.IsClockAligned = e.IsClockAligned;
      //    taskScheduleElement.ScheduleElementPriority = e.ScheduleElementPriority;
      //    taskScheduleElement.StartDate = e.StartDate;
      //    taskScheduleElement.StartTime = e.StartTime;
      //    taskScheduleElement.EndDate = e.EndDate;
      //    taskScheduleElement.EndTime = e.EndTime;
      //    taskScheduleElement.IntervalType = g.ToEnum<IntervalType>(e.IntervalTypeId, IntervalType.NotSet);
      //    taskScheduleElement.OnSunday = e.OnSunday;
      //    taskScheduleElement.OnMonday = e.OnMonday;
      //    taskScheduleElement.OnTuesday = e.OnTuesday;
      //    taskScheduleElement.OnWednesday = e.OnWednesday;
      //    taskScheduleElement.OnThursday = e.OnThursday;
      //    taskScheduleElement.OnFriday = e.OnFriday;
      //    taskScheduleElement.OnSaturday = e.OnSaturday;
      //    taskScheduleElement.OnWorkDays = e.OnWorkDays;
      //    taskScheduleElement.OnEvenDays = e.OnEvenDays;
      //    taskScheduleElement.OnOddDays = e.OnOddDays;
      //    taskScheduleElement.SpecificDays = e.SpecificDays;
      //    taskScheduleElement.ExceptSpecificDays = e.ExceptSpecificDays;
      //    taskScheduleElement.First = e.First;
      //    taskScheduleElement.Second = e.Second;
      //    taskScheduleElement.Third = e.Third;
      //    taskScheduleElement.Fourth = e.Fourth;
      //    taskScheduleElement.Fifth = e.Fifth;
      //    taskScheduleElement.Last = e.Last;
      //    taskScheduleElement.Every = e.Every;
      //    taskScheduleElement.HolidayActions = g.ToEnum<HolidayActions>(e.HolidayActionId.Value, HolidayActions.NotSet);
      //    taskScheduleElement.PeriodContexts = g.ToEnum<PeriodContexts>(e.PeriodContextId, PeriodContexts.NotSet);
      //    taskScheduleElement.MaxExecutions = e.ExecutionLimit;
      //    taskScheduleElement.MaxRunTimeSeconds = e.MaxRunTimeSeconds;
      //    taskSchedule.TaskScheduleElements.Add(taskScheduleElement);
      //  }

      //  this.TaskSchedule = taskSchedule;
      //}
    }

    private void Load(TaskConfig t)
    {
      this.ScheduledTaskId = 0;
      this.TaskName = t.TaskName;
      this.ProcessorName = t.ProcessorName;
      this.ProcessorVersion = t.ProcessorVersion;
      this.IsManaged = false;
      this.TaskNumber = 0;
      this.AssemblyName = t.AssemblyName;
      this.AssemblyLocation = String.Empty;
      this.CatalogName = t.CatalogName;
      this.CatalogEntry = t.CatalogEntry;
      this.ObjectTypeName = t.ObjectTypeName;
      this.ClassName = String.Empty;
      this.StoredProcedureName = String.Empty;
      this.IsActive = t.IsActive;
      this.IsLongRunning = t.IsLongRunning;
      this.ActiveScheduleId = 0;

      this.ParmSet = new ParmSet();
      List<string> parmSetsToAdd = new List<string>();

      foreach (var taskParm in t.TaskParmSet.Values)
      {
        var parm = new Parm();
        parm.ParameterId = 0;
        parm.ScheduledTaskId = 0;
        parm.ParameterSetName = "Default";
        parm.ParameterName = taskParm.Key;
        parm.ParameterValue = taskParm.Value;
        parm.ParameterType = typeof(System.String);
        this.ParmSet.Add(parm);
      }

      this.TaskSchedule = new TaskSchedule();
      this.TaskSchedule.TaskScheduleId = 1;
      this.TaskSchedule.ScheduleName = "Default";
      this.TaskSchedule.ScheduleNumber = 1;
      this.TaskSchedule = this.TaskSchedule;

      foreach (var schedElement in t.TaskSchedule)
      {
        TaskScheduleElement taskScheduleElement = new TaskScheduleElement();
        taskScheduleElement.ScheduledTask = this;
        taskScheduleElement.TaskSchedule = this.TaskSchedule;
        taskScheduleElement.TaskScheduleElementId = this.TaskSchedule.TaskScheduleElements.Count + 1;
        taskScheduleElement.TaskScheduleId = this.TaskSchedule.TaskScheduleId;
        taskScheduleElement.IsActive = schedElement.IsActive;
        if (taskScheduleElement.IsActive)
          this.TaskSchedule.IsActive = true;
        taskScheduleElement.TaskExecutionType = schedElement.TaskExecutionType;
        taskScheduleElement.FrequencySeconds = schedElement.FrequencySeconds;
        taskScheduleElement.IsClockAligned = schedElement.IsClockAligned;
        taskScheduleElement.ScheduleElementPriority = 0;
        taskScheduleElement.StartDate = schedElement.TaskDateTime.StartDate;
        taskScheduleElement.StartTime = schedElement.TaskDateTime.StartTime;
        taskScheduleElement.EndDate = schedElement.TaskDateTime.EndDate;
        taskScheduleElement.EndTime = schedElement.TaskDateTime.EndTime;
        taskScheduleElement.IntervalType = schedElement.TaskDateTime.IntervalType;

        string weekdayControl = schedElement.TaskCalendar.WeekdayControl;
        if (weekdayControl.Length != 7)
          throw new Exception("Invalid WeekdayControl value in TaskCalendar for task '" + this.TaskName + "': " + weekdayControl + "' - length must be 7.");

        foreach (char c in weekdayControl)
        {
          if (c != 'T' && c != 'F')
            throw new Exception("Invalid WeekdayControl value in TaskCalendar for task '" + this.TaskName + "': " + weekdayControl +
                                " - valid values are 'T' and 'F' for each of the 7 week days.");
        }

        taskScheduleElement.OnSunday = weekdayControl[0] == 'T';
        taskScheduleElement.OnMonday = weekdayControl[1] == 'T';
        taskScheduleElement.OnTuesday = weekdayControl[2] == 'T';
        taskScheduleElement.OnWednesday = weekdayControl[3] == 'T';
        taskScheduleElement.OnThursday = weekdayControl[4] == 'T';
        taskScheduleElement.OnFriday = weekdayControl[5] == 'T';
        taskScheduleElement.OnSaturday = weekdayControl[6] == 'T';

        string ordinalControl = schedElement.TaskCalendar.OrdinalControl;
        if (ordinalControl.Length != 11)
          throw new Exception("Invalid OrdinalControl value in TaskCalendar for task '" + this.TaskName + "': " + ordinalControl + "' - length must be 12.");

        foreach (char c in ordinalControl)
        {
          if (c != 'T' && c != 'F')
            throw new Exception("Invalid OrdinalControl value in TaskCalendar for task '" + this.TaskName + "': " + ordinalControl +
                                " - valid values are 'T' and 'F' for each of the 11 values.");
        }

        taskScheduleElement.Every = ordinalControl[0] == 'T';
        taskScheduleElement.First = ordinalControl[1] == 'T';
        taskScheduleElement.Second = ordinalControl[2] == 'T';
        taskScheduleElement.Third = ordinalControl[3] == 'T';
        taskScheduleElement.Fourth = ordinalControl[4] == 'T';
        taskScheduleElement.Fifth = ordinalControl[5] == 'T';
        taskScheduleElement.Last = ordinalControl[6] == 'T';
        taskScheduleElement.OnWorkDays = ordinalControl[7] == 'T';
        taskScheduleElement.OnEvenDays = ordinalControl[8] == 'T';
        taskScheduleElement.OnOddDays = ordinalControl[9] == 'T';
        taskScheduleElement.ExceptSpecificDays = ordinalControl[10] == 'T';

        taskScheduleElement.SpecificDays = schedElement.TaskCalendar.SpecificDays;
        taskScheduleElement.HolidayActions = schedElement.TaskCalendar.HolidayActions;
        taskScheduleElement.PeriodContexts = schedElement.TaskCalendar.PeriodContexts;
        taskScheduleElement.MaxExecutions = schedElement.RunControl.MaxExecutions;
        taskScheduleElement.MaxRunTimeSeconds = schedElement.RunControl.MaxRunTimeSeconds;
        this.TaskSchedule.TaskScheduleElements.Add(taskScheduleElement);
      }
    }

    private CurrentPeriod Get_CurrentPeriod()
    {
      try
      {
        if (!this.RunUntilPeriodContextID.HasValue)
          return null;

        var periodContext = this.RunUntilPeriodContextID.Value.ToEnum<PeriodContexts>(PeriodContexts.NotSet);
        if (periodContext == PeriodContexts.NotSet)
          return null;

        int offsetMinutes = this.RunUntilOffsetMinutes.HasValue ? this.RunUntilOffsetMinutes.Value : 0;

        return new CurrentPeriod(periodContext, offsetMinutes);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurrred attempting to build the CurrentPeriod object for the scheduled task named '" + this.TaskName + "'.", ex);
      }
    }
  }
}

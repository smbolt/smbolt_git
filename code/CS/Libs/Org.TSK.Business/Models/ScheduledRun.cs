using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TSK.Business.Models
{
  public enum ScheduledRunStatus
  {
    Pending,
    SkippedLate,
    Enqueued,
    Executing,
    Completed
  }

  public enum ScheduledRunType
  {
    RunImmediate,
    RunAt,
    RunOnFrequency,
    RunOnDemand
  }

  public class ScheduledRun
  {
    public ScheduledTask ScheduledTask {
      get;
      set;
    }
    public TaskSchedule TaskSchedule {
      get;
      set;
    }
    public TaskScheduleElement TaskScheduleElement {
      get;
      set;
    }
    public DateTime ScheduledRunDateTime {
      get;
      set;
    }
    public ScheduledRunType ScheduledRunType {
      get;
      set;
    }
    public ScheduledRunStatus ScheduledRunStatus {
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

    public ScheduledRun(ScheduledTask scheduledTask, TaskSchedule taskSchedule, TaskScheduleElement taskScheduleElement, DateTime runDateTime, ScheduledRunType scheduledRunType)
    {
      this.ScheduledTask = scheduledTask;
      this.TaskSchedule = taskSchedule;
      this.ScheduledRunType = scheduledRunType;
      this.TaskScheduleElement = taskScheduleElement;
      this.ScheduledRunDateTime = runDateTime;
      this.ScheduledRunStatus = ScheduledRunStatus.Pending;
      this.TimeDispatched = DateTime.MinValue;
      this.TimeCompleted = DateTime.MinValue;
    }

    public TaskRequest ToTaskRequest()
    {
      TaskRequest taskRequest = new TaskRequest(
        this.ScheduledTask.ScheduledTaskId,
        this.ScheduledTask.TaskName,
        this.ScheduledTask.ProcessorName,
        this.ScheduledTask.ProcessorVersion,
        this.ScheduledTask.ProcessorTypeId,
        this.ScheduledTask.AssemblyName,
        this.ScheduledTask.CatalogName,
        this.ScheduledTask.CatalogEntry,
        this.ScheduledTask.ObjectTypeName,
        TaskRequestType.ScheduledTask,
        this.ScheduledTask.ParmSet,
        this.ScheduledRunDateTime,
        this.ScheduledTask.TrackHistory,
        this.ScheduledTask.RunUntilTask,
        this.ScheduledTask.RunUntilPeriodContextID,
        this.ScheduledTask.RunUntilOffsetMinutes,
        this.ScheduledTask.SuppressNotificationsOnSuccess
      );

      taskRequest.IsActive = true;

      return taskRequest;
    }
  }
}

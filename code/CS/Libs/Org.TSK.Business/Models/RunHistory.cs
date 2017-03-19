using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TSK.Business.Models
{
  public class RunHistory
  {
    public int RunId { get; set; }
    public int? PeriodHistoryId { get; set; }
    public int ScheduledTaskId { get; set; }
    public string TaskName { get; set; }
    public string ProcessorName { get; set; }
    public string ProcessorVersion { get; set; }
    public ProcessorType ProcessorType { get; set; }
    public ExecutionStatus ExecutionStatus { get; set; }
    public RunStatus RunStatus { get; set; }
    public int RunCode { get; set; }
    public bool NoWorkDone { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string RunHost { get; set; }
    public string RunUser { get; set; }
    public string Message { get; set; }
    public bool RunUntilTask { get; set; }
    public Period RunUntilPeriod { get; set; }
    public int? RunUntilOffsetMinutes { get; set; }
    public RunStats RunStats { get; set; }

    public RunHistory()
    {
      this.RunStats = new RunStats();
    }

    public RunHistory(TaskRequest taskRequest)
    {
      this.ScheduledTaskId = taskRequest.ScheduledTaskId;
      this.TaskName = taskRequest.TaskName;
      this.ProcessorName = taskRequest.ProcessorName;
      this.ProcessorVersion = taskRequest.ProcessorVersion;
      this.ProcessorType = taskRequest.ProcessorTypeId.ToEnum(ProcessorType.TaskEngine);
      this.ExecutionStatus = ExecutionStatus.Initiated;
      this.RunStatus = RunStatus.Initiated;
      this.RunCode = 0;
      this.NoWorkDone = false;
      this.StartDateTime = DateTime.Now;
      this.RunHost = g.SystemInfo.DomainAndComputer;
      this.RunUser = g.SystemInfo.DomainAndUser;
      this.Message = "Task Initiated";
      this.RunUntilTask = taskRequest.RunUntilTask;
      this.RunUntilPeriod = taskRequest.RunUntilPeriodContextId.ToEnum(Period.NotSet);
      this.RunUntilOffsetMinutes = taskRequest.RunUntilOffsetMinutes;

      this.RunStats = new RunStats();
    }

    public RunHistory(TaskResult taskResult)
    {
      if (taskResult.Object != null && taskResult.Object.GetType() == typeof(RunStats))
        this.RunStats = (RunStats)taskResult.Object;
      else
        this.RunStats = new RunStats();

      this.RunId = taskResult.OriginalTaskRequest.RunId.Value;
      this.StartDateTime = taskResult.BeginDateTime;
      this.EndDateTime = taskResult.EndDateTime;
      this.RunCode = taskResult.Code;
      this.NoWorkDone = taskResult.NoWorkDone;

      switch (taskResult.TaskResultStatus)
      {
        case TaskResultStatus.Success:
          this.ExecutionStatus = ExecutionStatus.Completed;
          this.RunStatus = RunStatus.Success;
          this.Message = "Task Completed Successfully";
          break;

        case TaskResultStatus.Warning:
          this.ExecutionStatus = ExecutionStatus.Completed;
          this.RunStatus = RunStatus.Warning;
          this.Message = "Task Completed with Warning";
          break;

        case TaskResultStatus.Failed:
          this.ExecutionStatus = ExecutionStatus.Completed;
          this.RunStatus = RunStatus.Failed;
          this.Message = "Task Completed with Error";
          break;

        case TaskResultStatus.Canceled:
          this.ExecutionStatus = ExecutionStatus.Completed;
          this.RunStatus = RunStatus.Canceled;
          this.Message = "Task Canceled";
          break;

        case TaskResultStatus.InProgress:
          this.ExecutionStatus = ExecutionStatus.InProgress;
          this.RunStatus = RunStatus.Processing;
          this.Message = "Task is currently processing";
          break;

        default:
          throw new Exception("Invalid TaskResultStatus '" + taskResult.TaskResultStatus.ToString() + "' to convert to RunStatus.");
      }
    }
  }
}

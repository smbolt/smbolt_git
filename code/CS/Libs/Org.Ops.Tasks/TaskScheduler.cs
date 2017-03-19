using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS.Performance;
using Org.Ops.Business;
using Org.TP.Concrete;
using Org.TSK.Business;
using Org.TSK.Business.Models;

namespace Org.Ops.Tasks
{
  public class TaskScheduler : TaskProcessorBase
  {
    private ConfigDbSpec _tasksDbSpec;
    private DateTime _sqlMinDateTime = new DateTime(1753, 1, 1);

    public override async Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();

      try
      {
        taskResult = await Task.Run<TaskResult>(() =>
        {
          //this.Initialize();

          //_tasksDbSpec = GetParmValue("TasksDbSpec") as ConfigDbSpec;

          //TimeSpan interval = TimeSpan.Parse(GetParmValue("Interval").ToString());

          //var scheduledTasks = new List<ScheduledTask>();

          //using (var repo = new TaskRepository(_tasksDbSpec))
          //{
          //  repo.DeleteScheduledRunsNotStarted(IsDryRun);
          //  scheduledTasks = repo.GetTasksForScheduling();
          //} 

          //var scheduledPeriodSet = GetScheduledPeriodsAndRuns(scheduledTasks, interval);

          //using (var repo = new TaskRepository(_tasksDbSpec))
          //  repo.AddScheduledPeriods(scheduledPeriodSet.Values.ToList(), IsDryRun);

          //if (IsDryRun)
          //  taskResult.NoWorkDone = true;

          return taskResult.Success();
        });     

        return taskResult;
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing.", ex);
      }
    }

    //public ScheduledPeriodSet GetScheduledPeriodsAndRuns(List<ScheduledTask> tasks, TimeSpan interval)
    //{
    //  try
    //  {
    //    var periodSet = new ScheduledPeriodSet();

    //    foreach (var task in tasks)
    //    {
    //      if (!task.IsActive)
    //        continue;

    //      List<ScheduledRun> runSet = GetRunsFromSchedule(task, interval);
          
    //      if (!task.RunUntilTask)
    //      {
    //        var currentRuns = new List<ScheduledRun>();

    //        string newPeriodKey = task.ScheduledTaskId + "|" + _sqlMinDateTime.ToString("yyyyMMdd-HHmmss");
    //        foreach (var run in runSet)
    //        {
    //          if (periodSet.ContainsKey(newPeriodKey))
    //          {
    //            var period = periodSet[newPeriodKey];
    //            if (!period.ScheduledRuns.ContainsKey(run.ScheduledRunDateTime))
    //              period.ScheduledRuns.Add(run.ScheduledRunDateTime, run);
    //          }
    //          else
    //          {
    //            var period = new PeriodHistory(task, _sqlMinDateTime, DateTime.MaxValue);
    //            period.ScheduledRuns.Add(run.ScheduledRunDateTime, run);

    //            periodSet.Add(newPeriodKey, period);
    //          }
    //        }
    //        continue;
    //      }

    //      foreach (var run in runSet)
    //      {
    //        DateTime startDate = run.ScheduledRunDateTime.Date;

    //        int offsetMinutes = task.RunUntilOffsetMinutes.HasValue ? task.RunUntilOffsetMinutes.Value : 0;

    //        DateTime periodStart;
    //        DateTime periodEnd;
    //        switch (task.RunUntilPeriodContextID.ToEnum<PeriodContexts>(PeriodContexts.NotSet))
    //        {
    //          case PeriodContexts.Day:
    //            periodStart = startDate.AddMinutes(offsetMinutes);
    //            periodEnd = periodStart.AddDays(1).AddMilliseconds(-1);
    //            break;

    //          case PeriodContexts.Week:
    //            int diff = startDate.DayOfWeek - DayOfWeek.Sunday;
    //            periodStart = startDate.AddDays(-1 * diff).AddMinutes(offsetMinutes);
    //            periodEnd = periodStart.AddDays(7).AddMilliseconds(-1);
    //            break;

    //          case PeriodContexts.Month:
    //            periodStart = startDate.FirstDayOfMonth().AddMinutes(offsetMinutes);
    //            periodEnd = periodStart.AddMonths(1).AddMilliseconds(-1);
    //            break;

    //          case PeriodContexts.Quarter:
    //            int quarter = startDate.Month / 3 + 1;
    //            periodStart = new DateTime(startDate.Year, quarter * 3 - 2, 1).AddMinutes(offsetMinutes);
    //            periodEnd = periodStart.AddMonths(3).AddMilliseconds(-1);
    //            break;

    //          case PeriodContexts.Year:
    //            periodStart = new DateTime(startDate.Year, 1, 1).AddMinutes(offsetMinutes);
    //            periodEnd = periodStart.AddYears(1).AddMilliseconds(-1);
    //            break;

    //          default:
    //            continue;
    //        }

    //        string newPeriodKey = task.ScheduledTaskId + "|" + periodStart.ToString("yyyyMMdd-HHmmss");
    //        //if new run's period was newly created
    //        if (periodSet.ContainsKey(newPeriodKey))
    //        {
    //          var period = periodSet[newPeriodKey];

    //          //if run doesn't already exist
    //          if (!period.ScheduledRuns.ContainsKey(run.ScheduledRunDateTime))
    //            period.ScheduledRuns.Add(run.ScheduledRunDateTime, run);
    //        }
    //        //create new period and add run
    //        else
    //        {
    //          var period = new PeriodHistory(task, periodStart, periodEnd);
    //          period.ScheduledRuns.Add(run.ScheduledRunDateTime, run);

    //          periodSet.Add(newPeriodKey, period);
    //        }
    //      }
    //    }

    //    return periodSet;
    //  }
    //  catch (Exception ex)
    //  {
    //    throw new Exception("An exception occurred attempting to build the ScheduleRuns for the ScheduledTasks.", ex);
    //  }
    //}

    //public List<ScheduledRun> GetRunsFromSchedule(ScheduledTask task, TimeSpan interval)
    //{
    //  List<ScheduledRun> runSet = new List<ScheduledRun>();

    //  foreach (var e in task.TaskSchedule.TaskScheduleElements.Where(t => t.IsActive))
    //  {
    //    ScheduleDateControl sdc = new ScheduleDateControl(e);
    //    List<DateTime> scheduleRunDateTimes = new List<DateTime>();

    //    switch (e.TaskExecutionType)
    //    {
    //      case TaskExecutionType.RunOnFrequency:
    //      case TaskExecutionType.RunImmediateAndOnFrequency:
    //        scheduleRunDateTimes.AddRange(sdc.GetScheduleOnFrequency(interval, false));
    //        foreach (var runDateTime in scheduleRunDateTimes)
    //          runSet.Add(new ScheduledRun(task, runDateTime, ScheduledRunType.RunOnFrequency));
    //        break;

    //      case TaskExecutionType.RunOnceAt:
    //        if (!e.RunAtHasBeenScheduled)
    //        {
    //          // Add the RunAt ScheduledRun
    //          if (!e.StartDateTime.HasValue)
    //            throw new Exception("RunAt task does not have valid StartDateTime property in Task '" + task.TaskName + ", " +
    //                                  "Schedule '" + task.TaskSchedule.ScheduleName + "', ScheduleElementId '" + e.TaskScheduleElementId.ToString() + "'.");
    //          if (e.StartDateTime > DateTime.Now)
    //          {
    //            runSet.Add(new ScheduledRun(task, e.StartDateTime.Value, ScheduledRunType.RunAt));
    //            e.RunAtHasBeenScheduled = true;
    //          }
    //        }
    //        break;

    //      case TaskExecutionType.RunAtAndOnFrequency:
    //        // add the RunAt ScheduledRun
    //        if (!e.RunAtHasBeenScheduled)
    //        {
    //          if (!e.StartDateTime.HasValue)
    //            throw new Exception("RunAt task does not have valid StartDateTime property in Task '" + task.TaskName + ", " +
    //                                "Schedule '" + task.TaskSchedule.ScheduleName + "', ScheduleElementId '" + e.TaskScheduleElementId.ToString() + "'.");
    //          if (e.StartDateTime > DateTime.Now)
    //          {
    //            runSet.Add(new ScheduledRun(task, e.StartDateTime.Value, ScheduledRunType.RunAt));
    //            e.RunAtHasBeenScheduled = true;
    //          }
    //        }

    //        // Add the OnFrequency ScheduledRuns
    //        scheduleRunDateTimes.AddRange(sdc.GetScheduleOnFrequency(interval, true));
    //        foreach (var runDateTime in scheduleRunDateTimes)
    //        {
    //          if (!runSet.Any(r => r.ScheduledRunDateTime == runDateTime))
    //            runSet.Add(new ScheduledRun(task, runDateTime, ScheduledRunType.RunOnFrequency));
    //        }
    //        break;
    //    }
    //  }

    //  return runSet;
    //}

    protected override void Initialize()
    {
      base.Initialize();

      this.AssertParmExistence("TasksDbSpec");
      this.AssertParmExistence("Interval");
    }
  }
}

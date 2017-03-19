using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;

namespace Org.TSK.Business.Models
{
  public class PeriodHistory
  {
    public int ScheduledPeriodId { get; set; }
    public int ScheduledTaskId { get; set; }
    public string TaskName { get; set; }
    public bool RunForPeriod { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public DateTime? OverdueDateTime { get; set; }
    public bool RunUntilTask { get; set; }
    public int? RunUntilPeriodContextId { get; set; }
    public int? RunUntilOffsetMinutes { get; set; }
    public bool OverdueNotificationSent { get; set; }
    public bool OverdueNoticeAcknowledged { get; set; }
    public string AcknowledgedBy { get; set; }
    public DateTime? AcknowledgedDate { get; set; }

    public SortedList<DateTime, RunHistory> RunHistory { get; set; }

    public PeriodHistory()
    {
      this.ScheduledPeriodId = 0;
      this.ScheduledTaskId = 0;
      this.TaskName = String.Empty;
      this.RunForPeriod = false;
      this.StartDateTime = DateTime.MinValue;
      this.EndDateTime = DateTime.MaxValue;
      this.RunUntilTask = false;
      this.OverdueNotificationSent = false;
      this.OverdueNoticeAcknowledged = false;

      this.RunHistory = new SortedList<DateTime, RunHistory>();
    }

    public PeriodHistory(ScheduledTask scheduledTask, DateTime startDateTime, DateTime endDateTime)
    {
      this.ScheduledPeriodId = 0;
      this.ScheduledTaskId = scheduledTask.ScheduledTaskId;
      this.TaskName = scheduledTask.TaskName;
      this.RunForPeriod = false;
      this.StartDateTime = startDateTime;
      this.EndDateTime = endDateTime;
      //need to add overdueDateTime here
      this.RunUntilTask = scheduledTask.RunUntilTask;
      this.RunUntilPeriodContextId = scheduledTask.RunUntilPeriodContextID;
      this.RunUntilOffsetMinutes = scheduledTask.RunUntilOffsetMinutes;
      this.OverdueNotificationSent = false;
      this.OverdueNoticeAcknowledged = false;

      this.RunHistory = new SortedList<DateTime, RunHistory>();
    }

    public PeriodHistory(TaskRequest taskRequest)
    {
      this.ScheduledTaskId = taskRequest.ScheduledTaskId;
      this.TaskName = taskRequest.TaskName;
      this.RunForPeriod = false;
      this.RunUntilTask = taskRequest.RunUntilTask;
      this.RunUntilPeriodContextId = taskRequest.RunUntilPeriodContextId;
      this.RunUntilOffsetMinutes = taskRequest.RunUntilOffsetMinutes;
      this.OverdueNotificationSent = false;
      this.OverdueNoticeAcknowledged = false;

      SetStartEndDateTime(taskRequest);
      this.RunHistory = new SortedList<DateTime, RunHistory>();

      //need to add OverdueDateTime
    }

    private void SetStartEndDateTime(TaskRequest taskRequest)
    {
      DateTime startDate = taskRequest.ScheduledRunDateTime.Date;

      int offsetMinutes = this.RunUntilOffsetMinutes.HasValue ? this.RunUntilOffsetMinutes.Value : 0;

      DateTime periodStart;
      DateTime periodEnd;
      switch (this.RunUntilPeriodContextId.ToEnum<PeriodContexts>(PeriodContexts.NotSet))
      {
        case PeriodContexts.Day:
          periodStart = startDate.AddMinutes(offsetMinutes);
          periodEnd = periodStart.AddDays(1).AddMilliseconds(-1);
          break;

        case PeriodContexts.Week:
          int diff = startDate.DayOfWeek - DayOfWeek.Sunday;
          periodStart = startDate.AddDays(-1 * diff).AddMinutes(offsetMinutes);
          periodEnd = periodStart.AddDays(7).AddMilliseconds(-1);
          break;

        case PeriodContexts.Month:
          periodStart = startDate.FirstDayOfMonth().AddMinutes(offsetMinutes);
          periodEnd = periodStart.AddMonths(1).AddMilliseconds(-1);
          break;

        case PeriodContexts.Quarter:
          int quarter = startDate.Month / 3 + 1;
          periodStart = new DateTime(startDate.Year, quarter * 3 - 2, 1).AddMinutes(offsetMinutes);
          periodEnd = periodStart.AddMonths(3).AddMilliseconds(-1);
          break;

        case PeriodContexts.Year:
          periodStart = new DateTime(startDate.Year, 1, 1).AddMinutes(offsetMinutes);
          periodEnd = periodStart.AddYears(1).AddMilliseconds(-1);
          break;

        default:
          return;
      }

      this.StartDateTime = periodStart;
      this.EndDateTime = periodEnd;
    }
  }
}

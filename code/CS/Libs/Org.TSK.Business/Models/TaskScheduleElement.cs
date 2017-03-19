using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;
using Org.TSK.Business.Models;

namespace Org.TSK.Business.Models
{
  public class TaskScheduleElement
  {
    public ScheduledTask ScheduledTask { get; set; }
    public TaskSchedule TaskSchedule { get; set; }
    public int TaskScheduleElementId { get; set; }
    public int TaskScheduleId { get; set; }
    public bool IsActive { get; set; }
    public bool IsManaged { get; set; }
    public int? ElementNumber { get; set; }
    public TaskExecutionType TaskExecutionType { get; set; }
    public decimal? FrequencySeconds { get; set; }
    public bool IsClockAligned { get; set; }
    public int? ScheduleElementPriority { get; set; }
    public int QueuePriority { get; set; }
    public int? MaxQueueCount { get; set; }
    public bool BypassQueue { get; set; }
    public DateTime? StartDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeSpan? EndTime { get; set; }
    public IntervalType IntervalType { get; set; }
    public bool OnSunday { get; set; }
    public bool OnMonday { get; set; }
    public bool OnTuesday { get; set; }
    public bool OnWednesday { get; set; }
    public bool OnThursday { get; set; }
    public bool OnFriday { get; set; }
    public bool OnSaturday { get; set; }
    public bool OnWorkDays { get; set; }
    public bool OnEvenDays { get; set; }
    public bool OnOddDays { get; set; }
    public string SpecificDays { get; set; }
    public bool ExceptSpecificDays { get; set; }
    public bool First { get; set; }
    public bool Second { get; set; }
    public bool Third { get; set; }
    public bool Fourth { get; set; }
    public bool Fifth { get; set; }
    public bool Last { get; set; }
    public bool Every { get; set; }
    public HolidayActions HolidayActions { get; set; }
    public PeriodContexts PeriodContexts { get; set; }
    public int? ExecutionLimit { get; set; }
    public int? MaxExecutions { get; set; }
    public int TotalExecutions { get; set; }
    public int? MaxRunTimeSeconds { get; set; }
    public bool RunImmediateHasBeenScheduled { get; set; }
    public bool RunAtHasBeenScheduled { get; set; }
    public DateTime RunThroughDateTime { get; set; }
    public bool IsScheduleBasedOnFrequency { get { return Get_IsScheduleBasedOnFrequency(); } }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public DateTime? StartDateTime { get { return Get_StartDateTime(); } }

    public TaskScheduleElement()
    {
      this.TotalExecutions = 0;
      this.RunImmediateHasBeenScheduled = false;
      this.RunAtHasBeenScheduled = false;
      this.RunThroughDateTime = DateTime.MinValue;
    }

    public DateTime? Get_StartDateTime()
    {
      if (!this.StartTime.HasValue)
        return null;

      if (!this.StartDate.HasValue)
      {
        var dt = DateTime.Now;
        var startDate = new DateTime(dt.Year, dt.Month, dt.Day);
        return startDate.Add(this.StartTime.Value);
      }

      return this.StartDate.Value.Add(this.StartTime.Value); 
    }        

    private bool Get_IsScheduleBasedOnFrequency()
    {
      switch (this.TaskExecutionType)
      {
        case TaskExecutionType.RunOnFrequency:
        case TaskExecutionType.RunImmediateAndOnFrequency:
          return true;

        default:
          return false;
      }
    }

    public string GetCondensedReport()
    {
      StringBuilder sb = new StringBuilder();

      string active = this.IsActive ? "Y" : "N";
      string interval;
      switch (this.IntervalType)
      {
        case IntervalType.DailyInterval: interval = "DI"; break;
        case IntervalType.SingleSpan: interval = "SS"; break;
        default: interval = "NS"; break;
      }
      string startDate = this.StartDate.HasValue ? this.StartDate.Value.ToString("yyyyMMdd") : "";
      string endDate = this.EndDate.HasValue ? this.EndDate.Value.ToString("yyyyMMdd") : "";
      string startTime = this.StartTime.HasValue ? this.StartTime.Value.ToString() : "";
      string endTime = this.EndTime.HasValue ? this.EndTime.Value.ToString() : "";
      string occurrence = (this.First ? "X" : " ") + (this.Second ? "X" : " ") + (this.Third ? "X" : " ") + (this.Fourth ? "X" : " ") +
                          (this.Fifth ? "X" : " ") + (this.Last ? "X" : " ") + (this.Every ? "X" : " ");
      string dayOfWeek = (this.OnSunday ? "X" : " ") + (this.OnMonday ? "X" : " ") + (this.OnTuesday ? "X" : " ") + (this.OnWednesday ? "X" : " ") +
                         (this.OnThursday ? "X" : " ") + (this.OnFriday ? "X" : " ") + (this.OnSaturday ? "X" : " ") + (this.OnWorkDays ? "X" : " ") +
                         (this.OnEvenDays ? "X" : " ") + (this.OnOddDays ? "X" : " ");
      string frequency = string.Format("{0:N3}", this.FrequencySeconds);

      sb.Append("Active Interval StartDate EndDate  StartTime EndTime  12345LE Period  SMTWTFSWEO Freq(sec)" + g.crlf +
                "  " + active.PadTo(8) + interval.PadTo(6) + startDate.PadTo(10) + endDate.PadTo(9) + startTime.PadTo(10) +
                endTime.PadTo(9) + occurrence.PadTo(8) + this.PeriodContexts.ToString().PadTo(8) + dayOfWeek.PadTo(11) + frequency);

      return sb.ToString();
    }
  }
}

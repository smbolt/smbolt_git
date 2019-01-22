using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;
using Org.TSK.Business;

namespace Org.TSK.Business.Models
{
  public class ScheduleDateControl
  {
    private TaskScheduleElement _taskScheduleElement; 

    public DateTime BaseDate { get; private set; }
    public StartDateOptions StartDateOptions { get; private set; }
    public EndDateOptions EndDateOptions { get; private set; }

    public DateTime StartDateTime { get; private set; }
    public DateTime StartDate { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public DateTime EndDateTime { get; private set; }
    public DateTime EndDate { get; private set; }
    public TimeSpan EndTime { get; private set; }

    private DateTime? _startDate;
    private TimeSpan? _startTime;
    private DateTime? _endDate;
    private TimeSpan? _endTime;
    private IntervalType _intervalType;

    public ScheduleDateControl(TaskScheduleElement taskScheduleElement)
    {
      _taskScheduleElement = taskScheduleElement;
      this.BaseDate = _taskScheduleElement.ScheduledTask.BaseDateTime;
      _startDate = _taskScheduleElement.StartDate;
      _startTime = _taskScheduleElement.StartTime;
      _endDate = _taskScheduleElement.EndDate;
      _endTime = _taskScheduleElement.EndTime;
      _intervalType = g.ToEnum<IntervalType>(_taskScheduleElement.IntervalType, IntervalType.DailyInterval); 

      AdjustBaseDate();

      InitializeControl();
    }

    private void AdjustBaseDate()
    {
      if (_startTime == null)
        return;

      if (_taskScheduleElement.TaskExecutionType != TaskExecutionType.RunImmediateAndOnFrequency &&
          _taskScheduleElement.TaskExecutionType != TaskExecutionType.RunAtAndOnFrequency &&
          _taskScheduleElement.TaskExecutionType != TaskExecutionType.RunOnFrequency)
        return;

      DateTime? startDate = _startDate;

      if (!startDate.HasValue)
        startDate = DateTime.Now.Date();

      this.BaseDate = startDate.Value.Add(_startTime.Value); 
    }

    public List<DateTime> GetScheduleOnFrequency(DateTime intervalStart, DateTime intervalEnd, TimeIntervalSet exclusionIntervals, bool runUntilOverride, bool skipBaseDateTime)
    {
      double frequency = (double)_taskScheduleElement.FrequencySeconds;

      if (frequency < 1)
      {
        throw new Exception("The task " + _taskScheduleElement.ScheduledTask.TaskName + " is set to run on a frequency schedule. The frequency seconds cannot be set to zero." + g.crlf +
                            "Please update the schedule elements.");
      }

      List<DateTime> runTimes = new List<DateTime>();

      DateTime startDt = skipBaseDateTime ? this.BaseDate.AddSeconds(frequency) :
                                            this.BaseDate;
      if (this.StartDateOptions == StartDateOptions.UseStartDateTime)
      {
        if (this.StartDateTime != DateTime.MinValue)
          startDt = this.StartDateTime;
      }

      while (startDt < intervalStart)
        startDt = startDt.AddSeconds(frequency);
          
      if (startDt < _taskScheduleElement.RunThroughDateTime)
        startDt = _taskScheduleElement.RunThroughDateTime;

      DateTime endDt = DateTime.MaxValue;
      if (this.EndDateOptions == EndDateOptions.UseEndDateTime)
        endDt = this.EndDateTime;

      if (endDt > intervalEnd)
        endDt = intervalEnd;

      DateTime runTime = startDt;

      int count = 0;
      int loopCount = 0;

      while (runTime <= endDt)
      {
        loopCount++;
        if (loopCount > 5000)
          break;

        if (this.InActiveInterval(runTime) && (runUntilOverride || !exclusionIntervals.Contains(runTime)))
        {
          if (runUntilOverride)
            runUntilOverride = false;
          runTimes.Add(runTime);
          count++;
        }

        if (runTimes.Count > 1000)
          break;

        runTime = runTime.AddSeconds(frequency);
      }

      return runTimes;
    }

    public bool InActiveInterval(DateTime dt)
    {
      var e = _taskScheduleElement;

      switch (_intervalType)
      {
        case IntervalType.DailyInterval:
          DateTime runDate = new DateTime(dt.Year, dt.Month, dt.Day);
          TimeSpan runTime = new TimeSpan(0, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);

          if (runDate < this.StartDate || runDate > this.EndDate)
            return false;

          if (runTime < this.StartTime || runTime > this.EndTime)
            return false;

          break;

        case IntervalType.SingleSpan:
          if (dt >= this.StartDateTime && dt <= this.EndDateTime)
            break;
          return false;
      }

      bool dayIncluded = false;

      // check if specific days to run are specified
      if (e.SpecificDays.IsNotBlank())
      {
        int dayOfMonth = dt.Day;
        List<int> dayList = new List<int>();
        string days = e.SpecificDays.Replace(" ", String.Empty);
        List<string> dayString = days.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
        foreach (string s in dayString)
          dayList.Add(Int32.Parse(s));
        dayList.Sort();

        if (e.ExceptSpecificDays && dayList.Contains(dayOfMonth))
          return false;

        if (!e.ExceptSpecificDays && dayList.Contains(dayOfMonth))
          dayIncluded = true;
      }

      // check if day of week is included
      RunDay runDay = new RunDay(dt);
      switch (runDay.DayOfWeekInt)
      {
        case 0: if (e.OnSunday) dayIncluded = true; break;
        case 1: if (e.OnMonday) dayIncluded = true; break;
        case 2: if (e.OnTuesday) dayIncluded = true; break;
        case 3: if (e.OnWednesday) dayIncluded = true; break;
        case 4: if (e.OnThursday) dayIncluded = true; break;
        case 5: if (e.OnFriday) dayIncluded = true; break;
        case 6: if (e.OnSaturday) dayIncluded = true; break;
      }

      if ((runDay.IsEven && e.OnEvenDays) || (runDay.IsOdd && e.OnOddDays))
        dayIncluded = true;

      if (!dayIncluded)
        return false;
      
      // Need to consider that the RunDay might be the "Third" weekday and we cannot exclude it just because e.First is true, e.Third might also be true
      // That is when e.First is true and RunDay.First is false, we cant exclude it because e.Third (first or third) might be try and RunDay.Third being true
      // would qualify the RunDate.

      return true;
    }

    private void InitializeControl()
    {
      this.StartDateTime = DateTime.MinValue;
      this.StartTime = new TimeSpan(0, 0, 0);
      this.EndDateTime = DateTime.MaxValue;
      this.EndTime = new TimeSpan(0, 23, 59, 59, 999); 

      DateTime startDate = DateTime.MinValue;
      TimeSpan startTime = new TimeSpan(0, 0, 0);

      if (_startDate.HasValue)
        startDate = new DateTime(_startDate.Value.Year, _startDate.Value.Month, _startDate.Value.Day);

      if (_startTime.HasValue)
        startTime = new TimeSpan(_startTime.Value.Hours, _startTime.Value.Minutes, 0); 

      DateTime endDate = DateTime.MaxValue;
      TimeSpan endTime = new TimeSpan(0, 23, 59, 59, 999);

      if (_endDate.HasValue)
        endDate = new DateTime(_endDate.Value.Year, _endDate.Value.Month, _endDate.Value.Day);

      if (_endTime.HasValue)
        endTime = new TimeSpan(0, _endTime.Value.Hours, _endTime.Value.Minutes, 59, 999);            

      if (_startDate.HasValue)
      {
        this.StartDateTime = startDate.Add(startTime);
        this.StartTime = startTime;
        this.StartDateOptions = StartDateOptions.UseStartDateTime;
      }
      else
      {
        if (_startTime.HasValue)
        {
          this.StartDateTime = DateTime.MinValue;
          this.StartTime = startTime;
          this.StartDateOptions = StartDateOptions.UseStartTimeOnly;
        }
        else
        {
          this.StartDateTime = DateTime.MinValue;
          this.StartTime = startTime;
          this.StartDateOptions = StartDateOptions.UseStartDateTime;
        }
      }

      if (_endDate.HasValue)
      {
        this.EndDateTime = endDate.Add(endTime);
        this.EndTime = endTime;
        this.EndDateOptions = EndDateOptions.UseEndDateTime;
      }
      else
      {
        if (_endTime.HasValue)
        {
          this.EndTime = endTime;
          this.EndDateOptions = EndDateOptions.UseEndTimeOnly;
        }
        else
        {
          this.EndDateTime = DateTime.MaxValue;
          this.EndTime = endTime;
          this.EndDateOptions = EndDateOptions.UseEndDateTime;
        }
      }

      this.StartDate = new DateTime(this.StartDateTime.Year, this.StartDateTime.Month, this.StartDateTime.Day);
      this.EndDate = new DateTime(this.EndDateTime.Year, this.EndDateTime.Month, this.EndDateTime.Day); 
    }
  }
}

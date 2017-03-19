using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;

namespace Org.TSK.Business.Models
{
  public class ScheduledTaskSet : List<ScheduledTask>
  {
    private SortedList<UInt64, ScheduledRun> _tasksToRun;
    private object TaskSet_LockObject = new object();

    public DateTime NextLoadTime { get; set; }
    public bool MoreTasksToProcess { get { return Get_MoreTasksToProcess(); } }
    public int  TasksToProcessCount{ get { return Get_TasksToProcessCount(); } }

    public ScheduledTaskSet()
    {
      _tasksToRun = new SortedList<UInt64, ScheduledRun>();
      this.NextLoadTime = DateTime.MinValue;
    }

    public int GetWaitInterval(int maxInterval)
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskSet_LockObject, 1000))
      {
        try
        {
          // if no tasks, default to waiting 1 second to check again
          if (_tasksToRun.Count == 0)
            return maxInterval;

          ScheduledRun sr = _tasksToRun.Values.ElementAt(0);
          TimeSpan ts = sr.ScheduledRunDateTime - DateTime.Now;

          int waitMilliseconds = (int) ts.TotalMilliseconds;

          if (waitMilliseconds < 0)
            return 0;
          else
            if (waitMilliseconds > maxInterval)
              return maxInterval;

          return waitMilliseconds;
        }
        finally
        {
          Monitor.Exit(TaskSet_LockObject);
        }
      }

      return maxInterval;
    }

    public ScheduledRun GetNext(int maxInterval)
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskSet_LockObject, 1000))
      {
        try
        {
          if (_tasksToRun.Count == 0)
            return null;

          ScheduledRun sr = _tasksToRun.Values.ElementAt(0);
          TimeSpan ts = sr.ScheduledRunDateTime - DateTime.Now;

          int taskInterval = (int) ts.TotalMilliseconds;

          if (taskInterval > maxInterval)
            return null;

          _tasksToRun.RemoveAt(0);

          sr.TaskScheduleElement.RunThroughDateTime = sr.ScheduledRunDateTime;
          return sr;
        }
        finally
        {
          Monitor.Exit(TaskSet_LockObject);
        }
      }

      return null;
    }

    public ScheduledRun PeekNext()
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskSet_LockObject, 1000))
      {
        try
        {
          if (_tasksToRun.Count == 0)
            return null;

          ScheduledRun sr = _tasksToRun.Values.ElementAt(0);
          TimeSpan ts = sr.ScheduledRunDateTime - DateTime.Now;

          return sr;
        }
        finally
        {
          Monitor.Exit(TaskSet_LockObject);
        }
      }

      return null;
    }

    private bool Get_MoreTasksToProcess()
    {
      // Lock this object, wait for up to 5 seconds
      if (Monitor.TryEnter(TaskSet_LockObject, 5000))
      {
        try
        {
          return _tasksToRun.Count == 0;
        }
        finally
        {
          Monitor.Exit(TaskSet_LockObject);
        }
      }

      // may want to throw an exception here
      return false;
    }

    private int Get_TasksToProcessCount()
    {
      // Lock this object, wait for up to 5 seconds
      if (Monitor.TryEnter(TaskSet_LockObject, 5000))
      {
        try
        {
          return _tasksToRun.Count;
        }
        finally
        {
          Monitor.Exit(TaskSet_LockObject);
        }
      }

      // may want to throw an exception here
      return 0;
    }

    public void LoadTasksToRun(int loadIntervalSeconds)
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskSet_LockObject, 1000))
      {
        try
        {
          DateTime now = DateTime.Now;
          int nextLoadIntervalSeconds = (int)(loadIntervalSeconds * 0.8F);
          this.NextLoadTime = now.AddSeconds(nextLoadIntervalSeconds);

          LoadTasksToRun(now, now.AddSeconds(loadIntervalSeconds), false);
        }
        finally
        {
          Monitor.Exit(TaskSet_LockObject);
        }
      }
    }

    public void LoadTasksToRun(DateTime intervalStart, DateTime intervalEnd, bool viewOnly)
    {
      if (intervalEnd - intervalStart > new TimeSpan(90, 0, 0, 0, 0))
        throw new Exception("Cannot LoadTasksToRun with an interval greater than 90 days." + g.crlf +
                            "IntervalStart: " + intervalStart.ToString("yyyy-MM-dd HH:mm:ss") + g.crlf +
                            "IntervalEnd:   " + intervalEnd.ToString("yyyy-MM-dd HH:mm:ss"));

      _tasksToRun = new SortedList<ulong, ScheduledRun>();

      foreach (var task in this)
      {
        if (!task.IsActive)
          continue;

        ScheduledRunSet runSet = task.GetScheduledRunSet(intervalStart, intervalEnd, viewOnly);

        foreach (var scheduledRun in runSet)
        {
          UInt64 longSortKey = scheduledRun.Key.ToLongSortKey();

          while (_tasksToRun.ContainsKey(longSortKey))
            longSortKey++;

          if (scheduledRun.Value.ScheduledRunType == ScheduledRunType.RunImmediate ||
                      scheduledRun.Value.ScheduledRunDateTime > DateTime.Now)
            _tasksToRun.Add(longSortKey, scheduledRun.Value);
        }
      }
    }

    public List<TaskRequest> GetTaskRequests()
    {
      var taskRequests = new List<TaskRequest>();

      foreach (var scheduledRun in _tasksToRun.Values)
        taskRequests.Add(scheduledRun.ToTaskRequest());

      return taskRequests;
    }

    public List<ScheduledRun> GetScheduledRuns()
    {
      return _tasksToRun.Values.ToList();
    }

    public int? GetShortestFrequencySeconds()
    {
      int? shortestFrequencySeconds = null;

      foreach (var scheduledTask in this)
      {
        if (scheduledTask.TaskSchedule != null)
        {
          foreach (var taskScheduleElement in scheduledTask.TaskSchedule.TaskScheduleElements)
          {
            if (taskScheduleElement.IsScheduleBasedOnFrequency)
            {
              int frequency = (int) taskScheduleElement.FrequencySeconds.Value;
              if (shortestFrequencySeconds.HasValue)
              {
                if (frequency < shortestFrequencySeconds.Value)
                  shortestFrequencySeconds  = frequency;
              }
              else
                shortestFrequencySeconds = frequency;
            }
          }
        }
      }

      return shortestFrequencySeconds;
    }

    public string GetParametersReport()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("SCHEDULED TASKS" + g.crlf2);
      foreach (var scheduledTask in this)
      {
        if (!scheduledTask.IsActive)
          continue; 

        sb.Append(scheduledTask.TaskName + ": " +
                  scheduledTask.ProcessorName + " " +
                  scheduledTask.ProcessorVersion.PadTo(30) + g.crlf +
                  "--------------------------------------------------------------------------------------------------------" + g.crlf +
                  "Schedule Name: " + scheduledTask.TaskSchedule.ScheduleName + g.crlf);
        foreach (var taskScheduleElement in scheduledTask.TaskSchedule.TaskScheduleElements)
        {
          sb.Append(taskScheduleElement.GetCondensedReport() + g.crlf);
        }
        sb.Append(g.crlf);
        foreach (var parm in scheduledTask.ParmSet)
        {
          sb.Append("  " + parm.ParameterId.ToString("00000").PadTo(8) +
                    parm.ParameterName.PadTo(40) + "  ");

          switch (parm.ParameterType.Name)
          {
            case "String":
              sb.Append(parm.ParameterValue.ToString());
              break;

            case "Int32":
              sb.Append(parm.ParameterValue.ToInt32());
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
              sb.Append("Dictionary<string, string>");
              foreach (var kvp in dict)
              {
                sb.Append(g.crlf + String.Empty.PadTo(12) + kvp.Key.PadTo(40) + "  " + kvp.Value);
              }
              break;

            default:
              sb.Append("[Type:" + parm.ParameterType.Name + "] " + parm.ParameterValue.ToString());
              break;
          }
          sb.Append(g.crlf);
        }
        sb.Append(g.crlf);
      }
      return sb.ToString();
    }
  }
}

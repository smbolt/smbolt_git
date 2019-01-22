using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Org.GS
{
  public class TaskRequestSet : SortedList<UInt64, TaskRequest>
  {
    public SortedList<UInt64, TaskRequest> _runningTasks;
    private object TaskRequestSet_LockObject = new object();

    public DateTime NextLoadTime { get; set; }
    public bool MoreTasksToProcess { get { return Get_MoreTasksToProcess(); } }
    public int TasksToProcessCount { get { return Get_TasksToProcessCount(); } }
    public int WaitInterval { get { return Get_WaitInterval(); } }
    public string TaskReport { get { return Get_TaskReport(); } }
    private int _maxWaitInterval;

    public TaskRequestSet(int maxWaitInterval)
    {
      _maxWaitInterval = maxWaitInterval;
      _runningTasks = new SortedList<UInt64, TaskRequest>();
      this.NextLoadTime = DateTime.MinValue;
    }

    public void AddTaskRequests(List<TaskRequest> taskRequests)
    {
      foreach (var taskRequest in taskRequests)
        this.AddTaskRequest(taskRequest); 
    }

    public void AddTaskRequest(TaskRequest taskRequest)
    {
      if (taskRequest == null)
        return;

      UInt64 longSortKey = taskRequest.ScheduledRunDateTime.ToLongSortKey();

      while (this.ContainsKey(longSortKey))
        longSortKey++;

      taskRequest.TaskRequestId = longSortKey.ToString();

      this.Add(longSortKey, taskRequest);
    }

    public void AddRunningTask(TaskRequest runningTask)
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          UInt64 longSortKey = runningTask.ScheduledRunDateTime.ToLongSortKey();
          if (!_runningTasks.ContainsKey(longSortKey))
            _runningTasks.Add(longSortKey, runningTask);
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }
    }

    public void RemoveRunningTask(TaskRequest finishedTask)
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          UInt64 longSortKey = finishedTask.ScheduledRunDateTime.ToLongSortKey();
          if (_runningTasks.ContainsKey(longSortKey))
            _runningTasks.Remove(longSortKey); 
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }
    }

    public bool CheckDiscardConcurrent(TaskRequest taskRequest)
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          if (taskRequest.AllowConcurrent)
            return false;

          foreach (var runningTask in _runningTasks.Values)
          {
            if (runningTask.TaskName == taskRequest.TaskName)
              return true;
          }

          return false;
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }

      return false;
    }

    private int Get_WaitInterval()
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          // if no tasks, default to waiting 1 second to check again
          if (this.Count == 0)
            return _maxWaitInterval;

          TaskRequest tr = this.Values.ElementAt(0);
          TimeSpan ts = tr.ScheduledRunDateTime - DateTime.Now;

          int waitMilliseconds = (int)ts.TotalMilliseconds;

          if (waitMilliseconds < 0)
            return 0;
          else
            if (waitMilliseconds > _maxWaitInterval)
              return _maxWaitInterval;

          return waitMilliseconds;
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }

      return _maxWaitInterval;
    }

    public TaskRequest GetNext()
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          if (this.Count == 0)
            return null;

          TaskRequest tr = this.Values.ElementAt(0);
          TimeSpan ts = tr.ScheduledRunDateTime - DateTime.Now;

          int taskInterval = (int)ts.TotalMilliseconds;

          if (taskInterval > _maxWaitInterval)
            return null;

          this.RemoveAt(0);

          return tr;
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }

      return null;
    }

    public TaskRequest GetNextNow()
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          if (this.Count == 0)
            return null;

          TaskRequest tr = this.Values.ElementAt(0);
          this.RemoveAt(0);
          return tr;
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }

      return null;
    }

    public TaskRequest PeekNext()
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          if (this.Count == 0)
            return null;

          TaskRequest tr = this.Values.ElementAt(0);
          TimeSpan ts = tr.ScheduledRunDateTime - DateTime.Now;

          return tr;
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }

      return null;
    }

    public void RemoveAllTaskRequestsForTaskId(int taskId)
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          var keysToRemove = new List<ulong>();
          foreach (var taskRequest in this)
          {
            if (taskRequest.Value.ScheduledTaskId == taskId)
              keysToRemove.Add(taskRequest.Key);
          }

          foreach (var key in keysToRemove)
            this.Remove(key);
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }
    }


    public void RemoveAllTaskRequestsForTaskId(TaskResult taskResult)
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          if (taskResult.OriginalTaskRequest == null)
            return; 

          if (!taskResult.OriginalTaskRequest.RunUntilTask || taskResult.NoWorkDone)
            return;

          var keysToRemove = new List<ulong>();
          foreach (var taskRequest in this)
          {
            if (taskRequest.Value.ScheduledTaskId == taskResult.OriginalTaskRequest.ScheduledTaskId)
              keysToRemove.Add(taskRequest.Key);
          }

          foreach (var key in keysToRemove)
            this.Remove(key);
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }
    }

    private bool Get_MoreTasksToProcess()
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          return this.Count == 0;
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }

      // may want to throw an exception here
      return false;
    }

    private int Get_TasksToProcessCount()
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          return this.Count;
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }

      // may want to throw an exception here
      return 0;
    }

    private string Get_TaskReport()
    {
      // Lock this object, wait for up to a second
      if (Monitor.TryEnter(TaskRequestSet_LockObject, 1000))
      {
        try
        {
          StringBuilder sb = new StringBuilder();

          sb.Append("Report of Tasks Scheduled" + g.crlf2);

          foreach (var task in this.Values)
          {
            sb.Append("    TASK: " + task.TaskName.PadTo(40) + " scheduled at " +
                task.ScheduledRunDateTime.ToString("yyyy-MM-dd HH:mm:ss") + g.crlf);
          }

          if (this.TasksToProcessCount == 0)
            sb.Append("No tasks are scheduled" + g.crlf);

          if (this.NextLoadTime.Year > 2000)
            sb.Append(g.crlf + "Next update of the task request list is scheduled for " + this.NextLoadTime.ToString("yyyy-MM-dd HH:mm:ss") + g.crlf);

          string report = sb.ToString();
          return report;
        }
        finally
        {
          Monitor.Exit(TaskRequestSet_LockObject);
        }
      }

      return String.Empty;
    }
  }
}

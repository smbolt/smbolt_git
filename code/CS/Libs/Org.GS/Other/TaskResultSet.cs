using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public class TaskResultSet : SortedList<int, TaskResult>
  {
    private TaskResult _parent;
    public TaskResult Parent
    {
      get { return _parent; }
      set { _parent = value; }
    }

    public void AddTaskResult(TaskResult taskResult)
    {
      int taskNumber = this.Count + 1;
      taskResult.TaskNumber = taskNumber;
      this.Add(taskNumber, taskResult);
            
      DistributeParentageAndDepth();
    }

    public void DistributeParentageAndDepth()
    {
      foreach (TaskResult taskResult in this.Values)
      {
        taskResult.Parent = this.Parent;
        taskResult.Depth = this.Parent.Depth + 1;
        taskResult.TaskResultSet.DistributeParentageAndDepth();
      }
    }

    public string GetLastTaskName()
    {
      if (this.Count == 0)
        return String.Empty;

      return this.Values.Last().TaskName;
    }

    public TaskResultStatus GetWorstResult()
    {
      int worstResult = this.Parent.TaskResultStatus.ToInt32();

      if (worstResult > 2)
        worstResult = 0;

      foreach (var taskResult in this.Values)
      {
        int worstChildResult = (int) taskResult.TaskResultSet.GetWorstResult();
        if (worstChildResult < 3 && worstChildResult > worstResult)
          worstResult = worstChildResult;
      }

      return g.ToEnum<TaskResultStatus>(worstResult, TaskResultStatus.Success);
    }

  }
}

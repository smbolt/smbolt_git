using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.TSK
{
  public enum TaskScheduleMode
  {
    FromConfig,
    FromDatabase
  }

  public class TaskEngineParms
  {
    public TaskScheduleMode TaskScheduleMode { get; set; }
    public int TaskLoadIntervalSeconds { get; set; }
    public string TasksDbSpecPrefix { get; set; }
    public string TaskProfile { get; set; }
    public string WSHTaskProfile { get; set; }
    public List<string> TasksToRun { get; set; }
    public string MEFModulesPath { get; set; }
    public bool LimitMEFImports { get; set; }
    public string MEFLimitListName { get; set; }
    public string TasksToRunReport { get { return Get_TasksToRunReport(); } }
        
    public TaskEngineParms()
    {
      this.TaskScheduleMode = TaskScheduleMode.FromDatabase;
      this.TaskLoadIntervalSeconds = 1200;
      this.TasksDbSpecPrefix = String.Empty;
      this.TaskProfile = "Normal";
      this.WSHTaskProfile = "FastTest"; 
      this.TasksToRun = new List<string>();
      this.MEFModulesPath = String.Empty;
      this.LimitMEFImports = false;
      this.MEFLimitListName = String.Empty;
    }

    private string Get_TasksToRunReport()
    {
      if (this.TasksToRun == null || this.TasksToRun.Count == 0)
        return "No tasks are configured to be run.";

      string tasksToRun = "Tasks configured to be run: ";

      int count = 0;
      foreach (var task in this.TasksToRun)
      {
        if (count == 0)
          tasksToRun += task;
        else
          tasksToRun += ", " + task;
        count++;
      }

      return tasksToRun;
    }
  }
}

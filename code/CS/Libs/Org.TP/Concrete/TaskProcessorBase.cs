using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Performance;
using Org.TP;

namespace Org.TP.Concrete
{
  public delegate bool CheckContinueTask();

  public class TaskProcessorBase : ITaskProcessor, IDisposable
  {
    public virtual int EntityId { get { return 9999; } }
    public event Action<NotifyMessage> NotifyMessage;
    public Func<bool> CheckContinue;
    public bool TrackPerformance { get; set; }
    public bool MoveFilesToError { get; set; }
    public bool IsDryRun { get; set; }
    public string DryRunIndicator { get { return IsDryRun ? "### DRY-RUN ### " : String.Empty; } }

    protected List<string> ErrorList { get; private set; }

    public event Action<ProgressMessage> ProgressUpdate;
    public bool ProgressUpdateEventMapped;

    private TaskRequest _taskRequest;
    public TaskRequest TaskRequest
    {
      get { return _taskRequest; }
      set 
      {
        _taskRequest = value;
        this.ParmSet = _taskRequest.ParmSet; 
      }
    }

    public ParmSet ParmSet { get; set; }

    public PerfMetricSet PerfMetricSet { get; set; }

    public TaskProcessorBase()
    {
      this.TrackPerformance = false;
      this.PerfMetricSet = new PerfMetricSet();
      this.IsDryRun = false;
      this.MoveFilesToError = true;
    }

    public virtual async Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      Initialize();
      string errorMessage = "Error - the ProcessTaskAsync method must be overridden in the derived task processor class";
      string taskName = "Undefined";

      if (this.TaskRequest != null)
        taskName = this.TaskRequest.TaskName;

      var taskResult = await Task.Run<TaskResult>(() =>
      {
        var result  = new TaskResult(taskName, errorMessage, TaskResultStatus.Failed);
        return result;
      });

      return taskResult;
    }

    protected virtual TaskResult InitializeTaskResult()
    {
      TaskResult taskResult = new TaskResult();
      taskResult.OriginalTaskRequest = this.TaskRequest;
      taskResult.TaskName = this.TaskRequest.TaskName;
      taskResult.BeginDateTime = DateTime.Now;
      return taskResult;
    }

    protected bool ParmExists(string parmName)
    {
      if (this.ParmSet == null)
        return false;

      return this.ParmSet.ParmExists(parmName);
    }

    protected object GetParmValue(string parmName)
    {
      if (this.ParmSet == null)
        return String.Empty;

      return this.ParmSet.GetParmValue(parmName);
    }

    protected virtual void AssertParmExistence (string parmName)
    {
      if (this.ParmSet == null)
        throw new Exception("Parameter set is null.");

      this.ParmSet.AssertParmExistence(parmName);
    }

    protected void Notify(string message)
    {
      string defaultSubject = "Notification from " + this.TaskRequest.TaskName;
      string defaultEvent = "Default";
      Notify(message, defaultSubject, defaultEvent);
    }

    protected void Notify(string message, string subject)
    {
      string defaultEvent = "Default";
      Notify(message, subject, defaultEvent);
    }

    protected void Notify(string message, string subject, string eventName)
    {
      if (this.NotifyMessage == null)
        return;

      this.NotifyMessage(new NotifyMessage(message, subject, eventName));
    }

    protected void ProgressNotify(int completedItems, int totalItems)
    {
      if (this.ProgressUpdate == null)
        return;

      this.ProgressUpdate(new ProgressMessage(completedItems, totalItems)); 
    }
    
    ~TaskProcessorBase()
    {
      Dispose(false); 
    }

    protected string GetErrorReport()
    {
      StringBuilder sb = new StringBuilder();

      for(int i = 0; i < this.ErrorList.Count; i++)
        sb.Append((i + 1).ToString("00") + ". " + this.ErrorList.ElementAt(i) + g.crlf);

      return sb.ToString();
    }

    protected virtual void Initialize()
    {
      this.ErrorList = new List<string>();

      var isDryRun = this.GetParmValue("IsDryRun");
      if (isDryRun != null)
        this.IsDryRun = isDryRun.ToBoolean();
    }

    public void Dispose()
    {
      Dispose(true); 
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        //  if (_conn != null)
        //  {
        //    if (_conn.State != ConnectionState.Closed)
        //      _conn.Close();
        //    _conn.Dispose();
        //    _conn = null;
        //  }
      }
    }

  }
}

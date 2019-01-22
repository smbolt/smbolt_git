using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPL = System.Threading.Tasks;
using Org.GS;
using Org.GS.Logging;
using Org.GS.Configuration;
using Org.GS.Performance;
using Org.GS.AppDomainManagement;
using Org.TP;

namespace Org.TP.Concrete
{
  public delegate bool CheckContinueTask();

  public class TaskProcessorBase : MarshalByRefObject, ITaskProcessor, IDisposable
  {
    private static a a;
    public virtual int EntityId { get { return 9999; } }
    public string Name { get { return Get_Name(); } }
    public event Action<NotifyMessage> NotifyMessage;
    public Func<bool> CheckContinue;
    public bool TrackPerformance { get; set; }
    public bool MoveFilesToError { get; set; }
    public bool IsDryRun { get; set; }
    public string DryRunIndicator { get { return IsDryRun ? "### DRY-RUN ### " : String.Empty; } }
    protected Logger Logger { get; private set; }

    private string _processorName;
    public string ProcessorName { get { return _processorName; } }

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
      if (!AppDomain.CurrentDomain.IsTheDefaultAppDomain() && a == null)
      {
        a = new a();
      }

      Logger = new Logger();
      Logger.Log(this.GetType().Name + " created."); 

      this.TrackPerformance = false;
      this.PerfMetricSet = new PerfMetricSet();
      this.IsDryRun = false;
      this.MoveFilesToError = true;
      _processorName = this.GetType().Name; 
    }

    public virtual async TPL.Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      Initialize();
      string errorMessage = "Error - the ProcessTaskAsync method must be overridden in the derived task processor class";
      string taskName = "Undefined";

      if (this.TaskRequest != null)
        taskName = this.TaskRequest.TaskName;

      var taskResult = await TPL.Task.Run<TaskResult>(() =>
      {
        var result  = new TaskResult(taskName, errorMessage, TaskResultStatus.Failed);
        return result;
      });

      return taskResult;
    }

    public virtual TaskResult ProcessTask()
    {
      Initialize();

      string errorMessage = "Error - the ProcessTask method must be overridden in the derived task processor class";
      string taskName = "Undefined";

      if (this.TaskRequest != null)
        taskName = this.TaskRequest.TaskName;

      var taskResult = new TaskResult(taskName, errorMessage, TaskResultStatus.Failed);

      return taskResult;
    }

    private string Get_Name()
    {
      string taskProcessorName = this.GetType().Name;
      return taskProcessorName; 
    }

    protected virtual TaskResult InitializeTaskResult()
    {
      TaskResult taskResult = new TaskResult();
      taskResult.OriginalTaskRequest = this.TaskRequest;
      taskResult.TaskName = this.TaskRequest.TaskName;
      taskResult.BeginDateTime = DateTime.Now;

      if (this.ParmExists("LoggingDbSpec"))
        LogContext.LogConfigDbSpec = this.GetParmValue("LoggingDbSpec") as ConfigDbSpec; 

      return taskResult;
    }

    protected bool ParmExists(string parmName)
    {
      if (this.ParmSet == null)
        return false;

      return this.ParmSet.ParmExists(parmName);
    }

    protected object GetParmValue(string parmName, string defaultValue = "")
    {
      if (this.ParmSet == null)
        return String.Empty;

      return this.ParmSet.GetParmValue(parmName, defaultValue);
    }

    protected void SetParmValue(string parmName, object parmValue)
    {
      if (this.ParmSet == null)
        this.ParmSet = new ParmSet();

      this.ParmSet.SetParmValue(parmName, parmValue);
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

    protected void ProgressNotify(string activityName, string message, int completedItems, int totalItems)
    {
      if (this.ProgressUpdate == null)
        return;

      this.ProgressUpdate(new ProgressMessage(activityName, completedItems, totalItems, message));
    }

    ~TaskProcessorBase()
    {
      if (Logger == null)
        Logger = new Logger();
      Logger.Log(this.GetType().Name + " in destructor.");

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

    public virtual string Identify()
    {
      return "TaskProcessorBase";
    }

    public void Dispose()
    {
      Dispose(true); 
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
      }
    }
  }
}

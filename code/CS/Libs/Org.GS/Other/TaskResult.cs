using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Data;
using System.Threading;
using Org.GS.Logging;

namespace Org.GS
{
  [Serializable]
  public class TaskResult
  {
    private string indent;

    public string TaskName {
      get;
      set;
    }
    private TaskResultStatus _taskResultStatus;
    public TaskResultStatus TaskResultStatus
    {
      get {
        return _taskResultStatus;
      }
      set
      {
        _taskResultStatus = value;
        this.EndDateTime = DateTime.Now;
      }
    }
    public TaskRequest OriginalTaskRequest {
      get;
      set;
    }
    public string Message {
      get;
      set;
    }
    public int Code {
      get;
      set;
    }
    public LogSeverity LogSeverity {
      get;
      set;
    }
    public int OrgId {
      get;
      set;
    }
    public int AccountId {
      get;
      set;
    }
    public int ModuleId {
      get;
      set;
    }
    public string SessionId {
      get;
      set;
    }
    public int EntityTypeId {
      get;
      set;
    }
    public int EntityId {
      get;
      set;
    }
    public string UserName {
      get;
      set;
    }
    public string Header {
      get;
      set;
    }
    public bool ReenqueueTask {
      get;
      set;
    }
    public string FullDetail {
      get;
      set;
    }
    public string FullErrorDetail {
      get;
      set;
    }
    //public XElement Xml { get; set; }
    public string Data {
      get;
      set;
    }
    public bool? Boolean1 {
      get;
      set;
    }
    public bool? Boolean2 {
      get;
      set;
    }
    public string Field {
      get;
      set;
    }
    public Exception Exception {
      get;
      set;
    }
    public int MessageCode {
      get;
      set;
    }
    public DataSet DataSet {
      get;
      set;
    }
    public TaskResultSet TaskResultSet {
      get;
      set;
    }
    public int Depth {
      get;
      set;
    }
    public TaskResult Parent {
      get;
      set;
    }
    public DateTime BeginDateTime {
      get;
      set;
    }
    public DateTime EndDateTime {
      get;
      set;
    }
    public string OutputFileName {
      get;
      set;
    }
    public int TaskNumber {
      get;
      set;
    }
    public object OriginalTask {
      get;
      set;
    }
    public int ThreadId {
      get;
      set;
    }
    public int TaskId {
      get;
      set;
    }
    public object Object {
      get;
      set;
    }
    public int TotalEntityCount {
      get;
      set;
    }
    public int SubsetEntityCount {
      get;
      set;
    }
    public bool IsPaging {
      get;
      set;
    }
    public bool IsLogged {
      get;
      set;
    }
    public bool NoWorkDone {
      get;
      set;
    }
    public bool IsDryRun {
      get;
      set;
    }
    public string Report {
      get {
        return Get_Report(this);
      }
    }
    //public bool ReportOnly { get; set; }
    public bool NotificationsSent {
      get;
      set;
    }

    private string _notificationMessage = String.Empty;
    public string NotificationMessage
    {
      get
      {
        if (_notificationMessage.IsBlank())
        {
          _notificationMessage = "Task '" + this.TaskName + "' - Status is '" + this.TaskResultStatus.ToString() + "'." + g.crlf +
                                 "        Code    : " + this.Code + g.crlf +
                                 "        Started : " + this.BeginDateTime.ToString() + g.crlf +
                                 "        Ended   : " + this.EndDateTime.ToString() + g.crlf +
                                 "        Duration: " + this.DurationString + " seconds" + g.crlf2 +
                                 "Message : " + g.crlf2 +  this.Message;

          if (this.TaskResultStatus != GS.TaskResultStatus.Success && this.FullErrorDetail.IsNotBlank())
            _notificationMessage += g.crlf2 + "Full Error Detail" + g.crlf + this.FullErrorDetail;

          if (this.Exception != null)
            _notificationMessage += g.crlf2 + "Exception" + g.crlf2 + this.Exception.ToReport();
        }
        return _notificationMessage;
      }
      set
      {
        _notificationMessage = value;
      }
    }

    public string DurationString
    {
      get
      {
        if (this.BeginDateTime == DateTime.MinValue || this.EndDateTime == DateTime.MinValue)
          return "not calculated";

        if (this.EndDateTime < this.BeginDateTime)
          return "not calculated";

        TimeSpan ts = this.Duration;

        return ts.TotalSeconds.ToString("000.000");
      }
    }

    public TimeSpan Duration
    {
      get
      {
        if (this.BeginDateTime == DateTime.MinValue || this.EndDateTime == DateTime.MinValue)
          return new TimeSpan(0);

        if (this.EndDateTime < this.BeginDateTime)
          return new TimeSpan(0);

        return this.EndDateTime - this.BeginDateTime;
      }
    }

    public string NotificationEventName
    {
      get
      {
        return this.TaskName.Trim() + "_" + this.TaskResultStatus.ToString();
      }
    }

    public string VariableNotificationEventName
    {
      get
      {
        return "$TASKNAME$_" + this.TaskResultStatus.ToString();
      }
    }

    public TaskResult()
    {
      Initialize();
      this.ThreadId = Thread.CurrentThread.ManagedThreadId;
    }

    public TaskResult(string taskName)
    {
      Initialize();
      this.TaskName = taskName;
      this.ThreadId = Thread.CurrentThread.ManagedThreadId;
    }

    public TaskResult(string taskName, string message, bool defaultToSuccess)
    {
      Initialize();
      this.TaskName = taskName;
      this.ThreadId = Thread.CurrentThread.ManagedThreadId;
      this.Message = message;
      if (defaultToSuccess)
        this.TaskResultStatus = TaskResultStatus.Success;
    }

    public TaskResult(string taskName, string message, TaskResultStatus taskStatus)
    {
      Initialize();
      this.TaskName = taskName;
      this.ThreadId = Thread.CurrentThread.ManagedThreadId;
      this.Message = message;
      this.TaskResultStatus = taskStatus;
      if (this.TaskResultStatus == TaskResultStatus.InProgress)
        this.EndDateTime = DateTime.Now;
    }

    public TaskResult(string taskName, string message, TaskResultStatus taskStatus, int code)
    {
      Initialize();
      this.TaskName = taskName;
      this.ThreadId = Thread.CurrentThread.ManagedThreadId;
      this.Message = message;
      this.TaskResultStatus = taskStatus;
      this.Code = code;
    }

    public TaskResult(string taskName, string message, TaskResultStatus taskStatus, int code, Exception ex)
    {
      Initialize();
      this.TaskName = taskName;
      this.ThreadId = Thread.CurrentThread.ManagedThreadId;
      this.Message = message;
      this.TaskResultStatus = taskStatus;
      this.Code = code;
      this.Exception = ex;
    }

    private void Initialize()
    {
      indent = g.BlankString(12);
      this.TaskName = String.Empty;
      this.OriginalTaskRequest = null;
      this.TaskResultStatus = TaskResultStatus.NotSet;
      this.Message = String.Empty;
      this.Code = 0;
      this.OrgId = 0;
      this.AccountId = 0;
      this.ModuleId = 0;
      this.SessionId = String.Empty;
      this.EntityTypeId = 0;
      this.EntityId = 0;
      this.UserName = String.Empty;
      this.LogSeverity = Logging.LogSeverity.INFO;
      this.NotificationMessage = String.Empty;
      this.FullErrorDetail = String.Empty;
      this.FullDetail = String.Empty;
      this.Header = String.Empty;
      this.ReenqueueTask = false;
      //this.Xml = new XElement("Empty");
      this.Data = String.Empty;
      this.Boolean1 = null;
      this.Boolean2 = null;
      this.Field = String.Empty;
      this.Exception = null;
      this.MessageCode = 0;
      this.DataSet = new DataSet();
      this.TaskResultSet = new TaskResultSet();
      this.TaskResultSet.Parent = this;
      this.Depth = 0;
      this.Parent = null;
      this.BeginDateTime = DateTime.Now;
      this.EndDateTime = DateTime.MinValue;
      this.OutputFileName = String.Empty;
      this.TaskNumber = 0;
      this.TaskId = 0;
      this.Object = null;
      this.TotalEntityCount = 0;
      this.SubsetEntityCount = 0;
      this.IsPaging = false;
      this.IsLogged = false;
      this.NoWorkDone = false;
      this.IsDryRun = false;
      //this.ReportOnly = false;
      this.NotificationsSent = false;
    }

    //public string GetIndentedXml()
    //{
    //  string rawXml = this.Xml.ToString();
    //  string indentedXml = rawXml.Replace("\r", String.Empty);
    //  indentedXml = indentedXml.Replace("\n", "`");
    //  string[] xmlLines = indentedXml.Split('`');

    //  StringBuilder sb = new StringBuilder();
    //  foreach (string xmlLine in xmlLines)
    //    sb.Append(indent + xmlLine + g.crlf);

    //  return sb.ToString();
    //}

    public string GetFormattedTaskNumber()
    {
      TaskResult taskResult = this;

      string taskNumber = this.TaskNumber.ToString().Trim();

      while (taskResult.Parent != null)
      {
        taskResult = taskResult.Parent;
        if (taskResult.TaskNumber != 0)
          taskNumber = taskResult.TaskNumber.ToString().Trim() + "." + taskNumber;
      }

      return taskNumber;
    }

    public TaskResult Success()
    {
      this.TaskResultStatus = TaskResultStatus.Success;
      return this;
    }

    public TaskResult Success(int code)
    {
      this.TaskResultStatus = TaskResultStatus.Success;
      this.Code = code;
      return this;
    }

    public TaskResult Success(bool noWorkDone)
    {
      this.TaskResultStatus = TaskResultStatus.Success;
      this.NoWorkDone = noWorkDone;
      return this;
    }

    public TaskResult Success(string message)
    {
      this.Message = message;
      this.TaskResultStatus = TaskResultStatus.Success;
      return this;
    }

    public TaskResult Success(string message, int code)
    {
      this.Message = message;
      this.TaskResultStatus = TaskResultStatus.Success;
      this.Code = code;
      return this;
    }

    public TaskResult Warning()
    {
      this.TaskResultStatus = TaskResultStatus.Warning;
      return this;
    }

    public TaskResult Warning(string message)
    {
      this.Message = message;
      this.TaskResultStatus = TaskResultStatus.Warning;
      return this;
    }

    public TaskResult Warning(string message, int code)
    {
      this.LogSeverity = LogSeverity.WARN;
      this.Message = message;
      this.Code = code;
      this.TaskResultStatus = TaskResultStatus.Warning;
      return this;
    }

    public TaskResult Warning(string message, int code, LogSeverity logSeverity)
    {
      this.LogSeverity = logSeverity;
      this.Message = message;
      this.Code = code;
      this.TaskResultStatus = TaskResultStatus.Warning;
      return this;
    }

    public TaskResult Warning(string message, Exception ex)
    {
      this.LogSeverity = LogSeverity.WARN;
      this.Message = message;
      this.Exception = ex;
      this.TaskResultStatus = TaskResultStatus.Warning;
      return this;
    }

    public TaskResult Warning(string message, Exception ex, LogSeverity logSeverity)
    {
      this.LogSeverity = logSeverity;
      this.Message = message;
      this.Exception = ex;
      this.TaskResultStatus = TaskResultStatus.Warning;
      return this;
    }

    public TaskResult Warning(string message, int code, Exception ex)
    {
      this.LogSeverity = LogSeverity.WARN;
      this.Message = message;
      this.Code = code;
      this.Exception = ex;
      this.TaskResultStatus = TaskResultStatus.Warning;
      return this;
    }

    public TaskResult Warning(string message, int code, Exception ex, LogSeverity logSeverity)
    {
      this.LogSeverity = logSeverity;
      this.Message = message;
      this.Code = code;
      this.Exception = ex;
      this.TaskResultStatus = TaskResultStatus.Warning;
      return this;
    }

    public TaskResult Failed()
    {
      this.LogSeverity = LogSeverity.SEVR;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult Failed(LogSeverity logSeverity)
    {
      this.LogSeverity = logSeverity;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult Failed(int code)
    {
      this.LogSeverity = LogSeverity.SEVR;
      this.Code = code;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult Failed(int code, LogSeverity logSeverity)
    {
      this.LogSeverity = logSeverity;
      this.Code = code;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult Failed(string message)
    {
      this.LogSeverity = LogSeverity.SEVR;
      this.Message = message;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult Failed(string message, LogSeverity logSeverity)
    {
      this.LogSeverity = logSeverity;
      this.Message = message;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult Failed(string message, int code)
    {
      this.LogSeverity = LogSeverity.SEVR;
      this.Message = message;
      this.Code = code;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult Failed(string message, int code, LogSeverity logSeverity)
    {
      this.LogSeverity = logSeverity;
      this.LogSeverity = LogSeverity.SEVR;
      this.Message = message;
      this.Code = code;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult Failed(string message, Exception ex)
    {
      this.LogSeverity = LogSeverity.SEVR;
      this.Message = message;
      this.Exception = ex;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult Failed(string message, Exception ex, LogSeverity logSeverity)
    {
      this.LogSeverity = logSeverity;
      this.LogSeverity = LogSeverity.SEVR;
      this.Message = message;
      this.Exception = ex;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult Failed(string message, int code, Exception ex)
    {
      this.LogSeverity = LogSeverity.SEVR;
      this.Message = message;
      this.Code = code;
      this.Exception = ex;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult Failed(string message, int code, Exception ex, LogSeverity logSeverity)
    {
      this.LogSeverity = logSeverity;
      this.Message = message;
      this.Code = code;
      this.Exception = ex;
      this.TaskResultStatus = TaskResultStatus.Failed;
      return this;
    }

    public TaskResult SetCode(int code)
    {
      this.Code = code;
      return this;
    }

    public TaskResult NotFound()
    {
      this.TaskResultStatus = TaskResultStatus.NotFound;
      return this;
    }

    public TaskResult NotFound(string message)
    {
      this.Message = message;
      this.TaskResultStatus = TaskResultStatus.NotFound;
      return this;
    }

    private string Get_Report(TaskResult r)
    {
      var sb = new StringBuilder();

      sb.Append("Task " + r.TaskName + " Status is " + r.TaskResultStatus.ToString() + " Duration " + r.DurationString + g.crlf + r.Message);

      if (r.TaskResultSet != null && r.TaskResultSet.Count > 0)
      {
        foreach (var childTaskResult in r.TaskResultSet.Values)
        {
          sb.Append(childTaskResult.Report + g.crlf);
        }
      }

      string report = sb.ToString();

      return report;
    }
  }
}

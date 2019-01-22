using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.Configuration;
using Org.GS;

namespace Org.GS.Notifications
{
  public enum NotificationSource
  {
    NotSet,
    TaskResult,
    NamedEvent,
    Other
  }

  public enum NotificationStatus
  {
    NotSet,
    ReadyToSend,
    SentUnconfirmed,
    SentConfirmed,
    Failed,
    NotConfigured
  }

  public class Notification
  {
    public NotificationSource NotificationSource { get; set; }
    public string EventName { get; set; }
    public string TaskName { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool IsBodyHtml { get; set; }
    public int Code { get; set; }
    public NotificationStatus NotificationStatus { get; set; }
    public TaskResult TaskResult { get; set; }

    public Exception Exception 
    {
      set
      {
        if (value != null)
        {
          if (this.Body.IsBlank())
          {
            this.Body = value.ToReport();
          }
        }
      }
    }

    public Notification(TaskResult taskResult, NotifyConfigSet notifyConfigSet )
    {
      this.NotificationSource = NotificationSource.TaskResult;
      this.TaskResult = taskResult;
      this.TaskName = taskResult.TaskName;

      if (!notifyConfigSet.ContainsConfigForTaskResult(taskResult))
      {
        this.NotificationStatus = NotificationStatus.NotConfigured;
        return;
      }

      var notifyEvent = notifyConfigSet.GetNotifyEventForTaskResult(taskResult);
      
      if (notifyEvent == null)
      {
        this.NotificationStatus = NotificationStatus.NotConfigured;
        return;
      }

      this.Subject = notifyEvent.DefaultSubject.Replace("$TASKNAME$", this.TaskName).Trim();
      if (taskResult.IsDryRun)
        this.Subject += " (DRY RUN)";
      this.Body = taskResult.NotificationMessage;
      this.NotificationStatus = NotificationStatus.ReadyToSend;
      this.Code = taskResult.Code;
      this.Exception = taskResult.Exception;
      this.EventName = notifyEvent.Name;
    }

    public Notification(string eventName, NotifyConfigSet notifyConfigSet, Exception ex)
    {
      this.NotificationSource = NotificationSource.NamedEvent;
      this.TaskResult = new TaskResult(eventName, "Generated TaskResult for event name '" + eventName + "'", TaskResultStatus.NotSet);

      if (!notifyConfigSet.ContainsConfigForNamedEvent(eventName))
      {
        this.NotificationStatus = NotificationStatus.NotConfigured;
        return;
      }

      var notifyEvent = notifyConfigSet.GetNotifyEventForNamedEvent(eventName);

      if (notifyEvent == null)
      {
        this.NotificationStatus = NotificationStatus.NotConfigured;
        return;
      }

      this.Subject = notifyEvent.DefaultSubject.Replace("$TASKNAME$", this.TaskName).Trim();
      this.Body = "*** AN EXCEPTION HAS OCCURRED ***" + g.crlf2 + ex.ToReport();
      this.NotificationStatus = NotificationStatus.ReadyToSend;
      this.Code = 0;
      this.Exception = ex;
      this.EventName = notifyEvent.Name;
    }

    public Notification(string eventName, NotifyConfigSet notifyConfigSet, string message, string subject = "")
    {
      this.NotificationSource = NotificationSource.NamedEvent;
      this.TaskResult = new TaskResult(eventName, "Generated TaskResult for event name '" + eventName + "'", TaskResultStatus.NotSet);

      if (!notifyConfigSet.ContainsConfigForNamedEvent(eventName))
      {
        this.NotificationStatus = NotificationStatus.NotConfigured;
        return;
      }

      var notifyEvent = notifyConfigSet.GetNotifyEventForNamedEvent(eventName);

      if (notifyEvent == null)
      {
        this.NotificationStatus = NotificationStatus.NotConfigured;
        return;
      }

      this.Subject = notifyEvent.DefaultSubject.Replace("$TASKNAME$", this.TaskName).Trim();
      this.Body = message;
      this.NotificationStatus = NotificationStatus.ReadyToSend;
      this.Code = 0;
      this.Exception = null;
      this.EventName = notifyEvent.Name;
    }

    public Notification()
    {
      this.NotificationSource = NotificationSource.NotSet;
      this.EventName = "Default";
      this.TaskName = "Default";
      this.Subject = "Default Subject";
      this.Body = "Default Body";
      this.IsBodyHtml = false;
      this.Code = -1;
      this.NotificationStatus = NotificationStatus.NotSet;
      this.Exception = null; 
    }
  }
}

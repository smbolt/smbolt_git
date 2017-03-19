using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Org.GS.Configuration;

namespace Org.GS.Notifications
{
  public class NotificationEngine : IDisposable
  {
    private NotifyConfig _notificationConfig;
    private SmtpParms _smtpParms;

    public NotificationEngine(NotifyConfig notifyConfig, SmtpParms smtpParms)
    {
      _notificationConfig = notifyConfig;
      _smtpParms = smtpParms;
    }

    public async Task<TaskResult> ProcessNotificationsAsync(Notification notification)
    {
      var taskResult = new TaskResult("ProcessNotifications");

      try
      {
        if (_notificationConfig == null || 
            !_notificationConfig.NotifyEventSet.ContainsKey(notification.EventName) || 
            _notificationConfig.NotifyGroupSet.Count == 0)
        {
          taskResult.TaskResultStatus = TaskResultStatus.Success;
          if (_notificationConfig == null)
            taskResult.Message = "No notifications sent, notification configurations are null.";
          else if (!_notificationConfig.NotifyEventSet.ContainsKey(notification.EventName))
            taskResult.Message = "No notifications sent, notify event '" + notification.EventName + "' not configured.";
          else if (_notificationConfig.NotifyGroupSet.Count == 0)
            taskResult.Message = "No notifications sent, no notification groups are configured.";

          return taskResult;
        }

        var notifyEvent = _notificationConfig.NotifyEventSet[notification.EventName];

        List<Task<TaskResult>> emailTasks = new List<Task<TaskResult>>();

        foreach(var notifyGroupReference in notifyEvent.Where(e => e.IsActive))
        {
          var notifyGroup = _notificationConfig.NotifyGroupSet[notifyGroupReference.NotifyGroupName];
          if (notifyGroup != null)
          {
            foreach (var notifyPerson in notifyGroup)
            {
              if (notifyPerson.IsEmailActive)
              {
                var emailMessage = new EmailNotificationMessage();
                emailMessage.NotificationMessageSubject = notification.Subject;
                emailMessage.IsBodyHtml = notification.IsBodyHtml;
                emailMessage.NotificationMessageBody = notification.Body;
                emailMessage.NotificationAddresses.Add(new EmailNotificationAddress(NotificationAddressType.EmailToAddress, notifyPerson.EmailAddress));
                emailMessage.NotificationAddresses.Add(new EmailNotificationAddress(NotificationAddressType.EmailFromAddress, _smtpParms.EmailFromAddress));
                var emailTask = Task.Run(() => { return ProcessEmailTaskAsync(_smtpParms, emailMessage); });
                emailTasks.Add(emailTask); 
              }

              if (notifyPerson.IsSmsActive)
              {


              }
            }
          }
        }

        Task<TaskResult>[] taskArray = emailTasks.ToArray();
        var emailTaskResultSet = await Task.WhenAll(taskArray);

        taskResult.TaskResultStatus = TaskResultStatus.Success;
        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Message = "An exception occurred processing notifications.";
        taskResult.Exception = ex;
        return taskResult;
      }
    }

    public TaskResult ProcessNotifications(Notification notification)
    {
      var taskResult = new TaskResult("ProcessNotifications");

      try
      {
        if (_notificationConfig == null || 
            !_notificationConfig.NotifyEventSet.ContainsKey(notification.EventName) || 
            _notificationConfig.NotifyGroupSet.Count == 0)
        {
          taskResult.TaskResultStatus = TaskResultStatus.Success;
          if (_notificationConfig == null)
            taskResult.Message = "No notifications sent, notification configurations are null.";
          else if (!_notificationConfig.NotifyEventSet.ContainsKey(notification.EventName))
            taskResult.Message = "No notifications sent, notify event '" + notification.EventName + "' not configured.";
          else if (_notificationConfig.NotifyGroupSet.Count == 0)
            taskResult.Message = "No notifications sent, no notification groups are configured.";

          return taskResult;
        }

        var notifyEvent = _notificationConfig.NotifyEventSet[notification.EventName];

        List<Task<TaskResult>> emailTasks = new List<Task<TaskResult>>();

        foreach(var notifyGroupReference in notifyEvent.Where(e => e.IsActive))
        {
          var notifyGroup = _notificationConfig.NotifyGroupSet[notifyGroupReference.NotifyGroupName];
          if (notifyGroup != null)
          {
            foreach (var notifyPerson in notifyGroup)
            {
              if (notifyPerson.IsEmailActive)
              {
                var emailMessage = new EmailNotificationMessage();
                emailMessage.NotificationMessageSubject = notification.Subject;
                emailMessage.IsBodyHtml = notification.IsBodyHtml;
                emailMessage.NotificationMessageBody = notification.Body;
                emailMessage.NotificationAddresses.Add(new EmailNotificationAddress(NotificationAddressType.EmailToAddress, notifyPerson.EmailAddress));
                emailMessage.NotificationAddresses.Add(new EmailNotificationAddress(NotificationAddressType.EmailFromAddress, _smtpParms.EmailFromAddress));
                var emailTask = Task.Run(() => { return ProcessEmailTaskAsync(_smtpParms, emailMessage); });
                emailTasks.Add(emailTask); 
              }

              if (notifyPerson.IsSmsActive)
              {


              }
            }
          }
        }

        Task<TaskResult>[] taskArray = emailTasks.ToArray();
        var emailTaskResultSet = Task.WhenAll(taskArray);

        taskResult.TaskResultStatus = TaskResultStatus.Success;
        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Message = "An exception occurred processing notifications.";
        taskResult.Exception = ex;
        return taskResult;
      }
    }

    private async Task<TaskResult> ProcessEmailTaskAsync(SmtpParms smtpParms, EmailNotificationMessage emailMessage)
    {
      using (var emailProcessor = new EmailNotificationProcessor())
      {
        return await emailProcessor.SendMessageAsync(smtpParms, emailMessage);
      }
    }

    public void Dispose()
    {
    }
  }
}

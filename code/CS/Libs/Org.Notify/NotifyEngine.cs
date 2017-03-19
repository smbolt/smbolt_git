using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Notifications;

namespace Org.Notify
{
  public class NotifyEngine : IDisposable
  {
    public event Action<NotifyEventArgs> NotifyAction;

    private SmtpParms _smtpParms;
    private NotifyConfigSet _notifyConfigSet;

    public NotifyEngine(NotifyConfigSet notifyConfigSet, SmtpParms smtpParms)
    {
      _smtpParms = smtpParms;
      _notifyConfigSet = notifyConfigSet;
    }


    public async Task<TaskResult> ProcessNotificationsAsync(Notification notification)
    {
      var taskResult = new TaskResult("ProcessNotifications");

      try
      {
        SendNotifyAction(NotifyEventType.NotificationRequested, false);

        if (_notifyConfigSet == null)
        {
          SendNotifyAction(NotifyEventType.NotificationFailed, false);
          return taskResult.Failed("NotifyConfigSet supplied to NotifyEngine is null.", 6001);
        }

        NotifyConfig notifyConfig = null;

        switch (notification.NotificationSource)
        {
          case NotificationSource.TaskResult:
            notifyConfig = _notifyConfigSet.GetConfigForTaskResult(notification.TaskResult);
            break;

          case NotificationSource.NamedEvent:
            notifyConfig = _notifyConfigSet.GetConfigForNamedEvent(notification.EventName);
            break;
        }


        if (notifyConfig == null)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6002;
          taskResult.Message = "No NotifyConfig for specified EventName in the supplied NotifyConfigSet.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, false);
          return taskResult;
        }

        if (!notifyConfig.SendEmail && !notifyConfig.SendSms)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6003;
          taskResult.Message = "The NotifyConfig is not configured to send emails or SMS messages.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, false);
          return taskResult;
        }

        if (!notifyConfig.SendSms && notifyConfig.SendEmail && notifyConfig.TotalActiveEmailAddresses == 0)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6004;
          taskResult.Message = "No active email addresses are included in the supplied NotifyConfigSet.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, false);
          return taskResult;
        }

        if (notifyConfig.SendSms && !notifyConfig.SendEmail && notifyConfig.TotalActiveSmsNumbers == 0)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6005;
          taskResult.Message = "No active SMS numbers are included in the supplied NotifyConfigSet.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, false);
          return taskResult;
        }

        var notifyEvent = notifyConfig.NotifyEventSet.GetNotifyEventForNotification(notification);

        if (notifyEvent == null)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6006;
          taskResult.Message = "No NotifyEvent exists for the Notification.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, false);
          return taskResult;
        }

        if (notifyEvent.Where(e => e.IsActive).Count() == 0)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6007;
          taskResult.Message = "No active GroupReferences exist for the NotifyEvent.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, false);
          return taskResult;
        }

        if (notification.Subject.IsBlank())
          notification.Subject = "NO SUBJECT SUPPLIED";

        List<string> emailAddressesUsed = new List<string>();

        List<Task<TaskResult>> emailTasks = new List<Task<TaskResult>>();

        foreach (var notifyGroupReference in notifyEvent.Where(e => e.IsActive))
        {
          var notifyGroup = notifyConfig.NotifyGroupSet[notifyGroupReference.NotifyGroupName];
          if (notifyGroup != null && notifyGroup.IsActive)
          {
            foreach (var notifyPerson in notifyGroup)
            {
              if (notifyPerson.IsActive && notifyPerson.IsEmailActive && !emailAddressesUsed.Contains(notifyPerson.EmailAddress))
              {
                var emailMessage = new EmailNotificationMessage();
                emailMessage.NotificationMessageSubject = notification.Subject;
                emailMessage.IsBodyHtml = notification.IsBodyHtml;
                emailMessage.NotificationMessageBody = notification.Body;
                emailMessage.NotificationAddresses.Add(new EmailNotificationAddress(NotificationAddressType.EmailToAddress, notifyPerson.EmailAddress));
                emailAddressesUsed.Add(notifyPerson.EmailAddress);
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
        var emailTaskResult = await Task.WhenAll(taskArray);

        foreach (var result in emailTaskResult)
        {
          taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, result);
        }

        int emailSendsAttempted = taskResult.TaskResultSet.Count;
        int emailSendsSuccessful = taskResult.TaskResultSet.Values.Where(e => e.TaskResultStatus == TaskResultStatus.Success).Count();
        int emailSendsNotSuccessful = taskResult.TaskResultSet.Values.Where(e => e.TaskResultStatus != TaskResultStatus.Success).Count();
        taskResult.TaskResultStatus = emailSendsNotSuccessful == 0 ? TaskResultStatus.Success : TaskResultStatus.Warning;
        taskResult.Message = "Email sends attempted: " + emailSendsAttempted.ToString() + ", sends successful: " +
                              emailSendsSuccessful.ToString() + ", sends failed: " + emailSendsNotSuccessful.ToString();
        SendNotifyAction(NotifyEventType.NotificationCompleted, false);
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
        SendNotifyAction(NotifyEventType.NotificationRequested, true);

        if (_notifyConfigSet == null)
        {
          SendNotifyAction(NotifyEventType.NotificationFailed, true);
          return taskResult.Failed("NotifyConfigSet supplied to NotifyEngine is null.", 6001);
        }

        var notifyConfig = _notifyConfigSet.GetConfigForTaskResult(notification.TaskResult);

        if (notifyConfig == null)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6002;
          taskResult.Message = "No NotifyConfig for specified EventName in the supplied NotifyConfigSet.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, true);
          return taskResult;
        }

        if (!notifyConfig.SendEmail && !notifyConfig.SendSms)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6003;
          taskResult.Message = "The NotifyConfig is not configured to send emails or SMS messages.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, true);
          return taskResult;
        }

        if (!notifyConfig.SendSms && notifyConfig.SendEmail && notifyConfig.TotalActiveEmailAddresses == 0)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6004;
          taskResult.Message = "No active email addresses are included in the supplied NotifyConfigSet.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, true);
          return taskResult;
        }

        if (notifyConfig.SendSms && !notifyConfig.SendEmail && notifyConfig.TotalActiveSmsNumbers == 0)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6005;
          taskResult.Message = "No active SMS numbers are included in the supplied NotifyConfigSet.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, true);
          return taskResult;
        }

        var notifyEvent = notifyConfig.NotifyEventSet.GetNotifyEventForNotification(notification);

        if (notifyEvent == null)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6006;
          taskResult.Message = "No NotifyEvent exists for the Notification.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, true);
          return taskResult;
        }

        if (notifyEvent.Where(e => e.IsActive).Count() == 0)
        {
          taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
          taskResult.Code = 6007;
          taskResult.Message = "No active GroupReferences exist for the NotifyEvent.";
          SendNotifyAction(NotifyEventType.NotificationNotExecuted, true);
          return taskResult;
        }

        List<string> emailAddressesUsed = new List<string>();

        foreach (var notifyGroupReference in notifyEvent.Where(e => e.IsActive))
        {
          var notifyGroup = notifyConfig.NotifyGroupSet[notifyGroupReference.NotifyGroupName];
          if (notifyGroup != null && notifyGroup.IsActive)
          {
            foreach (var notifyPerson in notifyGroup)
            {
              if (notifyPerson.IsActive && notifyPerson.IsEmailActive && !emailAddressesUsed.Contains(notifyPerson.EmailAddress))
              {
                var emailMessage = new EmailNotificationMessage();
                emailMessage.NotificationMessageSubject = notification.Subject;
                emailMessage.IsBodyHtml = notification.IsBodyHtml;
                emailMessage.NotificationMessageBody = notification.Body;
                emailMessage.NotificationAddresses.Add(new EmailNotificationAddress(NotificationAddressType.EmailToAddress, notifyPerson.EmailAddress));
                emailAddressesUsed.Add(notifyPerson.EmailAddress);
                emailMessage.NotificationAddresses.Add(new EmailNotificationAddress(NotificationAddressType.EmailFromAddress, _smtpParms.EmailFromAddress));
                var emailTaskResult = ProcessEmailTask(_smtpParms, emailMessage);
                taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, emailTaskResult);
              }

              if (notifyPerson.IsSmsActive)
              {


              }
            }
          }
        }


        SendNotifyAction(NotifyEventType.NotificationCompleted, true);

        int emailSendsAttempted = taskResult.TaskResultSet.Count;
        int emailSendsSuccessful = taskResult.TaskResultSet.Values.Where(e => e.TaskResultStatus == TaskResultStatus.Success).Count();
        int emailSendsNotSuccessful = taskResult.TaskResultSet.Values.Where(e => e.TaskResultStatus != TaskResultStatus.Success).Count();
        taskResult.TaskResultStatus = emailSendsNotSuccessful == 0 ? TaskResultStatus.Success : TaskResultStatus.Warning;
        taskResult.Message = "Email sends attempted: " + emailSendsAttempted.ToString() + ", sends successful: " +
                             emailSendsSuccessful.ToString() + ", sends failed: " + emailSendsNotSuccessful.ToString();

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

    private TaskResult ProcessEmailTask(SmtpParms smtpParms, EmailNotificationMessage emailMessage)
    {
      using (var emailProcessor = new EmailNotificationProcessor())
      {
        return emailProcessor.SendMessage(smtpParms, emailMessage);
      }
    }

    private async Task<TaskResult> ProcessEmailTaskAsync(SmtpParms smtpParms, EmailNotificationMessage emailMessage)
    {
      using (var emailProcessor = new EmailNotificationProcessor())
      {
        return await emailProcessor.SendMessageAsync(smtpParms, emailMessage);
      }
    }


    public void TestEvent(NotifyEventType eventType)
    {
      if (NotifyAction == null)
        return;

      NotifyAction(new NotifyEventArgs(eventType, false));
    }

    private void SendNotifyAction(NotifyEventType eventType, bool isSync)
    {
      if (NotifyAction == null)
        return;

      NotifyAction(new NotifyEventArgs(eventType, isSync));
    }

    public void Dispose()
    {
    }
  }
}

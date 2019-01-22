using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Threading = System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.Ops.Business;
using Org.TP.Concrete;
using Org.TSK.Business;
using Org.TSK.Business.Models;
using Microsoft.Exchange.WebServices.Data;

namespace Org.Ops.Tasks
{
  public class OverdueTaskAcknowledgementMonitor : TaskProcessorBase
  {
    private ConfigDbSpec _tasksDbSpec;
    private ConfigSmtpSpec _exchangeSmtpSpec;
    private StringBuilder _headerMessage;
    private StringBuilder _approvedMessage;
    private StringBuilder _rejectedMessage;
    private StringBuilder _errorMessage;

    private List<Item> _successItems = new List<Item>();
    private List<Item> _errorItems = new List<Item>();

    public override async Threading.Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();

      _headerMessage = new StringBuilder();
      _approvedMessage = new StringBuilder();
      _rejectedMessage = new StringBuilder();
      _errorMessage = new StringBuilder();
      try
      {
        taskResult = await Threading.Task.Run<TaskResult>(() =>
        {
          this.Initialize();

          _tasksDbSpec = GetParmValue("TasksDbSpec") as ConfigDbSpec;

          _exchangeSmtpSpec = GetParmValue("ExchangeSmtpSpec") as ConfigSmtpSpec;
          _exchangeSmtpSpec.SmtpPassword = TokenMaker.DecodeToken2(_exchangeSmtpSpec.SmtpPassword);

          ExchangeServiceManager exServMgr = new ExchangeServiceManager(_exchangeSmtpSpec);

          List<ScheduledTask> monitoredTasks;
          using (var taskRepo = new TaskRepository(_tasksDbSpec))
            monitoredTasks = taskRepo.GetMonitoredTasks();

          List<Item> items = exServMgr.GetItems("Overdue Inbox");
          if (items.Count == 0)
          {
            taskResult.NoWorkDone = true;
            return taskResult.Success("No emails in the Overdue TaskAcknowledgements inbox.");
          }

          _headerMessage.Append("Action".PadTo(13) + "Task Name".PadTo(35) + "PeriodStart".PadTo(20) + "Sender" + g.crlf);
          _headerMessage.Append("--------------------------------------------------------------------------------" + g.crlf);

          

          foreach (var item in items)
          {
            if (item.GetType() != typeof(EmailMessage))
            {
              _errorItems.Add(item);
              continue;
            }

            var email = item as EmailMessage;

            var subject = email.Subject.ToLower();
            if (!(subject.StartsWith("approve: overdue task") || subject.StartsWith("reject: overdue task")))
            {
              _errorItems.Add(item);
              continue;
            }

            string taskName = email.Subject.Trim().Split(Constants.DashDelimiter, StringSplitOptions.RemoveEmptyEntries).ToArray()[1].Trim();

            //if (!monitoredTasks.Any(t => t.TaskName == taskName))
            //  continue;

            int index = email.Subject.IndexOf("PeriodStart:");
            DateTime periodStart = email.Subject.Substring(index + 13).ToDateTime();
            string sender = email.From.Address;

            email.IsRead = true;
            email.Update(ConflictResolutionMode.AutoResolve);

            _successItems.Add(item);

            if (email.Subject.ToLower().StartsWith("reject: overdue task"))
            {
              _rejectedMessage.Append("REJECTED".PadTo(13) + taskName.PadTo(35) + periodStart.ToString("yyyy-MM-dd HH:mm:ss").PadTo(20) + sender + g.crlf);
              continue;
            }

            //var monitoredTask = monitoredTasks.First(t => t.TaskName == taskName);
            //if (monitoredTask.LastAcknowledgedPeriod >= periodStart)
            //  continue;

            //monitoredTask.LastAcknowledgedPeriod = periodStart;
            //monitoredTask.AcknowledgedBy = sender;
            //monitoredTask.AcknowledgedDate = email.DateTimeReceived;
            //monitoredTask.ModifiedBy = g.SystemInfo.DomainAndUser;
            //monitoredTask.ModifiedDate = DateTime.Now;

            using (var taskRepo = new TaskRepository(_tasksDbSpec))
            {
              //if (overdueTaskNotificationsSent.ContainsKey(taskName))
              //  taskRepo.UpdateTaskParameterByNameAndParmSetName(taskNotifyParm, IsDryRun);
              //else
              //{
              //  taskRepo.InsertTaskParameter(taskNotifyParm, IsDryRun);
              //  overdueTaskNotificationsSent.Add(taskName, periodStart);
              //}
            }

            _approvedMessage.Append("ACKNOWLEDGED".PadTo(13) + taskName.PadTo(35) + periodStart.ToString("yyyy-MM-dd HH:mm:ss").PadTo(20) + sender + g.crlf);
          }

          if (IsDryRun)
            taskResult.NoWorkDone = true;
          else
            exServMgr.MoveItems(_successItems, "Overdue Processed");

          string successMessage = _headerMessage.ToString() + _approvedMessage.ToString() + _rejectedMessage;

          if (_errorItems.Count > 0)
          {
            _errorMessage.Append("OverdueTaskAcknowledgementMonitor could not handle the following emails in the inbox:" + g.crlf);
            foreach (var item in _errorItems)
              _errorMessage.Append("ReceivedOn: " + item.DateTimeReceived.ToString("yyyy-MM-dd HH:mm:ss") + " Subject: " + item.Subject + g.crlf);
            string fullMessage = successMessage + g.crlf + _errorMessage.ToString();
            exServMgr.MoveItems(_errorItems, "Overdue Error");
            return taskResult.Warning(fullMessage);
          }

          return taskResult.Success(successMessage);
        });

        return taskResult;
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing.", ex);
      }
    }

    protected override void Initialize()
    {
      base.Initialize();

      AssertParmExistence("TasksDbSpec");
      AssertParmExistence("ExchangeSmtpSpec");
    }
  }
}

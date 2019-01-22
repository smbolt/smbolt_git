using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.Ops.Business;
using Org.TP.Concrete;
using Org.TSK;
using Org.TSK.Business;
using Org.TSK.Business.Models;
using tsk = Org.TSK.Business.Models;

namespace Org.Ops.Tasks
{
  public class TaskMonitor : TaskProcessorBase
  {
    private ConfigDbSpec _tasksDbSpec;
    private ConfigDbSpec _notifyDbSpec;
    private NotifyConfigSet _notifyConfigs;
    private ConfigSmtpSpec _exchangeSmtpSpec;
    private ExchangeServiceManager _exchangeSvcMgr;
    private StringBuilder _message;
    private bool _sentOverdueEmail = false;
    private DateTime _currentDateTime;
    private string _defaultEventName;

    public override async Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();

      _message = new StringBuilder();
      try
      {
        taskResult = await Task.Run<TaskResult>(() =>
        {
          this.Initialize();

          _tasksDbSpec = GetParmValue("TasksDbSpec") as ConfigDbSpec;

          _notifyDbSpec = GetParmValue("NotifyDbSpec") as ConfigDbSpec;

          _defaultEventName = GetParmValue("DefaultEventName").ToString();

          _currentDateTime = DateTime.Now;

          var monitoredTasks = new ScheduledTaskSet();
          using (var taskRepo = new TaskRepository(_tasksDbSpec))
            monitoredTasks = taskRepo.GetMonitoredTasks();

          using (var notifyRepo = new NotifyRepository(_notifyDbSpec))
            _notifyConfigs = notifyRepo.GetNotifyConfigSet("IM-AppIntegrationOps", true);

          _exchangeSmtpSpec = GetParmValue("ExchangeSmtpSpec") as ConfigSmtpSpec;
          _exchangeSmtpSpec.SmtpPassword = TokenMaker.DecodeToken2(_exchangeSmtpSpec.SmtpPassword);

          _exchangeSvcMgr = new ExchangeServiceManager(_exchangeSmtpSpec);

          _message.Append("Task Name".PadTo(35) + "Start of Period".PadTo(20) + "Time Overdue".PadTo(20) + "Notification Recipients" + g.crlf);
          _message.Append("----------------------------------------------------------------------------------------------------------" + g.crlf);

          foreach (var task in monitoredTasks)
          {
            if (!task.RunUntilTask || !task.IsActive)
              continue;

            tsk.TaskScheduleElement overdueElement = task.TaskSchedule.TaskScheduleElements.FirstOrDefault();

            PeriodHistory currentPeriodHistory = GetPeriodHistoryRecord(overdueElement, task.CurrentPeriod);
            PeriodHistory previousPeriodHistory = GetPeriodHistoryRecord(overdueElement, task.PreviousPeriod);

            if (!currentPeriodHistory.OverdueNoticeAcknowledged && currentPeriodHistory.OverdueDateTime < _currentDateTime)
            {
              SendOverdueEmail(overdueElement.ScheduledTask, currentPeriodHistory);
            }

            if (!previousPeriodHistory.OverdueNoticeAcknowledged && currentPeriodHistory.OverdueDateTime < _currentDateTime)
            {
              SendOverdueEmail(overdueElement.ScheduledTask, previousPeriodHistory);
            }
          }

          if (IsDryRun)
            taskResult.NoWorkDone = true;

          if (!_sentOverdueEmail)
            return taskResult.Success("No overdue tasks.");

          return taskResult.Success(_message.ToString());
        });

        return taskResult;
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing.", ex);
      }
    }

    private PeriodHistory GetPeriodHistoryRecord(tsk.TaskScheduleElement overdueElement, PeriodInterval periodInterval)
    {
      try
      {
        if (!periodInterval.StartDateTime.HasValue || !periodInterval.EndDateTime.HasValue)
          throw new Exception("PeriodInterval Start or End DateTime was null for ScheduledTaskID: " + overdueElement.ScheduledTask.ScheduledTaskId);

        PeriodHistory periodHistory;
        using (var taskRepo = new TaskRepository(_tasksDbSpec))
          periodHistory = taskRepo.GetPeriodHistory(overdueElement.ScheduledTask.ScheduledTaskId, periodInterval);

        if (periodHistory != null)
        {
          if (!periodHistory.OverdueDateTime.HasValue)
          {
            periodHistory.OverdueDateTime = overdueElement.GetOverdueDateTime(periodInterval);
            if (!periodHistory.OverdueDateTime.HasValue)
              throw new Exception("Could not determine the OverdueDateTime for ScheduledTaskID: " + overdueElement.ScheduledTask.ScheduledTaskId);
          }

          return periodHistory;
        }

        periodHistory = new PeriodHistory();
        periodHistory.ScheduledTaskId = overdueElement.ScheduledTask.ScheduledTaskId;
        periodHistory.TaskName = overdueElement.ScheduledTask.TaskName;

        periodHistory.PeriodInterval = periodInterval;
        periodHistory.RunUntilTask = overdueElement.ScheduledTask.RunUntilTask;
        periodHistory.RunUntilPeriodContextId = overdueElement.ScheduledTask.RunUntilPeriodContextID;
        periodHistory.RunUntilOffsetMinutes = overdueElement.ScheduledTask.RunUntilOffsetMinutes;

        periodHistory.OverdueDateTime = overdueElement.GetOverdueDateTime(periodInterval);

        if (!periodHistory.OverdueDateTime.HasValue)
          throw new Exception("Could not determine the OverdueDateTime for ScheduledTaskID: " + overdueElement.ScheduledTask.ScheduledTaskId);

        using (var taskRepo = new TaskRepository(_tasksDbSpec))
          taskRepo.InsertPeriodHistory(periodHistory);

        return periodHistory;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get the PeriodHistory record for ScheduledTaskID: " + overdueElement.ScheduledTask.ScheduledTaskId, ex);
      }
    }

    private void SendOverdueEmail(ScheduledTask overdueTask, PeriodHistory ph)
    {
      _sentOverdueEmail = true;
      _message.Append(overdueTask.TaskName.PadTo(35) + ph.PeriodInterval.StartDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss").PadTo(20) + ph.OverdueDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss").PadTo(20));

      string eventName;
      if (_notifyConfigs.ContainsConfigForNamedEvent(_defaultEventName.Replace("$TASKNAME$", overdueTask.TaskName)))
        eventName = _defaultEventName.Replace("$TASKNAME$", overdueTask.TaskName);
      else if (_notifyConfigs.ContainsConfigForNamedEvent(_defaultEventName))
        eventName = _defaultEventName;
      else
        return;

      string subject = overdueTask.TaskName + " is overdue - Period Start: " + ph.PeriodInterval.StartDateTime.Value.ToString("MM/dd/yyyy HH:mm");
      string body = "Scheduled Task '" + overdueTask.TaskName + "' was expected to run by " + ph.OverdueDateTime.Value.ToString("MM/dd/yyyy HH:mm") + "." + g.crlf +
                    "Period Start: " + ph.PeriodInterval.StartDateTime.Value.ToString("MM/dd/yyyy HH:mm") + g.crlf +
                    "Period End: " + ph.PeriodInterval.EndDateTime.Value.ToString("MM/dd/yyyy HH:mm") + g.crlf2 +
                    "Vote 'Approve' or reply 'STOP' to stop emails for the remainder of the period.";

      ExchangeEmail email = new ExchangeEmail(eventName, _notifyConfigs, body, subject, ExchangeEmailType.Voting);

      //_exchangeSvcMgr.SendEmails(email);

      //_message.Append(String.Join(",", email.EmailMessage.ToRecipients.Select(r => r.Address)) + g.crlf);   
    }

    protected override void Initialize()
    {
      base.Initialize();

      AssertParmExistence("TasksDbSpec");
      AssertParmExistence("NotifyDbSpec");
      AssertParmExistence("DefaultEventName");
      AssertParmExistence("ExchangeSmtpSpec");
    }
  }
}

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
    private ExchangeServiceManager _exchangeSvcMgr;
    private StringBuilder _message;
    private bool _sentOverdueEmail = false;

    public override async Threading.Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();

      _message = new StringBuilder();
      try
      {
        taskResult = await Threading.Task.Run<TaskResult>(() =>
        {
          this.Initialize();

          string monitorFrequency = GetParmValue("MonitorFrequency").ToString();

          _tasksDbSpec = GetParmValue("TasksDbSpec") as ConfigDbSpec;

          _notifyDbSpec = GetParmValue("NotifyDbSpec") as ConfigDbSpec;

          //var monitoredTasks = new List<MonitoredTask>();
          //using (var taskRepo = new TaskRepository(_tasksDbSpec))
            //monitoredTasks = taskRepo.GetMonitoredTasks(monitorFrequency);

          using (var notifyRepo = new NotifyRepository(_notifyDbSpec))
            _notifyConfigs = notifyRepo.GetNotifyConfigSet("IM-AppIntegrationOps", true);

          var opsEmailAddress = GetParmValue("OpsEmailAddress").ToString();
          var tokenizedPassword = GetParmValue("OpsEmailPassword").ToString();

          _exchangeSvcMgr = new ExchangeServiceManager(opsEmailAddress, TokenMaker.GenerateToken2(tokenizedPassword));
          tokenizedPassword = null;

          _message.Append("Task Name".PadTo(35) + "Start of Period".PadTo(20) + "Time Overdue".PadTo(20) + "Notification Recipients" + g.crlf);
          _message.Append("----------------------------------------------------------------------------------------------------------" + g.crlf);
          
          //foreach (var monitoredTask in monitoredTasks)
          //{
          //  if (monitoredTask.ScheduledTask == null)
          //  {
          //    _message.Insert(0, "Could not find ScheduledTaskID '" + monitoredTask.ScheduledTaskId + "' in the TaskScheduling database." + g.crlf);
          //    continue;
          //  }

          //  if (!monitoredTask.ScheduledTask.RunUntilTask || !monitoredTask.ScheduledTask.IsActive)
          //    continue;

          //  DateTime currentDate = DateTime.Now.Date;

          //  int offsetMinutes = monitoredTask.ScheduledTask.RunUntilOffsetMinutes.HasValue ? monitoredTask.ScheduledTask.RunUntilOffsetMinutes.Value : 0;

          //  DateTime periodStart;
          //  DateTime overdueTime;
          //  DateTime periodEnd;
          //  switch (monitoredTask.ScheduledTask.RunUntilPeriodContextID.ToEnum<PeriodContexts>(PeriodContexts.NotSet))
          //  {
          //    case PeriodContexts.Day:
          //      periodStart = currentDate.AddMinutes(offsetMinutes);
          //      periodEnd = currentDate.AddDays(1).AddMilliseconds(-1);
          //      break;

          //    case PeriodContexts.Week:
          //      int diff = currentDate.DayOfWeek - DayOfWeek.Sunday;
          //      periodStart = currentDate.AddDays(-1 * diff).Date.AddMinutes(offsetMinutes);
          //      periodEnd = periodStart.AddDays(7).AddMilliseconds(-1);
          //      break;

          //    case PeriodContexts.Month:
          //      periodStart = currentDate.FirstDayOfMonth().AddMinutes(offsetMinutes);
          //      periodEnd = periodStart.AddMonths(1).AddMilliseconds(-1);
          //      break;

          //    case PeriodContexts.Quarter:
          //      int quarter = currentDate.Month / 3 + 1;
          //      periodStart = new DateTime(currentDate.Year, quarter * 3 - 2, 1).AddMinutes(offsetMinutes);
          //      periodEnd = periodStart.AddMonths(3).AddMilliseconds(-1);
          //      break;

          //    case PeriodContexts.Year:
          //      periodStart = new DateTime(currentDate.Year, 1, 1).AddMinutes(offsetMinutes);
          //      periodEnd = periodStart.AddYears(1).AddMilliseconds(-1);
          //      break;

          //    default:
          //      continue;
          //  }

          //  decimal decHours = monitoredTask.HoursUntilOverdue;
          //  int hours = (int)decHours;
          //  int minutes = (int)(60 * (decHours - hours));
          //  overdueTime = periodStart.AddHours(hours).AddMinutes(minutes);

          //  //skip on weekend if configured to do so
          //  if (!monitoredTask.NotifyOnWeekend)
          //  {
          //    if (currentDate.DayOfWeek.ToInt32() == 1 && overdueTime.DayOfWeek.ToInt32().In("0,6"))
          //      continue;
          //    if (currentDate.DayOfWeek.ToInt32().In("0,6"))
          //      continue;
          //  }

          //  //skip if not past Overdue Time
          //  if (DateTime.Now < overdueTime)
          //    continue;

          //  //skip if previous notification has been acknowledged past Overdue Time
          //  if (monitoredTask.AcknowledgedDate != null && periodStart <= monitoredTask.AcknowledgedDate)
          //    continue;

          //  //Send notification if there is not a last successful run
          //  if (monitoredTask.LastSuccessfulRun == null)
          //  {
          //    SendOverdueEmail(monitoredTask.ScheduledTask, overdueTime, periodStart);
          //    continue;
          //  }

          //  if (overdueTime > monitoredTask.LastSuccessfulRun)
          //    SendOverdueEmail(monitoredTask.ScheduledTask, overdueTime, periodStart);
          //}

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

    private void SendOverdueEmail(ScheduledTask overdueTask, DateTime overdueTime, DateTime periodStart)
    {
      _sentOverdueEmail = true;
      _message.Append(overdueTask.TaskName.PadTo(35) + periodStart.ToString("yyyy-MM-dd HH:mm:ss").PadTo(20) + overdueTime.ToString("yyyy-MM-dd HH:mm:ss").PadTo(20));
      List<string> emailAddresses = new List<string>();
      string eventName;
      if (_notifyConfigs.ContainsConfigForNamedEvent(overdueTask.TaskName + "_Overdue"))
        eventName = overdueTask.TaskName + "_Overdue";
      else if (_notifyConfigs.ContainsConfigForNamedEvent("$TASKNAME$_Overdue"))
        eventName = "$TASKNAME$_Overdue";
      else
        return;

      //NotifyConfig notifyConfig = _notifyConfigs.GetConfigForNamedEvent(eventName);
      //emailAddresses = notifyConfig.GetActiveEmailAddressesForNamedEvent(eventName);

      if (emailAddresses.Count == 0)
      {
        _message.Append("No Email Recipients" + g.crlf);
        return;
      }

      string subject = "Overdue Task - " + overdueTask.TaskName + " - PeriodStart: " + periodStart.ToString("MM/dd/yyyy HH:mm");
      string body = "Scheduled Task '" + overdueTask.TaskName + "' was expected to run by " +
                          overdueTime.ToString("MM/dd/yyyy HH:mm") + ". Vote 'Approve' to stop emails for the remainder of the period.";

      _exchangeSvcMgr.SendVotingEmail(subject, body, emailAddresses);

      _message.Append(String.Join(",", emailAddresses) + g.crlf);   
    }

    static bool RedirectionCallback(string url)
    {
      // Return true if the URL is an HTTPS URL.
      return url.ToLower().StartsWith("https://");
    }

    protected override void Initialize()
    {
      base.Initialize();

      AssertParmExistence("TasksDbSpec");
      AssertParmExistence("NotifyDbSpec");
      AssertParmExistence("OpsEmailAddress");
      AssertParmExistence("OpsEmailPassword");
      AssertParmExistence("MonitorFrequency");
    }
  }
}

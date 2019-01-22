using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Threading.Tasks;
using Org.WinSvc;
using Org.TP;
using Org.GS.Logging;
using Org.GS;

namespace Org.PF
{
  public class PerfEngine : WinServiceEngineBase
  {
    private TaskRequestSet _taskRequests;

    public PerfEngine(WinServiceParms winServiceParms)
      : base(winServiceParms, "PerfEngine")
    {
      base.EntityId = base.WinServiceParms.EntityId > 0 ? base.WinServiceParms.EntityId : 306;
      base.Logger.EntityId = base.EntityId;

      StartupLogging.StartupLogPath = g.ExecutablePath + @"\StartupLogging";
      StartupLogging.WriteStartupLog("PerfService starting up - at start of " + base.EngineName + " constructor");

      Initialize();
    }


    private void ProcessTasks()
    {
      try
      {
        do
        {
          if (base.CancellationTokenSource.IsCancellationRequested)
          {
            string cancelMessage = base.ServiceName + " - task processing (thread) has been canceled (Code 6034).";
            base.Logger.Log(cancelMessage, 6034);
            base.ProcessNotifications(cancelMessage, String.Empty, base.DefaultNotifyEventName);
            return;
          }

          while (base.WinServiceState == WinServiceState.Paused)
          {
            Thread.Sleep(base.WinServiceParms.SleepInterval);
            continue;
          }

          int waitInterval = base.WinServiceParms.MaxWaitIntervalMilliseconds;
          System.Threading.Thread.Sleep(waitInterval);

          TaskRequest taskRequest = null;

          if (base.GetNextTaskNow)
          {
            taskRequest = _taskRequests.GetNextNow();
            base.GetNextTaskNow = false;
          }
          else
          {
            taskRequest = _taskRequests.GetNext();
          }

          int tasksToProcessCount = _taskRequests.TasksToProcessCount;

          if (taskRequest == null)
          {
            var nextRequest = _taskRequests.PeekNext();
            string nextTaskReport = nextRequest == null ? String.Empty : " - next is " + nextRequest.TaskName + " time remaining is " + nextRequest.RemainingTimeTilScheduleFmt;

            if (base.WinServiceParms.InDiagnosticsMode)
            {
              this.NotifyHostEvent("No task to process - remaining task count is " + tasksToProcessCount.ToString() + nextTaskReport +
                                   " - next task set refresh at " + _taskRequests.NextLoadTime.ToString("yyyyMMdd HH:mm:ss"));
            }

            CheckTaskListRefresh();
            continue;
          }

          this.TasksProcessed++;

          if (!base.WinServiceParms.SuppressNonErrorOutput)
          {
            this.NotifyHostEvent("The task engine will attempt to locate a task processor for and run task " + taskRequest.TaskName + " for " +
                                 taskRequest.ScheduledRunDateTime.ToString("yyyy-MM-dd HH:mm:ss") +
                                 " - taskId is " + taskRequest.TaskRequestId +
                                 " - remaining task count is " + tasksToProcessCount.ToString() + ".");
          }

          CheckTaskListRefresh();

          if (_taskRequests.CheckDiscardConcurrent(taskRequest))
          {
            this.NotifyHostEvent("Task '" + taskRequest.TaskName + "' will be discarded due to already running task of same type.");
            continue;
          }

          if (taskRequest.IsDryRun)
          {
            string parmReport = taskRequest.GetParmReport();
            ProcessNotifications(base.ServiceName + " Parms for " + taskRequest.TaskName + " @ " + taskRequest.ScheduledRunDateTime, parmReport, base.DefaultNotifyEventName);
            base.Logger.Log(LogSeverity.INFO, parmReport, 6123);
          }

          _taskRequests.AddRunningTask(taskRequest);

          Task.Run(async () =>
          {
            try
            {
              ITaskProcessor tp = new PerfTaskProcessor();
              //taskRequest.RunId = CreateRunHistory(taskRequest);
              var taskResult = await base.TaskDispatcher.DispatchTaskAsync(tp, taskRequest);
              //_taskRequests.RemoveRunningTask(taskRequest);
              Task.Run(() =>
              {
                _taskRequests.RemoveAllTaskRequestsForTaskId(taskResult);
                //UpdateRunHistory(taskResult);
                ProcessNotifications(taskResult);
              });
            }
            catch (Exception ex)
            {
              string errorMessage = "An exception occurred ....";
              this.NotifyHostEvent(errorMessage);
              _taskRequests.RemoveRunningTask(taskRequest);
              HandleExceptions(errorMessage, ex, 6126, false, true, true, false);
            }
          });

        } while (true);
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred in the ProcessTask method.", ex, 6036, false, true, true, true);
      }
    }

    public override bool Start()
    {
      try
      {
        base.ProcessNotifications(base.EngineName + " is starting", base.DefaultNotifyEventName);
        StartupLogging.WriteStartupLog("Just entered " + base.EngineName + ".Start");

        if (base.WinServiceState == WinServiceState.Paused || base.WinServiceState == WinServiceState.Running)
          return true;

        base.Logger.Log("TaskEngine start method is beginning.", 6028);

        base.TaskDispatcher = new PerfTaskDispatcher();
        base.TaskDispatcher.ContinueTask = true;
        base.TaskDispatcher.NotifyMessage += base.NotifyMessageHandler;

        _taskRequests = GetTaskRequests();

        if (base.WinServiceParms.RunWebService)
        {
          bool webServiceIsRunning = false;
          if (base.ServiceHost != null)
          {
            if (base.ServiceHost.State == CommunicationState.Opened)
              webServiceIsRunning = true;
          }

          if (!webServiceIsRunning)
          {
            StartupLogging.WriteStartupLog("Just after starting the WinService Web Service in " + base.EngineName + ".Start.");
            base.StartWebService();
          }
        }

        base.WinServiceState = WinServiceState.Running;

        base.CancellationTokenSource = new CancellationTokenSource();
        base.ProcessTasks = new Task(() => this.ProcessTasks(), base.CancellationTokenSource.Token, TaskCreationOptions.LongRunning);
        base.ProcessTasks.Start();

        StartupLogging.WriteStartupLog("ProcessTasks thread is started in " + base.EngineName + ".Start");

        StartupLogging.WriteStartupLog(base.ServiceName + " startup is complete.");
        base.Logger.Log(base.EngineName + " Start method is finished", 6029);

        NotifyHostEvent(new IpdxMessage("UI", IpdxMessageType.Notification, "STARTED"));

        StartupLogging.WriteStartupLog(base.EngineName + " has been started" + " (send to notifications)");

        string startupReport = "The service is started.";

        base.ProcessNotifications(base.EngineName + " has been successfully started", startupReport, base.DefaultNotifyEventName);

        StartupLogging.StartupComplete = true;
        return true;
      }
      catch (Exception ex)
      {
        base.HandleExceptions("An exception occurred attempting to start the service.", ex, 6030, true, true, true, true);
        return false;
      }
    }

    public override void Stop()
    {
      if (base.WinServiceState == WinServiceState.Stopped)
        return;

      try
      {
        base.WinServiceState = WinServiceState.Stopped;
        base.TaskDispatcher.ContinueTask = false;
        base.CancellationTokenSource.Cancel();

        if (base.WinServiceParms.RunWebService)
          base.StopWebService();

        base.WinServiceState = WinServiceState.Stopped;

        NotifyHostEvent(new IpdxMessage("UI", IpdxMessageType.Notification, "STOPPED"));

        base.Logger.Log("TaskEngine has been stopped.", 6113);
        base.ProcessNotifications(base.ServiceName + " TaskEngine has been stopped (Code 6113)", String.Empty, base.DefaultNotifyEventName);
      }
      catch (Exception ex)
      {
        base.HandleExceptions("An exception occurred attempting to stop the service.", ex, 6031, false, true, true, false);
      }
    }

    public override void Pause()
    {
      try
      {
        base.WinServiceState = WinServiceState.Paused;
        base.TaskDispatcher.ContinueTask = false;

        if (base.WinServiceParms.RunWebService)
          base.StopWebService();

        NotifyHostEvent(new IpdxMessage("UI", IpdxMessageType.Notification, "PAUSED"));

        base.Logger.Log(base.EngineName + " has been paused.", 9999);
        base.ProcessNotifications(base.ServiceName + " " + base.EngineName + " has been paused (Code 9999)", String.Empty, base.DefaultNotifyEventName);
      }
      catch (Exception ex)
      {
        base.HandleExceptions("An exception occurred attempting to pause the service.", ex, 6032, false, true, true, true);
      }
    }

    public override void Resume()
    {
      //// NEED TO GET THIS IMPLEMENTED
    }

    private void Initialize()
    {
      base.InitializeBase();

      try
      {
        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' POINT 1.  Beginning " + base.EngineName + ".Initialize.");

        WireUpSuperMethod();

        if (base.WinServiceParms.IsRunningAsWindowsService)
        {
          // do stuff specific to the functionality of the PerfEngine
        }


        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' POINT x.  In " + base.EngineName + ".Initialize.");
      }
      catch (Exception ex)
      {
        base.HandleExceptions("An exception occurred during " + base.EngineName + " initialization.", ex, 6027, true, true, true, true);
      }
    }

    public TaskRequestSet GetTaskRequests()
    {
      try
      {
        var taskRequests = new TaskRequestSet(base.WinServiceParms.MaxWaitIntervalMilliseconds);


        this.NotifyHostEvent("Loading list of Task Requests to process");

        //base.Logger.Log("Building list of tasks to process - source is " + _taskEngineParms.TaskScheduleMode.ToString() + ".", 6065);


        //taskRequests.NextLoadTime = DateTime.Now.AddSeconds((int)(_taskLoadInterval.TotalSeconds * 0.8));

        string taskReport = taskRequests.TaskReport;

        base.Logger.Log(taskReport, 6061);
        this.NotifyHostEvent(taskReport);

        //if (_notifyOnGetTasks)
        //{
        //  //ProcessNotifications(base.WinServiceParms.WindowsServiceName + " Task Schedule Reloaded from " + _taskEngineParms.TaskScheduleMode.ToString().Replace("From", String.Empty),
        //  //                     "The task schedule has been reloaded." + g.crlf2 + taskReport, base.DefaultNotifyEventName);
        //}

        return taskRequests;
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred while attempting to build the TaskRequestSet.", ex, 6060, false, true, true, true);
        return null;
      }
    }

    private void CheckTaskListRefresh()
    {
      try
      {
        if (_taskRequests.NextLoadTime < DateTime.Now || base.RefreshTaskRequests || base.RefreshTaskList)
        {
          base.RefreshTaskList = false;
          if (base.WinServiceParms.SuppressTaskReload)
          {
            //_taskRequests.NextLoadTime = DateTime.Now.AddSeconds((int)(_taskLoadInterval.TotalSeconds * 0.8));
          }
          else
          {
            //_taskRequests = GetTaskRequests(_taskEngineParms.TasksToRun);
          }
        }
      }
      catch (Exception ex)
      {
        //_taskRequests.NextLoadTime = DateTime.Now.AddSeconds((int)(_taskLoadInterval.TotalSeconds * 0.8));
        base.RefreshTaskRequests = false;
        HandleExceptions("An exception occurred in the CheckTaskListRefresh method.", ex, 6125, false, true, true, true);
      }
    }

    private void WireUpSuperMethod()
    {
      base.WireUpSuperMethod(SuperMethod);
    }

    private TaskResult SuperMethod(string command, object[] parms)
    {
      TaskResult taskResult = new TaskResult(command);

      switch (command)
      {
        case "StartService":
          this.Start();
          break;

        case "StopService":
          this.Stop();
          break;

        case "PauseService":
          this.Pause();
          break;

        case "ResumeService":
          this.Resume();
          break;
      }

      taskResult.Message = "Command '" + command + "' is not implemented in the SuperMethod method in the " + base.EngineName + ".";
      return taskResult.Failed();
    }

    public new void Dispose()
    {
      base.Dispose();
    }
  }
}

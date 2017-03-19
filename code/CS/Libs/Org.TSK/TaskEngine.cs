using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Org.TSK.Business.Models;
using Org.TSK.Business;
using Org.TP;
using Org.WSO.Transactions;
using Org.WSO;
using Org.WinSvc;
using Org.Notify;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS.Notifications;
using Org.GS;

namespace Org.TSK
{
  public class TaskEngine : WinServiceEngineBase
  {
    private TaskEngineParms _taskEngineParms;

    // MEF-Related objects
    [ImportMany(typeof(ITaskProcessorFactory))]
    private IEnumerable<Lazy<ITaskProcessorFactory, ITaskProcessorMetadata>> taskProcessorFactories;
    private CompositionContainer CompositionContainer;
    private Dictionary<string, ITaskProcessorFactory> LoadedTaskProcessorFactories;

    private string _taskRequiringTaskRequestUpdate = String.Empty;
    private ConfigDbSpec _tasksDbSpec;
    
    public TaskEngine(WinServiceParms winServiceParms, TaskEngineParms taskEngineParms)
      : base(winServiceParms, "TaskEngine")
    {
      _taskEngineParms = taskEngineParms;

      base.EntityId = base.WinServiceParms.EntityId > 0 ? base.WinServiceParms.EntityId : 306;
      base.Logger.EntityId = base.EntityId;

      StartupLogging.StartupLogPath = g.ExecutablePath + @"\StartupLogging";
      StartupLogging.WriteStartupLog("WinService starting up - at start of " + base.EngineName + " constructor");

      Initialize();

      StartupLogging.WriteStartupLog("WinService starting up - at end of " + base.EngineName + " constructor");
    }

    private void Initialize()
    {
      base.InitializeBase();

      try
      {
        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + 
              "' POINT 1.  Beginning " + base.EngineName + ".Initialize.");

        WireUpSuperMethod();

        if (base.WinServiceParms.IsRunningAsWindowsService)
        {
          _taskEngineParms.TaskScheduleMode = g.ToEnum<TaskScheduleMode>(g.CI("TaskScheduleMode"), TaskScheduleMode.FromDatabase);
          _taskEngineParms.TaskLoadIntervalSeconds = g.CI("TaskLoadIntervalSeconds").ToInt32OrDefault(1200);
          _taskEngineParms.TasksDbSpecPrefix = g.CI("TasksDbSpecPrefix").OrDefault("Tasks");
          _taskEngineParms.TaskProfile = g.CI("TaskProfile").OrDefault("Normal");
          _taskEngineParms.WSHTaskProfile = g.CI("WSHTaskProfile").OrDefault("FastTest");
          _taskEngineParms.MEFModulesPath = g.CI("MEFModulesPath");
          _taskEngineParms.LimitMEFImports = g.CI("LimitMEFImports").ToBoolean();
          _taskEngineParms.MEFLimitListName = g.CI("MEFLimitListName");
          _taskEngineParms.TaskLoadIntervalSeconds = g.GetCI("TaskLoadIntervalSeconds").ToInt32OrDefault(1200); 
        }

        if (_taskEngineParms.TaskScheduleMode == TaskScheduleMode.FromDatabase)
          _tasksDbSpec = g.GetDbSpec(_taskEngineParms.TasksDbSpecPrefix);

        if (_taskEngineParms.TaskScheduleMode == TaskScheduleMode.FromDatabase && !_tasksDbSpec.IsReadyToConnect())
          throw new Exception("The TasksConfigDbSpec is not ready to connect to the database - connection string prefix is '" + _taskEngineParms.TasksDbSpecPrefix + ".");        
        
        if (_taskEngineParms.MEFModulesPath == "$MEFCATALOG$" || _taskEngineParms.MEFModulesPath.IsBlank())
          _taskEngineParms.MEFModulesPath = g.MEFCatalog;

        if (!Directory.Exists(_taskEngineParms.MEFModulesPath))
          throw new Exception("The value of the configuration item 'MEFModulesPath' is not an existing directory '" + _taskEngineParms.MEFModulesPath + "'.");

        base.Logger.Log("Modules will be loaded from path '" + _taskEngineParms.MEFModulesPath + "'.", 6062);
        this.NotifyHostEvent("Modules will be loaded from path '" + _taskEngineParms.MEFModulesPath + "'.");

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' POINT 4.  In " + base.EngineName + ".Initialize.");
        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "Starting to load MEF Catalog.");

        List<string> mefImportList = new List<string>();

        if (_taskEngineParms.LimitMEFImports && g.AppConfig.ContainsList(_taskEngineParms.MEFLimitListName))
          mefImportList = g.GetList(_taskEngineParms.MEFLimitListName);

        using (var catalog = new AggregateCatalog())
        {
          var mefCatalog = new OSFolder(_taskEngineParms.MEFModulesPath);
          mefCatalog.SearchParms.ProcessChildFolders = true;
          var leafFolders = mefCatalog.GetLeafFolders();

          foreach (string leafFolder in leafFolders)
          {
            if (_taskEngineParms.LimitMEFImports)
            {
              string versionFolderName = Path.GetDirectoryName(leafFolder);
              string mefLeafFolder = Path.GetFileName(versionFolderName);
              if (!mefImportList.Contains(mefLeafFolder))
                continue;
            }

            StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + " Loading MEF Catalog - path: " + leafFolder); 
            catalog.Catalogs.Add(new DirectoryCatalog(leafFolder));
          }

          this.CompositionContainer = new CompositionContainer(catalog);

          try
          {
            this.CompositionContainer.ComposeParts(this);
          }
          catch (CompositionException ex)
          {
            throw new Exception("An exception occurred attempting to compose MEF components.", ex);
          }
        }

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "MEF Catalog is loaded.");

        this.LoadedTaskProcessorFactories = new Dictionary<string, ITaskProcessorFactory>();
        this.TasksProcessed = 0;

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + 
                                       "' POINT 5.  Finished " + base.EngineName + ".Initialize.");
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred during " + base.EngineName + " initialization.", ex, 6027, true, true, true, true); 
      }
    }

    private void GenerateException()
    {
      int denominator = 1;
      int numerator = 3;
      float badNumber = numerator / (denominator - 1);
    }

    public override bool Start()
    {
      try
      {
        ProcessNotifications(base.EngineName + " is starting", base.DefaultNotifyEventName);
        StartupLogging.WriteStartupLog("Just entered " + base.EngineName + ".Start");

        if (base.WinServiceState == WinServiceState.Paused || base.WinServiceState == WinServiceState.Running)
          return true;

        base.Logger.Log(base.EngineName + " start method is beginning.", 6028);
          
        base.TaskDispatcher = new TaskDispatcher(this.LoadedTaskProcessorFactories);
        base.TaskDispatcher.ContinueTask = true;
        base.TaskDispatcher.NotifyMessage += base.NotifyMessageHandler;
        
        _taskEngineParms.TasksToRun = g.AppConfig.GetList("TasksToRun");
        base.Logger.Log(_taskEngineParms.TasksToRunReport, 6116); 

        StartupLogging.WriteStartupLog(base.ServiceName + " is configured to run the following tasks:" + _taskEngineParms.TasksToRunReport);

        base.TaskRequests = GetTaskRequests(_taskEngineParms.TasksToRun);

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

        StartupLogging.WriteStartupLog("ProcessTasks thread is started - - in " + base.EngineName + ".Start.");

        StartupLogging.WriteStartupLog("WinService startup is complete.");
        base.Logger.Log(base.EngineName + " Start method is finished", 6029);

        NotifyHostEvent(new IpdxMessage("UI", IpdxMessageType.Notification, "STARTED"));

        StartupLogging.WriteStartupLog(base.EngineName + " has been started" + " (send to notifications)");

        string startupReport = "The service is started and configured to run the following tasks:" + g.crlf + _taskEngineParms.TasksToRunReport;

        base.ProcessNotifications(base.EngineName + " has been successfully started", startupReport, base.DefaultNotifyEventName);

        StartupLogging.StartupComplete = true;
        return true;
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred attempting to start the service.", ex, 6030, true, true, true, true);
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

        base.Logger.Log(base.EngineName + " has been stopped.", 6113);
        base.ProcessNotifications(base.ServiceName + " " + base.EngineName + " has been stopped (Code 6113)", String.Empty, base.DefaultNotifyEventName);
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred attempting to stop the service.", ex, 6031, false, true, true, false);
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

        base.Logger.Log(base.EngineName + " has been paused.", 6114);
        ProcessNotifications(base.ServiceName + " " + base.EngineName + " has been paused (Code 6114)", String.Empty, base.DefaultNotifyEventName);
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred attempting to pause the service.", ex, 6032, false, true, true, true);
      }
    }

    public override void Resume()
    {
      try
      {
        base.WinServiceState = WinServiceState.Running;
        base.TaskDispatcher.ContinueTask = true;

        if (base.WinServiceParms.RunWebService)
            StartWebService();

        NotifyHostEvent(new IpdxMessage("UI", IpdxMessageType.Notification, "RESUMED"));

        base.Logger.Log(base.EngineName + " has been resumed.", 6115);
        ProcessNotifications(base.ServiceName + " " + base.EngineName + " has been resumed (Code 6115)", String.Empty, base.DefaultNotifyEventName);
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred attempting to resume the service.", ex, 6033, false, true, true, true);
      }
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
            ProcessNotifications(cancelMessage, String.Empty, base.DefaultNotifyEventName);
            return;
          }

          while (base.WinServiceState == WinServiceState.Paused)
          {
            Thread.Sleep(base.WinServiceParms.SleepInterval);
            continue;
          }
                    
          int waitInterval = base.TaskRequests.WaitInterval;
          System.Threading.Thread.Sleep(waitInterval);

          TaskRequest taskRequest = null;

          if (base.GetNextTaskNow)
          {
            taskRequest = base.TaskRequests.GetNextNow();
            base.GetNextTaskNow = false;
          }
          else
          {
            taskRequest = base.TaskRequests.GetNext();
          }

          int tasksToProcessCount = base.TaskRequests.TasksToProcessCount;

          if (taskRequest == null)
          {
            var nextRequest = base.TaskRequests.PeekNext();
            string nextTaskReport = nextRequest == null ? String.Empty : " - next is " + nextRequest.TaskName + " time remaining is " + nextRequest.RemainingTimeTilScheduleFmt;

            if (base.WinServiceParms.InDiagnosticsMode)
            {
              this.NotifyHostEvent("No task to process - remaining task count is " + tasksToProcessCount.ToString() + nextTaskReport +
                  " - next task set refresh at " + base.TaskRequests.NextLoadTime.ToString("yyyyMMdd HH:mm:ss"));
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

          if (base.TaskRequests.CheckDiscardConcurrent(taskRequest))
          {
            this.NotifyHostEvent("Task '" + taskRequest.TaskName + "' will be discarded due to already running task of same type.");
            continue;
          }

          ITaskProcessorFactory taskProcessorFactory = GetTaskProcessorFactory(taskRequest.ProcessorName + "_" + taskRequest.ProcessorVersion);
          if (taskProcessorFactory == null)
          {
            string discardMessage = "The task '" + taskRequest.TaskName + "' will be discarded because task processor factory could not be located.";
            ProcessNotifications(discardMessage, base.DefaultNotifyEventName); 
            this.NotifyHostEvent(discardMessage);
            base.Logger.Log(LogSeverity.SEVR, discardMessage, 6035); 
            continue;
          }

          if (taskRequest.IsDryRun)
          {
            string parmReport = taskRequest.GetParmReport();
            ProcessNotifications(base.ServiceName + " Parms for " + taskRequest.TaskName + " @ " + taskRequest.ScheduledRunDateTime, parmReport, base.DefaultNotifyEventName);
            base.Logger.Log(LogSeverity.INFO, parmReport, 6123);
          }

          base.TaskRequests.AddRunningTask(taskRequest);

          Task.Run(async () =>
          {
            try
            {
              var taskProcessor = taskProcessorFactory.CreateTaskProcessor(taskRequest.ProcessorNameAndVersion);
              CreateRunHistory(taskRequest);
              var taskResult = await base.TaskDispatcher.DispatchTaskAsync(taskProcessor, taskRequest);
              base.TaskRequests.RemoveRunningTask(taskRequest);
              Task.Run(() =>
              {
                base.TaskRequests.RemoveAllTaskRequestsForTaskId(taskResult);
                UpdateRunHistory(taskResult);
                ProcessNotifications(taskResult);
              });
            }
            catch (Exception ex)
            {
              string errorMessage = "An exception occurred while attempting to create the TaskProcessor for '" + taskRequest.ProcessorNameAndVersion + "' from " +
                               "the TaskProcessorFactory of type '" + taskProcessorFactory.GetType().FullName + "'.";
              this.NotifyHostEvent(errorMessage);
              base.TaskRequests.RemoveRunningTask(taskRequest);
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

    private void CheckTaskListRefresh()
    {
      try
      {
        if (base.TaskRequests.NextLoadTime < DateTime.Now || base.RefreshTaskRequests || base.RefreshTaskList)
        {
          base.RefreshTaskList = false;
          if (base.WinServiceParms.SuppressTaskReload)
          {
            base.TaskRequests.NextLoadTime = DateTime.Now.AddSeconds((int)(_taskEngineParms.TaskLoadIntervalSeconds * 0.8));
          }
          else
          {
            base.TaskRequests = GetTaskRequests(_taskEngineParms.TasksToRun);
          }
        }
      }
      catch (Exception ex)
      {
        base.TaskRequests.NextLoadTime = DateTime.Now.AddSeconds((int)(_taskEngineParms.TaskLoadIntervalSeconds * 0.8));
        base.RefreshTaskRequests = false;
        HandleExceptions("An exception occurred in the CheckTaskListRefresh method.", ex, 6125, false, true, true, true);
      }
    }

    private void CreateRunHistory(TaskRequest taskRequest)
    {
      try
      {
        if (!taskRequest.TrackHistory)
          return; 

        var runHistory = new RunHistory(taskRequest);
        using (var repo = new TaskRepository(_tasksDbSpec))
        {
          if (taskRequest.RunUntilTask)
            runHistory.PeriodHistoryId = repo.InsertPeriodHistory(new PeriodHistory(taskRequest));
          taskRequest.RunId = repo.InsertRunHistory(runHistory);
        }
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred when trying to insert a RunHistory record.", ex, 6121, false, false, true, false);
      }
    }

    private void UpdateRunHistory(TaskResult taskResult)
    {
      try
      {
        if (!taskResult.OriginalTaskRequest.RunId.HasValue || !taskResult.OriginalTaskRequest.TrackHistory)
          return;

        using (var repo = new TaskRepository(_tasksDbSpec))
          repo.UpdateRunHistory(new RunHistory(taskResult));
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred when trying to update a RunHistory record.", ex, 6122, false, false, true, false);
      }
    }

    private ITaskProcessorFactory GetTaskProcessorFactory(string processorKey)
    {
      if (this.LoadedTaskProcessorFactories.ContainsKey(processorKey))
        return this.LoadedTaskProcessorFactories[processorKey];

      foreach (Lazy<ITaskProcessorFactory, ITaskProcessorMetadata> taskProcessorFactory in taskProcessorFactories)
      {
        if (taskProcessorFactory.Metadata.Processors.ToListContains(Constants.SpaceDelimiter, processorKey))
        {
          this.LoadedTaskProcessorFactories.Add(processorKey, taskProcessorFactory.Value);
          return taskProcessorFactory.Value;
        }
      }

      return null;
    }

    private async void ExecuteImmediate(TaskRequest taskRequest)
    {
      try
      {
        if (base.CancellationTokenSource != null)
        {
          if (base.CancellationTokenSource.IsCancellationRequested)
          {
            string cancelMessage = base.ServiceName + " - Task processing (thread) has been canceled.";
            base.Logger.Log(cancelMessage, 6064);
            ProcessNotifications(cancelMessage, String.Empty, base.DefaultNotifyEventName);
            return;
          }
        }

        if (base.WinServiceState == WinServiceState.Paused || this.IsSuspended)
          return;
                                    
        base.IsSuspendedReported = false;

        if (g.DebugOrVerbose)
            this.NotifyHostEvent(new IpdxMessage("UI", "DispatchTask being called for task " + taskRequest.TaskName));

        ITaskProcessorFactory taskProcessorFactory = GetTaskProcessorFactory(taskRequest.TaskName + "_1.0.0.0");

        if (taskProcessorFactory == null)
        {
          string discardMessage = "The task '" + taskRequest.TaskName + "' will be discarded in ExecuteImmediate method because task processor factory could not be located.";
          ProcessNotifications(discardMessage, base.DefaultNotifyEventName);
          this.NotifyHostEvent(discardMessage);
          base.Logger.Log(LogSeverity.SEVR, discardMessage, 6035);
          return;
        }

        Task.Run(async () =>
        {
          var taskProcessor = taskProcessorFactory.CreateTaskProcessor(taskRequest.ProcessorName + "_" + taskRequest.ProcessorVersion);
          var taskResult = await base.TaskDispatcher.DispatchTaskAsync(taskProcessor, taskRequest);
          base.TaskRequests.RemoveRunningTask(taskRequest);
          Task.Run(() => {
            UpdateRunHistory(taskResult); 
            ProcessNotifications(taskResult); 
          });
        });
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred in the ProcessTask method.", ex, 6037, false, true, true, true);
      }
    }

        
    private void ReportSuspended()
    {
      if (!this.IsSuspendedReported)
      {
        this.NotifyHostEvent(new IpdxMessage("UI", g.crlf +
                    "**************************************** ALL TASKS ARE SUSPENDED ****************************************" + g.crlf +
                    "**************************************** ALL TASKS ARE SUSPENDED ****************************************" + g.crlf +
                    "**************************************** ALL TASKS ARE SUSPENDED ****************************************" + g.crlf));
        this.IsSuspendedReported = true;
      }
    }

        
    public TaskRequestSet GetTaskRequests(List<string> tasksToRun)
    {
      try
      {
        var taskRequests = new TaskRequestSet(base.WinServiceParms.MaxWaitIntervalMilliseconds);

        base.RefreshTaskRequests = false;
        _taskRequiringTaskRequestUpdate = String.Empty;

        this.NotifyHostEvent("Loading list of Task Requests to process");

        base.Logger.Log("Building list of tasks to process - source is " + _taskEngineParms.TaskScheduleMode.ToString() + ".", 6065); 

        switch (_taskEngineParms.TaskScheduleMode)
        {
          case TaskScheduleMode.FromDatabase:
            var scheduledTasksFromDb = LoadTasksFromDatabase(tasksToRun); 
            scheduledTasksFromDb.LoadTasksToRun(_taskEngineParms.TaskLoadIntervalSeconds);
            taskRequests.AddTaskRequests(scheduledTasksFromDb.GetTaskRequests());
            break;

          case TaskScheduleMode.FromConfig:
            var scheduledTasksFromAppConfig = LoadTasksFromAppConfig(tasksToRun);
            scheduledTasksFromAppConfig.LoadTasksToRun(_taskEngineParms.TaskLoadIntervalSeconds);
            taskRequests.AddTaskRequests(scheduledTasksFromAppConfig.GetTaskRequests());
            break;
        }

        taskRequests.NextLoadTime = DateTime.Now.AddSeconds((int)(_taskEngineParms.TaskLoadIntervalSeconds * 0.8));

        string taskReport = taskRequests.TaskReport;

        base.Logger.Log(taskReport, 6061);
        this.NotifyHostEvent(taskReport);

        if (base.NotifyOnGetTasks)
        {
          ProcessNotifications(base.WinServiceParms.WindowsServiceName + " Task Schedule Reloaded from " + _taskEngineParms.TaskScheduleMode.ToString().Replace("From", String.Empty),
                               "The task schedule has been reloaded." + g.crlf2 + taskReport, base.DefaultNotifyEventName);
        }
        
        return taskRequests;
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred while attempting to build the TaskRequestSet.", ex, 6060, false, true, true, true);
        return null;
      }
    }

    private ScheduledTaskSet LoadTasksFromDatabase(List<string> tasksToRun)
    {
      ScheduledTaskSet scheduledTaskSet = new ScheduledTaskSet();

      using (var repo = new TaskRepository(_tasksDbSpec))
      {
        var tasks = repo.GetTasksForScheduling(tasksToRun);

        foreach (var task in tasks)
        {
          if (task.IsActive && tasksToRun.Contains(task.TaskName))
            scheduledTaskSet.Add(task);
        }
      }

      return scheduledTaskSet;
    }

    private ScheduledTaskSet LoadTasksFromAppConfig(List<string> tasksToRun)
    {
      ScheduledTaskSet scheduledTaskSet = new ScheduledTaskSet();
      TaskConfigurations tc = g.AppConfig.GetTaskConfigurations();

      string taskProfile = base.WinServiceParms.RunWebService ? _taskEngineParms.TaskProfile : _taskEngineParms.WSHTaskProfile; 
      var taskConfigSet = tc[taskProfile];

      if (taskConfigSet == null)
        throw new Exception("TaskConfigSet for task profile '" + taskProfile + "' is null.  Task configurations load from AppConfig failed.");

      DateTime baseDateTime = DateTime.Now;
      foreach (var taskConfig in taskConfigSet)
      {
        ScheduledTask busTask = new ScheduledTask(taskConfig.Value, baseDateTime);
        if (busTask.TaskSchedule != null)
          scheduledTaskSet.Add(busTask);
      }

      return scheduledTaskSet;
    }

    private void PerformControlTasks()
    {

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
        case "GetRunningTasksReportRequest":
          taskResult.Data = base.TaskRequests.TaskReport;
          return taskResult.Success();

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

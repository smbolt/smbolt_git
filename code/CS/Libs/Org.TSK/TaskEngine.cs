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
using TPL = System.Threading.Tasks;
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
using Org.GS.AppDomainManagement;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS.Notifications;
using Org.GS;

namespace Org.TSK
{
  public class TaskEngine : WinServiceEngineBase
  {
    private TaskEngineParms _taskEngineParms;

    // AppDomain-Related Objects
    private AppDomainSupervisor _appDomainSupervisor;
    private AppDomainEventManager _appDomainEventManager;

    // MEF-Related Objects
    [ImportMany(typeof(ITaskProcessorFactory))]
    private IEnumerable<Lazy<ITaskProcessorFactory, ITaskProcessorMetadata>> taskProcessorFactories;
    private CompositionContainer CompositionContainer;
    private Dictionary<string, ITaskProcessorFactory> LoadedTaskProcessorFactories;
    //public List<string> RunnableTaskNames { get { return Get_RunnableTaskNames(); } }

    private string _catalogStem = String.Empty;
    private string _catalogEnvironment = String.Empty;
    private string _catalogNode = String.Empty;
    private TaskRequest _lastRunTaskRequest = null;
    private object _taskManagementObject = new object();
    private List<int> _tasksToRemoveFromQueue = new List<int>();
    private List<int> _tasksToRemoveFromAssignment = new List<int>();

    private string _taskRequiringTaskRequestUpdate = String.Empty;
    private ConfigDbSpec _tasksDbSpec;
    
    public TaskEngine(WinServiceParms winServiceParms, TaskEngineParms taskEngineParms)
      : base(winServiceParms, "TaskEngine")
    {
      _taskEngineParms = taskEngineParms;
      base.OverrideEnvironment = _taskEngineParms.OverrideEnvironment;

      base.EntityId = base.WinServiceParms.EntityId > 0 ? base.WinServiceParms.EntityId : 304;
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
          _taskEngineParms.TaskScheduleMode = g.ToEnum<TaskScheduleMode>(g.CI("TaskScheduleMode"), TaskScheduleMode.Database);
          _taskEngineParms.TaskLoadIntervalSeconds = g.CI("TaskLoadIntervalSeconds").ToInt32OrDefault(1200);
          _taskEngineParms.TasksDbSpecPrefix = g.CI("TasksDbSpecPrefix").OrDefault("Tasks");
          _taskEngineParms.TaskProfile = g.CI("TaskProfile").OrDefault("Normal");
          _taskEngineParms.WSHTaskProfile = g.CI("WSHTaskProfile").OrDefault("FastTest");
          _taskEngineParms.MEFModulesPath = g.CI("MEFModulesPath");
          _taskEngineParms.LimitMEFImports = g.CI("LimitMEFImports").ToBoolean();
          _taskEngineParms.MEFLimitListName = g.CI("MEFLimitListName");
          _taskEngineParms.TaskLoadIntervalSeconds = g.GetCI("TaskLoadIntervalSeconds").ToInt32OrDefault(1200);
          _taskEngineParms.TaskAssignmentSource = g.ToEnum<TaskAssignmentSource>(g.CI("TaskAssignmentSource"), TaskAssignmentSource.Database);

          base.WinServiceParms.WindowsServiceName = base.ServiceName = g.CI("WindowsServiceName").OrDefault("WindowsServiceNameNotSet");
          base.WinServiceParms.EntityId = base.EntityId = g.CI("EntityID").ToInt32OrDefault(0);
        }

        if (_taskEngineParms.TaskScheduleMode == TaskScheduleMode.Database)
          _tasksDbSpec = g.GetDbSpec(_taskEngineParms.TasksDbSpecPrefix);

        if (_taskEngineParms.TaskScheduleMode == TaskScheduleMode.Database && !_tasksDbSpec.IsReadyToConnect())
          throw new Exception("The TasksConfigDbSpec is not ready to connect to the database - connection string prefix is '" + _taskEngineParms.TasksDbSpecPrefix + ".");        
        
        if (_taskEngineParms.MEFModulesPath == "$MEFCATALOG$" || _taskEngineParms.MEFModulesPath.IsBlank())
          _taskEngineParms.MEFModulesPath = g.MEFCatalog;

        if (!Directory.Exists(_taskEngineParms.MEFModulesPath))
          throw new Exception("The value of the configuration item 'MEFModulesPath' is not an existing directory '" + _taskEngineParms.MEFModulesPath + "'.");

        base.Logger.Log("Modules will be loaded from path '" + _taskEngineParms.MEFModulesPath + "'.", 6062);
        this.NotifyHostEvent("Modules will be loaded from path '" + _taskEngineParms.MEFModulesPath + "'.");

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' POINT 4.  In " + base.EngineName + ".Initialize.");
        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "Starting to load MEF Catalog.");

        _appDomainSupervisor = new AppDomainSupervisor();
        _appDomainEventManager = new AppDomainEventManager();
        _appDomainEventManager.NotifyMessage += Module_NotifyMessage;
        _appDomainEventManager.ProgressUpdate += Module_ProgressUpdate;

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
        string notificationMessage = base.ServiceName + " (" + base.EngineName + ") on " + base.DomainAndComputer + " is starting";
        string notificationSubject = notificationMessage; 
        ProcessNotifications(notificationSubject, notificationMessage, base.DefaultNotifyEventName);
        StartupLogging.WriteStartupLog("Just entered " + base.EngineName + ".Start");

        if (base.WinServiceState == WinServiceState.Paused || base.WinServiceState == WinServiceState.Running)
          return true;

        base.Logger.Log(base.EngineName + " start method is beginning.", 6028);
          
        base.TaskDispatcher = new TaskDispatcher(this.LoadedTaskProcessorFactories);
        base.TaskDispatcher.ContinueTask = true;
        base.TaskDispatcher.NotifyMessage += base.NotifyMessageHandler;

        _taskEngineParms.TasksToRun = GetTasksToRun(); 
        base.Logger.Log(_taskEngineParms.TasksToRunReport, 6116); 

        StartupLogging.WriteStartupLog(base.ServiceName + " is configured to run the following tasks:" + _taskEngineParms.TasksToRunReport);

        GetCatalogPath();

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

        base.ProcessTaskHasBeenStopped = false;
        base.ProcessTasks_CancellationTokenSource = new CancellationTokenSource();
        base.ProcessTasks_ResetEvent = new ManualResetEvent(false);
        base.ProcessTasksThread = new TPL.Task(() => this.ProcessTasks(), base.ProcessTasks_CancellationTokenSource.Token, TPL.TaskCreationOptions.LongRunning);
        base.ProcessTasksThread.Start();

        if (base.EngineMonitorParms.EngineMonitorActive)
        {
          base.MonitorMainLoop_CancellationTokenSource = new CancellationTokenSource();
          base.MonitorMainLoop_ResetEvent = new ManualResetEvent(false);
          base.MonitorMainLoopThread = new TPL.Task(() => this.MonitorMainLoop(), base.MonitorMainLoop_CancellationTokenSource.Token, TPL.TaskCreationOptions.LongRunning);
          base.MonitorMainLoopThread.Start();
          base.EngineMonitorParms.EngineMonitorDependencyCheckCount = 0;
          StartupLogging.WriteStartupLog("EngineMonitorActive set to 'True' - monitoring thread has been started.");
        }

        StartupLogging.WriteStartupLog("ProcessTasks thread is started - - in " + base.EngineName + ".Start.");

        StartupLogging.WriteStartupLog("WinService startup is complete.");
        base.Logger.Log(base.EngineName + " Start method is finished", 6029);

        NotifyHostEvent(new IpdxMessage("UI", IpdxMessageType.Notification, "STARTED"));

        StartupLogging.WriteStartupLog(base.EngineName + " has been started" + " (send to notifications)");

        string startupReport = "The service is started and configured to run the following tasks:" + g.crlf + _taskEngineParms.TasksToRunReport;

        string startSuccessMessage = base.EngineName + " has been successfully started" + g.crlf2 + "Host Name: " + base.DomainAndComputer + g.crlf +
                                     "Windows Service: " + base.ServiceName + g.crlf2 + startupReport;
        string startSuccessSubject = base.ServiceName + " (" + base.EngineName + ") on " + base.DomainAndComputer + " started successfully";
        base.ProcessNotifications(startSuccessSubject, startSuccessMessage, base.DefaultNotifyEventName);

        StartupLogging.StartupComplete = true;
        return true;
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred attempting to start the service.", ex, 6030, true, true, true, true);
        return false;
      }
    }

    private List<string> GetTasksToRun()
    {
      try
      {
        if (_taskEngineParms.TaskAssignmentSource == TaskAssignmentSource.AppConfig)
          return g.AppConfig.GetList("TasksToRun");

        var tasksToRun = new List<string>();
        
        using (var taskRepo = new TaskRepository(_tasksDbSpec))
        {
          var taskList = taskRepo.GetTaskAssignmentsForTaskName(base.ServiceName);
          foreach(var task in taskList)
          {
            tasksToRun.Add(task.TaskName.ToString());
          }
        }

        return tasksToRun;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the list of tasks to run.  The TaskAssignmentSource is '" +
                            _taskEngineParms.TaskAssignmentSource.ToString() + "'.", ex); 
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
        base.ProcessTasks_CancellationTokenSource.Cancel();
        base.ProcessTasks_ResetEvent.Set();
        base.ProcessTaskHasBeenStopped = true;

        if (base.MonitorMainLoopThread != null && base.MonitorMainLoop_CancellationTokenSource != null)
        {
          base.MonitorMainLoop_CancellationTokenSource.Cancel();
          if (base.MonitorMainLoop_ResetEvent != null)
            base.MonitorMainLoop_ResetEvent.Set();
        }

        if (base.WinServiceParms.RunWebService)
          base.StopWebService();

        base.WinServiceState = WinServiceState.Stopped;

        NotifyHostEvent(new IpdxMessage("UI", IpdxMessageType.Notification, "STOPPED"));

        base.Logger.Log(base.EngineName + " is stopping.", 6113);

        string stopNotificationMessage = base.ServiceName + " (" + base.EngineName + ") on " + base.DomainAndComputer + " is stopping (Code 6113)";
        base.ProcessNotifications(stopNotificationMessage, stopNotificationMessage, base.DefaultNotifyEventName);
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred attempting to stop the service.", ex, 6031, false, true, true, false);
      }
    }

    public override void Pause(bool pauseWebService = true)
    {
      try
      {
        base.WinServiceState = WinServiceState.Paused;
        base.TaskDispatcher.ContinueTask = false;

        if (base.WinServiceParms.RunWebService && pauseWebService)
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

    public override void Resume(bool resumeWebService = true)
    {
      try
      {
        base.WinServiceState = WinServiceState.Running;
        base.TaskDispatcher.ContinueTask = true;

        if (base.WinServiceParms.RunWebService && resumeWebService)
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
          if (base.ProcessTasks_CancellationTokenSource.IsCancellationRequested)
          {
            base.ProcessTaskHasBeenStopped = true;
            string cancelMessage = base.ServiceName + " (Engine:" + base.EngineName + ") on " + base.DomainAndComputer + " main processing thread has been canceled (Code 6034)";
            base.Logger.Log(cancelMessage, 6034);
            ProcessNotifications(cancelMessage, cancelMessage, base.DefaultNotifyEventName);
            return;
          }

          if (_tasksToRemoveFromQueue.Count > 0)
          {
            var taskIdsRemoved = new List<int>();
            foreach (int taskId in _tasksToRemoveFromQueue)
            {
              base.TaskRequests.RemoveAllTaskRequestsForTaskId(taskId);
              taskIdsRemoved.Add(taskId);
            }

            if (taskIdsRemoved.Count > 0)
            {
              if (Monitor.TryEnter(_taskManagementObject, 2000))
              {
                foreach (var taskId in taskIdsRemoved)
                {
                  _tasksToRemoveFromQueue.Remove(taskId);
                  this.NotifyHostEvent("Task Requests for TaskID '" + taskId.ToString() + "' have been removed from the queue.");
                }
              }
            }
          }

          if (_tasksToRemoveFromAssignment.Count > 0)
          {
            var taskIdsRemoved = new List<int>();
            foreach (int taskId in _tasksToRemoveFromAssignment)
            {
              RemoveTaskFromServiceAssignment(taskId);
              taskIdsRemoved.Add(taskId);
            }

            if (taskIdsRemoved.Count > 0)
            {
              if (Monitor.TryEnter(_taskManagementObject, 2000))
              {
                foreach (var taskId in taskIdsRemoved)
                {
                  _tasksToRemoveFromAssignment.Remove(taskId);
                  this.NotifyHostEvent("Task Requests for TaskID '" + taskId.ToString() + "' have been removed from assignment.");
                }
              }
            }
          }


          while (base.WinServiceState == WinServiceState.Paused)
          {
            base.ProcessTasks_ResetEvent.WaitOne(base.WinServiceParms.SleepInterval);
            continue;
          }
                    
          base.ProcessTasks_ResetEvent.WaitOne(base.TaskRequests.WaitInterval);

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

          // If no task request was pulled from the queue, we do some housekeeping and loop back to the top.
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
                  
          // If a task request has been pulled from the queue, we continue to process it.  It will eventually be dispatched
          // to a task processor for processing below.

          this.TasksProcessed++;

          GetTaskParameters(taskRequest);

          // Check if status has been updated since task request was built
          if (!taskRequest.IsActive)
          {
            this.NotifyHostEvent("Task '" + taskRequest.TaskName + "' will be discarded because the scheduled task's IsActive property has been set to false since the task request was built.");
            continue;
          }

          if (!base.RunningAsService)
            _lastRunTaskRequest = taskRequest;

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

          if (base.ScheduleOnceNow)
            taskRequest.ParmSet.ProcessOverrides(this.OverrideParms);

          if (this.IsDryRun)
          {
            taskRequest.IsDryRun = true;
            taskRequest.ParmSet.SetParmValue("IsDryRun", true);
          }

          var processorType = g.ToEnum<ProcessorType>(taskRequest.ProcessorTypeId, ProcessorType.NotSet); 
          
          if (processorType == ProcessorType.StandardCatalog)
          {
            var taskProcessorAD = GetTaskProcessorFromAppDomain(taskRequest);

            if (taskProcessorAD == null)
            {
              string discardMessage = "The task '" + taskRequest.TaskName + "' will be discarded because task processor factory could not be located.";
              ProcessNotifications(discardMessage, base.DefaultNotifyEventName);
              this.NotifyHostEvent(discardMessage);
              base.Logger.Log(LogSeverity.SEVR, discardMessage, 6035);
              continue;
            }

            taskProcessorAD.NotifyMessage += _appDomainEventManager.PlugIn_NotifyMessage;
            taskProcessorAD.ProgressUpdate += _appDomainEventManager.PlugIn_ProgressUpdate;

            TPL.Task.Run(async () =>
            {
              ProcessTaskAD(taskProcessorAD, taskRequest);
            });

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

          TPL.Task.Run(async () =>
          {
            try
            {
              var taskProcessor = taskProcessorFactory.CreateTaskProcessor(taskRequest.ProcessorNameAndVersion);
              CreateRunHistory(taskRequest);
              var taskResult = await base.TaskDispatcher.DispatchTaskAsync(taskProcessor, taskRequest);
              base.TaskRequests.RemoveRunningTask(taskRequest);
              TPL.Task.Run(() =>
              {
                base.TaskRequests.RemoveAllTaskRequestsForTaskId(taskResult);
                UpdateRunHistory(taskResult);
                ProcessNotifications(taskResult);
                this.NotifyHostEvent("Task '" + taskResult.TaskName + "' completed with status '" + taskResult.TaskResultStatus.ToString() + "'.");
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
        base.ProcessTaskHasBeenStopped = true;
        HandleExceptions("An exception occurred in the ProcessTask method.", ex, 6036, false, true, true, true);
      }
    }

    private void GetTaskParameters(TaskRequest taskRequest)
    {
      if (_taskEngineParms.TaskScheduleMode == TaskScheduleMode.AppConfig)
        return;

      try
      {
        using (var repo = new TaskRepository(_tasksDbSpec))
        {
          taskRequest.IsActive = repo.GetTaskActiveStatus(taskRequest.ScheduledTaskId);
          if (taskRequest.IsActive)
            taskRequest.ParmSet = repo.GetTaskParms(taskRequest.ScheduledTaskId, true);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to refresh the active state and parameters for the task request '" +
                            taskRequest.Report + "'.", ex);
      }
    }

    private void MonitorMainLoop()
    {
      try
      {
        var parms = base.EngineMonitorParms;
        parms.EnsureMinimumValues();

        while (true)
        {
          // wait for specified interval...
          base.MonitorMainLoop_ResetEvent.WaitOne(TimeSpan.FromSeconds(parms.EngineMonitorIntervalSeconds));

          // reset notification count to zero if it has been "x" hours since last notifications were
          // sent due to a main thread failure
          if (parms.LastMainLoopRestart.HasValue)
          {
            if (parms.LastMainLoopRestart.Value.AddHours(parms.EngineMonitorResetNotifyLimitHours) < DateTime.Now)
            {
              parms.LastMainLoopRestart = null;
              parms.EngineMonitorNotifyCount = 0;
              base.Logger.Log("Log monitoring notification count set to zero after " + parms.EngineMonitorResetNotifyLimitHours.ToString() +
                              " hours since restart of main processing loop.");
            }
          }

          // If the main processing loop has been stopped - continue...
          // This thread should be stopped soon also.
          if (base.ProcessTaskHasBeenStopped)
            continue;

          if (base.ProcessTasksThread == null)
          {
            base.Logger.Log("The ProcessTasksThread (TPL object) is null. The MonitorMainLoop is throwing this exception."); 
            throw new Exception("The ProcessTasksThread (TPL object) is null. The MonitorMainLoop is throwing this exception.");
          }

          // all statuses except Faulted are acceptable to continue monitoring
          if (base.ProcessTasksThread.Status != TPL.TaskStatus.Faulted)
          {
            if (parms.LogNormalMonitoringActivity)
              base.Logger.Log("Monitoring of main processing thread - normal conditions found. Main TPL task status is '" + base.ProcessTasksThread.Status.ToString() + "'.");
            continue;
          }

          // if not configured to attempt restart
          if (!parms.EngineMonitorAttemptRestart)
          {
            base.Logger.Log("TaskEngine ProcessTasksThread is faulted - not configured to attempt restart.");
            // notify and exit monitoring thread
            ProcessNotifications("TaskEngine ProcessTasksThread is faulted - not configured to attempt restart.", base.DefaultNotifyEventName);
            return;
          }

          base.Logger.Log("The TaskEngine ProcessTaskThread is faulted - restart logic is beginning.");
          ProcessNotifications("The TaskEngine ProcessTaskThread is faulted - restart logic is beginning.", base.DefaultNotifyEventName);

          // if configured to check dependencies...
          // loop until dependencies are validated to be available or until configured limits are reached

          if (parms.EngineMonitorRunDependenciesChecks)
            RunDependenciesCheck(parms);

          base.ProcessTaskHasBeenStopped = false;
          base.ProcessTasks_CancellationTokenSource = new CancellationTokenSource();
          base.ProcessTasks_ResetEvent = new ManualResetEvent(false);
          base.ProcessTasksThread = new TPL.Task(() => this.ProcessTasks(), base.ProcessTasks_CancellationTokenSource.Token, TPL.TaskCreationOptions.LongRunning);
          base.ProcessTasksThread.Start();

          base.Logger.Log("The TaskEngine ProcessTaskThread has been restarted by the MonitorMainLoop method.");
          ProcessNotifications("The TaskEngine ProcessTaskThread has been restarted by the MonitorMainLoop method.", base.DefaultNotifyEventName);

          parms.LastMainLoopRestart = DateTime.Now;
        }
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred in the MonitorMainLoop method.", ex, 6036, false, true, true, true);
      }
    }

    private bool RunDependenciesCheck(EngineMonitorParms parms)
    {
      try
      {
        parms.EngineMonitorDependencyCheckCount = 0;
        int retrySeconds = parms.EngineMonitorDependencyRetryIntervalSeconds;

        while (true)
        {
          if (DependenciesCheckPassed())
          {
            base.Logger.Log("Main processing loop dependencies check passed - restart of faulted main processing loop will be attempted.");
            return true;
          }

          // see if we've reached the limit of number of retries 
          if (!parms.ContinueToRetryDependenciesCheck())
            return false;

          // if we're notifying on retry attempts and we haven't passed the notify limit
          if (parms.PerformTryRestartNotification())
          {
            string message = "TaskEngine MainLoopMonitoring is attempting to restart the main processing loop - attempt number " +
                                  parms.EngineMonitorDependencyCheckCount.ToString() + ".";
            base.Logger.Log(message);
            ProcessNotifications(message, DefaultNotifyEventName);
          }

          //wait the configured time between retries
          base.MonitorMainLoop_ResetEvent.WaitOne(TimeSpan.FromSeconds(retrySeconds));
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to run the dependencies check for restarting the main task processing loop after the loop " +
                            "was detected to be faulted by the main loop monitoring process.", ex); 
      }
    }

    private bool DependenciesCheckPassed()
    {
      try
      {
        // add dependencies here - currently connecting to the TaskScheduling DB is the only dependency

        if (_taskEngineParms.TaskScheduleMode == TaskScheduleMode.Database || _taskEngineParms.TaskAssignmentSource == TaskAssignmentSource.Database)
        {
          using (var repo = new TaskRepository(_tasksDbSpec))
          {
            return repo.ConnectionCheck();
          }
        }
        else
        {
          return true;
        }
      }
      catch
      {
        return false;
      }
    }

    private ITaskProcessor GetTaskProcessorFromAppDomain(TaskRequest taskRequest)
    {
      try
      {
        var appDomainSetUp = new AppDomainSetup();
        string catalogFullPath = _catalogStem + @"\" + _catalogEnvironment + @"\" + _catalogNode;        
        appDomainSetUp.ApplicationBase = catalogFullPath + @"\" + taskRequest.CatalogName + @"\" + taskRequest.CatalogEntry + @"\" + taskRequest.ProcessorVersion;
        var objDesc = new AppDomainObjectDescriptor(taskRequest.AssemblyName, taskRequest.ProcessorNameAndVersion, taskRequest.AssemblyName, taskRequest.ObjectTypeName, appDomainSetUp);
        ITaskProcessor taskProcessor = (ITaskProcessor)_appDomainSupervisor.GetObject(objDesc);
        return taskProcessor; 
      }
      catch
      {
        throw new Exception("An exception occurred while attempting to retrieve the object named '" + taskRequest.ProcessorNameAndVersion + "' from a child AppDomain.");
      }
    }


    private async void ProcessTaskAD(ITaskProcessor taskProcessor, TaskRequest taskRequest)
    {
      try
      {
        taskProcessor.TaskRequest = taskRequest;
        var taskResult = taskProcessor.ProcessTask();

        switch (taskResult.TaskResultStatus)
        {
          case TaskResultStatus.Success:
            NotifyHostEvent("Task Processor '" + taskProcessor.Name + " completed successfully."); 
            break;

          case TaskResultStatus.Warning:
            NotifyHostEvent("Task Processor '" + taskProcessor.Name + " completed with status 'Warning." + g.crlf +
                            "Warning Message is '" + taskResult.Message); 
            break;

          default:
            NotifyHostEvent("Task Processor '" + taskProcessor.Name + " ended with status '" + taskResult.TaskResultStatus.ToString() + "'." + g.crlf +
                            "Message is '" + taskResult.Message); 
            break;

        }

        taskProcessor.NotifyMessage -= _appDomainEventManager.PlugIn_NotifyMessage;
        taskProcessor.ProgressUpdate -= _appDomainEventManager.PlugIn_ProgressUpdate;

        // figure out what to do with task result
        // run history, notifications, etc.

        //await Task.Run(() =>
        //{
        //  txtMain.Invoke((Action)((() =>
        //  {
        //    txtMain.Text += taskResult.Message;
        //  })));

        //});
      }
      catch (Exception ex)
      {
        // handle error result
        // run history, notifications, etc.

        //txtMain.Invoke((Action)((() =>
        //{
        //  this.Cursor = Cursors.Default;
        //  MessageBox.Show("An exception occurred while attempting to execute the task [provide task info...]." + g.crlf2 + ex.ToReport(),
        //                  "AppDomain Manager - Error Running Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //})));
      }
    }

    public override void ReExecuteLastRunTaskRequest()
    {
      try
      {
        if (_lastRunTaskRequest == null)
          return;

        _lastRunTaskRequest.ScheduledRunDateTime = DateTime.Now;

        base.TaskRequests.AddTaskRequest(_lastRunTaskRequest); 
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to re-execute the last run task request.", ex); 
      }
    }

    private void CheckTaskListRefresh()
    {
      try
      {
        if (this.ScheduleOnceNow)
          return; 

        if (base.TaskRequests.NextLoadTime < DateTime.Now || base.RefreshTaskRequests || base.RefreshTaskList)
        {
          base.RefreshTaskList = false;
          base.RefreshTaskRequests = false;

          _taskEngineParms.TasksToRun = GetTasksToRun();

          if (base.NotifyOnTaskRefresh)
          {
            string tasksToRunReport = _taskEngineParms.TasksToRunReport;
            this.NotifyHostEvent(tasksToRunReport);
          }

          if (base.WinServiceParms.SuppressTaskReload)
          {
            base.TaskRequests.NextLoadTime = DateTime.Now.AddSeconds((int)(_taskEngineParms.TaskLoadIntervalSeconds * 0.8));
          }
          else
          {
            base.TaskRequests = GetTaskRequests(_taskEngineParms.TasksToRun);
          }

          base.NotifyOnTaskRefresh = false;
        }
      }
      catch (Exception ex)
      {
        base.TaskRequests.NextLoadTime = DateTime.Now.AddSeconds((int)(_taskEngineParms.TaskLoadIntervalSeconds * 0.8));
        base.RefreshTaskRequests = false;
        base.RefreshTaskList = false;
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
        {
          repo.UpdateRunHistory(new RunHistory(taskResult));
        }
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

    private void GetCatalogPath()
    {
      try
      {
        switch (_taskEngineParms.TaskScheduleMode)
        {
          case TaskScheduleMode.Database:
            using (var repo = new TaskRepository(_tasksDbSpec))
            {
              _catalogStem = repo.GetParameterValue("$CATALOG_STEM$");
              _catalogEnvironment = repo.GetParameterValue("$ENV$");
              _catalogNode = repo.GetParameterValue("$CATALOG_NODE$"); 
            }
            break;

          case TaskScheduleMode.AppConfig:
            _catalogStem = g.GetVariableValue("$CATALOG_STEM$");
            _catalogEnvironment = g.GetVariableValue("$ENV$");
            _catalogNode = g.GetVariableValue("$CATALOG_NODE$");
            break;
        }

        string fullCatalogPath = _catalogStem + @"\" + _catalogEnvironment + @"\" + _catalogNode;

        if (!Directory.Exists(fullCatalogPath))
        {
          throw new Exception("The configured catalog path does not exist, configured value is '" + fullCatalogPath + "'."); 
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the catalog stem and catalog environment.", ex);
      }
    }

    private async void ExecuteImmediate(TaskRequest taskRequest)
    {
      try
      {
        if (base.ProcessTasks_CancellationTokenSource != null)
        {
          if (base.ProcessTasks_CancellationTokenSource.IsCancellationRequested)
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

        TPL.Task.Run(async () =>
        {
          var taskProcessor = taskProcessorFactory.CreateTaskProcessor(taskRequest.ProcessorName + "_" + taskRequest.ProcessorVersion);
          var taskResult = await base.TaskDispatcher.DispatchTaskAsync(taskProcessor, taskRequest);
          base.TaskRequests.RemoveRunningTask(taskRequest);
          TPL.Task.Run(() => {
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
          case TaskScheduleMode.Database:
            bool scheduleOnceNow = false;
            if (this.TaskToRun.IsNotBlank())
            {
              tasksToRun = new List<string>() { this.TaskToRun };
              scheduleOnceNow = true;
            }
            var scheduledTasksFromDb = LoadTasksFromDatabase(tasksToRun, scheduleOnceNow); 
            scheduledTasksFromDb.LoadTasksToRun(_taskEngineParms.TaskLoadIntervalSeconds, scheduleOnceNow, false);
            taskRequests.AddTaskRequests(scheduledTasksFromDb.GetTaskRequests());
            break;

          case TaskScheduleMode.AppConfig:
            var scheduledTasksFromAppConfig = LoadTasksFromAppConfig(tasksToRun);
            scheduledTasksFromAppConfig.LoadTasksToRun(_taskEngineParms.TaskLoadIntervalSeconds, false, false);
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

    private void RemoveTaskFromServiceAssignment(int taskId)
    {
      try
      {
        using (var repo = new TaskRepository(_tasksDbSpec))
        {
          repo.RemoveTaskFromServiceAssignment(base.ServiceName, taskId);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to remove TaskID " + taskId.ToString() + " from assignment to this service.", ex);
      }
    }

    private ScheduledTaskSet LoadTasksFromDatabase(List<string> tasksToRun, bool scheduleOnceNow)
    {
      ScheduledTaskSet scheduledTaskSet = new ScheduledTaskSet();

      using (var repo = new TaskRepository(_tasksDbSpec))
      {
        var tasks = repo.GetTasksForScheduling(tasksToRun, scheduleOnceNow, true);

        foreach (var task in tasks)
        {
          if (scheduleOnceNow)
            scheduledTaskSet.Add(task);
          else
          {
            if (task.IsActive && tasksToRun.Contains(task.TaskName))
              scheduledTaskSet.Add(task);
          }
        }
      }

      return scheduledTaskSet;
    }

    private ScheduledTaskSet LoadTasksFromAppConfig(List<string> tasksToRun)
    {
      try
      {
        ScheduledTaskSet scheduledTaskSet = new ScheduledTaskSet();
        var tc = g.AppConfig.GetTaskConfigurations();

        string taskProfileName = base.WinServiceParms.IsRunningAsWindowsService ? _taskEngineParms.TaskProfile : _taskEngineParms.WSHTaskProfile;
        var taskConfigSet = tc[taskProfileName];

        if (taskConfigSet == null)
          throw new Exception("TaskConfigSet for task profile '" + taskProfileName + "' is null.  Task configurations load from AppConfig failed.");

        DateTime baseDateTime = DateTime.Now;

        foreach (var taskConfig in taskConfigSet)
        {
          var task = new ScheduledTask(taskConfig.Value, baseDateTime);
          if (task.TaskSchedule != null)
            scheduledTaskSet.Add(task);
        }

        return scheduledTaskSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the ScheduledTaskSet from the AppConfig file.", ex); 
      }
    }

    private void Module_ProgressUpdate(ProgressMessage progressMessage)
    {
      var message = new IpdxMessage();
      message.MessageType = IpdxMessageType.Text;
      message.Recipient = "UI";
      message.Text = progressMessage.ActivityName + " : " + progressMessage.MessageText + " - " + progressMessage.ActvityProgress;
      NotifyHostEvent(message);
    }

    private void Module_NotifyMessage(NotifyMessage notifyMessage)
    {
      var message = new IpdxMessage();
      message.MessageType = IpdxMessageType.Text;
      message.Recipient = "UI";
      message.Text = notifyMessage.Subject + " : " + notifyMessage.Message;
      NotifyHostEvent(message); 
    }


    private void PerformControlTasks()
    {
    }

    //private List<string> Get_RunnableTaskNames()
    //{
    //  try
    //  {
    //    var runnableTaskNames = new List<string>();

    //    if (this.CompositionContainer == null)
    //      return runnableTaskNames;

    //    return this.CompositionContainer.GetRunnableTaskNames(taskProcessorFactories); 
    //  }
    //  catch(Exception ex)
    //  {
    //    throw new Exception("An exception occurred while attempting to build a list of runnable task names from the MEF catalog.", ex); 
    //  }
    //}

    private void WireUpSuperMethod()
    {
      base.WireUpSuperMethod(SuperMethod); 
    }

    private TaskResult SuperMethod(WsCommand command)
    {
      TaskResult taskResult = new TaskResult(command.WsCommandName.ToString()); 

      switch (command.WsCommandName)
      {
        case WsCommandName.GetRunningTaskReport:
          var getRunningTasksReportResponse = new GetRunningTasksReportResponse();
          getRunningTasksReportResponse.RunningTasksReport = base.TaskRequests.TaskReport;
          taskResult.Object = getRunningTasksReportResponse;
          return taskResult.Success();

        case WsCommandName.StartWinService:
          this.Start();
          return taskResult.Success();

        case WsCommandName.StopWinService:
          this.Stop();
          return taskResult.Success();

        case WsCommandName.PauseWinService:
          this.Pause(false);
          return taskResult.Success();

        case WsCommandName.ResumeWinService:
          this.Resume(false);
          return taskResult.Success();

        case WsCommandName.RefreshTaskList:
          base.NotifyOnTaskRefresh = true;
          this.NotifyHostEvent("Refreshing Task List");
          this.RefreshTaskList = true;
          return taskResult.Success();

        case WsCommandName.RefreshTaskRequests:
          this.NotifyOnTaskRefresh = true;
          this.RefreshTaskRequests = true;
          this.NotifyHostEvent("Refreshing Task Requests");
          return taskResult.Success();

        case WsCommandName.ShowServiceTaskReport:
          this.NotifyHostEvent("ShowServiceTaskReport transaction processed.");
          taskResult.Message = base.TaskRequests.TaskReport;
          return taskResult.Success();

        case WsCommandName.RemoveFromQueue:
          int? taskIdToRemoveFromQueue = command?.Parms?["ScheduledTaskID"]?.DbToInt32();
          if (!taskIdToRemoveFromQueue.HasValue)
          {
            this.NotifyHostEvent("RemoveFromQueue transaction failed - required ScheduledTaskId parameter not found.");
            taskResult.Message = "Request to remove task from the queue failed - required ScheduledTaskId parameter not found.";
            return taskResult.Failed();
          }

          if (Monitor.TryEnter(_taskManagementObject, 2000))
          {
            if (!_tasksToRemoveFromQueue.Contains(taskIdToRemoveFromQueue.Value))
              _tasksToRemoveFromQueue.Add(taskIdToRemoveFromQueue.Value);
            Monitor.Exit(_taskManagementObject);
            this.NotifyHostEvent("RemoveFromQueue transaction processed.");
            taskResult.Message = "Request to remove task from the queue was processed successfully.";
            return taskResult.Success();
          }
          else
          {
            this.NotifyHostEvent("RemoveFromQueue transaction failed - could not obtain lock.");
            taskResult.Message = "Request to remove task from the queue failed - could not obtain lock.";
            return taskResult.Failed();
          }          

        case WsCommandName.RemoveFromAssignment:
          int? taskIdToRemoveFromAssignment = command?.Parms?["ScheduledTaskID"]?.DbToInt32();
          if (!taskIdToRemoveFromAssignment.HasValue)
          {
            this.NotifyHostEvent("RemoveFromAssignment transaction failed - required ScheduledTaskId parameter not found.");
            taskResult.Message = "Request to remove task from assignment failed - required ScheduledTaskId parameter not found.";
            return taskResult.Failed();
          }

          if (Monitor.TryEnter(_taskManagementObject, 2000))
          {
            if (!_tasksToRemoveFromAssignment.Contains(taskIdToRemoveFromAssignment.Value))
              _tasksToRemoveFromAssignment.Add(taskIdToRemoveFromAssignment.Value);
            Monitor.Exit(_taskManagementObject);
            this.NotifyHostEvent("RemoveFromAssignment transaction processed.");
            taskResult.Message = "Request to remove task from assignment was processed successfully.";
            return taskResult.Success();
          }
          else
          {
            this.NotifyHostEvent("RemoveFromAssignment transaction failed - could not obtain lock.");
            taskResult.Message = "Request to remove task from assignment failed - could not obtain lock.";
            return taskResult.Failed();
          }

        case WsCommandName.RunNow:
          this.NotifyHostEvent("RunNow transaction processed.");
          taskResult.Message = "This will be the run now response.";
          return taskResult.Success();
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

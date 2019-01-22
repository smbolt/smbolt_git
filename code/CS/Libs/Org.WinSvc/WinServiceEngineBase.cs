using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TPL = System.Threading.Tasks;
using System.Threading;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Runtime.Remoting.Lifetime;
using Org.WSO;
using Org.WSO.Transactions;
using Org.Notify;
using Org.GS.AppDomainManagement;
using Org.GS.Notifications;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS;

namespace Org.WinSvc
{
  public class WinServiceEngineBase : IWinServiceEngine, IDisposable
  {
    #region IWinServiceEngine interface events and properties

    public event Action<IpdxMessage> NotifyHost;

    public string ServiceName { get; set; }
    public string EngineName { get; set; }
    public int EntityId { get; set; }
    public WinServiceState WinServiceState { get; set; }
    public bool RunningAsService { get { return this.WinServiceParms.IsRunningAsWindowsService; } }
    public bool IsSuspended { get; set; }
    public bool IsSuspendedReported { get; set; }
    public bool RefreshTaskList { get; set; }
    public bool RefreshTaskRequests { get; set; }
    public bool NotifyOnTaskRefresh { get; set; }
    public ITaskDispatcher TaskDispatcher { get; set; }
    public TaskRequestSet TaskRequests { get; set; }
    public int TasksProcessed { get; set; }
    protected string OverrideEnvironment { get; set; }
    
    private bool _getNextTaskNow = false;
    private object GetNextTaskNow_LockObject = new object();
    public bool GetNextTaskNow 
    {
      get { return Get_GetNextTaskNowValue(); }
      set { Set_GetNextTaskNowValue(value); }
    }

    #endregion

    public TPL.Task ProcessTasksThread { get; set; }
    public CancellationTokenSource ProcessTasks_CancellationTokenSource { get; set; }
    public ManualResetEvent ProcessTasks_ResetEvent { get; set; }
    public bool ProcessTaskHasBeenStopped { get; set; }
    public TPL.Task MonitorMainLoopThread { get; set; }
    public CancellationTokenSource MonitorMainLoop_CancellationTokenSource { get; set; }
    public ManualResetEvent MonitorMainLoop_ResetEvent { get; set; }

    private a a;
    private static Func<WsCommand, TaskResult> SuperMethods;
    protected Dictionary<string, string> DevComputers { get; set; }
    protected Dictionary<string, string> DevUsers { get; set; }
    protected string DomainAndComputer { get; set; }

    public bool ScheduleOnceNow { get; set; }
    public string TaskToRun { get; set; }
    public bool IsDryRun { get; set; }
    public bool SuppressNotifications { get; set; }
    public Dictionary<string, string> OverrideParms { get; set; }
    public EngineMonitorParms EngineMonitorParms { get; set; }

    public string DefaultNotifyEventName 
    { 
      get 
      {
        if (_configNotifySpec == null || _configNotifySpec.NotifyDefaultEventName.IsBlank())
          return String.Empty;
        return _configNotifySpec.NotifyDefaultEventName;
      } 
    }

    public bool NotifyOnGetTasks
    {
      get
      {
        if (_configNotifySpec == null)
          return false;
        return _configNotifySpec.NotifyOnGetTasks; 
      }
    }

    protected WinServiceParms WinServiceParms { get; set; }
    protected Logger Logger;
    protected ServiceHost ServiceHost { get; set; }

    private ConfigSmtpSpec _configSmtpSpec;
    private SmtpParms _smtpParms;
    private ConfigNotifySpec _configNotifySpec;
    private ConfigDbSpec _notifyDbSpec;
    private NotificationsManager _notificationsManager;
    private NotifyConfigSets _notifyConfigs;
    private NotifyConfigSets _defaultNotifyConfigs;

    public WinServiceEngineBase(WinServiceParms winServiceParms, string engineName)
    {
      try
      {

        StartupLogging.WriteStartupLog("WinServiceEngineBase - entering constructor.");

        // set up remote (non-default) AppDomain objects to last 75 minutes minimum.
        if (!g.LeaseTimeSet)
        {
          LifetimeServices.LeaseTime = TimeSpan.FromMinutes(75);
          LifetimeServices.RenewOnCallTime = TimeSpan.FromMinutes(75);
          g.LeaseTimeSet = true;
        }

        MarshalBase.InitialLeaseTime = TimeSpan.FromMinutes(75);
        MarshalBase.RenewOnCallTime = TimeSpan.FromMinutes(75);
        MarshalBase.SponsorshipTimeout = TimeSpan.FromMinutes(2);

        this.OverrideEnvironment = String.Empty;
        this.ServiceName = winServiceParms.WindowsServiceName;
        this.EngineName = engineName;
        this.WinServiceParms = winServiceParms;
        this.Logger = new Logger();
        g.ModuleId = GetModuleId();
        this.Logger.ModuleId = g.ModuleId.Value;
        this.IsSuspendedReported = false;

        this.EngineMonitorParms = new EngineMonitorParms();

        IPDX.FireIpdxEvent += IPDX_EventHandler;

        StartupLogging.WriteStartupLog("WinServiceEngineBase - existing constructor.");
      }
      catch (Exception ex)
      {
        string errorMessage = "An exception occurred in the constructor of WinServiceEngineBase." + g.crlf + ex.ToReport();
        StartupLogging.WriteStartupLog(errorMessage);
        throw;
      }
    }

    #region IWinServiceEngine interface methods

    public virtual bool Start()
    {
      throw new Exception("Base virtual method 'Start' must be overridden in derived class.");
    }

    public virtual void Stop()
    {
      throw new Exception("Base virtual method 'Stop' must be overridden in derived class.");
    }

    public virtual void Pause(bool pauseWebService = true)
    {
      throw new Exception("Base virtual method 'Pause' must be overridden in derived class.");
    }

    public virtual void Resume(bool pauseWebService = true)
    {
      throw new Exception("Base virtual method 'Resume' must be overridden in derived class.");
    }

    public void WireUpSuperMethod(Func<WsCommand, TaskResult> func)
    {
      SuperMethods = func;
    }

    #endregion


    private int GetModuleId()
    {
      string executablePath = AppDomain.CurrentDomain.BaseDirectory.ToLower();

      if (executablePath.Contains(@"c:\dev-main\"))
        return 1225;

      if (executablePath.Contains(@"\gptaskservice"))
      {
        int beg = executablePath.IndexOf(@"\gptaskservice");
        int end = executablePath.IndexOf(@"\", beg + 14);
        string serviceName = executablePath.Substring(beg + 1, end - (beg + 1));

        switch (serviceName)
        {
          case "gptaskservice01": return 2601;
          case "gptaskservice02": return 2602;
          case "gptaskservice03": return 2603;
          case "gptaskservice04": return 2604;
          case "gptaskservice05": return 2605;
          case "gptaskservice06": return 2606;
          case "gptaskservice07": return 2607;
          case "gptaskservice08": return 2608;
          case "gptaskservice09": return 2609;
          case "gptaskservice10": return 2610;
        }
      }

      if (executablePath.Contains("smartsheetdatameshservice"))
        return 2503;

      return 2500;
    }

    public void StartWebService()
    {
      // NEED TO GENERALIZE THE NAMING IN THIS METHOD - THERE ARE REFERENCES TO THE TASK ENGINE


      StartupLogging.WriteStartupLog("Just entered method TaskEngine.StartWebService.");
      bool runWebService = this.WinServiceParms.RunWebService;

      this.Logger.Log("WinServiceEngineBase.StartWebService Method beginning.", 6038);

      this.ServiceHost = null;

      try
      {
        StartupLogging.WriteStartupLog("In WinServiceEngineBase.StartWebService - WinService 'RunWebService' configuration item is set to '" + runWebService.ToString() + "'.");
        this.Logger.Log("RunWebService configuration item is set to '" + runWebService.ToString() + "'.", 6039);

        if (runWebService)
        {
          StartupLogging.WriteStartupLog("In WinServiceEngineBase.StartWebService - Beginning procedure to start the WinService Web Service.");

          ConfigWsSpec configWsSpec = g.AppConfig.GetWsSpec("WsHost");

          StartupLogging.WriteStartupLog("In WinServiceEngineBase.StartWebService - WinService Web Service endpoint is set to '" + configWsSpec.WebServiceEndpoint + "'.");
          this.Logger.Log("WinService web service configuration item is set to '" + configWsSpec.WebServiceEndpoint + "'.", 6040);

          this.ServiceHost = new ServiceHost(typeof(WinServiceWebService), new Uri(configWsSpec.WebServiceEndpoint));

          WinServiceWebService.WireUpWebService(this.MessageProcessor);

          System.ServiceModel.Channels.Binding binding;
          System.ServiceModel.Channels.Binding mexBinding;

          if (configWsSpec.WsBinding == WebServiceBinding.BasicHttp)
          {
            StartupLogging.WriteStartupLog("In WinServiceEngineBase.StartWebService - WinService web service will be started using HTTP protocol.");
            this.Logger.Log("WinService web service will be started using HTTP protocol and endpoint '" + configWsSpec.WebServiceEndpoint + "'.", 6041);
            binding = ConstructBinding("HTTP");
            mexBinding = MetadataExchangeBindings.CreateMexHttpBinding();
          }
          else
          {
            StartupLogging.WriteStartupLog("In WinServiceEngineBase.StartWebService - WinService Web Service will be started using TCP protocol.");
            this.Logger.Log("WinService web service will be started using TCP protocol and endpoint '" + configWsSpec.WebServiceEndpoint + "'.", 6041);
            binding = ConstructBinding("TCP");
            mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
          }

          this.ServiceHost.AddServiceEndpoint(typeof(ISimpleService), binding, "");
          ServiceMetadataBehavior metadataBehavior = new ServiceMetadataBehavior();
          this.ServiceHost.Description.Behaviors.Add(metadataBehavior);
          this.ServiceHost.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, configWsSpec.WebServiceEndpoint + @"/mex");

          StartupLogging.WriteStartupLog("In WinServiceEngineBase.StartWebService - WinService Web Service configuration complete.");
          StartupLogging.WriteStartupLog("In WinServiceEngineBase.StartWebService - Just prior to call of Open method on _serviceHost.");
          this.ServiceHost.Open();

          StartupLogging.WriteStartupLog("In WinServiceEngineBase.StartWebService - Just after to call of Open method on _serviceHost - status of host is '"
              + this.ServiceHost.State.ToString() + "'.");

          this.Logger.Log("WinService web service has been opened and its status is '" + this.ServiceHost.State.ToString() + "'.", 6042);
          this.Logger.Log("WinServiceEngineBase.StartWebService Method completed successfully.", 6043);

          StartupLogging.WriteStartupLog("Just prior to exiting method WinServiceEngineBase.StartWebService with '_runWebService' set to True.");

          NotifyHostEvent("Web Service is open and listening on port " + configWsSpec.WsPort + ".");
        }
        else
        {
          StartupLogging.WriteStartupLog("Just prior to exiting method WinServiceEngineBase.StartWebService with '_runWebService' set to False.");
        }
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred while attempting to start the web service.", ex, 6044, true, true, true, true);
      }
    }

    public void PauseWebService()
    {
      this.Logger.Log("WinServiceEngineBase.PauseWebService Method beginning.", 6045);

      if (this.ServiceHost == null)
      {
        this.Logger.Log("No WinServiceEngineBase WinService Web Service exists - exiting WinServiceEngineBase.PauseWebService method.", 6046);
        return;
      }

      try
      {
        if (this.ServiceHost.State != CommunicationState.Closed && this.ServiceHost.State != CommunicationState.Closing)
        {
          this.Logger.Log("WinServiceEngineBase WinService Web Service is being closed but not destroyed.", 6047);
          this.ServiceHost.Close();
        }

        this.Logger.Log("WinServiceEngineBase.PauseWebService Method finished.", 6048);
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred while attempting to pause the web service.", ex, 6049, false, true, true, true);
      }
    }

    public void ResumeWebService()
    {
      this.Logger.Log("WinServiceEngineBase.ResumeWebService Method beginning.", 6050);

      if (this.ServiceHost == null)
      {
        this.Logger.Log("No WinServiceEngineBase WinService Web Service exists - exiting WinServiceEngineBase.ResumeWebService method.", 6051);
        return;
      }

      try
      {
        if (this.ServiceHost.State != CommunicationState.Opened && this.ServiceHost.State != CommunicationState.Opening)
        {
          this.Logger.Log("Previously existing WinServiceEngineBase WinService Web Service is being re-opened.", 6052);
          this.ServiceHost.Open();
        }

        this.Logger.Log("WinServiceEngineBase.ResumeWebService Method finished.", 6053);
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred while attempting to resume the web service.", ex, 6054, false, true, true, true);
      }
    }

    public void StopWebService()
    {
      this.Logger.Log("WinServiceEngineBase.StopWebService Method beginning.", 6055);

      if (this.ServiceHost == null)
      {
        this.Logger.Log("No WinServiceEngineBase WinService Web Service exists - exiting WinServiceEngineBase.StopWebService method.", 6056);
        return;
      }

      try
      {
        if (this.ServiceHost.State != CommunicationState.Closed && this.ServiceHost.State != CommunicationState.Closing)
        {
          this.Logger.Log("WinServiceEngineBase WinService Web Services is being closed.", 6057);
          this.ServiceHost.Close();
        }

        this.ServiceHost = null;

        this.Logger.Log("WinServiceEngineBase.StopWebService Method finished.", 6058);
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred while attempting to stop the web service.", ex, 6059, false, true, true, true);
      }
    }

    public virtual void ReExecuteLastRunTaskRequest()
    {
      // override in derived types is desired
    }

    private System.ServiceModel.Channels.Binding ConstructBinding(string bindingProtocol)
    {
      XmlDictionaryReaderQuotas quotas = new XmlDictionaryReaderQuotas();
      quotas.MaxStringContentLength = 2147483647;
      quotas.MaxArrayLength = 2147483647;
      quotas.MaxBytesPerRead = 16348;
      quotas.MaxDepth = 64;

      switch (bindingProtocol)
      {
        case "TCP":
          NetTcpBinding tcpBinding = new NetTcpBinding();
          tcpBinding.Name = "WinServiceEngineBase";
          tcpBinding.ReaderQuotas = quotas;
          tcpBinding.MaxReceivedMessageSize = 2147483647;
          tcpBinding.MaxBufferSize = 2147483647;
          return tcpBinding;

        default:
          BasicHttpBinding httpBinding = new BasicHttpBinding();
          httpBinding.Name = "WinServiceEngineBase";
          httpBinding.ReaderQuotas = quotas;
          httpBinding.MaxReceivedMessageSize = 2147483647;
          httpBinding.MaxBufferSize = 2147483647;
          return httpBinding;
      }
    }

    public void NotifyMessageHandler(NotifyMessage notifyMessage)
    {
      if (notifyMessage == null)
        return;
      TPL.Task.Run(() => { this.ProcessNotifications(notifyMessage.Subject, notifyMessage.Message, notifyMessage.EventName); });
    }

    private XElement MessageProcessor(WsMessage receivedMessage, Org.WSO.ServiceBase serviceBase)
    {
      try
      {
        var engine = new WebServiceEngine(serviceBase);
        engine.Message = receivedMessage;

        using (var requestProcessor = new WebServiceRequestProcessor())
        {
          requestProcessor.WireUpTransactionProcessor(WebServiceTransactionProcessor);
          requestProcessor.SetBaseAndEngine(serviceBase, engine);
          var transactionResult = requestProcessor.ProcessRequest();
          return transactionResult;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to process a web service call in the WinServiceEngineBase.", ex);
      }
    }

    private TaskResult WebServiceTransactionProcessor(TransactionBase trans)
    {
      try
      {
        var taskResult = new TaskResult(trans.Name);

        switch (trans.Name)
        {
          case "PingRequest":
            NotifyHostEvent("Ping web service request processed.");
            var pingResponse = new PingResponse();
            pingResponse.TransactionStatus = TransactionStatus.Success;
            pingResponse.Message = "Ping transaction processed successfully.";
            taskResult.Object = pingResponse;
            return taskResult.Success();

          case "GetAssemblyReportRequest":
            var getAssemblyReportRequest = trans as GetAssemblyReportRequest;
            NotifyHostEvent("GetAssemblyReportRequest web service request processed.");
            var assemblyReportResponse = new GetAssemblyReportResponse();
            assemblyReportResponse.AssemblyReport = AssemblyHelper.GetAssemblyReport(getAssemblyReportRequest.IncludeAllAssemblies);

            taskResult.Object = assemblyReportResponse;
            return taskResult.Success();

          case "WsCommandRequest":
            var wsCommandRequest = trans as WsCommandRequest;
            if (wsCommandRequest.WsCommandSet.Count > 0)
            {
              var command = wsCommandRequest.WsCommandSet[0];
              if (SuperMethods != null)
              {
                var superTaskResult = SuperMethods(command);
                if (superTaskResult.TaskResultStatus == TaskResultStatus.Success)
                {
                  var commandResponse = new WsCommandResponse();
                  command.TaskResultStatus = TaskResultStatus.Success;
                  command.Message = "Command '" + command.WsCommandName.ToString() + "' executed successfully." + g.crlf2 + superTaskResult.Message;
                  commandResponse.WsCommandSet.Add(command);
                  taskResult.Object = commandResponse;
                  return taskResult.Success();
                }
                else
                {
                  taskResult.Message = "Processing of 'GetRunningtTaskReportRequest' was not successful - status is '" + superTaskResult.TaskResultStatus.ToString() +
                                       "Message is: " + superTaskResult.NotificationMessage;
                  taskResult.TaskResultStatus = superTaskResult.TaskResultStatus;
                  return taskResult;
                }
              }
              else
              {
                taskResult.Message = "Processing of 'SendServiceCommand' failed. There was no reference to a super class function for processing web service " +
                                     "transactions that are passed up to the super (derived class), in this case the WinServiceEngineBase.";
                taskResult.TaskResultStatus = TaskResultStatus.Failed; ;
                return taskResult;
              }
            }
            break;

          case "GetRunningTasksReportRequest":
            var getRunningTasksReport = trans as GetRunningTasksReportRequest;
            NotifyHostEvent("GetRunningTasksReport web service request processed.");
            if (SuperMethods != null)
            {
              var command = new WsCommand();
              command.WsCommandName = WsCommandName.GetRunningTaskReport;
              return SuperMethods(command);
            }
            else
            {
              taskResult.Message = "Processing of 'GetRunningtTaskReportRequest' failed. There was no reference to a super class function for processing web service " +
                                   "transactions that are passed up to the super (derived class), in this case the WinServiceEngineBase.";
              taskResult.TaskResultStatus = TaskResultStatus.Failed; ;
              return taskResult;
            }
        }

        return taskResult.Success();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to process the web service command in the WinServiceEngineBase.", ex);
      }
    }

    public void NotifyHostEvent(string message)
    {
      if (NotifyHost == null)
        return;

      if (message == null)
        return;

      TPL.Task.Factory.StartNew(() => {
        NotifyHost(new IpdxMessage("UI", message));
      });
    }

    public void NotifyHostEvent(IpdxMessage message)
    {
      if (NotifyHost == null)
        return;

      if (message == null)
        return;

      TPL.Task.Factory.StartNew(() => {
        NotifyHost(message);
      });
    }

    public void DummyEventHandler(TaskResult taskResult) { } 

    protected void InitializeBase()
    {
      try
      {
        this.WinServiceState = WinServiceState.Stopped;

        if (this.WinServiceParms.IsRunningAsWindowsService)
          a = new a();
        else
          a = new a(false, false);
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred during WinServiceEngineBase initialization (during the creation of the application object 'a').", ex, 6026, true, true, true, true);
      }

      try
      {
        if (this.OverrideEnvironment.IsNotBlank())
        {
          string selectedDbServer = g.CI(this.OverrideEnvironment + "_DBSERVER");
          if (g.AppConfig.Variables.ContainsKey("DB_SERVER") && this.OverrideEnvironment.In("PROD,TEST"))
          {
            g.AppConfig.Variables["DB_SERVER"] = selectedDbServer;
          }
        }
        
        DevComputers = g.GetDictionary("DevComputers");
        DevUsers = g.GetDictionary("DevUsers");
        DomainAndComputer = g.SystemInfo.DomainAndComputer;
        if (DevUsers.ContainsKey(DomainAndComputer))
        {
          if (!this.ServiceName.ToString().Contains(DevUsers[DomainAndComputer.ToString()]))
          {
            this.ServiceName = this.ServiceName + " - " + DevUsers[DomainAndComputer];
          }
        }

        if (DevComputers.ContainsKey(DomainAndComputer))
          DomainAndComputer = DevComputers[DomainAndComputer];

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' POINT 1.  Beginning WinServiceEngineBase.InitializeBase.");
        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' The application object 'a' has been initialized.");
        StartupLogging.WriteStartupLog("ConfigName is '" + g.AppConfig.ConfigName + "'.");
        StartupLogging.WriteStartupLog("AppConfig:" + g.crlf + g.AppConfig.CurrentImage);

        _notificationsManager = new NotificationsManager();
        _configSmtpSpec = g.GetSmtpSpec("Default");
        _smtpParms = new SmtpParms(_configSmtpSpec);
        _defaultNotifyConfigs = NotifyConfigHelper.GetNotifyConfigs(NotifyConfigMode.AppConfig);

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' POINT 2.  In WinServiceEngineBase.InitializeBase.");

        if (this.WinServiceParms.IsRunningAsWindowsService)
        {
          this.WinServiceParms.WindowsServiceName = g.CI("WindowsServiceName");
          this.WinServiceParms.EntityId = g.CI("EntityId").ToInt32OrDefault(0);
          this.WinServiceParms.ConfigLogSpecPrefix = g.CI("ConfigLogSpecPrefix").OrDefault("Default");
          this.WinServiceParms.ConfigNotifySpecPrefix = g.CI("ConfigNotifySpecPrefix").OrDefault("Default"); 
          this.WinServiceParms.MaxWaitIntervalMilliseconds = g.CI("MaxWaitIntervalMilliseconds").ToInt32();
          this.WinServiceParms.InDiagnosticsMode = g.CI("InDiagnosticsMode").ToBoolean();
          this.WinServiceParms.SuppressNonErrorOutput = g.CI("SuppressNonErrorOutput").ToBoolean();
          this.WinServiceParms.SuppressTaskReload = g.CI("SuppressTaskReload").ToBoolean();
          this.WinServiceParms.RunWebService = g.CI("RunWebService").ToBoolean();
        }

        this.EngineMonitorParms = GetEngineMonitorParms();

        _configNotifySpec = g.GetNotifySpec(this.WinServiceParms.ConfigNotifySpecPrefix); 

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' POINT 3.  In WinServiceEngineBase.InitializeBase.");


        if (_configNotifySpec.NotifyConfigMode == NotifyConfigMode.Database)
        {
          _notifyDbSpec = g.GetDbSpec(_configNotifySpec.NotifyDbSpecPrefix);
          if (!_notifyDbSpec.IsReadyToConnect())
            throw new Exception("The NotifyConfigDbSpec is not ready to connect to the database - connection string prefix is '" + this.WinServiceParms.ConfigNotifySpecPrefix + ".");
          NotifyConfigHelper.SetNotifyConfigDbSpec(_notifyDbSpec); 
          _notifyConfigs = NotifyConfigHelper.GetNotifyConfigs(_configNotifySpec.NotifyConfigMode);
        }
        else
        {
          _notifyConfigs = NotifyConfigHelper.GetNotifyConfigs(_configNotifySpec.NotifyConfigMode); 
        }

        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' POINT 4.  In WinServiceEngineBase.InitializeBase.");

        this.IsSuspended = false;
        this.IsSuspendedReported = false;
        
        StartupLogging.WriteStartupLog(g.AppInfo.OrgApplicationType.ToString() + " '" + g.AppInfo.AppName + "' POINT 5.  Successful completion of WinServiceEngineBase.InitializeBase.");
      }
      catch (Exception ex)
      {
        HandleExceptions("An exception occurred during WinServiceEngineBase initialization.", ex, 6027, true, true, true, true); 
      }
    }

    public EngineMonitorParms GetEngineMonitorParms()
    {
      var parms = new EngineMonitorParms();
      parms.EngineMonitorActive = g.CI("EngineMonitorActive").ToBoolean(true);
      parms.EngineMonitorIntervalSeconds = g.CI("EngineMonitorIntervalSeconds").ToInt32OrDefault(900);
      parms.EngineMonitorAttemptRestart = g.CI("EngineMonitorAttemptRestart").ToBoolean(true);
      parms.EngineMonitorNotifyOnRestartAttempts = g.CI("EngineMonitorNotifyOnRestartAttempts").ToBoolean(true);
      parms.EngineMonitorNotifyLimit = g.CI("EngineMonitorNotifyLimit").ToInt32OrDefault(5);
      parms.EngineMonitorResetNotifyLimitHours = g.CI("EngineMonitorResetNotifyLimitHours").ToInt32OrDefault(12);
      parms.EngineMonitorRunDependenciesChecks = g.CI("EngineMonitorRunDependenciesChecks").ToBoolean(true);
      parms.EngineMonitorDependencyCheckLimit = g.CI("EngineMonitorDependencyCheckLimit").ToInt32OrDefault(9999999);
      parms.EngineMonitorDependencyRetryIntervalSeconds = g.CI("EngineMonitorDependencyRetryIntervalSeconds").ToInt32OrDefault(900);
      parms.LogNormalMonitoringActivity = g.CI("LogNormalMonitoringActivity").ToBoolean(false);
      return parms;
    }

    protected void HandleExceptions(string message, Exception ex, int code, bool useStartupLogging, bool rethrowException, bool fireEventToHost, bool stopService)
    {
      ProcessNotifications(new Exception(message, ex), _configNotifySpec.NotifyDefaultEventName);

      this.Logger.Log(LogSeverity.SEVR, message, code, ex);

      if (useStartupLogging)
        StartupLogging.WriteStartupLog(message + g.crlf + ex.ToReport());

      WinServiceHelper.ServiceAlert(message + g.crlf2 + ex.ToReport());

      if (stopService)
      {
        if (SuperMethods != null)
        {
          var superTaskResult = SuperMethods(new WsCommand(WsCommandName.StopWinService));
        }
      }

      if (rethrowException)
        throw new Exception(message, ex);
    }


    protected void ProcessNotifications(Exception exception, string errorEventName)
    {
      try
      {
        if (this.SuppressNotifications)
          return;

        var notifyConfigs = _notifyConfigs;
        string notifyConfigSetName = _configNotifySpec.NotifyConfigSetName;

        if (notifyConfigs == null)
        {
          notifyConfigs = _defaultNotifyConfigs;
          notifyConfigSetName = "Default";
        }

        if (notifyConfigs == null)
          return;

        if (!notifyConfigs.ContainsKey(notifyConfigSetName))
          return;

        var notifyConfigSet = notifyConfigs[notifyConfigSetName];

        if (!notifyConfigSet.ContainsConfigForNamedEvent(errorEventName))
          return;

        var notification = new Notification(errorEventName, notifyConfigSet, exception);

        if (notification.NotificationStatus != NotificationStatus.ReadyToSend)
          // do we do any logging here - might just be no configurations 
          return;

        using (var notifyEngine = new NotifyEngine(notifyConfigSet, _smtpParms))
        {
          notifyEngine.NotifyAction += _notificationsManager.NotifyActionHandler;
          notifyEngine.ProcessNotificationsAsync(notification).ContinueWith(r => { NotificationsAsyncComplete(r); });
        }
      }
      catch (Exception ex)
      {
        // handle notification errors by doing some logging - don't stop the operational processes for the sake of notifications
      }
    }

    protected void ProcessNotifications(string message, string eventName)
    {
      try
      {
        if (this.SuppressNotifications)
          return;

        var notifyConfigs = _notifyConfigs;
        string notifyConfigSetName = _configNotifySpec.NotifyConfigSetName;

        if (notifyConfigs == null)
        {
          notifyConfigs = _defaultNotifyConfigs;
          notifyConfigSetName = "Default";
        }

        if (notifyConfigs == null)
          return;

        if (!notifyConfigs.ContainsKey(notifyConfigSetName))
          return;

        var notifyConfigSet = notifyConfigs[notifyConfigSetName];

        if (!notifyConfigSet.ContainsConfigForNamedEvent(eventName))
          return;

        var notification = new Notification(eventName, notifyConfigSet, message);

        if (notification.NotificationStatus != NotificationStatus.ReadyToSend)
          // do we do any logging here - might just be no configurations 
          return;

        using (var notifyEngine = new NotifyEngine(notifyConfigSet, _smtpParms))
        {
          notifyEngine.NotifyAction += _notificationsManager.NotifyActionHandler;
          notifyEngine.ProcessNotificationsAsync(notification).ContinueWith(r => { NotificationsAsyncComplete(r); });
        }
      }
      catch (Exception ex)
      {
        // handle notification errors by doing some logging - don't stop the operational processes for the sake of notifications
      }
    }

    public void ProcessNotifications(string subject, string message, string notifyEventName)
    {
      try
      {
        if (this.SuppressNotifications)
          return;

        var notifyConfigs = _notifyConfigs;
        var notifyConfigSetName = _configNotifySpec.NotifyConfigSetName;

        if (notifyConfigs == null)
          notifyConfigs = _defaultNotifyConfigs;

        if (notifyConfigs == null)
          return;

        if (!notifyConfigs.ContainsKey(notifyConfigSetName))
          return;

        var notifyConfigSet = notifyConfigs[notifyConfigSetName];

        var notification = new Notification();
        notification.Subject = subject;
        notification.Body = message;
        notification.NotificationSource = NotificationSource.NamedEvent;
        string eventName = notifyEventName;
        if (eventName.IsBlank())
          eventName = _configNotifySpec.NotifyDefaultEventName;
        notification.EventName = eventName;

        using (var notifyEngine = new NotifyEngine(notifyConfigSet, _smtpParms))
        {
          notifyEngine.NotifyAction += _notificationsManager.NotifyActionHandler;
          notifyEngine.ProcessNotificationsAsync(notification).ContinueWith(r => { NotificationsAsyncComplete(r); });
        }
      }
      catch (Exception ex)
      {
        this.Logger.Log(LogSeverity.WARN, "An exception occurred while attempting to process a notification with subject '" + subject + "' " +
                   "message '" + message.PadTo(100).Trim() + "' for notification event name '" + notifyEventName + "'.", 6015, ex);
      }
    }

    public void ProcessNotifications(TaskResult taskResult)
    {
      try
      {
        LogSeverity logSeverity = LogSeverity.INFO;
        switch (taskResult.TaskResultStatus)
        {
          case TaskResultStatus.Warning:
            logSeverity = LogSeverity.WARN;
            break;
          case TaskResultStatus.Failed:
            logSeverity = LogSeverity.SEVR;
            break;
        }

        this.Logger.RunId = taskResult.OriginalTaskRequest.RunId;
        string message = this.ServiceName + " - Task " + taskResult.TaskName + " completed with status '" + taskResult.TaskResultStatus.ToString() + "." + g.crlf + taskResult.Message;
        this.Logger.Log(logSeverity, message, 6112, this.EntityId, taskResult.Exception);
        this.Logger.RunId = null;

        if (this.SuppressNotifications)
          return;

        if (taskResult.TaskResultStatus == TaskResultStatus.Success && taskResult.NoWorkDone && !_configNotifySpec.NotifyOnNoWorkDone)
          return;

        if (taskResult.TaskResultStatus == TaskResultStatus.Success && taskResult.OriginalTaskRequest.SuppressNotificationsOnSuccess)
          return;

        if (!_notifyConfigs.ContainsKey(_configNotifySpec.NotifyConfigSetName))
          return;

        var notifyConfigSet = _notifyConfigs[_configNotifySpec.NotifyConfigSetName];

        if (!notifyConfigSet.ContainsConfigForTaskResult(taskResult))
          return;

        var notification = new Notification(taskResult, notifyConfigSet);

        if (notification.NotificationStatus != NotificationStatus.ReadyToSend)
          // do we do any logging here - might just be no configurations 
          return;

        using (var notifyEngine = new NotifyEngine(notifyConfigSet, _smtpParms))
        {
          notifyEngine.NotifyAction += _notificationsManager.NotifyActionHandler;
          notifyEngine.ProcessNotificationsAsync(notification).ContinueWith(r => { NotificationsAsyncComplete(r); });
        }
      }
      catch (Exception ex)
      {
        // handle notification errors by doing some logging - don't stop the operational processes for the sake of notifications
      }
    }

    private void NotificationsAsyncComplete(TPL.Task<TaskResult> result)
    {
      return;

      // this method is not doing anything productive.  need to implement logic for error conditions...

      Logger logger = new Logger();
      switch (result.Status)
      {
        case TPL.TaskStatus.RanToCompletion:
          TaskResult taskResult = result.Result;
          switch (taskResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              logger.Log(taskResult.NotificationMessage.ToString());

              foreach (var childTaskResult in taskResult.TaskResultSet.Values)
              {
                
              }


              break;

            case TaskResultStatus.Warning:

              break;

            case TaskResultStatus.Failed:

              break;

            default:

              break;

          }
          break;


        case TPL.TaskStatus.Faulted:

          break;


        default:

          break;
      }
    }
    
    protected void IPDX_EventHandler(IpdxMessage message)
    {
      switch (message.Recipient)
      {
        case "TaskEngine":
          switch (message.MessageType)
          {
            case IpdxMessageType.CommandSet:
              IpdxCommandSet commandSet = message.IpdxCommandSet;
              foreach (IpdxCommand command in commandSet)
              {
                switch (command.CommandName)
                {
                  case "SendServiceCommand":
                    if (command.Parms.ContainsKey("ServiceCommand"))
                    {
                      string serviceCommand = command.Parms["ServiceCommand"];
                      if (SuperMethods != null)
                      {
                        TaskResult superTaskResult = null; 

                        switch (serviceCommand)
                        {
                          case "START":
                            if (this.WinServiceState == WinServiceState.Stopped)
                            {
                              superTaskResult = SuperMethods(new WsCommand(WsCommandName.StartWinService));
                            }
                            break;

                          case "STOP":
                            if (this.WinServiceState == WinServiceState.Running)
                            {
                              superTaskResult = SuperMethods(new WsCommand(WsCommandName.StopWinService));
                            }
                            break;

                          case "PAUSE":
                            if (this.WinServiceState == WinServiceState.Running)
                            {
                              superTaskResult = SuperMethods(new WsCommand(WsCommandName.PauseWinService));
                            }
                            break;

                          case "RESUME":
                            if (this.WinServiceState == WinServiceState.Paused)
                            {
                              superTaskResult = SuperMethods(new WsCommand(WsCommandName.ResumeWinService));
                            }
                            break;
                        }
                      }
                    }
                    break;
                }
              }
              break;

            case IpdxMessageType.Notification:
              message.Recipient = "UI";
              NotifyHostEvent(message);
              break;

            case IpdxMessageType.Text:
              message.Recipient = "UI";
              NotifyHostEvent(message);
              break;
          }
          break;
      }
    }

    // retrieve value possibly managed by multiple threads
    private bool Get_GetNextTaskNowValue()
    {
      if (Monitor.TryEnter(GetNextTaskNow_LockObject, 1000))
      {
        try
        {
          return _getNextTaskNow;
        }
        catch
        {
          return false;
        }
        finally
        {
          Monitor.Exit(GetNextTaskNow_LockObject);
        }
      }
      else
      {
        return false;
      }
    }

    // set value possibly managed by multiple threads
    private void Set_GetNextTaskNowValue(bool value)
    {
      if (Monitor.TryEnter(GetNextTaskNow_LockObject, 1000))
      {
        try
        {
          _getNextTaskNow = value;
        }
        finally
        {
          Monitor.Exit(GetNextTaskNow_LockObject);
        }
      }
    }

    public void Dispose()
    {

    }
  }
}

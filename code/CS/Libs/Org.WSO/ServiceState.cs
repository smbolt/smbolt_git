using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Runtime.Remoting.Lifetime;
using System.Threading.Tasks;
using System.Reflection;
using Org.WSO;
using Org.WSO.Transactions;
using Org.Notify;
using Org.GS.AppDomainManagement;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS;

namespace Org.WSO
{
  [Serializable]
  public class ServiceState
  {
    private ServiceBase _serviceBase;
    public int InvokeCount {
      get;
      set;
    }
    public bool IsNew {
      get;
      set;
    }
    public string DefaultNotifyEventName {
      get;
      set;
    }
    public ComponentLoadMode ComponentLoadMode {
      get;
      set;
    }


    // AppDomain-Related Objects
    private AppDomainSupervisor _appDomainSupervisor;
    private AppDomainEventManager _appDomainEventManager;
    private Dictionary<string, CatalogEntry> _catalogEntries;

    public string CatalogStem {
      get;
      set;
    }
    public string CatalogEnvironment {
      get;
      set;
    }
    public string CatalogTaskNode {
      get;
      set;
    }
    public string CatalogName {
      get;
      set;
    }

    // MEF Module-Related Objects
    [ImportMany(typeof(IRequestProcessorFactory))]
    public IEnumerable<Lazy<IRequestProcessorFactory, IRequestProcessorMetadata>> requestProcessorFactories;
    public CompositionContainer CompositionContainer;
    public Dictionary<string, IRequestProcessorFactory> LoadedRequestProcessorFactories;

    private Dictionary<string, string> _devUsers;
    private Dictionary<string, string> _devComputers;
    public string WebServiceName {
      get;
      set;
    }
    public string ComputerName {
      get {
        return Get_ComputerName();
      }
    }

    public string NotifyConfigSetName {
      get;
      private set;
    }
    private NotifyConfigSets _defaultNotifyConfigs;
    public NotifyConfigSets DefaultNotifyConfigs {
      get {
        return _defaultNotifyConfigs;
      }
    }
    private NotifyConfigSets _notifyConfigs;
    public NotifyConfigSets NotifyConfigs {
      get {
        return _notifyConfigs;
      }
    }

    private ConfigSmtpSpec _notifySmtpSpec;
    private SmtpParms _notifySmtpParms;
    public SmtpParms NotifySmtpParms {
      get {
        return _notifySmtpParms;
      }
    }

    private Logger _logger;

    public ServiceState(ServiceBase serviceBase)
    {
      StartupLogging.WriteStartupLog("ServiceState - at top of constructor.");

      _serviceBase = serviceBase;
      this.IsNew = true;
      this.InvokeCount = 0;
      this.ComponentLoadMode = ComponentLoadMode.LocalCatalog;

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

      try
      {
        if (g.AppConfig == null || !g.AppConfig.IsLoaded)
        {
          new a();
        }
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to initialize the 'a' (application) object.", ex);
      }

      try
      {
        StartupLogging.WriteStartupLog("ServiceState - after creation of application object (a).");

        _devComputers = g.GetDictionary("DevComputers");
        _devUsers = g.GetDictionary("DevUsers");
        this.WebServiceName = g.CI("WebServiceName");

        string startupMessage = "ServiceState for " + g.AppInfo.AppName + " running on port " + _serviceBase.Port.ToString() + " is being initialized.";
        g.LogToMemory(startupMessage);
        _logger = new Logger();
        _logger.ModuleId = g.AppInfo.ModuleCode;

        this.ComponentLoadMode = g.ToEnum<ComponentLoadMode>(g.CI("ComponentLoadMode"), ComponentLoadMode.NotSet);

        if (this.ComponentLoadMode == ComponentLoadMode.NotSet)
          throw new Exception("The ComponentLoadMode configuration item (CI) is not specified - must be set to 'LocalCatalog' or 'CentralCatalog'.");

        _catalogEntries = new Dictionary<string, CatalogEntry>();
        var rawCatalogEntries = g.GetDictionary("CatalogEntries");
        foreach (var kvp in rawCatalogEntries)
        {
          string trans = kvp.Key;
          var catalogEntry = new CatalogEntry(kvp.Value);
          _catalogEntries.Add(trans, catalogEntry);
        }

        this.CatalogStem = g.CI("CATALOG_STEM");
        this.CatalogEnvironment = g.CI("CATALOG_ENV");
        this.CatalogTaskNode = g.CI("CATALOG_TASKNODE");
        this.CatalogName = g.CI("CATALOG_NAME");

        InitNotifyConfigs();

        StartupLogging.WriteStartupLog("ServiceState - after initialization of NotifyConfigs.");

        this.LoadedRequestProcessorFactories = new Dictionary<string, IRequestProcessorFactory>();

        if (this.ComponentLoadMode == ComponentLoadMode.LocalCatalog)
        {
          using (var catalog = new AggregateCatalog())
          {
            if (g.AppConfig.ContainsKey("MEFModulesPath") && g.CI("UseMEFModulesPath").ToBoolean())
            {
              string mefModulesPath = g.CI("MEFModulesPath");
              catalog.Catalogs.Add(new DirectoryCatalog(mefModulesPath));
            }
            else
            {
              var mefCatalog = new OSFolder(g.MEFCatalog);
              mefCatalog.SearchParms.ProcessChildFolders = true;
              var leafFolders = mefCatalog.GetLeafFolders();

              foreach (string leafFolder in leafFolders)
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
        }

        _appDomainSupervisor = new AppDomainSupervisor();
        _appDomainEventManager = new AppDomainEventManager();
        _appDomainEventManager.NotifyMessage += Module_NotifyMessage;
        _appDomainEventManager.ProgressUpdate += Module_ProgressUpdate;

        StartupLogging.WriteStartupLog("ServiceState - after loading of MEF components.");

        StartupLogging.WriteStartupLog("ServiceState - at end of constructor.");
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the constructor of ServiceState.", ex);
      }
    }

    public IRequestProcessorFactory GetRequestProcessorFactory(string processorKey)
    {
      if (this.LoadedRequestProcessorFactories.ContainsKey(processorKey))
        return this.LoadedRequestProcessorFactories[processorKey];

      foreach (Lazy<IRequestProcessorFactory, IRequestProcessorMetadata> requestProcessorFactory in requestProcessorFactories)
      {
        if (requestProcessorFactory.Metadata.Processors.ToListContains(Constants.SpaceDelimiter, processorKey))
        {
          this.LoadedRequestProcessorFactories.Add(processorKey, requestProcessorFactory.Value);
          return requestProcessorFactory.Value;
        }
      }

      return null;
    }


    public IRequestProcessorFactory GetRequestProcessorFactoryFromAppDomain(string assemblyName, string processorKey, string catalogName, string catalogType, string objectTypeName)
    {
      try
      {
        var appDomainSetUp = new AppDomainSetup();
        string catalogFullPath = this.CatalogStem + @"\" + this.CatalogEnvironment + @"\" + this.CatalogTaskNode +
                                 @"\" + catalogName + @"\" + catalogType + @"\" + @"WSO\1.0.0.0";

        appDomainSetUp.ApplicationBase = catalogFullPath;
        var objDesc = new AppDomainObjectDescriptor(assemblyName, processorKey, assemblyName, objectTypeName, appDomainSetUp);
        var requestProcessorFactory = (IRequestProcessorFactory)_appDomainSupervisor.GetObject(objDesc);
        return requestProcessorFactory;
      }
      catch
      {
        throw new Exception("An exception occurred while attempting to retrieve the object named '" + processorKey + "' from a child AppDomain.");
      }
    }

    public TaskResult GetAppDomainReport()
    {
      if (_appDomainSupervisor == null)
        return new TaskResult("GetAppDomainReport", "The _appDomainSupervisor object in ServiceState is null.", TaskResultStatus.Failed);

      string appDomainReport = _appDomainSupervisor.Report;

      return new TaskResult("GetAppDomainReport", appDomainReport, true);
    }

    public TaskResult FlushAppDomains()
    {
      try
      {
        if (_appDomainSupervisor == null)
          return new TaskResult("FlushAppDomains", "The AppDomainSupervisor object of the ServiceState object is null.", TaskResultStatus.Failed);

        var appDomainList = _appDomainSupervisor.AppDomainNames;
        _appDomainSupervisor.FlushAppDomains();

        return new TaskResult("FlushAppDomains", "The following AppDomains have been unloaded: " + appDomainList.ToDelimitedList(",") + ".", true);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to flush all AppDomains.", ex);
      }
    }

    private void InitNotifyConfigs()
    {
      try
      {
        this.NotifyConfigSetName = g.CI("NotifyConfigSetName");
        this.DefaultNotifyEventName = g.CI("DefaultNotifyEventName");
        if (this.DefaultNotifyEventName.IsBlank())
          this.DefaultNotifyEventName = "WebServiceDefault";
        var notifyConfigMode = g.CI("NotifyConfigMode").ToEnum<NotifyConfigMode>(NotifyConfigMode.Database);
        _defaultNotifyConfigs = NotifyConfigHelper.GetNotifyConfigs(NotifyConfigMode.AppConfig);

        if (notifyConfigMode == NotifyConfigMode.Database)
        {
          string notifyDbSpecPrefix = g.CI("NotifyDbSpecPrefix");
          var notifyDbSpec = g.GetDbSpec(notifyDbSpecPrefix);
          if (!notifyDbSpec.IsReadyToConnect())
            throw new Exception("The Notifications database ConfigDbSpec is not ready to connect.");
          NotifyConfigHelper.SetNotifyConfigDbSpec(notifyDbSpec);
        }

        _notifyConfigs = NotifyConfigHelper.GetNotifyConfigs(notifyConfigMode);

        string notifySmtpSpecPrefix = g.CI("NotifySmtpSpecPrefix");
        if (notifySmtpSpecPrefix.IsBlank())
          notifySmtpSpecPrefix = "Default";

        _notifySmtpSpec = g.GetSmtpSpec(notifySmtpSpecPrefix);
        _notifySmtpParms = new SmtpParms(_notifySmtpSpec);

        StartupLogging.WriteStartupLog("Notification configs have been successfully created.");
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to initialize the notification configs.", ex);
      }
    }

    public CatalogEntry GetCatalogEntry(string transName)
    {
      if (_catalogEntries == null)
        throw new Exception("The _catalogEntries dictionary is null.");

      if (!_catalogEntries.ContainsKey(transName))
        throw new Exception("There is no catalog entry for transaction name '" + transName + "'.");

      return _catalogEntries[transName];
    }

    private string Get_ComputerName()
    {
      if (_devComputers != null && _devComputers.ContainsKey(g.SystemInfo.DomainAndComputer))
        return _devComputers[g.SystemInfo.DomainAndComputer];

      return g.SystemInfo.DomainAndComputer;
    }

    private void Module_ProgressUpdate(ProgressMessage progressMessage)
    {
      var message = new IpdxMessage();
      message.MessageType = IpdxMessageType.Text;
      message.Recipient = "UI";
      message.Text = progressMessage.ActivityName + " : " + progressMessage.MessageText + " - " + progressMessage.ActvityProgress;
      //NotifyHostEvent(message);
    }

    private void Module_NotifyMessage(NotifyMessage notifyMessage)
    {
      var message = new IpdxMessage();
      message.MessageType = IpdxMessageType.Text;
      message.Recipient = "UI";
      message.Text = notifyMessage.Subject + " : " + notifyMessage.Message;
      //NotifyHostEvent(message);
    }
  }
}

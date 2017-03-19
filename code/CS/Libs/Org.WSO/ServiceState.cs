using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Threading.Tasks;
using System.Reflection;
using Org.Notify;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;

namespace Org.WSO
{
  public class ServiceState
  {
    private a a;
    private ServiceBase _serviceBase;
    public int InvokeCount { get; set; }
    public bool IsNew { get; set; }
    public string DefaultNotifyEventName { get; set; }

    [ImportMany(typeof(IRequestProcessorFactory))]
    public IEnumerable<Lazy<IRequestProcessorFactory, IRequestProcessorMetadata>> requestProcessorFactories;
    public CompositionContainer CompositionContainer;
    public Dictionary<string, IRequestProcessorFactory> LoadedRequestProcessorFactories; 
    public string NotifyConfigSetName { get; private set; }
    private NotifyConfigSets _defaultNotifyConfigs;
    public NotifyConfigSets DefaultNotifyConfigs { get { return _defaultNotifyConfigs; } }
    private NotifyConfigSets _notifyConfigs;
    public NotifyConfigSets NotifyConfigs { get { return _notifyConfigs; } }

    private ConfigSmtpSpec _notifySmtpSpec;
    private SmtpParms _notifySmtpParms;
    public SmtpParms NotifySmtpParms { get { return _notifySmtpParms; } }

    private Logger _logger;

    public ServiceState(ServiceBase serviceBase)
    {
      StartupLogging.WriteStartupLog("ServiceState - at top of constructor.");

      _serviceBase = serviceBase;
      this.IsNew = true;
      this.InvokeCount = 0;

      try
      {
        if (g.AppConfig == null || !g.AppConfig.IsLoaded)
        {
          a = new a();
        }
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to initialize the 'a' (application) object.", ex); 
      }

      StartupLogging.WriteStartupLog("ServiceState - after creation of application object (a).");

      string startupMessage = "ServiceState for " + g.AppInfo.AppName + " running on port " + _serviceBase.Port.ToString() + " is being initialized.";
      g.LogToMemory(startupMessage); 
      _logger = new Logger();
      _logger.ModuleId = g.AppInfo.ModuleCode;

      InitNotifyConfigs();

      StartupLogging.WriteStartupLog("ServiceState - after initialization of NotifyConfigs.");
          
      this.LoadedRequestProcessorFactories = new Dictionary<string,IRequestProcessorFactory>();
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

          foreach(string leafFolder in leafFolders)
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

        StartupLogging.WriteStartupLog("ServiceState - after loading of MEF components.");

        StartupLogging.WriteStartupLog("ServiceState - at end of constructor.");
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
  }
}

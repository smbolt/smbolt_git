using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.WSO;
using Org.GS;
using Org.GS.Configuration;

namespace Org.RPTest
{
  public partial class frmMain : Form
  {
    private a a;

    public CompositionContainer CompositionContainer;

    // items for managing MEF request processors
    [ImportMany(typeof(IRequestProcessorFactory))]
    public IEnumerable<Lazy<IRequestProcessorFactory, IRequestProcessorMetadata>> requestProcessorFactories;
    public Dictionary<string, IRequestProcessorFactory> LoadedRequestProcessorFactories; 

    // items for managing MEF message factories
    [ImportMany(typeof(IMessageFactory))]
    public IEnumerable<Lazy<IMessageFactory, IMessageFactoryMetadata>> messageFactories;
    public Dictionary<string, IMessageFactory> LoadedMessageFactories; 

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "RunRequestProcessor":
          RunRequestProcessor();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void RunRequestProcessor()
    {
      this.Cursor = Cursors.WaitCursor;

      txtOut.Text = String.Empty;
      Application.DoEvents();
      System.Threading.Thread.Sleep(100); 

      try
      {
        var wsParms = BuildWsParms(cboRequestProcessors.Text);
        var messageFactory = GetMessageFactory(wsParms);

        if (messageFactory == null)
        {
          MessageBox.Show("The MessageFactory could not be located for transaction '" +
                          cboRequestProcessors.Text + "'.", "Request Processor Test - Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
          this.Cursor = Cursors.Default;
          return;
        }

        var requestMessage = messageFactory.CreateRequestMessage(wsParms);
        var responseTransBody = EmulateWebService(requestMessage);

        txtOut.Text = "REQUEST MESSAGE" + g.crlf + 
                      requestMessage.TransactionBody.ToString() + g.crlf2 + 
                      "RESPONSE MESSAGE" + g.crlf + 
                      responseTransBody.ToString();


        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during web service request processing." + g.crlf2 + ex.ToReport(), "Request Processor Test - Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      this.Cursor = Cursors.Default;
    }
    }

    private XElement EmulateWebService(WsMessage requestMessage)
    {
      XElement transBody = requestMessage.TransactionBody;
      string transAndVersion = requestMessage.TransactionHeader.ProcessorNameAndVersion;

      var requestProcessorFactory = GetRequestProcessorFactory(transAndVersion); 

      if (requestProcessorFactory == null)
        throw new Exception("RequestProcessorFactory could not be created for transaction '" + transAndVersion + "'.");

      using (var requestProcessor = requestProcessorFactory.CreateRequestProcessor(transAndVersion))
      {
        if (requestProcessor == null)
          throw new Exception("RequestProcessor could not be created for transaction '" + transAndVersion + "'.");

        requestProcessor.SetBaseAndEngine(null, new DummyTransactionEngine(requestMessage));

        return requestProcessor.ProcessRequest();
      }
    }

    
    public IMessageFactory GetMessageFactory(WsParms wsParms)
    {
      string transactionVersion = wsParms.TransactionName + "_" + wsParms.TransactionVersion;

      if (this.LoadedMessageFactories.ContainsKey(transactionVersion))
        return this.LoadedMessageFactories[transactionVersion];

      foreach (Lazy<IMessageFactory, IMessageFactoryMetadata> messageFactory in messageFactories)
      {
        if (messageFactory.Metadata.Transactions.Contains(transactionVersion))
        {
          this.LoadedMessageFactories.Add(transactionVersion, messageFactory.Value);
          return messageFactory.Value;
        }
      }

      return null;
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

    private WsParms BuildWsParms(string requestProcessor)
    {
      string[] tokens = requestProcessor.Split(Constants.UnderscoreDelimiter, StringSplitOptions.RemoveEmptyEntries);

      string requestProcessorName = tokens[0];
      string requestProcessorVersion = tokens[1];

      WsParms wsParms = new WsParms();
      wsParms.TransactionName = requestProcessorName;
      wsParms.TransactionVersion = requestProcessorVersion;
      wsParms.MessagingParticipant = MessagingParticipant.Sender;
      wsParms.ConfigWsSpec = null;
      wsParms.TrackPerformance = true;

      wsParms.DomainName = g.SystemInfo.DomainName;
      wsParms.MachineName = g.SystemInfo.ComputerName;
      wsParms.UserName = g.SystemInfo.UserName;
      wsParms.ModuleCode = g.AppInfo.ModuleCode;
      wsParms.ModuleName = g.AppInfo.ModuleName;
      wsParms.ModuleVersion = g.AppInfo.AppVersion;
      wsParms.AppName = g.AppInfo.AppName;
      wsParms.AppVersion = g.AppInfo.AppVersion;

      wsParms.ModuleCode = 303;
      wsParms.ModuleName = "RPTest";
      wsParms.ModuleVersion = "1.0.0.0";
      wsParms.OrgId = 3;

      return wsParms;
    }

    private void InitializeForm()
    {
      try
      {
        btnRunRequestProcessor.Enabled = false;

        a = new a();

        var requestProcessors = g.AppConfig.GetList("RequestProcessors");
        requestProcessors.Insert(0, String.Empty); 
        cboRequestProcessors.Items.Clear();
        cboRequestProcessors.DataSource = requestProcessors;

        this.LoadedRequestProcessorFactories = new Dictionary<string, IRequestProcessorFactory>();
        this.LoadedMessageFactories = new Dictionary<string, IMessageFactory>();

        MigrateMEFModules();
        ComposeMEFContainer();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(), "Request Processor Test - Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }


    private void MigrateMEFModules()
    {
      var fsActionSet = g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName].FSActionSet;

      using (var fsEngine = new FSEngine())
      {
        var taskResult = fsEngine.Run(fsActionSet);
        if (taskResult.TaskResultStatus != TaskResultStatus.Success)
        {
          MessageBox.Show("An error occurred attempting to migrate the MEF Modules." + g.crlf2 + taskResult.Message,
                          "RP Test - Module Migration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private void ComposeMEFContainer()
    {      
      this.LoadedRequestProcessorFactories = new Dictionary<string,IRequestProcessorFactory>();
      using (var catalog = new AggregateCatalog())
      {
        if (g.AppConfig.ContainsKey("MEFModulesPath"))
        {
          catalog.Catalogs.Add(new DirectoryCatalog(g.CI("MEFModulesPath")));
        }
        else
        {
          var mefCatalog = new OSFolder(g.MEFCatalog);
          mefCatalog.ProcessChildFolders = true;
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

    private void cboRequestProcessors_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnRunRequestProcessor.Enabled = cboRequestProcessors.Text.IsNotBlank();
    }
  }
}

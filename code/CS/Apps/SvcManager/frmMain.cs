using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TSK.Business;
using Org.TSK.Business.Models;
using Org.GS.ServiceManagement;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS;

namespace Org.SvcManager
{
  public partial class frmMain : Form
  {
    private a a;
    private Logger _logger;
    private ConfigDbSpec _taskProdDbSpec;
    private ConfigDbSpec _taskTestDbSpec;
    private ConfigDbSpec _taskDbSpec;

    private ServiceEnvironmentSet _serviceEnvironmentSet;
    private ServiceEnvironmentSet _serviceEnvironmentSetDB;
    private ServiceEnvironment _selectedEnvironment;
    private ServiceHost _selectedServiceHost;
    private ServiceType _selectedServiceType;
    private ServiceSpec _selectedServiceSpec;
    private WCFHostingModel _selectedWCFHostingModel;
    private ServiceSpec _selectedParentServiceSpec;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "ManageHosts":
          ManageHosts();
          break;

        case "ManageTaskServices":
          ManageTaskServices();
          break;

        case "AddWindowsService":
          AddWindowsService();
          break;

        case "AddWCFWebService":
        case "UpdateWCFWebService":
        case "DeleteWCFWebService":
          ManageWCFWebService(action);
          break;

        case "AddWebSite":
          AddWebSite();
          break;

        case "RunMigr":
          RunMigr();
          break;

        case "ManageTaskAssignments":
          ManageTaskAssignments();
          break;

        case "SetProdServiceTypes":
          SetServiceTypes("Prod");
          break;

        case "SetTestServiceTypes":
          SetServiceTypes("Test");
          break;

        case "RefreshTreeView":
          RefreshTreeView();
          break;

        case "Xml":
          RunXml();
          break;

        case "Exit":
          this.Close();
          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void RunXml()
    {
      string beforeConfig = g.AppConfig.CurrentImage;

      var beforeXml = XElement.Parse(beforeConfig);
      var afterXml = XElement.Parse(beforeConfig);

      bool equivalent = afterXml.IsEquivalent(beforeXml);


    }


    private void ManageHosts()
    {
      try
      {
        using (var fManageHosts = new frmManageHosts(_selectedEnvironment.Name, _taskDbSpec))
        {
          fManageHosts.ShowDialog(); 
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to manage the service hosts." + g.crlf2 + ex.ToReport(),
                        "Service Manager - Host Management Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ManageTaskServices()
    {
      try
      {
        using (var fTaskServices = new frmTaskServices(_taskProdDbSpec, _taskTestDbSpec))
        {
          fTaskServices.ShowDialog(); 
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to manage the task services." + g.crlf2 + ex.ToReport(),
                        "Service Manager - Task Service Management Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void AddWindowsService()
    {
      try
      {
        using (var fAddOrUpdateWindowsService = new frmAddOrUpdateWindowsService(_selectedEnvironment, _selectedServiceHost, _selectedServiceType, _taskDbSpec, true))
        {
          if (fAddOrUpdateWindowsService.ShowDialog() == DialogResult.OK)
          {
            RefreshTreeView(_selectedServiceHost.Name);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to add a new Windows Service." + g.crlf2 + ex.ToReport(),
                        "Service Manager - Error Adding Windows Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void RefreshTreeView(string selectedNode = "")
    {
      _serviceEnvironmentSet = GetServiceEnvironmentSet();
      LoadTreeView(selectedNode);
    }

    private void ManageWCFWebService(string action)
    {
      try
      {
        if (action == "DeleteWCFWebService")
        {
          RefreshTreeView();
        }
        else
        {
          bool isNew = action == "AddWCFWebService";

          ServiceSpec parentServiceSpec = null;
          if (isNew && _selectedServiceSpec != null)
            parentServiceSpec = _selectedServiceSpec;

          ServiceSpec existingServiceSpec = null;
          if (!isNew && _selectedServiceSpec != null)
            existingServiceSpec = _selectedServiceSpec;

          if (!isNew && _selectedParentServiceSpec != null)
            parentServiceSpec = _selectedParentServiceSpec;

          if (parentServiceSpec != null && parentServiceSpec.ServiceType == ServiceType.WindowsService)
          {
            _selectedWCFHostingModel = WCFHostingModel.HostedInWindowsService;
            _selectedServiceType = ServiceType.WCFWebService;
          }

          using (var fAddOrUpdateWCFService = new frmAddOrUpdateWCFService(existingServiceSpec, _selectedEnvironment, _selectedServiceHost, 
                     _selectedServiceType, _selectedWCFHostingModel, _taskDbSpec, parentServiceSpec, isNew))
          {
            if (fAddOrUpdateWCFService.ShowDialog() == DialogResult.OK)
            {
              RefreshTreeView();
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to manage the WCF web service." + g.crlf2 + ex.ToReport(),
                        "AppDomain Manager - WCF Web Service Management Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void AddWebSite()
    {

    }

    private void ManageTaskAssignments()
    {
      try
      {
        using (var fServiceAssignments = new frmServiceAssignments(_selectedServiceSpec, _taskDbSpec))
        {
          fServiceAssignments.ShowDialog(); 
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to manage the task assignments for a task service." + g.crlf2 + ex.ToReport(),
                        "AppDomain Manager - Task Assignment Management Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadTreeView(string selectedName = "")
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        tvMain.Nodes.Clear();

        if (_serviceEnvironmentSet == null)
          return;

        TreeNode rootNode = new TreeNode("Services", 0, 1);
        rootNode.Tag = "Services";
        tvMain.Nodes.Add(rootNode);

        foreach (var environment in _serviceEnvironmentSet.Values)
        {
          var envNode = new TreeNode(environment.Name, 2, 3);
          envNode.Tag = environment;
          rootNode.Nodes.Add(envNode);
          LoadTreeView(environment, envNode, 1);
        }

        tvMain.ExpandAll();

        if (selectedName.IsNotBlank())
          SelectTreeViewNode(tvMain.Nodes[0], selectedName);
        else
          tvMain.SelectedNode = rootNode;

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to load the services objects to the TreeView." + g.crlf2 + ex.ToReport(),
                        "AppDomain Manager - TreeView Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadTreeView(ServiceObject serviceObject, TreeNode parentNode, int level)
    {
      switch (serviceObject.ServiceObjectType)
      {
        case ServiceObjectType.ServiceEnvironment:
          var env = serviceObject as ServiceEnvironment;
          foreach (var serviceHost in env.ServiceHostSet.Values)
          {
            var hostNode = new TreeNode(serviceHost.Name, 4, 5);
            hostNode.Tag = serviceHost;
            parentNode.Nodes.Add(hostNode);
            LoadTreeView(serviceHost, hostNode, level + 1);
          }
          break;

        case ServiceObjectType.ServiceHost:
          var host = serviceObject as ServiceHost;

          var winsvcsNode = new TreeNode("Windows Services", 6, 7);
          winsvcsNode.Tag = "Windows Services";
          parentNode.Nodes.Add(winsvcsNode);

          foreach (var serviceSpec in host.ServiceSpecSet.Values)
          {
            if (serviceSpec.ServiceType == ServiceType.WindowsService)
            {
              var winsvcNode = new TreeNode(serviceSpec.Name, 6, 7);
              winsvcNode.Tag = serviceSpec;
              winsvcsNode.Nodes.Add(winsvcNode);

              foreach (var mgmtServiceSpec in serviceSpec.ServiceSpecSet.Values)
              {
                var wcfsvcNode = new TreeNode(mgmtServiceSpec.Name + " (" + mgmtServiceSpec.WsPort + ")", 8, 9);
                wcfsvcNode.Tag = mgmtServiceSpec;
                winsvcNode.Nodes.Add(wcfsvcNode);
              }
            }
          }
          
          var wcfsvcsNode = new TreeNode("WCF Web Services", 8, 9);
          wcfsvcsNode.Tag = "WCF Web Services";
          parentNode.Nodes.Add(wcfsvcsNode);

          foreach (var serviceSpec in host.ServiceSpecSet.Values)
          {
            if (serviceSpec.ServiceType == ServiceType.WCFWebService)
            {
              var wcfsvcNode = new TreeNode(serviceSpec.Name + " (" + serviceSpec.WsPort + ")", 8, 9);
              wcfsvcNode.Tag = serviceSpec;
              wcfsvcsNode.Nodes.Add(wcfsvcNode);
            }              
          }
          break;
      }
    }

    private bool SelectTreeViewNode(TreeNode node, string selectedName)
    {
      if (node.Name == selectedName)
      {
        tvMain.SelectedNode = node;
        return true;
      }

      foreach (TreeNode childNode in node.Nodes)
      {
        if (SelectTreeViewNode(childNode, selectedName))
          return true;
      }

      return false;
    }

    private void SetServiceTypes(string env)
    {
      var dbSpec = env == "Prod" ? _taskProdDbSpec : _taskTestDbSpec;

      try
      {
        using (var taskRepo = new TaskRepository(dbSpec))
        {
          taskRepo.SetServiceTypes();
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to establish the service types in the database." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Establishing Service Types", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private ServiceEnvironmentSet GetServiceEnvironmentSet()
    {
      try
      {
        var serviceEnvironmentSet = new ServiceEnvironmentSet();
        var prodEnvironment = new ServiceEnvironment();
        prodEnvironment.Name = "Prod";
        prodEnvironment.IsProductionEnvironment = true;
        serviceEnvironmentSet.Add(prodEnvironment.Name, prodEnvironment);

        var testEnvironment = new ServiceEnvironment();
        testEnvironment.Name = "Test";
        testEnvironment.IsProductionEnvironment = false;
        serviceEnvironmentSet.Add(testEnvironment.Name, testEnvironment);

        foreach (var env in serviceEnvironmentSet.Values)
        {
          var dbSpec = env.Name == "Prod" ? _taskProdDbSpec : _taskTestDbSpec;
          
          using (var taskRepo = new TaskRepository(dbSpec))
          {
            env.ServiceHostSet = taskRepo.GetServiceHostSet(env);
          }
        }

        return serviceEnvironmentSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to build the ServiceEnvironmentSet from the database.", ex); 
      }
    }

    private void RunMigr()
    {
      ProcessParms processParms = new ProcessParms();
      processParms.ExecutablePath = @"C:\DEV-MAIN\Main\Apps\migr\bin\Debug\migr.exe";
      processParms.Args = new string[] {
        "\"-baseDir\"",
        "\"C:\\_work\\migr\"",
        "\"-scripted\"",
        "\"-test\""
      };

      var processHelper = new ProcessHelper();
      var taskResult = processHelper.RunExternalProcess(processParms);

      txtOut.Text = taskResult.Message;
    }

    private void InitializeForm()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        a = new a();        
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred during the initialization of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        InitializeTreeViewImageList();

        pnlTvTop.Visible = false;
        pnlTvBottom.Visible = false;

        ctxMnuTreeViewDeleteWCFWebService.Enabled = false;

        _taskProdDbSpec = g.GetDbSpec("TaskProd");
        _taskTestDbSpec = g.GetDbSpec("TaskTest"); 

        int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
        int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);
        _logger = new Logger();
        _logger.Log("Program AppDomain Workbench is starting up.");

        _serviceEnvironmentSetDB = GetServiceEnvironmentSet();

        _serviceEnvironmentSet = _serviceEnvironmentSetDB;

        //string serviceSpecPath = g.ImportsPath + @"\ServiceSpec.xml";
        //if (!File.Exists(serviceSpecPath))
        //  throw new Exception("The ServiceSpec.xml file is missing from the application Imports folder.");

        //var serviceSpecXml = XElement.Parse(File.ReadAllText(serviceSpecPath));
        //using (var f = new ObjectFactory2())
        //{
        //  _serviceEnvironmentSet = f.Deserialize(serviceSpecXml) as ServiceEnvironmentSet;
        //}

        LoadTreeView();

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred during application initialization." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }
    }


    private void InitializeTreeViewImageList()
    {
      try
      {
        imgListTreeView.Images.Clear();
        imgListTreeView.ImageSize = new Size(16, 16);


        var resourceManager = new ResourceManager("Org.SvcManager.Properties.Resources", Assembly.GetExecutingAssembly());

        var org = (Icon)resourceManager.GetObject("org");
        imgListTreeView.Images.Add("org", org);
        var orgSelected = (Icon)resourceManager.GetObject("org");
        imgListTreeView.Images.Add("orgSelected", orgSelected);

        var environment = (Icon)resourceManager.GetObject("environment");
        imgListTreeView.Images.Add("environment", environment);
        var environmentSelected = (Icon)resourceManager.GetObject("environment");
        imgListTreeView.Images.Add("environmentSelected", environmentSelected);

        var host = (Icon)resourceManager.GetObject("host");
        imgListTreeView.Images.Add("host", host);
        var hostSelected = (Icon)resourceManager.GetObject("host");
        imgListTreeView.Images.Add("hostSelected", hostSelected);

        var winsvc = (Icon)resourceManager.GetObject("winsvc");
        imgListTreeView.Images.Add("winsvc", winsvc);
        var winsvcSelected = (Icon)resourceManager.GetObject("winsvc");
        imgListTreeView.Images.Add("winsvcSelected", winsvcSelected);

        var websvc = (Icon)resourceManager.GetObject("websvc");
        imgListTreeView.Images.Add("websvc", websvc);
        var websvcSelected = (Icon)resourceManager.GetObject("websvc");
        imgListTreeView.Images.Add("websvcSelected", websvcSelected);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to initialize the image list for the tree view.", ex);
      }
    }

    private void tvMain_Click(object sender, EventArgs e)
    {
      string typeName = sender.GetType().Name;
      var mouseEventArgs = e as MouseEventArgs;
      if (mouseEventArgs == null)
        return;

      var hitText = tvMain.HitTest(mouseEventArgs.Location);

      if (hitText.Node == null)
        return;

      tvMain.SelectedNode = hitText.Node;
    }

    private void ctxMenuTreeView_Opening(object sender, CancelEventArgs e)
    {
      try
      {
        if (tvMain.SelectedNode == null || tvMain.SelectedNode.Tag == null)
        {
          e.Cancel = true;
          return;
        }

        ctxMnuTreeViewManageHosts.Visible = false;
        ctxMnuTreeViewManageTaskAssignments.Visible = false;
        ctxMnuTreeViewAddWCFWebService.Visible = false;
        ctxMnuTreeViewAddWebSite.Visible = false;
        ctxMnuTreeViewAddWindowsService.Visible = false;
        ctxMnuTreeViewAddWCFWebService.Visible = false;
        ctxMnuTreeViewDeleteWCFWebService.Visible = false;

        DetermineSelection();

        var tag = tvMain.SelectedNode.Tag;
        string objectType = tag.GetType().Name;

        switch (objectType)
        {
          case "String":

            string serviceType = tag.ToString();
            switch (serviceType)
            {
              case "Windows Services":
                _selectedServiceType = ServiceType.WindowsService;
                ctxMnuTreeViewAddWindowsService.Visible = true;
                break;

              case "WCF Web Services":
                _selectedServiceType = ServiceType.WCFWebService;
                ctxMnuTreeViewAddWCFWebService.Text = "Add WCF Web Service";
                ctxMnuTreeViewAddWCFWebService.Tag = "AddWCFWebService";
                ctxMnuTreeViewAddWCFWebService.Visible = true;
                break;

              case "Web Sites":
                _selectedServiceType = ServiceType.WebSite;
                ctxMnuTreeViewAddWebSite.Visible = true;
                break;

              default:
                e.Cancel = true;
                return;
            }
            break;

          case "ServiceEnvironment":
            ctxMnuTreeViewManageHosts.Visible = true;
            _selectedEnvironment = (ServiceEnvironment)tag;
            break;

          case "ServiceSpec":
            _selectedServiceSpec = (ServiceSpec)tag;
            _selectedServiceType = _selectedServiceSpec.ServiceType;
            _selectedEnvironment = _selectedServiceSpec.ServiceEnvironment;

            switch (_selectedServiceType)
            {
              case ServiceType.WindowsService:
                ctxMnuTreeViewManageTaskAssignments.Visible = true;
                if (_selectedServiceSpec.ServiceSpecSet.Count == 0)
                {
                  ctxMnuTreeViewAddWCFWebService.Visible = true;
                  ctxMnuTreeViewAddWCFWebService.Text = "Add WCF Web Service";
                  ctxMnuTreeViewAddWCFWebService.Tag = "AddWCFWebService";
                }
                break;

              case ServiceType.WCFWebService:
                ctxMnuTreeViewAddWCFWebService.Visible = true;
                ctxMnuTreeViewAddWCFWebService.Text = "Update WCF Web Service";
                ctxMnuTreeViewAddWCFWebService.Tag = "UpdateWCFWebService";
                ctxMnuTreeViewDeleteWCFWebService.Visible = true;
                break;

              case ServiceType.WebSite:
                e.Cancel = true;
                return;
            }
            break;

          default:
            e.Cancel = true;
            return;
        }

        _taskDbSpec = _selectedEnvironment.Name == "Prod" ? _taskProdDbSpec : _taskTestDbSpec;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception has occurred while processing the TreeView selection change." + ex.ToReport(),
                        g.AppInfo.AppName + " - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        e.Cancel = true;
      }
    }

    private void DetermineSelection()
    {
      _selectedEnvironment = null;
      _selectedServiceHost = null;
      _selectedServiceType = ServiceType.Unidentified;
      _selectedServiceSpec = null;
      _selectedWCFHostingModel = WCFHostingModel.NotSet;
      _selectedParentServiceSpec = null;

      if (tvMain.SelectedNode == null)
        return;

      var node = tvMain.SelectedNode;

      while (node.Parent != null)
      {
        string objectType = node.Tag.GetType().Name;
        switch (objectType)
        {
          case "ServiceEnvironment":
            _selectedEnvironment = (ServiceEnvironment)node.Tag;
            break;

          case "ServiceHost":
            _selectedServiceHost = (ServiceHost) node.Tag;
            break;

          case "ServiceSpec":
            ServiceSpec serviceSpec = (ServiceSpec)node.Tag;
            if (_selectedServiceSpec == null)
            {
              _selectedServiceSpec = serviceSpec;
              _selectedServiceType = _selectedServiceSpec.ServiceType;
              if (_selectedServiceType == ServiceType.WCFWebService)
                _selectedWCFHostingModel = _selectedServiceSpec.ParentServiceID.HasValue ? WCFHostingModel.HostedInWindowsService : WCFHostingModel.IIS;
            }
            else
            {
              _selectedParentServiceSpec = serviceSpec;
            }
            break;
        }

        node = node.Parent;
      }
    }
  }


  public static class XM
  {
    public static bool IsEquivalent(this XElement xml, XElement xmlCompare)
    {

      // Elements also have values in some cases, though we don't use them much.  Any values that "are" the child elements 
      // will be taken care of below.  But some Elements have "text based values" such as in  <AnElement>ElementValue</AnExement>.
      // When no "text-based" value exists, the "value" of the XElement will be a blank string.
      if (xml.Value != xmlCompare.Value)
        return false;

      // It makes sense to take care of the attibutes first
      foreach (XAttribute attr in xml.Attributes())
      {
        // if the attribute doesn't exist, return false
        if (xmlCompare.Attribute(attr.Name) == null)
          return false;

        var compareAttr = xmlCompare.Attribute(attr.Name);
        if (attr.Value != compareAttr.Value)
          return false;
      }
      
      // Now we loop through the child elements and find out of the xmlCompare "parent" element also
      // contains the like-named child.  If it does not, we return false, if it does, we simply return the
      // result of any lower level recursions based on the child elements compares.  Grandchildren, great-grandchildren, etc.
      // will all be taken care of via recursion.
      foreach (XElement e in xml.Elements())
      {
        if (xmlCompare.Element(e.Name) == null)
          return false;

        XElement compareElement = xmlCompare.Element(e.Name);
        return e.IsEquivalent(compareElement);
      }

      return true;
    }
  }
}

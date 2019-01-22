using System;
using System.Collections.Generic;
using System.Resources;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS.AppDomainManagement;
using Org.TP.Concrete;
using Org.TP;
using Org.GS.Logging;
using Org.GS.PlugIn;
using Org.GS;

namespace AppDomainWorkbench
{
  public partial class frmMain : Form
  {
    private a a;
    private Logger _logger;
    private AppDomainEventManager _eventMgr;

    private AppDomainSupervisor _appDomainSupervisor;
    private Dictionary<string, string> _taskConfiguration;

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
        case "IdentifyPlugIn":
          IdentifyPlugIn();
          break;

        case "RunTask":
          RunTask();
          break;

        case "ClearDisplay":
          txtMain.Text = String.Empty;
          Application.DoEvents();
          break;

        case "Exit":
          _logger.Log("Program AppDomainWorkbench is closing.");
          this.Close();
          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void CreateAppDomain()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        string appDomainName = "DummyPlugIn";
        var appDomainSetup = new AppDomainSetup();
        appDomainSetup.ApplicationBase = g.MEFCatalog + @"\PlugIns\1.0.0.0";
        string objectAssemblyName = "Org.PlugIn";
        string objectTypeName = "Org.PlugIn.DummyPlugIn";
        var appDomainObjectDescriptor = new AppDomainObjectDescriptor(appDomainName, "DummyPlugIn_1.0.0.0", objectAssemblyName, objectTypeName, appDomainSetup);
        _appDomainSupervisor.CreateAppDomain(appDomainObjectDescriptor);
        LoadTreeView(appDomainObjectDescriptor.AppDomainFriendlyName);
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to create a new AppDomain." + ex.ToReport(),
                        "AppDomainWorkbench - Error Creating New AppDomain", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private async void IdentifyPlugIn()
    {
      try
      {
        if (tvMain.SelectedNode == null || tvMain.SelectedNode.Tag == null)
          return;
        
        var objDesc = tvMain.SelectedNode.Tag as AppDomainObjectDescriptor;
        if (objDesc == null)
          return;

        var plugIn = (ITaskProcessor)objDesc.ObjectHandle;

        txtMain.Text = plugIn.Identify();
        Application.DoEvents();
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to get the plug-in to identify itself." + ex.ToReport(),
                        "AppDomain Workbench - Error Running Plug-In Identify", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private async void RunTask()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        if (cboTasks.Text.IsBlank())
          return;
        
        string taskToRun = _taskConfiguration[cboTasks.Text];

        string[] tokens = taskToRun.Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);

        string appDomainName = tokens[0];
        string catalogEntry = tokens[1];
        string assemblyName = tokens[2];
        string objectTypeName = tokens[3];
        string objectRegistryName = tokens[4];

        var taskRequest = BuildTaskRequest(); 
     
        var appDomainSetUp = new AppDomainSetup();
        appDomainSetUp.ApplicationBase = g.CI("$COMPONENTCATALOG$") + @"\" + catalogEntry + @"\1.0.0.0";
        var appDomainObjectDescriptor = new AppDomainObjectDescriptor(appDomainName, objectRegistryName, assemblyName, objectTypeName, appDomainSetUp);

        ITaskProcessor taskProcessor = (ITaskProcessor)_appDomainSupervisor.GetObject(appDomainObjectDescriptor);

        if (taskProcessor == null)
        {
          this.Cursor = Cursors.Default;
          MessageBox.Show("No IPlugIn object was created from the AppDomain '" + appDomainName + "'.",
                          "AppDomain Workbench - PlugIn Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        taskProcessor.NotifyMessage += _eventMgr.PlugIn_NotifyMessage;
        taskProcessor.ProgressUpdate += _eventMgr.PlugIn_ProgressUpdate;

        string result = String.Empty;

        Task.Run(async () => ProcessTask(taskProcessor, taskRequest));

        txtMain.Text = "Task is processing";
        Application.DoEvents();
;


        LoadTreeView();

        // this was working but not handling the "Task" across boundary
        //txtMain.Text = await plugIn.ProcessTask(); 


        cboTasks.SelectedIndex = -1;

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to execute the plug-in." + ex.ToReport(),
                        "AppDomain Workbench - Error Executing Plug-In", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void PlugIn_ProgressUpdate(ProgressMessage progressMessage)
    {
      txtMain.Invoke((Action)((() =>
      {
        txtMain.Text += progressMessage.ActvityProgress + g.crlf;
      })));
    }

    private void PlugIn_NotifyMessage(NotifyMessage notifyMessage)
    {
      txtMain.Invoke((Action)((() =>
      {
        txtMain.Text += notifyMessage.Message + g.crlf;
      })));
    }

    private async void ProcessTask(ITaskProcessor taskProcessor, TaskRequest taskRequest)
    {
      try
      {
        taskProcessor.TaskRequest = taskRequest;
        var taskResult = taskProcessor.ProcessTask();
        await Task.Run(() =>
        {
          txtMain.Invoke((Action)((() =>
          {
            txtMain.Text += taskResult.Message;
          })));

        });
      }
      catch (Exception ex)
      {
        txtMain.Invoke((Action)((() =>
        {
          this.Cursor = Cursors.Default;
          MessageBox.Show("An exception occurred while attempting to execute the task [provide task info...]." + g.crlf2 + ex.ToReport(),
                          "AppDomain Manager - Error Running Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
        })));
      }
    }
    
    private TaskRequest BuildTaskRequest()
    {
      TaskRequest taskRequest = new TaskRequest(0, "Dummy", "DummyPlugIn", "1.0.0.0", 1, TaskRequestType.ScheduledTask,
        new ParmSet() { new Parm("Key1", "Value1") }, DateTime.Now, false, false, (int?)null, (int?)null, false);

      return taskRequest;
    }
    
    private void LoadTreeView(string selectedName = "")
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        tvMain.Nodes.Clear();

        if (_appDomainSupervisor.RootAppDomain == null)
          return;

        string rootName = _appDomainSupervisor.RootAppDomain.FriendlyName;

        TreeNode rootNode = new TreeNode(rootName, 0, 1);
        rootNode.Tag = _appDomainSupervisor.RootAppDomain;
        tvMain.Nodes.Add(rootNode);

        foreach (var childAppDomainInfo in _appDomainSupervisor.RootAppDomain.AppDomainInfoSet.Values)
        {
          LoadTreeView(childAppDomainInfo, rootNode, 1);
        }

        tvMain.ExpandAll();

        if (selectedName.IsNotBlank())
          SelectTreeViewNode(tvMain.Nodes[0], selectedName);

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to load the AppDomainInfo objects to the TreeView." + g.crlf2 + ex.ToReport(),
                        "AppDomain Manager - TreeView Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadTreeView(AppDomainInfo appDomainInfo, TreeNode parentNode, int level)
    {
      var treeNode = new TreeNode(appDomainInfo.FriendlyName, 0, 1);
      treeNode.Tag = appDomainInfo;
      parentNode.Nodes.Add(treeNode);

      if (level == 1)
      {
        foreach (var registeredObject in appDomainInfo.RegisteredObjects)
        {
          var regObjectNode = new TreeNode(registeredObject.Value.ObjectRegistryName, 2, 3);
          regObjectNode.Tag = registeredObject.Value;
          treeNode.Nodes.Add(regObjectNode); 
        }
      }

      foreach (var childAppDomainInfo in appDomainInfo.AppDomainInfoSet.Values)
      {
        LoadTreeView(childAppDomainInfo, treeNode, level + 1);
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

    private void InitializeForm()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the creation of the application object (a)." + g.crlf2 + ex.ToReport(),
                        "AppDomain Manager - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
        int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);
        _logger = new Logger();
        _logger.Log("Program AppDomain Workbench is starting up.");


        _appDomainSupervisor = new AppDomainSupervisor();
        _eventMgr = new AppDomainEventManager();
        _eventMgr.NotifyMessage += PlugIn_NotifyMessage;
        _eventMgr.ProgressUpdate += PlugIn_ProgressUpdate;

        _taskConfiguration = g.GetDictionary("TaskConfiguration");
        cboTasks.Items.Clear();
        cboTasks.LoadItems(_taskConfiguration.Keys.ToList(), true);

        btnRunTask.Enabled = false;
        btnIdentifyPlugIn.Enabled = false;

        InitializeTreeViewImageList();
        LoadTreeView();

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
                        "AppDomain Manager - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    
    private void InitializeTreeViewImageList()
    {
      try
      {
        imgListTreeView.Images.Clear();
        imgListTreeView.ImageSize = new Size(16, 16);


        var resourceManager = new ResourceManager("AppDomainWorkbench.Properties.Resources", Assembly.GetExecutingAssembly());

        var appDomain = (Image)resourceManager.GetObject("AppDomain");
        imgListTreeView.Images.Add("AppDomain", appDomain);
        var appDomainSelected = (Image)resourceManager.GetObject("AppDomain");
        imgListTreeView.Images.Add("AppDomainSelected", appDomainSelected);
        var component = (Image)resourceManager.GetObject("Component");
        imgListTreeView.Images.Add("Component", component);
        var componentSelected = (Image)resourceManager.GetObject("Component");
        imgListTreeView.Images.Add("ComponentSelected", componentSelected);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to initialize the image list for the tree view.", ex);
      }
    }

    private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
    {
      try
      {
        if (e.Node == null || e.Node.Tag == null)
        {
          txtMain.Text = String.Empty;
          return;
        }

        var adInfo = e.Node.Tag as AppDomainInfo;
        var objDesc = e.Node.Tag as AppDomainObjectDescriptor;

        btnIdentifyPlugIn.Enabled = objDesc != null;

        if (adInfo == null && objDesc == null)
        {
          txtMain.Text = String.Empty;
          return;
        }

        if (adInfo != null)
        {
          if (adInfo.AppDomain.IsDefaultAppDomain())
          {
            txtMain.Text = adInfo.Report;
          }
          else
          {
            var appDomainUtility = (IAppDomainUtility)(adInfo.AppDomain.CreateInstanceAndUnwrap("Org.GS", "Org.GS.AppDomainManagement.AppDomainUtility"));

            txtMain.Text = appDomainUtility.GetAssemblyReport();
          }
        }

        if (objDesc != null)
        {
          var appDomainUtility = (IAppDomainUtility)(objDesc.AppDomain.CreateInstanceAndUnwrap("Org.GS", "Org.GS.AppDomainManagement.AppDomainUtility"));
          txtMain.Text = "Component Name    : " + objDesc.ObjectRegistryName + " running in AppDomain:" + g.crlf2 + appDomainUtility.GetAssemblyReport(); 
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred during program TreeView selection change handler and the " +
                        "subsequent attempt to build the AppDomain assembly report." + g.crlf2 + ex.ToReport(), 
                        "AppDomain Manager - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

      }
    }

    private void cboTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnRunTask.Enabled = cboTasks.Text.IsNotBlank();
    }
  }
}

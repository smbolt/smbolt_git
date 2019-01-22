using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using TPL = System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Controls;

namespace Org.SvnHelper
{
  public partial class frmMain : Form
  {
    private a a;
    private bool _continueProcess = true;
    private bool isInitialShowing = true;
    private bool _initializationComplete = false;
    private Size _normalizedSize = new Size(900, 600);
    private bool _hideSyncTab = false;

    private SvnResults _svnResults; 

    private Task svnUpdateTask;
    private Task svnCommitTask;
    private CancellationTokenSource svnUpdateCancellationTokenSource;
    private CancellationTokenSource svnCommitCancellationTokenSource;
    public event Action<string> ProgressNotification;

    private List<string> _svnControlList;
    private List<string> _syncTaskList;
    private Dictionary<string, string> _syncTaskConfigs;
    private string _bcExePath;

    public frmMain()
    {
      InitializeComponent();
      this.isInitialShowing = true;
      InitializeApplication();
      this.ProgressNotification += ProgressNotificationProcessor;
    }

    private void ProgressNotificationProcessor(string message)
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Action)(((() => this.UpdateUI(message)))));
      }
      else
      {
        this.UpdateUI(message);
      }
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "ShowForm":
          this.MakeVisible();
          break;

        case "Exit":
          this.ExitApplication();
          break;
      }
    }

    private void buttonBar_ControlEvent(Org.CTL.ControlEventArgs e)
    {
      lblStatus.Text = e.ControlEventText;

      switch (e.ControlEventText)
      {
        case "Update":
          UpdateSvn();
          break;

        case "Commit":
          CommitSvn();
          break;

        case "SyncRight":
        case "SyncLeft":
          SyncFiles(e.ControlEventText);
          break;

        case "Stop":
          Stop();
          break;

        case "ShowLog":
          ShowLog();
          break;

        case "ClearLog":
          a.ClearLogFile();
          txtOut.Clear();
          break; 

        case "Hide":
          //txtOut.Text = _svnResults.GetResults();
          this.Visible = false;
          break; 
      }
    }

    private void Stop()
    {
      _continueProcess = false; 
    }

    private void UpdateSvn()
    {
      _continueProcess = true;
      _svnResults = new SvnResults();
      notifyIconMain.BalloonTipText = "Running SVN Update";
      notifyIconMain.ShowBalloonTip(2000);
      a.Log("SVN Update process is beginning.");

      var t = new TPL.Task(() => this.UpdateSvnTask());
      t.Start();
    }
        
    private void UpdateSvnTask()
    {
      this.Invoke((Action)(((() => this.Cursor = Cursors.WaitCursor))));

      try
      {
        bool processAllFolders = true;
        if (g.AppConfig.ContainsKey("ProcessAllFolders"))
          processAllFolders = g.AppConfig.GetBoolean("ProcessAllFolders");
        
        int updateInterval = g.GetCI("UpdateInterval").ToInt32OrDefault(1000);

        if (!g.AppConfig.ContainsKey("ProjectPath"))
        {
          ProgressNotification("SVN Update Failed - No 'ProjectPath' specified.");
          a.Log("SVN Update Failed - No 'ProjectPath' specified.");
          return;
        }

        string projectPath = g.AppConfig.GetCI("ProjectPath");

        if (!Directory.Exists(projectPath))
        {
          ProgressNotification("SVN Update Failed - 'ProjectPath' does not exist.");
          a.Log("SVN Update Failed - 'ProjectPath' does not exist.");
          return;
        }

        List<string> projects = new List<string>();

        if (processAllFolders)
        {
          List<string> directoryPaths = Directory.GetDirectories(projectPath).ToList();
          foreach (string directoryPath in directoryPaths)
          {
            string projectName = Path.GetFileName(directoryPath);
            if (_svnControlList.Contains(projectName))
              projects.Add(Path.GetFileName(projectName));
          }
        }

        StringBuilder sbResults = new StringBuilder();
        string statusCommand = "svn status";
        string command = "svn update";

        int projectCount = 0;

        this.Invoke((Action)(((() => progBarMain.Value = 0 ))));
        this.Invoke((Action)(((() => progBarMain.Maximum = projects.Count ))));
        this.Invoke((Action)(((() => progBarMain.Visible = true ))));

        foreach (string projectName in projects)
        {
          if (!_continueProcess)
          {
            ProgressNotification("SvnUpdate processing interrupted.");
            a.Log("SvnUpdate processing interrupted.");
            break;
          }
          string workingCopy = projectPath + @"\" + projectName;

          string fullStatusCommand = statusCommand + " " + workingCopy;
          string statusResult = ProcessHelper.RunCommand(fullStatusCommand);
          Application.DoEvents();

          if (!statusResult.ToLower().Contains("is not a working copy"))
          {
            string cleanupCommand = "svn cleanup " + workingCopy;
            string cleanupResult = ProcessHelper.RunCommand(cleanupCommand);
            Application.DoEvents();

            projectCount++;
            string fullCommand = command + " " + workingCopy;
            string result = ProcessHelper.RunCommand(fullCommand);
            Application.DoEvents();

            this.Invoke((Action)(((() => progBarMain.Value++))));
            ProgressNotification(result);

            a.Log("SVN Update Result: " + result);

            Application.DoEvents();

            System.Threading.Thread.Sleep(updateInterval);
          }
        }

        if (_continueProcess)
        {
          ProgressNotification("SvnUpdate processing complete.");
          a.Log("SvnUpdate processing complete.");
        }

      }
      catch (Exception ex)
      {
        System.Threading.Thread.Sleep(100); 
        ProgressNotification("Exception occurred during SVN Update Process.");
        a.Log("Exception occurred during SVN Update Process." + g.crlf + ex.ToReport());
      }

      this.Invoke((Action)(((() => progBarMain.Visible = false))));
      this.Invoke((Action)(((() => this.Cursor = Cursors.Default))));
    }

    private void CommitSvn()
    {
      _continueProcess = true;
      _svnResults = new SvnResults();
      notifyIconMain.BalloonTipText = "Running SVN Commit";
      notifyIconMain.ShowBalloonTip(2000);
      a.Log("SVN Commit process is beginning.");

      var t = new TPL.Task(() => this.CommitSvnTask());
      t.Start();
    }

    private void CommitSvnTask()
    {
      this.Invoke((Action)(((() => this.Cursor = Cursors.WaitCursor))));

      try
      {
        bool processAllFolders = true;
        if (g.AppConfig.ContainsKey("ProcessAllFolders"))
          processAllFolders = g.AppConfig.GetBoolean("ProcessAllFolders");
        
        int updateInterval = g.GetCI("UpdateInterval").ToInt32OrDefault(1000);

        if (!g.AppConfig.ContainsKey("ProjectPath"))
        {
          ProgressNotification("SVN Commit Failed - No 'ProjectPath' specified.");
          a.Log("SVN Commit Failed - No 'ProjectPath' specified.");
          return;
        }

        string projectPath = g.AppConfig.GetCI("ProjectPath");

        if (!Directory.Exists(projectPath))
        {
          ProgressNotification("SVN Commit Failed - 'ProjectPath' does not exist.");
          a.Log("SVN Commit Failed - 'ProjectPath' does not exist.");
          return;
        }

        List<string> projects = new List<string>();

        if (processAllFolders)
        {
          List<string> directoryPaths = Directory.GetDirectories(projectPath).ToList();
          foreach (string directoryPath in directoryPaths)
          {
              string projectName = Path.GetFileName(directoryPath);
              if (_svnControlList.Contains(projectName))
                  projects.Add(Path.GetFileName(projectName));
          }
        }

        StringBuilder sbResults = new StringBuilder();
        string statusCommand = "svn status";
        string command = "svn commit";

        int projectCount = 0;

        this.Invoke((Action)(((() => progBarMain.Value = 0))));
        this.Invoke((Action)(((() => progBarMain.Maximum = projects.Count))));
        this.Invoke((Action)(((() => progBarMain.Visible = true))));

        foreach (string projectName in projects)
        {
          if (!_continueProcess)
          {
            ProgressNotification("SvnCommit processing interrupted.");
            a.Log("SvnCommit processing interrupted.");
            break;
          }

          string workingCopy = projectPath + @"\" + projectName;

          string fullStatusCommand = statusCommand + " " + workingCopy;
          string statusResult = ProcessHelper.RunCommand(fullStatusCommand);
          Application.DoEvents();

          if (!statusResult.ToLower().Contains("is not a working copy"))
          {
            string cleanupCommand = "svn cleanup " + workingCopy;
            string cleanupResult = ProcessHelper.RunCommand(cleanupCommand);
            Application.DoEvents();

            projectCount++;
            string fullCommand = command + " " + workingCopy + " -m \"Automated commit. \" ";
            string result = ProcessHelper.RunCommand(fullCommand);
            Application.DoEvents();

            this.Invoke((Action)(((() => progBarMain.Value++))));
            ProgressNotification(result);

            a.Log("SVN Commit Result: " + result);

            System.Threading.Thread.Sleep(updateInterval);

            Application.DoEvents();
          }
        }

        if (_continueProcess)
        {
            System.Threading.Thread.Sleep(100); 
            ProgressNotification("SvnCommit processing complete.");
            a.Log("SvnCommit processing complete.");
        }
                
      }
      catch (Exception ex)
      {
        ProgressNotification("Exception occurred during SVN Commit Process.");
        a.Log("Exception occurred during SVN Commit Process." + g.crlf + ex.ToReport());
      }

      this.Invoke((Action)(((() => progBarMain.Visible = false))));
      this.Invoke((Action)(((() => this.Cursor = Cursors.Default))));
    }

    private void SyncFiles(string syncCommand)
    {
      
      _continueProcess = true;
      _svnResults = new SvnResults();
      notifyIconMain.BalloonTipText = "Running Sync: " + syncCommand;
      notifyIconMain.ShowBalloonTip(2000);
      a.Log(syncCommand + " process is beginning.");

      var t = new TPL.Task(() => this.SyncFilesTask(syncCommand));
      t.Start();
    }

    private void SyncFilesTask(string syncCommand)
    {      
      this.Invoke((Action)(((() => this.Cursor = Cursors.WaitCursor))));

      try
      {    
        int updateInterval = g.GetCI("UpdateInterval").ToInt32OrDefault(1000);

        StringBuilder sbResults = new StringBuilder();
        
        int taskCount = 0;
        var syncTaskList = GetSyncTasks();

        this.Invoke((Action)(((() => progBarMain.Value = 0))));
        this.Invoke((Action)(((() => progBarMain.Maximum = syncTaskList.Count))));
        this.Invoke((Action)(((() => progBarMain.Visible = true))));

        foreach (string syncTaskName in syncTaskList)
        {
          if (!_continueProcess)
          {
            ProgressNotification(syncCommand + " processing interrupted.");
            a.Log(syncCommand + " processing interrupted.");
            break;
          }          

          if (!_syncTaskConfigs.ContainsKey(syncTaskName))
          {
            this.Cursor = Cursors.Default;
            MessageBox.Show("SyncConfigs configurations do not include an entry for they task '" + syncTaskName + "'.",
                            "SvnHelper - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }

          string syncConfigName = _syncTaskConfigs[syncTaskName];
          var syncSpec = g.AppConfig.GetSyncSpec(syncConfigName);

          if (!syncSpec.IsValid())
          {
            MessageBox.Show("Syncronization specifications (ConfigSyncSpec) named '" + syncConfigName + "ConfigSyncSpec' does not exist or is invalid.",
                            "SvnHelper - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }

          // Script  Parameters
          // %1 = Left Folder
          // %2 = Right Folder
          // %3 = Direction ( left->right or right->left )
          // %4 = Filters
          // %5 = Log File

          string bcScriptName = syncSpec.ScriptName;
          string bcScriptsPath = g.ImportsPath + @"\" + bcScriptName;
          string syncDirection = syncCommand == "SyncRight" ? "left->right" : "right->left";
          string syncDirectionAbbr = syncCommand == "SyncRight" ? "L2R" : "R2L";
          string logFilePath = g.LogPath + @"\SyncLog\" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + "-" +
                               syncConfigName + "-" + syncDirectionAbbr + ".log";

          ProcessParms processParms = new ProcessParms();
          processParms.ExecutablePath = _bcExePath;
          processParms.Args = new string[] {  
              "/silent",
              "@" + bcScriptsPath,
              syncSpec.LeftFolder,
              syncSpec.RightFolder, 
              syncDirection,
              syncSpec.Filter,
              logFilePath
          };

          var processHelper = new ProcessHelper();
          var taskResult = processHelper.RunExternalProcess(processParms); 
          Application.DoEvents();

          if (taskResult.TaskResultStatus == TaskResultStatus.Success)
          {
            taskCount++;
            this.Invoke((Action)(((() => progBarMain.Value++))));
            ProgressNotification(syncConfigName + "-" + syncDirectionAbbr + " sync task was successful");
            a.Log(syncConfigName + "-" + syncDirectionAbbr + " sync task was successful");

            System.Threading.Thread.Sleep(updateInterval);

            Application.DoEvents();
          }
        }

        if (_continueProcess)
        {
            System.Threading.Thread.Sleep(100); 
            ProgressNotification(syncCommand + " processing complete.");
            a.Log(syncCommand + " processing complete.");
        }                
      }
      catch (Exception ex)
      {
        ProgressNotification("Exception occurred during " + syncCommand + " Process.");
        a.Log("Exception occurred during " + syncCommand + " Process." + g.crlf + ex.ToReport());
      }

      this.Invoke((Action)(((() => progBarMain.Visible = false))));
      this.Invoke((Action)(((() => this.Cursor = Cursors.Default))));
    }

    private List<string> GetSyncTasks()
    {
      List<string> syncTasks = new List<string>();

      foreach(int checkedIndex in lstSyncSelect.CheckedIndices)
      {
        syncTasks.Add(lstSyncSelect.Items[checkedIndex].ToString());
      }

      return syncTasks;
    }

    private void InitializeApplication()
    {
      a = new a();
      //a.ClearLogFile();
      a.Log("SvnHelper starting up.");

      progBarMain.Visible = false;

      buttonBar.ControlEvent += buttonBar_ControlEvent;
      LoadSvnList();

      _hideSyncTab = g.AppConfig.GetBoolean("HideSyncTab");

      _syncTaskConfigs = g.AppConfig.GetDictionary("SyncConfigs"); 

      if (_hideSyncTab)
      {
        tabMain.TabPages.Remove(tabPageSync);
        buttonBar.HideButton("SyncLeft");
        buttonBar.HideButton("SyncRight");
      }
      else
      {
        LoadSyncList();
      }

      tabMain.SelectedIndex = 0;
      buttonBar.HideButton("SyncLeft");
      buttonBar.HideButton("SyncRight");

      _bcExePath = g.GetCI("BCExePath");

      _initializationComplete = true;
    }

    private void LoadSvnList()
    {
      bool processAllFolders = true;
      if (g.AppConfig.ContainsKey("ProcessAllFolders"))
        processAllFolders = g.AppConfig.GetBoolean("ProcessAllFolders");

      string projectPath = g.AppConfig.GetCI("ProjectPath");

      _svnControlList = g.AppConfig.GetList("SvnControl");

      List<string> foldersToProcess = Directory.GetDirectories(projectPath).ToList();

      lstSvnSelect.Items.Clear();

      foreach (string folderToProcess in foldersToProcess)
      {
        string folderName = Path.GetFileName(folderToProcess);
        lstSvnSelect.Items.Add(folderName);
        if (_svnControlList.Contains(folderName))
          lstSvnSelect.SetItemChecked(lstSvnSelect.Items.Count - 1, true); 
      }
    }

    private void LoadSyncList()
    {
      _syncTaskList = g.AppConfig.GetList("SyncControl");
      
      lstSyncSelect.Items.Clear();

      foreach (string syncTask in _syncTaskList)
      {
        lstSyncSelect.Items.Add(syncTask);
        lstSyncSelect.SetItemChecked(lstSyncSelect.Items.Count - 1, true); 
      }
    }

    private void ExitApplication()
    {
      this.Close();
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      notifyIconMain.Icon = SystemIcons.Application;
      notifyIconMain.BalloonTipText = "Started up";
      notifyIconMain.ShowBalloonTip(2000);

      this.Size = _normalizedSize;
      if (!System.Diagnostics.Debugger.IsAttached)
      {
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Size = new Size(0, 0);
        this.Visible = false;
      }
    }


    private void notifyIconMain_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      MessageBox.Show("Doing something important on double-click...");
      // Then, hide the icon.
      notifyIconMain.Visible = false;

    }

    private void mnuFileHide_Click(object sender, EventArgs e)
    {
      this.Visible = false;
    }

    private void notifyIconMain_BalloonTipClicked(object sender, EventArgs e)
    {
      MakeVisible();
    }

    private void MakeVisible()
    {
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
      this.Size = _normalizedSize;
      this.Visible = true;
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (this.isInitialShowing)
      {
        if (!System.Diagnostics.Debugger.IsAttached)
        {
          this.isInitialShowing = false;
          this.Visible = false;
        }
      }
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      notifyIconMain.BalloonTipText = "SvnHelper Exiting";
      notifyIconMain.ShowBalloonTip(2000);
      Application.DoEvents();
      a.Log("SvnHelper shutting down.");
    }

    private void ShowLog()
    {
      string logPath = a.LogFilePath;

      if (logPath.IsBlank())
      {
        txtOut.Text = "Log file path is blank.";
        return;
      }

      if (!File.Exists(logPath))
      {
        txtOut.Text = "Log file does not exist at '" + logPath + "'.";
        return;
      }

      txtOut.Text = File.ReadAllText(logPath);
    }
        

    private void SentNotifyIconMessage(string message)
    {

    }

    private void UpdateUI(string message)
    {
      _svnResults.AddResult(message);

      txtOut.Text = _svnResults.GetResults();
    }

    private void lstSelect_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (!_initializationComplete)
        return;

      string listItem = lstSvnSelect.Items[e.Index].ToString();

      bool isChecked = false;
      if (e.NewValue == CheckState.Checked)
        isChecked = true;

      bool listUpdated = false;
      if (isChecked)
      {
        if (!_svnControlList.Contains(listItem))
        {
          _svnControlList.Add(listItem);
          listUpdated = true;
        }
      }
      else
      {
        if (_svnControlList.Contains(listItem))
        {
          _svnControlList.Remove(listItem);
          listUpdated = true;
        }
      }

      if (listUpdated)
      {
        g.AppConfig.UpdateList("SvnControl", _svnControlList);
        g.AppConfig.Save();
      }
    }

    private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (tabMain.SelectedTab == null)
        return;

      string tag = tabMain.SelectedTab.Tag.ObjectToTrimmedString();
      switch (tag)
      {
        case "SVN":
          buttonBar.ShowButton("Update");
          buttonBar.ShowButton("Commit");
          buttonBar.ShowButton("Stop");
          buttonBar.HideButton("SyncLeft");
          buttonBar.HideButton("SyncRight");
          break;

        case "SYNC":
          buttonBar.ShowButton("SyncLeft");
          buttonBar.ShowButton("SyncRight");
          buttonBar.HideButton("Update");
          buttonBar.HideButton("Commit");
          buttonBar.HideButton("Stop");
          break;
      }
    }

    private void lstSyncSelect_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (!_initializationComplete)
        return;

      string syncTaskName = lstSyncSelect.Items[e.Index].ToString();

      if (!_syncTaskConfigs.ContainsKey(syncTaskName))
      {
        MessageBox.Show("SyncConfigs configurations do not include an entry for they task '" + syncTaskName + "'.",
                        "SvnHelper - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      string syncConfigName = _syncTaskConfigs[syncTaskName];
      var syncSpec = g.AppConfig.GetSyncSpec(syncConfigName);

      if (!syncSpec.IsValid())
      {
        MessageBox.Show("Syncronization specifications (ConfigSyncSpec) named '" + syncConfigName + "ConfigSyncSpec' does not exist or is invalid.", 
                        "SvnHelper - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      bool active = false;
      if (e.NewValue == CheckState.Checked)
        active = true;

      if (syncSpec.Active == active)
        return;

      syncSpec.Active = active;

      g.AppConfig.UpdateCO<ConfigSyncSpec>(syncSpec);  
      g.AppConfig.Save();
    }
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS.Logging;
using Org.GS;

namespace DevOpsWorkbench
{
  public partial class frmMain : Form
  {
    private RunAction _runAction;
    private string _streamName;
    private string _scriptName;

    private OSFolder _scriptsFolder;
    private bool _scriptFileLoaded;
    private string _scriptsPath;
    private string _scriptFullPath;
    private string _scriptText;
    private bool _scriptInProgress;
    private Dictionary<string, string> _scriptsDictionary;
    private Logger _logger;

    private string _outputText;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      switch (sender.ActionTag())
      {
        case "Run":
          RunStream();
          break;

        case "SaveScript":
          SaveScript();
          break;

        case "ClearDisplay":
          txtOutput.Text = String.Empty;
          Application.DoEvents();
          break;

        case "RefreshTreeView":
          RefreshTreeView();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void LoadScriptFile()
    {
      if (!File.Exists(_scriptFullPath))
      {
        MessageBox.Show("The script file was not found at '" + _scriptFullPath + "'.",
                        "DevOpsWorkbench - Script File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }


      _scriptText = File.ReadAllText(_scriptFullPath);
      txtScript.Text = _scriptText;
      _scriptFileLoaded = true;
      tabMain.SelectedTab = tabPageScript;
      lblScriptFilePath.Text = _scriptFullPath;
      CheckForScriptChanges();
    }

    private void LoadStream()
    {
      var sb = new StringBuilder();

      var streamScripts = GetStreamScripts();

      sb.Append("SCRIPTS FOR JOB STREAM '" + _streamName + "'." + g.crlf2);

      foreach (var scriptFile in streamScripts)
      {
        sb.Append("Script file: " + scriptFile + g.crlf2);
        sb.Append(File.ReadAllText(scriptFile) + g.crlf2);
      }

      txtScript.Text = sb.ToString();
      tabMain.SelectedTab = tabPageScript;
    }

    private void SaveScript()
    {
      try
      {
        _scriptText = txtScript.Text;
        File.WriteAllText(_scriptFullPath, txtScript.Text);
        CheckForScriptChanges();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception has occurred while attempting to save the script." + g.crlf2 + ex.ToReport(),
                        "DevOpsWorkbench - Error Saving Script", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private async void RunStream()
    {
      System.Threading.Tasks.Task displayTask = null;

      try
      {
        this.Cursor = Cursors.WaitCursor;

        var streamScripts = GetStreamScripts();

        string taskName = _runAction == RunAction.RunStream ? _streamName : _scriptName;

        _logger.Log("Running task '" + taskName + "'.");

        TaskResult streamTaskResult = new TaskResult(taskName);

        _outputText = String.Empty;
        txtOutput.Text = String.Empty;
        tabMain.SelectedTab = tabPageOutput;
        Application.DoEvents();

        _scriptInProgress = true;
        displayTask = new System.Threading.Tasks.Task(MonitorDisplay);
        displayTask.Start();

        bool continueStream = true;
        foreach (var streamScript in streamScripts)
        {
          await RunSingleScript(streamScript).ContinueWith((r) =>
          {
            switch (r.Status)
            {
              case TaskStatus.RanToCompletion:
                var taskResult = r.Result as TaskResult;
                switch (taskResult.TaskResultStatus)
                {
                  case TaskResultStatus.Success:
                    streamTaskResult.TaskResultSet.Add(streamTaskResult.TaskResultSet.Count, taskResult);
                    break;

                  default:
                    streamTaskResult.TaskResultSet.Add(streamTaskResult.TaskResultSet.Count, taskResult);
                    break;
                }
                break;

              default:
                var aggregateException = r.Exception;
                foreach (var exception in aggregateException.InnerExceptions)
                {
                  if (exception.Data.Count > 0 && exception.Data["TaskResult"] != null)
                  {
                    streamTaskResult.TaskResultSet.Add(streamTaskResult.TaskResultSet.Count, (TaskResult)exception.Data["TaskResult"]);
                    continueStream = false;
                    break;
                  }
                  else
                  {
                    var errorTaskResult = new TaskResult(_streamName + "_ERROR");
                    errorTaskResult.TaskResultStatus = TaskResultStatus.Failed;
                    errorTaskResult.Exception = aggregateException;
                    streamTaskResult.TaskResultSet.Add(streamTaskResult.TaskResultSet.Count, errorTaskResult);
                    continueStream = false;
                    break;
                  }
                }
                break;
            }

          });

          if (!continueStream)
            break;

          // be sure other threads have caught up...
          System.Threading.Thread.Sleep(1000);
        }

        _scriptInProgress = false;

        streamTaskResult.TaskResultStatus = streamTaskResult.TaskResultSet.GetWorstResult();

        _logger.Log("Running task '" + taskName + "' is complete task result status is '" + streamTaskResult.TaskResultStatus.ToString() + "'.");

        this.Cursor = Cursors.Default;

        switch (streamTaskResult.TaskResultStatus)
        {
          case TaskResultStatus.Success:
            MessageBox.Show("The PowerShell script '" + streamTaskResult.TaskName + "' finished successfully.",
                            "DevOpsWorkbench - PowerShell Script Completed Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            break;

          default:
            MessageBox.Show("The PowerShell script '" + streamTaskResult.TaskName + "' finished with status '" + streamTaskResult.TaskResultStatus.ToString() + "'." + g.crlf +
                            streamTaskResult.Report, "DevOpsWorkbench - PowerShell Script Completed Abnormally", MessageBoxButtons.OK, MessageBoxIcon.Error);
            break;

        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        string errorMessage = "An exception occurred while attempting to execute the script stream named '" + _streamName + "'." + g.crlf + ex.ToReport();
        _logger.Log(errorMessage);
        MessageBox.Show(errorMessage, "DevOpsWorkbench - PowerShell Script Stream Completed Abnormally", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    private async System.Threading.Tasks.Task<TaskResult> RunSingleScript(string scriptPath)
    {
      try
      {
        return await System.Threading.Tasks.Task.Run(() =>
        {
          using (var psEngine = new Org.PS.PsEngine())
          {
            psEngine.PsOutputGenerated += this.PsEngine_PSOutputGenerated;
            psEngine.PsDebugOutputGenerated += this.PsEngine_PSOutputGenerated;
            psEngine.PsWarningOutputGenerated += this.PsEngine_PSOutputGenerated;
            psEngine.PsErrorOutputGenerated += this.PsEngine_PSOutputGenerated;
            psEngine.PsVerboseOutputGenerated += this.PsEngine_PSOutputGenerated;
            psEngine.PsInvocationStateChanged += PsEngine_PSInvocationStateChanged;
            return psEngine.RunScriptAsync(scriptPath);
          }
        });
      }
      catch (Exception ex)
      {
        var exTaskResult = new TaskResult("ScriptExecutionError");
        exTaskResult.Exception = ex;
        exTaskResult.Message = "An error occurrred while executing the script.";
        exTaskResult.TaskResultStatus = TaskResultStatus.Failed;
        return exTaskResult;
      }
    }

    private List<string> GetStreamScripts()
    {
      var streamScripts = new List<string>();

      if (_runAction == RunAction.RunScript)
      {
        streamScripts.Add(_scriptFullPath);
        return streamScripts;
      }

      var streamNode = tvMain.SelectedNode;
      foreach (TreeNode scriptNode in streamNode.Nodes)
      {
        if (scriptNode.Tag != null && !scriptNode.Text.ToUpper().StartsWith("STREAM-"))
        {
          streamScripts.Add(scriptNode.Tag.ToString().Trim());
        }
      }

      return streamScripts;
    }

    private void PsEngine_PSInvocationStateChanged(string message)
    {
      WriteToDisplay(message);
    }

    private void PsEngine_PSOutputGenerated(string message)
    {
      WriteToDisplay(message);
    }

    private void MonitorDisplay()
    {
      while (_scriptInProgress)
      {
        System.Threading.Thread.Sleep(250);

        if (_outputText.Length > 0)
        {
          if (Monitor.TryEnter(DisplayWriteLockObject, 1000))
          {
            try
            {
              txtOutput.Invoke((Action)((() =>
              {
                txtOutput.Text += _outputText;
                txtOutput.SelectionStart = 0;
                txtOutput.SelectionLength = 0;
                txtOutput.Navigate(txtOutput.Lines.Count - 1);
                _outputText = String.Empty;
                Application.DoEvents();
              })));
            }
            catch (Exception ex)
            {
            }
            finally
            {
              Monitor.Exit(DisplayWriteLockObject);
            }
          }
        }
      }
    }

    private object DisplayWriteLockObject = new object();
    private void WriteToDisplay(string message)
    {
      if (Monitor.TryEnter(DisplayWriteLockObject, 1000))
      {
        try
        {
          _outputText += message + g.crlf;
        }
        catch (Exception ex)
        {
          MessageBox.Show("An exception has occurred while attempting to update the display." + g.crlf2 + ex.ToReport(),
                          "DevOpsWorkbench - Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
          Monitor.Exit(DisplayWriteLockObject);
        }
      }
    }

    private void RefreshTreeView()
    {
      BuildScriptFolder();
      LoadTreeView();
    }

    private void LoadTreeView()
    {
      try
      {
        tvMain.ItemHeight = 18;

        tvMain.Nodes.Clear();

        TreeNode rootNode = new TreeNode("Scripts");
        rootNode.Tag = "Scripts";
        rootNode.ImageKey = "folder";
        rootNode.SelectedImageKey = "folder";
        tvMain.Nodes.Add(rootNode);

        foreach (var folder in _scriptsFolder.OSFolderSet)
        {
          LoadTreeView(rootNode, folder);
        }

        tvMain.ExpandAll();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to load script files to the TreeView." + g.crlf2 + ex.ToReport(),
                        "DevOpsWorkbench - File Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadTreeView(TreeNode node, OSFolder folder)
    {
      string folderName = folder.FolderName;

      var folderNode = new TreeNode(folderName);
      if (folderName.ToUpper().StartsWith("STREAM-"))
        folderNode.Tag = folder.FullPath;
      folderNode.ImageKey = "folder";
      folderNode.SelectedImageKey = "folder";
      node.Nodes.Add(folderNode);

      foreach (var file in folder.OSFileSet.Values)
      {
        string scriptName = Path.GetFileNameWithoutExtension(file.FileName);

        var fileNode = new TreeNode(scriptName);
        fileNode.ImageKey = "script";
        fileNode.SelectedImageKey = "script";
        fileNode.Tag = file.FullPath;
        folderNode.Nodes.Add(fileNode);
      }

      foreach (var childFolder in folder.OSFolderSet)
      {
        LoadTreeView(folderNode, childFolder);
      }
    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the creation of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "DevOpsWorkbench - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {
        _logger = new Logger();
        this.SetInitialSizeAndLocation();

        _runAction = RunAction.None;

        imgListTreeView.Images.Add("folder", DevOpsWorkbench.Properties.Resources.folder);
        imgListTreeView.Images.Add("script", DevOpsWorkbench.Properties.Resources.script);

        tabMain.SelectedTab = tabPageScript;
        _outputText = String.Empty;

        lblScriptFilePath.Text = String.Empty;
        _scriptFileLoaded = false;
        _scriptsPath = g.CI("ScriptsPath");

        RefreshTreeView();

        btnRun.Enabled = false;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application." + g.crlf2 + ex.ToReport(),
                        "DevOpsWorkbench - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void BuildScriptFolder()
    {
      try
      {
        var searchParms = new SearchParms();
        searchParms.RootPath = _scriptsPath;
        searchParms.Extensions = new List<string> { "ps1" };

        _scriptsFolder = new OSFolder();
        _scriptsFolder.FullPath = _scriptsPath;
        _scriptsFolder.RootFolderPath = _scriptsPath;
        _scriptsFolder.IsRootFolder = true;
        _scriptsFolder.DepthFromRoot = 0;

        _scriptsFolder.BuildFolderAndFileList();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to build a file list from the path '" + _scriptsPath + "'.", ex);
      }
    }

    private bool CheckForScriptChanges()
    {
      if (!_scriptFileLoaded)
      {
        btnSaveScript.Enabled = false;
        return false;
      }
      else
      {
        if (txtScript.Text != _scriptText)
        {
          btnSaveScript.Enabled = true;
          return true;
        }
        else
        {
          btnSaveScript.Enabled = false;
          return false;
        }
      }
    }

    private void txtOut_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
    {
      CheckForScriptChanges();
    }

    private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (tvMain.SelectedNode?.Tag == null)
      {
        _runAction = RunAction.None;
        _scriptFullPath = String.Empty;
        txtOutput.Text = String.Empty;
        txtScript.Text = String.Empty;
        btnRun.Enabled = false;
      }
      else
      {
        _scriptFullPath = tvMain.SelectedNode.Tag.ToString();
        string fileName = Path.GetFileName(_scriptFullPath);
        if (fileName.StartsWith("STREAM-"))
        {
          _streamName = fileName.ToUpper().Trim();
          _runAction = RunAction.RunStream;
          btnRun.Enabled = true;
          LoadStream();
        }
        else
        {
          _scriptName = Path.GetFileNameWithoutExtension(fileName);
          _runAction = RunAction.RunScript;
          LoadScriptFile();
          btnRun.Enabled = true;
        }
      }
    }

    private void tvMain_MouseDown(object sender, MouseEventArgs e)
    {
      var hitTest = tvMain.HitTest(e.X, e.Y);

      if (hitTest.Node != null)
        tvMain.SelectedNode = hitTest.Node;

      tabMain.SelectedTab = tabPageScript;
    }

    private void frmMain_KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.S:
          if (e.Control)
          {
            if (CheckForScriptChanges())
              SaveScript();
          }
          break;

        case Keys.R:
          if (e.Control)
          {
            if (tabMain.SelectedTab == tabPageScript)
            {
              RunStream();
            }
          }
          break;
      }
    }
  }
}

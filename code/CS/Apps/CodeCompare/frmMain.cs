using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using fctb = FastColoredTextBoxNS; 
using Org.GS;

namespace Org.CodeCompare
{
  public partial class frmMain : Form
  {
    private bool _isFirstShowing = true;
    private string _fullLeftPath;
    private bool _fullLeftPathValid;
    private string _fullRightPath;
    private bool _fullRightPathValid;
    private bool _pathValidationSuspended;

    private bool _canCompare = false;
    private string _filePath = String.Empty;

    private string _winMergeExePath;

    private Image _copyLeftImage;
    private Image _copyRightImage;
    private Image _compareImage;
    private Image _checkImage;
    private Image _emptyImage;
    private Image _questionMarkImage;
    private string _leftPathImageTooltip;
    private string _rightPathImageTooltip;

    private List<string> _fileSystemStems;

    
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
        case "BrowseLeft":
          BrowseLeft();
          break;

        case "BrowseRight":
          BrowseRight();
          break;

        case "SaveConfig":
          SaveConfig();
          break;

        case "Compare":
          Compare();
          break;

        case "ViewFile":
          ViewFile();
          break;

        case "CompareFiles":
          RunFileCompareUtility();
          break;

        case "CopyRightToLeft":
          ProcessFile(FileSystemCommand.Copy, LeftRightDirection.RightToLeft);
          break;


        case "CopyLeftToRight":
          ProcessFile(FileSystemCommand.Copy, LeftRightDirection.LeftToRight);
          break;

        case "DeleteRight":
          ProcessFile(FileSystemCommand.Delete, LeftRightDirection.Right);
          break;

        case "DeleteLeft":
          ProcessFile(FileSystemCommand.Delete, LeftRightDirection.Left);
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void BrowseLeft()
    {

    }

    private void BrowseRight()
    {

    }


    private void Compare()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        gvResults.Rows.Clear();
        Application.DoEvents();

        //g.AppConfig.ReloadFromFile();

        var fileTypes = cboFileTypes.Text.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();

        var leftSearchParms = new SearchParms();
        leftSearchParms.ProcessChildFolders = true;
        leftSearchParms.RootPath = _fullLeftPath;
        leftSearchParms.FolderNameExcludes = g.AppConfig.GetList("LeftFolderNameExcludes");
        leftSearchParms.FileNameExcludes = g.AppConfig.GetList("FileNameExcludes");
        leftSearchParms.Extensions = fileTypes;

        var rightSearchParms = new SearchParms();
        rightSearchParms.ProcessChildFolders = true;
        rightSearchParms.RootPath = _fullRightPath;
        rightSearchParms.FolderNameExcludes = g.AppConfig.GetList("RightFolderNameExcludes");
        rightSearchParms.FileNameExcludes = g.AppConfig.GetList("FileNameExcludes");
        rightSearchParms.Extensions = fileTypes;

        var leftFolder = new OSFolder(leftSearchParms);
        var rightFolder = new OSFolder(rightSearchParms);
       
        StringBuilder sb = new StringBuilder();

        //Task[] tasks = new Task[2];
        //tasks[0] = Task.Factory.StartNew(() => leftFolder.BuildFolderAndFileList());
        //tasks[1] = Task.Factory.StartNew(() => rightFolder.BuildFolderAndFileList());
        //Task.WaitAll(tasks);

        leftFolder.BuildFolderAndFileList();
        rightFolder.BuildFolderAndFileList();


        foreach(var kvpFile in leftFolder.FileList)
        {
          if (rightFolder.FileList.ContainsKey(kvpFile.Key))
          {
            kvpFile.Value.CompareLastChangeDateTime = rightFolder.FileList[kvpFile.Key].LastChangedDateTime;
            rightFolder.FileList[kvpFile.Key].CompareLastChangeDateTime = kvpFile.Value.LastChangedDateTime;
            
            string leftFile = File.ReadAllText(kvpFile.Value.FullPath); 
            string rightFile = File.ReadAllText(rightFolder.FileList[kvpFile.Key].FullPath);

            if (leftFile == rightFile)
            {
              kvpFile.Value.FileCompareStatus = FileCompareStatus.Identical;
              rightFolder.FileList[kvpFile.Key].FileCompareStatus = FileCompareStatus.Identical;
            }
            else
            {
              if (kvpFile.Value.LastChangedDateTime > kvpFile.Value.CompareLastChangeDateTime)
              {
                kvpFile.Value.FileCompareStatus = FileCompareStatus.OppositeOlder;
                rightFolder.FileList[kvpFile.Key].FileCompareStatus = FileCompareStatus.OppositeNewer;
              }
              else
              {
                kvpFile.Value.FileCompareStatus = FileCompareStatus.OppositeNewer;
                rightFolder.FileList[kvpFile.Key].FileCompareStatus = FileCompareStatus.OppositeOlder;
              }
            }
          }
          else
          {
            kvpFile.Value.FileCompareStatus = FileCompareStatus.OppositeMissing;
          }
        }

        foreach(var kvpFile in rightFolder.FileList.Where(f => f.Value.FileCompareStatus == FileCompareStatus.NotSet))
        {
          kvpFile.Value.FileCompareStatus = FileCompareStatus.OppositeMissing;
        }

        gvResults.SuspendLayout();

        sb.Append("LEFT FOLDER" + g.crlf2); 
        foreach (var file in leftFolder.FileList.Values)
        {
          if (file.FileCompareStatus != FileCompareStatus.Identical)
          {
            gvResults.Rows.Add(new object[] {
              (gvResults.Rows.Count + 1).ToString("00000"),
              "LEFT",
              file.FileName,
              file.LastChangedDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
              file.CompareLastChangeDateTime == DateTime.MinValue ? String.Empty : file.CompareLastChangeDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
              GetActionImage("LEFT", file.FileCompareStatus),
              file.FileCompareStatus != FileCompareStatus.OppositeMissing ? _compareImage : _emptyImage,
              file.FullPath,
              file.FileCompareStatus
            });
          }
        }

        sb.Append(g.crlf2 + "RIGHT FOLDER" + g.crlf2); 

        foreach (var file in rightFolder.FileList.Values.Where(f => f.FileCompareStatus == FileCompareStatus.OppositeMissing))
        {
          if (file.FileCompareStatus != FileCompareStatus.Identical)
          {
            gvResults.Rows.Add(new object[] {
              (gvResults.Rows.Count + 1).ToString("00000"),
              "RIGHT",
              file.FileName,
              file.LastChangedDateTime.ToString(),
              file.CompareLastChangeDateTime == DateTime.MinValue ? String.Empty : file.CompareLastChangeDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
              GetActionImage("RIGHT", file.FileCompareStatus),
              file.FileCompareStatus != FileCompareStatus.OppositeMissing ? _compareImage : _emptyImage,
              file.FullPath,
              file.FileCompareStatus
            });
          }
        }

        gvResults.ResumeLayout(true);

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during compare operation'." + g.crlf2 +
          ex.ToReport(), "Code Compare - Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private Image GetActionImage(string side, FileCompareStatus fileCompareStatus)
    {
      switch (fileCompareStatus)
      {
        case FileCompareStatus.OppositeMissing:
          return side == "LEFT" ? _copyRightImage : _copyLeftImage;

        case FileCompareStatus.OppositeNewer:
          return side == "LEFT" ? _copyLeftImage : _copyRightImage;

        case FileCompareStatus.OppositeOlder:
          return side == "LEFT" ? _copyRightImage : _copyLeftImage;
      }

      return null;
    }

    private void SaveConfig()
    {
      try
      {
        g.AppConfig.Save();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to save the configuration file'." + g.crlf2 +
          ex.ToReport(), g.AppInfo.AppName + " - Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        MessageBox.Show("An exception occurred while attempting to initialize the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "Code Compare - Application (a) Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        lblIdenticalPathWarning.Visible = false;

        _copyLeftImage = Org.CodeCompare.Properties.Resources.LeftArrow;
        _copyRightImage = Org.CodeCompare.Properties.Resources.RightArrow;
        _compareImage = Org.CodeCompare.Properties.Resources.Compare;
        _checkImage = Org.CodeCompare.Properties.Resources.Check;
        _emptyImage = Org.CodeCompare.Properties.Resources.Empty;
        _questionMarkImage = Org.CodeCompare.Properties.Resources.QuestionMark;


        pbLeftPath.Image = _checkImage;
        _leftPathImageTooltip = "Path is valid";
        pbRightPath.Image = _questionMarkImage;
        _rightPathImageTooltip = "Path doesn't exist";

        _fileSystemStems = g.GetList("FileSystemStems");
        cboLeftStem.LoadItems(_fileSystemStems);
        cboLeftStem.SelectItem(g.CI("SelectedLeftStem"));
        cboRightStem.LoadItems(_fileSystemStems);
        cboRightStem.SelectItem(g.CI("SelectedRightStem"));


        //string defaultLeftPath = g.CI("DefaultLeftPath");
        //string defaultRightPath = g.CI("DefaultRightPath");

        string defaultFileTypes = g.CI("DefaultFileTypes"); 

        //var searchPaths = g.AppConfig.GetList("SearchPaths");

        //cboLeftPath.Items.Clear();
        //cboRightPath.Items.Clear();
        //foreach (var searchPath in searchPaths)
        //{
        //  cboLeftPath.Items.Add(searchPath);
        //  cboRightPath.Items.Add(searchPath);
        //}

        //for (int i = 0; i < cboLeftPath.Items.Count; i++)
        //{
        //  if (cboLeftPath.Items[i].ToString() == defaultLeftPath)
        //  {
        //    cboLeftPath.SelectedIndex = i;
        //    break;
        //  }
        //}

        //for (int i = 0; i < cboRightPath.Items.Count; i++)
        //{
        //  if (cboRightPath.Items[i].ToString() == defaultRightPath)
        //  {
        //    cboRightPath.SelectedIndex = i;
        //    break;
        //  }
        //}

        var fileTypes = g.AppConfig.GetList("FileTypes"); 

        cboFileTypes.Items.Clear();
        foreach (var fileType in fileTypes)
          cboFileTypes.Items.Add(fileType);

        for (int i = 0; i < cboFileTypes.Items.Count; i++)
        {
          if (cboFileTypes.Items[i].ToString() == defaultFileTypes)
          {
            cboFileTypes.SelectedIndex = i;
            break;
          }
        }

        _winMergeExePath = g.CI("WinMergeExePath");

        InitializeGrid();


      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred the initialization of the program." + g.crlf2 +
          ex.ToReport(), "Code Compare - Program Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void InitializeGrid()
    {
      gvResults.Columns.Clear();

      DataGridViewColumn col = new DataGridViewTextBoxColumn();
      col.Tag = 8;
      col.Name = "Count";
      col.HeaderText = "Count";
      col.Width = 80;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvResults.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Tag = 8;
      col.Name = "Side";
      col.HeaderText = "Side";
      col.Width = 80;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvResults.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Tag = 20;
      col.Name = "FileName";
      col.HeaderText = "FileName";
      col.Width = 200;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gvResults.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Tag = 152;
      col.Name = "LeftModified";
      col.HeaderText = "Left Mod";
      col.Width = 150;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvResults.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Tag = 15;
      col.Name = "RightModified";
      col.HeaderText = "Right Mod";
      col.Width = 150;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvResults.Columns.Add(col);

      col = new DataGridViewImageColumn();
      col.Tag = 4;
      col.Name = "Act";
      col.HeaderText = "Act";
      col.Width = 40;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvResults.Columns.Add(col);

      col = new DataGridViewImageColumn();
      col.Tag = 4;
      col.Name = "Cmp";
      col.HeaderText = "Cmp";
      col.Width = 40;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvResults.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Tag = 30;
      col.Name = "FullPath";
      col.HeaderText = "FullPath";
      col.Width = 300;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      gvResults.Columns.Add(col);

      col = new DataGridViewImageColumn();
      col.Tag = 4;
      col.Name = "Action";
      col.HeaderText = "Action";
      col.Width = 0;
      col.Visible = false;
      gvResults.Columns.Add(col);
    }

    private void ViewFile()
    {
      frmCode fCode = new frmCode(_filePath);
      fCode.ShowDialog();
    }

    private void ctxMnuResults_Opening(object sender, CancelEventArgs e)
    {
      lblStatus.Text = String.Empty;

      _filePath = String.Empty;

      if (_canCompare)
      {
        ctxMnuResultsDeleteRight.Visible = false;
        ctxMnuResultsDeleteLeft.Visible = false;
        ctxMnuResultsCopyLeftToRight.Visible = true;
        ctxMnuResultsCopyRightToLeft.Visible = true;
      }
      else
      {
        if (_filePath.Contains(cboLeftPath.Text))
        {
          ctxMnuResultsCopyLeftToRight.Visible = true;
          ctxMnuResultsDeleteLeft.Visible = true;
          ctxMnuResultsCopyRightToLeft.Visible = false;
          ctxMnuResultsDeleteRight.Visible = false;
        }
        else
        {
          ctxMnuResultsCopyRightToLeft.Visible = true;
          ctxMnuResultsDeleteRight.Visible = true;
          ctxMnuResultsCopyLeftToRight.Visible = false;
          ctxMnuResultsDeleteLeft.Visible = false;
        }
      }


      if (_filePath == String.Empty)
      {
        e.Cancel = true;
      }
      else
      {
        ctxMnuResultsCompareFiles.Visible = _canCompare;
      }     
    }
    

    private void RunFileCompareUtility()
    {
      try
      {
        string relativePath = gvResults.SelectedRows[0].Cells[1].Value.ToString() == "LEFT" ?
          _filePath.Replace(_fullLeftPath, String.Empty) :
          _filePath.Replace(_fullRightPath, String.Empty);

        string leftFilePath = _fullLeftPath + relativePath;
        string rightFilePath = _fullRightPath + relativePath;

        if (!File.Exists(leftFilePath))
        {
          MessageBox.Show("The left file path '" + leftFilePath + "' cannot be located.",
            g.AppInfo.AppName + " - Error Locating Left File", MessageBoxButtons.OK, MessageBoxIcon.Error);

          return;
        }

        if (!File.Exists(rightFilePath))
        {
          MessageBox.Show("The right file path '" + rightFilePath + "' cannot be located.",
            g.AppInfo.AppName + " - Error Locating Right File", MessageBoxButtons.OK, MessageBoxIcon.Error);

          return;
        }

        ProcessParms processParms = new ProcessParms();
        processParms.ExecutablePath = _winMergeExePath;
        processParms.Args = new string[] {
          "/e",
          "/s",
          "/u",
          leftFilePath,
          rightFilePath
      };

        var processHelper = new ProcessHelper();
        var taskResult = processHelper.RunExternalProcess(processParms);


        string leftFile = File.ReadAllText(leftFilePath);
        var leftFileInfo = new FileInfo(leftFilePath);
        string rightFile = File.ReadAllText(rightFilePath);
        var rightFileInfo = new FileInfo(rightFilePath);


        if (leftFile == rightFile)
        {
          gvResults.SelectedRows[0].Cells[5].Value = _emptyImage;
          gvResults.SelectedRows[0].Cells[6].Value = _emptyImage;
          gvResults.SelectedRows[0].Cells[8].Value = FileCompareStatus.Identical;
        }
        else
        {


        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to run the file compare utility'." + g.crlf2 +
          ex.ToReport(), g.AppInfo.AppName + " - Error Running File Compare Utility", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ProcessFile(FileSystemCommand fsCommand, LeftRightDirection direction)
    {
      if (_filePath.IsBlank())
        return;

      string sourceFile = String.Empty;
      string destFile = String.Empty;
      string leftRoot = cboLeftPath.Text;
      string rightRoot = cboRightPath.Text;
      string relativePath = String.Empty;

      if (_filePath.Contains(leftRoot))
      {
        relativePath = _filePath.Replace(leftRoot, String.Empty);
      }
      else
      {
        if (_filePath.Contains(rightRoot))
          relativePath = _filePath.Replace(rightRoot, String.Empty);
        else
          return;
      }

      try
      {
        switch (direction)
        {
          case LeftRightDirection.Left:
            sourceFile = leftRoot + relativePath;
            break;

          case LeftRightDirection.LeftToRight:
            sourceFile = leftRoot + relativePath;
            destFile = rightRoot + relativePath;
            break;

          case LeftRightDirection.Right:
            sourceFile = rightRoot + relativePath;
            break;

          case LeftRightDirection.RightToLeft:
            sourceFile = rightRoot + relativePath;
            destFile = leftRoot + relativePath;

            break;
        }

        if (fsCommand == FileSystemCommand.Copy)
        {
          string directoryName = Path.GetDirectoryName(destFile);
          if (!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName); 

          File.Copy(sourceFile, destFile, true);
        }
        else
        {
          File.Delete(sourceFile);
        }
        
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to execute the command " + fsCommand.ToString() + " for the direction " + 
                        direction.ToString() + "." + g.crlf2 + ex.ToReport(),
                        "Code Compare - File Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    
    private void gvResults_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.RowIndex < 0)
        return;

      gvResults.Rows[e.RowIndex].Selected = true;
    }

    private void gvResults_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
        return;

      var status = gvResults.Rows[e.RowIndex].Cells[8].Value;

      if (status == null)
        return;

      var fileCompareStatus = (FileCompareStatus)status;


      switch (e.ColumnIndex)
      {
        case 5: // run file copy actions

          break;


        case 6:
          if (fileCompareStatus != FileCompareStatus.OppositeMissing)
            RunFileCompareUtility();
          break;

        case 7:
          ViewFile();
          break;
      }
    }

    private void gvResults_SelectionChanged(object sender, EventArgs e)
    {
      if (gvResults.SelectedRows.Count == 0)
      {
        _filePath = String.Empty;
        return;
      }

      _filePath = gvResults.SelectedRows[0].Cells[7].Value.ToString();
    }

    private void cboLeftStem_TextChanged(object sender, EventArgs e)
    {
      bool folderExists = Directory.Exists(cboLeftStem.Text);

      if (folderExists)
      {
        _pathValidationSuspended = true;
        string[] leftBranchPaths = Directory.GetDirectories(cboLeftStem.Text);
        var leftBranches = new List<string>();
        foreach (var leftBranchPath in leftBranchPaths)
        {
          leftBranches.Add(leftBranchPath.Replace(cboLeftStem.Text, String.Empty));
        }
        cboLeftBranch.LoadItems(leftBranches);
        _pathValidationSuspended = false;
      }
      else
      {
        ClearLeft();
      }

      ValidatePaths();
    }

    private void ClearLeft()
    {
      cboLeftBranch.Text = String.Empty;
      cboLeftPath.Text = String.Empty;
    }   

    private void cboRightStem_TextChanged(object sender, EventArgs e)
    {
      bool folderExists = Directory.Exists(cboRightStem.Text);

      if (folderExists)
      {
        _pathValidationSuspended = true;
        string[] rightBranchPaths = Directory.GetDirectories(cboRightStem.Text);
        var rightBranches = new List<string>();
        foreach (var rightBranchPath in rightBranchPaths)
        {
          rightBranches.Add(rightBranchPath.Replace(cboRightStem.Text, String.Empty));
        }
        cboRightBranch.LoadItems(rightBranches);
        _pathValidationSuspended = false;
      }
      else
      {
        ClearRight();
      }

      ValidatePaths();
    }

    private void ClearRight()
    {
      cboRightBranch.Text = String.Empty;
      cboRightPath.Text = String.Empty;
    }

    private void cboLeftBranch_TextChanged(object sender, EventArgs e)
    {
      ValidatePaths();

      if (_fullLeftPathValid)
      {
        _pathValidationSuspended = true;
        string[] leftPath = Directory.GetDirectories(_fullLeftPath);
        var leftFolders = new List<string>();
        foreach (var leftFolder in leftPath)
        {
          leftFolders.Add(leftFolder.Replace(_fullLeftPath, String.Empty));
        }
        cboLeftPath.LoadItems(leftFolders);
        _pathValidationSuspended = false;
        cboLeftPath.Select();
      }
    }

    private void cboRightBranch_TextChanged(object sender, EventArgs e)
    {
      ValidatePaths();

      if (_fullRightPathValid)
      {
        _pathValidationSuspended = true;
        string[] rightPath = Directory.GetDirectories(_fullRightPath);
        var rightFolders = new List<string>();
        foreach (var rightFolder in rightPath)
        {
          rightFolders.Add(rightFolder.Replace(_fullRightPath, String.Empty));
        }
        cboRightPath.LoadItems(rightFolders);
        _pathValidationSuspended = false;
        cboRightPath.Select();
      }
    }

    private void cboLeftPath_TextChanged(object sender, EventArgs e)
    {
      ValidatePaths();
    }

    private void cboRightPath_TextChanged(object sender, EventArgs e)
    {
      ValidatePaths();
    }

    private void SetLeftFullPath()
    {
      _fullLeftPath = (cboLeftStem.Text.Trim() + @"\" + cboLeftBranch.Text.Trim() + @"\" + cboLeftPath.Text.Trim()).NormalizePathString();
    }

    private void SetRightFullPath()
    {
      _fullRightPath = (cboRightStem.Text.Trim() + @"\" + cboRightBranch.Text.Trim() + @"\" + cboRightPath.Text.Trim()).NormalizePathString();
    }

    private void ValidatePaths()
    {
      SetLeftFullPath();
      SetRightFullPath();

      if (_pathValidationSuspended)
        return;

      _fullLeftPathValid = false;
      _fullRightPathValid = false;

      if (_fullLeftPath.IsNotBlank() && Directory.Exists(_fullLeftPath))
      {
        pbLeftPath.Image = _checkImage;
        _leftPathImageTooltip = "Path is valid";
        _fullLeftPathValid = true;
      }
      else
      {
        pbLeftPath.Image = _questionMarkImage;
        _leftPathImageTooltip = "Path not found";
      }

      if (_fullRightPath.IsNotBlank() && Directory.Exists(_fullRightPath))
      {
        pbRightPath.Image = _checkImage;
        _rightPathImageTooltip = "Path is valid";
        _fullRightPathValid = true;
      }
      else
      {
        pbRightPath.Image = _questionMarkImage;
        _rightPathImageTooltip = "Path not found";
      }

      bool identicalPathWarningVisible = false;
      if (_fullLeftPath.IsNotBlank() && _fullRightPath.IsNotBlank())
      {
        if (_fullLeftPath.ToLower() == _fullRightPath.ToLower())
          identicalPathWarningVisible = true;
      }

      lblIdenticalPathWarning.Visible = identicalPathWarningVisible;

      btnCompare.Enabled = _fullLeftPathValid && _fullRightPathValid && !identicalPathWarningVisible;
    }

    private void pbLeftPath_MouseHover(object sender, EventArgs e)
    {
      ToolTip tt = new ToolTip();
      tt.SetToolTip(pbLeftPath, _leftPathImageTooltip);
    }

    private void pbRightPath_MouseHover(object sender, EventArgs e)
    {
      ToolTip tt = new ToolTip();
      tt.SetToolTip(pbRightPath, _rightPathImageTooltip);
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_isFirstShowing)
        return;

      btnCompare.Select();

      _isFirstShowing = false;
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (g.AppConfig.IsUpdated)
      {
        switch (MessageBox.Show("The configuration is updated." + g.crlf2 +
                                "Do you want to save the configuration updates?" + g.crlf2 +
                                "Click 'Yes' to save or 'No' to discard." + g.crlf2 +
                                "Click 'Cancel' to continue using the application.",
                                g.MsgTitle("Save Configuration?"), MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question))
        {
          case DialogResult.Yes:
            SaveConfig();
            break;

          case DialogResult.No:
            break;

          case DialogResult.Cancel:
            e.Cancel = true;
            break;
        }
      }
    }
  }
}
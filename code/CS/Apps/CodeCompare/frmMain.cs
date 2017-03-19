using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    private a a;
    private int _mouseX;
    private int _mouseY;
    private int _lineNumber = -1;
    private int _charNumber = -1;
    private bool _canCompare = false;
    private string _filePath = String.Empty;
    private fctb.MarkerStyle _bgHighlightStyle;
    private fctb.MarkerStyle _bgClearStyle;
    private List<fctb.Range> _rangesToClear;
    private string _winMergeExePath; 
    
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
        case "Compare":
          Compare();
          break;

        case "ViewFile":
          ViewFile();
          break;

        case "CompareFiles":
          CompareFiles();
          break;

        case "CopyRightToLeft":
        case "CopyLeftToRight":
        case "DeleteRight":
        case "DeleteLeft":
          ProcessFile(action);
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void Compare()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        txtOut.Text = String.Empty;
        Application.DoEvents();

        string leftPath = cboLeftPath.Text;
        string rightPath = cboRightPath.Text;

        g.AppConfig.ReloadFromFile();

        if (!Directory.Exists(leftPath))
        {
          MessageBox.Show("Left Path '" + leftPath + "' does not exist.", "Code Compare - Invalid Left Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
          this.Cursor = Cursors.Default;
          return;
        }

        if (!Directory.Exists(rightPath))
        {
          MessageBox.Show("Right Path '" + rightPath + "' does not exist.", "Code Compare - Invalid Right Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
          this.Cursor = Cursors.Default;
          return;
        }

        var fileTypes = cboFileTypes.Text.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();

        var leftSearchParms = new SearchParms();
        leftSearchParms.ProcessChildFolders = true;
        leftSearchParms.RootPath = leftPath;
        leftSearchParms.FolderNameExcludes = g.AppConfig.GetList("LeftFolderNameExcludes");
        leftSearchParms.FileNameExcludes = g.AppConfig.GetList("FileNameExcludes");
        leftSearchParms.Extensions = fileTypes;

        var rightSearchParms = new SearchParms();
        rightSearchParms.ProcessChildFolders = true;
        rightSearchParms.RootPath = rightPath;
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
            kvpFile.Value.FileCompareStatus = FileCompareStatus.OppositeExists;

            string leftFile = File.ReadAllText(kvpFile.Value.FullPath); 
            string rightFile = File.ReadAllText(rightFolder.FileList[kvpFile.Key].FullPath);
            if (leftFile == rightFile)
            {
              kvpFile.Value.FileCompareStatus = FileCompareStatus.Identical;
            }
            else
            {
              kvpFile.Value.FileCompareStatus = FileCompareStatus.OppositeExists;
            }
          }
          else
          {
            kvpFile.Value.FileCompareStatus = FileCompareStatus.OppositeMissing;
          }
        }

        foreach(var kvpFile in rightFolder.FileList)
        {
          if (leftFolder.FileList.ContainsKey(kvpFile.Key))
          {
            kvpFile.Value.FileCompareStatus = FileCompareStatus.OppositeExists;
          }
          else
          {
            kvpFile.Value.FileCompareStatus = FileCompareStatus.OppositeMissing;
          }
        }


        sb.Append("LEFT FOLDER" + g.crlf2); 
        foreach (var file in leftFolder.FileList.Values)
        {    
          if (file.FileCompareStatus == FileCompareStatus.OppositeMissing)
            sb.Append("OPPOSITE MISSING" + "   " + file.FullPath + g.crlf);

          if (file.FileCompareStatus == FileCompareStatus.OppositeExists)
            sb.Append("FILES DIFFERENT " + "   " + file.FullPath + g.crlf);
        }

        sb.Append(g.crlf2 + "RIGHT FOLDER" + g.crlf2); 
        foreach (var file in rightFolder.FileList.Values)
        {
          if (file.FileCompareStatus == FileCompareStatus.OppositeMissing)
            sb.Append("OPPOSITE MISSING" + "   " + file.FullPath + g.crlf);
        }

        txtOut.Text = sb.ToString();


        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during compare operation'." + g.crlf2 +
          ex.ToReport(), "Code Compare - Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();

        string defaultLeftPath = g.CI("DefaultLeftPath");
        string defaultRightPath = g.CI("DefaultRightPath");
        string defaultFileTypes = g.CI("DefaultFileTypes"); 

        var searchPaths = g.AppConfig.GetList("SearchPaths");

        cboLeftPath.Items.Clear();
        cboRightPath.Items.Clear();
        foreach (var searchPath in searchPaths)
        {
          cboLeftPath.Items.Add(searchPath);
          cboRightPath.Items.Add(searchPath);
        }

        for (int i = 0; i < cboLeftPath.Items.Count; i++)
        {
          if (cboLeftPath.Items[i].ToString() == defaultLeftPath)
          {
            cboLeftPath.SelectedIndex = i;
            break;
          }
        }

        for (int i = 0; i < cboRightPath.Items.Count; i++)
        {
          if (cboRightPath.Items[i].ToString() == defaultRightPath)
          {
            cboRightPath.SelectedIndex = i;
            break;
          }
        }

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

        _bgClearStyle = new fctb.MarkerStyle(Brushes.White);
        _bgHighlightStyle = new fctb.MarkerStyle(Brushes.LightSteelBlue);
        _rangesToClear = new List<fctb.Range>();
        _winMergeExePath = g.CI("WinMergeExePath"); 

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during initialization of the application object 'a'." + g.crlf2 +
          ex.ToReport(), "Code Compare - Program Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void txtCompareResults_MouseMove(object sender, MouseEventArgs e)
    {
      _mouseX = e.X;
      _mouseY = e.Y;
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

      if (_mouseX > -1 && _mouseY > -1)
      {
        var place = txtOut.PointToPlace(new Point(_mouseX, _mouseY));
        if (place != null)
        {
          _lineNumber = place.iLine;
          _charNumber = place.iChar;

          _filePath = GetFilePath(_lineNumber, _charNumber);
        }
      }


      // if both files exist
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

    private string GetFilePath(int lineNumber, int charNumber)
    {
      _canCompare = false;

      var lastLine = txtOut.Lines.LastOrDefault();
      if (lastLine == null)
        return String.Empty;

      foreach(var rangeToClear in _rangesToClear)
      {
        rangeToClear.ClearStyle(fctb.StyleIndex.All); 
      }

      if (lineNumber == -1 || charNumber == -1)
        return String.Empty;

      string fileLineNumber = String.Empty;
      string fileName = String.Empty;
      string folderName = String.Empty;

      var line = txtOut.GetLine(lineNumber);
      int charCount = line.Text.Length; 


      string lineText = line.Text.Trim();
      if (lineText.IsBlank())
        return String.Empty;

      if (lineText.StartsWith("FILES DIFFERENT"))
      {
        lineText = lineText.Replace("FILES DIFFERENT", String.Empty).Trim();
        _canCompare = true;
      }

      if (lineText.StartsWith("OPPOSITE MISSING"))
        lineText = lineText.Replace("OPPOSITE MISSING", String.Empty).Trim();

      if (!File.Exists(lineText))
        return String.Empty;

      var newRange = txtOut.GetRange(new fctb.Place(0, lineNumber), new fctb.Place(charCount, lineNumber));

      if (txtOut.Styles[0] != null)
        newRange.SetStyle(txtOut.Styles[0]); 
      else
        newRange.SetStyle(new fctb.MarkerStyle(Brushes.LightSteelBlue));

      _rangesToClear.Add(newRange); 

      return lineText;
    }

    private void CompareFiles()
    {
      string relativePath = _filePath.Replace(cboLeftPath.Text, String.Empty);

      string leftFullPath = cboLeftPath.Text + relativePath;
      string rightFullPath = cboRightPath.Text + relativePath;

      ProcessParms processParms = new ProcessParms();
      processParms.ExecutablePath = _winMergeExePath; 
      processParms.Args = new string[] {  
          "/e",
          "/s", 
          "/u", 
          leftFullPath,
          rightFullPath
      };

      var processHelper = new ProcessHelper();
      var taskResult = processHelper.RunExternalProcess(processParms);
    }

    private void ProcessFile(string action)
    {
      if (_filePath.IsBlank())
        return;

      var line = txtOut.GetLine(_lineNumber);
      string lineText = line.Text.Trim();

      bool oppositeMissing = false;
      if (line.Text.StartsWith("OPPOSITE MISSING"))
        oppositeMissing = true;

      string fileAction = "copy";
      if (action.Contains("Delete"))
        fileAction = "delete";

      string sourceFile = String.Empty;
      string destFile = String.Empty;
      string copyDescription = String.Empty;
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
        switch (action)
        {
          case "CopyLeftToRight":
          case "DeleteLeft":
            copyDescription = "left to right";
            sourceFile = leftRoot + relativePath;
            destFile = rightRoot + relativePath;
            break;

          default:
            copyDescription = "right to left";
            sourceFile = rightRoot + relativePath;
            destFile = leftRoot + relativePath;
            break;
        }

        if (fileAction == "copy")
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

        txtOut.RemoveLines(new List<int>() { _lineNumber }); 
      }
      catch (Exception ex)
      {
        if (fileAction == "delete")
        {
          MessageBox.Show("An exception occurred while attempting to delete file " + sourceFile + "." + g.crlf2 + ex.ToReport(),
            "Code Compare - File Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
          MessageBox.Show("An exception occurred while attempting to copy file from " + copyDescription + " - source file " +
            sourceFile + " destination file " + destFile + "." + g.crlf2 + ex.ToReport(),
            "Code Compare - File Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private void txtOut_MouseClick(object sender, MouseEventArgs e)
    {
      lblStatus.Text = String.Empty;

      _filePath = String.Empty;

      if (_mouseX > -1 && _mouseY > -1)
      {
        var place = txtOut.PointToPlace(new Point(_mouseX, _mouseY));
        if (place != null)
        {
          _lineNumber = place.iLine;
          _charNumber = place.iChar;

          _filePath = GetFilePath(_lineNumber, _charNumber);
        }
      }
    }
  }
}

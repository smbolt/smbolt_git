using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Org.GS;

namespace Org.FileUtility
{
  public partial class frmMain : Form
  {
    private a a;

		private bool _firstShowing = true;

    private string _filePath = String.Empty;
		private int _lineNumber = -1;
		private int _charNumber = -1;
		private int _mouseX = -1;
		private int _mouseY = -1;
    private int _defaultFileLimit = 0; 

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
        case "RunSearch":
          RunSearch();
          break;

        case "FindFiles":
          FindFiles();
          break;

        case "ViewFile":
          ViewFile();
          break;

        case "FindRecentChanges":
          FindRecentChanges();
          break;

				case "BrowseSearchPath":
					BrowseSearchPath();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

		private void FindFiles()
		{
      txtOut.Clear();
      Application.DoEvents();

      if (!ValidateInput(false))
        return;

      this.Cursor = Cursors.WaitCursor;

      var searchParms = new SearchParms();

      txtOut.Text = String.Empty;
      Application.DoEvents();

      char[] delim = new char[] { ',' };

      searchParms.RootPath = cboSearchPath.Text; 
      searchParms.Extensions = cboSearchFilters.Text.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();
      searchParms.ContentIncludes = txtSearchPatternsInclude.Text.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();
      searchParms.ContentExcludes = txtSearchPatternsExclude.Text.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();
      searchParms.FileCountLimit = txtFileCountLimit.Text.ToInt32();

      var rootFolder = BuildFileList(searchParms, true);

			StringBuilder sb = new StringBuilder();
			foreach (var file in rootFolder.FileList.Values)
			{
				sb.Append(file.FullPath + g.crlf); 
			}

			txtOut.Text = "The results of the search are as follows..." + g.crlf + sb.ToString();

      this.Cursor = Cursors.Default;
		}

    private void RunSearch()
    {
      txtOut.Clear();
      Application.DoEvents();

      if (!ValidateInput(true))
        return;

      this.Cursor = Cursors.WaitCursor;

      var searchParms = new SearchParms();

      txtOut.Text = String.Empty;
      Application.DoEvents();

      char[] delim = new char[] { ',' };

      searchParms.RootPath = cboSearchPath.Text; 
      searchParms.Extensions = cboSearchFilters.Text.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();
      searchParms.ContentIncludes = txtSearchPatternsInclude.Text.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();
      searchParms.ContentExcludes = txtSearchPatternsExclude.Text.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();

      var rootFolder = BuildFileList(searchParms, false);
      
      string searchResults = rootFolder.RunSearch();

      if (searchResults.Trim().Length == 0)
        searchResults = Environment.NewLine + "*** NO MATCHES FOUND ***" + g.crlf;

      txtOut.Text = "The results of the search are as follows..." + g.crlf + searchResults;

      this.Cursor = Cursors.Default;
    }

    private void FindRecentChanges()
    {
      this.Cursor = Cursors.WaitCursor;

      var searchParms = new SearchParms();
      searchParms.SinceDate = dtpSince.Value;

      txtOut.Text = String.Empty;
      Application.DoEvents();

      char[] delim = new char[] { ',' };

      searchParms.Extensions = cboSearchFilters.Text.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();
      searchParms.ContentIncludes = txtSearchPatternsInclude.Text.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();
      searchParms.ContentExcludes = txtSearchPatternsExclude.Text.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();

      var rootFolder = BuildFileList(searchParms, false);
      string searchResults = rootFolder.GetChangedFiles();

      if (searchResults.Trim().Length == 0)
        searchResults = Environment.NewLine + "*** NO CHANGED FILES FOUND ***" + g.crlf;

      txtOut.Text = "The results of the search are as follows..." + g.crlf + searchResults;

      this.Cursor = Cursors.Default;
    }

    private DateTime GetChangesSinceDate()
    {
      TimeUnit timeUnit = TimeUnit.Day;
      if (cboChangeTimeFrame.Text.ToLower().Contains("week"))
        timeUnit = TimeUnit.Week;
      if (cboChangeTimeFrame.Text.ToLower().Contains("month"))
        timeUnit = TimeUnit.Month;

      int units = cboChangeTimeFrame.Text.GetIntegerFromString();

      switch (timeUnit)
      {
        case TimeUnit.Month:
          return DateTime.Now.AddMonths(units * -1);
        case TimeUnit.Week:
          return DateTime.Now.AddDays(units * 7 * -1);
        default:
          return DateTime.Now.AddDays(units * -1);
      }
    }

    private OSFolder BuildFileList(SearchParms searchParms, bool wireUpEvents)
    {      
      var rootFolder = new OSFolder();
      rootFolder.FullPath = cboSearchPath.Text.Trim();
      rootFolder.RootFolderPath = cboSearchPath.Text.Trim();
      rootFolder.IsRootFolder = true;
      rootFolder.DepthFromRoot = 0;
      rootFolder.SearchParms.ProcessChildFolders = ckIncludeChildFolders.Checked;
      if (wireUpEvents)
      {
        OSFolder.SetLimitReachedFunction(FileLimitReached);
        OSFolder.FSNotification += NotifyHost;
      }

      rootFolder.SearchParms = searchParms;
      rootFolder.BuildFolderAndFileList();
      return rootFolder;
    }
    
    private bool FileLimitReached(OSFolder rootFolder, bool processAllFolders)
    {
      int folderOmitCount = processAllFolders ? 0 : 1;

      if (!processAllFolders && rootFolder.OSFolderSet.Count < 2)
        return false;

      for (int i = 0; i < rootFolder.OSFolderSet.Count - folderOmitCount; i++)
      {
        var folder = rootFolder.OSFolderSet[i];

        StringBuilder sb = new StringBuilder();
        foreach (var file in folder.FileList)
        {
          sb.Append(file + g.crlf);
        }

        if (txtOut.InvokeRequired)
        {
          txtOut.Invoke((Action)((() =>
          {
            txtOut.Text = sb.ToString();
            Application.DoEvents();
          })));
        }
        else
        {
          txtOut.Text = sb.ToString();
          Application.DoEvents();
        }
        
        folder.IsProcessed = true;
      }

      return true;
    }

    private void NotifyHost(string notifyMessage)
    {
      if (lblStatus.InvokeRequired)
      {
        lblStatus.Invoke((Action)((() =>
        {
          lblStatus.Text = notifyMessage;
          Application.DoEvents();
        })));
      }
      else
      {
        lblStatus.Text = notifyMessage;
        Application.DoEvents();
      }
    }


    private void ViewFile()
    {
      frmCode fCode = new frmCode(_filePath);

			fCode.ShowDialog();
    }

		private void BrowseSearchPath()
		{
			if (dlgFolderBrowser.ShowDialog() == DialogResult.OK)
			{
				cboSearchPath.Text = dlgFolderBrowser.SelectedPath;
			}
		}

    private void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the 'a' (application management) object." + g.crlf2 +
                        "Exception Report:" + g.crlf + ex.ToReport(), "File Utility - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      cboChangeTimeFrame.SelectedIndex = 0;


      var searchPaths = g.AppConfig.GetList("SearchPaths");
      cboSearchPath.Items.Clear();
      foreach (string searchPath in searchPaths)
        cboSearchPath.Items.Add(searchPath);

      string defaultSearchPath = g.CI("DefaultSearchPath");
      if (defaultSearchPath.IsNotBlank())
      {
        for (int i = 0; i < cboSearchPath.Items.Count; i++)
        {
          if (cboSearchPath.Items[i].ToString() == defaultSearchPath)
          {
            cboSearchPath.SelectedIndex = i;
            break;
          }
        }
      }

      var searchFilters = g.AppConfig.GetList("SearchFilters");
      cboSearchFilters.Items.Clear();
      foreach (string searchFilter in searchFilters)
        cboSearchFilters.Items.Add(searchFilter);

      string defaultSearchFilter = g.CI("DefaultSearchFilter");
      if (defaultSearchFilter.IsNotBlank())
      {
        for (int i = 0; i < cboSearchFilters.Items.Count; i++)
        {
          if (cboSearchFilters.Items[i].ToString() == defaultSearchFilter)
          {
            cboSearchFilters.SelectedIndex = i;
            break;
          }
        }
      }

      ckIncludeChildFolders.Checked = true;
      ckUseSearchResults.Checked = false;

      _defaultFileLimit = g.CI("DefaultFileLimit").ToInt32();
      if (_defaultFileLimit > 0)
        txtFileCountLimit.Text = _defaultFileLimit.ToString();


      btnRunSearch.Enabled = false;
    }

    private bool ValidateInput(bool requireSearchPatterns)
    {
      if (cboSearchPath.Text.Trim().Length == 0)
      {
        MessageBox.Show("Please enter a search path or use the File|Open menu item to locate a path.", "File Utility Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        cboSearchPath.Focus();
        return false;
      }

      if (!Directory.Exists(cboSearchPath.Text.Trim()))
      {
        MessageBox.Show("The search path entered is invalid.", "File Utility Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        cboSearchPath.Focus();
        return false;
      }

      if (requireSearchPatterns)
      {
        if (txtSearchPatternsInclude.Text.Trim().Length == 0)
        {
          MessageBox.Show("No search patterns were entered - please enter search patterns to match.", "File Utility Error",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
          txtSearchPatternsInclude.SelectAll();
          txtSearchPatternsInclude.Focus();
          return false;
        }
      }

      return true;
    }

    private void txtSearchPatternsChanged(object sender, EventArgs e)
    {
      if (txtSearchPatternsInclude.Text.IsBlank())
        btnRunSearch.Enabled = false;
      else
        btnRunSearch.Enabled = true;
    }

    private void ctxMnuResultsViewFile_DropDownOpening(object sender, EventArgs e)
    {
      //if (rtxtOut.Text.Trim().Length == 0)
      //{
      //  rtxtOut.ContextMenu = null;
      //}
    }

    private void rtxtOut_MouseUp(object sender, MouseEventArgs e)
    {

    }

		private string GetFilePath(int lineNumber, int charNumber)
		{
			if (lineNumber == -1 || charNumber == -1)
				return String.Empty;

			string fileLineNumber = String.Empty;
			string fileName = String.Empty;
			string folderName = String.Empty;

			var line = txtOut.GetLine(lineNumber);
			string lineText = line.Text.Trim();
			if (lineText.IsBlank())
				return String.Empty;

			string[] tokens = lineText.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

			if (tokens.Length < 2)
				return String.Empty;

			if (tokens[0].Length == 3 && tokens[0].IsNumeric() && tokens[1].ToLower() == "folder:")
				return String.Empty;

			if (tokens[0].ToLower() == "file:")
				fileName = tokens[1];
			else
				fileLineNumber = tokens[0];

			while (lineNumber > -1 && fileName.IsBlank())
			{
				lineNumber--;
				line = txtOut.GetLine(lineNumber);
				if (line == null)
					return String.Empty;
				lineText = line.Text.Trim();
				tokens = lineText.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
				if (tokens.Length > 1)
				{
					if (tokens[0].ToLower() == "file:")
						fileName = tokens[1];
				}
			}

			while (lineNumber > -1 && folderName.IsBlank())
			{
				lineNumber--;
				line = txtOut.GetLine(lineNumber);
				lineText = line.Text.Trim();
				tokens = lineText.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
				if (tokens.Length > 2)
				{
					if (tokens[1].ToLower() == "folder:")
						folderName = tokens[2];
				}
			}

			if (fileName.IsBlank() || folderName.IsBlank())
				return String.Empty;

			string fullPath = folderName + @"\" + fileName;

			if (!File.Exists(fullPath))
				return String.Empty;

			return fullPath;
		}

		private void ctxMnuResults_Opening(object sender, CancelEventArgs e)
		{
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

			if (_filePath == String.Empty)
				e.Cancel = true;
		}
		
		private void rtxtOut_MouseMove(object sender, MouseEventArgs e)
		{
			_mouseX = e.X;
			_mouseY = e.Y;
		}

		private void frmMain_KeyUp(object sender, KeyEventArgs e)
		{
			if (!btnRunSearch.Enabled)
				return;

			if (e.KeyCode == Keys.Enter)
			{
				RunSearch();
			}
		}

		private void frmMain_Shown(object sender, EventArgs e)
		{
			if (!_firstShowing)
				return;

			txtSearchPatternsExclude.Select();
			txtSearchPatternsInclude.Focus();
		}

    private void cboChangeTimeFrame_SelectedIndexChanged(object sender, EventArgs e)
    {
      dtpSince.Value = GetChangesSinceDate();
    }
  }
}

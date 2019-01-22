using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.FSO;
using Org.GS;
using Org.GS.Configuration;

namespace Org.FileOrganizer
{
  public partial class frmMain : Form
  {
    private a a;
    private ConfigDbSpec _dbSpec;
    private FsoRepository _fsoRepo;
    private ProjectSet _projectSet;
    private Project _project;
    private RootFolderSet _rootFolderSet;

    public List<string> ExtensionList = new List<string>();

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
        case "NewProject":
          NewProject();
          break;

        case "DeleteProject":
          DeleteProject();
          break;

        case "DeleteRootFolder":
          DeleteRootFolder();
          break;

        case "DeleteFolder":
          DeleteFolder();
          break;

        case "AddDocumentType":
          AddDocumentType();
          break;

        case "PopulateDocTypeGrid":
          PopulateDocTypeGrid();
          break;

        case "AddTagType":
          AddTagType();
          break;

        case "PopulateTagTypeGrid":
          PopulateTagTypeGrid();
          break;

        case "ProcessRootFolder":
          ProcessRootFolder();
          break;

        case "AddRootFolder":
          AddARootFolder();
          break;

        case "SelectedProjectChange":
          UpdateRootFolders();
          {}
          break;

        case "Exit":
          Close();
          break;
      }
    }

    private void DeleteRootFolder()
    {
      var rf = cboRootFolderDropDown.Text.Trim();
      var path = GetPathNameFromProjectName(rf);
      var id = _fsoRepo.GetRootFolderID(path);

      DialogResult result = MessageBox.Show("This will delete the root folder and all sub folders and files. Are you sure you want to do this",
                                            "Delete - This process cannot be undone.", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
      if(result != DialogResult.OK)
      {
        return;
      }

      _fsoRepo.DeleteRootFolderTable(id);

      UpdateRootFolders();
    }

    private void DeleteFolder()
    {
      var rf = cboRootFolderDropDown.Text.Trim();
      var path = GetPathNameFromProjectName(rf);
      var id = _fsoRepo.GetRootFolderID(path);

      DialogResult result = MessageBox.Show("This will delete the folders table and all files asociated with those folders. Are you sure you want to do this",
                                            "Delete - This process cannot be undone.", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
      if(result != DialogResult.OK)
      {
        return;
      }

      _fsoRepo.DeleteFolderTable(id);

      UpdateRootFolders();
    }

    private void ProcessRootFolder()
    {
      var rf = cboRootFolderDropDown.Text.Trim();
      var name = GetPathNameFromProjectName(rf);
      var id = _fsoRepo.GetRootFolderIDFromName(name);

      if(btnProcessRootFolder.Text == "Re-Process Root Folder")
      {
        DialogResult result = MessageBox.Show("Re-Processing a root folder will delete all current folders and files associated with the root folder " + g.crlf +
                                              " and then re-populate the the table with the current folder and file structure.", "This is a delete and cannot be undone.",
                                              MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
        if(result != DialogResult.OK)
        {
          return;
        }

        _fsoRepo.DeleteFolderTable(id);
      }

      txtOut.Clear();
      Application.DoEvents();

      if (!ValidateInput(false))
        return;

      this.Cursor = Cursors.WaitCursor;

      var searchParms = new SearchParms();

      txtOut.Text = String.Empty;
      Application.DoEvents();

      char[] delim = new char[] { ',' };

      searchParms.RootPath = GetPathNameFromProjectName(cboRootFolderDropDown.Text);
      searchParms.FileCountLimit = txtFileCountLimit.Text.ToInt32();
      searchParms.LogPathTooLongExceptions = true;

      var rootFolder = BuildFileList(searchParms);

      StringBuilder sb = new StringBuilder();
      foreach (var file in rootFolder.FileList.Values)
      {
        sb.Append(file.FullPath + g.crlf);
      }

      txtOut.Text = "The results of the search are as follows..." + g.crlf + sb.ToString();

      this.Cursor = Cursors.Default;
    }

    private bool ValidateInput(bool requireSearchPatterns)
    {
      if (cboRootFolderDropDown.Text.Trim().Length == 0)
      {
        MessageBox.Show("Please enter a search path or use the File|Open menu item to locate a path.", "File Utility Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        cboRootFolderDropDown.Focus();
        return false;
      }

      var path = GetPathNameFromProjectName(cboRootFolderDropDown.Text.Trim());
      string trimmedPath = path.Trim();
      if (!Directory.Exists(trimmedPath))
      {
        MessageBox.Show("The search path entered is invalid.", "File Utility Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        cboRootFolderDropDown.Focus();
        return false;
      }

      return true;
    }

    private OSFolder BuildFileList(SearchParms searchParms)
    {
      Project projectToProcess = new Project();
      foreach (var project in _projectSet)
        if(project.Key == cboProject.Text.ToString())
        {
          projectToProcess.Name = project.Value.Name;
          projectToProcess.ProjectID = project.Value.ProjectID;
        }
      var rootFolder = new OSFolder();
      rootFolder.FullPath = GetPathNameFromProjectName(cboRootFolderDropDown.Text.Trim());
      rootFolder.RootFolderPath = GetPathNameFromProjectName(cboRootFolderDropDown.Text.Trim());
      var p = _fsoRepo.GetProjectByName(projectToProcess.Name);
      rootFolder.ProjectID = p.ProjectID;
      rootFolder.IsRootFolder = true;
      rootFolder.DepthFromRoot = 0;
      rootFolder.SearchParms.ProcessChildFolders = ckIncludeChildFolders.Checked;
      OSFolder.SetLimitReachedFunction(FileLimitReached);
      OSFolder.FSNotification += NotifyHost;

      rootFolder.SearchParms = searchParms;
      rootFolder.BuildFolderAndFileList();
      return rootFolder;
    }

    private bool FileLimitReached(OSFolder rootFolder, bool processAllFolders)
    {
      int folderOmitCount = processAllFolders ? 0 : 1;

      int rootFolderId = -1;
      foreach (var rf in _rootFolderSet)
      {
        if (rf.Value.RootFolderPath.Trim().ToLower() == rootFolder.FullPath.Trim().ToLower())
        {
          rootFolderId = rf.Key;
          break;
        }
      }

      if (rootFolderId == -1)
        throw new Exception("An exception occurred while attempting to obtain the RootFolderID from the RootFolderSet collection for the folder being processed - '" + rootFolder.FullPath + "'.");


      if (!processAllFolders && rootFolder.OSFolderSet.Count < 2)
        return false;

      for (int i = 0; i < rootFolder.OSFolderSet.Count - folderOmitCount; i++)
      {
        var folder = rootFolder.OSFolderSet[i];

        using (var repo = new FsoRepository(_dbSpec))
        {
          repo.LoadFileSystem(folder, ExtensionList, rootFolderId);
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

    private void NewProject()
    {
      try
      {
        using (var fNewProject = new frmNewProject(_fsoRepo))
        {
          if (fNewProject.ShowDialog() == DialogResult.OK)
          {
            string newProjectName = fNewProject.NewProjectName;
            LoadProjects();
            SelectProjectByName(newProjectName);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to create a new project." + g.crlf2 +
                        ex.ToReport(), "File Organizer - Error Creating New Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadProjects()
    {
      _projectSet = _fsoRepo.GetProjects();

      cboProject.Items.Clear();
      foreach (var project in _projectSet.Values)
      {
        cboProject.Items.Add(project.Name);
      }
    }

    private void SelectProjectByName(string projectName)
    {
      for (int i = 0; i < cboProject.Items.Count; i++)
      {
        if (cboProject.Items[i].ToString() == projectName)
        {
          cboProject.SelectedIndex = i;
          break;
        }
      }

      if (_projectSet.ContainsKey(cboProject.Text))
        _project = _projectSet[cboProject.Text];
    }

    private void DeleteProject()
    {
      Project projectToDelete = new Project();
      foreach (var project in _projectSet)
        if(project.Key == cboProject.Text.ToString())
        {
          projectToDelete.Name = project.Value.Name;
          projectToDelete.ProjectID = project.Value.ProjectID;
        }

      _project = projectToDelete;

      if (_project == null)
        return;

      if (MessageBox.Show("Are you sure you want to delete project '" + _project.Name + "'?" + g.crlf2 +
                          "This action cannot be reversed.", "Confirm Project Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        return;


      try
      {
        _fsoRepo.DeleteProjectByName(_project.Name);
        _project = null;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to delete project '" + _project.Name + "'." + g.crlf2 +
                        ex.ToReport(), "File Organizer - Error Deleting Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void AddDocumentType()
    {
      if(cboProject.Text.Length < 1)
      {
        MessageBox.Show("You must first open a project before you can add a Document Type.",
                        "File Organizer Error - Error Creating Document Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      try
      {
        Project p = _fsoRepo.GetProjectByName(cboProject.Text.Trim());
        _fsoRepo.currentProjectID = p.ProjectID;
        using (var fNewDocType = new frmNewDocType(_fsoRepo))
        {
          fNewDocType.StartPosition = FormStartPosition.CenterScreen;
          if (fNewDocType.ShowDialog() == DialogResult.OK)
          {
            string newDocTypeName = fNewDocType.NewDocumentType;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred attempting to add a new Document Type.", ex);
      }
    }

    private void PopulateDocTypeGrid()
    {
      if(cboProject.Text.Length < 1)
      {
        MessageBox.Show("You must first open a project before you can add a Document Type.",
                        "File Organizer Error - Error Creating Document Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      try
      {
        Project p = _fsoRepo.GetProjectByName(cboProject.Text.Trim());
        _fsoRepo.currentProjectID = p.ProjectID;
        var dts = _fsoRepo.PopulateDocTypeGrid();
        gvDocTypes.Rows.Clear();
        foreach(var docType in dts)
        {
          var doc = docType.Value;
          gvDocTypes.Rows.Add(docType.Value.DocTypeID.ToString(), docType.Value.ProjectName, docType.Value.DocName);
        }

      }
      catch (Exception ex)
      {
        throw new Exception ("An error occurred attempting to load data to the Doc Type grid.", ex);
      }
    }

    private void AddTagType()
    {
      if(cboProject.Text.Length < 1)
      {
        MessageBox.Show("You must first open a project before you can add a Tag Type.",
                        "File Organizer Error - Error Creating Tag Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      try
      {
        Project p = _fsoRepo.GetProjectByName(cboProject.Text.Trim());
        _fsoRepo.currentProjectID = p.ProjectID;
        using (var fNewTagType = new frmNewTagType(_fsoRepo))
        {
          fNewTagType.StartPosition = FormStartPosition.CenterScreen;
          if (fNewTagType.ShowDialog() == DialogResult.OK)
          {
            string newTagTypeName = fNewTagType.NewTagType;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred attempting to add a new Tag Type.", ex);
      }
    }

    private void PopulateTagTypeGrid()
    {
      if(cboProject.Text.Length < 1)
      {
        MessageBox.Show("You must first open a project before you can add a Tag Type.",
                        "File Organizer Error - Error Creating Tag Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      try
      {
        Project p = _fsoRepo.GetProjectByName(cboProject.Text.Trim());
        _fsoRepo.currentProjectID = p.ProjectID;
        var tts = _fsoRepo.PopulateTagTypeGrid();
        gvTagTypes.Rows.Clear();
        foreach(var tagType in tts)
        {
          var tag = tagType.Value;
          gvTagTypes.Rows.Add(tagType.Value.TagTypeID.ToString(), tagType.Value.ProjectName, tagType.Value.TagName);
        }

      }
      catch (Exception ex)
      {
        throw new Exception ("An error occurred attempting to load data to the Doc Type grid.", ex);
      }
    }

    private void AddARootFolder()
    {
      if(cboProject.Text.Length < 1)
      {
        MessageBox.Show("You must first open a project before you can add a Root Folder.",
                        "File Organizer Error - Error Adding a Root Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      try
      {
        Project p = _fsoRepo.GetProjectByName(cboProject.Text.Trim());
        _fsoRepo.currentProjectID = p.ProjectID;
        using (var fNewRootFolder = new frmRootFolder(_fsoRepo))
        {
          fNewRootFolder.StartPosition = FormStartPosition.CenterScreen;
          if(fNewRootFolder.ShowDialog() == DialogResult.OK);
          {
            string newRootFolderName = fNewRootFolder.NewRootFolderName;
            string newRootFolderPath = fNewRootFolder.NewRootFolderPath;
          }
        }

        UpdateRootFolders();
      }
      catch (Exception ex)
      {
        throw new Exception ("An error occurred attempting to load add a root folder to the project.", ex);
      }
    }

    private string GetPathNameFromProjectName(string name)
    {
      var startIndex = name.IndexOf("'", 0);
      var endIndex = name.IndexOf("'", startIndex + 1);
      string path = name.Substring(startIndex + 1, endIndex - startIndex - 1);

      return path;
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 +
                        ex.ToReport(), "File Organizer - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        return;
      }

      int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
      int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

      this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                           Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
      this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);

      _dbSpec = g.GetDbSpec("FileOrg");
      _fsoRepo = new FsoRepository(_dbSpec);
      btnProcessRootFolder.Visible = false;
      var fileLimit = g.AppConfig.GetCI("DefaultFileLimit");
      txtFileCountLimit.Text = fileLimit.ToString();
      InitializeGrid();

      LoadProjects();

      //outputDbSpec = g.GetDbSpec("Output");
      //var searchPaths = g.AppConfig.GetCI("DefaultSearchPath");
      //txtSearchPath.Text = searchPaths;
      //var fileLimit = g.AppConfig.GetCI("DefaultFileLimit");
      //txtFileCountLimit.Text = fileLimit;
    }

    private void InitializeGrid()
    {
      gvDocTypes.Rows.Clear();
      DataGridViewColumn docTypeCol = new DataGridViewTextBoxColumn();
      docTypeCol.Name = "DocTypeID";
      docTypeCol.HeaderText = "Doc Type ID";
      docTypeCol.Width = 100;
      docTypeCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      docTypeCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvDocTypes.Columns.Add(docTypeCol);

      docTypeCol = new DataGridViewTextBoxColumn();
      docTypeCol.Name = "Name";
      docTypeCol.HeaderText = "Project Name";
      docTypeCol.Width = 150;
      docTypeCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      docTypeCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvDocTypes.Columns.Add(docTypeCol);

      docTypeCol = new DataGridViewTextBoxColumn();
      docTypeCol.Name = "DocName";
      docTypeCol.HeaderText = "Doc Name";
      docTypeCol.Width = 150;
      docTypeCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      docTypeCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvDocTypes.Columns.Add(docTypeCol);

      gvTagTypes.Rows.Clear();
      DataGridViewColumn tagTypeCol = new DataGridViewTextBoxColumn();
      tagTypeCol.Name = "TagTypeID";
      tagTypeCol.HeaderText = "Tag Type ID";
      tagTypeCol.Width = 100;
      tagTypeCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      tagTypeCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvTagTypes.Columns.Add(tagTypeCol);

      tagTypeCol = new DataGridViewTextBoxColumn();
      tagTypeCol.Name = "Name";
      tagTypeCol.HeaderText = "Project Name";
      tagTypeCol.Width = 150;
      tagTypeCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      tagTypeCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvTagTypes.Columns.Add(tagTypeCol);

      tagTypeCol = new DataGridViewTextBoxColumn();
      tagTypeCol.Name = "TagName";
      tagTypeCol.HeaderText = "Tag Name";
      tagTypeCol.Width = 150;
      tagTypeCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      tagTypeCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvTagTypes.Columns.Add(tagTypeCol);
    }

    private void UpdateRootFolders()
    {
      Project p = _fsoRepo.GetProjectByName(cboProject.Text.Trim());
      if(p.ProjectID == 0)
      {
        NewProject();
      }
      _fsoRepo.currentProjectID = p.ProjectID;
      _rootFolderSet = _fsoRepo.GetRootFolderSet(cboProject.Text.Trim());
      var dataSource = new List<string>();
      foreach (var rootFolder in _rootFolderSet)
      {
        var root = rootFolder.Value;
        string rootNamePath = root.RootFolderName + " - '" + root.RootFolderPath + "'";
        dataSource.Add(rootNamePath);
      }
      cboRootFolderDropDown.ResetText();
      cboRootFolderDropDown.DataSource = dataSource;
    }

    private void cboRootFolderDropDown_SelectedIndexChanged(object sender,EventArgs e)
    {
      var path = GetPathNameFromProjectName(cboRootFolderDropDown.Text.Trim());

      var exists = _fsoRepo.RootFolderExists(path);
      if(exists)
      {
        btnProcessRootFolder.Visible = true;
        btnProcessRootFolder.Text = "Re-Process Root Folder";
        return;
      }
      btnProcessRootFolder.Visible = true;
      btnProcessRootFolder.Text = "Process Root Folder";
    }
  }
}

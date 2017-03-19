using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.ShareFileApiClient;
using SF = ShareFile.Api.Models;
using Org.WebApi;
using Org.GS;
using Org.GS.Logging;

namespace Org.ShareFileMgr
{
  public partial class frmMain : Form
  {
    private a a;
    private ApiParms _apiParms;
    private SearchParms _searchParms;
    private bool _isDryRun; 
    private Logger _logger;

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
        case "Connect":
          ConnectToShareFile();
          break;

        case "ListFolderContents":
          ListFolderContents();
          break;

        case "ListFolderRecursive":
          ListFolderRecursive();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void ConnectToShareFile()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        using (var fm = new FileManager(_apiParms))
        {
          txtOut.Text = "Authentication successful";
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred attempting to connect to ShareFile." + g.crlf2 + ex.ToReport(),
                        "Share File Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }      
      
      this.Cursor = Cursors.Default;
    }

    private void ListFolderContents()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        using (var fm = new FileManager(_apiParms))
        {
          var taskResult = fm.ListRemoteFolder();

          if (taskResult.TaskResultStatus == TaskResultStatus.Success)
          {
            var files = taskResult.Object as List<ShareFile.Api.Models.File>;
            foreach (var file in files)
            {
              string fileId = file.Id;
              long fileSize = file.FileSizeBytes.HasValue ? file.FileSizeBytes.Value : 0;
              string fileName = file.Name; 
              bool isFolder = file.__type == "ShareFile.Api.Models.Folder";
              if (isFolder)
              {

              }
              else
              {

                //_totalBytes += file.FileSizeBytes.Value;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred attempting to connect to ShareFile." + g.crlf2 + ex.ToReport(),
                        "Share File Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }      
      
      this.Cursor = Cursors.Default;
    }

    private void ListFolderRecursive()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        using (var rootFolder = new SFFolder(_apiParms, _searchParms, _isDryRun))
        {
          rootFolder.GetAllContent(_apiParms.RootFolderId);
          txtOut.Text = rootFolder.GetReport();
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred attempting to list content of folder recursively." + g.crlf2 + ex.ToReport(),
                        "Share File Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }      
      
      this.Cursor = Cursors.Default;
    }

    public void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
                        "Share File Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      _logger = new Logger();

      _apiParms = GetApiParms();
      ckIsDryRun.Checked = g.CI("IsDryRun").ToBoolean(); 
    }


    private ApiParms GetApiParms()
    {
      string cfgPrefix = g.GetCI("ClientName");

      _logger.Log("Client configuration prefix is '" + cfgPrefix + "'.");

      var apiParms = new ApiParms();
      apiParms.HostName = g.CI(cfgPrefix + "HostName");
      apiParms.UserName = g.CI(cfgPrefix + "UserName");
      apiParms.Password = g.CI(cfgPrefix + "Password");
      apiParms.ClientId = g.CI(cfgPrefix + "ClientId");
      apiParms.ClientSecret = g.CI(cfgPrefix + "ClientSecret");
      apiParms.RootFolderId = g.CI(cfgPrefix + "RootFolderId");
      apiParms.RemoteTargetFolder = g.CI(cfgPrefix + "RemoteTargetFolderName");
      apiParms.RemoteArchiveFolder = g.CI(cfgPrefix + "RemoteArchiveFolderName");
      apiParms.SuppressRemoteDelete = false;
      if (g.AppConfig.ContainsKey(cfgPrefix + "SuppressRemoteDelete"))
        apiParms.SuppressRemoteDelete = g.CI(cfgPrefix + "SuppressRemoteDelete").ToBoolean();
      apiParms.ArchiveRemoteFiles = true;
      if (g.AppConfig.ContainsKey(cfgPrefix + "ArchiveRemoteFiles"))
        apiParms.ArchiveRemoteFiles = g.CI(cfgPrefix + "ArchiveRemoteFiles").ToBoolean();

      apiParms.MaxUploadSize = g.CI("MaxUploadSize").ToInt32OrDefault(2000000);
      apiParms.MaxDownloadSize = g.CI("MaxUploadSize").ToInt32OrDefault(2000000);
      apiParms.MaxFilesUpload = g.CI("MaxFilesUpload").ToInt32OrDefault(30);
      apiParms.MaxFilesDownload = g.CI("MaxFilesDownload").ToInt32OrDefault(30);

      apiParms.AllowDuplicateFiles = g.CI("AllowDuplicateFiles").ToBoolean();
      apiParms.FolderFilter = g.CI("FolderFilter");
      if (apiParms.FolderFilter.IsBlank())
        apiParms.FolderFilter = "*.*";
      apiParms.ArchiveFolder = g.CI("ArchiveFolder");
      return apiParms;
    }
  }
}

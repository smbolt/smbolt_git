using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.ShareFileApiClient
{
  public class ApiParms
  {
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string HostName { get; set; }

    public string RootFolderId { get; set; }
    public string RemoteTargetFolder { get; set; }
    public bool AllowDuplicateFiles { get; set; }
    public string ArchiveFolder { get; set; }
    public string FolderFilter { get; set; }
    public int MaxFilesUpload { get; set; }
    public int MaxUploadSize { get; set; }
    public int MaxFilesDownload { get; set; }
    public int MaxDownloadSize { get; set; }
    public string RemoteArchiveFolder { get; set; }
    public bool ArchiveRemoteFiles { get; set; }
    public bool SuppressRemoteDelete { get; set; }

    public ApiParms()
    {
      this.ClientId = String.Empty;
      this.ClientSecret = String.Empty;
      this.UserName = String.Empty;
      this.Password = String.Empty;
      this.HostName = String.Empty;

      this.RootFolderId = String.Empty;
      this.RemoteTargetFolder = String.Empty;
      this.AllowDuplicateFiles = true;
      this.ArchiveFolder = String.Empty;
      this.FolderFilter = "*.*";
      this.MaxFilesUpload = 20; 
      this.MaxUploadSize = 2000000;
      this.MaxFilesDownload = 20; 
      this.MaxDownloadSize = 2000000;
      this.RemoteArchiveFolder = String.Empty;
      this.ArchiveRemoteFiles = true;
      this.SuppressRemoteDelete = false;
    }
  }
}

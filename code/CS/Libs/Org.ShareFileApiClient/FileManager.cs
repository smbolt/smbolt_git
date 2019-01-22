using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using SFModels = ShareFile.Api.Models;
using Org.ShareFileApiClient;
using Org.WebApi;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS;

namespace Org.ShareFileApiClient
{
  public class FileManager : IDisposable
  {
    public event Action<string> ProgressMessage;

    private ApiParms _apiParms;
    private ApiClient _apiClient;
    private bool _isDryRun;
    private Logger _logger;

    private bool _authenticationSuccessful = false;
    public bool AuthenticationSuccessful { get { return _authenticationSuccessful; } }

    public FileManager(ApiParms apiParms, Logger logger, bool isDryRun = false)
    {
      _apiParms = apiParms;
      _logger = logger;
      _apiClient = new ApiClient(isDryRun);
      _apiClient.ProgressMessage += _apiClient_ProgressMessage;
      _isDryRun = isDryRun;

      var authParms = new AuthParms();
      authParms.HostName = _apiParms.HostName;
      authParms.ClientId = _apiParms.ClientId;
      authParms.ClientSecret = _apiParms.ClientSecret;
      authParms.UserName = _apiParms.UserName;
      authParms.Password = _apiParms.Password;

      // implemented a looping authentication retry due to repeated timeout failures

      // hard limit on retries to 10
      if (_apiParms.AuthRetryLimit > 10)
        _apiParms.AuthRetryLimit = 10;

      // hard range limit on waiting between auth tries is 5 seconds to 300 seconds (5 minutes)
      if (_apiParms.AuthRetryWaitIntervalSeconds < 5)
        _apiParms.AuthRetryWaitIntervalSeconds = 5;
      if (_apiParms.AuthRetryWaitIntervalSeconds > 300)
        _apiParms.AuthRetryWaitIntervalSeconds = 300;

      // ensure a minimum of 1 try (to drive at least one spin through loop)
      int remainingAuthRetries = _apiParms.AuthRetryLimit;
      if (remainingAuthRetries < 1)
        remainingAuthRetries = 1;

      _authenticationSuccessful = false;

      while (!_authenticationSuccessful && remainingAuthRetries > 0)
      {
        remainingAuthRetries--;
        _logger.Log("Authentication request in progress - remaining retries is " + remainingAuthRetries.ToString() + ".", 6133); 

        _authenticationSuccessful = _apiClient.Authenticate(authParms);

        if (_authenticationSuccessful)
        {
          _logger.Log("Authentication was successful.", 6132); 
          return;
        }

        _logger.Log("Authentication failed due to timeout - remaining retries is " + remainingAuthRetries.ToString() + ".", 6131);

        // sleep the main thread no more than 5 seconds at a time... 
        int remainingWaitSeconds = _apiParms.AuthRetryWaitIntervalSeconds;
        while (remainingWaitSeconds > 0)
        {
          System.Threading.Thread.Sleep(5000);
          remainingWaitSeconds -= 5;
        }
      }

      if (!_authenticationSuccessful)
      {
        _logger.Log(LogSeverity.SEVR, "Authentication failed due to timeout - all " + _apiParms.AuthRetryLimit.ToString() + " retries have been exhausted.", 6128);

        throw new Exception("The authentication was not successful.  Max authentication retries is " + _apiParms.AuthRetryLimit.ToString() + " and " + 
                            "interval between retries is " + _apiParms.AuthRetryWaitIntervalSeconds.ToString() + " seconds. Remaining retries is " +
                            remainingAuthRetries.ToString() + ".");
      }
    }

    public SFFolder GetFolderById(string folderId, SFFolder parentFolder)
    {
      try
      {
        var taskResult = _apiClient.GetFolderById(_apiParms.RootFolderId);
        if (taskResult.TaskResultStatus == TaskResultStatus.Success)
        {
          ShareFile.Api.Models.Item folder = taskResult.Object as ShareFile.Api.Models.Item;
          var sfFolder = new SFFolder(parentFolder);
          sfFolder.Id = folder.Id;
          sfFolder.Name = folder.Name;
          return sfFolder; 
        }

        return null; 
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in GetFolderById.", ex);
      }
    }

    public TaskResult ListRemoteFolder()
    {
      TaskResult taskResult = new TaskResult("ListRemoteFolder");

      try
      {
        taskResult = _apiClient.GetFolderById(_apiParms.RootFolderId, _apiParms.RemoteTargetFolder);
        if (taskResult.TaskResultStatus == TaskResultStatus.Success)
        {
          ShareFile.Api.Models.Item folder = taskResult.Object as ShareFile.Api.Models.Item;
          return _apiClient.GetFolderContents(folder.Id);
        }
        else
        {
          return taskResult;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in ListRemoteFolder.", ex);
      }
    }

    public SFFolderContent GetFolderContent(SFFolder parentFolder)
    {
      var fc = new SFFolderContent();

      try
      {
        var taskResult = _apiClient.GetFolderContents(parentFolder.Id);
        if (taskResult.TaskResultStatus == TaskResultStatus.Success)
        {
          var files = taskResult.Object as List<ShareFile.Api.Models.File>;
          foreach (var file in files)
          {
            bool isFolder = file.__type == "ShareFile.Api.Models.Folder";
            if (isFolder)
            {
              var sfFolder = new SFFolder(parentFolder);
              sfFolder.Name = file.Name;
              sfFolder.Id = file.Id;
              int seq = 1;
              string folderName = sfFolder.Name;
              while (fc.SFFolderSet.ContainsKey(folderName))
              {
                folderName = sfFolder.Name + "(" + seq.ToString() + ")";
                seq++;
              }
              fc.SFFolderSet.Add(folderName, sfFolder); 
            }
            else
            {
              var sfFile = new SFFile(parentFolder);
              sfFile.Name = file.Name;
              sfFile.Id = file.Id;
              sfFile.Size = file.FileSizeBytes.HasValue ? file.FileSizeBytes.Value : 0;
              int seq = 0;
              string fileName = file.Name;
              while(fc.SFFileSet.ContainsKey(fileName))
              {
                fileName = file.Name + "(" + seq.ToString() + ")";
                seq++;
              }
              fc.SFFileSet.Add(fileName, sfFile); 
            }
          }

          return fc;
        }
        
        throw new Exception("The TaskResult returned from the apiClient on the GetFolderContents call had a TaskResultStatus of '" + taskResult.TaskResultStatus.ToString() + "." +
                            "The NotificationMessage property value is '" + taskResult.NotificationMessage + "'."); 
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the folder contents of folder '" + parentFolder.Name + "'.", ex);
      }
    }

    public TaskResult ClearRemoteFolder()
    {
      TaskResult taskResult = new TaskResult("ClearRemoteFolder");

      try
      {
        taskResult = _apiClient.GetFolderById(_apiParms.RootFolderId, _apiParms.RemoteTargetFolder);
        if (taskResult.TaskResultStatus == TaskResultStatus.Success)
        {
          ShareFile.Api.Models.Item folder = taskResult.Object as ShareFile.Api.Models.Item;
          return _apiClient.ClearFolderContents(folder.Id);
        }
        else
        {
          return taskResult;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in ClearRemoteFolder.", ex);
      }
    }

    public TaskResult DeleteRemoteFile(string remoteFileName)
    {
      TaskResult taskResult = new TaskResult("DeleteRemoteFile");

      try
      {
        taskResult = _apiClient.GetFolderById(_apiParms.RootFolderId, _apiParms.RemoteTargetFolder);
        if (taskResult.TaskResultStatus == TaskResultStatus.Success)
        {
          ShareFile.Api.Models.Item folder = taskResult.Object as ShareFile.Api.Models.Item;
          return _apiClient.DeleteRemoteFile(folder.Id, remoteFileName);
        }
        else
        {
          return taskResult;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in DeleteRemoteFile.", ex);
      }
    }

    public TaskResult UploadSingleFile(string filePath)
    {
      TaskResult taskResult = new TaskResult("UploadSingleFile");

      try
      {
        var subTaskResult = _apiClient.GetFolderById(_apiParms.RootFolderId, _apiParms.RemoteTargetFolder);
        if (subTaskResult.TaskResultStatus == TaskResultStatus.Success)
        {
          ShareFile.Api.Models.Item folder = subTaskResult.Object as ShareFile.Api.Models.Item;
          taskResult = _apiClient.UploadFile(folder.Id, filePath, _apiParms.AllowDuplicateFiles, "UploadSingleFile");
          if (_apiParms.ArchiveFolder.IsNotBlank())
          {
            if (taskResult.TaskResultStatus == TaskResultStatus.Success)
            {
              string fileName = Path.GetFileName(filePath);
              string archiveFullPath = _apiParms.ArchiveFolder + @"\" + fileName;
              int seq = 1;
              while (File.Exists(archiveFullPath))
              {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(archiveFullPath);
                string ext = Path.GetExtension(archiveFullPath);
                archiveFullPath = _apiParms.ArchiveFolder + @"\" + fileNameWithoutExtension + "(" + seq.ToString() + ")" + ext;
              }
              if (!_isDryRun)
                File.Move(filePath, archiveFullPath);
            }
          }
          else
          {
            if (!_isDryRun)
              File.Delete(filePath);
          }
          return taskResult;
        }
        else
        {
          return subTaskResult;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in UploadSingleFile.", ex);
      }
    }

    public TaskResult UploadByFolder(string localFolder)
    {
      TaskResult taskResult = new TaskResult("UploadByFolder");

      try
      {
        var subTaskResult = _apiClient.GetFolderById(_apiParms.RootFolderId, _apiParms.RemoteTargetFolder);
        if (subTaskResult.TaskResultStatus == TaskResultStatus.Success)
        {
          ShareFile.Api.Models.Item folder = subTaskResult.Object as ShareFile.Api.Models.Item;
          List<string> filePaths = Directory.GetFiles(localFolder, _apiParms.FolderFilter).ToList();

          SendMessageToHost("Files to upload: " + filePaths.Count.ToString() + "."); 

          foreach (string filePath in filePaths)
          {
            FileInfo fi = new FileInfo(filePath);
            var fileTaskResult = new TaskResult("FileUpload"); 
            if (fi.Length <= _apiParms.MaxUploadSize)
            {
              SendMessageToHost("Uploading file: " + fi.Name + " (" + fi.Length.ToString("###,###,##0") + " bytes)");
              subTaskResult = _apiClient.UploadFile(folder.Id, filePath, _apiParms.AllowDuplicateFiles, "UploadByFolder");
              subTaskResult.Object = fi;
              if (_apiParms.ArchiveFolder.IsNotBlank())
              {
                if (subTaskResult.TaskResultStatus == TaskResultStatus.Success)
                {
                  string fileName = Path.GetFileName(filePath);
                  string archiveFullPath = _apiParms.ArchiveFolder + @"\" + fileName;
                  int seq = 1;
                  while (File.Exists(archiveFullPath))
                  {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(archiveFullPath);
                    string ext = Path.GetExtension(archiveFullPath);
                    archiveFullPath = _apiParms.ArchiveFolder + @"\" + fileNameWithoutExtension + "(" + seq.ToString() + ")" + ext;
                  }
                  if (!_isDryRun)
                    File.Move(filePath, archiveFullPath);
                }
              }
              else
              {
                if (!_isDryRun)
                  File.Delete(filePath); 
              }
              taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, subTaskResult); 
            }
            else
            {
              fileTaskResult.TaskResultStatus = TaskResultStatus.Failed;
              fileTaskResult.Message = "File '" + filePath + "' contains " + fi.Length.ToString("###,###,##0") + " bytes " +
                                       "which exceeds the file size limit of " + _apiParms.MaxUploadSize.ToString("###,###,##0") + " bytes.";
              fileTaskResult.Object = fi;
              fileTaskResult.Code = 144;
              taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, fileTaskResult); 
            }
          }

          int failedCount = 0;
          int successCount = 0; 
          foreach (var childTaskResult in taskResult.TaskResultSet.Values)
          {
            if (childTaskResult.TaskResultStatus == TaskResultStatus.Success)
              successCount++;
            else
              failedCount++; 
          }

          if (failedCount == 0)
            taskResult.TaskResultStatus = TaskResultStatus.Success;
          else
            if (successCount == 0)
              taskResult.TaskResultStatus = TaskResultStatus.Failed;
            else
              taskResult.TaskResultStatus = TaskResultStatus.Warning;

          taskResult.EndDateTime = DateTime.Now;
          return taskResult;
        }
        else
        {
          return subTaskResult;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in UploadByFolder.", ex);
      }
    }

    public TaskResult DownloadFiles(string outputFolder, string cfgDownloadFileName, FileProcessingOptions fpOpts, string remoteFileName)
    {
      TaskResult taskResult = new TaskResult("DownloadFiles");

      int downloadCount = 0;
      int maxDownloadCount = _apiParms.MaxFilesDownload;
      if (maxDownloadCount == 0)
        maxDownloadCount = 999;

      bool fileSizeExceeded = false;
      
      try
      {
        string specifiedFileName = GetSpecifiedFileName(cfgDownloadFileName); 

        string archiveFolderId = String.Empty;        

        if (_apiParms.ArchiveRemoteFiles)
        {
          var archiveFolderTaskResult = _apiClient.GetFolderById(_apiParms.RootFolderId, _apiParms.RemoteArchiveFolder);
          if (archiveFolderTaskResult.TaskResultStatus == TaskResultStatus.Success)
          {
            ShareFile.Api.Models.Item folder = archiveFolderTaskResult.Object as ShareFile.Api.Models.Item;
            archiveFolderId = folder.Id;
          }
          else
          {
            // didn't find folder or an exception occurred...
            // may want to augment message to have better higher-level context (Code is 172), maybe override... 
            return archiveFolderTaskResult;
          }
        }

        var subTaskResult = _apiClient.GetFolderById(_apiParms.RootFolderId, _apiParms.RemoteTargetFolder);
        if (subTaskResult.TaskResultStatus == TaskResultStatus.Success)
        {          
          ShareFile.Api.Models.Item folder = subTaskResult.Object as ShareFile.Api.Models.Item;
          subTaskResult = _apiClient.GetFolderContents(folder.Id);
          if (subTaskResult.TaskResultStatus == TaskResultStatus.Success)
          {
            var fileList = subTaskResult.Object as List<ShareFile.Api.Models.File>;

            int totalFilesToDownload = 0;
            // first count up the total number of files to see if it exceeds the max allowed
            foreach (var file in fileList)
            {
              if (file.__type == "ShareFile.Api.Models.File")
              {
                if (remoteFileName.IsNotBlank())
                {
                  if (file.Name.ToLower() == remoteFileName.ToLower())
                    totalFilesToDownload++;
                }
                else
                  totalFilesToDownload++;
              }
            }

            // limit file count to max allowed
            if (totalFilesToDownload > _apiParms.MaxFilesDownload)
              totalFilesToDownload = _apiParms.MaxFilesDownload;

            foreach (var file in fileList)
            {
              if (file.__type == "ShareFile.Api.Models.File") // ignore folders
              {
                if (remoteFileName.IsNotBlank())
                {
                  if (file.Name.ToLower() != remoteFileName.ToLower())
                    continue;
                }

                TaskResult downloadTaskResult = new TaskResult("DownloadFile");

                // have we already exceeded the maximum count of files to download?
                // this is a Warning... 
                if (downloadCount >= maxDownloadCount)
                {
                  downloadTaskResult.TaskResultStatus = TaskResultStatus.Warning;
                  downloadTaskResult.EndDateTime = DateTime.Now;
                  downloadTaskResult.Message = "The file download operation has reached the maximum number of files that can be " +
                                               "downloaded in a single operation which is " + maxDownloadCount.ToString("###,###,##0") + ".";
                  taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, downloadTaskResult);
                  taskResult.TaskResultStatus = TaskResultStatus.Warning;
                  taskResult.EndDateTime = DateTime.Now;
                  return taskResult;
                }

                // commit to downloading...
                downloadCount++;

                SendMessageToHost("Downloading file " + downloadCount.ToString() + " of " + totalFilesToDownload.ToString()); 

                string fileId = file.Id;
                string fileName = file.FileName;
                long fileBytes = file.FileSizeBytes.HasValue ? file.FileSizeBytes.Value : 0;
                string downloadFileName = outputFolder + @"\" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + "-" + fileName;

                if (specifiedFileName.IsNotBlank())
                  downloadFileName = outputFolder + @"\" + specifiedFileName;

                if (fpOpts == FileProcessingOptions.ThrowException && File.Exists(downloadFileName))
                  throw new Exception("A file with the name '" + downloadFileName + "' already exists and this program is configured to throw an exception " +
                                      "when this condition exists.");

                // if configured to skip duplicate file names in local file system
                // and file already exists, skip the download.
                // currently causes TaskResultStatus.Failed, just for this one file... should it be a Warning instead
                if (fpOpts == FileProcessingOptions.Skip)
                {
                  if (File.Exists(downloadFileName))
                  {
                    downloadTaskResult.TaskResultStatus = TaskResultStatus.Failed;
                    downloadTaskResult.EndDateTime = DateTime.Now;
                    downloadTaskResult.Message = "Local file name '" + downloadFileName + "' already exists and this program is configured to " + 
                                                 "not overwrite existing files and not update the file name with a sequence number.  The remote file " +
                                                 "will not be downloaded.";
                    taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, downloadTaskResult);
                    continue;
                  }
                }

                // if it's ok to append a sequence number of duplicate local file names (to enable download)
                // then add a sequence number to any duplicates - no error or warning
                if (fpOpts == FileProcessingOptions.SequenceDuplicates)
                {
                  int seq = 1;
                  string seqFileName = String.Empty;
                  while(File.Exists(downloadFileName))
                  {
                    string workFileName = Path.GetFileName(downloadFileName);
                    string folderName = Path.GetDirectoryName(downloadFileName);
                    string ext = Path.GetExtension(downloadFileName);

                    if (workFileName.EndsWith(")"))
                    {
                      seq++;
                      int op = workFileName.LastIndexOf("(");
                      seqFileName = workFileName.Substring(0, op) + "(" + seq.ToString() + ")";
                      downloadFileName = folderName + @"\" + seqFileName + ext;
                    }
                    else
                    {
                      seqFileName = Path.GetFileNameWithoutExtension(downloadFileName) + "(" + seq.ToString() + ")";
                      downloadFileName = folderName + @"\" + seqFileName + ext;
                    }
                  }
                }   

                // if the file is too big - this is an error
                if (fileBytes > _apiParms.MaxDownloadSize)
                {
                  fileSizeExceeded = true;
                  downloadTaskResult.TaskResultStatus = TaskResultStatus.Failed;
                  downloadTaskResult.EndDateTime = DateTime.Now;
                  downloadTaskResult.Message = "File '" + fileName + "' contains " + fileBytes.ToString("###,###,##0") + " bytes which is " +
                                               "larger than the maximum size allowed for download which is " + _apiParms.MaxDownloadSize.ToString("###,###,##0") + ".";
                  taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, downloadTaskResult);
                  continue;
                }

                bool overwritingFile = false;
                if (fpOpts == FileProcessingOptions.OverwriteFile && File.Exists(downloadFileName))
                  overwritingFile = true;

                downloadTaskResult = _apiClient.DownloadFile(fileId, downloadFileName, archiveFolderId, _apiParms.ArchiveRemoteFiles, _apiParms.SuppressRemoteDelete);
                if (downloadTaskResult.TaskResultStatus == TaskResultStatus.Success)
                {
                  if (overwritingFile)
                  {
                    downloadTaskResult.TaskResultStatus = TaskResultStatus.Warning;
                    downloadTaskResult.Code = 5;
                    downloadTaskResult.Message = "Existing file by the same name was overwritten.";
                  }
                  FileInfo fi = new FileInfo(downloadFileName);
                  downloadTaskResult.Object = fi;
                  taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, downloadTaskResult);
                }
                else  // exception occurred when downloading file (or re-uploading from local to remote archive - to copy to archive)
                {
                  taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, downloadTaskResult);
                }
              }
            }

            // if a specific file was requested but it was not found
            if (remoteFileName.IsNotBlank() && totalFilesToDownload == 0)
            {
              taskResult.Code = 4;
              taskResult.TaskResultStatus = TaskResultStatus.Warning;
              taskResult.Message = "Remote file '" + remoteFileName + "' does not exist."; 
            }
            else
            {
              if (fileSizeExceeded)
                taskResult.TaskResultStatus = TaskResultStatus.Warning;
              else
                taskResult.TaskResultStatus = TaskResultStatus.Success;
            }

            taskResult.EndDateTime = DateTime.Now;
            return taskResult;
          }
          else // failed getting folder contents (probably just an exception, i.e. folder not found; empty folder is ok; Code is 150)
          {
            return subTaskResult;
          }
        }
        else // failed to find remote target folder or exception occurred trying (Code is 172)
        {
          return subTaskResult;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in DownloadFiles.", ex);
      }
    }

    private string GetSpecifiedFileName(string specFileName)
    {
      if (specFileName.IsBlank())
        return String.Empty;

      if (specFileName.Contains("@"))
      {
        if (specFileName.Contains("@CCYYMMDD@"))
        {
          string ccyymmdd = DateTime.Now.ToString("yyyyMMdd");
          return specFileName.Replace("@CCYYMMDD@", ccyymmdd); 
        }

        throw new Exception("Handling of file name pattern '" + specFileName + "' not yet implemented."); 
      }

      return specFileName;
    }

    private void SendMessageToHost(string message)
    {
      if (this.ProgressMessage != null)
      {
        ProgressMessage(message); 
      }
    }

    private void _apiClient_ProgressMessage(string message)
    {
      SendMessageToHost(message);
    }

    public void Dispose()
    {

    }
  }
}

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Org.TP.Concrete;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS;
using Org.ShareFileApiClient;
using sfModels = ShareFile.Api.Models;

namespace Org.ShareFile.Tasks
{
  public class ShareFileUtility : TaskProcessorBase
  {
    public override int EntityId {
      get {
        return 515;
      }
    }
    private Logger _logger;
    private ApiParms _apiParms;
    private int _fileCount = 0;
    private int _folderCount = 0;
    private long _totalBytes = 0;

    public override async Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();
      CheckContinue = checkContinue;
      _logger = new Logger();
      _logger.ModuleId = g.AppInfo.ModuleCode;
      _apiParms = new ApiParms();

      _logger.Log("ShareFileUtility task processor is starting up.", 6129);

      try
      {
        return await Task.Run<TaskResult>(() =>
        {
          this.Initialize();

          taskResult.IsDryRun = IsDryRun;

          var loginParms = base.GetParmValue("ShareFileLoginParms") as Dictionary<string, string>;

          if (!loginParms.ContainsKey("HostName") || !loginParms.ContainsKey("UserName") || !loginParms.ContainsKey("Password") ||
              !loginParms.ContainsKey("ClientId") || !loginParms.ContainsKey("ClientSecret"))
            return taskResult.Failed("ShareFileLoginParms did not contain all required parameters: HostName, UserName, Password, ClientId, ClientSecret");

          _apiParms.HostName = loginParms["HostName"];
          _apiParms.UserName = loginParms["UserName"];
          _apiParms.Password = loginParms["Password"];
          _apiParms.ClientId = loginParms["ClientId"];
          _apiParms.ClientSecret = loginParms["ClientSecret"];
          _apiParms.RootFolderId = base.GetParmValue("RootFolderId").ToString();
          if (base.ParmExists("AuthRetryLimit"))
            _apiParms.AuthRetryLimit = base.GetParmValue("AuthRetryLimit").ToInt32OrDefault(0);
          if (base.ParmExists("AuthRetryWaitInterval"))
            _apiParms.AuthRetryWaitIntervalSeconds = base.GetParmValue("AuthRetryWaitInterval").ToInt32OrDefault(0);

          if (base.ParmExists("RemoteTargetFolder"))
            _apiParms.RemoteTargetFolder = base.GetParmValue("RemoteTargetFolder").ToString();

          _logger.Log("The ShareFileUtility is processing task named '" + taskResult.TaskName + "'.", 6130);

          switch (taskResult.TaskName)
          {
            case "ListRemoteFolder":
              return ListRemoteFolder(taskResult);

            case "UploadSingleFile":
              if (base.ParmExists("ArchiveFolder"))
                _apiParms.ArchiveFolder = base.GetParmValue("ArchiveFolder").ToString();
              if (base.ParmExists("MaxUploadSize"))
                _apiParms.MaxUploadSize = base.GetParmValue("MaxUploadSize").ToInt32();
              if (base.ParmExists("AllowDuplicateFiles"))
                _apiParms.AllowDuplicateFiles = base.GetParmValue("AllowDuplicateFiles").ToBoolean();
              if (base.ParmExists("MaxFilesUpload"))
                _apiParms.MaxFilesUpload = base.GetParmValue("MaxFilesUpload").ToInt32();
              if (base.ParmExists("MaxUploadSize"))
                _apiParms.MaxUploadSize = base.GetParmValue("MaxUploadSize").ToInt32();

              string uploadFilePath = base.GetParmValue("UploadFilePath").ToString();
              return UploadSingleFile(taskResult, uploadFilePath);

            case "UploadByFolder":
              if (base.ParmExists("ArchiveFolder"))
                _apiParms.ArchiveFolder = base.GetParmValue("ArchiveFolder").ToString();
              if (base.ParmExists("MaxUploadSize"))
                _apiParms.MaxUploadSize = base.GetParmValue("MaxUploadSize").ToInt32();
              if (base.ParmExists("AllowDuplicateFiles"))
                _apiParms.AllowDuplicateFiles = base.GetParmValue("AllowDuplicateFiles").ToBoolean();
              if (base.ParmExists("MaxFilesUpload"))
                _apiParms.MaxFilesUpload = base.GetParmValue("MaxFilesUpload").ToInt32();
              if (base.ParmExists("MaxUploadSize"))
                _apiParms.MaxUploadSize = base.GetParmValue("MaxUploadSize").ToInt32();

              string uploadFolderPath = base.GetParmValue("UploadFolderPath").ToString();
              return UploadByFolder(taskResult, uploadFolderPath);

            case "DeleteRemoteFile":
              string deleteFileName = base.GetParmValue("DeleteFileName").ToString();
              return DeleteRemoteFile(taskResult, deleteFileName);

            case "ClearRemoteFolder":
              return ClearRemoteFolder(taskResult);

            case "DailyDownloadLaScadaFiles":
              string downloadFileName = String.Empty;
              string remoteFileName = String.Empty;
              if (base.ParmExists("DownloadFileName"))
                downloadFileName = base.GetParmValue("DownloadFileName").ToString();
              if (base.ParmExists("RemoteFileName"))
                remoteFileName = base.GetParmValue("RemoteFileName").ToString();
              if (base.ParmExists("RemoteArchiveFolder"))
                _apiParms.RemoteArchiveFolder = base.GetParmValue("RemoteArchiveFolder").ToString();
              if (base.ParmExists("ArchiveRemoteFiles"))
                _apiParms.ArchiveRemoteFiles = base.GetParmValue("ArchiveRemoteFiles").ToBoolean();
              if (base.ParmExists("SuppressRemoteDelete"))
                _apiParms.SuppressRemoteDelete = base.GetParmValue("SuppressRemoteDelete").ToBoolean();
              if (base.ParmExists("MaxFilesDownload"))
                _apiParms.MaxFilesDownload = base.GetParmValue("MaxFilesDownload").ToInt32();
              if (base.ParmExists("MaxDownloadSize"))
                _apiParms.MaxDownloadSize = base.GetParmValue("MaxDownloadSize").ToInt32();

              var fileProcessingOption = base.GetParmValue("FileProcessingOption").ToEnum<FileProcessingOptions>(FileProcessingOptions.None);
              string outputFolderPath = base.GetParmValue("OutputFolderPath").ToString();
              return DownloadFiles(taskResult, outputFolderPath, downloadFileName, fileProcessingOption, remoteFileName);

            default:
              return taskResult.Failed("ShareFileUtility does not recognize the requested Task Name '" + taskResult.TaskName + "'.");
          }
        });
      }
      catch (Exception ex)
      {
        _logger.Log(LogSeverity.SEVR, "An exception occurred in the ShareFileUtility task processor.", 6127, ex);
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing." + g.crlf + ex.ToReport(), ex);
      }
    }

    private TaskResult ListRemoteFolder(TaskResult taskResult)
    {
      TaskResult listRemoteFolderTR = new TaskResult("ListRemoteFolder");

      try
      {
        StringBuilder sb = new StringBuilder();

        using (var fm = new FileManager(_apiParms, _logger))
        {
          string remoteFolder = _apiParms.RootFolderId;
          if (_apiParms.RemoteTargetFolder.IsNotBlank())
            remoteFolder += "/" + _apiParms.RemoteTargetFolder;
          sb.Append("Remote folder is '" + remoteFolder + "'");

          listRemoteFolderTR = fm.ListRemoteFolder();
          switch (listRemoteFolderTR.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              List<sfModels.File> files = (List<sfModels.File>)listRemoteFolderTR.Object;
              sb.Append(g.crlf + "Folder Contents (" + files.Count.ToString() + " files)");

              _fileCount = 0;
              _folderCount = 0;
              foreach (var file in files)
              {
                bool isFolder = file.__type == "ShareFile.Api.Models.Folder";
                string folderIndicator = " ";

                if (isFolder)
                {
                  _folderCount++;
                  folderIndicator = " (FOLDER) ";
                }
                else
                {
                  _fileCount++;
                  _totalBytes += file.FileSizeBytes.Value;
                }

                int totalCount = _fileCount + _folderCount;

                sb.Append(g.crlf + totalCount.ToString("000") + folderIndicator + file.FileName + " (" + file.FileSizeBytes.Value.ToString("###,###,##0") + " bytes )");
              }
              sb.Append(g.crlf2 + "Operation completed in " + listRemoteFolderTR.DurationString + " seconds");
              return taskResult.Success(sb.ToString());

            default:
              taskResult.TaskResultStatus = listRemoteFolderTR.TaskResultStatus;
              taskResult.Message = listRemoteFolderTR.Message;
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred running 'ListRemoteFolder'.", ex);
      }
    }

    private TaskResult UploadSingleFile(TaskResult taskResult, string fileName)
    {
      TaskResult uploadSingleFileTR = new TaskResult("UploadSingleFile");

      try
      {
        StringBuilder sb = new StringBuilder();

        if (_apiParms.ArchiveFolder.IsNotBlank())
          if (!Directory.Exists(_apiParms.ArchiveFolder))
            try
            {
              Directory.CreateDirectory(_apiParms.ArchiveFolder);
            }
            catch (Exception ex)
            {
              return taskResult.Failed(DryRunIndicator + "An exception occurred attempting to create the ArchiveFolder '" + _apiParms.ArchiveFolder + "'.", ex);
            }

        string remoteFolder = _apiParms.RootFolderId;
        if (_apiParms.RemoteTargetFolder.IsNotBlank())
          remoteFolder += "/" + _apiParms.RemoteTargetFolder;
        sb.Append("Remote folder is '" + remoteFolder + "'");

        FileInfo fi = new FileInfo(fileName);
        _fileCount++;

        sb.Append(g.crlf + _fileCount.ToString("000") + " Name: " + fileName +
                  g.crlf + _fileCount.ToString("000") + " Size: " + fi.Length.ToString("###,###,##0"));

        if (fi.Length > _apiParms.MaxUploadSize)
        {
          return taskResult.Failed(DryRunIndicator + _fileCount.ToString("000") + " ERROR - File size of " + fi.Length.ToString("###,###,##0") + " bytes exceeds the maximum size allowed of " +
                                   _apiParms.MaxUploadSize.ToString("###,###,##0") + " bytes.");
        }

        using (var fm = new FileManager(_apiParms, _logger, IsDryRun))
        {
          uploadSingleFileTR = fm.UploadSingleFile(fileName);

          switch (uploadSingleFileTR.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              _totalBytes += fi.Length;
              sb.Append(g.crlf + _fileCount.ToString("000") + " Result: Upload successful - duration was " + taskResult.DurationString + " seconds");
              return taskResult.Success(DryRunIndicator + sb.ToString());

            default:
              taskResult.TaskResultStatus = uploadSingleFileTR.TaskResultStatus;
              taskResult.Message = DryRunIndicator + uploadSingleFileTR.Message;
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        return taskResult.Failed(DryRunIndicator + _fileCount.ToString("000") + " " + "An exception occurred attempting to upload a single file '" + fileName + "'.", ex);
      }
    }

    private TaskResult UploadByFolder(TaskResult taskResult, string folderPath)
    {
      TaskResult uploadByFolderTR = new TaskResult("UploadByFolder");

      try
      {
        StringBuilder sb = new StringBuilder();

        if (_apiParms.ArchiveFolder.IsNotBlank())
        {
          if (!Directory.Exists(_apiParms.ArchiveFolder))
          {
            try {
              Directory.CreateDirectory(_apiParms.ArchiveFolder);
            }
            catch (Exception ex) {
              return taskResult.Failed(DryRunIndicator + "An exception occurred attempting to create the ArchiveFolder '" + _apiParms.ArchiveFolder + "'.", ex);
            }
          }
        }

        string remoteFolder = _apiParms.RootFolderId;
        if (_apiParms.RemoteTargetFolder.IsNotBlank())
          remoteFolder += "/" + _apiParms.RemoteTargetFolder;

        sb.Append("Remote folder is '" + remoteFolder + "'." + g.crlf);

        using (var fm = new FileManager(_apiParms, _logger, IsDryRun))
        {
          uploadByFolderTR = fm.UploadByFolder(folderPath);
          _fileCount = 0;

          switch (uploadByFolderTR.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              foreach (var childTaskResult in uploadByFolderTR.TaskResultSet.Values)
              {
                FileInfo fi = (FileInfo)childTaskResult.Object;
                _fileCount++;
                _totalBytes += fi.Length;

                sb.Append(g.crlf + _fileCount.ToString("000") + " Name: " + fi.FullName +
                          g.crlf + _fileCount.ToString("000") + " Size: " + fi.Length.ToString("###,###,##0") +
                          g.crlf + _fileCount.ToString("000") + " Result: Upload successful - duration was " + childTaskResult.DurationString + " seconds");
              }
              return taskResult.Success(DryRunIndicator + sb.ToString());

            case TaskResultStatus.Warning:
              foreach (var childTaskResult in uploadByFolderTR.TaskResultSet.Values)
              {
                FileInfo fi = (FileInfo)childTaskResult.Object;
                _fileCount++;
                _totalBytes += fi.Length;

                string result = "Upload successful - duration was " + childTaskResult.DurationString + " seconds";
                if (childTaskResult.TaskResultStatus != TaskResultStatus.Success)
                  result = "*** FAILED *** " + childTaskResult.Message + " (code " + childTaskResult.Code + ")";

                sb.Append(g.crlf + _fileCount.ToString("000") + " Name: " + fi.FullName +
                          g.crlf + _fileCount.ToString("000") + " Size: " + fi.Length.ToString("###,###,##0") +
                          g.crlf + _fileCount.ToString("000") + " Result: " + result);

              }
              return taskResult.Warning(DryRunIndicator + sb.ToString());

            default:
              taskResult.TaskResultStatus = uploadByFolderTR.TaskResultStatus;
              taskResult.Message = DryRunIndicator + uploadByFolderTR.Message;
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        return taskResult.Failed(DryRunIndicator + "An exception occurred attempting to upload folder of files '" + folderPath + "'.", ex);
      }
    }

    private TaskResult DeleteRemoteFile(TaskResult taskResult, string remoteFileName)
    {
      TaskResult deleteRemoteFileTR = new TaskResult("DeleteRemoteFile");

      try
      {
        StringBuilder sb = new StringBuilder();

        using (var fm = new FileManager(_apiParms, _logger, IsDryRun))
        {
          string remoteFolder = _apiParms.RootFolderId;
          if (_apiParms.RemoteTargetFolder.IsNotBlank())
            remoteFolder += "/" + _apiParms.RemoteTargetFolder;
          sb.Append("Remote folder is '" + remoteFolder + "'" + g.crlf);

          deleteRemoteFileTR = fm.DeleteRemoteFile(remoteFileName);
          switch (deleteRemoteFileTR.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              List<sfModels.File> files = (List<sfModels.File>)deleteRemoteFileTR.Object;

              _fileCount = 0;
              foreach (var file in files)
              {
                _fileCount++;
                _totalBytes += file.FileSizeBytes.Value;
                sb.Append(g.crlf + "Remote file deleted - " + file.FileName + " (" + file.FileSizeBytes.Value.ToString("###,###,##0") + " bytes )");
              }
              sb.Append(g.crlf + "Operation completed in " + deleteRemoteFileTR.DurationString + " seconds");
              return taskResult.Success(DryRunIndicator + sb.ToString());

            case TaskResultStatus.Warning:
              sb.Append("Remote file '" + remoteFileName + "' not found.");
              return taskResult.Warning(DryRunIndicator + sb.ToString());

            default:
              taskResult.TaskResultStatus = deleteRemoteFileTR.TaskResultStatus;
              taskResult.Message = DryRunIndicator + deleteRemoteFileTR.Message;
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        return taskResult.Failed(DryRunIndicator + "Error occurred running DeleteRemoteFile.", ex);
      }
    }

    private TaskResult ClearRemoteFolder(TaskResult taskResult)
    {
      TaskResult clearRemoteFolderTR = new TaskResult("ClearRemoteFolder");

      try
      {
        StringBuilder sb = new StringBuilder();

        using (var fm = new FileManager(_apiParms, _logger, IsDryRun))
        {
          string remoteFolder = _apiParms.RootFolderId;
          if (_apiParms.RemoteTargetFolder.IsNotBlank())
            remoteFolder += "/" + _apiParms.RemoteTargetFolder;
          sb.Append("Remote folder is '" + remoteFolder + "'");

          clearRemoteFolderTR = fm.ClearRemoteFolder();
          switch (clearRemoteFolderTR.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              List<sfModels.File> files = (List<sfModels.File>)clearRemoteFolderTR.Object;
              sb.Append(g.crlf2 + "Folder Contents (" + files.Count.ToString() + " files) deleted" + g.crlf);

              _fileCount = 0;
              _folderCount = 0;
              foreach (var file in files)
              {
                bool isFolder = file.__type == "ShareFile.Api.Models.Folder";
                string folderIndicator = " *** DELETED *** ";

                if (isFolder)
                {
                  _folderCount++;
                  folderIndicator = " (FOLDER) ";
                }
                else
                {
                  _fileCount++;
                  _totalBytes += file.FileSizeBytes.Value;
                }

                int totalCount = _fileCount + _folderCount;

                sb.Append(g.crlf + totalCount.ToString("000") + folderIndicator + file.FileName + " (" + file.FileSizeBytes.Value.ToString("###,###,##0") + " bytes )");
              }

              sb.Append(g.crlf + "Operation completed in " + clearRemoteFolderTR.DurationString + " seconds");

              return taskResult.Success(DryRunIndicator + sb.ToString());

            default:
              taskResult.TaskResultStatus = clearRemoteFolderTR.TaskResultStatus;
              taskResult.Message = DryRunIndicator + clearRemoteFolderTR.Message;
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        return taskResult.Failed(DryRunIndicator + "An exception occurred running 'ClearRemoteFolder'.", ex);
      }
    }

    private TaskResult DownloadFiles(TaskResult taskResult, string outputFolderPath, string downloadFileName, FileProcessingOptions fileProcessingOption, string remoteFileName)
    {
      TaskResult downloadFilesTR = new TaskResult("DownloadFiles");

      try
      {
        StringBuilder sb = new StringBuilder();

        string remoteFolder = _apiParms.RootFolderId;
        if (_apiParms.RemoteTargetFolder.IsNotBlank())
          remoteFolder += "/" + _apiParms.RemoteTargetFolder;

        sb.Append("Remote folder is '" + remoteFolder + "'");

        _fileCount = 0;
        _totalBytes = 0;

        using (var fm = new FileManager(_apiParms, _logger, IsDryRun))
        {
          downloadFilesTR = fm.DownloadFiles(outputFolderPath, downloadFileName, fileProcessingOption, remoteFileName);
          taskResult.TaskResultStatus = downloadFilesTR.TaskResultStatus;

          switch (downloadFilesTR.TaskResultStatus)
          {
            case TaskResultStatus.Success:   // download, archive and delete successful (archive and delete are configurable)
            case TaskResultStatus.Warning:   // download successful, but problem with archive and/or delete
              StringBuilder summaryMessage = new StringBuilder();

              // if specific file was requested but not found
              if (downloadFilesTR.Code == 4)
              {
                taskResult.Message = DryRunIndicator + downloadFilesTR.Message;
                return taskResult;
              }

              if (downloadFilesTR.TaskResultSet.Count() == 0)
                taskResult.NoWorkDone = true;

              foreach (var childTaskResult in downloadFilesTR.TaskResultSet.Values)
              {
                bool remoteFileArchived = false;
                bool remoteFileDeleted = false;
                string remoteFileArchiveError = String.Empty;
                string remoteFileDeleteError = String.Empty;

                foreach (var grandChildTaskResult in childTaskResult.TaskResultSet.Values)
                {
                  if (grandChildTaskResult.TaskName == "ArchiveFile")
                  {
                    if (grandChildTaskResult.TaskResultStatus == TaskResultStatus.Success)
                      remoteFileArchived = true;
                    else
                    {
                      // failed to archive the downloaded file
                      remoteFileArchiveError = grandChildTaskResult.Message;
                      if (grandChildTaskResult.Exception != null)
                        remoteFileArchiveError += g.crlf + grandChildTaskResult.Exception.ToReport();
                      taskResult.Warning();
                    }
                  }

                  if (grandChildTaskResult.TaskName == "DeleteItem")
                  {
                    if (grandChildTaskResult.TaskResultStatus == TaskResultStatus.Success)
                      remoteFileDeleted = true;
                    else
                    {
                      // failed to delete the downloaded file
                      remoteFileArchiveError = grandChildTaskResult.Message;
                      if (grandChildTaskResult.Exception != null)
                        remoteFileDeleteError += g.crlf + grandChildTaskResult.Exception.ToReport();
                      taskResult.Warning();
                    }
                  }
                }

                string resultsMessage = String.Empty;
                if (childTaskResult.TaskResultStatus == TaskResultStatus.Success)
                {
                  FileInfo fi = (FileInfo)childTaskResult.Object;
                  _fileCount++;
                  _totalBytes += fi.Length;

                  resultsMessage = g.crlf + _fileCount.ToString("000") + " Name: " + fi.FullName + g.crlf +
                                   _fileCount.ToString("000") + " Size: " + fi.Length.ToString("###,###,##0") + g.crlf +
                                   _fileCount.ToString("000") + " Result: Download successful - duration was " + childTaskResult.DurationString + " seconds" + g.crlf;

                  if (_apiParms.ArchiveRemoteFiles)
                  {
                    if (remoteFileArchived)
                      resultsMessage += _fileCount.ToString("000") + " Remote file copied to archive folder" + g.crlf;
                    else
                      resultsMessage += _fileCount.ToString("000") + " Remote file archive error: " + remoteFileArchiveError + g.crlf;
                  }
                  if (!_apiParms.SuppressRemoteDelete)
                  {
                    if (remoteFileDeleted)
                      resultsMessage += _fileCount.ToString("000") + " Original remote file deleted" + g.crlf;
                    else
                      resultsMessage += _fileCount.ToString("000") + " Original remote file delete error: " + remoteFileDeleteError + g.crlf;
                  }

                  summaryMessage.Append(resultsMessage);
                }
                else // failures and warnings
                {
                  taskResult.TaskResultStatus = childTaskResult.TaskResultStatus;
                  if (childTaskResult.Code == 5)
                  {
                    FileInfo fi = (FileInfo)childTaskResult.Object;
                    _fileCount++;
                    _totalBytes += fi.Length;

                    resultsMessage = g.crlf + _fileCount.ToString("000") + " Name: " + fi.FullName + g.crlf + "Existing file of the same name was overwritten." + g.crlf +
                                     _fileCount.ToString("000") + " Size: " + fi.Length.ToString("###,###,##0") + g.crlf +
                                     _fileCount.ToString("000") + " Result: Download successful - duration was " + childTaskResult.DurationString + " seconds" + g.crlf;

                    if (_apiParms.ArchiveRemoteFiles)
                    {
                      if (remoteFileArchived)
                        resultsMessage += _fileCount.ToString("000") + " Remote file copied to archive folder" + g.crlf;
                      else
                        resultsMessage += _fileCount.ToString("000") + " Remote file archive error: " + remoteFileArchiveError + g.crlf;
                    }
                    if (!_apiParms.SuppressRemoteDelete)
                    {
                      if (remoteFileDeleted)
                        resultsMessage += _fileCount.ToString("000") + " Original remote file deleted" + g.crlf;
                      else
                        resultsMessage += _fileCount.ToString("000") + " Original remote file delete error: " + remoteFileDeleteError + g.crlf;
                    }

                    summaryMessage.Append(resultsMessage);
                  }
                  else
                  {
                    resultsMessage = g.crlf + "*** Child Task " + childTaskResult.TaskResultStatus.ToString() + "***" + g.crlf +
                                     "Code: " + childTaskResult.Code.ToString("0000") + g.crlf +
                                     "Message: " + childTaskResult.Message + childTaskResult.FullErrorDetail + g.crlf;
                    summaryMessage.Append(resultsMessage);
                  }
                }
              }

              string statsMessage = "Total files downloaded: " + _fileCount.ToString() + g.crlf +
                                    "Total bytes downloaded: " + _totalBytes.ToString("###,###,##0") + g.crlf;

              taskResult.TotalEntityCount = _fileCount;
              taskResult.Message = DryRunIndicator + statsMessage + summaryMessage.ToString() + g.crlf + "Download operation completed in " + downloadFilesTR.Duration + " seconds";

              _logger.Log("The ShareFileUtility DownloadFiles task was successful.  The results of the operation follow." + g.crlf + taskResult.Message, 6134);

              return taskResult;

            default: // download failed
              taskResult.Message = DryRunIndicator + downloadFilesTR.Message;
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        return taskResult.Failed(DryRunIndicator + "An exception occurred in DownloadFiles", ex);
      }
    }

    protected override void Initialize()
    {
      base.Initialize();
      this.AssertParmExistence("ShareFileLoginParms");
      this.AssertParmExistence("RootFolderId");

      switch(base.TaskRequest.TaskName)
      {
        case "UploadSingleFile":
          base.AssertParmExistence("UploadFilePath");
          break;

        case "UploadByFolder":
          base.AssertParmExistence("UploadFolderPath");
          break;

        case "DeleteRemoteFile":
          base.AssertParmExistence("DeleteFileName");
          break;

        case "DailyDownloadLaScadaFiles":
          base.AssertParmExistence("OutputFolderPath");
          base.AssertParmExistence("FileProcessingOption");
          break;
      }
    }
  }
}

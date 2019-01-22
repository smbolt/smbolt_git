using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.ShareFileApiClient;
using SF = ShareFile.Api.Models;
using Org.WebApi;
using Org.GS.Configuration;
using Org.GS.Notifications;
using Org.GS.Logging;
using Org.GS;

namespace ShareFileUtility
{
  public enum ProgramMode
  {
    HelpMode,
    EditConfig,
    UploadSingleFile,
    UploadByFolder,
    UploadWatchFolder,
    DownloadFiles,
    ListRemoteFolder,
    ClearRemoteFolder,
    DeleteRemoteFile,
    TestNotifications,
    Invalid
  }

  public class Program
  {
    private static string _programName = "ShareFileUtility";
    private static ProgramMode _programMode;
    private static ApiParms _apiParms;
    private static Logger _logger = new Logger();
    private static a a;
    private static string _commandLine;
    private static bool _inSilentMode = false;
    private static bool _inWaitMode = false;
    private static bool _overrideWaitMode = false;
    private static int _fileCount = 0;
    private static int _folderCount = 0;
    private static long _totalBytes = 0;
    private static bool _sendNotifications = false;
    private static SmtpParms _smtpParms;
    private static string _testEventName = String.Empty;
    private static bool _notificationsInProgress = false;
    private static bool _finalNotificationsInProgress = false;
    private static string _notifyConfigSetName;
    private static string _notifyConfigName;
    private static NotifyConfig _notifyConfig;

    private static string _inputFilePath;
    private static string _inputFolderPath;
    private static string _outputFolderPath;
    private static string _archiveFolderPath;
    private static string _remoteFileName;
    private static string _downloadFileName;
    private static FileProcessingOptions _downloadOpts;
    private static FileSystemWatcher _watcher;
    private static int _notificationCompletionLimit = 15000;
    private static Dictionary<int, string> _errorMessages;

    private static string _errorMessage = String.Empty;
    private static int _returnCode = 0;

    static int Main(string[] args)
    {
      _logger.Log("Program " + _programName + " starting up.");

      var taskResult = InitializeProgram(args);

      if (taskResult.TaskResultStatus != TaskResultStatus.Success)
      {
        _finalNotificationsInProgress = true;
        ReportInitializationError(taskResult);
        return taskResult.Code;
      }

      _returnCode = taskResult.Code.ToInt32();

      _logger.Log("Program mode is '" + _programMode.ToString() + "'.");
      if (!_inSilentMode && _programMode != ProgramMode.HelpMode)
        Console.WriteLine("Program mode is '" + _programMode.ToString() + "'." + g.crlf);

      _apiParms = GetApiParms();
      if (_archiveFolderPath.IsNotBlank())
        _apiParms.ArchiveFolder = _archiveFolderPath;

      switch (_programMode)
      {
        case ProgramMode.TestNotifications:
          taskResult = new TaskResult("NotificationTest");
          switch (_testEventName)
          {
            case "NotificationTest_Warning":
              taskResult.Warning();
              break;
            case "NotificationTest_Failed":
              taskResult.Failed();
              break;
            default:
              taskResult.Success();
              break;
          }
          break;

        case ProgramMode.ListRemoteFolder:
          taskResult = RunAsyncMethod(ListRemoteFolder, String.Empty);
          break;

        case ProgramMode.ClearRemoteFolder:
          taskResult = RunAsyncMethod(ClearRemoteFolder, String.Empty);
          break;

        case ProgramMode.DeleteRemoteFile:
          taskResult = RunAsyncMethod(DeleteRemoteFile, String.Empty);
          break;

        case ProgramMode.UploadSingleFile:
          taskResult = RunAsyncMethod(UploadSingleFile, _inputFilePath);
          break;

        case ProgramMode.UploadByFolder:
          taskResult = RunAsyncMethod(UploadByFolder, _inputFolderPath);
          break;

        case ProgramMode.UploadWatchFolder:
          taskResult = RunAsyncMethod(WatchFolder, _inputFolderPath);
          break;

        case ProgramMode.DownloadFiles:
          taskResult = RunAsyncMethod(DownloadFiles, String.Empty);
          break;

        case ProgramMode.EditConfig:
          taskResult = EditConfigFile();
          break;

        case ProgramMode.HelpMode:
          taskResult = DisplayHelpInfo();
          break;
      }

      _finalNotificationsInProgress = true;
      if (!taskResult.NotificationsSent && _notifyConfig != null)
        ProcessNotifications(taskResult);

      DisplaySummary();

      if (_notificationsInProgress)
      {
        if (!_inSilentMode)
          Console.WriteLine("Waiting for notifications to complete...");

        int spinCount = 0;
        while (_notificationsInProgress && _notificationCompletionLimit > 0)
        {
          if (!_inSilentMode)
          {
            spinCount++;
            switch (spinCount % 4)
            {
              case 0:
                Console.Write("/");
                break;
              case 1:
                Console.Write("-");
                break;
              case 2:
                Console.Write("\\");
                break;
              case 3:
                Console.Write("|");
                break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
          }
          _notificationCompletionLimit -= 200;
          System.Threading.Thread.Sleep(200);
        }
      }

      if (_overrideWaitMode && !_inWaitMode)
        _inWaitMode = true;

      if (_programMode != ProgramMode.UploadWatchFolder)
        _logger.Log("Program " + _programName + " terminating with return code " + _returnCode.ToString() + ".");

      if (_inWaitMode)
      {
        if (_programMode == ProgramMode.UploadWatchFolder)
        {
          Console.WriteLine(g.crlf + "ShareFileUtility is watching for files...");
          Console.WriteLine("Press any key to stop watching and end program." + g.crlf + "***");
          Console.ReadLine();
          DisplaySummary();
        }
        else
        {
          if (!_inSilentMode)
          {
            Console.WriteLine(g.crlf + "Return code is " + _returnCode.ToString());
            Console.WriteLine("Press any key to end program." + g.crlf + "***");
            Console.ReadLine();
          }
        }
      }

      return _returnCode;
    }

    private static void DisplaySummary()
    {
      if (_inSilentMode)
        return;

      if (_programMode == ProgramMode.UploadWatchFolder || _programMode == ProgramMode.EditConfig ||
          _programMode == ProgramMode.HelpMode || _programMode == ProgramMode.TestNotifications)
        return;

      Console.WriteLine(g.crlf + "Run Summary");
      _logger.Log("Run Summary");

      switch (_programMode)
      {
        case ProgramMode.UploadSingleFile:
        case ProgramMode.UploadByFolder:
        case ProgramMode.UploadWatchFolder:
          Console.WriteLine("Total files uploaded: " + _fileCount.ToString("###,##0"));
          Console.WriteLine("Total bytes uploaded: " + _totalBytes.ToString("###,###,###,##0"));
          _logger.Log("Total files uploaded: " + _fileCount.ToString("###,##0"));
          _logger.Log("Total bytes uploaded: " + _totalBytes.ToString("###,###,###,##0"));
          break;

        case ProgramMode.ListRemoteFolder:
          Console.WriteLine("Total folders in remote folder: " + _folderCount.ToString("###,##0"));
          Console.WriteLine("Total files in remote folder: " + _fileCount.ToString("###,##0"));
          Console.WriteLine("Total bytes in remote folder: " + _totalBytes.ToString("###,###,###,##0"));
          _logger.Log("Total folders in remote folder: " + _folderCount.ToString("###,##0"));
          _logger.Log("Total files in remote folder: " + _fileCount.ToString("###,##0"));
          _logger.Log("Total bytes in remote folder: " + _totalBytes.ToString("###,###,###,##0"));
          break;;

        case ProgramMode.ClearRemoteFolder:
          Console.WriteLine("Total folders not deleted: " + _folderCount.ToString("###,##0"));
          Console.WriteLine("Total files deleted: " + _fileCount.ToString("###,##0"));
          Console.WriteLine("Total bytes deleted: " + _totalBytes.ToString("###,###,###,##0"));
          _logger.Log("Total folders not deleted: " + _folderCount.ToString("###,##0"));
          _logger.Log("Total files deleted: " + _fileCount.ToString("###,##0"));
          _logger.Log("Total bytes deleted: " + _totalBytes.ToString("###,###,###,##0"));
          break;

        case ProgramMode.DeleteRemoteFile:
          Console.WriteLine("Total files deleted: " + _fileCount.ToString("###,##0"));
          Console.WriteLine("Total bytes deleted: " + _totalBytes.ToString("###,###,###,##0"));
          _logger.Log("Total files deleted: " + _fileCount.ToString("###,##0"));
          _logger.Log("Total bytes deleted: " + _totalBytes.ToString("###,###,###,##0"));
          break;

        case ProgramMode.DownloadFiles:
          Console.WriteLine("Total files downloaded: " + _fileCount.ToString("###,##0"));
          Console.WriteLine("Total bytes downloaded: " + _totalBytes.ToString("###,###,###,##0"));
          _logger.Log("Total files downloaded: " + _fileCount.ToString("###,##0"));
          _logger.Log("Total bytes downloaded: " + _totalBytes.ToString("###,###,###,##0"));
          break;
      }
    }

    // This method is used to run interactions with ShareFile on a new thread while displaying a "spinner" (progress indicator)
    // in the console.  Not all target methods require the string parameter and thus use "dummy" as the name of the unused
    // unused parameter.  The parameter must exist to match the "Func<string, int>" method signature.
    private static TaskResult RunAsyncMethod(Func<string, TaskResult> method, string parm)
    {
      TaskResult taskResult = null;
      string methodName = String.Empty;

      try
      {
        bool taskRunning = true;

        methodName = method.Method.Name;

        Task.Run<TaskResult>(() => method(parm)).ContinueWith(r =>
        {
          taskResult = r.Result;
          taskRunning = false;
          taskResult.NotificationsSent = true;

          bool runNotifications = true;
          if (taskResult.TaskName == "DownloadFiles" && taskResult.TaskResultStatus == TaskResultStatus.Success)
          {
            if (taskResult.TotalEntityCount == 0)
              runNotifications = false;
          }

          if (runNotifications && _notifyConfig != null)
            ProcessNotifications(taskResult);
        });

        int spinCount = 0;
        while (taskRunning)
        {
          if (!_inSilentMode)
          {
            spinCount++;
            switch (spinCount % 4)
            {
              case 0:
                Console.Write("/");
                break;
              case 1:
                Console.Write("-");
                break;
              case 2:
                Console.Write("\\");
                break;
              case 3:
                Console.Write("|");
                break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
          }
          System.Threading.Thread.Sleep(200);
        }

        if (taskResult == null)
          taskResult = new TaskResult("ShareFileUtility", "A null task result was returned from running of async method '" + methodName + "'.", TaskResultStatus.Failed, 198);

        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult = new TaskResult("ShareFileUtility").Failed("An exception occurred running async method '" + methodName + "'.", 301, ex);
        Console.WriteLine(taskResult.Message + " The exception report follows: " + g.crlf + ex.ToReport());
        return taskResult;
      }
    }

    private static TaskResult ListRemoteFolder(string dummy)
    {
      TaskResult taskResult = new TaskResult("ListRemoteFolder");

      try
      {
        using (var fm = new FileManager(_apiParms))
        {
          fm.ProgressMessage += fm_ProgressMessage;
          string remoteFolder = _apiParms.RootFolderId;
          if (_apiParms.RemoteTargetFolder.IsNotBlank())
            remoteFolder += "/" + _apiParms.RemoteTargetFolder;
          _logger.Log("Remote folder is '" + remoteFolder + "'");
          if (!_inSilentMode)
            Console.WriteLine("Remote folder is '" + remoteFolder + "'");

          taskResult = fm.ListRemoteFolder();
          switch (taskResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              List<ShareFile.Api.Models.File> files = (List<ShareFile.Api.Models.File>)taskResult.Object;
              LogAndShowMessageOnConsole("Folder Contents (" + files.Count.ToString() + " files)", !_inSilentMode);

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

                LogAndShowMessageOnConsole(totalCount.ToString("000") + folderIndicator + file.FileName + " (" + file.FileSizeBytes.Value.ToString("###,###,##0") + " bytes )", !_inSilentMode);
              }
              LogAndShowMessageOnConsole("Operation completed in " + taskResult.DurationString + " seconds", !_inSilentMode);
              return taskResult.Success();

            default:
              LogAndShowMessageOnConsole(taskResult);
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        taskResult.Failed("An exception occurred running 'ListRemoteFolder'.", 302, ex);
        LogAndShowMessageOnConsole(taskResult);
        return taskResult.Failed(302);
      }
    }

    private static TaskResult ClearRemoteFolder(string dummy)
    {
      TaskResult taskResult = new TaskResult("ClearRemoteFolder");

      try
      {
        using (var fm = new FileManager(_apiParms))
        {
          fm.ProgressMessage += fm_ProgressMessage;
          string remoteFolder = _apiParms.RootFolderId;
          if (_apiParms.RemoteTargetFolder.IsNotBlank())
            remoteFolder += "/" + _apiParms.RemoteTargetFolder;
          _logger.Log("Remote folder is '" + remoteFolder + "'");
          if (!_inSilentMode)
            Console.WriteLine("Remote folder is '" + remoteFolder + "'");

          taskResult = fm.ClearRemoteFolder();
          switch (taskResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              List<ShareFile.Api.Models.File> files = (List<ShareFile.Api.Models.File>)taskResult.Object;
              _logger.Log("Folder Contents (" + files.Count.ToString() + " files) deleted");
              if (!_inSilentMode)
              {
                int fileCount = 0;
                int folderCount = 0;
                foreach (var file in files)
                {
                  if (file.__type == "ShareFile.Api.Models.File")
                    fileCount++;
                  else
                    folderCount++;
                }

                LogAndShowMessageOnConsole("Folder Contents (" + fileCount.ToString() + " files) deleted" + g.crlf +
                                           "Folder Contents (" + folderCount.ToString() + " folders) not deleted", !_inSilentMode);
              }

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

                LogAndShowMessageOnConsole(totalCount.ToString("000") + folderIndicator + file.FileName + " (" + file.FileSizeBytes.Value.ToString("###,###,##0") + " bytes )", !_inSilentMode);
              }

              LogAndShowMessageOnConsole("Operation completed in " + taskResult.DurationString + " seconds", !_inSilentMode);

              return taskResult;

            default:
              LogAndShowMessageOnConsole(taskResult);
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        taskResult.Failed("An exception occurred running 'ClearRemoteFolder'.", 304, ex);
        LogAndShowMessageOnConsole(taskResult);
        return taskResult;
      }
    }

    private static TaskResult DeleteRemoteFile(string dummy)
    {
      TaskResult taskResult = new TaskResult("DeleteRemoteFile");

      try
      {
        using (var fm = new FileManager(_apiParms))
        {
          fm.ProgressMessage += fm_ProgressMessage;
          string remoteFolder = _apiParms.RootFolderId;
          if (_apiParms.RemoteTargetFolder.IsNotBlank())
            remoteFolder += "/" + _apiParms.RemoteTargetFolder;
          _logger.Log("Remote folder is '" + remoteFolder + "'");
          if (!_inSilentMode)
            Console.WriteLine("Remote folder is '" + remoteFolder + "'");

          taskResult = fm.DeleteRemoteFile(_remoteFileName);
          switch (taskResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              List<ShareFile.Api.Models.File> files = (List<ShareFile.Api.Models.File>)taskResult.Object;

              _fileCount = 0;
              foreach (var file in files)
              {
                _fileCount++;
                _totalBytes += file.FileSizeBytes.Value;
                LogAndShowMessageOnConsole("Remote file deleted - " + file.FileName + " (" + file.FileSizeBytes.Value.ToString("###,###,##0") + " bytes )", !_inSilentMode);
              }
              LogAndShowMessageOnConsole("Operation completed in " + taskResult.DurationString + " seconds", !_inSilentMode);
              return taskResult;

            case TaskResultStatus.Warning:
              LogAndShowMessageOnConsole("Remote file '" + _remoteFileName + "' not found.", true);
              return taskResult.SetCode(4);

            default:
              LogAndShowMessageOnConsole(taskResult);
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        taskResult.Failed("Error occurred running DeleteRemoteFile.", 313, ex);
        LogAndShowMessageOnConsole(taskResult);
        return taskResult;
      }
    }

    private static TaskResult UploadSingleFile(string fileName)
    {
      TaskResult taskResult = new TaskResult("UploadSingleFile");

      try
      {
        if (_apiParms.ArchiveFolder.IsNotBlank())
        {
          if (!Directory.Exists(_apiParms.ArchiveFolder))
          {
            try
            {
              Directory.CreateDirectory(_apiParms.ArchiveFolder);
            }
            catch(Exception ex)
            {
              taskResult.Failed("An exception occurred attempting to create the ArchiveFolder '" + _apiParms.ArchiveFolder + "'.", 141, ex);
              LogAndShowMessageOnConsole(taskResult);
              return taskResult;
            }
          }
        }

        if (_programMode != ProgramMode.UploadWatchFolder)
        {
          string remoteFolder = _apiParms.RootFolderId;
          if (_apiParms.RemoteTargetFolder.IsNotBlank())
            remoteFolder += "/" + _apiParms.RemoteTargetFolder;
          LogAndShowMessageOnConsole("Remote folder is '" + remoteFolder + "'", !_inSilentMode);
        }

        FileInfo fi = new FileInfo(fileName);
        _fileCount++;

        LogAndShowMessageOnConsole(_fileCount.ToString("000") + " Name: " + fileName + g.crlf +
                                   _fileCount.ToString("000") + " Size: " + fi.Length.ToString("###,###,##0"), !_inSilentMode);

        if (fi.Length > _apiParms.MaxUploadSize)
        {
          taskResult.Failed(_fileCount.ToString("000") + " ERROR - File size of " + fi.Length.ToString("###,###,##0") + " bytes exceeds the maximum size allowed which is " +
                            _apiParms.MaxUploadSize.ToString("###,###,##0") + " bytes.", 142);
          LogAndShowMessageOnConsole(taskResult);
          return taskResult;
        }

        using (var fm = new FileManager(_apiParms))
        {
          fm.ProgressMessage += fm_ProgressMessage;
          taskResult = fm.UploadSingleFile(fileName);

          switch (taskResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              _totalBytes += fi.Length;
              LogAndShowMessageOnConsole(_fileCount.ToString("000") + " Result: Upload successful - duration was " + taskResult.DurationString + " seconds", !_inSilentMode);
              return taskResult;

            default:
              _fileCount = _fileCount > 0 ? _fileCount - 1: 0;
              LogAndShowMessageOnConsole(taskResult);
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        taskResult.Failed(_fileCount.ToString("000") + " " + _errorMessages[306].Replace("@FileName@", fileName), 306, ex);
        LogAndShowMessageOnConsole(taskResult);
        return taskResult;
      }
    }

    private static TaskResult UploadByFolder(string folderPath)
    {
      TaskResult taskResult = new TaskResult("UploadByFolder");

      try
      {
        if (_apiParms.ArchiveFolder.IsNotBlank())
        {
          if (!Directory.Exists(_apiParms.ArchiveFolder))
          {
            try
            {
              Directory.CreateDirectory(_apiParms.ArchiveFolder);
            }
            catch (Exception ex)
            {
              taskResult.Failed(_errorMessages[143].Replace("@ArchiveFolder@", _apiParms.ArchiveFolder), 143, ex);
              LogAndShowMessageOnConsole(taskResult);
              return taskResult;
            }
          }
        }

        string remoteFolder = _apiParms.RootFolderId;
        if (_apiParms.RemoteTargetFolder.IsNotBlank())
          remoteFolder += "/" + _apiParms.RemoteTargetFolder;

        LogAndShowMessageOnConsole("Remote folder is '" + remoteFolder + "'.", !_inSilentMode);

        using (var fm = new FileManager(_apiParms))
        {
          fm.ProgressMessage += fm_ProgressMessage;

          taskResult = fm.UploadByFolder(folderPath);
          _fileCount = 0;

          switch (taskResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              foreach (var childTaskResult in taskResult.TaskResultSet.Values)
              {
                FileInfo fi = (FileInfo)childTaskResult.Object;
                _fileCount++;
                _totalBytes += fi.Length;

                LogAndShowMessageOnConsole(g.crlf + _fileCount.ToString("000") + " Name: " + fi.FullName + g.crlf +
                                           _fileCount.ToString("000") + " Size: " + fi.Length.ToString("###,###,##0") + g.crlf +
                                           _fileCount.ToString("000") + " Result: Upload successful - duration was " + childTaskResult.DurationString + " seconds", !_inSilentMode);
              }
              return taskResult;

            case TaskResultStatus.Warning:
              foreach (var childTaskResult in taskResult.TaskResultSet.Values)
              {
                FileInfo fi = (FileInfo)childTaskResult.Object;
                _fileCount++;
                _totalBytes += fi.Length;

                string result = "Upload successful - duration was " + childTaskResult.DurationString + " seconds";
                if (childTaskResult.TaskResultStatus != TaskResultStatus.Success)
                  result = "*** FAILED *** " + childTaskResult.Message + " (code " + childTaskResult.Code + ")";

                LogAndShowMessageOnConsole(g.crlf + _fileCount.ToString("000") + " Name: " + fi.FullName + g.crlf +
                                           _fileCount.ToString("000") + " Size: " + fi.Length.ToString("###,###,##0") + g.crlf +
                                           _fileCount.ToString("000") + " Result: " + result, true);

              }
              return taskResult.SetCode(308);

            default:
              LogAndShowMessageOnConsole(taskResult);
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        taskResult.Failed(_errorMessages[309].Replace("@FolderPath@", folderPath), 309, ex);
        LogAndShowMessageOnConsole(taskResult);
        return taskResult;
      }
    }

    private static TaskResult WatchFolder(string watchFolderPath)
    {
      TaskResult taskResult = new TaskResult("WatchFolder");

      try
      {
        string remoteFolder = _apiParms.RootFolderId;
        if (_apiParms.RemoteTargetFolder.IsNotBlank())
          remoteFolder += "/" + _apiParms.RemoteTargetFolder;

        LogAndShowMessageOnConsole("Remote folder is '" + remoteFolder + "'" + g.crlf +
                                   "Watching local folder '" + watchFolderPath + "'.", !_inSilentMode);

        _inWaitMode = true;
        _watcher = new FileSystemWatcher();
        _watcher.Path = watchFolderPath;
        _watcher.Filter = _apiParms.FolderFilter;
        _watcher.NotifyFilter = NotifyFilters.FileName;
        _watcher.Created += _watcher_Created;
        _watcher.EnableRaisingEvents = true;
        return taskResult.Success();
      }
      catch (Exception ex)
      {
        taskResult.Failed(_errorMessages[145].Replace("@WatchFolderPath@", watchFolderPath), 145, ex);
        LogAndShowMessageOnConsole(taskResult);
        return taskResult;
      }
    }

    private static void _watcher_Created(object sender, FileSystemEventArgs e)
    {
      TaskResult taskResult = new TaskResult("WatchFolder");

      try
      {
        FileInfo fi = new FileInfo(e.FullPath);
        if (fi.Length > _apiParms.MaxUploadSize)
        {
          taskResult.Failed("FILE EXCLUDED FROM UPLOAD DUE TO SIZE" + g.crlf +
                            "File '" + e.Name + "' exclude because its size " + fi.Length.ToString("###,###,##0") + " exceeds " +
                            "the max allowed size of " + _apiParms.MaxUploadSize.ToString("###,###,##0") + ".", 155);
          LogAndShowMessageOnConsole(taskResult);
          return;
        }

        string fileName = Path.GetFileName(e.Name);
        Console.WriteLine(g.crlf + "File added to watched folder: '" + fileName + "'.");
        taskResult = UploadSingleFile(e.FullPath);

        LogAndShowMessageOnConsole(taskResult);

        // need to do this here as this TaskResult is not returned into the main line logic, but just created in this event handler.
        if (_notifyConfig != null)
          ProcessNotifications(taskResult);

        Console.WriteLine("Press any key to stop watching and end program...");
      }
      catch (Exception ex)
      {
        taskResult.Failed("An exception occurred while attempting to upload a file '" + e.Name + "'.", 321, ex);
        LogAndShowMessageOnConsole(taskResult);
      }
    }

    private static TaskResult DownloadFiles(string dummy)
    {
      TaskResult taskResult = new TaskResult("DownloadFiles");

      try
      {
        string remoteFolder = _apiParms.RootFolderId;
        if (_apiParms.RemoteTargetFolder.IsNotBlank())
          remoteFolder += "/" + _apiParms.RemoteTargetFolder;

        LogAndShowMessageOnConsole("Remote folder is '" + remoteFolder + "'", !_inSilentMode);

        _fileCount = 0;
        _totalBytes = 0;

        using (var fm = new FileManager(_apiParms))
        {
          fm.ProgressMessage += fm_ProgressMessage;

          taskResult = fm.DownloadFiles(_outputFolderPath, _downloadFileName, _downloadOpts, _remoteFileName);

          switch (taskResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:   // download, archive and delete successful (archive and delete are configurable)
            case TaskResultStatus.Warning:   // download successful, but problem with archive and/or delete
              StringBuilder summaryMessage = new StringBuilder();

              // if specific file was requested but not found
              if (taskResult.Code == 4)
              {
                LogAndShowMessageOnConsole(taskResult);
                return taskResult;
              }

              foreach (var childTaskResult in taskResult.TaskResultSet.Values)
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
                      taskResult.SetCode(146);
                      remoteFileArchiveError = grandChildTaskResult.Message;
                      if (grandChildTaskResult.Exception != null)
                        remoteFileArchiveError += g.crlf + grandChildTaskResult.Exception.ToReport();
                    }
                  }

                  if (grandChildTaskResult.TaskName == "DeleteItem")
                  {
                    if (grandChildTaskResult.TaskResultStatus == TaskResultStatus.Success)
                      remoteFileDeleted = true;
                    else
                    {
                      // failed to delete the downloaded file
                      taskResult.SetCode(147);
                      remoteFileArchiveError = grandChildTaskResult.Message;
                      if (grandChildTaskResult.Exception != null)
                        remoteFileDeleteError += g.crlf + grandChildTaskResult.Exception.ToReport();
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
                      resultsMessage += _fileCount.ToString("000") + " Remote file copied archive folder";
                    else
                      resultsMessage += _fileCount.ToString("000") + " Remote file archive error: " + remoteFileArchiveError;
                  }
                  if (!_apiParms.SuppressRemoteDelete)
                  {
                    if (remoteFileDeleted)
                      resultsMessage += _fileCount.ToString("000") + " Original remote file deleted";
                    else
                      resultsMessage += _fileCount.ToString("000") + " Original remote file delete error: " + remoteFileDeleteError;
                  }

                  summaryMessage.Append(resultsMessage);

                  LogAndShowMessageOnConsole(resultsMessage, !_inSilentMode);
                }
                else // failures and warnings
                {
                  if (childTaskResult.Code == 5)
                  {
                    FileInfo fi = (FileInfo)childTaskResult.Object;
                    _fileCount++;
                    _totalBytes += fi.Length;

                    resultsMessage = g.crlf + _fileCount.ToString("000") + " Name: " + fi.FullName + g.crlf +
                                     "Existing file of the same name was overwritten." + g.crlf +
                                     _fileCount.ToString("000") + " Size: " + fi.Length.ToString("###,###,##0") + g.crlf +
                                     _fileCount.ToString("000") + " Result: Download successful - duration was " + childTaskResult.DurationString + " seconds" + g.crlf;

                    if (_apiParms.ArchiveRemoteFiles)
                    {
                      if (remoteFileArchived)
                        resultsMessage += _fileCount.ToString("000") + " Remote file copied archive folder";
                      else
                        resultsMessage += _fileCount.ToString("000") + " Remote file archive error: " + remoteFileArchiveError;
                    }
                    if (!_apiParms.SuppressRemoteDelete)
                    {
                      if (remoteFileDeleted)
                        resultsMessage += _fileCount.ToString("000") + " Original remote file deleted";
                      else
                        resultsMessage += _fileCount.ToString("000") + " Original remote file delete error: " + remoteFileDeleteError;
                    }

                    summaryMessage.Append(resultsMessage);

                    LogAndShowMessageOnConsole(resultsMessage, !_inSilentMode);
                  }
                  else
                  {
                    resultsMessage = g.crlf + "*** Child Task " + childTaskResult.TaskResultStatus.ToString() + "***" + g.crlf +
                                     "Code: " + childTaskResult.Code.ToString("0000") + g.crlf +
                                     "Message: " + childTaskResult.Message + g.crlf;
                    summaryMessage.Append(resultsMessage);
                  }

                  taskResult.SetCode(148);
                  taskResult.Message = resultsMessage;
                  LogAndShowMessageOnConsole(taskResult);
                }
              }

              string statsMessage = "Total files downloaded: " + _fileCount.ToString() + g.crlf +
                                    "Total bytes downloaded: " + _totalBytes.ToString("###,###,##0") + g.crlf;

              taskResult.TotalEntityCount = _fileCount;
              taskResult.Message = statsMessage + summaryMessage.ToString();

              LogAndShowMessageOnConsole(g.crlf + "Download operation completed in " + taskResult.Duration + " seconds", !_inSilentMode);

              return taskResult;

            default: // download failed
              LogAndShowMessageOnConsole(taskResult);
              return taskResult;
          }
        }
      }
      catch (Exception ex)
      {
        taskResult.Failed(_errorMessages[310], 310, ex);
        LogAndShowMessageOnConsole(taskResult);
        return taskResult;
      }
    }

    private static async Task<int> ProcessNotifications(TaskResult notifyTaskResult)
    {
      if (!_sendNotifications)
        return 0;

      if (_notifyConfig == null)
        return 0;

      try
      {
        if (notifyTaskResult.TaskName == "NotificationTest")
        {
          Console.WriteLine("In TestNotifications method");
          Console.WriteLine("Event Name to test: " + _testEventName);
        }

        if (_smtpParms == null)
          return 0;

        NotifyEvent notifyEvent = _notifyConfig.NotifyEventSet.GetNotifyEventForTaskResult(notifyTaskResult);
        if (notifyEvent == null)
          return 0;

        _notificationsInProgress = true;

        await ProcessNotificationsAsync(notifyTaskResult).ContinueWith((r) =>
        {
          var taskResult = r.Result;
          _notificationsInProgress = false;
          LogAndShowMessageOnConsole(taskResult);
        });

        return 0;
      }
      catch (Exception ex)
      {
        var taskResult = new TaskResult("ProcessNotifications").Failed(_errorMessages[314], 314, ex);
        LogAndShowMessageOnConsole(taskResult);
        return taskResult.Code;
      }
    }

    private static async Task<TaskResult> ProcessNotificationsAsync(TaskResult notifyTaskResult)
    {
      var taskResult = new TaskResult("ProcessNotifications");

      try
      {
        taskResult = await Task.Run<TaskResult>(async () =>
        {
          var notifyEvent = _notifyConfig.NotifyEventSet.GetNotifyEventForTaskResult(notifyTaskResult);

          if (notifyEvent == null)
            return taskResult.Success();

          var notification = new Notification();
          notification.Subject = notifyEvent.DefaultSubject;
          notification.Body = notifyTaskResult.NotificationMessage;
          notification.Code = notifyTaskResult.Code;
          notification.EventName = notifyEvent.Name;

          using (var notificationEngine = new NotificationEngine(_notifyConfig, _smtpParms))
          {
            return await notificationEngine.ProcessNotificationsAsync(notification);
          }
        });

        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult.Failed(_errorMessages[315], 315, ex);
        LogAndShowMessageOnConsole(taskResult);
        return taskResult;
      }
    }

    private static void fm_ProgressMessage(string message)
    {
      if (!_inSilentMode)
        Console.WriteLine(message);

      _logger.Log(message);
    }

    private static ApiParms GetApiParms()
    {
      string cfgPrefix = g.GetCI("ClientName");

      _logger.Log("Client configuration prefix is '" + cfgPrefix + "'.");
      if (!_inSilentMode && _programMode != ProgramMode.HelpMode)
        Console.WriteLine("Client configuration prefix is '" + cfgPrefix + "'.");

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

    private static TaskResult EditConfigFile()
    {
      TaskResult taskResult = new TaskResult("EditConfigFile");

      try
      {
        frmConfigEdit fEditConfig = new frmConfigEdit();
        fEditConfig.ShowDialog();
        return taskResult.Success();
      }
      catch (Exception ex)
      {
        taskResult.Failed("An exception occurred running 'EditConfigFile'.", 311, ex);
        LogAndShowMessageOnConsole(taskResult);
        return taskResult;
      }
    }

    private static TaskResult InitializeProgram(string[] args)
    {
      var taskResult = new TaskResult("Initialization");

      try
      {
        LoadErrorMessages();

        a = new a();

        _commandLine = g.CI("CommandLine");
        _overrideWaitMode = g.CI("OverrideWaitMode").ToBoolean();

        _notifyConfig = g.AppConfig.NotifyConfig;

        _sendNotifications = g.CI("SendNotifications").ToBoolean();
        if (_sendNotifications)
        {
          string smtpSpecPrefix = g.CI("SmtpSpecPrefix");
          var smtpSpec = g.GetSmtpSpec(smtpSpecPrefix);

          if (smtpSpec.ReadyToTestEmail())
          {
            _smtpParms = new SmtpParms(smtpSpec);
            _smtpParms.UseSmtpCredentials = true;
            _smtpParms.SuppressEmailSend = g.CI("SuppressEmailSend").ToBoolean(false);
          }
        }

        _notificationCompletionLimit = g.CI("NotificationCompletionLimit").ToInt32OrDefault(15000);

        _downloadFileName = g.CI("DownloadFileName");
        _downloadOpts = g.ToEnum<FileProcessingOptions>(g.CI("DownloadOptions"), FileProcessingOptions.SequenceDuplicates);

        if (_commandLine.IsNotBlank())
          args = GetArgsFromCommandLine(_commandLine);

        return ParseCommandTokens(args);
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during program initialization.", 199, ex);
      }
    }

    private static TaskResult ParseCommandTokens(string[] args)
    {
      var taskResult = new TaskResult("Initialization");

      if (args.Length == 0)
      {
        _programMode = ProgramMode.HelpMode;
        return taskResult.Success();
      }

      // first parameter must specify the file processing mode
      int argNumber = 0;
      string modeSwitch = args[argNumber].ToLower().Trim();
      _programMode = GetProgramMode(modeSwitch);

      if (_programMode == ProgramMode.Invalid)
        return taskResult.Failed(_errorMessages[101], 101);

      if (args.Contains("-q"))
        _inSilentMode = true;

      // turn off silent mode when it makes no sense
      if (_programMode == ProgramMode.HelpMode ||
          _programMode == ProgramMode.EditConfig ||
          _programMode == ProgramMode.ListRemoteFolder)
        _inSilentMode = false;

      if (!_inSilentMode)
        Console.WriteLine("ShareFileUtility is starting up.");

      if (_programMode == ProgramMode.HelpMode ||
          _programMode == ProgramMode.EditConfig ||
          _programMode == ProgramMode.ListRemoteFolder ||
          _programMode == ProgramMode.ClearRemoteFolder)
        return taskResult.Success();


      // if we don't have -i to indicate an input file or folder for the upload methods, then we have an error
      if (!args.Contains("-i"))
      {
        switch (_programMode)
        {
          case ProgramMode.UploadSingleFile:
            return taskResult.Failed(_errorMessages[102], 102);

          case ProgramMode.UploadByFolder:
            return taskResult.Failed(_errorMessages[103], 103);

          case ProgramMode.UploadWatchFolder:
            return taskResult.Failed(_errorMessages[104], 104);
        }
      }

      // if we don't have -o to indicate an output folder for the download files mode, then we have an error
      if (!args.Contains("-o") && _programMode == ProgramMode.DownloadFiles)
        return taskResult.Failed(_errorMessages[105], 105);

      // PROCESS THE INPUT FILE OR FOLDER SPECIFICATIONS FROM THE COMMAND LINE

      int indexOfI = GetIndexOf("-i", args);

      // if we have "-i", it must be followed immediately by the name of the file or folder to be processed
      if (indexOfI > -1)
      {
        int indexOfValueI = indexOfI + 1;

        // if we don't have a parameter following "-i", then we have an error
        if (args.Length < indexOfValueI + 1)
        {
          switch (_programMode)
          {
            case ProgramMode.UploadSingleFile:
              return taskResult.Failed(_errorMessages[106], 106);

            case ProgramMode.UploadByFolder:
            case ProgramMode.UploadWatchFolder:
              return taskResult.Failed(_errorMessages[107], 107);
          }
        }

        // since we do have a parameter after "-i", we need to validate that it is a file or folder as appropriate.
        string inputValue = args[indexOfValueI];

        switch (_programMode)
        {
          case ProgramMode.UploadSingleFile:
            if (!File.Exists(inputValue))
              return taskResult.Failed(_errorMessages[108].Replace("@InputValue", inputValue), 108);

            _inputFilePath = inputValue;
            break;

          case ProgramMode.UploadByFolder:
          case ProgramMode.UploadWatchFolder:
            if (!Directory.Exists(inputValue))
              return taskResult.Failed(_errorMessages[109].Replace("@InputValue", inputValue), 109);

            _inputFolderPath = inputValue;
            break;
        }
      }

      // PROCESS THE OUTPUT FOLDER SPECIFICATIONS FROM THE COMMAND LINE

      int indexOfO = GetIndexOf("-o", args);

      // if we have "-o", it must be followed immediately by the name of the folder that files will be downloaded into
      if (indexOfO > -1)
      {
        int indexOfValueO = indexOfO + 1;

        if (args.Length < indexOfValueO + 1 && _programMode == ProgramMode.DownloadFiles)
          return taskResult.Failed(_errorMessages[110], 110);

        // since we do have a parameter after "-o", we need to validate that it is a folder or that we can create the folder
        string outputValue = args[indexOfValueO];

        if (!g.IsValidPath(outputValue))
          return taskResult.Failed(_errorMessages[111].Replace("@OutputValue@", outputValue), 111);

        if (!Directory.Exists(outputValue))
        {
          try
          {
            Directory.CreateDirectory(outputValue);
          }
          catch (Exception ex)
          {
            return taskResult.Failed(_errorMessages[112].Replace("@OutputValue@", outputValue), 112, ex);
          }
        }
        _outputFolderPath = outputValue;
      }

      // if we don't have -f to indicate the remote file name to be deleted, then we have an error
      if (!args.Contains("-f") && _programMode == ProgramMode.DeleteRemoteFile)
        return taskResult.Failed(_errorMessages[113], 113);

      int indexOfF = GetIndexOf("-f", args);

      // if we have "-f", it must be followed immediately by the name of the remote file to be downloaded or deleted
      if (indexOfF > -1)
      {
        int indexOfValueF = indexOfF + 1;

        if (args.Length < indexOfValueF + 1)
        {
          switch (_programMode)
          {
            case ProgramMode.DownloadFiles:
              return taskResult.Failed(_errorMessages[114], 114);

            case ProgramMode.DeleteRemoteFile:
              return taskResult.Failed(_errorMessages[115], 115);
          }
        }

        // since we do have a parameter after "-f", we will use it as the remote file name
        _remoteFileName = args[indexOfValueF];
      }

      // PROCESS THE ARCHIVE FOLDER SPECIFICATIONS FROM THE COMMAND LINE IF IT EXISTS (NOT REQUIRED)

      int indexOfA = GetIndexOf("-a", args);

      // if we have "-a", it must be followed immediately by the name of the folder will be used to archive uploads
      if (indexOfA > -1)
      {
        int indexOfValueA = indexOfA + 1;

        if (args.Length < indexOfValueA + 1)
        {
          switch (_programMode)
          {
            case ProgramMode.UploadByFolder:
            case ProgramMode.UploadSingleFile:
            case ProgramMode.UploadWatchFolder:
              return taskResult.Failed(_errorMessages[116], 116);
          }
        }

        // since we do have a parameter after "-a", we need to validate that it is a folder or that we can create the folder for archival
        string archiveValue = args[indexOfValueA];

        if (!g.IsValidPath(archiveValue))
        {
          taskResult.Failed(_errorMessages[120].Replace("@ArchiveValue@", archiveValue), 120);
          return taskResult;
        }

        if (!Directory.Exists(archiveValue))
        {
          try
          {
            Directory.CreateDirectory(archiveValue);
          }
          catch (Exception ex)
          {
            return taskResult.Failed(_errorMessages[117].Replace("@ArchiveValue@", archiveValue), 117, ex);
          }
        }
        _archiveFolderPath = archiveValue;
      }

      // for testing notifications...

      // if we don't have -e to indicate the name of the event that notifications will be tested for
      if (!args.Contains("-e") && _programMode == ProgramMode.TestNotifications)
        return taskResult.Failed(_errorMessages[118], 118);

      int indexOfE = GetIndexOf("-e", args);

      // if we have "-e", it must be followed immediately by the name of the event to be used for testing notifications
      if (indexOfE > -1)
      {
        int indexOfValueE = indexOfE + 1;

        if (args.Length < indexOfValueE + 1 && _programMode == ProgramMode.TestNotifications)
          return taskResult.Failed(_errorMessages[119], 119);

        // since we do have a parameter after "-e", we will use it as the notification event name
        _testEventName = args[indexOfValueE];
      }

      return taskResult.Success();
    }

    #region Utility Methods

    private static string[] GetArgsFromCommandLine(string commandLine)
    {
      string cmdWork = commandLine.Trim().Replace("'", "\"");

      while (cmdWork.Contains("\""))
      {
        int pos = cmdWork.IndexOf("\"");
        if (pos == -1)
          break;
        int endPos = cmdWork.IndexOf("\"", pos + 1);
        if (endPos == -1)
          break;

        // temporarily replace all spaces with pipes "|" so that the command line
        // can be split up with a space delimiter
        string quoted = cmdWork.Substring(pos, (endPos - pos + 1));
        string quotedNoSpace = quoted.Replace(" ", "|").Replace("\"", " ");

        cmdWork = cmdWork.ReplaceAtPosition(quotedNoSpace, pos);
      }

      // split the work string
      string[] argArray = cmdWork.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

      // replace the pipes with spaces to restore values
      for (int i = 0; i < argArray.Length; i++)
      {
        argArray[i] = argArray[i].Replace("|", " ");
      }

      return argArray;
    }

    private static ProgramMode GetProgramMode(string modeSwitch)
    {
      switch (modeSwitch.ToLower())
      {
        case "-help":
          return ProgramMode.HelpMode;
        case "-editconfig":
          return ProgramMode.EditConfig;
        case "-uploadsinglefile":
          return ProgramMode.UploadSingleFile;
        case "-uploadbyfolder":
          return ProgramMode.UploadByFolder;
        case "-uploadwatchfolder":
          return ProgramMode.UploadWatchFolder;
        case "-listremotefolder":
          return ProgramMode.ListRemoteFolder;
        case "-clearremotefolder":
          return ProgramMode.ClearRemoteFolder;
        case "-deleteremotefile":
          return ProgramMode.DeleteRemoteFile;
        case "-downloadfiles":
          return ProgramMode.DownloadFiles;
        case "-testnotifications":
          return ProgramMode.TestNotifications;
      }

      return ProgramMode.Invalid;
    }

    private static int GetIndexOf(string item, string[] args)
    {
      if (args == null || args.Length == 0)
        return -1;

      for (int i = 0; i < args.Length; i++)
      {
        if (args[i] == item)
          return i;
      }

      return -1;
    }

    private static void ReportInitializationError(TaskResult taskResult)
    {
      LogAndShowMessageOnConsole(taskResult);

      if (_notifyConfig != null)
        ProcessNotifications(taskResult);

      if (_notificationsInProgress)
      {
        if (!_inSilentMode)
          Console.WriteLine("Waiting for notifications to complete...");

        int spinCount = 0;
        while (_notificationsInProgress && _notificationCompletionLimit > 0)
        {
          if (!_inSilentMode)
          {
            spinCount++;
            switch (spinCount % 4)
            {
              case 0:
                Console.Write("/");
                break;
              case 1:
                Console.Write("-");
                break;
              case 2:
                Console.Write("\\");
                break;
              case 3:
                Console.Write("|");
                break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
          }
          _notificationCompletionLimit -= 200;
          System.Threading.Thread.Sleep(200);
        }
      }

      Console.WriteLine("Press any key to exit");
      Console.ReadLine();
      _logger.Log("Program " + _programName + " terminating with return code " + _returnCode.ToString() + ".");
    }

    private static TaskResult DisplayHelpInfo()
    {
      Console.WriteLine("-------------------------------------------------");
      Console.WriteLine("ShareFileUtility Program");
      Console.WriteLine("-------------------------------------------------" + g.crlf);
      Console.WriteLine("See the 'readme.html' file in the same directory as the ShareFileUtility.exe program for usage information.");

      _inWaitMode = true;

      return new TaskResult("DisplayHelpInfo", String.Empty, true);
    }

    private static void LogAndShowMessageOnConsole(TaskResult taskResult)
    {
      if (_finalNotificationsInProgress && taskResult.TaskName == "ProcessNotifications")
        return;

      _logger.Log(taskResult);
      taskResult.IsLogged = true;

      if (taskResult.TaskResultStatus != TaskResultStatus.Success)
      {
        if (taskResult.TaskResultStatus == TaskResultStatus.Failed)
          Console.WriteLine(g.crlf + "*** ERROR ***" + g.crlf);
        else
          Console.WriteLine(g.crlf + "*** WARNING ***" + g.crlf);
      }

      Console.Write("Task: " + taskResult.TaskName + g.crlf +
                    "Code: " + taskResult.Code.ToString() + g.crlf +
                    "Message: " + taskResult.Message + g.crlf);

      if (taskResult.Exception != null)
        Console.Write("Exception: " + taskResult.Exception.ToReport() + g.crlf);

      if (taskResult.FullErrorDetail.IsNotBlank())
        Console.Write("Full Error Detail: " + taskResult.FullErrorDetail);
      Console.WriteLine(g.crlf);
    }

    private static void LogAndShowMessageOnConsole(string message, bool writeToConsole)
    {
      if (writeToConsole)
        Console.WriteLine(message);
      _logger.Log(message);
    }

    private static void LoadErrorMessages()
    {
      _errorMessages = new Dictionary<int, string>();

      _errorMessages.Add(101, "ShareFileUtility could not determine the requested mode of operation." + g.crlf +
                         "The first command line parameter must be one of the following: " + g.crlf +
                         "  '-help'   to display usage information and instructions" + g.crlf +
                         "  '-editConfig'   to edit the AppConfig file" + g.crlf +
                         "  '-uploadSingleFile'   to upload a single file" + g.crlf +
                         "  '-uploadFolder'  to upload all files in a single folder" + g.crlf +
                         "  '-uploadFolderWatch'  to watch folder for files and upload them upon arrival" + g.crlf +
                         "  '-listRemoteFolder'  to list contents of remote folder" + g.crlf +
                         "  '-clearRemoteFolder'  to clear contents of remote folder" + g.crlf +
                         "  '-deleteRemoteFile'  to delete a specific remote file" + g.crlf +
                         "  '-downloadFiles'   to download files from the online folder" + g.crlf2 +
                         "  '-testNotifications'   to test notifications" + g.crlf2 +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(102, "The ShareFileUtility command line must include an '-i' switch to specify the input file to be uploaded." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(103, "The ShareFileUtility command line must include an '-i' switch to specify the input folder of files to be uploaded." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(104, "The ShareFileUtility command line must include an '-i' switch to specify the input folder to watch for files to be uploaded." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(105, "The ShareFileUtility command line must include an '-o' switch to specify the output folder location." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(106, "The ShareFileUtility command line must include the input file name immediately following the '-i' switch." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(107, "The ShareFileUtility command line must include the input folder name immediately following the '-i' switch." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(108, "The ShareFileUtility command line parameter immediately following the '-i' switch must be a valid input file name." + g.crlf +
                         "The value '@InputValue@' does not correspond to a valid file name." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(109, "The ShareFileUtility command line parameter immediately following the '-i' switch must be a valid input folder name." + g.crlf +
                         "The value '@InputValue@' does not correspond to a valid folder name." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(110, "The ShareFileUtility command line must include the output file name immediately following the '-o' switch." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(111, "The ShareFileUtility command line parameter immediately following the '-o' switch must be a valid output folder path name (whether or not it exists)." + g.crlf +
                         "The value '@OutputValue@' does not correspond to a valid folder path name." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(112, "The ShareFileUtility program is unable to create the non-existent output folder at '@OutputValue@'.");

      _errorMessages.Add(113, "The ShareFileUtility command line must include an '-f' switch to specify the remote file name to be deleted." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(114, "The ShareFileUtility command line must include the remote file name to be downloaded immediately following the '-f' switch." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(115, "The ShareFileUtility command line must include the remote file name to be deleted immediately following the '-f' switch." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(116, "The ShareFileUtility command line must include the archive folder name immediately following the '-a' switch." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(117, "The ShareFileUtility program is unable to create the non-existent archive folder at '@ArchiveValue@'.");

      _errorMessages.Add(118, "The ShareFileUtility command line must include an '-e' switch to specify the name of the event for which notifications will be tested." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(119, "The ShareFileUtility command line must include the name of the event to be use for testing notifications immediately following the '-e' switch." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(120, "The ShareFileUtility command line parameter immediately following the '-a' switch must be a valid output folder path name (whether or not it exists)." + g.crlf +
                         "The value '@ArchiveValue@' does not correspond to a valid folder path name." + g.crlf +
                         "See 'readme.html' in the program directory for help.");

      _errorMessages.Add(143, "An exception occurred attempting to create the ArchiveFolder '@ArchiveFolder@'.");
      _errorMessages.Add(145, "An exception occurred while attempting to establish the FileSystemWatcher to watch folder '@WatchFolderPath@' for new files.");
      _errorMessages.Add(306, "An exception occurred attempting to upload a single file '@FileName@'.");
      _errorMessages.Add(309, "An exception occurred attempting to upload folder of files '@FolderPath@'.");
      _errorMessages.Add(310, "An exception occurred in DownloadFiles.");
      _errorMessages.Add(314, "Error occurred running TestNotifications.");
      _errorMessages.Add(315, "Error occurred running TestNotifications.");

    }

    #endregion
  }
}

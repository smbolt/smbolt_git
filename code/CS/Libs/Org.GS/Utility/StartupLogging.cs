using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Web;

namespace Org.GS
{
  public static class StartupLogging
  {
    private static bool _internalSuppressLogging = false;
    private static bool _isFirstTimeIn = true;
    private static string _logFileName = String.Empty;
    public static string StartupLogPath = String.Empty;
    public static int StartupLogKeepFiles = 20;
    private static string crlf = Environment.NewLine;
    private static string crlf2 = Environment.NewLine + Environment.NewLine;

    private static bool _startupComplete;
    public static bool StartupComplete
    {
      get {
        return _startupComplete;
      }
      set {
        _startupComplete = value;
      }
    }

    public static void WriteStartupLog(string logRecord)
    {
      if (_startupComplete || _internalSuppressLogging)
        return;


      if (String.IsNullOrEmpty(StartupLogPath))
      {
        StartupLogPath = @"C:\StartupLogging";
      }

      if (String.IsNullOrEmpty(StartupLogPath))
        return;

      try
      {
        if (!Directory.Exists(StartupLogPath))
          Directory.CreateDirectory(StartupLogPath);

        if (_isFirstTimeIn)
          ClearOldLogFiles(StartupLogPath);

        WriteLogRecord(logRecord);
      }
      catch (Exception ex)
      {
        string message = ex.Message;
      }
    }

    private static void WriteLogRecord(string logRecord)
    {
      if (_logFileName.Trim().Length == 0)
        _logFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-Startup.log";

      string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

      File.AppendAllText(StartupLogPath + @"\" + _logFileName, currentTime + " - " + logRecord + crlf2);
    }

    private static void ClearOldLogFiles(string logPath)
    {
      if (!Directory.Exists(logPath))
        return;

      List<string> files = Directory.GetFiles(logPath).ToList();
      files.Sort();

      int filesToDelete = files.Count - StartupLogKeepFiles;
      if (filesToDelete < 1)
        return ;

      int filesDeleted = 0;

      while (filesToDelete > filesDeleted)
      {
        string fileToDelete = files[filesDeleted];
        FileInfo fi = new FileInfo(fileToDelete);
        fi.Delete();
        filesDeleted++;
      }
    }
  }
}

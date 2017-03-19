using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Logging;

namespace Org.TSK
{
  public class FileProcessor
  {
    public static event Action<String> ReportTaskProgress;

    private static string indent = g.BlankString(12);
    private static string _configFilesPath = String.Empty;
    public static bool ServiceHasBeenTerminated { get; set; }


    private static object DeleteFiles_LockObject = new object();
    public static TaskResult DeleteFiles(string deleteFilesPath, string agingUnit, string aging)
    {
      lock (DeleteFiles_LockObject)
      {
        Logger logger = new Logger();
        TaskResult taskResult = new TaskResult();
        taskResult.TaskName = "DeleteFiles";

        taskResult.TaskResultStatus = TaskResultStatus.NotExecuted;
        taskResult.Message = "DeleteFiles processing is not yet implemented.";
        return taskResult;
      }
    }

    private static object DeleteAppLogFiles_LockObject = new object();
    public static TaskResult DeleteAppLogFiles(int appLogKeepDays, string appLogPath)
    {
      lock (DeleteAppLogFiles_LockObject)
      {
        Logger logger = new Logger();
        TaskResult taskResult = new TaskResult();
        taskResult.TaskName = "AppLogCleanup";
        StringBuilder sbReport = new StringBuilder();

        sbReport.Append("APPLICATION LOG FILE CLEANUP PROCESS" + g.crlf);
        sbReport.Append(indent + "Performing Application Log File Cleanup " + g.crlf +
                        indent + "    Deleting Application Log Files older than " + appLogKeepDays.ToString("00") + " days from path: " + appLogPath + g.crlf);

        if (!Directory.Exists(appLogPath))
        {
          logger.Log("The high level folder for AppLog files was not found '" + appLogPath);
          // need to send an email... 
        }

        string pathBeingDeleted = String.Empty;
        int totalFilesDeleted = 0;
        try
        {
          totalFilesDeleted += ClearAppLogFiles(appLogPath, appLogKeepDays, sbReport);
        }
        catch (Exception ex)
        {
          string errorMessage = "An error occurred deleting application log files." + g.crlf2 + "File Path: " + appLogPath + g.crlf2 + ex.Message;
          sbReport.Append(indent + "*** ERROR ***  " + errorMessage + g.crlf);
          logger.Log(errorMessage);
          // send an email... 
        }

        sbReport.Append(indent + "    Total files deleted: " + totalFilesDeleted.ToString() + g.crlf);
        taskResult.TaskResultStatus = TaskResultStatus.Success;
        taskResult.Header = sbReport.ToString();
        return taskResult;
      }
    }

    private static int ClearAppLogFiles(string path, int days, StringBuilder sb)
    {
      int deleteCount = 0;
      int fileCount = 0;
      DateTime currentDate = DateTime.Now;
      DateTime cutoffDate = currentDate.AddDays((days * -1));
      int months = currentDate.Month - cutoffDate.Month;

      string[] filePaths = Directory.GetFiles(path, "*.log", SearchOption.AllDirectories);
      fileCount += filePaths.Count();

      foreach (string file in filePaths)
      {
        if (IsAppLogFileAged(Path.GetFileNameWithoutExtension(file), cutoffDate))
        {
          sb.Append(indent + "      DELETING FILE: " + file + g.crlf);
          File.Delete(file);
          deleteCount++;
        }
      }

      sb.Append(indent + "    Deleting wdf files created before " + cutoffDate.ToString() + g.crlf);
      sb.Append(indent + "    Total files in path: " + fileCount.ToString() + g.crlf);
      sb.Append(indent + "      Total files deleted: " + deleteCount.ToString() + g.crlf);

      return deleteCount;
    }

    private static bool IsAppLogFileAged(string fileName, DateTime cutoffDate)
    {
      if (fileName.Length != 11)
        return false;

      string dateCCYYMMDD = fileName.Substring(0, 8);
      string dateHH = fileName.Substring(9, 2);

      if (!dateCCYYMMDD.IsNumeric())
        return false;

      if (!dateHH.IsNumeric())
        return false;

      int year = Int32.Parse(dateCCYYMMDD.Substring(0, 4));
      int month = Int32.Parse(dateCCYYMMDD.Substring(4, 2));
      int day = Int32.Parse(dateCCYYMMDD.Substring(6, 2));
      int hour = Int32.Parse(dateHH);

      if (year < 2000 || year > 2050)
        return false;

      if (month < 1 || month > 12)
        return false;

      if (day < 1 || day > 31)
        return false;

      if (hour < 0 || hour > 24)
        return false;

      DateTime fileDate = new DateTime(year, month, day, hour, 0, 0);

      if (fileDate < cutoffDate)
        return true;

      return false;
    }

    private static object DeleteIISLogFiles_LockObject = new object();
    public static TaskResult DeleteIISLogFiles(int IISLogKeepDays, string IISLogPath, string fileType)
    {
      lock (DeleteAppLogFiles_LockObject)
      {
        TaskResult taskResult = new TaskResult();
        StringBuilder sbReport = new StringBuilder();
        Logger logger = new Logger();

        sbReport.Append("IIS LOG CLEANUP PROCESS - " + fileType + g.crlf);
        sbReport.Append(indent + "Performing IIS Log Cleanup " + g.crlf +
                        indent + "    Deleting IIS Logs older than " + IISLogKeepDays.ToString("00") + " days." + g.crlf);

        if (!Directory.Exists(IISLogPath))
        {
          sbReport.Append("The high level folder for IIS log files was not found '" + IISLogPath + "'.");
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Header = sbReport.ToString();
          taskResult.Message = taskResult.Header;
          taskResult.NotificationMessage = "The high level folder for IIS log files was not found '" + IISLogPath + "'. This task will terminate.";
          logger.Log(taskResult.NotificationMessage);
          return taskResult;
        }

        int totalFilesDeleted = 0;
        try
        {
          totalFilesDeleted += ClearIISLogFiles(IISLogPath, IISLogKeepDays, sbReport);
        }
        catch (Exception ex)
        {
          string errorMessage = "An error occurred deleting IIS Logs." + g.crlf2 + g.crlf2 + ex.Message;
          sbReport.Append(indent + "*** ERROR ***  " + errorMessage + g.crlf);
          logger.Log(errorMessage);
        }


        taskResult.TaskResultStatus = TaskResultStatus.Success;
        taskResult.Header = sbReport.ToString();
        taskResult.Message = taskResult.Header;
        logger.Log(taskResult.Message);
        return taskResult;
      }
    }

    private static int ClearIISLogFiles(string path, int days, StringBuilder sb)
    {
      int deleteCount = 0;
      int deleteCount2 = 0;
      int fileCount = 0;
      bool logFileIsAged = false;
      DateTime currentDT = DateTime.Now;
      DateTime deleteThruDT = currentDT.AddDays((days * -1));

      string[] filePaths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

      foreach (string filePath in filePaths)
      {
        string fileName = Path.GetFileName(filePath);
        string firstFour = fileName.Substring(0, 4);
        string lastFour = fileName.Substring(10, 4);
        fileCount++;

        if (fileName.Length == 14 && firstFour == "u_ex" && lastFour == ".log")
        {
          logFileIsAged = IsIISLogFileAged(fileName, deleteThruDT);
          if (logFileIsAged)
          {
            sb.Append(indent + "      DELETING FILE: " + filePath + g.crlf);
            File.Delete(filePath);
            deleteCount++;
            System.Threading.Thread.Sleep(10);
          }
        }
        else
        {
          sb.Append(indent + "      DELETING MISPLACED FILE: " + filePath + g.crlf);
          File.Delete(filePath);
          deleteCount++;
          deleteCount2++;
          System.Threading.Thread.Sleep(10);
        }
      }

      sb.Append(indent + "    Deleting IIS log files created before " + deleteThruDT.ToString() + g.crlf);
      sb.Append(indent + "    Total files in path: " + fileCount.ToString() + g.crlf);
      sb.Append(indent + "    Total files deleted: " + deleteCount.ToString() + g.crlf);
      sb.Append(indent + "    Total misplaced files deleted: " + deleteCount2.ToString() + g.crlf);

      return deleteCount;
    }

    private static bool IsIISLogFileAged(string fileName, DateTime deleteThruDT)
    {
      if (fileName.Length < 10)
        return false;

      string dateYYMMDD = fileName.Substring(4, 6);

      if (!dateYYMMDD.IsNumeric())
        return false;

      int year = Int32.Parse(dateYYMMDD.Substring(0, 2)) + 2000;
      int month = Int32.Parse(dateYYMMDD.Substring(2, 2));
      int day = Int32.Parse(dateYYMMDD.Substring(4, 2));

      if (year < 2000 || year > 2050)
        return false;

      if (month < 1 || month > 12)
        return false;

      if (day < 1 || day > 31)
        return false;

      DateTime fileDate = new DateTime(year, month, day, 0, 0, 0);

      if (fileDate < deleteThruDT)
        return true;

      return false;
    }
  }
}

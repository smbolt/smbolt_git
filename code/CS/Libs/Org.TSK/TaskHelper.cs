using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Diagnostics;
using Org.GS;
using Org.GS.Configuration;

namespace Org.TSK
{
  public class TaskHelper
  {
    public static void ServiceAlert(string alertMessage)
    {
      try
      {
        bool useAlerter = g.CI("UseAlerter").ToBoolean();

        if (!useAlerter)
          return;

        string alerterPath = g.CI("AlerterPath");

        if (alerterPath.IsBlank())
          return;

        if (!File.Exists(alerterPath))
          return;

        string alerterProcessName = Path.GetFileNameWithoutExtension(alerterPath);
        if (ProgramIsAlreadyRunning(alerterProcessName))
          return;

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = alerterPath;
        startInfo.Arguments = "\"" + alertMessage + "\"";
        Process.Start(startInfo);
      }
      catch (Exception ex)
      {
        string exMessage = ex.Message;
      }
    }

    public static void ServiceAlert(string alertMessage, bool startupInProgress)
    {
      try
      {
        if (!startupInProgress)
          return;

        string appPath = g.GetAppPath();
        string alerterPath = appPath + @"\ServiceAlert.exe";

        if (!File.Exists(alerterPath))
          return;

        string alerterProcessName = Path.GetFileNameWithoutExtension(alerterPath);
        if (ProgramIsAlreadyRunning(alerterProcessName))
          return;

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = alerterPath;
        startInfo.Arguments = "\"" + alertMessage + "\"";
        Process.Start(startInfo);
      }
      catch (Exception ex)
      {
        string exMessage = ex.Message;
      };
    }

    public static bool ServiceAlertForce(string alertMessage)
    {
      try
      {
        string alerterPath = g.CI("AlerterPath");

        if (alerterPath.IsBlank())
          alerterPath = g.GetAppPath() + @"\ServiceAlert.exe";

        if (!File.Exists(alerterPath))
          return false;

        string alerterProcessName = Path.GetFileNameWithoutExtension(alerterPath);
        if (ProgramIsAlreadyRunning(alerterProcessName))
          return true;

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = alerterPath;
        startInfo.Arguments = "\"" + alertMessage + "\"";
        Process.Start(startInfo);
        return true;
      }
      catch (Exception ex)
      {
        string exMessage = ex.Message;
        return true;
      };
    }

    public static bool ProgramIsAlreadyRunning(string processName)
    {
      Process[] runningProcesses = Process.GetProcessesByName(processName);
      if (runningProcesses.Count() > 0)
        return true;

      return false;
    }

  }
}

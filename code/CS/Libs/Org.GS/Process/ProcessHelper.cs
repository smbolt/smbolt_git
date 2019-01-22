using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Org.GS.Logging;

namespace Org.GS
{
  public class ProcessHelper : IDisposable
  {
    [DllImport("user32.dll")]
    private static extern int ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    public static string RunCommand(string commandInput)
    {
      StringBuilder sb = new StringBuilder();

      string[] delim = new string[] { g.crlf };
      string[] commands = commandInput.Split(delim, StringSplitOptions.RemoveEmptyEntries);

      foreach (string command in commands)
      {
        string[] tokens = command.Split(new char[] {' '}, 2);
        if (tokens.Length == 2)
        {
          ProcessStartInfo psi = new ProcessStartInfo();
          psi.FileName = tokens[0];
          psi.Arguments = tokens[1];
          psi.UseShellExecute = false;
          psi.RedirectStandardOutput = true;
          psi.RedirectStandardError = true;
          psi.CreateNoWindow = true;

          using (Process process = Process.Start(psi))
          {
            using (StreamReader reader = process.StandardOutput)
            {
              string result = reader.ReadToEnd();
              sb.Append("**** " + command + "****" + g.crlf);
              sb.Append(result);
              sb.Append(g.crlf2);
            }

            using (StreamReader reader = process.StandardError)
            {
              string error = reader.ReadToEnd();
              if (error.Length > 0)
              {
                sb.Append("ERROR **** " + command + "**** ERROR" + g.crlf);
                sb.Append(error);
                sb.Append(g.crlf2);
              }
            }
          }
        }
      }

      return sb.ToString();
    }

    private ProcessParms _processParms;
    private Process _process;

    public TaskResult RunExternalProcess(ProcessParms processParms)
    {
      _processParms = processParms;

      TaskResult taskResult = new TaskResult();

      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.FileName = processParms.ExecutablePath;
      startInfo.Arguments = String.Empty;

      foreach (var arg in processParms.Args)
      {
        if (arg.StartsWith("\"") && arg.EndsWith("\""))
        {
          startInfo.Arguments += arg + " ";
        }
        else
        {
          if (arg.Contains(" "))
          {
            startInfo.Arguments += "\"" + arg + "\" ";
          }
          else
          {
            startInfo.Arguments += arg + " ";
          }
        }
      }

      startInfo.CreateNoWindow = true;
      startInfo.UseShellExecute = false;
      startInfo.RedirectStandardError = true;
      startInfo.RedirectStandardOutput = true;

      bool processReturnsTaskResult = GetProcessReturnsTaskResult(processParms.ExecutablePath);

      try
      {
        using (_process = Process.Start(startInfo))
        {
          string output = String.Empty;
          string errorOutput = String.Empty;

          var sbOut = new StringBuilder();
          var sbErr = new StringBuilder();

          while (_process.StandardOutput.Peek() > -1)
          {
            sbOut.Append(_process.StandardOutput.ReadLine());
          }
          output = sbOut.ToString();

          while (_process.StandardError.Peek() > -1)
          {
            sbErr.Append(_process.StandardError.ReadLine()); 
          }
          errorOutput = sbErr.ToString();
          
          _process.WaitForExit();

          taskResult.Code = _process.ExitCode;
          
          if (processReturnsTaskResult)
          {
            var fmt = new BinaryFormatter();
            var bytes = Convert.FromBase64String(output);
            var ms = new MemoryStream(bytes);
            taskResult = (TaskResult)fmt.Deserialize(ms);
            return taskResult;
          }

          if (String.IsNullOrEmpty(errorOutput))
          {
            taskResult.TaskResultStatus = TaskResultStatus.Success;
          }
          else
          {
            taskResult.TaskResultStatus = TaskResultStatus.Failed;
          }
        }
      }
      catch (Exception ex)
      {
        taskResult.Exception = ex;
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
      }

      taskResult.EndDateTime = DateTime.Now;

      return taskResult;
    }

    public void RunExternalProcessNoWait(ProcessParms processParms)
    {
      _processParms = processParms;

      TaskResult taskResult = new TaskResult();

      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.FileName = processParms.ExecutablePath;
      startInfo.Arguments = String.Empty;
      foreach (var arg in processParms.Args)
      {
        if (arg.StartsWith("\"") && arg.EndsWith("\""))
          startInfo.Arguments += arg + " ";
        else
          startInfo.Arguments += "\"" + arg + "\" ";
      }
      startInfo.CreateNoWindow = true;
      startInfo.UseShellExecute = false;
      Process.Start(startInfo);
    }

    private bool GetProcessReturnsTaskResult(string executablePath)
    {
      if (!File.Exists(executablePath))
        return false;

      string exeName = Path.GetFileNameWithoutExtension(executablePath).ToLower();

      if (exeName.In("migr"))
        return true;

      return false;
    }

    private XElement BuildXmlResult(string result)
    {
      XElement xml;

      if (result == "pass")
      {
        xml = new XElement("Results", "Pass");
      }
      else
      {
        xml = new XElement("Results", "Fail");
      }

      return xml;
    }

    public static bool IsProgramInstanceUnique(string displayProgramName)
    {
        string thisProcessName = string.Empty;
        int thisProcessID = 0;
        string debugProcessName = string.Empty;
        List<Process> processes = new List<Process>();
        List<Process> debugProcesses = new List<Process>();
        SortedList<int, Process> allProcesses = new SortedList<int, Process>();

        Logger logger = new Logger();

        try
        {
          Process thisProcess = Process.GetCurrentProcess();
          thisProcessID = thisProcess.Id;

          if (Debugger.IsAttached)
          {
            thisProcessName = thisProcess.ProcessName.Replace(".vshost", String.Empty);
            debugProcessName = thisProcess.ProcessName;
            debugProcesses = Process.GetProcessesByName(debugProcessName).ToList();
          }
          else
          {
            thisProcessName = thisProcess.ProcessName;
          }

          processes = Process.GetProcessesByName(thisProcessName).ToList();

          foreach (Process p in processes)
            allProcesses.Add(p.Id, p);

          foreach (Process p in debugProcesses)
            allProcesses.Add(p.Id, p);

          if (allProcesses.Count == 1)
            return true;


          string otherProcessIDs = string.Empty;
          int otherProcessCount = 0;

          foreach (Process proc in allProcesses.Values)
          {
            if (proc.Id != thisProcessID)
            {
              otherProcessCount += 1;

              if (otherProcessCount == 1)
                otherProcessIDs = proc.Id.ToString();
              else
                otherProcessIDs += ", " + proc.Id.ToString();
            }
          }

          string otherProcessesMessage = string.Empty;
          if (otherProcessCount == 1)
            otherProcessesMessage = "The Process ID of the other " + displayProgramName + " process is " + otherProcessIDs;
          else
            otherProcessesMessage = "The Process IDs of the other " + displayProgramName + " processes are " + otherProcessIDs;


          logger.Log("INFO - Another instance of " + displayProgramName + " is already running - this instance, ProcessId=" + thisProcessID.ToString() + ", will close - Code 114");

          MessageBox.Show("Another instance of " + displayProgramName + " is already running." +
            g.crlf + g.crlf + otherProcessesMessage + g.crlf + g.crlf +
            "This instance of " + displayProgramName + " will close." + g.crlf + g.crlf + "(Code 114)", displayProgramName + " - Another Instance of " + displayProgramName + " is Running",
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        foreach (Process proc in allProcesses.Values)
        {
          if (proc.Id != thisProcessID)
          {
            if (proc.MainWindowHandle == (IntPtr)0)
            {
              logger.Log("INFO - Attempting to kill another instance of " + displayProgramName + " with MainWindowHandle = 0, ProcessID=" + proc.Id.ToString());
              if (KillPredecessor(proc, logger) && allProcesses.Count == 2)
              {
                return true;  
              }
              else
              {
                logger.Log("INFO - Another instance of " + displayProgramName + " will be brought to the front, ProcessID=" + proc.Id.ToString());
                ShowWindowAsync(proc.MainWindowHandle, (int)ShowWindowConstants.SW_SHOWMINIMIZED);
                ShowWindowAsync(proc.MainWindowHandle, (int)ShowWindowConstants.SW_RESTORE);
              }
            }
          }
        }

        return false;
      }
      catch (Exception ex)
      {
        logger.Log("ERROR - Exception occurred retrieving process information in IsProgramInstanceUnique method.", 0, ex);
        return true;
      }
    }

    public static bool KillPredecessor(Process processToKill, Logger logger)
    {
      int processToKillId = processToKill.Id;

      try
      {
        processToKill.Kill();
        System.Threading.Thread.Sleep(2000);
        //The following code will cause an ArgumentException if the process was successfully killed
        Process p = Process.GetProcessById(processToKillId);
        return false; //Return False - process is still running
      }
      catch (ArgumentException argException)
      {
        if (argException.Message.IndexOf(" not running") != -1)
            logger.Log("INFO - Kill of process " + processToKillId.ToString() + " was successful - this instance of this program will continue to run");
        return true;  // the process was successfully killed
      }
      catch (Exception ex)
      {
        logger.Log("ERROR - Error occurred trying to kill ProcesID " + processToKill.Id.ToString() + ".", 0, ex);
        return false;
      }
    }


    public void Dispose()
    {

    }

  }
}

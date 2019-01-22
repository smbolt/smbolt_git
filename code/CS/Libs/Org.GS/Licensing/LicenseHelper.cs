using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Org.GS;

namespace Org.GS.Licensing
{
  public class LicenseHelper
  {
    public static LicenseControl LicenseControl {
      get;
      set;
    }

    private static char[] delim = new char[] { ' ' };

    public static void LicenseCheck()
    {
      string path = g.GetAppPath();
      LicenseControl = new LicenseControl();
      LicenseControl.LoadFromPath(path);
    }

    public static ComputerSignature GetComputerSignature()
    {
      ComputerSignature cs = new ComputerSignature();
      cs.Version = "1.0";
      cs.SignatureDateTime = DateTime.Now;
      cs.ComputerName = Environment.MachineName.Trim().ToUpper();

      string[] commands = new string[] {
        @"BASEBOARD_SERIALNUMBER          wmic baseboard get serialnumber /value ",
        @"CPU_PROCESSORID                 wmic cpu get processorid /value",
        @"DISKDRIVE_SERIALNUMBER          wmic diskdrive get serialnumber /value",
        @"SYSTEMENCLOSURE_SERIALNUMBER    wmic systemenclosure get serialnumber /value",
        @"COMPUTERSYSTEM_MAKE             wmic computersystem get manufacturer /value",
        @"COMPUTERSYSTEM_MODEL            wmic computersystem get model /value",
        @"COMPUTERSYSTEM_TYPE             wmic computersystem get systemtype /value"
      };

      foreach(string command in commands)
      {
        string[] tokens = command.Split(delim, 3, StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length == 3)
        {
          string cmd = tokens[1];
          string args = tokens[2];

          string result = RunCommand(cmd, args);

          switch (tokens[0])
          {
            case "BASEBOARD_SERIALNUMBER":
              cs.Baseboard_SerialNumber = result;
              break;

            case "CPU_PROCESSORID":
              cs.Cpu_ProcessorID = result;
              break;

            case "DISKDRIVE_SERIALNUMBER":
              cs.DiskDrive_SerialNumber = result;
              break;

            case "SYSTEMENCLOSURE_SERIALNUMBER":
              cs.SystemEnclosure_SerialNumber = result;
              break;

            case "COMPUTERSYSTEM_MAKE":
              cs.System_MakeModelType = result;
              break;

            case "COMPUTERSYSTEM_MODEL":
              cs.System_MakeModelType += "|" + result;
              break;

            case "COMPUTERSYSTEM_TYPE":
              cs.System_MakeModelType += "|" + result;
              break;
          }
        }
      }

      return cs;
    }

    public static string RunCommand(string command, string args)
    {
      string result = String.Empty;

      try
      {
        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = command;
        psi.Arguments = args;
        psi.UseShellExecute = false;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;
        psi.CreateNoWindow = true;

        using (Process process = Process.Start(psi))
        {
          process.WaitForExit();
          using (StreamReader reader = process.StandardOutput)
          {
            string rawResult = reader.ReadToEnd().Trim();
            string rawResultFirst = rawResult.Split(new char[] { '\r', '\n' }).First();
            result = PrepareString(rawResultFirst);
          }
        }
        string[] values = result.Split('=');

        if (values.Length == 2)
          return values[1].Trim();

        return String.Empty;
      }
      catch
      {
        return String.Empty;
      }
    }

    private static string PrepareString(string s)
    {
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < s.Length; i++)
      {
        char c = s[i];
        if (Char.IsLetterOrDigit(c) || c == '=')
          sb.Append(c);
      }
      string value = sb.ToString();
      return value.Trim().ToUpper();
    }

    public static TaskResult MatchComputerSignatures(ComputerSignature baseSignature, ComputerSignature currentSignature)
    {
      TaskResult taskResult = new TaskResult();
      taskResult.TaskName = "Match Computer Signatures";

      int matchCount = 0;

      if (baseSignature.ComputerName == currentSignature.ComputerName)
        matchCount++;
      else
      {
        TaskResult computerNameMatchResult = new TaskResult();
        computerNameMatchResult.TaskName = "Match Computer Name";
        computerNameMatchResult.TaskResultStatus = TaskResultStatus.Failed;
        computerNameMatchResult.Message = "Base computer name '" + baseSignature.ComputerName + "' does not match current computer name '" + currentSignature.ComputerName + "'.";
        taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, computerNameMatchResult);
      }

      if (baseSignature.Baseboard_SerialNumber == currentSignature.Baseboard_SerialNumber)
        matchCount++;
      else
      {
        TaskResult baseboardMatchResult = new TaskResult();
        baseboardMatchResult.TaskName = "Match Baseboard Serial Number";
        baseboardMatchResult.TaskResultStatus = TaskResultStatus.Failed;
        baseboardMatchResult.Message = "Base baseboard serial number '" + baseSignature.Baseboard_SerialNumber + "' does not match current baseboard serial number '" + currentSignature.Baseboard_SerialNumber + "'.";
        taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, baseboardMatchResult);
      }

      if (baseSignature.Cpu_ProcessorID == currentSignature.Cpu_ProcessorID)
        matchCount++;
      else
      {
        TaskResult cpuMatchResult = new TaskResult();
        cpuMatchResult.TaskName = "Match First CPU Processor ID";
        cpuMatchResult.TaskResultStatus = TaskResultStatus.Failed;
        cpuMatchResult.Message = "Base cpu processor id '" + baseSignature.Cpu_ProcessorID + "' does not match current cpu processor id '" + currentSignature.Cpu_ProcessorID + "'.";
        taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, cpuMatchResult);
      }

      if (baseSignature.DiskDrive_SerialNumber == currentSignature.DiskDrive_SerialNumber)
        matchCount++;
      else
      {
        TaskResult diskMatchResult = new TaskResult();
        diskMatchResult.TaskName = "Match First Disk Drive Serial Number";
        diskMatchResult.TaskResultStatus = TaskResultStatus.Failed;
        diskMatchResult.Message = "Base disk serial number '" + baseSignature.DiskDrive_SerialNumber + "' does not match current disk serial number '" + currentSignature.DiskDrive_SerialNumber + "'.";
        taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, diskMatchResult);
      }

      if (baseSignature.SystemEnclosure_SerialNumber == currentSignature.SystemEnclosure_SerialNumber)
        matchCount++;
      else
      {
        TaskResult caseMatchResult = new TaskResult();
        caseMatchResult.TaskName = "Match System Enclosure (case) Serial Number";
        caseMatchResult.TaskResultStatus = TaskResultStatus.Failed;
        caseMatchResult.Message = "Base system enclosure serial number '" + baseSignature.SystemEnclosure_SerialNumber + "' does not match current system enclosure serial number '" + currentSignature.SystemEnclosure_SerialNumber + "'.";
        taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, caseMatchResult);
      }

      if (baseSignature.System_MakeModelType == currentSignature.System_MakeModelType)
        matchCount++;
      else
      {
        TaskResult modelMatchResult = new TaskResult();
        modelMatchResult.TaskName = "Match System Model, Make and Type";
        modelMatchResult.TaskResultStatus = TaskResultStatus.Failed;
        modelMatchResult.Message = "Base system make, model and type '" + baseSignature.System_MakeModelType + "' does not match current system make, model and type '" + currentSignature.System_MakeModelType + "'.";
        taskResult.TaskResultSet.Add(taskResult.TaskResultSet.Count, modelMatchResult);
      }

      if (matchCount > 3)
        taskResult.TaskResultStatus = TaskResultStatus.Success;
      else
        taskResult.TaskResultStatus = TaskResultStatus.Failed;

      taskResult.Message = matchCount.ToString() + " of 6 current computer signature elements match the base computer signature elements.";

      return taskResult;
    }

    //public static string ValidateLicenseData(SoftwareLicenseData sld)
    //{
    //  if (sld.CustomerID.IsNotNumeric())
    //    return "CustomerID must be numeric - value received is '" + sld.CustomerID + "'.";

    //  if (sld.SubOrgID.IsNotNumeric())
    //    return "SubOrgID must be numeric - value received is '" + sld.SubOrgID + "'.";

    //  if (sld.LicenseDate == DateTime.MinValue)
    //    return "LicenseDate must contain a valid date - value received is 'MinValue'";

    //  if (sld.LicenseDate == DateTime.MaxValue)
    //    return "LicenseDate must contain a valid date - value received is 'MaxValue'";

    //  if (sld.Scheme != 0)
    //    return "Scheme must be set to 0.";

    //  return String.Empty;
    //}
  }
}

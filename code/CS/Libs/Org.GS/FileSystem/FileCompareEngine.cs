using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class FileCompareEngine : IDisposable
  {
    private string _scriptsFilePath;
    private string _resultsPath;
    private string _resultsFilePath;
    private string _bcompareExePath;
    private string _bcompComPath;

    public FileCompareEngine()
    {
      _bcompareExePath = g.GetCI("BCompareExePath");
      _bcompComPath = g.GetCI("BCompComPath");

      _scriptsFilePath = g.ImportsPath + "\\" + "bcscript.txt";
      _resultsPath = g.ExportsPath;

      File.Delete(_resultsPath + "\\" + "compareResults.txt");
      _resultsFilePath = _resultsPath + "\\" + "compareResults.txt";
    }

    public FileCompareResult CompareFiles(string scriptFile, string baseFile, string compareFile, string reportFile)
    {
      try
      {
        ProcessParms processParms = new ProcessParms();
        processParms.ExecutablePath = _bcompComPath;

        processParms.Args = new string[] {
          "@" + "\"" + scriptFile + "\" ",
          baseFile,
          compareFile,
          reportFile,
          "/silent",
          "/qc=binary"
        };

        using (var processHelper = new ProcessHelper())
        {
          var taskResult = processHelper.RunExternalProcess(processParms);

          switch (taskResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              var result = new FileCompareResult();
              switch (taskResult.Code)
              {
                case 0:
                  result.FilesMatch = false;
                  result.ComparisionReportPath = reportFile;
                  break;

                default:
                  string errorMessage = GetBCMessage(taskResult.Code);
                  throw new Exception("An unexpected BeyondCompare exit code was encountered '" + taskResult.Code.ToString() +
                                      "(" + errorMessage + ")");
              }

              return result;

            default:
              throw new Exception("The difference file generation operation returned a TaskResult with status '" + taskResult.TaskResultStatus.ToString() +
                                  "." + g.crlf + taskResult.FullErrorDetail);
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get a to compare files and (if they are not matching) generate a difference " +
                            "report for base file '" + baseFile + "' and compare file '" + compareFile + "'.", ex);
      }
    }

    private string GetBCMessage(int code)
    {
      switch (code)
      {
        case 0:
          return "Success";
        case 1:
          return "Binary same";
        case 2:
          return "Rules-based same";
        case 11:
          return "Binary differences";
        case 12:
          return "Similar";
        case 13:
          return "Rules-based differences";
        case 14:
          return "Conflicts detected";
        case 100:
          return "Unknown error";
        case 101:
          return "Conflicts detected, merge output not written";
        case 102:
          return "BComp.exe unable to wait until BCompare.exe finishes";
        case 103:
          return "Bcomp.exe cannot find Bcompare.exe";
        case 104:
          return "Trial period expired";
        case 105:
          return "Error loading script";
        case 106:
          return "Script syntax error";
        case 107:
          return "Script failed to load folders or files";

        default:
          return "Unidentified code:" + code.ToString();
      }
    }

    public void Dispose()
    {
    }
  }
}
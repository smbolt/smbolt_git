using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Network;
using Org.GS.Logging;
using Org.GS.Configuration;

namespace Org.CommandUtility
{
  public class Program
  {
    private static ProgramMode _programMode;
    private static Logger _logger = new Logger();
    private static a a;
    private static string _commandLine;
    private static bool _inSilentMode = false;
    private static bool _inWaitMode = false;
    private static bool _overrideWaitMode = false;
    private static Dictionary<int, string> _errorMessages;
    private static int _returnCode = 0;

    static int Main(string[] args)
    {
      _logger.Log("Program CommandUtility starting up.");

      var taskResult = InitializeProgram(args);

      if (taskResult.TaskResultStatus != TaskResultStatus.Success)
      {
        ReportInitializationError(taskResult);
        return taskResult.Code;
      }

      _returnCode = taskResult.Code.ToInt32();

      _logger.Log("Program mode is '" + _programMode.ToString() + "'.");
      if (!_inSilentMode && _programMode != ProgramMode.HelpMode)
        Console.WriteLine("Program mode is '" + _programMode.ToString() + "'." + g.crlf);
      
      switch (_programMode)
      {
        case ProgramMode.PingPort:
          taskResult = RunAsyncMethod(PingPort, String.Empty);
          break;

        case ProgramMode.EditConfig:
          taskResult = EditConfigFile();
          break;

        case ProgramMode.HelpMode:
          taskResult = DisplayHelpInfo();
          break;
      }

      LogAndShowMessageOnConsole(taskResult); 

      DisplaySummary();
      
      if (_overrideWaitMode && !_inWaitMode)
        _inWaitMode = true;
      
      if (_inWaitMode)
      {
        if (!_inSilentMode)
        {
          Console.WriteLine(g.crlf + "Return code is " + _returnCode.ToString());
          Console.WriteLine("Press any key to end program." + g.crlf + "***");
          Console.ReadLine();
        }
      }

      return _returnCode;
    }


    private static void DisplaySummary()
    {
      if (_inSilentMode)
        return;

      Console.WriteLine(g.crlf + "Run Summary");
      _logger.Log("Run Summary");

      switch (_programMode)
      {
        case ProgramMode.PingPort:
          Console.WriteLine("Message for port ping.");
          _logger.Log("Message for port ping.");
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
        });

        int spinCount = 0;
        while (taskRunning)
        {
          if (!_inSilentMode)
          {
            spinCount++;
            switch (spinCount % 4)
            {
              case 0: Console.Write("/"); break;
              case 1: Console.Write("-"); break;
              case 2: Console.Write("\\"); break;
              case 3: Console.Write("|"); break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
          }
          System.Threading.Thread.Sleep(200);
        }

        if (taskResult == null)
          taskResult = new TaskResult("CommandUtility", "A null task result was returned from running of async method '" + methodName + "'.", TaskResultStatus.Failed, 198);

        return taskResult;
      }
      catch (Exception ex)
      {
        taskResult = new TaskResult("CommandUtility").Failed("An exception occurred running async method '" + methodName + "'.", 301, ex);
        Console.WriteLine(taskResult.Message + " The exception report follows: " + g.crlf + ex.ToReport());
        return taskResult;
      }
    }

    private static TaskResult PingPort(string ipAddress)
    {
      TaskResult taskResult = new TaskResult("PingPort");

      try
      {
        return PortPing.PingPort("107.161.158.19", 21);
      }
      catch (Exception ex)
      {
        taskResult.Failed("An exception occurred running 'ListRemoteFolder'.", 302, ex);
        LogAndShowMessageOnConsole(taskResult);
        return taskResult.Failed(302);
      }
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
      if (_programMode == ProgramMode.PingPort)
        _inSilentMode = false;

      if (!_inSilentMode)
        Console.WriteLine("CommandUtility is starting up.");

      if (_programMode == ProgramMode.HelpMode ||
          _programMode == ProgramMode.EditConfig)
        return taskResult.Success();


      // require -a (address) switch for PingPort function
      if (!args.Contains("-a"))
      {
        switch (_programMode)
        {
          case ProgramMode.PingPort:
            return taskResult.Failed(_errorMessages[102], 102);
        }
      }

      int indexOfA = GetIndexOf("-a", args);

      // if we have "-a", it must be followed immediately by address (IP:Port) to try to ping... 
      if (indexOfA > -1)
      {
        int indexOfAValue = indexOfA + 1;

        // if we don't have a parameter following "-a", then we have an error
        if (args.Length < indexOfAValue + 1)
          return taskResult.Failed(_errorMessages[107], 107);

        // since we do have a parameter after "-i", we need to validate that it is a file or folder as appropriate.
        string addressValue = args[indexOfAValue];

        // probably want to validate the format of the ip/port (what about IPV4 and IPV6?)
      }

      return taskResult.Success();
    }


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
        case "-help": return ProgramMode.HelpMode;
        case "-editconfig": return ProgramMode.EditConfig;
        case "-pingport": return ProgramMode.PingPort;
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


      Console.WriteLine("Press any key to exit");
      Console.ReadLine();
      _logger.Log("Program CommandUtility terminating with return code " + _returnCode.ToString() + ".");
    }

    private static TaskResult DisplayHelpInfo()
    {
      Console.WriteLine("-------------------------------------------------");
      Console.WriteLine("CommandUtility Program");
      Console.WriteLine("-------------------------------------------------" + g.crlf);
      Console.WriteLine("See the 'readme.html' file in the same directory as the CommandUtility.exe program for usage information.");

      _inWaitMode = true;

      return new TaskResult("DisplayHelpInfo", String.Empty, true);
    }

    private static void LogAndShowMessageOnConsole(TaskResult taskResult)
    {
      _logger.Log(taskResult);
      taskResult.IsLogged = true;

      Console.Write(g.crlf + "*** ERROR ***" + g.crlf +
                        "Task: " + taskResult.TaskName + g.crlf +
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
                              "  '-portPing'   to attempt to ping an IP address and port" + g.crlf +
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


  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using Org.AX;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS.Code;

namespace Org.migr
{
  class Program
  {
    private static a a;
    private static int _returnCode;
    private static bool _runFromScript = false;
    private static bool _pauseAtEnd = false;
    private static string _srcEnv = String.Empty;
    private static string _srcApp = String.Empty;
    private static string _tgtApp = String.Empty;
    private static string _tgtAppType = String.Empty;
    private static string _srcAppType = String.Empty;
    private static string _localModule = String.Empty;
    private static string _localTgtApp = String.Empty;
    private static string _localVersion = String.Empty;
    private static bool _isDryRun = false;
    private static bool _inTestMode = false;
    private static string _profileFile = String.Empty;
    private static string _profileName = String.Empty;
    private static AxEngine _axEngine;
    private static AxProfileSet _axProfileSet;
    private static Logger _logger;
    private static string _profilesPath;
    private static ObjectFactory2 _f;
    private static TaskResult _taskResult;
    private static string _serializedTaskResult;

    static int Main(string[] args)
    {
      try
      {
        args = Initialize(args);

        _logger = new Logger();
        _axEngine = new AxEngine();

        if (!_runFromScript)
          Console.WriteLine("migr starting...");
      }
      catch (Exception ex)
      {
        _returnCode = 16;
        if (_logger != null)
          _logger.Log("migr completed with return code " + _returnCode.ToString() + "." + ex.ToReport());

        if (!_runFromScript)
        {
          Console.WriteLine("migr completed with return code " + _returnCode.ToString() + ".");
          Console.WriteLine();
          Console.WriteLine("*** INITIALIZATION ERROR ***");
          Console.WriteLine(ex.ToReport());
          Console.WriteLine("Press any key to end program");
          Console.ReadKey();
        }

        return _returnCode;
      }

      try
      {
        if (args.Length == 0)
        {
          _logger.Log("No command received in command line arguments."); 
          Console.WriteLine("No command received");
          return 16;
        }

        _taskResult = ProcessCommand(args);
        _serializedTaskResult = SerializeTaskResult(_taskResult);

        //var deserializedTaskResult = DeserializeTaskResult(_serializedTaskResult);

        if (!_runFromScript)
        {
          Console.WriteLine("migr completed with return code " + _returnCode.ToString() + "...");
          Console.WriteLine();
          Console.WriteLine("Press any key to end program");
          Console.ReadKey();
        }

        if (_runFromScript)
        {
          Console.Write(_serializedTaskResult);
          if (_pauseAtEnd)
            Console.ReadKey();
        }

        return _returnCode;
      }
      catch (Exception ex)
      {
        _returnCode = 16;
        _logger.Log("migr completed with return code " + _returnCode.ToString() + "." + g.crlf + ex.ToReport());

        if (!_runFromScript)
        {
          Console.WriteLine("migr completed with return code " + _returnCode.ToString() + ".");
          Console.WriteLine();
          Console.WriteLine("*** ERROR ***");
          Console.WriteLine(ex.ToReport());
          Console.WriteLine("Press any key to end program");
          Console.ReadKey();
        }
      }

      return _returnCode;
    }

    private static string SerializeTaskResult(TaskResult taskResult)
    {
      try
      {
        using (var ms = new MemoryStream())
        {
          var fmt = new BinaryFormatter();
          fmt.Serialize(ms, taskResult);
          byte[] bytes = ms.ToArray();
          ms.Close();
          string bytes64 = Convert.ToBase64String(bytes);
          return bytes64;
        }
      }
      catch (Exception ex)
      {
        return "Error serializing task result.";
      }
    }

    private static TaskResult DeserializeTaskResult(string s)
    {
      try
      {
        var fmt = new BinaryFormatter();
        var bytes = Convert.FromBase64String(s);
        var ms = new MemoryStream(bytes);
        var taskResult = (TaskResult)fmt.Deserialize(ms);
        return taskResult;
      }
      catch (Exception ex)
      {
        return new TaskResult("MigrExecution", "An exception occurred while attempting to deserialize the task result.", TaskResultStatus.Failed, 0, ex); 
      }
    }
    

    private static string[] Initialize(string[] args)
    {
      _returnCode = 0;

      try
      {
        PeekAtArgs(args);

        a = new a();

        string configCommandLine = g.CI("CommandLine"); 

        if (configCommandLine.IsNotBlank())
          args = GetArgsFromCommandLine(configCommandLine);

        _profilesPath = g.CI("ProfilesPath");
        if (_profilesPath.IsBlank())
          _profilesPath = g.ImportsPath;

        if (_profilesPath.IsBlank())
          throw new Exception("Path to action set profiles configuration is blank.");

        _f = new ObjectFactory2(g.CI("InDiagnosticsMode").ToBoolean());
        _f.LogToMemory = g.CI("LogToMemory").ToBoolean();

        return args;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred during program initialization.", ex);
      }
    }

    private static void PeekAtArgs(string[] args)
    {
      int indexOfScripted = GetIndexOf("-scripted", args);
      if (indexOfScripted > -1)
        _runFromScript = true;

      int indexOfPauseAtEnd = GetIndexOf("-pauseatend", args);
      if (indexOfPauseAtEnd > -1)
        _pauseAtEnd = true;
    }

    private static TaskResult ProcessCommand(string[] args)
    {
      try
      {
        ProcessArgs(args);

        if (_inTestMode)
        {
          var testTaskResult = new TaskResult("MigrTest", "Migr is running in test mode and returing this message in a TaskResult object.", true);
          return testTaskResult;
        }


        if (_profileName.IsBlank())
          throw new Exception("No profile name was read from the command line.");

        if (_profileFile.IsNotBlank())
        {
          string profileFullPath = _profilesPath + @"\" + _profileFile + ".mpx";
          if (!File.Exists(profileFullPath))
            throw new Exception("Profile full file path '" + profileFullPath + "' does not exist.");

          string profilesString = File.ReadAllText(profileFullPath);
          XElement profilesXml = XElement.Parse(profilesString);
          _axProfileSet = _f.Deserialize(profilesXml) as AxProfileSet;
        }
        else
        {
          var mpxFiles = Directory.GetFiles(_profilesPath, "*.mpx");

          foreach (var mpxFile in mpxFiles)
          {
            string mpxString = File.ReadAllText(mpxFile);
            XElement mpXml = XElement.Parse(mpxString);
            var axProfSet = _f.Deserialize(mpXml) as AxProfileSet;
            foreach (var axProf in axProfSet)
            {
              if (axProf.Value.NameLower == _profileName.ToLower().Trim())
              {
                _axProfileSet = axProfSet;
                break; 
              }
            }
            if (_axProfileSet != null)
              break; 
          }
        }

        if (_axProfileSet == null)
          throw new Exception("Could not find AxProfile '" + args[0] + "' in any .mpx file in '" + _profilesPath + "'.");

        var profileParms = new AxProfileParms();

        
        profileParms.ParmSet.AddParm("IsDryRun", _isDryRun);
        profileParms.ParmSet.AddParm("SrcEnv", _srcEnv);
        profileParms.ParmSet.AddParm("SrcApp", _srcApp);
        profileParms.ParmSet.AddParm("TgtApp", _tgtApp);
        profileParms.ParmSet.AddParm("TgtAppType", _tgtAppType);
        profileParms.ParmSet.AddParm("SrcAppType", _srcAppType);
        profileParms.ParmSet.AddParm("LocalModule", _localModule);
        profileParms.ParmSet.AddParm("LocalTgtApp", _localTgtApp);
        profileParms.ParmSet.AddParm("LocalVersion", _localVersion);

        _returnCode = 0;

        var taskResult = _axEngine.RunAxProfile(_axProfileSet, _profileName, profileParms);

        return taskResult;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to load the ActionProfileSet.", ex);
      }
    }

    private static string[] WaitForCommand()
    {
      Wait:
      string commandLine = Console.ReadLine();
      string[] args = new string[0];
      if (commandLine.IsBlank())
        goto Wait;

      if (commandLine.Trim().ToLower() == "exit")
        Environment.Exit(0);

      if (commandLine.Trim().ToLower() == "help")
      {
        DisplayHelpInfo();
        goto Wait;
      }

      args = GetArgsFromCommandLine(commandLine);

      return args;
    }

    private static void ProcessArgs(string[] args)
    {
      try
      {
        if (args.Length == 0)
          return;

        // change all switches to lower case
        for (int i = 0; i < args.Length; i++)
        {
          if (args[i].StartsWith("-"))
            args[i] = args[i].ToLower();
        }
        
        if (args.Contains("-dryrun"))
          _isDryRun = true;

        if (args.Contains("-test"))
          _inTestMode = true;

        if (_inTestMode)
          return;

        // srcEnv (source environment)
        if (!args.Contains("-srcenv"))
          throw new Exception("The '-srcEnv' command line argument is required.");

        int indexOfSrcEnv = GetIndexOf("-srcenv", args);
        int indexOfValueSrcEnv = indexOfSrcEnv + 1;

        // if we don't have a parameter following "-srcEnv", then we have an error
        if (args.Length < indexOfValueSrcEnv + 1)
          throw new Exception("The '-srcEnv' command line argument must be followed by the migration source value parameter.");

        _srcEnv = args[indexOfValueSrcEnv];

        if (!_srcEnv.In("bld,mgr"))
          throw new Exception("The '-srcEnv' command line argument must be followed by either 'bld' (build environment) or 'mgr' (migration environment).");

        // srcApp (source application)
        if (!args.Contains("-srcapp"))
          throw new Exception("The '-srcApp' command line argument is required.");

        int indexOfSrcApp = 0;
        int indexOfValueSrcApp = 0;

        indexOfSrcApp = GetIndexOf("-srcapp", args);
        indexOfValueSrcApp = indexOfSrcApp + 1;

        // if we don't have a parameter following "-srcApp", then we have an error
        if (args.Length < indexOfValueSrcApp + 1)
          throw new Exception("The '-srcApp' command line argument must be followed by the source application value parameter.");

        _srcApp = args[indexOfValueSrcApp];

        // tgtApp (target application)
        if (args.Contains("-tgtapp"))
        {
          int indexOfTgtApp = 0;
          int indexOfValueTgtApp = 0;

          indexOfTgtApp = GetIndexOf("-tgtapp", args);
          indexOfValueTgtApp = indexOfTgtApp + 1;

          // if we don't have a parameter following "-tgtApp", then we have an error
          if (args.Length < indexOfValueTgtApp + 1)
            throw new Exception("The '-tgtApp' command line argument must be followed by the target application value parameter.");
        
          _tgtApp = args[indexOfValueTgtApp];
        }
        else
        {
          _tgtApp = _srcApp;
        }

        // appType (application type for Server destination)

        if (!args.Contains("-srcapptype"))
          throw new Exception("The '-srcAppType' command line argument is required.");

          int indexOfSrcAppType = GetIndexOf("-srcapptype", args);
          int indexOfValueSrcAppType = indexOfSrcAppType + 1;
          if (args.Length < indexOfValueSrcAppType + 1)
            throw new Exception("The -srcAppType command line argument must be followed by the AppType parameter.");
          _srcAppType = args[indexOfValueSrcAppType];

        if(_srcEnv == "mgr")
        {
          if (!args.Contains("-tgtapptype"))
            throw new Exception("The '-tgtAppType' command line argument is required when the source environment is 'mgr'.");

          int indexOfTgtAppType = GetIndexOf("-tgtapptype", args);
          int indexOfValueTgtAppType = indexOfTgtAppType + 1;

          // if we don't have a parameter following "-tgtAppType", then we have an error
          if (args.Length < indexOfValueTgtAppType + 1)
            throw new Exception("The '-tgtAppType' command line argument must be followed by the migration/server application type parameter.");

          _tgtAppType = args[indexOfValueTgtAppType];

          if (!_tgtAppType.In("webservices,websites,windowsservices,modules,apps"))
            throw new Exception("The '-tgtAppType' command line argument must be followed by either 'webservices', 'websites', 'windowsservices', 'modules' or 'apps'.");
            
        }


        if (args.Contains("-locmodule"))
        {
          if (!args.Contains("-localtgtapp"))
            throw new Exception("There must be a -localTgtApp command line argument if a local module exists.");
          if(!args.Contains("-localversion"))
            throw new Exception("There must be a -localVersion command line argument if a local module exists.");
            
          int indexOfLocalModule = GetIndexOf("-locmodule", args);
          int indexOfValueLocalModule = indexOfLocalModule + 1;
          if (args.Length < indexOfValueLocalModule + 1)
            throw new Exception("The -localModule command line argument must be followed by the migration/server application type parameter.");
          _localModule = args[indexOfValueLocalModule];
          
          int indexOfLocalTgtApp = GetIndexOf("-localtgtapp", args);
          int indexOfValueLocalTgtApp = indexOfLocalTgtApp + 1;
          if (args.Length < indexOfValueLocalTgtApp + 1)
            throw new Exception("The -localTgtApp command line argument must be followed by the migration/server application type parameter.");
          _localTgtApp = args[indexOfValueLocalTgtApp];
            
          int indexOfLocalVersion = GetIndexOf("-localversion", args);
          int indexOfValueLocalVersion = indexOfLocalVersion + 1;
          if (args.Length < indexOfValueLocalVersion + 1)
            throw new Exception("The -localVersion command line argument must be followed by the migration/server application version parameter.");
          _localVersion = args[indexOfValueLocalVersion];
        }

        string[] tokens = args[0].Split(Constants.PeriodDelimiter, StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length == 2)
        {
          _profileFile = tokens[0];
          _profileName = tokens[1];
        }
        else if (tokens.Length == 1)
        {
          _profileFile = "Profiles";
          _profileName = tokens[0];
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to process args.", ex);
      }
    }
    
    private static int GetIndexOf(string item, string[] args)
    {
      if (args == null || args.Length == 0)
        return -1;

      for (int i = 0; i < args.Length; i++)
      {
        if (args[i].ToLower() == item.ToLower().Trim())
          return i;
      }

      return -1;
    }

    private static TaskResult ReportOnResult(TaskResult taskResult)
    {
      try
      {
        StringBuilder report = new StringBuilder();

        if (_isDryRun && !_runFromScript)
          report.Append(g.crlf + "### DRY-RUN ###");

        if (_inTestMode)
        {
          report.Append("Migr running in test mode.");
        }
        else
        {

          foreach (var childTaskResult in taskResult.TaskResultSet.Values)
          {
            switch (childTaskResult.TaskResultStatus)
            {
              case TaskResultStatus.Success:
                var axResult = childTaskResult.Object as AxResult;
                if (axResult == null)
                  continue;
                report.Append(g.crlf + axResult.AxionReport);
                break;

              default:
                report.Append(g.crlf + "migr encountered an error while processing command from TaskResult '" + childTaskResult.TaskName + "'" + g.crlf + taskResult.Message);
                _returnCode = 16;
                break;
            }
          }
        }

        string rpt = report.ToString();

        taskResult.Message = rpt;
        return taskResult;

        if (!_runFromScript)
          Console.WriteLine(report.ToString());
        else
        {
          if (_pauseAtEnd)
          {
            Console.WriteLine(report.ToString());
            Console.ReadKey();
          }
        }

        _logger.Log(report.ToString());
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to report on the task result.", ex);
      }
    }

    private static string[] GetArgsFromCommandLine(string commandLine)
    {
      try
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
          if (i > 0)
            argArray[i] = argArray[i].Replace("|", " ").ToLower().Trim();
        }

        return argArray;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to get args from command line entry '" + commandLine + "'.", ex);
      }
    }

    private static void DisplayHelpInfo()
    {
      Console.WriteLine();
      Console.WriteLine("-------------------------------------------------");
      Console.WriteLine("migr program - help information");
      Console.WriteLine("-------------------------------------------------" + g.crlf);

      Console.WriteLine("GENERAL DESCRIPTION" + g.crlf);
      Console.WriteLine("The migr program performs file migrations typically used for deploying");
      Console.WriteLine("binaries and configuration files, etc. to various runtime environments." + g.crlf);

      Console.WriteLine("Regardless, the program is a general purpose tool for moving files from a");
      Console.WriteLine("source folder to a target folder based on configuration." + g.crlf);

      Console.WriteLine("The specific migration tasks to be accomplished are specified through a migr");
      Console.WriteLine("profile XML configuration file (.mpx)." + g.crlf);

      Console.WriteLine("Multiple migration tasks can be assembled into a migration profile.  And");
      Console.WriteLine("multiple migration profiles can exist in a single configuration file." + g.crlf);

      Console.WriteLine("When the migr program is run it is required that a profile name be specified");
      Console.WriteLine("as the first parameter following the executable name on the command line.");
      Console.WriteLine("If no parameter is supplied, the program will run in 'help mode' and will");
      Console.WriteLine("display this help information." + g.crlf);

      Console.WriteLine("CONCERNING CONFIGURATION FILES" + g.crlf);
      Console.WriteLine("The migr program uses two configuation files. One is a general purpose");
      Console.WriteLine(@"application configuration file named AppConfig.xml which is located at the path");
      Console.WriteLine(@"[exe-path]\ProgramData\AppData\AppConfig\AppConfig.xml. The second");
      Console.WriteLine("configuration file will contain the migration profiles and will be located at");
      Console.WriteLine(@"the path DEV-MAIN\Migr\Profiles unless the 'ProfilesPath' configuration item");
      Console.WriteLine("(CI) in the AppConfig.xml file specifies a different path." + g.crlf);

      Console.WriteLine("APPLICATION COMMANDS" + g.crlf);
      Console.WriteLine("[AxProfile Name].[Profiles File Name] OR [AxProfile Name]");
      Console.WriteLine("If not File Name is specified, the migr program will search through all");
      Console.WriteLine(".mpx files in the specified 'ProfilesPath'. If the AxProfile");
      Console.WriteLine("Name is present in more than 1 .mpx file, migr will return with error.");
      Console.WriteLine("You may also append ' -dr' for DryRun or ' -s' to run from script.");
    }
  }
}

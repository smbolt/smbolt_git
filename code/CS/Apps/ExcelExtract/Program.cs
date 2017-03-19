using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Gulfport.MarkWestStmtXlsToXml;
using Org.GS.Logging;
using Org.GS;

namespace Org.ExcelExtract
{
  public enum FileProcessMode
  {
    SingleFileMode,
    ProcessFolderMode,
    WatchFolderMode,
    InfoOnlyMode,
    InvalidMode
  }

  class Program
  {
    private static string _programName = "ExcelExtract";
    private static FileProcessMode _fileProcessMode;
    private static Logger _logger = new Logger();
    private static a a;
    private static string _commandLine;
    private static bool _inSilentMode = false;
    private static bool _inWaitMode = false;
    private static bool _echoText = false;

    private static string _inputFilePath;
    private static string _outputFilePath;
    private static string _inputFolderPath;
    private static string _outputFolderPath;
    
    private static string _errorMessage = String.Empty;
    private static int _returnCode = 0;


    static int Main(string[] args)
    {
      _logger.Log("Program " + _programName + " starting up.");

      InitializeProgram(args);

      if (_returnCode != 0)
      {
        Console.WriteLine(g.crlf + "*** ERROR ***");
        Console.WriteLine("*** ERROR ***");
        Console.WriteLine("*** ERROR ***" + g.crlf);
        Console.WriteLine("An error has occurred in " + _programName + ".  The error message is below." + g.crlf2 +
                          _errorMessage + g.crlf2 +
                          "Return code is " + _returnCode.ToString() + g.crlf);
        Console.WriteLine("Press any key to exit");
        Console.ReadLine();
        _logger.Log("Program " + _programName + " terminating with return code " + _returnCode.ToString() + ".");
        return _returnCode;
      }

      switch (_fileProcessMode)
      {
        case FileProcessMode.SingleFileMode:
          _returnCode = ProcessSingleFile();
          break;

        case FileProcessMode.ProcessFolderMode:
          _returnCode = ProcessFolder();
          break;

        case FileProcessMode.WatchFolderMode:
          _returnCode = WatchFolder();
          break;

        case FileProcessMode.InfoOnlyMode:
          _returnCode = DisplayUsageInfo();
          break;
      }

      _logger.Log("Program " + _programName + " terminating with return code " + _returnCode.ToString() + ".");

      if (_inWaitMode)
      {
        Console.WriteLine("Press any key to end program.");
        Console.ReadLine();
      }
      
      return _returnCode;
    }

    private static void InitializeProgram(string[] args)
    {
      try
      {
        a = new a();

        _commandLine = g.CI("CommandLine");

        if (_commandLine.IsNotBlank())
          args = GetArgsFromCommandLine(_commandLine);

        ParseCommandTokens(args);
      }
      catch (Exception ex)
      {
        string message = ex.ToReport();
        _returnCode = 199;
      }
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

    private static void ParseCommandTokens(string[] args)
    {
      if (args.Length == 0)
      {
        _fileProcessMode = FileProcessMode.InfoOnlyMode;
        return;
      }

      int argNumber = 0;

      string modeSwitch = args[argNumber].ToLower().Trim();
      _fileProcessMode = GetMode(modeSwitch);

      if (_fileProcessMode == FileProcessMode.InvalidMode)
      {
        _errorMessage = _programName + " could not determine the requested mode of operation." + g.crlf + 
                        "The first command line parameter must be one of the following: " + g.crlf +
                        "  '-h'   to display usage information and instructions" + g.crlf +
                        "  '-s'   to process a single file" + g.crlf +
                        "  '-f'   to process a folder of files" + g.crlf +
                        "  '-w'   to watch folder for files and proces them upon arrival" + g.crlf2 +
                        "Use 'ExcelExtract -h' for help.";
        _returnCode = 101;
        return;
      }

      if (_fileProcessMode == FileProcessMode.InfoOnlyMode)
        return;



      if (!args.Contains("-i"))
      {
        switch(_fileProcessMode)
        {
          case FileProcessMode.SingleFileMode:
            _errorMessage = "The " + _programName + " command line must include an '-i' switch to specify the input file to be parsed." + g.crlf + 
                            "Use 'ExcelExtract -h' for help.";
            _returnCode = 102;
            return;

          case FileProcessMode.ProcessFolderMode:
            _errorMessage = "The " + _programName + " command line must include an '-i' switch to specify the input folder of files." + g.crlf +
                            "Use 'ExcelExtract -h' for help.";
            _returnCode = 103;
            return;

          case FileProcessMode.WatchFolderMode:
            _errorMessage = "The " + _programName + " command line must include an '-i' switch to specify the input folder to watch for files." + g.crlf +
                            "Use 'ExcelExtract -h' for help.";
            _returnCode = 104;
            return;
        }
      }

      if (!args.Contains("-o"))
      {
        switch(_fileProcessMode)
        {
          case FileProcessMode.SingleFileMode:
            _errorMessage = "The " + _programName + " command line must include an '-o' switch to specify the output file name." + g.crlf + 
                            "Use 'ExcelExtract -h' for help.";
            _returnCode = 105;
            return;

          case FileProcessMode.ProcessFolderMode:
          case FileProcessMode.WatchFolderMode:
            _errorMessage = "The " + _programName + " command line must include an '-o' switch to specify the output folder location." + g.crlf +
                            "Use 'ExcelExtract -h' for help.";
            _returnCode = 105;
            return;
        }
      }


      // PROCESS THE INPUT FILE OR FOLDER SPECIFICATIONS FROM THE COMMAND LINE

      int indexOfI = GetIndexOf("-i", args);
      int indexOfValueI = indexOfI + 1;

      if (args.Length < indexOfValueI + 1)
      {
        switch(_fileProcessMode)
        {
          case FileProcessMode.SingleFileMode:
            _errorMessage = "The " + _programName + " command line must include the input file name immediately following the '-i' switch." + g.crlf +
                            "Use 'ExcelExtract -h' for help.";
            _returnCode = 107;
            return;

          case FileProcessMode.ProcessFolderMode:
          case FileProcessMode.WatchFolderMode:
            _errorMessage = "The " + _programName + " command line must include the input folder name immediately following the '-i' switch." + g.crlf +
                            "Use 'ExcelExtract -h' for help.";
            _returnCode = 108;
            return;
        }
      }

      string inputValue = args[indexOfValueI];

      if (_fileProcessMode == FileProcessMode.SingleFileMode)
      {
        if (!File.Exists(inputValue))
        {
          _errorMessage = "The " + _programName + " command line parameter immediately following the '-i' switch must be a valid input file name." + g.crlf + 
                          "The value '" + inputValue + "' does not correspond to a valid file name." + g.crlf +
                          "Use 'ExcelExtract -h' for help.";
          _returnCode = 109;
          return;
        }
      }
      else
      {
        if (!Directory.Exists(inputValue))
        {
          _errorMessage = "The " + _programName + " command line parameter immediately following the '-i' switch must be a valid input folder name." + g.crlf + 
                          "The value '" + inputValue + "' does not correspond to a valid folder name." + g.crlf +
                          "Use 'ExcelExtract -h' for help.";
          _returnCode = 110;
          return;
        }
      }

      // use validated input path value
      if (_fileProcessMode == FileProcessMode.SingleFileMode)
        _inputFilePath = inputValue;
      else
        _inputFolderPath = inputValue;


      // PROCESS THE OUTPUT FILE OR FOLDER SPECIFICATIONS FROM THE COMMAND LINE

      int indexOfO = GetIndexOf("-o", args);
      int indexOfValueO = indexOfO + 1;

      if (args.Length < indexOfValueO + 1)
      {
        switch(_fileProcessMode)
        {
          case FileProcessMode.SingleFileMode:
            _errorMessage = "The " + _programName + " command line must include the output file name immediately following the '-o' switch." + g.crlf +
                            "Use 'ExcelExtract -h' for help.";
            _returnCode = 111;
            return;

          case FileProcessMode.ProcessFolderMode:
          case FileProcessMode.WatchFolderMode:
            _errorMessage = "The " + _programName + " command line must include the output folder name immediately following the '-o' switch." + g.crlf +
                            "Use 'ExcelExtract -h' for help.";
            _returnCode = 112;
            return;
        }
      }

      string outputValue = args[indexOfValueO];

      if (_fileProcessMode == FileProcessMode.SingleFileMode)
      {
        if (!g.IsValidPath(outputValue))
        {
          _errorMessage = "The " + _programName + " command line parameter immediately following the '-o' switch must be a valid output file name (regardless of whether the file exists)." + g.crlf +
                          "The value '" + outputValue + "' does not correspond to a valid file path name." + g.crlf +
                          "Use 'ExcelExtract -h' for help.";
          _returnCode = 113;
          return;
        }

        string directoryName = Path.GetDirectoryName(outputValue);

        if (!Directory.Exists(directoryName))
        {
          try
          {
            Directory.CreateDirectory(directoryName);
          }
          catch (Exception ex)
          {
            _errorMessage = "The " + _programName + " program is unable to create the non-existent output folder at '" + directoryName + "' to contain the output file." + g.crlf + 
                            "The exception message follows." + g.crlf + ex.MessageReport(); 
            _returnCode = 116;
            return;
          }
        }
      }
      else
      {
        if (!g.IsValidPath(outputValue))
        {
          _errorMessage = "The " + _programName + " command line parameter immediately following the '-o' switch must be a valid output folder path name (whether or not it exists)." + g.crlf +
                          "The value '" + outputValue + "' does not correspond to a valid folder path name." + g.crlf +
                          "Use 'ExcelExtract -h' for help.";
          _returnCode = 115;
          return;
        }

        if (!Directory.Exists(outputValue))
        {
          try
          {
            Directory.CreateDirectory(outputValue);
          }
          catch (Exception ex)
          {
            _errorMessage = "The " + _programName + " program is unable to create the non-existent output folder at '" + outputValue + "'." + g.crlf + 
                            "The exception message follows." + g.crlf + ex.MessageReport(); 
            _returnCode = 116;
            return;
          }
        }

      }

      // use validated output path value
      if (_fileProcessMode == FileProcessMode.SingleFileMode)
        _outputFilePath = outputValue;
      else
        _outputFolderPath = outputValue;
    }

    private static FileProcessMode GetMode(string modeSwitch)
    {
      switch (modeSwitch)
      {
        case "-h": return FileProcessMode.InfoOnlyMode;
        case "-s": return FileProcessMode.SingleFileMode;
        case "-f": return FileProcessMode.ProcessFolderMode;
        case "-w": return FileProcessMode.WatchFolderMode;
      }

      return FileProcessMode.InvalidMode;
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

    private static int ProcessSingleFile()
    {
      try
      {
        if (!_inSilentMode)
        {
          Console.WriteLine( _programName + "  running in single file mode will extract text from file " + g.crlf + "  " + _inputFilePath +
                            g.crlf + "    into text file " + g.crlf + "  " + _outputFilePath + "." + g.crlf); 
        }

        _logger.Log("Attempting to extract data from Excel file " + _inputFilePath + ".");
        var taskResult = ExtractEngine.ExtractExcelToXml(_inputFilePath);

        if (taskResult.TaskResultStatus == TaskResultStatus.Success)
        {
          File.WriteAllText(_outputFilePath, taskResult.Xml.ToString());
          _logger.Log("Data extraction from Excel file was successful, " + taskResult.Data.Length.ToString("###,###,##0") +
                      " bytes extracted in duration " + taskResult.DurationString + ".");

          if (!_inSilentMode)
            Console.WriteLine("Data extraction was successful - " + taskResult.Data.Length.ToString("###,###,##0") + " bytes written.");

          if (_echoText)
          {
            Console.WriteLine("Echo option is ON - extracted data follows:" + g.crlf + "----EXTRACTED DATA ----" + g.crlf + taskResult.Data + g.crlf +
                              "----END OF EXTRACTED DATA---");
          }

          return 0;
        }
        else
        {
          _logger.Log("Error occurred during data extraction.  Error message follows." + g.crlf +
                      taskResult.FullErrorDetail + ".");
          return 201;
        }
      }
      catch (Exception ex)
      {
        if (!_inSilentMode)
          Console.WriteLine("Data extraction failed - error message follows: " + g.crlf + ex.ToReport()); 

        _logger.Log("An exception occurred attempting to extract data from Excel file '" + _inputFilePath + 
                    ". The exception message follows." + g.crlf + ex.ToReport());
        return 202;
      }
    }

    private static int ProcessFolder()
    {



      return 0;
    }

    private static int WatchFolder()
    {



      return 0;
    }


    private static int DisplayUsageInfo()
    {
      Console.WriteLine(g.crlf);
      Console.WriteLine("---------------------------");
      Console.WriteLine("ExcelExtract Program Usage Info");
      Console.WriteLine("GENERAL DESCRIPTION" + g.crlf);
      Console.WriteLine("ExcelExtract will parse Excel files and extract selected data from them and write XML data in an output file." + g.crlf2);

      Console.WriteLine("MODES OF OPERATION" + g.crlf);
      Console.WriteLine("ExcelExtract will operate in one of four operational modes as follows:" + g.crlf2);

      Console.WriteLine("1. Help Mode   [ '-h' switch or no command line parameters ]");
      Console.WriteLine("   ---------" + g.crlf);
      Console.Write("ExcelExtract is currently operating in 'Help Mode' which is used to provide usage information and instructions.  ");
      Console.Write("ExcelExtract runs in 'Help Mode' when the '-h' switch is passed in as the first parameter on the command line.  ");
      Console.WriteLine("ExcelExtract also runs in 'Help Mode' when no parameters are entered on the command line.  " + g.crlf2);
      Console.WriteLine("Example:" + g.crlf);
      Console.WriteLine("  Enter the command :    ExcelExtract -h ");
      Console.WriteLine("                 or :    ExcelExtract " + g.crlf2);

      Console.WriteLine("2. Single File Mode   [ '-s' switch ]");
      Console.WriteLine("   ----------------" + g.crlf);
      Console.Write("ExcelExtract runs in 'Single File Mode' when the '-s' switch is the first command line parameter.  ");
      Console.Write("'Single File Mode' requires that additional parameters are entered to specify the name of the input Excel file ");
      Console.WriteLine("and the output file." + g.crlf);
      Console.WriteLine("The '-i' switch followed by the full path of the input Excel file is used to specify the input file name." + g.crlf);
      Console.WriteLine("The '-o' switch followed by the full path of the output file is used to specify the output file name." + g.crlf);
      Console.WriteLine("Example:" + g.crlf);
      Console.WriteLine("  ExcelExtract -s -i \"C:\\InputFolder\\ExcelFile1.xslx\" -o \"C:\\OutputFolder\\XmlFile1.xml\" " + g.crlf);
      Console.WriteLine("The file names must be enclosed in double quotes (\") if there are embedded spaces in the file name." + g.crlf2);

      Console.WriteLine("3. Process Folder Mode   [ '-f' switch ]");
      Console.WriteLine("   -------------------" + g.crlf);
      Console.Write("ExcelExtract runs in 'Process Folder Mode' when the '-f' switch is the first command line parameter.  ");
      Console.Write("'Process Folder Mode' requires that additional parameters are entered to specify the names of the folder containing the Excel files to be processed ");
      Console.WriteLine("and the folder where the output files will be placed." + g.crlf);
      Console.WriteLine("The '-i' switch followed by the full path of the input folder is used to specify the input folder." + g.crlf);
      Console.WriteLine("The '-o' switch followed by the full path of the output folder is used to specify the output folder for files." + g.crlf);
      Console.WriteLine("Example:" + g.crlf);
      Console.WriteLine("  ExcelExtract -f -i \"C:\\InputFolder\" -o \"C:\\OutputFolder\" " + g.crlf);
      Console.WriteLine("The folder names must be enclosed in double quotes (\") if there are embedded spaces in the file name." + g.crlf2);

      Console.WriteLine("4. Watch Folder Mode   [ '-w' switch ]");
      Console.WriteLine("   -----------------" + g.crlf);
      Console.Write("ExcelExtract runs in 'Watch Folder Mode' when the '-w' switch is the first command line parameter.  ");
      Console.Write("'Watch Folder Mode' requires that additional parameters are entered to specify the names of the folder containing the Excel files to be watched ");
      Console.WriteLine("and the folder where the output files will be placed when processed." + g.crlf);
      Console.WriteLine("The '-i' switch followed by the full path of the input folder is used to specify the input folder to be watched." + g.crlf);
      Console.WriteLine("The '-o' switch followed by the full path of the output folder is used to specify the output folder for files." + g.crlf);
      Console.WriteLine("Example:" + g.crlf);
      Console.WriteLine("  ExcelExtract -w -i \"C:\\InputFolder\" -o \"C:\\OutputFolder\" " + g.crlf);
      Console.WriteLine("The folder names must be enclosed in double quotes (\") if there are embedded spaces in the file name." + g.crlf2);

      Console.WriteLine("*** IMPORTANT POINTS ***" + g.crlf);
      Console.Write("When runing in 'Help Mode' ExcelExtract requires the user to press a key to terminate the program - otherwise the help contents would display ");
      Console.WriteLine("and the user would not have time to read them before the program closes." + g.crlf);

      Console.WriteLine("When run in 'Single File Mode' or 'Process Folder Mode' ExcelExtract will process the file(s) and immediately terminate.");
      Console.Write("If it is desired that ExcelExtract not close immediately after finishing single file or folder mode processing then use the '-wait' switch ");
      Console.WriteLine("after the parameters specifying the file or folder names." + g.crlf);
      Console.WriteLine("Example:" + g.crlf);
      Console.WriteLine("  ExcelExtract -f -i \"C:\\InputFolder\" -o \"C:\\OutputFolder\" -wait" + g.crlf);
      Console.WriteLine("The '-wait' switch is only applicable in 'Single File Mode' or 'Process Folder Mode' and will be ignored if entered in other program modes." + g.crlf2);

      Console.Write("When runing in 'Watch Folder Mode' ExcelExtract will run leaving a command window open while it watches for files to be placed in the ");
      Console.WriteLine("folder to be watched.  In order to terminate the program, the user must enter 'Ctrl-C' on the keyboard." + g.crlf);
      Console.WriteLine("Without terminating ExcelExtract by entering the 'Ctrl-C' key sequence, ExcelExtract will run until the computer is shut down." + g.crlf);

      Console.Write("When runing in 'Watch Folder Mode' ExcelExtract will write processing results out to the console window as each file is processed. ");
      Console.WriteLine("If this behavior is not desired the use the '-silent' switch after the '-i' and '-o' parameters and their corresponding values. ");
      Console.WriteLine("Example:" + g.crlf);
      Console.WriteLine("  ExcelExtract -w -i \"C:\\InputFolder\" -o \"C:\\OutputFolder\" -silent" + g.crlf);

      Console.WriteLine("The '-silent' and '-wait' parameters are mutually exclusive. If both are found on the command line, the -silent parameter will be discarded." + g.crlf2);


      Console.Write("NOTE: When runing in any mode of operation that results in writing output files, if the output directory does not exist, ExcelExtract will attempt to ");
      Console.WriteLine("create the directory and will then write the output files to it. ");

      _inWaitMode = true;
      return 0;
    }

  }
}

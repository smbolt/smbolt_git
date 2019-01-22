using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Pipes
{
  public class NamedPipeUtility : IDisposable
  {
    private string _pipeList64Path;

    public NamedPipeUtility(string pipeList64Path)
    {
      _pipeList64Path = pipeList64Path;
    }

    public NamedPipeSet GetNamedPipeSet(string filter)
    {
      try
      {
        if (_pipeList64Path.IsBlank())
          throw new Exception("The path specified for the PipeList64 program is blank or null.");

        if (!File.Exists(_pipeList64Path))
          throw new Exception("The PipeList64.exe program does not exist at the path '" + _pipeList64Path + "'.");

        var namedPipeSet = new NamedPipeSet();


        var processParms = new ProcessParms();
        processParms.ExecutablePath = _pipeList64Path;
        var processHelper = new ProcessHelper();
        var taskResult = processHelper.RunExternalProcess(processParms);

        if (taskResult.TaskResultStatus == TaskResultStatus.Failed)
          throw new Exception("An exception occurred in the call to PipeList64 to get the list of existing named pipes." + g.crlf + taskResult.Message);

        string pipesList = taskResult.Message;
        var lines = pipesList.ToLines();

        bool pastHeader = false;
        foreach (var line in lines)
        {
          if (line.CountOfChar('-') > 25)
          {
            pastHeader = true;
            continue;
          }

          if (!pastHeader)
            continue;

          string[] tokens = new string[3];
          tokens[0] = String.Empty;
          tokens[1] = String.Empty;
          tokens[2] = String.Empty;

          string s = line.Trim();
          int p = s.LastIndexOf(' ');
          if (p == -1)
            continue;

          tokens[2] = s.Substring(p + 1).Trim();
          s = s.Substring(0, p).Trim();
          p = s.LastIndexOf(' ');
          if (p == -1)
            continue; 

          tokens[1] = s.Substring(p + 1).Trim();
          tokens[0] = s.Substring(0, p).Trim();

          //string[] tokens = line.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

          if (tokens[1].IsNotNumeric())
            throw new Exception("The second token in the string '" + line + "' is expected to be numeric (number of instances of the named pipe) " +
                                "but was not.");

          int token2Negator = 1;
          if (tokens[2].StartsWith("-"))
          {
            token2Negator = -1;
            tokens[2] = tokens[2].Substring(1);
          }

          if (tokens[2].IsNotNumeric())
            throw new Exception("The third token in the string '" + line + "' is expected to be numeric (max number of instances of the named pipe) " +
                                "but was not.");

          var namedPipe = new NamedPipe(tokens[0], tokens[1].ToInt32(), tokens[2].ToInt32() * token2Negator);

          if (namedPipeSet.ContainsKey(namedPipe.PipeName))
            throw new Exception("A duplicate named pipe name has been encountered '" + namedPipe.PipeName + "'.");

          if (filter.IsNotBlank() && !namedPipe.PipeName.Contains(filter))
            continue;

          namedPipeSet.Add(namedPipe.PipeName, namedPipe);
        }

        return namedPipeSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a list of named pipes in existence.", ex);
      }
    }

    public void Dispose()
    {
    }
  }
}

using Org.GS;
using Org.GS.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Org.PS
{
  public class PsEngine : IDisposable
  {
    public event Action<string> PsOutputGenerated;
    public event Action<string> PsErrorOutputGenerated;
    public event Action<string> PsDebugOutputGenerated;
    public event Action<string> PsWarningOutputGenerated;
    public event Action<string> PsVerboseOutputGenerated;
    public event Action<string> PsInvocationStateChanged;

    private PSInvocationState _psInvocationState;
    private string _jobName;
    private TaskResult _taskResult;
    private int _returnCode;
    private Logger _logger;

    public PsEngine()
    {
      _logger = new Logger();
    }
    
    public async Task<TaskResult> RunScriptAsync(string scriptPath)
    {
      try
      {
        _psInvocationState = PSInvocationState.NotStarted;
        _jobName = Path.GetFileNameWithoutExtension(scriptPath);
        _taskResult = new TaskResult(_jobName);
        _returnCode = -1;

        using (var ps = PowerShell.Create())
        {
          string script = File.ReadAllText(scriptPath);
          ps.AddScript(script);

          PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();
          outputCollection.DataAdded += OutputCollection_DataAdded;
          ps.Streams.Verbose.DataAdded += Verbose_DataAdded;
          ps.Streams.Warning.DataAdded += Warning_DataAdded;
          ps.Streams.Debug.DataAdded += Debug_DataAdded;
          ps.Streams.Error.DataAdded += Error_DataAdded;
          ps.InvocationStateChanged += Ps_InvocationStateChanged;

          await System.Threading.Tasks.Task.Run(() =>
          {
            try
            {
              var result = ps.BeginInvoke<PSObject, PSObject>(null, outputCollection);
              result.AsyncWaitHandle.WaitOne();
              ps.EndInvoke(result);
            }
            catch (Exception ex)
            {
              throw new Exception("An exception occurred while executing the PowerShell script.", ex);
            }
          });

          _taskResult.TaskResultStatus = _taskResult.TaskResultSet.GetWorstResult();
          
          return _taskResult;          
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to execute the PowerShell script '" + scriptPath + "'.", ex);
      }
    }

    private void OutputCollection_DataAdded(object sender, DataAddedEventArgs e)
    {
      try
      {
        if (this.PsOutputGenerated == null)
          return;

        var psObject = ((PSDataCollection<PSObject>)sender)[e.Index];
        string message = String.Empty;
        string type = psObject.BaseObject.GetType().Name;

        switch (type)
        {
          case "ServiceController":
            message = ((ServiceController)psObject.BaseObject).Report();
            break;

          default:
            message = psObject.BaseObject.ToString();
            break;
        }

        if (message.StartsWith("### STEP"))
          UpdateTaskResult(message);

        this.PsOutputGenerated(message);
      }
      catch (Exception ex)
      {

      }
    }

    private void UpdateTaskResult(string message)
    {
      if (message.ToUpper().Contains("STARTING"))
      {
        var taskResult = new TaskResult(message.ToUpper().Replace("###", String.Empty).Replace("STARTING", String.Empty)
                                               .CondenseExtraSpaces().Replace(" ", "-"));

        taskResult.TaskResultStatus = TaskResultStatus.InProgress;
        _taskResult.TaskResultSet.Add(_taskResult.TaskResultSet.Count, taskResult);
      }

      if (message.ToUpper().Contains("COMPLETE"))
      {
        int pos = message.ToUpper().IndexOf("RC=");
        if (pos == -1)
          throw new Exception("Cannot find return code (looking for'RC=') in script completion notification - value found is '" + message + "'.");

        string returnCodeText = message.ToUpper().Substring(pos).Replace("RC=", String.Empty).Trim();
        if (returnCodeText.IsNotInteger())
          throw new Exception("A non-integer value was found following 'RC=' in the script completion notification message '" + message + "'.");

        _returnCode = returnCodeText.ToInt32();

        string taskName = message.ToUpper().Substring(0, pos).Trim().Replace("###", String.Empty).Replace("COMPLETE", String.Empty)
                                           .CondenseExtraSpaces().Replace(" ", "-");

        var taskResult = _taskResult.TaskResultSet.Values.Where(t => t.TaskName == taskName).FirstOrDefault();

        if (taskResult == null)
          throw new Exception("Cannot locate TaskResult named '" + taskName + "' in the TaskResult set for top level task '" + _taskResult.TaskName + "'.");
        
        if (_returnCode == 0)
        {
          taskResult.TaskResultStatus = TaskResultStatus.Success;
          taskResult.Code = _returnCode;
        }
        else
        {
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Code = _returnCode;
        }
      }
    }

    private void Ps_InvocationStateChanged(object sender, PSInvocationStateChangedEventArgs e)
    {
      string message = String.Empty;

      switch (e.InvocationStateInfo.State)
      {
        case PSInvocationState.Running:
          if (_psInvocationState == PSInvocationState.NotStarted)
          {
            message = g.crlf +
                      "########################################################################################################" + g.crlf +
                      "### JOB STARTED (" + _jobName + ")" + g.crlf +
                      "########################################################################################################" + g.crlf;
          }
          else
          {
            message = g.crlf + "### JOB (" + _jobName + ") is Running" + g.crlf;
          }
          _psInvocationState = e.InvocationStateInfo.State;
          break;

        case PSInvocationState.Completed:
          message = g.crlf +
                    "########################################################################################################" + g.crlf +
                    "### JOB COMPLETED (" + _jobName + ")" + g.crlf +
                    "### RETURN CODE: " + _returnCode.ToString() + g.crlf +  
                    "########################################################################################################" + g.crlf;
          _psInvocationState = e.InvocationStateInfo.State;
          break;

        default:
          message = g.crlf + "### JOB (" + _jobName + ") invocation state is '" + e.InvocationStateInfo.State.ToString() + "'." + g.crlf;
          break;
      }

      this.PsInvocationStateChanged?.Invoke(message);

      System.Threading.Thread.Sleep(500);
    }

    private void Debug_DataAdded(object sender, DataAddedEventArgs e)
    {
      if (this.PsDebugOutputGenerated == null)
        return;

      var debugRecord = ((PSDataCollection<DebugRecord>)sender)[e.Index];
      this.PsErrorOutputGenerated(debugRecord.ToString());
    }

    private void Warning_DataAdded(object sender, DataAddedEventArgs e)
    {
      if (this.PsWarningOutputGenerated == null)
        return;

      var warningRecord = ((PSDataCollection<WarningRecord>)sender)[e.Index];
      this.PsErrorOutputGenerated(warningRecord.ToString());
    }

    private void Verbose_DataAdded(object sender, DataAddedEventArgs e)
    {
      if (this.PsVerboseOutputGenerated == null)
        return;

      var verboseRecord = ((PSDataCollection<VerboseRecord>)sender)[e.Index];
      this.PsErrorOutputGenerated(verboseRecord.ToString());
    }

    private void Error_DataAdded(object sender, DataAddedEventArgs e)
    {
      if (this.PsErrorOutputGenerated == null)
        return;

      var errorRecord = ((PSDataCollection<ErrorRecord>)sender)[e.Index];
      this.PsErrorOutputGenerated(errorRecord.ToString());
    }

    public void Dispose()
    {
    }
  }
}

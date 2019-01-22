using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Org.DynamoDbService
{
  public class DynamoDbManager : IDisposable
  {
    public event Action<MonitorEventArgs> MonitorEvent;

    public DynamoDbServiceState DynamoDbServiceState {
      get;
      private set;
    }

    private string _installDirectory;

    private Task _dynamoDbThread;
    private CancellationTokenSource _dynamoDbThread_CancellationTokenSource;
    private ManualResetEvent _dynamoDbThread_ResetEvent;
    private bool _dynamoDbThreadHasBeenStopped;
    private bool _continueDynamoDbThread;

    private Task _monitorThread;
    private CancellationTokenSource _monitorThread_CancellationTokenSource;
    public ManualResetEvent _monitorMainLoop_ResetEvent;
    private bool _continueMonitorDbThread;

    private int _monitorIntervalSeconds = 5;
    private bool _monitorAttemptRestart = true;

    public DynamoDbManager(string installDirectory)
    {
      _installDirectory = installDirectory;
      this.DynamoDbServiceState = DynamoDbServiceState.Stopped;
    }

    private void RunDynamoDbThread()
    {
      try
      {
        _continueDynamoDbThread = true;

        Process dynamoDbProcess = null;

        while (true)
        {
          if (dynamoDbProcess == null)
          {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "java";
            startInfo.Arguments = "-DJava.library.path=" + _installDirectory + @"\DynamoDbLocal_lib " +
                                  "-jar " + _installDirectory + @"\DynamoDbLocal.jar -sharedDb";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;
            dynamoDbProcess = Process.Start(startInfo);
            System.Threading.Thread.Sleep(3000);

            string processOutput = ReadStream(dynamoDbProcess.StandardOutput, Environment.NewLine);
            string errorOutput = ReadStream(dynamoDbProcess.StandardError, Environment.NewLine);

            if (processOutput.Length > 0)
              this.MonitorEvent?.Invoke(new MonitorEventArgs(this.DynamoDbServiceState, "Standard output from DynamoDb process start:" + g.crlf + processOutput));

            if (errorOutput.Length > 0)
              this.MonitorEvent?.Invoke(new MonitorEventArgs(this.DynamoDbServiceState, "Error output from DynamoDb process start:" + g.crlf + errorOutput));
          }


          if (!_continueDynamoDbThread)
          {
            dynamoDbProcess.Kill();

            int waitSeconds = 10;
            while (waitSeconds > 0)
            {
              if (!dynamoDbProcess.HasExited)
              {
                System.Threading.Thread.Sleep(1000);

              }
              else
              {
                // log the fact that the process has exited...
                break;
              }

              waitSeconds--;
            }

            _dynamoDbThreadHasBeenStopped = true;

            return;
          }

          System.Threading.Thread.Sleep(3000);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception has occurred in the thread running DynamoDb.", ex);
      }
    }

    private string ReadStream(StreamReader sr, string eol)
    {
      var sb = new StringBuilder();
      char[] buffer = new char[4096];
      Task<int> readTask = null;

      while (true)
      {
        readTask = sr.ReadAsync(buffer, 0, buffer.Length);

        readTask.Wait(200);

        if (readTask.IsCompleted)
        {
          if (readTask.Result > 0)
          {
            string bufferString = new String(buffer, 0, readTask.Result);
            sb.Append(bufferString);
          }
          else
          {
            break;
          }

          readTask = null;
        }
        else
        {
          break;
        }
      }

      return sb.ToString().Trim();
    }

    private void MonitorMainLoop()
    {
      try
      {
        while (true)
        {
          if (!_continueMonitorDbThread)
            return;

          int remainingSeconds = _monitorIntervalSeconds;

          while (remainingSeconds > 0)
          {
            _monitorMainLoop_ResetEvent.WaitOne(TimeSpan.FromSeconds(1));

            if (!_continueMonitorDbThread)
              return;

            remainingSeconds--;
          }

          if (_dynamoDbThreadHasBeenStopped)
          {
            this.MonitorEvent?.Invoke(new MonitorEventArgs(this.DynamoDbServiceState, "DynamoDbService state is Stopped."));
            continue;
          }

          if (_dynamoDbThread == null)
          {
            this.MonitorEvent?.Invoke(new MonitorEventArgs(DynamoDbServiceState.Faulted, "DynamoDbService main thread null."));
            throw new Exception("The _dynamoDbThread (TPL object) is null. The MonitorMainLoop is throwing this exception.");
          }

          // all statuses except Faulted are acceptable to continue monitoring
          if (_dynamoDbThread.Status != TaskStatus.Faulted)
          {
            this.MonitorEvent?.Invoke(new MonitorEventArgs(this.DynamoDbServiceState, "DynamoDbService state is " + this.DynamoDbServiceState.ToString()));
            continue;
          }

          // if not configured to attempt restart
          if (!_monitorAttemptRestart)
          {
            this.MonitorEvent?.Invoke(new MonitorEventArgs(this.DynamoDbServiceState, "DynamoDbService state is " +
                                      this.DynamoDbServiceState.ToString() + " - restart will not be attempted."));
            return;
          }

          // attempt restart

          this.MonitorEvent?.Invoke(new MonitorEventArgs(this.DynamoDbServiceState, "DynamoDbService state is " +
                                    this.DynamoDbServiceState.ToString() + " - restart will be attempted."));

          StartDynamoDbThread();

          System.Threading.Thread.Sleep(3000);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception has occurred in the monitoring thread.", ex);
      }
    }


    public void Start()
    {
      try
      {
        ValidateInstallDirectory();
        StartDynamoDbThread();

        if (_monitorThread != null)
        {
          if (_monitorThread.Status != TaskStatus.Running)
          {
            if (!_monitorThread_CancellationTokenSource.IsCancellationRequested)
            {
              _monitorThread_CancellationTokenSource.Cancel();
              int waitSeconds = 10;
              while (_monitorThread.Status != TaskStatus.Canceled && _monitorThread.Status != TaskStatus.RanToCompletion)
              {
                System.Threading.Thread.Sleep(1000);
                waitSeconds--;
                if (waitSeconds < 0)
                  break;
              }

              _monitorThread = null;
            }
          }
        }

        if (_monitorThread == null || _monitorThread.Status != TaskStatus.Running)
          StartMonitoringThread();

        this.DynamoDbServiceState = DynamoDbServiceState.Running;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to start the DynamoDb database engine.", ex);
      }
    }

    private void StartDynamoDbThread()
    {
      _dynamoDbThreadHasBeenStopped = false;
      _dynamoDbThread_CancellationTokenSource = new CancellationTokenSource();
      _dynamoDbThread_ResetEvent = new ManualResetEvent(false);
      _dynamoDbThread = new Task(() => this.RunDynamoDbThread(), _dynamoDbThread_CancellationTokenSource.Token, TaskCreationOptions.LongRunning);
      _dynamoDbThread.Start();
    }

    private void StartMonitoringThread()
    {
      _monitorThread_CancellationTokenSource = new CancellationTokenSource();
      _monitorMainLoop_ResetEvent = new ManualResetEvent(false);
      _continueMonitorDbThread = true;
      _monitorThread = new Task(() => this.MonitorMainLoop(), _monitorThread_CancellationTokenSource.Token, TaskCreationOptions.LongRunning);
      _monitorThread.Start();
    }

    public void Stop()
    {
      try
      {
        _continueDynamoDbThread = false;

        this.DynamoDbServiceState = DynamoDbServiceState.Stopped;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to stop the DynamoDb database engine.", ex);
      }
    }

    public void Pause()
    {
      try
      {


        this.DynamoDbServiceState = DynamoDbServiceState.Paused;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to pause the DynamoDb database engine.", ex);
      }
    }

    public void Resume()
    {
      try
      {


        this.DynamoDbServiceState = DynamoDbServiceState.Running;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to resume the DynamoDb database engine.", ex);
      }
    }

    private void ValidateInstallDirectory()
    {
      if (String.IsNullOrEmpty(_installDirectory))
        throw new Exception("The DynamoDb installation directory is blank or null.");

      if (!Directory.Exists(_installDirectory))
        throw new Exception("The DynamoDb installation directory '" + _installDirectory + "' does not exist.");

      string dynamoDbLocalLibDirectory = _installDirectory + @"\DynamoDbLocal_lib";
      if (!Directory.Exists(dynamoDbLocalLibDirectory))
        throw new Exception("The DynamoDb installation is invalid - directory '" + dynamoDbLocalLibDirectory + "' doesn't exist.");

      string dynamoDbLocalJarFile = _installDirectory + @"\DynamoDbLocal.jar";
      if (!File.Exists(dynamoDbLocalJarFile))
        throw new Exception("The DynamoDb installation is inavlid - file '" + dynamoDbLocalJarFile + "' is not found.");
    }

    public void Dispose()
    {
      if (_monitorThread != null)
      {
        _monitorThread_CancellationTokenSource.Cancel();


      }

      if (_dynamoDbThread != null)
      {
        _dynamoDbThread_CancellationTokenSource.Cancel();


      }
    }
  }
}

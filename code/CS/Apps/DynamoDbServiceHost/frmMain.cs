using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Org.DynamoDbService;

namespace DynamoDbServiceHost
{
  public partial class frmMain : Form
  {
    private string _installDirectory;
    private Dictionary<string, string> _appConfig;
    private DynamoDbManager _dynamoDbManager;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object c, EventArgs e)
    {
      switch (c.ActionTag())
      {
        case "Start":
          Start();
          break;

        case "Stop":
          Stop();
          break;

        case "Pause":
          Pause();
          break;

        case "Resume":
          Resume();
          break;

        case "ClearDisplay":
          txtOut.Text = String.Empty;
          break;

        case "Exit":
          TerminateService();
          break;
      }
    }

    private void Start()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        if (_dynamoDbManager == null)
        {
          _dynamoDbManager = new DynamoDbManager(_installDirectory);
          _dynamoDbManager.MonitorEvent += DynamoDbServiceMonitorEventHandler;
        }

        _dynamoDbManager.Start();

        WriteToDisplay("DynamoDbManager service started.", true);

        SetUIForStatus();
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        string errorMessage = "An exception occurred while attemting to start the DynamoDbService." + g.crlf2 + ex.ToReport();
        WriteToDisplay(errorMessage, true);
        MessageBox.Show(errorMessage, "DynamoDbService Host - Error Starting DynamoDbService",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void DynamoDbServiceMonitorEventHandler(MonitorEventArgs args)
    {
      switch (args.DynamoDbServiceState)
      {
        case DynamoDbServiceState.Running:
          WriteToDisplay(args.Message, true);

          break;


        case DynamoDbServiceState.Paused:
          WriteToDisplay(args.Message, true);

          break;


        case DynamoDbServiceState.Faulted:
          WriteToDisplay(args.Message, true);

          break;

        case DynamoDbServiceState.Stopped:
          WriteToDisplay(args.Message, true);
          break;
      }
    }

    private void Stop()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        _dynamoDbManager.Stop();

        WriteToDisplay("DynamoDbManager service stopped.", true);

        SetUIForStatus();
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        string errorMessage = "An exception occurred while attemting to stop the DynamoDbService." + g.crlf2 + ex.ToReport();
        WriteToDisplay(errorMessage, true);
        MessageBox.Show(errorMessage, "DynamoDbService Host - Error Stopping DynamoDbService",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void Pause()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        txtOut.Text = "Pause";

        SetUIForStatus();
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        string errorMessage = "An exception occurred while attemting to pause the DynamoDbService." + g.crlf2 + ex.ToReport();
        WriteToDisplay(errorMessage, true);
        MessageBox.Show(errorMessage, "DynamoDbService Host - Error Pausing DynamoDbService",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void Resume()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;


        txtOut.Text = "Resume";

        SetUIForStatus();
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        string errorMessage = "An exception occurred while attemting to resume the DynamoDbService." + g.crlf2 + ex.ToReport();
        WriteToDisplay(errorMessage, true);
        MessageBox.Show(errorMessage, "DynamoDbService Host - Error Resuming DynamoDbService",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void SetUIForStatus()
    {
      if (_dynamoDbManager == null)
      {
        btnStart.Enabled = true;
        btnPause.Enabled = false;
        btnResume.Enabled = false;
        btnStop.Enabled = false;
      }
      else
      {
        switch (_dynamoDbManager.DynamoDbServiceState)
        {
          case DynamoDbServiceState.Running:
            btnStart.Enabled = false;
            btnPause.Enabled = true;
            btnResume.Enabled = false;
            btnStop.Enabled = true;
            break;

          case DynamoDbServiceState.Paused:
            btnStart.Enabled = false;
            btnPause.Enabled = false;
            btnResume.Enabled = true;
            btnStop.Enabled = true;
            break;

          case DynamoDbServiceState.Stopped:
          case DynamoDbServiceState.Faulted:
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnResume.Enabled = false;
            btnStop.Enabled = false;
            break;
        }
      }
    }

    private void TerminateService()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        if (_dynamoDbManager != null)
        {
          _dynamoDbManager.Stop();
          _dynamoDbManager.Dispose();

        }



        this.Cursor = Cursors.Default;
        this.Close();
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attemting to terminate the program." + g.crlf2 + ex.ToReport(),
                        "DynamoDbService Host - Error Terminating Program", MessageBoxButtons.OK, MessageBoxIcon.Error);

        MessageBox.Show("This program will close.",
                        "DynamoDbService Host - Program Closing", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }


    private void InitializeForm()
    {
      try
      {
        string executablePath = AppDomain.CurrentDomain.BaseDirectory;
        if (executablePath.ToLower().Contains(@"\bin\"))
          executablePath = Directory.GetParent(executablePath).Parent.Parent.FullName;

        string appConfigPath = executablePath + @"\AppConfig.xml";

        if (!File.Exists(appConfigPath))
        {
          MessageBox.Show("The application configuration file is missing at '" + appConfigPath + "'.",
                          "DynamoDbService Host - Missing Configuration File", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        _appConfig = new Dictionary<string, string>();
        var appConfig = XElement.Parse(File.ReadAllText(appConfigPath));
        var ciElements = appConfig.Elements("CI");
        foreach (var ci in ciElements)
        {
          if (ci.Attribute("K") != null && ci.Attribute("V") != null)
          {
            string key = ci.Attribute("K").Value;
            string value = ci.Attribute("V").Value;
            if (!_appConfig.ContainsKey(key))
              _appConfig.Add(key, value);
          }
        }

        _installDirectory = _appConfig.ContainsKey("DynamoDbInstallationPath") ? _appConfig["DynamoDbInstallationPath"] : String.Empty;


        SetUIForStatus();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during application startup." + g.crlf2 + ex.ToReport(),
                        "DynamoDbService Host - Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    private void WriteToDisplay(string message, bool prependTimeStamp = false)
    {
      string timestamp = String.Empty;
      if (prependTimeStamp)
        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ";

      if (txtOut.InvokeRequired)
      {
        txtOut.Invoke((Action)((() => {
          txtOut.Text += timestamp + message + g.crlf;
          txtOut.SelectionStart = txtOut.Text.Length;
          txtOut.SelectionLength = 0;
          txtOut.ScrollToCaret();
        })));
      }
      else
      {
        txtOut.Text += timestamp + message + g.crlf;
        txtOut.SelectionStart = txtOut.Text.Length;
        txtOut.SelectionLength = 0;
        txtOut.ScrollToCaret();
      }

      Application.DoEvents();
    }
  }
}

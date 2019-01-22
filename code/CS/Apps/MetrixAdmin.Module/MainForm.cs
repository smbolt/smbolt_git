using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using Adsdi.FTP;
using Adsdi.WSO;
using Adsdi.GS;
using Adsdi.GS.Dynamic;
using Adsdi.GS.Database;
using Adsdi.GS.Configuration;
using Adsdi.Controls;
using Adsdi.ConfigWizard;
using MetrixDb = Adsdi.GS.Database.Metrix;

namespace Adsdi.MetrixAdmin.Module
{
  public partial class MainForm : DynamicFormBase
  {
    private string _appPath;
    private string _configPath;

    public event Action<DXO> HostCommand;
    public ModuleParms ModuleParms {
      get;
      set;
    }

    private Assembly _assembly;
    private App _app;
    private ResourceManager _resourceManager;
    private ImageList _imgList16;

    private DbIntKeyedSet<MetrixDb.Applications> applicationSet;
    private DbIntKeyedSet<MetrixDb.Categories> categorySet;
    private DbIntKeyedSet<MetrixDb.Intervals> intervalSet;
    private DbIntKeyedSet<MetrixDb.Metrics> metricSet;
    private DbIntKeyedSet<MetrixDb.MetricTypes> metricTypeSet;
    private DbIntKeyedSet<MetrixDb.Servers> serverSet;
    private DbIntKeyedSet<MetrixDb.Systems> systemSet;

    private SqlConnection _connection;
    private string _dbServer;
    private string _databaseName;
    private string _dbUserId;
    private string _dbPassword;
    private bool _isWindowsAuthentication;

    private frmConfigWizard fConfigWizard;

    public MainForm()
      : base(Assembly.GetExecutingAssembly())
    {
      InitializeComponent();
      InitializeForm();
    }

    private void InitializeForm()
    {
      if (!g.AppConfig.IsLoaded)
      {
        RunConfigWizard();
        _appPath = GetAppPath();
        _configPath = _appPath + @"\AppConfig";
        g.AppInfo.ConfigName = this.ModuleName;
        g.AppConfig = new AppConfig(g.AppInfo.ConfigName);
        if (File.Exists(_configPath + @"\AppConfig.xmlx"))
          g.AppConfig.LoadFromFile(_configPath + @"\AppConfig.xmlx");
      }


      _connection = null;
      _dbServer = String.Empty;
      _databaseName = String.Empty;
      _dbUserId = String.Empty;
      _dbPassword = String.Empty;
      _isWindowsAuthentication = true;
      _assembly = Assembly.GetExecutingAssembly();
      string moduleName = _assembly.ManifestModule.Name.Replace(".dll", String.Empty);
      g.AppConfig.ModuleName = moduleName.Replace("Adsdi.", String.Empty).Replace(".Module", String.Empty);
      if (!moduleName.StartsWith("Adsdi."))
        moduleName = "Adsdi." + moduleName;

      _resourceManager = new ResourceManager(moduleName + ".Resource1", _assembly);
      this.ModuleParms = new ModuleParms(_resourceManager);
      _app = new App(Assembly.GetExecutingAssembly());

      InitializeConfiguration();

      //base.Map = "set_map";

      g.MetrixConfigDbSpec = g.AppConfig.GetDbSpec("Metrix$HOST");

      LoadDimensionTables();
    }

    private void LoadDimensionTables()
    {
      using (DbContext db = new DbContext(g.MetrixConfigDbSpec.ConnectionString))
      {
        applicationSet = db.DbSet<MetrixDb.Applications>().Select().ToIntKeyedSet();
        categorySet = db.DbSet<MetrixDb.Categories>().Select().ToIntKeyedSet();
        intervalSet = db.DbSet<MetrixDb.Intervals>().Select().ToIntKeyedSet();
        metricSet = db.DbSet<MetrixDb.Metrics>().Select().ToIntKeyedSet();
        metricTypeSet = db.DbSet<MetrixDb.MetricTypes>().Select().ToIntKeyedSet();
        serverSet = db.DbSet<MetrixDb.Servers>().Select().ToIntKeyedSet();
        systemSet = db.DbSet<MetrixDb.Systems>().Select().ToIntKeyedSet();
      }
    }




    private void InitializeConfiguration()
    {


    }

    private void buttonBar_ControlEvent(Controls.ControlEventArgs e)
    {
      base.DoAction(e.ControlEventText);

      switch (e.ControlEventText)
      {
        case "Start":
          RunConfigWizard();
          break;

        case "Pause":
          RunSpinner();
          break;

        case "Stop":
          StopSpinner();
          break;
      }
    }

    private void Action_Click(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      base.DoAction(action);

      switch (action)
      {
        case "DOWNLOAD_FILE":
          DownloadFile();
          break;

        case "DOWNLOAD_FTP":
          DownloadFTP();
          break;

        case "FTP_EXPLORER":
          FtpExplorer();
          break;

        case "PING_MAINSVC":
          PingMainSvc();
          break;

        case "TEST1":
          MessageBox.Show("TEST1 clicked.");
          break;

        case "EXIT":
          Exit();
          break;
      }
    }

    public override void DoAction(string action)
    {
      if (action == null)
        return;

      switch (action)
      {
        case "PING_CENTRAL_UPDATE_CONTROL":
          HostCommand(new DXO("PingCentralUpdateControl"));
          break;

        case "CHECK_CENTRAL_UPDATE_CONTROL":
          HostCommand(new DXO("CheckCentralUpdateControl"));
          break;

      }
    }


    private void DownloadFile()
    {
      //int segmentLength = 100000;
      //if (txtSegmentLength.Text.Trim().IsNumeric())
      //    segmentLength = Int32.Parse(txtSegmentLength.Text.Trim());

      //string timeTaken = FileSystemUtility.DownloadFile(@"C:\_work\PatientLinkSetup.exe", segmentLength);
      //MessageBox.Show("Duration = " + timeTaken);
    }

    private void DownloadFTP()
    {
      this.Cursor = Cursors.WaitCursor;

      FtpParms ftpParms = new FtpParms();
      ftpParms.FtpServer = @"";
      ftpParms.RemoteDirectory = @"_GeneralTransfer";
      ftpParms.FileName = @"PatientLinkSetup.exe";
      ftpParms.UserId = "Administrator";
      ftpParms.Password = "gen126PLAdmin";
      ftpParms.Method = WebRequestMethods.Ftp.DownloadFile;
      ftpParms.LocalDirectory = @"C:\_work";

      frmFtp fFtp = new frmFtp(ftpParms);
      fFtp.ShowDialog();

      this.Cursor = Cursors.Default;
    }

    public void FtpUtility_FtpNotification(FtpProgress ftpStatus)
    {
      long bytesTransferred = ftpStatus.BytesTransferred;
      int seconds = (int) ftpStatus.Duration.TotalSeconds;

      int bytesPerSecond = (int)((double)bytesTransferred / seconds);


      //lblStatus.Text = "Bytes transferred: " + ftpStatus.BytesTransferred.ToString("###,###,###,##0") +
      //    "  Duration: " + seconds.ToString() +
      //    "  Download rate: " + bytesPerSecond.ToString("###,###,##0");
      Application.DoEvents();
    }

    private void FtpExplorer()
    {
      frmFtpExplorer fFtpExplorer = new frmFtpExplorer();
      fFtpExplorer.ShowDialog();
    }

    private void PingMainSvc()
    {
      //string returnCode = WscHelper.PingMainSvc();
      MessageBox.Show("update this code");
    }

    private void Exit()
    {
      HostCommand(new DXO("CloseForm"));
    }

    private void RunConfigWizard()
    {
      fConfigWizard = new frmConfigWizard("MetrixAdmin.Module");

      fConfigWizard.ShowDialog();
    }

    private void RunSpinner()
    {
      this.waitSpinner1.StartSpinning();
    }

    private void StopSpinner()
    {
      this.waitSpinner1.StopSpinning();
    }

  }
}

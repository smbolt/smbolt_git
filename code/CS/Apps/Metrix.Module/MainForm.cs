using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using Adsdi.FTP;
using Adsdi.WSO;
using Adsdi.GS;
using Adsdi.GS.Dynamic;
using Adsdi.GS.Database;
using MetrixDb = Adsdi.GS.Database.Metrix;

namespace Adsdi.Metrix.Module
{
  public partial class MainForm : DynamicFormBase
  {
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


    public MainForm()
      : base(Assembly.GetExecutingAssembly())
    {
      InitializeComponent();
      InitializeForm();
    }

    private void InitializeForm()
    {
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
      Load_ImageList();

      InitializeConfiguration();

      //base.Map = "set_map";

      g.MetrixConfigDbSpec = g.AppConfig.GetDbSpec("Metrix");

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



    private void Load_ImageList()
    {
      //_imgList16 = new ImageList();
      //_imgList16.ImageSize = new Size(16, 16);

      //_imgList16.Images.Add("event", (Bitmap)_resourceManager.GetObject("_event"));
      //_imgList16.Images.Add("bus_object", (Bitmap)_resourceManager.GetObject("bus_object"));
      //_imgList16.Images.Add("bus_objects", (Bitmap)_resourceManager.GetObject("bus_objects"));
      //_imgList16.Images.Add("closed_connection", (Bitmap)_resourceManager.GetObject("closed_connection"));
      //_imgList16.Images.Add("connections", (Bitmap)_resourceManager.GetObject("connections"));
      //_imgList16.Images.Add("column", (Bitmap)_resourceManager.GetObject("column"));
      //_imgList16.Images.Add("cs_file", (Bitmap)_resourceManager.GetObject("cs_file"));
      //_imgList16.Images.Add("database", (Bitmap)_resourceManager.GetObject("database"));
      //_imgList16.Images.Add("field", (Bitmap)_resourceManager.GetObject("field"));
      //_imgList16.Images.Add("fkey", (Bitmap)_resourceManager.GetObject("fkey"));
      //_imgList16.Images.Add("folder", (Bitmap)_resourceManager.GetObject("folder"));
      //_imgList16.Images.Add("open_connection", (Bitmap)_resourceManager.GetObject("open_connection"));
      //_imgList16.Images.Add("open_folder", (Bitmap)_resourceManager.GetObject("open_folder"));
      //_imgList16.Images.Add("pkey", (Bitmap)_resourceManager.GetObject("pkey"));
      //_imgList16.Images.Add("private_method", (Bitmap)_resourceManager.GetObject("private_method"));
      //_imgList16.Images.Add("project", (Bitmap)_resourceManager.GetObject("project"));
      //_imgList16.Images.Add("property", (Bitmap)_resourceManager.GetObject("property"));
      //_imgList16.Images.Add("protected_method", (Bitmap)_resourceManager.GetObject("protected_method"));
      //_imgList16.Images.Add("public_method", (Bitmap)_resourceManager.GetObject("public_method"));
      //_imgList16.Images.Add("table", (Bitmap)_resourceManager.GetObject("table"));
      //_imgList16.Images.Add("view", (Bitmap)_resourceManager.GetObject("view"));
    }

    private void InitializeConfiguration()
    {


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


  }
}

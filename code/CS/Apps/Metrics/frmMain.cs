using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.MX;
using Org.MX.Model;
using Org.GS.Configuration;
using Org.GS;

namespace Org.Metrics
{
  public partial class frmMain : Form
  {
    private bool _initializationSuccessful = false;
    private bool _isFirstShowing = true;
    private MetricData _metricData;
    private ConfigDbSpec _metricsConfigDbSpec;
    private ConfigDbSpec _metricObservationsConfigDbSpec;
    private int _loadLimit;

    private Dictionary<int, int> _volumeFactor;
    private Dictionary<int, decimal> _metricFactor;

    public frmMain()
    {
      InitializeComponent();
      InitializeApplication();
    }

    private void Action(object sender, EventArgs e)
    {

      switch (sender.ActionTag())
      {
        case "ReloadMetricData":
          ReloadMetricData();
          break;

        case "LoadObservations":
          LoadObservations();
          break;

        case "ClearObservations":
          ClearObservations();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void LoadObservations()
    {

      try
      {
        this.Cursor = Cursors.WaitCursor;
        txtMain.Text = String.Empty; ;
        Application.DoEvents();
        var sb = new StringBuilder();

        LoadFactors();
        var ms = DateTime.Now.Millisecond;
        var r = new Random(ms);
        var crankFactor = 1.8F;

        int rowsLoaded = 0;


        int metricsLoaded = 0;

        int envCode = 1;
        int metricStateCode = 1;
        int metricTypeCode = 1;
        int metricValueTypeCode = 1;
        int measuredValueTypeCode = 1;
        int observerServerCode = 5;
        int observerSystemCode = 5;
        int observerAppCode = 5;
        int intervalCode = 5;
        var beginDateTime = new DateTime(2018, 7, 28, 0, 0, 0);
        var endDateTime = new DateTime(2018, 7, 29, 0, 0, 1);

        using (var conn = new SqlConnection(_metricObservationsConfigDbSpec.ConnectionString))
        {
          conn.Open();
          var cmd = new SqlCommand("", conn);

          for (int targetSystemCode = 1; targetSystemCode < 5; targetSystemCode++)
          {
            for (int targetAppCode = 1; targetAppCode < 5; targetAppCode++)
            {
              for (int targetServerCode = 1; targetServerCode < 5; targetServerCode++)
              {
                var metricDateTime = beginDateTime;
                while (metricDateTime < endDateTime)
                {
                  rowsLoaded++;
                  if (_loadLimit > -1 && rowsLoaded > _loadLimit)
                  {
                    metricDateTime = endDateTime.AddDays(1);
                    targetServerCode = 999;
                    targetAppCode = 999;
                    targetSystemCode = 999;
                    break;
                  }


                  decimal cpu = 0M;
                  decimal reqPerSec = 0M;
                  decimal excessCpu = 0M;
                  int reqQueued = 0;
                  int availMem = 0;

                  for (int metricCode = 1; metricCode < 5; metricCode++)
                  {
                    var volFactor = _volumeFactor[metricDateTime.Hour];
                    var metricFactor = _metricFactor[metricCode];
                    var randomNbr = r.Next(0, 100);
                                        
                    decimal metricValue = 0M;
                    switch (metricCode)
                    {
                      case 1:
                        metricValue = Convert.ToDecimal(volFactor * crankFactor * .88F + (float)randomNbr / 100 * (float)volFactor * 0.30F);
                        reqPerSec = metricValue;
                        break;

                      case 2:
                        randomNbr = r.Next(0, 100);
                        metricValue = Convert.ToDecimal(reqPerSec * 0.9M * (1 + (Convert.ToDecimal((float)randomNbr / 100 * 25 / 100))));
                        if (metricValue < 100M)
                        {
                          excessCpu = 0;
                        }
                        else
                        {
                          excessCpu = metricValue - 100M;
                          metricValue = 100M;
                        }
                        cpu = metricValue;
                        break;

                      case 3:
                        if (excessCpu == 0)
                          metricValue = 0M;
                        else
                          metricValue = Math.Round(reqPerSec * excessCpu / 100, 0);
                        reqQueued = Convert.ToInt32(metricValue);
                        break;

                      case 4:
                        randomNbr = r.Next(75, 100);
                        metricValue = Convert.ToInt32(5192 * (100 - ((decimal) randomNbr / 100 * cpu)) / 100);
                        availMem = (int) metricValue;
                        break;
                    }

                    string sql = "INSERT INTO dbo.MetricObservation " + g.crlf +
                                 "(" + g.crlf +
                                 "  ObserverSystemCode," + g.crlf +
                                 "  ObserverAppCode," + g.crlf +
                                 "  ObserverServerCode," + g.crlf +
                                 "  TargetSystemCode," + g.crlf +
                                 "  TargetAppCode," + g.crlf +
                                 "  TargetServerCode," + g.crlf +
                                 "  EnvironmentCode," + g.crlf +
                                 "  MeasuredValueTypeCode," + g.crlf +
                                 "  MetricCode," + g.crlf +
                                 "  MetricStateCode," + g.crlf +
                                 "  MetricTypeCode," + g.crlf +
                                 "  IntervalCode," + g.crlf +
                                 "  MetricValueTypeCode," + g.crlf +
                                 "  MetricValue," + g.crlf +
                                 "  MetricDuration," + g.crlf +
                                 "  MetricDateTime," + g.crlf +
                                 "  ReceivedFromObserverDateTime" + g.crlf +
                                 ")" + g.crlf +
                                 "VALUES " + g.crlf +
                                 "(" + g.crlf +
                                 "  " + observerSystemCode.ToString() + "," + g.crlf +
                                 "  " + observerAppCode.ToString() + "," + g.crlf +
                                 "  " + observerServerCode.ToString() + "," + g.crlf +
                                 "  " + targetSystemCode.ToString() + "," + g.crlf +
                                 "  " + targetAppCode.ToString() + "," + g.crlf +
                                 "  " + targetServerCode.ToString() + "," + g.crlf +
                                 "  " + envCode.ToString() + "," + g.crlf +
                                 "  " + measuredValueTypeCode.ToString() + "," + g.crlf +
                                 "  " + metricCode.ToString() + "," + g.crlf +
                                 "  " + metricStateCode.ToString() + "," + g.crlf +
                                 "  " + metricTypeCode.ToString() + "," + g.crlf +
                                 "  " + intervalCode.ToString() + "," + g.crlf +
                                 "  " + metricValueTypeCode.ToString() + "," + g.crlf +
                                 "  " + metricValue.ToString() + "," + g.crlf +
                                 "  " + "NULL," + g.crlf +
                                 "  '" + metricDateTime.ToString("MM/dd/yyyy HH:mm:ss") + "'," + g.crlf +
                                 "  '" + metricDateTime.ToString("MM/dd/yyyy HH:mm:ss") + "'" + g.crlf +
                                 ")";

                    cmd.CommandText = sql;
                    int nbr = cmd.ExecuteNonQuery();

                    metricsLoaded++;

                    if (metricsLoaded % 1000 == 0)
                    {
                      lblStatus.Text = metricDateTime.ToString("yyyyMMdd HH:mm:ss") + "  " +
                                reqPerSec.ToString("###,###,##0.00").PadToJustifyRight(20) + "  " +
                                cpu.ToString("###,###,##0.00").PadToJustifyRight(20) + "  " +
                                excessCpu.ToString("###,###,##0.000").PadToJustifyRight(20) + "  " +
                                reqQueued.ToString("#,##0").PadToJustifyRight(10) + "  " +
                                availMem.ToString("#,##0").PadToJustifyRight(10) + "  " +
                                metricsLoaded.ToString() + "," +
                                targetSystemCode.ToString() + "," +
                                targetAppCode.ToString() + "," +
                                targetServerCode.ToString() + "," +
                                metricCode.ToString();
                      Application.DoEvents();                      
                    }
                  }

                  metricDateTime = metricDateTime.AddSeconds(15);
                }
              }
            }
          }

          conn.Close();
        }
        

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to load metric observations" + g.crlf2 + ex.ToReport(),
                        "Metrics - Error Loading Metric Observations", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadFactors()
    {
      _volumeFactor = new Dictionary<int, int>();
      _volumeFactor.Add(0, 9);
      _volumeFactor.Add(1, 7);
      _volumeFactor.Add(2, 5);
      _volumeFactor.Add(3, 3);
      _volumeFactor.Add(4, 3);
      _volumeFactor.Add(5, 5);
      _volumeFactor.Add(6, 7);
      _volumeFactor.Add(7, 11);
      _volumeFactor.Add(8, 15);
      _volumeFactor.Add(9, 22);
      _volumeFactor.Add(10, 30);
      _volumeFactor.Add(11, 42);
      _volumeFactor.Add(12, 55);
      _volumeFactor.Add(13, 58);
      _volumeFactor.Add(14, 62);
      _volumeFactor.Add(15, 61);
      _volumeFactor.Add(16, 59);
      _volumeFactor.Add(17, 55);
      _volumeFactor.Add(18, 43);
      _volumeFactor.Add(19, 30);
      _volumeFactor.Add(20, 17);
      _volumeFactor.Add(21, 14);
      _volumeFactor.Add(22, 11);
      _volumeFactor.Add(23, 10);

      _metricFactor = new Dictionary<int, decimal>();
      _metricFactor.Add(1, 1.6M);
      _metricFactor.Add(2, 1.0M);
      _metricFactor.Add(3, 1.0M);
      _metricFactor.Add(4, 1.0M);
    }

    private void ClearObservations()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        using (var conn = new SqlConnection(_metricObservationsConfigDbSpec.ConnectionString))
        {
          conn.Open();
          var cmd = new SqlCommand("DELETE FROM dbo.MetricObservation", conn);
          cmd.ExecuteNonQuery();
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to clear metric observations" + g.crlf2 + ex.ToReport(),
                        "Metrics - Error Clearing Metric Observations", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ReloadMetricData()
    {
      this.Cursor = Cursors.WaitCursor;

      _metricData = new MetricData(_metricsConfigDbSpec);
      _metricData.Load();

      txtMain.Text = _metricData.Report;
      txtMain.SelectionStart = 0;
      txtMain.SelectionLength = 0;

      this.Cursor = Cursors.Default;
    }

    private void InitializeApplication()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        new a();
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to initialize the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "Metrics - Application (a) Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        this.SetInitialSizeAndLocation();

        _metricsConfigDbSpec = g.GetDbSpec(g.CI("Env") + g.CI("MetricsDbSpecName"));
        _metricObservationsConfigDbSpec = g.GetDbSpec(g.CI("Env") + g.CI("MetricObservationsDbSpecName"));

        _metricData = new MetricData(_metricsConfigDbSpec);
        _metricData.Load();

        txtMain.Text = _metricData.Report;

        txtMain.SelectionStart = 0;
        txtMain.SelectionLength = 0;

        _initializationSuccessful = true;

        _loadLimit = g.CI("LoadLimit").ToInt32OrDefault(-1);

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred the initialization of the program." + g.crlf2 +
          ex.ToReport(), "Metrics - Program Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_isFirstShowing)
        return;

      if (!_initializationSuccessful)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An error occurred during program initialization." + g.crlf2 +
                        "The program will close.", "Metrics - Initialization Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

        this.Close();
      }
    }
  }
}

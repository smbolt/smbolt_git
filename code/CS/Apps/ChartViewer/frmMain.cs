using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Gulfport.Forecasting;
using Gulfport.Common;
using Gulfport.BusinessObjects;
using Gulfport.Data;
using Org.GS;

namespace Org.ChartViewer
{
  public partial class frmMain : Form
  {
    private bool _firstShowing;
    private string _saveLastSuccessfulEnvironment = String.Empty;
    private bool _ignoreEnvironmentSelectionChange = false;
    private bool _producingOnly = true;
    private PrcDataAccess _prcData;
    private string _forecastData;
    private frmDataDisplay _fDataDisplay;
    private string _afvReport;

    private a a;

    private SortedList<int, Well> _wells;
    private Dictionary<string, int> _wellIds;

    public frmMain()
    {
      InitializeComponent();
      InitializeApplication();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "RunForecast":
          this.RunForecast();
          break;

        case "ZoomIn":
          this.ZoomIn();
          break;

        case "ViewAriesForecastVariables":
          this.ViewAriesForecastVariables();
          break;

        case "HideData":
        case "ShowData":
          this.ShowData();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void RunForecast()
    {
      this.Cursor = Cursors.WaitCursor;

      chartMain.Series.Clear();
      chartMain.Titles.Clear();

      chartMain.ChartAreas[0].Position.X = 0;
      chartMain.ChartAreas[0].Position.Y = 6;
      chartMain.ChartAreas[0].Position.Height = 92;
      chartMain.ChartAreas[0].Position.Width = 100;
      chartMain.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
      chartMain.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
      chartMain.ChartAreas[0].CursorX.IsUserEnabled = true;
      chartMain.ChartAreas[0].CursorY.IsUserEnabled = true;
      chartMain.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
      chartMain.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;



      try
      {
        var forecastParms = GetForecastParmsFromUI();

        var mainTitle = new Title();
        mainTitle.Position.X = 8;
        mainTitle.Position.Y = 3.5F;
        mainTitle.Text = forecastParms.WellName;
        mainTitle.Font = new System.Drawing.Font("Calibri", 22.0F, FontStyle.Bold);
        chartMain.Titles.Add(mainTitle);


        var forecaster = new Forecaster(_prcData, forecastParms);
        var forecastResultsSet = forecaster.Run();
        _fDataDisplay.SetText(forecastResultsSet.ForecastReport, forecastResultsSet.ForecastReportCSV);

        foreach (var forecastResult in forecastResultsSet.Values)
        {
          var series = new Series(forecastResult.ForecastResultName);
          series.ChartType = SeriesChartType.Line;
          series.ChartArea = chartMain.ChartAreas[0].Name;
          series.XValueType = ChartValueType.Date;
          series.BorderWidth = forecastResult.ForecastValueType == ForecastValueType.Actuals ? 1 : 2;
          series.Color = GetLineColor(forecastResult.ForecastRequest);

          bool omitZeros = true;
          foreach (var forecastPoint in forecastResult.ForecastPointSet.Values)
          {
            if (omitZeros && forecastPoint.ForecastValue == 0)
              continue;
            if (forecastPoint.ForecastValue > 0)
              omitZeros = false;
            series.Points.AddXY(forecastPoint.ForecastDate, forecastPoint.ForecastValue);
          }
          chartMain.Series.Add(series);
        }

        chartMain.Invalidate();

        lblStatus.Text = "Chart created";
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred during forecasting for well '" + cboWell.Text + "'." + g.crlf2 + ex.ToReport(), "ChartViewer - Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

        this.Cursor = Cursors.Default;
        return;
      }

      this.Cursor = Cursors.Default;
    }

    private void ViewAriesForecastVariables()
    {
      using (var fAriesForecastVariables = new frmAriesForecastVariables(_afvReport))
      {
        fAriesForecastVariables.ShowDialog();
      }
    }

    private Color GetLineColor(ForecastRequest forecastRequest)
    {
      switch (forecastRequest)
      {
        case ForecastRequest.AriesGasForecast:
          return Color.Green;
        case ForecastRequest.EprcGasForecast:
          return Color.LimeGreen;
        case ForecastRequest.AriesGasActuals:
          return Color.LightGreen;
        case ForecastRequest.EprcGasActuals:
          return Color.LightGreen;

        case ForecastRequest.AriesOilForecast:
          return Color.DarkRed;
        case ForecastRequest.EprcOilForecast:
          return Color.Red;
        case ForecastRequest.AriesOilActuals:
          return Color.LightCoral;
        case ForecastRequest.EprcOilActuals:
          return Color.LightCoral;

        case ForecastRequest.AriesH2OForecast:
          return Color.DarkBlue;
        case ForecastRequest.EprcH2OForecast:
          return Color.Blue;
        case ForecastRequest.AriesH2OActuals:
          return Color.LightSteelBlue;
        case ForecastRequest.EprcH2OActuals:
          return Color.LightSteelBlue;

        case ForecastRequest.TubingPressureActuals:
          return Color.Magenta;
      }

      return Color.Gray;
    }

    private int GetWellIdFromCbo()
    {
      string selectedWell = cboWell.Text;
      int beg = selectedWell.LastIndexOf('(');
      string wellName = selectedWell.Substring(0, beg - 1).Trim();
      return _wellIds[wellName];
    }

    private void ZoomIn()
    {
      pnlChart.Width = pnlChart.Width += Convert.ToInt32(pnlMain.Width * .1);
    }

    private void InitializeApplication()
    {
      _firstShowing = true;
      _forecastData = "No forecast data to show";
      _fDataDisplay = new frmDataDisplay();
      _fDataDisplay.VisibleChanged += _fDataDisplay_VisibleChanged;
      _fDataDisplay.SetText(_forecastData, _forecastData);
      cboTimeUnit.SelectedIndex = 0;

      chartMain.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
      chartMain.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;
      chartMain.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Consolas", 8);
      chartMain.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Consolas", 8);
      chartMain.ChartAreas[0].AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Years;
      chartMain.ChartAreas[0].AxisX.LabelStyle.Interval = 1;


      chartMain.ChartAreas[0].Position.X = 0;
      chartMain.ChartAreas[0].Position.Y = 0;
      chartMain.ChartAreas[0].Position.Height = 100;
      chartMain.ChartAreas[0].Position.Width = 100;
      chartMain.ChartAreas[0].BackColor = Color.White;
      chartMain.ChartAreas[0].AxisX.MajorGrid.IntervalType = DateTimeIntervalType.Years;
      chartMain.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
      chartMain.ChartAreas[0].AxisX.MinorGrid.IntervalType = DateTimeIntervalType.Months;
      chartMain.ChartAreas[0].AxisX.MinorGrid.Interval = 2;
      chartMain.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
      chartMain.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.LightGray;
      chartMain.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dot;

      try
      {
        a = new a();

        int formVerticalSize = 90;
        int formHorizontalSize = 90;

        if (g.AppConfig.ContainsKey("FormVerticalSize"))
          formVerticalSize = g.GetCI("FormVerticalSize").ToInt32();

        if (g.AppConfig.ContainsKey("FormHorizontalSize"))
          formHorizontalSize = g.GetCI("FormHorizontalSize").ToInt32();

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);

      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred during program initialization." + g.crlf2 + ex.ToReport(), "ChartViewer - Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        if (g.AppConfig.GetBoolean("DisableEnvironmentDropDown"))
          cboEnvironment.Enabled = false;

        _ignoreEnvironmentSelectionChange = true;

        cboEnvironment.DataSource = g.GetList("Environments");
        string defaultEnvironment = g.GetCI("DefaultEnvironment");
        if (defaultEnvironment.IsNotBlank())
        {
          for (int i = 0; i < cboEnvironment.Items.Count; i++)
          {
            if (cboEnvironment.Items[i].ToString() == defaultEnvironment)
            {
              _ignoreEnvironmentSelectionChange = false;
              cboEnvironment.SelectedIndex = i;
              break;
            }
          }
        }

        ConnectAndInitializeForEnvironment();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred during program initialization while attempting to retrieve data." + g.crlf2 + ex.ToReport(), "ChartViewer - Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void ReloadWells()
    {
      var wells = _prcData.GetWells();
      _wells = new SortedList<int,Well>();
      _wellIds = new Dictionary<string, int>();

      if (wells != null)
      {
        var wellList = new List<string>();

        foreach (var well in wells)
        {
          if (_producingOnly && well.PRCWellStatus != PRCWellStatus.Producing)
            continue;

          _wells.Add(well.PRCWellId, well);
          string producingStatus = well.PRCWellStatus.IsProducing() ? "Producing" : "TIL";
          wellList.Add(well.WellName + "  (" + producingStatus + ")");
          _wellIds.Add(well.WellName, well.PRCWellId);
        }

        wellList.Sort();
        cboWell.DataSource = wellList;
      }

      InitializeWellDisplay();

      if (cboWell.Text.IsNotBlank())
      {
        int wellId = GetWellIdFromCbo();
        DisplayWellInfo(_wells[wellId]);
      }
    }

    private void _fDataDisplay_VisibleChanged(object sender, EventArgs e)
    {
      if (_fDataDisplay.Visible)
      {
        ctxChartShowData.Tag = "HideData";
        ctxChartShowData.Text = "Hide Data";
      }
      else
      {
        ctxChartShowData.Tag = "ShowData";
        ctxChartShowData.Text = "Show Data";
      }
    }

    private void DisplayWellInfo(Well well)
    {
      InitializeWellDisplay();

      lblPRCWellIDValue.Text = well.PRCWellId.ToString();
      lblGISWellIDValue.Text = well.GISWellId.HasValue ? well.GISWellId.Value.ToString() : String.Empty;
      lblSSIWellIDValue.Text = well.SSIWellId.HasValue ? well.SSIWellId.Value.ToString() : String.Empty;
      lblGPWellIDValue.Text = String.Empty; // later

      WellForecastVariable efv = _prcData.GetWellForecastVariables(well.PRCWellId);
      var forecastParms = GetForecastParmsFromUI();
      var forecaster = new Forecaster(_prcData, GetForecastParmsFromUI());
      WellForecastVariable afv = forecaster.GetAriesForecastVariables(forecastParms);

      if (afv == null)
      {
        if (well.PRCWellStatus.IsProducing())
        {
          string msg = "Aries forecast variables object is null (could not be created due to unavailability or an exception).";
          MessageBox.Show(msg, "Forecast Viewer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      else
      {
        if (!afv.AriesVariablesExist)
        {
          if (well.PRCWellStatus.IsProducing())
          {
            string msg = "Aries forecast variables do not exist.";
            MessageBox.Show(msg, "Forecast Viewer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
        else
        {
          if (afv.AriesVariablesInvalid)
          {
            if (well.PRCWellStatus.IsProducing())
            {
              string msg = "Aries forecast variables cannot be translated to EPRC forecast variables." + g.crlf2 +
                           "To view the raw Aries variables data and specific error messages use the View menu.";
              _afvReport = afv.AriesWfvRawReport + g.crlf2 + "EXCEPTION" + g.crlf + afv.AriesWfvReport;
              MessageBox.Show(msg, "Forecast Viewer - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          }
        }
      }

      if (afv != null && afv.AriesVariablesExist && !afv.AriesVariablesInvalid)
        _afvReport = afv.AriesWfvReport;

      ckTubingPressure.Checked = ckTubingPressure.Enabled = well.PRCWellStatus.IsProducing();

      if (efv == null)
      {
        ckEprcOilForecast.Checked = ckEprcOilForecast.Enabled = ckEprcOilActuals.Checked = ckEprcOilActuals.Enabled = false;
        ckEprcGasForecast.Checked = ckEprcGasForecast.Enabled = ckEprcGasActuals.Checked = ckEprcGasActuals.Enabled = false;
        ckEprcH2OForecast.Checked = ckEprcH2OForecast.Enabled = ckEprcH2OActuals.Checked = ckEprcH2OActuals.Enabled = false;
      }
      else
      {
        lblEprcOilStartDate.Text = efv.StartDate.AddDays(efv.Oil_ForecastDelay).ToShortDateString();
        lblEprcGasStartDate.Text = efv.StartDate.AddDays(efv.Gas_ForecastDelay).ToShortDateString();
        lblEprcH2OStartDate.Text = efv.StartDate.AddDays(efv.H2O_ForecastDelay).ToShortDateString();

        lblEprcOilInitialRate.Text = efv.Oil_InitialRate.ToString("###,##0.00");
        lblEprcGasInitialRate.Text = efv.Gas_InitialRate.ToString("###,##0.00");
        lblEprcH2OInitialRate.Text = efv.H2O_InitialRate.ToString("###,##0.00");

        lblEprcOilDelayDays.Text = efv.Oil_ForecastDelay.ToString("#,##0");
        lblEprcGasDelayDays.Text = efv.Gas_ForecastDelay.ToString("#,##0");
        lblEprcH2ODelayDays.Text = efv.H2O_ForecastDelay.ToString("#,##0");

        lblEprcOilFlatDays.Text = efv.Oil_FlatProduction.ToString("#,##0");
        lblEprcGasFlatDays.Text = efv.Gas_FlatProduction.ToString("#,##0");
        lblEprcH2OFlatDays.Text = efv.H2O_FlatProduction.ToString("#,##0");

        lblEprcOilHypExp.Text = efv.Oil_HyberbolicExponent.ToString("#0.0000");
        lblEprcGasHypExp.Text = efv.Gas_HyberbolicExponent.ToString("#0.0000");
        lblEprcH2OHypExp.Text = efv.H2O_HyberbolicExponent.ToString("#0.0000");

        lblEprcOilYr1NomDec.Text = efv.Oil_1stYearNominalDecline.ToString("#0.000000");
        lblEprcGasYr1NomDec.Text = efv.Gas_1stYearNominalDecline.ToString("#0.000000");
        lblEprcH2OYr1NomDec.Text = efv.H2O_1stYearNominalDecline.ToString("#0.000000");

        ckEprcOilForecast.Enabled = ckEprcOilForecast.Checked = ckEprcOilActuals.Enabled = ckEprcOilActuals.Checked = efv.Oil_InitialRate > 0;
        ckEprcGasForecast.Enabled = ckEprcGasForecast.Checked = ckEprcGasActuals.Enabled = ckEprcGasActuals.Checked = efv.Gas_InitialRate > 0;
        ckEprcH2OForecast.Enabled = ckEprcH2OForecast.Checked = ckEprcH2OActuals.Enabled = ckEprcH2OActuals.Checked = efv.H2O_InitialRate > 0;

      }

      if (afv == null)
      {
        ckAriesOilForecast.Checked = ckAriesOilForecast.Enabled = ckAriesOilActuals.Checked = ckAriesOilActuals.Enabled = false;
        ckAriesGasForecast.Checked = ckAriesGasForecast.Enabled = ckAriesGasActuals.Checked = ckAriesGasActuals.Enabled = false;
        ckAriesH2OForecast.Checked = ckAriesH2OForecast.Enabled = ckAriesH2OActuals.Checked = ckAriesH2OActuals.Enabled = false;
      }
      else
      {
        if (afv.AriesVariablesExist && !afv.AriesVariablesInvalid)
        {
          lblAriesOilStartDate.Text = afv.StartDate.AddDays(afv.Oil_ForecastDelay).ToShortDateString();
          lblAriesGasStartDate.Text = afv.StartDate.AddDays(afv.Gas_ForecastDelay).ToShortDateString();
          lblAriesH2OStartDate.Text = afv.StartDate.AddDays(afv.H2O_ForecastDelay).ToShortDateString();

          lblAriesOilInitialRate.Text = afv.Oil_InitialRate.ToString("###,##0.00");
          lblAriesGasInitialRate.Text = afv.Gas_InitialRate.ToString("###,##0.00");
          lblAriesH2OInitialRate.Text = afv.H2O_InitialRate.ToString("###,##0.00");

          lblAriesOilDelayDays.Text = afv.Oil_ForecastDelay.ToString("#,##0");
          lblAriesGasDelayDays.Text = afv.Gas_ForecastDelay.ToString("#,##0");
          lblAriesH2ODelayDays.Text = afv.H2O_ForecastDelay.ToString("#,##0");

          lblAriesOilFlatDays.Text = afv.Oil_FlatProduction.ToString("#,##0");
          lblAriesGasFlatDays.Text = afv.Gas_FlatProduction.ToString("#,##0");
          lblAriesH2OFlatDays.Text = afv.H2O_FlatProduction.ToString("#,##0");

          lblAriesOilHypExp.Text = afv.Oil_HyberbolicExponent.ToString("#0.0000");
          lblAriesGasHypExp.Text = afv.Gas_HyberbolicExponent.ToString("#0.0000");
          lblAriesH2OHypExp.Text = afv.H2O_HyberbolicExponent.ToString("#0.0000");

          lblAriesOilYr1NomDec.Text = afv.Oil_1stYearNominalDecline.ToString("#0.000000");
          lblAriesGasYr1NomDec.Text = afv.Gas_1stYearNominalDecline.ToString("#0.000000");
          lblAriesH2OYr1NomDec.Text = afv.H2O_1stYearNominalDecline.ToString("#0.000000");

          ckAriesOilForecast.Enabled = ckAriesOilForecast.Checked = ckAriesOilActuals.Enabled = ckAriesOilActuals.Checked = afv.Oil_InitialRate > 0;
          ckAriesGasForecast.Enabled = ckAriesGasForecast.Checked = ckAriesGasActuals.Enabled = ckAriesGasActuals.Checked = afv.Gas_InitialRate > 0;
          ckAriesH2OForecast.Enabled = ckAriesH2OForecast.Checked = ckAriesH2OActuals.Enabled = ckAriesH2OActuals.Checked = afv.H2O_InitialRate > 0;
        }
      }

      if (!well.PRCWellStatus.IsProducing())
      {
        ckAriesOilActuals.Enabled = ckAriesOilActuals.Checked = ckAriesGasActuals.Enabled = ckAriesGasActuals.Checked = ckAriesH2OActuals.Enabled = ckAriesH2OActuals.Checked =
                                      ckEprcOilActuals.Enabled = ckEprcOilActuals.Checked = ckEprcGasActuals.Enabled = ckEprcGasActuals.Checked = ckEprcH2OActuals.Enabled = ckEprcH2OActuals.Checked = false;
      }


      ckAriesSystemOilFcst.Enabled = ckAriesSystemOilFcst.Checked = ckAriesSystemGasFcst.Enabled = ckAriesSystemGasFcst.Checked =
                                       ckAriesSystemH2OFcst.Enabled = ckAriesSystemH2OFcst.Checked = true;

      ckAriesH2OForecast.Enabled = ckAriesH2OForecast.Checked = ckAriesH2OActuals.Enabled = ckAriesH2OActuals.Checked =
                                     ckEprcH2OForecast.Enabled = ckEprcH2OForecast.Checked = ckEprcH2OActuals.Enabled = ckEprcH2OActuals.Checked =
                                           ckAriesSystemH2OFcst.Checked = ckAriesSystemH2OFcst.Enabled = false;

      ckTubingPressure.Checked = ckTubingPressure.Enabled = false;

      if (ckAriesOilActuals.Checked && ckEprcOilActuals.Checked)
        ckAriesOilActuals.Checked = ckAriesOilActuals.Enabled = false;

      if (ckAriesGasActuals.Checked && ckEprcGasActuals.Checked)
        ckAriesGasActuals.Checked = ckAriesGasActuals.Enabled = false;

      if (ckAriesH2OActuals.Checked && ckEprcH2OActuals.Checked)
        ckAriesH2OActuals.Checked = ckAriesH2OActuals.Enabled = false;
    }

    private void InitializeWellDisplay()
    {
      lblPRCWellIDValue.Text = "00000";
      lblGISWellIDValue.Text = "00000";
      lblSSIWellIDValue.Text = "00000";
      lblGPWellIDValue.Text = String.Empty;

      lblAriesOilStartDate.Text = String.Empty;
      lblEprcOilStartDate.Text = String.Empty;
      lblAriesGasStartDate.Text = String.Empty;
      lblEprcGasStartDate.Text = String.Empty;
      lblAriesH2OStartDate.Text = String.Empty;
      lblEprcH2OStartDate.Text = String.Empty;

      lblAriesOilInitialRate.Text = String.Empty;
      lblEprcOilInitialRate.Text = String.Empty;
      lblAriesGasInitialRate.Text = String.Empty;
      lblEprcGasInitialRate.Text = String.Empty;
      lblAriesH2OInitialRate.Text = String.Empty;
      lblEprcH2OInitialRate.Text = String.Empty;

      lblAriesOilDelayDays.Text = String.Empty;
      lblEprcOilDelayDays.Text = String.Empty;
      lblAriesGasDelayDays.Text = String.Empty;
      lblEprcGasDelayDays.Text = String.Empty;
      lblAriesH2ODelayDays.Text = String.Empty;
      lblEprcH2ODelayDays.Text = String.Empty;

      lblAriesOilFlatDays.Text = String.Empty;
      lblEprcOilFlatDays.Text = String.Empty;
      lblAriesGasFlatDays.Text = String.Empty;
      lblEprcGasFlatDays.Text = String.Empty;
      lblAriesH2OFlatDays.Text = String.Empty;
      lblEprcH2OFlatDays.Text = String.Empty;

      lblAriesOilHypExp.Text = String.Empty;
      lblEprcOilHypExp.Text = String.Empty;
      lblAriesGasHypExp.Text = String.Empty;
      lblEprcGasHypExp.Text = String.Empty;
      lblAriesH2OHypExp.Text = String.Empty;

      lblAriesOilYr1NomDec.Text = String.Empty;
      lblEprcOilYr1NomDec.Text = String.Empty;
      lblAriesGasYr1NomDec.Text = String.Empty;
      lblEprcGasYr1NomDec.Text = String.Empty;
      lblAriesH2OYr1NomDec.Text = String.Empty;
      lblEprcH2OYr1NomDec.Text = String.Empty;

      Application.DoEvents();
    }

    private ForecastParms GetForecastParmsFromUI()
    {
      var forecastParms = new ForecastParms();
      forecastParms.EPRCWellID = GetWellIdFromCbo();
      forecastParms.StartDate = dtpStartDate.Value;
      forecastParms.EndDate = dtpEndDate.Value;
      forecastParms.ExcludeDelayDays = ckExcludeDelayDays.Checked;
      forecastParms.ForecastTimeUnit = cboTimeUnit.Text.ToEnum<ForecastTimeUnit>(ForecastTimeUnit.Days);
      forecastParms.WellName = _wells[forecastParms.EPRCWellID].WellName;
      forecastParms.PRCWellStatus = _wells[forecastParms.EPRCWellID].PRCWellStatus;
      forecastParms.SSIWellId = _wells[forecastParms.EPRCWellID].SSIWellId;

      if (ckAriesOilForecast.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckAriesOilForecast.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckAriesOilActuals.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckAriesOilActuals.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckAriesGasForecast.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckAriesGasForecast.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckAriesGasActuals.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckAriesGasActuals.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckAriesH2OForecast.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckAriesH2OForecast.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckAriesH2OActuals.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckAriesH2OActuals.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckEprcOilForecast.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckEprcOilForecast.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckEprcOilActuals.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckEprcOilActuals.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckEprcGasForecast.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckEprcGasForecast.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckEprcGasActuals.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckEprcGasActuals.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckEprcH2OForecast.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckEprcH2OForecast.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckEprcH2OActuals.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckEprcH2OActuals.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));

      if (ckAriesSystemOilFcst.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckAriesSystemOilFcst.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckAriesSystemGasFcst.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckAriesSystemGasFcst.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));
      if (ckAriesSystemH2OFcst.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckAriesSystemH2OFcst.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));

      if (ckTubingPressure.Checked)
        forecastParms.ForecastRequestSet.Add(forecastParms.ForecastRequestSet.Count, ckTubingPressure.Tag.ToString().ToEnum<ForecastRequest>(ForecastRequest.NotSet));

      return forecastParms;
    }

    private string GetDbSpecFromEnvironment()
    {
      string env = cboEnvironment.Text;
      int beg = env.IndexOf('-');
      return env.Substring(beg + 1).Trim();
    }

    private void ShowData()
    {
      if (_fDataDisplay == null)
        _fDataDisplay = new frmDataDisplay();

      if (_fDataDisplay.IsDisposed)
        _fDataDisplay = new frmDataDisplay();

      if (ctxChartShowData.Tag.ToString() == "ShowData")
      {
        ctxChartShowData.Tag = "HideData";
        ctxChartShowData.Text = "Hide Data";
        _fDataDisplay.Visible = true;
        _fDataDisplay.BringToFront();
      }
      else
      {
        ctxChartShowData.Tag = "ShowData";
        ctxChartShowData.Text = "Show Data";
        _fDataDisplay.Visible = false;
      }
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_firstShowing)
        return;

      _firstShowing = false;
      pnlChart.Width = this.ClientRectangle.Width;
    }

    private void frmMain_ResizeEnd(object sender, EventArgs e)
    {
      pnlChart.Width = this.ClientRectangle.Width;
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_prcData != null)
      {
        _prcData.Dispose();
      }
    }

    private void cboWell_SelectedIndexChanged(object sender, EventArgs e)
    {
      chartMain.Series.Clear();
      chartMain.Titles.Clear();
      Application.DoEvents();


      _fDataDisplay.SetText("No forecast data", "No forecast data");

      if (cboWell.Text.IsNotBlank())
      {
        int wellId = GetWellIdFromCbo();
        DisplayWellInfo(_wells[wellId]);
      }
    }

    private void ckProducingWellsOnly_CheckedChanged(object sender, EventArgs e)
    {
      if (ckProducingWellsOnly.Checked != _producingOnly)
      {
        _producingOnly = ckProducingWellsOnly.Checked;
        ReloadWells();
      }
    }

    private void cboEnvironment_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_ignoreEnvironmentSelectionChange)
      {
        _ignoreEnvironmentSelectionChange = false;
        return;
      }

      ConnectAndInitializeForEnvironment();

      if (!_firstShowing)
        MessageBox.Show("Environment set to " + _prcData.SqlConnection.DataSource, "Forecast Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ConnectAndInitializeForEnvironment()
    {
      string dbSpecName = GetDbSpecFromEnvironment();
      var dbSpec = g.GetDbSpec(dbSpecName);

      if (!dbSpec.IsReadyToConnect())
      {
        MessageBox.Show("Database connection configuration is not correct." + g.crlf2 + "Database configuration spec name is '" + dbSpecName + "'.",
                        "EPRC Manager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        if (_saveLastSuccessfulEnvironment.IsNotBlank())
        {
          for (int i = 0; i < cboEnvironment.Items.Count; i++)
          {
            if (cboEnvironment.Items[i].ToString() == _saveLastSuccessfulEnvironment)
            {
              _ignoreEnvironmentSelectionChange = true;
              cboEnvironment.SelectedIndex = i;
              break;
            }
          }
        }
        return;
      }

      _prcData = new PrcDataAccess(dbSpec.ConnectionString);

      _saveLastSuccessfulEnvironment = cboEnvironment.Text;

      ReloadWells();

      var wells = _prcData.GetWells();
      _wells = new SortedList<int, Well>();
      _wellIds = new Dictionary<string, int>();

      if (wells != null)
      {
        var wellList = new List<string>();

        foreach (var well in wells)
        {
          if (well.PRCWellStatus == PRCWellStatus.Producing)
          {
            _wells.Add(well.PRCWellId, well);
            string producingStatus = well.PRCWellStatus.IsProducing() ? "Producing" : "TIL";
            wellList.Add(well.WellName + "  (" + producingStatus + ")");
            _wellIds.Add(well.WellName, well.PRCWellId);
          }
        }

        wellList.Sort();
        cboWell.DataSource = wellList;
      }

      InitializeWellDisplay();

      if (cboWell.Text.IsNotBlank())
      {
        int wellId = GetWellIdFromCbo();
        DisplayWellInfo(_wells[wellId]);
      }
    }
  }
}

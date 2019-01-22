namespace Org.ChartViewer
{
  partial class frmMain
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuViewAriesVariables = new System.Windows.Forms.ToolStripMenuItem();
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlTopControl = new System.Windows.Forms.Panel();
      this.lblEprcH2OYr1NomDec = new System.Windows.Forms.Label();
      this.lblEprcGasYr1NomDec = new System.Windows.Forms.Label();
      this.lblEprcOilYr1NomDec = new System.Windows.Forms.Label();
      this.lblAriesH2OYr1NomDec = new System.Windows.Forms.Label();
      this.lblAriesGasYr1NomDec = new System.Windows.Forms.Label();
      this.lblAriesOilYr1NomDec = new System.Windows.Forms.Label();
      this.lblYr1NomDec = new System.Windows.Forms.Label();
      this.lblEprcH2OHypExp = new System.Windows.Forms.Label();
      this.lblEprcGasHypExp = new System.Windows.Forms.Label();
      this.lblEprcOilHypExp = new System.Windows.Forms.Label();
      this.lblAriesH2OHypExp = new System.Windows.Forms.Label();
      this.lblAriesGasHypExp = new System.Windows.Forms.Label();
      this.lblAriesOilHypExp = new System.Windows.Forms.Label();
      this.lblHypExp = new System.Windows.Forms.Label();
      this.lblEprcH2OFlatDays = new System.Windows.Forms.Label();
      this.lblEprcGasFlatDays = new System.Windows.Forms.Label();
      this.lblEprcOilFlatDays = new System.Windows.Forms.Label();
      this.lblAriesH2OFlatDays = new System.Windows.Forms.Label();
      this.lblAriesGasFlatDays = new System.Windows.Forms.Label();
      this.lblAriesOilFlatDays = new System.Windows.Forms.Label();
      this.lblFlatDays = new System.Windows.Forms.Label();
      this.lblEprcH2ODelayDays = new System.Windows.Forms.Label();
      this.lblEprcGasDelayDays = new System.Windows.Forms.Label();
      this.lblEprcOilDelayDays = new System.Windows.Forms.Label();
      this.lblAriesH2ODelayDays = new System.Windows.Forms.Label();
      this.lblAriesGasDelayDays = new System.Windows.Forms.Label();
      this.lblAriesOilDelayDays = new System.Windows.Forms.Label();
      this.lblEprcH2OStartDate = new System.Windows.Forms.Label();
      this.lblEprcH2OInitialRate = new System.Windows.Forms.Label();
      this.lblEprcGasStartDate = new System.Windows.Forms.Label();
      this.lblEprcGasInitialRate = new System.Windows.Forms.Label();
      this.lblEprcOilStartDate = new System.Windows.Forms.Label();
      this.lblEprcOilInitialRate = new System.Windows.Forms.Label();
      this.lblAriesH2OStartDate = new System.Windows.Forms.Label();
      this.lblAriesH2OInitialRate = new System.Windows.Forms.Label();
      this.lblAriesGasStartDate = new System.Windows.Forms.Label();
      this.lblAriesGasInitialRate = new System.Windows.Forms.Label();
      this.lblDelayDays = new System.Windows.Forms.Label();
      this.lblH2OEprc = new System.Windows.Forms.Label();
      this.lblGasEprc = new System.Windows.Forms.Label();
      this.lblAriesOilStartDate = new System.Windows.Forms.Label();
      this.lblAriesOilInitialRate = new System.Windows.Forms.Label();
      this.lblOilEprc = new System.Windows.Forms.Label();
      this.lblH2OAries = new System.Windows.Forms.Label();
      this.lblGasAries = new System.Windows.Forms.Label();
      this.lblFcstActTp = new System.Windows.Forms.Label();
      this.lblFcstStartDate = new System.Windows.Forms.Label();
      this.lblInitialRate = new System.Windows.Forms.Label();
      this.lblOilAries = new System.Windows.Forms.Label();
      this.cboTimeUnit = new System.Windows.Forms.ComboBox();
      this.btnZoomIn = new System.Windows.Forms.Button();
      this.cboEnvironment = new System.Windows.Forms.ComboBox();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.ckEprcH2OActuals = new System.Windows.Forms.CheckBox();
      this.ckAriesH2OActuals = new System.Windows.Forms.CheckBox();
      this.ckEprcH2OForecast = new System.Windows.Forms.CheckBox();
      this.ckTubingPressure = new System.Windows.Forms.CheckBox();
      this.ckEprcGasActuals = new System.Windows.Forms.CheckBox();
      this.ckAriesH2OForecast = new System.Windows.Forms.CheckBox();
      this.ckAriesGasActuals = new System.Windows.Forms.CheckBox();
      this.ckEprcGasForecast = new System.Windows.Forms.CheckBox();
      this.ckAriesGasForecast = new System.Windows.Forms.CheckBox();
      this.ckEprcOilActuals = new System.Windows.Forms.CheckBox();
      this.ckProducingWellsOnly = new System.Windows.Forms.CheckBox();
      this.ckExcludeDelayDays = new System.Windows.Forms.CheckBox();
      this.ckAriesOilActuals = new System.Windows.Forms.CheckBox();
      this.ckEprcOilForecast = new System.Windows.Forms.CheckBox();
      this.ckAriesOilForecast = new System.Windows.Forms.CheckBox();
      this.lblEndDate = new System.Windows.Forms.Label();
      this.lblStartDate = new System.Windows.Forms.Label();
      this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
      this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
      this.lblGPWellIDValue = new System.Windows.Forms.Label();
      this.lblSSIWellIDValue = new System.Windows.Forms.Label();
      this.lblGISWellIDValue = new System.Windows.Forms.Label();
      this.lblPRCWellIDValue = new System.Windows.Forms.Label();
      this.lblGPWellID = new System.Windows.Forms.Label();
      this.lblSSIWellID = new System.Windows.Forms.Label();
      this.lblGISWellID = new System.Windows.Forms.Label();
      this.lblPRCWellID = new System.Windows.Forms.Label();
      this.lblTimeUnit = new System.Windows.Forms.Label();
      this.lblWell = new System.Windows.Forms.Label();
      this.cboWell = new System.Windows.Forms.ComboBox();
      this.btnRunForecast = new System.Windows.Forms.Button();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.pnlChart = new System.Windows.Forms.Panel();
      this.chartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.ctxChart = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxChartShowData = new System.Windows.Forms.ToolStripMenuItem();
      this.ckAriesSystemOilFcst = new System.Windows.Forms.CheckBox();
      this.ckAriesSystemGasFcst = new System.Windows.Forms.CheckBox();
      this.ckAriesSystemH2OFcst = new System.Windows.Forms.CheckBox();
      this.mnuMain.SuspendLayout();
      this.pnlTopControl.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.pnlChart.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
      this.ctxChart.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1508, 24);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "&File";
      // 
      // mnuFileExit
      // 
      this.mnuFileExit.Name = "mnuFileExit";
      this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      // 
      // mnuView
      // 
      this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewAriesVariables});
      this.mnuView.Name = "mnuView";
      this.mnuView.Size = new System.Drawing.Size(44, 20);
      this.mnuView.Text = "View";
      // 
      // mnuViewAriesVariables
      // 
      this.mnuViewAriesVariables.Name = "mnuViewAriesVariables";
      this.mnuViewAriesVariables.Size = new System.Drawing.Size(197, 22);
      this.mnuViewAriesVariables.Tag = "ViewAriesForecastVariables";
      this.mnuViewAriesVariables.Text = "Aries Forecast Variables";
      this.mnuViewAriesVariables.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 760);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1508, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTopControl
      // 
      this.pnlTopControl.BackColor = System.Drawing.SystemColors.ControlLight;
      this.pnlTopControl.Controls.Add(this.lblEprcH2OYr1NomDec);
      this.pnlTopControl.Controls.Add(this.lblEprcGasYr1NomDec);
      this.pnlTopControl.Controls.Add(this.lblEprcOilYr1NomDec);
      this.pnlTopControl.Controls.Add(this.lblAriesH2OYr1NomDec);
      this.pnlTopControl.Controls.Add(this.lblAriesGasYr1NomDec);
      this.pnlTopControl.Controls.Add(this.lblAriesOilYr1NomDec);
      this.pnlTopControl.Controls.Add(this.lblYr1NomDec);
      this.pnlTopControl.Controls.Add(this.lblEprcH2OHypExp);
      this.pnlTopControl.Controls.Add(this.lblEprcGasHypExp);
      this.pnlTopControl.Controls.Add(this.lblEprcOilHypExp);
      this.pnlTopControl.Controls.Add(this.lblAriesH2OHypExp);
      this.pnlTopControl.Controls.Add(this.lblAriesGasHypExp);
      this.pnlTopControl.Controls.Add(this.lblAriesOilHypExp);
      this.pnlTopControl.Controls.Add(this.lblHypExp);
      this.pnlTopControl.Controls.Add(this.lblEprcH2OFlatDays);
      this.pnlTopControl.Controls.Add(this.lblEprcGasFlatDays);
      this.pnlTopControl.Controls.Add(this.lblEprcOilFlatDays);
      this.pnlTopControl.Controls.Add(this.lblAriesH2OFlatDays);
      this.pnlTopControl.Controls.Add(this.lblAriesGasFlatDays);
      this.pnlTopControl.Controls.Add(this.lblAriesOilFlatDays);
      this.pnlTopControl.Controls.Add(this.lblFlatDays);
      this.pnlTopControl.Controls.Add(this.lblEprcH2ODelayDays);
      this.pnlTopControl.Controls.Add(this.lblEprcGasDelayDays);
      this.pnlTopControl.Controls.Add(this.lblEprcOilDelayDays);
      this.pnlTopControl.Controls.Add(this.lblAriesH2ODelayDays);
      this.pnlTopControl.Controls.Add(this.lblAriesGasDelayDays);
      this.pnlTopControl.Controls.Add(this.lblAriesOilDelayDays);
      this.pnlTopControl.Controls.Add(this.lblEprcH2OStartDate);
      this.pnlTopControl.Controls.Add(this.lblEprcH2OInitialRate);
      this.pnlTopControl.Controls.Add(this.lblEprcGasStartDate);
      this.pnlTopControl.Controls.Add(this.lblEprcGasInitialRate);
      this.pnlTopControl.Controls.Add(this.lblEprcOilStartDate);
      this.pnlTopControl.Controls.Add(this.lblEprcOilInitialRate);
      this.pnlTopControl.Controls.Add(this.lblAriesH2OStartDate);
      this.pnlTopControl.Controls.Add(this.lblAriesH2OInitialRate);
      this.pnlTopControl.Controls.Add(this.lblAriesGasStartDate);
      this.pnlTopControl.Controls.Add(this.lblAriesGasInitialRate);
      this.pnlTopControl.Controls.Add(this.lblDelayDays);
      this.pnlTopControl.Controls.Add(this.lblH2OEprc);
      this.pnlTopControl.Controls.Add(this.lblGasEprc);
      this.pnlTopControl.Controls.Add(this.lblAriesOilStartDate);
      this.pnlTopControl.Controls.Add(this.lblAriesOilInitialRate);
      this.pnlTopControl.Controls.Add(this.lblOilEprc);
      this.pnlTopControl.Controls.Add(this.lblH2OAries);
      this.pnlTopControl.Controls.Add(this.lblGasAries);
      this.pnlTopControl.Controls.Add(this.lblFcstStartDate);
      this.pnlTopControl.Controls.Add(this.lblInitialRate);
      this.pnlTopControl.Controls.Add(this.lblOilAries);
      this.pnlTopControl.Controls.Add(this.cboTimeUnit);
      this.pnlTopControl.Controls.Add(this.btnZoomIn);
      this.pnlTopControl.Controls.Add(this.cboEnvironment);
      this.pnlTopControl.Controls.Add(this.lblEnvironment);
      this.pnlTopControl.Controls.Add(this.ckEprcH2OActuals);
      this.pnlTopControl.Controls.Add(this.ckAriesH2OActuals);
      this.pnlTopControl.Controls.Add(this.ckEprcH2OForecast);
      this.pnlTopControl.Controls.Add(this.ckTubingPressure);
      this.pnlTopControl.Controls.Add(this.ckEprcGasActuals);
      this.pnlTopControl.Controls.Add(this.ckAriesH2OForecast);
      this.pnlTopControl.Controls.Add(this.ckAriesGasActuals);
      this.pnlTopControl.Controls.Add(this.ckEprcGasForecast);
      this.pnlTopControl.Controls.Add(this.ckAriesGasForecast);
      this.pnlTopControl.Controls.Add(this.ckEprcOilActuals);
      this.pnlTopControl.Controls.Add(this.ckProducingWellsOnly);
      this.pnlTopControl.Controls.Add(this.ckExcludeDelayDays);
      this.pnlTopControl.Controls.Add(this.ckAriesSystemH2OFcst);
      this.pnlTopControl.Controls.Add(this.ckAriesSystemGasFcst);
      this.pnlTopControl.Controls.Add(this.ckAriesSystemOilFcst);
      this.pnlTopControl.Controls.Add(this.ckAriesOilActuals);
      this.pnlTopControl.Controls.Add(this.ckEprcOilForecast);
      this.pnlTopControl.Controls.Add(this.ckAriesOilForecast);
      this.pnlTopControl.Controls.Add(this.lblEndDate);
      this.pnlTopControl.Controls.Add(this.lblStartDate);
      this.pnlTopControl.Controls.Add(this.dtpEndDate);
      this.pnlTopControl.Controls.Add(this.dtpStartDate);
      this.pnlTopControl.Controls.Add(this.lblGPWellIDValue);
      this.pnlTopControl.Controls.Add(this.lblSSIWellIDValue);
      this.pnlTopControl.Controls.Add(this.lblGISWellIDValue);
      this.pnlTopControl.Controls.Add(this.lblPRCWellIDValue);
      this.pnlTopControl.Controls.Add(this.lblGPWellID);
      this.pnlTopControl.Controls.Add(this.lblSSIWellID);
      this.pnlTopControl.Controls.Add(this.lblGISWellID);
      this.pnlTopControl.Controls.Add(this.lblPRCWellID);
      this.pnlTopControl.Controls.Add(this.lblTimeUnit);
      this.pnlTopControl.Controls.Add(this.lblWell);
      this.pnlTopControl.Controls.Add(this.cboWell);
      this.pnlTopControl.Controls.Add(this.btnRunForecast);
      this.pnlTopControl.Controls.Add(this.lblFcstActTp);
      this.pnlTopControl.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTopControl.Location = new System.Drawing.Point(0, 24);
      this.pnlTopControl.Name = "pnlTopControl";
      this.pnlTopControl.Size = new System.Drawing.Size(1508, 147);
      this.pnlTopControl.TabIndex = 2;
      // 
      // lblEprcH2OYr1NomDec
      // 
      this.lblEprcH2OYr1NomDec.Location = new System.Drawing.Point(1040, 115);
      this.lblEprcH2OYr1NomDec.Name = "lblEprcH2OYr1NomDec";
      this.lblEprcH2OYr1NomDec.Size = new System.Drawing.Size(70, 15);
      this.lblEprcH2OYr1NomDec.TabIndex = 12;
      this.lblEprcH2OYr1NomDec.Text = "0.00";
      this.lblEprcH2OYr1NomDec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcGasYr1NomDec
      // 
      this.lblEprcGasYr1NomDec.Location = new System.Drawing.Point(1040, 81);
      this.lblEprcGasYr1NomDec.Name = "lblEprcGasYr1NomDec";
      this.lblEprcGasYr1NomDec.Size = new System.Drawing.Size(70, 15);
      this.lblEprcGasYr1NomDec.TabIndex = 12;
      this.lblEprcGasYr1NomDec.Text = "0.00";
      this.lblEprcGasYr1NomDec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcOilYr1NomDec
      // 
      this.lblEprcOilYr1NomDec.Location = new System.Drawing.Point(1040, 47);
      this.lblEprcOilYr1NomDec.Name = "lblEprcOilYr1NomDec";
      this.lblEprcOilYr1NomDec.Size = new System.Drawing.Size(70, 15);
      this.lblEprcOilYr1NomDec.TabIndex = 12;
      this.lblEprcOilYr1NomDec.Text = "0.00";
      this.lblEprcOilYr1NomDec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesH2OYr1NomDec
      // 
      this.lblAriesH2OYr1NomDec.Location = new System.Drawing.Point(1040, 100);
      this.lblAriesH2OYr1NomDec.Name = "lblAriesH2OYr1NomDec";
      this.lblAriesH2OYr1NomDec.Size = new System.Drawing.Size(70, 15);
      this.lblAriesH2OYr1NomDec.TabIndex = 12;
      this.lblAriesH2OYr1NomDec.Text = "0.00";
      this.lblAriesH2OYr1NomDec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesGasYr1NomDec
      // 
      this.lblAriesGasYr1NomDec.Location = new System.Drawing.Point(1040, 66);
      this.lblAriesGasYr1NomDec.Name = "lblAriesGasYr1NomDec";
      this.lblAriesGasYr1NomDec.Size = new System.Drawing.Size(70, 15);
      this.lblAriesGasYr1NomDec.TabIndex = 12;
      this.lblAriesGasYr1NomDec.Text = "0.00";
      this.lblAriesGasYr1NomDec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesOilYr1NomDec
      // 
      this.lblAriesOilYr1NomDec.Location = new System.Drawing.Point(1040, 32);
      this.lblAriesOilYr1NomDec.Name = "lblAriesOilYr1NomDec";
      this.lblAriesOilYr1NomDec.Size = new System.Drawing.Size(70, 15);
      this.lblAriesOilYr1NomDec.TabIndex = 12;
      this.lblAriesOilYr1NomDec.Text = "0.00";
      this.lblAriesOilYr1NomDec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblYr1NomDec
      // 
      this.lblYr1NomDec.Location = new System.Drawing.Point(1040, 13);
      this.lblYr1NomDec.Name = "lblYr1NomDec";
      this.lblYr1NomDec.Size = new System.Drawing.Size(70, 15);
      this.lblYr1NomDec.TabIndex = 12;
      this.lblYr1NomDec.Text = "Yr1NomDec";
      this.lblYr1NomDec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcH2OHypExp
      // 
      this.lblEprcH2OHypExp.Location = new System.Drawing.Point(965, 115);
      this.lblEprcH2OHypExp.Name = "lblEprcH2OHypExp";
      this.lblEprcH2OHypExp.Size = new System.Drawing.Size(70, 15);
      this.lblEprcH2OHypExp.TabIndex = 12;
      this.lblEprcH2OHypExp.Text = "0.00";
      this.lblEprcH2OHypExp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcGasHypExp
      // 
      this.lblEprcGasHypExp.Location = new System.Drawing.Point(965, 81);
      this.lblEprcGasHypExp.Name = "lblEprcGasHypExp";
      this.lblEprcGasHypExp.Size = new System.Drawing.Size(70, 15);
      this.lblEprcGasHypExp.TabIndex = 12;
      this.lblEprcGasHypExp.Text = "0.00";
      this.lblEprcGasHypExp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcOilHypExp
      // 
      this.lblEprcOilHypExp.Location = new System.Drawing.Point(965, 47);
      this.lblEprcOilHypExp.Name = "lblEprcOilHypExp";
      this.lblEprcOilHypExp.Size = new System.Drawing.Size(70, 15);
      this.lblEprcOilHypExp.TabIndex = 12;
      this.lblEprcOilHypExp.Text = "0.00";
      this.lblEprcOilHypExp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesH2OHypExp
      // 
      this.lblAriesH2OHypExp.Location = new System.Drawing.Point(965, 100);
      this.lblAriesH2OHypExp.Name = "lblAriesH2OHypExp";
      this.lblAriesH2OHypExp.Size = new System.Drawing.Size(70, 15);
      this.lblAriesH2OHypExp.TabIndex = 12;
      this.lblAriesH2OHypExp.Text = "0.00";
      this.lblAriesH2OHypExp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesGasHypExp
      // 
      this.lblAriesGasHypExp.Location = new System.Drawing.Point(965, 66);
      this.lblAriesGasHypExp.Name = "lblAriesGasHypExp";
      this.lblAriesGasHypExp.Size = new System.Drawing.Size(70, 15);
      this.lblAriesGasHypExp.TabIndex = 12;
      this.lblAriesGasHypExp.Text = "0.00";
      this.lblAriesGasHypExp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesOilHypExp
      // 
      this.lblAriesOilHypExp.Location = new System.Drawing.Point(965, 32);
      this.lblAriesOilHypExp.Name = "lblAriesOilHypExp";
      this.lblAriesOilHypExp.Size = new System.Drawing.Size(70, 15);
      this.lblAriesOilHypExp.TabIndex = 12;
      this.lblAriesOilHypExp.Text = "0.00";
      this.lblAriesOilHypExp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblHypExp
      // 
      this.lblHypExp.Location = new System.Drawing.Point(965, 13);
      this.lblHypExp.Name = "lblHypExp";
      this.lblHypExp.Size = new System.Drawing.Size(70, 15);
      this.lblHypExp.TabIndex = 12;
      this.lblHypExp.Text = "Hyp Exp";
      this.lblHypExp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcH2OFlatDays
      // 
      this.lblEprcH2OFlatDays.Location = new System.Drawing.Point(890, 115);
      this.lblEprcH2OFlatDays.Name = "lblEprcH2OFlatDays";
      this.lblEprcH2OFlatDays.Size = new System.Drawing.Size(70, 15);
      this.lblEprcH2OFlatDays.TabIndex = 12;
      this.lblEprcH2OFlatDays.Text = "0.00";
      this.lblEprcH2OFlatDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcGasFlatDays
      // 
      this.lblEprcGasFlatDays.Location = new System.Drawing.Point(890, 81);
      this.lblEprcGasFlatDays.Name = "lblEprcGasFlatDays";
      this.lblEprcGasFlatDays.Size = new System.Drawing.Size(70, 15);
      this.lblEprcGasFlatDays.TabIndex = 12;
      this.lblEprcGasFlatDays.Text = "0.00";
      this.lblEprcGasFlatDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcOilFlatDays
      // 
      this.lblEprcOilFlatDays.Location = new System.Drawing.Point(890, 47);
      this.lblEprcOilFlatDays.Name = "lblEprcOilFlatDays";
      this.lblEprcOilFlatDays.Size = new System.Drawing.Size(70, 15);
      this.lblEprcOilFlatDays.TabIndex = 12;
      this.lblEprcOilFlatDays.Text = "0.00";
      this.lblEprcOilFlatDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesH2OFlatDays
      // 
      this.lblAriesH2OFlatDays.Location = new System.Drawing.Point(890, 100);
      this.lblAriesH2OFlatDays.Name = "lblAriesH2OFlatDays";
      this.lblAriesH2OFlatDays.Size = new System.Drawing.Size(70, 15);
      this.lblAriesH2OFlatDays.TabIndex = 12;
      this.lblAriesH2OFlatDays.Text = "0.00";
      this.lblAriesH2OFlatDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesGasFlatDays
      // 
      this.lblAriesGasFlatDays.Location = new System.Drawing.Point(890, 66);
      this.lblAriesGasFlatDays.Name = "lblAriesGasFlatDays";
      this.lblAriesGasFlatDays.Size = new System.Drawing.Size(70, 15);
      this.lblAriesGasFlatDays.TabIndex = 12;
      this.lblAriesGasFlatDays.Text = "0.00";
      this.lblAriesGasFlatDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesOilFlatDays
      // 
      this.lblAriesOilFlatDays.Location = new System.Drawing.Point(890, 32);
      this.lblAriesOilFlatDays.Name = "lblAriesOilFlatDays";
      this.lblAriesOilFlatDays.Size = new System.Drawing.Size(70, 15);
      this.lblAriesOilFlatDays.TabIndex = 12;
      this.lblAriesOilFlatDays.Text = "0.00";
      this.lblAriesOilFlatDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblFlatDays
      // 
      this.lblFlatDays.Location = new System.Drawing.Point(890, 13);
      this.lblFlatDays.Name = "lblFlatDays";
      this.lblFlatDays.Size = new System.Drawing.Size(70, 15);
      this.lblFlatDays.TabIndex = 12;
      this.lblFlatDays.Text = "Flat Days";
      this.lblFlatDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcH2ODelayDays
      // 
      this.lblEprcH2ODelayDays.Location = new System.Drawing.Point(815, 115);
      this.lblEprcH2ODelayDays.Name = "lblEprcH2ODelayDays";
      this.lblEprcH2ODelayDays.Size = new System.Drawing.Size(70, 15);
      this.lblEprcH2ODelayDays.TabIndex = 12;
      this.lblEprcH2ODelayDays.Text = "0.00";
      this.lblEprcH2ODelayDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcGasDelayDays
      // 
      this.lblEprcGasDelayDays.Location = new System.Drawing.Point(815, 81);
      this.lblEprcGasDelayDays.Name = "lblEprcGasDelayDays";
      this.lblEprcGasDelayDays.Size = new System.Drawing.Size(70, 15);
      this.lblEprcGasDelayDays.TabIndex = 12;
      this.lblEprcGasDelayDays.Text = "0.00";
      this.lblEprcGasDelayDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcOilDelayDays
      // 
      this.lblEprcOilDelayDays.Location = new System.Drawing.Point(815, 47);
      this.lblEprcOilDelayDays.Name = "lblEprcOilDelayDays";
      this.lblEprcOilDelayDays.Size = new System.Drawing.Size(70, 15);
      this.lblEprcOilDelayDays.TabIndex = 12;
      this.lblEprcOilDelayDays.Text = "0.00";
      this.lblEprcOilDelayDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesH2ODelayDays
      // 
      this.lblAriesH2ODelayDays.Location = new System.Drawing.Point(815, 100);
      this.lblAriesH2ODelayDays.Name = "lblAriesH2ODelayDays";
      this.lblAriesH2ODelayDays.Size = new System.Drawing.Size(70, 15);
      this.lblAriesH2ODelayDays.TabIndex = 12;
      this.lblAriesH2ODelayDays.Text = "0.00";
      this.lblAriesH2ODelayDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesGasDelayDays
      // 
      this.lblAriesGasDelayDays.Location = new System.Drawing.Point(815, 66);
      this.lblAriesGasDelayDays.Name = "lblAriesGasDelayDays";
      this.lblAriesGasDelayDays.Size = new System.Drawing.Size(70, 15);
      this.lblAriesGasDelayDays.TabIndex = 12;
      this.lblAriesGasDelayDays.Text = "0.00";
      this.lblAriesGasDelayDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesOilDelayDays
      // 
      this.lblAriesOilDelayDays.Location = new System.Drawing.Point(815, 32);
      this.lblAriesOilDelayDays.Name = "lblAriesOilDelayDays";
      this.lblAriesOilDelayDays.Size = new System.Drawing.Size(70, 15);
      this.lblAriesOilDelayDays.TabIndex = 12;
      this.lblAriesOilDelayDays.Text = "0.00";
      this.lblAriesOilDelayDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcH2OStartDate
      // 
      this.lblEprcH2OStartDate.Location = new System.Drawing.Point(667, 115);
      this.lblEprcH2OStartDate.Name = "lblEprcH2OStartDate";
      this.lblEprcH2OStartDate.Size = new System.Drawing.Size(70, 15);
      this.lblEprcH2OStartDate.TabIndex = 12;
      this.lblEprcH2OStartDate.Text = "1/1/2016";
      this.lblEprcH2OStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblEprcH2OInitialRate
      // 
      this.lblEprcH2OInitialRate.Location = new System.Drawing.Point(740, 115);
      this.lblEprcH2OInitialRate.Name = "lblEprcH2OInitialRate";
      this.lblEprcH2OInitialRate.Size = new System.Drawing.Size(70, 15);
      this.lblEprcH2OInitialRate.TabIndex = 12;
      this.lblEprcH2OInitialRate.Text = "0.00";
      this.lblEprcH2OInitialRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcGasStartDate
      // 
      this.lblEprcGasStartDate.Location = new System.Drawing.Point(667, 81);
      this.lblEprcGasStartDate.Name = "lblEprcGasStartDate";
      this.lblEprcGasStartDate.Size = new System.Drawing.Size(70, 15);
      this.lblEprcGasStartDate.TabIndex = 12;
      this.lblEprcGasStartDate.Text = "1/1/2016";
      this.lblEprcGasStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblEprcGasInitialRate
      // 
      this.lblEprcGasInitialRate.Location = new System.Drawing.Point(740, 81);
      this.lblEprcGasInitialRate.Name = "lblEprcGasInitialRate";
      this.lblEprcGasInitialRate.Size = new System.Drawing.Size(70, 15);
      this.lblEprcGasInitialRate.TabIndex = 12;
      this.lblEprcGasInitialRate.Text = "0.00";
      this.lblEprcGasInitialRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblEprcOilStartDate
      // 
      this.lblEprcOilStartDate.Location = new System.Drawing.Point(667, 47);
      this.lblEprcOilStartDate.Name = "lblEprcOilStartDate";
      this.lblEprcOilStartDate.Size = new System.Drawing.Size(70, 15);
      this.lblEprcOilStartDate.TabIndex = 12;
      this.lblEprcOilStartDate.Text = "1/1/2016";
      this.lblEprcOilStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblEprcOilInitialRate
      // 
      this.lblEprcOilInitialRate.Location = new System.Drawing.Point(740, 47);
      this.lblEprcOilInitialRate.Name = "lblEprcOilInitialRate";
      this.lblEprcOilInitialRate.Size = new System.Drawing.Size(70, 15);
      this.lblEprcOilInitialRate.TabIndex = 12;
      this.lblEprcOilInitialRate.Text = "0.00";
      this.lblEprcOilInitialRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesH2OStartDate
      // 
      this.lblAriesH2OStartDate.Location = new System.Drawing.Point(667, 100);
      this.lblAriesH2OStartDate.Name = "lblAriesH2OStartDate";
      this.lblAriesH2OStartDate.Size = new System.Drawing.Size(70, 15);
      this.lblAriesH2OStartDate.TabIndex = 12;
      this.lblAriesH2OStartDate.Text = "1/1/2016";
      this.lblAriesH2OStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblAriesH2OInitialRate
      // 
      this.lblAriesH2OInitialRate.Location = new System.Drawing.Point(740, 100);
      this.lblAriesH2OInitialRate.Name = "lblAriesH2OInitialRate";
      this.lblAriesH2OInitialRate.Size = new System.Drawing.Size(70, 15);
      this.lblAriesH2OInitialRate.TabIndex = 12;
      this.lblAriesH2OInitialRate.Text = "0.00";
      this.lblAriesH2OInitialRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblAriesGasStartDate
      // 
      this.lblAriesGasStartDate.Location = new System.Drawing.Point(667, 66);
      this.lblAriesGasStartDate.Name = "lblAriesGasStartDate";
      this.lblAriesGasStartDate.Size = new System.Drawing.Size(70, 15);
      this.lblAriesGasStartDate.TabIndex = 12;
      this.lblAriesGasStartDate.Text = "1/1/2016";
      this.lblAriesGasStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblAriesGasInitialRate
      // 
      this.lblAriesGasInitialRate.Location = new System.Drawing.Point(740, 66);
      this.lblAriesGasInitialRate.Name = "lblAriesGasInitialRate";
      this.lblAriesGasInitialRate.Size = new System.Drawing.Size(70, 15);
      this.lblAriesGasInitialRate.TabIndex = 12;
      this.lblAriesGasInitialRate.Text = "0.00";
      this.lblAriesGasInitialRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblDelayDays
      // 
      this.lblDelayDays.Location = new System.Drawing.Point(815, 13);
      this.lblDelayDays.Name = "lblDelayDays";
      this.lblDelayDays.Size = new System.Drawing.Size(70, 15);
      this.lblDelayDays.TabIndex = 12;
      this.lblDelayDays.Text = "Delay Days";
      this.lblDelayDays.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblH2OEprc
      // 
      this.lblH2OEprc.Location = new System.Drawing.Point(477, 116);
      this.lblH2OEprc.Name = "lblH2OEprc";
      this.lblH2OEprc.Size = new System.Drawing.Size(70, 15);
      this.lblH2OEprc.TabIndex = 12;
      this.lblH2OEprc.Text = "H2O - EPRC";
      // 
      // lblGasEprc
      // 
      this.lblGasEprc.Location = new System.Drawing.Point(477, 82);
      this.lblGasEprc.Name = "lblGasEprc";
      this.lblGasEprc.Size = new System.Drawing.Size(70, 15);
      this.lblGasEprc.TabIndex = 12;
      this.lblGasEprc.Text = "Gas - EPRC";
      // 
      // lblAriesOilStartDate
      // 
      this.lblAriesOilStartDate.Location = new System.Drawing.Point(667, 32);
      this.lblAriesOilStartDate.Name = "lblAriesOilStartDate";
      this.lblAriesOilStartDate.Size = new System.Drawing.Size(70, 15);
      this.lblAriesOilStartDate.TabIndex = 12;
      this.lblAriesOilStartDate.Text = "1/1/2016";
      this.lblAriesOilStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblAriesOilInitialRate
      // 
      this.lblAriesOilInitialRate.Location = new System.Drawing.Point(740, 32);
      this.lblAriesOilInitialRate.Name = "lblAriesOilInitialRate";
      this.lblAriesOilInitialRate.Size = new System.Drawing.Size(70, 15);
      this.lblAriesOilInitialRate.TabIndex = 12;
      this.lblAriesOilInitialRate.Text = "0.00";
      this.lblAriesOilInitialRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblOilEprc
      // 
      this.lblOilEprc.Location = new System.Drawing.Point(477, 48);
      this.lblOilEprc.Name = "lblOilEprc";
      this.lblOilEprc.Size = new System.Drawing.Size(70, 15);
      this.lblOilEprc.TabIndex = 12;
      this.lblOilEprc.Text = "Oil - EPRC";
      // 
      // lblH2OAries
      // 
      this.lblH2OAries.Location = new System.Drawing.Point(477, 101);
      this.lblH2OAries.Name = "lblH2OAries";
      this.lblH2OAries.Size = new System.Drawing.Size(70, 15);
      this.lblH2OAries.TabIndex = 12;
      this.lblH2OAries.Text = "H2O - Aries";
      // 
      // lblGasAries
      // 
      this.lblGasAries.Location = new System.Drawing.Point(477, 67);
      this.lblGasAries.Name = "lblGasAries";
      this.lblGasAries.Size = new System.Drawing.Size(70, 15);
      this.lblGasAries.TabIndex = 12;
      this.lblGasAries.Text = "Gas - Aries";
      // 
      // lblFcstActTp
      // 
      this.lblFcstActTp.Location = new System.Drawing.Point(552, 13);
      this.lblFcstActTp.Name = "lblFcstActTp";
      this.lblFcstActTp.Size = new System.Drawing.Size(126, 15);
      this.lblFcstActTp.TabIndex = 12;
      this.lblFcstActTp.Text = "Fcst / Act / Aries / TP";
      this.lblFcstActTp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblFcstStartDate
      // 
      this.lblFcstStartDate.Location = new System.Drawing.Point(667, 13);
      this.lblFcstStartDate.Name = "lblFcstStartDate";
      this.lblFcstStartDate.Size = new System.Drawing.Size(70, 15);
      this.lblFcstStartDate.TabIndex = 12;
      this.lblFcstStartDate.Text = "StartDate";
      this.lblFcstStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblInitialRate
      // 
      this.lblInitialRate.Location = new System.Drawing.Point(740, 13);
      this.lblInitialRate.Name = "lblInitialRate";
      this.lblInitialRate.Size = new System.Drawing.Size(70, 15);
      this.lblInitialRate.TabIndex = 12;
      this.lblInitialRate.Text = "Initial Rate";
      this.lblInitialRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblOilAries
      // 
      this.lblOilAries.Location = new System.Drawing.Point(477, 33);
      this.lblOilAries.Name = "lblOilAries";
      this.lblOilAries.Size = new System.Drawing.Size(70, 15);
      this.lblOilAries.TabIndex = 12;
      this.lblOilAries.Text = "Oil - Aries";
      // 
      // cboTimeUnit
      // 
      this.cboTimeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTimeUnit.Enabled = false;
      this.cboTimeUnit.FormattingEnabled = true;
      this.cboTimeUnit.Items.AddRange(new object[] {
            "Days",
            "Weeks",
            "Months",
            "Quarters",
            "Years"});
      this.cboTimeUnit.Location = new System.Drawing.Point(148, 97);
      this.cboTimeUnit.Name = "cboTimeUnit";
      this.cboTimeUnit.Size = new System.Drawing.Size(121, 21);
      this.cboTimeUnit.TabIndex = 11;
      // 
      // btnZoomIn
      // 
      this.btnZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnZoomIn.Location = new System.Drawing.Point(1329, 55);
      this.btnZoomIn.Name = "btnZoomIn";
      this.btnZoomIn.Size = new System.Drawing.Size(70, 23);
      this.btnZoomIn.TabIndex = 10;
      this.btnZoomIn.Tag = "ZoomIn";
      this.btnZoomIn.Text = "Zoom In";
      this.btnZoomIn.UseVisualStyleBackColor = true;
      this.btnZoomIn.Click += new System.EventHandler(this.Action);
      // 
      // cboEnvironment
      // 
      this.cboEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cboEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboEnvironment.FormattingEnabled = true;
      this.cboEnvironment.Location = new System.Drawing.Point(1329, 28);
      this.cboEnvironment.Name = "cboEnvironment";
      this.cboEnvironment.Size = new System.Drawing.Size(167, 21);
      this.cboEnvironment.TabIndex = 8;
      this.cboEnvironment.SelectedIndexChanged += new System.EventHandler(this.cboEnvironment_SelectedIndexChanged);
      // 
      // lblEnvironment
      // 
      this.lblEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(1326, 14);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(66, 13);
      this.lblEnvironment.TabIndex = 9;
      this.lblEnvironment.Text = "Environment";
      // 
      // ckEprcH2OActuals
      // 
      this.ckEprcH2OActuals.AutoSize = true;
      this.ckEprcH2OActuals.Location = new System.Drawing.Point(586, 115);
      this.ckEprcH2OActuals.Name = "ckEprcH2OActuals";
      this.ckEprcH2OActuals.Size = new System.Drawing.Size(15, 14);
      this.ckEprcH2OActuals.TabIndex = 7;
      this.ckEprcH2OActuals.Tag = "EprcH2OActuals";
      this.ckEprcH2OActuals.UseVisualStyleBackColor = true;
      // 
      // ckAriesH2OActuals
      // 
      this.ckAriesH2OActuals.AutoSize = true;
      this.ckAriesH2OActuals.Location = new System.Drawing.Point(586, 100);
      this.ckAriesH2OActuals.Name = "ckAriesH2OActuals";
      this.ckAriesH2OActuals.Size = new System.Drawing.Size(15, 14);
      this.ckAriesH2OActuals.TabIndex = 7;
      this.ckAriesH2OActuals.Tag = "AriesH2OActuals";
      this.ckAriesH2OActuals.UseVisualStyleBackColor = true;
      // 
      // ckEprcH2OForecast
      // 
      this.ckEprcH2OForecast.AutoSize = true;
      this.ckEprcH2OForecast.Location = new System.Drawing.Point(560, 115);
      this.ckEprcH2OForecast.Name = "ckEprcH2OForecast";
      this.ckEprcH2OForecast.Size = new System.Drawing.Size(15, 14);
      this.ckEprcH2OForecast.TabIndex = 7;
      this.ckEprcH2OForecast.Tag = "EprcH2OForecast";
      this.ckEprcH2OForecast.UseVisualStyleBackColor = true;
      // 
      // ckTubingPressure
      // 
      this.ckTubingPressure.AutoSize = true;
      this.ckTubingPressure.Checked = true;
      this.ckTubingPressure.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckTubingPressure.Location = new System.Drawing.Point(647, 81);
      this.ckTubingPressure.Name = "ckTubingPressure";
      this.ckTubingPressure.Size = new System.Drawing.Size(15, 14);
      this.ckTubingPressure.TabIndex = 7;
      this.ckTubingPressure.Tag = "TubingPressureActuals";
      this.ckTubingPressure.UseVisualStyleBackColor = true;
      // 
      // ckEprcGasActuals
      // 
      this.ckEprcGasActuals.AutoSize = true;
      this.ckEprcGasActuals.Checked = true;
      this.ckEprcGasActuals.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckEprcGasActuals.Location = new System.Drawing.Point(586, 81);
      this.ckEprcGasActuals.Name = "ckEprcGasActuals";
      this.ckEprcGasActuals.Size = new System.Drawing.Size(15, 14);
      this.ckEprcGasActuals.TabIndex = 7;
      this.ckEprcGasActuals.Tag = "EprcGasActuals";
      this.ckEprcGasActuals.UseVisualStyleBackColor = true;
      // 
      // ckAriesH2OForecast
      // 
      this.ckAriesH2OForecast.AutoSize = true;
      this.ckAriesH2OForecast.Location = new System.Drawing.Point(560, 100);
      this.ckAriesH2OForecast.Name = "ckAriesH2OForecast";
      this.ckAriesH2OForecast.Size = new System.Drawing.Size(15, 14);
      this.ckAriesH2OForecast.TabIndex = 7;
      this.ckAriesH2OForecast.Tag = "AriesH2OForecast";
      this.ckAriesH2OForecast.UseVisualStyleBackColor = true;
      // 
      // ckAriesGasActuals
      // 
      this.ckAriesGasActuals.AutoSize = true;
      this.ckAriesGasActuals.Checked = true;
      this.ckAriesGasActuals.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckAriesGasActuals.Location = new System.Drawing.Point(586, 66);
      this.ckAriesGasActuals.Name = "ckAriesGasActuals";
      this.ckAriesGasActuals.Size = new System.Drawing.Size(15, 14);
      this.ckAriesGasActuals.TabIndex = 7;
      this.ckAriesGasActuals.Tag = "AriesGasActuals";
      this.ckAriesGasActuals.UseVisualStyleBackColor = true;
      // 
      // ckEprcGasForecast
      // 
      this.ckEprcGasForecast.AutoSize = true;
      this.ckEprcGasForecast.Checked = true;
      this.ckEprcGasForecast.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckEprcGasForecast.Location = new System.Drawing.Point(560, 81);
      this.ckEprcGasForecast.Name = "ckEprcGasForecast";
      this.ckEprcGasForecast.Size = new System.Drawing.Size(15, 14);
      this.ckEprcGasForecast.TabIndex = 7;
      this.ckEprcGasForecast.Tag = "EprcGasForecast";
      this.ckEprcGasForecast.UseVisualStyleBackColor = true;
      // 
      // ckAriesGasForecast
      // 
      this.ckAriesGasForecast.AutoSize = true;
      this.ckAriesGasForecast.Checked = true;
      this.ckAriesGasForecast.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckAriesGasForecast.Location = new System.Drawing.Point(560, 66);
      this.ckAriesGasForecast.Name = "ckAriesGasForecast";
      this.ckAriesGasForecast.Size = new System.Drawing.Size(15, 14);
      this.ckAriesGasForecast.TabIndex = 7;
      this.ckAriesGasForecast.Tag = "AriesGasForecast";
      this.ckAriesGasForecast.UseVisualStyleBackColor = true;
      // 
      // ckEprcOilActuals
      // 
      this.ckEprcOilActuals.AutoSize = true;
      this.ckEprcOilActuals.Checked = true;
      this.ckEprcOilActuals.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckEprcOilActuals.Location = new System.Drawing.Point(586, 47);
      this.ckEprcOilActuals.Name = "ckEprcOilActuals";
      this.ckEprcOilActuals.Size = new System.Drawing.Size(15, 14);
      this.ckEprcOilActuals.TabIndex = 7;
      this.ckEprcOilActuals.Tag = "EprcOilActuals";
      this.ckEprcOilActuals.UseVisualStyleBackColor = true;
      // 
      // ckProducingWellsOnly
      // 
      this.ckProducingWellsOnly.AutoSize = true;
      this.ckProducingWellsOnly.Checked = true;
      this.ckProducingWellsOnly.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckProducingWellsOnly.Location = new System.Drawing.Point(23, 57);
      this.ckProducingWellsOnly.Name = "ckProducingWellsOnly";
      this.ckProducingWellsOnly.Size = new System.Drawing.Size(98, 17);
      this.ckProducingWellsOnly.TabIndex = 7;
      this.ckProducingWellsOnly.Tag = "";
      this.ckProducingWellsOnly.Text = "Producing Only";
      this.ckProducingWellsOnly.UseVisualStyleBackColor = true;
      this.ckProducingWellsOnly.CheckedChanged += new System.EventHandler(this.ckProducingWellsOnly_CheckedChanged);
      // 
      // ckExcludeDelayDays
      // 
      this.ckExcludeDelayDays.AutoSize = true;
      this.ckExcludeDelayDays.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ckExcludeDelayDays.Checked = true;
      this.ckExcludeDelayDays.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckExcludeDelayDays.Location = new System.Drawing.Point(148, 57);
      this.ckExcludeDelayDays.Name = "ckExcludeDelayDays";
      this.ckExcludeDelayDays.Size = new System.Drawing.Size(121, 17);
      this.ckExcludeDelayDays.TabIndex = 7;
      this.ckExcludeDelayDays.Tag = "";
      this.ckExcludeDelayDays.Text = "Exclude Delay Days";
      this.ckExcludeDelayDays.UseVisualStyleBackColor = true;
      // 
      // ckAriesOilActuals
      // 
      this.ckAriesOilActuals.AutoSize = true;
      this.ckAriesOilActuals.Checked = true;
      this.ckAriesOilActuals.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckAriesOilActuals.Location = new System.Drawing.Point(586, 32);
      this.ckAriesOilActuals.Name = "ckAriesOilActuals";
      this.ckAriesOilActuals.Size = new System.Drawing.Size(15, 14);
      this.ckAriesOilActuals.TabIndex = 7;
      this.ckAriesOilActuals.Tag = "AriesOilActuals";
      this.ckAriesOilActuals.UseVisualStyleBackColor = true;
      // 
      // ckEprcOilForecast
      // 
      this.ckEprcOilForecast.AutoSize = true;
      this.ckEprcOilForecast.Checked = true;
      this.ckEprcOilForecast.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckEprcOilForecast.Location = new System.Drawing.Point(560, 47);
      this.ckEprcOilForecast.Name = "ckEprcOilForecast";
      this.ckEprcOilForecast.Size = new System.Drawing.Size(15, 14);
      this.ckEprcOilForecast.TabIndex = 7;
      this.ckEprcOilForecast.Tag = "EprcOilForecast";
      this.ckEprcOilForecast.UseVisualStyleBackColor = true;
      // 
      // ckAriesOilForecast
      // 
      this.ckAriesOilForecast.AutoSize = true;
      this.ckAriesOilForecast.Checked = true;
      this.ckAriesOilForecast.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckAriesOilForecast.Location = new System.Drawing.Point(560, 32);
      this.ckAriesOilForecast.Name = "ckAriesOilForecast";
      this.ckAriesOilForecast.Size = new System.Drawing.Size(15, 14);
      this.ckAriesOilForecast.TabIndex = 7;
      this.ckAriesOilForecast.Tag = "AriesOilForecast";
      this.ckAriesOilForecast.UseVisualStyleBackColor = true;
      // 
      // lblEndDate
      // 
      this.lblEndDate.AutoSize = true;
      this.lblEndDate.Location = new System.Drawing.Point(1136, 57);
      this.lblEndDate.Name = "lblEndDate";
      this.lblEndDate.Size = new System.Drawing.Size(52, 13);
      this.lblEndDate.TabIndex = 3;
      this.lblEndDate.Text = "End Date";
      // 
      // lblStartDate
      // 
      this.lblStartDate.AutoSize = true;
      this.lblStartDate.Location = new System.Drawing.Point(1136, 14);
      this.lblStartDate.Name = "lblStartDate";
      this.lblStartDate.Size = new System.Drawing.Size(55, 13);
      this.lblStartDate.TabIndex = 4;
      this.lblStartDate.Text = "Start Date";
      // 
      // dtpEndDate
      // 
      this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dtpEndDate.Location = new System.Drawing.Point(1139, 73);
      this.dtpEndDate.Name = "dtpEndDate";
      this.dtpEndDate.Size = new System.Drawing.Size(100, 20);
      this.dtpEndDate.TabIndex = 5;
      this.dtpEndDate.Value = new System.DateTime(2021, 12, 31, 0, 0, 0, 0);
      // 
      // dtpStartDate
      // 
      this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dtpStartDate.Location = new System.Drawing.Point(1139, 30);
      this.dtpStartDate.Name = "dtpStartDate";
      this.dtpStartDate.Size = new System.Drawing.Size(100, 20);
      this.dtpStartDate.TabIndex = 6;
      this.dtpStartDate.Value = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
      // 
      // lblGPWellIDValue
      // 
      this.lblGPWellIDValue.Location = new System.Drawing.Point(377, 80);
      this.lblGPWellIDValue.Name = "lblGPWellIDValue";
      this.lblGPWellIDValue.Size = new System.Drawing.Size(60, 13);
      this.lblGPWellIDValue.TabIndex = 2;
      this.lblGPWellIDValue.Text = "00000";
      this.lblGPWellIDValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblSSIWellIDValue
      // 
      this.lblSSIWellIDValue.Location = new System.Drawing.Point(377, 66);
      this.lblSSIWellIDValue.Name = "lblSSIWellIDValue";
      this.lblSSIWellIDValue.Size = new System.Drawing.Size(60, 13);
      this.lblSSIWellIDValue.TabIndex = 2;
      this.lblSSIWellIDValue.Text = "00000";
      this.lblSSIWellIDValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblGISWellIDValue
      // 
      this.lblGISWellIDValue.Location = new System.Drawing.Point(377, 48);
      this.lblGISWellIDValue.Name = "lblGISWellIDValue";
      this.lblGISWellIDValue.Size = new System.Drawing.Size(60, 13);
      this.lblGISWellIDValue.TabIndex = 2;
      this.lblGISWellIDValue.Text = "00000";
      this.lblGISWellIDValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblPRCWellIDValue
      // 
      this.lblPRCWellIDValue.Location = new System.Drawing.Point(377, 33);
      this.lblPRCWellIDValue.Name = "lblPRCWellIDValue";
      this.lblPRCWellIDValue.Size = new System.Drawing.Size(60, 13);
      this.lblPRCWellIDValue.TabIndex = 2;
      this.lblPRCWellIDValue.Text = "00000";
      this.lblPRCWellIDValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblGPWellID
      // 
      this.lblGPWellID.AutoSize = true;
      this.lblGPWellID.Location = new System.Drawing.Point(304, 80);
      this.lblGPWellID.Name = "lblGPWellID";
      this.lblGPWellID.Size = new System.Drawing.Size(60, 13);
      this.lblGPWellID.TabIndex = 2;
      this.lblGPWellID.Text = "GP Well ID";
      // 
      // lblSSIWellID
      // 
      this.lblSSIWellID.AutoSize = true;
      this.lblSSIWellID.Location = new System.Drawing.Point(304, 66);
      this.lblSSIWellID.Name = "lblSSIWellID";
      this.lblSSIWellID.Size = new System.Drawing.Size(62, 13);
      this.lblSSIWellID.TabIndex = 2;
      this.lblSSIWellID.Text = "SSI Well ID";
      // 
      // lblGISWellID
      // 
      this.lblGISWellID.AutoSize = true;
      this.lblGISWellID.Location = new System.Drawing.Point(304, 48);
      this.lblGISWellID.Name = "lblGISWellID";
      this.lblGISWellID.Size = new System.Drawing.Size(63, 13);
      this.lblGISWellID.TabIndex = 2;
      this.lblGISWellID.Text = "GIS Well ID";
      // 
      // lblPRCWellID
      // 
      this.lblPRCWellID.AutoSize = true;
      this.lblPRCWellID.Location = new System.Drawing.Point(304, 33);
      this.lblPRCWellID.Name = "lblPRCWellID";
      this.lblPRCWellID.Size = new System.Drawing.Size(67, 13);
      this.lblPRCWellID.TabIndex = 2;
      this.lblPRCWellID.Text = "PRC Well ID";
      // 
      // lblTimeUnit
      // 
      this.lblTimeUnit.AutoSize = true;
      this.lblTimeUnit.Location = new System.Drawing.Point(145, 83);
      this.lblTimeUnit.Name = "lblTimeUnit";
      this.lblTimeUnit.Size = new System.Drawing.Size(52, 13);
      this.lblTimeUnit.TabIndex = 2;
      this.lblTimeUnit.Text = "Time Unit";
      // 
      // lblWell
      // 
      this.lblWell.AutoSize = true;
      this.lblWell.Location = new System.Drawing.Point(20, 14);
      this.lblWell.Name = "lblWell";
      this.lblWell.Size = new System.Drawing.Size(28, 13);
      this.lblWell.TabIndex = 2;
      this.lblWell.Text = "Well";
      // 
      // cboWell
      // 
      this.cboWell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboWell.FormattingEnabled = true;
      this.cboWell.Location = new System.Drawing.Point(23, 30);
      this.cboWell.Name = "cboWell";
      this.cboWell.Size = new System.Drawing.Size(246, 21);
      this.cboWell.TabIndex = 1;
      this.cboWell.SelectedIndexChanged += new System.EventHandler(this.cboWell_SelectedIndexChanged);
      // 
      // btnRunForecast
      // 
      this.btnRunForecast.Location = new System.Drawing.Point(23, 97);
      this.btnRunForecast.Name = "btnRunForecast";
      this.btnRunForecast.Size = new System.Drawing.Size(111, 23);
      this.btnRunForecast.TabIndex = 0;
      this.btnRunForecast.Tag = "RunForecast";
      this.btnRunForecast.Text = "Run Forecast";
      this.btnRunForecast.UseVisualStyleBackColor = true;
      this.btnRunForecast.Click += new System.EventHandler(this.Action);
      // 
      // pnlMain
      // 
      this.pnlMain.AutoScroll = true;
      this.pnlMain.BackColor = System.Drawing.Color.White;
      this.pnlMain.Controls.Add(this.pnlChart);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 171);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1508, 589);
      this.pnlMain.TabIndex = 3;
      // 
      // pnlChart
      // 
      this.pnlChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlChart.Controls.Add(this.chartMain);
      this.pnlChart.Location = new System.Drawing.Point(0, 0);
      this.pnlChart.Name = "pnlChart";
      this.pnlChart.Size = new System.Drawing.Size(1239, 591);
      this.pnlChart.TabIndex = 1;
      // 
      // chartMain
      // 
      chartArea1.Name = "chartAreaMain";
      this.chartMain.ChartAreas.Add(chartArea1);
      this.chartMain.ContextMenuStrip = this.ctxChart;
      this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
      legend1.Alignment = System.Drawing.StringAlignment.Far;
      legend1.BackColor = System.Drawing.Color.White;
      legend1.BorderColor = System.Drawing.Color.DarkGray;
      legend1.DockedToChartArea = "chartAreaMain";
      legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
      legend1.Name = "Legend1";
      this.chartMain.Legends.Add(legend1);
      this.chartMain.Location = new System.Drawing.Point(0, 0);
      this.chartMain.Name = "chartMain";
      series1.ChartArea = "chartAreaMain";
      series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series1.Legend = "Legend1";
      series1.Name = "Series1";
      this.chartMain.Series.Add(series1);
      this.chartMain.Size = new System.Drawing.Size(1239, 591);
      this.chartMain.TabIndex = 0;
      this.chartMain.Text = "Main Chart";
      // 
      // ctxChart
      // 
      this.ctxChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxChartShowData});
      this.ctxChart.Name = "contextMenuStrip1";
      this.ctxChart.Size = new System.Drawing.Size(131, 26);
      // 
      // ctxChartShowData
      // 
      this.ctxChartShowData.Name = "ctxChartShowData";
      this.ctxChartShowData.Size = new System.Drawing.Size(130, 22);
      this.ctxChartShowData.Tag = "ShowData";
      this.ctxChartShowData.Text = "Show Data";
      this.ctxChartShowData.Click += new System.EventHandler(this.Action);
      // 
      // ckAriesSystemOilFcst
      // 
      this.ckAriesSystemOilFcst.AutoSize = true;
      this.ckAriesSystemOilFcst.Checked = true;
      this.ckAriesSystemOilFcst.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckAriesSystemOilFcst.Location = new System.Drawing.Point(618, 32);
      this.ckAriesSystemOilFcst.Name = "ckAriesSystemOilFcst";
      this.ckAriesSystemOilFcst.Size = new System.Drawing.Size(15, 14);
      this.ckAriesSystemOilFcst.TabIndex = 7;
      this.ckAriesSystemOilFcst.Tag = "AriesSysOilForecast";
      this.ckAriesSystemOilFcst.UseVisualStyleBackColor = true;
      // 
      // ckAriesSystemGasFcst
      // 
      this.ckAriesSystemGasFcst.AutoSize = true;
      this.ckAriesSystemGasFcst.Checked = true;
      this.ckAriesSystemGasFcst.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckAriesSystemGasFcst.Location = new System.Drawing.Point(618, 67);
      this.ckAriesSystemGasFcst.Name = "ckAriesSystemGasFcst";
      this.ckAriesSystemGasFcst.Size = new System.Drawing.Size(15, 14);
      this.ckAriesSystemGasFcst.TabIndex = 7;
      this.ckAriesSystemGasFcst.Tag = "AriesSysGasForecast";
      this.ckAriesSystemGasFcst.UseVisualStyleBackColor = true;
      // 
      // ckAriesSystemH2OFcst
      // 
      this.ckAriesSystemH2OFcst.AutoSize = true;
      this.ckAriesSystemH2OFcst.Checked = true;
      this.ckAriesSystemH2OFcst.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckAriesSystemH2OFcst.Location = new System.Drawing.Point(618, 100);
      this.ckAriesSystemH2OFcst.Name = "ckAriesSystemH2OFcst";
      this.ckAriesSystemH2OFcst.Size = new System.Drawing.Size(15, 14);
      this.ckAriesSystemH2OFcst.TabIndex = 7;
      this.ckAriesSystemH2OFcst.Tag = "AriesSysH2OForecast";
      this.ckAriesSystemH2OFcst.UseVisualStyleBackColor = true;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1508, 783);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlTopControl);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Gulfport - Forecast Viewer 1.0.1";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.ResizeEnd += new System.EventHandler(this.frmMain_ResizeEnd);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTopControl.ResumeLayout(false);
      this.pnlTopControl.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      this.pnlChart.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
      this.ctxChart.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTopControl;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.DataVisualization.Charting.Chart chartMain;
    private System.Windows.Forms.Button btnRunForecast;
    private System.Windows.Forms.CheckBox ckAriesGasForecast;
    private System.Windows.Forms.CheckBox ckAriesOilForecast;
    private System.Windows.Forms.Label lblEndDate;
    private System.Windows.Forms.Label lblStartDate;
    private System.Windows.Forms.DateTimePicker dtpEndDate;
    private System.Windows.Forms.DateTimePicker dtpStartDate;
    private System.Windows.Forms.Label lblWell;
    private System.Windows.Forms.ComboBox cboWell;
    private System.Windows.Forms.ComboBox cboEnvironment;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.CheckBox ckAriesH2OForecast;
    private System.Windows.Forms.CheckBox ckEprcH2OForecast;
    private System.Windows.Forms.CheckBox ckEprcGasForecast;
    private System.Windows.Forms.CheckBox ckEprcOilForecast;
    private System.Windows.Forms.Button btnZoomIn;
    private System.Windows.Forms.Panel pnlChart;
    private System.Windows.Forms.CheckBox ckExcludeDelayDays;
    private System.Windows.Forms.ComboBox cboTimeUnit;
    private System.Windows.Forms.Label lblTimeUnit;
    private System.Windows.Forms.Label lblEprcGasYr1NomDec;
    private System.Windows.Forms.Label lblEprcOilYr1NomDec;
    private System.Windows.Forms.Label lblAriesGasYr1NomDec;
    private System.Windows.Forms.Label lblAriesOilYr1NomDec;
    private System.Windows.Forms.Label lblYr1NomDec;
    private System.Windows.Forms.Label lblEprcGasHypExp;
    private System.Windows.Forms.Label lblEprcOilHypExp;
    private System.Windows.Forms.Label lblAriesGasHypExp;
    private System.Windows.Forms.Label lblAriesOilHypExp;
    private System.Windows.Forms.Label lblHypExp;
    private System.Windows.Forms.Label lblEprcGasFlatDays;
    private System.Windows.Forms.Label lblEprcOilFlatDays;
    private System.Windows.Forms.Label lblAriesGasFlatDays;
    private System.Windows.Forms.Label lblAriesOilFlatDays;
    private System.Windows.Forms.Label lblFlatDays;
    private System.Windows.Forms.Label lblEprcGasDelayDays;
    private System.Windows.Forms.Label lblEprcOilDelayDays;
    private System.Windows.Forms.Label lblAriesGasDelayDays;
    private System.Windows.Forms.Label lblAriesOilDelayDays;
    private System.Windows.Forms.Label lblEprcGasInitialRate;
    private System.Windows.Forms.Label lblEprcOilInitialRate;
    private System.Windows.Forms.Label lblAriesGasInitialRate;
    private System.Windows.Forms.Label lblDelayDays;
    private System.Windows.Forms.Label lblGasEprc;
    private System.Windows.Forms.Label lblAriesOilInitialRate;
    private System.Windows.Forms.Label lblOilEprc;
    private System.Windows.Forms.Label lblGasAries;
    private System.Windows.Forms.Label lblInitialRate;
    private System.Windows.Forms.Label lblOilAries;
    private System.Windows.Forms.Label lblEprcH2OYr1NomDec;
    private System.Windows.Forms.Label lblAriesH2OYr1NomDec;
    private System.Windows.Forms.Label lblEprcH2OHypExp;
    private System.Windows.Forms.Label lblAriesH2OHypExp;
    private System.Windows.Forms.Label lblEprcH2OFlatDays;
    private System.Windows.Forms.Label lblAriesH2OFlatDays;
    private System.Windows.Forms.Label lblEprcH2ODelayDays;
    private System.Windows.Forms.Label lblAriesH2ODelayDays;
    private System.Windows.Forms.Label lblEprcH2OInitialRate;
    private System.Windows.Forms.Label lblAriesH2OInitialRate;
    private System.Windows.Forms.Label lblH2OEprc;
    private System.Windows.Forms.Label lblH2OAries;
    private System.Windows.Forms.Label lblEprcH2OStartDate;
    private System.Windows.Forms.Label lblEprcGasStartDate;
    private System.Windows.Forms.Label lblEprcOilStartDate;
    private System.Windows.Forms.Label lblAriesH2OStartDate;
    private System.Windows.Forms.Label lblAriesGasStartDate;
    private System.Windows.Forms.Label lblAriesOilStartDate;
    private System.Windows.Forms.Label lblFcstStartDate;
    private System.Windows.Forms.ContextMenuStrip ctxChart;
    private System.Windows.Forms.ToolStripMenuItem ctxChartShowData;
    private System.Windows.Forms.Label lblFcstActTp;
    private System.Windows.Forms.CheckBox ckEprcH2OActuals;
    private System.Windows.Forms.CheckBox ckAriesH2OActuals;
    private System.Windows.Forms.CheckBox ckEprcGasActuals;
    private System.Windows.Forms.CheckBox ckAriesGasActuals;
    private System.Windows.Forms.CheckBox ckEprcOilActuals;
    private System.Windows.Forms.CheckBox ckAriesOilActuals;
    private System.Windows.Forms.Label lblPRCWellIDValue;
    private System.Windows.Forms.Label lblPRCWellID;
    private System.Windows.Forms.Label lblGPWellIDValue;
    private System.Windows.Forms.Label lblSSIWellIDValue;
    private System.Windows.Forms.Label lblGISWellIDValue;
    private System.Windows.Forms.Label lblGPWellID;
    private System.Windows.Forms.Label lblSSIWellID;
    private System.Windows.Forms.Label lblGISWellID;
    private System.Windows.Forms.CheckBox ckTubingPressure;
    private System.Windows.Forms.CheckBox ckProducingWellsOnly;
    private System.Windows.Forms.ToolStripMenuItem mnuView;
    private System.Windows.Forms.ToolStripMenuItem mnuViewAriesVariables;
    private System.Windows.Forms.CheckBox ckAriesSystemH2OFcst;
    private System.Windows.Forms.CheckBox ckAriesSystemGasFcst;
    private System.Windows.Forms.CheckBox ckAriesSystemOilFcst;
  }
}


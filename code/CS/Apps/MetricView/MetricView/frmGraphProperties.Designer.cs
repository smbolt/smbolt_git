namespace Teleflora.Operations.MetricView
{
  partial class frmGraphProperties
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
      this.btnUpdate = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.gbGraphInfo = new System.Windows.Forms.GroupBox();
      this.txtRefreshInterval = new System.Windows.Forms.TextBox();
      this.ckUseEsqlmgmt = new System.Windows.Forms.CheckBox();
      this.ckActive = new System.Windows.Forms.CheckBox();
      this.txtGraphName = new System.Windows.Forms.TextBox();
      this.lblRefreshInterval = new System.Windows.Forms.Label();
      this.lblGraphName = new System.Windows.Forms.Label();
      this.lvAvailableMetrics = new System.Windows.Forms.ListView();
      this.mnuContextListViews = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuContextListViewMetricProperties = new System.Windows.Forms.ToolStripMenuItem();
      this.lblFromDate = new System.Windows.Forms.Label();
      this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
      this.gbAvailableMetrics = new System.Windows.Forms.GroupBox();
      this.gbDivider = new System.Windows.Forms.GroupBox();
      this.cboListFilterByMetric = new System.Windows.Forms.ComboBox();
      this.cboListFilterObsvSvr = new System.Windows.Forms.ComboBox();
      this.cboTargetApplication = new System.Windows.Forms.ComboBox();
      this.cboEnvironment = new System.Windows.Forms.ComboBox();
      this.cboTargetSystem = new System.Windows.Forms.ComboBox();
      this.btnAddToGraph = new System.Windows.Forms.Button();
      this.btnGetMetrics = new System.Windows.Forms.Button();
      this.lblListFilterByObsvSvr = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.lblQueryFilters = new System.Windows.Forms.Label();
      this.lblListFilter = new System.Windows.Forms.Label();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.lblApplication = new System.Windows.Forms.Label();
      this.lblTargetSystem = new System.Windows.Forms.Label();
      this.optionLatestMetrics = new System.Windows.Forms.RadioButton();
      this.optionFilterByDate = new System.Windows.Forms.RadioButton();
      this.lblToDate = new System.Windows.Forms.Label();
      this.dtpToDate = new System.Windows.Forms.DateTimePicker();
      this.gbIncludedMetrics = new System.Windows.Forms.GroupBox();
      this.lvIncludedMetrics = new System.Windows.Forms.ListView();
      this.btnRemoveAllFromGraph = new System.Windows.Forms.Button();
      this.btnRemoveFromGraph = new System.Windows.Forms.Button();
      this.cboDataPoints = new System.Windows.Forms.ComboBox();
      this.lblDataPoints = new System.Windows.Forms.Label();
      this.gbDataPoints = new System.Windows.Forms.GroupBox();
      this.mnuContextListViewRollUp = new System.Windows.Forms.ToolStripMenuItem();
      this.gbGraphInfo.SuspendLayout();
      this.mnuContextListViews.SuspendLayout();
      this.gbAvailableMetrics.SuspendLayout();
      this.gbIncludedMetrics.SuspendLayout();
      this.gbDataPoints.SuspendLayout();
      this.SuspendLayout();
      //
      // btnUpdate
      //
      this.btnUpdate.Location = new System.Drawing.Point(30, 691);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(152, 25);
      this.btnUpdate.TabIndex = 33;
      this.btnUpdate.Text = "Update";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(769, 691);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(152, 25);
      this.btnCancel.TabIndex = 34;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      //
      // gbGraphInfo
      //
      this.gbGraphInfo.Controls.Add(this.txtRefreshInterval);
      this.gbGraphInfo.Controls.Add(this.ckUseEsqlmgmt);
      this.gbGraphInfo.Controls.Add(this.ckActive);
      this.gbGraphInfo.Controls.Add(this.txtGraphName);
      this.gbGraphInfo.Controls.Add(this.lblRefreshInterval);
      this.gbGraphInfo.Controls.Add(this.lblGraphName);
      this.gbGraphInfo.Location = new System.Drawing.Point(12, 12);
      this.gbGraphInfo.Name = "gbGraphInfo";
      this.gbGraphInfo.Size = new System.Drawing.Size(253, 138);
      this.gbGraphInfo.TabIndex = 0;
      this.gbGraphInfo.TabStop = false;
      this.gbGraphInfo.Text = "General Graph Properties";
      //
      // txtRefreshInterval
      //
      this.txtRefreshInterval.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtRefreshInterval.Location = new System.Drawing.Point(18, 82);
      this.txtRefreshInterval.Name = "txtRefreshInterval";
      this.txtRefreshInterval.Size = new System.Drawing.Size(73, 21);
      this.txtRefreshInterval.TabIndex = 2;
      this.txtRefreshInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtRefreshInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRefreshInterval_KeyPress);
      //
      // ckUseEsqlmgmt
      //
      this.ckUseEsqlmgmt.AutoSize = true;
      this.ckUseEsqlmgmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ckUseEsqlmgmt.Location = new System.Drawing.Point(148, 109);
      this.ckUseEsqlmgmt.Name = "ckUseEsqlmgmt";
      this.ckUseEsqlmgmt.Size = new System.Drawing.Size(101, 17);
      this.ckUseEsqlmgmt.TabIndex = 4;
      this.ckUseEsqlmgmt.Text = "Use ESQLMGMT";
      this.ckUseEsqlmgmt.UseVisualStyleBackColor = true;
      this.ckUseEsqlmgmt.CheckedChanged += new System.EventHandler(this.ckUseEsqlmgmt_CheckedChanged);
      //
      // ckActive
      //
      this.ckActive.AutoSize = true;
      this.ckActive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ckActive.Location = new System.Drawing.Point(148, 86);
      this.ckActive.Name = "ckActive";
      this.ckActive.Size = new System.Drawing.Size(56, 17);
      this.ckActive.TabIndex = 3;
      this.ckActive.Text = "Active";
      this.ckActive.UseVisualStyleBackColor = true;
      //
      // txtGraphName
      //
      this.txtGraphName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtGraphName.Location = new System.Drawing.Point(18, 36);
      this.txtGraphName.Name = "txtGraphName";
      this.txtGraphName.Size = new System.Drawing.Size(182, 21);
      this.txtGraphName.TabIndex = 1;
      //
      // lblRefreshInterval
      //
      this.lblRefreshInterval.AutoSize = true;
      this.lblRefreshInterval.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblRefreshInterval.Location = new System.Drawing.Point(15, 66);
      this.lblRefreshInterval.Name = "lblRefreshInterval";
      this.lblRefreshInterval.Size = new System.Drawing.Size(156, 13);
      this.lblRefreshInterval.TabIndex = 0;
      this.lblRefreshInterval.Text = "Refresh Interval (milliseconds):";
      //
      // lblGraphName
      //
      this.lblGraphName.AutoSize = true;
      this.lblGraphName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblGraphName.Location = new System.Drawing.Point(15, 20);
      this.lblGraphName.Name = "lblGraphName";
      this.lblGraphName.Size = new System.Drawing.Size(70, 13);
      this.lblGraphName.TabIndex = 0;
      this.lblGraphName.Text = "Graph Name:";
      //
      // lvAvailableMetrics
      //
      this.lvAvailableMetrics.ContextMenuStrip = this.mnuContextListViews;
      this.lvAvailableMetrics.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lvAvailableMetrics.FullRowSelect = true;
      this.lvAvailableMetrics.GridLines = true;
      this.lvAvailableMetrics.HideSelection = false;
      this.lvAvailableMetrics.Location = new System.Drawing.Point(18, 107);
      this.lvAvailableMetrics.MultiSelect = false;
      this.lvAvailableMetrics.Name = "lvAvailableMetrics";
      this.lvAvailableMetrics.Size = new System.Drawing.Size(874, 172);
      this.lvAvailableMetrics.TabIndex = 25;
      this.lvAvailableMetrics.UseCompatibleStateImageBehavior = false;
      this.lvAvailableMetrics.View = System.Windows.Forms.View.Details;
      this.lvAvailableMetrics.SelectedIndexChanged += new System.EventHandler(this.lvAvailableMetrics_SelectedIndexChanged);
      this.lvAvailableMetrics.DoubleClick += new System.EventHandler(this.lvAvailableMetrics_DoubleClick);
      //
      // mnuContextListViews
      //
      this.mnuContextListViews.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuContextListViewMetricProperties,
        this.mnuContextListViewRollUp
      });
      this.mnuContextListViews.Name = "mnuContextListViews";
      this.mnuContextListViews.Size = new System.Drawing.Size(167, 70);
      //
      // mnuContextListViewMetricProperties
      //
      this.mnuContextListViewMetricProperties.Name = "mnuContextListViewMetricProperties";
      this.mnuContextListViewMetricProperties.Size = new System.Drawing.Size(166, 22);
      this.mnuContextListViewMetricProperties.Text = "Metric Properties";
      this.mnuContextListViewMetricProperties.Click += new System.EventHandler(this.mnuContextListViewMetricProperties_Click);
      //
      // lblFromDate
      //
      this.lblFromDate.AutoSize = true;
      this.lblFromDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFromDate.Location = new System.Drawing.Point(135, 70);
      this.lblFromDate.Name = "lblFromDate";
      this.lblFromDate.Size = new System.Drawing.Size(93, 13);
      this.lblFromDate.TabIndex = 101;
      this.lblFromDate.Text = "From Date / Time:";
      //
      // dtpFromDate
      //
      this.dtpFromDate.CustomFormat = "yyyy-MM-dd HH:mm ";
      this.dtpFromDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpFromDate.Location = new System.Drawing.Point(138, 86);
      this.dtpFromDate.Name = "dtpFromDate";
      this.dtpFromDate.Size = new System.Drawing.Size(121, 21);
      this.dtpFromDate.TabIndex = 7;
      //
      // gbAvailableMetrics
      //
      this.gbAvailableMetrics.Controls.Add(this.gbDivider);
      this.gbAvailableMetrics.Controls.Add(this.cboListFilterByMetric);
      this.gbAvailableMetrics.Controls.Add(this.cboListFilterObsvSvr);
      this.gbAvailableMetrics.Controls.Add(this.cboTargetApplication);
      this.gbAvailableMetrics.Controls.Add(this.cboEnvironment);
      this.gbAvailableMetrics.Controls.Add(this.cboTargetSystem);
      this.gbAvailableMetrics.Controls.Add(this.btnAddToGraph);
      this.gbAvailableMetrics.Controls.Add(this.btnGetMetrics);
      this.gbAvailableMetrics.Controls.Add(this.lvAvailableMetrics);
      this.gbAvailableMetrics.Controls.Add(this.lblListFilterByObsvSvr);
      this.gbAvailableMetrics.Controls.Add(this.label1);
      this.gbAvailableMetrics.Controls.Add(this.lblQueryFilters);
      this.gbAvailableMetrics.Controls.Add(this.lblListFilter);
      this.gbAvailableMetrics.Controls.Add(this.lblEnvironment);
      this.gbAvailableMetrics.Controls.Add(this.lblApplication);
      this.gbAvailableMetrics.Controls.Add(this.lblTargetSystem);
      this.gbAvailableMetrics.Location = new System.Drawing.Point(12, 162);
      this.gbAvailableMetrics.Name = "gbAvailableMetrics";
      this.gbAvailableMetrics.Size = new System.Drawing.Size(909, 322);
      this.gbAvailableMetrics.TabIndex = 20;
      this.gbAvailableMetrics.TabStop = false;
      this.gbAvailableMetrics.Text = "Available Metrics / Filters";
      //
      // gbDivider
      //
      this.gbDivider.Location = new System.Drawing.Point(455, 9);
      this.gbDivider.Name = "gbDivider";
      this.gbDivider.Size = new System.Drawing.Size(2, 92);
      this.gbDivider.TabIndex = 28;
      this.gbDivider.TabStop = false;
      this.gbDivider.Text = "groupBox1";
      //
      // cboListFilterByMetric
      //
      this.cboListFilterByMetric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboListFilterByMetric.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboListFilterByMetric.FormattingEnabled = true;
      this.cboListFilterByMetric.Location = new System.Drawing.Point(601, 74);
      this.cboListFilterByMetric.Name = "cboListFilterByMetric";
      this.cboListFilterByMetric.Size = new System.Drawing.Size(291, 21);
      this.cboListFilterByMetric.TabIndex = 27;
      this.cboListFilterByMetric.SelectedIndexChanged += new System.EventHandler(this.cboListFilterByMetric_SelectedIndexChanged);
      //
      // cboListFilterObsvSvr
      //
      this.cboListFilterObsvSvr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboListFilterObsvSvr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboListFilterObsvSvr.FormattingEnabled = true;
      this.cboListFilterObsvSvr.Location = new System.Drawing.Point(601, 47);
      this.cboListFilterObsvSvr.Name = "cboListFilterObsvSvr";
      this.cboListFilterObsvSvr.Size = new System.Drawing.Size(198, 21);
      this.cboListFilterObsvSvr.Sorted = true;
      this.cboListFilterObsvSvr.TabIndex = 23;
      this.cboListFilterObsvSvr.SelectedIndexChanged += new System.EventHandler(this.cboListFilterByMetric_SelectedIndexChanged);
      //
      // cboTargetApplication
      //
      this.cboTargetApplication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTargetApplication.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboTargetApplication.FormattingEnabled = true;
      this.cboTargetApplication.Location = new System.Drawing.Point(130, 74);
      this.cboTargetApplication.Name = "cboTargetApplication";
      this.cboTargetApplication.Size = new System.Drawing.Size(198, 21);
      this.cboTargetApplication.TabIndex = 23;
      //
      // cboEnvironment
      //
      this.cboEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboEnvironment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboEnvironment.FormattingEnabled = true;
      this.cboEnvironment.Location = new System.Drawing.Point(130, 20);
      this.cboEnvironment.Name = "cboEnvironment";
      this.cboEnvironment.Size = new System.Drawing.Size(198, 21);
      this.cboEnvironment.TabIndex = 21;
      //
      // cboTargetSystem
      //
      this.cboTargetSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTargetSystem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboTargetSystem.FormattingEnabled = true;
      this.cboTargetSystem.Location = new System.Drawing.Point(130, 47);
      this.cboTargetSystem.Name = "cboTargetSystem";
      this.cboTargetSystem.Size = new System.Drawing.Size(198, 21);
      this.cboTargetSystem.TabIndex = 22;
      //
      // btnAddToGraph
      //
      this.btnAddToGraph.Location = new System.Drawing.Point(18, 287);
      this.btnAddToGraph.Name = "btnAddToGraph";
      this.btnAddToGraph.Size = new System.Drawing.Size(152, 25);
      this.btnAddToGraph.TabIndex = 26;
      this.btnAddToGraph.Text = "Add to Graph";
      this.btnAddToGraph.UseVisualStyleBackColor = true;
      this.btnAddToGraph.Click += new System.EventHandler(this.btnAddToGraph_Click);
      //
      // btnGetMetrics
      //
      this.btnGetMetrics.Location = new System.Drawing.Point(337, 47);
      this.btnGetMetrics.Name = "btnGetMetrics";
      this.btnGetMetrics.Size = new System.Drawing.Size(110, 48);
      this.btnGetMetrics.TabIndex = 24;
      this.btnGetMetrics.Text = "Get Available Metrics";
      this.btnGetMetrics.UseVisualStyleBackColor = true;
      this.btnGetMetrics.Click += new System.EventHandler(this.btnGetMetrics_Click);
      //
      // lblListFilterByObsvSvr
      //
      this.lblListFilterByObsvSvr.AutoSize = true;
      this.lblListFilterByObsvSvr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblListFilterByObsvSvr.Location = new System.Drawing.Point(482, 50);
      this.lblListFilterByObsvSvr.Name = "lblListFilterByObsvSvr";
      this.lblListFilterByObsvSvr.Size = new System.Drawing.Size(113, 13);
      this.lblListFilterByObsvSvr.TabIndex = 0;
      this.lblListFilterByObsvSvr.Text = "Filter by Obsv Server:";
      //
      // label1
      //
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(463, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(65, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "List Filters";
      //
      // lblQueryFilters
      //
      this.lblQueryFilters.AutoSize = true;
      this.lblQueryFilters.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblQueryFilters.Location = new System.Drawing.Point(350, 13);
      this.lblQueryFilters.Name = "lblQueryFilters";
      this.lblQueryFilters.Size = new System.Drawing.Size(97, 13);
      this.lblQueryFilters.TabIndex = 0;
      this.lblQueryFilters.Text = "DB Query Filters";
      //
      // lblListFilter
      //
      this.lblListFilter.AutoSize = true;
      this.lblListFilter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblListFilter.Location = new System.Drawing.Point(482, 77);
      this.lblListFilter.Name = "lblListFilter";
      this.lblListFilter.Size = new System.Drawing.Size(82, 13);
      this.lblListFilter.TabIndex = 0;
      this.lblListFilter.Text = "Filter by Metric:";
      //
      // lblEnvironment
      //
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblEnvironment.Location = new System.Drawing.Point(18, 23);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(71, 13);
      this.lblEnvironment.TabIndex = 0;
      this.lblEnvironment.Text = "Environment:";
      //
      // lblApplication
      //
      this.lblApplication.AutoSize = true;
      this.lblApplication.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblApplication.Location = new System.Drawing.Point(18, 77);
      this.lblApplication.Name = "lblApplication";
      this.lblApplication.Size = new System.Drawing.Size(98, 13);
      this.lblApplication.TabIndex = 0;
      this.lblApplication.Text = "Target Application:";
      //
      // lblTargetSystem
      //
      this.lblTargetSystem.AutoSize = true;
      this.lblTargetSystem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTargetSystem.Location = new System.Drawing.Point(18, 50);
      this.lblTargetSystem.Name = "lblTargetSystem";
      this.lblTargetSystem.Size = new System.Drawing.Size(81, 13);
      this.lblTargetSystem.TabIndex = 0;
      this.lblTargetSystem.Text = "Target System:";
      //
      // optionLatestMetrics
      //
      this.optionLatestMetrics.AutoSize = true;
      this.optionLatestMetrics.Checked = true;
      this.optionLatestMetrics.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.optionLatestMetrics.Location = new System.Drawing.Point(9, 40);
      this.optionLatestMetrics.Name = "optionLatestMetrics";
      this.optionLatestMetrics.Size = new System.Drawing.Size(110, 17);
      this.optionLatestMetrics.TabIndex = 11;
      this.optionLatestMetrics.TabStop = true;
      this.optionLatestMetrics.Text = "Use latest metrics";
      this.optionLatestMetrics.UseVisualStyleBackColor = true;
      this.optionLatestMetrics.CheckedChanged += new System.EventHandler(this.optionLatestMetrics_CheckedChanged);
      //
      // optionFilterByDate
      //
      this.optionFilterByDate.AutoSize = true;
      this.optionFilterByDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.optionFilterByDate.Location = new System.Drawing.Point(9, 86);
      this.optionFilterByDate.Name = "optionFilterByDate";
      this.optionFilterByDate.Size = new System.Drawing.Size(123, 17);
      this.optionFilterByDate.TabIndex = 6;
      this.optionFilterByDate.Text = "Use date/time range";
      this.optionFilterByDate.UseVisualStyleBackColor = true;
      this.optionFilterByDate.CheckedChanged += new System.EventHandler(this.optionFilterByDate_CheckedChanged);
      //
      // lblToDate
      //
      this.lblToDate.AutoSize = true;
      this.lblToDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblToDate.Location = new System.Drawing.Point(265, 70);
      this.lblToDate.Name = "lblToDate";
      this.lblToDate.Size = new System.Drawing.Size(81, 13);
      this.lblToDate.TabIndex = 101;
      this.lblToDate.Text = "To Date / Time:";
      //
      // dtpToDate
      //
      this.dtpToDate.CustomFormat = "yyyy-MM-dd HH:mm ";
      this.dtpToDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpToDate.Location = new System.Drawing.Point(268, 86);
      this.dtpToDate.Name = "dtpToDate";
      this.dtpToDate.Size = new System.Drawing.Size(121, 21);
      this.dtpToDate.TabIndex = 8;
      //
      // gbIncludedMetrics
      //
      this.gbIncludedMetrics.Controls.Add(this.lvIncludedMetrics);
      this.gbIncludedMetrics.Controls.Add(this.btnRemoveAllFromGraph);
      this.gbIncludedMetrics.Controls.Add(this.btnRemoveFromGraph);
      this.gbIncludedMetrics.Location = new System.Drawing.Point(12, 498);
      this.gbIncludedMetrics.Name = "gbIncludedMetrics";
      this.gbIncludedMetrics.Size = new System.Drawing.Size(909, 184);
      this.gbIncludedMetrics.TabIndex = 30;
      this.gbIncludedMetrics.TabStop = false;
      this.gbIncludedMetrics.Text = "Metrics Included in Graph";
      //
      // lvIncludedMetrics
      //
      this.lvIncludedMetrics.ContextMenuStrip = this.mnuContextListViews;
      this.lvIncludedMetrics.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lvIncludedMetrics.FullRowSelect = true;
      this.lvIncludedMetrics.GridLines = true;
      this.lvIncludedMetrics.Location = new System.Drawing.Point(18, 24);
      this.lvIncludedMetrics.MultiSelect = false;
      this.lvIncludedMetrics.Name = "lvIncludedMetrics";
      this.lvIncludedMetrics.Size = new System.Drawing.Size(874, 118);
      this.lvIncludedMetrics.TabIndex = 31;
      this.lvIncludedMetrics.UseCompatibleStateImageBehavior = false;
      this.lvIncludedMetrics.View = System.Windows.Forms.View.Details;
      this.lvIncludedMetrics.SelectedIndexChanged += new System.EventHandler(this.lvIncludedMetrics_SelectedIndexChanged);
      //
      // btnRemoveAllFromGraph
      //
      this.btnRemoveAllFromGraph.Location = new System.Drawing.Point(176, 150);
      this.btnRemoveAllFromGraph.Name = "btnRemoveAllFromGraph";
      this.btnRemoveAllFromGraph.Size = new System.Drawing.Size(152, 25);
      this.btnRemoveAllFromGraph.TabIndex = 32;
      this.btnRemoveAllFromGraph.Text = "Remove All";
      this.btnRemoveAllFromGraph.UseVisualStyleBackColor = true;
      this.btnRemoveAllFromGraph.Click += new System.EventHandler(this.btnRemoveAllFromGraph_Click);
      //
      // btnRemoveFromGraph
      //
      this.btnRemoveFromGraph.Location = new System.Drawing.Point(18, 150);
      this.btnRemoveFromGraph.Name = "btnRemoveFromGraph";
      this.btnRemoveFromGraph.Size = new System.Drawing.Size(152, 25);
      this.btnRemoveFromGraph.TabIndex = 32;
      this.btnRemoveFromGraph.Text = "Remove from Graph";
      this.btnRemoveFromGraph.UseVisualStyleBackColor = true;
      this.btnRemoveFromGraph.Click += new System.EventHandler(this.btnRemoveFromGraph_Click);
      //
      // cboDataPoints
      //
      this.cboDataPoints.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboDataPoints.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboDataPoints.FormattingEnabled = true;
      this.cboDataPoints.ItemHeight = 13;
      this.cboDataPoints.Items.AddRange(new object[] {
        "--------",
        "10 data points",
        "25 data points",
        "50 data points",
        "100 data points",
        "200 data points",
        "300 data points",
        "500 data points",
        "1000 data points",
        "2000 data points",
        "3000 data points"
      });
      this.cboDataPoints.Location = new System.Drawing.Point(138, 40);
      this.cboDataPoints.Name = "cboDataPoints";
      this.cboDataPoints.Size = new System.Drawing.Size(121, 21);
      this.cboDataPoints.TabIndex = 5;
      //
      // lblDataPoints
      //
      this.lblDataPoints.AutoSize = true;
      this.lblDataPoints.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDataPoints.Location = new System.Drawing.Point(135, 24);
      this.lblDataPoints.Name = "lblDataPoints";
      this.lblDataPoints.Size = new System.Drawing.Size(66, 13);
      this.lblDataPoints.TabIndex = 101;
      this.lblDataPoints.Text = "Data points:";
      //
      // gbDataPoints
      //
      this.gbDataPoints.Controls.Add(this.cboDataPoints);
      this.gbDataPoints.Controls.Add(this.optionLatestMetrics);
      this.gbDataPoints.Controls.Add(this.dtpToDate);
      this.gbDataPoints.Controls.Add(this.lblFromDate);
      this.gbDataPoints.Controls.Add(this.dtpFromDate);
      this.gbDataPoints.Controls.Add(this.lblToDate);
      this.gbDataPoints.Controls.Add(this.lblDataPoints);
      this.gbDataPoints.Controls.Add(this.optionFilterByDate);
      this.gbDataPoints.Location = new System.Drawing.Point(284, 12);
      this.gbDataPoints.Name = "gbDataPoints";
      this.gbDataPoints.Size = new System.Drawing.Size(637, 138);
      this.gbDataPoints.TabIndex = 10;
      this.gbDataPoints.TabStop = false;
      this.gbDataPoints.Text = "Data Points or Date/Time Range";
      //
      // mnuContextListViewRollUp
      //
      this.mnuContextListViewRollUp.Name = "mnuContextListViewRollUp";
      this.mnuContextListViewRollUp.Size = new System.Drawing.Size(166, 22);
      this.mnuContextListViewRollUp.Text = "Rollup to Hourly";
      this.mnuContextListViewRollUp.Click += new System.EventHandler(this.mnuContextListViewRollUp_Click);
      //
      // frmGraphProperties
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(933, 728);
      this.ControlBox = false;
      this.Controls.Add(this.gbDataPoints);
      this.Controls.Add(this.gbIncludedMetrics);
      this.Controls.Add(this.gbAvailableMetrics);
      this.Controls.Add(this.gbGraphInfo);
      this.Controls.Add(this.btnUpdate);
      this.Controls.Add(this.btnCancel);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmGraphProperties";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Graph Properties";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGraphProperties_KeyDown);
      this.gbGraphInfo.ResumeLayout(false);
      this.gbGraphInfo.PerformLayout();
      this.mnuContextListViews.ResumeLayout(false);
      this.gbAvailableMetrics.ResumeLayout(false);
      this.gbAvailableMetrics.PerformLayout();
      this.gbIncludedMetrics.ResumeLayout(false);
      this.gbDataPoints.ResumeLayout(false);
      this.gbDataPoints.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.GroupBox gbGraphInfo;
    private System.Windows.Forms.TextBox txtRefreshInterval;
    private System.Windows.Forms.TextBox txtGraphName;
    private System.Windows.Forms.Label lblRefreshInterval;
    private System.Windows.Forms.Label lblGraphName;
    private System.Windows.Forms.CheckBox ckActive;
    private System.Windows.Forms.ListView lvAvailableMetrics;
    private System.Windows.Forms.Label lblFromDate;
    private System.Windows.Forms.DateTimePicker dtpFromDate;
    private System.Windows.Forms.GroupBox gbAvailableMetrics;
    private System.Windows.Forms.DateTimePicker dtpToDate;
    private System.Windows.Forms.Label lblToDate;
    private System.Windows.Forms.RadioButton optionLatestMetrics;
    private System.Windows.Forms.RadioButton optionFilterByDate;
    private System.Windows.Forms.ComboBox cboTargetApplication;
    private System.Windows.Forms.ComboBox cboEnvironment;
    private System.Windows.Forms.ComboBox cboTargetSystem;
    private System.Windows.Forms.Button btnGetMetrics;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.Label lblApplication;
    private System.Windows.Forms.Label lblTargetSystem;
    private System.Windows.Forms.Button btnAddToGraph;
    private System.Windows.Forms.GroupBox gbIncludedMetrics;
    private System.Windows.Forms.ListView lvIncludedMetrics;
    private System.Windows.Forms.Button btnRemoveFromGraph;
    private System.Windows.Forms.ComboBox cboDataPoints;
    private System.Windows.Forms.Label lblDataPoints;
    private System.Windows.Forms.GroupBox gbDataPoints;
    private System.Windows.Forms.ContextMenuStrip mnuContextListViews;
    private System.Windows.Forms.ToolStripMenuItem mnuContextListViewMetricProperties;
    private System.Windows.Forms.CheckBox ckUseEsqlmgmt;
    private System.Windows.Forms.ComboBox cboListFilterByMetric;
    private System.Windows.Forms.Label lblListFilter;
    private System.Windows.Forms.Button btnRemoveAllFromGraph;
    private System.Windows.Forms.ComboBox cboListFilterObsvSvr;
    private System.Windows.Forms.Label lblListFilterByObsvSvr;
    private System.Windows.Forms.GroupBox gbDivider;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblQueryFilters;
    private System.Windows.Forms.ToolStripMenuItem mnuContextListViewRollUp;
  }
}
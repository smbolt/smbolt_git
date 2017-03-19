namespace Org.OpsManager
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.cboEnvironment = new System.Windows.Forms.ComboBox();
      this.btnMaintainTaskParameters = new System.Windows.Forms.Button();
      this.btnNewScheduledTask = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabScheduledTasks = new System.Windows.Forms.TabPage();
      this.gvScheduledTasks = new System.Windows.Forms.DataGridView();
      this.ctxMenuScheduledTasks = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuScheduledTaskDisplayTaskReport = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuScheduledTaskDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuScheduledTaskMigrateTask = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuScheduledTaskViewRunHistory = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuScheduledTaskSetDryRunOn = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuScheduledTaskSetDryRunOff = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuScheduledTaskSetActiveOn = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuScheduledTaskSetActiveOff = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuScheduledTaskSetRunUntilOverrideOn = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuScheduledTaskSetRunUntilOverrideOff = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlScheduledTasks = new System.Windows.Forms.Panel();
      this.lblScheduleInterval = new System.Windows.Forms.Label();
      this.cboScheduleInterval = new System.Windows.Forms.ComboBox();
      this.btnGetTasksReport = new System.Windows.Forms.Button();
      this.btnGetScheduledTasks = new System.Windows.Forms.Button();
      this.tabNotifications = new System.Windows.Forms.TabPage();
      this.pnlNotificationHolder = new System.Windows.Forms.Panel();
      this.treeViewNotifications = new System.Windows.Forms.TreeView();
      this.imageListNotifications = new System.Windows.Forms.ImageList(this.components);
      this.pnlNotifications = new System.Windows.Forms.Panel();
      this.lblNotifyConfigSet = new System.Windows.Forms.Label();
      this.cboNotifyConfigSets = new System.Windows.Forms.ComboBox();
      this.btnGetNotifyConfigReport = new System.Windows.Forms.Button();
      this.btnRefreshNotificationsTree = new System.Windows.Forms.Button();
      this.tabServicesSitesAppPools = new System.Windows.Forms.TabPage();
      this.tabServSitesAppPools = new System.Windows.Forms.TabControl();
      this.tabWinServices = new System.Windows.Forms.TabPage();
      this.gvWindowsServices = new System.Windows.Forms.DataGridView();
      this.ctxMenuWinServices = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuWinServicesStart = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuWinServicesStop = new System.Windows.Forms.ToolStripMenuItem();
      this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlWindowsServices = new System.Windows.Forms.Panel();
      this.btnGetWindowsServices = new System.Windows.Forms.Button();
      this.tabWebSites = new System.Windows.Forms.TabPage();
      this.gvWebSites = new System.Windows.Forms.DataGridView();
      this.ctxMenuWebSites = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuWebSitesStart = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuWebSitesStop = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlWebSites = new System.Windows.Forms.Panel();
      this.btnGetWebSites = new System.Windows.Forms.Button();
      this.tabAppPools = new System.Windows.Forms.TabPage();
      this.gvAppPools = new System.Windows.Forms.DataGridView();
      this.ctxMenuAppPools = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuAppPoolsStart = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuAppPoolsStop = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlAppPools = new System.Windows.Forms.Panel();
      this.btnGetAppPools = new System.Windows.Forms.Button();
      this.tabLogging = new System.Windows.Forms.TabPage();
      this.loggingSplitter = new System.Windows.Forms.SplitContainer();
      this.gvLogging = new System.Windows.Forms.DataGridView();
      this.txtLogDetails = new System.Windows.Forms.TextBox();
      this.pnlTopLogging = new System.Windows.Forms.Panel();
      this.lblLogMessage = new System.Windows.Forms.Label();
      this.txtLogMessage = new System.Windows.Forms.TextBox();
      this.chkDescending = new System.Windows.Forms.CheckBox();
      this.lblRecordCount = new System.Windows.Forms.Label();
      this.lblEntities = new System.Windows.Forms.Label();
      this.lblEvents = new System.Windows.Forms.Label();
      this.lblModules = new System.Windows.Forms.Label();
      this.lblSeverityCode = new System.Windows.Forms.Label();
      this.cboRecordCount = new System.Windows.Forms.ComboBox();
      this.cboLogEntities = new System.Windows.Forms.ComboBox();
      this.cboLogEvents = new System.Windows.Forms.ComboBox();
      this.cboLogModules = new System.Windows.Forms.ComboBox();
      this.cboSeverityCode = new System.Windows.Forms.ComboBox();
      this.btnRefreshLog = new System.Windows.Forms.Button();
      this.tabIdentifiers = new System.Windows.Forms.TabPage();
      this.gvIdentifiers = new System.Windows.Forms.DataGridView();
      this.ctxMenuIdentifiers = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuIdentifiersMigrate = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTopIdentifiers = new System.Windows.Forms.Panel();
      this.lblIdentifierDescription = new System.Windows.Forms.Label();
      this.txtIdentifierDescription = new System.Windows.Forms.TextBox();
      this.btnNewIdentifier = new System.Windows.Forms.Button();
      this.lblIdentifier = new System.Windows.Forms.Label();
      this.cboIdentifiers = new System.Windows.Forms.ComboBox();
      this.tabScheduledRuns = new System.Windows.Forms.TabPage();
      this.pnlScheduledRunsBottom = new System.Windows.Forms.Panel();
      this.gvScheduledRuns = new System.Windows.Forms.DataGridView();
      this.clbScheduledRuns = new System.Windows.Forms.CheckedListBox();
      this.pnlScheduledRunsTop = new System.Windows.Forms.Panel();
      this.btnViewScheduledRuns = new System.Windows.Forms.Button();
      this.lblIntervalEndDateTime = new System.Windows.Forms.Label();
      this.lblIntervalStartDateTime = new System.Windows.Forms.Label();
      this.dtpIntervalEndTime = new System.Windows.Forms.DateTimePicker();
      this.dtpIntervalStartTime = new System.Windows.Forms.DateTimePicker();
      this.dtpIntervalEndDate = new System.Windows.Forms.DateTimePicker();
      this.dtpIntervalStartDate = new System.Windows.Forms.DateTimePicker();
      this.ctxMenuNotifications = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuNotificationsAddNewObject = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuNotificationsDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabScheduledTasks.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvScheduledTasks)).BeginInit();
      this.ctxMenuScheduledTasks.SuspendLayout();
      this.pnlScheduledTasks.SuspendLayout();
      this.tabNotifications.SuspendLayout();
      this.pnlNotifications.SuspendLayout();
      this.tabServicesSitesAppPools.SuspendLayout();
      this.tabServSitesAppPools.SuspendLayout();
      this.tabWinServices.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvWindowsServices)).BeginInit();
      this.ctxMenuWinServices.SuspendLayout();
      this.pnlWindowsServices.SuspendLayout();
      this.tabWebSites.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvWebSites)).BeginInit();
      this.ctxMenuWebSites.SuspendLayout();
      this.pnlWebSites.SuspendLayout();
      this.tabAppPools.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvAppPools)).BeginInit();
      this.ctxMenuAppPools.SuspendLayout();
      this.pnlAppPools.SuspendLayout();
      this.tabLogging.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.loggingSplitter)).BeginInit();
      this.loggingSplitter.Panel1.SuspendLayout();
      this.loggingSplitter.Panel2.SuspendLayout();
      this.loggingSplitter.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvLogging)).BeginInit();
      this.pnlTopLogging.SuspendLayout();
      this.tabIdentifiers.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvIdentifiers)).BeginInit();
      this.ctxMenuIdentifiers.SuspendLayout();
      this.pnlTopIdentifiers.SuspendLayout();
      this.tabScheduledRuns.SuspendLayout();
      this.pnlScheduledRunsBottom.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvScheduledRuns)).BeginInit();
      this.pnlScheduledRunsTop.SuspendLayout();
      this.ctxMenuNotifications.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1484, 24);
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
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.lblEnvironment);
      this.pnlTop.Controls.Add(this.cboEnvironment);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1484, 67);
      this.pnlTop.TabIndex = 1;
      // 
      // lblEnvironment
      // 
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(12, 6);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(66, 13);
      this.lblEnvironment.TabIndex = 4;
      this.lblEnvironment.Text = "Environment";
      // 
      // cboEnvironment
      // 
      this.cboEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboEnvironment.FormattingEnabled = true;
      this.cboEnvironment.Location = new System.Drawing.Point(12, 22);
      this.cboEnvironment.Name = "cboEnvironment";
      this.cboEnvironment.Size = new System.Drawing.Size(90, 21);
      this.cboEnvironment.TabIndex = 3;
      this.cboEnvironment.Tag = "EnvironmentChange";
      this.cboEnvironment.TextChanged += new System.EventHandler(this.Action);
      // 
      // btnMaintainTaskParameters
      // 
      this.btnMaintainTaskParameters.Location = new System.Drawing.Point(155, 20);
      this.btnMaintainTaskParameters.Name = "btnMaintainTaskParameters";
      this.btnMaintainTaskParameters.Size = new System.Drawing.Size(144, 35);
      this.btnMaintainTaskParameters.TabIndex = 2;
      this.btnMaintainTaskParameters.Tag = "MaintainTaskParameters";
      this.btnMaintainTaskParameters.Text = "Maintain Task Parameters";
      this.btnMaintainTaskParameters.UseVisualStyleBackColor = true;
      this.btnMaintainTaskParameters.Click += new System.EventHandler(this.Action);
      // 
      // btnNewScheduledTask
      // 
      this.btnNewScheduledTask.Location = new System.Drawing.Point(5, 20);
      this.btnNewScheduledTask.Name = "btnNewScheduledTask";
      this.btnNewScheduledTask.Size = new System.Drawing.Size(144, 35);
      this.btnNewScheduledTask.TabIndex = 1;
      this.btnNewScheduledTask.Tag = "NewScheduledTask";
      this.btnNewScheduledTask.Text = "New Scheduled Task";
      this.btnNewScheduledTask.UseVisualStyleBackColor = true;
      this.btnNewScheduledTask.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 656);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1484, 23);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabScheduledTasks);
      this.tabMain.Controls.Add(this.tabNotifications);
      this.tabMain.Controls.Add(this.tabServicesSitesAppPools);
      this.tabMain.Controls.Add(this.tabLogging);
      this.tabMain.Controls.Add(this.tabIdentifiers);
      this.tabMain.Controls.Add(this.tabScheduledRuns);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(150, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 91);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1484, 565);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 3;
      this.tabMain.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabMain_Selecting);
      // 
      // tabScheduledTasks
      // 
      this.tabScheduledTasks.Controls.Add(this.gvScheduledTasks);
      this.tabScheduledTasks.Controls.Add(this.pnlScheduledTasks);
      this.tabScheduledTasks.Location = new System.Drawing.Point(4, 22);
      this.tabScheduledTasks.Name = "tabScheduledTasks";
      this.tabScheduledTasks.Padding = new System.Windows.Forms.Padding(3);
      this.tabScheduledTasks.Size = new System.Drawing.Size(1476, 539);
      this.tabScheduledTasks.TabIndex = 0;
      this.tabScheduledTasks.Text = "Scheduled Tasks";
      this.tabScheduledTasks.UseVisualStyleBackColor = true;
      // 
      // gvScheduledTasks
      // 
      this.gvScheduledTasks.AllowUserToAddRows = false;
      this.gvScheduledTasks.AllowUserToDeleteRows = false;
      this.gvScheduledTasks.AllowUserToResizeRows = false;
      this.gvScheduledTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvScheduledTasks.ContextMenuStrip = this.ctxMenuScheduledTasks;
      this.gvScheduledTasks.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvScheduledTasks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvScheduledTasks.Location = new System.Drawing.Point(3, 83);
      this.gvScheduledTasks.MultiSelect = false;
      this.gvScheduledTasks.Name = "gvScheduledTasks";
      this.gvScheduledTasks.RowHeadersVisible = false;
      this.gvScheduledTasks.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvScheduledTasks.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvScheduledTasks.RowTemplate.Height = 19;
      this.gvScheduledTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvScheduledTasks.Size = new System.Drawing.Size(1470, 453);
      this.gvScheduledTasks.TabIndex = 5;
      this.gvScheduledTasks.Tag = "EditScheduledTask";
      this.gvScheduledTasks.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridView_CellMouseUp);
      this.gvScheduledTasks.DoubleClick += new System.EventHandler(this.Action);
      // 
      // ctxMenuScheduledTasks
      // 
      this.ctxMenuScheduledTasks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuScheduledTaskDisplayTaskReport,
            this.ctxMenuScheduledTaskDelete,
            this.ctxMenuScheduledTaskMigrateTask,
            this.ctxMenuScheduledTaskViewRunHistory,
            this.ctxMenuScheduledTaskSetDryRunOn,
            this.ctxMenuScheduledTaskSetDryRunOff,
            this.ctxMenuScheduledTaskSetActiveOn,
            this.ctxMenuScheduledTaskSetActiveOff,
            this.ctxMenuScheduledTaskSetRunUntilOverrideOn,
            this.ctxMenuScheduledTaskSetRunUntilOverrideOff});
      this.ctxMenuScheduledTasks.Name = "ctxMenuScheduledTasks";
      this.ctxMenuScheduledTasks.Size = new System.Drawing.Size(212, 224);
      this.ctxMenuScheduledTasks.Opening += new System.ComponentModel.CancelEventHandler(this.gridContextMenu_Opening);
      // 
      // ctxMenuScheduledTaskDisplayTaskReport
      // 
      this.ctxMenuScheduledTaskDisplayTaskReport.Name = "ctxMenuScheduledTaskDisplayTaskReport";
      this.ctxMenuScheduledTaskDisplayTaskReport.Size = new System.Drawing.Size(211, 22);
      this.ctxMenuScheduledTaskDisplayTaskReport.Tag = "DisplayTaskReport";
      this.ctxMenuScheduledTaskDisplayTaskReport.Text = "Display Task Report";
      this.ctxMenuScheduledTaskDisplayTaskReport.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuScheduledTaskDelete
      // 
      this.ctxMenuScheduledTaskDelete.Name = "ctxMenuScheduledTaskDelete";
      this.ctxMenuScheduledTaskDelete.Size = new System.Drawing.Size(211, 22);
      this.ctxMenuScheduledTaskDelete.Tag = "DeleteScheduledTask";
      this.ctxMenuScheduledTaskDelete.Text = "Delete this Task";
      this.ctxMenuScheduledTaskDelete.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuScheduledTaskMigrateTask
      // 
      this.ctxMenuScheduledTaskMigrateTask.Name = "ctxMenuScheduledTaskMigrateTask";
      this.ctxMenuScheduledTaskMigrateTask.Size = new System.Drawing.Size(211, 22);
      this.ctxMenuScheduledTaskMigrateTask.Tag = "MigrateTask";
      this.ctxMenuScheduledTaskMigrateTask.Text = "Migrate Task to [new env]";
      this.ctxMenuScheduledTaskMigrateTask.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuScheduledTaskViewRunHistory
      // 
      this.ctxMenuScheduledTaskViewRunHistory.Name = "ctxMenuScheduledTaskViewRunHistory";
      this.ctxMenuScheduledTaskViewRunHistory.Size = new System.Drawing.Size(211, 22);
      this.ctxMenuScheduledTaskViewRunHistory.Tag = "ViewRunHistory";
      this.ctxMenuScheduledTaskViewRunHistory.Text = "View Run History";
      this.ctxMenuScheduledTaskViewRunHistory.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuScheduledTaskSetDryRunOn
      // 
      this.ctxMenuScheduledTaskSetDryRunOn.Name = "ctxMenuScheduledTaskSetDryRunOn";
      this.ctxMenuScheduledTaskSetDryRunOn.Size = new System.Drawing.Size(211, 22);
      this.ctxMenuScheduledTaskSetDryRunOn.Tag = "SetDryRunOn";
      this.ctxMenuScheduledTaskSetDryRunOn.Text = "Set Dry Run On";
      this.ctxMenuScheduledTaskSetDryRunOn.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuScheduledTaskSetDryRunOff
      // 
      this.ctxMenuScheduledTaskSetDryRunOff.Name = "ctxMenuScheduledTaskSetDryRunOff";
      this.ctxMenuScheduledTaskSetDryRunOff.Size = new System.Drawing.Size(211, 22);
      this.ctxMenuScheduledTaskSetDryRunOff.Tag = "SetDryRunOff";
      this.ctxMenuScheduledTaskSetDryRunOff.Text = "Set Dry Run Off";
      this.ctxMenuScheduledTaskSetDryRunOff.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuScheduledTaskSetActiveOn
      // 
      this.ctxMenuScheduledTaskSetActiveOn.Name = "ctxMenuScheduledTaskSetActiveOn";
      this.ctxMenuScheduledTaskSetActiveOn.Size = new System.Drawing.Size(211, 22);
      this.ctxMenuScheduledTaskSetActiveOn.Tag = "SetActiveOn";
      this.ctxMenuScheduledTaskSetActiveOn.Text = "Set Active On";
      this.ctxMenuScheduledTaskSetActiveOn.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuScheduledTaskSetActiveOff
      // 
      this.ctxMenuScheduledTaskSetActiveOff.Name = "ctxMenuScheduledTaskSetActiveOff";
      this.ctxMenuScheduledTaskSetActiveOff.Size = new System.Drawing.Size(211, 22);
      this.ctxMenuScheduledTaskSetActiveOff.Tag = "SetActiveOff";
      this.ctxMenuScheduledTaskSetActiveOff.Text = "Set Active Off";
      this.ctxMenuScheduledTaskSetActiveOff.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuScheduledTaskSetRunUntilOverrideOn
      // 
      this.ctxMenuScheduledTaskSetRunUntilOverrideOn.Name = "ctxMenuScheduledTaskSetRunUntilOverrideOn";
      this.ctxMenuScheduledTaskSetRunUntilOverrideOn.Size = new System.Drawing.Size(211, 22);
      this.ctxMenuScheduledTaskSetRunUntilOverrideOn.Tag = "SetRunUntilOverrideOn";
      this.ctxMenuScheduledTaskSetRunUntilOverrideOn.Text = "Set Run Until Override On";
      this.ctxMenuScheduledTaskSetRunUntilOverrideOn.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuScheduledTaskSetRunUntilOverrideOff
      // 
      this.ctxMenuScheduledTaskSetRunUntilOverrideOff.Name = "ctxMenuScheduledTaskSetRunUntilOverrideOff";
      this.ctxMenuScheduledTaskSetRunUntilOverrideOff.Size = new System.Drawing.Size(211, 22);
      this.ctxMenuScheduledTaskSetRunUntilOverrideOff.Tag = "SetRunUntilOverrideOff";
      this.ctxMenuScheduledTaskSetRunUntilOverrideOff.Text = "Set Run Until Override Off";
      this.ctxMenuScheduledTaskSetRunUntilOverrideOff.Click += new System.EventHandler(this.Action);
      // 
      // pnlScheduledTasks
      // 
      this.pnlScheduledTasks.Controls.Add(this.lblScheduleInterval);
      this.pnlScheduledTasks.Controls.Add(this.cboScheduleInterval);
      this.pnlScheduledTasks.Controls.Add(this.btnGetTasksReport);
      this.pnlScheduledTasks.Controls.Add(this.btnGetScheduledTasks);
      this.pnlScheduledTasks.Controls.Add(this.btnNewScheduledTask);
      this.pnlScheduledTasks.Controls.Add(this.btnMaintainTaskParameters);
      this.pnlScheduledTasks.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlScheduledTasks.Location = new System.Drawing.Point(3, 3);
      this.pnlScheduledTasks.Name = "pnlScheduledTasks";
      this.pnlScheduledTasks.Size = new System.Drawing.Size(1470, 80);
      this.pnlScheduledTasks.TabIndex = 6;
      // 
      // lblScheduleInterval
      // 
      this.lblScheduleInterval.AutoSize = true;
      this.lblScheduleInterval.Location = new System.Drawing.Point(691, 12);
      this.lblScheduleInterval.Name = "lblScheduleInterval";
      this.lblScheduleInterval.Size = new System.Drawing.Size(93, 13);
      this.lblScheduleInterval.TabIndex = 6;
      this.lblScheduleInterval.Text = "Schedule Interval:";
      // 
      // cboScheduleInterval
      // 
      this.cboScheduleInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboScheduleInterval.FormattingEnabled = true;
      this.cboScheduleInterval.Items.AddRange(new object[] {
            "Day",
            "Week",
            "Month"});
      this.cboScheduleInterval.Location = new System.Drawing.Point(691, 28);
      this.cboScheduleInterval.Name = "cboScheduleInterval";
      this.cboScheduleInterval.Size = new System.Drawing.Size(121, 21);
      this.cboScheduleInterval.TabIndex = 5;
      // 
      // btnGetTasksReport
      // 
      this.btnGetTasksReport.Location = new System.Drawing.Point(493, 20);
      this.btnGetTasksReport.Name = "btnGetTasksReport";
      this.btnGetTasksReport.Size = new System.Drawing.Size(144, 35);
      this.btnGetTasksReport.TabIndex = 4;
      this.btnGetTasksReport.Tag = "GetTasksReport";
      this.btnGetTasksReport.Text = "Get Tasks Report";
      this.btnGetTasksReport.UseVisualStyleBackColor = true;
      this.btnGetTasksReport.Click += new System.EventHandler(this.Action);
      // 
      // btnGetScheduledTasks
      // 
      this.btnGetScheduledTasks.Location = new System.Drawing.Point(1100, 20);
      this.btnGetScheduledTasks.Name = "btnGetScheduledTasks";
      this.btnGetScheduledTasks.Size = new System.Drawing.Size(144, 35);
      this.btnGetScheduledTasks.TabIndex = 3;
      this.btnGetScheduledTasks.Tag = "GetScheduledTasks";
      this.btnGetScheduledTasks.Text = "Refresh Scheduled Tasks";
      this.btnGetScheduledTasks.UseVisualStyleBackColor = true;
      this.btnGetScheduledTasks.Click += new System.EventHandler(this.Action);
      // 
      // tabNotifications
      // 
      this.tabNotifications.Controls.Add(this.pnlNotificationHolder);
      this.tabNotifications.Controls.Add(this.treeViewNotifications);
      this.tabNotifications.Controls.Add(this.pnlNotifications);
      this.tabNotifications.Location = new System.Drawing.Point(4, 22);
      this.tabNotifications.Name = "tabNotifications";
      this.tabNotifications.Size = new System.Drawing.Size(1476, 539);
      this.tabNotifications.TabIndex = 5;
      this.tabNotifications.Text = "Notifications";
      this.tabNotifications.UseVisualStyleBackColor = true;
      // 
      // pnlNotificationHolder
      // 
      this.pnlNotificationHolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnlNotificationHolder.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlNotificationHolder.Location = new System.Drawing.Point(255, 80);
      this.pnlNotificationHolder.Name = "pnlNotificationHolder";
      this.pnlNotificationHolder.Size = new System.Drawing.Size(1221, 459);
      this.pnlNotificationHolder.TabIndex = 2;
      // 
      // treeViewNotifications
      // 
      this.treeViewNotifications.AllowDrop = true;
      this.treeViewNotifications.Dock = System.Windows.Forms.DockStyle.Left;
      this.treeViewNotifications.ImageKey = "treeroot.ico";
      this.treeViewNotifications.ImageList = this.imageListNotifications;
      this.treeViewNotifications.Location = new System.Drawing.Point(0, 80);
      this.treeViewNotifications.Name = "treeViewNotifications";
      this.treeViewNotifications.SelectedImageIndex = 0;
      this.treeViewNotifications.Size = new System.Drawing.Size(255, 459);
      this.treeViewNotifications.TabIndex = 0;
      this.treeViewNotifications.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewNotifications_ItemDrag);
      this.treeViewNotifications.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewNotifications_BeforeSelect);
      this.treeViewNotifications.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewNotifications_AfterSelect);
      this.treeViewNotifications.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewNotifications_NodeMouseClick);
      this.treeViewNotifications.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewNotifications_DragDrop);
      this.treeViewNotifications.DragOver += new System.Windows.Forms.DragEventHandler(this.treeViewNotifications_DragOver);
      // 
      // imageListNotifications
      // 
      this.imageListNotifications.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListNotifications.ImageStream")));
      this.imageListNotifications.TransparentColor = System.Drawing.Color.Transparent;
      this.imageListNotifications.Images.SetKeyName(0, "treeroot.ico");
      this.imageListNotifications.Images.SetKeyName(1, "configset.ico");
      this.imageListNotifications.Images.SetKeyName(2, "config.ico");
      this.imageListNotifications.Images.SetKeyName(3, "event.ico");
      this.imageListNotifications.Images.SetKeyName(4, "group.ico");
      this.imageListNotifications.Images.SetKeyName(5, "person.ico");
      // 
      // pnlNotifications
      // 
      this.pnlNotifications.Controls.Add(this.lblNotifyConfigSet);
      this.pnlNotifications.Controls.Add(this.cboNotifyConfigSets);
      this.pnlNotifications.Controls.Add(this.btnGetNotifyConfigReport);
      this.pnlNotifications.Controls.Add(this.btnRefreshNotificationsTree);
      this.pnlNotifications.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlNotifications.Location = new System.Drawing.Point(0, 0);
      this.pnlNotifications.Name = "pnlNotifications";
      this.pnlNotifications.Size = new System.Drawing.Size(1476, 80);
      this.pnlNotifications.TabIndex = 1;
      // 
      // lblNotifyConfigSet
      // 
      this.lblNotifyConfigSet.AutoSize = true;
      this.lblNotifyConfigSet.Location = new System.Drawing.Point(831, 14);
      this.lblNotifyConfigSet.Name = "lblNotifyConfigSet";
      this.lblNotifyConfigSet.Size = new System.Drawing.Size(89, 13);
      this.lblNotifyConfigSet.TabIndex = 6;
      this.lblNotifyConfigSet.Text = "Notify Config Set:";
      // 
      // cboNotifyConfigSets
      // 
      this.cboNotifyConfigSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboNotifyConfigSets.FormattingEnabled = true;
      this.cboNotifyConfigSets.Location = new System.Drawing.Point(831, 30);
      this.cboNotifyConfigSets.Name = "cboNotifyConfigSets";
      this.cboNotifyConfigSets.Size = new System.Drawing.Size(284, 21);
      this.cboNotifyConfigSets.TabIndex = 5;
      this.cboNotifyConfigSets.Tag = "NotifyConfigSetChange";
      // 
      // btnGetNotifyConfigReport
      // 
      this.btnGetNotifyConfigReport.Location = new System.Drawing.Point(1121, 21);
      this.btnGetNotifyConfigReport.Name = "btnGetNotifyConfigReport";
      this.btnGetNotifyConfigReport.Size = new System.Drawing.Size(133, 35);
      this.btnGetNotifyConfigReport.TabIndex = 1;
      this.btnGetNotifyConfigReport.Tag = "GetNotifyConfigReport";
      this.btnGetNotifyConfigReport.Text = "Get Notify Config Report";
      this.btnGetNotifyConfigReport.UseVisualStyleBackColor = true;
      this.btnGetNotifyConfigReport.Click += new System.EventHandler(this.Action);
      // 
      // btnRefreshNotificationsTree
      // 
      this.btnRefreshNotificationsTree.Location = new System.Drawing.Point(11, 21);
      this.btnRefreshNotificationsTree.Name = "btnRefreshNotificationsTree";
      this.btnRefreshNotificationsTree.Size = new System.Drawing.Size(140, 37);
      this.btnRefreshNotificationsTree.TabIndex = 0;
      this.btnRefreshNotificationsTree.Tag = "RefreshNotificationsTree";
      this.btnRefreshNotificationsTree.Text = "Refresh Notifications Tree";
      this.btnRefreshNotificationsTree.UseVisualStyleBackColor = true;
      this.btnRefreshNotificationsTree.Click += new System.EventHandler(this.Action);
      // 
      // tabServicesSitesAppPools
      // 
      this.tabServicesSitesAppPools.Controls.Add(this.tabServSitesAppPools);
      this.tabServicesSitesAppPools.Location = new System.Drawing.Point(4, 22);
      this.tabServicesSitesAppPools.Name = "tabServicesSitesAppPools";
      this.tabServicesSitesAppPools.Size = new System.Drawing.Size(1476, 539);
      this.tabServicesSitesAppPools.TabIndex = 7;
      this.tabServicesSitesAppPools.Text = "Services / Sites / App Pools";
      this.tabServicesSitesAppPools.UseVisualStyleBackColor = true;
      // 
      // tabServSitesAppPools
      // 
      this.tabServSitesAppPools.Controls.Add(this.tabWinServices);
      this.tabServSitesAppPools.Controls.Add(this.tabWebSites);
      this.tabServSitesAppPools.Controls.Add(this.tabAppPools);
      this.tabServSitesAppPools.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabServSitesAppPools.Location = new System.Drawing.Point(0, 0);
      this.tabServSitesAppPools.Name = "tabServSitesAppPools";
      this.tabServSitesAppPools.SelectedIndex = 0;
      this.tabServSitesAppPools.Size = new System.Drawing.Size(1476, 539);
      this.tabServSitesAppPools.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabServSitesAppPools.TabIndex = 0;
      // 
      // tabWinServices
      // 
      this.tabWinServices.Controls.Add(this.gvWindowsServices);
      this.tabWinServices.Controls.Add(this.pnlWindowsServices);
      this.tabWinServices.Location = new System.Drawing.Point(4, 22);
      this.tabWinServices.Name = "tabWinServices";
      this.tabWinServices.Padding = new System.Windows.Forms.Padding(3);
      this.tabWinServices.Size = new System.Drawing.Size(1468, 513);
      this.tabWinServices.TabIndex = 0;
      this.tabWinServices.Text = "Win Services";
      this.tabWinServices.UseVisualStyleBackColor = true;
      // 
      // gvWindowsServices
      // 
      this.gvWindowsServices.AllowUserToAddRows = false;
      this.gvWindowsServices.AllowUserToDeleteRows = false;
      this.gvWindowsServices.AllowUserToResizeRows = false;
      this.gvWindowsServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvWindowsServices.ContextMenuStrip = this.ctxMenuWinServices;
      this.gvWindowsServices.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvWindowsServices.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvWindowsServices.Location = new System.Drawing.Point(3, 78);
      this.gvWindowsServices.MultiSelect = false;
      this.gvWindowsServices.Name = "gvWindowsServices";
      this.gvWindowsServices.RowHeadersVisible = false;
      this.gvWindowsServices.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvWindowsServices.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvWindowsServices.RowTemplate.Height = 19;
      this.gvWindowsServices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvWindowsServices.Size = new System.Drawing.Size(1462, 432);
      this.gvWindowsServices.TabIndex = 8;
      this.gvWindowsServices.Tag = "EditWindowsServices";
      this.gvWindowsServices.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridView_CellMouseUp);
      // 
      // ctxMenuWinServices
      // 
      this.ctxMenuWinServices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuWinServicesStart,
            this.ctxMenuWinServicesStop,
            this.pauseToolStripMenuItem,
            this.resumeToolStripMenuItem});
      this.ctxMenuWinServices.Name = "ctxMenuWinServices";
      this.ctxMenuWinServices.Size = new System.Drawing.Size(117, 92);
      this.ctxMenuWinServices.Tag = "CTXWinServices";
      this.ctxMenuWinServices.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuWinServices_Opening);
      // 
      // ctxMenuWinServicesStart
      // 
      this.ctxMenuWinServicesStart.Name = "ctxMenuWinServicesStart";
      this.ctxMenuWinServicesStart.Size = new System.Drawing.Size(116, 22);
      this.ctxMenuWinServicesStart.Tag = "StartWinService";
      this.ctxMenuWinServicesStart.Text = "Start";
      this.ctxMenuWinServicesStart.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuWinServicesStop
      // 
      this.ctxMenuWinServicesStop.Name = "ctxMenuWinServicesStop";
      this.ctxMenuWinServicesStop.Size = new System.Drawing.Size(116, 22);
      this.ctxMenuWinServicesStop.Tag = "StopWinService";
      this.ctxMenuWinServicesStop.Text = "Stop";
      this.ctxMenuWinServicesStop.Click += new System.EventHandler(this.Action);
      // 
      // pauseToolStripMenuItem
      // 
      this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
      this.pauseToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
      this.pauseToolStripMenuItem.Tag = "PauseWinService";
      this.pauseToolStripMenuItem.Text = "Pause";
      this.pauseToolStripMenuItem.Click += new System.EventHandler(this.Action);
      // 
      // resumeToolStripMenuItem
      // 
      this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
      this.resumeToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
      this.resumeToolStripMenuItem.Tag = "ResumeWinService";
      this.resumeToolStripMenuItem.Text = "Resume";
      this.resumeToolStripMenuItem.Click += new System.EventHandler(this.Action);
      // 
      // pnlWindowsServices
      // 
      this.pnlWindowsServices.Controls.Add(this.btnGetWindowsServices);
      this.pnlWindowsServices.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlWindowsServices.Location = new System.Drawing.Point(3, 3);
      this.pnlWindowsServices.Name = "pnlWindowsServices";
      this.pnlWindowsServices.Size = new System.Drawing.Size(1462, 75);
      this.pnlWindowsServices.TabIndex = 2;
      // 
      // btnGetWindowsServices
      // 
      this.btnGetWindowsServices.Location = new System.Drawing.Point(4, 20);
      this.btnGetWindowsServices.Name = "btnGetWindowsServices";
      this.btnGetWindowsServices.Size = new System.Drawing.Size(133, 35);
      this.btnGetWindowsServices.TabIndex = 1;
      this.btnGetWindowsServices.Tag = "GetWindowsServices";
      this.btnGetWindowsServices.Text = "Get Windows Services";
      this.btnGetWindowsServices.UseVisualStyleBackColor = true;
      this.btnGetWindowsServices.Click += new System.EventHandler(this.Action);
      // 
      // tabWebSites
      // 
      this.tabWebSites.Controls.Add(this.gvWebSites);
      this.tabWebSites.Controls.Add(this.pnlWebSites);
      this.tabWebSites.Location = new System.Drawing.Point(4, 22);
      this.tabWebSites.Name = "tabWebSites";
      this.tabWebSites.Padding = new System.Windows.Forms.Padding(3);
      this.tabWebSites.Size = new System.Drawing.Size(1468, 513);
      this.tabWebSites.TabIndex = 1;
      this.tabWebSites.Text = "Web Sites";
      this.tabWebSites.UseVisualStyleBackColor = true;
      // 
      // gvWebSites
      // 
      this.gvWebSites.AllowUserToAddRows = false;
      this.gvWebSites.AllowUserToDeleteRows = false;
      this.gvWebSites.AllowUserToResizeRows = false;
      this.gvWebSites.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvWebSites.ContextMenuStrip = this.ctxMenuWebSites;
      this.gvWebSites.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvWebSites.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvWebSites.Location = new System.Drawing.Point(3, 78);
      this.gvWebSites.MultiSelect = false;
      this.gvWebSites.Name = "gvWebSites";
      this.gvWebSites.RowHeadersVisible = false;
      this.gvWebSites.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvWebSites.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvWebSites.RowTemplate.Height = 19;
      this.gvWebSites.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvWebSites.Size = new System.Drawing.Size(1462, 432);
      this.gvWebSites.TabIndex = 8;
      this.gvWebSites.Tag = "EditWebSites";
      this.gvWebSites.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridView_CellMouseUp);
      // 
      // ctxMenuWebSites
      // 
      this.ctxMenuWebSites.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuWebSitesStart,
            this.ctxMenuWebSitesStop});
      this.ctxMenuWebSites.Name = "ctxMenuWebSites";
      this.ctxMenuWebSites.Size = new System.Drawing.Size(99, 48);
      this.ctxMenuWebSites.Tag = "CTXWebSite";
      this.ctxMenuWebSites.Opening += new System.ComponentModel.CancelEventHandler(this.ctxWebSites_Opening);
      // 
      // ctxMenuWebSitesStart
      // 
      this.ctxMenuWebSitesStart.Name = "ctxMenuWebSitesStart";
      this.ctxMenuWebSitesStart.Size = new System.Drawing.Size(98, 22);
      this.ctxMenuWebSitesStart.Tag = "StartWebSite";
      this.ctxMenuWebSitesStart.Text = "Start";
      this.ctxMenuWebSitesStart.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuWebSitesStop
      // 
      this.ctxMenuWebSitesStop.Name = "ctxMenuWebSitesStop";
      this.ctxMenuWebSitesStop.Size = new System.Drawing.Size(98, 22);
      this.ctxMenuWebSitesStop.Tag = "StopWebSite";
      this.ctxMenuWebSitesStop.Text = "Stop";
      this.ctxMenuWebSitesStop.Click += new System.EventHandler(this.Action);
      // 
      // pnlWebSites
      // 
      this.pnlWebSites.Controls.Add(this.btnGetWebSites);
      this.pnlWebSites.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlWebSites.Location = new System.Drawing.Point(3, 3);
      this.pnlWebSites.Name = "pnlWebSites";
      this.pnlWebSites.Size = new System.Drawing.Size(1462, 75);
      this.pnlWebSites.TabIndex = 2;
      // 
      // btnGetWebSites
      // 
      this.btnGetWebSites.Location = new System.Drawing.Point(4, 20);
      this.btnGetWebSites.Name = "btnGetWebSites";
      this.btnGetWebSites.Size = new System.Drawing.Size(133, 35);
      this.btnGetWebSites.TabIndex = 0;
      this.btnGetWebSites.Tag = "GetWebSites";
      this.btnGetWebSites.Text = "Get Web Sites";
      this.btnGetWebSites.UseVisualStyleBackColor = true;
      this.btnGetWebSites.Click += new System.EventHandler(this.Action);
      // 
      // tabAppPools
      // 
      this.tabAppPools.Controls.Add(this.gvAppPools);
      this.tabAppPools.Controls.Add(this.pnlAppPools);
      this.tabAppPools.Location = new System.Drawing.Point(4, 22);
      this.tabAppPools.Name = "tabAppPools";
      this.tabAppPools.Padding = new System.Windows.Forms.Padding(3);
      this.tabAppPools.Size = new System.Drawing.Size(1468, 513);
      this.tabAppPools.TabIndex = 2;
      this.tabAppPools.Text = "App Pools";
      this.tabAppPools.UseVisualStyleBackColor = true;
      // 
      // gvAppPools
      // 
      this.gvAppPools.AllowUserToAddRows = false;
      this.gvAppPools.AllowUserToDeleteRows = false;
      this.gvAppPools.AllowUserToResizeRows = false;
      this.gvAppPools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvAppPools.ContextMenuStrip = this.ctxMenuAppPools;
      this.gvAppPools.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvAppPools.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvAppPools.Location = new System.Drawing.Point(3, 78);
      this.gvAppPools.MultiSelect = false;
      this.gvAppPools.Name = "gvAppPools";
      this.gvAppPools.RowHeadersVisible = false;
      this.gvAppPools.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvAppPools.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvAppPools.RowTemplate.Height = 19;
      this.gvAppPools.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvAppPools.Size = new System.Drawing.Size(1462, 432);
      this.gvAppPools.TabIndex = 9;
      this.gvAppPools.Tag = "EditAppPools";
      this.gvAppPools.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridView_CellMouseUp);
      // 
      // ctxMenuAppPools
      // 
      this.ctxMenuAppPools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuAppPoolsStart,
            this.ctxMenuAppPoolsStop});
      this.ctxMenuAppPools.Name = "ctxMenuWebSites";
      this.ctxMenuAppPools.Size = new System.Drawing.Size(99, 48);
      this.ctxMenuAppPools.Tag = "CTXAppPool";
      this.ctxMenuAppPools.Opening += new System.ComponentModel.CancelEventHandler(this.ctxAppPools_Opening);
      // 
      // ctxMenuAppPoolsStart
      // 
      this.ctxMenuAppPoolsStart.Name = "ctxMenuAppPoolsStart";
      this.ctxMenuAppPoolsStart.Size = new System.Drawing.Size(98, 22);
      this.ctxMenuAppPoolsStart.Tag = "StartAppPool";
      this.ctxMenuAppPoolsStart.Text = "Start";
      this.ctxMenuAppPoolsStart.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuAppPoolsStop
      // 
      this.ctxMenuAppPoolsStop.Name = "ctxMenuAppPoolsStop";
      this.ctxMenuAppPoolsStop.Size = new System.Drawing.Size(98, 22);
      this.ctxMenuAppPoolsStop.Tag = "StopAppPool";
      this.ctxMenuAppPoolsStop.Text = "Stop";
      this.ctxMenuAppPoolsStop.Click += new System.EventHandler(this.Action);
      // 
      // pnlAppPools
      // 
      this.pnlAppPools.Controls.Add(this.btnGetAppPools);
      this.pnlAppPools.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlAppPools.Location = new System.Drawing.Point(3, 3);
      this.pnlAppPools.Name = "pnlAppPools";
      this.pnlAppPools.Size = new System.Drawing.Size(1462, 75);
      this.pnlAppPools.TabIndex = 3;
      // 
      // btnGetAppPools
      // 
      this.btnGetAppPools.Location = new System.Drawing.Point(4, 20);
      this.btnGetAppPools.Name = "btnGetAppPools";
      this.btnGetAppPools.Size = new System.Drawing.Size(133, 35);
      this.btnGetAppPools.TabIndex = 0;
      this.btnGetAppPools.Tag = "GetAppPools";
      this.btnGetAppPools.Text = "Get App Pools";
      this.btnGetAppPools.UseVisualStyleBackColor = true;
      this.btnGetAppPools.Click += new System.EventHandler(this.Action);
      // 
      // tabLogging
      // 
      this.tabLogging.Controls.Add(this.loggingSplitter);
      this.tabLogging.Controls.Add(this.pnlTopLogging);
      this.tabLogging.Location = new System.Drawing.Point(4, 22);
      this.tabLogging.Name = "tabLogging";
      this.tabLogging.Size = new System.Drawing.Size(1476, 539);
      this.tabLogging.TabIndex = 4;
      this.tabLogging.Text = "Logging";
      this.tabLogging.UseVisualStyleBackColor = true;
      // 
      // loggingSplitter
      // 
      this.loggingSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.loggingSplitter.Location = new System.Drawing.Point(0, 80);
      this.loggingSplitter.Name = "loggingSplitter";
      this.loggingSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // loggingSplitter.Panel1
      // 
      this.loggingSplitter.Panel1.Controls.Add(this.gvLogging);
      // 
      // loggingSplitter.Panel2
      // 
      this.loggingSplitter.Panel2.Controls.Add(this.txtLogDetails);
      this.loggingSplitter.Size = new System.Drawing.Size(1476, 459);
      this.loggingSplitter.SplitterDistance = 351;
      this.loggingSplitter.TabIndex = 10;
      // 
      // gvLogging
      // 
      this.gvLogging.AllowUserToAddRows = false;
      this.gvLogging.AllowUserToDeleteRows = false;
      this.gvLogging.AllowUserToResizeRows = false;
      this.gvLogging.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvLogging.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvLogging.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvLogging.Location = new System.Drawing.Point(0, 0);
      this.gvLogging.MultiSelect = false;
      this.gvLogging.Name = "gvLogging";
      this.gvLogging.RowHeadersVisible = false;
      this.gvLogging.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvLogging.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvLogging.RowTemplate.Height = 19;
      this.gvLogging.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvLogging.Size = new System.Drawing.Size(1476, 351);
      this.gvLogging.TabIndex = 8;
      this.gvLogging.Tag = "LogSelectionChange";
      this.gvLogging.SelectionChanged += new System.EventHandler(this.Action);
      // 
      // txtLogDetails
      // 
      this.txtLogDetails.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtLogDetails.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtLogDetails.Location = new System.Drawing.Point(0, 0);
      this.txtLogDetails.Multiline = true;
      this.txtLogDetails.Name = "txtLogDetails";
      this.txtLogDetails.ReadOnly = true;
      this.txtLogDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtLogDetails.Size = new System.Drawing.Size(1476, 104);
      this.txtLogDetails.TabIndex = 9;
      this.txtLogDetails.WordWrap = false;
      // 
      // pnlTopLogging
      // 
      this.pnlTopLogging.Controls.Add(this.lblLogMessage);
      this.pnlTopLogging.Controls.Add(this.txtLogMessage);
      this.pnlTopLogging.Controls.Add(this.chkDescending);
      this.pnlTopLogging.Controls.Add(this.lblRecordCount);
      this.pnlTopLogging.Controls.Add(this.lblEntities);
      this.pnlTopLogging.Controls.Add(this.lblEvents);
      this.pnlTopLogging.Controls.Add(this.lblModules);
      this.pnlTopLogging.Controls.Add(this.lblSeverityCode);
      this.pnlTopLogging.Controls.Add(this.cboRecordCount);
      this.pnlTopLogging.Controls.Add(this.cboLogEntities);
      this.pnlTopLogging.Controls.Add(this.cboLogEvents);
      this.pnlTopLogging.Controls.Add(this.cboLogModules);
      this.pnlTopLogging.Controls.Add(this.cboSeverityCode);
      this.pnlTopLogging.Controls.Add(this.btnRefreshLog);
      this.pnlTopLogging.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTopLogging.Location = new System.Drawing.Point(0, 0);
      this.pnlTopLogging.Name = "pnlTopLogging";
      this.pnlTopLogging.Size = new System.Drawing.Size(1476, 80);
      this.pnlTopLogging.TabIndex = 1;
      // 
      // lblLogMessage
      // 
      this.lblLogMessage.AutoSize = true;
      this.lblLogMessage.Location = new System.Drawing.Point(229, 37);
      this.lblLogMessage.Name = "lblLogMessage";
      this.lblLogMessage.Size = new System.Drawing.Size(53, 13);
      this.lblLogMessage.TabIndex = 16;
      this.lblLogMessage.Text = "Message:";
      // 
      // txtLogMessage
      // 
      this.txtLogMessage.Location = new System.Drawing.Point(229, 53);
      this.txtLogMessage.Name = "txtLogMessage";
      this.txtLogMessage.Size = new System.Drawing.Size(576, 20);
      this.txtLogMessage.TabIndex = 15;
      // 
      // chkDescending
      // 
      this.chkDescending.AutoSize = true;
      this.chkDescending.Checked = true;
      this.chkDescending.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkDescending.Location = new System.Drawing.Point(4, 4);
      this.chkDescending.Name = "chkDescending";
      this.chkDescending.Size = new System.Drawing.Size(83, 17);
      this.chkDescending.TabIndex = 14;
      this.chkDescending.Text = "Descending";
      this.chkDescending.UseVisualStyleBackColor = true;
      // 
      // lblRecordCount
      // 
      this.lblRecordCount.AutoSize = true;
      this.lblRecordCount.Location = new System.Drawing.Point(8, 37);
      this.lblRecordCount.Name = "lblRecordCount";
      this.lblRecordCount.Size = new System.Drawing.Size(76, 13);
      this.lblRecordCount.TabIndex = 13;
      this.lblRecordCount.Text = "Record Count:";
      // 
      // lblEntities
      // 
      this.lblEntities.AutoSize = true;
      this.lblEntities.Location = new System.Drawing.Point(1104, 37);
      this.lblEntities.Name = "lblEntities";
      this.lblEntities.Size = new System.Drawing.Size(44, 13);
      this.lblEntities.TabIndex = 12;
      this.lblEntities.Text = "Entities:";
      // 
      // lblEvents
      // 
      this.lblEvents.AutoSize = true;
      this.lblEvents.Location = new System.Drawing.Point(1023, 37);
      this.lblEvents.Name = "lblEvents";
      this.lblEvents.Size = new System.Drawing.Size(43, 13);
      this.lblEvents.TabIndex = 11;
      this.lblEvents.Text = "Events:";
      // 
      // lblModules
      // 
      this.lblModules.AutoSize = true;
      this.lblModules.Location = new System.Drawing.Point(811, 37);
      this.lblModules.Name = "lblModules";
      this.lblModules.Size = new System.Drawing.Size(50, 13);
      this.lblModules.TabIndex = 10;
      this.lblModules.Text = "Modules:";
      // 
      // lblSeverityCode
      // 
      this.lblSeverityCode.AutoSize = true;
      this.lblSeverityCode.Location = new System.Drawing.Point(147, 37);
      this.lblSeverityCode.Name = "lblSeverityCode";
      this.lblSeverityCode.Size = new System.Drawing.Size(76, 13);
      this.lblSeverityCode.TabIndex = 9;
      this.lblSeverityCode.Text = "Severity Code:";
      // 
      // cboRecordCount
      // 
      this.cboRecordCount.FormattingEnabled = true;
      this.cboRecordCount.Items.AddRange(new object[] {
            "",
            "100",
            "500",
            "1000",
            "9999"});
      this.cboRecordCount.Location = new System.Drawing.Point(8, 53);
      this.cboRecordCount.Name = "cboRecordCount";
      this.cboRecordCount.Size = new System.Drawing.Size(74, 21);
      this.cboRecordCount.TabIndex = 8;
      this.cboRecordCount.Text = "100";
      this.cboRecordCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboRecordCount_KeyPress);
      // 
      // cboLogEntities
      // 
      this.cboLogEntities.FormattingEnabled = true;
      this.cboLogEntities.Items.AddRange(new object[] {
            " "});
      this.cboLogEntities.Location = new System.Drawing.Point(1104, 53);
      this.cboLogEntities.Name = "cboLogEntities";
      this.cboLogEntities.Size = new System.Drawing.Size(226, 21);
      this.cboLogEntities.TabIndex = 7;
      this.cboLogEntities.Tag = "";
      this.cboLogEntities.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.logFilters_KeyPress);
      // 
      // cboLogEvents
      // 
      this.cboLogEvents.FormattingEnabled = true;
      this.cboLogEvents.Items.AddRange(new object[] {
            " "});
      this.cboLogEvents.Location = new System.Drawing.Point(1023, 53);
      this.cboLogEvents.Name = "cboLogEvents";
      this.cboLogEvents.Size = new System.Drawing.Size(76, 21);
      this.cboLogEvents.TabIndex = 5;
      this.cboLogEvents.Tag = "";
      this.cboLogEvents.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.logFilters_KeyPress);
      // 
      // cboLogModules
      // 
      this.cboLogModules.FormattingEnabled = true;
      this.cboLogModules.Items.AddRange(new object[] {
            " "});
      this.cboLogModules.Location = new System.Drawing.Point(811, 53);
      this.cboLogModules.Name = "cboLogModules";
      this.cboLogModules.Size = new System.Drawing.Size(209, 21);
      this.cboLogModules.TabIndex = 3;
      this.cboLogModules.Tag = "";
      this.cboLogModules.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.logFilters_KeyPress);
      // 
      // cboSeverityCode
      // 
      this.cboSeverityCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboSeverityCode.FormattingEnabled = true;
      this.cboSeverityCode.Items.AddRange(new object[] {
            "ALL",
            "INFO",
            "ALL BUT INFO",
            "WARN",
            "MINR",
            "MAJR",
            "SEVR",
            "DIAG",
            "AUDT",
            "TRAC"});
      this.cboSeverityCode.Location = new System.Drawing.Point(147, 53);
      this.cboSeverityCode.Name = "cboSeverityCode";
      this.cboSeverityCode.Size = new System.Drawing.Size(76, 21);
      this.cboSeverityCode.TabIndex = 1;
      // 
      // btnRefreshLog
      // 
      this.btnRefreshLog.Location = new System.Drawing.Point(1335, 20);
      this.btnRefreshLog.Name = "btnRefreshLog";
      this.btnRefreshLog.Size = new System.Drawing.Size(133, 35);
      this.btnRefreshLog.TabIndex = 0;
      this.btnRefreshLog.Tag = "RefreshLog";
      this.btnRefreshLog.Text = "Refresh Log";
      this.btnRefreshLog.UseVisualStyleBackColor = true;
      this.btnRefreshLog.Click += new System.EventHandler(this.Action);
      // 
      // tabIdentifiers
      // 
      this.tabIdentifiers.Controls.Add(this.gvIdentifiers);
      this.tabIdentifiers.Controls.Add(this.pnlTopIdentifiers);
      this.tabIdentifiers.Location = new System.Drawing.Point(4, 22);
      this.tabIdentifiers.Name = "tabIdentifiers";
      this.tabIdentifiers.Size = new System.Drawing.Size(1476, 539);
      this.tabIdentifiers.TabIndex = 6;
      this.tabIdentifiers.Text = "Identifiers";
      this.tabIdentifiers.UseVisualStyleBackColor = true;
      // 
      // gvIdentifiers
      // 
      this.gvIdentifiers.AllowUserToAddRows = false;
      this.gvIdentifiers.AllowUserToDeleteRows = false;
      this.gvIdentifiers.AllowUserToResizeRows = false;
      this.gvIdentifiers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvIdentifiers.ContextMenuStrip = this.ctxMenuIdentifiers;
      this.gvIdentifiers.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvIdentifiers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvIdentifiers.Location = new System.Drawing.Point(0, 80);
      this.gvIdentifiers.MultiSelect = false;
      this.gvIdentifiers.Name = "gvIdentifiers";
      this.gvIdentifiers.RowHeadersVisible = false;
      this.gvIdentifiers.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvIdentifiers.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvIdentifiers.RowTemplate.Height = 19;
      this.gvIdentifiers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvIdentifiers.Size = new System.Drawing.Size(1476, 459);
      this.gvIdentifiers.TabIndex = 8;
      this.gvIdentifiers.Tag = "UpdateIdentifier";
      this.gvIdentifiers.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridView_CellMouseUp);
      this.gvIdentifiers.DoubleClick += new System.EventHandler(this.Action);
      // 
      // ctxMenuIdentifiers
      // 
      this.ctxMenuIdentifiers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuIdentifiersMigrate});
      this.ctxMenuIdentifiers.Name = "ctxMenuScheduledTasks";
      this.ctxMenuIdentifiers.Size = new System.Drawing.Size(185, 26);
      this.ctxMenuIdentifiers.Opening += new System.ComponentModel.CancelEventHandler(this.gridContextMenu_Opening);
      // 
      // ctxMenuIdentifiersMigrate
      // 
      this.ctxMenuIdentifiersMigrate.Name = "ctxMenuIdentifiersMigrate";
      this.ctxMenuIdentifiersMigrate.Size = new System.Drawing.Size(184, 22);
      this.ctxMenuIdentifiersMigrate.Tag = "MigrateIdentifier";
      this.ctxMenuIdentifiersMigrate.Text = "Migrate to [new env]";
      this.ctxMenuIdentifiersMigrate.Click += new System.EventHandler(this.Action);
      // 
      // pnlTopIdentifiers
      // 
      this.pnlTopIdentifiers.Controls.Add(this.lblIdentifierDescription);
      this.pnlTopIdentifiers.Controls.Add(this.txtIdentifierDescription);
      this.pnlTopIdentifiers.Controls.Add(this.btnNewIdentifier);
      this.pnlTopIdentifiers.Controls.Add(this.lblIdentifier);
      this.pnlTopIdentifiers.Controls.Add(this.cboIdentifiers);
      this.pnlTopIdentifiers.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTopIdentifiers.Location = new System.Drawing.Point(0, 0);
      this.pnlTopIdentifiers.Name = "pnlTopIdentifiers";
      this.pnlTopIdentifiers.Size = new System.Drawing.Size(1476, 80);
      this.pnlTopIdentifiers.TabIndex = 2;
      // 
      // lblIdentifierDescription
      // 
      this.lblIdentifierDescription.AutoSize = true;
      this.lblIdentifierDescription.Location = new System.Drawing.Point(179, 21);
      this.lblIdentifierDescription.Name = "lblIdentifierDescription";
      this.lblIdentifierDescription.Size = new System.Drawing.Size(60, 13);
      this.lblIdentifierDescription.TabIndex = 12;
      this.lblIdentifierDescription.Text = "Description";
      // 
      // txtIdentifierDescription
      // 
      this.txtIdentifierDescription.Location = new System.Drawing.Point(179, 37);
      this.txtIdentifierDescription.Name = "txtIdentifierDescription";
      this.txtIdentifierDescription.Size = new System.Drawing.Size(294, 20);
      this.txtIdentifierDescription.TabIndex = 11;
      this.txtIdentifierDescription.Tag = "IdentifierDescChange";
      this.txtIdentifierDescription.TextChanged += new System.EventHandler(this.Action);
      // 
      // btnNewIdentifier
      // 
      this.btnNewIdentifier.Enabled = false;
      this.btnNewIdentifier.Location = new System.Drawing.Point(1099, 22);
      this.btnNewIdentifier.Name = "btnNewIdentifier";
      this.btnNewIdentifier.Size = new System.Drawing.Size(144, 35);
      this.btnNewIdentifier.TabIndex = 10;
      this.btnNewIdentifier.Tag = "InsertIdentifier";
      this.btnNewIdentifier.Text = "New Identifier";
      this.btnNewIdentifier.UseVisualStyleBackColor = true;
      this.btnNewIdentifier.Click += new System.EventHandler(this.Action);
      // 
      // lblIdentifier
      // 
      this.lblIdentifier.AutoSize = true;
      this.lblIdentifier.Location = new System.Drawing.Point(22, 21);
      this.lblIdentifier.Name = "lblIdentifier";
      this.lblIdentifier.Size = new System.Drawing.Size(50, 13);
      this.lblIdentifier.TabIndex = 9;
      this.lblIdentifier.Text = "Identifier:";
      // 
      // cboIdentifiers
      // 
      this.cboIdentifiers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboIdentifiers.FormattingEnabled = true;
      this.cboIdentifiers.Items.AddRange(new object[] {
            "Modules",
            "Events",
            "Entities"});
      this.cboIdentifiers.Location = new System.Drawing.Point(22, 37);
      this.cboIdentifiers.Name = "cboIdentifiers";
      this.cboIdentifiers.Size = new System.Drawing.Size(86, 21);
      this.cboIdentifiers.TabIndex = 1;
      this.cboIdentifiers.Tag = "IdentifierSelectionChange";
      this.cboIdentifiers.SelectedIndexChanged += new System.EventHandler(this.Action);
      // 
      // tabScheduledRuns
      // 
      this.tabScheduledRuns.Controls.Add(this.pnlScheduledRunsBottom);
      this.tabScheduledRuns.Controls.Add(this.pnlScheduledRunsTop);
      this.tabScheduledRuns.Location = new System.Drawing.Point(4, 22);
      this.tabScheduledRuns.Name = "tabScheduledRuns";
      this.tabScheduledRuns.Size = new System.Drawing.Size(1476, 539);
      this.tabScheduledRuns.TabIndex = 8;
      this.tabScheduledRuns.Text = "Scheduled Runs";
      this.tabScheduledRuns.UseVisualStyleBackColor = true;
      // 
      // pnlScheduledRunsBottom
      // 
      this.pnlScheduledRunsBottom.Controls.Add(this.gvScheduledRuns);
      this.pnlScheduledRunsBottom.Controls.Add(this.clbScheduledRuns);
      this.pnlScheduledRunsBottom.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlScheduledRunsBottom.Location = new System.Drawing.Point(0, 80);
      this.pnlScheduledRunsBottom.Name = "pnlScheduledRunsBottom";
      this.pnlScheduledRunsBottom.Size = new System.Drawing.Size(1476, 459);
      this.pnlScheduledRunsBottom.TabIndex = 8;
      // 
      // gvScheduledRuns
      // 
      this.gvScheduledRuns.AllowUserToAddRows = false;
      this.gvScheduledRuns.AllowUserToDeleteRows = false;
      this.gvScheduledRuns.AllowUserToResizeRows = false;
      this.gvScheduledRuns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvScheduledRuns.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvScheduledRuns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvScheduledRuns.Location = new System.Drawing.Point(216, 0);
      this.gvScheduledRuns.MultiSelect = false;
      this.gvScheduledRuns.Name = "gvScheduledRuns";
      this.gvScheduledRuns.RowHeadersVisible = false;
      this.gvScheduledRuns.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvScheduledRuns.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvScheduledRuns.RowTemplate.Height = 19;
      this.gvScheduledRuns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvScheduledRuns.Size = new System.Drawing.Size(1260, 459);
      this.gvScheduledRuns.TabIndex = 9;
      this.gvScheduledRuns.Tag = "";
      // 
      // clbScheduledRuns
      // 
      this.clbScheduledRuns.CheckOnClick = true;
      this.clbScheduledRuns.Dock = System.Windows.Forms.DockStyle.Left;
      this.clbScheduledRuns.FormattingEnabled = true;
      this.clbScheduledRuns.Location = new System.Drawing.Point(0, 0);
      this.clbScheduledRuns.Name = "clbScheduledRuns";
      this.clbScheduledRuns.Size = new System.Drawing.Size(216, 459);
      this.clbScheduledRuns.TabIndex = 8;
      // 
      // pnlScheduledRunsTop
      // 
      this.pnlScheduledRunsTop.Controls.Add(this.btnViewScheduledRuns);
      this.pnlScheduledRunsTop.Controls.Add(this.lblIntervalEndDateTime);
      this.pnlScheduledRunsTop.Controls.Add(this.lblIntervalStartDateTime);
      this.pnlScheduledRunsTop.Controls.Add(this.dtpIntervalEndTime);
      this.pnlScheduledRunsTop.Controls.Add(this.dtpIntervalStartTime);
      this.pnlScheduledRunsTop.Controls.Add(this.dtpIntervalEndDate);
      this.pnlScheduledRunsTop.Controls.Add(this.dtpIntervalStartDate);
      this.pnlScheduledRunsTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlScheduledRunsTop.Location = new System.Drawing.Point(0, 0);
      this.pnlScheduledRunsTop.Name = "pnlScheduledRunsTop";
      this.pnlScheduledRunsTop.Size = new System.Drawing.Size(1476, 80);
      this.pnlScheduledRunsTop.TabIndex = 7;
      // 
      // btnViewScheduledRuns
      // 
      this.btnViewScheduledRuns.Location = new System.Drawing.Point(284, 22);
      this.btnViewScheduledRuns.Name = "btnViewScheduledRuns";
      this.btnViewScheduledRuns.Size = new System.Drawing.Size(144, 35);
      this.btnViewScheduledRuns.TabIndex = 10;
      this.btnViewScheduledRuns.Tag = "ViewScheduledRuns";
      this.btnViewScheduledRuns.Text = "View Scheduled Runs";
      this.btnViewScheduledRuns.UseVisualStyleBackColor = true;
      this.btnViewScheduledRuns.Click += new System.EventHandler(this.Action);
      // 
      // lblIntervalEndDateTime
      // 
      this.lblIntervalEndDateTime.AutoSize = true;
      this.lblIntervalEndDateTime.Location = new System.Drawing.Point(115, 8);
      this.lblIntervalEndDateTime.Name = "lblIntervalEndDateTime";
      this.lblIntervalEndDateTime.Size = new System.Drawing.Size(80, 13);
      this.lblIntervalEndDateTime.TabIndex = 8;
      this.lblIntervalEndDateTime.Text = "End Date/Time";
      // 
      // lblIntervalStartDateTime
      // 
      this.lblIntervalStartDateTime.AutoSize = true;
      this.lblIntervalStartDateTime.Location = new System.Drawing.Point(8, 8);
      this.lblIntervalStartDateTime.Name = "lblIntervalStartDateTime";
      this.lblIntervalStartDateTime.Size = new System.Drawing.Size(83, 13);
      this.lblIntervalStartDateTime.TabIndex = 7;
      this.lblIntervalStartDateTime.Text = "Start Date/Time";
      // 
      // dtpIntervalEndTime
      // 
      this.dtpIntervalEndTime.CustomFormat = "hh:mm tt";
      this.dtpIntervalEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpIntervalEndTime.Location = new System.Drawing.Point(115, 50);
      this.dtpIntervalEndTime.Name = "dtpIntervalEndTime";
      this.dtpIntervalEndTime.ShowUpDown = true;
      this.dtpIntervalEndTime.Size = new System.Drawing.Size(101, 20);
      this.dtpIntervalEndTime.TabIndex = 6;
      // 
      // dtpIntervalStartTime
      // 
      this.dtpIntervalStartTime.CustomFormat = "hh:mm tt";
      this.dtpIntervalStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpIntervalStartTime.Location = new System.Drawing.Point(8, 50);
      this.dtpIntervalStartTime.Name = "dtpIntervalStartTime";
      this.dtpIntervalStartTime.ShowUpDown = true;
      this.dtpIntervalStartTime.Size = new System.Drawing.Size(101, 20);
      this.dtpIntervalStartTime.TabIndex = 5;
      // 
      // dtpIntervalEndDate
      // 
      this.dtpIntervalEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dtpIntervalEndDate.Location = new System.Drawing.Point(115, 24);
      this.dtpIntervalEndDate.Name = "dtpIntervalEndDate";
      this.dtpIntervalEndDate.Size = new System.Drawing.Size(101, 20);
      this.dtpIntervalEndDate.TabIndex = 4;
      // 
      // dtpIntervalStartDate
      // 
      this.dtpIntervalStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dtpIntervalStartDate.Location = new System.Drawing.Point(8, 24);
      this.dtpIntervalStartDate.Name = "dtpIntervalStartDate";
      this.dtpIntervalStartDate.Size = new System.Drawing.Size(101, 20);
      this.dtpIntervalStartDate.TabIndex = 3;
      // 
      // ctxMenuNotifications
      // 
      this.ctxMenuNotifications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuNotificationsAddNewObject,
            this.ctxMenuNotificationsDelete});
      this.ctxMenuNotifications.Name = "ctxMenuNotifications";
      this.ctxMenuNotifications.Size = new System.Drawing.Size(168, 48);
      // 
      // ctxMenuNotificationsAddNewObject
      // 
      this.ctxMenuNotificationsAddNewObject.Name = "ctxMenuNotificationsAddNewObject";
      this.ctxMenuNotificationsAddNewObject.Size = new System.Drawing.Size(167, 22);
      this.ctxMenuNotificationsAddNewObject.Tag = "NewNotificationObject";
      this.ctxMenuNotificationsAddNewObject.Text = "Add New [object]";
      this.ctxMenuNotificationsAddNewObject.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuNotificationsDelete
      // 
      this.ctxMenuNotificationsDelete.Name = "ctxMenuNotificationsDelete";
      this.ctxMenuNotificationsDelete.Size = new System.Drawing.Size(167, 22);
      this.ctxMenuNotificationsDelete.Tag = "DeleteNotificationObject";
      this.ctxMenuNotificationsDelete.Text = "Delete [object]";
      this.ctxMenuNotificationsDelete.Click += new System.EventHandler(this.Action);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1484, 679);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "OpsManager - 1.0.0.0";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabScheduledTasks.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvScheduledTasks)).EndInit();
      this.ctxMenuScheduledTasks.ResumeLayout(false);
      this.pnlScheduledTasks.ResumeLayout(false);
      this.pnlScheduledTasks.PerformLayout();
      this.tabNotifications.ResumeLayout(false);
      this.pnlNotifications.ResumeLayout(false);
      this.pnlNotifications.PerformLayout();
      this.tabServicesSitesAppPools.ResumeLayout(false);
      this.tabServSitesAppPools.ResumeLayout(false);
      this.tabWinServices.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvWindowsServices)).EndInit();
      this.ctxMenuWinServices.ResumeLayout(false);
      this.pnlWindowsServices.ResumeLayout(false);
      this.tabWebSites.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvWebSites)).EndInit();
      this.ctxMenuWebSites.ResumeLayout(false);
      this.pnlWebSites.ResumeLayout(false);
      this.tabAppPools.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvAppPools)).EndInit();
      this.ctxMenuAppPools.ResumeLayout(false);
      this.pnlAppPools.ResumeLayout(false);
      this.tabLogging.ResumeLayout(false);
      this.loggingSplitter.Panel1.ResumeLayout(false);
      this.loggingSplitter.Panel2.ResumeLayout(false);
      this.loggingSplitter.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.loggingSplitter)).EndInit();
      this.loggingSplitter.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvLogging)).EndInit();
      this.pnlTopLogging.ResumeLayout(false);
      this.pnlTopLogging.PerformLayout();
      this.tabIdentifiers.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvIdentifiers)).EndInit();
      this.ctxMenuIdentifiers.ResumeLayout(false);
      this.pnlTopIdentifiers.ResumeLayout(false);
      this.pnlTopIdentifiers.PerformLayout();
      this.tabScheduledRuns.ResumeLayout(false);
      this.pnlScheduledRunsBottom.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvScheduledRuns)).EndInit();
      this.pnlScheduledRunsTop.ResumeLayout(false);
      this.pnlScheduledRunsTop.PerformLayout();
      this.ctxMenuNotifications.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabScheduledTasks;
    private System.Windows.Forms.DataGridView gvScheduledTasks;
    private System.Windows.Forms.Button btnNewScheduledTask;
    private System.Windows.Forms.ContextMenuStrip ctxMenuScheduledTasks;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuScheduledTaskDelete;
    private System.Windows.Forms.Button btnMaintainTaskParameters;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.ComboBox cboEnvironment;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuScheduledTaskDisplayTaskReport;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuScheduledTaskMigrateTask;
    private System.Windows.Forms.ContextMenuStrip ctxMenuWebSites;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuWebSitesStart;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuWebSitesStop;
    private System.Windows.Forms.ContextMenuStrip ctxMenuWinServices;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuWinServicesStart;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuWinServicesStop;
    private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem resumeToolStripMenuItem;
    private System.Windows.Forms.TabPage tabLogging;
    private System.Windows.Forms.DataGridView gvLogging;
    private System.Windows.Forms.Panel pnlTopLogging;
    private System.Windows.Forms.Button btnRefreshLog;
    private System.Windows.Forms.SplitContainer loggingSplitter;
    private System.Windows.Forms.TextBox txtLogDetails;
    private System.Windows.Forms.Panel pnlScheduledTasks;
    private System.Windows.Forms.Button btnGetScheduledTasks;
    private System.Windows.Forms.Button btnGetTasksReport;
    private System.Windows.Forms.ComboBox cboScheduleInterval;
    private System.Windows.Forms.Label lblScheduleInterval;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuScheduledTaskViewRunHistory;
    private System.Windows.Forms.TabPage tabNotifications;
    private System.Windows.Forms.TreeView treeViewNotifications;
    private System.Windows.Forms.Panel pnlNotifications;
    private System.Windows.Forms.Button btnRefreshNotificationsTree;
    private System.Windows.Forms.Panel pnlNotificationHolder;
    private System.Windows.Forms.ContextMenuStrip ctxMenuNotifications;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuNotificationsAddNewObject;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuNotificationsDelete;
    private System.Windows.Forms.ImageList imageListNotifications;
    private System.Windows.Forms.Label lblNotifyConfigSet;
    private System.Windows.Forms.ComboBox cboNotifyConfigSets;
    private System.Windows.Forms.Button btnGetNotifyConfigReport;
    private System.Windows.Forms.CheckBox chkDescending;
    private System.Windows.Forms.Label lblRecordCount;
    private System.Windows.Forms.Label lblEntities;
    private System.Windows.Forms.Label lblEvents;
    private System.Windows.Forms.Label lblModules;
    private System.Windows.Forms.Label lblSeverityCode;
    private System.Windows.Forms.ComboBox cboRecordCount;
    private System.Windows.Forms.ComboBox cboLogEntities;
    private System.Windows.Forms.ComboBox cboLogEvents;
    private System.Windows.Forms.ComboBox cboLogModules;
    private System.Windows.Forms.ComboBox cboSeverityCode;
    private System.Windows.Forms.TabPage tabIdentifiers;
    private System.Windows.Forms.DataGridView gvIdentifiers;
    private System.Windows.Forms.Panel pnlTopIdentifiers;
    private System.Windows.Forms.Label lblIdentifier;
    private System.Windows.Forms.ComboBox cboIdentifiers;
    private System.Windows.Forms.Button btnNewIdentifier;
    private System.Windows.Forms.ContextMenuStrip ctxMenuIdentifiers;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuIdentifiersMigrate;
    private System.Windows.Forms.Label lblIdentifierDescription;
    private System.Windows.Forms.TextBox txtIdentifierDescription;
    private System.Windows.Forms.TabPage tabServicesSitesAppPools;
    private System.Windows.Forms.TabControl tabServSitesAppPools;
    private System.Windows.Forms.TabPage tabWinServices;
    private System.Windows.Forms.DataGridView gvWindowsServices;
    private System.Windows.Forms.Panel pnlWindowsServices;
    private System.Windows.Forms.Button btnGetWindowsServices;
    private System.Windows.Forms.TabPage tabWebSites;
    private System.Windows.Forms.DataGridView gvWebSites;
    private System.Windows.Forms.Panel pnlWebSites;
    private System.Windows.Forms.Button btnGetWebSites;
    private System.Windows.Forms.TabPage tabAppPools;
    private System.Windows.Forms.DataGridView gvAppPools;
    private System.Windows.Forms.Panel pnlAppPools;
    private System.Windows.Forms.Button btnGetAppPools;
    private System.Windows.Forms.ContextMenuStrip ctxMenuAppPools;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuAppPoolsStart;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuAppPoolsStop;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuScheduledTaskSetDryRunOn;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuScheduledTaskSetDryRunOff;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuScheduledTaskSetActiveOn;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuScheduledTaskSetActiveOff;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuScheduledTaskSetRunUntilOverrideOn;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuScheduledTaskSetRunUntilOverrideOff;
    private System.Windows.Forms.TextBox txtLogMessage;
    private System.Windows.Forms.Label lblLogMessage;
    private System.Windows.Forms.TabPage tabScheduledRuns;
    private System.Windows.Forms.Panel pnlScheduledRunsBottom;
    private System.Windows.Forms.Panel pnlScheduledRunsTop;
    private System.Windows.Forms.DateTimePicker dtpIntervalEndDate;
    private System.Windows.Forms.DateTimePicker dtpIntervalStartDate;
    private System.Windows.Forms.DateTimePicker dtpIntervalEndTime;
    private System.Windows.Forms.DateTimePicker dtpIntervalStartTime;
    private System.Windows.Forms.Label lblIntervalEndDateTime;
    private System.Windows.Forms.Label lblIntervalStartDateTime;
    private System.Windows.Forms.Button btnViewScheduledRuns;
    private System.Windows.Forms.CheckedListBox clbScheduledRuns;
    private System.Windows.Forms.DataGridView gvScheduledRuns;
  }
}


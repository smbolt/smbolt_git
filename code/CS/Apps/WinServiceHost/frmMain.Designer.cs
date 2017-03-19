namespace Org.WinServiceHost
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuToggleShowControls = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsDumpTrace = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsRunConfigWizard = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsShowServiceAlert = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsConfigureScheduledTasks = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsGetNextNow = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsRefreshTaskList = new System.Windows.Forms.ToolStripMenuItem();
      this.panelTop = new System.Windows.Forms.Panel();
      this.pnlToolbar = new System.Windows.Forms.Panel();
      this.toolStripMain = new System.Windows.Forms.ToolStrip();
      this.tbtnStartService = new System.Windows.Forms.ToolStripButton();
      this.tbtnPauseService = new System.Windows.Forms.ToolStripButton();
      this.tbtnResumeService = new System.Windows.Forms.ToolStripButton();
      this.tbtnStopService = new System.Windows.Forms.ToolStripButton();
      this.tbtnSpacer = new System.Windows.Forms.ToolStripLabel();
      this.pbLogo = new System.Windows.Forms.PictureBox();
      this.pnlBottom = new System.Windows.Forms.Panel();
      this.lblStatus = new System.Windows.Forms.Label();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.btnErrorTask = new System.Windows.Forms.Button();
      this.btnCancelTask = new System.Windows.Forms.Button();
      this.btnStartTask = new System.Windows.Forms.Button();
      this.progTask = new System.Windows.Forms.ProgressBar();
      this.txtParms = new System.Windows.Forms.TextBox();
      this.lblParms = new System.Windows.Forms.Label();
      this.cboFunction = new System.Windows.Forms.ComboBox();
      this.btnTestNotifications = new System.Windows.Forms.Button();
      this.btnTestLogWrite = new System.Windows.Forms.Button();
      this.btnHideControls = new System.Windows.Forms.Button();
      this.btnClearReport = new System.Windows.Forms.Button();
      this.btnRunFunction = new System.Windows.Forms.Button();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageReport = new System.Windows.Forms.TabPage();
      this.txtReport = new System.Windows.Forms.TextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.mnuMain.SuspendLayout();
      this.panelTop.SuspendLayout();
      this.pnlToolbar.SuspendLayout();
      this.toolStripMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
      this.pnlBottom.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageReport.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuOptions});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1029, 24);
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
      this.mnuFileExit.Tag = "CloseForm";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      // 
      // mnuOptions
      // 
      this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToggleShowControls,
            this.mnuOptionsDumpTrace,
            this.mnuOptionsRunConfigWizard,
            this.mnuOptionsShowServiceAlert,
            this.mnuOptionsConfigureScheduledTasks,
            this.mnuOptionsGetNextNow,
            this.mnuOptionsRefreshTaskList});
      this.mnuOptions.Name = "mnuOptions";
      this.mnuOptions.Size = new System.Drawing.Size(61, 20);
      this.mnuOptions.Text = "&Options";
      // 
      // mnuToggleShowControls
      // 
      this.mnuToggleShowControls.Name = "mnuToggleShowControls";
      this.mnuToggleShowControls.Size = new System.Drawing.Size(216, 22);
      this.mnuToggleShowControls.Tag = "ToggleShowControls";
      this.mnuToggleShowControls.Text = "&Show Controls";
      this.mnuToggleShowControls.Click += new System.EventHandler(this.Action);
      // 
      // mnuOptionsDumpTrace
      // 
      this.mnuOptionsDumpTrace.Name = "mnuOptionsDumpTrace";
      this.mnuOptionsDumpTrace.Size = new System.Drawing.Size(216, 22);
      this.mnuOptionsDumpTrace.Tag = "DumpTrace";
      this.mnuOptionsDumpTrace.Text = "&Dump Trace";
      this.mnuOptionsDumpTrace.Click += new System.EventHandler(this.Action);
      // 
      // mnuOptionsRunConfigWizard
      // 
      this.mnuOptionsRunConfigWizard.Name = "mnuOptionsRunConfigWizard";
      this.mnuOptionsRunConfigWizard.Size = new System.Drawing.Size(216, 22);
      this.mnuOptionsRunConfigWizard.Tag = "RunConfigWizard";
      this.mnuOptionsRunConfigWizard.Text = "Run Configuration &Wizard";
      this.mnuOptionsRunConfigWizard.Click += new System.EventHandler(this.Action);
      // 
      // mnuOptionsShowServiceAlert
      // 
      this.mnuOptionsShowServiceAlert.Name = "mnuOptionsShowServiceAlert";
      this.mnuOptionsShowServiceAlert.Size = new System.Drawing.Size(216, 22);
      this.mnuOptionsShowServiceAlert.Tag = "ShowServiceAlert";
      this.mnuOptionsShowServiceAlert.Text = "Show Service&Alert";
      this.mnuOptionsShowServiceAlert.Click += new System.EventHandler(this.Action);
      // 
      // mnuOptionsConfigureScheduledTasks
      // 
      this.mnuOptionsConfigureScheduledTasks.Name = "mnuOptionsConfigureScheduledTasks";
      this.mnuOptionsConfigureScheduledTasks.Size = new System.Drawing.Size(216, 22);
      this.mnuOptionsConfigureScheduledTasks.Tag = "ConfigureScheduledTasks";
      this.mnuOptionsConfigureScheduledTasks.Text = "Configure Scheduled Tasks";
      this.mnuOptionsConfigureScheduledTasks.Click += new System.EventHandler(this.Action);
      // 
      // mnuOptionsGetNextNow
      // 
      this.mnuOptionsGetNextNow.Name = "mnuOptionsGetNextNow";
      this.mnuOptionsGetNextNow.Size = new System.Drawing.Size(216, 22);
      this.mnuOptionsGetNextNow.Tag = "GetNextNow";
      this.mnuOptionsGetNextNow.Text = "Get Next Now";
      this.mnuOptionsGetNextNow.Click += new System.EventHandler(this.Action);
      // 
      // mnuOptionsRefreshTaskList
      // 
      this.mnuOptionsRefreshTaskList.Name = "mnuOptionsRefreshTaskList";
      this.mnuOptionsRefreshTaskList.Size = new System.Drawing.Size(216, 22);
      this.mnuOptionsRefreshTaskList.Tag = "RefreshTaskList";
      this.mnuOptionsRefreshTaskList.Text = "Refresh Task List";
      this.mnuOptionsRefreshTaskList.Click += new System.EventHandler(this.Action);
      // 
      // panelTop
      // 
      this.panelTop.BackColor = System.Drawing.Color.White;
      this.panelTop.Controls.Add(this.pnlToolbar);
      this.panelTop.Controls.Add(this.pbLogo);
      this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelTop.Location = new System.Drawing.Point(0, 24);
      this.panelTop.Name = "panelTop";
      this.panelTop.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
      this.panelTop.Size = new System.Drawing.Size(1029, 59);
      this.panelTop.TabIndex = 8;
      // 
      // pnlToolbar
      // 
      this.pnlToolbar.Controls.Add(this.toolStripMain);
      this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Left;
      this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
      this.pnlToolbar.Name = "pnlToolbar";
      this.pnlToolbar.Size = new System.Drawing.Size(629, 59);
      this.pnlToolbar.TabIndex = 0;
      // 
      // toolStripMain
      // 
      this.toolStripMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.toolStripMain.AutoSize = false;
      this.toolStripMain.BackColor = System.Drawing.Color.White;
      this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStripMain.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripMain.ImageScalingSize = new System.Drawing.Size(32, 32);
      this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnStartService,
            this.tbtnPauseService,
            this.tbtnResumeService,
            this.tbtnStopService,
            this.tbtnSpacer});
      this.toolStripMain.Location = new System.Drawing.Point(-2, -4);
      this.toolStripMain.Name = "toolStripMain";
      this.toolStripMain.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
      this.toolStripMain.Size = new System.Drawing.Size(652, 67);
      this.toolStripMain.TabIndex = 0;
      // 
      // tbtnStartService
      // 
      this.tbtnStartService.AutoSize = false;
      this.tbtnStartService.BackColor = System.Drawing.Color.Gainsboro;
      this.tbtnStartService.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnStartService.Image = ((System.Drawing.Image)(resources.GetObject("tbtnStartService.Image")));
      this.tbtnStartService.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnStartService.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnStartService.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
      this.tbtnStartService.Name = "tbtnStartService";
      this.tbtnStartService.Size = new System.Drawing.Size(76, 54);
      this.tbtnStartService.Tag = "StartService";
      this.tbtnStartService.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.tbtnStartService.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.tbtnStartService.ToolTipText = "Start the Windows Service Host";
      this.tbtnStartService.Click += new System.EventHandler(this.Action);
      // 
      // tbtnPauseService
      // 
      this.tbtnPauseService.AutoSize = false;
      this.tbtnPauseService.BackColor = System.Drawing.Color.Gainsboro;
      this.tbtnPauseService.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnPauseService.Enabled = false;
      this.tbtnPauseService.Image = global::Org.WinServiceHost.Properties.Resources.Pause;
      this.tbtnPauseService.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnPauseService.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnPauseService.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
      this.tbtnPauseService.Name = "tbtnPauseService";
      this.tbtnPauseService.Size = new System.Drawing.Size(76, 54);
      this.tbtnPauseService.Tag = "PauseService";
      this.tbtnPauseService.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.tbtnPauseService.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.tbtnPauseService.ToolTipText = "Pause the Windows Service Host";
      this.tbtnPauseService.Click += new System.EventHandler(this.Action);
      // 
      // tbtnResumeService
      // 
      this.tbtnResumeService.AutoSize = false;
      this.tbtnResumeService.BackColor = System.Drawing.Color.Gainsboro;
      this.tbtnResumeService.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnResumeService.Enabled = false;
      this.tbtnResumeService.Image = global::Org.WinServiceHost.Properties.Resources.Resume;
      this.tbtnResumeService.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnResumeService.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnResumeService.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
      this.tbtnResumeService.Name = "tbtnResumeService";
      this.tbtnResumeService.Size = new System.Drawing.Size(76, 54);
      this.tbtnResumeService.Tag = "ResumeService";
      this.tbtnResumeService.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.tbtnResumeService.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.tbtnResumeService.ToolTipText = "Resume the Windows Service Host";
      this.tbtnResumeService.Click += new System.EventHandler(this.Action);
      // 
      // tbtnStopService
      // 
      this.tbtnStopService.AutoSize = false;
      this.tbtnStopService.BackColor = System.Drawing.Color.Gainsboro;
      this.tbtnStopService.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnStopService.Enabled = false;
      this.tbtnStopService.Image = global::Org.WinServiceHost.Properties.Resources.Stop;
      this.tbtnStopService.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnStopService.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnStopService.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
      this.tbtnStopService.Name = "tbtnStopService";
      this.tbtnStopService.Size = new System.Drawing.Size(76, 54);
      this.tbtnStopService.Tag = "StopService";
      this.tbtnStopService.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.tbtnStopService.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.tbtnStopService.ToolTipText = "Stop the Windows Service Host";
      this.tbtnStopService.Click += new System.EventHandler(this.Action);
      // 
      // tbtnSpacer
      // 
      this.tbtnSpacer.AutoSize = false;
      this.tbtnSpacer.BackColor = System.Drawing.Color.Gainsboro;
      this.tbtnSpacer.Name = "tbtnSpacer";
      this.tbtnSpacer.Size = new System.Drawing.Size(75, 55);
      // 
      // pbLogo
      // 
      this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.pbLogo.Image = global::Org.WinServiceHost.Properties.Resources.WinServiceHostBanner;
      this.pbLogo.Location = new System.Drawing.Point(677, 5);
      this.pbLogo.Name = "pbLogo";
      this.pbLogo.Size = new System.Drawing.Size(352, 50);
      this.pbLogo.TabIndex = 1;
      this.pbLogo.TabStop = false;
      // 
      // pnlBottom
      // 
      this.pnlBottom.Controls.Add(this.lblStatus);
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 655);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(1029, 24);
      this.pnlBottom.TabIndex = 9;
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 1);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1029, 23);
      this.lblStatus.TabIndex = 0;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 83);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.btnErrorTask);
      this.splitContainer1.Panel1.Controls.Add(this.btnCancelTask);
      this.splitContainer1.Panel1.Controls.Add(this.btnStartTask);
      this.splitContainer1.Panel1.Controls.Add(this.progTask);
      this.splitContainer1.Panel1.Controls.Add(this.txtParms);
      this.splitContainer1.Panel1.Controls.Add(this.lblParms);
      this.splitContainer1.Panel1.Controls.Add(this.cboFunction);
      this.splitContainer1.Panel1.Controls.Add(this.btnTestNotifications);
      this.splitContainer1.Panel1.Controls.Add(this.btnTestLogWrite);
      this.splitContainer1.Panel1.Controls.Add(this.btnHideControls);
      this.splitContainer1.Panel1.Controls.Add(this.btnClearReport);
      this.splitContainer1.Panel1.Controls.Add(this.btnRunFunction);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tabMain);
      this.splitContainer1.Size = new System.Drawing.Size(1029, 572);
      this.splitContainer1.SplitterDistance = 101;
      this.splitContainer1.TabIndex = 10;
      // 
      // btnErrorTask
      // 
      this.btnErrorTask.Location = new System.Drawing.Point(742, 8);
      this.btnErrorTask.Name = "btnErrorTask";
      this.btnErrorTask.Size = new System.Drawing.Size(101, 23);
      this.btnErrorTask.TabIndex = 5;
      this.btnErrorTask.Tag = "ErrorTask";
      this.btnErrorTask.Text = "Error Task";
      this.btnErrorTask.UseVisualStyleBackColor = true;
      this.btnErrorTask.Click += new System.EventHandler(this.Action);
      // 
      // btnCancelTask
      // 
      this.btnCancelTask.Location = new System.Drawing.Point(635, 8);
      this.btnCancelTask.Name = "btnCancelTask";
      this.btnCancelTask.Size = new System.Drawing.Size(101, 23);
      this.btnCancelTask.TabIndex = 5;
      this.btnCancelTask.Tag = "CancelTask";
      this.btnCancelTask.Text = "Cancel Task";
      this.btnCancelTask.UseVisualStyleBackColor = true;
      this.btnCancelTask.Click += new System.EventHandler(this.Action);
      // 
      // btnStartTask
      // 
      this.btnStartTask.Location = new System.Drawing.Point(528, 8);
      this.btnStartTask.Name = "btnStartTask";
      this.btnStartTask.Size = new System.Drawing.Size(101, 23);
      this.btnStartTask.TabIndex = 5;
      this.btnStartTask.Tag = "StartTask";
      this.btnStartTask.Text = "Start Task";
      this.btnStartTask.UseVisualStyleBackColor = true;
      this.btnStartTask.Click += new System.EventHandler(this.Action);
      // 
      // progTask
      // 
      this.progTask.Location = new System.Drawing.Point(528, 37);
      this.progTask.Name = "progTask";
      this.progTask.Size = new System.Drawing.Size(315, 23);
      this.progTask.TabIndex = 4;
      // 
      // txtParms
      // 
      this.txtParms.Location = new System.Drawing.Point(108, 38);
      this.txtParms.Name = "txtParms";
      this.txtParms.Size = new System.Drawing.Size(302, 20);
      this.txtParms.TabIndex = 3;
      // 
      // lblParms
      // 
      this.lblParms.Location = new System.Drawing.Point(27, 38);
      this.lblParms.Name = "lblParms";
      this.lblParms.Size = new System.Drawing.Size(77, 23);
      this.lblParms.TabIndex = 2;
      this.lblParms.Text = "Parms";
      this.lblParms.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cboFunction
      // 
      this.cboFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboFunction.FormattingEnabled = true;
      this.cboFunction.Location = new System.Drawing.Point(108, 9);
      this.cboFunction.Name = "cboFunction";
      this.cboFunction.Size = new System.Drawing.Size(302, 21);
      this.cboFunction.TabIndex = 1;
      // 
      // btnTestNotifications
      // 
      this.btnTestNotifications.Location = new System.Drawing.Point(744, 71);
      this.btnTestNotifications.Name = "btnTestNotifications";
      this.btnTestNotifications.Size = new System.Drawing.Size(99, 23);
      this.btnTestNotifications.TabIndex = 0;
      this.btnTestNotifications.Tag = "TestNotifications";
      this.btnTestNotifications.Text = "Test Notifications";
      this.btnTestNotifications.UseVisualStyleBackColor = true;
      this.btnTestNotifications.Click += new System.EventHandler(this.Action);
      // 
      // btnTestLogWrite
      // 
      this.btnTestLogWrite.Location = new System.Drawing.Point(903, 64);
      this.btnTestLogWrite.Name = "btnTestLogWrite";
      this.btnTestLogWrite.Size = new System.Drawing.Size(99, 23);
      this.btnTestLogWrite.TabIndex = 0;
      this.btnTestLogWrite.Tag = "TestLogWrite";
      this.btnTestLogWrite.Text = "Test Log Write";
      this.btnTestLogWrite.UseVisualStyleBackColor = true;
      this.btnTestLogWrite.Click += new System.EventHandler(this.Action);
      // 
      // btnHideControls
      // 
      this.btnHideControls.Location = new System.Drawing.Point(903, 35);
      this.btnHideControls.Name = "btnHideControls";
      this.btnHideControls.Size = new System.Drawing.Size(99, 23);
      this.btnHideControls.TabIndex = 0;
      this.btnHideControls.Tag = "ToggleShowControls";
      this.btnHideControls.Text = "Hide Controls";
      this.btnHideControls.UseVisualStyleBackColor = true;
      this.btnHideControls.Click += new System.EventHandler(this.Action);
      // 
      // btnClearReport
      // 
      this.btnClearReport.Location = new System.Drawing.Point(903, 8);
      this.btnClearReport.Name = "btnClearReport";
      this.btnClearReport.Size = new System.Drawing.Size(99, 23);
      this.btnClearReport.TabIndex = 0;
      this.btnClearReport.Tag = "ClearReport";
      this.btnClearReport.Text = "Clear Report";
      this.btnClearReport.UseVisualStyleBackColor = true;
      this.btnClearReport.Click += new System.EventHandler(this.Action);
      // 
      // btnRunFunction
      // 
      this.btnRunFunction.Location = new System.Drawing.Point(2, 8);
      this.btnRunFunction.Name = "btnRunFunction";
      this.btnRunFunction.Size = new System.Drawing.Size(103, 23);
      this.btnRunFunction.TabIndex = 0;
      this.btnRunFunction.Tag = "RunFunction";
      this.btnRunFunction.Text = "Run Function";
      this.btnRunFunction.UseVisualStyleBackColor = true;
      this.btnRunFunction.Click += new System.EventHandler(this.Action);
      // 
      // tabMain
      // 
      this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabMain.Controls.Add(this.tabPageReport);
      this.tabMain.Controls.Add(this.tabPage2);
      this.tabMain.ItemSize = new System.Drawing.Size(1, 1);
      this.tabMain.Location = new System.Drawing.Point(-5, -5);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1043, 483);
      this.tabMain.TabIndex = 0;
      // 
      // tabPageReport
      // 
      this.tabPageReport.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageReport.Controls.Add(this.txtReport);
      this.tabPageReport.Location = new System.Drawing.Point(4, 5);
      this.tabPageReport.Name = "tabPageReport";
      this.tabPageReport.Padding = new System.Windows.Forms.Padding(6, 6, 10, 10);
      this.tabPageReport.Size = new System.Drawing.Size(1035, 474);
      this.tabPageReport.TabIndex = 0;
      // 
      // txtReport
      // 
      this.txtReport.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtReport.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtReport.Location = new System.Drawing.Point(6, 6);
      this.txtReport.Multiline = true;
      this.txtReport.Name = "txtReport";
      this.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtReport.Size = new System.Drawing.Size(1019, 458);
      this.txtReport.TabIndex = 0;
      this.txtReport.WordWrap = false;
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(4, 5);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
      this.tabPage2.Size = new System.Drawing.Size(1035, 517);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1029, 679);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.pnlBottom);
      this.Controls.Add(this.panelTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "WinService Host";
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.panelTop.ResumeLayout(false);
      this.pnlToolbar.ResumeLayout(false);
      this.toolStripMain.ResumeLayout(false);
      this.toolStripMain.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
      this.pnlBottom.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageReport.ResumeLayout(false);
      this.tabPageReport.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton tbtnStartService;
        private System.Windows.Forms.ToolStripButton tbtnPauseService;
        private System.Windows.Forms.ToolStripButton tbtnResumeService;
        private System.Windows.Forms.ToolStripButton tbtnStopService;
        private System.Windows.Forms.ToolStripLabel tbtnSpacer;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnRunFunction;
        private System.Windows.Forms.TextBox txtParms;
        private System.Windows.Forms.Label lblParms;
        private System.Windows.Forms.ComboBox cboFunction;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageReport;
        private System.Windows.Forms.TextBox txtReport;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuToggleShowControls;
        private System.Windows.Forms.Button btnHideControls;
        private System.Windows.Forms.Button btnClearReport;
        private System.Windows.Forms.Button btnErrorTask;
        private System.Windows.Forms.Button btnCancelTask;
        private System.Windows.Forms.Button btnStartTask;
        private System.Windows.Forms.ProgressBar progTask;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsDumpTrace;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsRunConfigWizard;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsShowServiceAlert;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsConfigureScheduledTasks;
        private System.Windows.Forms.Button btnTestLogWrite;
        private System.Windows.Forms.Button btnTestNotifications;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsGetNextNow;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsRefreshTaskList;
    }
}


namespace Org.DxWorkbookTester
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
      this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.ckOverrideConfigWsSpec = new System.Windows.Forms.CheckBox();
      this.lblMaxRows = new System.Windows.Forms.Label();
      this.txtMaxRows = new System.Windows.Forms.TextBox();
      this.lblWebServiceEndpoint = new System.Windows.Forms.Label();
      this.lblDbServer = new System.Windows.Forms.Label();
      this.gbRunOptions = new System.Windows.Forms.GroupBox();
      this.ckUseManualFolder = new System.Windows.Forms.CheckBox();
      this.ckSuppressMapping = new System.Windows.Forms.CheckBox();
      this.ckRunDatabaseLoad = new System.Windows.Forms.CheckBox();
      this.ckDryRun = new System.Windows.Forms.CheckBox();
      this.btnGetMap = new System.Windows.Forms.Button();
      this.btnTestWebService = new System.Windows.Forms.Button();
      this.btnSplitExcelFile = new System.Windows.Forms.Button();
      this.btnClearRawOutput = new System.Windows.Forms.Button();
      this.btnClearMappedOutput = new System.Windows.Forms.Button();
      this.btnRunTask = new System.Windows.Forms.Button();
      this.btnShowParameters = new System.Windows.Forms.Button();
      this.lblTaskGroup = new System.Windows.Forms.Label();
      this.lblScheduledTasks = new System.Windows.Forms.Label();
      this.lblWebServiceEnvironment = new System.Windows.Forms.Label();
      this.lblDatabaseEnvironment = new System.Windows.Forms.Label();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.cboScheduledTasks = new System.Windows.Forms.ComboBox();
      this.lblDxWorkbookSource = new System.Windows.Forms.Label();
      this.cboWsEnvironment = new System.Windows.Forms.ComboBox();
      this.cboDbEnvironment = new System.Windows.Forms.ComboBox();
      this.cboTaskGroup = new System.Windows.Forms.ComboBox();
      this.cboDxWorkbookSource = new System.Windows.Forms.ComboBox();
      this.txtOutputFilter = new System.Windows.Forms.TextBox();
      this.lblOutputFilter = new System.Windows.Forms.Label();
      this.txtTaskParms = new System.Windows.Forms.TextBox();
      this.lblTaskParms = new System.Windows.Forms.Label();
      this.lblStatus = new System.Windows.Forms.Label();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageMapped = new System.Windows.Forms.TabPage();
      this.tabPageUnMapped = new System.Windows.Forms.TabPage();
      this.txtUnmappedFile = new System.Windows.Forms.TextBox();
      this.tabPageRawReport = new System.Windows.Forms.TabPage();
      this.txtRawReport = new System.Windows.Forms.RichTextBox();
      this.dlgSaveAs = new System.Windows.Forms.SaveFileDialog();
      this.ckIncludeDxStructure = new System.Windows.Forms.CheckBox();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.gbRunOptions.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageMapped.SuspendLayout();
      this.tabPageUnMapped.SuspendLayout();
      this.tabPageRawReport.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
      this.mnuMain.Size = new System.Drawing.Size(1720, 25);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "mnuMain";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFileSave,
        this.mnuExit
      });
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 19);
      this.mnuFile.Text = "&File";
      //
      // mnuFileSave
      //
      this.mnuFileSave.Name = "mnuFileSave";
      this.mnuFileSave.Size = new System.Drawing.Size(179, 22);
      this.mnuFileSave.Tag = "SaveOutputToFile";
      this.mnuFileSave.Text = "Save output to file...";
      this.mnuFileSave.Click += new System.EventHandler(this.Action);
      //
      // mnuExit
      //
      this.mnuExit.Name = "mnuExit";
      this.mnuExit.Size = new System.Drawing.Size(179, 22);
      this.mnuExit.Tag = "Exit";
      this.mnuExit.Text = "&Exit";
      this.mnuExit.Click += new System.EventHandler(this.Action);
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.ckIncludeDxStructure);
      this.pnlTop.Controls.Add(this.ckOverrideConfigWsSpec);
      this.pnlTop.Controls.Add(this.lblMaxRows);
      this.pnlTop.Controls.Add(this.txtMaxRows);
      this.pnlTop.Controls.Add(this.lblWebServiceEndpoint);
      this.pnlTop.Controls.Add(this.lblDbServer);
      this.pnlTop.Controls.Add(this.gbRunOptions);
      this.pnlTop.Controls.Add(this.btnGetMap);
      this.pnlTop.Controls.Add(this.btnTestWebService);
      this.pnlTop.Controls.Add(this.btnSplitExcelFile);
      this.pnlTop.Controls.Add(this.btnClearRawOutput);
      this.pnlTop.Controls.Add(this.btnClearMappedOutput);
      this.pnlTop.Controls.Add(this.btnRunTask);
      this.pnlTop.Controls.Add(this.btnShowParameters);
      this.pnlTop.Controls.Add(this.lblTaskGroup);
      this.pnlTop.Controls.Add(this.lblScheduledTasks);
      this.pnlTop.Controls.Add(this.lblWebServiceEnvironment);
      this.pnlTop.Controls.Add(this.lblDatabaseEnvironment);
      this.pnlTop.Controls.Add(this.lblEnvironment);
      this.pnlTop.Controls.Add(this.cboScheduledTasks);
      this.pnlTop.Controls.Add(this.lblDxWorkbookSource);
      this.pnlTop.Controls.Add(this.cboWsEnvironment);
      this.pnlTop.Controls.Add(this.cboDbEnvironment);
      this.pnlTop.Controls.Add(this.cboTaskGroup);
      this.pnlTop.Controls.Add(this.cboDxWorkbookSource);
      this.pnlTop.Controls.Add(this.txtOutputFilter);
      this.pnlTop.Controls.Add(this.lblOutputFilter);
      this.pnlTop.Controls.Add(this.txtTaskParms);
      this.pnlTop.Controls.Add(this.lblTaskParms);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 25);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1720, 311);
      this.pnlTop.TabIndex = 1;
      //
      // ckOverrideConfigWsSpec
      //
      this.ckOverrideConfigWsSpec.AutoSize = true;
      this.ckOverrideConfigWsSpec.Location = new System.Drawing.Point(358, 152);
      this.ckOverrideConfigWsSpec.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ckOverrideConfigWsSpec.Name = "ckOverrideConfigWsSpec";
      this.ckOverrideConfigWsSpec.Size = new System.Drawing.Size(197, 24);
      this.ckOverrideConfigWsSpec.TabIndex = 14;
      this.ckOverrideConfigWsSpec.Text = "Override ConfigWsSpec";
      this.ckOverrideConfigWsSpec.UseVisualStyleBackColor = true;
      //
      // lblMaxRows
      //
      this.lblMaxRows.AutoSize = true;
      this.lblMaxRows.Location = new System.Drawing.Point(1428, 198);
      this.lblMaxRows.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblMaxRows.Name = "lblMaxRows";
      this.lblMaxRows.Size = new System.Drawing.Size(86, 20);
      this.lblMaxRows.TabIndex = 13;
      this.lblMaxRows.Text = "Max Rows:";
      //
      // txtMaxRows
      //
      this.txtMaxRows.Location = new System.Drawing.Point(1532, 194);
      this.txtMaxRows.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtMaxRows.Name = "txtMaxRows";
      this.txtMaxRows.Size = new System.Drawing.Size(118, 26);
      this.txtMaxRows.TabIndex = 12;
      this.txtMaxRows.Text = "1000";
      this.txtMaxRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      //
      // lblWebServiceEndpoint
      //
      this.lblWebServiceEndpoint.AutoSize = true;
      this.lblWebServiceEndpoint.Location = new System.Drawing.Point(780, 54);
      this.lblWebServiceEndpoint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblWebServiceEndpoint.Name = "lblWebServiceEndpoint";
      this.lblWebServiceEndpoint.Size = new System.Drawing.Size(345, 20);
      this.lblWebServiceEndpoint.TabIndex = 11;
      this.lblWebServiceEndpoint.Text = "OKCWEB1001/GPFileExtractService.svc:32301";
      //
      // lblDbServer
      //
      this.lblDbServer.AutoSize = true;
      this.lblDbServer.Location = new System.Drawing.Point(354, 54);
      this.lblDbServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblDbServer.Name = "lblDbServer";
      this.lblDbServer.Size = new System.Drawing.Size(125, 20);
      this.lblDbServer.TabIndex = 11;
      this.lblDbServer.Text = "OKC1EDW2001";
      //
      // gbRunOptions
      //
      this.gbRunOptions.Controls.Add(this.ckUseManualFolder);
      this.gbRunOptions.Controls.Add(this.ckSuppressMapping);
      this.gbRunOptions.Controls.Add(this.ckRunDatabaseLoad);
      this.gbRunOptions.Controls.Add(this.ckDryRun);
      this.gbRunOptions.Location = new System.Drawing.Point(1143, 54);
      this.gbRunOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.gbRunOptions.Name = "gbRunOptions";
      this.gbRunOptions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.gbRunOptions.Size = new System.Drawing.Size(260, 180);
      this.gbRunOptions.TabIndex = 10;
      this.gbRunOptions.TabStop = false;
      this.gbRunOptions.Text = "Run Options";
      //
      // ckUseManualFolder
      //
      this.ckUseManualFolder.AutoSize = true;
      this.ckUseManualFolder.Location = new System.Drawing.Point(32, 74);
      this.ckUseManualFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ckUseManualFolder.Name = "ckUseManualFolder";
      this.ckUseManualFolder.Size = new System.Drawing.Size(154, 24);
      this.ckUseManualFolder.TabIndex = 9;
      this.ckUseManualFolder.Text = "Use Manual Input";
      this.ckUseManualFolder.UseVisualStyleBackColor = true;
      //
      // ckSuppressMapping
      //
      this.ckSuppressMapping.AutoSize = true;
      this.ckSuppressMapping.Location = new System.Drawing.Point(32, 43);
      this.ckSuppressMapping.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ckSuppressMapping.Name = "ckSuppressMapping";
      this.ckSuppressMapping.Size = new System.Drawing.Size(161, 24);
      this.ckSuppressMapping.TabIndex = 9;
      this.ckSuppressMapping.Text = "Suppress Mapping";
      this.ckSuppressMapping.UseVisualStyleBackColor = true;
      this.ckSuppressMapping.CheckedChanged += new System.EventHandler(this.ckSuppressMapping_CheckedChanged);
      //
      // ckRunDatabaseLoad
      //
      this.ckRunDatabaseLoad.AutoSize = true;
      this.ckRunDatabaseLoad.Location = new System.Drawing.Point(32, 105);
      this.ckRunDatabaseLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ckRunDatabaseLoad.Name = "ckRunDatabaseLoad";
      this.ckRunDatabaseLoad.Size = new System.Drawing.Size(172, 24);
      this.ckRunDatabaseLoad.TabIndex = 9;
      this.ckRunDatabaseLoad.Text = "Run Database Load";
      this.ckRunDatabaseLoad.UseVisualStyleBackColor = true;
      this.ckRunDatabaseLoad.CheckedChanged += new System.EventHandler(this.ckRunDatabaseLoad_CheckedChanged);
      //
      // ckDryRun
      //
      this.ckDryRun.AutoSize = true;
      this.ckDryRun.Location = new System.Drawing.Point(32, 135);
      this.ckDryRun.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ckDryRun.Name = "ckDryRun";
      this.ckDryRun.Size = new System.Drawing.Size(86, 24);
      this.ckDryRun.TabIndex = 8;
      this.ckDryRun.Text = "Dry Run";
      this.ckDryRun.UseVisualStyleBackColor = true;
      //
      // btnGetMap
      //
      this.btnGetMap.Location = new System.Drawing.Point(780, 197);
      this.btnGetMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnGetMap.Name = "btnGetMap";
      this.btnGetMap.Size = new System.Drawing.Size(156, 35);
      this.btnGetMap.TabIndex = 3;
      this.btnGetMap.Tag = "GetMap";
      this.btnGetMap.Text = "Get Map";
      this.btnGetMap.UseVisualStyleBackColor = true;
      this.btnGetMap.Click += new System.EventHandler(this.Action);
      //
      // btnTestWebService
      //
      this.btnTestWebService.Location = new System.Drawing.Point(945, 197);
      this.btnTestWebService.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnTestWebService.Name = "btnTestWebService";
      this.btnTestWebService.Size = new System.Drawing.Size(156, 35);
      this.btnTestWebService.TabIndex = 3;
      this.btnTestWebService.Tag = "TestWebService";
      this.btnTestWebService.Text = "Test Web Service";
      this.btnTestWebService.UseVisualStyleBackColor = true;
      this.btnTestWebService.Click += new System.EventHandler(this.Action);
      //
      // btnSplitExcelFile
      //
      this.btnSplitExcelFile.Location = new System.Drawing.Point(1428, 149);
      this.btnSplitExcelFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnSplitExcelFile.Name = "btnSplitExcelFile";
      this.btnSplitExcelFile.Size = new System.Drawing.Size(224, 35);
      this.btnSplitExcelFile.TabIndex = 3;
      this.btnSplitExcelFile.Tag = "SplitExcelFile";
      this.btnSplitExcelFile.Text = "Split Excel File";
      this.btnSplitExcelFile.UseVisualStyleBackColor = true;
      this.btnSplitExcelFile.Click += new System.EventHandler(this.Action);
      //
      // btnClearRawOutput
      //
      this.btnClearRawOutput.Location = new System.Drawing.Point(1428, 105);
      this.btnClearRawOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnClearRawOutput.Name = "btnClearRawOutput";
      this.btnClearRawOutput.Size = new System.Drawing.Size(224, 35);
      this.btnClearRawOutput.TabIndex = 3;
      this.btnClearRawOutput.Tag = "ClearRawOutput";
      this.btnClearRawOutput.Text = "Clear Raw Output";
      this.btnClearRawOutput.UseVisualStyleBackColor = true;
      this.btnClearRawOutput.Click += new System.EventHandler(this.Action);
      //
      // btnClearMappedOutput
      //
      this.btnClearMappedOutput.Location = new System.Drawing.Point(1428, 62);
      this.btnClearMappedOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnClearMappedOutput.Name = "btnClearMappedOutput";
      this.btnClearMappedOutput.Size = new System.Drawing.Size(224, 35);
      this.btnClearMappedOutput.TabIndex = 3;
      this.btnClearMappedOutput.Tag = "ClearMappedOutput";
      this.btnClearMappedOutput.Text = "Clear Mapped Output";
      this.btnClearMappedOutput.UseVisualStyleBackColor = true;
      this.btnClearMappedOutput.Click += new System.EventHandler(this.Action);
      //
      // btnRunTask
      //
      this.btnRunTask.Location = new System.Drawing.Point(591, 149);
      this.btnRunTask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnRunTask.Name = "btnRunTask";
      this.btnRunTask.Size = new System.Drawing.Size(180, 83);
      this.btnRunTask.TabIndex = 3;
      this.btnRunTask.Tag = "RunTask";
      this.btnRunTask.Text = "Run Task";
      this.btnRunTask.UseVisualStyleBackColor = true;
      this.btnRunTask.Click += new System.EventHandler(this.Action);
      //
      // btnShowParameters
      //
      this.btnShowParameters.Location = new System.Drawing.Point(780, 152);
      this.btnShowParameters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnShowParameters.Name = "btnShowParameters";
      this.btnShowParameters.Size = new System.Drawing.Size(156, 35);
      this.btnShowParameters.TabIndex = 3;
      this.btnShowParameters.Tag = "ShowParameters";
      this.btnShowParameters.Text = "Show Parameters";
      this.btnShowParameters.UseVisualStyleBackColor = true;
      this.btnShowParameters.Click += new System.EventHandler(this.Action);
      //
      // lblTaskGroup
      //
      this.lblTaskGroup.AutoSize = true;
      this.lblTaskGroup.Location = new System.Drawing.Point(18, 205);
      this.lblTaskGroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblTaskGroup.Name = "lblTaskGroup";
      this.lblTaskGroup.Size = new System.Drawing.Size(96, 20);
      this.lblTaskGroup.TabIndex = 6;
      this.lblTaskGroup.Text = "Task Group:";
      //
      // lblScheduledTasks
      //
      this.lblScheduledTasks.AutoSize = true;
      this.lblScheduledTasks.Location = new System.Drawing.Point(18, 257);
      this.lblScheduledTasks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblScheduledTasks.Name = "lblScheduledTasks";
      this.lblScheduledTasks.Size = new System.Drawing.Size(47, 20);
      this.lblScheduledTasks.TabIndex = 6;
      this.lblScheduledTasks.Text = "Task:";
      //
      // lblWebServiceEnvironment
      //
      this.lblWebServiceEnvironment.AutoSize = true;
      this.lblWebServiceEnvironment.Location = new System.Drawing.Point(586, 25);
      this.lblWebServiceEnvironment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblWebServiceEnvironment.Name = "lblWebServiceEnvironment";
      this.lblWebServiceEnvironment.Size = new System.Drawing.Size(98, 20);
      this.lblWebServiceEnvironment.TabIndex = 6;
      this.lblWebServiceEnvironment.Text = "Web Service";
      //
      // lblDatabaseEnvironment
      //
      this.lblDatabaseEnvironment.AutoSize = true;
      this.lblDatabaseEnvironment.Location = new System.Drawing.Point(138, 25);
      this.lblDatabaseEnvironment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblDatabaseEnvironment.Name = "lblDatabaseEnvironment";
      this.lblDatabaseEnvironment.Size = new System.Drawing.Size(83, 20);
      this.lblDatabaseEnvironment.TabIndex = 6;
      this.lblDatabaseEnvironment.Text = "Database:";
      //
      // lblEnvironment
      //
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(18, 54);
      this.lblEnvironment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(102, 20);
      this.lblEnvironment.TabIndex = 6;
      this.lblEnvironment.Text = "Environment:";
      //
      // cboScheduledTasks
      //
      this.cboScheduledTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboScheduledTasks.FormattingEnabled = true;
      this.cboScheduledTasks.Location = new System.Drawing.Point(144, 252);
      this.cboScheduledTasks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboScheduledTasks.Name = "cboScheduledTasks";
      this.cboScheduledTasks.Size = new System.Drawing.Size(408, 28);
      this.cboScheduledTasks.TabIndex = 7;
      this.cboScheduledTasks.Tag = "";
      this.cboScheduledTasks.SelectedIndexChanged += new System.EventHandler(this.cboScheduledTasks_SelectedIndexChanged);
      //
      // lblDxWorkbookSource
      //
      this.lblDxWorkbookSource.AutoSize = true;
      this.lblDxWorkbookSource.Location = new System.Drawing.Point(18, 154);
      this.lblDxWorkbookSource.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblDxWorkbookSource.Name = "lblDxWorkbookSource";
      this.lblDxWorkbookSource.Size = new System.Drawing.Size(111, 20);
      this.lblDxWorkbookSource.TabIndex = 4;
      this.lblDxWorkbookSource.Text = "DxWb Source:";
      //
      // cboWsEnvironment
      //
      this.cboWsEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboWsEnvironment.FormattingEnabled = true;
      this.cboWsEnvironment.Location = new System.Drawing.Point(591, 49);
      this.cboWsEnvironment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboWsEnvironment.Name = "cboWsEnvironment";
      this.cboWsEnvironment.Size = new System.Drawing.Size(178, 28);
      this.cboWsEnvironment.TabIndex = 7;
      this.cboWsEnvironment.Tag = "";
      this.cboWsEnvironment.SelectedIndexChanged += new System.EventHandler(this.cboWsEnvironment_SelectedIndexChanged);
      //
      // cboDbEnvironment
      //
      this.cboDbEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboDbEnvironment.FormattingEnabled = true;
      this.cboDbEnvironment.Location = new System.Drawing.Point(142, 49);
      this.cboDbEnvironment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboDbEnvironment.Name = "cboDbEnvironment";
      this.cboDbEnvironment.Size = new System.Drawing.Size(200, 28);
      this.cboDbEnvironment.TabIndex = 7;
      this.cboDbEnvironment.Tag = "";
      this.cboDbEnvironment.SelectedIndexChanged += new System.EventHandler(this.cboDbEnvironment_SelectedIndexChanged);
      //
      // cboTaskGroup
      //
      this.cboTaskGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTaskGroup.FormattingEnabled = true;
      this.cboTaskGroup.Location = new System.Drawing.Point(144, 200);
      this.cboTaskGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboTaskGroup.Name = "cboTaskGroup";
      this.cboTaskGroup.Size = new System.Drawing.Size(408, 28);
      this.cboTaskGroup.TabIndex = 5;
      this.cboTaskGroup.Tag = "";
      //
      // cboDxWorkbookSource
      //
      this.cboDxWorkbookSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboDxWorkbookSource.FormattingEnabled = true;
      this.cboDxWorkbookSource.Items.AddRange(new object[] {
        "Create from map",
        "Use mapped file",
        "Use web service",
        "Use task processor"
      });
      this.cboDxWorkbookSource.Location = new System.Drawing.Point(142, 149);
      this.cboDxWorkbookSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboDxWorkbookSource.Name = "cboDxWorkbookSource";
      this.cboDxWorkbookSource.Size = new System.Drawing.Size(200, 28);
      this.cboDxWorkbookSource.TabIndex = 5;
      this.cboDxWorkbookSource.Tag = "";
      this.cboDxWorkbookSource.SelectedIndexChanged += new System.EventHandler(this.cboDxWorkbookSource_SelectedIndexChanged);
      //
      // txtOutputFilter
      //
      this.txtOutputFilter.Location = new System.Drawing.Point(780, 254);
      this.txtOutputFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtOutputFilter.Name = "txtOutputFilter";
      this.txtOutputFilter.Size = new System.Drawing.Size(620, 26);
      this.txtOutputFilter.TabIndex = 1;
      this.txtOutputFilter.Tag = "";
      this.txtOutputFilter.TextChanged += new System.EventHandler(this.txtOutputFilter_TextChanged);
      //
      // lblOutputFilter
      //
      this.lblOutputFilter.AutoSize = true;
      this.lblOutputFilter.Location = new System.Drawing.Point(670, 258);
      this.lblOutputFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblOutputFilter.Name = "lblOutputFilter";
      this.lblOutputFilter.Size = new System.Drawing.Size(101, 20);
      this.lblOutputFilter.TabIndex = 0;
      this.lblOutputFilter.Text = "Output Filter:";
      //
      // txtTaskParms
      //
      this.txtTaskParms.Location = new System.Drawing.Point(142, 98);
      this.txtTaskParms.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtTaskParms.Name = "txtTaskParms";
      this.txtTaskParms.Size = new System.Drawing.Size(626, 26);
      this.txtTaskParms.TabIndex = 1;
      this.txtTaskParms.Tag = "";
      //
      // lblTaskParms
      //
      this.lblTaskParms.AutoSize = true;
      this.lblTaskParms.Location = new System.Drawing.Point(18, 103);
      this.lblTaskParms.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblTaskParms.Name = "lblTaskParms";
      this.lblTaskParms.Size = new System.Drawing.Size(58, 20);
      this.lblTaskParms.TabIndex = 0;
      this.lblTaskParms.Text = "Parms:";
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 1010);
      this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1720, 35);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtOut
      //
      this.txtOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(4, 5);
      this.txtOut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1704, 638);
      this.txtOut.TabIndex = 3;
      this.txtOut.Tag = "txtOut";
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageMapped);
      this.tabMain.Controls.Add(this.tabPageUnMapped);
      this.tabMain.Controls.Add(this.tabPageRawReport);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(150, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 336);
      this.tabMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1720, 674);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 4;
      //
      // tabPageMapped
      //
      this.tabPageMapped.Controls.Add(this.txtOut);
      this.tabPageMapped.Location = new System.Drawing.Point(4, 22);
      this.tabPageMapped.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageMapped.Name = "tabPageMapped";
      this.tabPageMapped.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageMapped.Size = new System.Drawing.Size(1712, 648);
      this.tabPageMapped.TabIndex = 0;
      this.tabPageMapped.Text = "Mapped Output";
      this.tabPageMapped.UseVisualStyleBackColor = true;
      //
      // tabPageUnMapped
      //
      this.tabPageUnMapped.Controls.Add(this.txtUnmappedFile);
      this.tabPageUnMapped.Location = new System.Drawing.Point(4, 22);
      this.tabPageUnMapped.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageUnMapped.Name = "tabPageUnMapped";
      this.tabPageUnMapped.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageUnMapped.Size = new System.Drawing.Size(1712, 636);
      this.tabPageUnMapped.TabIndex = 1;
      this.tabPageUnMapped.Text = "Unmapped File";
      this.tabPageUnMapped.UseVisualStyleBackColor = true;
      //
      // txtUnmappedFile
      //
      this.txtUnmappedFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtUnmappedFile.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtUnmappedFile.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtUnmappedFile.Location = new System.Drawing.Point(4, 5);
      this.txtUnmappedFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtUnmappedFile.Multiline = true;
      this.txtUnmappedFile.Name = "txtUnmappedFile";
      this.txtUnmappedFile.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtUnmappedFile.Size = new System.Drawing.Size(1704, 626);
      this.txtUnmappedFile.TabIndex = 4;
      this.txtUnmappedFile.Tag = "txtOut";
      //
      // tabPageRawReport
      //
      this.tabPageRawReport.Controls.Add(this.txtRawReport);
      this.tabPageRawReport.Location = new System.Drawing.Point(4, 22);
      this.tabPageRawReport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageRawReport.Name = "tabPageRawReport";
      this.tabPageRawReport.Size = new System.Drawing.Size(1712, 636);
      this.tabPageRawReport.TabIndex = 2;
      this.tabPageRawReport.Text = "Raw Report";
      this.tabPageRawReport.UseVisualStyleBackColor = true;
      //
      // txtRawReport
      //
      this.txtRawReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtRawReport.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtRawReport.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtRawReport.Location = new System.Drawing.Point(0, 0);
      this.txtRawReport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtRawReport.Name = "txtRawReport";
      this.txtRawReport.Size = new System.Drawing.Size(1712, 636);
      this.txtRawReport.TabIndex = 0;
      this.txtRawReport.Text = "";
      this.txtRawReport.WordWrap = false;
      //
      // dlgSaveAs
      //
      this.dlgSaveAs.InitialDirectory = "C:\\";
      this.dlgSaveAs.Title = "Save output to file... ";
      //
      // ckIncludeDxStructure
      //
      this.ckIncludeDxStructure.AutoSize = true;
      this.ckIncludeDxStructure.Checked = true;
      this.ckIncludeDxStructure.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckIncludeDxStructure.Location = new System.Drawing.Point(1407, 255);
      this.ckIncludeDxStructure.Name = "ckIncludeDxStructure";
      this.ckIncludeDxStructure.Size = new System.Drawing.Size(223, 24);
      this.ckIncludeDxStructure.TabIndex = 15;
      this.ckIncludeDxStructure.Text = "Include Dx Object Structure";
      this.ckIncludeDxStructure.UseVisualStyleBackColor = true;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1720, 1045);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "DxWorkbookTester - 1.0.0.0";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.gbRunOptions.ResumeLayout(false);
      this.gbRunOptions.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabPageMapped.ResumeLayout(false);
      this.tabPageMapped.PerformLayout();
      this.tabPageUnMapped.ResumeLayout(false);
      this.tabPageUnMapped.PerformLayout();
      this.tabPageRawReport.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.ComboBox cboDbEnvironment;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.Button btnTestWebService;
    private System.Windows.Forms.CheckBox ckRunDatabaseLoad;
    private System.Windows.Forms.CheckBox ckDryRun;
    private System.Windows.Forms.Label lblScheduledTasks;
    private System.Windows.Forms.ComboBox cboScheduledTasks;
    private System.Windows.Forms.Button btnShowParameters;
    private System.Windows.Forms.TextBox txtTaskParms;
    private System.Windows.Forms.Label lblTaskParms;
    private System.Windows.Forms.Button btnRunTask;
    private System.Windows.Forms.Label lblDxWorkbookSource;
    private System.Windows.Forms.ComboBox cboDxWorkbookSource;
    private System.Windows.Forms.GroupBox gbRunOptions;
    private System.Windows.Forms.Label lblWebServiceEndpoint;
    private System.Windows.Forms.Label lblDbServer;
    private System.Windows.Forms.Label lblWebServiceEnvironment;
    private System.Windows.Forms.Label lblDatabaseEnvironment;
    private System.Windows.Forms.ComboBox cboWsEnvironment;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageMapped;
    private System.Windows.Forms.TabPage tabPageUnMapped;
    private System.Windows.Forms.TextBox txtUnmappedFile;
    private System.Windows.Forms.CheckBox ckSuppressMapping;
    private System.Windows.Forms.Button btnClearRawOutput;
    private System.Windows.Forms.Button btnClearMappedOutput;
    private System.Windows.Forms.TabPage tabPageRawReport;
    private System.Windows.Forms.RichTextBox txtRawReport;
    private System.Windows.Forms.Button btnSplitExcelFile;
    private System.Windows.Forms.Label lblMaxRows;
    private System.Windows.Forms.TextBox txtMaxRows;
    private System.Windows.Forms.Button btnGetMap;
    private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
    private System.Windows.Forms.SaveFileDialog dlgSaveAs;
    private System.Windows.Forms.CheckBox ckUseManualFolder;
    private System.Windows.Forms.CheckBox ckOverrideConfigWsSpec;
    private System.Windows.Forms.Label lblTaskGroup;
    private System.Windows.Forms.ComboBox cboTaskGroup;
    private System.Windows.Forms.TextBox txtOutputFilter;
    private System.Windows.Forms.Label lblOutputFilter;
    private System.Windows.Forms.CheckBox ckIncludeDxStructure;
  }
}


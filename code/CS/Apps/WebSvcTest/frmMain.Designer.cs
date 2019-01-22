namespace Org.WebSvcTest
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
      this.ckSuppressMessageOutput = new System.Windows.Forms.CheckBox();
      this.ckTrackPerformance = new System.Windows.Forms.CheckBox();
      this.btnPingPort = new System.Windows.Forms.Button();
      this.txtDuration = new System.Windows.Forms.TextBox();
      this.txtPort = new System.Windows.Forms.TextBox();
      this.txtFrequency = new System.Windows.Forms.TextBox();
      this.btnClearDisplay = new System.Windows.Forms.Button();
      this.btnCancelTestLoop = new System.Windows.Forms.Button();
      this.btnStop = new System.Windows.Forms.Button();
      this.btnUseCPU = new System.Windows.Forms.Button();
      this.btnRunTestLoop = new System.Windows.Forms.Button();
      this.btnDecryptToken = new System.Windows.Forms.Button();
      this.btnTestSecurityToken = new System.Windows.Forms.Button();
      this.btnGenerateSendFiles = new System.Windows.Forms.Button();
      this.btnDownloadSoftware = new System.Windows.Forms.Button();
      this.btnSendMessage = new System.Windows.Forms.Button();
      this.txtParameters = new System.Windows.Forms.TextBox();
      this.cboTransaction = new System.Windows.Forms.ComboBox();
      this.cboWebServiceHost = new System.Windows.Forms.ComboBox();
      this.cboWebService = new System.Windows.Forms.ComboBox();
      this.lblParameters = new System.Windows.Forms.Label();
      this.lblWebServiceHost = new System.Windows.Forms.Label();
      this.lblSelectTransaction = new System.Windows.Forms.Label();
      this.lblPort = new System.Windows.Forms.Label();
      this.lblFrequency = new System.Windows.Forms.Label();
      this.lblWebService = new System.Windows.Forms.Label();
      this.pnlBottom = new System.Windows.Forms.Panel();
      this.lblStatus = new System.Windows.Forms.Label();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tabTop = new System.Windows.Forms.TabControl();
      this.tabPageWebServices = new System.Windows.Forms.TabPage();
      this.cboSubCommandParameter = new System.Windows.Forms.ComboBox();
      this.lblSubCommandParameter = new System.Windows.Forms.Label();
      this.cboSubCommand = new System.Windows.Forms.ComboBox();
      this.lblSubCommand = new System.Windows.Forms.Label();
      this.ckClearDisplay = new System.Windows.Forms.CheckBox();
      this.lblFileName = new System.Windows.Forms.Label();
      this.txtFileName = new System.Windows.Forms.TextBox();
      this.btnTestDxWorkbook = new System.Windows.Forms.Button();
      this.tabPageWebSites = new System.Windows.Forms.TabPage();
      this.btnListWebSites = new System.Windows.Forms.Button();
      this.btnAppPoolList = new System.Windows.Forms.Button();
      this.tabPageAppSecurity = new System.Windows.Forms.TabPage();
      this.ckConciseTokenization = new System.Windows.Forms.CheckBox();
      this.txtCheckUserFunction = new System.Windows.Forms.TextBox();
      this.txtDetokenizeIn = new System.Windows.Forms.TextBox();
      this.txtTokenIn = new System.Windows.Forms.TextBox();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.btnCheckUserFunction = new System.Windows.Forms.Button();
      this.btnLoginUser = new System.Windows.Forms.Button();
      this.btnDisplaySystemInfo = new System.Windows.Forms.Button();
      this.btnDetokenize = new System.Windows.Forms.Button();
      this.btnTokenize = new System.Windows.Forms.Button();
      this.btnRunSecurityTest = new System.Windows.Forms.Button();
      this.btnValidateSecurity = new System.Windows.Forms.Button();
      this.btnListFunctionsByRole = new System.Windows.Forms.Button();
      this.tabBottom = new System.Windows.Forms.TabControl();
      this.tabPagePrimaryOutput = new System.Windows.Forms.TabPage();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.tabPageSecondaryOutput = new System.Windows.Forms.TabPage();
      this.txtOut2 = new System.Windows.Forms.RichTextBox();
      this.pnlToolbar = new System.Windows.Forms.Panel();
      this.mnuMain.SuspendLayout();
      this.pnlBottom.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabTop.SuspendLayout();
      this.tabPageWebServices.SuspendLayout();
      this.tabPageWebSites.SuspendLayout();
      this.tabPageAppSecurity.SuspendLayout();
      this.tabBottom.SuspendLayout();
      this.tabPagePrimaryOutput.SuspendLayout();
      this.tabPageSecondaryOutput.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1370, 24);
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
      this.mnuFileExit.Tag = "EXIT";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      // 
      // ckSuppressMessageOutput
      // 
      this.ckSuppressMessageOutput.AutoSize = true;
      this.ckSuppressMessageOutput.Location = new System.Drawing.Point(428, 190);
      this.ckSuppressMessageOutput.Name = "ckSuppressMessageOutput";
      this.ckSuppressMessageOutput.Size = new System.Drawing.Size(151, 17);
      this.ckSuppressMessageOutput.TabIndex = 6;
      this.ckSuppressMessageOutput.Text = "Suppress Message Output";
      this.ckSuppressMessageOutput.UseVisualStyleBackColor = true;
      // 
      // ckTrackPerformance
      // 
      this.ckTrackPerformance.AutoSize = true;
      this.ckTrackPerformance.Location = new System.Drawing.Point(305, 190);
      this.ckTrackPerformance.Name = "ckTrackPerformance";
      this.ckTrackPerformance.Size = new System.Drawing.Size(117, 17);
      this.ckTrackPerformance.TabIndex = 6;
      this.ckTrackPerformance.Text = "Track Performance";
      this.ckTrackPerformance.UseVisualStyleBackColor = true;
      // 
      // btnPingPort
      // 
      this.btnPingPort.Location = new System.Drawing.Point(796, 24);
      this.btnPingPort.Name = "btnPingPort";
      this.btnPingPort.Size = new System.Drawing.Size(78, 23);
      this.btnPingPort.TabIndex = 5;
      this.btnPingPort.Tag = "PingPort";
      this.btnPingPort.Text = "Ping Port";
      this.btnPingPort.UseVisualStyleBackColor = true;
      this.btnPingPort.Click += new System.EventHandler(this.Action);
      // 
      // txtDuration
      // 
      this.txtDuration.Location = new System.Drawing.Point(69, 187);
      this.txtDuration.Name = "txtDuration";
      this.txtDuration.Size = new System.Drawing.Size(49, 20);
      this.txtDuration.TabIndex = 4;
      // 
      // txtPort
      // 
      this.txtPort.Location = new System.Drawing.Point(674, 25);
      this.txtPort.Name = "txtPort";
      this.txtPort.Size = new System.Drawing.Size(86, 20);
      this.txtPort.TabIndex = 4;
      // 
      // txtFrequency
      // 
      this.txtFrequency.Location = new System.Drawing.Point(14, 187);
      this.txtFrequency.Name = "txtFrequency";
      this.txtFrequency.Size = new System.Drawing.Size(49, 20);
      this.txtFrequency.TabIndex = 4;
      // 
      // btnClearDisplay
      // 
      this.btnClearDisplay.Location = new System.Drawing.Point(1184, 23);
      this.btnClearDisplay.Name = "btnClearDisplay";
      this.btnClearDisplay.Size = new System.Drawing.Size(131, 23);
      this.btnClearDisplay.TabIndex = 3;
      this.btnClearDisplay.Tag = "CLEAR_DISPLAY";
      this.btnClearDisplay.Text = "Clear Display";
      this.btnClearDisplay.UseVisualStyleBackColor = true;
      this.btnClearDisplay.Click += new System.EventHandler(this.Action);
      // 
      // btnCancelTestLoop
      // 
      this.btnCancelTestLoop.Location = new System.Drawing.Point(232, 186);
      this.btnCancelTestLoop.Name = "btnCancelTestLoop";
      this.btnCancelTestLoop.Size = new System.Drawing.Size(67, 23);
      this.btnCancelTestLoop.TabIndex = 3;
      this.btnCancelTestLoop.Tag = "CancelTestLoop";
      this.btnCancelTestLoop.Text = "Cancel";
      this.btnCancelTestLoop.UseVisualStyleBackColor = true;
      this.btnCancelTestLoop.Click += new System.EventHandler(this.Action);
      // 
      // btnStop
      // 
      this.btnStop.Location = new System.Drawing.Point(1261, 165);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(55, 23);
      this.btnStop.TabIndex = 3;
      this.btnStop.Tag = "Stop";
      this.btnStop.Text = "Stop";
      this.btnStop.UseVisualStyleBackColor = true;
      this.btnStop.Click += new System.EventHandler(this.Action);
      // 
      // btnUseCPU
      // 
      this.btnUseCPU.Location = new System.Drawing.Point(1186, 165);
      this.btnUseCPU.Name = "btnUseCPU";
      this.btnUseCPU.Size = new System.Drawing.Size(69, 23);
      this.btnUseCPU.TabIndex = 3;
      this.btnUseCPU.Tag = "UseCPU";
      this.btnUseCPU.Text = "Use CPU";
      this.btnUseCPU.UseVisualStyleBackColor = true;
      this.btnUseCPU.Click += new System.EventHandler(this.Action);
      // 
      // btnRunTestLoop
      // 
      this.btnRunTestLoop.Location = new System.Drawing.Point(124, 186);
      this.btnRunTestLoop.Name = "btnRunTestLoop";
      this.btnRunTestLoop.Size = new System.Drawing.Size(102, 23);
      this.btnRunTestLoop.TabIndex = 3;
      this.btnRunTestLoop.Tag = "RunTestLoop";
      this.btnRunTestLoop.Text = "Run Test Loop";
      this.btnRunTestLoop.UseVisualStyleBackColor = true;
      this.btnRunTestLoop.Click += new System.EventHandler(this.Action);
      // 
      // btnDecryptToken
      // 
      this.btnDecryptToken.Location = new System.Drawing.Point(205, 178);
      this.btnDecryptToken.Name = "btnDecryptToken";
      this.btnDecryptToken.Size = new System.Drawing.Size(151, 23);
      this.btnDecryptToken.TabIndex = 3;
      this.btnDecryptToken.Tag = "DecryptToken";
      this.btnDecryptToken.Text = "Decrypt Token";
      this.btnDecryptToken.UseVisualStyleBackColor = true;
      this.btnDecryptToken.Click += new System.EventHandler(this.Action);
      // 
      // btnTestSecurityToken
      // 
      this.btnTestSecurityToken.Location = new System.Drawing.Point(16, 178);
      this.btnTestSecurityToken.Name = "btnTestSecurityToken";
      this.btnTestSecurityToken.Size = new System.Drawing.Size(151, 23);
      this.btnTestSecurityToken.TabIndex = 3;
      this.btnTestSecurityToken.Tag = "TestSecurityToken";
      this.btnTestSecurityToken.Text = "Test Security Token";
      this.btnTestSecurityToken.UseVisualStyleBackColor = true;
      this.btnTestSecurityToken.Click += new System.EventHandler(this.Action);
      // 
      // btnGenerateSendFiles
      // 
      this.btnGenerateSendFiles.Location = new System.Drawing.Point(1185, 136);
      this.btnGenerateSendFiles.Name = "btnGenerateSendFiles";
      this.btnGenerateSendFiles.Size = new System.Drawing.Size(131, 23);
      this.btnGenerateSendFiles.TabIndex = 3;
      this.btnGenerateSendFiles.Tag = "GenerateSendFiles";
      this.btnGenerateSendFiles.Text = "Generate Send Files";
      this.btnGenerateSendFiles.UseVisualStyleBackColor = true;
      this.btnGenerateSendFiles.Click += new System.EventHandler(this.Action);
      // 
      // btnDownloadSoftware
      // 
      this.btnDownloadSoftware.Location = new System.Drawing.Point(1184, 50);
      this.btnDownloadSoftware.Name = "btnDownloadSoftware";
      this.btnDownloadSoftware.Size = new System.Drawing.Size(131, 23);
      this.btnDownloadSoftware.TabIndex = 3;
      this.btnDownloadSoftware.Tag = "DownloadSoftware";
      this.btnDownloadSoftware.Text = "Download Software";
      this.btnDownloadSoftware.UseVisualStyleBackColor = true;
      this.btnDownloadSoftware.Click += new System.EventHandler(this.Action);
      // 
      // btnSendMessage
      // 
      this.btnSendMessage.Location = new System.Drawing.Point(878, 24);
      this.btnSendMessage.Name = "btnSendMessage";
      this.btnSendMessage.Size = new System.Drawing.Size(98, 23);
      this.btnSendMessage.TabIndex = 3;
      this.btnSendMessage.Tag = "SendMessage";
      this.btnSendMessage.Text = "Send Message";
      this.btnSendMessage.UseVisualStyleBackColor = true;
      this.btnSendMessage.Click += new System.EventHandler(this.Action);
      // 
      // txtParameters
      // 
      this.txtParameters.Location = new System.Drawing.Point(15, 109);
      this.txtParameters.Name = "txtParameters";
      this.txtParameters.Size = new System.Drawing.Size(745, 20);
      this.txtParameters.TabIndex = 2;
      // 
      // cboTransaction
      // 
      this.cboTransaction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTransaction.FormattingEnabled = true;
      this.cboTransaction.Location = new System.Drawing.Point(454, 24);
      this.cboTransaction.Name = "cboTransaction";
      this.cboTransaction.Size = new System.Drawing.Size(216, 21);
      this.cboTransaction.TabIndex = 1;
      this.cboTransaction.SelectedIndexChanged += new System.EventHandler(this.cboTransaction_SelectedIndexChanged);
      // 
      // cboWebServiceHost
      // 
      this.cboWebServiceHost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboWebServiceHost.FormattingEnabled = true;
      this.cboWebServiceHost.Location = new System.Drawing.Point(15, 24);
      this.cboWebServiceHost.Name = "cboWebServiceHost";
      this.cboWebServiceHost.Size = new System.Drawing.Size(216, 21);
      this.cboWebServiceHost.TabIndex = 1;
      this.cboWebServiceHost.SelectedIndexChanged += new System.EventHandler(this.cboWebServiceHost_SelectedIndexChanged);
      // 
      // cboWebService
      // 
      this.cboWebService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboWebService.FormattingEnabled = true;
      this.cboWebService.Location = new System.Drawing.Point(234, 24);
      this.cboWebService.Name = "cboWebService";
      this.cboWebService.Size = new System.Drawing.Size(216, 21);
      this.cboWebService.TabIndex = 1;
      this.cboWebService.SelectedIndexChanged += new System.EventHandler(this.cboWebService_SelectedIndexChanged);
      // 
      // lblParameters
      // 
      this.lblParameters.AutoSize = true;
      this.lblParameters.Location = new System.Drawing.Point(11, 94);
      this.lblParameters.Name = "lblParameters";
      this.lblParameters.Size = new System.Drawing.Size(60, 13);
      this.lblParameters.TabIndex = 0;
      this.lblParameters.Text = "Parameters";
      // 
      // lblWebServiceHost
      // 
      this.lblWebServiceHost.AutoSize = true;
      this.lblWebServiceHost.Location = new System.Drawing.Point(11, 9);
      this.lblWebServiceHost.Name = "lblWebServiceHost";
      this.lblWebServiceHost.Size = new System.Drawing.Size(62, 13);
      this.lblWebServiceHost.TabIndex = 0;
      this.lblWebServiceHost.Text = "Select Host";
      // 
      // lblSelectTransaction
      // 
      this.lblSelectTransaction.AutoSize = true;
      this.lblSelectTransaction.Location = new System.Drawing.Point(451, 9);
      this.lblSelectTransaction.Name = "lblSelectTransaction";
      this.lblSelectTransaction.Size = new System.Drawing.Size(96, 13);
      this.lblSelectTransaction.TabIndex = 0;
      this.lblSelectTransaction.Text = "Select Transaction";
      // 
      // lblPort
      // 
      this.lblPort.AutoSize = true;
      this.lblPort.Location = new System.Drawing.Point(671, 9);
      this.lblPort.Name = "lblPort";
      this.lblPort.Size = new System.Drawing.Size(26, 13);
      this.lblPort.TabIndex = 0;
      this.lblPort.Text = "Port";
      // 
      // lblFrequency
      // 
      this.lblFrequency.AutoSize = true;
      this.lblFrequency.Location = new System.Drawing.Point(12, 174);
      this.lblFrequency.Name = "lblFrequency";
      this.lblFrequency.Size = new System.Drawing.Size(106, 13);
      this.lblFrequency.TabIndex = 0;
      this.lblFrequency.Text = "Frequency  Duration ";
      // 
      // lblWebService
      // 
      this.lblWebService.AutoSize = true;
      this.lblWebService.Location = new System.Drawing.Point(229, 9);
      this.lblWebService.Name = "lblWebService";
      this.lblWebService.Size = new System.Drawing.Size(102, 13);
      this.lblWebService.TabIndex = 0;
      this.lblWebService.Text = "Select Web Service";
      // 
      // pnlBottom
      // 
      this.pnlBottom.Controls.Add(this.lblStatus);
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 659);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(1370, 20);
      this.pnlBottom.TabIndex = 2;
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblStatus.Location = new System.Drawing.Point(0, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(1370, 20);
      this.lblStatus.TabIndex = 0;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 28);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tabTop);
      this.splitContainer1.Panel1MinSize = 250;
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tabBottom);
      this.splitContainer1.Size = new System.Drawing.Size(1370, 631);
      this.splitContainer1.SplitterDistance = 250;
      this.splitContainer1.TabIndex = 3;
      // 
      // tabTop
      // 
      this.tabTop.Controls.Add(this.tabPageWebServices);
      this.tabTop.Controls.Add(this.tabPageWebSites);
      this.tabTop.Controls.Add(this.tabPageAppSecurity);
      this.tabTop.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabTop.ItemSize = new System.Drawing.Size(120, 18);
      this.tabTop.Location = new System.Drawing.Point(0, 0);
      this.tabTop.Name = "tabTop";
      this.tabTop.SelectedIndex = 0;
      this.tabTop.Size = new System.Drawing.Size(1370, 250);
      this.tabTop.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabTop.TabIndex = 0;
      // 
      // tabPageWebServices
      // 
      this.tabPageWebServices.Controls.Add(this.cboSubCommandParameter);
      this.tabPageWebServices.Controls.Add(this.lblSubCommandParameter);
      this.tabPageWebServices.Controls.Add(this.cboSubCommand);
      this.tabPageWebServices.Controls.Add(this.lblSubCommand);
      this.tabPageWebServices.Controls.Add(this.ckClearDisplay);
      this.tabPageWebServices.Controls.Add(this.ckSuppressMessageOutput);
      this.tabPageWebServices.Controls.Add(this.cboWebServiceHost);
      this.tabPageWebServices.Controls.Add(this.ckTrackPerformance);
      this.tabPageWebServices.Controls.Add(this.lblWebService);
      this.tabPageWebServices.Controls.Add(this.btnPingPort);
      this.tabPageWebServices.Controls.Add(this.lblFrequency);
      this.tabPageWebServices.Controls.Add(this.txtDuration);
      this.tabPageWebServices.Controls.Add(this.txtPort);
      this.tabPageWebServices.Controls.Add(this.lblPort);
      this.tabPageWebServices.Controls.Add(this.txtFrequency);
      this.tabPageWebServices.Controls.Add(this.lblSelectTransaction);
      this.tabPageWebServices.Controls.Add(this.btnClearDisplay);
      this.tabPageWebServices.Controls.Add(this.lblWebServiceHost);
      this.tabPageWebServices.Controls.Add(this.btnCancelTestLoop);
      this.tabPageWebServices.Controls.Add(this.lblFileName);
      this.tabPageWebServices.Controls.Add(this.lblParameters);
      this.tabPageWebServices.Controls.Add(this.cboWebService);
      this.tabPageWebServices.Controls.Add(this.btnStop);
      this.tabPageWebServices.Controls.Add(this.cboTransaction);
      this.tabPageWebServices.Controls.Add(this.btnUseCPU);
      this.tabPageWebServices.Controls.Add(this.txtFileName);
      this.tabPageWebServices.Controls.Add(this.txtParameters);
      this.tabPageWebServices.Controls.Add(this.btnSendMessage);
      this.tabPageWebServices.Controls.Add(this.btnTestDxWorkbook);
      this.tabPageWebServices.Controls.Add(this.btnDownloadSoftware);
      this.tabPageWebServices.Controls.Add(this.btnRunTestLoop);
      this.tabPageWebServices.Controls.Add(this.btnGenerateSendFiles);
      this.tabPageWebServices.Location = new System.Drawing.Point(4, 22);
      this.tabPageWebServices.Name = "tabPageWebServices";
      this.tabPageWebServices.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
      this.tabPageWebServices.Size = new System.Drawing.Size(1362, 224);
      this.tabPageWebServices.TabIndex = 0;
      this.tabPageWebServices.Text = "Web Services";
      this.tabPageWebServices.UseVisualStyleBackColor = true;
      // 
      // cboSubCommandParameter
      // 
      this.cboSubCommandParameter.FormattingEnabled = true;
      this.cboSubCommandParameter.Location = new System.Drawing.Point(234, 67);
      this.cboSubCommandParameter.Name = "cboSubCommandParameter";
      this.cboSubCommandParameter.Size = new System.Drawing.Size(216, 21);
      this.cboSubCommandParameter.TabIndex = 14;
      this.cboSubCommandParameter.Tag = "SubCommandParameter";
      this.cboSubCommandParameter.SelectedIndexChanged += new System.EventHandler(this.cboCommand_SelectedIndexChanged);
      // 
      // lblSubCommandParameter
      // 
      this.lblSubCommandParameter.AutoSize = true;
      this.lblSubCommandParameter.Location = new System.Drawing.Point(229, 51);
      this.lblSubCommandParameter.Name = "lblSubCommandParameter";
      this.lblSubCommandParameter.Size = new System.Drawing.Size(127, 13);
      this.lblSubCommandParameter.TabIndex = 13;
      this.lblSubCommandParameter.Text = "Sub Command Parameter";
      // 
      // cboSubCommand
      // 
      this.cboSubCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboSubCommand.FormattingEnabled = true;
      this.cboSubCommand.Location = new System.Drawing.Point(15, 67);
      this.cboSubCommand.Name = "cboSubCommand";
      this.cboSubCommand.Size = new System.Drawing.Size(216, 21);
      this.cboSubCommand.TabIndex = 12;
      this.cboSubCommand.Tag = "SubCommand";
      this.cboSubCommand.SelectedIndexChanged += new System.EventHandler(this.cboCommandName_SelectedIndexChanged);
      // 
      // lblSubCommand
      // 
      this.lblSubCommand.AutoSize = true;
      this.lblSubCommand.Location = new System.Drawing.Point(12, 51);
      this.lblSubCommand.Name = "lblSubCommand";
      this.lblSubCommand.Size = new System.Drawing.Size(76, 13);
      this.lblSubCommand.TabIndex = 11;
      this.lblSubCommand.Text = "Sub Command";
      // 
      // ckClearDisplay
      // 
      this.ckClearDisplay.AutoSize = true;
      this.ckClearDisplay.Location = new System.Drawing.Point(880, 50);
      this.ckClearDisplay.Name = "ckClearDisplay";
      this.ckClearDisplay.Size = new System.Drawing.Size(87, 17);
      this.ckClearDisplay.TabIndex = 10;
      this.ckClearDisplay.Text = "Clear Display";
      this.ckClearDisplay.UseVisualStyleBackColor = true;
      // 
      // lblFileName
      // 
      this.lblFileName.AutoSize = true;
      this.lblFileName.Location = new System.Drawing.Point(11, 135);
      this.lblFileName.Name = "lblFileName";
      this.lblFileName.Size = new System.Drawing.Size(54, 13);
      this.lblFileName.TabIndex = 0;
      this.lblFileName.Text = "File Name";
      // 
      // txtFileName
      // 
      this.txtFileName.Location = new System.Drawing.Point(15, 149);
      this.txtFileName.Name = "txtFileName";
      this.txtFileName.Size = new System.Drawing.Size(745, 20);
      this.txtFileName.TabIndex = 2;
      // 
      // btnTestDxWorkbook
      // 
      this.btnTestDxWorkbook.Location = new System.Drawing.Point(1184, 79);
      this.btnTestDxWorkbook.Name = "btnTestDxWorkbook";
      this.btnTestDxWorkbook.Size = new System.Drawing.Size(131, 23);
      this.btnTestDxWorkbook.TabIndex = 3;
      this.btnTestDxWorkbook.Tag = "TestDxWorkbook";
      this.btnTestDxWorkbook.Text = "Test DxWorkbook";
      this.btnTestDxWorkbook.UseVisualStyleBackColor = true;
      this.btnTestDxWorkbook.Click += new System.EventHandler(this.Action);
      // 
      // tabPageWebSites
      // 
      this.tabPageWebSites.Controls.Add(this.btnListWebSites);
      this.tabPageWebSites.Controls.Add(this.btnAppPoolList);
      this.tabPageWebSites.Location = new System.Drawing.Point(4, 22);
      this.tabPageWebSites.Name = "tabPageWebSites";
      this.tabPageWebSites.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
      this.tabPageWebSites.Size = new System.Drawing.Size(1362, 203);
      this.tabPageWebSites.TabIndex = 1;
      this.tabPageWebSites.Text = "Web Sites";
      this.tabPageWebSites.UseVisualStyleBackColor = true;
      // 
      // btnListWebSites
      // 
      this.btnListWebSites.Location = new System.Drawing.Point(193, 17);
      this.btnListWebSites.Name = "btnListWebSites";
      this.btnListWebSites.Size = new System.Drawing.Size(168, 23);
      this.btnListWebSites.TabIndex = 6;
      this.btnListWebSites.Tag = "LIST_WEB_SITES";
      this.btnListWebSites.Text = "List Web Sites";
      this.btnListWebSites.UseVisualStyleBackColor = true;
      this.btnListWebSites.Click += new System.EventHandler(this.Action);
      // 
      // btnAppPoolList
      // 
      this.btnAppPoolList.Location = new System.Drawing.Point(19, 17);
      this.btnAppPoolList.Name = "btnAppPoolList";
      this.btnAppPoolList.Size = new System.Drawing.Size(168, 23);
      this.btnAppPoolList.TabIndex = 6;
      this.btnAppPoolList.Tag = "LIST_APP_POOLS";
      this.btnAppPoolList.Text = "List App Pools";
      this.btnAppPoolList.UseVisualStyleBackColor = true;
      this.btnAppPoolList.Click += new System.EventHandler(this.Action);
      // 
      // tabPageAppSecurity
      // 
      this.tabPageAppSecurity.Controls.Add(this.ckConciseTokenization);
      this.tabPageAppSecurity.Controls.Add(this.btnTestSecurityToken);
      this.tabPageAppSecurity.Controls.Add(this.btnDecryptToken);
      this.tabPageAppSecurity.Controls.Add(this.txtCheckUserFunction);
      this.tabPageAppSecurity.Controls.Add(this.txtDetokenizeIn);
      this.tabPageAppSecurity.Controls.Add(this.txtTokenIn);
      this.tabPageAppSecurity.Controls.Add(this.txtUserName);
      this.tabPageAppSecurity.Controls.Add(this.btnCheckUserFunction);
      this.tabPageAppSecurity.Controls.Add(this.btnLoginUser);
      this.tabPageAppSecurity.Controls.Add(this.btnDisplaySystemInfo);
      this.tabPageAppSecurity.Controls.Add(this.btnDetokenize);
      this.tabPageAppSecurity.Controls.Add(this.btnTokenize);
      this.tabPageAppSecurity.Controls.Add(this.btnRunSecurityTest);
      this.tabPageAppSecurity.Controls.Add(this.btnValidateSecurity);
      this.tabPageAppSecurity.Controls.Add(this.btnListFunctionsByRole);
      this.tabPageAppSecurity.Location = new System.Drawing.Point(4, 22);
      this.tabPageAppSecurity.Name = "tabPageAppSecurity";
      this.tabPageAppSecurity.Size = new System.Drawing.Size(1362, 203);
      this.tabPageAppSecurity.TabIndex = 2;
      this.tabPageAppSecurity.Text = "App Security";
      this.tabPageAppSecurity.UseVisualStyleBackColor = true;
      // 
      // ckConciseTokenization
      // 
      this.ckConciseTokenization.AutoSize = true;
      this.ckConciseTokenization.Checked = true;
      this.ckConciseTokenization.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckConciseTokenization.Location = new System.Drawing.Point(515, 122);
      this.ckConciseTokenization.Name = "ckConciseTokenization";
      this.ckConciseTokenization.Size = new System.Drawing.Size(128, 17);
      this.ckConciseTokenization.TabIndex = 4;
      this.ckConciseTokenization.Text = "Concise Tokenization";
      this.ckConciseTokenization.UseVisualStyleBackColor = true;
      // 
      // txtCheckUserFunction
      // 
      this.txtCheckUserFunction.Location = new System.Drawing.Point(366, 53);
      this.txtCheckUserFunction.Name = "txtCheckUserFunction";
      this.txtCheckUserFunction.Size = new System.Drawing.Size(131, 20);
      this.txtCheckUserFunction.TabIndex = 1;
      // 
      // txtDetokenizeIn
      // 
      this.txtDetokenizeIn.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtDetokenizeIn.Location = new System.Drawing.Point(205, 149);
      this.txtDetokenizeIn.Name = "txtDetokenizeIn";
      this.txtDetokenizeIn.Size = new System.Drawing.Size(762, 21);
      this.txtDetokenizeIn.TabIndex = 1;
      // 
      // txtTokenIn
      // 
      this.txtTokenIn.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtTokenIn.Location = new System.Drawing.Point(205, 119);
      this.txtTokenIn.Name = "txtTokenIn";
      this.txtTokenIn.Size = new System.Drawing.Size(292, 21);
      this.txtTokenIn.TabIndex = 1;
      // 
      // txtUserName
      // 
      this.txtUserName.Location = new System.Drawing.Point(366, 24);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new System.Drawing.Size(131, 20);
      this.txtUserName.TabIndex = 1;
      // 
      // btnCheckUserFunction
      // 
      this.btnCheckUserFunction.Location = new System.Drawing.Point(205, 52);
      this.btnCheckUserFunction.Name = "btnCheckUserFunction";
      this.btnCheckUserFunction.Size = new System.Drawing.Size(151, 23);
      this.btnCheckUserFunction.TabIndex = 0;
      this.btnCheckUserFunction.Tag = "CheckUserFunction";
      this.btnCheckUserFunction.Text = "Check User Function";
      this.btnCheckUserFunction.UseVisualStyleBackColor = true;
      this.btnCheckUserFunction.Click += new System.EventHandler(this.Action);
      // 
      // btnLoginUser
      // 
      this.btnLoginUser.Location = new System.Drawing.Point(205, 23);
      this.btnLoginUser.Name = "btnLoginUser";
      this.btnLoginUser.Size = new System.Drawing.Size(151, 23);
      this.btnLoginUser.TabIndex = 0;
      this.btnLoginUser.Tag = "LoginUser";
      this.btnLoginUser.Text = "Login User";
      this.btnLoginUser.UseVisualStyleBackColor = true;
      this.btnLoginUser.Click += new System.EventHandler(this.Action);
      // 
      // btnDisplaySystemInfo
      // 
      this.btnDisplaySystemInfo.Location = new System.Drawing.Point(205, 81);
      this.btnDisplaySystemInfo.Name = "btnDisplaySystemInfo";
      this.btnDisplaySystemInfo.Size = new System.Drawing.Size(151, 23);
      this.btnDisplaySystemInfo.TabIndex = 0;
      this.btnDisplaySystemInfo.Tag = "DisplaySystemInfo";
      this.btnDisplaySystemInfo.Text = "Display System Info";
      this.btnDisplaySystemInfo.UseVisualStyleBackColor = true;
      this.btnDisplaySystemInfo.Click += new System.EventHandler(this.Action);
      // 
      // btnDetokenize
      // 
      this.btnDetokenize.Location = new System.Drawing.Point(16, 148);
      this.btnDetokenize.Name = "btnDetokenize";
      this.btnDetokenize.Size = new System.Drawing.Size(151, 23);
      this.btnDetokenize.TabIndex = 0;
      this.btnDetokenize.Tag = "Detokenize";
      this.btnDetokenize.Text = "Detokenize";
      this.btnDetokenize.UseVisualStyleBackColor = true;
      this.btnDetokenize.Click += new System.EventHandler(this.Action);
      // 
      // btnTokenize
      // 
      this.btnTokenize.Location = new System.Drawing.Point(16, 119);
      this.btnTokenize.Name = "btnTokenize";
      this.btnTokenize.Size = new System.Drawing.Size(151, 23);
      this.btnTokenize.TabIndex = 0;
      this.btnTokenize.Tag = "Tokenize";
      this.btnTokenize.Text = "Tokenize";
      this.btnTokenize.UseVisualStyleBackColor = true;
      this.btnTokenize.Click += new System.EventHandler(this.Action);
      // 
      // btnRunSecurityTest
      // 
      this.btnRunSecurityTest.Location = new System.Drawing.Point(16, 81);
      this.btnRunSecurityTest.Name = "btnRunSecurityTest";
      this.btnRunSecurityTest.Size = new System.Drawing.Size(151, 23);
      this.btnRunSecurityTest.TabIndex = 0;
      this.btnRunSecurityTest.Tag = "RunSecurityTest";
      this.btnRunSecurityTest.Text = "Run Security Test";
      this.btnRunSecurityTest.UseVisualStyleBackColor = true;
      this.btnRunSecurityTest.Click += new System.EventHandler(this.Action);
      // 
      // btnValidateSecurity
      // 
      this.btnValidateSecurity.Location = new System.Drawing.Point(16, 23);
      this.btnValidateSecurity.Name = "btnValidateSecurity";
      this.btnValidateSecurity.Size = new System.Drawing.Size(151, 23);
      this.btnValidateSecurity.TabIndex = 0;
      this.btnValidateSecurity.Tag = "ValidateSecurity";
      this.btnValidateSecurity.Text = "ValidateSecurity";
      this.btnValidateSecurity.UseVisualStyleBackColor = true;
      this.btnValidateSecurity.Click += new System.EventHandler(this.Action);
      // 
      // btnListFunctionsByRole
      // 
      this.btnListFunctionsByRole.Location = new System.Drawing.Point(16, 52);
      this.btnListFunctionsByRole.Name = "btnListFunctionsByRole";
      this.btnListFunctionsByRole.Size = new System.Drawing.Size(151, 23);
      this.btnListFunctionsByRole.TabIndex = 0;
      this.btnListFunctionsByRole.Tag = "ListFunctionsByRole";
      this.btnListFunctionsByRole.Text = "List Functions By Role";
      this.btnListFunctionsByRole.UseVisualStyleBackColor = true;
      this.btnListFunctionsByRole.Click += new System.EventHandler(this.Action);
      // 
      // tabBottom
      // 
      this.tabBottom.Controls.Add(this.tabPagePrimaryOutput);
      this.tabBottom.Controls.Add(this.tabPageSecondaryOutput);
      this.tabBottom.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabBottom.ItemSize = new System.Drawing.Size(120, 18);
      this.tabBottom.Location = new System.Drawing.Point(0, 0);
      this.tabBottom.Name = "tabBottom";
      this.tabBottom.SelectedIndex = 0;
      this.tabBottom.Size = new System.Drawing.Size(1370, 377);
      this.tabBottom.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabBottom.TabIndex = 0;
      // 
      // tabPagePrimaryOutput
      // 
      this.tabPagePrimaryOutput.Controls.Add(this.txtOut);
      this.tabPagePrimaryOutput.Location = new System.Drawing.Point(4, 22);
      this.tabPagePrimaryOutput.Name = "tabPagePrimaryOutput";
      this.tabPagePrimaryOutput.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
      this.tabPagePrimaryOutput.Size = new System.Drawing.Size(1362, 351);
      this.tabPagePrimaryOutput.TabIndex = 0;
      this.tabPagePrimaryOutput.Text = "Primary Output";
      this.tabPagePrimaryOutput.UseVisualStyleBackColor = true;
      // 
      // txtOut
      // 
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(3, 3);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1356, 345);
      this.txtOut.TabIndex = 0;
      this.txtOut.WordWrap = false;
      // 
      // tabPageSecondaryOutput
      // 
      this.tabPageSecondaryOutput.Controls.Add(this.txtOut2);
      this.tabPageSecondaryOutput.Location = new System.Drawing.Point(4, 22);
      this.tabPageSecondaryOutput.Name = "tabPageSecondaryOutput";
      this.tabPageSecondaryOutput.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
      this.tabPageSecondaryOutput.Size = new System.Drawing.Size(1362, 385);
      this.tabPageSecondaryOutput.TabIndex = 1;
      this.tabPageSecondaryOutput.Text = "Secondary Output";
      this.tabPageSecondaryOutput.UseVisualStyleBackColor = true;
      // 
      // txtOut2
      // 
      this.txtOut2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtOut2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut2.Location = new System.Drawing.Point(3, 3);
      this.txtOut2.Name = "txtOut2";
      this.txtOut2.Size = new System.Drawing.Size(1356, 379);
      this.txtOut2.TabIndex = 0;
      this.txtOut2.Text = "";
      this.txtOut2.WordWrap = false;
      // 
      // pnlToolbar
      // 
      this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlToolbar.Location = new System.Drawing.Point(0, 24);
      this.pnlToolbar.Name = "pnlToolbar";
      this.pnlToolbar.Size = new System.Drawing.Size(1370, 4);
      this.pnlToolbar.TabIndex = 4;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1370, 679);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.pnlToolbar);
      this.Controls.Add(this.pnlBottom);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Web Service Test - v1.0.0";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlBottom.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tabTop.ResumeLayout(false);
      this.tabPageWebServices.ResumeLayout(false);
      this.tabPageWebServices.PerformLayout();
      this.tabPageWebSites.ResumeLayout(false);
      this.tabPageAppSecurity.ResumeLayout(false);
      this.tabPageAppSecurity.PerformLayout();
      this.tabBottom.ResumeLayout(false);
      this.tabPagePrimaryOutput.ResumeLayout(false);
      this.tabPagePrimaryOutput.PerformLayout();
      this.tabPageSecondaryOutput.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlBottom;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button btnSendMessage;
    private System.Windows.Forms.TextBox txtParameters;
    private System.Windows.Forms.ComboBox cboTransaction;
    private System.Windows.Forms.ComboBox cboWebService;
    private System.Windows.Forms.Label lblParameters;
    private System.Windows.Forms.Label lblSelectTransaction;
    private System.Windows.Forms.Label lblWebService;
    private System.Windows.Forms.ComboBox cboWebServiceHost;
    private System.Windows.Forms.Label lblWebServiceHost;
    private System.Windows.Forms.Button btnClearDisplay;
    private System.Windows.Forms.TextBox txtDuration;
    private System.Windows.Forms.TextBox txtFrequency;
    private System.Windows.Forms.Button btnRunTestLoop;
    private System.Windows.Forms.Label lblFrequency;
    private System.Windows.Forms.Button btnCancelTestLoop;
    private System.Windows.Forms.Button btnPingPort;
    private System.Windows.Forms.TextBox txtPort;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.CheckBox ckTrackPerformance;
    private System.Windows.Forms.CheckBox ckSuppressMessageOutput;
    private System.Windows.Forms.Button btnDownloadSoftware;
    private System.Windows.Forms.Button btnGenerateSendFiles;
    private System.Windows.Forms.Button btnTestSecurityToken;
    private System.Windows.Forms.Button btnDecryptToken;
    private System.Windows.Forms.Button btnUseCPU;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TabControl tabTop;
    private System.Windows.Forms.TabPage tabPageWebServices;
    private System.Windows.Forms.TabPage tabPageWebSites;
    private System.Windows.Forms.TabControl tabBottom;
    private System.Windows.Forms.TabPage tabPagePrimaryOutput;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.TabPage tabPageSecondaryOutput;
    private System.Windows.Forms.Panel pnlToolbar;
    private System.Windows.Forms.Button btnAppPoolList;
    private System.Windows.Forms.Button btnListWebSites;
    private System.Windows.Forms.TabPage tabPageAppSecurity;
    private System.Windows.Forms.Button btnListFunctionsByRole;
    private System.Windows.Forms.Button btnRunSecurityTest;
    private System.Windows.Forms.Button btnValidateSecurity;
    private System.Windows.Forms.Button btnLoginUser;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.TextBox txtCheckUserFunction;
    private System.Windows.Forms.Button btnCheckUserFunction;
    private System.Windows.Forms.Button btnDisplaySystemInfo;
    private System.Windows.Forms.CheckBox ckClearDisplay;
    private System.Windows.Forms.TextBox txtDetokenizeIn;
    private System.Windows.Forms.TextBox txtTokenIn;
    private System.Windows.Forms.Button btnDetokenize;
    private System.Windows.Forms.Button btnTokenize;
    private System.Windows.Forms.Button btnTestDxWorkbook;
    private System.Windows.Forms.Label lblSubCommand;
    private System.Windows.Forms.ComboBox cboSubCommand;
    private System.Windows.Forms.ComboBox cboSubCommandParameter;
    private System.Windows.Forms.Label lblSubCommandParameter;
    private System.Windows.Forms.CheckBox ckConciseTokenization;
    private System.Windows.Forms.RichTextBox txtOut2;
    private System.Windows.Forms.Label lblFileName;
    private System.Windows.Forms.TextBox txtFileName;
  }
}


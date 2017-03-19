namespace Org.SoftwareUpdates
{
  partial class frmUpdateManager

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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateManager));
      this.pnlTopControl = new System.Windows.Forms.Panel();
      this.ckTrackPerformance = new System.Windows.Forms.CheckBox();
      this.btnClearDisplay = new System.Windows.Forms.Button();
      this.btnContinue = new System.Windows.Forms.Button();
      this.btnTerminate = new System.Windows.Forms.Button();
      this.cboEnvironment = new System.Windows.Forms.ComboBox();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.btnDownloadSoftware = new System.Windows.Forms.Button();
      this.btnGetFrameworkVersions = new System.Windows.Forms.Button();
      this.btnCheckForSoftwareUpdates = new System.Windows.Forms.Button();
      this.btnWsTestConnection = new System.Windows.Forms.Button();
      this.pnlLog = new System.Windows.Forms.Panel();
      this.txtLog = new System.Windows.Forms.TextBox();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.lblTest = new System.Windows.Forms.Label();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageWebServiceUpdates = new System.Windows.Forms.TabPage();
      this.tabFileSystemUpdates = new System.Windows.Forms.TabPage();
      this.pnlToolbar = new System.Windows.Forms.Panel();
      this.btnFsTestFileSystemAccess = new System.Windows.Forms.Button();
      this.btnFsCheckForUpdates = new System.Windows.Forms.Button();
      this.btnFsGetFrameworkVersions = new System.Windows.Forms.Button();
      this.btnFsDownloadUpdates = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlTopControl.SuspendLayout();
      this.pnlLog.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageWebServiceUpdates.SuspendLayout();
      this.tabFileSystemUpdates.SuspendLayout();
      this.pnlToolbar.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlTopControl
      // 
      this.pnlTopControl.Controls.Add(this.tabMain);
      this.pnlTopControl.Controls.Add(this.pnlToolbar);
      this.pnlTopControl.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTopControl.Location = new System.Drawing.Point(0, 0);
      this.pnlTopControl.Name = "pnlTopControl";
      this.pnlTopControl.Size = new System.Drawing.Size(724, 145);
      this.pnlTopControl.TabIndex = 0;
      // 
      // ckTrackPerformance
      // 
      this.ckTrackPerformance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ckTrackPerformance.AutoSize = true;
      this.ckTrackPerformance.Location = new System.Drawing.Point(532, 50);
      this.ckTrackPerformance.Name = "ckTrackPerformance";
      this.ckTrackPerformance.Size = new System.Drawing.Size(117, 17);
      this.ckTrackPerformance.TabIndex = 4;
      this.ckTrackPerformance.Text = "Track Performance";
      this.ckTrackPerformance.UseVisualStyleBackColor = true;
      // 
      // btnClearDisplay
      // 
      this.btnClearDisplay.Location = new System.Drawing.Point(256, 9);
      this.btnClearDisplay.Name = "btnClearDisplay";
      this.btnClearDisplay.Size = new System.Drawing.Size(116, 23);
      this.btnClearDisplay.TabIndex = 3;
      this.btnClearDisplay.Tag = "ClearDisplay";
      this.btnClearDisplay.Text = "Clear Display";
      this.btnClearDisplay.UseVisualStyleBackColor = true;
      this.btnClearDisplay.Click += new System.EventHandler(this.Action);
      // 
      // btnContinue
      // 
      this.btnContinue.Location = new System.Drawing.Point(12, 9);
      this.btnContinue.Name = "btnContinue";
      this.btnContinue.Size = new System.Drawing.Size(116, 23);
      this.btnContinue.TabIndex = 3;
      this.btnContinue.Tag = "Continue";
      this.btnContinue.Text = "Continue";
      this.btnContinue.UseVisualStyleBackColor = true;
      this.btnContinue.Click += new System.EventHandler(this.Action);
      // 
      // btnTerminate
      // 
      this.btnTerminate.Location = new System.Drawing.Point(134, 9);
      this.btnTerminate.Name = "btnTerminate";
      this.btnTerminate.Size = new System.Drawing.Size(116, 23);
      this.btnTerminate.TabIndex = 3;
      this.btnTerminate.Tag = "Terminate";
      this.btnTerminate.Text = "Terminate";
      this.btnTerminate.UseVisualStyleBackColor = true;
      this.btnTerminate.Click += new System.EventHandler(this.Action);
      // 
      // cboEnvironment
      // 
      this.cboEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cboEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboEnvironment.FormattingEnabled = true;
      this.cboEnvironment.Location = new System.Drawing.Point(529, 24);
      this.cboEnvironment.Name = "cboEnvironment";
      this.cboEnvironment.Size = new System.Drawing.Size(175, 21);
      this.cboEnvironment.TabIndex = 1;
      // 
      // lblEnvironment
      // 
      this.lblEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(526, 8);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(66, 13);
      this.lblEnvironment.TabIndex = 2;
      this.lblEnvironment.Text = "Environment";
      // 
      // btnDownloadSoftware
      // 
      this.btnDownloadSoftware.Location = new System.Drawing.Point(234, 41);
      this.btnDownloadSoftware.Name = "btnDownloadSoftware";
      this.btnDownloadSoftware.Size = new System.Drawing.Size(213, 23);
      this.btnDownloadSoftware.TabIndex = 0;
      this.btnDownloadSoftware.Tag = "WsDownloadSoftware";
      this.btnDownloadSoftware.Text = "Download Software Updates";
      this.btnDownloadSoftware.UseVisualStyleBackColor = true;
      this.btnDownloadSoftware.Click += new System.EventHandler(this.Action);
      // 
      // btnGetFrameworkVersions
      // 
      this.btnGetFrameworkVersions.Location = new System.Drawing.Point(14, 40);
      this.btnGetFrameworkVersions.Name = "btnGetFrameworkVersions";
      this.btnGetFrameworkVersions.Size = new System.Drawing.Size(213, 23);
      this.btnGetFrameworkVersions.TabIndex = 0;
      this.btnGetFrameworkVersions.Tag = "WsGetFrameworkVersions";
      this.btnGetFrameworkVersions.Text = "Get Framework Versions";
      this.btnGetFrameworkVersions.UseVisualStyleBackColor = true;
      this.btnGetFrameworkVersions.Click += new System.EventHandler(this.Action);
      // 
      // btnCheckForSoftwareUpdates
      // 
      this.btnCheckForSoftwareUpdates.Location = new System.Drawing.Point(234, 15);
      this.btnCheckForSoftwareUpdates.Name = "btnCheckForSoftwareUpdates";
      this.btnCheckForSoftwareUpdates.Size = new System.Drawing.Size(213, 23);
      this.btnCheckForSoftwareUpdates.TabIndex = 0;
      this.btnCheckForSoftwareUpdates.Tag = "WsCheckForUpdates";
      this.btnCheckForSoftwareUpdates.Text = "Check for Software Updates";
      this.btnCheckForSoftwareUpdates.UseVisualStyleBackColor = true;
      this.btnCheckForSoftwareUpdates.Click += new System.EventHandler(this.Action);
      // 
      // btnWsTestConnection
      // 
      this.btnWsTestConnection.Location = new System.Drawing.Point(14, 15);
      this.btnWsTestConnection.Name = "btnWsTestConnection";
      this.btnWsTestConnection.Size = new System.Drawing.Size(213, 23);
      this.btnWsTestConnection.TabIndex = 0;
      this.btnWsTestConnection.Tag = "WsTestConnection";
      this.btnWsTestConnection.Text = "Test Web Service Connection";
      this.btnWsTestConnection.UseVisualStyleBackColor = true;
      this.btnWsTestConnection.Click += new System.EventHandler(this.Action);
      // 
      // pnlLog
      // 
      this.pnlLog.Controls.Add(this.txtLog);
      this.pnlLog.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlLog.Location = new System.Drawing.Point(0, 145);
      this.pnlLog.Name = "pnlLog";
      this.pnlLog.Size = new System.Drawing.Size(724, 257);
      this.pnlLog.TabIndex = 1;
      // 
      // txtLog
      // 
      this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtLog.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtLog.Location = new System.Drawing.Point(0, 0);
      this.txtLog.Multiline = true;
      this.txtLog.Name = "txtLog";
      this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtLog.Size = new System.Drawing.Size(724, 257);
      this.txtLog.TabIndex = 0;
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.lblTest);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 402);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(724, 46);
      this.pnlMain.TabIndex = 2;
      // 
      // lblTest
      // 
      this.lblTest.AutoSize = true;
      this.lblTest.Location = new System.Drawing.Point(17, 16);
      this.lblTest.Name = "lblTest";
      this.lblTest.Size = new System.Drawing.Size(28, 13);
      this.lblTest.TabIndex = 0;
      this.lblTest.Text = "Test";
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageWebServiceUpdates);
      this.tabMain.Controls.Add(this.tabFileSystemUpdates);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(150, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 40);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(724, 105);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 5;
      // 
      // tabPageWebServiceUpdates
      // 
      this.tabPageWebServiceUpdates.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageWebServiceUpdates.Controls.Add(this.lblEnvironment);
      this.tabPageWebServiceUpdates.Controls.Add(this.btnWsTestConnection);
      this.tabPageWebServiceUpdates.Controls.Add(this.cboEnvironment);
      this.tabPageWebServiceUpdates.Controls.Add(this.btnCheckForSoftwareUpdates);
      this.tabPageWebServiceUpdates.Controls.Add(this.ckTrackPerformance);
      this.tabPageWebServiceUpdates.Controls.Add(this.btnGetFrameworkVersions);
      this.tabPageWebServiceUpdates.Controls.Add(this.btnDownloadSoftware);
      this.tabPageWebServiceUpdates.Location = new System.Drawing.Point(4, 22);
      this.tabPageWebServiceUpdates.Name = "tabPageWebServiceUpdates";
      this.tabPageWebServiceUpdates.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageWebServiceUpdates.Size = new System.Drawing.Size(716, 79);
      this.tabPageWebServiceUpdates.TabIndex = 0;
      this.tabPageWebServiceUpdates.Text = "Updates via Web Service";
      // 
      // tabFileSystemUpdates
      // 
      this.tabFileSystemUpdates.BackColor = System.Drawing.SystemColors.Control;
      this.tabFileSystemUpdates.Controls.Add(this.btnFsTestFileSystemAccess);
      this.tabFileSystemUpdates.Controls.Add(this.btnFsCheckForUpdates);
      this.tabFileSystemUpdates.Controls.Add(this.btnFsGetFrameworkVersions);
      this.tabFileSystemUpdates.Controls.Add(this.btnFsDownloadUpdates);
      this.tabFileSystemUpdates.Location = new System.Drawing.Point(4, 22);
      this.tabFileSystemUpdates.Name = "tabFileSystemUpdates";
      this.tabFileSystemUpdates.Padding = new System.Windows.Forms.Padding(3);
      this.tabFileSystemUpdates.Size = new System.Drawing.Size(716, 79);
      this.tabFileSystemUpdates.TabIndex = 1;
      this.tabFileSystemUpdates.Text = "Updates via File System";
      // 
      // pnlToolbar
      // 
      this.pnlToolbar.Controls.Add(this.btnTerminate);
      this.pnlToolbar.Controls.Add(this.btnClearDisplay);
      this.pnlToolbar.Controls.Add(this.btnContinue);
      this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
      this.pnlToolbar.Name = "pnlToolbar";
      this.pnlToolbar.Size = new System.Drawing.Size(724, 40);
      this.pnlToolbar.TabIndex = 6;
      // 
      // btnFsTestFileSystemAccess
      // 
      this.btnFsTestFileSystemAccess.Location = new System.Drawing.Point(14, 15);
      this.btnFsTestFileSystemAccess.Name = "btnFsTestFileSystemAccess";
      this.btnFsTestFileSystemAccess.Size = new System.Drawing.Size(213, 23);
      this.btnFsTestFileSystemAccess.TabIndex = 1;
      this.btnFsTestFileSystemAccess.Tag = "FsTestFileSystemAccess";
      this.btnFsTestFileSystemAccess.Text = "Test File System Access";
      this.btnFsTestFileSystemAccess.UseVisualStyleBackColor = true;
      this.btnFsTestFileSystemAccess.Click += new System.EventHandler(this.Action);
      // 
      // btnFsCheckForUpdates
      // 
      this.btnFsCheckForUpdates.Location = new System.Drawing.Point(234, 15);
      this.btnFsCheckForUpdates.Name = "btnFsCheckForUpdates";
      this.btnFsCheckForUpdates.Size = new System.Drawing.Size(213, 23);
      this.btnFsCheckForUpdates.TabIndex = 2;
      this.btnFsCheckForUpdates.Tag = "FsCheckForUpdates";
      this.btnFsCheckForUpdates.Text = "Check for Software Updates";
      this.btnFsCheckForUpdates.UseVisualStyleBackColor = true;
      this.btnFsCheckForUpdates.Click += new System.EventHandler(this.Action);
      // 
      // btnFsGetFrameworkVersions
      // 
      this.btnFsGetFrameworkVersions.Location = new System.Drawing.Point(14, 40);
      this.btnFsGetFrameworkVersions.Name = "btnFsGetFrameworkVersions";
      this.btnFsGetFrameworkVersions.Size = new System.Drawing.Size(213, 23);
      this.btnFsGetFrameworkVersions.TabIndex = 3;
      this.btnFsGetFrameworkVersions.Tag = "FsGetFrameworkVersions";
      this.btnFsGetFrameworkVersions.Text = "Get Framework Versions";
      this.btnFsGetFrameworkVersions.UseVisualStyleBackColor = true;
      this.btnFsGetFrameworkVersions.Click += new System.EventHandler(this.Action);
      // 
      // btnFsDownloadUpdates
      // 
      this.btnFsDownloadUpdates.Location = new System.Drawing.Point(234, 41);
      this.btnFsDownloadUpdates.Name = "btnFsDownloadUpdates";
      this.btnFsDownloadUpdates.Size = new System.Drawing.Size(213, 23);
      this.btnFsDownloadUpdates.TabIndex = 4;
      this.btnFsDownloadUpdates.Tag = "FsDownloadSoftware";
      this.btnFsDownloadUpdates.Text = "Download Software Updates";
      this.btnFsDownloadUpdates.UseVisualStyleBackColor = true;
      this.btnFsDownloadUpdates.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 448);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(724, 23);
      this.lblStatus.TabIndex = 3;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // frmUpdateManager
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(724, 471);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlLog);
      this.Controls.Add(this.pnlTopControl);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmUpdateManager";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Software Updates";
      this.pnlTopControl.ResumeLayout(false);
      this.pnlLog.ResumeLayout(false);
      this.pnlLog.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      this.pnlMain.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabPageWebServiceUpdates.ResumeLayout(false);
      this.tabPageWebServiceUpdates.PerformLayout();
      this.tabFileSystemUpdates.ResumeLayout(false);
      this.pnlToolbar.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlTopControl;
    private System.Windows.Forms.Button btnWsTestConnection;
    private System.Windows.Forms.Panel pnlLog;
    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.ComboBox cboEnvironment;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.Button btnTerminate;
    private System.Windows.Forms.CheckBox ckTrackPerformance;
    private System.Windows.Forms.Button btnCheckForSoftwareUpdates;
    private System.Windows.Forms.Button btnContinue;
    private System.Windows.Forms.Button btnDownloadSoftware;
    private System.Windows.Forms.Button btnGetFrameworkVersions;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.Label lblTest;
    private System.Windows.Forms.Button btnClearDisplay;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageWebServiceUpdates;
    private System.Windows.Forms.TabPage tabFileSystemUpdates;
    private System.Windows.Forms.Panel pnlToolbar;
    private System.Windows.Forms.Button btnFsTestFileSystemAccess;
    private System.Windows.Forms.Button btnFsCheckForUpdates;
    private System.Windows.Forms.Button btnFsGetFrameworkVersions;
    private System.Windows.Forms.Button btnFsDownloadUpdates;
    private System.Windows.Forms.Label lblStatus;
  }
}
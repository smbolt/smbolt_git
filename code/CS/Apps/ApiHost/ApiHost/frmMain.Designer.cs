namespace ApiHost
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
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnClearDisplay = new System.Windows.Forms.Button();
      this.btnStopApiHost = new System.Windows.Forms.Button();
      this.btnStartApiHost = new System.Windows.Forms.Button();
      this.splitMain = new System.Windows.Forms.SplitContainer();
      this.txtDisplay = new System.Windows.Forms.TextBox();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageDisplay = new System.Windows.Forms.TabPage();
      this.tabPageLog = new System.Windows.Forms.TabPage();
      this.tvMain = new System.Windows.Forms.TreeView();
      this.tabPageBrowser = new System.Windows.Forms.TabPage();
      this.browser = new System.Windows.Forms.WebBrowser();
      this.btnGo = new System.Windows.Forms.Button();
      this.btnClearBrowser = new System.Windows.Forms.Button();
      this.cboApiRequest = new System.Windows.Forms.ComboBox();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
      this.splitMain.Panel1.SuspendLayout();
      this.splitMain.Panel2.SuspendLayout();
      this.splitMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageDisplay.SuspendLayout();
      this.tabPageBrowser.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(4, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
      this.mnuMain.Size = new System.Drawing.Size(1271, 24);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFileExit
      });
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
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(4, 649);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(1271, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.cboApiRequest);
      this.pnlTop.Controls.Add(this.btnClearDisplay);
      this.pnlTop.Controls.Add(this.btnStopApiHost);
      this.pnlTop.Controls.Add(this.btnClearBrowser);
      this.pnlTop.Controls.Add(this.btnGo);
      this.pnlTop.Controls.Add(this.btnStartApiHost);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1271, 46);
      this.pnlTop.TabIndex = 2;
      //
      // btnClearDisplay
      //
      this.btnClearDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClearDisplay.Location = new System.Drawing.Point(1139, 11);
      this.btnClearDisplay.Name = "btnClearDisplay";
      this.btnClearDisplay.Size = new System.Drawing.Size(120, 23);
      this.btnClearDisplay.TabIndex = 0;
      this.btnClearDisplay.Tag = "ClearDisplay";
      this.btnClearDisplay.Text = "Clear Display";
      this.btnClearDisplay.UseVisualStyleBackColor = true;
      this.btnClearDisplay.Click += new System.EventHandler(this.Action);
      //
      // btnStopApiHost
      //
      this.btnStopApiHost.Location = new System.Drawing.Point(138, 11);
      this.btnStopApiHost.Name = "btnStopApiHost";
      this.btnStopApiHost.Size = new System.Drawing.Size(120, 23);
      this.btnStopApiHost.TabIndex = 0;
      this.btnStopApiHost.Tag = "StopApiHost";
      this.btnStopApiHost.Text = "Stop API Host";
      this.btnStopApiHost.UseVisualStyleBackColor = true;
      this.btnStopApiHost.Click += new System.EventHandler(this.Action);
      //
      // btnStartApiHost
      //
      this.btnStartApiHost.Location = new System.Drawing.Point(12, 11);
      this.btnStartApiHost.Name = "btnStartApiHost";
      this.btnStartApiHost.Size = new System.Drawing.Size(120, 23);
      this.btnStartApiHost.TabIndex = 0;
      this.btnStartApiHost.Tag = "StartApiHost";
      this.btnStartApiHost.Text = "Start API Host";
      this.btnStartApiHost.UseVisualStyleBackColor = true;
      this.btnStartApiHost.Click += new System.EventHandler(this.Action);
      //
      // splitMain
      //
      this.splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitMain.Location = new System.Drawing.Point(4, 70);
      this.splitMain.Name = "splitMain";
      //
      // splitMain.Panel1
      //
      this.splitMain.Panel1.Controls.Add(this.tvMain);
      //
      // splitMain.Panel2
      //
      this.splitMain.Panel2.Controls.Add(this.tabMain);
      this.splitMain.Size = new System.Drawing.Size(1271, 579);
      this.splitMain.SplitterDistance = 285;
      this.splitMain.TabIndex = 3;
      //
      // txtDisplay
      //
      this.txtDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtDisplay.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtDisplay.Location = new System.Drawing.Point(3, 3);
      this.txtDisplay.Multiline = true;
      this.txtDisplay.Name = "txtDisplay";
      this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtDisplay.Size = new System.Drawing.Size(735, 574);
      this.txtDisplay.TabIndex = 0;
      this.txtDisplay.WordWrap = false;
      //
      // tabMain
      //
      this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                             | System.Windows.Forms.AnchorStyles.Left)
                             | System.Windows.Forms.AnchorStyles.Right)));
      this.tabMain.Controls.Add(this.tabPageDisplay);
      this.tabMain.Controls.Add(this.tabPageLog);
      this.tabMain.Controls.Add(this.tabPageBrowser);
      this.tabMain.ItemSize = new System.Drawing.Size(1, 1);
      this.tabMain.Location = new System.Drawing.Point(-5, -6);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(991, 589);
      this.tabMain.TabIndex = 4;
      //
      // tabPageDisplay
      //
      this.tabPageDisplay.Controls.Add(this.txtDisplay);
      this.tabPageDisplay.Location = new System.Drawing.Point(4, 5);
      this.tabPageDisplay.Name = "tabPageDisplay";
      this.tabPageDisplay.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageDisplay.Size = new System.Drawing.Size(741, 580);
      this.tabPageDisplay.TabIndex = 0;
      this.tabPageDisplay.UseVisualStyleBackColor = true;
      //
      // tabPageLog
      //
      this.tabPageLog.Location = new System.Drawing.Point(4, 5);
      this.tabPageLog.Name = "tabPageLog";
      this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageLog.Size = new System.Drawing.Size(461, 226);
      this.tabPageLog.TabIndex = 1;
      this.tabPageLog.UseVisualStyleBackColor = true;
      //
      // tvMain
      //
      this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMain.Location = new System.Drawing.Point(0, 0);
      this.tvMain.Name = "tvMain";
      this.tvMain.Size = new System.Drawing.Size(283, 577);
      this.tvMain.TabIndex = 0;
      //
      // tabPageBrowser
      //
      this.tabPageBrowser.Controls.Add(this.browser);
      this.tabPageBrowser.Location = new System.Drawing.Point(4, 5);
      this.tabPageBrowser.Name = "tabPageBrowser";
      this.tabPageBrowser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.tabPageBrowser.Size = new System.Drawing.Size(983, 580);
      this.tabPageBrowser.TabIndex = 2;
      this.tabPageBrowser.UseVisualStyleBackColor = true;
      //
      // browser
      //
      this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.browser.Location = new System.Drawing.Point(0, 0);
      this.browser.MinimumSize = new System.Drawing.Size(20, 20);
      this.browser.Name = "browser";
      this.browser.Size = new System.Drawing.Size(983, 580);
      this.browser.TabIndex = 0;
      //
      // btnGo
      //
      this.btnGo.Location = new System.Drawing.Point(766, 11);
      this.btnGo.Name = "btnGo";
      this.btnGo.Size = new System.Drawing.Size(39, 23);
      this.btnGo.TabIndex = 0;
      this.btnGo.Tag = "Go";
      this.btnGo.Text = "Go";
      this.btnGo.UseVisualStyleBackColor = true;
      this.btnGo.Click += new System.EventHandler(this.Action);
      //
      // btnClearBrowser
      //
      this.btnClearBrowser.Location = new System.Drawing.Point(808, 11);
      this.btnClearBrowser.Name = "btnClearBrowser";
      this.btnClearBrowser.Size = new System.Drawing.Size(51, 23);
      this.btnClearBrowser.TabIndex = 0;
      this.btnClearBrowser.Tag = "ClearBrowser";
      this.btnClearBrowser.Text = "Clear";
      this.btnClearBrowser.UseVisualStyleBackColor = true;
      this.btnClearBrowser.Click += new System.EventHandler(this.Action);
      //
      // cboApiRequest
      //
      this.cboApiRequest.FormattingEnabled = true;
      this.cboApiRequest.Location = new System.Drawing.Point(289, 12);
      this.cboApiRequest.Name = "cboApiRequest";
      this.cboApiRequest.Size = new System.Drawing.Size(474, 21);
      this.cboApiRequest.TabIndex = 1;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1279, 672);
      this.Controls.Add(this.splitMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "API Host";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.splitMain.Panel1.ResumeLayout(false);
      this.splitMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
      this.splitMain.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageDisplay.ResumeLayout(false);
      this.tabPageDisplay.PerformLayout();
      this.tabPageBrowser.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.SplitContainer splitMain;
    private System.Windows.Forms.TextBox txtDisplay;
    private System.Windows.Forms.Button btnStartApiHost;
    private System.Windows.Forms.Button btnStopApiHost;
    private System.Windows.Forms.Button btnClearDisplay;
    private System.Windows.Forms.TreeView tvMain;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageDisplay;
    private System.Windows.Forms.TabPage tabPageLog;
    private System.Windows.Forms.TabPage tabPageBrowser;
    private System.Windows.Forms.Button btnGo;
    private System.Windows.Forms.WebBrowser browser;
    private System.Windows.Forms.Button btnClearBrowser;
    private System.Windows.Forms.ComboBox cboApiRequest;
  }
}


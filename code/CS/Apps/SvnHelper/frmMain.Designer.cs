namespace Org.SvnHelper
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
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlBackground = new System.Windows.Forms.Panel();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageSVN = new System.Windows.Forms.TabPage();
      this.lstSvnSelect = new System.Windows.Forms.CheckedListBox();
      this.lblSvnSelect = new System.Windows.Forms.Label();
      this.tabPageSync = new System.Windows.Forms.TabPage();
      this.lstSyncSelect = new System.Windows.Forms.CheckedListBox();
      this.lblSyncSelect = new System.Windows.Forms.Label();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.pnlTopControl = new System.Windows.Forms.Panel();
      this.progBarMain = new System.Windows.Forms.ProgressBar();
      this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
      this.ctxMnuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMnuSvnUpdate = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuSvnCommit = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuShowForm = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuExit = new System.Windows.Forms.ToolStripMenuItem();
      this.buttonBar = new Org.CTL.ButtonBar();
      this.mnuMain.SuspendLayout();
      this.pnlBackground.SuspendLayout();
      this.pnlMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageSVN.SuspendLayout();
      this.tabPageSync.SuspendLayout();
      this.pnlTopControl.SuspendLayout();
      this.ctxMnuTray.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(832, 24);
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
      this.mnuFile.Text = "File";
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
      this.lblStatus.Location = new System.Drawing.Point(0, 519);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(832, 24);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlBackground
      //
      this.pnlBackground.Controls.Add(this.pnlMain);
      this.pnlBackground.Controls.Add(this.pnlTopControl);
      this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlBackground.Location = new System.Drawing.Point(0, 24);
      this.pnlBackground.Name = "pnlBackground";
      this.pnlBackground.Size = new System.Drawing.Size(832, 495);
      this.pnlBackground.TabIndex = 2;
      //
      // pnlMain
      //
      this.pnlMain.Controls.Add(this.splitContainer1);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 57);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
      this.pnlMain.Size = new System.Drawing.Size(832, 438);
      this.pnlMain.TabIndex = 2;
      //
      // splitContainer1
      //
      this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 8);
      this.splitContainer1.Name = "splitContainer1";
      //
      // splitContainer1.Panel1
      //
      this.splitContainer1.Panel1.Controls.Add(this.tabMain);
      //
      // splitContainer1.Panel2
      //
      this.splitContainer1.Panel2.Controls.Add(this.txtOut);
      this.splitContainer1.Size = new System.Drawing.Size(832, 430);
      this.splitContainer1.SplitterDistance = 208;
      this.splitContainer1.SplitterWidth = 3;
      this.splitContainer1.TabIndex = 4;
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageSVN);
      this.tabMain.Controls.Add(this.tabPageSync);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(80, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(204, 426);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 4;
      this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
      //
      // tabPageSVN
      //
      this.tabPageSVN.Controls.Add(this.lstSvnSelect);
      this.tabPageSVN.Controls.Add(this.lblSvnSelect);
      this.tabPageSVN.Location = new System.Drawing.Point(4, 22);
      this.tabPageSVN.Name = "tabPageSVN";
      this.tabPageSVN.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageSVN.Size = new System.Drawing.Size(196, 400);
      this.tabPageSVN.TabIndex = 0;
      this.tabPageSVN.Tag = "SVN";
      this.tabPageSVN.Text = "SVN";
      this.tabPageSVN.UseVisualStyleBackColor = true;
      //
      // lstSvnSelect
      //
      this.lstSvnSelect.CheckOnClick = true;
      this.lstSvnSelect.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstSvnSelect.FormattingEnabled = true;
      this.lstSvnSelect.Location = new System.Drawing.Point(3, 30);
      this.lstSvnSelect.Name = "lstSvnSelect";
      this.lstSvnSelect.Size = new System.Drawing.Size(190, 367);
      this.lstSvnSelect.TabIndex = 2;
      this.lstSvnSelect.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstSelect_ItemCheck);
      //
      // lblSvnSelect
      //
      this.lblSvnSelect.BackColor = System.Drawing.SystemColors.Control;
      this.lblSvnSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblSvnSelect.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblSvnSelect.Location = new System.Drawing.Point(3, 3);
      this.lblSvnSelect.Name = "lblSvnSelect";
      this.lblSvnSelect.Size = new System.Drawing.Size(190, 27);
      this.lblSvnSelect.TabIndex = 3;
      this.lblSvnSelect.Text = "Select Items to Process";
      this.lblSvnSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // tabPageSync
      //
      this.tabPageSync.Controls.Add(this.lstSyncSelect);
      this.tabPageSync.Controls.Add(this.lblSyncSelect);
      this.tabPageSync.Location = new System.Drawing.Point(4, 22);
      this.tabPageSync.Name = "tabPageSync";
      this.tabPageSync.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageSync.Size = new System.Drawing.Size(196, 400);
      this.tabPageSync.TabIndex = 1;
      this.tabPageSync.Tag = "SYNC";
      this.tabPageSync.Text = "Sync";
      this.tabPageSync.UseVisualStyleBackColor = true;
      //
      // lstSyncSelect
      //
      this.lstSyncSelect.CheckOnClick = true;
      this.lstSyncSelect.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstSyncSelect.FormattingEnabled = true;
      this.lstSyncSelect.Location = new System.Drawing.Point(3, 30);
      this.lstSyncSelect.Name = "lstSyncSelect";
      this.lstSyncSelect.Size = new System.Drawing.Size(190, 367);
      this.lstSyncSelect.TabIndex = 4;
      this.lstSyncSelect.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstSyncSelect_ItemCheck);
      //
      // lblSyncSelect
      //
      this.lblSyncSelect.BackColor = System.Drawing.SystemColors.Control;
      this.lblSyncSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblSyncSelect.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblSyncSelect.Location = new System.Drawing.Point(3, 3);
      this.lblSyncSelect.Name = "lblSyncSelect";
      this.lblSyncSelect.Size = new System.Drawing.Size(190, 27);
      this.lblSyncSelect.TabIndex = 5;
      this.lblSyncSelect.Text = "Select Sync Tasks to Process";
      this.lblSyncSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtOut
      //
      this.txtOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(0, 0);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(617, 426);
      this.txtOut.TabIndex = 1;
      this.txtOut.WordWrap = false;
      //
      // pnlTopControl
      //
      this.pnlTopControl.BackColor = System.Drawing.Color.White;
      this.pnlTopControl.Controls.Add(this.buttonBar);
      this.pnlTopControl.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTopControl.Location = new System.Drawing.Point(0, 0);
      this.pnlTopControl.Name = "pnlTopControl";
      this.pnlTopControl.Size = new System.Drawing.Size(832, 57);
      this.pnlTopControl.TabIndex = 0;
      //
      // progBarMain
      //
      this.progBarMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.progBarMain.Location = new System.Drawing.Point(419, 524);
      this.progBarMain.Name = "progBarMain";
      this.progBarMain.Size = new System.Drawing.Size(407, 14);
      this.progBarMain.TabIndex = 0;
      //
      // notifyIconMain
      //
      this.notifyIconMain.BalloonTipText = "ORG SvnHelper";
      this.notifyIconMain.BalloonTipTitle = "ORG SvnHelper";
      this.notifyIconMain.ContextMenuStrip = this.ctxMnuTray;
      this.notifyIconMain.Text = "SvnHelper";
      this.notifyIconMain.Visible = true;
      this.notifyIconMain.BalloonTipClicked += new System.EventHandler(this.notifyIconMain_BalloonTipClicked);
      this.notifyIconMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconMain_MouseDoubleClick);
      //
      // ctxMnuTray
      //
      this.ctxMnuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMnuSvnUpdate,
        this.ctxMnuSvnCommit,
        this.ctxMnuShowForm,
        this.ctxMnuExit
      });
      this.ctxMnuTray.Name = "ctxMnuTray";
      this.ctxMnuTray.Size = new System.Drawing.Size(144, 92);
      //
      // ctxMnuSvnUpdate
      //
      this.ctxMnuSvnUpdate.Name = "ctxMnuSvnUpdate";
      this.ctxMnuSvnUpdate.Size = new System.Drawing.Size(143, 22);
      this.ctxMnuSvnUpdate.Tag = "SvnUpdate";
      this.ctxMnuSvnUpdate.Text = "SVN &Update";
      this.ctxMnuSvnUpdate.Click += new System.EventHandler(this.Action);
      //
      // ctxMnuSvnCommit
      //
      this.ctxMnuSvnCommit.Name = "ctxMnuSvnCommit";
      this.ctxMnuSvnCommit.Size = new System.Drawing.Size(143, 22);
      this.ctxMnuSvnCommit.Tag = "SvnCommit";
      this.ctxMnuSvnCommit.Text = "SVN &Commit";
      this.ctxMnuSvnCommit.Click += new System.EventHandler(this.Action);
      //
      // ctxMnuShowForm
      //
      this.ctxMnuShowForm.Name = "ctxMnuShowForm";
      this.ctxMnuShowForm.Size = new System.Drawing.Size(143, 22);
      this.ctxMnuShowForm.Tag = "ShowForm";
      this.ctxMnuShowForm.Text = "&Show Form";
      this.ctxMnuShowForm.Click += new System.EventHandler(this.Action);
      //
      // ctxMnuExit
      //
      this.ctxMnuExit.Name = "ctxMnuExit";
      this.ctxMnuExit.Size = new System.Drawing.Size(143, 22);
      this.ctxMnuExit.Tag = "Exit";
      this.ctxMnuExit.Text = "E&xit";
      this.ctxMnuExit.Click += new System.EventHandler(this.Action);
      //
      // buttonBar
      //
      this.buttonBar.ButtonSize = new System.Drawing.Size(76, 54);
      this.buttonBar.ConfigString = resources.GetString("buttonBar.ConfigString");
      this.buttonBar.ControlOrientation = Org.CTL.ControlOrientation.Horizontal;
      this.buttonBar.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(176)))), ((int)(((byte)(214)))));
      this.buttonBar.GradientColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
      this.buttonBar.LabelVerticalPosition = Org.CTL.VerticalPosition.Bottom;
      this.buttonBar.Location = new System.Drawing.Point(2, 0);
      this.buttonBar.Name = "buttonBar";
      this.buttonBar.Size = new System.Drawing.Size(644, 56);
      this.buttonBar.TabIndex = 1;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(832, 543);
      this.Controls.Add(this.progBarMain);
      this.Controls.Add(this.pnlBackground);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SVN Helper - V1.0";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlBackground.ResumeLayout(false);
      this.pnlMain.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageSVN.ResumeLayout(false);
      this.tabPageSync.ResumeLayout(false);
      this.pnlTopControl.ResumeLayout(false);
      this.ctxMnuTray.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlBackground;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.Panel pnlTopControl;
    private System.Windows.Forms.NotifyIcon notifyIconMain;
    private System.Windows.Forms.ContextMenuStrip ctxMnuTray;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuSvnUpdate;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuSvnCommit;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuShowForm;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuExit;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.Label lblSvnSelect;
    private System.Windows.Forms.CheckedListBox lstSvnSelect;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ProgressBar progBarMain;
    private Org.CTL.ButtonBar buttonBar;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageSVN;
    private System.Windows.Forms.TabPage tabPageSync;
    private System.Windows.Forms.CheckedListBox lstSyncSelect;
    private System.Windows.Forms.Label lblSyncSelect;
  }
}


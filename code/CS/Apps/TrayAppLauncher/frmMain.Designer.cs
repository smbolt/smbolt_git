namespace Org.TrayAppLauncher
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
      this.label1 = new System.Windows.Forms.Label();
      this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
      this.ctxMnuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMnuExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMain.SuspendLayout();
      this.ctxMnuTray.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(301, 24);
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
      this.mnuFileExit.Size = new System.Drawing.Size(180, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 66);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(301, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 37);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(190, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Tray Application Launcher is starting... ";
      // 
      // notifyIconMain
      // 
      this.notifyIconMain.BalloonTipText = "Application Launcher";
      this.notifyIconMain.BalloonTipTitle = "Application Launcher";
      this.notifyIconMain.ContextMenuStrip = this.ctxMnuTray;
      this.notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
      this.notifyIconMain.Text = "Application Launcher";
      this.notifyIconMain.Visible = true;
      // 
      // ctxMnuTray
      // 
      this.ctxMnuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMnuExit});
      this.ctxMnuTray.Name = "ctxMnuTray";
      this.ctxMnuTray.Size = new System.Drawing.Size(93, 26);
      // 
      // ctxMnuExit
      // 
      this.ctxMnuExit.Name = "ctxMnuExit";
      this.ctxMnuExit.Size = new System.Drawing.Size(92, 22);
      this.ctxMnuExit.Tag = "Exit";
      this.ctxMnuExit.Text = "E&xit";
      this.ctxMnuExit.Click += new System.EventHandler(this.Action);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(301, 89);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Tray Application Launcher";
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.ctxMnuTray.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.NotifyIcon notifyIconMain;
    private System.Windows.Forms.ContextMenuStrip ctxMnuTray;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuExit;
  }
}


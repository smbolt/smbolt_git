namespace Org.GS.Dynamic
{
    partial class DynamicFormBase
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxMnuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trayOption1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayOption11ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayOption12ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayOption2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.ctxMnuTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(792, 24);
            this.mnuMain.TabIndex = 1;
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
            this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
            this.mnuFileExit.Tag = "FILE_EXIT";
            this.mnuFileExit.Text = "E&xit";
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 460);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(792, 20);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 15);
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIconMain.BalloonTipText = "Notify tip text shows here";
            this.notifyIconMain.BalloonTipTitle = "Notify Title";
            this.notifyIconMain.ContextMenuStrip = this.ctxMnuTray;
            this.notifyIconMain.Tag = "NOTIFY_ICON";
            this.notifyIconMain.Text = "Notify";
            this.notifyIconMain.Visible = true;
            this.notifyIconMain.Click += new System.EventHandler(this.Action);
            // 
            // ctxMnuTray
            // 
            this.ctxMnuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayOption1ToolStripMenuItem,
            this.trayOption2ToolStripMenuItem});
            this.ctxMnuTray.Name = "ctxMnuTray";
            this.ctxMnuTray.Size = new System.Drawing.Size(147, 48);
            // 
            // trayOption1ToolStripMenuItem
            // 
            this.trayOption1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayOption11ToolStripMenuItem,
            this.trayOption12ToolStripMenuItem});
            this.trayOption1ToolStripMenuItem.Name = "trayOption1ToolStripMenuItem";
            this.trayOption1ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.trayOption1ToolStripMenuItem.Text = "Tray Option 1";
            // 
            // trayOption11ToolStripMenuItem
            // 
            this.trayOption11ToolStripMenuItem.Name = "trayOption11ToolStripMenuItem";
            this.trayOption11ToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.trayOption11ToolStripMenuItem.Text = "Tray Option 1.1";
            // 
            // trayOption12ToolStripMenuItem
            // 
            this.trayOption12ToolStripMenuItem.Name = "trayOption12ToolStripMenuItem";
            this.trayOption12ToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.trayOption12ToolStripMenuItem.Text = "Tray Option 1.2";
            // 
            // trayOption2ToolStripMenuItem
            // 
            this.trayOption2ToolStripMenuItem.Name = "trayOption2ToolStripMenuItem";
            this.trayOption2ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.trayOption2ToolStripMenuItem.Text = "Tray Option 2";
            // 
            // DynamicFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mnuMain);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DynamicFormBase";
            this.Size = new System.Drawing.Size(792, 480);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ctxMnuTray.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.ContextMenuStrip ctxMnuTray;
        private System.Windows.Forms.ToolStripMenuItem trayOption1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trayOption11ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trayOption12ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trayOption2ToolStripMenuItem;
    }
}

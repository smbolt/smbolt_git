namespace Adsdi.Tools.ResourceEditor
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
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnGetResourceFile = new System.Windows.Forms.Button();
            this.lblResourceFile = new System.Windows.Forms.Label();
            this.txtResourceFile = new System.Windows.Forms.TextBox();
            this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.lbResources = new System.Windows.Forms.ListBox();
            this.lblResourcesInFile = new System.Windows.Forms.Label();
            this.btnShowResource = new System.Windows.Forms.Button();
            this.lblEncryptionKey = new System.Windows.Forms.Label();
            this.txtEncryptionKey = new System.Windows.Forms.TextBox();
            this.mnuMain.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 24);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1252, 34);
            this.pnlToolbar.TabIndex = 0;
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1252, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(0, 722);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(1252, 30);
            this.pnlStatus.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(1252, 30);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnShowResource);
            this.pnlMain.Controls.Add(this.lblResourcesInFile);
            this.pnlMain.Controls.Add(this.lbResources);
            this.pnlMain.Controls.Add(this.txtEncryptionKey);
            this.pnlMain.Controls.Add(this.lblEncryptionKey);
            this.pnlMain.Controls.Add(this.txtResourceFile);
            this.pnlMain.Controls.Add(this.lblResourceFile);
            this.pnlMain.Controls.Add(this.btnGetResourceFile);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 58);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1252, 664);
            this.pnlMain.TabIndex = 3;
            // 
            // btnGetResourceFile
            // 
            this.btnGetResourceFile.Location = new System.Drawing.Point(510, 45);
            this.btnGetResourceFile.Name = "btnGetResourceFile";
            this.btnGetResourceFile.Size = new System.Drawing.Size(124, 22);
            this.btnGetResourceFile.TabIndex = 0;
            this.btnGetResourceFile.Text = "Browse...";
            this.btnGetResourceFile.UseVisualStyleBackColor = true;
            this.btnGetResourceFile.Click += new System.EventHandler(this.btnGetResourceFile_Click);
            // 
            // lblResourceFile
            // 
            this.lblResourceFile.Location = new System.Drawing.Point(43, 28);
            this.lblResourceFile.Name = "lblResourceFile";
            this.lblResourceFile.Size = new System.Drawing.Size(79, 17);
            this.lblResourceFile.TabIndex = 1;
            this.lblResourceFile.Text = "Resource File:";
            // 
            // txtResourceFile
            // 
            this.txtResourceFile.Location = new System.Drawing.Point(46, 46);
            this.txtResourceFile.Name = "txtResourceFile";
            this.txtResourceFile.Size = new System.Drawing.Size(459, 20);
            this.txtResourceFile.TabIndex = 2;
            // 
            // dlgFileOpen
            // 
            this.dlgFileOpen.Title = "Locate Resources File";
            // 
            // lbResources
            // 
            this.lbResources.FormattingEnabled = true;
            this.lbResources.Location = new System.Drawing.Point(46, 114);
            this.lbResources.Name = "lbResources";
            this.lbResources.Size = new System.Drawing.Size(458, 95);
            this.lbResources.TabIndex = 3;
            // 
            // lblResourcesInFile
            // 
            this.lblResourcesInFile.AutoSize = true;
            this.lblResourcesInFile.Location = new System.Drawing.Point(46, 97);
            this.lblResourcesInFile.Name = "lblResourcesInFile";
            this.lblResourcesInFile.Size = new System.Drawing.Size(91, 13);
            this.lblResourcesInFile.TabIndex = 4;
            this.lblResourcesInFile.Text = "Resources in File:";
            // 
            // btnShowResource
            // 
            this.btnShowResource.Location = new System.Drawing.Point(510, 114);
            this.btnShowResource.Name = "btnShowResource";
            this.btnShowResource.Size = new System.Drawing.Size(124, 22);
            this.btnShowResource.TabIndex = 5;
            this.btnShowResource.Text = "Show Resource";
            this.btnShowResource.UseVisualStyleBackColor = true;
            this.btnShowResource.Click += new System.EventHandler(this.btnShowResource_Click);
            // 
            // lblEncryptionKey
            // 
            this.lblEncryptionKey.Location = new System.Drawing.Point(45, 225);
            this.lblEncryptionKey.Name = "lblEncryptionKey";
            this.lblEncryptionKey.Size = new System.Drawing.Size(109, 19);
            this.lblEncryptionKey.TabIndex = 1;
            this.lblEncryptionKey.Text = "Encryption Key:";
            // 
            // txtEncryptionKey
            // 
            this.txtEncryptionKey.Location = new System.Drawing.Point(46, 241);
            this.txtEncryptionKey.Name = "txtEncryptionKey";
            this.txtEncryptionKey.Size = new System.Drawing.Size(459, 20);
            this.txtEncryptionKey.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 752);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resource Editor - v1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtResourceFile;
        private System.Windows.Forms.Label lblResourceFile;
        private System.Windows.Forms.Button btnGetResourceFile;
        private System.Windows.Forms.OpenFileDialog dlgFileOpen;
        private System.Windows.Forms.Label lblResourcesInFile;
        private System.Windows.Forms.ListBox lbResources;
        private System.Windows.Forms.Button btnShowResource;
        private System.Windows.Forms.TextBox txtEncryptionKey;
        private System.Windows.Forms.Label lblEncryptionKey;
    }
}


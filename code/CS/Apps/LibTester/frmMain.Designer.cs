namespace Org.LibTester
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
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblPath = new System.Windows.Forms.Label();
      this.txtArchivePath = new System.Windows.Forms.TextBox();
      this.btnProcessArchive = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageOutput = new System.Windows.Forms.TabPage();
      this.txtFileCountLimit = new System.Windows.Forms.TextBox();
      this.lblFileCountLimit = new System.Windows.Forms.Label();
      this.btnProcessRootFolder = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageOutput.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
      this.mnuMain.Size = new System.Drawing.Size(1672, 25);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 19);
      this.mnuFile.Text = "&File";
      // 
      // mnuFileExit
      // 
      this.mnuFileExit.Name = "mnuFileExit";
      this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.btnProcessRootFolder);
      this.pnlTop.Controls.Add(this.txtFileCountLimit);
      this.pnlTop.Controls.Add(this.lblFileCountLimit);
      this.pnlTop.Controls.Add(this.lblPath);
      this.pnlTop.Controls.Add(this.txtArchivePath);
      this.pnlTop.Controls.Add(this.btnProcessArchive);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 25);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1672, 108);
      this.pnlTop.TabIndex = 1;
      // 
      // lblPath
      // 
      this.lblPath.AutoSize = true;
      this.lblPath.Location = new System.Drawing.Point(12, 15);
      this.lblPath.Name = "lblPath";
      this.lblPath.Size = new System.Drawing.Size(42, 20);
      this.lblPath.TabIndex = 2;
      this.lblPath.Text = "Path";
      // 
      // txtArchivePath
      // 
      this.txtArchivePath.Location = new System.Drawing.Point(12, 47);
      this.txtArchivePath.Name = "txtArchivePath";
      this.txtArchivePath.Size = new System.Drawing.Size(620, 26);
      this.txtArchivePath.TabIndex = 1;
      this.txtArchivePath.Tag = "ArchivePath";
      // 
      // btnProcessArchive
      // 
      this.btnProcessArchive.Location = new System.Drawing.Point(654, 15);
      this.btnProcessArchive.Name = "btnProcessArchive";
      this.btnProcessArchive.Size = new System.Drawing.Size(245, 35);
      this.btnProcessArchive.TabIndex = 0;
      this.btnProcessArchive.Tag = "ProcessArchive";
      this.btnProcessArchive.Text = "Process Archive";
      this.btnProcessArchive.UseVisualStyleBackColor = true;
      this.btnProcessArchive.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 1016);
      this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1672, 29);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtOut
      // 
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(4, 5);
      this.txtOut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1656, 847);
      this.txtOut.TabIndex = 3;
      this.txtOut.WordWrap = false;
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageOutput);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(120, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 133);
      this.tabMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1672, 883);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 4;
      // 
      // tabPageOutput
      // 
      this.tabPageOutput.Controls.Add(this.txtOut);
      this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
      this.tabPageOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageOutput.Name = "tabPageOutput";
      this.tabPageOutput.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageOutput.Size = new System.Drawing.Size(1664, 857);
      this.tabPageOutput.TabIndex = 1;
      this.tabPageOutput.Text = "Output";
      this.tabPageOutput.UseVisualStyleBackColor = true;
      // 
      // txtFileCountLimit
      // 
      this.txtFileCountLimit.Location = new System.Drawing.Point(1059, 47);
      this.txtFileCountLimit.Name = "txtFileCountLimit";
      this.txtFileCountLimit.Size = new System.Drawing.Size(142, 26);
      this.txtFileCountLimit.TabIndex = 11;
      this.txtFileCountLimit.Tag = "txtFileCountLimit";
      // 
      // lblFileCountLimit
      // 
      this.lblFileCountLimit.BackColor = System.Drawing.SystemColors.Control;
      this.lblFileCountLimit.Location = new System.Drawing.Point(918, 44);
      this.lblFileCountLimit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblFileCountLimit.Name = "lblFileCountLimit";
      this.lblFileCountLimit.Size = new System.Drawing.Size(136, 32);
      this.lblFileCountLimit.TabIndex = 10;
      this.lblFileCountLimit.Text = "File Count Limit:";
      this.lblFileCountLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // btnProcessRootFolder
      // 
      this.btnProcessRootFolder.Location = new System.Drawing.Point(654, 65);
      this.btnProcessRootFolder.Name = "btnProcessRootFolder";
      this.btnProcessRootFolder.Size = new System.Drawing.Size(245, 35);
      this.btnProcessRootFolder.TabIndex = 12;
      this.btnProcessRootFolder.Tag = "ProcessRootFolder";
      this.btnProcessRootFolder.Text = "Process Root Folder";
      this.btnProcessRootFolder.UseVisualStyleBackColor = true;
      this.btnProcessRootFolder.Click += new System.EventHandler(this.Action);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1672, 1045);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Library Tester";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabPageOutput.ResumeLayout(false);
      this.tabPageOutput.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageOutput;
    private System.Windows.Forms.TextBox txtArchivePath;
    private System.Windows.Forms.Button btnProcessArchive;
    private System.Windows.Forms.Label lblPath;
    private System.Windows.Forms.TextBox txtFileCountLimit;
    private System.Windows.Forms.Label lblFileCountLimit;
    private System.Windows.Forms.Button btnProcessRootFolder;
  }
}


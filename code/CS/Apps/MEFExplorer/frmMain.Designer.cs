namespace Org.MEFExplorer
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
      this.btnExploreMEF = new System.Windows.Forms.Button();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.cboFolders = new System.Windows.Forms.ComboBox();
      this.lblPath = new System.Windows.Forms.Label();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.ckUseModuleSet = new System.Windows.Forms.CheckBox();
      this.ckTwoLevel = new System.Windows.Forms.CheckBox();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1000, 24);
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
      this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 662);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1000, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.ckTwoLevel);
      this.pnlTop.Controls.Add(this.ckUseModuleSet);
      this.pnlTop.Controls.Add(this.lblPath);
      this.pnlTop.Controls.Add(this.cboFolders);
      this.pnlTop.Controls.Add(this.btnBrowse);
      this.pnlTop.Controls.Add(this.btnExploreMEF);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1000, 65);
      this.pnlTop.TabIndex = 2;
      // 
      // btnExploreMEF
      // 
      this.btnExploreMEF.Location = new System.Drawing.Point(657, 13);
      this.btnExploreMEF.Name = "btnExploreMEF";
      this.btnExploreMEF.Size = new System.Drawing.Size(96, 23);
      this.btnExploreMEF.TabIndex = 0;
      this.btnExploreMEF.Tag = "ExploreMEF";
      this.btnExploreMEF.Text = "Explore MEF Components";
      this.btnExploreMEF.UseVisualStyleBackColor = true;
      this.btnExploreMEF.Click += new System.EventHandler(this.Action);
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.txtOut);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 89);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1000, 573);
      this.pnlMain.TabIndex = 3;
      // 
      // txtOut
      // 
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(0, 0);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1000, 573);
      this.txtOut.TabIndex = 0;
      // 
      // cboFolders
      // 
      this.cboFolders.FormattingEnabled = true;
      this.cboFolders.Location = new System.Drawing.Point(48, 14);
      this.cboFolders.Name = "cboFolders";
      this.cboFolders.Size = new System.Drawing.Size(536, 21);
      this.cboFolders.TabIndex = 1;
      this.cboFolders.SelectedIndexChanged += new System.EventHandler(this.cboFolders_SelectedIndexChanged);
      // 
      // lblPath
      // 
      this.lblPath.AutoSize = true;
      this.lblPath.Location = new System.Drawing.Point(13, 16);
      this.lblPath.Name = "lblPath";
      this.lblPath.Size = new System.Drawing.Size(29, 13);
      this.lblPath.TabIndex = 2;
      this.lblPath.Text = "Path";
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(590, 13);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(61, 23);
      this.btnBrowse.TabIndex = 0;
      this.btnBrowse.Tag = "Browse";
      this.btnBrowse.Text = "Browse...";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.Action);
      // 
      // ckUseModuleSet
      // 
      this.ckUseModuleSet.AutoSize = true;
      this.ckUseModuleSet.Location = new System.Drawing.Point(51, 39);
      this.ckUseModuleSet.Name = "ckUseModuleSet";
      this.ckUseModuleSet.Size = new System.Drawing.Size(99, 17);
      this.ckUseModuleSet.TabIndex = 3;
      this.ckUseModuleSet.Text = "Use ModuleSet";
      this.ckUseModuleSet.UseVisualStyleBackColor = true;
      // 
      // ckTwoLevel
      // 
      this.ckTwoLevel.AutoSize = true;
      this.ckTwoLevel.Location = new System.Drawing.Point(166, 39);
      this.ckTwoLevel.Name = "ckTwoLevel";
      this.ckTwoLevel.Size = new System.Drawing.Size(76, 17);
      this.ckTwoLevel.TabIndex = 3;
      this.ckTwoLevel.Text = "Two Level";
      this.ckTwoLevel.UseVisualStyleBackColor = true;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1000, 685);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "MEF Explorer";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      this.pnlMain.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnExploreMEF;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.Label lblPath;
    private System.Windows.Forms.ComboBox cboFolders;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.FolderBrowserDialog folderDialog;
    private System.Windows.Forms.CheckBox ckUseModuleSet;
    private System.Windows.Forms.CheckBox ckTwoLevel;
  }
}


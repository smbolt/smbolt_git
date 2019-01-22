namespace AppDomainWorkbench
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
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblTaskToRun = new System.Windows.Forms.Label();
      this.cboTasks = new System.Windows.Forms.ComboBox();
      this.btnIdentifyPlugIn = new System.Windows.Forms.Button();
      this.btnRunTask = new System.Windows.Forms.Button();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.tvMain = new System.Windows.Forms.TreeView();
      this.imgListTreeView = new System.Windows.Forms.ImageList(this.components);
      this.txtMain = new System.Windows.Forms.TextBox();
      this.btnClearDisplay = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.pnlMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
      this.mnuMain.Size = new System.Drawing.Size(1804, 25);
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
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 1010);
      this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1804, 35);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.lblTaskToRun);
      this.pnlTop.Controls.Add(this.cboTasks);
      this.pnlTop.Controls.Add(this.btnClearDisplay);
      this.pnlTop.Controls.Add(this.btnIdentifyPlugIn);
      this.pnlTop.Controls.Add(this.btnRunTask);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 25);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1804, 107);
      this.pnlTop.TabIndex = 2;
      // 
      // lblTaskToRun
      // 
      this.lblTaskToRun.AutoSize = true;
      this.lblTaskToRun.Location = new System.Drawing.Point(34, 15);
      this.lblTaskToRun.Name = "lblTaskToRun";
      this.lblTaskToRun.Size = new System.Drawing.Size(91, 20);
      this.lblTaskToRun.TabIndex = 2;
      this.lblTaskToRun.Text = "TaskToRun";
      // 
      // cboTasks
      // 
      this.cboTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTasks.FormattingEnabled = true;
      this.cboTasks.Location = new System.Drawing.Point(34, 41);
      this.cboTasks.Name = "cboTasks";
      this.cboTasks.Size = new System.Drawing.Size(266, 28);
      this.cboTasks.TabIndex = 1;
      this.cboTasks.SelectedIndexChanged += new System.EventHandler(this.cboTasks_SelectedIndexChanged);
      // 
      // btnIdentifyPlugIn
      // 
      this.btnIdentifyPlugIn.Location = new System.Drawing.Point(461, 40);
      this.btnIdentifyPlugIn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnIdentifyPlugIn.Name = "btnIdentifyPlugIn";
      this.btnIdentifyPlugIn.Size = new System.Drawing.Size(146, 35);
      this.btnIdentifyPlugIn.TabIndex = 0;
      this.btnIdentifyPlugIn.Tag = "IdentifyPlugIn";
      this.btnIdentifyPlugIn.Text = "Identify Plug-In";
      this.btnIdentifyPlugIn.UseVisualStyleBackColor = true;
      this.btnIdentifyPlugIn.Click += new System.EventHandler(this.Action);
      // 
      // btnRunTask
      // 
      this.btnRunTask.Location = new System.Drawing.Point(307, 40);
      this.btnRunTask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnRunTask.Name = "btnRunTask";
      this.btnRunTask.Size = new System.Drawing.Size(146, 35);
      this.btnRunTask.TabIndex = 0;
      this.btnRunTask.Tag = "RunTask";
      this.btnRunTask.Text = "Run Task";
      this.btnRunTask.UseVisualStyleBackColor = true;
      this.btnRunTask.Click += new System.EventHandler(this.Action);
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.splitterMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 132);
      this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1804, 878);
      this.pnlMain.TabIndex = 3;
      // 
      // splitterMain
      // 
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitterMain.Location = new System.Drawing.Point(0, 0);
      this.splitterMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.splitterMain.Name = "splitterMain";
      // 
      // splitterMain.Panel1
      // 
      this.splitterMain.Panel1.Controls.Add(this.tvMain);
      // 
      // splitterMain.Panel2
      // 
      this.splitterMain.Panel2.Controls.Add(this.txtMain);
      this.splitterMain.Size = new System.Drawing.Size(1804, 878);
      this.splitterMain.SplitterDistance = 300;
      this.splitterMain.TabIndex = 0;
      // 
      // tvMain
      // 
      this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMain.HideSelection = false;
      this.tvMain.ImageIndex = 0;
      this.tvMain.ImageList = this.imgListTreeView;
      this.tvMain.Location = new System.Drawing.Point(0, 0);
      this.tvMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tvMain.Name = "tvMain";
      this.tvMain.SelectedImageIndex = 0;
      this.tvMain.Size = new System.Drawing.Size(300, 878);
      this.tvMain.TabIndex = 0;
      this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
      // 
      // imgListTreeView
      // 
      this.imgListTreeView.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imgListTreeView.ImageSize = new System.Drawing.Size(16, 16);
      this.imgListTreeView.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // txtMain
      // 
      this.txtMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtMain.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtMain.Location = new System.Drawing.Point(0, 0);
      this.txtMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtMain.Multiline = true;
      this.txtMain.Name = "txtMain";
      this.txtMain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtMain.Size = new System.Drawing.Size(1500, 878);
      this.txtMain.TabIndex = 0;
      this.txtMain.WordWrap = false;
      // 
      // btnClearDisplay
      // 
      this.btnClearDisplay.Location = new System.Drawing.Point(687, 40);
      this.btnClearDisplay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnClearDisplay.Name = "btnClearDisplay";
      this.btnClearDisplay.Size = new System.Drawing.Size(146, 35);
      this.btnClearDisplay.TabIndex = 0;
      this.btnClearDisplay.Tag = "ClearDisplay";
      this.btnClearDisplay.Text = "Clear Display";
      this.btnClearDisplay.UseVisualStyleBackColor = true;
      this.btnClearDisplay.Click += new System.EventHandler(this.Action);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1804, 1045);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "AppDomain Manager - v1.0.0.0";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      this.splitterMain.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.TreeView tvMain;
    private System.Windows.Forms.TextBox txtMain;
    private System.Windows.Forms.ImageList imgListTreeView;
    private System.Windows.Forms.Button btnRunTask;
    private System.Windows.Forms.Button btnIdentifyPlugIn;
    private System.Windows.Forms.Label lblTaskToRun;
    private System.Windows.Forms.ComboBox cboTasks;
    private System.Windows.Forms.Button btnClearDisplay;
  }
}


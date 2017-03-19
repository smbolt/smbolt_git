namespace Org.GraphicReportTest
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
      this.pnlTopControl = new System.Windows.Forms.Panel();
      this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
      this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
      this.txtFilePath = new System.Windows.Forms.TextBox();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.btnGetRigSetForSpan = new System.Windows.Forms.Button();
      this.btnRunReport = new System.Windows.Forms.Button();
      this.lblLocateFile = new System.Windows.Forms.Label();
      this.lblSelectReport = new System.Windows.Forms.Label();
      this.cboReports = new System.Windows.Forms.ComboBox();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.pbReport = new System.Windows.Forms.PictureBox();
      this.pnlShadow = new System.Windows.Forms.Panel();
      this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageReport = new System.Windows.Forms.TabPage();
      this.tabPageReportData = new System.Windows.Forms.TabPage();
      this.txtReport = new FastColoredTextBoxNS.FastColoredTextBox();
      this.btnRefreshReport = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTopControl.SuspendLayout();
      this.pnlMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbReport)).BeginInit();
      this.tabMain.SuspendLayout();
      this.tabPageReport.SuspendLayout();
      this.tabPageReportData.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtReport)).BeginInit();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1365, 24);
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
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 735);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1365, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTopControl
      // 
      this.pnlTopControl.Controls.Add(this.dtpEndDate);
      this.pnlTopControl.Controls.Add(this.dtpStartDate);
      this.pnlTopControl.Controls.Add(this.txtFilePath);
      this.pnlTopControl.Controls.Add(this.btnBrowse);
      this.pnlTopControl.Controls.Add(this.btnGetRigSetForSpan);
      this.pnlTopControl.Controls.Add(this.btnRefreshReport);
      this.pnlTopControl.Controls.Add(this.btnRunReport);
      this.pnlTopControl.Controls.Add(this.lblLocateFile);
      this.pnlTopControl.Controls.Add(this.lblSelectReport);
      this.pnlTopControl.Controls.Add(this.cboReports);
      this.pnlTopControl.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTopControl.Location = new System.Drawing.Point(0, 24);
      this.pnlTopControl.Name = "pnlTopControl";
      this.pnlTopControl.Size = new System.Drawing.Size(1365, 98);
      this.pnlTopControl.TabIndex = 2;
      // 
      // dtpEndDate
      // 
      this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dtpEndDate.Location = new System.Drawing.Point(562, 69);
      this.dtpEndDate.Name = "dtpEndDate";
      this.dtpEndDate.Size = new System.Drawing.Size(86, 20);
      this.dtpEndDate.TabIndex = 4;
      // 
      // dtpStartDate
      // 
      this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.dtpStartDate.Location = new System.Drawing.Point(470, 69);
      this.dtpStartDate.Name = "dtpStartDate";
      this.dtpStartDate.Size = new System.Drawing.Size(86, 20);
      this.dtpStartDate.TabIndex = 4;
      // 
      // txtFilePath
      // 
      this.txtFilePath.Location = new System.Drawing.Point(90, 39);
      this.txtFilePath.Name = "txtFilePath";
      this.txtFilePath.Size = new System.Drawing.Size(795, 20);
      this.txtFilePath.TabIndex = 3;
      this.txtFilePath.TextChanged += new System.EventHandler(this.txtFilePath_TextChanged);
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(891, 37);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(65, 22);
      this.btnBrowse.TabIndex = 2;
      this.btnBrowse.Tag = "Browse";
      this.btnBrowse.Text = "Browse...";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.Action);
      // 
      // btnGetRigSetForSpan
      // 
      this.btnGetRigSetForSpan.Location = new System.Drawing.Point(329, 67);
      this.btnGetRigSetForSpan.Name = "btnGetRigSetForSpan";
      this.btnGetRigSetForSpan.Size = new System.Drawing.Size(129, 23);
      this.btnGetRigSetForSpan.TabIndex = 2;
      this.btnGetRigSetForSpan.Tag = "GetRigSetForSpan";
      this.btnGetRigSetForSpan.Text = "Get RigSet for Span";
      this.btnGetRigSetForSpan.UseVisualStyleBackColor = true;
      this.btnGetRigSetForSpan.Click += new System.EventHandler(this.Action);
      // 
      // btnRunReport
      // 
      this.btnRunReport.Location = new System.Drawing.Point(329, 9);
      this.btnRunReport.Name = "btnRunReport";
      this.btnRunReport.Size = new System.Drawing.Size(112, 23);
      this.btnRunReport.TabIndex = 2;
      this.btnRunReport.Tag = "RunReport";
      this.btnRunReport.Text = "Run Report";
      this.btnRunReport.UseVisualStyleBackColor = true;
      this.btnRunReport.Click += new System.EventHandler(this.Action);
      // 
      // lblLocateFile
      // 
      this.lblLocateFile.AutoSize = true;
      this.lblLocateFile.Location = new System.Drawing.Point(12, 42);
      this.lblLocateFile.Name = "lblLocateFile";
      this.lblLocateFile.Size = new System.Drawing.Size(49, 13);
      this.lblLocateFile.TabIndex = 1;
      this.lblLocateFile.Text = "Data File";
      // 
      // lblSelectReport
      // 
      this.lblSelectReport.AutoSize = true;
      this.lblSelectReport.Location = new System.Drawing.Point(12, 13);
      this.lblSelectReport.Name = "lblSelectReport";
      this.lblSelectReport.Size = new System.Drawing.Size(72, 13);
      this.lblSelectReport.TabIndex = 1;
      this.lblSelectReport.Text = "Select Report";
      // 
      // cboReports
      // 
      this.cboReports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboReports.FormattingEnabled = true;
      this.cboReports.Location = new System.Drawing.Point(90, 10);
      this.cboReports.Name = "cboReports";
      this.cboReports.Size = new System.Drawing.Size(233, 21);
      this.cboReports.TabIndex = 0;
      this.cboReports.SelectedIndexChanged += new System.EventHandler(this.cboReports_SelectedIndexChanged);
      // 
      // pnlMain
      // 
      this.pnlMain.AutoScroll = true;
      this.pnlMain.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.pnlMain.Controls.Add(this.pbReport);
      this.pnlMain.Controls.Add(this.pnlShadow);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(3, 3);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1351, 581);
      this.pnlMain.TabIndex = 3;
      // 
      // pbReport
      // 
      this.pbReport.BackColor = System.Drawing.Color.White;
      this.pbReport.Location = new System.Drawing.Point(20, 20);
      this.pbReport.Name = "pbReport";
      this.pbReport.Size = new System.Drawing.Size(1280, 470);
      this.pbReport.TabIndex = 0;
      this.pbReport.TabStop = false;
      this.pbReport.Paint += new System.Windows.Forms.PaintEventHandler(this.pbReport_Paint);
      // 
      // pnlShadow
      // 
      this.pnlShadow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.pnlShadow.ForeColor = System.Drawing.Color.Black;
      this.pnlShadow.Location = new System.Drawing.Point(24, 24);
      this.pnlShadow.Name = "pnlShadow";
      this.pnlShadow.Size = new System.Drawing.Size(1280, 470);
      this.pnlShadow.TabIndex = 1;
      // 
      // dlgFileOpen
      // 
      this.dlgFileOpen.InitialDirectory = "C:\\";
      this.dlgFileOpen.Title = "Locate Report Data File...";
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageReport);
      this.tabMain.Controls.Add(this.tabPageReportData);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(125, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 122);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1365, 613);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 4;
      // 
      // tabPageReport
      // 
      this.tabPageReport.Controls.Add(this.pnlMain);
      this.tabPageReport.Location = new System.Drawing.Point(4, 22);
      this.tabPageReport.Name = "tabPageReport";
      this.tabPageReport.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageReport.Size = new System.Drawing.Size(1357, 587);
      this.tabPageReport.TabIndex = 0;
      this.tabPageReport.Text = "Report";
      this.tabPageReport.UseVisualStyleBackColor = true;
      // 
      // tabPageReportData
      // 
      this.tabPageReportData.Controls.Add(this.txtReport);
      this.tabPageReportData.Location = new System.Drawing.Point(4, 22);
      this.tabPageReportData.Name = "tabPageReportData";
      this.tabPageReportData.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageReportData.Size = new System.Drawing.Size(1357, 587);
      this.tabPageReportData.TabIndex = 1;
      this.tabPageReportData.Text = "Data";
      this.tabPageReportData.UseVisualStyleBackColor = true;
      // 
      // txtReport
      // 
      this.txtReport.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
      this.txtReport.AutoScrollMinSize = new System.Drawing.Size(2, 13);
      this.txtReport.BackBrush = null;
      this.txtReport.CharHeight = 13;
      this.txtReport.CharWidth = 7;
      this.txtReport.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtReport.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtReport.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtReport.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtReport.IsReplaceMode = false;
      this.txtReport.Location = new System.Drawing.Point(3, 3);
      this.txtReport.Name = "txtReport";
      this.txtReport.Paddings = new System.Windows.Forms.Padding(0);
      this.txtReport.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtReport.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtReport.ServiceColors")));
      this.txtReport.Size = new System.Drawing.Size(1351, 581);
      this.txtReport.TabIndex = 0;
      this.txtReport.Zoom = 100;
      // 
      // btnRefreshReport
      // 
      this.btnRefreshReport.Location = new System.Drawing.Point(447, 9);
      this.btnRefreshReport.Name = "btnRefreshReport";
      this.btnRefreshReport.Size = new System.Drawing.Size(112, 23);
      this.btnRefreshReport.TabIndex = 2;
      this.btnRefreshReport.Tag = "RefreshReport";
      this.btnRefreshReport.Text = "Refresh Report";
      this.btnRefreshReport.UseVisualStyleBackColor = true;
      this.btnRefreshReport.Click += new System.EventHandler(this.Action);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1365, 758);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.pnlTopControl);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Graphic Report Test";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTopControl.ResumeLayout(false);
      this.pnlTopControl.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbReport)).EndInit();
      this.tabMain.ResumeLayout(false);
      this.tabPageReport.ResumeLayout(false);
      this.tabPageReportData.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtReport)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTopControl;
    private System.Windows.Forms.Button btnRunReport;
    private System.Windows.Forms.Label lblSelectReport;
    private System.Windows.Forms.ComboBox cboReports;
    private System.Windows.Forms.TextBox txtFilePath;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.Label lblLocateFile;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.PictureBox pbReport;
    private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabPageReport;
		private System.Windows.Forms.TabPage tabPageReportData;
		private FastColoredTextBoxNS.FastColoredTextBox txtReport;
		private System.Windows.Forms.Panel pnlShadow;
    private System.Windows.Forms.DateTimePicker dtpEndDate;
    private System.Windows.Forms.DateTimePicker dtpStartDate;
    private System.Windows.Forms.Button btnGetRigSetForSpan;
    private System.Windows.Forms.Button btnRefreshReport;
  }
}


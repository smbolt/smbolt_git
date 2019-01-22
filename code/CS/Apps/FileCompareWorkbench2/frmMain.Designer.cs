namespace FileCompareWorkbench2
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
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblFile2 = new System.Windows.Forms.Label();
      this.txtFile2 = new System.Windows.Forms.TextBox();
      this.lblFile1 = new System.Windows.Forms.Label();
      this.txtFile1 = new System.Windows.Forms.TextBox();
      this.btnBrowseFile2 = new System.Windows.Forms.Button();
      this.btnBrowseFile1 = new System.Windows.Forms.Button();
      this.btnCompareFiles = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
      this.cboReportFormat = new System.Windows.Forms.ComboBox();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(4, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1258, 24);
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
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.cboReportFormat);
      this.pnlTop.Controls.Add(this.lblFile2);
      this.pnlTop.Controls.Add(this.txtFile2);
      this.pnlTop.Controls.Add(this.lblFile1);
      this.pnlTop.Controls.Add(this.txtFile1);
      this.pnlTop.Controls.Add(this.btnBrowseFile2);
      this.pnlTop.Controls.Add(this.btnBrowseFile1);
      this.pnlTop.Controls.Add(this.btnCompareFiles);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1258, 109);
      this.pnlTop.TabIndex = 1;
      //
      // lblFile2
      //
      this.lblFile2.AutoSize = true;
      this.lblFile2.Location = new System.Drawing.Point(11, 51);
      this.lblFile2.Name = "lblFile2";
      this.lblFile2.Size = new System.Drawing.Size(32, 13);
      this.lblFile2.TabIndex = 4;
      this.lblFile2.Text = "File 2";
      //
      // txtFile2
      //
      this.txtFile2.Location = new System.Drawing.Point(11, 66);
      this.txtFile2.Name = "txtFile2";
      this.txtFile2.Size = new System.Drawing.Size(881, 20);
      this.txtFile2.TabIndex = 3;
      this.txtFile2.TextChanged += new System.EventHandler(this.TextValueChanged);
      //
      // lblFile1
      //
      this.lblFile1.AutoSize = true;
      this.lblFile1.Location = new System.Drawing.Point(11, 8);
      this.lblFile1.Name = "lblFile1";
      this.lblFile1.Size = new System.Drawing.Size(32, 13);
      this.lblFile1.TabIndex = 2;
      this.lblFile1.Text = "File 1";
      //
      // txtFile1
      //
      this.txtFile1.Location = new System.Drawing.Point(11, 23);
      this.txtFile1.Name = "txtFile1";
      this.txtFile1.Size = new System.Drawing.Size(881, 20);
      this.txtFile1.TabIndex = 1;
      this.txtFile1.TextChanged += new System.EventHandler(this.TextValueChanged);
      //
      // btnBrowseFile2
      //
      this.btnBrowseFile2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnBrowseFile2.Location = new System.Drawing.Point(898, 65);
      this.btnBrowseFile2.Name = "btnBrowseFile2";
      this.btnBrowseFile2.Size = new System.Drawing.Size(34, 22);
      this.btnBrowseFile2.TabIndex = 4;
      this.btnBrowseFile2.Tag = "BrowseFile2";
      this.btnBrowseFile2.Text = "...";
      this.btnBrowseFile2.UseVisualStyleBackColor = true;
      this.btnBrowseFile2.Click += new System.EventHandler(this.Action);
      //
      // btnBrowseFile1
      //
      this.btnBrowseFile1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnBrowseFile1.Location = new System.Drawing.Point(898, 22);
      this.btnBrowseFile1.Name = "btnBrowseFile1";
      this.btnBrowseFile1.Size = new System.Drawing.Size(34, 22);
      this.btnBrowseFile1.TabIndex = 2;
      this.btnBrowseFile1.Tag = "BrowseFile1";
      this.btnBrowseFile1.Text = "...";
      this.btnBrowseFile1.UseVisualStyleBackColor = true;
      this.btnBrowseFile1.Click += new System.EventHandler(this.Action);
      //
      // btnCompareFiles
      //
      this.btnCompareFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCompareFiles.Location = new System.Drawing.Point(1090, 13);
      this.btnCompareFiles.Name = "btnCompareFiles";
      this.btnCompareFiles.Size = new System.Drawing.Size(152, 23);
      this.btnCompareFiles.TabIndex = 5;
      this.btnCompareFiles.Tag = "CompareFiles";
      this.btnCompareFiles.Text = "Compare Files";
      this.btnCompareFiles.UseVisualStyleBackColor = true;
      this.btnCompareFiles.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(4, 682);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(1258, 23);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtOut
      //
      this.txtOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(4, 133);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1258, 549);
      this.txtOut.TabIndex = 10;
      this.txtOut.WordWrap = false;
      //
      // dlgOpenFile
      //
      this.dlgOpenFile.InitialDirectory = "C:\\";
      this.dlgOpenFile.Title = "Locate file for comparision";
      //
      // cboReportFormat
      //
      this.cboReportFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cboReportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboReportFormat.FormattingEnabled = true;
      this.cboReportFormat.Items.AddRange(new object[] {
        "Inline report",
        "Side by side report",
        "No report"
      });
      this.cboReportFormat.Location = new System.Drawing.Point(1090, 44);
      this.cboReportFormat.Name = "cboReportFormat";
      this.cboReportFormat.Size = new System.Drawing.Size(152, 21);
      this.cboReportFormat.TabIndex = 6;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1266, 705);
      this.Controls.Add(this.txtOut);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "File Compare Workbench 2";
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
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
    private System.Windows.Forms.Button btnCompareFiles;
    private System.Windows.Forms.Label lblFile2;
    private System.Windows.Forms.TextBox txtFile2;
    private System.Windows.Forms.Label lblFile1;
    private System.Windows.Forms.TextBox txtFile1;
    private System.Windows.Forms.Button btnBrowseFile2;
    private System.Windows.Forms.Button btnBrowseFile1;
    private System.Windows.Forms.OpenFileDialog dlgOpenFile;
    private System.Windows.Forms.ComboBox cboReportFormat;
  }
}


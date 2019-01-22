namespace NodeQueryWorkbench
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
      this.lblSelectQuery = new System.Windows.Forms.Label();
      this.cboQueryFiles = new System.Windows.Forms.ComboBox();
      this.lblSelectData = new System.Windows.Forms.Label();
      this.cboDataFiles = new System.Windows.Forms.ComboBox();
      this.btnStep = new System.Windows.Forms.Button();
      this.btnRun = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.txtRawData = new System.Windows.Forms.TextBox();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.splitterTop = new System.Windows.Forms.SplitContainer();
      this.lblRawData = new System.Windows.Forms.Label();
      this.txtDataStructure = new System.Windows.Forms.TextBox();
      this.lblDataStructure = new System.Windows.Forms.Label();
      this.splitterBottom = new System.Windows.Forms.SplitContainer();
      this.txtQuery = new System.Windows.Forms.TextBox();
      this.lblQuery = new System.Windows.Forms.Label();
      this.txtQueryResults = new System.Windows.Forms.TextBox();
      this.labelQueryResults = new System.Windows.Forms.Label();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterTop)).BeginInit();
      this.splitterTop.Panel1.SuspendLayout();
      this.splitterTop.Panel2.SuspendLayout();
      this.splitterTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterBottom)).BeginInit();
      this.splitterBottom.Panel1.SuspendLayout();
      this.splitterBottom.Panel2.SuspendLayout();
      this.splitterBottom.SuspendLayout();
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
      this.mnuMain.Size = new System.Drawing.Size(1251, 24);
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
      this.pnlTop.Controls.Add(this.lblSelectQuery);
      this.pnlTop.Controls.Add(this.cboQueryFiles);
      this.pnlTop.Controls.Add(this.lblSelectData);
      this.pnlTop.Controls.Add(this.cboDataFiles);
      this.pnlTop.Controls.Add(this.btnStep);
      this.pnlTop.Controls.Add(this.btnRun);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1251, 99);
      this.pnlTop.TabIndex = 10;
      //
      // lblSelectQuery
      //
      this.lblSelectQuery.AutoSize = true;
      this.lblSelectQuery.Location = new System.Drawing.Point(15, 51);
      this.lblSelectQuery.Name = "lblSelectQuery";
      this.lblSelectQuery.Size = new System.Drawing.Size(68, 13);
      this.lblSelectQuery.TabIndex = 2;
      this.lblSelectQuery.Text = "Select Query";
      //
      // cboQueryFiles
      //
      this.cboQueryFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboQueryFiles.FormattingEnabled = true;
      this.cboQueryFiles.Location = new System.Drawing.Point(84, 48);
      this.cboQueryFiles.Name = "cboQueryFiles";
      this.cboQueryFiles.Size = new System.Drawing.Size(262, 21);
      this.cboQueryFiles.TabIndex = 13;
      this.cboQueryFiles.SelectedIndexChanged += new System.EventHandler(this.cboQueryFiles_SelectedIndexChanged);
      //
      // lblSelectData
      //
      this.lblSelectData.AutoSize = true;
      this.lblSelectData.Location = new System.Drawing.Point(15, 19);
      this.lblSelectData.Name = "lblSelectData";
      this.lblSelectData.Size = new System.Drawing.Size(63, 13);
      this.lblSelectData.TabIndex = 2;
      this.lblSelectData.Text = "Select Data";
      //
      // cboDataFiles
      //
      this.cboDataFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboDataFiles.FormattingEnabled = true;
      this.cboDataFiles.Location = new System.Drawing.Point(84, 16);
      this.cboDataFiles.Name = "cboDataFiles";
      this.cboDataFiles.Size = new System.Drawing.Size(262, 21);
      this.cboDataFiles.TabIndex = 11;
      this.cboDataFiles.SelectedIndexChanged += new System.EventHandler(this.cboDataFiles_SelectedIndexChanged);
      //
      // btnStep
      //
      this.btnStep.Location = new System.Drawing.Point(356, 47);
      this.btnStep.Name = "btnStep";
      this.btnStep.Size = new System.Drawing.Size(96, 23);
      this.btnStep.TabIndex = 15;
      this.btnStep.Tag = "Step";
      this.btnStep.Text = "Step";
      this.btnStep.UseVisualStyleBackColor = true;
      this.btnStep.Click += new System.EventHandler(this.Action);
      //
      // btnRun
      //
      this.btnRun.Location = new System.Drawing.Point(356, 15);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(96, 23);
      this.btnRun.TabIndex = 16;
      this.btnRun.Tag = "Run";
      this.btnRun.Text = "Run";
      this.btnRun.UseVisualStyleBackColor = true;
      this.btnRun.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(4, 691);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(1251, 23);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtRawData
      //
      this.txtRawData.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtRawData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtRawData.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtRawData.Location = new System.Drawing.Point(0, 23);
      this.txtRawData.Multiline = true;
      this.txtRawData.Name = "txtRawData";
      this.txtRawData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtRawData.Size = new System.Drawing.Size(624, 258);
      this.txtRawData.TabIndex = 20;
      //
      // splitterMain
      //
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.Location = new System.Drawing.Point(4, 123);
      this.splitterMain.Name = "splitterMain";
      this.splitterMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
      //
      // splitterMain.Panel1
      //
      this.splitterMain.Panel1.Controls.Add(this.splitterTop);
      //
      // splitterMain.Panel2
      //
      this.splitterMain.Panel2.Controls.Add(this.splitterBottom);
      this.splitterMain.Size = new System.Drawing.Size(1251, 568);
      this.splitterMain.SplitterDistance = 283;
      this.splitterMain.SplitterWidth = 3;
      this.splitterMain.TabIndex = 4;
      //
      // splitterTop
      //
      this.splitterTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitterTop.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterTop.Location = new System.Drawing.Point(0, 0);
      this.splitterTop.Name = "splitterTop";
      //
      // splitterTop.Panel1
      //
      this.splitterTop.Panel1.Controls.Add(this.txtRawData);
      this.splitterTop.Panel1.Controls.Add(this.lblRawData);
      //
      // splitterTop.Panel2
      //
      this.splitterTop.Panel2.Controls.Add(this.txtDataStructure);
      this.splitterTop.Panel2.Controls.Add(this.lblDataStructure);
      this.splitterTop.Size = new System.Drawing.Size(1251, 283);
      this.splitterTop.SplitterDistance = 626;
      this.splitterTop.TabIndex = 5;
      //
      // lblRawData
      //
      this.lblRawData.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblRawData.Location = new System.Drawing.Point(0, 0);
      this.lblRawData.Name = "lblRawData";
      this.lblRawData.Size = new System.Drawing.Size(624, 23);
      this.lblRawData.TabIndex = 4;
      this.lblRawData.Text = "Raw Data";
      this.lblRawData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtDataStructure
      //
      this.txtDataStructure.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtDataStructure.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtDataStructure.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtDataStructure.Location = new System.Drawing.Point(0, 23);
      this.txtDataStructure.Multiline = true;
      this.txtDataStructure.Name = "txtDataStructure";
      this.txtDataStructure.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtDataStructure.Size = new System.Drawing.Size(619, 258);
      this.txtDataStructure.TabIndex = 22;
      //
      // lblDataStructure
      //
      this.lblDataStructure.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblDataStructure.Location = new System.Drawing.Point(0, 0);
      this.lblDataStructure.Name = "lblDataStructure";
      this.lblDataStructure.Size = new System.Drawing.Size(619, 23);
      this.lblDataStructure.TabIndex = 5;
      this.lblDataStructure.Text = "Data Structure";
      this.lblDataStructure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // splitterBottom
      //
      this.splitterBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitterBottom.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterBottom.Location = new System.Drawing.Point(0, 0);
      this.splitterBottom.Name = "splitterBottom";
      //
      // splitterBottom.Panel1
      //
      this.splitterBottom.Panel1.Controls.Add(this.txtQuery);
      this.splitterBottom.Panel1.Controls.Add(this.lblQuery);
      //
      // splitterBottom.Panel2
      //
      this.splitterBottom.Panel2.Controls.Add(this.txtQueryResults);
      this.splitterBottom.Panel2.Controls.Add(this.labelQueryResults);
      this.splitterBottom.Size = new System.Drawing.Size(1251, 282);
      this.splitterBottom.SplitterDistance = 626;
      this.splitterBottom.TabIndex = 7;
      //
      // txtQuery
      //
      this.txtQuery.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtQuery.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtQuery.Location = new System.Drawing.Point(0, 23);
      this.txtQuery.Multiline = true;
      this.txtQuery.Name = "txtQuery";
      this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtQuery.Size = new System.Drawing.Size(624, 257);
      this.txtQuery.TabIndex = 21;
      //
      // lblQuery
      //
      this.lblQuery.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblQuery.Location = new System.Drawing.Point(0, 0);
      this.lblQuery.Name = "lblQuery";
      this.lblQuery.Size = new System.Drawing.Size(624, 23);
      this.lblQuery.TabIndex = 5;
      this.lblQuery.Text = "Query";
      this.lblQuery.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtQueryResults
      //
      this.txtQueryResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtQueryResults.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtQueryResults.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtQueryResults.Location = new System.Drawing.Point(0, 23);
      this.txtQueryResults.Multiline = true;
      this.txtQueryResults.Name = "txtQueryResults";
      this.txtQueryResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtQueryResults.Size = new System.Drawing.Size(619, 257);
      this.txtQueryResults.TabIndex = 23;
      //
      // labelQueryResults
      //
      this.labelQueryResults.Dock = System.Windows.Forms.DockStyle.Top;
      this.labelQueryResults.Location = new System.Drawing.Point(0, 0);
      this.labelQueryResults.Name = "labelQueryResults";
      this.labelQueryResults.Size = new System.Drawing.Size(619, 23);
      this.labelQueryResults.TabIndex = 6;
      this.labelQueryResults.Text = "Query Results";
      this.labelQueryResults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1259, 714);
      this.Controls.Add(this.splitterMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "NodeQueryWorkbench";
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      this.splitterTop.Panel1.ResumeLayout(false);
      this.splitterTop.Panel1.PerformLayout();
      this.splitterTop.Panel2.ResumeLayout(false);
      this.splitterTop.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterTop)).EndInit();
      this.splitterTop.ResumeLayout(false);
      this.splitterBottom.Panel1.ResumeLayout(false);
      this.splitterBottom.Panel1.PerformLayout();
      this.splitterBottom.Panel2.ResumeLayout(false);
      this.splitterBottom.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterBottom)).EndInit();
      this.splitterBottom.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TextBox txtRawData;
    private System.Windows.Forms.Button btnRun;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.Label lblRawData;
    private System.Windows.Forms.TextBox txtQuery;
    private System.Windows.Forms.Label lblQuery;
    private System.Windows.Forms.Label lblSelectQuery;
    private System.Windows.Forms.ComboBox cboQueryFiles;
    private System.Windows.Forms.Label lblSelectData;
    private System.Windows.Forms.ComboBox cboDataFiles;
    private System.Windows.Forms.SplitContainer splitterTop;
    private System.Windows.Forms.Label lblDataStructure;
    private System.Windows.Forms.TextBox txtDataStructure;
    private System.Windows.Forms.SplitContainer splitterBottom;
    private System.Windows.Forms.TextBox txtQueryResults;
    private System.Windows.Forms.Label labelQueryResults;
    private System.Windows.Forms.Button btnStep;
  }
}


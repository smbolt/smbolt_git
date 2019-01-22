namespace Org.OpsManager
{
  partial class frmRunHistory
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRunHistory));
      this.lblStatus = new System.Windows.Forms.Label();
      this.btnBack = new System.Windows.Forms.Button();
      this.pnlBottom = new System.Windows.Forms.Panel();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.runHistorySplitter = new System.Windows.Forms.SplitContainer();
      this.gvRunHistory = new System.Windows.Forms.DataGridView();
      this.txtLogDetails = new System.Windows.Forms.TextBox();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnRefreshRunHistory = new System.Windows.Forms.Button();
      this.pnlBottom.SuspendLayout();
      this.mnuMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.runHistorySplitter)).BeginInit();
      this.runHistorySplitter.Panel1.SuspendLayout();
      this.runHistorySplitter.Panel2.SuspendLayout();
      this.runHistorySplitter.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvRunHistory)).BeginInit();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 776);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(1525, 20);
      this.lblStatus.TabIndex = 17;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // btnBack
      //
      this.btnBack.Location = new System.Drawing.Point(1171, 6);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(100, 23);
      this.btnBack.TabIndex = 5;
      this.btnBack.Tag = "Back";
      this.btnBack.Text = "Back";
      this.btnBack.UseVisualStyleBackColor = true;
      this.btnBack.Click += new System.EventHandler(this.Action);
      //
      // pnlBottom
      //
      this.pnlBottom.Controls.Add(this.btnBack);
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 744);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(1525, 32);
      this.pnlBottom.TabIndex = 26;
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1525, 24);
      this.mnuMain.TabIndex = 28;
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
      // runHistorySplitter
      //
      this.runHistorySplitter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.runHistorySplitter.Location = new System.Drawing.Point(0, 124);
      this.runHistorySplitter.Name = "runHistorySplitter";
      this.runHistorySplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
      //
      // runHistorySplitter.Panel1
      //
      this.runHistorySplitter.Panel1.Controls.Add(this.gvRunHistory);
      //
      // runHistorySplitter.Panel2
      //
      this.runHistorySplitter.Panel2.Controls.Add(this.txtLogDetails);
      this.runHistorySplitter.Size = new System.Drawing.Size(1525, 620);
      this.runHistorySplitter.SplitterDistance = 476;
      this.runHistorySplitter.TabIndex = 29;
      //
      // gvRunHistory
      //
      this.gvRunHistory.AllowUserToAddRows = false;
      this.gvRunHistory.AllowUserToDeleteRows = false;
      this.gvRunHistory.AllowUserToResizeRows = false;
      this.gvRunHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvRunHistory.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvRunHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvRunHistory.Location = new System.Drawing.Point(0, 0);
      this.gvRunHistory.MultiSelect = false;
      this.gvRunHistory.Name = "gvRunHistory";
      this.gvRunHistory.RowHeadersVisible = false;
      this.gvRunHistory.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvRunHistory.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvRunHistory.RowTemplate.Height = 19;
      this.gvRunHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvRunHistory.Size = new System.Drawing.Size(1525, 476);
      this.gvRunHistory.TabIndex = 8;
      this.gvRunHistory.Tag = "RunHistoryChange";
      this.gvRunHistory.SelectionChanged += new System.EventHandler(this.Action);
      //
      // txtLogDetails
      //
      this.txtLogDetails.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtLogDetails.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtLogDetails.Location = new System.Drawing.Point(0, 0);
      this.txtLogDetails.Multiline = true;
      this.txtLogDetails.Name = "txtLogDetails";
      this.txtLogDetails.ReadOnly = true;
      this.txtLogDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtLogDetails.Size = new System.Drawing.Size(1525, 140);
      this.txtLogDetails.TabIndex = 9;
      this.txtLogDetails.WordWrap = false;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.btnRefreshRunHistory);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1525, 100);
      this.pnlTop.TabIndex = 30;
      //
      // btnRefreshRunHistory
      //
      this.btnRefreshRunHistory.Location = new System.Drawing.Point(1154, 27);
      this.btnRefreshRunHistory.Name = "btnRefreshRunHistory";
      this.btnRefreshRunHistory.Size = new System.Drawing.Size(117, 37);
      this.btnRefreshRunHistory.TabIndex = 0;
      this.btnRefreshRunHistory.Tag = "RefreshRunHistory";
      this.btnRefreshRunHistory.Text = "Refresh Run History";
      this.btnRefreshRunHistory.UseVisualStyleBackColor = true;
      this.btnRefreshRunHistory.Click += new System.EventHandler(this.Action);
      //
      // frmRunHistory
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1525, 796);
      this.Controls.Add(this.runHistorySplitter);
      this.Controls.Add(this.pnlBottom);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmRunHistory";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Run History";
      this.pnlBottom.ResumeLayout(false);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.runHistorySplitter.Panel1.ResumeLayout(false);
      this.runHistorySplitter.Panel2.ResumeLayout(false);
      this.runHistorySplitter.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.runHistorySplitter)).EndInit();
      this.runHistorySplitter.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvRunHistory)).EndInit();
      this.pnlTop.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Panel pnlBottom;
    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.SplitContainer runHistorySplitter;
    private System.Windows.Forms.DataGridView gvRunHistory;
    private System.Windows.Forms.TextBox txtLogDetails;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnRefreshRunHistory;
  }
}
namespace Org.Metrics
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
      this.txtMain = new System.Windows.Forms.TextBox();
      this.btnReloadMetricData = new System.Windows.Forms.Button();
      this.btnLoadObservations = new System.Windows.Forms.Button();
      this.btnClearObservations = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1169, 24);
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
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 678);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1169, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.btnClearObservations);
      this.pnlTop.Controls.Add(this.btnLoadObservations);
      this.pnlTop.Controls.Add(this.btnReloadMetricData);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1169, 49);
      this.pnlTop.TabIndex = 2;
      //
      // txtMain
      //
      this.txtMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtMain.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtMain.Location = new System.Drawing.Point(0, 73);
      this.txtMain.Multiline = true;
      this.txtMain.Name = "txtMain";
      this.txtMain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtMain.Size = new System.Drawing.Size(1169, 605);
      this.txtMain.TabIndex = 3;
      this.txtMain.WordWrap = false;
      //
      // btnReloadMetricData
      //
      this.btnReloadMetricData.Location = new System.Drawing.Point(12, 14);
      this.btnReloadMetricData.Name = "btnReloadMetricData";
      this.btnReloadMetricData.Size = new System.Drawing.Size(135, 23);
      this.btnReloadMetricData.TabIndex = 0;
      this.btnReloadMetricData.Tag = "ReloadMetricData";
      this.btnReloadMetricData.Text = "Reload Metric Data";
      this.btnReloadMetricData.UseVisualStyleBackColor = true;
      this.btnReloadMetricData.Click += new System.EventHandler(this.Action);
      //
      // btnLoadObservations
      //
      this.btnLoadObservations.Location = new System.Drawing.Point(153, 14);
      this.btnLoadObservations.Name = "btnLoadObservations";
      this.btnLoadObservations.Size = new System.Drawing.Size(135, 23);
      this.btnLoadObservations.TabIndex = 0;
      this.btnLoadObservations.Tag = "LoadObservations";
      this.btnLoadObservations.Text = "Load Observations";
      this.btnLoadObservations.UseVisualStyleBackColor = true;
      this.btnLoadObservations.Click += new System.EventHandler(this.Action);
      //
      // btnClearObservations
      //
      this.btnClearObservations.Location = new System.Drawing.Point(294, 14);
      this.btnClearObservations.Name = "btnClearObservations";
      this.btnClearObservations.Size = new System.Drawing.Size(135, 23);
      this.btnClearObservations.TabIndex = 0;
      this.btnClearObservations.Tag = "ClearObservations";
      this.btnClearObservations.Text = "Clear Observations";
      this.btnClearObservations.UseVisualStyleBackColor = true;
      this.btnClearObservations.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1169, 701);
      this.Controls.Add(this.txtMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Metrix - v1.0.0.0";
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.TextBox txtMain;
    private System.Windows.Forms.Button btnReloadMetricData;
    private System.Windows.Forms.Button btnClearObservations;
    private System.Windows.Forms.Button btnLoadObservations;
  }
}


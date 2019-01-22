namespace DynamoDbServiceHost
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
      this.btnStop = new System.Windows.Forms.Button();
      this.btnResume = new System.Windows.Forms.Button();
      this.btnPause = new System.Windows.Forms.Button();
      this.btnStart = new System.Windows.Forms.Button();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.btnClearDisplay = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(4, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(956, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(4, 612);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(956, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.btnClearDisplay);
      this.pnlTop.Controls.Add(this.btnStop);
      this.pnlTop.Controls.Add(this.btnResume);
      this.pnlTop.Controls.Add(this.btnPause);
      this.pnlTop.Controls.Add(this.btnStart);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(956, 52);
      this.pnlTop.TabIndex = 2;
      // 
      // btnStop
      // 
      this.btnStop.Location = new System.Drawing.Point(236, 8);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(75, 37);
      this.btnStop.TabIndex = 3;
      this.btnStop.Tag = "Stop";
      this.btnStop.Text = "Stop";
      this.btnStop.UseVisualStyleBackColor = true;
      this.btnStop.Click += new System.EventHandler(this.Action);
      // 
      // btnResume
      // 
      this.btnResume.Location = new System.Drawing.Point(159, 8);
      this.btnResume.Name = "btnResume";
      this.btnResume.Size = new System.Drawing.Size(75, 37);
      this.btnResume.TabIndex = 2;
      this.btnResume.Tag = "Resume";
      this.btnResume.Text = "Resume";
      this.btnResume.UseVisualStyleBackColor = true;
      this.btnResume.Click += new System.EventHandler(this.Action);
      // 
      // btnPause
      // 
      this.btnPause.Location = new System.Drawing.Point(82, 8);
      this.btnPause.Name = "btnPause";
      this.btnPause.Size = new System.Drawing.Size(75, 37);
      this.btnPause.TabIndex = 1;
      this.btnPause.Tag = "Pause";
      this.btnPause.Text = "Pause";
      this.btnPause.UseVisualStyleBackColor = true;
      this.btnPause.Click += new System.EventHandler(this.Action);
      // 
      // btnStart
      // 
      this.btnStart.Location = new System.Drawing.Point(5, 8);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(75, 37);
      this.btnStart.TabIndex = 0;
      this.btnStart.Tag = "Start";
      this.btnStart.Text = "Start";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new System.EventHandler(this.Action);
      // 
      // txtOut
      // 
      this.txtOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(4, 76);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(956, 536);
      this.txtOut.TabIndex = 10;
      // 
      // btnClearDisplay
      // 
      this.btnClearDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClearDisplay.Location = new System.Drawing.Point(841, 8);
      this.btnClearDisplay.Name = "btnClearDisplay";
      this.btnClearDisplay.Size = new System.Drawing.Size(98, 37);
      this.btnClearDisplay.TabIndex = 4;
      this.btnClearDisplay.Tag = "ClearDisplay";
      this.btnClearDisplay.Text = "Clear Display";
      this.btnClearDisplay.UseVisualStyleBackColor = true;
      this.btnClearDisplay.Click += new System.EventHandler(this.Action);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(964, 635);
      this.Controls.Add(this.txtOut);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "DynamoDbService Host";
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
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Button btnResume;
    private System.Windows.Forms.Button btnPause;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.Button btnClearDisplay;
  }
}


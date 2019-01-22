namespace Org.RPTest
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
      this.lblRequestProcessors = new System.Windows.Forms.Label();
      this.cboRequestProcessors = new System.Windows.Forms.ComboBox();
      this.btnRunRequestProcessor = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.txtOut = new System.Windows.Forms.TextBox();
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
      this.mnuMain.Size = new System.Drawing.Size(1011, 24);
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
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.lblRequestProcessors);
      this.pnlTop.Controls.Add(this.cboRequestProcessors);
      this.pnlTop.Controls.Add(this.btnRunRequestProcessor);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1011, 99);
      this.pnlTop.TabIndex = 1;
      // 
      // lblRequestProcessors
      // 
      this.lblRequestProcessors.AutoSize = true;
      this.lblRequestProcessors.Location = new System.Drawing.Point(11, 13);
      this.lblRequestProcessors.Name = "lblRequestProcessors";
      this.lblRequestProcessors.Size = new System.Drawing.Size(102, 13);
      this.lblRequestProcessors.TabIndex = 2;
      this.lblRequestProcessors.Text = "Request Processors";
      // 
      // cboRequestProcessors
      // 
      this.cboRequestProcessors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboRequestProcessors.FormattingEnabled = true;
      this.cboRequestProcessors.Location = new System.Drawing.Point(119, 9);
      this.cboRequestProcessors.Name = "cboRequestProcessors";
      this.cboRequestProcessors.Size = new System.Drawing.Size(239, 21);
      this.cboRequestProcessors.TabIndex = 1;
      this.cboRequestProcessors.SelectedIndexChanged += new System.EventHandler(this.cboRequestProcessors_SelectedIndexChanged);
      // 
      // btnRunRequestProcessor
      // 
      this.btnRunRequestProcessor.Location = new System.Drawing.Point(364, 8);
      this.btnRunRequestProcessor.Name = "btnRunRequestProcessor";
      this.btnRunRequestProcessor.Size = new System.Drawing.Size(158, 23);
      this.btnRunRequestProcessor.TabIndex = 0;
      this.btnRunRequestProcessor.Tag = "RunRequestProcessor";
      this.btnRunRequestProcessor.Text = "Run Request Processor";
      this.btnRunRequestProcessor.UseVisualStyleBackColor = true;
      this.btnRunRequestProcessor.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 593);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1011, 20);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.txtOut);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 123);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1011, 470);
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
      this.txtOut.Size = new System.Drawing.Size(1011, 470);
      this.txtOut.TabIndex = 0;
      this.txtOut.WordWrap = false;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1011, 613);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Request Processor Test";
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
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnRunRequestProcessor;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.Label lblRequestProcessors;
    private System.Windows.Forms.ComboBox cboRequestProcessors;
  }
}


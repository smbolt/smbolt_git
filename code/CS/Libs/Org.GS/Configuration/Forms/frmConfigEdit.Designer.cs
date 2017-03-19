namespace Org.GS.Configuration
{
  partial class frmConfigEdit
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigEdit));
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileClose = new System.Windows.Forms.ToolStripMenuItem();
      this.lblStatus = new System.Windows.Forms.Label();
      this.txtMain = new System.Windows.Forms.TextBox();
      this.saveDialog = new System.Windows.Forms.SaveFileDialog();
      this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
      this.openDialog = new System.Windows.Forms.OpenFileDialog();
      this.mnuMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(784, 24);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileOpen,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileClose});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "&File";
      // 
      // mnuFileSave
      // 
      this.mnuFileSave.Name = "mnuFileSave";
      this.mnuFileSave.Size = new System.Drawing.Size(152, 22);
      this.mnuFileSave.Tag = "Save";
      this.mnuFileSave.Text = "&Save";
      this.mnuFileSave.Click += new System.EventHandler(this.Action);
      // 
      // mnuFileSaveAs
      // 
      this.mnuFileSaveAs.Name = "mnuFileSaveAs";
      this.mnuFileSaveAs.Size = new System.Drawing.Size(152, 22);
      this.mnuFileSaveAs.Tag = "SaveAs";
      this.mnuFileSaveAs.Text = "Save &As";
      this.mnuFileSaveAs.Click += new System.EventHandler(this.Action);
      // 
      // mnuFileClose
      // 
      this.mnuFileClose.Name = "mnuFileClose";
      this.mnuFileClose.Size = new System.Drawing.Size(152, 22);
      this.mnuFileClose.Tag = "Close";
      this.mnuFileClose.Text = "&Close";
      this.mnuFileClose.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 543);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(784, 19);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtMain
      // 
      this.txtMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtMain.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtMain.Location = new System.Drawing.Point(0, 24);
      this.txtMain.Multiline = true;
      this.txtMain.Name = "txtMain";
      this.txtMain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtMain.Size = new System.Drawing.Size(784, 519);
      this.txtMain.TabIndex = 2;
      this.txtMain.WordWrap = false;
      this.txtMain.TextChanged += new System.EventHandler(this.txtMain_TextChanged);
      // 
      // mnuFileOpen
      // 
      this.mnuFileOpen.Name = "mnuFileOpen";
      this.mnuFileOpen.Size = new System.Drawing.Size(152, 22);
      this.mnuFileOpen.Tag = "Open";
      this.mnuFileOpen.Text = "&Open";
      this.mnuFileOpen.Click += new System.EventHandler(this.Action);
      // 
      // frmConfigEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 562);
      this.Controls.Add(this.txtMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmConfigEdit";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "AppConfig Editor";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfigEdit_FormClosing);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileClose;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TextBox txtMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
    private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
    private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
    private System.Windows.Forms.SaveFileDialog saveDialog;
    private System.Windows.Forms.OpenFileDialog openDialog;
  }
}
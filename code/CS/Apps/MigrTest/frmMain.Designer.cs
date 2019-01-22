namespace Org.MigrTest
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
      this.lblStatus = new System.Windows.Forms.Label();
      this.btnRunAction = new System.Windows.Forms.Button();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.cboProfileFile = new System.Windows.Forms.ComboBox();
      this.txtProfilePath = new System.Windows.Forms.TextBox();
      this.ckDryRun = new System.Windows.Forms.CheckBox();
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
      this.mnuMain.Size = new System.Drawing.Size(1170, 24);
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
      this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.ckDryRun);
      this.pnlTop.Controls.Add(this.txtProfilePath);
      this.pnlTop.Controls.Add(this.cboProfileFile);
      this.pnlTop.Controls.Add(this.btnRunAction);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1170, 81);
      this.pnlTop.TabIndex = 1;
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(4, 729);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1170, 23);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // btnRunAction
      //
      this.btnRunAction.Location = new System.Drawing.Point(468, 40);
      this.btnRunAction.Name = "btnRunAction";
      this.btnRunAction.Size = new System.Drawing.Size(139, 23);
      this.btnRunAction.TabIndex = 0;
      this.btnRunAction.Tag = "RunAction";
      this.btnRunAction.Text = "Run Action";
      this.btnRunAction.UseVisualStyleBackColor = true;
      this.btnRunAction.Click += new System.EventHandler(this.Action);
      //
      // txtOut
      //
      this.txtOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(4, 105);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1170, 624);
      this.txtOut.TabIndex = 3;
      //
      // cboProfileFile
      //
      this.cboProfileFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboProfileFile.FormattingEnabled = true;
      this.cboProfileFile.Location = new System.Drawing.Point(13, 40);
      this.cboProfileFile.Name = "cboProfileFile";
      this.cboProfileFile.Size = new System.Drawing.Size(325, 21);
      this.cboProfileFile.TabIndex = 2;
      //
      // txtProfilePath
      //
      this.txtProfilePath.Location = new System.Drawing.Point(13, 10);
      this.txtProfilePath.Name = "txtProfilePath";
      this.txtProfilePath.Size = new System.Drawing.Size(594, 20);
      this.txtProfilePath.TabIndex = 3;
      //
      // ckDryRun
      //
      this.ckDryRun.AutoSize = true;
      this.ckDryRun.Location = new System.Drawing.Point(630, 12);
      this.ckDryRun.Name = "ckDryRun";
      this.ckDryRun.Size = new System.Drawing.Size(65, 17);
      this.ckDryRun.TabIndex = 4;
      this.ckDryRun.Text = "Dry Run";
      this.ckDryRun.UseVisualStyleBackColor = true;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1176, 752);
      this.Controls.Add(this.txtOut);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 2, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "MigrTest";
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
    private System.Windows.Forms.Button btnRunAction;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.TextBox txtProfilePath;
    private System.Windows.Forms.ComboBox cboProfileFile;
    private System.Windows.Forms.CheckBox ckDryRun;
  }
}


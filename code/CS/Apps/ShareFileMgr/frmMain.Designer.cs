namespace Org.ShareFileMgr
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
      this.btnListFolderRecursive = new System.Windows.Forms.Button();
      this.btnListFolderContents = new System.Windows.Forms.Button();
      this.btnConnect = new System.Windows.Forms.Button();
      this.txtOut = new FastColoredTextBoxNS.FastColoredTextBox();
      this.ckIsDryRun = new System.Windows.Forms.CheckBox();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtOut)).BeginInit();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1248, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(0, 701);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1248, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.ckIsDryRun);
      this.pnlTop.Controls.Add(this.btnListFolderRecursive);
      this.pnlTop.Controls.Add(this.btnListFolderContents);
      this.pnlTop.Controls.Add(this.btnConnect);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1248, 71);
      this.pnlTop.TabIndex = 2;
      //
      // btnListFolderRecursive
      //
      this.btnListFolderRecursive.Location = new System.Drawing.Point(147, 40);
      this.btnListFolderRecursive.Name = "btnListFolderRecursive";
      this.btnListFolderRecursive.Size = new System.Drawing.Size(128, 23);
      this.btnListFolderRecursive.TabIndex = 0;
      this.btnListFolderRecursive.Tag = "ListFolderRecursive";
      this.btnListFolderRecursive.Text = "List Folder Recursive";
      this.btnListFolderRecursive.UseVisualStyleBackColor = true;
      this.btnListFolderRecursive.Click += new System.EventHandler(this.Action);
      //
      // btnListFolderContents
      //
      this.btnListFolderContents.Location = new System.Drawing.Point(147, 11);
      this.btnListFolderContents.Name = "btnListFolderContents";
      this.btnListFolderContents.Size = new System.Drawing.Size(128, 23);
      this.btnListFolderContents.TabIndex = 0;
      this.btnListFolderContents.Tag = "ListFolderContents";
      this.btnListFolderContents.Text = "List Folder Contents";
      this.btnListFolderContents.UseVisualStyleBackColor = true;
      this.btnListFolderContents.Click += new System.EventHandler(this.Action);
      //
      // btnConnect
      //
      this.btnConnect.Location = new System.Drawing.Point(13, 11);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(128, 23);
      this.btnConnect.TabIndex = 0;
      this.btnConnect.Tag = "Connect";
      this.btnConnect.Text = "Connect";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.Action);
      //
      // txtOut
      //
      this.txtOut.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''
      };
      this.txtOut.AutoScrollMinSize = new System.Drawing.Size(27, 14);
      this.txtOut.BackBrush = null;
      this.txtOut.CharHeight = 14;
      this.txtOut.CharWidth = 8;
      this.txtOut.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtOut.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.IsReplaceMode = false;
      this.txtOut.Location = new System.Drawing.Point(0, 95);
      this.txtOut.Name = "txtOut";
      this.txtOut.Paddings = new System.Windows.Forms.Padding(0);
      this.txtOut.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtOut.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtOut.ServiceColors")));
      this.txtOut.Size = new System.Drawing.Size(1248, 606);
      this.txtOut.TabIndex = 3;
      this.txtOut.Zoom = 100;
      //
      // ckIsDryRun
      //
      this.ckIsDryRun.AutoSize = true;
      this.ckIsDryRun.Location = new System.Drawing.Point(349, 15);
      this.ckIsDryRun.Name = "ckIsDryRun";
      this.ckIsDryRun.Size = new System.Drawing.Size(65, 17);
      this.ckIsDryRun.TabIndex = 1;
      this.ckIsDryRun.Text = "Dry Run";
      this.ckIsDryRun.UseVisualStyleBackColor = true;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1248, 724);
      this.Controls.Add(this.txtOut);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "ShareFile Manager";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtOut)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnConnect;
    private FastColoredTextBoxNS.FastColoredTextBox txtOut;
    private System.Windows.Forms.Button btnListFolderContents;
    private System.Windows.Forms.Button btnListFolderRecursive;
    private System.Windows.Forms.CheckBox ckIsDryRun;
  }
}


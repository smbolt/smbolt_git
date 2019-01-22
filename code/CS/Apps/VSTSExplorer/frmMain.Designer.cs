namespace VSTSExplorer
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
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.tvMain = new System.Windows.Forms.TreeView();
      this.btnConnect = new System.Windows.Forms.Button();
      this.btnGetUsers = new System.Windows.Forms.Button();
      this.btnGetAccounts = new System.Windows.Forms.Button();
      this.btnWebApiGetToken = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
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
      this.mnuMain.Size = new System.Drawing.Size(911, 24);
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
      this.mnuFileExit.Size = new System.Drawing.Size(180, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(4, 678);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(911, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.btnGetAccounts);
      this.pnlTop.Controls.Add(this.btnGetUsers);
      this.pnlTop.Controls.Add(this.btnWebApiGetToken);
      this.pnlTop.Controls.Add(this.btnConnect);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(911, 93);
      this.pnlTop.TabIndex = 2;
      //
      // splitterMain
      //
      this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.Location = new System.Drawing.Point(4, 117);
      this.splitterMain.Name = "splitterMain";
      //
      // splitterMain.Panel1
      //
      this.splitterMain.Panel1.Controls.Add(this.tvMain);
      //
      // splitterMain.Panel2
      //
      this.splitterMain.Panel2.Controls.Add(this.txtOut);
      this.splitterMain.Size = new System.Drawing.Size(911, 561);
      this.splitterMain.SplitterDistance = 264;
      this.splitterMain.TabIndex = 3;
      //
      // txtOut
      //
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(0, 0);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(641, 559);
      this.txtOut.TabIndex = 0;
      this.txtOut.WordWrap = false;
      //
      // tvMain
      //
      this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMain.Location = new System.Drawing.Point(0, 0);
      this.tvMain.Name = "tvMain";
      this.tvMain.Size = new System.Drawing.Size(262, 559);
      this.tvMain.TabIndex = 0;
      //
      // btnConnect
      //
      this.btnConnect.Location = new System.Drawing.Point(15, 10);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(129, 23);
      this.btnConnect.TabIndex = 0;
      this.btnConnect.Tag = "Connect";
      this.btnConnect.Text = "Connect";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.Action);
      //
      // btnGetUsers
      //
      this.btnGetUsers.Location = new System.Drawing.Point(285, 10);
      this.btnGetUsers.Name = "btnGetUsers";
      this.btnGetUsers.Size = new System.Drawing.Size(129, 23);
      this.btnGetUsers.TabIndex = 2;
      this.btnGetUsers.Tag = "GetUsers";
      this.btnGetUsers.Text = "Get Users";
      this.btnGetUsers.UseVisualStyleBackColor = true;
      this.btnGetUsers.Click += new System.EventHandler(this.Action);
      //
      // btnGetAccounts
      //
      this.btnGetAccounts.Location = new System.Drawing.Point(150, 10);
      this.btnGetAccounts.Name = "btnGetAccounts";
      this.btnGetAccounts.Size = new System.Drawing.Size(129, 23);
      this.btnGetAccounts.TabIndex = 1;
      this.btnGetAccounts.Tag = "GetAccounts";
      this.btnGetAccounts.Text = "Get Accounts";
      this.btnGetAccounts.UseVisualStyleBackColor = true;
      this.btnGetAccounts.Click += new System.EventHandler(this.Action);
      //
      // btnWebApiGetToken
      //
      this.btnWebApiGetToken.Location = new System.Drawing.Point(15, 39);
      this.btnWebApiGetToken.Name = "btnWebApiGetToken";
      this.btnWebApiGetToken.Size = new System.Drawing.Size(129, 23);
      this.btnWebApiGetToken.TabIndex = 0;
      this.btnWebApiGetToken.Tag = "GetWebApiToken";
      this.btnWebApiGetToken.Text = "Get Web API Token";
      this.btnWebApiGetToken.UseVisualStyleBackColor = true;
      this.btnWebApiGetToken.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(919, 701);
      this.Controls.Add(this.splitterMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "VSTS Explorer";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      this.splitterMain.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.TreeView tvMain;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.Button btnConnect;
    private System.Windows.Forms.Button btnGetUsers;
    private System.Windows.Forms.Button btnGetAccounts;
    private System.Windows.Forms.Button btnWebApiGetToken;
  }
}


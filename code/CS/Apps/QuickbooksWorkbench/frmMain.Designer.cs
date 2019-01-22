namespace Org.QuickbooksWorkbench
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
      this.btnSendRequest = new System.Windows.Forms.Button();
      this.btnCloseConnection = new System.Windows.Forms.Button();
      this.btnEndSession = new System.Windows.Forms.Button();
      this.btnBeginSession = new System.Windows.Forms.Button();
      this.btnOpenConnection = new System.Windows.Forms.Button();
      this.txtOut = new FastColoredTextBoxNS.FastColoredTextBox();
      this.splitMain = new System.Windows.Forms.SplitContainer();
      this.btnCustomerQuery = new System.Windows.Forms.Button();
      this.btnCustomerAdd = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtOut)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
      this.splitMain.Panel2.SuspendLayout();
      this.splitMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1184, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(0, 717);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1184, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.btnSendRequest);
      this.pnlTop.Controls.Add(this.btnCustomerAdd);
      this.pnlTop.Controls.Add(this.btnCustomerQuery);
      this.pnlTop.Controls.Add(this.btnCloseConnection);
      this.pnlTop.Controls.Add(this.btnEndSession);
      this.pnlTop.Controls.Add(this.btnBeginSession);
      this.pnlTop.Controls.Add(this.btnOpenConnection);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1184, 87);
      this.pnlTop.TabIndex = 2;
      // 
      // btnSendRequest
      // 
      this.btnSendRequest.Location = new System.Drawing.Point(250, 16);
      this.btnSendRequest.Name = "btnSendRequest";
      this.btnSendRequest.Size = new System.Drawing.Size(107, 23);
      this.btnSendRequest.TabIndex = 2;
      this.btnSendRequest.Tag = "SendRequest";
      this.btnSendRequest.Text = "Send Request";
      this.btnSendRequest.UseVisualStyleBackColor = true;
      this.btnSendRequest.Click += new System.EventHandler(this.Action);
      // 
      // btnCloseConnection
      // 
      this.btnCloseConnection.Location = new System.Drawing.Point(476, 16);
      this.btnCloseConnection.Name = "btnCloseConnection";
      this.btnCloseConnection.Size = new System.Drawing.Size(107, 23);
      this.btnCloseConnection.TabIndex = 4;
      this.btnCloseConnection.Tag = "CloseConnection";
      this.btnCloseConnection.Text = "Close Connection";
      this.btnCloseConnection.UseVisualStyleBackColor = true;
      this.btnCloseConnection.Click += new System.EventHandler(this.Action);
      // 
      // btnEndSession
      // 
      this.btnEndSession.Location = new System.Drawing.Point(363, 16);
      this.btnEndSession.Name = "btnEndSession";
      this.btnEndSession.Size = new System.Drawing.Size(107, 23);
      this.btnEndSession.TabIndex = 3;
      this.btnEndSession.Tag = "EndSession";
      this.btnEndSession.Text = "End Session";
      this.btnEndSession.UseVisualStyleBackColor = true;
      this.btnEndSession.Click += new System.EventHandler(this.Action);
      // 
      // btnBeginSession
      // 
      this.btnBeginSession.Location = new System.Drawing.Point(137, 16);
      this.btnBeginSession.Name = "btnBeginSession";
      this.btnBeginSession.Size = new System.Drawing.Size(107, 23);
      this.btnBeginSession.TabIndex = 1;
      this.btnBeginSession.Tag = "BeginSession";
      this.btnBeginSession.Text = "Begin Session";
      this.btnBeginSession.UseVisualStyleBackColor = true;
      this.btnBeginSession.Click += new System.EventHandler(this.Action);
      // 
      // btnOpenConnection
      // 
      this.btnOpenConnection.Location = new System.Drawing.Point(24, 16);
      this.btnOpenConnection.Name = "btnOpenConnection";
      this.btnOpenConnection.Size = new System.Drawing.Size(107, 23);
      this.btnOpenConnection.TabIndex = 0;
      this.btnOpenConnection.Tag = "OpenConnection";
      this.btnOpenConnection.Text = "Open Connection";
      this.btnOpenConnection.UseVisualStyleBackColor = true;
      this.btnOpenConnection.Click += new System.EventHandler(this.Action);
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
        '\''};
      this.txtOut.AutoScrollMinSize = new System.Drawing.Size(25, 14);
      this.txtOut.BackBrush = null;
      this.txtOut.CharHeight = 14;
      this.txtOut.CharWidth = 7;
      this.txtOut.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtOut.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Consolas", 9F);
      this.txtOut.IsReplaceMode = false;
      this.txtOut.Location = new System.Drawing.Point(0, 0);
      this.txtOut.Name = "txtOut";
      this.txtOut.Paddings = new System.Windows.Forms.Padding(0);
      this.txtOut.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtOut.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtOut.ServiceColors")));
      this.txtOut.Size = new System.Drawing.Size(1184, 606);
      this.txtOut.TabIndex = 20;
      this.txtOut.Zoom = 100;
      // 
      // splitMain
      // 
      this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitMain.Location = new System.Drawing.Point(0, 111);
      this.splitMain.Name = "splitMain";
      this.splitMain.Panel1Collapsed = true;
      // 
      // splitMain.Panel2
      // 
      this.splitMain.Panel2.Controls.Add(this.txtOut);
      this.splitMain.Size = new System.Drawing.Size(1184, 606);
      this.splitMain.SplitterDistance = 152;
      this.splitMain.TabIndex = 4;
      // 
      // btnCustomerQuery
      // 
      this.btnCustomerQuery.Location = new System.Drawing.Point(865, 16);
      this.btnCustomerQuery.Name = "btnCustomerQuery";
      this.btnCustomerQuery.Size = new System.Drawing.Size(107, 23);
      this.btnCustomerQuery.TabIndex = 4;
      this.btnCustomerQuery.Tag = "CustomerQuery";
      this.btnCustomerQuery.Text = "Customer Query";
      this.btnCustomerQuery.UseVisualStyleBackColor = true;
      this.btnCustomerQuery.Click += new System.EventHandler(this.Action);
      // 
      // btnCustomerAdd
      // 
      this.btnCustomerAdd.Location = new System.Drawing.Point(752, 16);
      this.btnCustomerAdd.Name = "btnCustomerAdd";
      this.btnCustomerAdd.Size = new System.Drawing.Size(107, 23);
      this.btnCustomerAdd.TabIndex = 4;
      this.btnCustomerAdd.Tag = "CustomerAdd";
      this.btnCustomerAdd.Text = "Customer Add";
      this.btnCustomerAdd.UseVisualStyleBackColor = true;
      this.btnCustomerAdd.Click += new System.EventHandler(this.Action);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1184, 740);
      this.Controls.Add(this.splitMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Quickbooks Workbench - v 1.0.0.0";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtOut)).EndInit();
      this.splitMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
      this.splitMain.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnOpenConnection;
    private FastColoredTextBoxNS.FastColoredTextBox txtOut;
    private System.Windows.Forms.SplitContainer splitMain;
    private System.Windows.Forms.Button btnSendRequest;
    private System.Windows.Forms.Button btnCloseConnection;
    private System.Windows.Forms.Button btnEndSession;
    private System.Windows.Forms.Button btnBeginSession;
    private System.Windows.Forms.Button btnCustomerQuery;
    private System.Windows.Forms.Button btnCustomerAdd;
  }
}


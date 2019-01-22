namespace DevOpsWorkbench
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
      this.lblScriptFilePath = new System.Windows.Forms.Label();
      this.btnSaveScript = new System.Windows.Forms.Button();
      this.btnClearDisplay = new System.Windows.Forms.Button();
      this.btnRun = new System.Windows.Forms.Button();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.tvMain = new System.Windows.Forms.TreeView();
      this.imgListTreeView = new System.Windows.Forms.ImageList(this.components);
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageScript = new System.Windows.Forms.TabPage();
      this.txtScript = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tabPageOutput = new System.Windows.Forms.TabPage();
      this.txtOutput = new FastColoredTextBoxNS.FastColoredTextBox();
      this.btnRefreshTreeView = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageScript.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtScript)).BeginInit();
      this.tabPageOutput.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtOutput)).BeginInit();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(4, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
      this.mnuMain.Size = new System.Drawing.Size(1211, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(4, 748);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(1211, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.lblScriptFilePath);
      this.pnlTop.Controls.Add(this.btnSaveScript);
      this.pnlTop.Controls.Add(this.btnRefreshTreeView);
      this.pnlTop.Controls.Add(this.btnClearDisplay);
      this.pnlTop.Controls.Add(this.btnRun);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1211, 79);
      this.pnlTop.TabIndex = 2;
      // 
      // lblScriptFilePath
      // 
      this.lblScriptFilePath.AutoSize = true;
      this.lblScriptFilePath.Location = new System.Drawing.Point(136, 17);
      this.lblScriptFilePath.Name = "lblScriptFilePath";
      this.lblScriptFilePath.Size = new System.Drawing.Size(78, 13);
      this.lblScriptFilePath.TabIndex = 2;
      this.lblScriptFilePath.Text = "[script file path]";
      // 
      // btnSaveScript
      // 
      this.btnSaveScript.Location = new System.Drawing.Point(13, 41);
      this.btnSaveScript.Name = "btnSaveScript";
      this.btnSaveScript.Size = new System.Drawing.Size(117, 23);
      this.btnSaveScript.TabIndex = 1;
      this.btnSaveScript.Tag = "SaveScript";
      this.btnSaveScript.Text = "Save Script";
      this.btnSaveScript.UseVisualStyleBackColor = true;
      this.btnSaveScript.Click += new System.EventHandler(this.Action);
      // 
      // btnClearDisplay
      // 
      this.btnClearDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClearDisplay.Location = new System.Drawing.Point(1074, 12);
      this.btnClearDisplay.Name = "btnClearDisplay";
      this.btnClearDisplay.Size = new System.Drawing.Size(117, 23);
      this.btnClearDisplay.TabIndex = 0;
      this.btnClearDisplay.Tag = "ClearDisplay";
      this.btnClearDisplay.Text = "Clear Display";
      this.btnClearDisplay.UseVisualStyleBackColor = true;
      this.btnClearDisplay.Click += new System.EventHandler(this.Action);
      // 
      // btnRun
      // 
      this.btnRun.Location = new System.Drawing.Point(13, 12);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(117, 23);
      this.btnRun.TabIndex = 0;
      this.btnRun.Tag = "Run";
      this.btnRun.Text = "Run";
      this.btnRun.UseVisualStyleBackColor = true;
      this.btnRun.Click += new System.EventHandler(this.Action);
      // 
      // splitterMain
      // 
      this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.Location = new System.Drawing.Point(4, 103);
      this.splitterMain.Name = "splitterMain";
      // 
      // splitterMain.Panel1
      // 
      this.splitterMain.Panel1.Controls.Add(this.tvMain);
      // 
      // splitterMain.Panel2
      // 
      this.splitterMain.Panel2.Controls.Add(this.tabMain);
      this.splitterMain.Size = new System.Drawing.Size(1211, 645);
      this.splitterMain.SplitterDistance = 252;
      this.splitterMain.TabIndex = 3;
      // 
      // tvMain
      // 
      this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMain.HideSelection = false;
      this.tvMain.ImageIndex = 0;
      this.tvMain.ImageList = this.imgListTreeView;
      this.tvMain.Location = new System.Drawing.Point(0, 0);
      this.tvMain.Name = "tvMain";
      this.tvMain.SelectedImageIndex = 0;
      this.tvMain.Size = new System.Drawing.Size(250, 643);
      this.tvMain.TabIndex = 0;
      this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
      this.tvMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvMain_MouseDown);
      // 
      // imgListTreeView
      // 
      this.imgListTreeView.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imgListTreeView.ImageSize = new System.Drawing.Size(16, 16);
      this.imgListTreeView.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageScript);
      this.tabMain.Controls.Add(this.tabPageOutput);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(125, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(953, 643);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 0;
      // 
      // tabPageScript
      // 
      this.tabPageScript.Controls.Add(this.txtScript);
      this.tabPageScript.Location = new System.Drawing.Point(4, 22);
      this.tabPageScript.Name = "tabPageScript";
      this.tabPageScript.Size = new System.Drawing.Size(945, 617);
      this.tabPageScript.TabIndex = 0;
      this.tabPageScript.Text = "Script";
      this.tabPageScript.UseVisualStyleBackColor = true;
      // 
      // txtScript
      // 
      this.txtScript.AutoCompleteBracketsList = new char[] {
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
      this.txtScript.AutoIndentCharsPatterns = "";
      this.txtScript.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtScript.BackBrush = null;
      this.txtScript.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtScript.CharHeight = 13;
      this.txtScript.CharWidth = 7;
      this.txtScript.CommentPrefix = null;
      this.txtScript.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtScript.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtScript.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtScript.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtScript.IsReplaceMode = false;
      this.txtScript.Language = FastColoredTextBoxNS.Language.XML;
      this.txtScript.LeftBracket = '<';
      this.txtScript.LeftBracket2 = '(';
      this.txtScript.Location = new System.Drawing.Point(0, 0);
      this.txtScript.Name = "txtScript";
      this.txtScript.Paddings = new System.Windows.Forms.Padding(0);
      this.txtScript.RightBracket = '>';
      this.txtScript.RightBracket2 = ')';
      this.txtScript.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtScript.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtScript.ServiceColors")));
      this.txtScript.Size = new System.Drawing.Size(945, 617);
      this.txtScript.TabIndex = 5;
      this.txtScript.TabLength = 2;
      this.txtScript.Zoom = 100;
      this.txtScript.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txtOut_TextChanged);
      // 
      // tabPageOutput
      // 
      this.tabPageOutput.Controls.Add(this.txtOutput);
      this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
      this.tabPageOutput.Name = "tabPageOutput";
      this.tabPageOutput.Size = new System.Drawing.Size(945, 617);
      this.tabPageOutput.TabIndex = 1;
      this.tabPageOutput.Text = "Output";
      this.tabPageOutput.UseVisualStyleBackColor = true;
      // 
      // txtOutput
      // 
      this.txtOutput.AutoCompleteBracketsList = new char[] {
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
      this.txtOutput.AutoIndentCharsPatterns = "";
      this.txtOutput.AutoScrollMinSize = new System.Drawing.Size(2, 13);
      this.txtOutput.BackBrush = null;
      this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtOutput.CharHeight = 13;
      this.txtOutput.CharWidth = 7;
      this.txtOutput.CommentPrefix = null;
      this.txtOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtOutput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOutput.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtOutput.IsReplaceMode = false;
      this.txtOutput.Language = FastColoredTextBoxNS.Language.XML;
      this.txtOutput.LeftBracket = '<';
      this.txtOutput.LeftBracket2 = '(';
      this.txtOutput.Location = new System.Drawing.Point(0, 0);
      this.txtOutput.Name = "txtOutput";
      this.txtOutput.Paddings = new System.Windows.Forms.Padding(0);
      this.txtOutput.RightBracket = '>';
      this.txtOutput.RightBracket2 = ')';
      this.txtOutput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtOutput.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtOutput.ServiceColors")));
      this.txtOutput.Size = new System.Drawing.Size(945, 617);
      this.txtOutput.TabIndex = 6;
      this.txtOutput.TabLength = 2;
      this.txtOutput.Zoom = 100;
      // 
      // btnRefreshTreeView
      // 
      this.btnRefreshTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRefreshTreeView.Location = new System.Drawing.Point(1074, 41);
      this.btnRefreshTreeView.Name = "btnRefreshTreeView";
      this.btnRefreshTreeView.Size = new System.Drawing.Size(117, 23);
      this.btnRefreshTreeView.TabIndex = 0;
      this.btnRefreshTreeView.Tag = "RefreshTreeView";
      this.btnRefreshTreeView.Text = "Refresh TreeView";
      this.btnRefreshTreeView.UseVisualStyleBackColor = true;
      this.btnRefreshTreeView.Click += new System.EventHandler(this.Action);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1219, 771);
      this.Controls.Add(this.splitterMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "DevOps Workbench - v1.0.0.0";
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageScript.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtScript)).EndInit();
      this.tabPageOutput.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtOutput)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnRun;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.TreeView tvMain;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageScript;
    private System.Windows.Forms.TabPage tabPageOutput;
    private System.Windows.Forms.Button btnClearDisplay;
    private FastColoredTextBoxNS.FastColoredTextBox txtScript;
    private System.Windows.Forms.Button btnSaveScript;
    private System.Windows.Forms.Label lblScriptFilePath;
    private FastColoredTextBoxNS.FastColoredTextBox txtOutput;
    private System.Windows.Forms.ImageList imgListTreeView;
    private System.Windows.Forms.Button btnRefreshTreeView;
  }
}


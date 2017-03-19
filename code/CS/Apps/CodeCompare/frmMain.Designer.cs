namespace Org.CodeCompare
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
      this.pnlTop = new System.Windows.Forms.Panel();
      this.cboRightPath = new System.Windows.Forms.ComboBox();
      this.cboLeftPath = new System.Windows.Forms.ComboBox();
      this.cboFileTypes = new System.Windows.Forms.ComboBox();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.btnCompare = new System.Windows.Forms.Button();
      this.lblFileTypes = new System.Windows.Forms.Label();
      this.lblRightPath = new System.Windows.Forms.Label();
      this.lblLeftPath = new System.Windows.Forms.Label();
      this.lblStatus = new System.Windows.Forms.Label();
      this.txtOut = new FastColoredTextBoxNS.FastColoredTextBox();
      this.ctxMnuResults = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMnuResultsViewFile = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuResultsCompareFiles = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageCompareResults = new System.Windows.Forms.TabPage();
      this.ctxMnuResultsCopyLeftToRight = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuResultsCopyRightToLeft = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuResultsDeleteLeft = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuResultsDeleteRight = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtOut)).BeginInit();
      this.ctxMnuResults.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageCompareResults.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1128, 24);
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
      this.pnlTop.Controls.Add(this.cboRightPath);
      this.pnlTop.Controls.Add(this.cboLeftPath);
      this.pnlTop.Controls.Add(this.cboFileTypes);
      this.pnlTop.Controls.Add(this.button2);
      this.pnlTop.Controls.Add(this.button1);
      this.pnlTop.Controls.Add(this.btnCompare);
      this.pnlTop.Controls.Add(this.lblFileTypes);
      this.pnlTop.Controls.Add(this.lblRightPath);
      this.pnlTop.Controls.Add(this.lblLeftPath);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1128, 152);
      this.pnlTop.TabIndex = 1;
      // 
      // cboRightPath
      // 
      this.cboRightPath.FormattingEnabled = true;
      this.cboRightPath.Location = new System.Drawing.Point(13, 68);
      this.cboRightPath.Name = "cboRightPath";
      this.cboRightPath.Size = new System.Drawing.Size(713, 21);
      this.cboRightPath.TabIndex = 3;
      // 
      // cboLeftPath
      // 
      this.cboLeftPath.FormattingEnabled = true;
      this.cboLeftPath.Location = new System.Drawing.Point(13, 25);
      this.cboLeftPath.Name = "cboLeftPath";
      this.cboLeftPath.Size = new System.Drawing.Size(713, 21);
      this.cboLeftPath.TabIndex = 3;
      // 
      // cboFileTypes
      // 
      this.cboFileTypes.FormattingEnabled = true;
      this.cboFileTypes.Location = new System.Drawing.Point(13, 111);
      this.cboFileTypes.Name = "cboFileTypes";
      this.cboFileTypes.Size = new System.Drawing.Size(274, 21);
      this.cboFileTypes.TabIndex = 3;
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(733, 68);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(26, 21);
      this.button2.TabIndex = 2;
      this.button2.Text = "...";
      this.button2.UseVisualStyleBackColor = true;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(733, 25);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(26, 21);
      this.button1.TabIndex = 2;
      this.button1.Text = "...";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // btnCompare
      // 
      this.btnCompare.Location = new System.Drawing.Point(768, 25);
      this.btnCompare.Name = "btnCompare";
      this.btnCompare.Size = new System.Drawing.Size(78, 64);
      this.btnCompare.TabIndex = 2;
      this.btnCompare.Tag = "Compare";
      this.btnCompare.Text = "Compare";
      this.btnCompare.UseVisualStyleBackColor = true;
      this.btnCompare.Click += new System.EventHandler(this.Action);
      // 
      // lblFileTypes
      // 
      this.lblFileTypes.AutoSize = true;
      this.lblFileTypes.Location = new System.Drawing.Point(12, 95);
      this.lblFileTypes.Name = "lblFileTypes";
      this.lblFileTypes.Size = new System.Drawing.Size(55, 13);
      this.lblFileTypes.TabIndex = 1;
      this.lblFileTypes.Text = "File Types";
      // 
      // lblRightPath
      // 
      this.lblRightPath.AutoSize = true;
      this.lblRightPath.Location = new System.Drawing.Point(12, 52);
      this.lblRightPath.Name = "lblRightPath";
      this.lblRightPath.Size = new System.Drawing.Size(57, 13);
      this.lblRightPath.TabIndex = 1;
      this.lblRightPath.Text = "Right Path";
      // 
      // lblLeftPath
      // 
      this.lblLeftPath.AutoSize = true;
      this.lblLeftPath.Location = new System.Drawing.Point(13, 9);
      this.lblLeftPath.Name = "lblLeftPath";
      this.lblLeftPath.Size = new System.Drawing.Size(50, 13);
      this.lblLeftPath.TabIndex = 1;
      this.lblLeftPath.Text = "Left Path";
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 738);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1128, 19);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
      this.txtOut.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtOut.BackBrush = null;
      this.txtOut.CharHeight = 13;
      this.txtOut.CharWidth = 7;
      this.txtOut.ContextMenuStrip = this.ctxMnuResults;
      this.txtOut.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtOut.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtOut.IsReplaceMode = false;
      this.txtOut.Location = new System.Drawing.Point(3, 3);
      this.txtOut.Name = "txtOut";
      this.txtOut.Paddings = new System.Windows.Forms.Padding(0);
      this.txtOut.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtOut.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtOut.ServiceColors")));
      this.txtOut.Size = new System.Drawing.Size(1114, 530);
      this.txtOut.TabIndex = 3;
      this.txtOut.Zoom = 100;
      this.txtOut.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtOut_MouseClick);
      this.txtOut.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtCompareResults_MouseMove);
      // 
      // ctxMnuResults
      // 
      this.ctxMnuResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMnuResultsViewFile,
            this.ctxMnuResultsCompareFiles,
            this.ctxMnuResultsCopyLeftToRight,
            this.ctxMnuResultsCopyRightToLeft,
            this.ctxMnuResultsDeleteLeft,
            this.ctxMnuResultsDeleteRight});
      this.ctxMnuResults.Name = "ctxMnuResults";
      this.ctxMnuResults.Size = new System.Drawing.Size(171, 158);
      this.ctxMnuResults.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMnuResults_Opening);
      // 
      // ctxMnuResultsViewFile
      // 
      this.ctxMnuResultsViewFile.Name = "ctxMnuResultsViewFile";
      this.ctxMnuResultsViewFile.Size = new System.Drawing.Size(170, 22);
      this.ctxMnuResultsViewFile.Tag = "ViewFile";
      this.ctxMnuResultsViewFile.Text = "&View File";
      this.ctxMnuResultsViewFile.Click += new System.EventHandler(this.Action);
      // 
      // ctxMnuResultsCompareFiles
      // 
      this.ctxMnuResultsCompareFiles.Name = "ctxMnuResultsCompareFiles";
      this.ctxMnuResultsCompareFiles.Size = new System.Drawing.Size(170, 22);
      this.ctxMnuResultsCompareFiles.Tag = "CompareFiles";
      this.ctxMnuResultsCompareFiles.Text = "&Compare Files";
      this.ctxMnuResultsCompareFiles.Click += new System.EventHandler(this.Action);
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.tabMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 176);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1128, 562);
      this.pnlMain.TabIndex = 4;
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageCompareResults);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(125, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1128, 562);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 4;
      // 
      // tabPageCompareResults
      // 
      this.tabPageCompareResults.Controls.Add(this.txtOut);
      this.tabPageCompareResults.Location = new System.Drawing.Point(4, 22);
      this.tabPageCompareResults.Name = "tabPageCompareResults";
      this.tabPageCompareResults.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageCompareResults.Size = new System.Drawing.Size(1120, 536);
      this.tabPageCompareResults.TabIndex = 0;
      this.tabPageCompareResults.Text = "Compare Results";
      this.tabPageCompareResults.UseVisualStyleBackColor = true;
      // 
      // ctxMnuResultsCopyLeftToRight
      // 
      this.ctxMnuResultsCopyLeftToRight.Name = "ctxMnuResultsCopyLeftToRight";
      this.ctxMnuResultsCopyLeftToRight.Size = new System.Drawing.Size(170, 22);
      this.ctxMnuResultsCopyLeftToRight.Tag = "CopyLeftToRight";
      this.ctxMnuResultsCopyLeftToRight.Text = "Copy &Left to Right";
      this.ctxMnuResultsCopyLeftToRight.Click += new System.EventHandler(this.Action);
      // 
      // ctxMnuResultsCopyRightToLeft
      // 
      this.ctxMnuResultsCopyRightToLeft.Name = "ctxMnuResultsCopyRightToLeft";
      this.ctxMnuResultsCopyRightToLeft.Size = new System.Drawing.Size(170, 22);
      this.ctxMnuResultsCopyRightToLeft.Tag = "CopyRightToLeft";
      this.ctxMnuResultsCopyRightToLeft.Text = "Copy &Right to Left";
      this.ctxMnuResultsCopyRightToLeft.Click += new System.EventHandler(this.Action);
      // 
      // ctxMnuResultsDeleteLeft
      // 
      this.ctxMnuResultsDeleteLeft.Name = "ctxMnuResultsDeleteLeft";
      this.ctxMnuResultsDeleteLeft.Size = new System.Drawing.Size(170, 22);
      this.ctxMnuResultsDeleteLeft.Tag = "DeleteLeft";
      this.ctxMnuResultsDeleteLeft.Text = "Delete Left";
      this.ctxMnuResultsDeleteLeft.Click += new System.EventHandler(this.Action);
      // 
      // ctxMnuResultsDeleteRight
      // 
      this.ctxMnuResultsDeleteRight.Name = "ctxMnuResultsDeleteRight";
      this.ctxMnuResultsDeleteRight.Size = new System.Drawing.Size(170, 22);
      this.ctxMnuResultsDeleteRight.Tag = "DeleteRight";
      this.ctxMnuResultsDeleteRight.Text = "Delete Right";
      this.ctxMnuResultsDeleteRight.Click += new System.EventHandler(this.Action);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1128, 757);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Code Compare";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtOut)).EndInit();
      this.ctxMnuResults.ResumeLayout(false);
      this.pnlMain.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageCompareResults.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button btnCompare;
    private System.Windows.Forms.Label lblRightPath;
    private System.Windows.Forms.Label lblLeftPath;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.ComboBox cboFileTypes;
    private System.Windows.Forms.Label lblFileTypes;
    private System.Windows.Forms.ComboBox cboRightPath;
    private System.Windows.Forms.ComboBox cboLeftPath;
    private FastColoredTextBoxNS.FastColoredTextBox txtOut;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageCompareResults;
    private System.Windows.Forms.ContextMenuStrip ctxMnuResults;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuResultsViewFile;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuResultsCompareFiles;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuResultsCopyLeftToRight;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuResultsCopyRightToLeft;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuResultsDeleteLeft;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuResultsDeleteRight;
  }
}


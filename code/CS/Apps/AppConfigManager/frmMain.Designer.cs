namespace Org.AppConfigManager
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
      this.cboBreakProcess = new System.Windows.Forms.ComboBox();
      this.txtBreakOnThisElement = new System.Windows.Forms.TextBox();
      this.ckStopAtMemoryLogCount = new System.Windows.Forms.CheckBox();
      this.txtMemoryLogCount = new System.Windows.Forms.TextBox();
      this.btnLoadOriginalFile = new System.Windows.Forms.Button();
      this.btnTestLoad = new System.Windows.Forms.Button();
      this.btnSaveConfig = new System.Windows.Forms.Button();
      this.btnSimpleCompare = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageCompareReport = new System.Windows.Forms.TabPage();
      this.browserCompare = new System.Windows.Forms.WebBrowser();
      this.tabPageOriginalFile = new System.Windows.Forms.TabPage();
      this.txtOriginalFile = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tabPageOutput = new System.Windows.Forms.TabPage();
      this.txtOutput = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tabPageCompareFile = new System.Windows.Forms.TabPage();
      this.btnSaveCompare = new System.Windows.Forms.Button();
      this.txtCompareFile = new FastColoredTextBoxNS.FastColoredTextBox();
      this.btnSerializeAndCompare = new System.Windows.Forms.Button();
      this.btnLoadCompareFile = new System.Windows.Forms.Button();
      this.btnDebugSerialization = new System.Windows.Forms.Button();
      this.gbDebug = new System.Windows.Forms.GroupBox();
      this.lblStopAtNode = new System.Windows.Forms.Label();
      this.lblDebugChoice = new System.Windows.Forms.Label();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageCompareReport.SuspendLayout();
      this.tabPageOriginalFile.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtOriginalFile)).BeginInit();
      this.tabPageOutput.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtOutput)).BeginInit();
      this.tabPageCompareFile.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtCompareFile)).BeginInit();
      this.gbDebug.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1304, 24);
      this.mnuMain.TabIndex = 0;
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
      this.lblStatus.Location = new System.Drawing.Point(0, 777);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1304, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.gbDebug);
      this.pnlTop.Controls.Add(this.btnLoadCompareFile);
      this.pnlTop.Controls.Add(this.btnLoadOriginalFile);
      this.pnlTop.Controls.Add(this.btnTestLoad);
      this.pnlTop.Controls.Add(this.btnSaveCompare);
      this.pnlTop.Controls.Add(this.btnSaveConfig);
      this.pnlTop.Controls.Add(this.btnSerializeAndCompare);
      this.pnlTop.Controls.Add(this.btnSimpleCompare);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1304, 115);
      this.pnlTop.TabIndex = 2;
      //
      // cboBreakProcess
      //
      this.cboBreakProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboBreakProcess.FormattingEnabled = true;
      this.cboBreakProcess.Items.AddRange(new object[] {
        "Serialization",
        "Deserialization",
        "Both"
      });
      this.cboBreakProcess.Location = new System.Drawing.Point(252, 43);
      this.cboBreakProcess.Name = "cboBreakProcess";
      this.cboBreakProcess.Size = new System.Drawing.Size(96, 21);
      this.cboBreakProcess.TabIndex = 3;
      //
      // txtBreakOnThisElement
      //
      this.txtBreakOnThisElement.Location = new System.Drawing.Point(15, 44);
      this.txtBreakOnThisElement.Name = "txtBreakOnThisElement";
      this.txtBreakOnThisElement.Size = new System.Drawing.Size(231, 20);
      this.txtBreakOnThisElement.TabIndex = 1;
      //
      // ckStopAtMemoryLogCount
      //
      this.ckStopAtMemoryLogCount.AutoSize = true;
      this.ckStopAtMemoryLogCount.Location = new System.Drawing.Point(15, 72);
      this.ckStopAtMemoryLogCount.Name = "ckStopAtMemoryLogCount";
      this.ckStopAtMemoryLogCount.Size = new System.Drawing.Size(152, 17);
      this.ckStopAtMemoryLogCount.TabIndex = 2;
      this.ckStopAtMemoryLogCount.Text = "Stop at Memory Log Count";
      this.ckStopAtMemoryLogCount.UseVisualStyleBackColor = true;
      this.ckStopAtMemoryLogCount.CheckedChanged += new System.EventHandler(this.ckStopAtMemoryLogCount_CheckedChanged);
      //
      // txtMemoryLogCount
      //
      this.txtMemoryLogCount.Location = new System.Drawing.Point(173, 70);
      this.txtMemoryLogCount.Name = "txtMemoryLogCount";
      this.txtMemoryLogCount.Size = new System.Drawing.Size(73, 20);
      this.txtMemoryLogCount.TabIndex = 1;
      this.txtMemoryLogCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      //
      // btnLoadOriginalFile
      //
      this.btnLoadOriginalFile.Location = new System.Drawing.Point(12, 9);
      this.btnLoadOriginalFile.Name = "btnLoadOriginalFile";
      this.btnLoadOriginalFile.Size = new System.Drawing.Size(156, 23);
      this.btnLoadOriginalFile.TabIndex = 0;
      this.btnLoadOriginalFile.Tag = "LoadOriginalFile";
      this.btnLoadOriginalFile.Text = "Load Original File";
      this.btnLoadOriginalFile.UseVisualStyleBackColor = true;
      this.btnLoadOriginalFile.Click += new System.EventHandler(this.Action);
      //
      // btnTestLoad
      //
      this.btnTestLoad.Location = new System.Drawing.Point(12, 73);
      this.btnTestLoad.Name = "btnTestLoad";
      this.btnTestLoad.Size = new System.Drawing.Size(156, 23);
      this.btnTestLoad.TabIndex = 0;
      this.btnTestLoad.Tag = "LoadOrigAsAppConfig";
      this.btnTestLoad.Text = "Load Orig as AppConfig";
      this.btnTestLoad.UseVisualStyleBackColor = true;
      this.btnTestLoad.Click += new System.EventHandler(this.Action);
      //
      // btnSaveConfig
      //
      this.btnSaveConfig.Location = new System.Drawing.Point(174, 9);
      this.btnSaveConfig.Name = "btnSaveConfig";
      this.btnSaveConfig.Size = new System.Drawing.Size(156, 23);
      this.btnSaveConfig.TabIndex = 0;
      this.btnSaveConfig.Tag = "SaveOriginalFile";
      this.btnSaveConfig.Text = "Save Original File";
      this.btnSaveConfig.UseVisualStyleBackColor = true;
      this.btnSaveConfig.Click += new System.EventHandler(this.Action);
      //
      // btnSimpleCompare
      //
      this.btnSimpleCompare.Location = new System.Drawing.Point(336, 9);
      this.btnSimpleCompare.Name = "btnSimpleCompare";
      this.btnSimpleCompare.Size = new System.Drawing.Size(156, 23);
      this.btnSimpleCompare.TabIndex = 0;
      this.btnSimpleCompare.Tag = "SimpleCompare";
      this.btnSimpleCompare.Text = "Simple Compare";
      this.btnSimpleCompare.UseVisualStyleBackColor = true;
      this.btnSimpleCompare.Click += new System.EventHandler(this.Action);
      //
      // splitContainer1
      //
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 139);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      this.splitContainer1.Panel1Collapsed = true;
      //
      // splitContainer1.Panel2
      //
      this.splitContainer1.Panel2.Controls.Add(this.tabMain);
      this.splitContainer1.Size = new System.Drawing.Size(1304, 638);
      this.splitContainer1.SplitterDistance = 58;
      this.splitContainer1.TabIndex = 3;
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageOriginalFile);
      this.tabMain.Controls.Add(this.tabPageCompareFile);
      this.tabMain.Controls.Add(this.tabPageCompareReport);
      this.tabMain.Controls.Add(this.tabPageOutput);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(150, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1304, 638);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 0;
      //
      // tabPageCompareReport
      //
      this.tabPageCompareReport.Controls.Add(this.browserCompare);
      this.tabPageCompareReport.Location = new System.Drawing.Point(4, 22);
      this.tabPageCompareReport.Name = "tabPageCompareReport";
      this.tabPageCompareReport.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageCompareReport.Size = new System.Drawing.Size(1296, 655);
      this.tabPageCompareReport.TabIndex = 0;
      this.tabPageCompareReport.Text = "Comparison Report";
      this.tabPageCompareReport.UseVisualStyleBackColor = true;
      //
      // browserCompare
      //
      this.browserCompare.Dock = System.Windows.Forms.DockStyle.Fill;
      this.browserCompare.Location = new System.Drawing.Point(3, 3);
      this.browserCompare.Margin = new System.Windows.Forms.Padding(2);
      this.browserCompare.MinimumSize = new System.Drawing.Size(15, 16);
      this.browserCompare.Name = "browserCompare";
      this.browserCompare.Size = new System.Drawing.Size(1290, 649);
      this.browserCompare.TabIndex = 1;
      //
      // tabPageOriginalFile
      //
      this.tabPageOriginalFile.Controls.Add(this.txtOriginalFile);
      this.tabPageOriginalFile.Location = new System.Drawing.Point(4, 22);
      this.tabPageOriginalFile.Name = "tabPageOriginalFile";
      this.tabPageOriginalFile.Size = new System.Drawing.Size(1296, 655);
      this.tabPageOriginalFile.TabIndex = 1;
      this.tabPageOriginalFile.Text = "Original File";
      this.tabPageOriginalFile.UseVisualStyleBackColor = true;
      //
      // txtOriginalFile
      //
      this.txtOriginalFile.AutoCompleteBracketsList = new char[] {
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
      this.txtOriginalFile.AutoIndentCharsPatterns = "";
      this.txtOriginalFile.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtOriginalFile.BackBrush = null;
      this.txtOriginalFile.CharHeight = 13;
      this.txtOriginalFile.CharWidth = 7;
      this.txtOriginalFile.CommentPrefix = null;
      this.txtOriginalFile.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtOriginalFile.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtOriginalFile.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOriginalFile.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtOriginalFile.IsReplaceMode = false;
      this.txtOriginalFile.Language = FastColoredTextBoxNS.Language.XML;
      this.txtOriginalFile.LeftBracket = '<';
      this.txtOriginalFile.LeftBracket2 = '(';
      this.txtOriginalFile.Location = new System.Drawing.Point(0, 0);
      this.txtOriginalFile.Name = "txtOriginalFile";
      this.txtOriginalFile.Paddings = new System.Windows.Forms.Padding(0);
      this.txtOriginalFile.RightBracket = '>';
      this.txtOriginalFile.RightBracket2 = ')';
      this.txtOriginalFile.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtOriginalFile.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtOriginalFile.ServiceColors")));
      this.txtOriginalFile.Size = new System.Drawing.Size(1296, 655);
      this.txtOriginalFile.TabIndex = 4;
      this.txtOriginalFile.TabLength = 2;
      this.txtOriginalFile.Zoom = 100;
      //
      // tabPageOutput
      //
      this.tabPageOutput.Controls.Add(this.txtOutput);
      this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
      this.tabPageOutput.Name = "tabPageOutput";
      this.tabPageOutput.Size = new System.Drawing.Size(1296, 612);
      this.tabPageOutput.TabIndex = 2;
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
        '\''
      };
      this.txtOutput.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtOutput.BackBrush = null;
      this.txtOutput.CharHeight = 13;
      this.txtOutput.CharWidth = 7;
      this.txtOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtOutput.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOutput.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtOutput.IsReplaceMode = false;
      this.txtOutput.Location = new System.Drawing.Point(0, 0);
      this.txtOutput.Name = "txtOutput";
      this.txtOutput.Paddings = new System.Windows.Forms.Padding(0);
      this.txtOutput.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtOutput.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtOutput.ServiceColors")));
      this.txtOutput.Size = new System.Drawing.Size(1296, 612);
      this.txtOutput.TabIndex = 3;
      this.txtOutput.Zoom = 100;
      //
      // tabPageCompareFile
      //
      this.tabPageCompareFile.Controls.Add(this.txtCompareFile);
      this.tabPageCompareFile.Location = new System.Drawing.Point(4, 22);
      this.tabPageCompareFile.Name = "tabPageCompareFile";
      this.tabPageCompareFile.Size = new System.Drawing.Size(1296, 655);
      this.tabPageCompareFile.TabIndex = 3;
      this.tabPageCompareFile.Text = "Compare File";
      this.tabPageCompareFile.UseVisualStyleBackColor = true;
      //
      // btnSaveCompare
      //
      this.btnSaveCompare.Location = new System.Drawing.Point(174, 36);
      this.btnSaveCompare.Name = "btnSaveCompare";
      this.btnSaveCompare.Size = new System.Drawing.Size(156, 23);
      this.btnSaveCompare.TabIndex = 0;
      this.btnSaveCompare.Tag = "SaveCompareFile";
      this.btnSaveCompare.Text = "Save Compare File";
      this.btnSaveCompare.UseVisualStyleBackColor = true;
      this.btnSaveCompare.Click += new System.EventHandler(this.Action);
      //
      // txtCompareFile
      //
      this.txtCompareFile.AutoCompleteBracketsList = new char[] {
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
      this.txtCompareFile.AutoIndentCharsPatterns = "";
      this.txtCompareFile.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtCompareFile.BackBrush = null;
      this.txtCompareFile.CharHeight = 13;
      this.txtCompareFile.CharWidth = 7;
      this.txtCompareFile.CommentPrefix = null;
      this.txtCompareFile.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtCompareFile.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtCompareFile.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCompareFile.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtCompareFile.IsReplaceMode = false;
      this.txtCompareFile.Language = FastColoredTextBoxNS.Language.XML;
      this.txtCompareFile.LeftBracket = '<';
      this.txtCompareFile.LeftBracket2 = '(';
      this.txtCompareFile.Location = new System.Drawing.Point(0, 0);
      this.txtCompareFile.Name = "txtCompareFile";
      this.txtCompareFile.Paddings = new System.Windows.Forms.Padding(0);
      this.txtCompareFile.RightBracket = '>';
      this.txtCompareFile.RightBracket2 = ')';
      this.txtCompareFile.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtCompareFile.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtCompareFile.ServiceColors")));
      this.txtCompareFile.Size = new System.Drawing.Size(1296, 655);
      this.txtCompareFile.TabIndex = 5;
      this.txtCompareFile.TabLength = 2;
      this.txtCompareFile.Zoom = 100;
      //
      // btnSerializeAndCompare
      //
      this.btnSerializeAndCompare.Location = new System.Drawing.Point(336, 36);
      this.btnSerializeAndCompare.Name = "btnSerializeAndCompare";
      this.btnSerializeAndCompare.Size = new System.Drawing.Size(156, 23);
      this.btnSerializeAndCompare.TabIndex = 0;
      this.btnSerializeAndCompare.Tag = "SerializeAndCompare";
      this.btnSerializeAndCompare.Text = "Serialize and Compare";
      this.btnSerializeAndCompare.UseVisualStyleBackColor = true;
      this.btnSerializeAndCompare.Click += new System.EventHandler(this.Action);
      //
      // btnLoadCompareFile
      //
      this.btnLoadCompareFile.Location = new System.Drawing.Point(12, 36);
      this.btnLoadCompareFile.Name = "btnLoadCompareFile";
      this.btnLoadCompareFile.Size = new System.Drawing.Size(156, 23);
      this.btnLoadCompareFile.TabIndex = 0;
      this.btnLoadCompareFile.Tag = "LoadCompareFile";
      this.btnLoadCompareFile.Text = "Load Compare File";
      this.btnLoadCompareFile.UseVisualStyleBackColor = true;
      this.btnLoadCompareFile.Click += new System.EventHandler(this.Action);
      //
      // btnDebugSerialization
      //
      this.btnDebugSerialization.Location = new System.Drawing.Point(380, 35);
      this.btnDebugSerialization.Name = "btnDebugSerialization";
      this.btnDebugSerialization.Size = new System.Drawing.Size(130, 36);
      this.btnDebugSerialization.TabIndex = 0;
      this.btnDebugSerialization.Tag = "DebugSerialization";
      this.btnDebugSerialization.Text = "Debug Serialization";
      this.btnDebugSerialization.UseVisualStyleBackColor = true;
      this.btnDebugSerialization.Click += new System.EventHandler(this.Action);
      //
      // gbDebug
      //
      this.gbDebug.Controls.Add(this.lblDebugChoice);
      this.gbDebug.Controls.Add(this.lblStopAtNode);
      this.gbDebug.Controls.Add(this.ckStopAtMemoryLogCount);
      this.gbDebug.Controls.Add(this.cboBreakProcess);
      this.gbDebug.Controls.Add(this.txtMemoryLogCount);
      this.gbDebug.Controls.Add(this.txtBreakOnThisElement);
      this.gbDebug.Controls.Add(this.btnDebugSerialization);
      this.gbDebug.Location = new System.Drawing.Point(543, 9);
      this.gbDebug.Name = "gbDebug";
      this.gbDebug.Size = new System.Drawing.Size(749, 100);
      this.gbDebug.TabIndex = 4;
      this.gbDebug.TabStop = false;
      this.gbDebug.Text = "ObjectFactory Serialization and Deserialization Debugging";
      //
      // lblStopAtNode
      //
      this.lblStopAtNode.AutoSize = true;
      this.lblStopAtNode.Location = new System.Drawing.Point(12, 28);
      this.lblStopAtNode.Name = "lblStopAtNode";
      this.lblStopAtNode.Size = new System.Drawing.Size(171, 13);
      this.lblStopAtNode.TabIndex = 4;
      this.lblStopAtNode.Text = "Element Name or Element.Attribute";
      //
      // lblDebugChoice
      //
      this.lblDebugChoice.AutoSize = true;
      this.lblDebugChoice.Location = new System.Drawing.Point(249, 28);
      this.lblDebugChoice.Name = "lblDebugChoice";
      this.lblDebugChoice.Size = new System.Drawing.Size(113, 13);
      this.lblDebugChoice.TabIndex = 4;
      this.lblDebugChoice.Text = "Select Debug Process";
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1304, 800);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "AppConfig Manager";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageCompareReport.ResumeLayout(false);
      this.tabPageOriginalFile.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtOriginalFile)).EndInit();
      this.tabPageOutput.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtOutput)).EndInit();
      this.tabPageCompareFile.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtCompareFile)).EndInit();
      this.gbDebug.ResumeLayout(false);
      this.gbDebug.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageCompareReport;
    private System.Windows.Forms.TabPage tabPageOriginalFile;
    private System.Windows.Forms.WebBrowser browserCompare;
    private System.Windows.Forms.Button btnSimpleCompare;
    private System.Windows.Forms.Button btnSaveConfig;
    private System.Windows.Forms.Button btnLoadOriginalFile;
    private System.Windows.Forms.TabPage tabPageOutput;
    private FastColoredTextBoxNS.FastColoredTextBox txtOutput;
    private FastColoredTextBoxNS.FastColoredTextBox txtOriginalFile;
    private System.Windows.Forms.CheckBox ckStopAtMemoryLogCount;
    private System.Windows.Forms.TextBox txtMemoryLogCount;
    private System.Windows.Forms.TextBox txtBreakOnThisElement;
    private System.Windows.Forms.ComboBox cboBreakProcess;
    private System.Windows.Forms.Button btnTestLoad;
    private System.Windows.Forms.TabPage tabPageCompareFile;
    private System.Windows.Forms.Button btnSaveCompare;
    private FastColoredTextBoxNS.FastColoredTextBox txtCompareFile;
    private System.Windows.Forms.Button btnLoadCompareFile;
    private System.Windows.Forms.Button btnSerializeAndCompare;
    private System.Windows.Forms.Button btnDebugSerialization;
    private System.Windows.Forms.GroupBox gbDebug;
    private System.Windows.Forms.Label lblStopAtNode;
    private System.Windows.Forms.Label lblDebugChoice;
  }
}


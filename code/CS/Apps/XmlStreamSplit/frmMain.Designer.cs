namespace Org.XmlStreamSplit
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
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnShowLastSegment = new System.Windows.Forms.Button();
      this.btnShowFirstSegment = new System.Windows.Forms.Button();
      this.btnGoToLine = new System.Windows.Forms.Button();
      this.btnGo = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileSelect = new System.Windows.Forms.ToolStripMenuItem();
      this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
      this.lblSelectedInputFile = new System.Windows.Forms.Label();
      this.txtInputSelectedInputFile = new System.Windows.Forms.TextBox();
      this.lblFileInfo = new System.Windows.Forms.Label();
      this.lblSegmentSize = new System.Windows.Forms.Label();
      this.cboSegmentSize = new System.Windows.Forms.ComboBox();
      this.lblSegmentCount = new System.Windows.Forms.Label();
      this.btnNextSegment = new System.Windows.Forms.Button();
      this.btnShowPrevSegment = new System.Windows.Forms.Button();
      this.lblTextView = new System.Windows.Forms.Label();
      this.cboTextView = new System.Windows.Forms.ComboBox();
      this.ckWrapText = new System.Windows.Forms.CheckBox();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.tvMain = new System.Windows.Forms.TreeView();
      this.txtOut = new System.Windows.Forms.RichTextBox();
      this.btnAnalyzeFile = new System.Windows.Forms.Button();
      this.lblFileType = new System.Windows.Forms.Label();
      this.cboFileType = new System.Windows.Forms.ComboBox();
      this.txtXmlHierarchy = new System.Windows.Forms.TextBox();
      this.lblXmlHierarchy = new System.Windows.Forms.Label();
      this.btnFormatXml = new System.Windows.Forms.Button();
      this.pnlFileInfo = new System.Windows.Forms.Panel();
      this.scrollFile = new System.Windows.Forms.HScrollBar();
      this.pnlTop.SuspendLayout();
      this.mnuMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
      this.pnlFileInfo.SuspendLayout();
      this.SuspendLayout();
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.ckWrapText);
      this.pnlTop.Controls.Add(this.cboFileType);
      this.pnlTop.Controls.Add(this.cboTextView);
      this.pnlTop.Controls.Add(this.cboSegmentSize);
      this.pnlTop.Controls.Add(this.txtXmlHierarchy);
      this.pnlTop.Controls.Add(this.txtInputSelectedInputFile);
      this.pnlTop.Controls.Add(this.lblXmlHierarchy);
      this.pnlTop.Controls.Add(this.lblFileType);
      this.pnlTop.Controls.Add(this.lblFileInfo);
      this.pnlTop.Controls.Add(this.lblTextView);
      this.pnlTop.Controls.Add(this.lblSegmentCount);
      this.pnlTop.Controls.Add(this.lblSegmentSize);
      this.pnlTop.Controls.Add(this.lblSelectedInputFile);
      this.pnlTop.Controls.Add(this.btnShowLastSegment);
      this.pnlTop.Controls.Add(this.btnShowPrevSegment);
      this.pnlTop.Controls.Add(this.btnNextSegment);
      this.pnlTop.Controls.Add(this.btnShowFirstSegment);
      this.pnlTop.Controls.Add(this.btnGoToLine);
      this.pnlTop.Controls.Add(this.btnFormatXml);
      this.pnlTop.Controls.Add(this.btnAnalyzeFile);
      this.pnlTop.Controls.Add(this.btnGo);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1408, 152);
      this.pnlTop.TabIndex = 0;
      //
      // btnShowLastSegment
      //
      this.btnShowLastSegment.Location = new System.Drawing.Point(712, 66);
      this.btnShowLastSegment.Name = "btnShowLastSegment";
      this.btnShowLastSegment.Size = new System.Drawing.Size(115, 23);
      this.btnShowLastSegment.TabIndex = 0;
      this.btnShowLastSegment.Tag = "ShowLastSegment";
      this.btnShowLastSegment.Text = "Show LastSegment";
      this.btnShowLastSegment.UseVisualStyleBackColor = true;
      this.btnShowLastSegment.Click += new System.EventHandler(this.Action);
      //
      // btnShowFirstSegment
      //
      this.btnShowFirstSegment.Location = new System.Drawing.Point(528, 66);
      this.btnShowFirstSegment.Name = "btnShowFirstSegment";
      this.btnShowFirstSegment.Size = new System.Drawing.Size(115, 23);
      this.btnShowFirstSegment.TabIndex = 0;
      this.btnShowFirstSegment.Tag = "ShowFirstSegment";
      this.btnShowFirstSegment.Text = "Show First Segment";
      this.btnShowFirstSegment.UseVisualStyleBackColor = true;
      this.btnShowFirstSegment.Click += new System.EventHandler(this.Action);
      //
      // btnGoToLine
      //
      this.btnGoToLine.Location = new System.Drawing.Point(1124, 62);
      this.btnGoToLine.Name = "btnGoToLine";
      this.btnGoToLine.Size = new System.Drawing.Size(123, 23);
      this.btnGoToLine.TabIndex = 0;
      this.btnGoToLine.Tag = "GoToLine";
      this.btnGoToLine.Text = "Go To Line";
      this.btnGoToLine.UseVisualStyleBackColor = true;
      this.btnGoToLine.Click += new System.EventHandler(this.btnGoToLine_Click);
      //
      // btnGo
      //
      this.btnGo.Location = new System.Drawing.Point(1124, 36);
      this.btnGo.Name = "btnGo";
      this.btnGo.Size = new System.Drawing.Size(123, 23);
      this.btnGo.TabIndex = 0;
      this.btnGo.Tag = "Go";
      this.btnGo.Text = "Go";
      this.btnGo.UseVisualStyleBackColor = true;
      this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(4, 786);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1408, 23);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(4, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1408, 24);
      this.mnuMain.TabIndex = 3;
      this.mnuMain.Text = "menuStrip1";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFileSelect,
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
      // mnuFileSelect
      //
      this.mnuFileSelect.Name = "mnuFileSelect";
      this.mnuFileSelect.Size = new System.Drawing.Size(180, 22);
      this.mnuFileSelect.Tag = "SelectInputFile";
      this.mnuFileSelect.Text = "&Select Input File";
      this.mnuFileSelect.Click += new System.EventHandler(this.Action);
      //
      // lblSelectedInputFile
      //
      this.lblSelectedInputFile.AutoSize = true;
      this.lblSelectedInputFile.Location = new System.Drawing.Point(12, 7);
      this.lblSelectedInputFile.Name = "lblSelectedInputFile";
      this.lblSelectedInputFile.Size = new System.Drawing.Size(95, 13);
      this.lblSelectedInputFile.TabIndex = 1;
      this.lblSelectedInputFile.Text = "Selected Input File";
      //
      // txtInputSelectedInputFile
      //
      this.txtInputSelectedInputFile.Location = new System.Drawing.Point(15, 24);
      this.txtInputSelectedInputFile.Name = "txtInputSelectedInputFile";
      this.txtInputSelectedInputFile.Size = new System.Drawing.Size(813, 20);
      this.txtInputSelectedInputFile.TabIndex = 2;
      //
      // lblFileInfo
      //
      this.lblFileInfo.Location = new System.Drawing.Point(539, 7);
      this.lblFileInfo.Name = "lblFileInfo";
      this.lblFileInfo.Size = new System.Drawing.Size(294, 14);
      this.lblFileInfo.TabIndex = 1;
      this.lblFileInfo.Text = "(file info)";
      this.lblFileInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // lblSegmentSize
      //
      this.lblSegmentSize.AutoSize = true;
      this.lblSegmentSize.Location = new System.Drawing.Point(12, 51);
      this.lblSegmentSize.Name = "lblSegmentSize";
      this.lblSegmentSize.Size = new System.Drawing.Size(72, 13);
      this.lblSegmentSize.TabIndex = 1;
      this.lblSegmentSize.Text = "Segment Size";
      //
      // cboSegmentSize
      //
      this.cboSegmentSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboSegmentSize.FormattingEnabled = true;
      this.cboSegmentSize.Location = new System.Drawing.Point(15, 68);
      this.cboSegmentSize.Name = "cboSegmentSize";
      this.cboSegmentSize.Size = new System.Drawing.Size(169, 21);
      this.cboSegmentSize.TabIndex = 3;
      this.cboSegmentSize.SelectedIndexChanged += new System.EventHandler(this.cboSegmentSize_SelectedIndexChanged);
      //
      // lblSegmentCount
      //
      this.lblSegmentCount.AutoSize = true;
      this.lblSegmentCount.Location = new System.Drawing.Point(190, 72);
      this.lblSegmentCount.Name = "lblSegmentCount";
      this.lblSegmentCount.Size = new System.Drawing.Size(144, 13);
      this.lblSegmentCount.TabIndex = 1;
      this.lblSegmentCount.Text = "Approximate segement count";
      //
      // btnNextSegment
      //
      this.btnNextSegment.Location = new System.Drawing.Point(678, 66);
      this.btnNextSegment.Name = "btnNextSegment";
      this.btnNextSegment.Size = new System.Drawing.Size(30, 23);
      this.btnNextSegment.TabIndex = 0;
      this.btnNextSegment.Tag = "ShowNextSegment";
      this.btnNextSegment.Text = "-->";
      this.btnNextSegment.UseVisualStyleBackColor = true;
      this.btnNextSegment.Click += new System.EventHandler(this.Action);
      //
      // btnShowPrevSegment
      //
      this.btnShowPrevSegment.Location = new System.Drawing.Point(647, 66);
      this.btnShowPrevSegment.Name = "btnShowPrevSegment";
      this.btnShowPrevSegment.Size = new System.Drawing.Size(30, 23);
      this.btnShowPrevSegment.TabIndex = 0;
      this.btnShowPrevSegment.Tag = "ShowPrevSegment";
      this.btnShowPrevSegment.Text = "<--";
      this.btnShowPrevSegment.UseVisualStyleBackColor = true;
      this.btnShowPrevSegment.Click += new System.EventHandler(this.Action);
      //
      // lblTextView
      //
      this.lblTextView.AutoSize = true;
      this.lblTextView.Location = new System.Drawing.Point(12, 96);
      this.lblTextView.Name = "lblTextView";
      this.lblTextView.Size = new System.Drawing.Size(54, 13);
      this.lblTextView.TabIndex = 1;
      this.lblTextView.Text = "Text View";
      //
      // cboTextView
      //
      this.cboTextView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTextView.FormattingEnabled = true;
      this.cboTextView.Items.AddRange(new object[] {
        "Normal",
        "Hex Dump"
      });
      this.cboTextView.Location = new System.Drawing.Point(15, 113);
      this.cboTextView.Name = "cboTextView";
      this.cboTextView.Size = new System.Drawing.Size(169, 21);
      this.cboTextView.TabIndex = 3;
      this.cboTextView.SelectedIndexChanged += new System.EventHandler(this.cboTextView_SelectedIndexChanged);
      //
      // ckWrapText
      //
      this.ckWrapText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.ckWrapText.AutoSize = true;
      this.ckWrapText.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ckWrapText.Location = new System.Drawing.Point(1316, 129);
      this.ckWrapText.Name = "ckWrapText";
      this.ckWrapText.Size = new System.Drawing.Size(76, 17);
      this.ckWrapText.TabIndex = 4;
      this.ckWrapText.Tag = "ToggleWrapText";
      this.ckWrapText.Text = "Wrap Text";
      this.ckWrapText.UseVisualStyleBackColor = true;
      this.ckWrapText.CheckedChanged += new System.EventHandler(this.Action);
      //
      // splitterMain
      //
      this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.Location = new System.Drawing.Point(4, 229);
      this.splitterMain.Name = "splitterMain";
      //
      // splitterMain.Panel1
      //
      this.splitterMain.Panel1.Controls.Add(this.tvMain);
      //
      // splitterMain.Panel2
      //
      this.splitterMain.Panel2.Controls.Add(this.txtOut);
      this.splitterMain.Size = new System.Drawing.Size(1408, 557);
      this.splitterMain.SplitterDistance = 244;
      this.splitterMain.SplitterWidth = 3;
      this.splitterMain.TabIndex = 5;
      //
      // tvMain
      //
      this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMain.Location = new System.Drawing.Point(0, 0);
      this.tvMain.Name = "tvMain";
      this.tvMain.Size = new System.Drawing.Size(242, 555);
      this.tvMain.TabIndex = 0;
      //
      // txtOut
      //
      this.txtOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(0, 0);
      this.txtOut.Name = "txtOut";
      this.txtOut.Size = new System.Drawing.Size(1159, 555);
      this.txtOut.TabIndex = 0;
      this.txtOut.Text = "";
      this.txtOut.WordWrap = false;
      //
      // btnAnalyzeFile
      //
      this.btnAnalyzeFile.Location = new System.Drawing.Point(837, 24);
      this.btnAnalyzeFile.Name = "btnAnalyzeFile";
      this.btnAnalyzeFile.Size = new System.Drawing.Size(123, 23);
      this.btnAnalyzeFile.TabIndex = 0;
      this.btnAnalyzeFile.Tag = "AnalyzeFile";
      this.btnAnalyzeFile.Text = "Analyze File";
      this.btnAnalyzeFile.UseVisualStyleBackColor = true;
      this.btnAnalyzeFile.Click += new System.EventHandler(this.Action);
      //
      // lblFileType
      //
      this.lblFileType.AutoSize = true;
      this.lblFileType.Location = new System.Drawing.Point(190, 96);
      this.lblFileType.Name = "lblFileType";
      this.lblFileType.Size = new System.Drawing.Size(50, 13);
      this.lblFileType.TabIndex = 1;
      this.lblFileType.Text = "File Type";
      //
      // cboFileType
      //
      this.cboFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboFileType.FormattingEnabled = true;
      this.cboFileType.Items.AddRange(new object[] {
        "XML File",
        "Text File",
        "Binary File"
      });
      this.cboFileType.Location = new System.Drawing.Point(193, 113);
      this.cboFileType.Name = "cboFileType";
      this.cboFileType.Size = new System.Drawing.Size(169, 21);
      this.cboFileType.TabIndex = 3;
      this.cboFileType.SelectedIndexChanged += new System.EventHandler(this.cboTextView_SelectedIndexChanged);
      //
      // txtXmlHierarchy
      //
      this.txtXmlHierarchy.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtXmlHierarchy.Location = new System.Drawing.Point(377, 113);
      this.txtXmlHierarchy.Name = "txtXmlHierarchy";
      this.txtXmlHierarchy.Size = new System.Drawing.Size(451, 21);
      this.txtXmlHierarchy.TabIndex = 2;
      //
      // lblXmlHierarchy
      //
      this.lblXmlHierarchy.AutoSize = true;
      this.lblXmlHierarchy.Location = new System.Drawing.Point(374, 96);
      this.lblXmlHierarchy.Name = "lblXmlHierarchy";
      this.lblXmlHierarchy.Size = new System.Drawing.Size(77, 13);
      this.lblXmlHierarchy.TabIndex = 1;
      this.lblXmlHierarchy.Text = "XML Hierarchy";
      //
      // btnFormatXml
      //
      this.btnFormatXml.Location = new System.Drawing.Point(837, 111);
      this.btnFormatXml.Name = "btnFormatXml";
      this.btnFormatXml.Size = new System.Drawing.Size(123, 23);
      this.btnFormatXml.TabIndex = 0;
      this.btnFormatXml.Tag = "FormatXml";
      this.btnFormatXml.Text = "Format XML";
      this.btnFormatXml.UseVisualStyleBackColor = true;
      this.btnFormatXml.Click += new System.EventHandler(this.Action);
      //
      // pnlFileInfo
      //
      this.pnlFileInfo.Controls.Add(this.scrollFile);
      this.pnlFileInfo.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlFileInfo.Location = new System.Drawing.Point(4, 176);
      this.pnlFileInfo.Name = "pnlFileInfo";
      this.pnlFileInfo.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
      this.pnlFileInfo.Size = new System.Drawing.Size(1408, 53);
      this.pnlFileInfo.TabIndex = 6;
      //
      // scrollFile
      //
      this.scrollFile.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.scrollFile.Location = new System.Drawing.Point(0, 33);
      this.scrollFile.Name = "scrollFile";
      this.scrollFile.Size = new System.Drawing.Size(1408, 17);
      this.scrollFile.TabIndex = 0;
      this.scrollFile.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollFile_Scroll);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1416, 809);
      this.Controls.Add(this.splitterMain);
      this.Controls.Add(this.pnlFileInfo);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Xml Stream Splitter";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      this.pnlFileInfo.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnGo;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button btnGoToLine;
    private System.Windows.Forms.Button btnShowLastSegment;
    private System.Windows.Forms.Button btnShowFirstSegment;
    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileSelect;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.OpenFileDialog dlgFileOpen;
    private System.Windows.Forms.TextBox txtInputSelectedInputFile;
    private System.Windows.Forms.Label lblSelectedInputFile;
    private System.Windows.Forms.Label lblFileInfo;
    private System.Windows.Forms.ComboBox cboSegmentSize;
    private System.Windows.Forms.Label lblSegmentSize;
    private System.Windows.Forms.Label lblSegmentCount;
    private System.Windows.Forms.ComboBox cboTextView;
    private System.Windows.Forms.Label lblTextView;
    private System.Windows.Forms.Button btnShowPrevSegment;
    private System.Windows.Forms.Button btnNextSegment;
    private System.Windows.Forms.CheckBox ckWrapText;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.TreeView tvMain;
    private System.Windows.Forms.RichTextBox txtOut;
    private System.Windows.Forms.Button btnAnalyzeFile;
    private System.Windows.Forms.ComboBox cboFileType;
    private System.Windows.Forms.TextBox txtXmlHierarchy;
    private System.Windows.Forms.Label lblXmlHierarchy;
    private System.Windows.Forms.Label lblFileType;
    private System.Windows.Forms.Button btnFormatXml;
    private System.Windows.Forms.Panel pnlFileInfo;
    private System.Windows.Forms.HScrollBar scrollFile;
  }
}


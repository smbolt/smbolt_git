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
      this.mnuFileSaveConfig = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.pbRightPath = new System.Windows.Forms.PictureBox();
      this.pbLeftPath = new System.Windows.Forms.PictureBox();
      this.cboRightStem = new System.Windows.Forms.ComboBox();
      this.cboRightBranch = new System.Windows.Forms.ComboBox();
      this.cboLeftBranch = new System.Windows.Forms.ComboBox();
      this.cboLeftStem = new System.Windows.Forms.ComboBox();
      this.cboRightPath = new System.Windows.Forms.ComboBox();
      this.cboLeftPath = new System.Windows.Forms.ComboBox();
      this.cboFileTypes = new System.Windows.Forms.ComboBox();
      this.btnBrowseRight = new System.Windows.Forms.Button();
      this.btnBrowseLeft = new System.Windows.Forms.Button();
      this.btnCompare = new System.Windows.Forms.Button();
      this.lblFileTypes = new System.Windows.Forms.Label();
      this.lblRightPath = new System.Windows.Forms.Label();
      this.lblRightStem = new System.Windows.Forms.Label();
      this.lblRightBranch = new System.Windows.Forms.Label();
      this.lblLeftBranch = new System.Windows.Forms.Label();
      this.lblLeftStem = new System.Windows.Forms.Label();
      this.lblLeftPath = new System.Windows.Forms.Label();
      this.lblStatus = new System.Windows.Forms.Label();
      this.ctxMnuResults = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMnuResultsViewFile = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuResultsCompareFiles = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuResultsCopyLeftToRight = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuResultsCopyRightToLeft = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuResultsDeleteLeft = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuResultsDeleteRight = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageCompareResults = new System.Windows.Forms.TabPage();
      this.gvResults = new System.Windows.Forms.DataGridView();
      this.lblIdenticalPathWarning = new System.Windows.Forms.Label();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbRightPath)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbLeftPath)).BeginInit();
      this.ctxMnuResults.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageCompareResults.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvResults)).BeginInit();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
      this.mnuMain.Size = new System.Drawing.Size(1830, 25);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFileSaveConfig,
        this.mnuFileExit
      });
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 19);
      this.mnuFile.Text = "&File";
      //
      // mnuFileSaveConfig
      //
      this.mnuFileSaveConfig.Name = "mnuFileSaveConfig";
      this.mnuFileSaveConfig.Size = new System.Drawing.Size(137, 22);
      this.mnuFileSaveConfig.Tag = "SaveConfig";
      this.mnuFileSaveConfig.Text = "&Save Config";
      this.mnuFileSaveConfig.Click += new System.EventHandler(this.Action);
      //
      // mnuFileExit
      //
      this.mnuFileExit.Name = "mnuFileExit";
      this.mnuFileExit.Size = new System.Drawing.Size(137, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.pbRightPath);
      this.pnlTop.Controls.Add(this.pbLeftPath);
      this.pnlTop.Controls.Add(this.cboRightStem);
      this.pnlTop.Controls.Add(this.cboRightBranch);
      this.pnlTop.Controls.Add(this.cboLeftBranch);
      this.pnlTop.Controls.Add(this.cboLeftStem);
      this.pnlTop.Controls.Add(this.cboRightPath);
      this.pnlTop.Controls.Add(this.cboLeftPath);
      this.pnlTop.Controls.Add(this.cboFileTypes);
      this.pnlTop.Controls.Add(this.btnBrowseRight);
      this.pnlTop.Controls.Add(this.btnBrowseLeft);
      this.pnlTop.Controls.Add(this.btnCompare);
      this.pnlTop.Controls.Add(this.lblFileTypes);
      this.pnlTop.Controls.Add(this.lblRightPath);
      this.pnlTop.Controls.Add(this.lblRightStem);
      this.pnlTop.Controls.Add(this.lblRightBranch);
      this.pnlTop.Controls.Add(this.lblLeftBranch);
      this.pnlTop.Controls.Add(this.lblLeftStem);
      this.pnlTop.Controls.Add(this.lblIdenticalPathWarning);
      this.pnlTop.Controls.Add(this.lblLeftPath);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 25);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1830, 234);
      this.pnlTop.TabIndex = 1;
      //
      // pbRightPath
      //
      this.pbRightPath.Location = new System.Drawing.Point(1542, 102);
      this.pbRightPath.Name = "pbRightPath";
      this.pbRightPath.Size = new System.Drawing.Size(24, 24);
      this.pbRightPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbRightPath.TabIndex = 5;
      this.pbRightPath.TabStop = false;
      this.pbRightPath.MouseHover += new System.EventHandler(this.pbRightPath_MouseHover);
      //
      // pbLeftPath
      //
      this.pbLeftPath.Location = new System.Drawing.Point(1542, 38);
      this.pbLeftPath.Name = "pbLeftPath";
      this.pbLeftPath.Size = new System.Drawing.Size(24, 24);
      this.pbLeftPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbLeftPath.TabIndex = 5;
      this.pbLeftPath.TabStop = false;
      this.pbLeftPath.MouseHover += new System.EventHandler(this.pbLeftPath_MouseHover);
      //
      // cboRightStem
      //
      this.cboRightStem.FormattingEnabled = true;
      this.cboRightStem.Location = new System.Drawing.Point(20, 102);
      this.cboRightStem.Name = "cboRightStem";
      this.cboRightStem.Size = new System.Drawing.Size(409, 28);
      this.cboRightStem.TabIndex = 5;
      this.cboRightStem.TextChanged += new System.EventHandler(this.cboRightStem_TextChanged);
      //
      // cboRightBranch
      //
      this.cboRightBranch.FormattingEnabled = true;
      this.cboRightBranch.Location = new System.Drawing.Point(450, 102);
      this.cboRightBranch.Name = "cboRightBranch";
      this.cboRightBranch.Size = new System.Drawing.Size(317, 28);
      this.cboRightBranch.TabIndex = 6;
      this.cboRightBranch.TextChanged += new System.EventHandler(this.cboRightBranch_TextChanged);
      //
      // cboLeftBranch
      //
      this.cboLeftBranch.FormattingEnabled = true;
      this.cboLeftBranch.Location = new System.Drawing.Point(450, 38);
      this.cboLeftBranch.Name = "cboLeftBranch";
      this.cboLeftBranch.Size = new System.Drawing.Size(317, 28);
      this.cboLeftBranch.TabIndex = 2;
      this.cboLeftBranch.TextChanged += new System.EventHandler(this.cboLeftBranch_TextChanged);
      //
      // cboLeftStem
      //
      this.cboLeftStem.FormattingEnabled = true;
      this.cboLeftStem.Location = new System.Drawing.Point(20, 38);
      this.cboLeftStem.Name = "cboLeftStem";
      this.cboLeftStem.Size = new System.Drawing.Size(409, 28);
      this.cboLeftStem.TabIndex = 1;
      this.cboLeftStem.TextChanged += new System.EventHandler(this.cboLeftStem_TextChanged);
      //
      // cboRightPath
      //
      this.cboRightPath.FormattingEnabled = true;
      this.cboRightPath.Location = new System.Drawing.Point(789, 102);
      this.cboRightPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboRightPath.Name = "cboRightPath";
      this.cboRightPath.Size = new System.Drawing.Size(742, 28);
      this.cboRightPath.TabIndex = 7;
      this.cboRightPath.TextChanged += new System.EventHandler(this.cboRightPath_TextChanged);
      //
      // cboLeftPath
      //
      this.cboLeftPath.FormattingEnabled = true;
      this.cboLeftPath.Location = new System.Drawing.Point(789, 38);
      this.cboLeftPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboLeftPath.Name = "cboLeftPath";
      this.cboLeftPath.Size = new System.Drawing.Size(742, 28);
      this.cboLeftPath.TabIndex = 3;
      this.cboLeftPath.TextChanged += new System.EventHandler(this.cboLeftPath_TextChanged);
      //
      // cboFileTypes
      //
      this.cboFileTypes.FormattingEnabled = true;
      this.cboFileTypes.Location = new System.Drawing.Point(20, 171);
      this.cboFileTypes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboFileTypes.Name = "cboFileTypes";
      this.cboFileTypes.Size = new System.Drawing.Size(409, 28);
      this.cboFileTypes.TabIndex = 10;
      //
      // btnBrowseRight
      //
      this.btnBrowseRight.Location = new System.Drawing.Point(1585, 100);
      this.btnBrowseRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnBrowseRight.Name = "btnBrowseRight";
      this.btnBrowseRight.Size = new System.Drawing.Size(96, 36);
      this.btnBrowseRight.TabIndex = 8;
      this.btnBrowseRight.Tag = "BrowseRight";
      this.btnBrowseRight.Text = "Browse...";
      this.btnBrowseRight.UseVisualStyleBackColor = true;
      this.btnBrowseRight.Click += new System.EventHandler(this.Action);
      //
      // btnBrowseLeft
      //
      this.btnBrowseLeft.Location = new System.Drawing.Point(1585, 37);
      this.btnBrowseLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnBrowseLeft.Name = "btnBrowseLeft";
      this.btnBrowseLeft.Size = new System.Drawing.Size(96, 36);
      this.btnBrowseLeft.TabIndex = 4;
      this.btnBrowseLeft.Tag = "BrowseLeft";
      this.btnBrowseLeft.Text = "Browse...";
      this.btnBrowseLeft.UseVisualStyleBackColor = true;
      this.btnBrowseLeft.Click += new System.EventHandler(this.Action);
      //
      // btnCompare
      //
      this.btnCompare.Location = new System.Drawing.Point(1703, 37);
      this.btnCompare.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnCompare.Name = "btnCompare";
      this.btnCompare.Size = new System.Drawing.Size(185, 97);
      this.btnCompare.TabIndex = 9;
      this.btnCompare.Tag = "Compare";
      this.btnCompare.Text = "Compare";
      this.btnCompare.UseVisualStyleBackColor = true;
      this.btnCompare.Click += new System.EventHandler(this.Action);
      //
      // lblFileTypes
      //
      this.lblFileTypes.AutoSize = true;
      this.lblFileTypes.Location = new System.Drawing.Point(18, 146);
      this.lblFileTypes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblFileTypes.Name = "lblFileTypes";
      this.lblFileTypes.Size = new System.Drawing.Size(80, 20);
      this.lblFileTypes.TabIndex = 1;
      this.lblFileTypes.Text = "File Types";
      //
      // lblRightPath
      //
      this.lblRightPath.AutoSize = true;
      this.lblRightPath.Location = new System.Drawing.Point(785, 80);
      this.lblRightPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblRightPath.Name = "lblRightPath";
      this.lblRightPath.Size = new System.Drawing.Size(84, 20);
      this.lblRightPath.TabIndex = 1;
      this.lblRightPath.Text = "Right Path";
      //
      // lblRightStem
      //
      this.lblRightStem.AutoSize = true;
      this.lblRightStem.Location = new System.Drawing.Point(18, 79);
      this.lblRightStem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblRightStem.Name = "lblRightStem";
      this.lblRightStem.Size = new System.Drawing.Size(89, 20);
      this.lblRightStem.TabIndex = 1;
      this.lblRightStem.Text = "Right Stem";
      //
      // lblRightBranch
      //
      this.lblRightBranch.AutoSize = true;
      this.lblRightBranch.Location = new System.Drawing.Point(446, 79);
      this.lblRightBranch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblRightBranch.Name = "lblRightBranch";
      this.lblRightBranch.Size = new System.Drawing.Size(102, 20);
      this.lblRightBranch.TabIndex = 1;
      this.lblRightBranch.Text = "Right Branch";
      //
      // lblLeftBranch
      //
      this.lblLeftBranch.AutoSize = true;
      this.lblLeftBranch.Location = new System.Drawing.Point(446, 13);
      this.lblLeftBranch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblLeftBranch.Name = "lblLeftBranch";
      this.lblLeftBranch.Size = new System.Drawing.Size(92, 20);
      this.lblLeftBranch.TabIndex = 1;
      this.lblLeftBranch.Text = "Left Branch";
      //
      // lblLeftStem
      //
      this.lblLeftStem.AutoSize = true;
      this.lblLeftStem.Location = new System.Drawing.Point(16, 13);
      this.lblLeftStem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblLeftStem.Name = "lblLeftStem";
      this.lblLeftStem.Size = new System.Drawing.Size(79, 20);
      this.lblLeftStem.TabIndex = 1;
      this.lblLeftStem.Text = "Left Stem";
      //
      // lblLeftPath
      //
      this.lblLeftPath.AutoSize = true;
      this.lblLeftPath.Location = new System.Drawing.Point(785, 13);
      this.lblLeftPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblLeftPath.Name = "lblLeftPath";
      this.lblLeftPath.Size = new System.Drawing.Size(74, 20);
      this.lblLeftPath.TabIndex = 1;
      this.lblLeftPath.Text = "Left Path";
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 1016);
      this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1830, 29);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // ctxMnuResults
      //
      this.ctxMnuResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMnuResultsViewFile,
        this.ctxMnuResultsCompareFiles,
        this.ctxMnuResultsCopyLeftToRight,
        this.ctxMnuResultsCopyRightToLeft,
        this.ctxMnuResultsDeleteLeft,
        this.ctxMnuResultsDeleteRight
      });
      this.ctxMnuResults.Name = "ctxMnuResults";
      this.ctxMnuResults.Size = new System.Drawing.Size(171, 136);
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
      // pnlMain
      //
      this.pnlMain.Controls.Add(this.tabMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 259);
      this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1830, 757);
      this.pnlMain.TabIndex = 4;
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageCompareResults);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(125, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1830, 757);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 20;
      //
      // tabPageCompareResults
      //
      this.tabPageCompareResults.Controls.Add(this.gvResults);
      this.tabPageCompareResults.Location = new System.Drawing.Point(4, 22);
      this.tabPageCompareResults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageCompareResults.Name = "tabPageCompareResults";
      this.tabPageCompareResults.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageCompareResults.Size = new System.Drawing.Size(1822, 731);
      this.tabPageCompareResults.TabIndex = 0;
      this.tabPageCompareResults.Text = "Compare Results";
      this.tabPageCompareResults.UseVisualStyleBackColor = true;
      //
      // gvResults
      //
      this.gvResults.AllowUserToAddRows = false;
      this.gvResults.AllowUserToDeleteRows = false;
      this.gvResults.AllowUserToResizeRows = false;
      this.gvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.gvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvResults.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvResults.Location = new System.Drawing.Point(4, 5);
      this.gvResults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.gvResults.MultiSelect = false;
      this.gvResults.Name = "gvResults";
      this.gvResults.RowHeadersVisible = false;
      this.gvResults.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvResults.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightGreen;
      this.gvResults.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
      this.gvResults.RowTemplate.Height = 19;
      this.gvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvResults.Size = new System.Drawing.Size(1814, 721);
      this.gvResults.TabIndex = 30;
      this.gvResults.Tag = "EditScheduledTask";
      this.gvResults.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvResults_CellMouseClick);
      this.gvResults.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvResults_CellMouseDoubleClick);
      this.gvResults.SelectionChanged += new System.EventHandler(this.gvResults_SelectionChanged);
      //
      // lblIdenticalPathWarning
      //
      this.lblIdenticalPathWarning.AutoSize = true;
      this.lblIdenticalPathWarning.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblIdenticalPathWarning.ForeColor = System.Drawing.Color.Red;
      this.lblIdenticalPathWarning.Location = new System.Drawing.Point(1034, 75);
      this.lblIdenticalPathWarning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblIdenticalPathWarning.Name = "lblIdenticalPathWarning";
      this.lblIdenticalPathWarning.Size = new System.Drawing.Size(233, 14);
      this.lblIdenticalPathWarning.TabIndex = 1;
      this.lblIdenticalPathWarning.Text = "LEFT AND RIGHT PATHS ARE IDENTICAL";
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1830, 1045);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Code Compare";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbRightPath)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbLeftPath)).EndInit();
      this.ctxMnuResults.ResumeLayout(false);
      this.pnlMain.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageCompareResults.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvResults)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnBrowseRight;
    private System.Windows.Forms.Button btnBrowseLeft;
    private System.Windows.Forms.Button btnCompare;
    private System.Windows.Forms.Label lblRightPath;
    private System.Windows.Forms.Label lblLeftPath;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.ComboBox cboFileTypes;
    private System.Windows.Forms.Label lblFileTypes;
    private System.Windows.Forms.ComboBox cboRightPath;
    private System.Windows.Forms.ComboBox cboLeftPath;
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
    private System.Windows.Forms.DataGridView gvResults;
    private System.Windows.Forms.ComboBox cboRightStem;
    private System.Windows.Forms.ComboBox cboLeftStem;
    private System.Windows.Forms.Label lblRightBranch;
    private System.Windows.Forms.Label lblLeftStem;
    private System.Windows.Forms.ComboBox cboRightBranch;
    private System.Windows.Forms.ComboBox cboLeftBranch;
    private System.Windows.Forms.Label lblLeftBranch;
    private System.Windows.Forms.Label lblRightStem;
    private System.Windows.Forms.ToolStripMenuItem mnuFileSaveConfig;
    private System.Windows.Forms.PictureBox pbRightPath;
    private System.Windows.Forms.PictureBox pbLeftPath;
    private System.Windows.Forms.Label lblIdenticalPathWarning;
  }
}

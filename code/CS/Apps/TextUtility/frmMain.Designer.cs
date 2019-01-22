namespace Org.TextUtility
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
      this.lblBaseData = new System.Windows.Forms.Label();
      this.cboBaseData = new System.Windows.Forms.ComboBox();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.cboEnvironment = new System.Windows.Forms.ComboBox();
      this.btnLoadBaseText = new System.Windows.Forms.Button();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageTextLineCompare = new System.Windows.Forms.TabPage();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.splitterLeft = new System.Windows.Forms.SplitContainer();
      this.txtBaseText = new FastColoredTextBoxNS.FastColoredTextBox();
      this.pnlBaseTextTop = new System.Windows.Forms.Panel();
      this.btnSaveBaseTextAs = new System.Windows.Forms.Button();
      this.btnSaveBaseText = new System.Windows.Forms.Button();
      this.lblBaseTextLines = new System.Windows.Forms.Label();
      this.btnClearBaseText = new System.Windows.Forms.Button();
      this.lblBaseText = new System.Windows.Forms.Label();
      this.txtQuery = new FastColoredTextBoxNS.FastColoredTextBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.btnReloadQuery = new System.Windows.Forms.Button();
      this.cboQuery = new System.Windows.Forms.ComboBox();
      this.btnSaveQueryAs = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnRunQuery = new System.Windows.Forms.Button();
      this.lblQueryText = new System.Windows.Forms.Label();
      this.splitterRight = new System.Windows.Forms.SplitContainer();
      this.txtCompareText = new FastColoredTextBoxNS.FastColoredTextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.lblCompareTextLines = new System.Windows.Forms.Label();
      this.btnClearCompareText = new System.Windows.Forms.Button();
      this.btnCompareText = new System.Windows.Forms.Button();
      this.lblCompareText = new System.Windows.Forms.Label();
      this.pnlTextCompareResults = new System.Windows.Forms.Panel();
      this.txtCompareResults = new FastColoredTextBoxNS.FastColoredTextBox();
      this.panel3 = new System.Windows.Forms.Panel();
      this.rbShowMatches = new System.Windows.Forms.RadioButton();
      this.rbShowRight = new System.Windows.Forms.RadioButton();
      this.rbShowLeft = new System.Windows.Forms.RadioButton();
      this.rbShowAll = new System.Windows.Forms.RadioButton();
      this.lblCompareResults = new System.Windows.Forms.Label();
      this.tabPageWellGrid = new System.Windows.Forms.TabPage();
      this.gvWells = new System.Windows.Forms.DataGridView();
      this.pnlWellGridTop = new System.Windows.Forms.Panel();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageTextLineCompare.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterLeft)).BeginInit();
      this.splitterLeft.Panel1.SuspendLayout();
      this.splitterLeft.Panel2.SuspendLayout();
      this.splitterLeft.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtBaseText)).BeginInit();
      this.pnlBaseTextTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtQuery)).BeginInit();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterRight)).BeginInit();
      this.splitterRight.Panel1.SuspendLayout();
      this.splitterRight.Panel2.SuspendLayout();
      this.splitterRight.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtCompareText)).BeginInit();
      this.panel1.SuspendLayout();
      this.pnlTextCompareResults.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtCompareResults)).BeginInit();
      this.panel3.SuspendLayout();
      this.tabPageWellGrid.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvWells)).BeginInit();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(4, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1422, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(4, 746);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1422, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.lblBaseData);
      this.pnlTop.Controls.Add(this.cboBaseData);
      this.pnlTop.Controls.Add(this.lblEnvironment);
      this.pnlTop.Controls.Add(this.cboEnvironment);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1422, 94);
      this.pnlTop.TabIndex = 2;
      // 
      // lblBaseData
      // 
      this.lblBaseData.AutoSize = true;
      this.lblBaseData.Location = new System.Drawing.Point(120, 8);
      this.lblBaseData.Name = "lblBaseData";
      this.lblBaseData.Size = new System.Drawing.Size(57, 13);
      this.lblBaseData.TabIndex = 2;
      this.lblBaseData.Text = "Base Data";
      // 
      // cboBaseData
      // 
      this.cboBaseData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboBaseData.FormattingEnabled = true;
      this.cboBaseData.Items.AddRange(new object[] {
            "Prod",
            "Test"});
      this.cboBaseData.Location = new System.Drawing.Point(123, 24);
      this.cboBaseData.Name = "cboBaseData";
      this.cboBaseData.Size = new System.Drawing.Size(304, 21);
      this.cboBaseData.TabIndex = 1;
      this.cboBaseData.SelectedIndexChanged += new System.EventHandler(this.cboBaseData_SelectedIndexChanged);
      // 
      // lblEnvironment
      // 
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(16, 8);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(66, 13);
      this.lblEnvironment.TabIndex = 2;
      this.lblEnvironment.Text = "Environment";
      // 
      // cboEnvironment
      // 
      this.cboEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboEnvironment.FormattingEnabled = true;
      this.cboEnvironment.Items.AddRange(new object[] {
            "Prod",
            "Test"});
      this.cboEnvironment.Location = new System.Drawing.Point(19, 24);
      this.cboEnvironment.Name = "cboEnvironment";
      this.cboEnvironment.Size = new System.Drawing.Size(94, 21);
      this.cboEnvironment.TabIndex = 1;
      this.cboEnvironment.SelectedIndexChanged += new System.EventHandler(this.cboEnvironment_SelectedIndexChanged);
      // 
      // btnLoadBaseText
      // 
      this.btnLoadBaseText.Location = new System.Drawing.Point(70, 5);
      this.btnLoadBaseText.Name = "btnLoadBaseText";
      this.btnLoadBaseText.Size = new System.Drawing.Size(59, 23);
      this.btnLoadBaseText.TabIndex = 0;
      this.btnLoadBaseText.Tag = "LoadBaseText";
      this.btnLoadBaseText.Text = "Load";
      this.btnLoadBaseText.UseVisualStyleBackColor = true;
      this.btnLoadBaseText.Click += new System.EventHandler(this.Action);
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.tabMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(4, 118);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1422, 628);
      this.pnlMain.TabIndex = 3;
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageTextLineCompare);
      this.tabMain.Controls.Add(this.tabPageWellGrid);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(125, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Name = "tabMain";
      this.tabMain.Padding = new System.Drawing.Point(0, 0);
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1422, 628);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 0;
      // 
      // tabPageTextLineCompare
      // 
      this.tabPageTextLineCompare.BackColor = System.Drawing.Color.Transparent;
      this.tabPageTextLineCompare.Controls.Add(this.splitterMain);
      this.tabPageTextLineCompare.Location = new System.Drawing.Point(4, 22);
      this.tabPageTextLineCompare.Margin = new System.Windows.Forms.Padding(0);
      this.tabPageTextLineCompare.Name = "tabPageTextLineCompare";
      this.tabPageTextLineCompare.Size = new System.Drawing.Size(1414, 602);
      this.tabPageTextLineCompare.TabIndex = 0;
      this.tabPageTextLineCompare.Text = "Text Line Compare";
      // 
      // splitterMain
      // 
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.Location = new System.Drawing.Point(0, 0);
      this.splitterMain.Margin = new System.Windows.Forms.Padding(0);
      this.splitterMain.Name = "splitterMain";
      // 
      // splitterMain.Panel1
      // 
      this.splitterMain.Panel1.Controls.Add(this.splitterLeft);
      // 
      // splitterMain.Panel2
      // 
      this.splitterMain.Panel2.Controls.Add(this.splitterRight);
      this.splitterMain.Size = new System.Drawing.Size(1414, 602);
      this.splitterMain.SplitterDistance = 737;
      this.splitterMain.SplitterWidth = 3;
      this.splitterMain.TabIndex = 0;
      // 
      // splitterLeft
      // 
      this.splitterLeft.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterLeft.Location = new System.Drawing.Point(0, 0);
      this.splitterLeft.Name = "splitterLeft";
      this.splitterLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitterLeft.Panel1
      // 
      this.splitterLeft.Panel1.Controls.Add(this.txtBaseText);
      this.splitterLeft.Panel1.Controls.Add(this.pnlBaseTextTop);
      // 
      // splitterLeft.Panel2
      // 
      this.splitterLeft.Panel2.Controls.Add(this.txtQuery);
      this.splitterLeft.Panel2.Controls.Add(this.panel2);
      this.splitterLeft.Size = new System.Drawing.Size(737, 602);
      this.splitterLeft.SplitterDistance = 250;
      this.splitterLeft.TabIndex = 0;
      // 
      // txtBaseText
      // 
      this.txtBaseText.AutoCompleteBracketsList = new char[] {
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
      this.txtBaseText.AutoScrollMinSize = new System.Drawing.Size(25, 12);
      this.txtBaseText.BackBrush = null;
      this.txtBaseText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtBaseText.CharHeight = 12;
      this.txtBaseText.CharWidth = 7;
      this.txtBaseText.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtBaseText.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtBaseText.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtBaseText.Font = new System.Drawing.Font("Courier New", 8F);
      this.txtBaseText.ForeColor = System.Drawing.Color.Black;
      this.txtBaseText.IsReplaceMode = false;
      this.txtBaseText.Location = new System.Drawing.Point(0, 32);
      this.txtBaseText.Name = "txtBaseText";
      this.txtBaseText.Paddings = new System.Windows.Forms.Padding(0);
      this.txtBaseText.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtBaseText.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtBaseText.ServiceColors")));
      this.txtBaseText.Size = new System.Drawing.Size(737, 218);
      this.txtBaseText.TabIndex = 6;
      this.txtBaseText.Zoom = 100;
      this.txtBaseText.DoubleClick += new System.EventHandler(this.fctb_DoubleClick);
      // 
      // pnlBaseTextTop
      // 
      this.pnlBaseTextTop.Controls.Add(this.btnSaveBaseTextAs);
      this.pnlBaseTextTop.Controls.Add(this.btnSaveBaseText);
      this.pnlBaseTextTop.Controls.Add(this.lblBaseTextLines);
      this.pnlBaseTextTop.Controls.Add(this.btnClearBaseText);
      this.pnlBaseTextTop.Controls.Add(this.btnLoadBaseText);
      this.pnlBaseTextTop.Controls.Add(this.lblBaseText);
      this.pnlBaseTextTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlBaseTextTop.Location = new System.Drawing.Point(0, 0);
      this.pnlBaseTextTop.Name = "pnlBaseTextTop";
      this.pnlBaseTextTop.Size = new System.Drawing.Size(737, 32);
      this.pnlBaseTextTop.TabIndex = 7;
      // 
      // btnSaveBaseTextAs
      // 
      this.btnSaveBaseTextAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSaveBaseTextAs.Location = new System.Drawing.Point(675, 5);
      this.btnSaveBaseTextAs.Name = "btnSaveBaseTextAs";
      this.btnSaveBaseTextAs.Size = new System.Drawing.Size(59, 23);
      this.btnSaveBaseTextAs.TabIndex = 2;
      this.btnSaveBaseTextAs.Tag = "SaveBaseTextAs";
      this.btnSaveBaseTextAs.Text = "Save As";
      this.btnSaveBaseTextAs.UseVisualStyleBackColor = true;
      this.btnSaveBaseTextAs.Click += new System.EventHandler(this.Action);
      // 
      // btnSaveBaseText
      // 
      this.btnSaveBaseText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSaveBaseText.Location = new System.Drawing.Point(613, 5);
      this.btnSaveBaseText.Name = "btnSaveBaseText";
      this.btnSaveBaseText.Size = new System.Drawing.Size(59, 23);
      this.btnSaveBaseText.TabIndex = 3;
      this.btnSaveBaseText.Tag = "SaveBaseText";
      this.btnSaveBaseText.Text = "Save";
      this.btnSaveBaseText.UseVisualStyleBackColor = true;
      this.btnSaveBaseText.Click += new System.EventHandler(this.Action);
      // 
      // lblBaseTextLines
      // 
      this.lblBaseTextLines.AutoSize = true;
      this.lblBaseTextLines.Location = new System.Drawing.Point(222, 11);
      this.lblBaseTextLines.Name = "lblBaseTextLines";
      this.lblBaseTextLines.Size = new System.Drawing.Size(37, 13);
      this.lblBaseTextLines.TabIndex = 1;
      this.lblBaseTextLines.Text = "0 lines";
      // 
      // btnClearBaseText
      // 
      this.btnClearBaseText.Location = new System.Drawing.Point(132, 5);
      this.btnClearBaseText.Name = "btnClearBaseText";
      this.btnClearBaseText.Size = new System.Drawing.Size(59, 23);
      this.btnClearBaseText.TabIndex = 0;
      this.btnClearBaseText.Tag = "ClearBaseText";
      this.btnClearBaseText.Text = "Clear";
      this.btnClearBaseText.UseVisualStyleBackColor = true;
      this.btnClearBaseText.Click += new System.EventHandler(this.Action);
      // 
      // lblBaseText
      // 
      this.lblBaseText.Dock = System.Windows.Forms.DockStyle.Left;
      this.lblBaseText.Location = new System.Drawing.Point(0, 0);
      this.lblBaseText.Name = "lblBaseText";
      this.lblBaseText.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
      this.lblBaseText.Size = new System.Drawing.Size(70, 32);
      this.lblBaseText.TabIndex = 0;
      this.lblBaseText.Text = "Base Text";
      this.lblBaseText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtQuery
      // 
      this.txtQuery.AutoCompleteBracketsList = new char[] {
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
      this.txtQuery.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtQuery.BackBrush = null;
      this.txtQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtQuery.CharHeight = 13;
      this.txtQuery.CharWidth = 7;
      this.txtQuery.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtQuery.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtQuery.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtQuery.IsReplaceMode = false;
      this.txtQuery.Location = new System.Drawing.Point(0, 32);
      this.txtQuery.Name = "txtQuery";
      this.txtQuery.Paddings = new System.Windows.Forms.Padding(0);
      this.txtQuery.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtQuery.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtQuery.ServiceColors")));
      this.txtQuery.Size = new System.Drawing.Size(737, 316);
      this.txtQuery.TabIndex = 7;
      this.txtQuery.Zoom = 100;
      this.txtQuery.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txtQuery_TextChanged);
      this.txtQuery.DoubleClick += new System.EventHandler(this.fctb_DoubleClick);
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.btnReloadQuery);
      this.panel2.Controls.Add(this.cboQuery);
      this.panel2.Controls.Add(this.btnSaveQueryAs);
      this.panel2.Controls.Add(this.btnSave);
      this.panel2.Controls.Add(this.btnRunQuery);
      this.panel2.Controls.Add(this.lblQueryText);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(737, 32);
      this.panel2.TabIndex = 8;
      // 
      // btnReloadQuery
      // 
      this.btnReloadQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnReloadQuery.Location = new System.Drawing.Point(675, 4);
      this.btnReloadQuery.Name = "btnReloadQuery";
      this.btnReloadQuery.Size = new System.Drawing.Size(59, 23);
      this.btnReloadQuery.TabIndex = 3;
      this.btnReloadQuery.Tag = "LoadQuery";
      this.btnReloadQuery.Text = "Load Query";
      this.btnReloadQuery.UseVisualStyleBackColor = true;
      this.btnReloadQuery.Click += new System.EventHandler(this.Action);
      // 
      // cboQuery
      // 
      this.cboQuery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboQuery.FormattingEnabled = true;
      this.cboQuery.Items.AddRange(new object[] {
            "Prod",
            "Test"});
      this.cboQuery.Location = new System.Drawing.Point(70, 5);
      this.cboQuery.Name = "cboQuery";
      this.cboQuery.Size = new System.Drawing.Size(257, 21);
      this.cboQuery.TabIndex = 2;
      this.cboQuery.SelectedIndexChanged += new System.EventHandler(this.cboQuery_SelectedIndexChanged);
      // 
      // btnSaveQueryAs
      // 
      this.btnSaveQueryAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSaveQueryAs.Location = new System.Drawing.Point(613, 4);
      this.btnSaveQueryAs.Name = "btnSaveQueryAs";
      this.btnSaveQueryAs.Size = new System.Drawing.Size(59, 23);
      this.btnSaveQueryAs.TabIndex = 0;
      this.btnSaveQueryAs.Tag = "SaveQueryAs";
      this.btnSaveQueryAs.Text = "Save As";
      this.btnSaveQueryAs.UseVisualStyleBackColor = true;
      this.btnSaveQueryAs.Click += new System.EventHandler(this.Action);
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(550, 4);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(59, 23);
      this.btnSave.TabIndex = 0;
      this.btnSave.Tag = "SaveQuery";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Action);
      // 
      // btnRunQuery
      // 
      this.btnRunQuery.Location = new System.Drawing.Point(333, 4);
      this.btnRunQuery.Name = "btnRunQuery";
      this.btnRunQuery.Size = new System.Drawing.Size(59, 23);
      this.btnRunQuery.TabIndex = 0;
      this.btnRunQuery.Tag = "RunQuery";
      this.btnRunQuery.Text = "Run Query";
      this.btnRunQuery.UseVisualStyleBackColor = true;
      this.btnRunQuery.Click += new System.EventHandler(this.Action);
      // 
      // lblQueryText
      // 
      this.lblQueryText.Dock = System.Windows.Forms.DockStyle.Left;
      this.lblQueryText.Location = new System.Drawing.Point(0, 0);
      this.lblQueryText.Name = "lblQueryText";
      this.lblQueryText.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
      this.lblQueryText.Size = new System.Drawing.Size(70, 32);
      this.lblQueryText.TabIndex = 0;
      this.lblQueryText.Text = "Query";
      this.lblQueryText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // splitterRight
      // 
      this.splitterRight.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterRight.Location = new System.Drawing.Point(0, 0);
      this.splitterRight.Name = "splitterRight";
      this.splitterRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitterRight.Panel1
      // 
      this.splitterRight.Panel1.Controls.Add(this.txtCompareText);
      this.splitterRight.Panel1.Controls.Add(this.panel1);
      // 
      // splitterRight.Panel2
      // 
      this.splitterRight.Panel2.Controls.Add(this.pnlTextCompareResults);
      this.splitterRight.Size = new System.Drawing.Size(674, 602);
      this.splitterRight.SplitterDistance = 250;
      this.splitterRight.TabIndex = 1;
      // 
      // txtCompareText
      // 
      this.txtCompareText.AutoCompleteBracketsList = new char[] {
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
      this.txtCompareText.AutoScrollMinSize = new System.Drawing.Size(25, 12);
      this.txtCompareText.BackBrush = null;
      this.txtCompareText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtCompareText.CharHeight = 12;
      this.txtCompareText.CharWidth = 7;
      this.txtCompareText.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtCompareText.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtCompareText.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCompareText.Font = new System.Drawing.Font("Courier New", 8F);
      this.txtCompareText.ForeColor = System.Drawing.Color.Black;
      this.txtCompareText.IsReplaceMode = false;
      this.txtCompareText.Location = new System.Drawing.Point(0, 32);
      this.txtCompareText.Name = "txtCompareText";
      this.txtCompareText.Paddings = new System.Windows.Forms.Padding(0);
      this.txtCompareText.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtCompareText.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtCompareText.ServiceColors")));
      this.txtCompareText.Size = new System.Drawing.Size(674, 218);
      this.txtCompareText.TabIndex = 6;
      this.txtCompareText.Zoom = 100;
      this.txtCompareText.DoubleClick += new System.EventHandler(this.fctb_DoubleClick);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.lblCompareTextLines);
      this.panel1.Controls.Add(this.btnClearCompareText);
      this.panel1.Controls.Add(this.btnCompareText);
      this.panel1.Controls.Add(this.lblCompareText);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(674, 32);
      this.panel1.TabIndex = 8;
      // 
      // lblCompareTextLines
      // 
      this.lblCompareTextLines.AutoSize = true;
      this.lblCompareTextLines.Location = new System.Drawing.Point(248, 11);
      this.lblCompareTextLines.Name = "lblCompareTextLines";
      this.lblCompareTextLines.Size = new System.Drawing.Size(37, 13);
      this.lblCompareTextLines.TabIndex = 1;
      this.lblCompareTextLines.Text = "0 lines";
      // 
      // btnClearCompareText
      // 
      this.btnClearCompareText.Location = new System.Drawing.Point(157, 5);
      this.btnClearCompareText.Name = "btnClearCompareText";
      this.btnClearCompareText.Size = new System.Drawing.Size(59, 23);
      this.btnClearCompareText.TabIndex = 0;
      this.btnClearCompareText.Tag = "ClearCompareText";
      this.btnClearCompareText.Text = "Clear";
      this.btnClearCompareText.UseVisualStyleBackColor = true;
      this.btnClearCompareText.Click += new System.EventHandler(this.Action);
      // 
      // btnCompareText
      // 
      this.btnCompareText.Location = new System.Drawing.Point(95, 5);
      this.btnCompareText.Name = "btnCompareText";
      this.btnCompareText.Size = new System.Drawing.Size(59, 23);
      this.btnCompareText.TabIndex = 0;
      this.btnCompareText.Tag = "CompareText";
      this.btnCompareText.Text = "Compare";
      this.btnCompareText.UseVisualStyleBackColor = true;
      this.btnCompareText.Click += new System.EventHandler(this.Action);
      // 
      // lblCompareText
      // 
      this.lblCompareText.Dock = System.Windows.Forms.DockStyle.Left;
      this.lblCompareText.Location = new System.Drawing.Point(0, 0);
      this.lblCompareText.Name = "lblCompareText";
      this.lblCompareText.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
      this.lblCompareText.Size = new System.Drawing.Size(90, 32);
      this.lblCompareText.TabIndex = 0;
      this.lblCompareText.Text = "Compare Text";
      this.lblCompareText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTextCompareResults
      // 
      this.pnlTextCompareResults.Controls.Add(this.txtCompareResults);
      this.pnlTextCompareResults.Controls.Add(this.panel3);
      this.pnlTextCompareResults.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlTextCompareResults.Location = new System.Drawing.Point(0, 0);
      this.pnlTextCompareResults.Name = "pnlTextCompareResults";
      this.pnlTextCompareResults.Size = new System.Drawing.Size(674, 348);
      this.pnlTextCompareResults.TabIndex = 8;
      // 
      // txtCompareResults
      // 
      this.txtCompareResults.AutoCompleteBracketsList = new char[] {
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
      this.txtCompareResults.AutoScrollMinSize = new System.Drawing.Size(25, 12);
      this.txtCompareResults.BackBrush = null;
      this.txtCompareResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtCompareResults.CharHeight = 12;
      this.txtCompareResults.CharWidth = 7;
      this.txtCompareResults.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtCompareResults.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtCompareResults.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCompareResults.Font = new System.Drawing.Font("Courier New", 8F);
      this.txtCompareResults.ForeColor = System.Drawing.Color.Black;
      this.txtCompareResults.IsReplaceMode = false;
      this.txtCompareResults.Location = new System.Drawing.Point(0, 32);
      this.txtCompareResults.Name = "txtCompareResults";
      this.txtCompareResults.Paddings = new System.Windows.Forms.Padding(0);
      this.txtCompareResults.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtCompareResults.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtCompareResults.ServiceColors")));
      this.txtCompareResults.Size = new System.Drawing.Size(674, 316);
      this.txtCompareResults.TabIndex = 10;
      this.txtCompareResults.Zoom = 100;
      this.txtCompareResults.DoubleClick += new System.EventHandler(this.fctb_DoubleClick);
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.rbShowMatches);
      this.panel3.Controls.Add(this.rbShowRight);
      this.panel3.Controls.Add(this.rbShowLeft);
      this.panel3.Controls.Add(this.rbShowAll);
      this.panel3.Controls.Add(this.lblCompareResults);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(674, 32);
      this.panel3.TabIndex = 9;
      // 
      // rbShowMatches
      // 
      this.rbShowMatches.AutoSize = true;
      this.rbShowMatches.Location = new System.Drawing.Point(414, 8);
      this.rbShowMatches.Name = "rbShowMatches";
      this.rbShowMatches.Size = new System.Drawing.Size(96, 17);
      this.rbShowMatches.TabIndex = 1;
      this.rbShowMatches.Text = "Show Matches";
      this.rbShowMatches.UseVisualStyleBackColor = true;
      this.rbShowMatches.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // rbShowRight
      // 
      this.rbShowRight.AutoSize = true;
      this.rbShowRight.Location = new System.Drawing.Point(328, 8);
      this.rbShowRight.Name = "rbShowRight";
      this.rbShowRight.Size = new System.Drawing.Size(80, 17);
      this.rbShowRight.TabIndex = 1;
      this.rbShowRight.Text = "Show Right";
      this.rbShowRight.UseVisualStyleBackColor = true;
      this.rbShowRight.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // rbShowLeft
      // 
      this.rbShowLeft.AutoSize = true;
      this.rbShowLeft.Location = new System.Drawing.Point(249, 8);
      this.rbShowLeft.Name = "rbShowLeft";
      this.rbShowLeft.Size = new System.Drawing.Size(73, 17);
      this.rbShowLeft.TabIndex = 1;
      this.rbShowLeft.Text = "Show Left";
      this.rbShowLeft.UseVisualStyleBackColor = true;
      this.rbShowLeft.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // rbShowAll
      // 
      this.rbShowAll.AutoSize = true;
      this.rbShowAll.Checked = true;
      this.rbShowAll.Location = new System.Drawing.Point(177, 8);
      this.rbShowAll.Name = "rbShowAll";
      this.rbShowAll.Size = new System.Drawing.Size(66, 17);
      this.rbShowAll.TabIndex = 1;
      this.rbShowAll.TabStop = true;
      this.rbShowAll.Text = "Show All";
      this.rbShowAll.UseVisualStyleBackColor = true;
      this.rbShowAll.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // lblCompareResults
      // 
      this.lblCompareResults.Dock = System.Windows.Forms.DockStyle.Left;
      this.lblCompareResults.Location = new System.Drawing.Point(0, 0);
      this.lblCompareResults.Name = "lblCompareResults";
      this.lblCompareResults.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
      this.lblCompareResults.Size = new System.Drawing.Size(106, 32);
      this.lblCompareResults.TabIndex = 0;
      this.lblCompareResults.Text = "Compare Results";
      this.lblCompareResults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tabPageWellGrid
      // 
      this.tabPageWellGrid.Controls.Add(this.gvWells);
      this.tabPageWellGrid.Controls.Add(this.pnlWellGridTop);
      this.tabPageWellGrid.Location = new System.Drawing.Point(4, 22);
      this.tabPageWellGrid.Name = "tabPageWellGrid";
      this.tabPageWellGrid.Size = new System.Drawing.Size(1414, 602);
      this.tabPageWellGrid.TabIndex = 1;
      this.tabPageWellGrid.Text = "Well Grid";
      this.tabPageWellGrid.UseVisualStyleBackColor = true;
      // 
      // gvWells
      // 
      this.gvWells.AllowUserToAddRows = false;
      this.gvWells.AllowUserToDeleteRows = false;
      this.gvWells.AllowUserToResizeRows = false;
      this.gvWells.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.gvWells.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvWells.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvWells.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvWells.Location = new System.Drawing.Point(0, 100);
      this.gvWells.MultiSelect = false;
      this.gvWells.Name = "gvWells";
      this.gvWells.RowHeadersVisible = false;
      this.gvWells.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvWells.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvWells.RowTemplate.Height = 19;
      this.gvWells.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvWells.Size = new System.Drawing.Size(1414, 502);
      this.gvWells.TabIndex = 7;
      this.gvWells.Tag = "EditScheduledTask";
      // 
      // pnlWellGridTop
      // 
      this.pnlWellGridTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlWellGridTop.Location = new System.Drawing.Point(0, 0);
      this.pnlWellGridTop.Name = "pnlWellGridTop";
      this.pnlWellGridTop.Size = new System.Drawing.Size(1414, 100);
      this.pnlWellGridTop.TabIndex = 8;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1430, 769);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Text Utility v1.0.0.0";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageTextLineCompare.ResumeLayout(false);
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      this.splitterLeft.Panel1.ResumeLayout(false);
      this.splitterLeft.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterLeft)).EndInit();
      this.splitterLeft.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtBaseText)).EndInit();
      this.pnlBaseTextTop.ResumeLayout(false);
      this.pnlBaseTextTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtQuery)).EndInit();
      this.panel2.ResumeLayout(false);
      this.splitterRight.Panel1.ResumeLayout(false);
      this.splitterRight.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterRight)).EndInit();
      this.splitterRight.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtCompareText)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.pnlTextCompareResults.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtCompareResults)).EndInit();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.tabPageWellGrid.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvWells)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageTextLineCompare;
    private System.Windows.Forms.TabPage tabPageWellGrid;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.Label lblBaseText;
    private FastColoredTextBoxNS.FastColoredTextBox txtBaseText;
    private System.Windows.Forms.SplitContainer splitterLeft;
    private FastColoredTextBoxNS.FastColoredTextBox txtQuery;
    private System.Windows.Forms.SplitContainer splitterRight;
    private FastColoredTextBoxNS.FastColoredTextBox txtCompareText;
    private System.Windows.Forms.Label lblBaseData;
    private System.Windows.Forms.ComboBox cboBaseData;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.ComboBox cboEnvironment;
    private System.Windows.Forms.Button btnLoadBaseText;
    private System.Windows.Forms.Panel pnlBaseTextTop;
    private System.Windows.Forms.Label lblBaseTextLines;
    private System.Windows.Forms.Button btnClearBaseText;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lblCompareTextLines;
    private System.Windows.Forms.Button btnClearCompareText;
    private System.Windows.Forms.Button btnCompareText;
    private System.Windows.Forms.Label lblCompareText;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.ComboBox cboQuery;
    private System.Windows.Forms.Button btnRunQuery;
    private System.Windows.Forms.Label lblQueryText;
    private System.Windows.Forms.Button btnReloadQuery;
    private System.Windows.Forms.Button btnSaveBaseTextAs;
    private System.Windows.Forms.Button btnSaveBaseText;
    private System.Windows.Forms.Button btnSaveQueryAs;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Panel pnlTextCompareResults;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label lblCompareResults;
    private FastColoredTextBoxNS.FastColoredTextBox txtCompareResults;
    private System.Windows.Forms.RadioButton rbShowMatches;
    private System.Windows.Forms.RadioButton rbShowRight;
    private System.Windows.Forms.RadioButton rbShowLeft;
    private System.Windows.Forms.RadioButton rbShowAll;
    private System.Windows.Forms.DataGridView gvWells;
    private System.Windows.Forms.Panel pnlWellGridTop;
  }
}


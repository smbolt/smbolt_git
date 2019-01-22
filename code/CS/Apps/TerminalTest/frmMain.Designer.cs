namespace Org.TerminalTest
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
      this.pnlMain = new System.Windows.Forms.Panel();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageEditor1 = new System.Windows.Forms.TabPage();
      this.pnlEditor1 = new System.Windows.Forms.Panel();
      this.tabPageControlPanel = new System.Windows.Forms.TabPage();
      this.pnlControlPanel = new System.Windows.Forms.Panel();
      this.lblStatus = new System.Windows.Forms.Label();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuRun = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuRunShowEditor = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.ckShowFields = new System.Windows.Forms.CheckBox();
      this.udCharHeight = new System.Windows.Forms.NumericUpDown();
      this.udCharWidth = new System.Windows.Forms.NumericUpDown();
      this.udFontSize = new System.Windows.Forms.NumericUpDown();
      this.lblScreenLines = new System.Windows.Forms.Label();
      this.lblCurrLine = new System.Windows.Forms.Label();
      this.lblScreenCols = new System.Windows.Forms.Label();
      this.lblCurrCol = new System.Windows.Forms.Label();
      this.lblCurrLth = new System.Windows.Forms.Label();
      this.lblClientSize = new System.Windows.Forms.Label();
      this.lblScreenSize = new System.Windows.Forms.Label();
      this.lblPaddingValue = new System.Windows.Forms.Label();
      this.lblCurrSize = new System.Windows.Forms.Label();
      this.lblCurrLocation = new System.Windows.Forms.Label();
      this.lblOrigLine = new System.Windows.Forms.Label();
      this.lblOrigCol = new System.Windows.Forms.Label();
      this.lblOrigLth = new System.Windows.Forms.Label();
      this.lblOrigSize = new System.Windows.Forms.Label();
      this.lblOrigLocation = new System.Windows.Forms.Label();
      this.lblTagValue = new System.Windows.Forms.Label();
      this.lblControlNameValue = new System.Windows.Forms.Label();
      this.lblLine = new System.Windows.Forms.Label();
      this.lblCol = new System.Windows.Forms.Label();
      this.lblLth = new System.Windows.Forms.Label();
      this.lblPadding = new System.Windows.Forms.Label();
      this.lblClient = new System.Windows.Forms.Label();
      this.lblScreen = new System.Windows.Forms.Label();
      this.lblCurrValue = new System.Windows.Forms.Label();
      this.lblOrigValue = new System.Windows.Forms.Label();
      this.lblValue = new System.Windows.Forms.Label();
      this.lblSize = new System.Windows.Forms.Label();
      this.lblCurrent = new System.Windows.Forms.Label();
      this.lblOriginal = new System.Windows.Forms.Label();
      this.lblLocation = new System.Windows.Forms.Label();
      this.lblFontData = new System.Windows.Forms.Label();
      this.lblControlName = new System.Windows.Forms.Label();
      this.pnlMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageEditor1.SuspendLayout();
      this.tabPageControlPanel.SuspendLayout();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udCharHeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.udCharWidth)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).BeginInit();
      this.SuspendLayout();
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.tabMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 147);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1219, 571);
      this.pnlMain.TabIndex = 4;
      this.pnlMain.Enter += new System.EventHandler(this.pnlMain_Enter);
      this.pnlMain.Leave += new System.EventHandler(this.pnlMain_Leave);
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageEditor1);
      this.tabMain.Controls.Add(this.tabPageControlPanel);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(120, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1219, 571);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 4;
      this.tabMain.Enter += new System.EventHandler(this.tabMain_Enter);
      this.tabMain.Leave += new System.EventHandler(this.tabMain_Leave);
      this.tabMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabMain_MouseDoubleClick);
      // 
      // tabPageEditor1
      // 
      this.tabPageEditor1.Controls.Add(this.pnlEditor1);
      this.tabPageEditor1.Location = new System.Drawing.Point(4, 22);
      this.tabPageEditor1.Name = "tabPageEditor1";
      this.tabPageEditor1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageEditor1.Size = new System.Drawing.Size(1211, 545);
      this.tabPageEditor1.TabIndex = 1;
      this.tabPageEditor1.Tag = "TabPage_Editor1";
      this.tabPageEditor1.Text = "Editor1";
      this.tabPageEditor1.UseVisualStyleBackColor = true;
      this.tabPageEditor1.Enter += new System.EventHandler(this.tabPageControls_Enter);
      this.tabPageEditor1.Leave += new System.EventHandler(this.tabPageControls_Leave);
      // 
      // pnlEditor1
      // 
      this.pnlEditor1.BackColor = System.Drawing.SystemColors.Control;
      this.pnlEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlEditor1.Location = new System.Drawing.Point(3, 3);
      this.pnlEditor1.Margin = new System.Windows.Forms.Padding(0);
      this.pnlEditor1.Name = "pnlEditor1";
      this.pnlEditor1.Size = new System.Drawing.Size(1205, 539);
      this.pnlEditor1.TabIndex = 6;
      this.pnlEditor1.Tag = "DockTarget_Editor1";
      // 
      // tabPageControlPanel
      // 
      this.tabPageControlPanel.Controls.Add(this.pnlControlPanel);
      this.tabPageControlPanel.Location = new System.Drawing.Point(4, 22);
      this.tabPageControlPanel.Name = "tabPageControlPanel";
      this.tabPageControlPanel.Size = new System.Drawing.Size(1211, 567);
      this.tabPageControlPanel.TabIndex = 2;
      this.tabPageControlPanel.Tag = "TabPage_ControlPanel";
      this.tabPageControlPanel.Text = "Control Panel";
      this.tabPageControlPanel.UseVisualStyleBackColor = true;
      // 
      // pnlControlPanel
      // 
      this.pnlControlPanel.BackColor = System.Drawing.SystemColors.Control;
      this.pnlControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlControlPanel.Location = new System.Drawing.Point(0, 0);
      this.pnlControlPanel.Margin = new System.Windows.Forms.Padding(0);
      this.pnlControlPanel.Name = "pnlControlPanel";
      this.pnlControlPanel.Size = new System.Drawing.Size(1211, 567);
      this.pnlControlPanel.TabIndex = 7;
      this.pnlControlPanel.Tag = "DockTarget_ControlPanel";
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 718);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1219, 23);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuRun});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1219, 24);
      this.mnuMain.TabIndex = 5;
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
      // mnuRun
      // 
      this.mnuRun.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRunShowEditor});
      this.mnuRun.Name = "mnuRun";
      this.mnuRun.Size = new System.Drawing.Size(40, 20);
      this.mnuRun.Text = "&Run";
      // 
      // mnuRunShowEditor
      // 
      this.mnuRunShowEditor.Name = "mnuRunShowEditor";
      this.mnuRunShowEditor.Size = new System.Drawing.Size(137, 22);
      this.mnuRunShowEditor.Tag = "ShowEditor";
      this.mnuRunShowEditor.Text = "&Show Editor";
      this.mnuRunShowEditor.Click += new System.EventHandler(this.Action);
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.ckShowFields);
      this.pnlTop.Controls.Add(this.udCharHeight);
      this.pnlTop.Controls.Add(this.udCharWidth);
      this.pnlTop.Controls.Add(this.udFontSize);
      this.pnlTop.Controls.Add(this.lblScreenLines);
      this.pnlTop.Controls.Add(this.lblCurrLine);
      this.pnlTop.Controls.Add(this.lblScreenCols);
      this.pnlTop.Controls.Add(this.lblCurrCol);
      this.pnlTop.Controls.Add(this.lblCurrLth);
      this.pnlTop.Controls.Add(this.lblClientSize);
      this.pnlTop.Controls.Add(this.lblScreenSize);
      this.pnlTop.Controls.Add(this.lblPaddingValue);
      this.pnlTop.Controls.Add(this.lblCurrSize);
      this.pnlTop.Controls.Add(this.lblCurrLocation);
      this.pnlTop.Controls.Add(this.lblOrigLine);
      this.pnlTop.Controls.Add(this.lblOrigCol);
      this.pnlTop.Controls.Add(this.lblOrigLth);
      this.pnlTop.Controls.Add(this.lblOrigSize);
      this.pnlTop.Controls.Add(this.lblOrigLocation);
      this.pnlTop.Controls.Add(this.lblTagValue);
      this.pnlTop.Controls.Add(this.lblControlNameValue);
      this.pnlTop.Controls.Add(this.lblLine);
      this.pnlTop.Controls.Add(this.lblCol);
      this.pnlTop.Controls.Add(this.lblLth);
      this.pnlTop.Controls.Add(this.lblPadding);
      this.pnlTop.Controls.Add(this.lblClient);
      this.pnlTop.Controls.Add(this.lblScreen);
      this.pnlTop.Controls.Add(this.lblCurrValue);
      this.pnlTop.Controls.Add(this.lblOrigValue);
      this.pnlTop.Controls.Add(this.lblValue);
      this.pnlTop.Controls.Add(this.lblSize);
      this.pnlTop.Controls.Add(this.lblCurrent);
      this.pnlTop.Controls.Add(this.lblOriginal);
      this.pnlTop.Controls.Add(this.lblLocation);
      this.pnlTop.Controls.Add(this.lblFontData);
      this.pnlTop.Controls.Add(this.lblControlName);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1219, 123);
      this.pnlTop.TabIndex = 6;
      // 
      // ckShowFields
      // 
      this.ckShowFields.AutoSize = true;
      this.ckShowFields.Location = new System.Drawing.Point(181, 72);
      this.ckShowFields.Name = "ckShowFields";
      this.ckShowFields.Size = new System.Drawing.Size(83, 17);
      this.ckShowFields.TabIndex = 2;
      this.ckShowFields.Text = "Show Fields";
      this.ckShowFields.UseVisualStyleBackColor = true;
      this.ckShowFields.CheckedChanged += new System.EventHandler(this.ckShowFields_CheckedChanged);
      // 
      // udCharHeight
      // 
      this.udCharHeight.Location = new System.Drawing.Point(118, 70);
      this.udCharHeight.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.udCharHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.udCharHeight.Name = "udCharHeight";
      this.udCharHeight.Size = new System.Drawing.Size(44, 20);
      this.udCharHeight.TabIndex = 1;
      this.udCharHeight.Tag = "CharHeight";
      this.udCharHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.udCharHeight.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
      this.udCharHeight.ValueChanged += new System.EventHandler(this.FontMgmtChanged);
      // 
      // udCharWidth
      // 
      this.udCharWidth.Location = new System.Drawing.Point(68, 70);
      this.udCharWidth.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.udCharWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.udCharWidth.Name = "udCharWidth";
      this.udCharWidth.Size = new System.Drawing.Size(44, 20);
      this.udCharWidth.TabIndex = 1;
      this.udCharWidth.Tag = "CharWidth";
      this.udCharWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.udCharWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.udCharWidth.ValueChanged += new System.EventHandler(this.FontMgmtChanged);
      // 
      // udFontSize
      // 
      this.udFontSize.Location = new System.Drawing.Point(19, 70);
      this.udFontSize.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
      this.udFontSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
      this.udFontSize.Name = "udFontSize";
      this.udFontSize.Size = new System.Drawing.Size(44, 20);
      this.udFontSize.TabIndex = 1;
      this.udFontSize.Tag = "FontSize";
      this.udFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.udFontSize.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
      this.udFontSize.ValueChanged += new System.EventHandler(this.FontMgmtChanged);
      // 
      // lblScreenLines
      // 
      this.lblScreenLines.AutoSize = true;
      this.lblScreenLines.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblScreenLines.Location = new System.Drawing.Point(310, 54);
      this.lblScreenLines.Name = "lblScreenLines";
      this.lblScreenLines.Size = new System.Drawing.Size(25, 13);
      this.lblScreenLines.TabIndex = 0;
      this.lblScreenLines.Text = "000";
      // 
      // lblCurrLine
      // 
      this.lblCurrLine.AutoSize = true;
      this.lblCurrLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrLine.Location = new System.Drawing.Point(310, 34);
      this.lblCurrLine.Name = "lblCurrLine";
      this.lblCurrLine.Size = new System.Drawing.Size(25, 13);
      this.lblCurrLine.TabIndex = 0;
      this.lblCurrLine.Text = "000";
      // 
      // lblScreenCols
      // 
      this.lblScreenCols.AutoSize = true;
      this.lblScreenCols.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblScreenCols.Location = new System.Drawing.Point(342, 54);
      this.lblScreenCols.Name = "lblScreenCols";
      this.lblScreenCols.Size = new System.Drawing.Size(25, 13);
      this.lblScreenCols.TabIndex = 0;
      this.lblScreenCols.Text = "000";
      // 
      // lblCurrCol
      // 
      this.lblCurrCol.AutoSize = true;
      this.lblCurrCol.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrCol.Location = new System.Drawing.Point(342, 34);
      this.lblCurrCol.Name = "lblCurrCol";
      this.lblCurrCol.Size = new System.Drawing.Size(25, 13);
      this.lblCurrCol.TabIndex = 0;
      this.lblCurrCol.Text = "000";
      // 
      // lblCurrLth
      // 
      this.lblCurrLth.AutoSize = true;
      this.lblCurrLth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrLth.Location = new System.Drawing.Point(374, 34);
      this.lblCurrLth.Name = "lblCurrLth";
      this.lblCurrLth.Size = new System.Drawing.Size(25, 13);
      this.lblCurrLth.TabIndex = 0;
      this.lblCurrLth.Text = "000";
      // 
      // lblClientSize
      // 
      this.lblClientSize.AutoSize = true;
      this.lblClientSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblClientSize.Location = new System.Drawing.Point(635, 54);
      this.lblClientSize.Name = "lblClientSize";
      this.lblClientSize.Size = new System.Drawing.Size(86, 13);
      this.lblClientSize.TabIndex = 0;
      this.lblClientSize.Text = "W:0000  H:0000";
      // 
      // lblScreenSize
      // 
      this.lblScreenSize.AutoSize = true;
      this.lblScreenSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblScreenSize.Location = new System.Drawing.Point(222, 54);
      this.lblScreenSize.Name = "lblScreenSize";
      this.lblScreenSize.Size = new System.Drawing.Size(86, 13);
      this.lblScreenSize.TabIndex = 0;
      this.lblScreenSize.Text = "W:0000  H:0000";
      // 
      // lblPaddingValue
      // 
      this.lblPaddingValue.AutoSize = true;
      this.lblPaddingValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPaddingValue.Location = new System.Drawing.Point(457, 54);
      this.lblPaddingValue.Name = "lblPaddingValue";
      this.lblPaddingValue.Size = new System.Drawing.Size(113, 13);
      this.lblPaddingValue.TabIndex = 0;
      this.lblPaddingValue.Text = "T:00  R:00  B:00  L:00";
      // 
      // lblCurrSize
      // 
      this.lblCurrSize.AutoSize = true;
      this.lblCurrSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrSize.Location = new System.Drawing.Point(414, 34);
      this.lblCurrSize.Name = "lblCurrSize";
      this.lblCurrSize.Size = new System.Drawing.Size(86, 13);
      this.lblCurrSize.TabIndex = 0;
      this.lblCurrSize.Text = "W:0000  H:0000";
      // 
      // lblCurrLocation
      // 
      this.lblCurrLocation.AutoSize = true;
      this.lblCurrLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrLocation.Location = new System.Drawing.Point(225, 34);
      this.lblCurrLocation.Name = "lblCurrLocation";
      this.lblCurrLocation.Size = new System.Drawing.Size(81, 13);
      this.lblCurrLocation.TabIndex = 0;
      this.lblCurrLocation.Text = "X:0000  Y:0000";
      // 
      // lblOrigLine
      // 
      this.lblOrigLine.AutoSize = true;
      this.lblOrigLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigLine.Location = new System.Drawing.Point(310, 19);
      this.lblOrigLine.Name = "lblOrigLine";
      this.lblOrigLine.Size = new System.Drawing.Size(25, 13);
      this.lblOrigLine.TabIndex = 0;
      this.lblOrigLine.Text = "000";
      // 
      // lblOrigCol
      // 
      this.lblOrigCol.AutoSize = true;
      this.lblOrigCol.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigCol.Location = new System.Drawing.Point(342, 19);
      this.lblOrigCol.Name = "lblOrigCol";
      this.lblOrigCol.Size = new System.Drawing.Size(25, 13);
      this.lblOrigCol.TabIndex = 0;
      this.lblOrigCol.Text = "000";
      // 
      // lblOrigLth
      // 
      this.lblOrigLth.AutoSize = true;
      this.lblOrigLth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigLth.Location = new System.Drawing.Point(374, 19);
      this.lblOrigLth.Name = "lblOrigLth";
      this.lblOrigLth.Size = new System.Drawing.Size(25, 13);
      this.lblOrigLth.TabIndex = 0;
      this.lblOrigLth.Text = "000";
      // 
      // lblOrigSize
      // 
      this.lblOrigSize.AutoSize = true;
      this.lblOrigSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigSize.Location = new System.Drawing.Point(414, 19);
      this.lblOrigSize.Name = "lblOrigSize";
      this.lblOrigSize.Size = new System.Drawing.Size(86, 13);
      this.lblOrigSize.TabIndex = 0;
      this.lblOrigSize.Text = "W:0000  H:0000";
      // 
      // lblOrigLocation
      // 
      this.lblOrigLocation.AutoSize = true;
      this.lblOrigLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigLocation.Location = new System.Drawing.Point(225, 19);
      this.lblOrigLocation.Name = "lblOrigLocation";
      this.lblOrigLocation.Size = new System.Drawing.Size(81, 13);
      this.lblOrigLocation.TabIndex = 0;
      this.lblOrigLocation.Text = "X:0000  Y:0000";
      // 
      // lblTagValue
      // 
      this.lblTagValue.AutoSize = true;
      this.lblTagValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTagValue.Location = new System.Drawing.Point(16, 32);
      this.lblTagValue.Name = "lblTagValue";
      this.lblTagValue.Size = new System.Drawing.Size(25, 13);
      this.lblTagValue.TabIndex = 0;
      this.lblTagValue.Text = "Tag";
      // 
      // lblControlNameValue
      // 
      this.lblControlNameValue.AutoSize = true;
      this.lblControlNameValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblControlNameValue.Location = new System.Drawing.Point(16, 19);
      this.lblControlNameValue.Name = "lblControlNameValue";
      this.lblControlNameValue.Size = new System.Drawing.Size(33, 13);
      this.lblControlNameValue.TabIndex = 0;
      this.lblControlNameValue.Text = "Value";
      // 
      // lblLine
      // 
      this.lblLine.AutoSize = true;
      this.lblLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLine.Location = new System.Drawing.Point(310, 4);
      this.lblLine.Name = "lblLine";
      this.lblLine.Size = new System.Drawing.Size(26, 13);
      this.lblLine.TabIndex = 0;
      this.lblLine.Text = "Line";
      // 
      // lblCol
      // 
      this.lblCol.AutoSize = true;
      this.lblCol.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCol.Location = new System.Drawing.Point(343, 4);
      this.lblCol.Name = "lblCol";
      this.lblCol.Size = new System.Drawing.Size(22, 13);
      this.lblCol.TabIndex = 0;
      this.lblCol.Text = "Col";
      // 
      // lblLth
      // 
      this.lblLth.AutoSize = true;
      this.lblLth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLth.Location = new System.Drawing.Point(374, 4);
      this.lblLth.Name = "lblLth";
      this.lblLth.Size = new System.Drawing.Size(22, 13);
      this.lblLth.TabIndex = 0;
      this.lblLth.Text = "Lth";
      // 
      // lblPadding
      // 
      this.lblPadding.AutoSize = true;
      this.lblPadding.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPadding.Location = new System.Drawing.Point(414, 54);
      this.lblPadding.Name = "lblPadding";
      this.lblPadding.Size = new System.Drawing.Size(45, 13);
      this.lblPadding.TabIndex = 0;
      this.lblPadding.Text = "Padding";
      // 
      // lblClient
      // 
      this.lblClient.AutoSize = true;
      this.lblClient.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblClient.Location = new System.Drawing.Point(579, 54);
      this.lblClient.Name = "lblClient";
      this.lblClient.Size = new System.Drawing.Size(60, 13);
      this.lblClient.TabIndex = 0;
      this.lblClient.Text = "Client Size:";
      // 
      // lblScreen
      // 
      this.lblScreen.AutoSize = true;
      this.lblScreen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblScreen.Location = new System.Drawing.Point(178, 54);
      this.lblScreen.Name = "lblScreen";
      this.lblScreen.Size = new System.Drawing.Size(40, 13);
      this.lblScreen.TabIndex = 0;
      this.lblScreen.Text = "Screen";
      // 
      // lblCurrValue
      // 
      this.lblCurrValue.AutoSize = true;
      this.lblCurrValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrValue.Location = new System.Drawing.Point(529, 34);
      this.lblCurrValue.Name = "lblCurrValue";
      this.lblCurrValue.Size = new System.Drawing.Size(88, 13);
      this.lblCurrValue.TabIndex = 0;
      this.lblCurrValue.Text = "CURRENT VALUE";
      // 
      // lblOrigValue
      // 
      this.lblOrigValue.AutoSize = true;
      this.lblOrigValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigValue.Location = new System.Drawing.Point(529, 19);
      this.lblOrigValue.Name = "lblOrigValue";
      this.lblOrigValue.Size = new System.Drawing.Size(90, 13);
      this.lblOrigValue.TabIndex = 0;
      this.lblOrigValue.Text = "ORIGINAL VALUE";
      // 
      // lblValue
      // 
      this.lblValue.AutoSize = true;
      this.lblValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblValue.Location = new System.Drawing.Point(529, 4);
      this.lblValue.Name = "lblValue";
      this.lblValue.Size = new System.Drawing.Size(33, 13);
      this.lblValue.TabIndex = 0;
      this.lblValue.Text = "Value";
      // 
      // lblSize
      // 
      this.lblSize.AutoSize = true;
      this.lblSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSize.Location = new System.Drawing.Point(439, 4);
      this.lblSize.Name = "lblSize";
      this.lblSize.Size = new System.Drawing.Size(26, 13);
      this.lblSize.TabIndex = 0;
      this.lblSize.Text = "Size";
      // 
      // lblCurrent
      // 
      this.lblCurrent.AutoSize = true;
      this.lblCurrent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrent.Location = new System.Drawing.Point(178, 34);
      this.lblCurrent.Name = "lblCurrent";
      this.lblCurrent.Size = new System.Drawing.Size(48, 13);
      this.lblCurrent.TabIndex = 0;
      this.lblCurrent.Text = "Current:";
      // 
      // lblOriginal
      // 
      this.lblOriginal.AutoSize = true;
      this.lblOriginal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOriginal.Location = new System.Drawing.Point(178, 19);
      this.lblOriginal.Name = "lblOriginal";
      this.lblOriginal.Size = new System.Drawing.Size(47, 13);
      this.lblOriginal.TabIndex = 0;
      this.lblOriginal.Text = "Original:";
      // 
      // lblLocation
      // 
      this.lblLocation.AutoSize = true;
      this.lblLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLocation.Location = new System.Drawing.Point(240, 4);
      this.lblLocation.Name = "lblLocation";
      this.lblLocation.Size = new System.Drawing.Size(47, 13);
      this.lblLocation.TabIndex = 0;
      this.lblLocation.Text = "Location";
      // 
      // lblFontData
      // 
      this.lblFontData.AutoSize = true;
      this.lblFontData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFontData.Location = new System.Drawing.Point(12, 54);
      this.lblFontData.Name = "lblFontData";
      this.lblFontData.Size = new System.Drawing.Size(143, 13);
      this.lblFontData.TabIndex = 0;
      this.lblFontData.Text = "Font Size    Width       Height";
      // 
      // lblControlName
      // 
      this.lblControlName.AutoSize = true;
      this.lblControlName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblControlName.Location = new System.Drawing.Point(12, 4);
      this.lblControlName.Name = "lblControlName";
      this.lblControlName.Size = new System.Drawing.Size(100, 13);
      this.lblControlName.TabIndex = 0;
      this.lblControlName.Text = "Control Name / Tag";
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1219, 741);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Tag = "MainForm";
      this.Text = "Editor Test";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.ResizeEnd += new System.EventHandler(this.frmMain_ResizeEnd);
      this.LocationChanged += new System.EventHandler(this.frmMain_LocationChanged);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMain_KeyPress);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
      this.pnlMain.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageEditor1.ResumeLayout(false);
      this.tabPageControlPanel.ResumeLayout(false);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udCharHeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.udCharWidth)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageEditor1;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.ToolStripMenuItem mnuRun;
    private System.Windows.Forms.ToolStripMenuItem mnuRunShowEditor;
    private System.Windows.Forms.Panel pnlEditor1;
    private System.Windows.Forms.TabPage tabPageControlPanel;
    private System.Windows.Forms.Panel pnlControlPanel;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblControlName;
    private System.Windows.Forms.Label lblCurrLocation;
    private System.Windows.Forms.Label lblOrigLocation;
    private System.Windows.Forms.Label lblControlNameValue;
    private System.Windows.Forms.Label lblSize;
    private System.Windows.Forms.Label lblCurrent;
    private System.Windows.Forms.Label lblOriginal;
    private System.Windows.Forms.Label lblLocation;
    private System.Windows.Forms.Label lblCurrLine;
    private System.Windows.Forms.Label lblCurrCol;
    private System.Windows.Forms.Label lblCurrLth;
    private System.Windows.Forms.Label lblCurrSize;
    private System.Windows.Forms.Label lblOrigLine;
    private System.Windows.Forms.Label lblOrigCol;
    private System.Windows.Forms.Label lblOrigLth;
    private System.Windows.Forms.Label lblOrigSize;
    private System.Windows.Forms.Label lblLine;
    private System.Windows.Forms.Label lblCol;
    private System.Windows.Forms.Label lblLth;
    private System.Windows.Forms.Label lblCurrValue;
    private System.Windows.Forms.Label lblOrigValue;
    private System.Windows.Forms.Label lblValue;
    private System.Windows.Forms.Label lblTagValue;
    private System.Windows.Forms.Label lblScreenLines;
    private System.Windows.Forms.Label lblScreenCols;
    private System.Windows.Forms.Label lblClientSize;
    private System.Windows.Forms.Label lblScreenSize;
    private System.Windows.Forms.Label lblPaddingValue;
    private System.Windows.Forms.Label lblPadding;
    private System.Windows.Forms.Label lblClient;
    private System.Windows.Forms.Label lblScreen;
    private System.Windows.Forms.NumericUpDown udCharHeight;
    private System.Windows.Forms.NumericUpDown udCharWidth;
    private System.Windows.Forms.NumericUpDown udFontSize;
    private System.Windows.Forms.Label lblFontData;
    private System.Windows.Forms.CheckBox ckShowFields;
  }
}


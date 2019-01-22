namespace Org.EditorToolWindows
{
  partial class ControlPanel
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.pnlTop = new System.Windows.Forms.Panel();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageScreenManagement = new System.Windows.Forms.TabPage();
      this.gvFields = new System.Windows.Forms.DataGridView();
      this.lblScreenFields = new System.Windows.Forms.Label();
      this.lblScreenName = new System.Windows.Forms.Label();
      this.cboScreenName = new System.Windows.Forms.ComboBox();
      this.tabPageControlDetail = new System.Windows.Forms.TabPage();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileClose = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlMain = new System.Windows.Forms.Panel();
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
      this.tabMain.SuspendLayout();
      this.tabPageScreenManagement.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvFields)).BeginInit();
      this.tabPageControlDetail.SuspendLayout();
      this.mnuMain.SuspendLayout();
      this.pnlMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udCharHeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.udCharWidth)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).BeginInit();
      this.SuspendLayout();
      // 
      // pnlTop
      // 
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 50);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(736, 31);
      this.pnlTop.TabIndex = 2;
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageScreenManagement);
      this.tabMain.Controls.Add(this.tabPageControlDetail);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(130, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 6);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(736, 546);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 3;
      // 
      // tabPageScreenManagement
      // 
      this.tabPageScreenManagement.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageScreenManagement.Controls.Add(this.gvFields);
      this.tabPageScreenManagement.Controls.Add(this.lblScreenFields);
      this.tabPageScreenManagement.Controls.Add(this.lblScreenName);
      this.tabPageScreenManagement.Controls.Add(this.cboScreenName);
      this.tabPageScreenManagement.Location = new System.Drawing.Point(4, 22);
      this.tabPageScreenManagement.Name = "tabPageScreenManagement";
      this.tabPageScreenManagement.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageScreenManagement.Size = new System.Drawing.Size(728, 446);
      this.tabPageScreenManagement.TabIndex = 0;
      this.tabPageScreenManagement.Text = "Screen Management";
      // 
      // gvFields
      // 
      this.gvFields.AllowUserToAddRows = false;
      this.gvFields.AllowUserToDeleteRows = false;
      this.gvFields.AllowUserToResizeRows = false;
      this.gvFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvFields.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvFields.Location = new System.Drawing.Point(23, 87);
      this.gvFields.MultiSelect = false;
      this.gvFields.Name = "gvFields";
      this.gvFields.RowHeadersVisible = false;
      this.gvFields.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvFields.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DodgerBlue;
      this.gvFields.RowTemplate.Height = 19;
      this.gvFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvFields.Size = new System.Drawing.Size(684, 222);
      this.gvFields.TabIndex = 6;
      // 
      // lblScreenFields
      // 
      this.lblScreenFields.AutoSize = true;
      this.lblScreenFields.Location = new System.Drawing.Point(20, 71);
      this.lblScreenFields.Name = "lblScreenFields";
      this.lblScreenFields.Size = new System.Drawing.Size(71, 13);
      this.lblScreenFields.TabIndex = 1;
      this.lblScreenFields.Text = "Screen Fields";
      // 
      // lblScreenName
      // 
      this.lblScreenName.AutoSize = true;
      this.lblScreenName.Location = new System.Drawing.Point(20, 17);
      this.lblScreenName.Name = "lblScreenName";
      this.lblScreenName.Size = new System.Drawing.Size(105, 13);
      this.lblScreenName.TabIndex = 1;
      this.lblScreenName.Text = "Select Screen Name";
      // 
      // cboScreenName
      // 
      this.cboScreenName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboScreenName.FormattingEnabled = true;
      this.cboScreenName.Location = new System.Drawing.Point(23, 33);
      this.cboScreenName.Name = "cboScreenName";
      this.cboScreenName.Size = new System.Drawing.Size(259, 21);
      this.cboScreenName.TabIndex = 0;
      // 
      // tabPageControlDetail
      // 
      this.tabPageControlDetail.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageControlDetail.Controls.Add(this.ckShowFields);
      this.tabPageControlDetail.Controls.Add(this.udCharHeight);
      this.tabPageControlDetail.Controls.Add(this.udCharWidth);
      this.tabPageControlDetail.Controls.Add(this.udFontSize);
      this.tabPageControlDetail.Controls.Add(this.lblScreenLines);
      this.tabPageControlDetail.Controls.Add(this.lblCurrLine);
      this.tabPageControlDetail.Controls.Add(this.lblScreenCols);
      this.tabPageControlDetail.Controls.Add(this.lblCurrCol);
      this.tabPageControlDetail.Controls.Add(this.lblCurrLth);
      this.tabPageControlDetail.Controls.Add(this.lblClientSize);
      this.tabPageControlDetail.Controls.Add(this.lblScreenSize);
      this.tabPageControlDetail.Controls.Add(this.lblPaddingValue);
      this.tabPageControlDetail.Controls.Add(this.lblCurrSize);
      this.tabPageControlDetail.Controls.Add(this.lblCurrLocation);
      this.tabPageControlDetail.Controls.Add(this.lblOrigLine);
      this.tabPageControlDetail.Controls.Add(this.lblOrigCol);
      this.tabPageControlDetail.Controls.Add(this.lblOrigLth);
      this.tabPageControlDetail.Controls.Add(this.lblOrigSize);
      this.tabPageControlDetail.Controls.Add(this.lblOrigLocation);
      this.tabPageControlDetail.Controls.Add(this.lblTagValue);
      this.tabPageControlDetail.Controls.Add(this.lblControlNameValue);
      this.tabPageControlDetail.Controls.Add(this.lblLine);
      this.tabPageControlDetail.Controls.Add(this.lblCol);
      this.tabPageControlDetail.Controls.Add(this.lblLth);
      this.tabPageControlDetail.Controls.Add(this.lblPadding);
      this.tabPageControlDetail.Controls.Add(this.lblClient);
      this.tabPageControlDetail.Controls.Add(this.lblScreen);
      this.tabPageControlDetail.Controls.Add(this.lblCurrValue);
      this.tabPageControlDetail.Controls.Add(this.lblOrigValue);
      this.tabPageControlDetail.Controls.Add(this.lblValue);
      this.tabPageControlDetail.Controls.Add(this.lblSize);
      this.tabPageControlDetail.Controls.Add(this.lblCurrent);
      this.tabPageControlDetail.Controls.Add(this.lblOriginal);
      this.tabPageControlDetail.Controls.Add(this.lblLocation);
      this.tabPageControlDetail.Controls.Add(this.lblFontData);
      this.tabPageControlDetail.Controls.Add(this.lblControlName);
      this.tabPageControlDetail.Location = new System.Drawing.Point(4, 22);
      this.tabPageControlDetail.Name = "tabPageControlDetail";
      this.tabPageControlDetail.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageControlDetail.Size = new System.Drawing.Size(728, 520);
      this.tabPageControlDetail.TabIndex = 1;
      this.tabPageControlDetail.Text = "Control Detail";
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 26);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(736, 24);
      this.mnuMain.TabIndex = 4;
      this.mnuMain.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileClose});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "&File";
      // 
      // mnuFileClose
      // 
      this.mnuFileClose.Name = "mnuFileClose";
      this.mnuFileClose.Size = new System.Drawing.Size(103, 22);
      this.mnuFileClose.Text = "&Close";
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.tabMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 81);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
      this.pnlMain.Size = new System.Drawing.Size(736, 552);
      this.pnlMain.TabIndex = 5;
      // 
      // ckShowFields
      // 
      this.ckShowFields.AutoSize = true;
      this.ckShowFields.Location = new System.Drawing.Point(184, 81);
      this.ckShowFields.Name = "ckShowFields";
      this.ckShowFields.Size = new System.Drawing.Size(83, 17);
      this.ckShowFields.TabIndex = 74;
      this.ckShowFields.Text = "Show Fields";
      this.ckShowFields.UseVisualStyleBackColor = true;
      // 
      // udCharHeight
      // 
      this.udCharHeight.Location = new System.Drawing.Point(121, 79);
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
      this.udCharHeight.TabIndex = 73;
      this.udCharHeight.Tag = "CharHeight";
      this.udCharHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.udCharHeight.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
      // 
      // udCharWidth
      // 
      this.udCharWidth.Location = new System.Drawing.Point(71, 79);
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
      this.udCharWidth.TabIndex = 72;
      this.udCharWidth.Tag = "CharWidth";
      this.udCharWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.udCharWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      // 
      // udFontSize
      // 
      this.udFontSize.Location = new System.Drawing.Point(22, 79);
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
      this.udFontSize.TabIndex = 71;
      this.udFontSize.Tag = "FontSize";
      this.udFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.udFontSize.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
      // 
      // lblScreenLines
      // 
      this.lblScreenLines.AutoSize = true;
      this.lblScreenLines.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblScreenLines.Location = new System.Drawing.Point(313, 63);
      this.lblScreenLines.Name = "lblScreenLines";
      this.lblScreenLines.Size = new System.Drawing.Size(25, 13);
      this.lblScreenLines.TabIndex = 58;
      this.lblScreenLines.Text = "000";
      // 
      // lblCurrLine
      // 
      this.lblCurrLine.AutoSize = true;
      this.lblCurrLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrLine.Location = new System.Drawing.Point(313, 43);
      this.lblCurrLine.Name = "lblCurrLine";
      this.lblCurrLine.Size = new System.Drawing.Size(25, 13);
      this.lblCurrLine.TabIndex = 59;
      this.lblCurrLine.Text = "000";
      // 
      // lblScreenCols
      // 
      this.lblScreenCols.AutoSize = true;
      this.lblScreenCols.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblScreenCols.Location = new System.Drawing.Point(345, 63);
      this.lblScreenCols.Name = "lblScreenCols";
      this.lblScreenCols.Size = new System.Drawing.Size(25, 13);
      this.lblScreenCols.TabIndex = 60;
      this.lblScreenCols.Text = "000";
      // 
      // lblCurrCol
      // 
      this.lblCurrCol.AutoSize = true;
      this.lblCurrCol.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrCol.Location = new System.Drawing.Point(345, 43);
      this.lblCurrCol.Name = "lblCurrCol";
      this.lblCurrCol.Size = new System.Drawing.Size(25, 13);
      this.lblCurrCol.TabIndex = 61;
      this.lblCurrCol.Text = "000";
      // 
      // lblCurrLth
      // 
      this.lblCurrLth.AutoSize = true;
      this.lblCurrLth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrLth.Location = new System.Drawing.Point(377, 43);
      this.lblCurrLth.Name = "lblCurrLth";
      this.lblCurrLth.Size = new System.Drawing.Size(25, 13);
      this.lblCurrLth.TabIndex = 64;
      this.lblCurrLth.Text = "000";
      // 
      // lblClientSize
      // 
      this.lblClientSize.AutoSize = true;
      this.lblClientSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblClientSize.Location = new System.Drawing.Point(638, 63);
      this.lblClientSize.Name = "lblClientSize";
      this.lblClientSize.Size = new System.Drawing.Size(86, 13);
      this.lblClientSize.TabIndex = 63;
      this.lblClientSize.Text = "W:0000  H:0000";
      // 
      // lblScreenSize
      // 
      this.lblScreenSize.AutoSize = true;
      this.lblScreenSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblScreenSize.Location = new System.Drawing.Point(225, 63);
      this.lblScreenSize.Name = "lblScreenSize";
      this.lblScreenSize.Size = new System.Drawing.Size(86, 13);
      this.lblScreenSize.TabIndex = 70;
      this.lblScreenSize.Text = "W:0000  H:0000";
      // 
      // lblPaddingValue
      // 
      this.lblPaddingValue.AutoSize = true;
      this.lblPaddingValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPaddingValue.Location = new System.Drawing.Point(460, 63);
      this.lblPaddingValue.Name = "lblPaddingValue";
      this.lblPaddingValue.Size = new System.Drawing.Size(113, 13);
      this.lblPaddingValue.TabIndex = 65;
      this.lblPaddingValue.Text = "T:00  R:00  B:00  L:00";
      // 
      // lblCurrSize
      // 
      this.lblCurrSize.AutoSize = true;
      this.lblCurrSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrSize.Location = new System.Drawing.Point(417, 43);
      this.lblCurrSize.Name = "lblCurrSize";
      this.lblCurrSize.Size = new System.Drawing.Size(86, 13);
      this.lblCurrSize.TabIndex = 66;
      this.lblCurrSize.Text = "W:0000  H:0000";
      // 
      // lblCurrLocation
      // 
      this.lblCurrLocation.AutoSize = true;
      this.lblCurrLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrLocation.Location = new System.Drawing.Point(228, 43);
      this.lblCurrLocation.Name = "lblCurrLocation";
      this.lblCurrLocation.Size = new System.Drawing.Size(81, 13);
      this.lblCurrLocation.TabIndex = 67;
      this.lblCurrLocation.Text = "X:0000  Y:0000";
      // 
      // lblOrigLine
      // 
      this.lblOrigLine.AutoSize = true;
      this.lblOrigLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigLine.Location = new System.Drawing.Point(313, 28);
      this.lblOrigLine.Name = "lblOrigLine";
      this.lblOrigLine.Size = new System.Drawing.Size(25, 13);
      this.lblOrigLine.TabIndex = 68;
      this.lblOrigLine.Text = "000";
      // 
      // lblOrigCol
      // 
      this.lblOrigCol.AutoSize = true;
      this.lblOrigCol.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigCol.Location = new System.Drawing.Point(345, 28);
      this.lblOrigCol.Name = "lblOrigCol";
      this.lblOrigCol.Size = new System.Drawing.Size(25, 13);
      this.lblOrigCol.TabIndex = 69;
      this.lblOrigCol.Text = "000";
      // 
      // lblOrigLth
      // 
      this.lblOrigLth.AutoSize = true;
      this.lblOrigLth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigLth.Location = new System.Drawing.Point(377, 28);
      this.lblOrigLth.Name = "lblOrigLth";
      this.lblOrigLth.Size = new System.Drawing.Size(25, 13);
      this.lblOrigLth.TabIndex = 57;
      this.lblOrigLth.Text = "000";
      // 
      // lblOrigSize
      // 
      this.lblOrigSize.AutoSize = true;
      this.lblOrigSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigSize.Location = new System.Drawing.Point(417, 28);
      this.lblOrigSize.Name = "lblOrigSize";
      this.lblOrigSize.Size = new System.Drawing.Size(86, 13);
      this.lblOrigSize.TabIndex = 39;
      this.lblOrigSize.Text = "W:0000  H:0000";
      // 
      // lblOrigLocation
      // 
      this.lblOrigLocation.AutoSize = true;
      this.lblOrigLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigLocation.Location = new System.Drawing.Point(228, 28);
      this.lblOrigLocation.Name = "lblOrigLocation";
      this.lblOrigLocation.Size = new System.Drawing.Size(81, 13);
      this.lblOrigLocation.TabIndex = 55;
      this.lblOrigLocation.Text = "X:0000  Y:0000";
      // 
      // lblTagValue
      // 
      this.lblTagValue.AutoSize = true;
      this.lblTagValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTagValue.Location = new System.Drawing.Point(19, 41);
      this.lblTagValue.Name = "lblTagValue";
      this.lblTagValue.Size = new System.Drawing.Size(25, 13);
      this.lblTagValue.TabIndex = 46;
      this.lblTagValue.Text = "Tag";
      // 
      // lblControlNameValue
      // 
      this.lblControlNameValue.AutoSize = true;
      this.lblControlNameValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblControlNameValue.Location = new System.Drawing.Point(19, 28);
      this.lblControlNameValue.Name = "lblControlNameValue";
      this.lblControlNameValue.Size = new System.Drawing.Size(33, 13);
      this.lblControlNameValue.TabIndex = 40;
      this.lblControlNameValue.Text = "Value";
      // 
      // lblLine
      // 
      this.lblLine.AutoSize = true;
      this.lblLine.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLine.Location = new System.Drawing.Point(313, 13);
      this.lblLine.Name = "lblLine";
      this.lblLine.Size = new System.Drawing.Size(26, 13);
      this.lblLine.TabIndex = 41;
      this.lblLine.Text = "Line";
      // 
      // lblCol
      // 
      this.lblCol.AutoSize = true;
      this.lblCol.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCol.Location = new System.Drawing.Point(346, 13);
      this.lblCol.Name = "lblCol";
      this.lblCol.Size = new System.Drawing.Size(22, 13);
      this.lblCol.TabIndex = 42;
      this.lblCol.Text = "Col";
      // 
      // lblLth
      // 
      this.lblLth.AutoSize = true;
      this.lblLth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLth.Location = new System.Drawing.Point(377, 13);
      this.lblLth.Name = "lblLth";
      this.lblLth.Size = new System.Drawing.Size(22, 13);
      this.lblLth.TabIndex = 43;
      this.lblLth.Text = "Lth";
      // 
      // lblPadding
      // 
      this.lblPadding.AutoSize = true;
      this.lblPadding.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPadding.Location = new System.Drawing.Point(417, 63);
      this.lblPadding.Name = "lblPadding";
      this.lblPadding.Size = new System.Drawing.Size(45, 13);
      this.lblPadding.TabIndex = 44;
      this.lblPadding.Text = "Padding";
      // 
      // lblClient
      // 
      this.lblClient.AutoSize = true;
      this.lblClient.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblClient.Location = new System.Drawing.Point(582, 63);
      this.lblClient.Name = "lblClient";
      this.lblClient.Size = new System.Drawing.Size(60, 13);
      this.lblClient.TabIndex = 45;
      this.lblClient.Text = "Client Size:";
      // 
      // lblScreen
      // 
      this.lblScreen.AutoSize = true;
      this.lblScreen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblScreen.Location = new System.Drawing.Point(181, 63);
      this.lblScreen.Name = "lblScreen";
      this.lblScreen.Size = new System.Drawing.Size(40, 13);
      this.lblScreen.TabIndex = 47;
      this.lblScreen.Text = "Screen";
      // 
      // lblCurrValue
      // 
      this.lblCurrValue.AutoSize = true;
      this.lblCurrValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrValue.Location = new System.Drawing.Point(532, 43);
      this.lblCurrValue.Name = "lblCurrValue";
      this.lblCurrValue.Size = new System.Drawing.Size(88, 13);
      this.lblCurrValue.TabIndex = 54;
      this.lblCurrValue.Text = "CURRENT VALUE";
      // 
      // lblOrigValue
      // 
      this.lblOrigValue.AutoSize = true;
      this.lblOrigValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOrigValue.Location = new System.Drawing.Point(532, 28);
      this.lblOrigValue.Name = "lblOrigValue";
      this.lblOrigValue.Size = new System.Drawing.Size(90, 13);
      this.lblOrigValue.TabIndex = 48;
      this.lblOrigValue.Text = "ORIGINAL VALUE";
      // 
      // lblValue
      // 
      this.lblValue.AutoSize = true;
      this.lblValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblValue.Location = new System.Drawing.Point(532, 13);
      this.lblValue.Name = "lblValue";
      this.lblValue.Size = new System.Drawing.Size(33, 13);
      this.lblValue.TabIndex = 49;
      this.lblValue.Text = "Value";
      // 
      // lblSize
      // 
      this.lblSize.AutoSize = true;
      this.lblSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSize.Location = new System.Drawing.Point(442, 13);
      this.lblSize.Name = "lblSize";
      this.lblSize.Size = new System.Drawing.Size(26, 13);
      this.lblSize.TabIndex = 50;
      this.lblSize.Text = "Size";
      // 
      // lblCurrent
      // 
      this.lblCurrent.AutoSize = true;
      this.lblCurrent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurrent.Location = new System.Drawing.Point(181, 43);
      this.lblCurrent.Name = "lblCurrent";
      this.lblCurrent.Size = new System.Drawing.Size(48, 13);
      this.lblCurrent.TabIndex = 51;
      this.lblCurrent.Text = "Current:";
      // 
      // lblOriginal
      // 
      this.lblOriginal.AutoSize = true;
      this.lblOriginal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOriginal.Location = new System.Drawing.Point(181, 28);
      this.lblOriginal.Name = "lblOriginal";
      this.lblOriginal.Size = new System.Drawing.Size(47, 13);
      this.lblOriginal.TabIndex = 52;
      this.lblOriginal.Text = "Original:";
      // 
      // lblLocation
      // 
      this.lblLocation.AutoSize = true;
      this.lblLocation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblLocation.Location = new System.Drawing.Point(243, 13);
      this.lblLocation.Name = "lblLocation";
      this.lblLocation.Size = new System.Drawing.Size(47, 13);
      this.lblLocation.TabIndex = 53;
      this.lblLocation.Text = "Location";
      // 
      // lblFontData
      // 
      this.lblFontData.AutoSize = true;
      this.lblFontData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFontData.Location = new System.Drawing.Point(15, 63);
      this.lblFontData.Name = "lblFontData";
      this.lblFontData.Size = new System.Drawing.Size(143, 13);
      this.lblFontData.TabIndex = 62;
      this.lblFontData.Text = "Font Size    Width       Height";
      // 
      // lblControlName
      // 
      this.lblControlName.AutoSize = true;
      this.lblControlName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblControlName.Location = new System.Drawing.Point(15, 13);
      this.lblControlName.Name = "lblControlName";
      this.lblControlName.Size = new System.Drawing.Size(100, 13);
      this.lblControlName.TabIndex = 56;
      this.lblControlName.Text = "Control Name / Tag";
      // 
      // ControlPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Name = "ControlPanel";
      this.Size = new System.Drawing.Size(736, 633);
      this.Tag = "ToolPanel_ControlPanel";
      this.Controls.SetChildIndex(this.mnuMain, 0);
      this.Controls.SetChildIndex(this.pnlTop, 0);
      this.Controls.SetChildIndex(this.pnlMain, 0);
      this.tabMain.ResumeLayout(false);
      this.tabPageScreenManagement.ResumeLayout(false);
      this.tabPageScreenManagement.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvFields)).EndInit();
      this.tabPageControlDetail.ResumeLayout(false);
      this.tabPageControlDetail.PerformLayout();
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.udCharHeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.udCharWidth)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageScreenManagement;
    private System.Windows.Forms.TabPage tabPageControlDetail;
    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileClose;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.Label lblScreenName;
    private System.Windows.Forms.ComboBox cboScreenName;
    private System.Windows.Forms.DataGridView gvFields;
    private System.Windows.Forms.Label lblScreenFields;
    private System.Windows.Forms.CheckBox ckShowFields;
    private System.Windows.Forms.NumericUpDown udCharHeight;
    private System.Windows.Forms.NumericUpDown udCharWidth;
    private System.Windows.Forms.NumericUpDown udFontSize;
    private System.Windows.Forms.Label lblScreenLines;
    private System.Windows.Forms.Label lblCurrLine;
    private System.Windows.Forms.Label lblScreenCols;
    private System.Windows.Forms.Label lblCurrCol;
    private System.Windows.Forms.Label lblCurrLth;
    private System.Windows.Forms.Label lblClientSize;
    private System.Windows.Forms.Label lblScreenSize;
    private System.Windows.Forms.Label lblPaddingValue;
    private System.Windows.Forms.Label lblCurrSize;
    private System.Windows.Forms.Label lblCurrLocation;
    private System.Windows.Forms.Label lblOrigLine;
    private System.Windows.Forms.Label lblOrigCol;
    private System.Windows.Forms.Label lblOrigLth;
    private System.Windows.Forms.Label lblOrigSize;
    private System.Windows.Forms.Label lblOrigLocation;
    private System.Windows.Forms.Label lblTagValue;
    private System.Windows.Forms.Label lblControlNameValue;
    private System.Windows.Forms.Label lblLine;
    private System.Windows.Forms.Label lblCol;
    private System.Windows.Forms.Label lblLth;
    private System.Windows.Forms.Label lblPadding;
    private System.Windows.Forms.Label lblClient;
    private System.Windows.Forms.Label lblScreen;
    private System.Windows.Forms.Label lblCurrValue;
    private System.Windows.Forms.Label lblOrigValue;
    private System.Windows.Forms.Label lblValue;
    private System.Windows.Forms.Label lblSize;
    private System.Windows.Forms.Label lblCurrent;
    private System.Windows.Forms.Label lblOriginal;
    private System.Windows.Forms.Label lblLocation;
    private System.Windows.Forms.Label lblFontData;
    private System.Windows.Forms.Label lblControlName;
  }
}

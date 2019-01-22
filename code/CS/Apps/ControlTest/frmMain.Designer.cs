namespace Org.ControlTest
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
      this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsShowConfig = new System.Windows.Forms.ToolStripMenuItem();
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlTopControl = new System.Windows.Forms.Panel();
      this.cboTables = new System.Windows.Forms.ComboBox();
      this.cboModels = new System.Windows.Forms.ComboBox();
      this.cboPanel = new System.Windows.Forms.ComboBox();
      this.ckReloadControlSpec = new System.Windows.Forms.CheckBox();
      this.btnBuildClass = new System.Windows.Forms.Button();
      this.btnLoadTables = new System.Windows.Forms.Button();
      this.btnTestEF = new System.Windows.Forms.Button();
      this.btnShowPanel = new System.Windows.Forms.Button();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageDisplay = new System.Windows.Forms.TabPage();
      this.pnlDisplay = new System.Windows.Forms.Panel();
      this.tabPageControlMap = new System.Windows.Forms.TabPage();
      this.fctxtMap = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tabPageReport = new System.Windows.Forms.TabPage();
      this.txtReport = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tabPageControls = new System.Windows.Forms.TabPage();
      this.txtTest = new System.Windows.Forms.TextBox();
      this.gvTest = new System.Windows.Forms.DataGridView();
      this.mnuConfigurations = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuDBConnection = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMain.SuspendLayout();
      this.pnlTopControl.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageDisplay.SuspendLayout();
      this.tabPageControlMap.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.fctxtMap)).BeginInit();
      this.tabPageReport.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtReport)).BeginInit();
      this.tabPageControls.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvTest)).BeginInit();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuOptions,
            this.mnuConfigurations});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1193, 24);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExit});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "&File";
      // 
      // mnuExit
      // 
      this.mnuExit.Name = "mnuExit";
      this.mnuExit.Size = new System.Drawing.Size(152, 22);
      this.mnuExit.Tag = "Exit";
      this.mnuExit.Text = "E&xit";
      this.mnuExit.Click += new System.EventHandler(this.Action);
      // 
      // mnuOptions
      // 
      this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptionsShowConfig});
      this.mnuOptions.Name = "mnuOptions";
      this.mnuOptions.Size = new System.Drawing.Size(61, 20);
      this.mnuOptions.Text = "Options";
      // 
      // mnuOptionsShowConfig
      // 
      this.mnuOptionsShowConfig.Name = "mnuOptionsShowConfig";
      this.mnuOptionsShowConfig.Size = new System.Drawing.Size(185, 22);
      this.mnuOptionsShowConfig.Tag = "ShowControlConfig";
      this.mnuOptionsShowConfig.Text = "Show Control Config";
      this.mnuOptionsShowConfig.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 727);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1193, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTopControl
      // 
      this.pnlTopControl.Controls.Add(this.cboTables);
      this.pnlTopControl.Controls.Add(this.cboModels);
      this.pnlTopControl.Controls.Add(this.cboPanel);
      this.pnlTopControl.Controls.Add(this.ckReloadControlSpec);
      this.pnlTopControl.Controls.Add(this.btnBuildClass);
      this.pnlTopControl.Controls.Add(this.btnLoadTables);
      this.pnlTopControl.Controls.Add(this.btnTestEF);
      this.pnlTopControl.Controls.Add(this.btnShowPanel);
      this.pnlTopControl.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTopControl.Location = new System.Drawing.Point(0, 24);
      this.pnlTopControl.Name = "pnlTopControl";
      this.pnlTopControl.Size = new System.Drawing.Size(1193, 88);
      this.pnlTopControl.TabIndex = 2;
      // 
      // cboTables
      // 
      this.cboTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTables.FormattingEnabled = true;
      this.cboTables.Location = new System.Drawing.Point(760, 13);
      this.cboTables.Name = "cboTables";
      this.cboTables.Size = new System.Drawing.Size(161, 21);
      this.cboTables.TabIndex = 3;
      // 
      // cboModels
      // 
      this.cboModels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboModels.FormattingEnabled = true;
      this.cboModels.Location = new System.Drawing.Point(465, 13);
      this.cboModels.Name = "cboModels";
      this.cboModels.Size = new System.Drawing.Size(161, 21);
      this.cboModels.TabIndex = 3;
      // 
      // cboPanel
      // 
      this.cboPanel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboPanel.FormattingEnabled = true;
      this.cboPanel.Location = new System.Drawing.Point(183, 13);
      this.cboPanel.Name = "cboPanel";
      this.cboPanel.Size = new System.Drawing.Size(177, 21);
      this.cboPanel.TabIndex = 2;
      this.cboPanel.SelectedIndexChanged += new System.EventHandler(this.cboPanel_SelectedIndexChanged);
      // 
      // ckReloadControlSpec
      // 
      this.ckReloadControlSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ckReloadControlSpec.AutoSize = true;
      this.ckReloadControlSpec.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ckReloadControlSpec.Checked = true;
      this.ckReloadControlSpec.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckReloadControlSpec.Location = new System.Drawing.Point(1060, 16);
      this.ckReloadControlSpec.Name = "ckReloadControlSpec";
      this.ckReloadControlSpec.Size = new System.Drawing.Size(121, 17);
      this.ckReloadControlSpec.TabIndex = 1;
      this.ckReloadControlSpec.Text = "Reload ControlSpec";
      this.ckReloadControlSpec.UseVisualStyleBackColor = true;
      // 
      // btnBuildClass
      // 
      this.btnBuildClass.Location = new System.Drawing.Point(927, 12);
      this.btnBuildClass.Name = "btnBuildClass";
      this.btnBuildClass.Size = new System.Drawing.Size(98, 23);
      this.btnBuildClass.TabIndex = 0;
      this.btnBuildClass.Tag = "BuildClass";
      this.btnBuildClass.Text = "Build Class";
      this.btnBuildClass.UseVisualStyleBackColor = true;
      this.btnBuildClass.Click += new System.EventHandler(this.Action);
      // 
      // btnLoadTables
      // 
      this.btnLoadTables.Location = new System.Drawing.Point(656, 12);
      this.btnLoadTables.Name = "btnLoadTables";
      this.btnLoadTables.Size = new System.Drawing.Size(98, 23);
      this.btnLoadTables.TabIndex = 0;
      this.btnLoadTables.Tag = "LoadTables";
      this.btnLoadTables.Text = "Load Tables";
      this.btnLoadTables.UseVisualStyleBackColor = true;
      this.btnLoadTables.Click += new System.EventHandler(this.Action);
      // 
      // btnTestEF
      // 
      this.btnTestEF.Location = new System.Drawing.Point(379, 12);
      this.btnTestEF.Name = "btnTestEF";
      this.btnTestEF.Size = new System.Drawing.Size(81, 23);
      this.btnTestEF.TabIndex = 0;
      this.btnTestEF.Tag = "TestEF";
      this.btnTestEF.Text = "Test EF";
      this.btnTestEF.UseVisualStyleBackColor = true;
      this.btnTestEF.Click += new System.EventHandler(this.Action);
      // 
      // btnShowPanel
      // 
      this.btnShowPanel.Location = new System.Drawing.Point(12, 12);
      this.btnShowPanel.Name = "btnShowPanel";
      this.btnShowPanel.Size = new System.Drawing.Size(149, 23);
      this.btnShowPanel.TabIndex = 0;
      this.btnShowPanel.Tag = "ShowPanel";
      this.btnShowPanel.Text = "Show Panel";
      this.btnShowPanel.UseVisualStyleBackColor = true;
      this.btnShowPanel.Click += new System.EventHandler(this.Action);
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageDisplay);
      this.tabMain.Controls.Add(this.tabPageControlMap);
      this.tabMain.Controls.Add(this.tabPageReport);
      this.tabMain.Controls.Add(this.tabPageControls);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(120, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 112);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1193, 615);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 3;
      // 
      // tabPageDisplay
      // 
      this.tabPageDisplay.Controls.Add(this.pnlDisplay);
      this.tabPageDisplay.Location = new System.Drawing.Point(4, 22);
      this.tabPageDisplay.Name = "tabPageDisplay";
      this.tabPageDisplay.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageDisplay.Size = new System.Drawing.Size(1185, 589);
      this.tabPageDisplay.TabIndex = 1;
      this.tabPageDisplay.Text = "Control Display";
      this.tabPageDisplay.UseVisualStyleBackColor = true;
      // 
      // pnlDisplay
      // 
      this.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlDisplay.Location = new System.Drawing.Point(3, 3);
      this.pnlDisplay.Name = "pnlDisplay";
      this.pnlDisplay.Size = new System.Drawing.Size(1179, 583);
      this.pnlDisplay.TabIndex = 0;
      // 
      // tabPageControlMap
      // 
      this.tabPageControlMap.Controls.Add(this.fctxtMap);
      this.tabPageControlMap.Location = new System.Drawing.Point(4, 22);
      this.tabPageControlMap.Name = "tabPageControlMap";
      this.tabPageControlMap.Size = new System.Drawing.Size(1185, 589);
      this.tabPageControlMap.TabIndex = 2;
      this.tabPageControlMap.Text = "Control Map";
      this.tabPageControlMap.UseVisualStyleBackColor = true;
      // 
      // fctxtMap
      // 
      this.fctxtMap.AutoCompleteBracketsList = new char[] {
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
      this.fctxtMap.AutoIndent = false;
      this.fctxtMap.AutoIndentChars = false;
      this.fctxtMap.AutoIndentCharsPatterns = "";
      this.fctxtMap.AutoIndentExistingLines = false;
      this.fctxtMap.AutoScrollMinSize = new System.Drawing.Size(2, 13);
      this.fctxtMap.BackBrush = null;
      this.fctxtMap.CharHeight = 13;
      this.fctxtMap.CharWidth = 7;
      this.fctxtMap.CommentPrefix = null;
      this.fctxtMap.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.fctxtMap.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.fctxtMap.Dock = System.Windows.Forms.DockStyle.Fill;
      this.fctxtMap.Font = new System.Drawing.Font("Courier New", 9F);
      this.fctxtMap.Hotkeys = resources.GetString("fctxtMap.Hotkeys");
      this.fctxtMap.IsReplaceMode = false;
      this.fctxtMap.Language = FastColoredTextBoxNS.Language.XML;
      this.fctxtMap.LeftBracket = '<';
      this.fctxtMap.LeftBracket2 = '(';
      this.fctxtMap.Location = new System.Drawing.Point(0, 0);
      this.fctxtMap.Name = "fctxtMap";
      this.fctxtMap.Paddings = new System.Windows.Forms.Padding(0);
      this.fctxtMap.RightBracket = '>';
      this.fctxtMap.RightBracket2 = ')';
      this.fctxtMap.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.fctxtMap.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctxtMap.ServiceColors")));
      this.fctxtMap.Size = new System.Drawing.Size(1185, 589);
      this.fctxtMap.TabIndex = 2;
      this.fctxtMap.Zoom = 100;
      // 
      // tabPageReport
      // 
      this.tabPageReport.Controls.Add(this.txtReport);
      this.tabPageReport.Location = new System.Drawing.Point(4, 22);
      this.tabPageReport.Name = "tabPageReport";
      this.tabPageReport.Size = new System.Drawing.Size(1185, 589);
      this.tabPageReport.TabIndex = 3;
      this.tabPageReport.Text = "Report";
      this.tabPageReport.UseVisualStyleBackColor = true;
      // 
      // txtReport
      // 
      this.txtReport.AutoCompleteBracketsList = new char[] {
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
      this.txtReport.AutoIndent = false;
      this.txtReport.AutoIndentChars = false;
      this.txtReport.AutoIndentCharsPatterns = "";
      this.txtReport.AutoIndentExistingLines = false;
      this.txtReport.AutoScrollMinSize = new System.Drawing.Size(2, 13);
      this.txtReport.BackBrush = null;
      this.txtReport.CharHeight = 13;
      this.txtReport.CharWidth = 7;
      this.txtReport.CommentPrefix = null;
      this.txtReport.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtReport.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtReport.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtReport.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtReport.Hotkeys = resources.GetString("txtReport.Hotkeys");
      this.txtReport.IsReplaceMode = false;
      this.txtReport.LeftBracket = '<';
      this.txtReport.LeftBracket2 = '(';
      this.txtReport.Location = new System.Drawing.Point(0, 0);
      this.txtReport.Name = "txtReport";
      this.txtReport.Paddings = new System.Windows.Forms.Padding(0);
      this.txtReport.RightBracket = '>';
      this.txtReport.RightBracket2 = ')';
      this.txtReport.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtReport.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtReport.ServiceColors")));
      this.txtReport.Size = new System.Drawing.Size(1185, 589);
      this.txtReport.TabIndex = 3;
      this.txtReport.Zoom = 100;
      // 
      // tabPageControls
      // 
      this.tabPageControls.Controls.Add(this.txtTest);
      this.tabPageControls.Controls.Add(this.gvTest);
      this.tabPageControls.Location = new System.Drawing.Point(4, 22);
      this.tabPageControls.Name = "tabPageControls";
      this.tabPageControls.Size = new System.Drawing.Size(1185, 589);
      this.tabPageControls.TabIndex = 4;
      this.tabPageControls.Text = "Controls";
      this.tabPageControls.UseVisualStyleBackColor = true;
      // 
      // txtTest
      // 
      this.txtTest.Location = new System.Drawing.Point(331, 21);
      this.txtTest.Name = "txtTest";
      this.txtTest.Size = new System.Drawing.Size(125, 20);
      this.txtTest.TabIndex = 5;
      this.txtTest.TextChanged += new System.EventHandler(this.txtTest_TextChanged);
      // 
      // gvTest
      // 
      this.gvTest.AllowUserToAddRows = false;
      this.gvTest.AllowUserToDeleteRows = false;
      this.gvTest.AllowUserToResizeRows = false;
      this.gvTest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvTest.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvTest.Location = new System.Drawing.Point(24, 21);
      this.gvTest.Name = "gvTest";
      this.gvTest.RowHeadersVisible = false;
      this.gvTest.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvTest.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DodgerBlue;
      this.gvTest.RowTemplate.Height = 19;
      this.gvTest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvTest.Size = new System.Drawing.Size(300, 169);
      this.gvTest.TabIndex = 4;
      this.gvTest.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTest_RowLeave);
      this.gvTest.SelectionChanged += new System.EventHandler(this.gvTest_SelectionChanged);
      // 
      // mnuConfigurations
      // 
      this.mnuConfigurations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDBConnection});
      this.mnuConfigurations.Name = "mnuConfigurations";
      this.mnuConfigurations.Size = new System.Drawing.Size(98, 20);
      this.mnuConfigurations.Text = "&Configurations";
      // 
      // mnuDBConnection
      // 
      this.mnuDBConnection.Name = "mnuDBConnection";
      this.mnuDBConnection.Size = new System.Drawing.Size(187, 22);
      this.mnuDBConnection.Tag = "DBConnection";
      this.mnuDBConnection.Text = "&Database Connection";
      this.mnuDBConnection.Click += new System.EventHandler(this.Action);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1193, 750);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.pnlTopControl);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Control Test - Version 1.0.0";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTopControl.ResumeLayout(false);
      this.pnlTopControl.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabPageDisplay.ResumeLayout(false);
      this.tabPageControlMap.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.fctxtMap)).EndInit();
      this.tabPageReport.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtReport)).EndInit();
      this.tabPageControls.ResumeLayout(false);
      this.tabPageControls.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvTest)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTopControl;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageDisplay;
    private System.Windows.Forms.Panel pnlDisplay;
    private System.Windows.Forms.Button btnShowPanel;
    private System.Windows.Forms.CheckBox ckReloadControlSpec;
    private System.Windows.Forms.ComboBox cboPanel;
    private System.Windows.Forms.TabPage tabPageControlMap;
    private FastColoredTextBoxNS.FastColoredTextBox fctxtMap;
    private System.Windows.Forms.Button btnTestEF;
    private System.Windows.Forms.TabPage tabPageReport;
    private FastColoredTextBoxNS.FastColoredTextBox txtReport;
    private System.Windows.Forms.ComboBox cboModels;
    private System.Windows.Forms.ComboBox cboTables;
    private System.Windows.Forms.Button btnBuildClass;
    private System.Windows.Forms.Button btnLoadTables;
    private System.Windows.Forms.ToolStripMenuItem mnuOptions;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsShowConfig;
    private System.Windows.Forms.TabPage tabPageControls;
    private System.Windows.Forms.DataGridView gvTest;
    private System.Windows.Forms.TextBox txtTest;
    private System.Windows.Forms.ToolStripMenuItem mnuConfigurations;
    private System.Windows.Forms.ToolStripMenuItem mnuDBConnection;
  }
}


namespace Org.OrgScriptWorkbench
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
      this.txtCode1 = new FastColoredTextBoxNS.FastColoredTextBox();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnDebugCompile = new System.Windows.Forms.Button();
      this.btnRun = new System.Windows.Forms.Button();
      this.btnExecute = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCompile = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.splitMain = new System.Windows.Forms.SplitContainer();
      this.lbFiles = new System.Windows.Forms.ListBox();
      this.pnlLeftBottom = new System.Windows.Forms.Panel();
      this.lblFiles = new System.Windows.Forms.Label();
      this.splitSolution = new System.Windows.Forms.SplitContainer();
      this.splitCode = new System.Windows.Forms.SplitContainer();
      this.tabDrawer = new System.Windows.Forms.TabControl();
      this.tabPageConsole = new System.Windows.Forms.TabPage();
      this.txtConsole = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tabPageScope = new System.Windows.Forms.TabPage();
      this.label1 = new System.Windows.Forms.Label();
      this.btnStepForward = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.txtCode1)).BeginInit();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPage1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
      this.splitMain.Panel1.SuspendLayout();
      this.splitMain.Panel2.SuspendLayout();
      this.splitMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitSolution)).BeginInit();
      this.splitSolution.Panel1.SuspendLayout();
      this.splitSolution.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitCode)).BeginInit();
      this.splitCode.Panel1.SuspendLayout();
      this.splitCode.Panel2.SuspendLayout();
      this.splitCode.SuspendLayout();
      this.tabDrawer.SuspendLayout();
      this.tabPageConsole.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtConsole)).BeginInit();
      this.SuspendLayout();
      //
      // txtCode1
      //
      this.txtCode1.AutoCompleteBracketsList = new char[] {
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
      this.txtCode1.AutoIndentCharsPatterns = "\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\n";
      this.txtCode1.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtCode1.BackBrush = null;
      this.txtCode1.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
      this.txtCode1.CharHeight = 13;
      this.txtCode1.CharWidth = 7;
      this.txtCode1.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtCode1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtCode1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCode1.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtCode1.IsReplaceMode = false;
      this.txtCode1.Language = FastColoredTextBoxNS.Language.JS;
      this.txtCode1.LeftBracket = '(';
      this.txtCode1.LeftBracket2 = '{';
      this.txtCode1.Location = new System.Drawing.Point(3, 3);
      this.txtCode1.Name = "txtCode1";
      this.txtCode1.Paddings = new System.Windows.Forms.Padding(0);
      this.txtCode1.RightBracket = ')';
      this.txtCode1.RightBracket2 = '}';
      this.txtCode1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtCode1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtCode1.ServiceColors")));
      this.txtCode1.Size = new System.Drawing.Size(739, 424);
      this.txtCode1.TabIndex = 0;
      this.txtCode1.TabLength = 2;
      this.txtCode1.Zoom = 100;
      this.txtCode1.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txtCode1_TextChanged);
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(4, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1068, 24);
      this.mnuMain.TabIndex = 1;
      this.mnuMain.Text = "menuStrip1";
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
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.btnStepForward);
      this.pnlTop.Controls.Add(this.btnDebugCompile);
      this.pnlTop.Controls.Add(this.btnRun);
      this.pnlTop.Controls.Add(this.btnExecute);
      this.pnlTop.Controls.Add(this.btnSave);
      this.pnlTop.Controls.Add(this.btnCompile);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1068, 70);
      this.pnlTop.TabIndex = 2;
      //
      // btnDebugCompile
      //
      this.btnDebugCompile.Location = new System.Drawing.Point(239, 8);
      this.btnDebugCompile.Name = "btnDebugCompile";
      this.btnDebugCompile.Size = new System.Drawing.Size(103, 23);
      this.btnDebugCompile.TabIndex = 2;
      this.btnDebugCompile.Tag = "DebugCompile";
      this.btnDebugCompile.Text = "Debug Compile";
      this.btnDebugCompile.UseVisualStyleBackColor = true;
      this.btnDebugCompile.Click += new System.EventHandler(this.Action);
      //
      // btnRun
      //
      this.btnRun.Location = new System.Drawing.Point(457, 8);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(103, 23);
      this.btnRun.TabIndex = 4;
      this.btnRun.Tag = "Run";
      this.btnRun.Text = "Run";
      this.btnRun.UseVisualStyleBackColor = true;
      this.btnRun.Click += new System.EventHandler(this.Action);
      //
      // btnExecute
      //
      this.btnExecute.Location = new System.Drawing.Point(348, 8);
      this.btnExecute.Name = "btnExecute";
      this.btnExecute.Size = new System.Drawing.Size(103, 23);
      this.btnExecute.TabIndex = 3;
      this.btnExecute.Tag = "Execute";
      this.btnExecute.Text = "Execute";
      this.btnExecute.UseVisualStyleBackColor = true;
      this.btnExecute.Click += new System.EventHandler(this.Action);
      //
      // btnSave
      //
      this.btnSave.Location = new System.Drawing.Point(21, 8);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(103, 23);
      this.btnSave.TabIndex = 0;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Action);
      //
      // btnCompile
      //
      this.btnCompile.Location = new System.Drawing.Point(130, 8);
      this.btnCompile.Name = "btnCompile";
      this.btnCompile.Size = new System.Drawing.Size(103, 23);
      this.btnCompile.TabIndex = 1;
      this.btnCompile.Tag = "Compile";
      this.btnCompile.Text = "Compile";
      this.btnCompile.UseVisualStyleBackColor = true;
      this.btnCompile.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(4, 723);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1068, 26);
      this.lblStatus.TabIndex = 3;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // tabMain
      //
      this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                             | System.Windows.Forms.AnchorStyles.Left)
                             | System.Windows.Forms.AnchorStyles.Right)));
      this.tabMain.Controls.Add(this.tabPage1);
      this.tabMain.ItemSize = new System.Drawing.Size(1, 1);
      this.tabMain.Location = new System.Drawing.Point(-5, -5);
      this.tabMain.Multiline = true;
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(755, 441);
      this.tabMain.TabIndex = 4;
      //
      // tabPage1
      //
      this.tabPage1.Controls.Add(this.txtCode1);
      this.tabPage1.Location = new System.Drawing.Point(4, 5);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 5, 5);
      this.tabPage1.Size = new System.Drawing.Size(747, 432);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.UseVisualStyleBackColor = true;
      //
      // splitMain
      //
      this.splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitMain.Location = new System.Drawing.Point(4, 94);
      this.splitMain.Name = "splitMain";
      //
      // splitMain.Panel1
      //
      this.splitMain.Panel1.Controls.Add(this.lbFiles);
      this.splitMain.Panel1.Controls.Add(this.pnlLeftBottom);
      this.splitMain.Panel1.Controls.Add(this.lblFiles);
      //
      // splitMain.Panel2
      //
      this.splitMain.Panel2.Controls.Add(this.splitSolution);
      this.splitMain.Panel2.Controls.Add(this.label1);
      this.splitMain.Size = new System.Drawing.Size(1068, 629);
      this.splitMain.SplitterDistance = 178;
      this.splitMain.TabIndex = 5;
      //
      // lbFiles
      //
      this.lbFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.lbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbFiles.FormattingEnabled = true;
      this.lbFiles.Location = new System.Drawing.Point(0, 23);
      this.lbFiles.Name = "lbFiles";
      this.lbFiles.Size = new System.Drawing.Size(176, 344);
      this.lbFiles.TabIndex = 2;
      //
      // pnlLeftBottom
      //
      this.pnlLeftBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlLeftBottom.Location = new System.Drawing.Point(0, 367);
      this.pnlLeftBottom.Name = "pnlLeftBottom";
      this.pnlLeftBottom.Size = new System.Drawing.Size(176, 260);
      this.pnlLeftBottom.TabIndex = 1;
      //
      // lblFiles
      //
      this.lblFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblFiles.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFiles.Location = new System.Drawing.Point(0, 0);
      this.lblFiles.Name = "lblFiles";
      this.lblFiles.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
      this.lblFiles.Size = new System.Drawing.Size(176, 23);
      this.lblFiles.TabIndex = 0;
      this.lblFiles.Text = "Files";
      this.lblFiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // splitSolution
      //
      this.splitSolution.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitSolution.Location = new System.Drawing.Point(0, 23);
      this.splitSolution.Name = "splitSolution";
      //
      // splitSolution.Panel1
      //
      this.splitSolution.Panel1.Controls.Add(this.splitCode);
      this.splitSolution.Size = new System.Drawing.Size(884, 604);
      this.splitSolution.SplitterDistance = 751;
      this.splitSolution.TabIndex = 8;
      //
      // splitCode
      //
      this.splitCode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitCode.Location = new System.Drawing.Point(0, 0);
      this.splitCode.Name = "splitCode";
      this.splitCode.Orientation = System.Windows.Forms.Orientation.Horizontal;
      //
      // splitCode.Panel1
      //
      this.splitCode.Panel1.Controls.Add(this.tabMain);
      //
      // splitCode.Panel2
      //
      this.splitCode.Panel2.Controls.Add(this.tabDrawer);
      this.splitCode.Size = new System.Drawing.Size(751, 604);
      this.splitCode.SplitterDistance = 432;
      this.splitCode.TabIndex = 7;
      //
      // tabDrawer
      //
      this.tabDrawer.Controls.Add(this.tabPageConsole);
      this.tabDrawer.Controls.Add(this.tabPageScope);
      this.tabDrawer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabDrawer.Font = new System.Drawing.Font("Tahoma", 8F);
      this.tabDrawer.ItemSize = new System.Drawing.Size(125, 16);
      this.tabDrawer.Location = new System.Drawing.Point(0, 0);
      this.tabDrawer.Name = "tabDrawer";
      this.tabDrawer.SelectedIndex = 0;
      this.tabDrawer.Size = new System.Drawing.Size(751, 168);
      this.tabDrawer.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabDrawer.TabIndex = 0;
      //
      // tabPageConsole
      //
      this.tabPageConsole.Controls.Add(this.txtConsole);
      this.tabPageConsole.Location = new System.Drawing.Point(4, 20);
      this.tabPageConsole.Name = "tabPageConsole";
      this.tabPageConsole.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageConsole.Size = new System.Drawing.Size(743, 144);
      this.tabPageConsole.TabIndex = 0;
      this.tabPageConsole.Text = "Console";
      this.tabPageConsole.UseVisualStyleBackColor = true;
      //
      // txtConsole
      //
      this.txtConsole.AutoCompleteBracketsList = new char[] {
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
      this.txtConsole.AutoScrollMinSize = new System.Drawing.Size(2, 12);
      this.txtConsole.BackBrush = null;
      this.txtConsole.CharHeight = 12;
      this.txtConsole.CharWidth = 6;
      this.txtConsole.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtConsole.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtConsole.Font = new System.Drawing.Font("Consolas", 8.25F);
      this.txtConsole.IsReplaceMode = false;
      this.txtConsole.Location = new System.Drawing.Point(3, 3);
      this.txtConsole.Name = "txtConsole";
      this.txtConsole.Paddings = new System.Windows.Forms.Padding(0);
      this.txtConsole.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtConsole.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtConsole.ServiceColors")));
      this.txtConsole.ShowLineNumbers = false;
      this.txtConsole.Size = new System.Drawing.Size(737, 138);
      this.txtConsole.TabIndex = 1;
      this.txtConsole.Zoom = 100;
      //
      // tabPageScope
      //
      this.tabPageScope.Location = new System.Drawing.Point(4, 20);
      this.tabPageScope.Name = "tabPageScope";
      this.tabPageScope.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageScope.Size = new System.Drawing.Size(743, 144);
      this.tabPageScope.TabIndex = 1;
      this.tabPageScope.Text = "Scope Map";
      this.tabPageScope.UseVisualStyleBackColor = true;
      //
      // label1
      //
      this.label1.Dock = System.Windows.Forms.DockStyle.Top;
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
      this.label1.Size = new System.Drawing.Size(884, 23);
      this.label1.TabIndex = 5;
      this.label1.Text = "script.js";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // btnStepForward
      //
      this.btnStepForward.Location = new System.Drawing.Point(239, 37);
      this.btnStepForward.Name = "btnStepForward";
      this.btnStepForward.Size = new System.Drawing.Size(103, 23);
      this.btnStepForward.TabIndex = 5;
      this.btnStepForward.Tag = "StepForward";
      this.btnStepForward.Text = "Step Forward";
      this.btnStepForward.UseVisualStyleBackColor = true;
      this.btnStepForward.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1076, 749);
      this.Controls.Add(this.splitMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "OrgScript Workbench";
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      ((System.ComponentModel.ISupportInitialize)(this.txtCode1)).EndInit();
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.splitMain.Panel1.ResumeLayout(false);
      this.splitMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
      this.splitMain.ResumeLayout(false);
      this.splitSolution.Panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitSolution)).EndInit();
      this.splitSolution.ResumeLayout(false);
      this.splitCode.Panel1.ResumeLayout(false);
      this.splitCode.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitCode)).EndInit();
      this.splitCode.ResumeLayout(false);
      this.tabDrawer.ResumeLayout(false);
      this.tabPageConsole.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtConsole)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private FastColoredTextBoxNS.FastColoredTextBox txtCode1;
    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.SplitContainer splitMain;
    private System.Windows.Forms.ListBox lbFiles;
    private System.Windows.Forms.Panel pnlLeftBottom;
    private System.Windows.Forms.Label lblFiles;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.SplitContainer splitCode;
    private System.Windows.Forms.TabControl tabDrawer;
    private System.Windows.Forms.TabPage tabPageConsole;
    private System.Windows.Forms.TabPage tabPageScope;
    private System.Windows.Forms.SplitContainer splitSolution;
    private System.Windows.Forms.Button btnRun;
    private System.Windows.Forms.Button btnExecute;
    private System.Windows.Forms.Button btnCompile;
    private FastColoredTextBoxNS.FastColoredTextBox txtConsole;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnDebugCompile;
    private System.Windows.Forms.Button btnStepForward;
  }
}


namespace DynamoDbWorkbench
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
      this.btnGo = new System.Windows.Forms.Button();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.tvMain = new System.Windows.Forms.TreeView();
      this.pnlLeftTop = new System.Windows.Forms.Panel();
      this.pnlRightTop = new System.Windows.Forms.Panel();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.txtMain = new FastColoredTextBoxNS.FastColoredTextBox();
      this.btnTestConnection = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.pnlMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtMain)).BeginInit();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(4, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1118, 24);
      this.mnuMain.TabIndex = 0;
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
      this.mnuFileExit.Size = new System.Drawing.Size(180, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(4, 771);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(1118, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.btnTestConnection);
      this.pnlTop.Controls.Add(this.btnGo);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1118, 44);
      this.pnlTop.TabIndex = 2;
      //
      // btnGo
      //
      this.btnGo.Location = new System.Drawing.Point(13, 10);
      this.btnGo.Name = "btnGo";
      this.btnGo.Size = new System.Drawing.Size(109, 23);
      this.btnGo.TabIndex = 0;
      this.btnGo.Tag = "Go";
      this.btnGo.Text = "Go";
      this.btnGo.UseVisualStyleBackColor = true;
      this.btnGo.Click += new System.EventHandler(this.Action);
      //
      // pnlMain
      //
      this.pnlMain.Controls.Add(this.splitterMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(4, 68);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1118, 703);
      this.pnlMain.TabIndex = 3;
      //
      // splitterMain
      //
      this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.Location = new System.Drawing.Point(0, 0);
      this.splitterMain.Name = "splitterMain";
      //
      // splitterMain.Panel1
      //
      this.splitterMain.Panel1.Controls.Add(this.tvMain);
      this.splitterMain.Panel1.Controls.Add(this.pnlLeftTop);
      //
      // splitterMain.Panel2
      //
      this.splitterMain.Panel2.Controls.Add(this.pnlRightTop);
      this.splitterMain.Panel2.Controls.Add(this.tabMain);
      this.splitterMain.Size = new System.Drawing.Size(1118, 703);
      this.splitterMain.SplitterDistance = 212;
      this.splitterMain.TabIndex = 0;
      //
      // tvMain
      //
      this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMain.Location = new System.Drawing.Point(0, 65);
      this.tvMain.Name = "tvMain";
      this.tvMain.Size = new System.Drawing.Size(210, 636);
      this.tvMain.TabIndex = 0;
      //
      // pnlLeftTop
      //
      this.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlLeftTop.Location = new System.Drawing.Point(0, 0);
      this.pnlLeftTop.Name = "pnlLeftTop";
      this.pnlLeftTop.Size = new System.Drawing.Size(210, 65);
      this.pnlLeftTop.TabIndex = 0;
      //
      // pnlRightTop
      //
      this.pnlRightTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlRightTop.Location = new System.Drawing.Point(0, 0);
      this.pnlRightTop.Name = "pnlRightTop";
      this.pnlRightTop.Size = new System.Drawing.Size(900, 65);
      this.pnlRightTop.TabIndex = 1;
      //
      // tabMain
      //
      this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                             | System.Windows.Forms.AnchorStyles.Left)
                             | System.Windows.Forms.AnchorStyles.Right)));
      this.tabMain.Controls.Add(this.tabPage2);
      this.tabMain.ItemSize = new System.Drawing.Size(1, 1);
      this.tabMain.Location = new System.Drawing.Point(-2, 59);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(903, 642);
      this.tabMain.TabIndex = 2;
      //
      // tabPage2
      //
      this.tabPage2.Controls.Add(this.txtMain);
      this.tabPage2.Location = new System.Drawing.Point(4, 5);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(895, 633);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.UseVisualStyleBackColor = true;
      //
      // txtMain
      //
      this.txtMain.AutoCompleteBracketsList = new char[] {
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
      this.txtMain.AutoIndentCharsPatterns = "\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\n";
      this.txtMain.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtMain.BackBrush = null;
      this.txtMain.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
      this.txtMain.CharHeight = 13;
      this.txtMain.CharWidth = 7;
      this.txtMain.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtMain.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtMain.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtMain.IsReplaceMode = false;
      this.txtMain.Language = FastColoredTextBoxNS.Language.JS;
      this.txtMain.LeftBracket = '(';
      this.txtMain.LeftBracket2 = '{';
      this.txtMain.Location = new System.Drawing.Point(3, 3);
      this.txtMain.Name = "txtMain";
      this.txtMain.Paddings = new System.Windows.Forms.Padding(0);
      this.txtMain.RightBracket = ')';
      this.txtMain.RightBracket2 = '}';
      this.txtMain.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtMain.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtMain.ServiceColors")));
      this.txtMain.Size = new System.Drawing.Size(889, 627);
      this.txtMain.TabIndex = 1;
      this.txtMain.TabLength = 2;
      this.txtMain.Zoom = 100;
      //
      // btnTestConnection
      //
      this.btnTestConnection.Location = new System.Drawing.Point(222, 10);
      this.btnTestConnection.Name = "btnTestConnection";
      this.btnTestConnection.Size = new System.Drawing.Size(109, 23);
      this.btnTestConnection.TabIndex = 0;
      this.btnTestConnection.Tag = "TestConnection";
      this.btnTestConnection.Text = "Test Connection";
      this.btnTestConnection.UseVisualStyleBackColor = true;
      this.btnTestConnection.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1126, 794);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "DynamoDB Workbench";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlMain.ResumeLayout(false);
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtMain)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnGo;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.TreeView tvMain;
    private System.Windows.Forms.Panel pnlLeftTop;
    private System.Windows.Forms.Panel pnlRightTop;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPage2;
    private FastColoredTextBoxNS.FastColoredTextBox txtMain;
    private System.Windows.Forms.Button btnTestConnection;
  }
}


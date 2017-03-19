namespace Org.CodeCompare
{
  partial class frmCode
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCode));
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileClose = new System.Windows.Forms.ToolStripMenuItem();
      this.lblStatus = new System.Windows.Forms.Label();
      this.txtCode = new FastColoredTextBoxNS.FastColoredTextBox();
      this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtCode)).BeginInit();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1276, 24);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileClose});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "&File";
      // 
      // mnuFileSaveAs
      // 
      this.mnuFileSaveAs.Name = "mnuFileSaveAs";
      this.mnuFileSaveAs.Size = new System.Drawing.Size(152, 22);
      this.mnuFileSaveAs.Tag = "SaveAs";
      this.mnuFileSaveAs.Text = "Save &As";
      this.mnuFileSaveAs.Click += new System.EventHandler(this.Action);
      // 
      // mnuFileClose
      // 
      this.mnuFileClose.Name = "mnuFileClose";
      this.mnuFileClose.Size = new System.Drawing.Size(152, 22);
      this.mnuFileClose.Tag = "Close";
      this.mnuFileClose.Text = "&Close";
      this.mnuFileClose.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 930);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1276, 20);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtCode
      // 
      this.txtCode.AutoCompleteBracketsList = new char[] {
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
      this.txtCode.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
      this.txtCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
      this.txtCode.BackBrush = null;
      this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtCode.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
      this.txtCode.CharHeight = 14;
      this.txtCode.CharWidth = 8;
      this.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCode.IsReplaceMode = false;
      this.txtCode.Language = FastColoredTextBoxNS.Language.CSharp;
      this.txtCode.LeftBracket = '(';
      this.txtCode.LeftBracket2 = '{';
      this.txtCode.Location = new System.Drawing.Point(0, 24);
      this.txtCode.Name = "txtCode";
      this.txtCode.Paddings = new System.Windows.Forms.Padding(0);
      this.txtCode.RightBracket = ')';
      this.txtCode.RightBracket2 = '}';
      this.txtCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtCode.ServiceColors")));
      this.txtCode.Size = new System.Drawing.Size(1276, 906);
      this.txtCode.TabIndex = 2;
      this.txtCode.Zoom = 100;
      this.txtCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyUp);
      // 
      // mnuFileSave
      // 
      this.mnuFileSave.Name = "mnuFileSave";
      this.mnuFileSave.Size = new System.Drawing.Size(152, 22);
      this.mnuFileSave.Tag = "Save";
      this.mnuFileSave.Text = "&Save";
      this.mnuFileSave.Click += new System.EventHandler(this.Action);
      // 
      // frmCode
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1276, 950);
      this.Controls.Add(this.txtCode);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MainMenuStrip = this.mnuMain;
      this.MinimizeBox = false;
      this.Name = "frmCode";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Code Display";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtCode)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
    private System.Windows.Forms.ToolStripMenuItem mnuFileClose;
    private System.Windows.Forms.Label lblStatus;
    private FastColoredTextBoxNS.FastColoredTextBox txtCode;
    private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
  }
}
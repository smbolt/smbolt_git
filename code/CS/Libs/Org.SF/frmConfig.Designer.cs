namespace Org.SF
{
  partial class frmConfig
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnHide = new System.Windows.Forms.Button();
      this.fctxtMain = new FastColoredTextBoxNS.FastColoredTextBox();
      this.btnSaveAndRefresh = new System.Windows.Forms.Button();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.fctxtMain)).BeginInit();
      this.SuspendLayout();
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.btnSaveAndRefresh);
      this.pnlTop.Controls.Add(this.btnHide);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1053, 38);
      this.pnlTop.TabIndex = 0;
      //
      // btnHide
      //
      this.btnHide.Location = new System.Drawing.Point(17, 7);
      this.btnHide.Name = "btnHide";
      this.btnHide.Size = new System.Drawing.Size(104, 23);
      this.btnHide.TabIndex = 0;
      this.btnHide.Text = "Hide";
      this.btnHide.UseVisualStyleBackColor = true;
      this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
      //
      // fctxtMain
      //
      this.fctxtMain.AutoCompleteBracketsList = new char[] {
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
      this.fctxtMain.AutoIndent = false;
      this.fctxtMain.AutoIndentChars = false;
      this.fctxtMain.AutoIndentCharsPatterns = "";
      this.fctxtMain.AutoIndentExistingLines = false;
      this.fctxtMain.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.fctxtMain.BackBrush = null;
      this.fctxtMain.CharHeight = 13;
      this.fctxtMain.CharWidth = 7;
      this.fctxtMain.CommentPrefix = null;
      this.fctxtMain.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.fctxtMain.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.fctxtMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.fctxtMain.Font = new System.Drawing.Font("Courier New", 9F);
      this.fctxtMain.Hotkeys = resources.GetString("fctxtMain.Hotkeys");
      this.fctxtMain.IsReplaceMode = false;
      this.fctxtMain.Language = FastColoredTextBoxNS.Language.XML;
      this.fctxtMain.LeftBracket = '<';
      this.fctxtMain.LeftBracket2 = '(';
      this.fctxtMain.Location = new System.Drawing.Point(0, 38);
      this.fctxtMain.Name = "fctxtMain";
      this.fctxtMain.Paddings = new System.Windows.Forms.Padding(0);
      this.fctxtMain.RightBracket = '>';
      this.fctxtMain.RightBracket2 = ')';
      this.fctxtMain.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.fctxtMain.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctxtMain.ServiceColors")));
      this.fctxtMain.Size = new System.Drawing.Size(1053, 701);
      this.fctxtMain.TabIndex = 2;
      this.fctxtMain.Zoom = 100;
      this.fctxtMain.CustomAction += new System.EventHandler<FastColoredTextBoxNS.CustomActionEventArgs>(this.fctxtMain_CustomAction);
      //
      // btnSaveAndRefresh
      //
      this.btnSaveAndRefresh.Location = new System.Drawing.Point(127, 7);
      this.btnSaveAndRefresh.Name = "btnSaveAndRefresh";
      this.btnSaveAndRefresh.Size = new System.Drawing.Size(131, 23);
      this.btnSaveAndRefresh.TabIndex = 0;
      this.btnSaveAndRefresh.Tag = "SaveAndRefresh";
      this.btnSaveAndRefresh.Text = "Save and Refresh";
      this.btnSaveAndRefresh.UseVisualStyleBackColor = true;
      this.btnSaveAndRefresh.Click += new System.EventHandler(this.btnSaveAndRefresh_Click);
      //
      // frmConfig
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1053, 739);
      this.ControlBox = false;
      this.Controls.Add(this.fctxtMain);
      this.Controls.Add(this.pnlTop);
      this.KeyPreview = true;
      this.Name = "frmConfig";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Configuration Form";
      this.pnlTop.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.fctxtMain)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnHide;
    private FastColoredTextBoxNS.FastColoredTextBox fctxtMain;
    private System.Windows.Forms.Button btnSaveAndRefresh;
  }
}
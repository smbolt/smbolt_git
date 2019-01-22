namespace Org.PdfExtract
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
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.txtPageBreak = new System.Windows.Forms.TextBox();
      this.ckReplaceCrlfWithNewLine = new System.Windows.Forms.CheckBox();
      this.lblPageBreakText = new System.Windows.Forms.Label();
      this.lblTextExtractionStrategy = new System.Windows.Forms.Label();
      this.cboTextExtractionStrategy = new System.Windows.Forms.ComboBox();
      this.btnDisplayXml = new System.Windows.Forms.Button();
      this.btnRun = new System.Windows.Forms.Button();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.btnExtractImages = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1218, 24);
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
      this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 655);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1218, 24);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.txtPageBreak);
      this.pnlTop.Controls.Add(this.ckReplaceCrlfWithNewLine);
      this.pnlTop.Controls.Add(this.lblPageBreakText);
      this.pnlTop.Controls.Add(this.lblTextExtractionStrategy);
      this.pnlTop.Controls.Add(this.cboTextExtractionStrategy);
      this.pnlTop.Controls.Add(this.btnDisplayXml);
      this.pnlTop.Controls.Add(this.btnExtractImages);
      this.pnlTop.Controls.Add(this.btnRun);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1218, 107);
      this.pnlTop.TabIndex = 2;
      //
      // txtPageBreak
      //
      this.txtPageBreak.Location = new System.Drawing.Point(297, 81);
      this.txtPageBreak.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.txtPageBreak.Name = "txtPageBreak";
      this.txtPageBreak.Size = new System.Drawing.Size(241, 20);
      this.txtPageBreak.TabIndex = 4;
      //
      // ckReplaceCrlfWithNewLine
      //
      this.ckReplaceCrlfWithNewLine.AutoSize = true;
      this.ckReplaceCrlfWithNewLine.Checked = true;
      this.ckReplaceCrlfWithNewLine.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckReplaceCrlfWithNewLine.Location = new System.Drawing.Point(574, 36);
      this.ckReplaceCrlfWithNewLine.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.ckReplaceCrlfWithNewLine.Name = "ckReplaceCrlfWithNewLine";
      this.ckReplaceCrlfWithNewLine.Size = new System.Drawing.Size(166, 17);
      this.ckReplaceCrlfWithNewLine.TabIndex = 3;
      this.ckReplaceCrlfWithNewLine.Text = "Replace CRLF with New Line";
      this.ckReplaceCrlfWithNewLine.UseVisualStyleBackColor = true;
      //
      // lblPageBreakText
      //
      this.lblPageBreakText.AutoSize = true;
      this.lblPageBreakText.Location = new System.Drawing.Point(297, 66);
      this.lblPageBreakText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblPageBreakText.Name = "lblPageBreakText";
      this.lblPageBreakText.Size = new System.Drawing.Size(87, 13);
      this.lblPageBreakText.TabIndex = 2;
      this.lblPageBreakText.Text = "Page Break Text";
      //
      // lblTextExtractionStrategy
      //
      this.lblTextExtractionStrategy.AutoSize = true;
      this.lblTextExtractionStrategy.Location = new System.Drawing.Point(297, 19);
      this.lblTextExtractionStrategy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblTextExtractionStrategy.Name = "lblTextExtractionStrategy";
      this.lblTextExtractionStrategy.Size = new System.Drawing.Size(120, 13);
      this.lblTextExtractionStrategy.TabIndex = 2;
      this.lblTextExtractionStrategy.Text = "Text Extraction Strategy";
      //
      // cboTextExtractionStrategy
      //
      this.cboTextExtractionStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTextExtractionStrategy.FormattingEnabled = true;
      this.cboTextExtractionStrategy.Items.AddRange(new object[] {
        "Location",
        "Simple"
      });
      this.cboTextExtractionStrategy.Location = new System.Drawing.Point(297, 36);
      this.cboTextExtractionStrategy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.cboTextExtractionStrategy.Name = "cboTextExtractionStrategy";
      this.cboTextExtractionStrategy.Size = new System.Drawing.Size(241, 21);
      this.cboTextExtractionStrategy.TabIndex = 1;
      //
      // btnDisplayXml
      //
      this.btnDisplayXml.Location = new System.Drawing.Point(135, 12);
      this.btnDisplayXml.Name = "btnDisplayXml";
      this.btnDisplayXml.Size = new System.Drawing.Size(117, 23);
      this.btnDisplayXml.TabIndex = 0;
      this.btnDisplayXml.Tag = "ShowXml";
      this.btnDisplayXml.Text = "Show XML";
      this.btnDisplayXml.UseVisualStyleBackColor = true;
      this.btnDisplayXml.Click += new System.EventHandler(this.Action);
      //
      // btnRun
      //
      this.btnRun.Location = new System.Drawing.Point(12, 12);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(117, 23);
      this.btnRun.TabIndex = 0;
      this.btnRun.Tag = "Run";
      this.btnRun.Text = "Run";
      this.btnRun.UseVisualStyleBackColor = true;
      this.btnRun.Click += new System.EventHandler(this.Action);
      //
      // txtOut
      //
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(0, 131);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1218, 524);
      this.txtOut.TabIndex = 3;
      this.txtOut.WordWrap = false;
      //
      // btnExtractImages
      //
      this.btnExtractImages.Location = new System.Drawing.Point(12, 66);
      this.btnExtractImages.Name = "btnExtractImages";
      this.btnExtractImages.Size = new System.Drawing.Size(117, 23);
      this.btnExtractImages.TabIndex = 0;
      this.btnExtractImages.Tag = "ExtractImages";
      this.btnExtractImages.Text = "Extract Images";
      this.btnExtractImages.UseVisualStyleBackColor = true;
      this.btnExtractImages.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1218, 679);
      this.Controls.Add(this.txtOut);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Pdf Extract";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnRun;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.Button btnDisplayXml;
    private System.Windows.Forms.Label lblTextExtractionStrategy;
    private System.Windows.Forms.ComboBox cboTextExtractionStrategy;
    private System.Windows.Forms.CheckBox ckReplaceCrlfWithNewLine;
    private System.Windows.Forms.TextBox txtPageBreak;
    private System.Windows.Forms.Label lblPageBreakText;
    private System.Windows.Forms.Button btnExtractImages;
  }
}


namespace Org.ImageTest
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
      this.btnPrev = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.cboImage = new System.Windows.Forms.ComboBox();
      this.btnGetText = new System.Windows.Forms.Button();
      this.lblOcr = new System.Windows.Forms.Label();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.lblQuickenTags = new System.Windows.Forms.Label();
      this.btnMatchOcrTextToTag = new System.Windows.Forms.Button();
      this.lbTags = new System.Windows.Forms.ListBox();
      this.pbMain = new System.Windows.Forms.PictureBox();
      this.lblClippedCheckImage = new System.Windows.Forms.Label();
      this.lblTagId = new System.Windows.Forms.Label();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageImages = new System.Windows.Forms.TabPage();
      this.tabPageTagMatch = new System.Windows.Forms.TabPage();
      this.lblScores = new System.Windows.Forms.Label();
      this.lblQuickenTag = new System.Windows.Forms.Label();
      this.lblOcrText = new System.Windows.Forms.Label();
      this.txtOcrMatch = new System.Windows.Forms.TextBox();
      this.txtQuickenTag = new System.Windows.Forms.TextBox();
      this.btnComputeScores = new System.Windows.Forms.Button();
      this.txtMatchOut = new System.Windows.Forms.TextBox();
      this.btnComputeMatchScores = new System.Windows.Forms.Button();
      this.txtComputedScores = new System.Windows.Forms.TextBox();
      this.ckComputeScores = new System.Windows.Forms.CheckBox();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.pnlMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
      this.tabMain.SuspendLayout();
      this.tabPageImages.SuspendLayout();
      this.tabPageTagMatch.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1003, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(0, 718);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1003, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.ckComputeScores);
      this.pnlTop.Controls.Add(this.btnPrev);
      this.pnlTop.Controls.Add(this.btnNext);
      this.pnlTop.Controls.Add(this.cboImage);
      this.pnlTop.Controls.Add(this.btnGetText);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1003, 37);
      this.pnlTop.TabIndex = 2;
      //
      // btnPrev
      //
      this.btnPrev.Location = new System.Drawing.Point(279, 7);
      this.btnPrev.Name = "btnPrev";
      this.btnPrev.Size = new System.Drawing.Size(75, 23);
      this.btnPrev.TabIndex = 2;
      this.btnPrev.Tag = "Prev";
      this.btnPrev.Text = "<= Prev";
      this.btnPrev.UseVisualStyleBackColor = true;
      this.btnPrev.Click += new System.EventHandler(this.Action);
      //
      // btnNext
      //
      this.btnNext.Location = new System.Drawing.Point(360, 7);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(75, 23);
      this.btnNext.TabIndex = 2;
      this.btnNext.Tag = "Next";
      this.btnNext.Text = "Next =>";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.Action);
      //
      // cboImage
      //
      this.cboImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboImage.FormattingEnabled = true;
      this.cboImage.Location = new System.Drawing.Point(106, 8);
      this.cboImage.Name = "cboImage";
      this.cboImage.Size = new System.Drawing.Size(168, 21);
      this.cboImage.TabIndex = 1;
      this.cboImage.SelectedIndexChanged += new System.EventHandler(this.cboImage_SelectedIndexChanged);
      //
      // btnGetText
      //
      this.btnGetText.Location = new System.Drawing.Point(6, 7);
      this.btnGetText.Name = "btnGetText";
      this.btnGetText.Size = new System.Drawing.Size(95, 23);
      this.btnGetText.TabIndex = 0;
      this.btnGetText.Tag = "GetOCRText";
      this.btnGetText.Text = "Get OCR Text";
      this.btnGetText.UseVisualStyleBackColor = true;
      this.btnGetText.Click += new System.EventHandler(this.Action);
      //
      // lblOcr
      //
      this.lblOcr.BackColor = System.Drawing.Color.White;
      this.lblOcr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblOcr.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOcr.Location = new System.Drawing.Point(12, 289);
      this.lblOcr.Name = "lblOcr";
      this.lblOcr.Padding = new System.Windows.Forms.Padding(3, 0, 0, 2);
      this.lblOcr.Size = new System.Drawing.Size(1163, 26);
      this.lblOcr.TabIndex = 3;
      this.lblOcr.Text = "OCR";
      this.lblOcr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lblOcr.TextChanged += new System.EventHandler(this.MatchElementsTextChanged);
      //
      // pnlMain
      //
      this.pnlMain.BackColor = System.Drawing.SystemColors.Control;
      this.pnlMain.Controls.Add(this.txtComputedScores);
      this.pnlMain.Controls.Add(this.lblOcr);
      this.pnlMain.Controls.Add(this.lblTagId);
      this.pnlMain.Controls.Add(this.lblQuickenTags);
      this.pnlMain.Controls.Add(this.lblClippedCheckImage);
      this.pnlMain.Controls.Add(this.btnComputeMatchScores);
      this.pnlMain.Controls.Add(this.btnMatchOcrTextToTag);
      this.pnlMain.Controls.Add(this.lbTags);
      this.pnlMain.Controls.Add(this.pbMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(3, 3);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(989, 625);
      this.pnlMain.TabIndex = 3;
      //
      // lblQuickenTags
      //
      this.lblQuickenTags.AutoSize = true;
      this.lblQuickenTags.Location = new System.Drawing.Point(524, 12);
      this.lblQuickenTags.Name = "lblQuickenTags";
      this.lblQuickenTags.Size = new System.Drawing.Size(74, 13);
      this.lblQuickenTags.TabIndex = 2;
      this.lblQuickenTags.Text = "Quicken Tags";
      //
      // btnMatchOcrTextToTag
      //
      this.btnMatchOcrTextToTag.Location = new System.Drawing.Point(12, 326);
      this.btnMatchOcrTextToTag.Name = "btnMatchOcrTextToTag";
      this.btnMatchOcrTextToTag.Size = new System.Drawing.Size(168, 23);
      this.btnMatchOcrTextToTag.TabIndex = 0;
      this.btnMatchOcrTextToTag.Tag = "MatchOcrTextToTag";
      this.btnMatchOcrTextToTag.Text = "Match OCR Text toTag";
      this.btnMatchOcrTextToTag.UseVisualStyleBackColor = true;
      this.btnMatchOcrTextToTag.Click += new System.EventHandler(this.Action);
      //
      // lbTags
      //
      this.lbTags.FormattingEnabled = true;
      this.lbTags.Location = new System.Drawing.Point(527, 30);
      this.lbTags.Name = "lbTags";
      this.lbTags.Size = new System.Drawing.Size(411, 251);
      this.lbTags.TabIndex = 1;
      this.lbTags.SelectedIndexChanged += new System.EventHandler(this.MatchElementsTextChanged);
      //
      // pbMain
      //
      this.pbMain.BackColor = System.Drawing.Color.White;
      this.pbMain.Location = new System.Drawing.Point(12, 30);
      this.pbMain.Name = "pbMain";
      this.pbMain.Size = new System.Drawing.Size(495, 244);
      this.pbMain.TabIndex = 0;
      this.pbMain.TabStop = false;
      //
      // lblClippedCheckImage
      //
      this.lblClippedCheckImage.AutoSize = true;
      this.lblClippedCheckImage.Location = new System.Drawing.Point(12, 12);
      this.lblClippedCheckImage.Name = "lblClippedCheckImage";
      this.lblClippedCheckImage.Size = new System.Drawing.Size(108, 13);
      this.lblClippedCheckImage.TabIndex = 2;
      this.lblClippedCheckImage.Text = "Clipped Check Image";
      //
      // lblTagId
      //
      this.lblTagId.Location = new System.Drawing.Point(833, 12);
      this.lblTagId.Name = "lblTagId";
      this.lblTagId.Size = new System.Drawing.Size(105, 15);
      this.lblTagId.TabIndex = 2;
      this.lblTagId.Text = "TagID:";
      this.lblTagId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageImages);
      this.tabMain.Controls.Add(this.tabPageTagMatch);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(125, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 61);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1003, 657);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 4;
      //
      // tabPageImages
      //
      this.tabPageImages.Controls.Add(this.pnlMain);
      this.tabPageImages.Location = new System.Drawing.Point(4, 22);
      this.tabPageImages.Name = "tabPageImages";
      this.tabPageImages.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageImages.Size = new System.Drawing.Size(995, 631);
      this.tabPageImages.TabIndex = 0;
      this.tabPageImages.Text = "Images";
      this.tabPageImages.UseVisualStyleBackColor = true;
      //
      // tabPageTagMatch
      //
      this.tabPageTagMatch.Controls.Add(this.txtMatchOut);
      this.tabPageTagMatch.Controls.Add(this.lblScores);
      this.tabPageTagMatch.Controls.Add(this.lblQuickenTag);
      this.tabPageTagMatch.Controls.Add(this.lblOcrText);
      this.tabPageTagMatch.Controls.Add(this.txtOcrMatch);
      this.tabPageTagMatch.Controls.Add(this.txtQuickenTag);
      this.tabPageTagMatch.Controls.Add(this.btnComputeScores);
      this.tabPageTagMatch.Location = new System.Drawing.Point(4, 22);
      this.tabPageTagMatch.Name = "tabPageTagMatch";
      this.tabPageTagMatch.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageTagMatch.Size = new System.Drawing.Size(995, 631);
      this.tabPageTagMatch.TabIndex = 1;
      this.tabPageTagMatch.Text = "Tag Matching";
      this.tabPageTagMatch.UseVisualStyleBackColor = true;
      //
      // lblScores
      //
      this.lblScores.AutoSize = true;
      this.lblScores.Location = new System.Drawing.Point(697, 86);
      this.lblScores.Name = "lblScores";
      this.lblScores.Size = new System.Drawing.Size(38, 13);
      this.lblScores.TabIndex = 10;
      this.lblScores.Text = "Score:";
      //
      // lblQuickenTag
      //
      this.lblQuickenTag.AutoSize = true;
      this.lblQuickenTag.Location = new System.Drawing.Point(19, 20);
      this.lblQuickenTag.Name = "lblQuickenTag";
      this.lblQuickenTag.Size = new System.Drawing.Size(69, 13);
      this.lblQuickenTag.TabIndex = 11;
      this.lblQuickenTag.Text = "Quicken Tag";
      //
      // lblOcrText
      //
      this.lblOcrText.AutoSize = true;
      this.lblOcrText.Location = new System.Drawing.Point(19, 67);
      this.lblOcrText.Name = "lblOcrText";
      this.lblOcrText.Size = new System.Drawing.Size(56, 13);
      this.lblOcrText.TabIndex = 12;
      this.lblOcrText.Text = "From OCR";
      //
      // txtOcrMatch
      //
      this.txtOcrMatch.Location = new System.Drawing.Point(19, 83);
      this.txtOcrMatch.Name = "txtOcrMatch";
      this.txtOcrMatch.Size = new System.Drawing.Size(534, 20);
      this.txtOcrMatch.TabIndex = 9;
      //
      // txtQuickenTag
      //
      this.txtQuickenTag.Location = new System.Drawing.Point(19, 36);
      this.txtQuickenTag.Name = "txtQuickenTag";
      this.txtQuickenTag.Size = new System.Drawing.Size(534, 20);
      this.txtQuickenTag.TabIndex = 8;
      //
      // btnComputeScores
      //
      this.btnComputeScores.Location = new System.Drawing.Point(559, 81);
      this.btnComputeScores.Name = "btnComputeScores";
      this.btnComputeScores.Size = new System.Drawing.Size(129, 23);
      this.btnComputeScores.TabIndex = 7;
      this.btnComputeScores.Tag = "ComputeScores";
      this.btnComputeScores.Text = "Compute Scores";
      this.btnComputeScores.UseVisualStyleBackColor = true;
      this.btnComputeScores.Click += new System.EventHandler(this.Action);
      //
      // txtMatchOut
      //
      this.txtMatchOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                 | System.Windows.Forms.AnchorStyles.Left)
                                 | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMatchOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtMatchOut.Location = new System.Drawing.Point(8, 132);
      this.txtMatchOut.Multiline = true;
      this.txtMatchOut.Name = "txtMatchOut";
      this.txtMatchOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtMatchOut.Size = new System.Drawing.Size(979, 496);
      this.txtMatchOut.TabIndex = 13;
      //
      // btnComputeMatchScores
      //
      this.btnComputeMatchScores.Location = new System.Drawing.Point(186, 326);
      this.btnComputeMatchScores.Name = "btnComputeMatchScores";
      this.btnComputeMatchScores.Size = new System.Drawing.Size(168, 23);
      this.btnComputeMatchScores.TabIndex = 0;
      this.btnComputeMatchScores.Tag = "ComputeMatchScores";
      this.btnComputeMatchScores.Text = "Compute Match Scores";
      this.btnComputeMatchScores.UseVisualStyleBackColor = true;
      this.btnComputeMatchScores.Click += new System.EventHandler(this.Action);
      //
      // txtComputedScores
      //
      this.txtComputedScores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                       | System.Windows.Forms.AnchorStyles.Left)
                                       | System.Windows.Forms.AnchorStyles.Right)));
      this.txtComputedScores.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtComputedScores.Location = new System.Drawing.Point(12, 355);
      this.txtComputedScores.Multiline = true;
      this.txtComputedScores.Name = "txtComputedScores";
      this.txtComputedScores.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtComputedScores.Size = new System.Drawing.Size(961, 267);
      this.txtComputedScores.TabIndex = 14;
      //
      // ckComputeScores
      //
      this.ckComputeScores.AutoSize = true;
      this.ckComputeScores.Checked = true;
      this.ckComputeScores.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckComputeScores.Location = new System.Drawing.Point(445, 11);
      this.ckComputeScores.Name = "ckComputeScores";
      this.ckComputeScores.Size = new System.Drawing.Size(104, 17);
      this.ckComputeScores.TabIndex = 3;
      this.ckComputeScores.Text = "Compute Scores";
      this.ckComputeScores.UseVisualStyleBackColor = true;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1003, 741);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Image Test";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      this.pnlMain.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
      this.tabMain.ResumeLayout(false);
      this.tabPageImages.ResumeLayout(false);
      this.tabPageTagMatch.ResumeLayout(false);
      this.tabPageTagMatch.PerformLayout();
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
    private System.Windows.Forms.PictureBox pbMain;
    private System.Windows.Forms.ComboBox cboImage;
    private System.Windows.Forms.Button btnPrev;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnGetText;
    private System.Windows.Forms.Label lblOcr;
    private System.Windows.Forms.Label lblQuickenTags;
    private System.Windows.Forms.ListBox lbTags;
    private System.Windows.Forms.Button btnMatchOcrTextToTag;
    private System.Windows.Forms.Label lblTagId;
    private System.Windows.Forms.Label lblClippedCheckImage;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageImages;
    private System.Windows.Forms.TabPage tabPageTagMatch;
    private System.Windows.Forms.TextBox txtMatchOut;
    private System.Windows.Forms.Label lblScores;
    private System.Windows.Forms.Label lblQuickenTag;
    private System.Windows.Forms.Label lblOcrText;
    private System.Windows.Forms.TextBox txtOcrMatch;
    private System.Windows.Forms.TextBox txtQuickenTag;
    private System.Windows.Forms.Button btnComputeScores;
    private System.Windows.Forms.Button btnComputeMatchScores;
    private System.Windows.Forms.TextBox txtComputedScores;
    private System.Windows.Forms.CheckBox ckComputeScores;
  }
}


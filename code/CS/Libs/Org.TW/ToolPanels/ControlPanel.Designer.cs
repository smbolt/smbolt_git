namespace Org.TW.ToolPanels
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
      this.btnTestHyphenation = new System.Windows.Forms.Button();
      this.lblHyphenationTest = new System.Windows.Forms.Label();
      this.ckPrintToImage = new System.Windows.Forms.CheckBox();
      this.ckCreateDocument = new System.Windows.Forms.CheckBox();
      this.ckShowXmlMap = new System.Windows.Forms.CheckBox();
      this.ckShowMap = new System.Windows.Forms.CheckBox();
      this.ckShowScale = new System.Windows.Forms.CheckBox();
      this.ckIncludeProperties = new System.Windows.Forms.CheckBox();
      this.lblHyphenatedWord = new System.Windows.Forms.Label();
      this.lblRegions = new System.Windows.Forms.Label();
      this.txtHyphenate = new System.Windows.Forms.TextBox();
      this.lstRegions = new System.Windows.Forms.ListBox();
      this.btnRefresh = new System.Windows.Forms.Button();
      this.btnDrawOnOverlay = new System.Windows.Forms.Button();
      this.numDiagnosticsLevel = new System.Windows.Forms.NumericUpDown();
      this.btnClearOverlay = new System.Windows.Forms.Button();
      this.lblDiagnosticsLevel = new System.Windows.Forms.Label();
      this.numDocuments = new System.Windows.Forms.NumericUpDown();
      this.ckDiagnosticsMode = new System.Windows.Forms.CheckBox();
      this.lblDocumentCount = new System.Windows.Forms.Label();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageControl = new System.Windows.Forms.TabPage();
      this.txtSpaceWidthFactor = new System.Windows.Forms.TextBox();
      this.lblSpaceWidthFactor = new System.Windows.Forms.Label();
      this.txtLineFactor = new System.Windows.Forms.TextBox();
      this.lblLineFactor = new System.Windows.Forms.Label();
      this.txtWidthFactor = new System.Windows.Forms.TextBox();
      this.lblWidthFactor = new System.Windows.Forms.Label();
      this.tabPageRegions = new System.Windows.Forms.TabPage();
      this.tabPageHyphenation = new System.Windows.Forms.TabPage();
      ((System.ComponentModel.ISupportInitialize)(this.numDiagnosticsLevel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numDocuments)).BeginInit();
      this.tabMain.SuspendLayout();
      this.tabPageControl.SuspendLayout();
      this.tabPageRegions.SuspendLayout();
      this.tabPageHyphenation.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnTestHyphenation
      // 
      this.btnTestHyphenation.Location = new System.Drawing.Point(129, 24);
      this.btnTestHyphenation.Name = "btnTestHyphenation";
      this.btnTestHyphenation.Size = new System.Drawing.Size(49, 23);
      this.btnTestHyphenation.TabIndex = 31;
      this.btnTestHyphenation.Tag = "HYPHENATE";
      this.btnTestHyphenation.Text = "Go";
      this.btnTestHyphenation.UseVisualStyleBackColor = true;
      // 
      // lblHyphenationTest
      // 
      this.lblHyphenationTest.AutoSize = true;
      this.lblHyphenationTest.Location = new System.Drawing.Point(32, 29);
      this.lblHyphenationTest.Name = "lblHyphenationTest";
      this.lblHyphenationTest.Size = new System.Drawing.Size(91, 13);
      this.lblHyphenationTest.TabIndex = 30;
      this.lblHyphenationTest.Text = "Test Hyphenation";
      // 
      // ckPrintToImage
      // 
      this.ckPrintToImage.AutoSize = true;
      this.ckPrintToImage.Location = new System.Drawing.Point(15, 187);
      this.ckPrintToImage.Name = "ckPrintToImage";
      this.ckPrintToImage.Size = new System.Drawing.Size(98, 17);
      this.ckPrintToImage.TabIndex = 22;
      this.ckPrintToImage.Text = "Print with Word";
      this.ckPrintToImage.UseVisualStyleBackColor = true;
      // 
      // ckCreateDocument
      // 
      this.ckCreateDocument.AutoSize = true;
      this.ckCreateDocument.Checked = true;
      this.ckCreateDocument.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckCreateDocument.Location = new System.Drawing.Point(16, 164);
      this.ckCreateDocument.Name = "ckCreateDocument";
      this.ckCreateDocument.Size = new System.Drawing.Size(109, 17);
      this.ckCreateDocument.TabIndex = 21;
      this.ckCreateDocument.Text = "Create Document";
      this.ckCreateDocument.UseVisualStyleBackColor = true;
      // 
      // ckShowXmlMap
      // 
      this.ckShowXmlMap.AutoSize = true;
      this.ckShowXmlMap.Location = new System.Drawing.Point(16, 141);
      this.ckShowXmlMap.Name = "ckShowXmlMap";
      this.ckShowXmlMap.Size = new System.Drawing.Size(97, 17);
      this.ckShowXmlMap.TabIndex = 20;
      this.ckShowXmlMap.Text = "Show Xml Map";
      this.ckShowXmlMap.UseVisualStyleBackColor = true;
      // 
      // ckShowMap
      // 
      this.ckShowMap.AutoSize = true;
      this.ckShowMap.Location = new System.Drawing.Point(16, 118);
      this.ckShowMap.Name = "ckShowMap";
      this.ckShowMap.Size = new System.Drawing.Size(77, 17);
      this.ckShowMap.TabIndex = 25;
      this.ckShowMap.Text = "Show Map";
      this.ckShowMap.UseVisualStyleBackColor = true;
      // 
      // ckShowScale
      // 
      this.ckShowScale.AutoSize = true;
      this.ckShowScale.Checked = true;
      this.ckShowScale.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckShowScale.Location = new System.Drawing.Point(16, 72);
      this.ckShowScale.Name = "ckShowScale";
      this.ckShowScale.Size = new System.Drawing.Size(83, 17);
      this.ckShowScale.TabIndex = 26;
      this.ckShowScale.Text = "Show Scale";
      this.ckShowScale.UseVisualStyleBackColor = true;
      // 
      // ckIncludeProperties
      // 
      this.ckIncludeProperties.AutoSize = true;
      this.ckIncludeProperties.Location = new System.Drawing.Point(16, 95);
      this.ckIncludeProperties.Name = "ckIncludeProperties";
      this.ckIncludeProperties.Size = new System.Drawing.Size(111, 17);
      this.ckIncludeProperties.TabIndex = 24;
      this.ckIncludeProperties.Text = "Include Properties";
      this.ckIncludeProperties.UseVisualStyleBackColor = true;
      // 
      // lblHyphenatedWord
      // 
      this.lblHyphenatedWord.BackColor = System.Drawing.Color.White;
      this.lblHyphenatedWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblHyphenatedWord.Location = new System.Drawing.Point(28, 76);
      this.lblHyphenatedWord.Name = "lblHyphenatedWord";
      this.lblHyphenatedWord.Size = new System.Drawing.Size(149, 18);
      this.lblHyphenatedWord.TabIndex = 18;
      // 
      // lblRegions
      // 
      this.lblRegions.AutoSize = true;
      this.lblRegions.Location = new System.Drawing.Point(23, 16);
      this.lblRegions.Name = "lblRegions";
      this.lblRegions.Size = new System.Drawing.Size(46, 13);
      this.lblRegions.TabIndex = 29;
      this.lblRegions.Text = "Regions";
      // 
      // txtHyphenate
      // 
      this.txtHyphenate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtHyphenate.Location = new System.Drawing.Point(28, 53);
      this.txtHyphenate.Name = "txtHyphenate";
      this.txtHyphenate.Size = new System.Drawing.Size(150, 20);
      this.txtHyphenate.TabIndex = 27;
      // 
      // lstRegions
      // 
      this.lstRegions.FormattingEnabled = true;
      this.lstRegions.Location = new System.Drawing.Point(23, 35);
      this.lstRegions.Name = "lstRegions";
      this.lstRegions.Size = new System.Drawing.Size(174, 121);
      this.lstRegions.TabIndex = 28;
      // 
      // btnRefresh
      // 
      this.btnRefresh.Location = new System.Drawing.Point(22, 162);
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new System.Drawing.Size(82, 23);
      this.btnRefresh.TabIndex = 13;
      this.btnRefresh.Tag = "REFRESH_IMAGE";
      this.btnRefresh.Text = "Refresh";
      this.btnRefresh.UseVisualStyleBackColor = true;
      // 
      // btnDrawOnOverlay
      // 
      this.btnDrawOnOverlay.Location = new System.Drawing.Point(115, 162);
      this.btnDrawOnOverlay.Name = "btnDrawOnOverlay";
      this.btnDrawOnOverlay.Size = new System.Drawing.Size(82, 23);
      this.btnDrawOnOverlay.TabIndex = 12;
      this.btnDrawOnOverlay.Tag = "MARK_OVERLAY";
      this.btnDrawOnOverlay.Text = "Mark Overlay";
      this.btnDrawOnOverlay.UseVisualStyleBackColor = true;
      // 
      // numDiagnosticsLevel
      // 
      this.numDiagnosticsLevel.Location = new System.Drawing.Point(50, 25);
      this.numDiagnosticsLevel.Name = "numDiagnosticsLevel";
      this.numDiagnosticsLevel.Size = new System.Drawing.Size(49, 20);
      this.numDiagnosticsLevel.TabIndex = 16;
      this.numDiagnosticsLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // btnClearOverlay
      // 
      this.btnClearOverlay.Location = new System.Drawing.Point(115, 191);
      this.btnClearOverlay.Name = "btnClearOverlay";
      this.btnClearOverlay.Size = new System.Drawing.Size(82, 23);
      this.btnClearOverlay.TabIndex = 14;
      this.btnClearOverlay.Tag = "CLEAR_OVERLAY";
      this.btnClearOverlay.Text = "Clear Overlay";
      this.btnClearOverlay.UseVisualStyleBackColor = true;
      // 
      // lblDiagnosticsLevel
      // 
      this.lblDiagnosticsLevel.AutoSize = true;
      this.lblDiagnosticsLevel.Location = new System.Drawing.Point(14, 28);
      this.lblDiagnosticsLevel.Name = "lblDiagnosticsLevel";
      this.lblDiagnosticsLevel.Size = new System.Drawing.Size(33, 13);
      this.lblDiagnosticsLevel.TabIndex = 23;
      this.lblDiagnosticsLevel.Text = "Level";
      // 
      // numDocuments
      // 
      this.numDocuments.Location = new System.Drawing.Point(80, 230);
      this.numDocuments.Name = "numDocuments";
      this.numDocuments.Size = new System.Drawing.Size(49, 20);
      this.numDocuments.TabIndex = 15;
      this.numDocuments.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numDocuments.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
      // 
      // ckDiagnosticsMode
      // 
      this.ckDiagnosticsMode.AutoSize = true;
      this.ckDiagnosticsMode.Checked = true;
      this.ckDiagnosticsMode.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckDiagnosticsMode.Location = new System.Drawing.Point(16, 49);
      this.ckDiagnosticsMode.Name = "ckDiagnosticsMode";
      this.ckDiagnosticsMode.Size = new System.Drawing.Size(111, 17);
      this.ckDiagnosticsMode.TabIndex = 19;
      this.ckDiagnosticsMode.Text = "Diagnostics Mode";
      this.ckDiagnosticsMode.UseVisualStyleBackColor = true;
      // 
      // lblDocumentCount
      // 
      this.lblDocumentCount.AutoSize = true;
      this.lblDocumentCount.Location = new System.Drawing.Point(14, 234);
      this.lblDocumentCount.Name = "lblDocumentCount";
      this.lblDocumentCount.Size = new System.Drawing.Size(61, 13);
      this.lblDocumentCount.TabIndex = 17;
      this.lblDocumentCount.Text = "Documents";
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageControl);
      this.tabMain.Controls.Add(this.tabPageRegions);
      this.tabMain.Controls.Add(this.tabPageHyphenation);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(226, 591);
      this.tabMain.TabIndex = 32;
      // 
      // tabPageControl
      // 
      this.tabPageControl.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageControl.Controls.Add(this.txtSpaceWidthFactor);
      this.tabPageControl.Controls.Add(this.lblSpaceWidthFactor);
      this.tabPageControl.Controls.Add(this.txtLineFactor);
      this.tabPageControl.Controls.Add(this.lblLineFactor);
      this.tabPageControl.Controls.Add(this.txtWidthFactor);
      this.tabPageControl.Controls.Add(this.lblWidthFactor);
      this.tabPageControl.Controls.Add(this.ckDiagnosticsMode);
      this.tabPageControl.Controls.Add(this.numDocuments);
      this.tabPageControl.Controls.Add(this.lblDiagnosticsLevel);
      this.tabPageControl.Controls.Add(this.lblDocumentCount);
      this.tabPageControl.Controls.Add(this.numDiagnosticsLevel);
      this.tabPageControl.Controls.Add(this.ckPrintToImage);
      this.tabPageControl.Controls.Add(this.ckIncludeProperties);
      this.tabPageControl.Controls.Add(this.ckCreateDocument);
      this.tabPageControl.Controls.Add(this.ckShowScale);
      this.tabPageControl.Controls.Add(this.ckShowXmlMap);
      this.tabPageControl.Controls.Add(this.ckShowMap);
      this.tabPageControl.Location = new System.Drawing.Point(4, 22);
      this.tabPageControl.Name = "tabPageControl";
      this.tabPageControl.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageControl.Size = new System.Drawing.Size(216, 565);
      this.tabPageControl.TabIndex = 0;
      this.tabPageControl.Text = "Control";
      // 
      // txtSpaceWidthFactor
      // 
      this.txtSpaceWidthFactor.Location = new System.Drawing.Point(121, 340);
      this.txtSpaceWidthFactor.Name = "txtSpaceWidthFactor";
      this.txtSpaceWidthFactor.Size = new System.Drawing.Size(49, 20);
      this.txtSpaceWidthFactor.TabIndex = 32;
      this.txtSpaceWidthFactor.Text = "0.250";
      this.txtSpaceWidthFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // lblSpaceWidthFactor
      // 
      this.lblSpaceWidthFactor.AutoSize = true;
      this.lblSpaceWidthFactor.Location = new System.Drawing.Point(15, 342);
      this.lblSpaceWidthFactor.Name = "lblSpaceWidthFactor";
      this.lblSpaceWidthFactor.Size = new System.Drawing.Size(102, 13);
      this.lblSpaceWidthFactor.TabIndex = 31;
      this.lblSpaceWidthFactor.Text = "Space Width Factor";
      // 
      // txtLineFactor
      // 
      this.txtLineFactor.Location = new System.Drawing.Point(121, 312);
      this.txtLineFactor.Name = "txtLineFactor";
      this.txtLineFactor.Size = new System.Drawing.Size(49, 20);
      this.txtLineFactor.TabIndex = 30;
      this.txtLineFactor.Text = "0.975";
      this.txtLineFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // lblLineFactor
      // 
      this.lblLineFactor.AutoSize = true;
      this.lblLineFactor.Location = new System.Drawing.Point(15, 314);
      this.lblLineFactor.Name = "lblLineFactor";
      this.lblLineFactor.Size = new System.Drawing.Size(60, 13);
      this.lblLineFactor.TabIndex = 29;
      this.lblLineFactor.Text = "Line Factor";
      // 
      // txtWidthFactor
      // 
      this.txtWidthFactor.Location = new System.Drawing.Point(121, 282);
      this.txtWidthFactor.Name = "txtWidthFactor";
      this.txtWidthFactor.Size = new System.Drawing.Size(49, 20);
      this.txtWidthFactor.TabIndex = 28;
      this.txtWidthFactor.Text = "0.220";
      this.txtWidthFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // lblWidthFactor
      // 
      this.lblWidthFactor.AutoSize = true;
      this.lblWidthFactor.Location = new System.Drawing.Point(14, 286);
      this.lblWidthFactor.Name = "lblWidthFactor";
      this.lblWidthFactor.Size = new System.Drawing.Size(68, 13);
      this.lblWidthFactor.TabIndex = 27;
      this.lblWidthFactor.Text = "Width Factor";
      // 
      // tabPageRegions
      // 
      this.tabPageRegions.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageRegions.Controls.Add(this.lstRegions);
      this.tabPageRegions.Controls.Add(this.btnClearOverlay);
      this.tabPageRegions.Controls.Add(this.btnDrawOnOverlay);
      this.tabPageRegions.Controls.Add(this.btnRefresh);
      this.tabPageRegions.Controls.Add(this.lblRegions);
      this.tabPageRegions.Location = new System.Drawing.Point(4, 22);
      this.tabPageRegions.Name = "tabPageRegions";
      this.tabPageRegions.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageRegions.Size = new System.Drawing.Size(218, 565);
      this.tabPageRegions.TabIndex = 1;
      this.tabPageRegions.Text = "Regions";
      // 
      // tabPageHyphenation
      // 
      this.tabPageHyphenation.BackColor = System.Drawing.SystemColors.Control;
      this.tabPageHyphenation.Controls.Add(this.lblHyphenationTest);
      this.tabPageHyphenation.Controls.Add(this.btnTestHyphenation);
      this.tabPageHyphenation.Controls.Add(this.txtHyphenate);
      this.tabPageHyphenation.Controls.Add(this.lblHyphenatedWord);
      this.tabPageHyphenation.Location = new System.Drawing.Point(4, 22);
      this.tabPageHyphenation.Name = "tabPageHyphenation";
      this.tabPageHyphenation.Size = new System.Drawing.Size(216, 565);
      this.tabPageHyphenation.TabIndex = 2;
      this.tabPageHyphenation.Text = "Hyphenation";
      // 
      // ControlPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tabMain);
      this.Name = "ControlPanel";
      this.Size = new System.Drawing.Size(226, 591);
      this.Tag = "ToolPanel_ControlPanel";
      ((System.ComponentModel.ISupportInitialize)(this.numDiagnosticsLevel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numDocuments)).EndInit();
      this.tabMain.ResumeLayout(false);
      this.tabPageControl.ResumeLayout(false);
      this.tabPageControl.PerformLayout();
      this.tabPageRegions.ResumeLayout(false);
      this.tabPageRegions.PerformLayout();
      this.tabPageHyphenation.ResumeLayout(false);
      this.tabPageHyphenation.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnTestHyphenation;
    private System.Windows.Forms.Label lblHyphenationTest;
    private System.Windows.Forms.CheckBox ckPrintToImage;
    private System.Windows.Forms.CheckBox ckCreateDocument;
    private System.Windows.Forms.CheckBox ckShowXmlMap;
    private System.Windows.Forms.CheckBox ckShowMap;
    private System.Windows.Forms.CheckBox ckShowScale;
    private System.Windows.Forms.CheckBox ckIncludeProperties;
    private System.Windows.Forms.Label lblHyphenatedWord;
    private System.Windows.Forms.Label lblRegions;
    private System.Windows.Forms.TextBox txtHyphenate;
    private System.Windows.Forms.ListBox lstRegions;
    private System.Windows.Forms.Button btnRefresh;
    private System.Windows.Forms.Button btnDrawOnOverlay;
    private System.Windows.Forms.NumericUpDown numDiagnosticsLevel;
    private System.Windows.Forms.Button btnClearOverlay;
    private System.Windows.Forms.Label lblDiagnosticsLevel;
    private System.Windows.Forms.NumericUpDown numDocuments;
    private System.Windows.Forms.CheckBox ckDiagnosticsMode;
    private System.Windows.Forms.Label lblDocumentCount;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageControl;
    private System.Windows.Forms.TextBox txtWidthFactor;
    private System.Windows.Forms.Label lblWidthFactor;
    private System.Windows.Forms.TabPage tabPageRegions;
    private System.Windows.Forms.TabPage tabPageHyphenation;
    private System.Windows.Forms.TextBox txtLineFactor;
    private System.Windows.Forms.Label lblLineFactor;
    private System.Windows.Forms.TextBox txtSpaceWidthFactor;
    private System.Windows.Forms.Label lblSpaceWidthFactor;
  }
}

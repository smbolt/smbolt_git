namespace Org.XsdTest
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
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblFontSize = new System.Windows.Forms.Label();
      this.udFontSize = new System.Windows.Forms.NumericUpDown();
      this.btnReloadXsd = new System.Windows.Forms.Button();
      this.btnFormatXsd = new System.Windows.Forms.Button();
      this.btnSaveXsd = new System.Windows.Forms.Button();
      this.btnSaveXml = new System.Windows.Forms.Button();
      this.btnFormatXml = new System.Windows.Forms.Button();
      this.btnReloadXml = new System.Windows.Forms.Button();
      this.btnRunValidation = new System.Windows.Forms.Button();
      this.lblXsdFile = new System.Windows.Forms.Label();
      this.cboXsdFile = new System.Windows.Forms.ComboBox();
      this.lblXmlFile = new System.Windows.Forms.Label();
      this.cboXmlFile = new System.Windows.Forms.ComboBox();
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageXml = new System.Windows.Forms.TabPage();
      this.txtXml = new FastColoredTextBoxNS.FastColoredTextBox();
      this.lblXmlFileMessage = new System.Windows.Forms.Label();
      this.tabPageXsd = new System.Windows.Forms.TabPage();
      this.txtXsd = new FastColoredTextBoxNS.FastColoredTextBox();
      this.lblXsdFileMessage = new System.Windows.Forms.Label();
      this.tabPageValidationResults = new System.Windows.Forms.TabPage();
      this.txtValidation = new FastColoredTextBoxNS.FastColoredTextBox();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).BeginInit();
      this.pnlMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageXml.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtXml)).BeginInit();
      this.tabPageXsd.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtXsd)).BeginInit();
      this.tabPageValidationResults.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtValidation)).BeginInit();
      this.mnuMain.SuspendLayout();
      this.SuspendLayout();
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.lblFontSize);
      this.pnlTop.Controls.Add(this.udFontSize);
      this.pnlTop.Controls.Add(this.btnReloadXsd);
      this.pnlTop.Controls.Add(this.btnFormatXsd);
      this.pnlTop.Controls.Add(this.btnSaveXsd);
      this.pnlTop.Controls.Add(this.btnSaveXml);
      this.pnlTop.Controls.Add(this.btnFormatXml);
      this.pnlTop.Controls.Add(this.btnReloadXml);
      this.pnlTop.Controls.Add(this.btnRunValidation);
      this.pnlTop.Controls.Add(this.lblXsdFile);
      this.pnlTop.Controls.Add(this.cboXsdFile);
      this.pnlTop.Controls.Add(this.lblXmlFile);
      this.pnlTop.Controls.Add(this.cboXmlFile);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1047, 126);
      this.pnlTop.TabIndex = 0;
      //
      // lblFontSize
      //
      this.lblFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblFontSize.AutoSize = true;
      this.lblFontSize.Location = new System.Drawing.Point(948, 103);
      this.lblFontSize.Name = "lblFontSize";
      this.lblFontSize.Size = new System.Drawing.Size(51, 13);
      this.lblFontSize.TabIndex = 4;
      this.lblFontSize.Text = "Font Size";
      //
      // udFontSize
      //
      this.udFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.udFontSize.Location = new System.Drawing.Point(999, 100);
      this.udFontSize.Maximum = new decimal(new int[] {
        14,
        0,
        0,
        0
      });
      this.udFontSize.Minimum = new decimal(new int[] {
        7,
        0,
        0,
        0
      });
      this.udFontSize.Name = "udFontSize";
      this.udFontSize.Size = new System.Drawing.Size(36, 20);
      this.udFontSize.TabIndex = 3;
      this.udFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.udFontSize.Value = new decimal(new int[] {
        9,
        0,
        0,
        0
      });
      this.udFontSize.ValueChanged += new System.EventHandler(this.udFontSize_ValueChanged);
      //
      // btnReloadXsd
      //
      this.btnReloadXsd.Location = new System.Drawing.Point(359, 80);
      this.btnReloadXsd.Name = "btnReloadXsd";
      this.btnReloadXsd.Size = new System.Drawing.Size(101, 23);
      this.btnReloadXsd.TabIndex = 2;
      this.btnReloadXsd.Tag = "ReloadXsd";
      this.btnReloadXsd.Text = "Reload XSD";
      this.btnReloadXsd.UseVisualStyleBackColor = true;
      this.btnReloadXsd.Click += new System.EventHandler(this.Action);
      //
      // btnFormatXsd
      //
      this.btnFormatXsd.Location = new System.Drawing.Point(466, 80);
      this.btnFormatXsd.Name = "btnFormatXsd";
      this.btnFormatXsd.Size = new System.Drawing.Size(101, 23);
      this.btnFormatXsd.TabIndex = 2;
      this.btnFormatXsd.Tag = "FormatXsd";
      this.btnFormatXsd.Text = "Format XSD";
      this.btnFormatXsd.UseVisualStyleBackColor = true;
      this.btnFormatXsd.Click += new System.EventHandler(this.Action);
      //
      // btnSaveXsd
      //
      this.btnSaveXsd.Location = new System.Drawing.Point(573, 80);
      this.btnSaveXsd.Name = "btnSaveXsd";
      this.btnSaveXsd.Size = new System.Drawing.Size(101, 23);
      this.btnSaveXsd.TabIndex = 2;
      this.btnSaveXsd.Tag = "SaveXsd";
      this.btnSaveXsd.Text = "Save XSD";
      this.btnSaveXsd.UseVisualStyleBackColor = true;
      this.btnSaveXsd.Click += new System.EventHandler(this.Action);
      //
      // btnSaveXml
      //
      this.btnSaveXml.Location = new System.Drawing.Point(573, 32);
      this.btnSaveXml.Name = "btnSaveXml";
      this.btnSaveXml.Size = new System.Drawing.Size(101, 23);
      this.btnSaveXml.TabIndex = 2;
      this.btnSaveXml.Tag = "SaveXml";
      this.btnSaveXml.Text = "Save XML";
      this.btnSaveXml.UseVisualStyleBackColor = true;
      this.btnSaveXml.Click += new System.EventHandler(this.Action);
      //
      // btnFormatXml
      //
      this.btnFormatXml.Location = new System.Drawing.Point(466, 32);
      this.btnFormatXml.Name = "btnFormatXml";
      this.btnFormatXml.Size = new System.Drawing.Size(101, 23);
      this.btnFormatXml.TabIndex = 2;
      this.btnFormatXml.Tag = "FormatXml";
      this.btnFormatXml.Text = "Format XML";
      this.btnFormatXml.UseVisualStyleBackColor = true;
      this.btnFormatXml.Click += new System.EventHandler(this.Action);
      //
      // btnReloadXml
      //
      this.btnReloadXml.Location = new System.Drawing.Point(359, 32);
      this.btnReloadXml.Name = "btnReloadXml";
      this.btnReloadXml.Size = new System.Drawing.Size(101, 23);
      this.btnReloadXml.TabIndex = 2;
      this.btnReloadXml.Tag = "ReloadXml";
      this.btnReloadXml.Text = "Reload XML";
      this.btnReloadXml.UseVisualStyleBackColor = true;
      this.btnReloadXml.Click += new System.EventHandler(this.Action);
      //
      // btnRunValidation
      //
      this.btnRunValidation.Location = new System.Drawing.Point(772, 32);
      this.btnRunValidation.Name = "btnRunValidation";
      this.btnRunValidation.Size = new System.Drawing.Size(124, 69);
      this.btnRunValidation.TabIndex = 2;
      this.btnRunValidation.Tag = "RunValidation";
      this.btnRunValidation.Text = "Run Validation";
      this.btnRunValidation.UseVisualStyleBackColor = true;
      this.btnRunValidation.Click += new System.EventHandler(this.Action);
      //
      // lblXsdFile
      //
      this.lblXsdFile.AutoSize = true;
      this.lblXsdFile.Location = new System.Drawing.Point(16, 65);
      this.lblXsdFile.Name = "lblXsdFile";
      this.lblXsdFile.Size = new System.Drawing.Size(48, 13);
      this.lblXsdFile.TabIndex = 1;
      this.lblXsdFile.Text = "XSD File";
      //
      // cboXsdFile
      //
      this.cboXsdFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboXsdFile.FormattingEnabled = true;
      this.cboXsdFile.Location = new System.Drawing.Point(13, 81);
      this.cboXsdFile.Name = "cboXsdFile";
      this.cboXsdFile.Size = new System.Drawing.Size(322, 21);
      this.cboXsdFile.TabIndex = 0;
      this.cboXsdFile.SelectedIndexChanged += new System.EventHandler(this.SelectionChanged);
      //
      // lblXmlFile
      //
      this.lblXmlFile.AutoSize = true;
      this.lblXmlFile.Location = new System.Drawing.Point(16, 17);
      this.lblXmlFile.Name = "lblXmlFile";
      this.lblXmlFile.Size = new System.Drawing.Size(48, 13);
      this.lblXmlFile.TabIndex = 1;
      this.lblXmlFile.Text = "XML File";
      //
      // cboXmlFile
      //
      this.cboXmlFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboXmlFile.FormattingEnabled = true;
      this.cboXmlFile.Location = new System.Drawing.Point(13, 33);
      this.cboXmlFile.Name = "cboXmlFile";
      this.cboXmlFile.Size = new System.Drawing.Size(322, 21);
      this.cboXmlFile.TabIndex = 0;
      this.cboXmlFile.SelectedIndexChanged += new System.EventHandler(this.SelectionChanged);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 710);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1047, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlMain
      //
      this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnlMain.Controls.Add(this.tabMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 150);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1047, 560);
      this.pnlMain.TabIndex = 2;
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageXml);
      this.tabMain.Controls.Add(this.tabPageXsd);
      this.tabMain.Controls.Add(this.tabPageValidationResults);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(132, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1045, 558);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 0;
      //
      // tabPageXml
      //
      this.tabPageXml.Controls.Add(this.txtXml);
      this.tabPageXml.Controls.Add(this.lblXmlFileMessage);
      this.tabPageXml.Location = new System.Drawing.Point(4, 22);
      this.tabPageXml.Name = "tabPageXml";
      this.tabPageXml.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageXml.Size = new System.Drawing.Size(1037, 532);
      this.tabPageXml.TabIndex = 0;
      this.tabPageXml.Text = "XML File";
      this.tabPageXml.UseVisualStyleBackColor = true;
      //
      // txtXml
      //
      this.txtXml.AutoCompleteBracketsList = new char[] {
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
      this.txtXml.AutoScrollMinSize = new System.Drawing.Size(25, 14);
      this.txtXml.BackBrush = null;
      this.txtXml.CharHeight = 14;
      this.txtXml.CharWidth = 7;
      this.txtXml.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtXml.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtXml.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtXml.Font = new System.Drawing.Font("Consolas", 9F);
      this.txtXml.IsReplaceMode = false;
      this.txtXml.Location = new System.Drawing.Point(3, 21);
      this.txtXml.Name = "txtXml";
      this.txtXml.Paddings = new System.Windows.Forms.Padding(0);
      this.txtXml.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtXml.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtXml.ServiceColors")));
      this.txtXml.Size = new System.Drawing.Size(1031, 508);
      this.txtXml.TabIndex = 4;
      this.txtXml.Tag = "XML";
      this.txtXml.Zoom = 100;
      this.txtXml.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.TextValueChanged);
      //
      // lblXmlFileMessage
      //
      this.lblXmlFileMessage.BackColor = System.Drawing.Color.MistyRose;
      this.lblXmlFileMessage.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblXmlFileMessage.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblXmlFileMessage.ForeColor = System.Drawing.Color.Red;
      this.lblXmlFileMessage.Location = new System.Drawing.Point(3, 3);
      this.lblXmlFileMessage.Name = "lblXmlFileMessage";
      this.lblXmlFileMessage.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
      this.lblXmlFileMessage.Size = new System.Drawing.Size(1031, 18);
      this.lblXmlFileMessage.TabIndex = 3;
      this.lblXmlFileMessage.Text = "XML file message";
      this.lblXmlFileMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // tabPageXsd
      //
      this.tabPageXsd.Controls.Add(this.txtXsd);
      this.tabPageXsd.Controls.Add(this.lblXsdFileMessage);
      this.tabPageXsd.Location = new System.Drawing.Point(4, 22);
      this.tabPageXsd.Name = "tabPageXsd";
      this.tabPageXsd.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageXsd.Size = new System.Drawing.Size(1037, 532);
      this.tabPageXsd.TabIndex = 1;
      this.tabPageXsd.Text = "XSD File";
      this.tabPageXsd.UseVisualStyleBackColor = true;
      //
      // txtXsd
      //
      this.txtXsd.AutoCompleteBracketsList = new char[] {
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
      this.txtXsd.AutoScrollMinSize = new System.Drawing.Size(25, 14);
      this.txtXsd.BackBrush = null;
      this.txtXsd.CharHeight = 14;
      this.txtXsd.CharWidth = 7;
      this.txtXsd.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtXsd.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtXsd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtXsd.Font = new System.Drawing.Font("Consolas", 9F);
      this.txtXsd.IsReplaceMode = false;
      this.txtXsd.Location = new System.Drawing.Point(3, 21);
      this.txtXsd.Name = "txtXsd";
      this.txtXsd.Paddings = new System.Windows.Forms.Padding(0);
      this.txtXsd.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtXsd.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtXsd.ServiceColors")));
      this.txtXsd.Size = new System.Drawing.Size(1031, 508);
      this.txtXsd.TabIndex = 5;
      this.txtXsd.Tag = "XML";
      this.txtXsd.Zoom = 100;
      this.txtXsd.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.TextValueChanged);
      //
      // lblXsdFileMessage
      //
      this.lblXsdFileMessage.BackColor = System.Drawing.Color.MistyRose;
      this.lblXsdFileMessage.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblXsdFileMessage.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblXsdFileMessage.ForeColor = System.Drawing.Color.Red;
      this.lblXsdFileMessage.Location = new System.Drawing.Point(3, 3);
      this.lblXsdFileMessage.Name = "lblXsdFileMessage";
      this.lblXsdFileMessage.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
      this.lblXsdFileMessage.Size = new System.Drawing.Size(1031, 18);
      this.lblXsdFileMessage.TabIndex = 2;
      this.lblXsdFileMessage.Text = "XSD file message";
      this.lblXsdFileMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // tabPageValidationResults
      //
      this.tabPageValidationResults.Controls.Add(this.txtValidation);
      this.tabPageValidationResults.Location = new System.Drawing.Point(4, 22);
      this.tabPageValidationResults.Name = "tabPageValidationResults";
      this.tabPageValidationResults.Size = new System.Drawing.Size(1037, 532);
      this.tabPageValidationResults.TabIndex = 2;
      this.tabPageValidationResults.Text = "Validation Results";
      this.tabPageValidationResults.UseVisualStyleBackColor = true;
      //
      // txtValidation
      //
      this.txtValidation.AutoCompleteBracketsList = new char[] {
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
      this.txtValidation.AutoScrollMinSize = new System.Drawing.Size(25, 14);
      this.txtValidation.BackBrush = null;
      this.txtValidation.CharHeight = 14;
      this.txtValidation.CharWidth = 7;
      this.txtValidation.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtValidation.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtValidation.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtValidation.Font = new System.Drawing.Font("Consolas", 9F);
      this.txtValidation.IsReplaceMode = false;
      this.txtValidation.Location = new System.Drawing.Point(0, 0);
      this.txtValidation.Name = "txtValidation";
      this.txtValidation.Paddings = new System.Windows.Forms.Padding(0);
      this.txtValidation.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtValidation.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtValidation.ServiceColors")));
      this.txtValidation.Size = new System.Drawing.Size(1037, 532);
      this.txtValidation.TabIndex = 5;
      this.txtValidation.Tag = "XML";
      this.txtValidation.Zoom = 100;
      this.txtValidation.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.TextValueChanged);
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1047, 24);
      this.mnuMain.TabIndex = 3;
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
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1047, 733);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "IdmXsdTest - Version 1.0";
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.udFontSize)).EndInit();
      this.pnlMain.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageXml.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtXml)).EndInit();
      this.tabPageXsd.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtXsd)).EndInit();
      this.tabPageValidationResults.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtValidation)).EndInit();
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnRunValidation;
    private System.Windows.Forms.Label lblXsdFile;
    private System.Windows.Forms.ComboBox cboXsdFile;
    private System.Windows.Forms.Label lblXmlFile;
    private System.Windows.Forms.ComboBox cboXmlFile;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageXml;
    private System.Windows.Forms.TabPage tabPageXsd;
    private System.Windows.Forms.TabPage tabPageValidationResults;
    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Button btnReloadXsd;
    private System.Windows.Forms.Button btnReloadXml;
    private System.Windows.Forms.Button btnFormatXsd;
    private System.Windows.Forms.Button btnFormatXml;
    private System.Windows.Forms.Button btnSaveXsd;
    private System.Windows.Forms.Button btnSaveXml;
    private System.Windows.Forms.Label lblXsdFileMessage;
    private System.Windows.Forms.Label lblXmlFileMessage;
    private FastColoredTextBoxNS.FastColoredTextBox txtXml;
    private FastColoredTextBoxNS.FastColoredTextBox txtXsd;
    private FastColoredTextBoxNS.FastColoredTextBox txtValidation;
    private System.Windows.Forms.NumericUpDown udFontSize;
    private System.Windows.Forms.Label lblFontSize;
  }
}


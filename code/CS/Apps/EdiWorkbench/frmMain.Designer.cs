namespace Org.EdiWorkbench
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
      this.cboBusXmlAFiles = new System.Windows.Forms.ComboBox();
      this.cboFocus = new System.Windows.Forms.ComboBox();
      this.cboFileToProcess = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnReloadEdiXslt = new System.Windows.Forms.Button();
      this.lblFileToProcess = new System.Windows.Forms.Label();
      this.btnConvertEdiToFormattedEdi = new System.Windows.Forms.Button();
      this.btnSaveToEdiXslt = new System.Windows.Forms.Button();
      this.btnTransformToEdiXml = new System.Windows.Forms.Button();
      this.btnConvertEdiToOopBus = new System.Windows.Forms.Button();
      this.btnConvertEdiToOopEdi = new System.Windows.Forms.Button();
      this.btnLoadEdi = new System.Windows.Forms.Button();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageRawEdi = new System.Windows.Forms.TabPage();
      this.rtxtRawEdi = new System.Windows.Forms.RichTextBox();
      this.tabPageFmtEdi = new System.Windows.Forms.TabPage();
      this.rtxtFmtEdi = new System.Windows.Forms.RichTextBox();
      this.tabPageEdiXml_A = new System.Windows.Forms.TabPage();
      this.rtxtEdiXml_A = new System.Windows.Forms.RichTextBox();
      this.tabPageXsltToBusXml = new System.Windows.Forms.TabPage();
      this.rtxtToBusXslt = new System.Windows.Forms.RichTextBox();
      this.tabPageBusXml_A = new System.Windows.Forms.TabPage();
      this.rtxtBusXml_A = new System.Windows.Forms.RichTextBox();
      this.tabPageXsltToEdiXml = new System.Windows.Forms.TabPage();
      this.rtxtToEdiXslt = new System.Windows.Forms.RichTextBox();
      this.tabPageEdiXml_B = new System.Windows.Forms.TabPage();
      this.rtxtEdiXml_B = new System.Windows.Forms.RichTextBox();
      this.tabPageBusXml_B = new System.Windows.Forms.TabPage();
      this.rtxtBusXml_B = new System.Windows.Forms.RichTextBox();
      this.lblEdiXsltModified = new System.Windows.Forms.Label();
      this.lblMessage = new System.Windows.Forms.Label();
      this.ckUseDisplayForm = new System.Windows.Forms.CheckBox();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageRawEdi.SuspendLayout();
      this.tabPageFmtEdi.SuspendLayout();
      this.tabPageEdiXml_A.SuspendLayout();
      this.tabPageXsltToBusXml.SuspendLayout();
      this.tabPageBusXml_A.SuspendLayout();
      this.tabPageXsltToEdiXml.SuspendLayout();
      this.tabPageEdiXml_B.SuspendLayout();
      this.tabPageBusXml_B.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
      this.mnuMain.Size = new System.Drawing.Size(922, 24);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
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
      this.lblStatus.Location = new System.Drawing.Point(0, 603);
      this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(922, 19);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.cboBusXmlAFiles);
      this.pnlTop.Controls.Add(this.cboFocus);
      this.pnlTop.Controls.Add(this.cboFileToProcess);
      this.pnlTop.Controls.Add(this.label1);
      this.pnlTop.Controls.Add(this.btnReloadEdiXslt);
      this.pnlTop.Controls.Add(this.lblFileToProcess);
      this.pnlTop.Controls.Add(this.btnConvertEdiToFormattedEdi);
      this.pnlTop.Controls.Add(this.btnSaveToEdiXslt);
      this.pnlTop.Controls.Add(this.btnTransformToEdiXml);
      this.pnlTop.Controls.Add(this.btnConvertEdiToOopBus);
      this.pnlTop.Controls.Add(this.btnConvertEdiToOopEdi);
      this.pnlTop.Controls.Add(this.btnLoadEdi);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(922, 58);
      this.pnlTop.TabIndex = 2;
      // 
      // cboBusXmlAFiles
      // 
      this.cboBusXmlAFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboBusXmlAFiles.FormattingEnabled = true;
      this.cboBusXmlAFiles.Location = new System.Drawing.Point(430, 8);
      this.cboBusXmlAFiles.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.cboBusXmlAFiles.Name = "cboBusXmlAFiles";
      this.cboBusXmlAFiles.Size = new System.Drawing.Size(94, 21);
      this.cboBusXmlAFiles.TabIndex = 1;
      this.cboBusXmlAFiles.Tag = "BusXmlCboChange";
      this.cboBusXmlAFiles.SelectedIndexChanged += new System.EventHandler(this.Action);
      // 
      // cboFocus
      // 
      this.cboFocus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboFocus.FormattingEnabled = true;
      this.cboFocus.Items.AddRange(new object[] {
            "EdiInput",
            "BusXmlA"});
      this.cboFocus.Location = new System.Drawing.Point(586, 7);
      this.cboFocus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.cboFocus.Name = "cboFocus";
      this.cboFocus.Size = new System.Drawing.Size(94, 21);
      this.cboFocus.TabIndex = 1;
      this.cboFocus.Tag = "FocusChange";
      this.cboFocus.SelectedIndexChanged += new System.EventHandler(this.Action);
      // 
      // cboFileToProcess
      // 
      this.cboFileToProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboFileToProcess.FormattingEnabled = true;
      this.cboFileToProcess.Location = new System.Drawing.Point(35, 7);
      this.cboFileToProcess.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.cboFileToProcess.Name = "cboFileToProcess";
      this.cboFileToProcess.Size = new System.Drawing.Size(94, 21);
      this.cboFileToProcess.TabIndex = 1;
      this.cboFileToProcess.Tag = "EdiFileCboChange";
      this.cboFileToProcess.SelectedIndexChanged += new System.EventHandler(this.Action);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(550, 11);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(36, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Focus";
      // 
      // btnReloadEdiXslt
      // 
      this.btnReloadEdiXslt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnReloadEdiXslt.Location = new System.Drawing.Point(813, 32);
      this.btnReloadEdiXslt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.btnReloadEdiXslt.Name = "btnReloadEdiXslt";
      this.btnReloadEdiXslt.Size = new System.Drawing.Size(103, 20);
      this.btnReloadEdiXslt.TabIndex = 0;
      this.btnReloadEdiXslt.Tag = "ReloadEdiXslt";
      this.btnReloadEdiXslt.Text = "Reload Edi Xslt";
      this.btnReloadEdiXslt.UseVisualStyleBackColor = true;
      this.btnReloadEdiXslt.Click += new System.EventHandler(this.Action);
      // 
      // lblFileToProcess
      // 
      this.lblFileToProcess.AutoSize = true;
      this.lblFileToProcess.Location = new System.Drawing.Point(8, 11);
      this.lblFileToProcess.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblFileToProcess.Name = "lblFileToProcess";
      this.lblFileToProcess.Size = new System.Drawing.Size(28, 13);
      this.lblFileToProcess.TabIndex = 2;
      this.lblFileToProcess.Text = "Files";
      // 
      // btnConvertEdiToFormattedEdi
      // 
      this.btnConvertEdiToFormattedEdi.Location = new System.Drawing.Point(134, 32);
      this.btnConvertEdiToFormattedEdi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.btnConvertEdiToFormattedEdi.Name = "btnConvertEdiToFormattedEdi";
      this.btnConvertEdiToFormattedEdi.Size = new System.Drawing.Size(94, 20);
      this.btnConvertEdiToFormattedEdi.TabIndex = 0;
      this.btnConvertEdiToFormattedEdi.Tag = "ConvertEdiToFormattedEdi";
      this.btnConvertEdiToFormattedEdi.Text = "Format EDI";
      this.btnConvertEdiToFormattedEdi.UseVisualStyleBackColor = true;
      this.btnConvertEdiToFormattedEdi.Click += new System.EventHandler(this.Action);
      // 
      // btnSaveToEdiXslt
      // 
      this.btnSaveToEdiXslt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSaveToEdiXslt.Location = new System.Drawing.Point(813, 7);
      this.btnSaveToEdiXslt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.btnSaveToEdiXslt.Name = "btnSaveToEdiXslt";
      this.btnSaveToEdiXslt.Size = new System.Drawing.Size(103, 20);
      this.btnSaveToEdiXslt.TabIndex = 0;
      this.btnSaveToEdiXslt.Tag = "SaveToEdiXslt";
      this.btnSaveToEdiXslt.Text = "Save To EDI Xslt";
      this.btnSaveToEdiXslt.UseVisualStyleBackColor = true;
      this.btnSaveToEdiXslt.Click += new System.EventHandler(this.Action);
      // 
      // btnTransformToEdiXml
      // 
      this.btnTransformToEdiXml.Location = new System.Drawing.Point(430, 32);
      this.btnTransformToEdiXml.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.btnTransformToEdiXml.Name = "btnTransformToEdiXml";
      this.btnTransformToEdiXml.Size = new System.Drawing.Size(94, 20);
      this.btnTransformToEdiXml.TabIndex = 0;
      this.btnTransformToEdiXml.Tag = "TransformToEdiXml_B";
      this.btnTransformToEdiXml.Text = "Xform=>EdiXmlB";
      this.btnTransformToEdiXml.UseVisualStyleBackColor = true;
      this.btnTransformToEdiXml.Click += new System.EventHandler(this.Action);
      // 
      // btnConvertEdiToOopBus
      // 
      this.btnConvertEdiToOopBus.Location = new System.Drawing.Point(332, 32);
      this.btnConvertEdiToOopBus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.btnConvertEdiToOopBus.Name = "btnConvertEdiToOopBus";
      this.btnConvertEdiToOopBus.Size = new System.Drawing.Size(94, 20);
      this.btnConvertEdiToOopBus.TabIndex = 0;
      this.btnConvertEdiToOopBus.Tag = "TransformToBusXml_A";
      this.btnConvertEdiToOopBus.Text = "Xform=>BusXmlA";
      this.btnConvertEdiToOopBus.UseVisualStyleBackColor = true;
      this.btnConvertEdiToOopBus.Click += new System.EventHandler(this.Action);
      // 
      // btnConvertEdiToOopEdi
      // 
      this.btnConvertEdiToOopEdi.Location = new System.Drawing.Point(232, 32);
      this.btnConvertEdiToOopEdi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.btnConvertEdiToOopEdi.Name = "btnConvertEdiToOopEdi";
      this.btnConvertEdiToOopEdi.Size = new System.Drawing.Size(94, 20);
      this.btnConvertEdiToOopEdi.TabIndex = 0;
      this.btnConvertEdiToOopEdi.Tag = "ParseRawEdiToEdiXml";
      this.btnConvertEdiToOopEdi.Text = "Parse=>EdiXmlA";
      this.btnConvertEdiToOopEdi.UseVisualStyleBackColor = true;
      this.btnConvertEdiToOopEdi.Click += new System.EventHandler(this.Action);
      // 
      // btnLoadEdi
      // 
      this.btnLoadEdi.Location = new System.Drawing.Point(34, 32);
      this.btnLoadEdi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.btnLoadEdi.Name = "btnLoadEdi";
      this.btnLoadEdi.Size = new System.Drawing.Size(94, 20);
      this.btnLoadEdi.TabIndex = 0;
      this.btnLoadEdi.Tag = "LoadEdi";
      this.btnLoadEdi.Text = "Load Raw EDI";
      this.btnLoadEdi.UseVisualStyleBackColor = true;
      this.btnLoadEdi.Click += new System.EventHandler(this.Action);
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageRawEdi);
      this.tabMain.Controls.Add(this.tabPageFmtEdi);
      this.tabMain.Controls.Add(this.tabPageEdiXml_A);
      this.tabMain.Controls.Add(this.tabPageXsltToBusXml);
      this.tabMain.Controls.Add(this.tabPageBusXml_A);
      this.tabMain.Controls.Add(this.tabPageXsltToEdiXml);
      this.tabMain.Controls.Add(this.tabPageEdiXml_B);
      this.tabMain.Controls.Add(this.tabPageBusXml_B);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(100, 21);
      this.tabMain.Location = new System.Drawing.Point(0, 82);
      this.tabMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(922, 521);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 4;
      // 
      // tabPageRawEdi
      // 
      this.tabPageRawEdi.Controls.Add(this.rtxtRawEdi);
      this.tabPageRawEdi.Location = new System.Drawing.Point(4, 25);
      this.tabPageRawEdi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.tabPageRawEdi.Name = "tabPageRawEdi";
      this.tabPageRawEdi.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.tabPageRawEdi.Size = new System.Drawing.Size(914, 492);
      this.tabPageRawEdi.TabIndex = 0;
      this.tabPageRawEdi.Text = "Raw EDI";
      this.tabPageRawEdi.UseVisualStyleBackColor = true;
      // 
      // rtxtRawEdi
      // 
      this.rtxtRawEdi.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtxtRawEdi.Font = new System.Drawing.Font("Lucida Console", 8F);
      this.rtxtRawEdi.Location = new System.Drawing.Point(2, 2);
      this.rtxtRawEdi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.rtxtRawEdi.Name = "rtxtRawEdi";
      this.rtxtRawEdi.Size = new System.Drawing.Size(910, 488);
      this.rtxtRawEdi.TabIndex = 1;
      this.rtxtRawEdi.Tag = "ToEdiXmlXsltChanged";
      this.rtxtRawEdi.Text = "";
      this.rtxtRawEdi.WordWrap = false;
      // 
      // tabPageFmtEdi
      // 
      this.tabPageFmtEdi.Controls.Add(this.rtxtFmtEdi);
      this.tabPageFmtEdi.Location = new System.Drawing.Point(4, 25);
      this.tabPageFmtEdi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.tabPageFmtEdi.Name = "tabPageFmtEdi";
      this.tabPageFmtEdi.Size = new System.Drawing.Size(914, 493);
      this.tabPageFmtEdi.TabIndex = 3;
      this.tabPageFmtEdi.Text = "Fmt EDI";
      this.tabPageFmtEdi.UseVisualStyleBackColor = true;
      // 
      // rtxtFmtEdi
      // 
      this.rtxtFmtEdi.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.rtxtFmtEdi.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtxtFmtEdi.Font = new System.Drawing.Font("Lucida Console", 8F);
      this.rtxtFmtEdi.Location = new System.Drawing.Point(0, 0);
      this.rtxtFmtEdi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.rtxtFmtEdi.Name = "rtxtFmtEdi";
      this.rtxtFmtEdi.Size = new System.Drawing.Size(914, 493);
      this.rtxtFmtEdi.TabIndex = 1;
      this.rtxtFmtEdi.Tag = "ToEdiXmlXsltChanged";
      this.rtxtFmtEdi.Text = "";
      this.rtxtFmtEdi.WordWrap = false;
      // 
      // tabPageEdiXml_A
      // 
      this.tabPageEdiXml_A.Controls.Add(this.rtxtEdiXml_A);
      this.tabPageEdiXml_A.Location = new System.Drawing.Point(4, 25);
      this.tabPageEdiXml_A.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.tabPageEdiXml_A.Name = "tabPageEdiXml_A";
      this.tabPageEdiXml_A.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.tabPageEdiXml_A.Size = new System.Drawing.Size(914, 493);
      this.tabPageEdiXml_A.TabIndex = 1;
      this.tabPageEdiXml_A.Text = "EDI Xml (A)";
      this.tabPageEdiXml_A.UseVisualStyleBackColor = true;
      // 
      // rtxtEdiXml_A
      // 
      this.rtxtEdiXml_A.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.rtxtEdiXml_A.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtxtEdiXml_A.Font = new System.Drawing.Font("Lucida Console", 8F);
      this.rtxtEdiXml_A.Location = new System.Drawing.Point(2, 2);
      this.rtxtEdiXml_A.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.rtxtEdiXml_A.Name = "rtxtEdiXml_A";
      this.rtxtEdiXml_A.Size = new System.Drawing.Size(910, 489);
      this.rtxtEdiXml_A.TabIndex = 1;
      this.rtxtEdiXml_A.Tag = "ToEdiXmlXsltChanged";
      this.rtxtEdiXml_A.Text = "";
      this.rtxtEdiXml_A.WordWrap = false;
      // 
      // tabPageXsltToBusXml
      // 
      this.tabPageXsltToBusXml.Controls.Add(this.rtxtToBusXslt);
      this.tabPageXsltToBusXml.Location = new System.Drawing.Point(4, 25);
      this.tabPageXsltToBusXml.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.tabPageXsltToBusXml.Name = "tabPageXsltToBusXml";
      this.tabPageXsltToBusXml.Size = new System.Drawing.Size(914, 493);
      this.tabPageXsltToBusXml.TabIndex = 4;
      this.tabPageXsltToBusXml.Text = "Bus Xslt";
      this.tabPageXsltToBusXml.UseVisualStyleBackColor = true;
      // 
      // rtxtToBusXslt
      // 
      this.rtxtToBusXslt.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.rtxtToBusXslt.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtxtToBusXslt.Font = new System.Drawing.Font("Lucida Console", 8F);
      this.rtxtToBusXslt.Location = new System.Drawing.Point(0, 0);
      this.rtxtToBusXslt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.rtxtToBusXslt.Name = "rtxtToBusXslt";
      this.rtxtToBusXslt.Size = new System.Drawing.Size(914, 493);
      this.rtxtToBusXslt.TabIndex = 1;
      this.rtxtToBusXslt.Tag = "ToEdiXmlXsltChanged";
      this.rtxtToBusXslt.Text = "";
      this.rtxtToBusXslt.WordWrap = false;
      // 
      // tabPageBusXml_A
      // 
      this.tabPageBusXml_A.Controls.Add(this.rtxtBusXml_A);
      this.tabPageBusXml_A.Location = new System.Drawing.Point(4, 25);
      this.tabPageBusXml_A.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.tabPageBusXml_A.Name = "tabPageBusXml_A";
      this.tabPageBusXml_A.Size = new System.Drawing.Size(914, 493);
      this.tabPageBusXml_A.TabIndex = 2;
      this.tabPageBusXml_A.Text = "Bus Xml (A)";
      this.tabPageBusXml_A.UseVisualStyleBackColor = true;
      // 
      // rtxtBusXml_A
      // 
      this.rtxtBusXml_A.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.rtxtBusXml_A.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtxtBusXml_A.Font = new System.Drawing.Font("Lucida Console", 8F);
      this.rtxtBusXml_A.Location = new System.Drawing.Point(0, 0);
      this.rtxtBusXml_A.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.rtxtBusXml_A.Name = "rtxtBusXml_A";
      this.rtxtBusXml_A.Size = new System.Drawing.Size(914, 493);
      this.rtxtBusXml_A.TabIndex = 1;
      this.rtxtBusXml_A.Tag = "ToEdiXmlXsltChanged";
      this.rtxtBusXml_A.Text = "";
      this.rtxtBusXml_A.WordWrap = false;
      // 
      // tabPageXsltToEdiXml
      // 
      this.tabPageXsltToEdiXml.Controls.Add(this.rtxtToEdiXslt);
      this.tabPageXsltToEdiXml.Location = new System.Drawing.Point(4, 25);
      this.tabPageXsltToEdiXml.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.tabPageXsltToEdiXml.Name = "tabPageXsltToEdiXml";
      this.tabPageXsltToEdiXml.Size = new System.Drawing.Size(914, 493);
      this.tabPageXsltToEdiXml.TabIndex = 5;
      this.tabPageXsltToEdiXml.Text = "Edi Xslt";
      this.tabPageXsltToEdiXml.UseVisualStyleBackColor = true;
      // 
      // rtxtToEdiXslt
      // 
      this.rtxtToEdiXslt.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.rtxtToEdiXslt.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtxtToEdiXslt.Font = new System.Drawing.Font("Lucida Console", 8F);
      this.rtxtToEdiXslt.Location = new System.Drawing.Point(0, 0);
      this.rtxtToEdiXslt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.rtxtToEdiXslt.Name = "rtxtToEdiXslt";
      this.rtxtToEdiXslt.Size = new System.Drawing.Size(914, 493);
      this.rtxtToEdiXslt.TabIndex = 0;
      this.rtxtToEdiXslt.Tag = "ToEdiXmlXsltChanged";
      this.rtxtToEdiXslt.Text = "";
      this.rtxtToEdiXslt.WordWrap = false;
      this.rtxtToEdiXslt.TextChanged += new System.EventHandler(this.Action);
      // 
      // tabPageEdiXml_B
      // 
      this.tabPageEdiXml_B.Controls.Add(this.rtxtEdiXml_B);
      this.tabPageEdiXml_B.Location = new System.Drawing.Point(4, 25);
      this.tabPageEdiXml_B.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.tabPageEdiXml_B.Name = "tabPageEdiXml_B";
      this.tabPageEdiXml_B.Size = new System.Drawing.Size(914, 493);
      this.tabPageEdiXml_B.TabIndex = 7;
      this.tabPageEdiXml_B.Text = "EDI Xml (B)";
      this.tabPageEdiXml_B.UseVisualStyleBackColor = true;
      // 
      // rtxtEdiXml_B
      // 
      this.rtxtEdiXml_B.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.rtxtEdiXml_B.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtxtEdiXml_B.Font = new System.Drawing.Font("Lucida Console", 8F);
      this.rtxtEdiXml_B.Location = new System.Drawing.Point(0, 0);
      this.rtxtEdiXml_B.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.rtxtEdiXml_B.Name = "rtxtEdiXml_B";
      this.rtxtEdiXml_B.Size = new System.Drawing.Size(914, 493);
      this.rtxtEdiXml_B.TabIndex = 1;
      this.rtxtEdiXml_B.Tag = "ToEdiXmlXsltChanged";
      this.rtxtEdiXml_B.Text = "";
      this.rtxtEdiXml_B.WordWrap = false;
      // 
      // tabPageBusXml_B
      // 
      this.tabPageBusXml_B.Controls.Add(this.rtxtBusXml_B);
      this.tabPageBusXml_B.Location = new System.Drawing.Point(4, 25);
      this.tabPageBusXml_B.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.tabPageBusXml_B.Name = "tabPageBusXml_B";
      this.tabPageBusXml_B.Size = new System.Drawing.Size(914, 493);
      this.tabPageBusXml_B.TabIndex = 6;
      this.tabPageBusXml_B.Text = "Bus Xml (B)";
      this.tabPageBusXml_B.UseVisualStyleBackColor = true;
      // 
      // rtxtBusXml_B
      // 
      this.rtxtBusXml_B.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.rtxtBusXml_B.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtxtBusXml_B.Font = new System.Drawing.Font("Lucida Console", 8F);
      this.rtxtBusXml_B.Location = new System.Drawing.Point(0, 0);
      this.rtxtBusXml_B.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.rtxtBusXml_B.Name = "rtxtBusXml_B";
      this.rtxtBusXml_B.Size = new System.Drawing.Size(914, 493);
      this.rtxtBusXml_B.TabIndex = 1;
      this.rtxtBusXml_B.Tag = "ToEdiXmlXsltChanged";
      this.rtxtBusXml_B.Text = "";
      this.rtxtBusXml_B.WordWrap = false;
      // 
      // lblEdiXsltModified
      // 
      this.lblEdiXsltModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblEdiXsltModified.BackColor = System.Drawing.Color.Transparent;
      this.lblEdiXsltModified.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblEdiXsltModified.ForeColor = System.Drawing.Color.Red;
      this.lblEdiXsltModified.Location = new System.Drawing.Point(806, 2);
      this.lblEdiXsltModified.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblEdiXsltModified.Name = "lblEdiXsltModified";
      this.lblEdiXsltModified.Size = new System.Drawing.Size(114, 19);
      this.lblEdiXsltModified.TabIndex = 3;
      this.lblEdiXsltModified.Text = "XSLT MODIFIED";
      this.lblEdiXsltModified.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblMessage
      // 
      this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMessage.ForeColor = System.Drawing.Color.Red;
      this.lblMessage.Location = new System.Drawing.Point(230, 2);
      this.lblMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(328, 19);
      this.lblMessage.TabIndex = 3;
      this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lblMessage.DoubleClick += new System.EventHandler(this.lblMessage_DoubleClick);
      // 
      // ckUseDisplayForm
      // 
      this.ckUseDisplayForm.AutoSize = true;
      this.ckUseDisplayForm.Location = new System.Drawing.Point(586, 3);
      this.ckUseDisplayForm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.ckUseDisplayForm.Name = "ckUseDisplayForm";
      this.ckUseDisplayForm.Size = new System.Drawing.Size(108, 17);
      this.ckUseDisplayForm.TabIndex = 4;
      this.ckUseDisplayForm.Text = "Use Display Form";
      this.ckUseDisplayForm.UseVisualStyleBackColor = true;
      this.ckUseDisplayForm.CheckedChanged += new System.EventHandler(this.ckUseDisplayForm_CheckedChanged);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(922, 622);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.lblMessage);
      this.Controls.Add(this.lblEdiXsltModified);
      this.Controls.Add(this.ckUseDisplayForm);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "EDI Test App";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabPageRawEdi.ResumeLayout(false);
      this.tabPageFmtEdi.ResumeLayout(false);
      this.tabPageEdiXml_A.ResumeLayout(false);
      this.tabPageXsltToBusXml.ResumeLayout(false);
      this.tabPageBusXml_A.ResumeLayout(false);
      this.tabPageXsltToEdiXml.ResumeLayout(false);
      this.tabPageEdiXml_B.ResumeLayout(false);
      this.tabPageBusXml_B.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnLoadEdi;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageRawEdi;
        private System.Windows.Forms.TabPage tabPageEdiXml_A;
        private System.Windows.Forms.TabPage tabPageBusXml_A;
        private System.Windows.Forms.Button btnConvertEdiToOopEdi;
        private System.Windows.Forms.TabPage tabPageFmtEdi;
        private System.Windows.Forms.Button btnConvertEdiToFormattedEdi;
        private System.Windows.Forms.Button btnConvertEdiToOopBus;
        private System.Windows.Forms.TabPage tabPageXsltToBusXml;
        private System.Windows.Forms.TabPage tabPageXsltToEdiXml;
        private System.Windows.Forms.TabPage tabPageBusXml_B;
        private System.Windows.Forms.TabPage tabPageEdiXml_B;
        private System.Windows.Forms.Label lblFileToProcess;
        private System.Windows.Forms.ComboBox cboFileToProcess;
        private System.Windows.Forms.Label lblEdiXsltModified;
        private System.Windows.Forms.Button btnTransformToEdiXml;
        private System.Windows.Forms.RichTextBox rtxtToEdiXslt;
        private System.Windows.Forms.RichTextBox rtxtRawEdi;
        private System.Windows.Forms.RichTextBox rtxtFmtEdi;
        private System.Windows.Forms.RichTextBox rtxtEdiXml_A;
        private System.Windows.Forms.RichTextBox rtxtToBusXslt;
        private System.Windows.Forms.RichTextBox rtxtBusXml_A;
        private System.Windows.Forms.RichTextBox rtxtEdiXml_B;
        private System.Windows.Forms.RichTextBox rtxtBusXml_B;
        private System.Windows.Forms.Button btnSaveToEdiXslt;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnReloadEdiXslt;
        private System.Windows.Forms.CheckBox ckUseDisplayForm;
        private System.Windows.Forms.ComboBox cboBusXmlAFiles;
        private System.Windows.Forms.ComboBox cboFocus;
        private System.Windows.Forms.Label label1;
    }
}


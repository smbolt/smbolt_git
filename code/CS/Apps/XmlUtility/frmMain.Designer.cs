namespace Org.XmlUtility
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
      this.btnBack = new System.Windows.Forms.Button();
      this.btnRunQuery = new System.Windows.Forms.Button();
      this.btnClearXml = new System.Windows.Forms.Button();
      this.btnLoadFile = new System.Windows.Forms.Button();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.lblXPathExpression = new System.Windows.Forms.Label();
      this.txtXmlFile = new System.Windows.Forms.TextBox();
      this.lblXmlFile = new System.Windows.Forms.Label();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageXml = new System.Windows.Forms.TabPage();
      this.tvXml = new System.Windows.Forms.TreeView();
      this.tabPageResults = new System.Windows.Forms.TabPage();
      this.txtResults = new System.Windows.Forms.TextBox();
      this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
      this.cboXPathExpression = new System.Windows.Forms.ComboBox();
      this.btnAddToList = new System.Windows.Forms.Button();
      this.btnDeleteFromList = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageXml.SuspendLayout();
      this.tabPageResults.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1254, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(0, 699);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1254, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.cboXPathExpression);
      this.pnlTop.Controls.Add(this.btnDeleteFromList);
      this.pnlTop.Controls.Add(this.btnAddToList);
      this.pnlTop.Controls.Add(this.btnBack);
      this.pnlTop.Controls.Add(this.btnRunQuery);
      this.pnlTop.Controls.Add(this.btnClearXml);
      this.pnlTop.Controls.Add(this.btnLoadFile);
      this.pnlTop.Controls.Add(this.btnBrowse);
      this.pnlTop.Controls.Add(this.lblXPathExpression);
      this.pnlTop.Controls.Add(this.txtXmlFile);
      this.pnlTop.Controls.Add(this.lblXmlFile);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1254, 101);
      this.pnlTop.TabIndex = 2;
      // 
      // btnBack
      // 
      this.btnBack.Location = new System.Drawing.Point(721, 64);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(84, 22);
      this.btnBack.TabIndex = 2;
      this.btnBack.Tag = "Back";
      this.btnBack.Text = "Back";
      this.btnBack.UseVisualStyleBackColor = true;
      this.btnBack.Click += new System.EventHandler(this.Action);
      // 
      // btnRunQuery
      // 
      this.btnRunQuery.Location = new System.Drawing.Point(634, 64);
      this.btnRunQuery.Name = "btnRunQuery";
      this.btnRunQuery.Size = new System.Drawing.Size(84, 22);
      this.btnRunQuery.TabIndex = 2;
      this.btnRunQuery.Tag = "RunQuery";
      this.btnRunQuery.Text = "Run Query";
      this.btnRunQuery.UseVisualStyleBackColor = true;
      this.btnRunQuery.Click += new System.EventHandler(this.Action);
      // 
      // btnClearXml
      // 
      this.btnClearXml.Location = new System.Drawing.Point(807, 21);
      this.btnClearXml.Name = "btnClearXml";
      this.btnClearXml.Size = new System.Drawing.Size(84, 22);
      this.btnClearXml.TabIndex = 2;
      this.btnClearXml.Tag = "ClearXml";
      this.btnClearXml.Text = "Clear XML";
      this.btnClearXml.UseVisualStyleBackColor = true;
      this.btnClearXml.Click += new System.EventHandler(this.Action);
      // 
      // btnLoadFile
      // 
      this.btnLoadFile.Location = new System.Drawing.Point(721, 21);
      this.btnLoadFile.Name = "btnLoadFile";
      this.btnLoadFile.Size = new System.Drawing.Size(84, 22);
      this.btnLoadFile.TabIndex = 2;
      this.btnLoadFile.Tag = "LoadFile";
      this.btnLoadFile.Text = "LoadFile";
      this.btnLoadFile.UseVisualStyleBackColor = true;
      this.btnLoadFile.Click += new System.EventHandler(this.Action);
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(634, 21);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(84, 22);
      this.btnBrowse.TabIndex = 2;
      this.btnBrowse.Tag = "Browse";
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.Action);
      // 
      // lblXPathExpression
      // 
      this.lblXPathExpression.AutoSize = true;
      this.lblXPathExpression.Location = new System.Drawing.Point(13, 49);
      this.lblXPathExpression.Name = "lblXPathExpression";
      this.lblXPathExpression.Size = new System.Drawing.Size(90, 13);
      this.lblXPathExpression.TabIndex = 0;
      this.lblXPathExpression.Text = "XPath Expression";
      // 
      // txtXmlFile
      // 
      this.txtXmlFile.Location = new System.Drawing.Point(12, 22);
      this.txtXmlFile.Name = "txtXmlFile";
      this.txtXmlFile.Size = new System.Drawing.Size(620, 20);
      this.txtXmlFile.TabIndex = 1;
      this.txtXmlFile.TextChanged += new System.EventHandler(this.txtXmlFile_TextChanged);
      // 
      // lblXmlFile
      // 
      this.lblXmlFile.AutoSize = true;
      this.lblXmlFile.Location = new System.Drawing.Point(13, 6);
      this.lblXmlFile.Name = "lblXmlFile";
      this.lblXmlFile.Size = new System.Drawing.Size(48, 13);
      this.lblXmlFile.TabIndex = 0;
      this.lblXmlFile.Text = "XML File";
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageXml);
      this.tabMain.Controls.Add(this.tabPageResults);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(125, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 125);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1254, 574);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 3;
      // 
      // tabPageXml
      // 
      this.tabPageXml.Controls.Add(this.tvXml);
      this.tabPageXml.Location = new System.Drawing.Point(4, 22);
      this.tabPageXml.Name = "tabPageXml";
      this.tabPageXml.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageXml.Size = new System.Drawing.Size(1246, 548);
      this.tabPageXml.TabIndex = 0;
      this.tabPageXml.Text = "XML";
      this.tabPageXml.UseVisualStyleBackColor = true;
      // 
      // tvXml
      // 
      this.tvXml.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvXml.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvXml.Font = new System.Drawing.Font("Consolas", 8F);
      this.tvXml.Location = new System.Drawing.Point(3, 3);
      this.tvXml.Name = "tvXml";
      this.tvXml.Size = new System.Drawing.Size(1240, 542);
      this.tvXml.TabIndex = 1;
      // 
      // tabPageResults
      // 
      this.tabPageResults.Controls.Add(this.txtResults);
      this.tabPageResults.Location = new System.Drawing.Point(4, 22);
      this.tabPageResults.Name = "tabPageResults";
      this.tabPageResults.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageResults.Size = new System.Drawing.Size(1086, 548);
      this.tabPageResults.TabIndex = 1;
      this.tabPageResults.Text = "Results";
      this.tabPageResults.UseVisualStyleBackColor = true;
      // 
      // txtResults
      // 
      this.txtResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtResults.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtResults.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtResults.Location = new System.Drawing.Point(3, 3);
      this.txtResults.Multiline = true;
      this.txtResults.Name = "txtResults";
      this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtResults.Size = new System.Drawing.Size(1080, 542);
      this.txtResults.TabIndex = 0;
      // 
      // cboXPathExpression
      // 
      this.cboXPathExpression.FormattingEnabled = true;
      this.cboXPathExpression.Location = new System.Drawing.Point(12, 64);
      this.cboXPathExpression.Name = "cboXPathExpression";
      this.cboXPathExpression.Size = new System.Drawing.Size(620, 21);
      this.cboXPathExpression.TabIndex = 3;
      // 
      // btnAddToList
      // 
      this.btnAddToList.Location = new System.Drawing.Point(807, 64);
      this.btnAddToList.Name = "btnAddToList";
      this.btnAddToList.Size = new System.Drawing.Size(84, 22);
      this.btnAddToList.TabIndex = 2;
      this.btnAddToList.Tag = "AddToList";
      this.btnAddToList.Text = "Add To List";
      this.btnAddToList.UseVisualStyleBackColor = true;
      this.btnAddToList.Click += new System.EventHandler(this.Action);
      // 
      // btnDeleteFromList
      // 
      this.btnDeleteFromList.Location = new System.Drawing.Point(893, 64);
      this.btnDeleteFromList.Name = "btnDeleteFromList";
      this.btnDeleteFromList.Size = new System.Drawing.Size(97, 22);
      this.btnDeleteFromList.TabIndex = 2;
      this.btnDeleteFromList.Tag = "Delete";
      this.btnDeleteFromList.Text = "Delete From List";
      this.btnDeleteFromList.UseVisualStyleBackColor = true;
      this.btnDeleteFromList.Click += new System.EventHandler(this.Action);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1254, 722);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "XmlUtility - 1.0.0.0";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabPageXml.ResumeLayout(false);
      this.tabPageResults.ResumeLayout(false);
      this.tabPageResults.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageXml;
    private System.Windows.Forms.TreeView tvXml;
    private System.Windows.Forms.TabPage tabPageResults;
    private System.Windows.Forms.TextBox txtResults;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.TextBox txtXmlFile;
    private System.Windows.Forms.Label lblXmlFile;
    private System.Windows.Forms.OpenFileDialog dlgFileOpen;
    private System.Windows.Forms.Button btnClearXml;
    private System.Windows.Forms.Button btnLoadFile;
    private System.Windows.Forms.Button btnRunQuery;
    private System.Windows.Forms.Label lblXPathExpression;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.ComboBox cboXPathExpression;
    private System.Windows.Forms.Button btnDeleteFromList;
    private System.Windows.Forms.Button btnAddToList;
  }
}


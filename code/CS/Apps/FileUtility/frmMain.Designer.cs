namespace Org.FileUtility
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
      this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
      this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.button1 = new System.Windows.Forms.Button();
      this.dtpSince = new System.Windows.Forms.DateTimePicker();
      this.cboChangeTimeFrame = new System.Windows.Forms.ComboBox();
      this.cboSearchFilters = new System.Windows.Forms.ComboBox();
      this.cboSearchPath = new System.Windows.Forms.ComboBox();
      this.btnBrowsSearchPath = new System.Windows.Forms.Button();
      this.btnFindRecentChanges = new System.Windows.Forms.Button();
      this.btnFindFiles = new System.Windows.Forms.Button();
      this.btnRunSearch = new System.Windows.Forms.Button();
      this.ckUseSearchResults = new System.Windows.Forms.CheckBox();
      this.ckIncludeChildFolders = new System.Windows.Forms.CheckBox();
      this.txtFileCountLimit = new System.Windows.Forms.TextBox();
      this.txtSearchPatternsExclude = new System.Windows.Forms.TextBox();
      this.txtSearchPatternsInclude = new System.Windows.Forms.TextBox();
      this.lblFileCountLimit = new System.Windows.Forms.Label();
      this.lblExclude = new System.Windows.Forms.Label();
      this.lblInclude = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.lblFileExtensionFilters = new System.Windows.Forms.Label();
      this.lblPath = new System.Windows.Forms.Label();
      this.pnlStatusBar = new System.Windows.Forms.Panel();
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageSearchResult = new System.Windows.Forms.TabPage();
      this.ctxMnuResults = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMnuResultsViewFile = new System.Windows.Forms.ToolStripMenuItem();
      this.txtOut = new FastColoredTextBoxNS.FastColoredTextBox();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.pnlStatusBar.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageSearchResult.SuspendLayout();
      this.ctxMnuResults.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtOut)).BeginInit();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
      this.mnuMain.Size = new System.Drawing.Size(1947, 25);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 19);
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
      this.pnlTop.Controls.Add(this.button1);
      this.pnlTop.Controls.Add(this.dtpSince);
      this.pnlTop.Controls.Add(this.cboChangeTimeFrame);
      this.pnlTop.Controls.Add(this.cboSearchFilters);
      this.pnlTop.Controls.Add(this.cboSearchPath);
      this.pnlTop.Controls.Add(this.btnBrowsSearchPath);
      this.pnlTop.Controls.Add(this.btnFindRecentChanges);
      this.pnlTop.Controls.Add(this.btnFindFiles);
      this.pnlTop.Controls.Add(this.btnRunSearch);
      this.pnlTop.Controls.Add(this.ckUseSearchResults);
      this.pnlTop.Controls.Add(this.ckIncludeChildFolders);
      this.pnlTop.Controls.Add(this.txtFileCountLimit);
      this.pnlTop.Controls.Add(this.txtSearchPatternsExclude);
      this.pnlTop.Controls.Add(this.txtSearchPatternsInclude);
      this.pnlTop.Controls.Add(this.lblFileCountLimit);
      this.pnlTop.Controls.Add(this.lblExclude);
      this.pnlTop.Controls.Add(this.lblInclude);
      this.pnlTop.Controls.Add(this.label1);
      this.pnlTop.Controls.Add(this.lblFileExtensionFilters);
      this.pnlTop.Controls.Add(this.lblPath);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 25);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1947, 220);
      this.pnlTop.TabIndex = 1;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(202, 166);
      this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(112, 35);
      this.button1.TabIndex = 11;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // dtpSince
      // 
      this.dtpSince.CustomFormat = "yyyy/MM/dd HH:mm:ss";
      this.dtpSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpSince.Location = new System.Drawing.Point(1281, 115);
      this.dtpSince.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.dtpSince.Name = "dtpSince";
      this.dtpSince.Size = new System.Drawing.Size(234, 26);
      this.dtpSince.TabIndex = 10;
      // 
      // cboChangeTimeFrame
      // 
      this.cboChangeTimeFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboChangeTimeFrame.FormattingEnabled = true;
      this.cboChangeTimeFrame.Items.AddRange(new object[] {
            "Last 1 day",
            "Last 2 days",
            "Last 3 days",
            "Last 4 days",
            "Last 5 days",
            "Last 1 week",
            "Last 10 days",
            "Last 2 weeks",
            "Last 3 weeks",
            "Last 1 month",
            "Last 6 weeks",
            "Last 2 months",
            "Last 3 months",
            "Last 6 months"});
      this.cboChangeTimeFrame.Location = new System.Drawing.Point(1281, 77);
      this.cboChangeTimeFrame.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboChangeTimeFrame.Name = "cboChangeTimeFrame";
      this.cboChangeTimeFrame.Size = new System.Drawing.Size(234, 28);
      this.cboChangeTimeFrame.TabIndex = 9;
      this.cboChangeTimeFrame.SelectedIndexChanged += new System.EventHandler(this.cboChangeTimeFrame_SelectedIndexChanged);
      // 
      // cboSearchFilters
      // 
      this.cboSearchFilters.FormattingEnabled = true;
      this.cboSearchFilters.Location = new System.Drawing.Point(198, 43);
      this.cboSearchFilters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboSearchFilters.Name = "cboSearchFilters";
      this.cboSearchFilters.Size = new System.Drawing.Size(781, 28);
      this.cboSearchFilters.TabIndex = 3;
      // 
      // cboSearchPath
      // 
      this.cboSearchPath.FormattingEnabled = true;
      this.cboSearchPath.Location = new System.Drawing.Point(198, 6);
      this.cboSearchPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboSearchPath.Name = "cboSearchPath";
      this.cboSearchPath.Size = new System.Drawing.Size(781, 28);
      this.cboSearchPath.TabIndex = 1;
      // 
      // btnBrowsSearchPath
      // 
      this.btnBrowsSearchPath.Location = new System.Drawing.Point(986, 5);
      this.btnBrowsSearchPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnBrowsSearchPath.Name = "btnBrowsSearchPath";
      this.btnBrowsSearchPath.Size = new System.Drawing.Size(39, 35);
      this.btnBrowsSearchPath.TabIndex = 2;
      this.btnBrowsSearchPath.Tag = "BrowseSearchPath";
      this.btnBrowsSearchPath.Text = "...";
      this.btnBrowsSearchPath.UseVisualStyleBackColor = true;
      this.btnBrowsSearchPath.Click += new System.EventHandler(this.Action);
      // 
      // btnFindRecentChanges
      // 
      this.btnFindRecentChanges.Location = new System.Drawing.Point(1526, 75);
      this.btnFindRecentChanges.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnFindRecentChanges.Name = "btnFindRecentChanges";
      this.btnFindRecentChanges.Size = new System.Drawing.Size(210, 35);
      this.btnFindRecentChanges.TabIndex = 6;
      this.btnFindRecentChanges.Tag = "FindRecentChanges";
      this.btnFindRecentChanges.Text = "Find Recent Changes";
      this.btnFindRecentChanges.UseVisualStyleBackColor = true;
      this.btnFindRecentChanges.Click += new System.EventHandler(this.Action);
      // 
      // btnFindFiles
      // 
      this.btnFindFiles.Location = new System.Drawing.Point(1070, 49);
      this.btnFindFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnFindFiles.Name = "btnFindFiles";
      this.btnFindFiles.Size = new System.Drawing.Size(129, 35);
      this.btnFindFiles.TabIndex = 6;
      this.btnFindFiles.Tag = "FindFiles";
      this.btnFindFiles.Text = "Find Files";
      this.btnFindFiles.UseVisualStyleBackColor = true;
      this.btnFindFiles.Click += new System.EventHandler(this.Action);
      // 
      // btnRunSearch
      // 
      this.btnRunSearch.Location = new System.Drawing.Point(1070, 5);
      this.btnRunSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnRunSearch.Name = "btnRunSearch";
      this.btnRunSearch.Size = new System.Drawing.Size(129, 35);
      this.btnRunSearch.TabIndex = 6;
      this.btnRunSearch.Tag = "RunSearch";
      this.btnRunSearch.Text = "Run Search";
      this.btnRunSearch.UseVisualStyleBackColor = true;
      this.btnRunSearch.Click += new System.EventHandler(this.Action);
      // 
      // ckUseSearchResults
      // 
      this.ckUseSearchResults.AutoSize = true;
      this.ckUseSearchResults.Location = new System.Drawing.Point(1281, 38);
      this.ckUseSearchResults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ckUseSearchResults.Name = "ckUseSearchResults";
      this.ckUseSearchResults.Size = new System.Drawing.Size(185, 24);
      this.ckUseSearchResults.TabIndex = 8;
      this.ckUseSearchResults.Text = "Search Within Results";
      this.ckUseSearchResults.UseVisualStyleBackColor = true;
      // 
      // ckIncludeChildFolders
      // 
      this.ckIncludeChildFolders.AutoSize = true;
      this.ckIncludeChildFolders.Location = new System.Drawing.Point(1281, 9);
      this.ckIncludeChildFolders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.ckIncludeChildFolders.Name = "ckIncludeChildFolders";
      this.ckIncludeChildFolders.Size = new System.Drawing.Size(176, 24);
      this.ckIncludeChildFolders.TabIndex = 7;
      this.ckIncludeChildFolders.Text = "Include Child Folders";
      this.ckIncludeChildFolders.UseVisualStyleBackColor = true;
      // 
      // txtFileCountLimit
      // 
      this.txtFileCountLimit.Location = new System.Drawing.Point(1070, 115);
      this.txtFileCountLimit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtFileCountLimit.Name = "txtFileCountLimit";
      this.txtFileCountLimit.Size = new System.Drawing.Size(127, 26);
      this.txtFileCountLimit.TabIndex = 5;
      this.txtFileCountLimit.TextChanged += new System.EventHandler(this.txtSearchPatternsChanged);
      // 
      // txtSearchPatternsExclude
      // 
      this.txtSearchPatternsExclude.Location = new System.Drawing.Point(286, 115);
      this.txtSearchPatternsExclude.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtSearchPatternsExclude.Name = "txtSearchPatternsExclude";
      this.txtSearchPatternsExclude.Size = new System.Drawing.Size(692, 26);
      this.txtSearchPatternsExclude.TabIndex = 5;
      this.txtSearchPatternsExclude.TextChanged += new System.EventHandler(this.txtSearchPatternsChanged);
      // 
      // txtSearchPatternsInclude
      // 
      this.txtSearchPatternsInclude.Location = new System.Drawing.Point(286, 80);
      this.txtSearchPatternsInclude.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtSearchPatternsInclude.Name = "txtSearchPatternsInclude";
      this.txtSearchPatternsInclude.Size = new System.Drawing.Size(692, 26);
      this.txtSearchPatternsInclude.TabIndex = 4;
      this.txtSearchPatternsInclude.TextChanged += new System.EventHandler(this.txtSearchPatternsChanged);
      // 
      // lblFileCountLimit
      // 
      this.lblFileCountLimit.Location = new System.Drawing.Point(1065, 89);
      this.lblFileCountLimit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblFileCountLimit.Name = "lblFileCountLimit";
      this.lblFileCountLimit.Size = new System.Drawing.Size(134, 31);
      this.lblFileCountLimit.TabIndex = 0;
      this.lblFileCountLimit.Text = "File Count Limit";
      this.lblFileCountLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblExclude
      // 
      this.lblExclude.Location = new System.Drawing.Point(198, 111);
      this.lblExclude.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblExclude.Name = "lblExclude";
      this.lblExclude.Size = new System.Drawing.Size(80, 31);
      this.lblExclude.TabIndex = 0;
      this.lblExclude.Text = "Exclude:";
      this.lblExclude.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblInclude
      // 
      this.lblInclude.Location = new System.Drawing.Point(198, 78);
      this.lblInclude.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblInclude.Name = "lblInclude";
      this.lblInclude.Size = new System.Drawing.Size(80, 31);
      this.lblInclude.TabIndex = 0;
      this.lblInclude.Text = "Include:";
      this.lblInclude.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(18, 80);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(134, 31);
      this.label1.TabIndex = 0;
      this.label1.Text = "Search Patterns:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblFileExtensionFilters
      // 
      this.lblFileExtensionFilters.Location = new System.Drawing.Point(18, 45);
      this.lblFileExtensionFilters.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblFileExtensionFilters.Name = "lblFileExtensionFilters";
      this.lblFileExtensionFilters.Size = new System.Drawing.Size(171, 31);
      this.lblFileExtensionFilters.TabIndex = 0;
      this.lblFileExtensionFilters.Text = "File Extension Filters:";
      this.lblFileExtensionFilters.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblPath
      // 
      this.lblPath.Location = new System.Drawing.Point(18, 8);
      this.lblPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblPath.Name = "lblPath";
      this.lblPath.Size = new System.Drawing.Size(148, 32);
      this.lblPath.TabIndex = 0;
      this.lblPath.Text = "Search Path:";
      this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlStatusBar
      // 
      this.pnlStatusBar.Controls.Add(this.lblStatus);
      this.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlStatusBar.Location = new System.Drawing.Point(0, 1003);
      this.pnlStatusBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlStatusBar.Name = "pnlStatusBar";
      this.pnlStatusBar.Size = new System.Drawing.Size(1947, 42);
      this.pnlStatusBar.TabIndex = 2;
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblStatus.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblStatus.Location = new System.Drawing.Point(0, 0);
      this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1947, 42);
      this.lblStatus.TabIndex = 0;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.tabMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 245);
      this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Padding = new System.Windows.Forms.Padding(6, 6, 6, 0);
      this.pnlMain.Size = new System.Drawing.Size(1947, 758);
      this.pnlMain.TabIndex = 3;
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageSearchResult);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.Location = new System.Drawing.Point(6, 6);
      this.tabMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1935, 752);
      this.tabMain.TabIndex = 20;
      // 
      // tabPageSearchResult
      // 
      this.tabPageSearchResult.Controls.Add(this.txtOut);
      this.tabPageSearchResult.Location = new System.Drawing.Point(4, 29);
      this.tabPageSearchResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageSearchResult.Name = "tabPageSearchResult";
      this.tabPageSearchResult.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageSearchResult.Size = new System.Drawing.Size(1927, 719);
      this.tabPageSearchResult.TabIndex = 0;
      this.tabPageSearchResult.Text = "Search Results";
      this.tabPageSearchResult.UseVisualStyleBackColor = true;
      // 
      // ctxMnuResults
      // 
      this.ctxMnuResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMnuResultsViewFile});
      this.ctxMnuResults.Name = "ctxMnuResults";
      this.ctxMnuResults.Size = new System.Drawing.Size(121, 26);
      this.ctxMnuResults.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMnuResults_Opening);
      // 
      // ctxMnuResultsViewFile
      // 
      this.ctxMnuResultsViewFile.Name = "ctxMnuResultsViewFile";
      this.ctxMnuResultsViewFile.Size = new System.Drawing.Size(120, 22);
      this.ctxMnuResultsViewFile.Tag = "ViewFile";
      this.ctxMnuResultsViewFile.Text = "&View File";
      this.ctxMnuResultsViewFile.Click += new System.EventHandler(this.Action);
      // 
      // txtOut
      // 
      this.txtOut.AutoCompleteBracketsList = new char[] {
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
      this.txtOut.AutoScrollMinSize = new System.Drawing.Size(27, 14);
      this.txtOut.BackBrush = null;
      this.txtOut.CharHeight = 14;
      this.txtOut.CharWidth = 8;
      this.txtOut.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtOut.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9.75F);
      this.txtOut.IsReplaceMode = false;
      this.txtOut.Location = new System.Drawing.Point(4, 5);
      this.txtOut.Name = "txtOut";
      this.txtOut.Paddings = new System.Windows.Forms.Padding(0);
      this.txtOut.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtOut.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtOut.ServiceColors")));
      this.txtOut.Size = new System.Drawing.Size(1919, 709);
      this.txtOut.TabIndex = 0;
      this.txtOut.Zoom = 100;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1947, 1045);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlStatusBar);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "File Search Utility";
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.pnlStatusBar.ResumeLayout(false);
      this.pnlMain.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageSearchResult.ResumeLayout(false);
      this.ctxMnuResults.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtOut)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.OpenFileDialog dlgFileOpen;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblFileExtensionFilters;
        private System.Windows.Forms.CheckBox ckIncludeChildFolders;
        private System.Windows.Forms.TextBox txtSearchPatternsInclude;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlStatusBar;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnRunSearch;
        private System.Windows.Forms.TextBox txtSearchPatternsExclude;
        private System.Windows.Forms.Label lblExclude;
        private System.Windows.Forms.Label lblInclude;
        private System.Windows.Forms.CheckBox ckUseSearchResults;
        private System.Windows.Forms.ComboBox cboSearchPath;
        private System.Windows.Forms.Button btnBrowsSearchPath;
        private System.Windows.Forms.ComboBox cboSearchFilters;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageSearchResult;
        private System.Windows.Forms.ContextMenuStrip ctxMnuResults;
        private System.Windows.Forms.ToolStripMenuItem ctxMnuResultsViewFile;
        private System.Windows.Forms.ComboBox cboChangeTimeFrame;
        private System.Windows.Forms.Button btnFindRecentChanges;
        private System.Windows.Forms.DateTimePicker dtpSince;
				private System.Windows.Forms.Button btnFindFiles;
        private System.Windows.Forms.TextBox txtFileCountLimit;
        private System.Windows.Forms.Label lblFileCountLimit;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button button1;
        private FastColoredTextBoxNS.FastColoredTextBox txtOut;
    }
}


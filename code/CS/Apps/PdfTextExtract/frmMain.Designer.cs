namespace Org.PdfTextExtract
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
      this.ckSuppressSections = new System.Windows.Forms.CheckBox();
      this.ckKeepBreakpointEnabled = new System.Windows.Forms.CheckBox();
      this.ckBreakpointEnabled = new System.Windows.Forms.CheckBox();
      this.btnReloadFormats = new System.Windows.Forms.Button();
      this.lblFileNameFilters = new System.Windows.Forms.Label();
      this.lblFolder = new System.Windows.Forms.Label();
      this.cboFileNameFilters = new System.Windows.Forms.ComboBox();
      this.cboRootFolder = new System.Windows.Forms.ComboBox();
      this.btnLoadFiles = new System.Windows.Forms.Button();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuToolWindows = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuToolWindowsToggleTreeView = new System.Windows.Forms.ToolStripMenuItem();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPagePdfFiles = new System.Windows.Forms.TabPage();
      this.splitterPdfFiles = new System.Windows.Forms.SplitContainer();
      this.ckListFormats = new System.Windows.Forms.CheckedListBox();
      this.pnlPdfFiltersTop = new System.Windows.Forms.Panel();
      this.ckUndefinedOnly = new System.Windows.Forms.CheckBox();
      this.ckUseFormatFilters = new System.Windows.Forms.CheckBox();
      this.gvFiles = new System.Windows.Forms.DataGridView();
      this.ctxMnuFileGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMnuFileGridProcessFiles = new System.Windows.Forms.ToolStripMenuItem();
      this.lblPdfFiles = new System.Windows.Forms.Label();
      this.pnlFilesTop = new System.Windows.Forms.Panel();
      this.btnLoadDxWorkbook = new System.Windows.Forms.Button();
      this.btnDefineTextStructure = new System.Windows.Forms.Button();
      this.btnRecognizeFormats = new System.Windows.Forms.Button();
      this.btnFilterList = new System.Windows.Forms.Button();
      this.tabPagePdfViewer = new System.Windows.Forms.TabPage();
      this.pnlPdfViewerDockingTarget = new System.Windows.Forms.Panel();
      this.tabPageRawExtractedText = new System.Windows.Forms.TabPage();
      this.pnlRawTextExtractDockingTarget = new System.Windows.Forms.Panel();
      this.tabPageFormatRecognition = new System.Windows.Forms.TabPage();
      this.pnlFormatRecognitionDockingTarget = new System.Windows.Forms.Panel();
      this.tabPageConfigEdit = new System.Windows.Forms.TabPage();
      this.pnlConfigEditorDockingTarget = new System.Windows.Forms.Panel();
      this.tabPageTextExtractDesigner = new System.Windows.Forms.TabPage();
      this.pnlTextExtractDesignerDockingTarget = new System.Windows.Forms.Panel();
      this.tabPageTextExtractResults = new System.Windows.Forms.TabPage();
      this.pnlTextExtractResultsDockingTarget = new System.Windows.Forms.Panel();
      this.tabPageTextExtractErrors = new System.Windows.Forms.TabPage();
      this.pnlTextExtractErrorsDockingTarget = new System.Windows.Forms.Panel();
      this.lblStatus = new System.Windows.Forms.Label();
      this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
      this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
      this.pnlTop.SuspendLayout();
      this.mnuMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPagePdfFiles.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterPdfFiles)).BeginInit();
      this.splitterPdfFiles.Panel1.SuspendLayout();
      this.splitterPdfFiles.Panel2.SuspendLayout();
      this.splitterPdfFiles.SuspendLayout();
      this.pnlPdfFiltersTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvFiles)).BeginInit();
      this.ctxMnuFileGrid.SuspendLayout();
      this.pnlFilesTop.SuspendLayout();
      this.tabPagePdfViewer.SuspendLayout();
      this.tabPageRawExtractedText.SuspendLayout();
      this.tabPageFormatRecognition.SuspendLayout();
      this.tabPageConfigEdit.SuspendLayout();
      this.tabPageTextExtractDesigner.SuspendLayout();
      this.tabPageTextExtractResults.SuspendLayout();
      this.tabPageTextExtractErrors.SuspendLayout();
      this.SuspendLayout();
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.ckSuppressSections);
      this.pnlTop.Controls.Add(this.ckKeepBreakpointEnabled);
      this.pnlTop.Controls.Add(this.ckBreakpointEnabled);
      this.pnlTop.Controls.Add(this.btnReloadFormats);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(2);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1244, 34);
      this.pnlTop.TabIndex = 1;
      //
      // ckSuppressSections
      //
      this.ckSuppressSections.AutoSize = true;
      this.ckSuppressSections.Location = new System.Drawing.Point(855, 9);
      this.ckSuppressSections.Name = "ckSuppressSections";
      this.ckSuppressSections.Size = new System.Drawing.Size(160, 17);
      this.ckSuppressSections.TabIndex = 3;
      this.ckSuppressSections.Text = "Suppress Extract By Section";
      this.ckSuppressSections.UseVisualStyleBackColor = true;
      //
      // ckKeepBreakpointEnabled
      //
      this.ckKeepBreakpointEnabled.AutoSize = true;
      this.ckKeepBreakpointEnabled.Location = new System.Drawing.Point(702, 10);
      this.ckKeepBreakpointEnabled.Name = "ckKeepBreakpointEnabled";
      this.ckKeepBreakpointEnabled.Size = new System.Drawing.Size(147, 17);
      this.ckKeepBreakpointEnabled.TabIndex = 1;
      this.ckKeepBreakpointEnabled.Tag = "KeepBreakpointEnabled";
      this.ckKeepBreakpointEnabled.Text = "Keep Breakpoint Enabled";
      this.ckKeepBreakpointEnabled.UseVisualStyleBackColor = true;
      this.ckKeepBreakpointEnabled.CheckedChanged += new System.EventHandler(this.Action);
      //
      // ckBreakpointEnabled
      //
      this.ckBreakpointEnabled.AutoSize = true;
      this.ckBreakpointEnabled.Location = new System.Drawing.Point(576, 10);
      this.ckBreakpointEnabled.Name = "ckBreakpointEnabled";
      this.ckBreakpointEnabled.Size = new System.Drawing.Size(119, 17);
      this.ckBreakpointEnabled.TabIndex = 1;
      this.ckBreakpointEnabled.Tag = "BreakpointEnabled";
      this.ckBreakpointEnabled.Text = "Breakpoint Enabled";
      this.ckBreakpointEnabled.UseVisualStyleBackColor = true;
      this.ckBreakpointEnabled.CheckedChanged += new System.EventHandler(this.Action);
      //
      // btnReloadFormats
      //
      this.btnReloadFormats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.btnReloadFormats.Location = new System.Drawing.Point(18, 5);
      this.btnReloadFormats.Margin = new System.Windows.Forms.Padding(2);
      this.btnReloadFormats.Name = "btnReloadFormats";
      this.btnReloadFormats.Size = new System.Drawing.Size(122, 23);
      this.btnReloadFormats.TabIndex = 0;
      this.btnReloadFormats.Tag = "ReloadFormats";
      this.btnReloadFormats.Text = "Reload Formats";
      this.btnReloadFormats.UseVisualStyleBackColor = true;
      this.btnReloadFormats.Click += new System.EventHandler(this.Action);
      //
      // lblFileNameFilters
      //
      this.lblFileNameFilters.AutoSize = true;
      this.lblFileNameFilters.Location = new System.Drawing.Point(18, 42);
      this.lblFileNameFilters.Name = "lblFileNameFilters";
      this.lblFileNameFilters.Size = new System.Drawing.Size(84, 13);
      this.lblFileNameFilters.TabIndex = 2;
      this.lblFileNameFilters.Text = "File Name Filters";
      //
      // lblFolder
      //
      this.lblFolder.AutoSize = true;
      this.lblFolder.Location = new System.Drawing.Point(18, 19);
      this.lblFolder.Name = "lblFolder";
      this.lblFolder.Size = new System.Drawing.Size(62, 13);
      this.lblFolder.TabIndex = 2;
      this.lblFolder.Text = "Root Folder";
      //
      // cboFileNameFilters
      //
      this.cboFileNameFilters.FormattingEnabled = true;
      this.cboFileNameFilters.Location = new System.Drawing.Point(108, 38);
      this.cboFileNameFilters.Name = "cboFileNameFilters";
      this.cboFileNameFilters.Size = new System.Drawing.Size(589, 21);
      this.cboFileNameFilters.TabIndex = 1;
      //
      // cboRootFolder
      //
      this.cboRootFolder.FormattingEnabled = true;
      this.cboRootFolder.Location = new System.Drawing.Point(108, 15);
      this.cboRootFolder.Name = "cboRootFolder";
      this.cboRootFolder.Size = new System.Drawing.Size(589, 21);
      this.cboRootFolder.TabIndex = 1;
      this.cboRootFolder.SelectedIndexChanged += new System.EventHandler(this.cboRootFolder_SelectedIndexChanged);
      //
      // btnLoadFiles
      //
      this.btnLoadFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.btnLoadFiles.Location = new System.Drawing.Point(702, 14);
      this.btnLoadFiles.Margin = new System.Windows.Forms.Padding(2);
      this.btnLoadFiles.Name = "btnLoadFiles";
      this.btnLoadFiles.Size = new System.Drawing.Size(100, 23);
      this.btnLoadFiles.TabIndex = 0;
      this.btnLoadFiles.Tag = "LoadFiles";
      this.btnLoadFiles.Text = "Load Files";
      this.btnLoadFiles.UseVisualStyleBackColor = true;
      this.btnLoadFiles.Click += new System.EventHandler(this.Action);
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile,
        this.mnuToolWindows
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
      this.mnuMain.Size = new System.Drawing.Size(1244, 24);
      this.mnuMain.TabIndex = 2;
      this.mnuMain.Text = "menuStrip1";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFileSaveAs,
        this.mnuFileExit
      });
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 22);
      this.mnuFile.Tag = "";
      this.mnuFile.Text = "&File";
      //
      // mnuFileExit
      //
      this.mnuFileExit.Name = "mnuFileExit";
      this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // mnuToolWindows
      //
      this.mnuToolWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuToolWindowsToggleTreeView
      });
      this.mnuToolWindows.Name = "mnuToolWindows";
      this.mnuToolWindows.Size = new System.Drawing.Size(94, 22);
      this.mnuToolWindows.Text = "Tool Windows";
      //
      // mnuToolWindowsToggleTreeView
      //
      this.mnuToolWindowsToggleTreeView.Name = "mnuToolWindowsToggleTreeView";
      this.mnuToolWindowsToggleTreeView.Size = new System.Drawing.Size(160, 22);
      this.mnuToolWindowsToggleTreeView.Tag = "TW_Float_TextExtractDesigner";
      this.mnuToolWindowsToggleTreeView.Text = "Toggle TreeView";
      this.mnuToolWindowsToggleTreeView.Click += new System.EventHandler(this.Action);
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPagePdfFiles);
      this.tabMain.Controls.Add(this.tabPagePdfViewer);
      this.tabMain.Controls.Add(this.tabPageRawExtractedText);
      this.tabMain.Controls.Add(this.tabPageFormatRecognition);
      this.tabMain.Controls.Add(this.tabPageConfigEdit);
      this.tabMain.Controls.Add(this.tabPageTextExtractDesigner);
      this.tabMain.Controls.Add(this.tabPageTextExtractResults);
      this.tabMain.Controls.Add(this.tabPageTextExtractErrors);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.tabMain.ItemSize = new System.Drawing.Size(135, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 58);
      this.tabMain.Margin = new System.Windows.Forms.Padding(2);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1244, 597);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 3;
      this.tabMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabMain_MouseDoubleClick);
      //
      // tabPagePdfFiles
      //
      this.tabPagePdfFiles.Controls.Add(this.splitterPdfFiles);
      this.tabPagePdfFiles.Controls.Add(this.pnlFilesTop);
      this.tabPagePdfFiles.Location = new System.Drawing.Point(4, 22);
      this.tabPagePdfFiles.Name = "tabPagePdfFiles";
      this.tabPagePdfFiles.Size = new System.Drawing.Size(1236, 571);
      this.tabPagePdfFiles.TabIndex = 3;
      this.tabPagePdfFiles.Tag = "TabPage_PdfFiles";
      this.tabPagePdfFiles.Text = "PDF Files";
      this.tabPagePdfFiles.UseVisualStyleBackColor = true;
      //
      // splitterPdfFiles
      //
      this.splitterPdfFiles.BackColor = System.Drawing.SystemColors.Control;
      this.splitterPdfFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterPdfFiles.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitterPdfFiles.Location = new System.Drawing.Point(0, 95);
      this.splitterPdfFiles.Name = "splitterPdfFiles";
      //
      // splitterPdfFiles.Panel1
      //
      this.splitterPdfFiles.Panel1.Controls.Add(this.ckListFormats);
      this.splitterPdfFiles.Panel1.Controls.Add(this.pnlPdfFiltersTop);
      //
      // splitterPdfFiles.Panel2
      //
      this.splitterPdfFiles.Panel2.Controls.Add(this.gvFiles);
      this.splitterPdfFiles.Panel2.Controls.Add(this.lblPdfFiles);
      this.splitterPdfFiles.Size = new System.Drawing.Size(1236, 476);
      this.splitterPdfFiles.SplitterDistance = 180;
      this.splitterPdfFiles.SplitterWidth = 3;
      this.splitterPdfFiles.TabIndex = 9;
      //
      // ckListFormats
      //
      this.ckListFormats.CheckOnClick = true;
      this.ckListFormats.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ckListFormats.FormattingEnabled = true;
      this.ckListFormats.Location = new System.Drawing.Point(0, 23);
      this.ckListFormats.Name = "ckListFormats";
      this.ckListFormats.Size = new System.Drawing.Size(180, 453);
      this.ckListFormats.TabIndex = 6;
      this.ckListFormats.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ckListFormats_ItemCheck);
      this.ckListFormats.SelectedIndexChanged += new System.EventHandler(this.FormatControls_Changed);
      this.ckListFormats.SelectedValueChanged += new System.EventHandler(this.FormatControls_Changed);
      //
      // pnlPdfFiltersTop
      //
      this.pnlPdfFiltersTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnlPdfFiltersTop.Controls.Add(this.ckUndefinedOnly);
      this.pnlPdfFiltersTop.Controls.Add(this.ckUseFormatFilters);
      this.pnlPdfFiltersTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlPdfFiltersTop.Location = new System.Drawing.Point(0, 0);
      this.pnlPdfFiltersTop.Name = "pnlPdfFiltersTop";
      this.pnlPdfFiltersTop.Size = new System.Drawing.Size(180, 23);
      this.pnlPdfFiltersTop.TabIndex = 9;
      //
      // ckUndefinedOnly
      //
      this.ckUndefinedOnly.AutoSize = true;
      this.ckUndefinedOnly.Dock = System.Windows.Forms.DockStyle.Right;
      this.ckUndefinedOnly.Location = new System.Drawing.Point(91, 0);
      this.ckUndefinedOnly.Name = "ckUndefinedOnly";
      this.ckUndefinedOnly.Size = new System.Drawing.Size(87, 21);
      this.ckUndefinedOnly.TabIndex = 9;
      this.ckUndefinedOnly.Tag = "UndefinedFormatOnly";
      this.ckUndefinedOnly.Text = "UNDEF Only";
      this.ckUndefinedOnly.UseVisualStyleBackColor = true;
      this.ckUndefinedOnly.CheckedChanged += new System.EventHandler(this.Action);
      //
      // ckUseFormatFilters
      //
      this.ckUseFormatFilters.AutoSize = true;
      this.ckUseFormatFilters.Checked = true;
      this.ckUseFormatFilters.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckUseFormatFilters.Dock = System.Windows.Forms.DockStyle.Left;
      this.ckUseFormatFilters.Location = new System.Drawing.Point(0, 0);
      this.ckUseFormatFilters.Name = "ckUseFormatFilters";
      this.ckUseFormatFilters.Size = new System.Drawing.Size(75, 21);
      this.ckUseFormatFilters.TabIndex = 8;
      this.ckUseFormatFilters.Tag = "";
      this.ckUseFormatFilters.Text = "Use Filters";
      this.ckUseFormatFilters.UseVisualStyleBackColor = true;
      this.ckUseFormatFilters.CheckedChanged += new System.EventHandler(this.FormatControls_Changed);
      //
      // gvFiles
      //
      this.gvFiles.AllowUserToAddRows = false;
      this.gvFiles.AllowUserToDeleteRows = false;
      this.gvFiles.AllowUserToResizeRows = false;
      this.gvFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.gvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvFiles.ContextMenuStrip = this.ctxMnuFileGrid;
      this.gvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvFiles.Location = new System.Drawing.Point(0, 23);
      this.gvFiles.MultiSelect = false;
      this.gvFiles.Name = "gvFiles";
      this.gvFiles.RowHeadersVisible = false;
      this.gvFiles.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvFiles.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DodgerBlue;
      this.gvFiles.RowTemplate.Height = 19;
      this.gvFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvFiles.Size = new System.Drawing.Size(1053, 453);
      this.gvFiles.TabIndex = 5;
      this.gvFiles.SelectionChanged += new System.EventHandler(this.gvFiles_SelectionChanged);
      this.gvFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvFiles_MouseDown);
      this.gvFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvFiles_MouseUp);
      //
      // ctxMnuFileGrid
      //
      this.ctxMnuFileGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMnuFileGridProcessFiles
      });
      this.ctxMnuFileGrid.Name = "ctxMnuFileGrid";
      this.ctxMnuFileGrid.Size = new System.Drawing.Size(136, 26);
      this.ctxMnuFileGrid.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMnuFileGrid_Opening);
      //
      // ctxMnuFileGridProcessFiles
      //
      this.ctxMnuFileGridProcessFiles.Name = "ctxMnuFileGridProcessFiles";
      this.ctxMnuFileGridProcessFiles.Size = new System.Drawing.Size(135, 22);
      this.ctxMnuFileGridProcessFiles.Tag = "ProcessFile";
      this.ctxMnuFileGridProcessFiles.Text = "&Process File";
      this.ctxMnuFileGridProcessFiles.Click += new System.EventHandler(this.Action);
      //
      // lblPdfFiles
      //
      this.lblPdfFiles.BackColor = System.Drawing.SystemColors.Control;
      this.lblPdfFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblPdfFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblPdfFiles.Location = new System.Drawing.Point(0, 0);
      this.lblPdfFiles.Name = "lblPdfFiles";
      this.lblPdfFiles.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblPdfFiles.Size = new System.Drawing.Size(1053, 23);
      this.lblPdfFiles.TabIndex = 8;
      this.lblPdfFiles.Text = "Files located in selected path";
      this.lblPdfFiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlFilesTop
      //
      this.pnlFilesTop.BackColor = System.Drawing.SystemColors.Control;
      this.pnlFilesTop.Controls.Add(this.lblFileNameFilters);
      this.pnlFilesTop.Controls.Add(this.lblFolder);
      this.pnlFilesTop.Controls.Add(this.btnLoadDxWorkbook);
      this.pnlFilesTop.Controls.Add(this.btnDefineTextStructure);
      this.pnlFilesTop.Controls.Add(this.btnRecognizeFormats);
      this.pnlFilesTop.Controls.Add(this.btnFilterList);
      this.pnlFilesTop.Controls.Add(this.btnLoadFiles);
      this.pnlFilesTop.Controls.Add(this.cboFileNameFilters);
      this.pnlFilesTop.Controls.Add(this.cboRootFolder);
      this.pnlFilesTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlFilesTop.Location = new System.Drawing.Point(0, 0);
      this.pnlFilesTop.Name = "pnlFilesTop";
      this.pnlFilesTop.Size = new System.Drawing.Size(1236, 95);
      this.pnlFilesTop.TabIndex = 1;
      //
      // btnLoadDxWorkbook
      //
      this.btnLoadDxWorkbook.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.btnLoadDxWorkbook.Location = new System.Drawing.Point(1018, 14);
      this.btnLoadDxWorkbook.Margin = new System.Windows.Forms.Padding(2);
      this.btnLoadDxWorkbook.Name = "btnLoadDxWorkbook";
      this.btnLoadDxWorkbook.Size = new System.Drawing.Size(137, 23);
      this.btnLoadDxWorkbook.TabIndex = 0;
      this.btnLoadDxWorkbook.Tag = "LoadDxWorkbook";
      this.btnLoadDxWorkbook.Text = "Load DxWorkbook";
      this.btnLoadDxWorkbook.UseVisualStyleBackColor = true;
      this.btnLoadDxWorkbook.Click += new System.EventHandler(this.Action);
      //
      // btnDefineTextStructure
      //
      this.btnDefineTextStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.btnDefineTextStructure.Location = new System.Drawing.Point(881, 37);
      this.btnDefineTextStructure.Margin = new System.Windows.Forms.Padding(2);
      this.btnDefineTextStructure.Name = "btnDefineTextStructure";
      this.btnDefineTextStructure.Size = new System.Drawing.Size(122, 23);
      this.btnDefineTextStructure.TabIndex = 0;
      this.btnDefineTextStructure.Tag = "DefineTextStructure";
      this.btnDefineTextStructure.Text = "Define Text Structure";
      this.btnDefineTextStructure.UseVisualStyleBackColor = true;
      this.btnDefineTextStructure.Click += new System.EventHandler(this.Action);
      //
      // btnRecognizeFormats
      //
      this.btnRecognizeFormats.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.btnRecognizeFormats.Location = new System.Drawing.Point(881, 14);
      this.btnRecognizeFormats.Margin = new System.Windows.Forms.Padding(2);
      this.btnRecognizeFormats.Name = "btnRecognizeFormats";
      this.btnRecognizeFormats.Size = new System.Drawing.Size(122, 23);
      this.btnRecognizeFormats.TabIndex = 0;
      this.btnRecognizeFormats.Tag = "RecognizeFormats";
      this.btnRecognizeFormats.Text = "Recognize Formats";
      this.btnRecognizeFormats.UseVisualStyleBackColor = true;
      this.btnRecognizeFormats.Click += new System.EventHandler(this.Action);
      //
      // btnFilterList
      //
      this.btnFilterList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.btnFilterList.Location = new System.Drawing.Point(702, 37);
      this.btnFilterList.Margin = new System.Windows.Forms.Padding(2);
      this.btnFilterList.Name = "btnFilterList";
      this.btnFilterList.Size = new System.Drawing.Size(100, 23);
      this.btnFilterList.TabIndex = 0;
      this.btnFilterList.Tag = "LoadFilteredList";
      this.btnFilterList.Text = "Filter List";
      this.btnFilterList.UseVisualStyleBackColor = true;
      this.btnFilterList.Click += new System.EventHandler(this.Action);
      //
      // tabPagePdfViewer
      //
      this.tabPagePdfViewer.Controls.Add(this.pnlPdfViewerDockingTarget);
      this.tabPagePdfViewer.Location = new System.Drawing.Point(4, 22);
      this.tabPagePdfViewer.Margin = new System.Windows.Forms.Padding(2);
      this.tabPagePdfViewer.Name = "tabPagePdfViewer";
      this.tabPagePdfViewer.Padding = new System.Windows.Forms.Padding(2);
      this.tabPagePdfViewer.Size = new System.Drawing.Size(1236, 571);
      this.tabPagePdfViewer.TabIndex = 1;
      this.tabPagePdfViewer.Tag = "TabPage_PdfViewer";
      this.tabPagePdfViewer.Text = "PDF Viewer";
      this.tabPagePdfViewer.UseVisualStyleBackColor = true;
      //
      // pnlPdfViewerDockingTarget
      //
      this.pnlPdfViewerDockingTarget.BackColor = System.Drawing.SystemColors.Control;
      this.pnlPdfViewerDockingTarget.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlPdfViewerDockingTarget.Location = new System.Drawing.Point(2, 2);
      this.pnlPdfViewerDockingTarget.Name = "pnlPdfViewerDockingTarget";
      this.pnlPdfViewerDockingTarget.Size = new System.Drawing.Size(1232, 567);
      this.pnlPdfViewerDockingTarget.TabIndex = 5;
      this.pnlPdfViewerDockingTarget.Tag = "DockTarget_PdfViewer";
      //
      // tabPageRawExtractedText
      //
      this.tabPageRawExtractedText.Controls.Add(this.pnlRawTextExtractDockingTarget);
      this.tabPageRawExtractedText.Location = new System.Drawing.Point(4, 22);
      this.tabPageRawExtractedText.Margin = new System.Windows.Forms.Padding(2);
      this.tabPageRawExtractedText.Name = "tabPageRawExtractedText";
      this.tabPageRawExtractedText.Padding = new System.Windows.Forms.Padding(2);
      this.tabPageRawExtractedText.Size = new System.Drawing.Size(1236, 571);
      this.tabPageRawExtractedText.TabIndex = 0;
      this.tabPageRawExtractedText.Tag = "TabPage_RawExtractedText";
      this.tabPageRawExtractedText.Text = "Raw Extracted Text";
      this.tabPageRawExtractedText.UseVisualStyleBackColor = true;
      //
      // pnlRawTextExtractDockingTarget
      //
      this.pnlRawTextExtractDockingTarget.BackColor = System.Drawing.SystemColors.Control;
      this.pnlRawTextExtractDockingTarget.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlRawTextExtractDockingTarget.Location = new System.Drawing.Point(2, 2);
      this.pnlRawTextExtractDockingTarget.Name = "pnlRawTextExtractDockingTarget";
      this.pnlRawTextExtractDockingTarget.Size = new System.Drawing.Size(1232, 567);
      this.pnlRawTextExtractDockingTarget.TabIndex = 5;
      this.pnlRawTextExtractDockingTarget.Tag = "DockTarget_RawExtractedText";
      //
      // tabPageFormatRecognition
      //
      this.tabPageFormatRecognition.Controls.Add(this.pnlFormatRecognitionDockingTarget);
      this.tabPageFormatRecognition.Location = new System.Drawing.Point(4, 22);
      this.tabPageFormatRecognition.Name = "tabPageFormatRecognition";
      this.tabPageFormatRecognition.Size = new System.Drawing.Size(1236, 571);
      this.tabPageFormatRecognition.TabIndex = 2;
      this.tabPageFormatRecognition.Tag = "TabPage_FormatRecognition";
      this.tabPageFormatRecognition.Text = "Format Recognition";
      this.tabPageFormatRecognition.UseVisualStyleBackColor = true;
      //
      // pnlFormatRecognitionDockingTarget
      //
      this.pnlFormatRecognitionDockingTarget.BackColor = System.Drawing.SystemColors.Control;
      this.pnlFormatRecognitionDockingTarget.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlFormatRecognitionDockingTarget.Location = new System.Drawing.Point(0, 0);
      this.pnlFormatRecognitionDockingTarget.Name = "pnlFormatRecognitionDockingTarget";
      this.pnlFormatRecognitionDockingTarget.Size = new System.Drawing.Size(1236, 571);
      this.pnlFormatRecognitionDockingTarget.TabIndex = 6;
      this.pnlFormatRecognitionDockingTarget.Tag = "DockTarget_FormatRecognition";
      //
      // tabPageConfigEdit
      //
      this.tabPageConfigEdit.Controls.Add(this.pnlConfigEditorDockingTarget);
      this.tabPageConfigEdit.Location = new System.Drawing.Point(4, 22);
      this.tabPageConfigEdit.Margin = new System.Windows.Forms.Padding(2);
      this.tabPageConfigEdit.Name = "tabPageConfigEdit";
      this.tabPageConfigEdit.Size = new System.Drawing.Size(1236, 571);
      this.tabPageConfigEdit.TabIndex = 5;
      this.tabPageConfigEdit.Tag = "TabPage_ConfigEdit";
      this.tabPageConfigEdit.Text = "Configuration Editor";
      this.tabPageConfigEdit.ToolTipText = "TabPage_ConfigEdit";
      this.tabPageConfigEdit.UseVisualStyleBackColor = true;
      //
      // pnlConfigEditorDockingTarget
      //
      this.pnlConfigEditorDockingTarget.BackColor = System.Drawing.SystemColors.Control;
      this.pnlConfigEditorDockingTarget.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlConfigEditorDockingTarget.Location = new System.Drawing.Point(0, 0);
      this.pnlConfigEditorDockingTarget.Name = "pnlConfigEditorDockingTarget";
      this.pnlConfigEditorDockingTarget.Size = new System.Drawing.Size(1236, 571);
      this.pnlConfigEditorDockingTarget.TabIndex = 7;
      this.pnlConfigEditorDockingTarget.Tag = "DockTarget_ConfigEdit";
      //
      // tabPageTextExtractDesigner
      //
      this.tabPageTextExtractDesigner.Controls.Add(this.pnlTextExtractDesignerDockingTarget);
      this.tabPageTextExtractDesigner.Location = new System.Drawing.Point(4, 22);
      this.tabPageTextExtractDesigner.Name = "tabPageTextExtractDesigner";
      this.tabPageTextExtractDesigner.Size = new System.Drawing.Size(1236, 571);
      this.tabPageTextExtractDesigner.TabIndex = 4;
      this.tabPageTextExtractDesigner.Tag = "TabPage_TextExtractDesigner";
      this.tabPageTextExtractDesigner.Text = "Text Extract Designer";
      this.tabPageTextExtractDesigner.UseVisualStyleBackColor = true;
      //
      // pnlTextExtractDesignerDockingTarget
      //
      this.pnlTextExtractDesignerDockingTarget.BackColor = System.Drawing.SystemColors.Control;
      this.pnlTextExtractDesignerDockingTarget.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlTextExtractDesignerDockingTarget.Location = new System.Drawing.Point(0, 0);
      this.pnlTextExtractDesignerDockingTarget.Name = "pnlTextExtractDesignerDockingTarget";
      this.pnlTextExtractDesignerDockingTarget.Size = new System.Drawing.Size(1236, 571);
      this.pnlTextExtractDesignerDockingTarget.TabIndex = 4;
      this.pnlTextExtractDesignerDockingTarget.Tag = "DockTarget_TextExtractDesigner";
      //
      // tabPageTextExtractResults
      //
      this.tabPageTextExtractResults.Controls.Add(this.pnlTextExtractResultsDockingTarget);
      this.tabPageTextExtractResults.Location = new System.Drawing.Point(4, 22);
      this.tabPageTextExtractResults.Margin = new System.Windows.Forms.Padding(2);
      this.tabPageTextExtractResults.Name = "tabPageTextExtractResults";
      this.tabPageTextExtractResults.Size = new System.Drawing.Size(1236, 571);
      this.tabPageTextExtractResults.TabIndex = 6;
      this.tabPageTextExtractResults.Tag = "TabPage_TextExtractResults";
      this.tabPageTextExtractResults.Text = "Text Extract Results";
      this.tabPageTextExtractResults.UseVisualStyleBackColor = true;
      //
      // pnlTextExtractResultsDockingTarget
      //
      this.pnlTextExtractResultsDockingTarget.BackColor = System.Drawing.SystemColors.Control;
      this.pnlTextExtractResultsDockingTarget.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlTextExtractResultsDockingTarget.Location = new System.Drawing.Point(0, 0);
      this.pnlTextExtractResultsDockingTarget.Name = "pnlTextExtractResultsDockingTarget";
      this.pnlTextExtractResultsDockingTarget.Size = new System.Drawing.Size(1236, 571);
      this.pnlTextExtractResultsDockingTarget.TabIndex = 5;
      this.pnlTextExtractResultsDockingTarget.Tag = "DockTarget_TextExtractResults";
      //
      // tabPageTextExtractErrors
      //
      this.tabPageTextExtractErrors.Controls.Add(this.pnlTextExtractErrorsDockingTarget);
      this.tabPageTextExtractErrors.Location = new System.Drawing.Point(4, 22);
      this.tabPageTextExtractErrors.Name = "tabPageTextExtractErrors";
      this.tabPageTextExtractErrors.Size = new System.Drawing.Size(1236, 571);
      this.tabPageTextExtractErrors.TabIndex = 7;
      this.tabPageTextExtractErrors.Tag = "TabPage_TextExtractErrors";
      this.tabPageTextExtractErrors.Text = "Text Extract Errors";
      this.tabPageTextExtractErrors.UseVisualStyleBackColor = true;
      //
      // pnlTextExtractErrorsDockingTarget
      //
      this.pnlTextExtractErrorsDockingTarget.BackColor = System.Drawing.SystemColors.Control;
      this.pnlTextExtractErrorsDockingTarget.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlTextExtractErrorsDockingTarget.Location = new System.Drawing.Point(0, 0);
      this.pnlTextExtractErrorsDockingTarget.Name = "pnlTextExtractErrorsDockingTarget";
      this.pnlTextExtractErrorsDockingTarget.Size = new System.Drawing.Size(1236, 571);
      this.pnlTextExtractErrorsDockingTarget.TabIndex = 6;
      this.pnlTextExtractErrorsDockingTarget.Tag = "DockTarget_TextExtractErrors";
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.lblStatus.Location = new System.Drawing.Point(0, 655);
      this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1244, 24);
      this.lblStatus.TabIndex = 4;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // mnuFileSaveAs
      //
      this.mnuFileSaveAs.Name = "mnuFileSaveAs";
      this.mnuFileSaveAs.Size = new System.Drawing.Size(152, 22);
      this.mnuFileSaveAs.Tag = "SaveAs";
      this.mnuFileSaveAs.Text = "Save &As";
      this.mnuFileSaveAs.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1244, 679);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(1);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Tag = "MainForm";
      this.Text = "PDF Text Extractor";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabPagePdfFiles.ResumeLayout(false);
      this.splitterPdfFiles.Panel1.ResumeLayout(false);
      this.splitterPdfFiles.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterPdfFiles)).EndInit();
      this.splitterPdfFiles.ResumeLayout(false);
      this.pnlPdfFiltersTop.ResumeLayout(false);
      this.pnlPdfFiltersTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvFiles)).EndInit();
      this.ctxMnuFileGrid.ResumeLayout(false);
      this.pnlFilesTop.ResumeLayout(false);
      this.pnlFilesTop.PerformLayout();
      this.tabPagePdfViewer.ResumeLayout(false);
      this.tabPageRawExtractedText.ResumeLayout(false);
      this.tabPageFormatRecognition.ResumeLayout(false);
      this.tabPageConfigEdit.ResumeLayout(false);
      this.tabPageTextExtractDesigner.ResumeLayout(false);
      this.tabPageTextExtractResults.ResumeLayout(false);
      this.tabPageTextExtractErrors.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageRawExtractedText;
    private System.Windows.Forms.TabPage tabPagePdfViewer;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button btnLoadFiles;
    private System.Windows.Forms.Label lblFolder;
    private System.Windows.Forms.ComboBox cboRootFolder;
    private System.Windows.Forms.Label lblFileNameFilters;
    private System.Windows.Forms.ComboBox cboFileNameFilters;
    private System.Windows.Forms.TabPage tabPageFormatRecognition;
    private System.Windows.Forms.TabPage tabPagePdfFiles;
    private System.Windows.Forms.Panel pnlFilesTop;
    private System.Windows.Forms.DataGridView gvFiles;
    private System.Windows.Forms.Button btnRecognizeFormats;
    private System.Windows.Forms.Button btnReloadFormats;
    private System.Windows.Forms.Button btnFilterList;
    private System.Windows.Forms.CheckedListBox ckListFormats;
    private System.Windows.Forms.SplitContainer splitterPdfFiles;
    private System.Windows.Forms.Label lblPdfFiles;
    private System.Windows.Forms.Panel pnlPdfFiltersTop;
    private System.Windows.Forms.CheckBox ckUseFormatFilters;
    private System.Windows.Forms.TabPage tabPageTextExtractDesigner;
    private System.Windows.Forms.Panel pnlTextExtractDesignerDockingTarget;
    private System.Windows.Forms.ToolStripMenuItem mnuToolWindows;
    private System.Windows.Forms.ToolStripMenuItem mnuToolWindowsToggleTreeView;
    private System.Windows.Forms.Panel pnlPdfViewerDockingTarget;
    private System.Windows.Forms.Panel pnlRawTextExtractDockingTarget;
    private System.Windows.Forms.CheckBox ckUndefinedOnly;
    private System.Windows.Forms.Button btnDefineTextStructure;
    private System.Windows.Forms.TabPage tabPageConfigEdit;
    private System.Windows.Forms.Panel pnlFormatRecognitionDockingTarget;
    private System.Windows.Forms.Panel pnlConfigEditorDockingTarget;
    private System.Windows.Forms.ContextMenuStrip ctxMnuFileGrid;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuFileGridProcessFiles;
    private System.Windows.Forms.TabPage tabPageTextExtractResults;
    private System.Windows.Forms.Panel pnlTextExtractResultsDockingTarget;
    private System.Windows.Forms.CheckBox ckKeepBreakpointEnabled;
    private System.Windows.Forms.CheckBox ckBreakpointEnabled;
    private System.Windows.Forms.Button btnLoadDxWorkbook;
    private System.Windows.Forms.CheckBox ckSuppressSections;
    private System.Windows.Forms.TabPage tabPageTextExtractErrors;
    private System.Windows.Forms.Panel pnlTextExtractErrorsDockingTarget;
    private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
    private System.Windows.Forms.SaveFileDialog dlgFileSave;
  }
}


namespace Org.SdxAdmin
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
      this.lblStatus = new System.Windows.Forms.Label();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.tvSolutions = new System.Windows.Forms.TreeView();
      this.ctxMenuSolution = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuSolutionRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuSolutionAddSolution = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuSolutionAddLogicalTable = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuSolutionUpdateSolution = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuSolutionDeleteSolution = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuSolutionAddColumn = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuSolutionUpdateLogicalTable = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuSolutionDeleteLogicalTable = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuSolutionUpdateColumn = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuSolutionDeleteColumn = new System.Windows.Forms.ToolStripMenuItem();
      this.imgListTreeView = new System.Windows.Forms.ImageList(this.components);
      this.pnlTreeViewBottom = new System.Windows.Forms.Panel();
      this.pnlTreeViewTop = new System.Windows.Forms.Panel();
      this.lblTreeViewTop = new System.Windows.Forms.Label();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageDesign = new System.Windows.Forms.TabPage();
      this.splitterDesign = new System.Windows.Forms.SplitContainer();
      this.pnlDesignColumnsTop = new System.Windows.Forms.Panel();
      this.lblDesignColumns = new System.Windows.Forms.Label();
      this.tabPageLoad = new System.Windows.Forms.TabPage();
      this.tabPageExtract = new System.Windows.Forms.TabPage();
      this.pnlToolBar = new System.Windows.Forms.Panel();
      this.lblMode = new System.Windows.Forms.Label();
      this.cboMode = new System.Windows.Forms.ComboBox();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.cboEnvironment = new System.Windows.Forms.ComboBox();
      this.gvColumns = new System.Windows.Forms.DataGridView();
      this.ctxMenuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuGridUpdateColumn = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuGridDeleteColumn = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuGridAddColumn = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuGridReorderColumns = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
      this.ctxMenuSolution.SuspendLayout();
      this.pnlTreeViewTop.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageDesign.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterDesign)).BeginInit();
      this.splitterDesign.Panel1.SuspendLayout();
      this.splitterDesign.SuspendLayout();
      this.pnlDesignColumnsTop.SuspendLayout();
      this.pnlToolBar.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvColumns)).BeginInit();
      this.ctxMenuGrid.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(4, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1393, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(4, 768);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1393, 28);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // splitterMain
      //
      this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.Location = new System.Drawing.Point(4, 84);
      this.splitterMain.Name = "splitterMain";
      //
      // splitterMain.Panel1
      //
      this.splitterMain.Panel1.Controls.Add(this.tvSolutions);
      this.splitterMain.Panel1.Controls.Add(this.pnlTreeViewBottom);
      this.splitterMain.Panel1.Controls.Add(this.pnlTreeViewTop);
      //
      // splitterMain.Panel2
      //
      this.splitterMain.Panel2.Controls.Add(this.tabMain);
      this.splitterMain.Size = new System.Drawing.Size(1393, 684);
      this.splitterMain.SplitterDistance = 337;
      this.splitterMain.TabIndex = 2;
      //
      // tvSolutions
      //
      this.tvSolutions.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvSolutions.ContextMenuStrip = this.ctxMenuSolution;
      this.tvSolutions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvSolutions.ImageIndex = 0;
      this.tvSolutions.ImageList = this.imgListTreeView;
      this.tvSolutions.Location = new System.Drawing.Point(0, 35);
      this.tvSolutions.Name = "tvSolutions";
      this.tvSolutions.SelectedImageIndex = 0;
      this.tvSolutions.Size = new System.Drawing.Size(335, 615);
      this.tvSolutions.TabIndex = 2;
      this.tvSolutions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSolutions_AfterSelect);
      this.tvSolutions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvSolutions_MouseDown);
      //
      // ctxMenuSolution
      //
      this.ctxMenuSolution.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMenuSolutionRefresh,
        this.ctxMenuSolutionAddSolution,
        this.ctxMenuSolutionAddLogicalTable,
        this.ctxMenuSolutionUpdateSolution,
        this.ctxMenuSolutionDeleteSolution,
        this.ctxMenuSolutionAddColumn,
        this.ctxMenuSolutionUpdateLogicalTable,
        this.ctxMenuSolutionDeleteLogicalTable,
        this.ctxMenuSolutionUpdateColumn,
        this.ctxMenuSolutionDeleteColumn
      });
      this.ctxMenuSolution.Name = "ctxMenuSolution";
      this.ctxMenuSolution.Size = new System.Drawing.Size(185, 224);
      this.ctxMenuSolution.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuSolution_Opening);
      //
      // ctxMenuSolutionRefresh
      //
      this.ctxMenuSolutionRefresh.Name = "ctxMenuSolutionRefresh";
      this.ctxMenuSolutionRefresh.Size = new System.Drawing.Size(184, 22);
      this.ctxMenuSolutionRefresh.Tag = "Refresh";
      this.ctxMenuSolutionRefresh.Text = "Refresh";
      this.ctxMenuSolutionRefresh.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuSolutionAddSolution
      //
      this.ctxMenuSolutionAddSolution.Name = "ctxMenuSolutionAddSolution";
      this.ctxMenuSolutionAddSolution.Size = new System.Drawing.Size(184, 22);
      this.ctxMenuSolutionAddSolution.Tag = "AddSolution";
      this.ctxMenuSolutionAddSolution.Text = "Add Solution";
      this.ctxMenuSolutionAddSolution.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuSolutionAddLogicalTable
      //
      this.ctxMenuSolutionAddLogicalTable.Name = "ctxMenuSolutionAddLogicalTable";
      this.ctxMenuSolutionAddLogicalTable.Size = new System.Drawing.Size(184, 22);
      this.ctxMenuSolutionAddLogicalTable.Tag = "AddLogicalTable";
      this.ctxMenuSolutionAddLogicalTable.Text = "Add Logical Table";
      this.ctxMenuSolutionAddLogicalTable.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuSolutionUpdateSolution
      //
      this.ctxMenuSolutionUpdateSolution.Name = "ctxMenuSolutionUpdateSolution";
      this.ctxMenuSolutionUpdateSolution.Size = new System.Drawing.Size(184, 22);
      this.ctxMenuSolutionUpdateSolution.Tag = "UpdateSolution";
      this.ctxMenuSolutionUpdateSolution.Text = "Update Solution";
      this.ctxMenuSolutionUpdateSolution.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuSolutionDeleteSolution
      //
      this.ctxMenuSolutionDeleteSolution.Name = "ctxMenuSolutionDeleteSolution";
      this.ctxMenuSolutionDeleteSolution.Size = new System.Drawing.Size(184, 22);
      this.ctxMenuSolutionDeleteSolution.Tag = "DeleteSolution";
      this.ctxMenuSolutionDeleteSolution.Text = "Delete Solution";
      this.ctxMenuSolutionDeleteSolution.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuSolutionAddColumn
      //
      this.ctxMenuSolutionAddColumn.Name = "ctxMenuSolutionAddColumn";
      this.ctxMenuSolutionAddColumn.Size = new System.Drawing.Size(184, 22);
      this.ctxMenuSolutionAddColumn.Tag = "AddColumn";
      this.ctxMenuSolutionAddColumn.Text = "Add Column";
      this.ctxMenuSolutionAddColumn.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuSolutionUpdateLogicalTable
      //
      this.ctxMenuSolutionUpdateLogicalTable.Name = "ctxMenuSolutionUpdateLogicalTable";
      this.ctxMenuSolutionUpdateLogicalTable.Size = new System.Drawing.Size(184, 22);
      this.ctxMenuSolutionUpdateLogicalTable.Tag = "UpdateLogicalTable";
      this.ctxMenuSolutionUpdateLogicalTable.Text = "Update Logical Table";
      this.ctxMenuSolutionUpdateLogicalTable.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuSolutionDeleteLogicalTable
      //
      this.ctxMenuSolutionDeleteLogicalTable.Name = "ctxMenuSolutionDeleteLogicalTable";
      this.ctxMenuSolutionDeleteLogicalTable.Size = new System.Drawing.Size(184, 22);
      this.ctxMenuSolutionDeleteLogicalTable.Tag = "DeleteLogicalTable";
      this.ctxMenuSolutionDeleteLogicalTable.Text = "Delete Logical Table";
      this.ctxMenuSolutionDeleteLogicalTable.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuSolutionUpdateColumn
      //
      this.ctxMenuSolutionUpdateColumn.Name = "ctxMenuSolutionUpdateColumn";
      this.ctxMenuSolutionUpdateColumn.Size = new System.Drawing.Size(184, 22);
      this.ctxMenuSolutionUpdateColumn.Tag = "UpdateColumn";
      this.ctxMenuSolutionUpdateColumn.Text = "Update Column";
      this.ctxMenuSolutionUpdateColumn.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuSolutionDeleteColumn
      //
      this.ctxMenuSolutionDeleteColumn.Name = "ctxMenuSolutionDeleteColumn";
      this.ctxMenuSolutionDeleteColumn.Size = new System.Drawing.Size(184, 22);
      this.ctxMenuSolutionDeleteColumn.Tag = "DeleteColumn";
      this.ctxMenuSolutionDeleteColumn.Text = "Delete Column";
      this.ctxMenuSolutionDeleteColumn.Click += new System.EventHandler(this.Action);
      //
      // imgListTreeView
      //
      this.imgListTreeView.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imgListTreeView.ImageSize = new System.Drawing.Size(16, 16);
      this.imgListTreeView.TransparentColor = System.Drawing.Color.Transparent;
      //
      // pnlTreeViewBottom
      //
      this.pnlTreeViewBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlTreeViewBottom.Location = new System.Drawing.Point(0, 650);
      this.pnlTreeViewBottom.Name = "pnlTreeViewBottom";
      this.pnlTreeViewBottom.Size = new System.Drawing.Size(335, 32);
      this.pnlTreeViewBottom.TabIndex = 1;
      //
      // pnlTreeViewTop
      //
      this.pnlTreeViewTop.Controls.Add(this.lblTreeViewTop);
      this.pnlTreeViewTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTreeViewTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTreeViewTop.Name = "pnlTreeViewTop";
      this.pnlTreeViewTop.Size = new System.Drawing.Size(335, 35);
      this.pnlTreeViewTop.TabIndex = 0;
      //
      // lblTreeViewTop
      //
      this.lblTreeViewTop.BackColor = System.Drawing.Color.SteelBlue;
      this.lblTreeViewTop.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblTreeViewTop.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTreeViewTop.ForeColor = System.Drawing.Color.White;
      this.lblTreeViewTop.Location = new System.Drawing.Point(0, 0);
      this.lblTreeViewTop.Name = "lblTreeViewTop";
      this.lblTreeViewTop.Size = new System.Drawing.Size(335, 35);
      this.lblTreeViewTop.TabIndex = 0;
      this.lblTreeViewTop.Text = "SDX Solutions";
      this.lblTreeViewTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      //
      // tabMain
      //
      this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                             | System.Windows.Forms.AnchorStyles.Left)
                             | System.Windows.Forms.AnchorStyles.Right)));
      this.tabMain.Controls.Add(this.tabPageDesign);
      this.tabMain.Controls.Add(this.tabPageLoad);
      this.tabMain.Controls.Add(this.tabPageExtract);
      this.tabMain.ItemSize = new System.Drawing.Size(1, 1);
      this.tabMain.Location = new System.Drawing.Point(-5, -5);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1067, 698);
      this.tabMain.TabIndex = 2;
      //
      // tabPageDesign
      //
      this.tabPageDesign.Controls.Add(this.splitterDesign);
      this.tabPageDesign.Location = new System.Drawing.Point(4, 5);
      this.tabPageDesign.Name = "tabPageDesign";
      this.tabPageDesign.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageDesign.Size = new System.Drawing.Size(1059, 689);
      this.tabPageDesign.TabIndex = 0;
      this.tabPageDesign.UseVisualStyleBackColor = true;
      //
      // splitterDesign
      //
      this.splitterDesign.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterDesign.Location = new System.Drawing.Point(3, 3);
      this.splitterDesign.Name = "splitterDesign";
      this.splitterDesign.Orientation = System.Windows.Forms.Orientation.Horizontal;
      //
      // splitterDesign.Panel1
      //
      this.splitterDesign.Panel1.Controls.Add(this.gvColumns);
      this.splitterDesign.Panel1.Controls.Add(this.pnlDesignColumnsTop);
      this.splitterDesign.Panel2Collapsed = true;
      this.splitterDesign.Size = new System.Drawing.Size(1053, 683);
      this.splitterDesign.SplitterDistance = 643;
      this.splitterDesign.TabIndex = 1;
      //
      // pnlDesignColumnsTop
      //
      this.pnlDesignColumnsTop.Controls.Add(this.lblDesignColumns);
      this.pnlDesignColumnsTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlDesignColumnsTop.Location = new System.Drawing.Point(0, 0);
      this.pnlDesignColumnsTop.Name = "pnlDesignColumnsTop";
      this.pnlDesignColumnsTop.Size = new System.Drawing.Size(1053, 32);
      this.pnlDesignColumnsTop.TabIndex = 1;
      //
      // lblDesignColumns
      //
      this.lblDesignColumns.BackColor = System.Drawing.Color.SteelBlue;
      this.lblDesignColumns.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblDesignColumns.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDesignColumns.ForeColor = System.Drawing.Color.White;
      this.lblDesignColumns.Location = new System.Drawing.Point(0, 0);
      this.lblDesignColumns.Name = "lblDesignColumns";
      this.lblDesignColumns.Size = new System.Drawing.Size(1053, 32);
      this.lblDesignColumns.TabIndex = 1;
      this.lblDesignColumns.Text = "Logical Table Design";
      this.lblDesignColumns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      //
      // tabPageLoad
      //
      this.tabPageLoad.Location = new System.Drawing.Point(4, 5);
      this.tabPageLoad.Name = "tabPageLoad";
      this.tabPageLoad.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageLoad.Size = new System.Drawing.Size(1059, 689);
      this.tabPageLoad.TabIndex = 1;
      this.tabPageLoad.UseVisualStyleBackColor = true;
      //
      // tabPageExtract
      //
      this.tabPageExtract.Location = new System.Drawing.Point(4, 5);
      this.tabPageExtract.Name = "tabPageExtract";
      this.tabPageExtract.Size = new System.Drawing.Size(1059, 689);
      this.tabPageExtract.TabIndex = 2;
      this.tabPageExtract.UseVisualStyleBackColor = true;
      //
      // pnlToolBar
      //
      this.pnlToolBar.Controls.Add(this.lblMode);
      this.pnlToolBar.Controls.Add(this.cboMode);
      this.pnlToolBar.Controls.Add(this.lblEnvironment);
      this.pnlToolBar.Controls.Add(this.cboEnvironment);
      this.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlToolBar.Location = new System.Drawing.Point(4, 24);
      this.pnlToolBar.Name = "pnlToolBar";
      this.pnlToolBar.Size = new System.Drawing.Size(1393, 60);
      this.pnlToolBar.TabIndex = 3;
      //
      // lblMode
      //
      this.lblMode.AutoSize = true;
      this.lblMode.Location = new System.Drawing.Point(160, 11);
      this.lblMode.Name = "lblMode";
      this.lblMode.Size = new System.Drawing.Size(34, 13);
      this.lblMode.TabIndex = 1;
      this.lblMode.Text = "Mode";
      //
      // cboMode
      //
      this.cboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboMode.FormattingEnabled = true;
      this.cboMode.Location = new System.Drawing.Point(160, 27);
      this.cboMode.Name = "cboMode";
      this.cboMode.Size = new System.Drawing.Size(128, 21);
      this.cboMode.TabIndex = 0;
      this.cboMode.SelectedIndexChanged += new System.EventHandler(this.cboMode_SelectedIndexChanged);
      //
      // lblEnvironment
      //
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(12, 11);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(66, 13);
      this.lblEnvironment.TabIndex = 1;
      this.lblEnvironment.Text = "Environment";
      //
      // cboEnvironment
      //
      this.cboEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboEnvironment.FormattingEnabled = true;
      this.cboEnvironment.Location = new System.Drawing.Point(12, 27);
      this.cboEnvironment.Name = "cboEnvironment";
      this.cboEnvironment.Size = new System.Drawing.Size(128, 21);
      this.cboEnvironment.TabIndex = 0;
      this.cboEnvironment.SelectedIndexChanged += new System.EventHandler(this.cboEnvironment_SelectedIndexChanged);
      //
      // gvColumns
      //
      this.gvColumns.AllowUserToAddRows = false;
      this.gvColumns.AllowUserToDeleteRows = false;
      this.gvColumns.AllowUserToResizeRows = false;
      this.gvColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.gvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvColumns.ContextMenuStrip = this.ctxMenuGrid;
      this.gvColumns.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvColumns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvColumns.Location = new System.Drawing.Point(0, 32);
      this.gvColumns.MultiSelect = false;
      this.gvColumns.Name = "gvColumns";
      this.gvColumns.RowHeadersVisible = false;
      this.gvColumns.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvColumns.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvColumns.RowTemplate.Height = 19;
      this.gvColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvColumns.Size = new System.Drawing.Size(1053, 651);
      this.gvColumns.TabIndex = 9;
      this.gvColumns.Tag = "EditScheduledTask";
      this.gvColumns.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gvColumns_CellBeginEdit);
      this.gvColumns.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvColumns_CellClick);
      this.gvColumns.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvColumns_CellEndEdit);
      this.gvColumns.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvColumns_CellMouseDown);
      this.gvColumns.SelectionChanged += new System.EventHandler(this.gvColumns_SelectionChanged);
      this.gvColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvColumns_MouseDown);
      //
      // ctxMenuGrid
      //
      this.ctxMenuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMenuGridAddColumn,
        this.ctxMenuGridUpdateColumn,
        this.ctxMenuGridDeleteColumn,
        this.ctxMenuGridReorderColumns
      });
      this.ctxMenuGrid.Name = "ctxMenuGrid";
      this.ctxMenuGrid.Size = new System.Drawing.Size(167, 92);
      this.ctxMenuGrid.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuGrid_Opening);
      //
      // ctxMenuGridUpdateColumn
      //
      this.ctxMenuGridUpdateColumn.Name = "ctxMenuGridUpdateColumn";
      this.ctxMenuGridUpdateColumn.Size = new System.Drawing.Size(166, 22);
      this.ctxMenuGridUpdateColumn.Tag = "UpdateColumn";
      this.ctxMenuGridUpdateColumn.Text = "Update Column";
      this.ctxMenuGridUpdateColumn.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuGridDeleteColumn
      //
      this.ctxMenuGridDeleteColumn.Name = "ctxMenuGridDeleteColumn";
      this.ctxMenuGridDeleteColumn.Size = new System.Drawing.Size(166, 22);
      this.ctxMenuGridDeleteColumn.Tag = "DeleteColumn";
      this.ctxMenuGridDeleteColumn.Text = "Delete Column";
      this.ctxMenuGridDeleteColumn.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuGridAddColumn
      //
      this.ctxMenuGridAddColumn.Name = "ctxMenuGridAddColumn";
      this.ctxMenuGridAddColumn.Size = new System.Drawing.Size(166, 22);
      this.ctxMenuGridAddColumn.Tag = "AddColumn";
      this.ctxMenuGridAddColumn.Text = "Add Column";
      this.ctxMenuGridAddColumn.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuGridReorderColumns
      //
      this.ctxMenuGridReorderColumns.Name = "ctxMenuGridReorderColumns";
      this.ctxMenuGridReorderColumns.Size = new System.Drawing.Size(166, 22);
      this.ctxMenuGridReorderColumns.Tag = "ReorderColumns";
      this.ctxMenuGridReorderColumns.Text = "Reorder Columns";
      this.ctxMenuGridReorderColumns.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1401, 796);
      this.Controls.Add(this.splitterMain);
      this.Controls.Add(this.pnlToolBar);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SDX Admin";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      this.ctxMenuSolution.ResumeLayout(false);
      this.pnlTreeViewTop.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageDesign.ResumeLayout(false);
      this.splitterDesign.Panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterDesign)).EndInit();
      this.splitterDesign.ResumeLayout(false);
      this.pnlDesignColumnsTop.ResumeLayout(false);
      this.pnlToolBar.ResumeLayout(false);
      this.pnlToolBar.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvColumns)).EndInit();
      this.ctxMenuGrid.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.TreeView tvSolutions;
    private System.Windows.Forms.Panel pnlTreeViewBottom;
    private System.Windows.Forms.Panel pnlTreeViewTop;
    private System.Windows.Forms.Label lblTreeViewTop;
    private System.Windows.Forms.Panel pnlToolBar;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.ComboBox cboEnvironment;
    private System.Windows.Forms.SplitContainer splitterDesign;
    private System.Windows.Forms.Panel pnlDesignColumnsTop;
    private System.Windows.Forms.Label lblDesignColumns;
    private System.Windows.Forms.ImageList imgListTreeView;
    private System.Windows.Forms.ContextMenuStrip ctxMenuSolution;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuSolutionRefresh;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuSolutionAddSolution;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuSolutionUpdateSolution;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuSolutionDeleteSolution;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuSolutionAddLogicalTable;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuSolutionUpdateLogicalTable;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuSolutionDeleteLogicalTable;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuSolutionAddColumn;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuSolutionUpdateColumn;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuSolutionDeleteColumn;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageDesign;
    private System.Windows.Forms.TabPage tabPageLoad;
    private System.Windows.Forms.TabPage tabPageExtract;
    private System.Windows.Forms.Label lblMode;
    private System.Windows.Forms.ComboBox cboMode;
    private System.Windows.Forms.DataGridView gvColumns;
    private System.Windows.Forms.ContextMenuStrip ctxMenuGrid;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuGridAddColumn;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuGridUpdateColumn;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuGridDeleteColumn;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuGridReorderColumns;
  }
}
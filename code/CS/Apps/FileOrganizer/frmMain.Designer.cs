namespace Org.FileOrganizer
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
      this.mnuFileNewProject = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileDeleteProject = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblProject = new System.Windows.Forms.Label();
      this.cboProject = new System.Windows.Forms.ComboBox();
      this.lblStatus = new System.Windows.Forms.Label();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageMain = new System.Windows.Forms.TabPage();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.pnlFileProcessing = new System.Windows.Forms.Panel();
      this.btnDeleteRootFolder = new System.Windows.Forms.Button();
      this.txtFileCountLimit = new System.Windows.Forms.TextBox();
      this.lblFileCountLimit = new System.Windows.Forms.Label();
      this.ckIncludeChildFolders = new System.Windows.Forms.CheckBox();
      this.btnProcessRootFolder = new System.Windows.Forms.Button();
      this.btnAddRootFolder = new System.Windows.Forms.Button();
      this.lblProjectRoot = new System.Windows.Forms.Label();
      this.cboRootFolderDropDown = new System.Windows.Forms.ComboBox();
      this.tabPageDocs = new System.Windows.Forms.TabPage();
      this.gvDocTypes = new System.Windows.Forms.DataGridView();
      this.pnlDocTypes = new System.Windows.Forms.Panel();
      this.btnPopulateDocTypeGrid = new System.Windows.Forms.Button();
      this.btnAddDocType = new System.Windows.Forms.Button();
      this.tabPageTags = new System.Windows.Forms.TabPage();
      this.gvTagTypes = new System.Windows.Forms.DataGridView();
      this.pnlTagType = new System.Windows.Forms.Panel();
      this.btnPopulateTagTypeGrid = new System.Windows.Forms.Button();
      this.btnAddTagType = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageMain.SuspendLayout();
      this.pnlFileProcessing.SuspendLayout();
      this.tabPageDocs.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvDocTypes)).BeginInit();
      this.pnlDocTypes.SuspendLayout();
      this.tabPageTags.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvTagTypes)).BeginInit();
      this.pnlTagType.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.ImageScalingSize = new System.Drawing.Size(24, 24);
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
      this.mnuMain.Size = new System.Drawing.Size(1479, 25);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFileNewProject,
        this.mnuFileDeleteProject,
        this.mnuFileExit
      });
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 19);
      this.mnuFile.Text = "&File";
      //
      // mnuFileNewProject
      //
      this.mnuFileNewProject.Name = "mnuFileNewProject";
      this.mnuFileNewProject.Size = new System.Drawing.Size(147, 22);
      this.mnuFileNewProject.Tag = "NewProject";
      this.mnuFileNewProject.Text = "New Project";
      this.mnuFileNewProject.Click += new System.EventHandler(this.Action);
      //
      // mnuFileDeleteProject
      //
      this.mnuFileDeleteProject.Name = "mnuFileDeleteProject";
      this.mnuFileDeleteProject.Size = new System.Drawing.Size(147, 22);
      this.mnuFileDeleteProject.Tag = "DeleteProject";
      this.mnuFileDeleteProject.Text = "&Delete Project";
      this.mnuFileDeleteProject.Click += new System.EventHandler(this.Action);
      //
      // mnuFileExit
      //
      this.mnuFileExit.Name = "mnuFileExit";
      this.mnuFileExit.Size = new System.Drawing.Size(147, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.lblProject);
      this.pnlTop.Controls.Add(this.cboProject);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 25);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1479, 154);
      this.pnlTop.TabIndex = 1;
      //
      // lblProject
      //
      this.lblProject.AutoSize = true;
      this.lblProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblProject.Location = new System.Drawing.Point(27, 35);
      this.lblProject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblProject.Name = "lblProject";
      this.lblProject.Size = new System.Drawing.Size(40, 13);
      this.lblProject.TabIndex = 1;
      this.lblProject.Text = "Project";
      //
      // cboProject
      //
      this.cboProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboProject.FormattingEnabled = true;
      this.cboProject.Location = new System.Drawing.Point(33, 71);
      this.cboProject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cboProject.Name = "cboProject";
      this.cboProject.Size = new System.Drawing.Size(490, 21);
      this.cboProject.TabIndex = 0;
      this.cboProject.Tag = "SelectedProjectChange";
      this.cboProject.SelectedIndexChanged += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblStatus.Location = new System.Drawing.Point(0, 726);
      this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1479, 42);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageMain);
      this.tabMain.Controls.Add(this.tabPageDocs);
      this.tabMain.Controls.Add(this.tabPageTags);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tabMain.ItemSize = new System.Drawing.Size(160, 25);
      this.tabMain.Location = new System.Drawing.Point(0, 179);
      this.tabMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1479, 547);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 3;
      //
      // tabPageMain
      //
      this.tabPageMain.Controls.Add(this.txtOut);
      this.tabPageMain.Controls.Add(this.pnlFileProcessing);
      this.tabPageMain.Location = new System.Drawing.Point(4, 29);
      this.tabPageMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageMain.Name = "tabPageMain";
      this.tabPageMain.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageMain.Size = new System.Drawing.Size(1471, 514);
      this.tabPageMain.TabIndex = 0;
      this.tabPageMain.Text = "File Processing";
      this.tabPageMain.UseVisualStyleBackColor = true;
      //
      // txtOut
      //
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(4, 148);
      this.txtOut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1463, 361);
      this.txtOut.TabIndex = 1;
      this.txtOut.Tag = "txtOut";
      this.txtOut.WordWrap = false;
      //
      // pnlFileProcessing
      //
      this.pnlFileProcessing.Controls.Add(this.btnDeleteRootFolder);
      this.pnlFileProcessing.Controls.Add(this.txtFileCountLimit);
      this.pnlFileProcessing.Controls.Add(this.lblFileCountLimit);
      this.pnlFileProcessing.Controls.Add(this.ckIncludeChildFolders);
      this.pnlFileProcessing.Controls.Add(this.btnProcessRootFolder);
      this.pnlFileProcessing.Controls.Add(this.btnAddRootFolder);
      this.pnlFileProcessing.Controls.Add(this.lblProjectRoot);
      this.pnlFileProcessing.Controls.Add(this.cboRootFolderDropDown);
      this.pnlFileProcessing.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlFileProcessing.Location = new System.Drawing.Point(4, 5);
      this.pnlFileProcessing.Name = "pnlFileProcessing";
      this.pnlFileProcessing.Size = new System.Drawing.Size(1463, 143);
      this.pnlFileProcessing.TabIndex = 0;
      //
      // btnDeleteRootFolder
      //
      this.btnDeleteRootFolder.Location = new System.Drawing.Point(458, 91);
      this.btnDeleteRootFolder.Name = "btnDeleteRootFolder";
      this.btnDeleteRootFolder.Size = new System.Drawing.Size(225, 35);
      this.btnDeleteRootFolder.TabIndex = 10;
      this.btnDeleteRootFolder.Tag = "DeleteRootFolder";
      this.btnDeleteRootFolder.Text = "Delete Root Folder";
      this.btnDeleteRootFolder.UseVisualStyleBackColor = true;
      this.btnDeleteRootFolder.Click += new System.EventHandler(this.Action);
      //
      // txtFileCountLimit
      //
      this.txtFileCountLimit.Location = new System.Drawing.Point(858, 94);
      this.txtFileCountLimit.Name = "txtFileCountLimit";
      this.txtFileCountLimit.Size = new System.Drawing.Size(142, 20);
      this.txtFileCountLimit.TabIndex = 9;
      this.txtFileCountLimit.Tag = "txtFileCountLimit";
      //
      // lblFileCountLimit
      //
      this.lblFileCountLimit.BackColor = System.Drawing.SystemColors.Control;
      this.lblFileCountLimit.Location = new System.Drawing.Point(717, 91);
      this.lblFileCountLimit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblFileCountLimit.Name = "lblFileCountLimit";
      this.lblFileCountLimit.Size = new System.Drawing.Size(136, 32);
      this.lblFileCountLimit.TabIndex = 8;
      this.lblFileCountLimit.Text = "File Count Limit:";
      this.lblFileCountLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      //
      // ckIncludeChildFolders
      //
      this.ckIncludeChildFolders.AutoSize = true;
      this.ckIncludeChildFolders.Location = new System.Drawing.Point(722, 48);
      this.ckIncludeChildFolders.Name = "ckIncludeChildFolders";
      this.ckIncludeChildFolders.Size = new System.Drawing.Size(124, 17);
      this.ckIncludeChildFolders.TabIndex = 7;
      this.ckIncludeChildFolders.Text = "Include Child Folders";
      this.ckIncludeChildFolders.UseVisualStyleBackColor = true;
      //
      // btnProcessRootFolder
      //
      this.btnProcessRootFolder.Location = new System.Drawing.Point(212, 89);
      this.btnProcessRootFolder.Name = "btnProcessRootFolder";
      this.btnProcessRootFolder.Size = new System.Drawing.Size(240, 35);
      this.btnProcessRootFolder.TabIndex = 3;
      this.btnProcessRootFolder.Tag = "ProcessRootFolder";
      this.btnProcessRootFolder.Text = "Process Root Folder";
      this.btnProcessRootFolder.UseVisualStyleBackColor = true;
      this.btnProcessRootFolder.Click += new System.EventHandler(this.Action);
      //
      // btnAddRootFolder
      //
      this.btnAddRootFolder.Location = new System.Drawing.Point(26, 89);
      this.btnAddRootFolder.Name = "btnAddRootFolder";
      this.btnAddRootFolder.Size = new System.Drawing.Size(180, 35);
      this.btnAddRootFolder.TabIndex = 2;
      this.btnAddRootFolder.Tag = "AddRootFolder";
      this.btnAddRootFolder.Text = "Add A Root Folder";
      this.btnAddRootFolder.UseVisualStyleBackColor = true;
      this.btnAddRootFolder.Click += new System.EventHandler(this.Action);
      //
      // lblProjectRoot
      //
      this.lblProjectRoot.AutoSize = true;
      this.lblProjectRoot.Location = new System.Drawing.Point(24, 15);
      this.lblProjectRoot.Name = "lblProjectRoot";
      this.lblProjectRoot.Size = new System.Drawing.Size(62, 13);
      this.lblProjectRoot.TabIndex = 1;
      this.lblProjectRoot.Text = "Root Folder";
      //
      // cboRootFolderDropDown
      //
      this.cboRootFolderDropDown.FormattingEnabled = true;
      this.cboRootFolderDropDown.Location = new System.Drawing.Point(26, 45);
      this.cboRootFolderDropDown.Name = "cboRootFolderDropDown";
      this.cboRootFolderDropDown.Size = new System.Drawing.Size(655, 21);
      this.cboRootFolderDropDown.TabIndex = 0;
      this.cboRootFolderDropDown.SelectedIndexChanged += new System.EventHandler(this.cboRootFolderDropDown_SelectedIndexChanged);
      //
      // tabPageDocs
      //
      this.tabPageDocs.Controls.Add(this.gvDocTypes);
      this.tabPageDocs.Controls.Add(this.pnlDocTypes);
      this.tabPageDocs.Location = new System.Drawing.Point(4, 29);
      this.tabPageDocs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageDocs.Name = "tabPageDocs";
      this.tabPageDocs.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageDocs.Size = new System.Drawing.Size(1471, 502);
      this.tabPageDocs.TabIndex = 1;
      this.tabPageDocs.Text = "Doc Types";
      this.tabPageDocs.UseVisualStyleBackColor = true;
      //
      // gvDocTypes
      //
      this.gvDocTypes.AllowUserToAddRows = false;
      this.gvDocTypes.AllowUserToDeleteRows = false;
      this.gvDocTypes.AllowUserToResizeRows = false;
      this.gvDocTypes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
      this.gvDocTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvDocTypes.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvDocTypes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvDocTypes.Location = new System.Drawing.Point(4, 131);
      this.gvDocTypes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.gvDocTypes.MultiSelect = false;
      this.gvDocTypes.Name = "gvDocTypes";
      this.gvDocTypes.RowHeadersVisible = false;
      this.gvDocTypes.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvDocTypes.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvDocTypes.RowTemplate.Height = 19;
      this.gvDocTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvDocTypes.Size = new System.Drawing.Size(1463, 366);
      this.gvDocTypes.TabIndex = 6;
      this.gvDocTypes.Tag = "DocTypes";
      //
      // pnlDocTypes
      //
      this.pnlDocTypes.Controls.Add(this.btnPopulateDocTypeGrid);
      this.pnlDocTypes.Controls.Add(this.btnAddDocType);
      this.pnlDocTypes.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlDocTypes.Location = new System.Drawing.Point(4, 5);
      this.pnlDocTypes.Name = "pnlDocTypes";
      this.pnlDocTypes.Size = new System.Drawing.Size(1463, 126);
      this.pnlDocTypes.TabIndex = 0;
      //
      // btnPopulateDocTypeGrid
      //
      this.btnPopulateDocTypeGrid.Location = new System.Drawing.Point(202, 22);
      this.btnPopulateDocTypeGrid.Name = "btnPopulateDocTypeGrid";
      this.btnPopulateDocTypeGrid.Size = new System.Drawing.Size(234, 35);
      this.btnPopulateDocTypeGrid.TabIndex = 1;
      this.btnPopulateDocTypeGrid.Tag = "PopulateDocTypeGrid";
      this.btnPopulateDocTypeGrid.Text = "Populate Doc Type Grid";
      this.btnPopulateDocTypeGrid.UseVisualStyleBackColor = true;
      this.btnPopulateDocTypeGrid.Click += new System.EventHandler(this.Action);
      //
      // btnAddDocType
      //
      this.btnAddDocType.Location = new System.Drawing.Point(22, 22);
      this.btnAddDocType.Name = "btnAddDocType";
      this.btnAddDocType.Size = new System.Drawing.Size(154, 35);
      this.btnAddDocType.TabIndex = 0;
      this.btnAddDocType.Tag = "AddDocumentType";
      this.btnAddDocType.Text = "Add Doc Type";
      this.btnAddDocType.UseVisualStyleBackColor = true;
      this.btnAddDocType.Click += new System.EventHandler(this.Action);
      //
      // tabPageTags
      //
      this.tabPageTags.Controls.Add(this.gvTagTypes);
      this.tabPageTags.Controls.Add(this.pnlTagType);
      this.tabPageTags.Location = new System.Drawing.Point(4, 29);
      this.tabPageTags.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.tabPageTags.Name = "tabPageTags";
      this.tabPageTags.Size = new System.Drawing.Size(1471, 502);
      this.tabPageTags.TabIndex = 2;
      this.tabPageTags.Text = "Tags";
      this.tabPageTags.UseVisualStyleBackColor = true;
      //
      // gvTagTypes
      //
      this.gvTagTypes.AllowUserToAddRows = false;
      this.gvTagTypes.AllowUserToDeleteRows = false;
      this.gvTagTypes.AllowUserToResizeRows = false;
      this.gvTagTypes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
      this.gvTagTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvTagTypes.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvTagTypes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvTagTypes.Location = new System.Drawing.Point(0, 126);
      this.gvTagTypes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.gvTagTypes.MultiSelect = false;
      this.gvTagTypes.Name = "gvTagTypes";
      this.gvTagTypes.RowHeadersVisible = false;
      this.gvTagTypes.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvTagTypes.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvTagTypes.RowTemplate.Height = 19;
      this.gvTagTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvTagTypes.Size = new System.Drawing.Size(1471, 376);
      this.gvTagTypes.TabIndex = 7;
      this.gvTagTypes.Tag = "TagTypes";
      //
      // pnlTagType
      //
      this.pnlTagType.Controls.Add(this.btnPopulateTagTypeGrid);
      this.pnlTagType.Controls.Add(this.btnAddTagType);
      this.pnlTagType.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTagType.Location = new System.Drawing.Point(0, 0);
      this.pnlTagType.Name = "pnlTagType";
      this.pnlTagType.Size = new System.Drawing.Size(1471, 126);
      this.pnlTagType.TabIndex = 1;
      //
      // btnPopulateTagTypeGrid
      //
      this.btnPopulateTagTypeGrid.Location = new System.Drawing.Point(202, 22);
      this.btnPopulateTagTypeGrid.Name = "btnPopulateTagTypeGrid";
      this.btnPopulateTagTypeGrid.Size = new System.Drawing.Size(234, 35);
      this.btnPopulateTagTypeGrid.TabIndex = 1;
      this.btnPopulateTagTypeGrid.Tag = "PopulateTagTypeGrid";
      this.btnPopulateTagTypeGrid.Text = "Populate Tag Type Grid";
      this.btnPopulateTagTypeGrid.UseVisualStyleBackColor = true;
      this.btnPopulateTagTypeGrid.Click += new System.EventHandler(this.Action);
      //
      // btnAddTagType
      //
      this.btnAddTagType.Location = new System.Drawing.Point(22, 22);
      this.btnAddTagType.Name = "btnAddTagType";
      this.btnAddTagType.Size = new System.Drawing.Size(154, 35);
      this.btnAddTagType.TabIndex = 0;
      this.btnAddTagType.Tag = "AddTagType";
      this.btnAddTagType.Text = "Add Tag Type";
      this.btnAddTagType.UseVisualStyleBackColor = true;
      this.btnAddTagType.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1479, 768);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "File Organizer";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabPageMain.ResumeLayout(false);
      this.tabPageMain.PerformLayout();
      this.pnlFileProcessing.ResumeLayout(false);
      this.pnlFileProcessing.PerformLayout();
      this.tabPageDocs.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvDocTypes)).EndInit();
      this.pnlDocTypes.ResumeLayout(false);
      this.tabPageTags.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvTagTypes)).EndInit();
      this.pnlTagType.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.ToolStripMenuItem mnuFileNewProject;
    private System.Windows.Forms.Label lblProject;
    private System.Windows.Forms.ComboBox cboProject;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageMain;
    private System.Windows.Forms.TabPage tabPageDocs;
    private System.Windows.Forms.TabPage tabPageTags;
    private System.Windows.Forms.ToolStripMenuItem mnuFileDeleteProject;
    private System.Windows.Forms.Panel pnlDocTypes;
    private System.Windows.Forms.DataGridView gvDocTypes;
    private System.Windows.Forms.Button btnAddDocType;
    private System.Windows.Forms.Button btnPopulateDocTypeGrid;
    private System.Windows.Forms.Panel pnlTagType;
    private System.Windows.Forms.Button btnPopulateTagTypeGrid;
    private System.Windows.Forms.Button btnAddTagType;
    private System.Windows.Forms.DataGridView gvTagTypes;
    private System.Windows.Forms.Panel pnlFileProcessing;
    private System.Windows.Forms.Button btnAddRootFolder;
    private System.Windows.Forms.Label lblProjectRoot;
    private System.Windows.Forms.ComboBox cboRootFolderDropDown;
    private System.Windows.Forms.Button btnProcessRootFolder;
    private System.Windows.Forms.CheckBox ckIncludeChildFolders;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.TextBox txtFileCountLimit;
    private System.Windows.Forms.Label lblFileCountLimit;
    private System.Windows.Forms.Button btnDeleteRootFolder;
  }
}


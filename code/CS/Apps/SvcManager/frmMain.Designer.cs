namespace Org.SvcManager
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
      this.mnuMaintenance = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMaintenanceManageTaskServices = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMaintenanceSetTestServiceTypes = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMaintenanceSetProdServiceTypes = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMaintenanceRefreshTreeView = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMaintenanceRunMigr = new System.Windows.Forms.ToolStripMenuItem();
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.splitMain = new System.Windows.Forms.SplitContainer();
      this.tvMain = new System.Windows.Forms.TreeView();
      this.ctxMenuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMnuTreeViewManageTaskAssignments = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuTreeViewManageHosts = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuTreeViewAddWindowsService = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuTreeViewAddWCFWebService = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuTreeViewDeleteWCFWebService = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuTreeViewAddWebSite = new System.Windows.Forms.ToolStripMenuItem();
      this.imgListTreeView = new System.Windows.Forms.ImageList(this.components);
      this.pnlTvBottom = new System.Windows.Forms.Panel();
      this.pnlTvTop = new System.Windows.Forms.Panel();
      this.pnlWinSvc = new System.Windows.Forms.Panel();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.btnXml = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.pnlMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
      this.splitMain.Panel1.SuspendLayout();
      this.splitMain.Panel2.SuspendLayout();
      this.splitMain.SuspendLayout();
      this.ctxMenuTreeView.SuspendLayout();
      this.pnlWinSvc.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile,
        this.mnuMaintenance
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1065, 24);
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
      // mnuMaintenance
      //
      this.mnuMaintenance.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuMaintenanceManageTaskServices,
        this.mnuMaintenanceSetTestServiceTypes,
        this.mnuMaintenanceSetProdServiceTypes,
        this.mnuMaintenanceRefreshTreeView,
        this.mnuMaintenanceRunMigr
      });
      this.mnuMaintenance.Name = "mnuMaintenance";
      this.mnuMaintenance.Size = new System.Drawing.Size(88, 20);
      this.mnuMaintenance.Text = "&Maintenance";
      //
      // mnuMaintenanceManageTaskServices
      //
      this.mnuMaintenanceManageTaskServices.Name = "mnuMaintenanceManageTaskServices";
      this.mnuMaintenanceManageTaskServices.Size = new System.Drawing.Size(191, 22);
      this.mnuMaintenanceManageTaskServices.Tag = "ManageTaskServices";
      this.mnuMaintenanceManageTaskServices.Text = "Manage Task &Services";
      this.mnuMaintenanceManageTaskServices.Click += new System.EventHandler(this.Action);
      //
      // mnuMaintenanceSetTestServiceTypes
      //
      this.mnuMaintenanceSetTestServiceTypes.Name = "mnuMaintenanceSetTestServiceTypes";
      this.mnuMaintenanceSetTestServiceTypes.Size = new System.Drawing.Size(191, 22);
      this.mnuMaintenanceSetTestServiceTypes.Tag = "SetTestServiceTypes";
      this.mnuMaintenanceSetTestServiceTypes.Text = "Set Test Service Types";
      this.mnuMaintenanceSetTestServiceTypes.Click += new System.EventHandler(this.Action);
      //
      // mnuMaintenanceSetProdServiceTypes
      //
      this.mnuMaintenanceSetProdServiceTypes.Name = "mnuMaintenanceSetProdServiceTypes";
      this.mnuMaintenanceSetProdServiceTypes.Size = new System.Drawing.Size(191, 22);
      this.mnuMaintenanceSetProdServiceTypes.Tag = "SetProdServiceTypes";
      this.mnuMaintenanceSetProdServiceTypes.Text = "Set Prod Service Types";
      this.mnuMaintenanceSetProdServiceTypes.Click += new System.EventHandler(this.Action);
      //
      // mnuMaintenanceRefreshTreeView
      //
      this.mnuMaintenanceRefreshTreeView.Name = "mnuMaintenanceRefreshTreeView";
      this.mnuMaintenanceRefreshTreeView.Size = new System.Drawing.Size(191, 22);
      this.mnuMaintenanceRefreshTreeView.Tag = "RefreshTreeView";
      this.mnuMaintenanceRefreshTreeView.Text = "Refresh TreeView";
      this.mnuMaintenanceRefreshTreeView.Click += new System.EventHandler(this.Action);
      //
      // mnuMaintenanceRunMigr
      //
      this.mnuMaintenanceRunMigr.Name = "mnuMaintenanceRunMigr";
      this.mnuMaintenanceRunMigr.Size = new System.Drawing.Size(191, 22);
      this.mnuMaintenanceRunMigr.Tag = "RunMigr";
      this.mnuMaintenanceRunMigr.Text = "Run migr";
      this.mnuMaintenanceRunMigr.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 656);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1065, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.btnXml);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1065, 42);
      this.pnlTop.TabIndex = 2;
      //
      // pnlMain
      //
      this.pnlMain.Controls.Add(this.splitMain);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 66);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.pnlMain.Size = new System.Drawing.Size(1065, 590);
      this.pnlMain.TabIndex = 3;
      //
      // splitMain
      //
      this.splitMain.BackColor = System.Drawing.Color.White;
      this.splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitMain.Location = new System.Drawing.Point(4, 0);
      this.splitMain.Name = "splitMain";
      //
      // splitMain.Panel1
      //
      this.splitMain.Panel1.Controls.Add(this.tvMain);
      this.splitMain.Panel1.Controls.Add(this.pnlTvBottom);
      this.splitMain.Panel1.Controls.Add(this.pnlTvTop);
      //
      // splitMain.Panel2
      //
      this.splitMain.Panel2.Controls.Add(this.pnlWinSvc);
      this.splitMain.Panel2.Padding = new System.Windows.Forms.Padding(4);
      this.splitMain.Size = new System.Drawing.Size(1057, 590);
      this.splitMain.SplitterDistance = 257;
      this.splitMain.SplitterWidth = 3;
      this.splitMain.TabIndex = 0;
      //
      // tvMain
      //
      this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvMain.ContextMenuStrip = this.ctxMenuTreeView;
      this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMain.ImageIndex = 0;
      this.tvMain.ImageList = this.imgListTreeView;
      this.tvMain.Location = new System.Drawing.Point(0, 38);
      this.tvMain.Name = "tvMain";
      this.tvMain.SelectedImageIndex = 0;
      this.tvMain.Size = new System.Drawing.Size(255, 512);
      this.tvMain.TabIndex = 3;
      this.tvMain.Click += new System.EventHandler(this.tvMain_Click);
      //
      // ctxMenuTreeView
      //
      this.ctxMenuTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMnuTreeViewManageTaskAssignments,
        this.ctxMnuTreeViewManageHosts,
        this.ctxMnuTreeViewAddWindowsService,
        this.ctxMnuTreeViewAddWCFWebService,
        this.ctxMnuTreeViewDeleteWCFWebService,
        this.ctxMnuTreeViewAddWebSite
      });
      this.ctxMenuTreeView.Name = "ctxMenuTreeView";
      this.ctxMenuTreeView.Size = new System.Drawing.Size(215, 136);
      this.ctxMenuTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuTreeView_Opening);
      //
      // ctxMnuTreeViewManageTaskAssignments
      //
      this.ctxMnuTreeViewManageTaskAssignments.Name = "ctxMnuTreeViewManageTaskAssignments";
      this.ctxMnuTreeViewManageTaskAssignments.Size = new System.Drawing.Size(214, 22);
      this.ctxMnuTreeViewManageTaskAssignments.Tag = "ManageTaskAssignments";
      this.ctxMnuTreeViewManageTaskAssignments.Text = "Manage Task &Assignments";
      this.ctxMnuTreeViewManageTaskAssignments.Click += new System.EventHandler(this.Action);
      //
      // ctxMnuTreeViewManageHosts
      //
      this.ctxMnuTreeViewManageHosts.Name = "ctxMnuTreeViewManageHosts";
      this.ctxMnuTreeViewManageHosts.Size = new System.Drawing.Size(214, 22);
      this.ctxMnuTreeViewManageHosts.Tag = "ManageHosts";
      this.ctxMnuTreeViewManageHosts.Text = "Manage &Hosts";
      this.ctxMnuTreeViewManageHosts.Click += new System.EventHandler(this.Action);
      //
      // ctxMnuTreeViewAddWindowsService
      //
      this.ctxMnuTreeViewAddWindowsService.Name = "ctxMnuTreeViewAddWindowsService";
      this.ctxMnuTreeViewAddWindowsService.Size = new System.Drawing.Size(214, 22);
      this.ctxMnuTreeViewAddWindowsService.Tag = "AddWindowsService";
      this.ctxMnuTreeViewAddWindowsService.Text = "Add Windows Service";
      this.ctxMnuTreeViewAddWindowsService.Click += new System.EventHandler(this.Action);
      //
      // ctxMnuTreeViewAddWCFWebService
      //
      this.ctxMnuTreeViewAddWCFWebService.Name = "ctxMnuTreeViewAddWCFWebService";
      this.ctxMnuTreeViewAddWCFWebService.Size = new System.Drawing.Size(214, 22);
      this.ctxMnuTreeViewAddWCFWebService.Tag = "AddWCFWebService";
      this.ctxMnuTreeViewAddWCFWebService.Text = "Add WCF Web Service";
      this.ctxMnuTreeViewAddWCFWebService.Click += new System.EventHandler(this.Action);
      //
      // ctxMnuTreeViewDeleteWCFWebService
      //
      this.ctxMnuTreeViewDeleteWCFWebService.Name = "ctxMnuTreeViewDeleteWCFWebService";
      this.ctxMnuTreeViewDeleteWCFWebService.Size = new System.Drawing.Size(214, 22);
      this.ctxMnuTreeViewDeleteWCFWebService.Tag = "DeleteWCFWebService";
      this.ctxMnuTreeViewDeleteWCFWebService.Text = "Delete WCF Web Service";
      this.ctxMnuTreeViewDeleteWCFWebService.Click += new System.EventHandler(this.Action);
      //
      // ctxMnuTreeViewAddWebSite
      //
      this.ctxMnuTreeViewAddWebSite.Name = "ctxMnuTreeViewAddWebSite";
      this.ctxMnuTreeViewAddWebSite.Size = new System.Drawing.Size(214, 22);
      this.ctxMnuTreeViewAddWebSite.Tag = "AddWebSite";
      this.ctxMnuTreeViewAddWebSite.Text = "Add Web Site";
      this.ctxMnuTreeViewAddWebSite.Click += new System.EventHandler(this.Action);
      //
      // imgListTreeView
      //
      this.imgListTreeView.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imgListTreeView.ImageSize = new System.Drawing.Size(16, 16);
      this.imgListTreeView.TransparentColor = System.Drawing.Color.Transparent;
      //
      // pnlTvBottom
      //
      this.pnlTvBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlTvBottom.Location = new System.Drawing.Point(0, 550);
      this.pnlTvBottom.Name = "pnlTvBottom";
      this.pnlTvBottom.Size = new System.Drawing.Size(255, 38);
      this.pnlTvBottom.TabIndex = 2;
      //
      // pnlTvTop
      //
      this.pnlTvTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTvTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTvTop.Name = "pnlTvTop";
      this.pnlTvTop.Size = new System.Drawing.Size(255, 38);
      this.pnlTvTop.TabIndex = 1;
      //
      // pnlWinSvc
      //
      this.pnlWinSvc.BackColor = System.Drawing.SystemColors.Control;
      this.pnlWinSvc.Controls.Add(this.txtOut);
      this.pnlWinSvc.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlWinSvc.Location = new System.Drawing.Point(4, 4);
      this.pnlWinSvc.Name = "pnlWinSvc";
      this.pnlWinSvc.Size = new System.Drawing.Size(787, 580);
      this.pnlWinSvc.TabIndex = 0;
      //
      // txtOut
      //
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Top;
      this.txtOut.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(0, 0);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(787, 549);
      this.txtOut.TabIndex = 0;
      //
      // btnXml
      //
      this.btnXml.Location = new System.Drawing.Point(507, 9);
      this.btnXml.Name = "btnXml";
      this.btnXml.Size = new System.Drawing.Size(75, 23);
      this.btnXml.TabIndex = 0;
      this.btnXml.Tag = "Xml";
      this.btnXml.Text = "Xml";
      this.btnXml.UseVisualStyleBackColor = true;
      this.btnXml.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1065, 679);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Service Manager - 1.0.0.0";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlMain.ResumeLayout(false);
      this.splitMain.Panel1.ResumeLayout(false);
      this.splitMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
      this.splitMain.ResumeLayout(false);
      this.ctxMenuTreeView.ResumeLayout(false);
      this.pnlWinSvc.ResumeLayout(false);
      this.pnlWinSvc.PerformLayout();
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
    private System.Windows.Forms.SplitContainer splitMain;
    private System.Windows.Forms.TreeView tvMain;
    private System.Windows.Forms.Panel pnlTvBottom;
    private System.Windows.Forms.Panel pnlTvTop;
    private System.Windows.Forms.Panel pnlWinSvc;
    private System.Windows.Forms.ImageList imgListTreeView;
    private System.Windows.Forms.ContextMenuStrip ctxMenuTreeView;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuTreeViewManageTaskAssignments;
    private System.Windows.Forms.ToolStripMenuItem mnuMaintenance;
    private System.Windows.Forms.ToolStripMenuItem mnuMaintenanceManageTaskServices;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuTreeViewManageHosts;
    private System.Windows.Forms.ToolStripMenuItem mnuMaintenanceSetTestServiceTypes;
    private System.Windows.Forms.ToolStripMenuItem mnuMaintenanceSetProdServiceTypes;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuTreeViewAddWindowsService;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuTreeViewAddWCFWebService;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuTreeViewAddWebSite;
    private System.Windows.Forms.ToolStripMenuItem mnuMaintenanceRefreshTreeView;
    private System.Windows.Forms.ToolStripMenuItem mnuMaintenanceRunMigr;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.ToolStripMenuItem ctxMnuTreeViewDeleteWCFWebService;
    private System.Windows.Forms.Button btnXml;
  }
}


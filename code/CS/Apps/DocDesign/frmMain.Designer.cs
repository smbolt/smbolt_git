namespace Org.DocDesign
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
      this.pnlTop = new System.Windows.Forms.Panel();
      this.pnlToolbar = new System.Windows.Forms.Panel();
      this.toolStripMain = new System.Windows.Forms.ToolStrip();
      this.tbtnGenerateDocument = new System.Windows.Forms.ToolStripButton();
      this.cboPackages = new System.Windows.Forms.ToolStripComboBox();
      this.tbtnDocTree = new System.Windows.Forms.ToolStripDropDownButton();
      this.tmnuHideDocTree = new System.Windows.Forms.ToolStripMenuItem();
      this.tmnuShowDocTree = new System.Windows.Forms.ToolStripMenuItem();
      this.tmnuDockDocTree = new System.Windows.Forms.ToolStripMenuItem();
      this.tmnuFloatDocTree = new System.Windows.Forms.ToolStripMenuItem();
      this.tbtnCode = new System.Windows.Forms.ToolStripDropDownButton();
      this.tmnuHideCode = new System.Windows.Forms.ToolStripMenuItem();
      this.tmnuShowCode = new System.Windows.Forms.ToolStripMenuItem();
      this.tmnuDockCode = new System.Windows.Forms.ToolStripMenuItem();
      this.tmnuFloatCode = new System.Windows.Forms.ToolStripMenuItem();
      this.tbtnProperties = new System.Windows.Forms.ToolStripButton();
      this.tbtnInfo = new System.Windows.Forms.ToolStripButton();
      this.tbtnImage = new System.Windows.Forms.ToolStripButton();
      this.tbtnControlPanel = new System.Windows.Forms.ToolStripButton();
      this.tbtnDebug = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.lblScale = new System.Windows.Forms.ToolStripLabel();
      this.tbtnZoomIn = new System.Windows.Forms.ToolStripButton();
      this.tbtnZoomOut = new System.Windows.Forms.ToolStripButton();
      this.statusStripMain = new System.Windows.Forms.StatusStrip();
      this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.splitterTv = new System.Windows.Forms.SplitContainer();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.splitterImageAndCode = new System.Windows.Forms.SplitContainer();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageImage = new System.Windows.Forms.TabPage();
      this.pnlImage = new System.Windows.Forms.Panel();
      this.pbOverlay = new System.Windows.Forms.PictureBox();
      this.tabPageValidationErrors = new System.Windows.Forms.TabPage();
      this.txtValidationErrors = new System.Windows.Forms.TextBox();
      this.tabPageWordprocessingXml = new System.Windows.Forms.TabPage();
      this.txtWordXml = new System.Windows.Forms.TextBox();
      this.tabTags = new System.Windows.Forms.TabPage();
      this.txtTagReport = new System.Windows.Forms.TextBox();
      this.tabPageResumeXml = new System.Windows.Forms.TabPage();
      this.txtResumeXml = new System.Windows.Forms.TextBox();
      this.tabPageRegions = new System.Windows.Forms.TabPage();
      this.txtRegionsOccupied = new System.Windows.Forms.TextBox();
      this.mnuDebug = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuDebugMarkOverlay = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuDebugClearOverlay = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.pnlToolbar.SuspendLayout();
      this.toolStripMain.SuspendLayout();
      this.statusStripMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterTv)).BeginInit();
      this.splitterTv.Panel2.SuspendLayout();
      this.splitterTv.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterImageAndCode)).BeginInit();
      this.splitterImageAndCode.Panel1.SuspendLayout();
      this.splitterImageAndCode.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageImage.SuspendLayout();
      this.pnlImage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbOverlay)).BeginInit();
      this.tabPageValidationErrors.SuspendLayout();
      this.tabPageWordprocessingXml.SuspendLayout();
      this.tabTags.SuspendLayout();
      this.tabPageResumeXml.SuspendLayout();
      this.tabPageRegions.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile,
        this.mnuDebug
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1031, 24);
      this.mnuMain.TabIndex = 1;
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
      this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // pnlTop
      //
      this.pnlTop.BackColor = System.Drawing.Color.LightSteelBlue;
      this.pnlTop.Controls.Add(this.pnlToolbar);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1031, 39);
      this.pnlTop.TabIndex = 2;
      //
      // pnlToolbar
      //
      this.pnlToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlToolbar.Controls.Add(this.toolStripMain);
      this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
      this.pnlToolbar.Name = "pnlToolbar";
      this.pnlToolbar.Size = new System.Drawing.Size(1035, 39);
      this.pnlToolbar.TabIndex = 3;
      //
      // toolStripMain
      //
      this.toolStripMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                   | System.Windows.Forms.AnchorStyles.Right)));
      this.toolStripMain.AutoSize = false;
      this.toolStripMain.BackColor = System.Drawing.Color.Transparent;
      this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.tbtnGenerateDocument,
        this.cboPackages,
        this.tbtnDocTree,
        this.tbtnCode,
        this.tbtnProperties,
        this.tbtnInfo,
        this.tbtnImage,
        this.tbtnControlPanel,
        this.tbtnDebug,
        this.toolStripSeparator1,
        this.lblScale,
        this.tbtnZoomIn,
        this.tbtnZoomOut
      });
      this.toolStripMain.Location = new System.Drawing.Point(-1, 0);
      this.toolStripMain.Name = "toolStripMain";
      this.toolStripMain.Padding = new System.Windows.Forms.Padding(4, 0, 1, 0);
      this.toolStripMain.Size = new System.Drawing.Size(1033, 41);
      this.toolStripMain.TabIndex = 0;
      //
      // tbtnGenerateDocument
      //
      this.tbtnGenerateDocument.AutoSize = false;
      this.tbtnGenerateDocument.AutoToolTip = false;
      this.tbtnGenerateDocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnGenerateDocument.Image = global::Org.DocDesign.Properties.Resources.GenerateDocument;
      this.tbtnGenerateDocument.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnGenerateDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnGenerateDocument.Name = "tbtnGenerateDocument";
      this.tbtnGenerateDocument.Size = new System.Drawing.Size(36, 36);
      this.tbtnGenerateDocument.Tag = "GenerateDocument";
      this.tbtnGenerateDocument.ToolTipText = "Generate Document";
      this.tbtnGenerateDocument.Click += new System.EventHandler(this.Action);
      //
      // cboPackages
      //
      this.cboPackages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboPackages.Name = "cboPackages";
      this.cboPackages.Size = new System.Drawing.Size(121, 41);
      //
      // tbtnDocTree
      //
      this.tbtnDocTree.AutoSize = false;
      this.tbtnDocTree.AutoToolTip = false;
      this.tbtnDocTree.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnDocTree.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.tmnuHideDocTree,
        this.tmnuShowDocTree,
        this.tmnuDockDocTree,
        this.tmnuFloatDocTree
      });
      this.tbtnDocTree.Image = global::Org.DocDesign.Properties.Resources.DocTree;
      this.tbtnDocTree.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnDocTree.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnDocTree.Name = "tbtnDocTree";
      this.tbtnDocTree.Size = new System.Drawing.Size(48, 36);
      this.tbtnDocTree.Tag = "ToggleTreeView";
      this.tbtnDocTree.ToolTipText = "Document Tree";
      this.tbtnDocTree.Click += new System.EventHandler(this.Action);
      //
      // tmnuHideDocTree
      //
      this.tmnuHideDocTree.Name = "tmnuHideDocTree";
      this.tmnuHideDocTree.Size = new System.Drawing.Size(153, 22);
      this.tmnuHideDocTree.Tag = "TW_Hide_TreeView";
      this.tmnuHideDocTree.Text = "Hide Doc Tree";
      this.tmnuHideDocTree.ToolTipText = "Hide the Document Tree";
      this.tmnuHideDocTree.Click += new System.EventHandler(this.Action);
      //
      // tmnuShowDocTree
      //
      this.tmnuShowDocTree.Name = "tmnuShowDocTree";
      this.tmnuShowDocTree.Size = new System.Drawing.Size(153, 22);
      this.tmnuShowDocTree.Tag = "TW_Show_TreeView";
      this.tmnuShowDocTree.Text = "Show Doc Tree";
      this.tmnuShowDocTree.Click += new System.EventHandler(this.Action);
      //
      // tmnuDockDocTree
      //
      this.tmnuDockDocTree.Name = "tmnuDockDocTree";
      this.tmnuDockDocTree.Size = new System.Drawing.Size(153, 22);
      this.tmnuDockDocTree.Tag = "TW_Dock_TreeView";
      this.tmnuDockDocTree.Text = "Dock DocTree";
      this.tmnuDockDocTree.ToolTipText = "Hide the Document Tree";
      this.tmnuDockDocTree.Click += new System.EventHandler(this.Action);
      //
      // tmnuFloatDocTree
      //
      this.tmnuFloatDocTree.Name = "tmnuFloatDocTree";
      this.tmnuFloatDocTree.Size = new System.Drawing.Size(153, 22);
      this.tmnuFloatDocTree.Tag = "TW_Float_TreeView";
      this.tmnuFloatDocTree.Text = "Float DocTree";
      this.tmnuFloatDocTree.ToolTipText = "Float the Document Tree";
      this.tmnuFloatDocTree.Click += new System.EventHandler(this.Action);
      //
      // tbtnCode
      //
      this.tbtnCode.AutoSize = false;
      this.tbtnCode.AutoToolTip = false;
      this.tbtnCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnCode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.tmnuHideCode,
        this.tmnuShowCode,
        this.tmnuDockCode,
        this.tmnuFloatCode
      });
      this.tbtnCode.Image = global::Org.DocDesign.Properties.Resources.Code;
      this.tbtnCode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnCode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnCode.Name = "tbtnCode";
      this.tbtnCode.Size = new System.Drawing.Size(48, 36);
      this.tbtnCode.Tag = "ToggleCode";
      this.tbtnCode.ToolTipText = "Document Code";
      //
      // tmnuHideCode
      //
      this.tmnuHideCode.Name = "tmnuHideCode";
      this.tmnuHideCode.Size = new System.Drawing.Size(181, 22);
      this.tmnuHideCode.Tag = "TW_Hide_Code";
      this.tmnuHideCode.Text = "Hide Code Window";
      this.tmnuHideCode.ToolTipText = "Hide the Code Window";
      this.tmnuHideCode.Click += new System.EventHandler(this.Action);
      //
      // tmnuShowCode
      //
      this.tmnuShowCode.Name = "tmnuShowCode";
      this.tmnuShowCode.Size = new System.Drawing.Size(181, 22);
      this.tmnuShowCode.Tag = "TW_Show_Code";
      this.tmnuShowCode.Text = "Show Code Window";
      this.tmnuShowCode.ToolTipText = "Show the Code Window";
      this.tmnuShowCode.Click += new System.EventHandler(this.Action);
      //
      // tmnuDockCode
      //
      this.tmnuDockCode.Name = "tmnuDockCode";
      this.tmnuDockCode.Size = new System.Drawing.Size(181, 22);
      this.tmnuDockCode.Tag = "TW_Dock_Code";
      this.tmnuDockCode.Text = "Dock Code Window";
      this.tmnuDockCode.ToolTipText = "Dock the Code Window";
      this.tmnuDockCode.Click += new System.EventHandler(this.Action);
      //
      // tmnuFloatCode
      //
      this.tmnuFloatCode.Name = "tmnuFloatCode";
      this.tmnuFloatCode.Size = new System.Drawing.Size(181, 22);
      this.tmnuFloatCode.Tag = "TW_Float_Code";
      this.tmnuFloatCode.Text = "Float Code Window";
      this.tmnuFloatCode.ToolTipText = "Float the Code Window";
      this.tmnuFloatCode.Click += new System.EventHandler(this.Action);
      //
      // tbtnProperties
      //
      this.tbtnProperties.AutoSize = false;
      this.tbtnProperties.AutoToolTip = false;
      this.tbtnProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnProperties.Image = global::Org.DocDesign.Properties.Resources.Properties;
      this.tbtnProperties.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnProperties.Name = "tbtnProperties";
      this.tbtnProperties.Size = new System.Drawing.Size(36, 36);
      this.tbtnProperties.Tag = "TW_Toggle_Properties";
      this.tbtnProperties.ToolTipText = "Toggle Properties Window";
      this.tbtnProperties.Click += new System.EventHandler(this.Action);
      //
      // tbtnInfo
      //
      this.tbtnInfo.AutoSize = false;
      this.tbtnInfo.AutoToolTip = false;
      this.tbtnInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnInfo.Image = global::Org.DocDesign.Properties.Resources.Info;
      this.tbtnInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnInfo.Name = "tbtnInfo";
      this.tbtnInfo.Size = new System.Drawing.Size(36, 36);
      this.tbtnInfo.Tag = "TW_Toggle_Info";
      this.tbtnInfo.ToolTipText = "Toggle Info Panel";
      this.tbtnInfo.Click += new System.EventHandler(this.Action);
      //
      // tbtnImage
      //
      this.tbtnImage.AutoSize = false;
      this.tbtnImage.AutoToolTip = false;
      this.tbtnImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnImage.Image = global::Org.DocDesign.Properties.Resources.Image;
      this.tbtnImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnImage.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnImage.Name = "tbtnImage";
      this.tbtnImage.Size = new System.Drawing.Size(36, 36);
      this.tbtnImage.Tag = "TW_Toggle_Image";
      this.tbtnImage.ToolTipText = "Toggle Image Panel";
      this.tbtnImage.Click += new System.EventHandler(this.Action);
      //
      // tbtnControlPanel
      //
      this.tbtnControlPanel.AutoSize = false;
      this.tbtnControlPanel.AutoToolTip = false;
      this.tbtnControlPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnControlPanel.Image = global::Org.DocDesign.Properties.Resources.ControlPanel;
      this.tbtnControlPanel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnControlPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnControlPanel.Name = "tbtnControlPanel";
      this.tbtnControlPanel.Size = new System.Drawing.Size(36, 36);
      this.tbtnControlPanel.Tag = "TW_Toggle_ControlPanel";
      this.tbtnControlPanel.ToolTipText = "Toggle Control Panel visibility";
      this.tbtnControlPanel.Click += new System.EventHandler(this.Action);
      //
      // tbtnDebug
      //
      this.tbtnDebug.AutoSize = false;
      this.tbtnDebug.AutoToolTip = false;
      this.tbtnDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnDebug.Image = global::Org.DocDesign.Properties.Resources.Debug;
      this.tbtnDebug.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnDebug.Name = "tbtnDebug";
      this.tbtnDebug.Size = new System.Drawing.Size(36, 36);
      this.tbtnDebug.Tag = "TW_Toggle_Debug";
      this.tbtnDebug.ToolTipText = "Toggle Control Panel visibility";
      this.tbtnDebug.Click += new System.EventHandler(this.Action);
      //
      // toolStripSeparator1
      //
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 41);
      //
      // lblScale
      //
      this.lblScale.AutoSize = false;
      this.lblScale.Margin = new System.Windows.Forms.Padding(0, 1, 6, 2);
      this.lblScale.Name = "lblScale";
      this.lblScale.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
      this.lblScale.Size = new System.Drawing.Size(45, 38);
      this.lblScale.Text = "100%";
      this.lblScale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // tbtnZoomIn
      //
      this.tbtnZoomIn.AutoSize = false;
      this.tbtnZoomIn.AutoToolTip = false;
      this.tbtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnZoomIn.Image = global::Org.DocDesign.Properties.Resources.ZoomIn;
      this.tbtnZoomIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnZoomIn.Name = "tbtnZoomIn";
      this.tbtnZoomIn.Size = new System.Drawing.Size(36, 36);
      this.tbtnZoomIn.Tag = "ZoomIn";
      this.tbtnZoomIn.ToolTipText = "Generate Document";
      this.tbtnZoomIn.Click += new System.EventHandler(this.Action);
      //
      // tbtnZoomOut
      //
      this.tbtnZoomOut.AutoSize = false;
      this.tbtnZoomOut.AutoToolTip = false;
      this.tbtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbtnZoomOut.Image = global::Org.DocDesign.Properties.Resources.ZoomOut;
      this.tbtnZoomOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnZoomOut.Name = "tbtnZoomOut";
      this.tbtnZoomOut.Size = new System.Drawing.Size(36, 36);
      this.tbtnZoomOut.Tag = "ZoomOut";
      this.tbtnZoomOut.ToolTipText = "Generate Document";
      this.tbtnZoomOut.Click += new System.EventHandler(this.Action);
      //
      // statusStripMain
      //
      this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.lblStatus
      });
      this.statusStripMain.Location = new System.Drawing.Point(0, 531);
      this.statusStripMain.Name = "statusStripMain";
      this.statusStripMain.Size = new System.Drawing.Size(1031, 22);
      this.statusStripMain.TabIndex = 3;
      this.statusStripMain.Text = "statusStrip1";
      //
      // lblStatus
      //
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(39, 17);
      this.lblStatus.Text = "Status";
      //
      // splitterTv
      //
      this.splitterTv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.splitterTv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterTv.Location = new System.Drawing.Point(0, 63);
      this.splitterTv.Name = "splitterTv";
      //
      // splitterTv.Panel2
      //
      this.splitterTv.Panel2.Controls.Add(this.splitterMain);
      this.splitterTv.Size = new System.Drawing.Size(1031, 468);
      this.splitterTv.SplitterDistance = 176;
      this.splitterTv.SplitterWidth = 2;
      this.splitterTv.TabIndex = 4;
      //
      // splitterMain
      //
      this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.Location = new System.Drawing.Point(0, 0);
      this.splitterMain.Name = "splitterMain";
      //
      // splitterMain.Panel1
      //
      this.splitterMain.Panel1.Controls.Add(this.splitterImageAndCode);
      this.splitterMain.Size = new System.Drawing.Size(853, 468);
      this.splitterMain.SplitterDistance = 701;
      this.splitterMain.SplitterWidth = 2;
      this.splitterMain.TabIndex = 0;
      //
      // splitterImageAndCode
      //
      this.splitterImageAndCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.splitterImageAndCode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterImageAndCode.Location = new System.Drawing.Point(0, 0);
      this.splitterImageAndCode.Name = "splitterImageAndCode";
      this.splitterImageAndCode.Orientation = System.Windows.Forms.Orientation.Horizontal;
      //
      // splitterImageAndCode.Panel1
      //
      this.splitterImageAndCode.Panel1.Controls.Add(this.tabMain);
      this.splitterImageAndCode.Size = new System.Drawing.Size(701, 468);
      this.splitterImageAndCode.SplitterDistance = 375;
      this.splitterImageAndCode.SplitterWidth = 2;
      this.splitterImageAndCode.TabIndex = 0;
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageImage);
      this.tabMain.Controls.Add(this.tabPageValidationErrors);
      this.tabMain.Controls.Add(this.tabPageWordprocessingXml);
      this.tabMain.Controls.Add(this.tabTags);
      this.tabMain.Controls.Add(this.tabPageResumeXml);
      this.tabMain.Controls.Add(this.tabPageRegions);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(135, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 0);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(697, 371);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 1;
      //
      // tabPageImage
      //
      this.tabPageImage.Controls.Add(this.pnlImage);
      this.tabPageImage.Location = new System.Drawing.Point(4, 22);
      this.tabPageImage.Name = "tabPageImage";
      this.tabPageImage.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageImage.Size = new System.Drawing.Size(689, 345);
      this.tabPageImage.TabIndex = 0;
      this.tabPageImage.Text = "Document Image";
      this.tabPageImage.UseVisualStyleBackColor = true;
      //
      // pnlImage
      //
      this.pnlImage.AutoScroll = true;
      this.pnlImage.AutoScrollMargin = new System.Drawing.Size(0, 50);
      this.pnlImage.BackColor = System.Drawing.Color.Silver;
      this.pnlImage.Controls.Add(this.pbOverlay);
      this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlImage.Location = new System.Drawing.Point(3, 3);
      this.pnlImage.Name = "pnlImage";
      this.pnlImage.Size = new System.Drawing.Size(683, 339);
      this.pnlImage.TabIndex = 2;
      //
      // pbOverlay
      //
      this.pbOverlay.BackColor = System.Drawing.Color.Transparent;
      this.pbOverlay.Location = new System.Drawing.Point(8, 17);
      this.pbOverlay.Name = "pbOverlay";
      this.pbOverlay.Size = new System.Drawing.Size(100, 50);
      this.pbOverlay.TabIndex = 5;
      this.pbOverlay.TabStop = false;
      this.pbOverlay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseClick);
      this.pbOverlay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
      //
      // tabPageValidationErrors
      //
      this.tabPageValidationErrors.Controls.Add(this.txtValidationErrors);
      this.tabPageValidationErrors.Location = new System.Drawing.Point(4, 22);
      this.tabPageValidationErrors.Name = "tabPageValidationErrors";
      this.tabPageValidationErrors.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageValidationErrors.Size = new System.Drawing.Size(716, 341);
      this.tabPageValidationErrors.TabIndex = 1;
      this.tabPageValidationErrors.Text = "Validation Errors";
      this.tabPageValidationErrors.UseVisualStyleBackColor = true;
      //
      // txtValidationErrors
      //
      this.txtValidationErrors.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtValidationErrors.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtValidationErrors.Location = new System.Drawing.Point(3, 3);
      this.txtValidationErrors.Multiline = true;
      this.txtValidationErrors.Name = "txtValidationErrors";
      this.txtValidationErrors.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtValidationErrors.Size = new System.Drawing.Size(710, 335);
      this.txtValidationErrors.TabIndex = 1;
      //
      // tabPageWordprocessingXml
      //
      this.tabPageWordprocessingXml.Controls.Add(this.txtWordXml);
      this.tabPageWordprocessingXml.Location = new System.Drawing.Point(4, 22);
      this.tabPageWordprocessingXml.Name = "tabPageWordprocessingXml";
      this.tabPageWordprocessingXml.Size = new System.Drawing.Size(716, 341);
      this.tabPageWordprocessingXml.TabIndex = 2;
      this.tabPageWordprocessingXml.Text = "Wordprocessing Xml";
      this.tabPageWordprocessingXml.UseVisualStyleBackColor = true;
      //
      // txtWordXml
      //
      this.txtWordXml.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtWordXml.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtWordXml.Location = new System.Drawing.Point(0, 0);
      this.txtWordXml.Multiline = true;
      this.txtWordXml.Name = "txtWordXml";
      this.txtWordXml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtWordXml.Size = new System.Drawing.Size(716, 341);
      this.txtWordXml.TabIndex = 1;
      //
      // tabTags
      //
      this.tabTags.Controls.Add(this.txtTagReport);
      this.tabTags.Location = new System.Drawing.Point(4, 22);
      this.tabTags.Name = "tabTags";
      this.tabTags.Size = new System.Drawing.Size(716, 341);
      this.tabTags.TabIndex = 3;
      this.tabTags.Text = "Tags";
      this.tabTags.UseVisualStyleBackColor = true;
      //
      // txtTagReport
      //
      this.txtTagReport.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtTagReport.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtTagReport.Location = new System.Drawing.Point(0, 0);
      this.txtTagReport.Multiline = true;
      this.txtTagReport.Name = "txtTagReport";
      this.txtTagReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtTagReport.Size = new System.Drawing.Size(716, 341);
      this.txtTagReport.TabIndex = 2;
      //
      // tabPageResumeXml
      //
      this.tabPageResumeXml.Controls.Add(this.txtResumeXml);
      this.tabPageResumeXml.Location = new System.Drawing.Point(4, 22);
      this.tabPageResumeXml.Name = "tabPageResumeXml";
      this.tabPageResumeXml.Size = new System.Drawing.Size(716, 341);
      this.tabPageResumeXml.TabIndex = 4;
      this.tabPageResumeXml.Text = "Resume Xml";
      this.tabPageResumeXml.UseVisualStyleBackColor = true;
      //
      // txtResumeXml
      //
      this.txtResumeXml.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtResumeXml.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtResumeXml.Location = new System.Drawing.Point(0, 0);
      this.txtResumeXml.Multiline = true;
      this.txtResumeXml.Name = "txtResumeXml";
      this.txtResumeXml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtResumeXml.Size = new System.Drawing.Size(716, 341);
      this.txtResumeXml.TabIndex = 2;
      this.txtResumeXml.WordWrap = false;
      //
      // tabPageRegions
      //
      this.tabPageRegions.Controls.Add(this.txtRegionsOccupied);
      this.tabPageRegions.Location = new System.Drawing.Point(4, 22);
      this.tabPageRegions.Name = "tabPageRegions";
      this.tabPageRegions.Size = new System.Drawing.Size(716, 341);
      this.tabPageRegions.TabIndex = 5;
      this.tabPageRegions.Text = "Regions Occupied";
      this.tabPageRegions.UseVisualStyleBackColor = true;
      //
      // txtRegionsOccupied
      //
      this.txtRegionsOccupied.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtRegionsOccupied.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtRegionsOccupied.Location = new System.Drawing.Point(0, 0);
      this.txtRegionsOccupied.Multiline = true;
      this.txtRegionsOccupied.Name = "txtRegionsOccupied";
      this.txtRegionsOccupied.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtRegionsOccupied.Size = new System.Drawing.Size(716, 341);
      this.txtRegionsOccupied.TabIndex = 3;
      this.txtRegionsOccupied.WordWrap = false;
      //
      // mnuDebug
      //
      this.mnuDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuDebugMarkOverlay,
        this.mnuDebugClearOverlay
      });
      this.mnuDebug.Name = "mnuDebug";
      this.mnuDebug.Size = new System.Drawing.Size(54, 20);
      this.mnuDebug.Text = "Debug";
      //
      // mnuDebugMarkOverlay
      //
      this.mnuDebugMarkOverlay.Name = "mnuDebugMarkOverlay";
      this.mnuDebugMarkOverlay.Size = new System.Drawing.Size(152, 22);
      this.mnuDebugMarkOverlay.Tag = "MarkOverlay";
      this.mnuDebugMarkOverlay.Text = "Mark Overlay";
      this.mnuDebugMarkOverlay.Click += new System.EventHandler(this.Action);
      //
      // mnuDebugClearOverlay
      //
      this.mnuDebugClearOverlay.Name = "mnuDebugClearOverlay";
      this.mnuDebugClearOverlay.Size = new System.Drawing.Size(152, 22);
      this.mnuDebugClearOverlay.Tag = "ClearOverlay";
      this.mnuDebugClearOverlay.Text = "Clear Overlay";
      this.mnuDebugClearOverlay.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1031, 553);
      this.Controls.Add(this.splitterTv);
      this.Controls.Add(this.statusStripMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Tag = "MainForm";
      this.Text = "ADSDI - Document Designer";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlToolbar.ResumeLayout(false);
      this.toolStripMain.ResumeLayout(false);
      this.toolStripMain.PerformLayout();
      this.statusStripMain.ResumeLayout(false);
      this.statusStripMain.PerformLayout();
      this.splitterTv.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterTv)).EndInit();
      this.splitterTv.ResumeLayout(false);
      this.splitterMain.Panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      this.splitterImageAndCode.Panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterImageAndCode)).EndInit();
      this.splitterImageAndCode.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageImage.ResumeLayout(false);
      this.pnlImage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbOverlay)).EndInit();
      this.tabPageValidationErrors.ResumeLayout(false);
      this.tabPageValidationErrors.PerformLayout();
      this.tabPageWordprocessingXml.ResumeLayout(false);
      this.tabPageWordprocessingXml.PerformLayout();
      this.tabTags.ResumeLayout(false);
      this.tabTags.PerformLayout();
      this.tabPageResumeXml.ResumeLayout(false);
      this.tabPageResumeXml.PerformLayout();
      this.tabPageRegions.ResumeLayout(false);
      this.tabPageRegions.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.StatusStrip statusStripMain;
    private System.Windows.Forms.SplitContainer splitterTv;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.SplitContainer splitterImageAndCode;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageImage;
    private System.Windows.Forms.TabPage tabPageValidationErrors;
    private System.Windows.Forms.TextBox txtValidationErrors;
    private System.Windows.Forms.TabPage tabPageWordprocessingXml;
    private System.Windows.Forms.TextBox txtWordXml;
    private System.Windows.Forms.TabPage tabTags;
    private System.Windows.Forms.TextBox txtTagReport;
    private System.Windows.Forms.TabPage tabPageResumeXml;
    private System.Windows.Forms.TextBox txtResumeXml;
    private System.Windows.Forms.TabPage tabPageRegions;
    private System.Windows.Forms.TextBox txtRegionsOccupied;
    private System.Windows.Forms.Panel pnlImage;
    private System.Windows.Forms.PictureBox pbOverlay;
    private System.Windows.Forms.Panel pnlToolbar;
    private System.Windows.Forms.ToolStrip toolStripMain;
    private System.Windows.Forms.ToolStripButton tbtnGenerateDocument;
    private System.Windows.Forms.ToolStripComboBox cboPackages;
    private System.Windows.Forms.ToolStripDropDownButton tbtnDocTree;
    private System.Windows.Forms.ToolStripMenuItem tmnuHideDocTree;
    private System.Windows.Forms.ToolStripMenuItem tmnuDockDocTree;
    private System.Windows.Forms.ToolStripMenuItem tmnuFloatDocTree;
    private System.Windows.Forms.ToolStripDropDownButton tbtnCode;
    private System.Windows.Forms.ToolStripMenuItem tmnuHideCode;
    private System.Windows.Forms.ToolStripMenuItem tmnuDockCode;
    private System.Windows.Forms.ToolStripMenuItem tmnuFloatCode;
    private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    private System.Windows.Forms.ToolStripMenuItem tmnuShowDocTree;
    private System.Windows.Forms.ToolStripMenuItem tmnuShowCode;
    private System.Windows.Forms.ToolStripButton tbtnProperties;
    private System.Windows.Forms.ToolStripButton tbtnInfo;
    private System.Windows.Forms.ToolStripButton tbtnImage;
    private System.Windows.Forms.ToolStripButton tbtnControlPanel;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripLabel lblScale;
    private System.Windows.Forms.ToolStripButton tbtnZoomIn;
    private System.Windows.Forms.ToolStripButton tbtnZoomOut;
    private System.Windows.Forms.ToolStripButton tbtnDebug;
    private System.Windows.Forms.ToolStripMenuItem mnuDebug;
    private System.Windows.Forms.ToolStripMenuItem mnuDebugMarkOverlay;
    private System.Windows.Forms.ToolStripMenuItem mnuDebugClearOverlay;
  }
}


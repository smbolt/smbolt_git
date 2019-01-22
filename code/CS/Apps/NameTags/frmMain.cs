using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Resources;
using System.Reflection;
using Org.CHCM.Business;
using Models = Org.CHCM.Business.Models;
using Org.GS.Configuration;
using Org.GS;

namespace NameTags
{
  /// <summary>
  /// Summary description for Form1.
  /// </summary>
  public class frmMain : System.Windows.Forms.Form
  {
    private a a;
    private string _dbSpecPrefix;
    private ConfigDbSpec _configDbSpec;

    private Project _project;

    private ImageList imgList = new ImageList();
    private Font fontName = new Font("Monotype Corsiva", 36, FontStyle.Italic);
    private Font fontOther = new Font("Times New Roman", 18, FontStyle.Italic);

    private int[] selectedObjectKeys = new int[0];
    private int[] getObjectKeys = new int[0];
    private char dimToChange = ' ';
    private bool bChangeBegun = false;
    private float beginX;
    private float beginY;

    private bool IsFirstShowingOfForm = true;

    #region Generated Class Members

    private System.Windows.Forms.MainMenu mnuMain;
    private System.Windows.Forms.MenuItem mnuFile;
    private System.Windows.Forms.MenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlToolBar;
    private System.Windows.Forms.Panel pnlStatusBar;
    private System.Windows.Forms.Panel pnlLeft;
    private System.Windows.Forms.Splitter splitMain;
    private System.Windows.Forms.Panel pnlRight;
    private System.Windows.Forms.Panel pnlShadow;
    private System.Windows.Forms.PictureBox pbMain;
    private System.Windows.Forms.Label lblScale;
    private System.Windows.Forms.FontDialog dlgFont;
    private System.Windows.Forms.Label lblTagCount;
    private System.Windows.Forms.MenuItem mnuView;
    private System.Windows.Forms.Panel pnlDesignTools;
    private System.Windows.Forms.ToolBar tbDesign;
    private System.Windows.Forms.ToolBarButton tbtnOpen;
    private System.Windows.Forms.ToolBarButton tbtnSave;
    private System.Windows.Forms.ToolBarButton tbtnPrintMode;
    private System.Windows.Forms.ToolBarButton tbtnNextPage;
    private System.Windows.Forms.ToolBarButton tbtnPrevPage;
    private System.Windows.Forms.ToolBarButton tbtnEditNames;
    private System.Windows.Forms.ToolBarButton tbtnText;
    private System.Windows.Forms.Label lblSelectedKey;
    private System.Windows.Forms.ToolBarButton tbbtnFont;
    private System.Windows.Forms.ToolBarButton tbbtnRectangle;
    private System.Windows.Forms.ColorDialog dlgColor;
    private System.Windows.Forms.ToolBarButton tbbtnColor;
    private System.Windows.Forms.OpenFileDialog dlgFile;
    private System.Windows.Forms.ToolBarButton tbtnZoomIn;
    private System.Windows.Forms.ToolBarButton tbtnZoomOut;
    private System.Windows.Forms.ToolBarButton tbtnDesignMode;
    private System.Windows.Forms.ToolBarButton tbtnPrintTags;
    private System.Windows.Forms.ToolBarButton tbtnPicture;
    private System.Windows.Forms.ToolBarButton tbtnSep1;
    private System.Windows.Forms.Label lblMousePos;
    private System.Windows.Forms.Label lblMousePosVal;
    private System.Windows.Forms.Label lblTagSizeVal;
    private System.Windows.Forms.Label lblTagSize;
    private System.Windows.Forms.Label lblObjPosVal;
    private System.Windows.Forms.Label lblObjPos;
    private System.Windows.Forms.Label lblObjSize;
    private System.Windows.Forms.Label lblObjSizeVal;
    private System.Windows.Forms.Label lblScalePos;
    private System.Windows.Forms.Label lblScalePosVal;
    private System.Windows.Forms.Label lblScaleSize;
    private System.Windows.Forms.Label lblScaleSizeVal;
    private System.Windows.Forms.Label lblMouseMoveVal;
    private System.Windows.Forms.Label lblMouseMove;
    private System.Windows.Forms.SaveFileDialog dlgFileSave;
    private System.Windows.Forms.ContextMenu mnuPbMainContext;
    private System.Windows.Forms.MenuItem mnuPbMainContextArrange;
    private System.Windows.Forms.MenuItem mnuPbMainContextArrangeFront;
    private System.Windows.Forms.MenuItem mnuPbMainContextArrangeForward;
    private System.Windows.Forms.MenuItem mnuPbMainContextArrangeBackward;
    private System.Windows.Forms.MenuItem mnuPbMainContextArrangeBack;
    private System.Windows.Forms.ToolBarButton tbtnSendToBack;
    private System.Windows.Forms.ToolBarButton tbtnSendBackward;
    private System.Windows.Forms.ToolBarButton tbtnBringForward;
    private System.Windows.Forms.ToolBarButton tbtnBringToFront;

    #endregion
    private System.Windows.Forms.ToolBarButton tbtnProjectProperties;
    private System.Windows.Forms.MenuItem mnuFileProjectProperties;
    private System.Windows.Forms.MenuItem mnuPbMainContextAlign;
    private System.Windows.Forms.MenuItem mnuPbMainContextAlignLeft;
    private System.Windows.Forms.MenuItem nuPbMainContextAlignCenter;
    private System.Windows.Forms.MenuItem mnuPbMainContextAlignRight;
    private System.Windows.Forms.ToolBarButton tbbtnBorderWidth;
    private System.Windows.Forms.ToolBarButton tbbtnBorderColor;
    private System.Windows.Forms.ContextMenu mnuBorderWidth;
    private System.Windows.Forms.MenuItem mnuBorderWidth0;
    private System.Windows.Forms.MenuItem mnuBorderWidth1;
    private System.Windows.Forms.MenuItem mnuBorderWidth2;
    private System.Windows.Forms.MenuItem mnuBorderWidth3;
    private System.Windows.Forms.MenuItem mnuBorderWidth4;
    private System.Windows.Forms.MenuItem mnuBorderWidth5;
    private System.Windows.Forms.MenuItem mnuBorderWidth6;
    private System.Windows.Forms.MenuItem mnuBorderWidth7;
    private System.Windows.Forms.MenuItem mnuBorderWidth8;
    private System.Windows.Forms.MenuItem mnuBorderWidth9;
    private System.Windows.Forms.MenuItem mnuBorderWidth10;
    private System.Windows.Forms.MenuItem mnuBorderWidth15;
    private System.Windows.Forms.MenuItem mnuPbMainContextNoFill;
    private MenuItem mnuFileNew;
    private MenuItem mnuFileOpen;
    private MenuItem mnuFileSave;
    private MenuItem mnuFileSaveAs;
    private MenuItem menuItem5;
    private MenuItem mnuPbMainContextLock;
    private MenuItem mnuPbMainContextUnlock;
    private ToolBarButton toolBarDiplomaPicture;
    private ContextMenu mnuAddShape;
    private MenuItem mnuAddShapeRectangle;
    private MenuItem mnuAddShapeEllipse;
    private Label lblName;
    private Label lblNameVal;
    private MenuItem mnuPbMainContextProperties;
    private MenuItem mnuAddShapeDiplomaPicture;
    private MenuItem mnuPbMainContextText;
    private MenuItem mnuPbMainContextTextUpper;
    private MenuItem mnuPbMainContextTextLower;
    private MenuItem mnuPbMainContextTextNormal;
    private Button btnTestPrint;
    private Label lblBlanks;
    private NumericUpDown numBlanks;
    private CheckBox ckOmitAll;
    private MenuItem mnuChildren;
    private MenuItem mnuChildrenList;
    private Label lblDrawingObjects;
    private ListBox lbDrawingObjects;
    private ContextMenuStrip ctxMnuObjectList;
    private ToolStripMenuItem ctxMnuObjectListProperties;
    private CheckBox ckLabels;
    private ToolStripMenuItem ctxMnuObjectListDelete;
    private ToolStripMenuItem ctxMnuObjectLock;
    private ToolStripMenuItem ctxMnuObjectListUnlock;
    private MenuItem mnuOptions;
    private MenuItem mnuObjectsUnlockAll;
    private IContainer components;

    public frmMain()
    {
      InitializeComponent();
      LogHelper.Log("Program started");
      InitializeApp();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
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
      this.mnuMain = new System.Windows.Forms.MainMenu(this.components);
      this.mnuFile = new System.Windows.Forms.MenuItem();
      this.mnuFileNew = new System.Windows.Forms.MenuItem();
      this.mnuFileOpen = new System.Windows.Forms.MenuItem();
      this.mnuFileSave = new System.Windows.Forms.MenuItem();
      this.mnuFileSaveAs = new System.Windows.Forms.MenuItem();
      this.mnuFileProjectProperties = new System.Windows.Forms.MenuItem();
      this.menuItem5 = new System.Windows.Forms.MenuItem();
      this.mnuFileExit = new System.Windows.Forms.MenuItem();
      this.mnuView = new System.Windows.Forms.MenuItem();
      this.mnuChildren = new System.Windows.Forms.MenuItem();
      this.mnuChildrenList = new System.Windows.Forms.MenuItem();
      this.pnlToolBar = new System.Windows.Forms.Panel();
      this.ckOmitAll = new System.Windows.Forms.CheckBox();
      this.lblBlanks = new System.Windows.Forms.Label();
      this.numBlanks = new System.Windows.Forms.NumericUpDown();
      this.btnTestPrint = new System.Windows.Forms.Button();
      this.pnlDesignTools = new System.Windows.Forms.Panel();
      this.tbDesign = new System.Windows.Forms.ToolBar();
      this.tbtnProjectProperties = new System.Windows.Forms.ToolBarButton();
      this.tbtnOpen = new System.Windows.Forms.ToolBarButton();
      this.tbtnSave = new System.Windows.Forms.ToolBarButton();
      this.tbtnDesignMode = new System.Windows.Forms.ToolBarButton();
      this.tbtnPrintMode = new System.Windows.Forms.ToolBarButton();
      this.tbtnPrevPage = new System.Windows.Forms.ToolBarButton();
      this.tbtnNextPage = new System.Windows.Forms.ToolBarButton();
      this.tbtnEditNames = new System.Windows.Forms.ToolBarButton();
      this.tbtnPrintTags = new System.Windows.Forms.ToolBarButton();
      this.tbtnSep1 = new System.Windows.Forms.ToolBarButton();
      this.tbtnText = new System.Windows.Forms.ToolBarButton();
      this.tbtnPicture = new System.Windows.Forms.ToolBarButton();
      this.tbbtnRectangle = new System.Windows.Forms.ToolBarButton();
      this.tbbtnBorderWidth = new System.Windows.Forms.ToolBarButton();
      this.tbbtnBorderColor = new System.Windows.Forms.ToolBarButton();
      this.tbbtnFont = new System.Windows.Forms.ToolBarButton();
      this.tbbtnColor = new System.Windows.Forms.ToolBarButton();
      this.tbtnZoomIn = new System.Windows.Forms.ToolBarButton();
      this.tbtnZoomOut = new System.Windows.Forms.ToolBarButton();
      this.tbtnSendToBack = new System.Windows.Forms.ToolBarButton();
      this.tbtnSendBackward = new System.Windows.Forms.ToolBarButton();
      this.tbtnBringForward = new System.Windows.Forms.ToolBarButton();
      this.tbtnBringToFront = new System.Windows.Forms.ToolBarButton();
      this.toolBarDiplomaPicture = new System.Windows.Forms.ToolBarButton();
      this.pnlStatusBar = new System.Windows.Forms.Panel();
      this.lblSelectedKey = new System.Windows.Forms.Label();
      this.lblTagCount = new System.Windows.Forms.Label();
      this.lblScale = new System.Windows.Forms.Label();
      this.pnlLeft = new System.Windows.Forms.Panel();
      this.lblDrawingObjects = new System.Windows.Forms.Label();
      this.lbDrawingObjects = new System.Windows.Forms.ListBox();
      this.lblMousePos = new System.Windows.Forms.Label();
      this.lblMousePosVal = new System.Windows.Forms.Label();
      this.lblTagSizeVal = new System.Windows.Forms.Label();
      this.lblTagSize = new System.Windows.Forms.Label();
      this.lblObjPosVal = new System.Windows.Forms.Label();
      this.lblObjPos = new System.Windows.Forms.Label();
      this.lblObjSize = new System.Windows.Forms.Label();
      this.lblObjSizeVal = new System.Windows.Forms.Label();
      this.lblScalePos = new System.Windows.Forms.Label();
      this.lblScalePosVal = new System.Windows.Forms.Label();
      this.lblName = new System.Windows.Forms.Label();
      this.lblScaleSize = new System.Windows.Forms.Label();
      this.lblNameVal = new System.Windows.Forms.Label();
      this.lblScaleSizeVal = new System.Windows.Forms.Label();
      this.lblMouseMoveVal = new System.Windows.Forms.Label();
      this.lblMouseMove = new System.Windows.Forms.Label();
      this.splitMain = new System.Windows.Forms.Splitter();
      this.pnlRight = new System.Windows.Forms.Panel();
      this.pbMain = new System.Windows.Forms.PictureBox();
      this.mnuPbMainContext = new System.Windows.Forms.ContextMenu();
      this.mnuPbMainContextArrange = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextArrangeFront = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextArrangeForward = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextArrangeBackward = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextArrangeBack = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextAlign = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextAlignLeft = new System.Windows.Forms.MenuItem();
      this.nuPbMainContextAlignCenter = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextAlignRight = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextNoFill = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextLock = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextUnlock = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextProperties = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextText = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextTextUpper = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextTextLower = new System.Windows.Forms.MenuItem();
      this.mnuPbMainContextTextNormal = new System.Windows.Forms.MenuItem();
      this.pnlShadow = new System.Windows.Forms.Panel();
      this.dlgFont = new System.Windows.Forms.FontDialog();
      this.dlgColor = new System.Windows.Forms.ColorDialog();
      this.dlgFile = new System.Windows.Forms.OpenFileDialog();
      this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
      this.mnuBorderWidth = new System.Windows.Forms.ContextMenu();
      this.mnuBorderWidth0 = new System.Windows.Forms.MenuItem();
      this.mnuBorderWidth1 = new System.Windows.Forms.MenuItem();
      this.mnuBorderWidth2 = new System.Windows.Forms.MenuItem();
      this.mnuBorderWidth3 = new System.Windows.Forms.MenuItem();
      this.mnuBorderWidth4 = new System.Windows.Forms.MenuItem();
      this.mnuBorderWidth5 = new System.Windows.Forms.MenuItem();
      this.mnuBorderWidth6 = new System.Windows.Forms.MenuItem();
      this.mnuBorderWidth7 = new System.Windows.Forms.MenuItem();
      this.mnuBorderWidth8 = new System.Windows.Forms.MenuItem();
      this.mnuBorderWidth9 = new System.Windows.Forms.MenuItem();
      this.mnuBorderWidth10 = new System.Windows.Forms.MenuItem();
      this.mnuBorderWidth15 = new System.Windows.Forms.MenuItem();
      this.mnuAddShape = new System.Windows.Forms.ContextMenu();
      this.mnuAddShapeRectangle = new System.Windows.Forms.MenuItem();
      this.mnuAddShapeEllipse = new System.Windows.Forms.MenuItem();
      this.mnuAddShapeDiplomaPicture = new System.Windows.Forms.MenuItem();
      this.ctxMnuObjectList = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMnuObjectListProperties = new System.Windows.Forms.ToolStripMenuItem();
      this.ckLabels = new System.Windows.Forms.CheckBox();
      this.ctxMnuObjectListDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuObjectLock = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMnuObjectListUnlock = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptions = new System.Windows.Forms.MenuItem();
      this.mnuObjectsUnlockAll = new System.Windows.Forms.MenuItem();
      this.pnlToolBar.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numBlanks)).BeginInit();
      this.pnlDesignTools.SuspendLayout();
      this.pnlStatusBar.SuspendLayout();
      this.pnlLeft.SuspendLayout();
      this.pnlRight.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
      this.ctxMnuObjectList.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
        this.mnuFile,
        this.mnuView,
        this.mnuChildren,
        this.mnuOptions
      });
      //
      // mnuFile
      //
      this.mnuFile.Index = 0;
      this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
        this.mnuFileNew,
        this.mnuFileOpen,
        this.mnuFileSave,
        this.mnuFileSaveAs,
        this.mnuFileProjectProperties,
        this.menuItem5,
        this.mnuFileExit
      });
      this.mnuFile.Text = "&File";
      //
      // mnuFileNew
      //
      this.mnuFileNew.Index = 0;
      this.mnuFileNew.Tag = "NEW_PROJECT";
      this.mnuFileNew.Text = "&New";
      this.mnuFileNew.Click += new System.EventHandler(this.mnuFileActions_Click);
      //
      // mnuFileOpen
      //
      this.mnuFileOpen.Index = 1;
      this.mnuFileOpen.Tag = "OPEN_PROJECT";
      this.mnuFileOpen.Text = "&Open";
      this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileActions_Click);
      //
      // mnuFileSave
      //
      this.mnuFileSave.Index = 2;
      this.mnuFileSave.Tag = "SAVE_PROJECT";
      this.mnuFileSave.Text = "&Save";
      this.mnuFileSave.Click += new System.EventHandler(this.mnuFileActions_Click);
      //
      // mnuFileSaveAs
      //
      this.mnuFileSaveAs.Index = 3;
      this.mnuFileSaveAs.Tag = "SAVE_PROJECT_AS";
      this.mnuFileSaveAs.Text = "Save &As";
      this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileActions_Click);
      //
      // mnuFileProjectProperties
      //
      this.mnuFileProjectProperties.Index = 4;
      this.mnuFileProjectProperties.Tag = "PROJECT_PROPERTIES";
      this.mnuFileProjectProperties.Text = "&Project Properties";
      this.mnuFileProjectProperties.Click += new System.EventHandler(this.mnuFileActions_Click);
      //
      // menuItem5
      //
      this.menuItem5.Index = 5;
      this.menuItem5.Tag = "CLOSE_PROJECT";
      this.menuItem5.Text = "&Close";
      this.menuItem5.Click += new System.EventHandler(this.mnuFileActions_Click);
      //
      // mnuFileExit
      //
      this.mnuFileExit.Index = 6;
      this.mnuFileExit.Tag = "EXIT_PROGRAM";
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.mnuFileActions_Click);
      //
      // mnuView
      //
      this.mnuView.Index = 1;
      this.mnuView.Text = "&View";
      //
      // mnuChildren
      //
      this.mnuChildren.Index = 2;
      this.mnuChildren.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
        this.mnuChildrenList
      });
      this.mnuChildren.Text = "Children";
      //
      // mnuChildrenList
      //
      this.mnuChildrenList.Index = 0;
      this.mnuChildrenList.Tag = "ChildrenList";
      this.mnuChildrenList.Text = "Children List";
      this.mnuChildrenList.Click += new System.EventHandler(this.Action);
      //
      // pnlToolBar
      //
      this.pnlToolBar.Controls.Add(this.ckLabels);
      this.pnlToolBar.Controls.Add(this.ckOmitAll);
      this.pnlToolBar.Controls.Add(this.lblBlanks);
      this.pnlToolBar.Controls.Add(this.numBlanks);
      this.pnlToolBar.Controls.Add(this.btnTestPrint);
      this.pnlToolBar.Controls.Add(this.pnlDesignTools);
      this.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlToolBar.Location = new System.Drawing.Point(4, 4);
      this.pnlToolBar.Name = "pnlToolBar";
      this.pnlToolBar.Size = new System.Drawing.Size(1494, 42);
      this.pnlToolBar.TabIndex = 0;
      //
      // ckOmitAll
      //
      this.ckOmitAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ckOmitAll.AutoSize = true;
      this.ckOmitAll.Location = new System.Drawing.Point(1225, 13);
      this.ckOmitAll.Name = "ckOmitAll";
      this.ckOmitAll.Size = new System.Drawing.Size(61, 17);
      this.ckOmitAll.TabIndex = 5;
      this.ckOmitAll.Text = "Omit All";
      this.ckOmitAll.UseVisualStyleBackColor = true;
      //
      // lblBlanks
      //
      this.lblBlanks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblBlanks.AutoSize = true;
      this.lblBlanks.Location = new System.Drawing.Point(1443, 14);
      this.lblBlanks.Name = "lblBlanks";
      this.lblBlanks.Size = new System.Drawing.Size(39, 13);
      this.lblBlanks.TabIndex = 4;
      this.lblBlanks.Text = "Blanks";
      //
      // numBlanks
      //
      this.numBlanks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.numBlanks.Location = new System.Drawing.Point(1396, 11);
      this.numBlanks.Maximum = new decimal(new int[] {
        10,
        0,
        0,
        0
      });
      this.numBlanks.Name = "numBlanks";
      this.numBlanks.Size = new System.Drawing.Size(43, 20);
      this.numBlanks.TabIndex = 3;
      this.numBlanks.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      //
      // btnTestPrint
      //
      this.btnTestPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnTestPrint.Location = new System.Drawing.Point(1297, 6);
      this.btnTestPrint.Name = "btnTestPrint";
      this.btnTestPrint.Size = new System.Drawing.Size(93, 28);
      this.btnTestPrint.TabIndex = 2;
      this.btnTestPrint.Text = "Test Print";
      this.btnTestPrint.UseVisualStyleBackColor = true;
      this.btnTestPrint.Click += new System.EventHandler(this.btnTestPrint_Click);
      //
      // pnlDesignTools
      //
      this.pnlDesignTools.Controls.Add(this.tbDesign);
      this.pnlDesignTools.Location = new System.Drawing.Point(8, 0);
      this.pnlDesignTools.Name = "pnlDesignTools";
      this.pnlDesignTools.Size = new System.Drawing.Size(1055, 38);
      this.pnlDesignTools.TabIndex = 1;
      //
      // tbDesign
      //
      this.tbDesign.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
      this.tbDesign.AutoSize = false;
      this.tbDesign.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
        this.tbtnProjectProperties,
        this.tbtnOpen,
        this.tbtnSave,
        this.tbtnDesignMode,
        this.tbtnPrintMode,
        this.tbtnPrevPage,
        this.tbtnNextPage,
        this.tbtnEditNames,
        this.tbtnPrintTags,
        this.tbtnSep1,
        this.tbtnText,
        this.tbtnPicture,
        this.tbbtnRectangle,
        this.tbbtnBorderWidth,
        this.tbbtnBorderColor,
        this.tbbtnFont,
        this.tbbtnColor,
        this.tbtnZoomIn,
        this.tbtnZoomOut,
        this.tbtnSendToBack,
        this.tbtnSendBackward,
        this.tbtnBringForward,
        this.tbtnBringToFront,
        this.toolBarDiplomaPicture
      });
      this.tbDesign.ButtonSize = new System.Drawing.Size(32, 32);
      this.tbDesign.CausesValidation = false;
      this.tbDesign.Dock = System.Windows.Forms.DockStyle.None;
      this.tbDesign.DropDownArrows = true;
      this.tbDesign.Location = new System.Drawing.Point(0, -2);
      this.tbDesign.Name = "tbDesign";
      this.tbDesign.ShowToolTips = true;
      this.tbDesign.Size = new System.Drawing.Size(1037, 46);
      this.tbDesign.TabIndex = 0;
      this.tbDesign.Wrappable = false;
      this.tbDesign.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbDesign_ButtonClick);
      //
      // tbtnProjectProperties
      //
      this.tbtnProjectProperties.Name = "tbtnProjectProperties";
      this.tbtnProjectProperties.Tag = "0";
      this.tbtnProjectProperties.ToolTipText = "Project Properties";
      //
      // tbtnOpen
      //
      this.tbtnOpen.Name = "tbtnOpen";
      this.tbtnOpen.Tag = "1";
      this.tbtnOpen.ToolTipText = "Open project";
      //
      // tbtnSave
      //
      this.tbtnSave.Name = "tbtnSave";
      this.tbtnSave.Tag = "2";
      this.tbtnSave.ToolTipText = "Save project";
      //
      // tbtnDesignMode
      //
      this.tbtnDesignMode.Name = "tbtnDesignMode";
      this.tbtnDesignMode.Tag = "3";
      this.tbtnDesignMode.ToolTipText = "Design mode";
      //
      // tbtnPrintMode
      //
      this.tbtnPrintMode.Name = "tbtnPrintMode";
      this.tbtnPrintMode.Tag = "4";
      this.tbtnPrintMode.ToolTipText = "Print mode";
      //
      // tbtnPrevPage
      //
      this.tbtnPrevPage.Name = "tbtnPrevPage";
      this.tbtnPrevPage.Tag = "5";
      this.tbtnPrevPage.ToolTipText = "Prev Page";
      //
      // tbtnNextPage
      //
      this.tbtnNextPage.Name = "tbtnNextPage";
      this.tbtnNextPage.Tag = "6";
      this.tbtnNextPage.ToolTipText = "Next Page";
      //
      // tbtnEditNames
      //
      this.tbtnEditNames.Name = "tbtnEditNames";
      this.tbtnEditNames.Tag = "7";
      this.tbtnEditNames.ToolTipText = "Edit names";
      //
      // tbtnPrintTags
      //
      this.tbtnPrintTags.Name = "tbtnPrintTags";
      this.tbtnPrintTags.Tag = "8";
      this.tbtnPrintTags.ToolTipText = "Print";
      //
      // tbtnSep1
      //
      this.tbtnSep1.Name = "tbtnSep1";
      this.tbtnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
      this.tbtnSep1.Tag = "9";
      //
      // tbtnText
      //
      this.tbtnText.Name = "tbtnText";
      this.tbtnText.Tag = "10";
      this.tbtnText.ToolTipText = "Add text";
      //
      // tbtnPicture
      //
      this.tbtnPicture.Name = "tbtnPicture";
      this.tbtnPicture.Tag = "11";
      this.tbtnPicture.ToolTipText = "Add a picture";
      //
      // tbbtnRectangle
      //
      this.tbbtnRectangle.Name = "tbbtnRectangle";
      this.tbbtnRectangle.Tag = "12";
      this.tbbtnRectangle.ToolTipText = "Rectangle shape";
      //
      // tbbtnBorderWidth
      //
      this.tbbtnBorderWidth.Name = "tbbtnBorderWidth";
      this.tbbtnBorderWidth.Tag = "13";
      this.tbbtnBorderWidth.ToolTipText = "Border Width";
      //
      // tbbtnBorderColor
      //
      this.tbbtnBorderColor.Name = "tbbtnBorderColor";
      this.tbbtnBorderColor.Tag = "14";
      this.tbbtnBorderColor.ToolTipText = "Border Color";
      //
      // tbbtnFont
      //
      this.tbbtnFont.Name = "tbbtnFont";
      this.tbbtnFont.Tag = "15";
      this.tbbtnFont.ToolTipText = "Text font";
      //
      // tbbtnColor
      //
      this.tbbtnColor.Name = "tbbtnColor";
      this.tbbtnColor.Tag = "16";
      this.tbbtnColor.ToolTipText = "Color";
      //
      // tbtnZoomIn
      //
      this.tbtnZoomIn.Name = "tbtnZoomIn";
      this.tbtnZoomIn.Tag = "17";
      this.tbtnZoomIn.ToolTipText = "Zoom in";
      //
      // tbtnZoomOut
      //
      this.tbtnZoomOut.Name = "tbtnZoomOut";
      this.tbtnZoomOut.Tag = "18";
      this.tbtnZoomOut.ToolTipText = "Zoom Out";
      //
      // tbtnSendToBack
      //
      this.tbtnSendToBack.Name = "tbtnSendToBack";
      this.tbtnSendToBack.Tag = "19";
      this.tbtnSendToBack.ToolTipText = "Send to back";
      //
      // tbtnSendBackward
      //
      this.tbtnSendBackward.Name = "tbtnSendBackward";
      this.tbtnSendBackward.Tag = "20";
      this.tbtnSendBackward.ToolTipText = "Send backward";
      //
      // tbtnBringForward
      //
      this.tbtnBringForward.Name = "tbtnBringForward";
      this.tbtnBringForward.Tag = "21";
      this.tbtnBringForward.ToolTipText = "Bring forward";
      //
      // tbtnBringToFront
      //
      this.tbtnBringToFront.Name = "tbtnBringToFront";
      this.tbtnBringToFront.Tag = "22";
      this.tbtnBringToFront.ToolTipText = "Bring to front";
      //
      // toolBarDiplomaPicture
      //
      this.toolBarDiplomaPicture.Name = "toolBarDiplomaPicture";
      this.toolBarDiplomaPicture.Tag = "23";
      //
      // pnlStatusBar
      //
      this.pnlStatusBar.Controls.Add(this.lblSelectedKey);
      this.pnlStatusBar.Controls.Add(this.lblTagCount);
      this.pnlStatusBar.Controls.Add(this.lblScale);
      this.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlStatusBar.Location = new System.Drawing.Point(4, 797);
      this.pnlStatusBar.Name = "pnlStatusBar";
      this.pnlStatusBar.Size = new System.Drawing.Size(1494, 32);
      this.pnlStatusBar.TabIndex = 1;
      //
      // lblSelectedKey
      //
      this.lblSelectedKey.Location = new System.Drawing.Point(16, 8);
      this.lblSelectedKey.Name = "lblSelectedKey";
      this.lblSelectedKey.Size = new System.Drawing.Size(144, 16);
      this.lblSelectedKey.TabIndex = 4;
      this.lblSelectedKey.Text = "-1";
      //
      // lblTagCount
      //
      this.lblTagCount.Location = new System.Drawing.Point(176, 8);
      this.lblTagCount.Name = "lblTagCount";
      this.lblTagCount.Size = new System.Drawing.Size(360, 16);
      this.lblTagCount.TabIndex = 3;
      //
      // lblScale
      //
      this.lblScale.Location = new System.Drawing.Point(608, 8);
      this.lblScale.Name = "lblScale";
      this.lblScale.Size = new System.Drawing.Size(48, 16);
      this.lblScale.TabIndex = 2;
      this.lblScale.Text = "100%";
      this.lblScale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // pnlLeft
      //
      this.pnlLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pnlLeft.Controls.Add(this.lblDrawingObjects);
      this.pnlLeft.Controls.Add(this.lbDrawingObjects);
      this.pnlLeft.Controls.Add(this.lblMousePos);
      this.pnlLeft.Controls.Add(this.lblMousePosVal);
      this.pnlLeft.Controls.Add(this.lblTagSizeVal);
      this.pnlLeft.Controls.Add(this.lblTagSize);
      this.pnlLeft.Controls.Add(this.lblObjPosVal);
      this.pnlLeft.Controls.Add(this.lblObjPos);
      this.pnlLeft.Controls.Add(this.lblObjSize);
      this.pnlLeft.Controls.Add(this.lblObjSizeVal);
      this.pnlLeft.Controls.Add(this.lblScalePos);
      this.pnlLeft.Controls.Add(this.lblScalePosVal);
      this.pnlLeft.Controls.Add(this.lblName);
      this.pnlLeft.Controls.Add(this.lblScaleSize);
      this.pnlLeft.Controls.Add(this.lblNameVal);
      this.pnlLeft.Controls.Add(this.lblScaleSizeVal);
      this.pnlLeft.Controls.Add(this.lblMouseMoveVal);
      this.pnlLeft.Controls.Add(this.lblMouseMove);
      this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
      this.pnlLeft.Location = new System.Drawing.Point(4, 46);
      this.pnlLeft.Name = "pnlLeft";
      this.pnlLeft.Size = new System.Drawing.Size(231, 751);
      this.pnlLeft.TabIndex = 2;
      //
      // lblDrawingObjects
      //
      this.lblDrawingObjects.AutoSize = true;
      this.lblDrawingObjects.Location = new System.Drawing.Point(11, 258);
      this.lblDrawingObjects.Name = "lblDrawingObjects";
      this.lblDrawingObjects.Size = new System.Drawing.Size(85, 13);
      this.lblDrawingObjects.TabIndex = 2;
      this.lblDrawingObjects.Text = "Drawing Objects";
      //
      // lbDrawingObjects
      //
      this.lbDrawingObjects.ContextMenuStrip = this.ctxMnuObjectList;
      this.lbDrawingObjects.FormattingEnabled = true;
      this.lbDrawingObjects.Location = new System.Drawing.Point(11, 277);
      this.lbDrawingObjects.Name = "lbDrawingObjects";
      this.lbDrawingObjects.Size = new System.Drawing.Size(205, 355);
      this.lbDrawingObjects.TabIndex = 1;
      this.lbDrawingObjects.SelectedIndexChanged += new System.EventHandler(this.lbDrawingObjects_SelectedIndexChanged);
      //
      // lblMousePos
      //
      this.lblMousePos.Location = new System.Drawing.Point(8, 40);
      this.lblMousePos.Name = "lblMousePos";
      this.lblMousePos.Size = new System.Drawing.Size(80, 16);
      this.lblMousePos.TabIndex = 0;
      this.lblMousePos.Text = "Mouse (x,y):";
      //
      // lblMousePosVal
      //
      this.lblMousePosVal.Location = new System.Drawing.Point(96, 40);
      this.lblMousePosVal.Name = "lblMousePosVal";
      this.lblMousePosVal.Size = new System.Drawing.Size(88, 16);
      this.lblMousePosVal.TabIndex = 0;
      this.lblMousePosVal.Text = "0,0";
      //
      // lblTagSizeVal
      //
      this.lblTagSizeVal.Location = new System.Drawing.Point(96, 24);
      this.lblTagSizeVal.Name = "lblTagSizeVal";
      this.lblTagSizeVal.Size = new System.Drawing.Size(88, 16);
      this.lblTagSizeVal.TabIndex = 0;
      this.lblTagSizeVal.Text = "0,0";
      //
      // lblTagSize
      //
      this.lblTagSize.Location = new System.Drawing.Point(8, 24);
      this.lblTagSize.Name = "lblTagSize";
      this.lblTagSize.Size = new System.Drawing.Size(80, 16);
      this.lblTagSize.TabIndex = 0;
      this.lblTagSize.Text = "Tag size (w,h):";
      //
      // lblObjPosVal
      //
      this.lblObjPosVal.Location = new System.Drawing.Point(96, 72);
      this.lblObjPosVal.Name = "lblObjPosVal";
      this.lblObjPosVal.Size = new System.Drawing.Size(88, 16);
      this.lblObjPosVal.TabIndex = 0;
      this.lblObjPosVal.Text = "0,0";
      //
      // lblObjPos
      //
      this.lblObjPos.Location = new System.Drawing.Point(8, 72);
      this.lblObjPos.Name = "lblObjPos";
      this.lblObjPos.Size = new System.Drawing.Size(80, 16);
      this.lblObjPos.TabIndex = 0;
      this.lblObjPos.Text = "Obj Pos (x,y):";
      //
      // lblObjSize
      //
      this.lblObjSize.Location = new System.Drawing.Point(8, 88);
      this.lblObjSize.Name = "lblObjSize";
      this.lblObjSize.Size = new System.Drawing.Size(80, 16);
      this.lblObjSize.TabIndex = 0;
      this.lblObjSize.Text = "Obj Size (w,h):";
      //
      // lblObjSizeVal
      //
      this.lblObjSizeVal.Location = new System.Drawing.Point(96, 88);
      this.lblObjSizeVal.Name = "lblObjSizeVal";
      this.lblObjSizeVal.Size = new System.Drawing.Size(88, 16);
      this.lblObjSizeVal.TabIndex = 0;
      this.lblObjSizeVal.Text = "0,0";
      //
      // lblScalePos
      //
      this.lblScalePos.Location = new System.Drawing.Point(8, 104);
      this.lblScalePos.Name = "lblScalePos";
      this.lblScalePos.Size = new System.Drawing.Size(80, 16);
      this.lblScalePos.TabIndex = 0;
      this.lblScalePos.Text = "Scl Pos (x,y):";
      //
      // lblScalePosVal
      //
      this.lblScalePosVal.Location = new System.Drawing.Point(96, 104);
      this.lblScalePosVal.Name = "lblScalePosVal";
      this.lblScalePosVal.Size = new System.Drawing.Size(88, 16);
      this.lblScalePosVal.TabIndex = 0;
      this.lblScalePosVal.Text = "0,0";
      //
      // lblName
      //
      this.lblName.Location = new System.Drawing.Point(8, 195);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(43, 16);
      this.lblName.TabIndex = 0;
      this.lblName.Text = "Name:";
      //
      // lblScaleSize
      //
      this.lblScaleSize.Location = new System.Drawing.Point(8, 120);
      this.lblScaleSize.Name = "lblScaleSize";
      this.lblScaleSize.Size = new System.Drawing.Size(80, 16);
      this.lblScaleSize.TabIndex = 0;
      this.lblScaleSize.Text = "Scl Size (w,h):";
      //
      // lblNameVal
      //
      this.lblNameVal.Location = new System.Drawing.Point(57, 195);
      this.lblNameVal.Name = "lblNameVal";
      this.lblNameVal.Size = new System.Drawing.Size(159, 16);
      this.lblNameVal.TabIndex = 0;
      this.lblNameVal.Text = "<name>";
      //
      // lblScaleSizeVal
      //
      this.lblScaleSizeVal.Location = new System.Drawing.Point(96, 120);
      this.lblScaleSizeVal.Name = "lblScaleSizeVal";
      this.lblScaleSizeVal.Size = new System.Drawing.Size(88, 16);
      this.lblScaleSizeVal.TabIndex = 0;
      this.lblScaleSizeVal.Text = "0,0";
      //
      // lblMouseMoveVal
      //
      this.lblMouseMoveVal.Location = new System.Drawing.Point(96, 56);
      this.lblMouseMoveVal.Name = "lblMouseMoveVal";
      this.lblMouseMoveVal.Size = new System.Drawing.Size(88, 16);
      this.lblMouseMoveVal.TabIndex = 0;
      this.lblMouseMoveVal.Text = "0,0";
      //
      // lblMouseMove
      //
      this.lblMouseMove.Location = new System.Drawing.Point(8, 56);
      this.lblMouseMove.Name = "lblMouseMove";
      this.lblMouseMove.Size = new System.Drawing.Size(80, 16);
      this.lblMouseMove.TabIndex = 0;
      this.lblMouseMove.Text = "Move (x,y):";
      //
      // splitMain
      //
      this.splitMain.Location = new System.Drawing.Point(235, 46);
      this.splitMain.Name = "splitMain";
      this.splitMain.Size = new System.Drawing.Size(3, 751);
      this.splitMain.TabIndex = 3;
      this.splitMain.TabStop = false;
      //
      // pnlRight
      //
      this.pnlRight.AutoScroll = true;
      this.pnlRight.BackColor = System.Drawing.Color.LightBlue;
      this.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pnlRight.Controls.Add(this.pbMain);
      this.pnlRight.Controls.Add(this.pnlShadow);
      this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlRight.Location = new System.Drawing.Point(238, 46);
      this.pnlRight.Name = "pnlRight";
      this.pnlRight.Size = new System.Drawing.Size(1260, 751);
      this.pnlRight.TabIndex = 4;
      this.pnlRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlRight_MouseUp);
      //
      // pbMain
      //
      this.pbMain.BackColor = System.Drawing.Color.White;
      this.pbMain.ContextMenu = this.mnuPbMainContext;
      this.pbMain.Location = new System.Drawing.Point(8, 8);
      this.pbMain.Name = "pbMain";
      this.pbMain.Size = new System.Drawing.Size(296, 296);
      this.pbMain.TabIndex = 0;
      this.pbMain.TabStop = false;
      this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
      this.pbMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseDown);
      this.pbMain.MouseLeave += new System.EventHandler(this.pbMain_MouseLeave);
      this.pbMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseMove);
      this.pbMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseUp);
      //
      // mnuPbMainContext
      //
      this.mnuPbMainContext.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
        this.mnuPbMainContextArrange,
        this.mnuPbMainContextAlign,
        this.mnuPbMainContextNoFill,
        this.mnuPbMainContextLock,
        this.mnuPbMainContextUnlock,
        this.mnuPbMainContextProperties,
        this.mnuPbMainContextText
      });
      //
      // mnuPbMainContextArrange
      //
      this.mnuPbMainContextArrange.Index = 0;
      this.mnuPbMainContextArrange.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
        this.mnuPbMainContextArrangeFront,
        this.mnuPbMainContextArrangeForward,
        this.mnuPbMainContextArrangeBackward,
        this.mnuPbMainContextArrangeBack
      });
      this.mnuPbMainContextArrange.Text = "Arrange";
      //
      // mnuPbMainContextArrangeFront
      //
      this.mnuPbMainContextArrangeFront.Index = 0;
      this.mnuPbMainContextArrangeFront.Tag = "BRING_TO_FRONT";
      this.mnuPbMainContextArrangeFront.Text = "Bring to front";
      this.mnuPbMainContextArrangeFront.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuPbMainContextArrangeForward
      //
      this.mnuPbMainContextArrangeForward.Index = 1;
      this.mnuPbMainContextArrangeForward.Tag = "BRING_FORWARD";
      this.mnuPbMainContextArrangeForward.Text = "Bring forward";
      this.mnuPbMainContextArrangeForward.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuPbMainContextArrangeBackward
      //
      this.mnuPbMainContextArrangeBackward.Index = 2;
      this.mnuPbMainContextArrangeBackward.Tag = "SEND_BACKWARD";
      this.mnuPbMainContextArrangeBackward.Text = "Send backward";
      this.mnuPbMainContextArrangeBackward.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuPbMainContextArrangeBack
      //
      this.mnuPbMainContextArrangeBack.Index = 3;
      this.mnuPbMainContextArrangeBack.Tag = "SEND_TO_BACK";
      this.mnuPbMainContextArrangeBack.Text = "Send to back";
      this.mnuPbMainContextArrangeBack.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuPbMainContextAlign
      //
      this.mnuPbMainContextAlign.Index = 1;
      this.mnuPbMainContextAlign.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
        this.mnuPbMainContextAlignLeft,
        this.nuPbMainContextAlignCenter,
        this.mnuPbMainContextAlignRight
      });
      this.mnuPbMainContextAlign.Text = "Align";
      //
      // mnuPbMainContextAlignLeft
      //
      this.mnuPbMainContextAlignLeft.Index = 0;
      this.mnuPbMainContextAlignLeft.Tag = "ALIGN_LEFT";
      this.mnuPbMainContextAlignLeft.Text = "Left";
      this.mnuPbMainContextAlignLeft.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // nuPbMainContextAlignCenter
      //
      this.nuPbMainContextAlignCenter.Index = 1;
      this.nuPbMainContextAlignCenter.Tag = "ALIGN_CENTER";
      this.nuPbMainContextAlignCenter.Text = "Center";
      this.nuPbMainContextAlignCenter.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuPbMainContextAlignRight
      //
      this.mnuPbMainContextAlignRight.Index = 2;
      this.mnuPbMainContextAlignRight.Tag = "ALIGN_RIGHT";
      this.mnuPbMainContextAlignRight.Text = "Right";
      this.mnuPbMainContextAlignRight.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuPbMainContextNoFill
      //
      this.mnuPbMainContextNoFill.Index = 2;
      this.mnuPbMainContextNoFill.Tag = "NO_FILL";
      this.mnuPbMainContextNoFill.Text = "No Fill";
      this.mnuPbMainContextNoFill.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuPbMainContextLock
      //
      this.mnuPbMainContextLock.Index = 3;
      this.mnuPbMainContextLock.Tag = "LOCK_SHAPE";
      this.mnuPbMainContextLock.Text = "Lock";
      this.mnuPbMainContextLock.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuPbMainContextUnlock
      //
      this.mnuPbMainContextUnlock.Index = 4;
      this.mnuPbMainContextUnlock.Tag = "UNLOCK_SHAPE";
      this.mnuPbMainContextUnlock.Text = "Unlock";
      this.mnuPbMainContextUnlock.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuPbMainContextProperties
      //
      this.mnuPbMainContextProperties.Index = 5;
      this.mnuPbMainContextProperties.Text = "Properties";
      this.mnuPbMainContextProperties.Click += new System.EventHandler(this.mnuPbMainContextProperties_Click);
      //
      // mnuPbMainContextText
      //
      this.mnuPbMainContextText.Index = 6;
      this.mnuPbMainContextText.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
        this.mnuPbMainContextTextUpper,
        this.mnuPbMainContextTextLower,
        this.mnuPbMainContextTextNormal
      });
      this.mnuPbMainContextText.Text = "Text";
      //
      // mnuPbMainContextTextUpper
      //
      this.mnuPbMainContextTextUpper.Index = 0;
      this.mnuPbMainContextTextUpper.Tag = "TEXT_UPPERCASE";
      this.mnuPbMainContextTextUpper.Text = "Use Upper Case";
      this.mnuPbMainContextTextUpper.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuPbMainContextTextLower
      //
      this.mnuPbMainContextTextLower.Index = 1;
      this.mnuPbMainContextTextLower.Tag = "TEXT_LOWERCASE";
      this.mnuPbMainContextTextLower.Text = "Use Lower Case";
      this.mnuPbMainContextTextLower.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuPbMainContextTextNormal
      //
      this.mnuPbMainContextTextNormal.Index = 2;
      this.mnuPbMainContextTextNormal.Tag = "TEXT_NORMALCASE";
      this.mnuPbMainContextTextNormal.Text = "Use Normal Case";
      this.mnuPbMainContextTextNormal.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // pnlShadow
      //
      this.pnlShadow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.pnlShadow.ForeColor = System.Drawing.Color.Black;
      this.pnlShadow.Location = new System.Drawing.Point(12, 12);
      this.pnlShadow.Name = "pnlShadow";
      this.pnlShadow.Size = new System.Drawing.Size(300, 300);
      this.pnlShadow.TabIndex = 0;
      //
      // dlgFileSave
      //
      this.dlgFileSave.DefaultExt = "tag";
      //
      // mnuBorderWidth
      //
      this.mnuBorderWidth.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
        this.mnuBorderWidth0,
        this.mnuBorderWidth1,
        this.mnuBorderWidth2,
        this.mnuBorderWidth3,
        this.mnuBorderWidth4,
        this.mnuBorderWidth5,
        this.mnuBorderWidth6,
        this.mnuBorderWidth7,
        this.mnuBorderWidth8,
        this.mnuBorderWidth9,
        this.mnuBorderWidth10,
        this.mnuBorderWidth15
      });
      //
      // mnuBorderWidth0
      //
      this.mnuBorderWidth0.Index = 0;
      this.mnuBorderWidth0.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth0.Text = "0 pixels";
      this.mnuBorderWidth0.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuBorderWidth1
      //
      this.mnuBorderWidth1.Index = 1;
      this.mnuBorderWidth1.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth1.Text = "1 pixel";
      this.mnuBorderWidth1.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuBorderWidth2
      //
      this.mnuBorderWidth2.Index = 2;
      this.mnuBorderWidth2.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth2.Text = "2 pixels";
      this.mnuBorderWidth2.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuBorderWidth3
      //
      this.mnuBorderWidth3.Index = 3;
      this.mnuBorderWidth3.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth3.Text = "3 pixels";
      this.mnuBorderWidth3.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuBorderWidth4
      //
      this.mnuBorderWidth4.Index = 4;
      this.mnuBorderWidth4.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth4.Text = "4 pixels";
      this.mnuBorderWidth4.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuBorderWidth5
      //
      this.mnuBorderWidth5.Index = 5;
      this.mnuBorderWidth5.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth5.Text = "5 pixels";
      this.mnuBorderWidth5.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuBorderWidth6
      //
      this.mnuBorderWidth6.Index = 6;
      this.mnuBorderWidth6.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth6.Text = "6 pixels";
      this.mnuBorderWidth6.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuBorderWidth7
      //
      this.mnuBorderWidth7.Index = 7;
      this.mnuBorderWidth7.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth7.Text = "7 pixels";
      this.mnuBorderWidth7.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuBorderWidth8
      //
      this.mnuBorderWidth8.Index = 8;
      this.mnuBorderWidth8.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth8.Text = "8 pixels";
      this.mnuBorderWidth8.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuBorderWidth9
      //
      this.mnuBorderWidth9.Index = 9;
      this.mnuBorderWidth9.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth9.Text = "9 pixels";
      this.mnuBorderWidth9.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuBorderWidth10
      //
      this.mnuBorderWidth10.Index = 10;
      this.mnuBorderWidth10.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth10.Text = "10 pixels";
      this.mnuBorderWidth10.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuBorderWidth15
      //
      this.mnuBorderWidth15.Index = 11;
      this.mnuBorderWidth15.Tag = "BORDER_WIDTH";
      this.mnuBorderWidth15.Text = "15 pixels";
      this.mnuBorderWidth15.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuAddShape
      //
      this.mnuAddShape.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
        this.mnuAddShapeRectangle,
        this.mnuAddShapeEllipse,
        this.mnuAddShapeDiplomaPicture
      });
      //
      // mnuAddShapeRectangle
      //
      this.mnuAddShapeRectangle.Index = 0;
      this.mnuAddShapeRectangle.Tag = "ADD_SHAPE_RECTANGLE";
      this.mnuAddShapeRectangle.Text = "Add Rectangle";
      this.mnuAddShapeRectangle.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuAddShapeEllipse
      //
      this.mnuAddShapeEllipse.Index = 1;
      this.mnuAddShapeEllipse.Tag = "ADD_SHAPE_ELLIPSE";
      this.mnuAddShapeEllipse.Text = "Add Ellipse";
      this.mnuAddShapeEllipse.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // mnuAddShapeDiplomaPicture
      //
      this.mnuAddShapeDiplomaPicture.Index = 2;
      this.mnuAddShapeDiplomaPicture.Tag = "ADD_SHAPE_DIPLOMA_PICTURE";
      this.mnuAddShapeDiplomaPicture.Text = "Add Diploma Picture";
      this.mnuAddShapeDiplomaPicture.Click += new System.EventHandler(this.mnuDrawingObjectAction);
      //
      // ctxMnuObjectList
      //
      this.ctxMnuObjectList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMnuObjectListProperties,
        this.ctxMnuObjectListDelete,
        this.ctxMnuObjectLock,
        this.ctxMnuObjectListUnlock
      });
      this.ctxMnuObjectList.Name = "ctxMnuObjectList";
      this.ctxMnuObjectList.Size = new System.Drawing.Size(128, 92);
      this.ctxMnuObjectList.Click += new System.EventHandler(this.ctxMnuObjectList_Click);
      //
      // ctxMnuObjectListProperties
      //
      this.ctxMnuObjectListProperties.Name = "ctxMnuObjectListProperties";
      this.ctxMnuObjectListProperties.Size = new System.Drawing.Size(127, 22);
      this.ctxMnuObjectListProperties.Tag = "ListProperties";
      this.ctxMnuObjectListProperties.Text = "Properties";
      this.ctxMnuObjectListProperties.Click += new System.EventHandler(this.Action);
      //
      // ckLabels
      //
      this.ckLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ckLabels.AutoSize = true;
      this.ckLabels.Location = new System.Drawing.Point(1162, 13);
      this.ckLabels.Name = "ckLabels";
      this.ckLabels.Size = new System.Drawing.Size(57, 17);
      this.ckLabels.TabIndex = 5;
      this.ckLabels.Text = "Labels";
      this.ckLabels.UseVisualStyleBackColor = true;
      this.ckLabels.CheckedChanged += new System.EventHandler(this.ckLabels_CheckedChanged);
      //
      // ctxMnuObjectListDelete
      //
      this.ctxMnuObjectListDelete.Name = "ctxMnuObjectListDelete";
      this.ctxMnuObjectListDelete.Size = new System.Drawing.Size(127, 22);
      this.ctxMnuObjectListDelete.Tag = "ListDelete";
      this.ctxMnuObjectListDelete.Text = "Delete";
      this.ctxMnuObjectListDelete.Click += new System.EventHandler(this.Action);
      //
      // ctxMnuObjectLock
      //
      this.ctxMnuObjectLock.Name = "ctxMnuObjectLock";
      this.ctxMnuObjectLock.Size = new System.Drawing.Size(127, 22);
      this.ctxMnuObjectLock.Tag = "ListLock";
      this.ctxMnuObjectLock.Text = "Lock";
      this.ctxMnuObjectLock.Click += new System.EventHandler(this.Action);
      //
      // ctxMnuObjectListUnlock
      //
      this.ctxMnuObjectListUnlock.Name = "ctxMnuObjectListUnlock";
      this.ctxMnuObjectListUnlock.Size = new System.Drawing.Size(127, 22);
      this.ctxMnuObjectListUnlock.Tag = "ListUnlock";
      this.ctxMnuObjectListUnlock.Text = "Unlock";
      this.ctxMnuObjectListUnlock.Click += new System.EventHandler(this.Action);
      //
      // mnuOptions
      //
      this.mnuOptions.Index = 3;
      this.mnuOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
        this.mnuObjectsUnlockAll
      });
      this.mnuOptions.Text = "Objects";
      //
      // mnuObjectsUnlockAll
      //
      this.mnuObjectsUnlockAll.Index = 0;
      this.mnuObjectsUnlockAll.Tag = "UnlockAll";
      this.mnuObjectsUnlockAll.Text = "Unlock All";
      this.mnuObjectsUnlockAll.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(1502, 833);
      this.Controls.Add(this.pnlRight);
      this.Controls.Add(this.splitMain);
      this.Controls.Add(this.pnlLeft);
      this.Controls.Add(this.pnlStatusBar);
      this.Controls.Add(this.pnlToolBar);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Menu = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Name Tag Creator - v1.0";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMain_KeyPress);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
      this.pnlToolBar.ResumeLayout(false);
      this.pnlToolBar.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numBlanks)).EndInit();
      this.pnlDesignTools.ResumeLayout(false);
      this.pnlStatusBar.ResumeLayout(false);
      this.pnlLeft.ResumeLayout(false);
      this.pnlLeft.PerformLayout();
      this.pnlRight.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
      this.ctxMnuObjectList.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion

    [STAThread]
    static void Main()
    {
      Application.Run(new frmMain());
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "ChildrenList":
          ShowChildrenList();
          break;

        case "ListProperties":
          ShowObjectPropertiesDialog();
          break;

        case "ListDelete":
          DeleteListObject();
          break;

        case "ListLock":
          LockListObject();
          break;

        case "ListUnlock":
          UnlockListObject();
          break;

        case "UnlockAll":
          UnlockAll();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    #region Toolbar Events

    private void tbDesign_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
    {
      switch (Int32.Parse(e.Button.Tag.ToString()))
      {
        case (int)Enums.Action.ProjectProperties:
          ProjectProperties();
          break;

        case (int)Enums.Action.OpenProject:
          OpenProject();
          break;

        case (int)Enums.Action.SaveProject:
          SaveProject(false);
          break;

        case (int)Enums.Action.DesignMode:
          EnterDesignMode();
          break;

        case (int)Enums.Action.PrintMode:
          EnterPrintMode();
          break;

        case (int)Enums.Action.PrevPage:
          PrevPage();
          break;

        case (int)Enums.Action.NextPage:
          NextPage();
          break;

        case (int)Enums.Action.EditNames:
          EditNames();
          break;

        case (int)Enums.Action.PrintTags:
          Print(false, (int)numBlanks.Value, ckOmitAll.Checked);
          break;

        case (int)Enums.Action.AddTextObject:
          AddTextObject();
          break;

        case (int)Enums.Action.AddPictureObject:
          AddGraphicObject();
          break;

        case (int)Enums.Action.SetBorderColor:
          SetBorderColor();
          break;

        case (int)Enums.Action.SetFont:
          SetFont();
          break;

        case (int)Enums.Action.SetFillColor:
          SetFillColor();
          break;

        case (int)Enums.Action.ZoomIn:
          ZoomIn();
          break;

        case (int)Enums.Action.ZoomOut:
          ZoomOut();
          break;

        case (int)Enums.Action.SendToBack:
          SendObjectToBack();
          break;

        case (int)Enums.Action.SendBackward:
          SendObjectBackward();
          break;

        case (int)Enums.Action.BringForward:
          BringObjectForward();
          break;

        case (int)Enums.Action.BringToFront:
          BringObjectToFront();
          break;

        case (int)Enums.Action.AddDiplomaPicture:
          AddDiplomaPicture();
          break;

        default:
          break;
      }

      LoadDrawingObjectsListBox();
    }

    private void ShowChildrenList()
    {
      frmChildren fChildren = new frmChildren(_configDbSpec);
      fChildren.ShowDialog();
    }


    #endregion

    #region Project Management (Properties, Open, Save, etc.)

    private void ProjectProperties()
    {
      frmProject fProject = new frmProject(_project);

      if (fProject.ShowDialog() == DialogResult.OK)
      {
        _project.IsDirty = true;
        this.Text = ConfigHelper.AppTitle + " - " + _project.Name;
        if (StateHelper.IsInDesignMode || _project.Persons.Count == 0)
          EnterDesignMode();
        else
          EnterPrintMode();
      }

      if (!StateHelper.IsInDesignMode)
      {
        UpdateTagsOnPage();
        pbMain.Refresh();
        LoadDrawingObjectsListBox();
      }

    }

    private void SaveProject(bool IsSaveAs)
    {
      if (_project.Name.CompareTo("NO_PROJECT") == 0)
        return;

      if (_project.Name.CompareTo("New Project") == 0 || IsSaveAs)
      {
        if (!Directory.Exists(ConfigHelper.DefaultProjectPath))
        {
          Directory.CreateDirectory(ConfigHelper.DefaultProjectPath);
        }
        dlgFileSave.InitialDirectory = ConfigHelper.DefaultProjectPath;
        dlgFileSave.DefaultExt = "tag";
        dlgFileSave.FileName = _project.Name.Trim() + ".tag";
        dlgFileSave.Filter = "tag files (*.tag)|*.tag";
        if (dlgFileSave.ShowDialog() == DialogResult.OK)
        {
          _project.FullFileName = dlgFileSave.FileName;
          SetProjectOpen();
        }
        else
          return;
      }


      Stream s;
      s = File.OpenWrite(_project.FullFileName);
      if (s != null)
      {
        IFormatter f = new BinaryFormatter();
        f.Serialize(s, _project);
        s.Close();
      }
      _project.IsDirty = false;
    }

    #endregion

    #region Add Objects to Object Collection

    private void AddTextObject()
    {
      Graphics gr = pbMain.CreateGraphics();
      DrawingObject obj = new DrawingObject();
      obj.SelectionChanged += new SelectionChangedHandler(_project.DrawingObjects.SelectedObjectsChanged);
      obj.SetDrawingObjectsDirty += new SetDrawingObjectsDirtyHandler(_project.DrawingObjects.SetDirty);
      obj.ObjType = Enums.ObjectType.TextObject;
      obj.Top = 20 + (10 * _project.ObjectCount());
      obj.Left = 20 + (10 * _project.ObjectCount());
      obj.Width = 200;
      obj.Height = 50;
      obj.Name = "object" + _project.ObjectCount().ToString().Trim();
      obj.FillColor = Color.Black;

      _project.DeselectAll();
      obj.Select();
      selectedObjectKeys = _project.AddObject(obj);
      lblSelectedKey.Text = _project.DrawingObjects.SelectedObjectKeysString;
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
      pbMain.Focus();
    }

    private void AddRectangleObject()
    {
      Graphics gr = pbMain.CreateGraphics();
      DrawingObject obj = new DrawingObject();
      obj.SelectionChanged += new SelectionChangedHandler(_project.DrawingObjects.SelectedObjectsChanged);
      obj.SetDrawingObjectsDirty += new SetDrawingObjectsDirtyHandler(_project.DrawingObjects.SetDirty);
      obj.ObjType = Enums.ObjectType.RectangleObject;
      obj.Top = 20 + (10 * _project.ObjectCount());
      obj.Left = 20 + (10 * _project.ObjectCount());
      obj.Width = 200;
      obj.Height = 50;
      obj.Name = "object" + _project.ObjectCount().ToString().Trim();
      obj.FillColor = Color.LightSkyBlue;

      _project.DeselectAll();
      obj.Select();
      selectedObjectKeys = _project.AddObject(obj);
      lblSelectedKey.Text = _project.DrawingObjects.SelectedObjectKeysString;
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
      pbMain.Focus();
    }

    private void AddEllipseObject()
    {
      Graphics gr = pbMain.CreateGraphics();
      DrawingObject obj = new DrawingObject();
      obj.SelectionChanged += new SelectionChangedHandler(_project.DrawingObjects.SelectedObjectsChanged);
      obj.SetDrawingObjectsDirty += new SetDrawingObjectsDirtyHandler(_project.DrawingObjects.SetDirty);
      obj.ObjType = Enums.ObjectType.EllipseObject;
      obj.Top = 20 + (10 * _project.ObjectCount());
      obj.Left = 20 + (10 * _project.ObjectCount());
      obj.Width = 200;
      obj.Height = 50;
      obj.Name = "object" + _project.ObjectCount().ToString().Trim();
      obj.FillColor = Color.LightSkyBlue;

      _project.DeselectAll();
      obj.Select();
      selectedObjectKeys = _project.AddObject(obj);
      lblSelectedKey.Text = _project.DrawingObjects.SelectedObjectKeysString;
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
      pbMain.Focus();
    }

    private void AddDiplomaPictureObject()
    {
      Graphics gr = pbMain.CreateGraphics();
      DrawingObject obj = new DrawingObject();
      obj.SelectionChanged += new SelectionChangedHandler(_project.DrawingObjects.SelectedObjectsChanged);
      obj.SetDrawingObjectsDirty += new SetDrawingObjectsDirtyHandler(_project.DrawingObjects.SetDirty);
      obj.ObjType = Enums.ObjectType.DiplomaPicture;
      obj.Top = 20 + (10 * _project.ObjectCount());
      obj.Left = 20 + (10 * _project.ObjectCount());
      obj.Width = 200;
      obj.Height = 50;
      obj.Name = "object" + _project.ObjectCount().ToString().Trim();
      obj.FillColor = Color.LightSkyBlue;

      _project.DeselectAll();
      obj.Select();
      selectedObjectKeys = _project.AddObject(obj);
      lblSelectedKey.Text = _project.DrawingObjects.SelectedObjectKeysString;
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
      pbMain.Focus();
    }


    private void AddDiplomaPicture()
    {
      Graphics gr = pbMain.CreateGraphics();
      DrawingObject obj = new DrawingObject();
      obj.SelectionChanged += new SelectionChangedHandler(_project.DrawingObjects.SelectedObjectsChanged);
      obj.SetDrawingObjectsDirty += new SetDrawingObjectsDirtyHandler(_project.DrawingObjects.SetDirty);
      obj.ObjType = Enums.ObjectType.DiplomaPicture;
      obj.Top = 20 + (10 * _project.ObjectCount());
      obj.Left = 20 + (10 * _project.ObjectCount());
      obj.Width = 200;
      obj.Height = 200;
      obj.Name = "object" + _project.ObjectCount().ToString().Trim();

      _project.DeselectAll();
      obj.Select();
      selectedObjectKeys = _project.AddObject(obj);
      lblSelectedKey.Text = _project.DrawingObjects.SelectedObjectKeysString;
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
      pbMain.Focus();
    }

    private void AddGraphicObject()
    {
      if (dlgFile.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      Graphics gr = pbMain.CreateGraphics();
      DrawingObject obj = new DrawingObject();
      obj.SetDrawingObjectsDirty += new SetDrawingObjectsDirtyHandler(_project.DrawingObjects.SetDirty);
      obj.SelectionChanged += new SelectionChangedHandler(_project.DrawingObjects.SelectedObjectsChanged);
      obj.ObjType = Enums.ObjectType.GraphicsObject;
      obj.Top = 50 + (10 * _project.ObjectCount());
      obj.Left = 50 + (10 * _project.ObjectCount());
      obj.Width = 100;
      obj.Height = 100;
      obj.Name = "object" + _project.ObjectCount().ToString().Trim();
      obj.GraphicsPath = dlgFile.FileName;
      _project.DeselectAll();
      obj.Select();
      selectedObjectKeys = _project.AddObject(obj);
      lblSelectedKey.Text = _project.DrawingObjects.SelectedObjectKeysString;
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
      pbMain.Focus();
    }

    #endregion

    #region Design / Print Mode Management

    private void EnterDesignMode()
    {
      StateHelper.IsInDesignMode = true;
      SetScale(StateHelper.PrevScale);
      pnlLeft.Visible = true;
      pbMain.Visible = true;
      pnlShadow.Visible = true;

      tbtnPrevPage.Enabled = false;
      tbtnNextPage.Enabled = false;
      lblSelectedKey.Visible = true;

      pnlLeft.Width = 237;

      ResizeDesignPage(StateHelper.Scale);
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
    }

    private void EnterPrintMode()
    {
      StateHelper.IsInDesignMode = false;
      StateHelper.PrevScale = StateHelper.Scale;
      SetScale(1.0F);
      pnlLeft.Visible = false;
      pbMain.Visible = true;
      pnlShadow.Visible = true;
      ResizePage(StateHelper.Scale);
      lblSelectedKey.Visible = false;

      Size tagMatrix = _project.GetTagMatrix();
      StateHelper.FirstTagOnPage = 0;
      StateHelper.TagsPerPage = tagMatrix.Width * tagMatrix.Height;
      StateHelper.TotalTags = _project.SelectedPersonCount();

      pbMain.Refresh();
      UpdateTagCountStatus();
    }

    private void RunDesigner(System.Drawing.Graphics gr)
    {
      gr.Clear(System.Drawing.Color.White);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      _project.DrawObjects(gr, new PointF(0F, 0F), new PointF(0F, 0F), Enums.DisplayMode.Designer, dictionary);
    }

    private void EditNames()
    {
      frmNames fNames = new frmNames(_project);
      fNames.ShowDialog();

      _project.IsDirty = true;

      if (!StateHelper.IsInDesignMode)
      {
        UpdateTagsOnPage();
        EnterPrintMode();
      }

      pbMain.Refresh();
      LoadDrawingObjectsListBox();
    }

    #endregion

    #region Print Related Functions / pbMain.Paint()

    private void pbMain_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
      if (StateHelper.IsInDesignMode)
      {
        tbtnPrevPage.Enabled = false;
        tbtnNextPage.Enabled = false;
        RunDesigner(e.Graphics);
        return;
      }

      tbtnPrevPage.Enabled = true;
      tbtnNextPage.Enabled = true;
      PrintNameTags(e.Graphics, Enums.DisplayMode.Display, e.ClipRectangle, (int)numBlanks.Value, ckOmitAll.Checked, ckLabels.Checked);
    }

    private void Print(bool testPrint, int blanks, bool omitAll)
    {
      if (StateHelper.TotalTags == 0)
        return;

      PersonSet persons = _project.Persons;
      List<Person> personsToPrint = _project.GetPersonPrintList();

      if (omitAll)
        personsToPrint.Clear();

      for (int i = 0; i < blanks; i++)
        personsToPrint.Add(GetDummyPerson());

      PrintDocument pd = new PrintDocument();

      if (_project.PrintPageSettings.PageOrientation == Enums.PageOrientation.Landscape)
        pd.DefaultPageSettings.Landscape = true;
      else
        pd.DefaultPageSettings.Landscape = false;

      pd.DefaultPageSettings.Margins.Top = 0;
      pd.DefaultPageSettings.Margins.Left = 0;
      pd.DefaultPageSettings.Margins.Bottom = 0;
      pd.DefaultPageSettings.Margins.Right = 0;

      pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);

      int tagnbr = 0;

      while (tagnbr < personsToPrint.Count)
      {
        StateHelper.FirstTagOnPage = tagnbr;
        StateHelper.LastTagOnPage = StateHelper.FirstTagOnPage + StateHelper.TagsPerPage - 1;
        tagnbr = StateHelper.LastTagOnPage + 1;
        if (StateHelper.LastTagOnPage > StateHelper.TotalTags - 1)
        {
          StateHelper.LastTagOnPage = StateHelper.TotalTags - 1;
          tagnbr = StateHelper.TotalTags;
          pd.Print();
          Application.DoEvents();
          return;
        }
        pd.Print();
        Application.DoEvents();
        if (testPrint && !omitAll)
          return;
      }
    }

    private void pd_PrintPage(object sender, PrintPageEventArgs ev)
    {
      PrintNameTags(ev.Graphics, Enums.DisplayMode.Printer, new Rectangle(new Point(0, 0), pbMain.Size), (int)numBlanks.Value, ckOmitAll.Checked, ckLabels.Checked);
    }

    private void PrintNameTags(Graphics gr, Enums.DisplayMode mode, Rectangle clip, int blanks, bool omitAll, bool printLabels)
    {
      if (StateHelper.TotalTags == 0)
        return;

      PersonSet persons = _project.Persons;
      List<Person> personsToPrint = _project.GetPersonPrintList();

      if (omitAll)
        personsToPrint.Clear();

      for (int i = 0; i < blanks; i++)
        personsToPrint.Add(GetDummyPerson());

      RectangleF clip2 = new RectangleF(clip.X, clip.Y, clip.Right, clip.Bottom);
      PointF origin = new PointF(_project.PrintPageSettings.LeftMargin, _project.PrintPageSettings.RightMargin);
      PointF adjust = new PointF(_project.PrintPageSettings.PrintAdjustHorizontal, _project.PrintPageSettings.PrintAdjustVertical);
      SizeF pageSize = _project.GetPageSize();
      Size tagMatrix = _project.GetTagMatrix();
      SizeF tagSize = new SizeF(_project.PrintPageSettings.NameTagWidth, _project.PrintPageSettings.NameTagHeight);

      int nRows = tagMatrix.Height;
      int nCols = tagMatrix.Width;

      float leftMargin = _project.PrintPageSettings.LeftMargin;
      float topMargin = _project.PrintPageSettings.TopMargin;
      float tagHeight = _project.PrintPageSettings.NameTagHeight;
      float tagWidth = _project.PrintPageSettings.NameTagWidth;
      float vertSpacing = _project.PrintPageSettings.VerticalSpacing;
      float horzSpacing = _project.PrintPageSettings.HorizontalSpacing;
      float horzAdjust = 0F;
      float vertAdjust = 0F;
      if (mode == Enums.DisplayMode.Printer)
      {
        horzAdjust = adjust.X;
        vertAdjust = adjust.Y;
      }
      else
      {
        adjust.X = 0;
        adjust.Y = 0;
      }

      gr.Clear(System.Drawing.Color.White);

      float scale = 1.0F;
      if (mode != Enums.DisplayMode.Printer)
      {
        scale = StateHelper.Scale;
      }

      int tagnbr = StateHelper.FirstTagOnPage - 1;

      for (int row = 0; row < nRows; row++)
      {
        for (int col = 0; col < nCols; col++)
        {
          tagnbr++;
          if (tagnbr > personsToPrint.Count - 1)
          {
            break;
          }
          //int psnIdx = (int) personsToPrint[tagnbr];
          Person p = personsToPrint[tagnbr];
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          dictionary.Add("@firstname", p.FirstName);
          dictionary.Add("@lastname", p.LastName);
          dictionary.Add("@fullname", p.FirstName + " " + p.LastName);
          dictionary.Add("@g", p.Group);
          origin.X = horzAdjust + leftMargin + (col * (tagWidth + horzSpacing));
          origin.Y = vertAdjust + topMargin + (row * (tagHeight + vertSpacing));
          RectangleF objRect = new RectangleF(origin.X * scale, origin.Y * scale,
                                              (origin.X + tagSize.Width) * scale,
                                              (origin.Y + tagSize.Height) * scale);

          if (clip2.IntersectsWith(objRect))
          {
            _project.DrawObjects(gr, origin, adjust, mode, dictionary);
          }
        }
      }

      UpdateTagCountStatus();

      numBlanks.Value = 0;
    }

    private Person GetDummyPerson()
    {
      Person p = new Person();
      p.FirstName = String.Empty;
      p.LastName = String.Empty;
      p.Group = String.Empty;
      p.Selected = true;
      p.Grade = "1";
      p.FullName = String.Empty;
      return p;
    }

    private void NextPage()
    {
      if (StateHelper.FirstTagOnPage + StateHelper.TagsPerPage > StateHelper.TotalTags - 1)
      {
        return;
      }
      StateHelper.FirstTagOnPage += StateHelper.TagsPerPage;
      StateHelper.LastTagOnPage += StateHelper.TagsPerPage;
      if (StateHelper.LastTagOnPage > StateHelper.TotalTags)
      {
        StateHelper.LastTagOnPage = StateHelper.TotalTags - 1;
      }

      pbMain.Refresh();
      UpdateTagCountStatus();
    }

    private void PrevPage()
    {
      if (StateHelper.FirstTagOnPage == 0)
      {
        return;
      }

      StateHelper.FirstTagOnPage -= StateHelper.TagsPerPage;
      StateHelper.LastTagOnPage = StateHelper.FirstTagOnPage + StateHelper.TagsPerPage - 1;
      pbMain.Refresh();
      UpdateTagCountStatus();
    }

    private void UpdateTagCountStatus()
    {
      int personPrintCount = _project.GetPersonPrintCount();

      lblTagCount.Text = ("Tags " +
                          (StateHelper.FirstTagOnPage + 1).ToString().Trim() + " to " +
                          (StateHelper.LastTagOnPage + 1).ToString().Trim() + " of " +
                          personPrintCount.ToString().Trim() + " selected tags shown");


      if (StateHelper.LastTagOnPage < personPrintCount - 1)
        tbtnNextPage.Enabled = true;
      else
        tbtnNextPage.Enabled = false;

      if (StateHelper.FirstTagOnPage == 0)
        tbtnPrevPage.Enabled = false;
      else
        tbtnPrevPage.Enabled = true;

      Application.DoEvents();
    }



    #endregion

    #region Scale and Paging Management

    private void SetScale(float scale)
    {
      StateHelper.Scale = scale;
      lblScale.Text = (StateHelper.Scale * 100).ToString().Trim() + "%";
      _project.Scale = StateHelper.Scale;

      if (StateHelper.IsInDesignMode)
      {
        ResizeDesignPage(StateHelper.Scale);
      }
      else
      {
        ResizePage(StateHelper.Scale);
      }
    }

    private void ZoomIn()
    {
      switch (Convert.ToInt32(StateHelper.Scale * 100))
      {
        case 25:
          StateHelper.Scale = 0.5F;
          break;
        case 50:
          StateHelper.Scale = 0.75F;
          break;
        case 75:
          StateHelper.Scale = 1.00F;
          break;
        case 100:
          StateHelper.Scale = 1.5F;
          break;
        case 150:
          StateHelper.Scale = 2.0F;
          break;
        case 200:
          StateHelper.Scale = 2.5F;
          break;
        case 250:
          StateHelper.Scale = 3.0F;
          break;
        case 300:
          StateHelper.Scale = 3.5F;
          break;
        default:
          break;
      }

      lblScale.Text = (StateHelper.Scale * 100).ToString().Trim() + "%";
      _project.Scale = StateHelper.Scale;

      if (StateHelper.IsInDesignMode)
      {
        ResizeDesignPage(StateHelper.Scale);
      }
      else
      {
        ResizePage(StateHelper.Scale);
      }
    }

    private void ZoomOut()
    {
      switch (Convert.ToInt32(StateHelper.Scale * 100))
      {
        case 350:
          StateHelper.Scale = 3.0F;
          break;
        case 300:
          StateHelper.Scale = 2.5F;
          break;
        case 250:
          StateHelper.Scale = 2.0F;
          break;
        case 200:
          StateHelper.Scale = 1.5F;
          break;
        case 150:
          StateHelper.Scale = 1.0F;
          break;
        case 100:
          StateHelper.Scale = 0.75F;
          break;
        case 75:
          StateHelper.Scale = 0.5F;
          break;
        case 50:
          StateHelper.Scale = 0.25F;
          break;
        default:
          break;
      }

      lblScale.Text = (StateHelper.Scale * 100).ToString().Trim() + "%";
      _project.Scale = StateHelper.Scale;

      if (StateHelper.IsInDesignMode)
      {
        ResizeDesignPage(StateHelper.Scale);
      }
      else
      {
        ResizePage(StateHelper.Scale);
      }
    }

    // this function resizes pbMain to the size of the individual tag for design purposes
    private void ResizeDesignPage(float scale)
    {
      int nWidth = (int)(_project.PrintPageSettings.NameTagWidth);
      int nHeight = (int)(_project.PrintPageSettings.NameTagHeight);

      pbMain.Width = Convert.ToInt32(nWidth * StateHelper.Scale);
      pbMain.Height = Convert.ToInt32(nHeight * StateHelper.Scale);
      pbMain.Left = 20;
      pbMain.Top = 20;

      lblTagSizeVal.Text = pbMain.Width.ToString() + "," + pbMain.Height.ToString();
      pnlShadow.Left = pbMain.Left + 4;
      pnlShadow.Top = pbMain.Top + 4;
      pnlShadow.Width = pbMain.Width;
      pnlShadow.Height = pbMain.Height;
      pbMain.Refresh();
    }

    // this function resizes pbMain to the page size for display
    private void ResizePage(float scale)
    {
      pbMain.Visible = false;
      pnlShadow.Visible = false;
      SizeF pageSize = _project.PrintPageSettings.GetPageSize();
      pbMain.Width = Convert.ToInt32(pageSize.Width * scale);
      pbMain.Height = Convert.ToInt32(pageSize.Height * scale);

      int left = pnlRight.Width / 2 - pbMain.Width / 2;
      int top = pnlRight.Height / 2 - pbMain.Height / 2;

      if (left < 40)
        pbMain.Left = 40;
      else
        pbMain.Left = left;

      if (top < 40)
        pbMain.Top = 40;
      else
        pbMain.Top = top;

      pnlShadow.Left = pbMain.Left + 4;
      pnlShadow.Top = pbMain.Top + 4;
      pnlShadow.Width = pbMain.Width;
      pnlShadow.Height = pbMain.Height;

      pbMain.Visible = true;
      pnlShadow.Visible = true;
      pbMain.Refresh();
    }

    #endregion

    #region pbMain Mouse Events

    private void pbMain_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (!StateHelper.IsInDesignMode)
      {
        return;
      }

      this.Cursor = Cursors.Arrow;
      bChangeBegun = false;

      selectedObjectKeys = _project.SelectAtXY(e.X, e.Y, ModifierKeys == Keys.Control);
      lblSelectedKey.Text = _project.DrawingObjects.SelectedObjectKeysString;

      if (selectedObjectKeys.Length > 0)
      {
        DrawingObject obj = _project.GetObjectByKey(selectedObjectKeys[0]);
        lblObjPosVal.Text = obj.Left.ToString() + ", " + obj.Top.ToString();
        lblObjSizeVal.Text = obj.Width.ToString() + ", " + obj.Height.ToString();

        float x = obj.Left * StateHelper.Scale;
        float y = obj.Top * StateHelper.Scale;
        float w = obj.Width * StateHelper.Scale;
        float h = obj.Height * StateHelper.Scale;
        float mx = e.X;
        float my = e.Y;

        lblScalePosVal.Text = x.ToString() + ", " + y.ToString();
        lblScaleSizeVal.Text = w.ToString("000.00") + ", " + h.ToString("000.00");
        lblNameVal.Text = obj.Name;
      }

      pbMain.Refresh();
    }

    private void pbMain_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      float dX = e.X - StateHelper.PrevX;
      float dY = e.Y - StateHelper.PrevY;

      lblMouseMoveVal.Text = dX.ToString() + ", " + dY.ToString();
      lblMousePosVal.Text = e.X.ToString() + ", " + e.Y.ToString();
      StateHelper.PrevX = e.X;
      StateHelper.PrevY = e.Y;

      getObjectKeys = _project.GetAtXY(e.X, e.Y, ModifierKeys == Keys.Control);

      if (getObjectKeys.Length > 0)
      {
        DrawingObject obj = _project.GetObjectByKey(getObjectKeys[0]);
        lblNameVal.Text = obj.Name;
        lblObjPosVal.Text = obj.Left.ToString() + ", " + obj.Top.ToString();
        lblObjSizeVal.Text = obj.Width.ToString() + ", " + obj.Height.ToString();

        float x = obj.Left * StateHelper.Scale;
        float y = obj.Top * StateHelper.Scale;
        float w = obj.Width * StateHelper.Scale;
        float h = obj.Height * StateHelper.Scale;

        lblScalePosVal.Text = x.ToString() + ", " + y.ToString();
        lblScaleSizeVal.Text = w.ToString("000.00") + ", " + h.ToString("000.00");
      }


      if (!StateHelper.IsInDesignMode)
        return;

      foreach (int i in _project.DrawingObjects.SelectedObjectKeys)
      {
        DrawingObject obj;
        obj = _project.GetObjectByKey(i);

        if (obj.IsLocked)
          continue;

        float x = obj.Left * StateHelper.Scale;
        float y = obj.Top * StateHelper.Scale;
        float w = obj.Width * StateHelper.Scale;
        float h = obj.Height * StateHelper.Scale;
        float mx = e.X;
        float my = e.Y;

        // right object handle grabbed to change width
        if (dimToChange == 'w' & bChangeBegun)
        {
          if ((x + w + dX < pbMain.Width) &  // don't let object go off edge of tag
              (w + dX > 5)) // don't let scaled width get under 5
          {
            _project.DrawingObjects.ChangeObjectWidth(i,
                (w + dX) / StateHelper.Scale);
            pbMain.Refresh();
          }

          lblObjPosVal.Text = obj.Left.ToString() + ", " + obj.Top.ToString();
          lblObjSizeVal.Text = obj.Width.ToString("000.00") + ", " + obj.Height.ToString("000.00");
          x = obj.Left * StateHelper.Scale;
          y = obj.Top * StateHelper.Scale;
          w = obj.Width * StateHelper.Scale;
          h = obj.Height * StateHelper.Scale;
          lblScalePosVal.Text = x.ToString() + ", " + y.ToString();
          lblScaleSizeVal.Text = w.ToString("000.00") + ", " + h.ToString("000.00");
          continue;
        }

        // bottom object handle grabbed to change height
        if (dimToChange == 'h' & bChangeBegun)
        {
          if ((y + h + dY < pbMain.Height) & // don't object go off edge of tag
              (h + dY > 5)) // don't let scaled height get under 5
          {
            _project.DrawingObjects.ChangeObjectHeight(i,
                (h + dY) / StateHelper.Scale);
            pbMain.Refresh();
          }

          lblObjPosVal.Text = obj.Left.ToString() + ", " + obj.Top.ToString();
          lblObjSizeVal.Text = obj.Width.ToString("000.00") + ", " + obj.Height.ToString("000.00");
          x = obj.Left * StateHelper.Scale;
          y = obj.Top * StateHelper.Scale;
          w = obj.Width * StateHelper.Scale;
          h = obj.Height * StateHelper.Scale;
          lblScalePosVal.Text = x.ToString() + ", " + y.ToString();
          lblScaleSizeVal.Text = w.ToString("000.00") + ", " + h.ToString("000.00");
          continue;
        }

        // changing the position of the object
        if (dimToChange == 'p' & bChangeBegun)
        {
          _project.DrawingObjects.ChangeObjectPosition(i,
              (x + dX) / StateHelper.Scale,
              (y + dY) / StateHelper.Scale);
          beginX = mx;
          beginY = my;

          pbMain.Refresh();

          lblObjPosVal.Text = obj.Left.ToString() + ", " + obj.Top.ToString();
          lblObjSizeVal.Text = obj.Width.ToString("000.00") + ", " + obj.Height.ToString("000.00");
          x = obj.Left * StateHelper.Scale;
          y = obj.Top * StateHelper.Scale;
          w = obj.Width * StateHelper.Scale;
          h = obj.Height * StateHelper.Scale;
          lblScalePosVal.Text = x.ToString() + ", " + y.ToString();
          lblScaleSizeVal.Text = w.ToString("000.00") + ", " + h.ToString("000.00");
          continue;
        }

        if (mx > x - 3 & mx < x + 3 & my > y + (h / 2) - 3 & my < y + (h / 2) + 3)
        {
          dimToChange = 'x';
          this.Cursor = Cursors.SizeWE;
        }
        else
        {
          if (mx > x + w - 3 & mx < x + w + 3 & my > y + (h / 2) - 3 & my < y + (h / 2) + 3)
          {
            dimToChange = 'w';
            this.Cursor = Cursors.SizeWE;
          }
          else
          {
            if (mx > x + (w / 2) - 3 & mx < x + (w / 2) + 3 & my > y - 3 & my < y + 3)
            {
              dimToChange = 'y';
              this.Cursor = Cursors.SizeNS;
            }
            else
            {
              if (mx > x + (w / 2) - 3 & mx < x + (w / 2) + 3 & my > y + h - 3 & my < y + h + 3)
              {
                dimToChange = 'h';
                this.Cursor = Cursors.SizeNS;
              }
              else
              {
                RectangleF rect = new RectangleF(x, y, w, h);
                if (rect.Contains(mx, my))
                {
                  dimToChange = 'p';
                  this.Cursor = Cursors.SizeAll;
                  beginX = mx;
                  beginY = my;
                }
                else
                {
                  dimToChange = ' ';
                  this.Cursor = Cursors.Arrow;
                }
              }
            }
          }

          lblObjPosVal.Text = obj.Left.ToString() + ", " + obj.Top.ToString();
          lblObjSizeVal.Text = obj.Width.ToString("000.00") + ", " + obj.Height.ToString("000.00");
          x = obj.Left * StateHelper.Scale;
          y = obj.Top * StateHelper.Scale;
          w = obj.Width * StateHelper.Scale;
          h = obj.Height * StateHelper.Scale;
          lblScalePosVal.Text = x.ToString() + ", " + y.ToString();
          lblScaleSizeVal.Text = w.ToString("000.00") + ", " + h.ToString("000.00");
          lblSelectedKey.Text = obj.Name;
        }
      }
    }

    private void pbMain_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      StateHelper.PrevX = e.X;
      StateHelper.PrevY = e.Y;

      if (!StateHelper.IsInDesignMode)
        return;

      if (_project.DrawingObjects.SelectedCount == 0)
        return;

      if (dimToChange != ' ')
        bChangeBegun = true;
    }

    private void pbMain_MouseLeave(object sender, System.EventArgs e)
    {
      this.Cursor = Cursors.Arrow;
    }

    private void pnlRight_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      _project.DeselectAll();
      pbMain.Refresh();
    }


    #endregion

    #region Object Font and Color Management

    private void SetFont()
    {
      if (dlgFont.ShowDialog() == DialogResult.OK)
      {
        Font f = dlgFont.Font;

        int objKey = _project.DrawingObjects.GetSoleSelectedTextObject();
        if (objKey == -1)
        {
          return;
        }
        DrawingObject obj = _project.GetObjectByKey(objKey);
        obj.TextFont = f;
        pbMain.Refresh();
        Application.DoEvents();
      }
    }

    private void SetFillColor()
    {
      int objKey = _project.DrawingObjects.GetSoleSelectedTextOrBlockObject();
      if (objKey == -1)
      {
        return;
      }

      if (dlgColor.ShowDialog() == DialogResult.OK)
      {
        DrawingObject obj = _project.GetObjectByKey(objKey);
        obj.FillColor = dlgColor.Color;
        obj.HasFillColor = true;
        pbMain.Refresh();
        Application.DoEvents();
      }
    }

    private void SetBorderColor()
    {
      int objKey = _project.DrawingObjects.GetSoleSelectedTextOrBlockObject();
      if (objKey == -1)
      {
        return;
      }

      if (dlgColor.ShowDialog() == DialogResult.OK)
      {
        DrawingObject obj = _project.GetObjectByKey(objKey);
        obj.BorderColor = dlgColor.Color;
        pbMain.Refresh();
        Application.DoEvents();
      }
    }

    private void SetBorderWidth(int width)
    {
      int objKey = _project.DrawingObjects.GetSoleSelectedTextOrBlockObject();
      if (objKey == -1)
      {
        return;
      }

      DrawingObject obj = _project.GetObjectByKey(objKey);
      obj.BorderWidth = width;
      pbMain.Refresh();
      Application.DoEvents();
    }

    private void SetTextAlignment(System.Windows.Forms.HorizontalAlignment align)
    {
      int objKey = _project.DrawingObjects.GetSoleSelectedTextObject();
      if (objKey == -1)
      {
        return;
      }
      DrawingObject obj = _project.GetObjectByKey(objKey);
      obj.TextHorizAlign = align;
      pbMain.Refresh();
      Application.DoEvents();
    }

    #endregion

    #region Keystroke Management Events and Functions

    private void frmMain_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      int objKey = _project.DrawingObjects.GetSoleSelectedTextObject();
      if (objKey == -1)
      {
        return;
      }

      DrawingObject obj = _project.GetObjectByKey(objKey);

      switch (e.KeyChar)
      {
        case (char)8:
          if (obj.Text.Length == 1)
          {
            obj.Text = "";
          }
          else
          {
            if (obj.Text.Length > 1)
            {
              obj.Text = obj.Text.Substring(0, obj.Text.Length - 1);
            }
          }
          break;

        default:
          obj.Text += e.KeyChar.ToString();
          break;
      }

      pbMain.Refresh();
      LoadDrawingObjectsListBox();
      Application.DoEvents();
      e.Handled = true;
    }

    private void frmMain_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Delete:
          _project.DeleteSelected();
          selectedObjectKeys = new int[0];
          this.Cursor = Cursors.Arrow;
          pbMain.Refresh();
          LoadDrawingObjectsListBox();
          break;
      }
    }

    private void frmMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      float multiplier = 1.0F;
      if (e.Control)
      {
        multiplier = 5.0F;
      }

      switch (e.KeyCode)
      {
        case Keys.Up:
          Nudge(-1 * multiplier, 0);
          pbMain.Refresh();
          break;

        case Keys.Down:
          Nudge(1 * multiplier, 0);
          pbMain.Refresh();
          break;

        case Keys.Right:
          Nudge(0, 1 * multiplier);
          pbMain.Refresh();
          break;

        case Keys.Left:
          Nudge(0, -1 * multiplier);
          pbMain.Refresh();
          break;

        case Keys.W:
          if (e.Alt)
          {
            Grow(0, 1 * multiplier);
            pbMain.Refresh();
          }
          break;

        case Keys.N:
          if (e.Alt)
          {
            Grow(0, -1 * multiplier);
            pbMain.Refresh();
          }
          break;

        case Keys.T:
          if (e.Alt)
          {
            Grow(1 * multiplier, 0);
            pbMain.Refresh();
          }
          break;

        case Keys.S:
          if (e.Alt)
          {
            Grow(-1 * multiplier, 0);
            pbMain.Refresh();
          }
          break;

        case Keys.F12:
          //tbtnDesignMode.Visible = !tbtnDesignMode.Visible;
          break;
      }
    }

    #endregion

    #region Object Sizing and Moving Functions

    private void Nudge(float vertical, float horizontal)
    {
      foreach (int i in _project.DrawingObjects.SelectedObjectKeys)
      {
        DrawingObject obj;
        obj = _project.GetObjectByKey(i);
        obj.Top += vertical;
        obj.Left += horizontal;

        lblObjPosVal.Text = obj.Left.ToString() + ", " + obj.Top.ToString();
        lblObjSizeVal.Text = obj.Width.ToString("000.00") + ", " + obj.Height.ToString("000.00");
        float x = obj.Left * StateHelper.Scale;
        float y = obj.Top * StateHelper.Scale;
        float w = obj.Width * StateHelper.Scale;
        float h = obj.Height * StateHelper.Scale;
        lblScalePosVal.Text = x.ToString() + ", " + y.ToString();
        lblScaleSizeVal.Text = w.ToString("000.00") + ", " + h.ToString("000.00");
      }


    }

    private void Grow(float vertical, float horizontal)
    {
      foreach (int i in _project.DrawingObjects.SelectedObjectKeys)
      {
        DrawingObject obj;
        obj = _project.GetObjectByKey(i);
        obj.Height += vertical;
        obj.Width += horizontal;

        lblObjPosVal.Text = obj.Left.ToString() + ", " + obj.Top.ToString();
        lblObjSizeVal.Text = obj.Width.ToString("000.00") + ", " + obj.Height.ToString("000.00");
        float x = obj.Left * StateHelper.Scale;
        float y = obj.Top * StateHelper.Scale;
        float w = obj.Width * StateHelper.Scale;
        float h = obj.Height * StateHelper.Scale;
        lblScalePosVal.Text = x.ToString() + ", " + y.ToString();
        lblScaleSizeVal.Text = w.ToString("000.00") + ", " + h.ToString("000.00");
      }
    }

    #endregion

    #region pbMain Context Menu Events

    private void mnuDrawingObjectAction(object sender, System.EventArgs e)
    {
      MenuItem m = (MenuItem)sender;

      switch (m.Tag.ToString())
      {
        case "BRING_TO_FRONT":
          BringObjectToFront();
          break;

        case "BRING_FORWARD":
          BringObjectForward();
          break;

        case "SEND_BACKWARD":
          SendObjectBackward();
          break;

        case "SEND_TO_BACK":
          SendObjectToBack();
          break;

        case "ALIGN_LEFT":
          SetTextAlignment(System.Windows.Forms.HorizontalAlignment.Left);
          break;

        case "ALIGN_CENTER":
          SetTextAlignment(System.Windows.Forms.HorizontalAlignment.Center);
          break;

        case "ALIGN_RIGHT":
          SetTextAlignment(System.Windows.Forms.HorizontalAlignment.Right);
          break;

        case "NO_FILL":
          SetObjectNoFill();
          break;

        case "TEXT_UPPERCASE":
          SetTextUpperCase();
          break;

        case "TEXT_LOWERCASE":
          SetTextLowerCase();
          break;

        case "TEXT_NORMALCASE":
          SetTextNormalCase();
          break;

        case "BORDER_WIDTH":
          char[] delim = { ' ', '.' };
          string[] s = m.Text.Split(delim, 2);
          SetBorderWidth(int.Parse(s[0].Trim()));
          break;

        case "ADD_SHAPE_RECTANGLE":
          this.AddRectangleObject();
          break;

        case "ADD_SHAPE_ELLIPSE":
          this.AddEllipseObject();
          break;

        case "ADD_SHAPE_DIPLOMA_PICTURE":
          this.AddDiplomaPictureObject();
          break;

        case "UNLOCK_SHAPE":
          this.UnlockObject();
          break;

        case "LOCK_SHAPE":
          this.LockObject();
          break;
      }

    }


    #endregion

    #region Object Z-Order Management

    private void BringObjectToFront()
    {
      if (_project.DrawingObjects.SelectedCount != 1)
      {
        return;
      }
      int key = _project.DrawingObjects.GetSelectedObject();
      _project.DrawingObjects.BringToFront(key);
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
    }

    private void BringObjectForward()
    {
      if (_project.DrawingObjects.SelectedCount != 1)
      {
        return;
      }
      int key = _project.DrawingObjects.GetSelectedObject();
      _project.DrawingObjects.BringForward(key);
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
    }

    private void SendObjectBackward()
    {
      if (_project.DrawingObjects.SelectedCount != 1)
      {
        return;
      }

      int key = _project.DrawingObjects.GetSelectedObject();
      _project.DrawingObjects.SendBackward(key);
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
    }

    private void SendObjectToBack()
    {
      if (_project.DrawingObjects.SelectedCount != 1)
      {
        return;
      }
      int key = _project.DrawingObjects.GetSelectedObject();
      _project.DrawingObjects.SendToBack(key);
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
    }

    private void SetObjectNoFill()
    {
      int objKey = _project.DrawingObjects.GetSoleSelectedTextOrBlockObject();
      if (objKey == -1)
      {
        return;
      }
      DrawingObject obj = _project.GetObjectByKey(objKey);
      obj.HasFillColor = false;
      pbMain.Refresh();
      Application.DoEvents();
    }

    private void SetTextUpperCase()
    {
      int objKey = _project.DrawingObjects.GetSoleSelectedTextOrBlockObject();
      if (objKey == -1)
      {
        return;
      }
      DrawingObject obj = _project.GetObjectByKey(objKey);
      obj.IsUpperCase = true;
      obj.IsLowerCase = false;
      pbMain.Refresh();
      Application.DoEvents();
    }

    private void SetTextLowerCase()
    {
      int objKey = _project.DrawingObjects.GetSoleSelectedTextOrBlockObject();
      if (objKey == -1)
      {
        return;
      }
      DrawingObject obj = _project.GetObjectByKey(objKey);
      obj.IsUpperCase = false;
      obj.IsLowerCase = true;
      pbMain.Refresh();
      Application.DoEvents();
    }

    private void SetTextNormalCase()
    {
      int objKey = _project.DrawingObjects.GetSoleSelectedTextOrBlockObject();
      if (objKey == -1)
      {
        return;
      }
      DrawingObject obj = _project.GetObjectByKey(objKey);
      obj.IsUpperCase = false;
      obj.IsLowerCase = false;
      pbMain.Refresh();
      Application.DoEvents();
    }

    private void LockObject()
    {
      if (_project.DrawingObjects.SelectedCount != 1)
      {
        return;
      }
      int key = _project.DrawingObjects.GetSelectedObject();
      _project.DrawingObjects.LockObject(key);
      pbMain.Refresh();
    }

    private void UnlockObject()
    {
      if (_project.DrawingObjects.SelectedCount != 1)
      {
        return;
      }
      int key = _project.DrawingObjects.GetSelectedObject();
      _project.DrawingObjects.UnlockObject(key);
      pbMain.Refresh();
    }

    #endregion

    #region Initialization and Termination Functions

    private void InitializeApp()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the creation of the application object 'a'." + g.crlf2 +
                        ex.ToReport(), "Name Tags - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      _dbSpecPrefix = g.CI("DbSpecPrefix");
      _configDbSpec = g.GetDbSpec(_dbSpecPrefix);

      LogHelper.Log("Initialization started");
      _project = new Project("New Project");
      StateHelper.PrevScale = 2.0F;
      LoadImageList();

      pnlLeft.Width = 200;
      ResizePage(StateHelper.Scale);
      pbMain.Visible = false;
      pnlShadow.Visible = false;
      pnlRight.Visible = false;
      pnlLeft.Visible = false;
      tbtnDesignMode.Visible = true;
      LogHelper.Log("Initialization complete");
    }

    private void SetProjectDirty()
    {
      _project.IsDirty = true;
    }

    private void LoadImageList()
    {
      imgList.ImageSize = new Size(32, 32);

      var rm = new ResourceManager("Org.NameTags.Resource1", Assembly.GetExecutingAssembly());
      Image i = rm.GetObject("project_properties_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("open_project_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("save_project_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("design_mode_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("display_mode_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("prev_page_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("next_page_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("edit_names_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("print_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("add_text_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("add_picture_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("add_shape_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("border_width_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("border_color_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("text_font_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("color_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("zoom_in_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("zoom_out_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("send_to_back_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("send_backward_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("bring_forward_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("bring_to_front_normal") as Image;
      imgList.Images.Add(i);
      i = rm.GetObject("diploma_picture_normal") as Image;
      imgList.Images.Add(i);

      tbDesign.ImageList = imgList;
      tbDesign.Buttons[0].ImageIndex = 0;
      tbDesign.Buttons[1].ImageIndex = 1;
      tbDesign.Buttons[2].ImageIndex = 2;
      tbDesign.Buttons[3].ImageIndex = 3;
      tbDesign.Buttons[4].ImageIndex = 4;
      tbDesign.Buttons[5].ImageIndex = 5;
      tbDesign.Buttons[6].ImageIndex = 6;
      tbDesign.Buttons[7].ImageIndex = 7;
      tbDesign.Buttons[8].ImageIndex = 8;
      tbDesign.Buttons[10].ImageIndex = 9;
      tbDesign.Buttons[11].ImageIndex = 10;
      tbDesign.Buttons[12].ImageIndex = 11;
      tbDesign.Buttons[13].ImageIndex = 12;
      tbDesign.Buttons[14].ImageIndex = 13;
      tbDesign.Buttons[15].ImageIndex = 14;
      tbDesign.Buttons[16].ImageIndex = 15;
      tbDesign.Buttons[17].ImageIndex = 16;
      tbDesign.Buttons[18].ImageIndex = 17;
      tbDesign.Buttons[19].ImageIndex = 18;
      tbDesign.Buttons[20].ImageIndex = 19;
      tbDesign.Buttons[21].ImageIndex = 20;
      tbDesign.Buttons[22].ImageIndex = 21;
      tbDesign.Buttons[23].ImageIndex = 22;

      tbDesign.Buttons[12].Style = ToolBarButtonStyle.DropDownButton;
      tbDesign.Buttons[12].DropDownMenu = this.mnuAddShape;
      tbDesign.Buttons[13].Style = ToolBarButtonStyle.DropDownButton;
      tbDesign.Buttons[13].DropDownMenu = this.mnuBorderWidth;

    }


    #endregion

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!IsFirstShowingOfForm)
        return;

      LogHelper.Log("Program form visible for first time");
      IsFirstShowingOfForm = false;
      OpenProject();
    }

    private void OpenProject()
    {
      _project = new Project("New Project");

      LogHelper.Log("Showing open project dialog");

      frmOpenProject fOpenProject = new frmOpenProject(_project);
      fOpenProject.ShowDialog();

      if (_project.Name.CompareTo("NO_PROJECT") == 0)
      {
        LogHelper.Log("Open project dialog canceled");
        StateHelper.PrevScale = 2.0F;
        _project.IsValidProject = false;
        return;
      }

      if (_project.Name.CompareTo("New Project") == 0)
      {
        LogHelper.Log("Open project dialog - New Project was selected");
        StateHelper.PrevScale = 2.0F;
        SetProjectOpen();
        return;
      }

      Stream s = File.OpenRead(_project.FullFileName);
      if (s != null)
      {

        LogHelper.Log("Open project dialog - [" + _project.FullFileName + "] was selected");
        IFormatter f = new BinaryFormatter();
        _project = (Project)f.Deserialize(s);
        _project.InitializeDrawingObjects();
        s.Close();
        _project.IsValidProject = true;
        _project.IsDirty = false;

        SetProjectOpen();
      }
      _project.IsDirty = false;
      _project.DrawingObjects.DeselectAll();
    }

    private void LoadDrawingObjectsListBox()
    {
      if (!StateHelper.IsInDesignMode)
        return;

      lbDrawingObjects.Items.Clear();

      foreach (var drawingObject in _project.DrawingObjects.Values)
      {
        lbDrawingObjects.Items.Add(drawingObject.Name + " (" + drawingObject.ObjType.ToString() + ")");
      }
    }

    private void mnuFileActions_Click(object sender, EventArgs e)
    {
      MenuItem i = (MenuItem)sender;

      switch (i.Tag.ToString())
      {
        case "EXIT_PROGRAM":
          if (_project.IsDirty)
          {
            switch (MessageBox.Show("Do you want to save the current name tag project?", "Closing Name Tag Project",
                                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
              case DialogResult.Yes:
                SaveProject(false);
                break;

              case DialogResult.No:
                break;

              case DialogResult.Cancel:
                return;
            }
          }
          this.Close();
          break;

        case "NEW_PROJECT":
          if (_project.IsDirty)
          {
            switch (MessageBox.Show("Do you want to save the current name tag project?", "Creating New Name Tag Project",
                                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
              case DialogResult.Yes:
                SaveProject(false);
                break;

              case DialogResult.No:
                break;

              case DialogResult.Cancel:
                return;
            }
          }
          _project = new Project("New Project");
          SetProjectOpen();
          _project.IsDirty = false;
          break;

        case "OPEN_PROJECT":
          OpenProject();
          break;

        case "SAVE_PROJECT":
          SaveProject(false);
          break;

        case "SAVE_PROJECT_AS":
          SaveProject(true);
          break;

        case "CLOSE_PROJECT":
          if (_project.IsDirty)
          {
            switch (MessageBox.Show("Do you want to save the current name tag project?", "Closing Name Tag Project",
                                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
              case DialogResult.Yes:
                SaveProject(false);
                break;

              case DialogResult.No:
                break;

              case DialogResult.Cancel:
                return;
            }
          }
          SetProjectClosed();
          break;

        case "PROJECT_PROPERTIES":
          ProjectProperties();
          break;
      }
    }

    private void SetProjectOpen()
    {
      SetScale(_project.Scale);
      _project.IsValidProject = true;
      this.Text = ConfigHelper.AppTitle + " - " + _project.Name;
      StateHelper.IsInitialized = true;
      pnlLeft.Visible = true;
      pnlRight.Visible = true;

      UpdateTagsOnPage();

      if (StateHelper.IsInDesignMode)
        EnterDesignMode();
      else if (_project.Persons.Count == 0)
        EnterDesignMode();
      else
        EnterPrintMode();

      pbMain.Refresh();
    }

    private void UpdateTagsOnPage()
    {
      StateHelper.FirstTagOnPage = 0;
      Size tagMatrix = _project.GetTagMatrix();
      StateHelper.TagsPerPage = tagMatrix.Width * tagMatrix.Height;
      StateHelper.TotalTags = _project.GetPersonPrintCount();

      if (StateHelper.TotalTags < StateHelper.TagsPerPage)
        StateHelper.LastTagOnPage = StateHelper.FirstTagOnPage + StateHelper.TotalTags - 1;
      else
        StateHelper.LastTagOnPage = StateHelper.FirstTagOnPage + StateHelper.TagsPerPage - 1;
    }


    private void SetProjectClosed()
    {
      _project = new Project("New Project");
      _project.IsValidProject = false;
      _project.IsDirty = false;
      pnlLeft.Visible = false;
      pnlRight.Visible = false;
      this.Text = ConfigHelper.AppTitle;
      pbMain.Refresh();
      StateHelper.IsInDesignMode = false;
    }

    private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
    {
      LogHelper.Log("Program ending");
    }

    private void mnuPbMainContextProperties_Click(object sender, EventArgs e)
    {
      ShowObjectPropertiesDialog();
    }

    private void ShowObjectPropertiesDialog()
    {
      using (frmObjectProperties fObjectProperties = new frmObjectProperties(_project.DrawingObjects))
      {
        fObjectProperties.DrawingObjectUpdated += DrawingObjectUpdated;
        fObjectProperties.ShowDialog();
      }

      pbMain.Refresh();
      LoadDrawingObjectsListBox();
    }

    private void DeleteListObject()
    {
      _project.DeleteSelected();
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
    }

    private void LockListObject()
    {

    }

    private void UnlockListObject()
    {

    }

    private void UnlockAll()
    {
      _project.UnlockAll();
    }

    private void DrawingObjectUpdated(bool objectUpdated)
    {
      pbMain.Refresh();
      LoadDrawingObjectsListBox();
    }

    private void btnTestPrint_Click(object sender, EventArgs e)
    {
      //Print(true, (int) numBlanks.Value, ckOmitAll.Checked);
    }

    private void lbDrawingObjects_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lbDrawingObjects.SelectedIndex == -1)
        return;

      string drawingObjectItem = lbDrawingObjects.SelectedItem.ToString();
      int parIndex = drawingObjectItem.IndexOf("(");

      if (parIndex == -1)
        return;

      string drawingObjectName = drawingObjectItem.Substring(0, parIndex - 1).Trim();

      SelectObjectByName(drawingObjectName);
    }

    private void SelectObjectByName(string objectName)
    {
      if (!StateHelper.IsInDesignMode)
        return;

      DrawingObject obj = _project.GetObjectByName(objectName);
      lblObjPosVal.Text = obj.Left.ToString() + ", " + obj.Top.ToString();
      lblObjSizeVal.Text = obj.Width.ToString() + ", " + obj.Height.ToString();

      float x = obj.Left * StateHelper.Scale;
      float y = obj.Top * StateHelper.Scale;
      float w = obj.Width * StateHelper.Scale;
      float h = obj.Height * StateHelper.Scale;

      lblScalePosVal.Text = x.ToString() + ", " + y.ToString();
      lblScaleSizeVal.Text = w.ToString("000.00") + ", " + h.ToString("000.00");

      lblSelectedKey.Text = _project.DrawingObjects.SelectedObjectKeysString;

      pbMain.Refresh();
      pbMain.Focus();
    }

    private void ctxMnuObjectList_Click(object sender, EventArgs e)
    {
      ShowObjectPropertiesDialog();
    }

    private void ckLabels_CheckedChanged(object sender, EventArgs e)
    {
      pbMain.Refresh();
    }
  }
}

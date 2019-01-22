using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Timers;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.EditorToolWindows;
using Org.FTW.Forms;
using Org.FTW.ToolPanels;
using Org.FTW;
using Org.Terminal.Controls;
using Org.Terminal.BMS;
using Org.Terminal.Screen;
using Org.SF;
using Org.GS.UI;
using Org.GS;

namespace Org.TerminalTest
{
  public partial class frmMain : frmToolWindowParent
  {
    private a a;
    private bool _isFirstShowing = true;
    private PerformanceInfoSet _perfInfoSet;
    private frmDisplayText _fDisplayText;
    private BmsMapErrorSet _bmsMapErrorSet;
    private ScreenSpecSet _screenSpecSet;
    private ScreenSpec _screenSpec;
    private System.Timers.Timer _moveTimer;

    private bool _suppressCharWidthChange = false;

    private EditorPanel _editorPanel;
    private ControlPanel _controlPanel;


    #region Tool Window Management Fields
    private ToolWindowManager _twMgr;
    private UIState _uiState;
    private float _scale = 100.0F;
    private bool propertiesShown = false;
    private bool codePanelShown = false;
    private PictureBox pb;
    private Image _emptyCell;
    private List<string> _tabPageOrder;
    #endregion

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Go":
          Go();
          break;

        //case "Up":
        //  Up();
        //  break;

        //case "Down":
        //  Down();
        //  break;

        //case "ShowPerfReport":
        //  ShowPerfReport();
        //  break;

        case "ShowEditor":
          ShowEditor();
          break;

        case "Exit":
          TerminateProgram();
          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void Go()
    {

    }

    private void ShowEditor()
    {
      if (_twMgr.ToolPanels.ContainsKey("Editor1"))
      {
        var tp = _twMgr.ToolPanels["Editor1"] as EditorPanel;
        tp.SetScreenSpec(_screenSpec);
        tp.ShowScreen();
      }
    }


    private void MFControlEvent(MFEventArgs e)
    {
      lblStatus.Text = "Clicked on " + e.Sender.Name;
    }




    private void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the creation of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "TerminalTest - Application Object Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {

        _tabPageOrder = new List<string>();
        foreach (TabPage tabPage in tabMain.TabPages)
        {
          if (tabPage.Tag != null)
            _tabPageOrder.Add(tabPage.Tag.ToString().Replace("TabPage_", String.Empty));
        }

        InitializeToolWindowForms();

        LoadMapErrorCodes();


        var bmsMapSetFile = new BmsMapSetFile(File.ReadAllText(g.ImportsPath + @"\BMS\PDFEDIT.BMS"), _bmsMapErrorSet);
        if (bmsMapSetFile.BmsMapSet.Count > 0)
        {
          _screenSpecSet = bmsMapSetFile.GetScreenSpecSet();
          _screenSpec = _screenSpecSet.Values.First();
        }


        _editorPanel = _twMgr.ToolPanels["Editor1"] as EditorPanel;
        _editorPanel.EventToHost += EventFromEditorPanel;

        _editorPanel.SetScreenSpec(_screenSpec);
        _editorPanel.ShowScreen();

        _controlPanel = _twMgr.ToolPanels["ControlPanel"] as ControlPanel;
        _controlPanel.SetScreenSpec(_screenSpecSet, _screenSpec.Name);

        //XmlMapper.AddAssembly(Assembly.GetAssembly(typeof(ScreenSpecSet)));
        //var specSetXml = XElement.Parse(File.ReadAllText(g.ImportsPath + @"\ScreenXml\PDF2.xml"));
        //var f = new ObjectFactory2();
        //var screenSpecSetObject = f.Deserialize(specSetXml) as ScreenSpecSet;
        //string recoil = f.Serialize(screenSpecSetObject).ToString();

        tabMain.SelectedTab = tabPageEditor1;
        pnlTop.Visible = false;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization process." + g.crlf2 + ex.ToReport(),
                        "TerminalTest  - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }


    }

    private void EventFromEditorPanel(MFEventArgs e)
    {
      switch (e.EventCommand)
      {
        case EventCommand.UpdateInfoPanel:
          if (_controlPanel != null)
            _controlPanel.MainEventHandler(e);
          break;
      }
    }

    private void LoadMapErrorCodes()
    {
      try
      {
        _bmsMapErrorSet = new BmsMapErrorSet();

        string errorCodePath = g.ImportsPath + @"\ReferenceData\MapErrorCodes.xml";

        if (File.Exists(errorCodePath))
        {
          var errorSet = XElement.Parse(File.ReadAllText(errorCodePath));
          var errors = errorSet.Elements();
          foreach (var error in errors)
          {
            var bmsMapError = new BmsMapError();
            bmsMapError.Code = error.Attribute("Code").Value.ToInt32();
            bmsMapError.BmsMapErrorLevel = g.ToEnum<BmsMapErrorLevel>(error.Attribute("Level").Value.ToInt32(), BmsMapErrorLevel.NoError);
            bmsMapError.ErrorMessage = error.Attribute("Msg").Value;

            if (_bmsMapErrorSet.ContainsKey(bmsMapError.Code))
              throw new Exception("MapError.Code " + bmsMapError.Code.ToString() + " already exists in the collection.");

            _bmsMapErrorSet.Add(bmsMapError.Code, bmsMapError);
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load the MapError codes from the xml file.", ex);
      }
    }

    //private void editMain_CellSelected(ScreenCell selectedCell)
    //{
    //  if (selectedCell == null)
    //    return;

    //  lblStatus.Text = "Row: " + selectedCell.CellMetrics.Row.ToString() + "  Col: " + selectedCell.CellMetrics.Column.ToString();
    //}

    private void frmMain_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F5:
          _editorPanel.RefreshUI();
          break;

        case Keys.F6:
          ckShowFields.Checked = !ckShowFields.Checked;
          break;
      }

      _editorPanel.KeyDown(sender, e);
    }

    private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
    {
      _editorPanel.KeyPress(sender, e);
    }

    private void frmMain_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyValue == 187 && e.Control)
      {
        BumpFontSizeUp();
        return;
      }

      if (e.KeyValue == 189 && e.Control)
      {
        BumpFontSizeDown();
        return;
      }

      switch (e.KeyCode)
      {
        case Keys.R:
          if (e.Control)
          {
            var bmsMapSetFile = new BmsMapSetFile(File.ReadAllText(g.ImportsPath + @"\BMS\PDFEDIT.BMS"), _bmsMapErrorSet);
            if (bmsMapSetFile.BmsMapSet.Count > 0)
            {
              var screenSpecSet = bmsMapSetFile.GetScreenSpecSet();
              _screenSpec = screenSpecSet.Values.First();
            }

            if (_editorPanel != null)
            {
              _editorPanel.SetScreenSpec(_screenSpec);
              _editorPanel.ShowScreen();
            }
            return;
          }
          break;



      }

      _editorPanel.KeyUp(sender, e);
    }

    private void tabMain_Enter(object sender, EventArgs e)
    {

    }

    private void tabMain_Leave(object sender, EventArgs e)
    {

    }

    private void pnlMain_Enter(object sender, EventArgs e)
    {

    }

    private void pnlMain_Leave(object sender, EventArgs e)
    {

    }

    private void tabPageControls_Enter(object sender, EventArgs e)
    {

    }

    private void tabPageControls_Leave(object sender, EventArgs e)
    {

    }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
    }

    private void TerminateProgram()
    {
      this.Close();
    }

    #region Tool Panel Processing


    private void InitializeToolWindowForms()
    {
      int secondScreenWidth = 0;
      int thirdScreenWidth = 0;

      Rectangle primaryScreenRectangle = new Rectangle(new Point(0, 0), Screen.PrimaryScreen.Bounds.Size);

      if (Screen.AllScreens.Count() > 1)
        secondScreenWidth = Screen.AllScreens[1].Bounds.Width;

      if (Screen.AllScreens.Count() > 2)
        thirdScreenWidth = Screen.AllScreens[2].Bounds.Width;

      Rectangle totalScreenArea =
        new Rectangle(new Point(0, 0), new Size(primaryScreenRectangle.Width + secondScreenWidth + thirdScreenWidth, primaryScreenRectangle.Height));

      List<string> splitterPanelsManaged = new List<string>();

      Point initialLocation = new Point(650, 250);

      var dockedPanels = new Dictionary<string, Panel>();
      foreach (TabPage tabPage in tabMain.TabPages)
      {
        foreach (Control control in tabPage.Controls)
        {
          if (control.GetType().Name == "Panel")
          {
            if (control.Tag != null && control.Tag.ToString().StartsWith("DockTarget_"))
            {
              string dockedPanelKey = control.Tag.ToString().Trim();
              if (dockedPanels.ContainsKey(dockedPanelKey))
              {
                MessageBox.Show("A duplicate tag values exists among the docked panels which are children of the tabPages included in tabMain." + g.crlf2 +
                                "The duplicate tag value is '" + dockedPanelKey + "'." + g.crlf2 +
                                "Please correct so that each docked panel has a unique tag value.",
                                "PdfTextExtract - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
              }

              dockedPanels.Add(dockedPanelKey, (Panel)control);
            }
          }
        }
      }

      _twMgr = new ToolWindowManager();
      _uiState = g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName].UIState;

      foreach (UIWindow uiWindow in _uiState.UIWindowSet.Values)
      {
        if (!uiWindow.IsActive)
          continue;

        if (uiWindow.IsMainForm)
        {
          MainFormHelper.ManageInitialSize(this, uiWindow);
          continue;
        }

        string toolName = uiWindow.Name.Trim();

        if (_twMgr.ToolWindowComponentsSet.ContainsKey(toolName))
          throw new Exception("A duplicate name exists in the set of UIWindow configurations in the AppConfig file. The duplicate name is '" + toolName + "'.");

        if (uiWindow.IsDockable && !dockedPanels.ContainsKey("DockTarget_" + toolName))
          throw new Exception("No panel has been designated for docking the tool window control named '" + toolName + "'.");

        var twComponents = new ToolWindowComponents();
        twComponents.Name = toolName;

        switch (uiWindow.TypeName)
        {
          case "RichTextViewer":
            twComponents.ToolPanel = new RichTextViewer();
            RichTextViewer rtv = (RichTextViewer)twComponents.ToolPanel;
            AddButtonsToToolWindow(uiWindow.Name, rtv);
            break;

          case "ControlPanel":
            twComponents.ToolPanel = new ControlPanel();
            ControlPanel cp = (ControlPanel)twComponents.ToolPanel;
            break;

          case "EditorPanel":
            twComponents.ToolPanel = new EditorPanel();
            EditorPanel ep = (EditorPanel)twComponents.ToolPanel;
            break;

            // other types in Org.FTW may be possible - implement later, or as needed.
        }

        twComponents.ToolWindow = new frmToolWindowBase(this, uiWindow.WindowTitle);
        twComponents.ToolWindow.Owner = this;
        twComponents.ToolWindow.Tag = "ToolWindow_" + toolName;
        twComponents.ToolWindow.ToolAction += ToolWindow_ToolAction;
        twComponents.ToolPanel.Tag = "ToolPanel_" + toolName;
        twComponents.ToolPanel.NotifyHostEvent += ToolPanel_NotifyHostEvent;
        twComponents.ToolPanel.DockButton.Click += Action;
        twComponents.FloatedTarget = twComponents.ToolWindow.DockPanel;

        if (dockedPanels.ContainsKey("DockTarget_" + toolName))
          twComponents.DockedTarget = dockedPanels["DockTarget_" + toolName];
        else
          twComponents.DockedTarget = null;

        _twMgr.ToolWindowComponentsSet.Add(toolName, twComponents);

        if (uiWindow.WindowLocation.IsDocked)
        {
          twComponents.ToolWindow.Visible = false;
          twComponents.ToolWindow.DockPanel.Controls.Remove(twComponents.ToolPanel);
          twComponents.DockedTarget.Controls.Clear();
          twComponents.DockedTarget.Controls.Add(twComponents.ToolPanel);

          twComponents.ToolPanel.Dock = DockStyle.Fill;
        }
        else
        {
          Point defaultLocation = new Point(200, 200);
          twComponents.FloatedTarget.Controls.Clear();
          Size tpSize = twComponents.ToolPanel.Size;
          twComponents.FloatedTarget.Controls.Add(twComponents.ToolPanel);

          twComponents.ToolPanel.Dock = DockStyle.Fill;

          if (twComponents.ToolWindow.FormBorderStyle == System.Windows.Forms.FormBorderStyle.FixedToolWindow)
          {
            twComponents.ToolWindow.Size = tpSize.Inflate(14, 14);
          }
          else
          {
            twComponents.ToolWindow.Size = uiWindow.WindowLocation.Size.ToSize();
          }

          Rectangle toolWindowRectangle = new Rectangle(uiWindow.WindowLocation.Location, twComponents.ToolWindow.Size);
          Rectangle visibleOverlap = totalScreenArea;
          visibleOverlap.Intersect(toolWindowRectangle);

          if (visibleOverlap.IsEmpty)
          {
            uiWindow.WindowLocation.Location = defaultLocation;
            defaultLocation.Offset(50, 50);
          }

          while (uiWindow.WindowLocation.Location.X > totalScreenArea.Width)
          {
            uiWindow.WindowLocation.Location = new Point(uiWindow.WindowLocation.Location.X - 200, uiWindow.WindowLocation.Location.Y);
            if (uiWindow.WindowLocation.Location.X < 200)
              break;
          }

          twComponents.ToolWindow.Location = uiWindow.WindowLocation.Location;
          twComponents.ToolWindow.Visible = uiWindow.WindowLocation.IsVisible;

          // removed the tabs if any tabs are being used - maybe multi-tabbed editor??
          switch (toolName)
          {

            case "BmsEditor":
              //tabMain.TabPages.Remove(tabPagePdfViewer);
              break;

            case "EditorPanel":
              //tabMain.TabPages.Remove(tabPagePdfViewer);
              break;

            case "ControlPanel":
              //tabMain.TabPages.Remove(tabPagePdfViewer);
              break;
          }
        }
      }

      MainFormHelper.InitializeUIState(this, _twMgr);
      base.SetToolWindows(_twMgr);
    }

    private void AddButtonsToToolWindow(string name, RichTextViewer rtv)
    {
      switch (name)
      {
        case "RawExtractedText":
          rtv.TopPanel.Height = 50;
          Button btnFindPatterns = new Button();
          btnFindPatterns.Name = "btnFindPatterns";
          btnFindPatterns.Tag = "FindPatterns";
          btnFindPatterns.Text = "Find Patterns";
          btnFindPatterns.Click += Action;
          rtv.TopPanel.Controls.Add(btnFindPatterns);
          btnFindPatterns.Size = new Size(100, 23);
          btnFindPatterns.Location = new Point(15, 5);
          break;

        case "FormatRecognition":
          break;

        case "ConfigEdit":
          rtv.TopPanel.Height = 35;
          Button btnSaveConfig = new Button();
          btnSaveConfig.Name = "btnSaveConfig";
          btnSaveConfig.Tag = "SaveConfig";
          btnSaveConfig.Text = "Save Config";
          btnSaveConfig.Click += Action;
          rtv.TopPanel.Controls.Add(btnSaveConfig);
          btnSaveConfig.Size = new Size(100, 23);
          btnSaveConfig.Location = new Point(15, 5);

          Label lblConfigType = new Label();
          lblConfigType.Name = "lblConfigType";
          lblConfigType.AutoSize = false;
          rtv.TopPanel.Controls.Add(lblConfigType);
          lblConfigType.Size = new Size(100, 23);
          lblConfigType.Location = new Point(130, 5);
          lblConfigType.Text = String.Empty;

          Label lblFullXmlPath = new Label();
          lblFullXmlPath.AutoSize = false;
          lblFullXmlPath.Name = "lblFullXmlPath";
          rtv.TopPanel.Controls.Add(lblFullXmlPath);
          lblFullXmlPath.Size = new Size(300, 23);
          lblFullXmlPath.Location = new Point(250, 5);
          lblFullXmlPath.Text = String.Empty;

          Label lblFullFilePath = new Label();
          lblFullFilePath.Name = "lblFullFilePath";
          lblFullFilePath.AutoSize = false;
          rtv.TopPanel.Controls.Add(lblFullFilePath);
          lblFullFilePath.Size = new Size(400, 23);
          lblFullFilePath.Location = new Point(15, 30);
          lblFullFilePath.Text = String.Empty;
          break;
      }

    }

    private void ToolPanel_NotifyHostEvent(ToolPanelNotifyEventArgs e)
    {
      if (e.Sender == null || e.Sender.Tag == null)
        return;

      string tag = e.Sender.Tag.ToString();
      if (tag.IsBlank())
        return;

      string command = e.Command.ToString();
      string toolWindowAction = "TW_" + command + "_" + tag.Replace("ToolPanel_", String.Empty);


      ToolWindowAction(toolWindowAction, e.ToolPanelUpdateParms);
    }

    private void ToolWindowAction(string action, ToolPanelUpdateParms parms = null)
    {
      string[] tokens = action.Split('_');

      if (tokens.Length != 3)
        return;

      if (tokens[0] != "TW")
        return;

      string toolWindowAction = tokens[1];

      if (toolWindowAction == "ReloadConfigs")
      {
        // commands from the tool windows

        //ReloadTextProcessingConfigs();
        //ProcessFile();
        return;
      }

      string toolWindowTarget = tokens[2];

      List<string> toolTargets = new List<string>();

      if (toolWindowTarget == "All")
      {
        toolTargets.Add("BmsEditor");
      }
      else
      {
        toolTargets.Add(toolWindowTarget);
      }

      foreach (string toolTarget in toolTargets)
      {
        if (!_uiState.UIWindowSet.ContainsKey(toolTarget))
          return;

        UIWindow uiWindow = _uiState.UIWindowSet[toolTarget];

        if (_twMgr == null || _twMgr.ToolWindowComponentsSet == null || !_twMgr.ToolWindowComponentsSet.ContainsKey(toolTarget))
          return;

        var twc = _twMgr.ToolWindowComponentsSet[toolTarget];

        string dockingTargetName = "DockedTarget_" + toolTarget;

        switch (toolWindowAction)
        {
          case "Dock":
            if (twc.DockedTarget != null)
            {
              twc.ToolWindow.Visible = false;
              twc.ToolWindow.DockPanel.Controls.Remove(twc.ToolPanel);
              twc.DockedTarget.Controls.Clear();
              twc.DockedTarget.Controls.Add(twc.ToolPanel);
              twc.ToolPanel.Dock = DockStyle.Fill;
              uiWindow.WindowLocation.IsDocked = true;

              int tabInsertIndex = GetTabInsertIndex(toolTarget);

              switch (toolTarget)
              {
                case "Editor1":
                  if (!tabMain.TabPages.Contains(tabPageEditor1))
                    tabMain.TabPages.Insert(tabInsertIndex, tabPageEditor1);
                  tabMain.SelectedTab = tabPageEditor1;
                  tabPageEditor1.Focus();
                  break;

                case "ControlPanel":
                  if (!tabMain.TabPages.Contains(tabPageControlPanel))
                    tabMain.TabPages.Insert(tabInsertIndex, tabPageControlPanel);
                  tabMain.SelectedTab = tabPageControlPanel;
                  tabPageControlPanel.Focus();
                  break;
              }

              twc.ToolPanel.UpdateDockButtonTagAndText();
              break;
            }
            else
            {
              if (twc.ToolWindow.Visible)
                twc.ToolWindow.Visible = false;
            }
            break;

          case "Float":
            if (twc.FloatedTarget != null)
            {
              twc.DockedTarget.Controls.Remove(twc.ToolPanel);
              twc.ToolWindow.DockPanel.Controls.Clear();
              twc.ToolWindow.DockPanel.Controls.Add(twc.ToolPanel);
              twc.ToolWindow.Location = uiWindow.WindowLocation.Location;
              twc.ToolWindow.Size = uiWindow.WindowLocation.Size.ToSize();
              twc.ToolPanel.Dock = DockStyle.Fill;
              uiWindow.WindowLocation.IsDocked = false;

              switch (toolTarget)
              {
                case "Editor1":
                  if (tabMain.TabPages.Contains(tabPageEditor1))
                    tabMain.TabPages.Remove(tabPageEditor1);
                  break;

                case "ControlPanel":
                  if (tabMain.TabPages.Contains(tabPageControlPanel))
                    tabMain.TabPages.Remove(tabPageControlPanel);
                  break;
              }

              twc.ToolWindow.Visible = true;
            }
            else
            {
              if (!twc.ToolWindow.Visible)
                twc.ToolWindow.Visible = true;
            }
            twc.ToolPanel.UpdateDockButtonTagAndText();
            break;

          case "Hide":
            twc.ToolWindow.Visible = false;
            uiWindow.WindowLocation.IsVisible = twc.ToolWindow.Visible;
            break;

          case "Show":
            twc.ToolWindow.Visible = true;
            uiWindow.WindowLocation.IsVisible = twc.ToolWindow.Visible;
            break;

          case "Toggle":
            twc.ToolWindow.Visible = !twc.ToolWindow.Visible;
            uiWindow.WindowLocation.IsVisible = twc.ToolWindow.Visible;
            break;

          case "UpdateToolWindow":
            if (parms == null || parms.ToolPanelName.IsBlank())
              return;

            if (_twMgr == null || _twMgr.ToolPanels == null || !_twMgr.ToolPanels.ContainsKey(parms.ToolPanelName))
              return;

            var targetToolPanel = _twMgr.ToolPanels[parms.ToolPanelName];
            ExecuteToolWindowCommand(targetToolPanel, parms);
            break;
        }
      }
    }

    private void ExecuteToolWindowCommand(ToolPanelBase toolPanel, ToolPanelUpdateParms parms)
    {
      if (toolPanel.Tag == null)
        return;

      string toolPanelTag = toolPanel.Tag.ToString().Replace("ToolPanel_", String.Empty);
      string command = parms.Command;

      switch (toolPanelTag)
      {
        case "ConfigEdit":
          var configEditToolPanel = toolPanel as RichTextViewer;
          //string path = parms.ConfigFullXmlPath;

          switch (command)
          {
            case "Sent-Tool-Window-Command":
              // code response to the command here...
              break;
          }

          break;
      }
    }

    private int GetTabInsertIndex(string tabName)
    {
      for (int i = 0; i < _tabPageOrder.Count; i++)
      {
        if (_tabPageOrder[i] == tabName)
          return i;
      }

      return 0;
    }


    public void ToolWindow_NotifyHostEvent(ToolPanelNotifyEventArgs e)
    {
      if (e == null)
        return;
    }

    private void ToolWindow_ToolAction(ToolActionEventArgs e)
    {
      if (e == null)
        return;

      frmToolWindowBase toolWindow = e.ToolWindow as frmToolWindowBase;
      string name = toolWindow.Tag.ToString().Replace("ToolWindow_", String.Empty);

      switch (e.ToolActionEvent)
      {
        case ToolActionEvent.ToolWindowMoved:
          _uiState.UIWindowSet[name].WindowLocation.Location = toolWindow.Location;
          break;

        case ToolActionEvent.ToolWindowResized:
          _uiState.UIWindowSet[name].WindowLocation.Size = toolWindow.Size;
          break;

        case ToolActionEvent.ToolWindowVisibleChanged:
          _uiState.UIWindowSet[name].WindowLocation.IsVisible = toolWindow.Visible;
          break;
      }
    }

    #endregion

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      try
      {
        if (g.AppConfig.IsUpdated)
          g.AppConfig.Save();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to save the updated AppConfig file." + g.crlf2 +
                        "Exception Message:" + ex.ToReport(), "TerminalTest - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {
        if (_twMgr != null)
          _twMgr.MarkTimeToClose();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while closing the main form and child forms." + g.crlf2 +
                        "Exception Message:" + ex.ToReport(), "TerminalTest - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_isFirstShowing)
        return;

      this.Focus();
      this.BringToFront();


      if (_twMgr.ToolPanels.ContainsKey("Editor1"))
      {
        var tp = _twMgr.ToolPanels["Editor1"] as EditorPanel;
        tp.SetInitialFocus();
      }

      _isFirstShowing = false;
    }

    private void tabMain_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (tabMain.SelectedTab.Tag == null)
        return;

      string selectedTab = tabMain.SelectedTab.Tag.ToString();

      switch (tabMain.SelectedTab.Tag.ToString())
      {
        case "TabPage_Editor1":
          ToolWindowAction("TW_Float_Editor1");
          break;

        case "TabPage_ControlPanel":
          ToolWindowAction("TW_Float_ControlPanel");
          break;
      }
    }

    private void frmMain_ResizeEnd(object sender, EventArgs e)
    {
      if (_editorPanel != null)
        _editorPanel.MainFormResizeEnd();

      var uiWindow = _uiState.UIWindowSet["MainForm"];

      switch (_uiState.UIWindowSet["MainForm"].WindowLocation.SizeMode)
      {
        case SizeMode.Literal:
          var windowLocation = new WindowLocation();
          windowLocation.SizeMode = uiWindow.WindowLocation.SizeMode;
          windowLocation.Location = uiWindow.WindowLocation.Location;
          windowLocation.Size = new Size(this.Width, this.Height);
          uiWindow.WindowLocation = windowLocation;
          break;

        case SizeMode.PercentOfScreen:

          break;
      }
    }

    private void frmMain_LocationChanged(object sender, EventArgs e)
    {
      if (_moveTimer == null)
      {
        _moveTimer = new System.Timers.Timer();
        _moveTimer.Enabled = false;
        _moveTimer.Interval = 500;
        _moveTimer.Elapsed += MoveTimer_Elapsed;
      }

      _moveTimer.Enabled = false;
      _moveTimer.Enabled = true;
    }

    private void MoveTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      _moveTimer.Enabled = false;

      _uiState.UIWindowSet["MainForm"].WindowLocation.Location = this.Location;
    }

    private void BumpFontSizeUp()
    {
      if (udFontSize.Value < udFontSize.Maximum)
        udFontSize.Value = udFontSize.Value + 1;
    }

    private void BumpFontSizeDown()
    {
      if (udFontSize.Value > udFontSize.Minimum)
        udFontSize.Value = udFontSize.Value - 1;
    }

    private void FontMgmtChanged(object sender, EventArgs e)
    {
      int fontSize = Convert.ToInt32(udFontSize.Value);
      int charWidth = Convert.ToInt32(udCharWidth.Value);
      int charHeight = Convert.ToInt32(udCharHeight.Value);

      float fontSizeToWidth = (float) 11 / 15;
      float fontSizeToHeight = (float) 20 / 15;

      string fontControl = ((NumericUpDown)sender).Tag.DbToString();

      switch (fontControl)
      {
        case "FontSize":
          _suppressCharWidthChange = true;
          udCharWidth.Value = Convert.ToInt32((float)fontSize * fontSizeToWidth);
          udCharHeight.Value = Convert.ToInt32((float)fontSize * fontSizeToHeight);
          break;

        case "CharWidth":
          if (_suppressCharWidthChange)
          {
            _suppressCharWidthChange = false;
            return;
          }
          _editorPanel.SetFontSpec(new FontSpec(fontSize, new Size(charWidth, charHeight)));
          break;

        case "CharHeight":
          _editorPanel.SetFontSpec(new FontSpec(fontSize, new Size(charWidth, charHeight)));
          break;
      }
    }

    private void ckShowFields_CheckedChanged(object sender, EventArgs e)
    {
      _editorPanel.ShowFields(ckShowFields.Checked);
    }
  }
}

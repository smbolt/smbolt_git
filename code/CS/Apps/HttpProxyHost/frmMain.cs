using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TW;
using Org.TW.Forms;
using Org.TW.ToolPanels;
using Org.GS.UI;
using Org.GS;

namespace HttpProxyHost
{
  public partial class frmMain : Form
  {

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
      switch (sender.ActionTag())
      {

        case "Exit":
          this.Close();
          break;
      }
    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the creation of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "HTTP Proxy Host - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {


        InitializeToolWindowForms();

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application." + g.crlf2 + ex.ToReport(),
                        "HTTP Proxy Host - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


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
        if (uiWindow.IsMainForm)
        {
          MainFormHelper.ManageInitialSize(this, uiWindow);
          continue;
        }

        string toolName = uiWindow.Name.Trim();

        if (_twMgr.ToolWindowComponentsSet.ContainsKey(toolName))
          throw new Exception("A duplicate name exists in the set of UIWindow configurations in the AppConfig file. The duplicate name is '" + toolName + "'.");

        if (!dockedPanels.ContainsKey("DockTarget_" + toolName))
          throw new Exception("No panel has been designated for docking the tool window control named '" + toolName + "'.");

        var twComponents = new ToolWindowComponents();
        twComponents.Name = toolName;

        switch (uiWindow.TypeName)
        {
          case "PdfViewer":
            twComponents.ToolPanel = new PdfViewerControl();
            break;

          case "RichTextViewer":
            twComponents.ToolPanel = new RichTextViewer();
            RichTextViewer rtv = (RichTextViewer)twComponents.ToolPanel;
            AddButtonsToToolWindow(uiWindow.Name, rtv);
            break;

          case "TextExtractDesigner":
            twComponents.ToolPanel = new TextExtractDesigner();
            break;

            // other types in Org.TW may be possible - implement later, or as needed.
        }

        twComponents.ToolWindow = new frmToolWindowBase(this, uiWindow.WindowTitle);
        twComponents.ToolWindow.Owner = this;
        twComponents.ToolWindow.Tag = "ToolWindow_" + toolName;
        twComponents.ToolWindow.ToolAction += ToolWindow_ToolAction;
        twComponents.ToolPanel.Tag = "ToolPanel_" + toolName;
        twComponents.ToolPanel.NotifyHostEvent += ToolPanel_NotifyHostEvent;
        twComponents.ToolPanel.DockButton.Click += Action;
        twComponents.FloatedTarget = twComponents.ToolWindow.DockPanel;
        twComponents.DockedTarget = dockedPanels["DockTarget_" + toolName];
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
            twComponents.ToolWindow.Size = tpSize.Inflate(14, 14);
          else
            twComponents.ToolWindow.Size = uiWindow.WindowLocation.Size.ToSize();

          Rectangle toolWindowRectangle = new Rectangle(uiWindow.WindowLocation.Location, twComponents.ToolWindow.Size);
          Rectangle visibleOverlap = totalScreenArea;
          visibleOverlap.Intersect(toolWindowRectangle);

          if (visibleOverlap.IsEmpty)
          {
            uiWindow.WindowLocation.Location = defaultLocation;
            defaultLocation.Offset(50, 50);
          }

          twComponents.ToolWindow.Location = uiWindow.WindowLocation.Location;
          twComponents.ToolWindow.Visible = uiWindow.WindowLocation.IsVisible;

          switch (toolName)
          {

            case "PdfViewer":
              tabMain.TabPages.Remove(tabPagePdfViewer);
              break;

            case "RawExtractedText":
              tabMain.TabPages.Remove(tabPageRawExtractedText);
              break;

            case "TextExtractDesigner":
              tabMain.TabPages.Remove(tabPageTextExtractDesigner);
              break;

            case "TextExtractResults":
              tabMain.TabPages.Remove(tabPageTextExtractResults);
              break;
          }
        }
      }

      MainFormHelper.InitializeUIState(this, _twMgr);
      base.SetToolWindows(_twMgr);
    }
  }
}

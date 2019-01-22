using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;

namespace Org.TW.ToolPanels
{
  public partial class ToolPanelBase : UserControl
  {
    public string DerivedTagName { get; set; }
    public event Action<ToolPanelNotifyEvent> NotifyHostEvent;
    public Button DockButton { get { return this.btnDockFloat; } }
		public Panel TopPanel { get { return this.pnlTopControl; } }

    public bool IsDockedInToolWindow
    {
      get { return Get_IsDockedInToolWindow(); }
    }

		public ToolPanelBase()
		{
			InitializeComponent();
		}

    public ToolPanelBase(string derivedTagName = "NotSet")
    {
      InitializeComponent();
      this.DerivedTagName = derivedTagName;
    }

    private bool Get_IsDockedInToolWindow()
    {
      if (this.Parent == null)
        return false;

      object grandParent = this.Parent.Parent;

      if (this.Parent == null)
        return false;

      if (grandParent.GetType().Name == "frmToolWindowBase")
        return true;

      return false;
    }

    public void NotifyHost(ToolPanelBase sender, ToolPanelHostCommand command, ToolPanelUpdateParms toolWindowUpdateParms = null)
    {
      if (this.NotifyHostEvent == null)
        return;

      NotifyHostEvent(new ToolPanelNotifyEvent(sender, command, toolWindowUpdateParms));
    }

    public void UpdateDockButtonTagAndText()
    {
      this.btnDockFloat.Tag = "TW_" + (this.IsDockedInToolWindow ? "Dock" : "Float") + "_" + this.DerivedTagName;
      this.btnDockFloat.Text = (this.IsDockedInToolWindow ? "Dock" : "Float"); 
    }

    private void ToolPanelBase_DockChanged(object sender, EventArgs e)
    {
      UpdateDockButtonTagAndText();
    }
  }
}

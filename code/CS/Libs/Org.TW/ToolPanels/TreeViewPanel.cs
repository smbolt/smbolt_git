using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.TW.ToolPanels
{
  public partial class TreeViewPanel : ToolPanelBase
  {
    public bool IsLoading {
      get;
      set;
    }

    public TreeView TreeView
    {
      get {
        return this.tvDoc;
      }
    }

    public TreeViewPanel()
    {
      InitializeComponent();
      InitializeControl();
    }

    private void InitializeControl()
    {
      this.pnlTop.Visible = false;
      this.pnlBottom.Visible = false;
      this.IsLoading = false;
    }

    private void tvDoc_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (this.IsLoading)
        return;

      if (e == null)
        return;

      if (e.Node.Tag == null)
        return;

      string tag = e.Node.Tag.ToString();

      base.NotifyHost(this, ToolPanelHostCommand.UpdateToolWindow, null);
    }
  }
}

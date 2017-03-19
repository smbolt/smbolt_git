using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TW.Forms;
using Org.TW.ToolPanels;
using Org.GS;

namespace Org.TW
{
  public class ToolWindowManager
  {
    public ToolWindowComponentsSet ToolWindowComponentsSet { get; set; }

    private Dictionary<string, frmToolWindowBase> _toolWindows;
    public Dictionary<string, frmToolWindowBase> ToolWindows { get { return Get_ToolWindows(); } }

    private Dictionary<string, ToolPanelBase> _toolPanels;
    public Dictionary<string, ToolPanelBase> ToolPanels { get { return Get_ToolPanels(); } }

    private Dictionary<string, Panel> _dockedTargets;
    public Dictionary<string, Panel> DockedTargets { get { return Get_DockedTargets(); } }

    private Dictionary<string, Panel> _floatedTargets;
    public Dictionary<string, Panel> FloatedTargets { get { return Get_FloatedTargets(); } }

    public ToolWindowManager()
    {
      this.ToolWindowComponentsSet = new ToolWindowComponentsSet();
    }

    public void MarkTimeToClose()
    {
      foreach (var toolWindow in this.ToolWindows.Values)
      {
        if (!toolWindow.IsDisposed)
          toolWindow.MarkTimeToClose();
      }
    }

    public ToolPanelBase GetToolPanel(string name)
    {
      if (_toolPanels == null)
        Get_ToolPanels();
      if (_toolPanels.ContainsKey(name))
        return _toolPanels[name];
      return null;
    }

    private Dictionary<string, frmToolWindowBase> Get_ToolWindows()
    {
      if (_toolWindows != null)
        return _toolWindows;

      _toolWindows = new Dictionary<string, frmToolWindowBase>();

      foreach(var kvp in this.ToolWindowComponentsSet)
      {
        if (!_toolWindows.ContainsKey(kvp.Key) && kvp.Value.ToolWindow != null)
        {
          _toolWindows.Add(kvp.Key, kvp.Value.ToolWindow);
        }
      }

      return _toolWindows;
    }

    private Dictionary<string, ToolPanelBase> Get_ToolPanels()
    {
      if (_toolPanels != null)
        return _toolPanels;

      _toolPanels = new Dictionary<string, ToolPanelBase>();

      foreach(var kvp in this.ToolWindowComponentsSet)
      {
        if (!_toolPanels.ContainsKey(kvp.Key) && kvp.Value.ToolWindow != null)
        {
          _toolPanels.Add(kvp.Key, kvp.Value.ToolPanel);
        }
      }

      return _toolPanels;
    }

    private Dictionary<string, Panel> Get_DockedTargets()
    {
      if (_dockedTargets != null)
        return _dockedTargets;

      _dockedTargets = new Dictionary<string, Panel>();

      foreach(var kvp in this.ToolWindowComponentsSet)
      {
        if (!_dockedTargets.ContainsKey(kvp.Key) && kvp.Value.DockedTarget != null)
        {
          _dockedTargets.Add(kvp.Key, kvp.Value.DockedTarget);
        }
      }

      return _dockedTargets;
    }

    private Dictionary<string, Panel> Get_FloatedTargets()
    {
      if (_floatedTargets != null)
        return _floatedTargets;

      _floatedTargets = new Dictionary<string, Panel>();

      foreach(var kvp in this.ToolWindowComponentsSet)
      {
        if (!_floatedTargets.ContainsKey(kvp.Key) && kvp.Value.FloatedTarget != null)
        {
          _floatedTargets.Add(kvp.Key, kvp.Value.FloatedTarget);
        }
      }

      return _floatedTargets;
    }

  }
}

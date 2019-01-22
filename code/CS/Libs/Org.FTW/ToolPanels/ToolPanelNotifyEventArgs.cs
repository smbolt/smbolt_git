using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.FTW.ToolPanels
{
  public enum ToolPanelHostCommand
  {
    Dock,
    Float,
    UpdateToolWindow,
    ReloadConfigs,
  }

  public class ToolPanelNotifyEventArgs
  {
    public ToolPanelBase Sender;
    public ToolPanelHostCommand Command;
    public ToolPanelUpdateParms ToolPanelUpdateParms;

    public ToolPanelNotifyEventArgs(ToolPanelBase sender, ToolPanelHostCommand command)
    {
      this.Sender = sender;
      this.Command = command;
      this.ToolPanelUpdateParms = null;
    }

    public ToolPanelNotifyEventArgs(ToolPanelBase sender, ToolPanelHostCommand command, ToolPanelUpdateParms parms)
    {
      this.Sender = sender;
      this.Command = command;
      this.ToolPanelUpdateParms = parms;
    }
  }
}
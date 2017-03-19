using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.TextProcessing;
using Org.GS;

namespace Org.TW.ToolPanels
{
  public enum ToolPanelHostCommand
  {
    Dock,
    Float,
    UpdateToolWindow
  }


  public class ToolPanelNotifyEvent
  {
    public ToolPanelBase Sender;
    public ToolPanelHostCommand Command;
    public ToolPanelUpdateParms ToolPanelUpdateParms;

    public ToolPanelNotifyEvent(ToolPanelBase sender, ToolPanelHostCommand command)
    {
      this.Sender = sender;
      this.Command = command;
      this.ToolPanelUpdateParms = null;
    }

    public ToolPanelNotifyEvent(ToolPanelBase sender, ToolPanelHostCommand command, ToolPanelUpdateParms parms)
    {
      this.Sender = sender;
      this.Command = command;
      this.ToolPanelUpdateParms = parms;
    }
  }
}
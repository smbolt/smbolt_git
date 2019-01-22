using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Org.GS.UI;

namespace Org.GS
{
  public enum ToolActionEvent
  {
    ToolWindowMoved,
    ToolWindowResized,
    ToolWindowVisibleChanged
  }

  public class ToolActionEventArgs
  {
    public Form ToolWindow {
      get;
      set;
    }
    public ToolActionEvent ToolActionEvent {
      get;
      set;
    }

    public ToolActionEventArgs(Form form, ToolActionEvent toolActionEvent)
    {
      this.ToolWindow = form;
      this.ToolActionEvent = toolActionEvent;
    }
  }
}

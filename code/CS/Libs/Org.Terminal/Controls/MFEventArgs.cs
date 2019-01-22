using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.Terminal.Controls
{
  public class MFEventArgs
  {
    public MFBase Sender {
      get;
      set;
    }
    public MFContainer MFContainer {
      get;
      set;
    }
    public EventType EventType {
      get;
      set;
    }
    public KeyEventArgs KeyEventArgs {
      get;
      set;
    }
    public KeyPressEventArgs KeyPressEventArgs {
      get;
      set;
    }
    public PaintEventArgs PaintEventArgs {
      get;
      set;
    }
    public MouseEventArgs MouseEventArgs {
      get;
      set;
    }
    public EventArgs EventArgs {
      get;
      set;
    }
    public EventCommand EventCommand {
      get;
      set;
    }
    public string EventMessage {
      get;
      set;
    }

    public MFEventArgs(MFBase sender, EventType eventType, object eventArgs = null)
    {
      this.Sender = sender;
      this.EventType = eventType;
      SetEventArgs(eventArgs);
      this.EventCommand = EventCommand.NotSet;
      this.EventMessage = String.Empty;
    }

    public MFEventArgs(MFContainer mfContainer, EventType eventType, object eventArgs = null)
    {
      this.Sender = null;
      this.MFContainer = mfContainer;
      this.EventType = eventType;
      SetEventArgs(eventArgs);
      this.EventCommand = EventCommand.NotSet;
      this.EventMessage = String.Empty;
    }

    public MFEventArgs(MFBase sender, EventType eventType, object eventArgs, EventCommand eventCommand, string eventMessage)
    {
      this.Sender = sender;
      this.EventType = eventType;
      SetEventArgs(eventArgs);
      this.EventCommand = eventCommand;
      this.EventMessage = eventMessage;
    }

    private void SetEventArgs(object eventArgs)
    {
      if (eventArgs == null)
        return;

      string eventArgsTypeName = eventArgs.GetType().Name;

      switch (eventArgsTypeName)
      {
        case "KeyEventArgs":
          this.KeyEventArgs = (KeyEventArgs)eventArgs;
          break;

        case "KeyPressEventArgs":
          this.KeyPressEventArgs = (KeyPressEventArgs)eventArgs;
          break;

        case "PaintEventArgs":
          this.PaintEventArgs = (PaintEventArgs)eventArgs;
          break;

        case "EventArgs":
          this.EventArgs = (EventArgs)eventArgs;
          break;

        case "MouseEventArgs":
          this.MouseEventArgs = (MouseEventArgs)eventArgs;
          break;

        default:
          throw new Exception("EventArgs object of type '" + eventArgsTypeName + "' is not implemented.");
      }
    }
  }
}

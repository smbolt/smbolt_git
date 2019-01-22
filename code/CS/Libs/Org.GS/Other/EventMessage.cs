using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class EventMessage
  {
    public string MessageText {
      get;
      set;
    }

    public EventMessage()
    {
      this.MessageText = String.Empty;
    }

    public EventMessage(string messageText)
    {
      this.MessageText = messageText;
    }
  }
}

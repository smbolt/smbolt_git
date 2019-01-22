using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS.Logging
{
  public class Message
  {
    public string Code {
      get;
      set;
    }
    public string Text {
      get;
      set;
    }

    public Message()
    {
      Code = "0000";
      Text = String.Empty;
    }
  }
}

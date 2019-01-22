using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.CTL
{
  public class ControlProperties
  {
    public string Name {
      get;
      set;
    }
    public string Text {
      get;
      set;
    }
    public string Tag {
      get;
      set;
    }

    public ControlProperties()
    {
      this.Name = String.Empty;
      this.Text = String.Empty;
      this.Tag = String.Empty;
    }
  }
}

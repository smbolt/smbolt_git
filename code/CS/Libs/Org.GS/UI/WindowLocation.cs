using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Org.GS;

namespace Org.GS.UI
{
  [XMap(XType = XType.Element)]
  public class WindowLocation
  {
    [XMap(DefaultValue = "False")]
    public bool IsVisible {
      get;
      set;
    }

    [XMap(DefaultValue = "False")]
    public bool IsDocked {
      get;
      set;
    }

    [XMap(DefaultValue = "Literal")]
    public SizeMode SizeMode {
      get;
      set;
    }

    [XMap]
    public Point Location {
      get;
      set;
    }

    [XMap]
    public SizeF Size {
      get;
      set;
    }

    public WindowLocation()
    {
      this.IsVisible = true;
      this.IsDocked = false;
      this.SizeMode = SizeMode.Literal;
      this.Location = new Point(0, 0);
      this.Size = new SizeF(0.0F, 0.0F);
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.RPT
{
  [XMap(XType = XType.Element)]
  public class ReportObjectLocation
  {
    [XMap(DefaultValue = "")]
    public string ColRef {
      get;
      set;
    }

    [XMap(DefaultValue = "0")]
    public float Left {
      get;
      set;
    }

    [XMap(DefaultValue = "0")]
    public float Top {
      get;
      set;
    }

    [XMap(DefaultValue = "Relative")]
    public HeightMode HeightMode {
      get;
      set;
    }

    public ReportObjectLocation()
    {
      this.ColRef = String.Empty;
      this.Left = 0.0F;
      this.Top = 0.0F;
      this.HeightMode = HeightMode.Relative;
    }
  }
}

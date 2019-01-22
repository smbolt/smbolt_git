using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.RPT
{
  [XMap(XType = XType.Attribute)]
  public class ReportObjectSize
  {
    [XMap]
    public float Width { get; set; }

    [XMap(DefaultValue = "0")]
    public float Height { get; set; }

    [XMap(DefaultValue = "Fixed")]
    public HeightSizeMode HeightSizeMode { get; set; }

    [XMap(DefaultValue = "Full")]
    public WidthSizeMode WidthSizeMode { get; set; }

    public ReportObjectSize()
    {
      this.Width = 0.0F;
      this.Height = 0.0F;
      this.HeightSizeMode = HeightSizeMode.Fixed;
      this.WidthSizeMode = WidthSizeMode.Full; 
    }
  }
}

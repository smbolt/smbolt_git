using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.RPT
{
  public enum ReportObjectType
  {
    ReportDef,
    ReportText,
    Rectangle,
    PageBorder
  }

  public enum HeightMode
  {
    Relative = 0,
    Absolute
  }

  public enum HeightSizeMode
  {
    Grow = 0,
    Fixed
  }

  public enum WidthSizeMode
  {
    Full = 0,
    Fixed
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS.Logging
{
  public class Location
  {
    public string Program { get; set; }
    public string At { get; set; }
    public string In { get; set; }

    public Location()
    {
      Program = String.Empty;
      At = String.Empty;
      In = String.Empty;
    }
  }
}

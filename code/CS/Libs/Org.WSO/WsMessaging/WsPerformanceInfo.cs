using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.WSO
{
  public class WsPerformanceInfo
  {
    public DateTime DateTime {
      get;
      set;
    }
    public string Label {
      get;
      set;
    }


    public WsPerformanceInfo()
    {
      this.DateTime = DateTime.MinValue;
      this.Label = String.Empty;
    }

    public WsPerformanceInfo(string label)
    {
      this.DateTime = DateTime.Now;
      this.Label = label;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class PerformanceInfo
  {
    public DateTime DateTime {
      get;
      set;
    }
    public string Label {
      get;
      set;
    }

    public PerformanceInfo()
    {
      this.DateTime = DateTime.MinValue;
      this.Label = String.Empty;
    }

    public PerformanceInfo(string label)
    {
      this.DateTime = DateTime.Now;
      this.Label = label;
    }
  }
}
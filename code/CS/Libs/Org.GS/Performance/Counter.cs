using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Org.GS;

namespace Org.GS.Performance
{
  [XMap(XType=XType.Element, CompositeKey="Key")]
  public class Counter
  {
    [XMap]
    public string CategoryName { get; set; }
    
    [XMap]
    public string CounterName { get; set; }

    [XMap]
    public PerformanceCounterType CounterType { get; set; }

    [XMap]
    public string InstanceName { get; set; }

    public string Key
    {
      get { return Get_Key(); }
    }

    public string DisplayKey
    {
      get { return Get_DisplayKey(); }
    }

    public Counter()
    {
      this.CategoryName = String.Empty;
      this.CounterName = String.Empty;
      this.CounterType = PerformanceCounterType.NumberOfItems32;
      this.InstanceName = String.Empty;
    }

    private string Get_Key()
    {
      string key = this.CategoryName.Trim() + "|||" + this.CounterName.Trim();

      if (this.InstanceName.Trim().IsNotBlank())
        key = "[" + this.InstanceName.Trim() + "]" + key;

      return key;
    }

    private string Get_DisplayKey()
    {
      string displayKey = this.CounterName.Trim();

      if (this.InstanceName.Trim().IsNotBlank())
        displayKey = "[" + this.InstanceName.Trim() + "]" + displayKey;

      return displayKey;
    }
  }
}

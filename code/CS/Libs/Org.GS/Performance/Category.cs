using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Org.GS.Performance
{
  [XMap(XType=XType.Element)]
  public class Category
  {
    [XMap(IsKey=true)]
    public string CategoryName {
      get;
      set;
    }

    [XMap]
    public PerformanceCounterCategoryType CategoryType {
      get;
      set;
    }

    [XMap(XType=XType.Element, CollectionElements="Counter")]
    public CounterSet CounterSet {
      get;
      set;
    }

    public Category()
    {
      this.CategoryName = String.Empty;
      this.CategoryType = PerformanceCounterCategoryType.Unknown;
      this.CounterSet = new CounterSet();
    }
  }
}

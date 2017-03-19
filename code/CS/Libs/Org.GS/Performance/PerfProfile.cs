using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Performance
{
  [XMap(XType=XType.Element)]
  public class PerfProfile
  {
    [XMap(IsKey=true)]
    public string ProfileName { get; set; }

    [XMap(XType=XType.Element, CollectionElements="Category")]
    public CategorySet CategorySet { get; set; }

    public int TotalCounters
    {
      get { return Get_TotalCounters(); }
    }

    public PerfProfile()
    {
      this.ProfileName = String.Empty;
      this.CategorySet = new CategorySet();
    }

    private int Get_TotalCounters()
    {
      int totalCounters = 0;

      foreach (Category c in this.CategorySet.Values)
        totalCounters += c.CounterSet.Count;

      return totalCounters;
    }
  }
}

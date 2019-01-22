using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
  [Serializable]
  public class MetricGraphCollection : System.Collections.Generic.SortedList<string, MetricGraphConfiguration>
  {
    private int _nextNumber;
    public int NextNumber
    {
      get {
        return ++_nextNumber;
      }
      set {
        _nextNumber = value;
      }
    }



  }
}

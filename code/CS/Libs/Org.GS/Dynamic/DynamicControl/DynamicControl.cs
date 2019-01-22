using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.GS.Dynamic
{
  [XMap(XType=XType.Element)]
  public class DynamicControl
  {
    [XMap(XType=XType.Element, CollectionElements="Control", WrapperElement="ControlSet")]
    public ControlSet ControlSet {
      get;
      set;
    }

    [XMap(XType=XType.Element, CollectionElements="EventMap", WrapperElement="EventMapSet")]
    public EventMapSet EventMapSet {
      get;
      set;
    }

    public bool IsEmpty
    {
      get {
        return Get_IsEmpty();
      }
    }

    public DynamicControl()
    {
      this.ControlSet = new ControlSet(this);
      this.EventMapSet = new EventMapSet();
    }

    private bool Get_IsEmpty()
    {
      if (this.ControlSet.Count > 0)
        return false;

      if (this.EventMapSet != null)
        return false;

      return true;
    }
  }
}

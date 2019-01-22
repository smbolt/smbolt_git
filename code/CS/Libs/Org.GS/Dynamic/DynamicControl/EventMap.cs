using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.GS.Dynamic
{
  public enum EventMapClass
  {
    NotSet,
    Derived,
    Base
  }

  [XMap(XType=XType.Element)]
  public class EventMap
  {
    [XMap]
    public string Tag {
      get;
      set;
    }
    [XMap]
    public string Method {
      get;
      set;
    }
    [XMap]
    public EventMapClass EventMapClass {
      get;
      set;
    }

    public EventMap()
    {
      this.Tag = String.Empty;
      this.Method = String.Empty;
      this.EventMapClass = EventMapClass.NotSet;
    }
  }
}

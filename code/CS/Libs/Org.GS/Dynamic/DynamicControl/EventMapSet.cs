using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.GS.Dynamic
{
  [XMap(XType = XType.Element, CollectionElements="EventMap")]
  public class EventMapSet : List<EventMap>
  {
    public EventMapSet()
    {
    }
  }
}

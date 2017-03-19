using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  [Serializable()]
  [XMap(XType = XType.Element, CollectionElements = "WinService", WrapperElement = "WinServiceList")]
  public class WinServiceSet : List<WinService>
  {
  }
}

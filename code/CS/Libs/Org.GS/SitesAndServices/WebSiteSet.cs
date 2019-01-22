using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  [Serializable()]
  [XMap(XType = XType.Element, CollectionElements = "WebSite")]
  public class WebSiteSet : List<WebSite>
  {
  }
}

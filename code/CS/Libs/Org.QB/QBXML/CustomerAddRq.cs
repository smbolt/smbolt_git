using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.QB.QBXML
{
  [XMap (XType = XType.Element)]
  public class CustomerAddRq : QbXmlBase
  {
    [XMap (XType = XType.Element)]
    public CustomerAdd CustomerAdd {
      get;
      set;
    }

    [XMap (XType = XType.Element)]
    public IncludeRetElementList IncludeRetElementList {
      get;
      set;
    }
  }
}

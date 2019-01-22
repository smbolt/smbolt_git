using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.QB.QBXML
{
  [XMap (XType = XType.Element)]
  public class Ref
  {
    [XMap (XType = XType.Element)]
    public string ListID {
      get;
      set;
    }

    [XMap (XType = XType.Element)]
    public string FullName {
      get;
      set;
    }
  }
}

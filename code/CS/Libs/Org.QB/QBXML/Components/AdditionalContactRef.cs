using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.QB.QBXML
{
  [XMap (XType = XType.Element)]
  public class AdditionalContactRef
  {
    [XMap(XType = XType.Element)]
    public string ContactName { get; set; }

    [XMap(XType = XType.Element)]
    public string ContactValue { get; set; }
  }
}

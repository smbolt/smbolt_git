using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.QB.QBXML
{
  [XMap(XType = XType.Element, Name = "QBXML")]
  public class QBXMLRq
  {
    [XMap (XType = XType.Element)]
    public QBXMLMsgsRq QBXMLMsgsRq {
      get;
      set;
    }

    public QBXMLRq()
    {
      this.QBXMLMsgsRq = new QBXMLMsgsRq();
    }
  }
}

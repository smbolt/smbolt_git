using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.QB.QBXML
{
  [XMap(XType = XType.Element)]
  public class QBXMLRs
  {
    public QBXMLMsgsRs QBXMLMsgRs { get; set; }
  }
}

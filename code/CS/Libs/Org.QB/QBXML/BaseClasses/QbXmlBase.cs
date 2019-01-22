using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.QB.QBXML
{
  public class QbXmlBase
  {
    [XMap (XType = XType.Attribute, Name = "requestID")]
    public int? RequestId {
      get;
      set;
    }

    public QbXmlBase()
    {

    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.QB.QBXML
{
  [XMap (XType = XType.Element)]
  public class QBXMLMsgsRq
  {
    [XMap (XType = XType.Element)]
    public List<QbXmlBase> RqList { get { return _rqList; } }
    private List<QbXmlBase> _rqList;

    public QBXMLMsgsRq()
    {
      _rqList = new List<QbXmlBase>();
    }

    public void Add(QbXmlBase qbXmlBase)
    {
      qbXmlBase.RequestId = _rqList.Count + 1;
      _rqList.Add(qbXmlBase);
    }
  }
}

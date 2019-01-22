using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  public class GetControlResponse : TransactionBase
  {
    [XMap(XType = XType.Element, CollectionElements = "WsCommand", WrapperElement = "WsCommandSet")]
    public WsCommandSet WsCommandSet { get; set; }

    public GetControlResponse()
    {
      this.WsCommandSet = new WsCommandSet();
    }
  }
}

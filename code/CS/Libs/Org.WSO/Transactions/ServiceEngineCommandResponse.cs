using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  public class ServiceEngineCommandResponse : TransactionBase
  {
    [XMap(XType = XType.Element, CollectionElements = "WsCommand", WrapperElement = "WsCommandSet")]
    public WsCommandSet WsCommandSet { get; set; }

    public ServiceEngineCommandResponse()
    {
      this.WsCommandSet = new WsCommandSet();
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;
using Org.WSO;
using Org.WSO.Transactions;

namespace Org.Dx.Business
{
  [XMap(XType = XType.Element)]
  [WCFTrans(Version = "1.0.0.0")]
  public class GetMapResponse : TransactionBase
  {
    [XMap]
    public string Map { get; set; }

    public GetMapResponse()
    {
      this.Map = String.Empty;
    }
  }
}
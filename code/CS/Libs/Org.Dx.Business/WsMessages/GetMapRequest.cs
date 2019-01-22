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
  public class GetMapRequest : TransactionBase
  {
    [XMap]
    public string MapName { get; set; }

    [XMap]
    public string ExtractTransName { get; set; }

    public GetMapRequest()
    {
      this.MapName = String.Empty;
      this.ExtractTransName = String.Empty;
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  public class GetConfigListResponse : TransactionBase
  {
    [XMap]
    public List<string> ConfigList { get; set; }

    public GetConfigListResponse()
    {
      this.ConfigList = new List<string>();
    }
  }
}

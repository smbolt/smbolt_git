using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  public class GetCentralConfigRequest : TransactionBase
  {
    [XMap]
    public string AppName {
      get;
      set;
    }

    public GetCentralConfigRequest()
    {
      this.AppName = String.Empty;
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;
using Org.WSO;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  [WCFTrans(Version = "1.0.0.0")]
  public class GetAssemblyReportRequest : TransactionBase
  {
    [XMap]
    public bool IncludeAllAssemblies { get; set; }

    public GetAssemblyReportRequest()
    {
      this.IncludeAllAssemblies = false;
    }

  }
}

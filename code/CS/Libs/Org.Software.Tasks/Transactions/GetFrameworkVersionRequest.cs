using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS;

namespace Org.Software.Tasks.Transactions
{
  [XMap(XType = XType.Element)]
  [WCFTrans(Version = "1.0.0.0")]
  public class GetFrameworkVersionsRequest : TransactionBase
  {
  }
}

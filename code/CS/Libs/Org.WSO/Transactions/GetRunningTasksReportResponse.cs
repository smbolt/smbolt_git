using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  [WCFTrans(Version = "1.0.0.0")]
  public class GetRunningTasksReportResponse : TransactionBase
  {
    [XMap]
    public string RunningTasksReport {
      get;
      set;
    }

    public GetRunningTasksReportResponse()
    {
      this.RunningTasksReport = string.Empty;
    }
  }
}

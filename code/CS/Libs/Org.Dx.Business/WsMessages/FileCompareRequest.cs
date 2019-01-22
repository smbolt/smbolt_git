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
  public class FileCompareRequest : TransactionBase
  {
    [XMap]
    public string BaseFilePath { get; set; }

    [XMap]
    public string CompareFilePath { get; set; }

    [XMap]
    public string ScriptFilePath { get; set; }

    [XMap]
    public string ReportFilePath { get; set; }

    public FileCompareRequest()
    {
      this.BaseFilePath = String.Empty;
      this.CompareFilePath = String.Empty;
      this.ScriptFilePath = String.Empty;
      this.ReportFilePath = String.Empty;
    }
  }
}

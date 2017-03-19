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
  public class ExcelExtractRequest : TransactionBase
  {
    [XMap]
    public string FullPath { get; set; }

    [XMap(XType = XType.Element, WrapperElement="WorksheetsToInclude", CollectionElements="WorksheetName")]
    public List<string> WorksheetsToInclude { get; set; }

    public ExcelExtractRequest()
    {
      this.FullPath = String.Empty;
      this.WorksheetsToInclude = null;
    }
  }
}

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
  public class PdfExtractResponse : TransactionBase
  {
    [XMap(XType=XType.Element, CollectionElements="DxWorksheet", WrapperElement="DxWorkbook")]
    public DxWorkbook DxWorkbook { get; set; }

    [XMap]
    public string RegressionFullFilePath { get; set; }

    public PdfExtractResponse()
    {
      this.DxWorkbook = null;
      this.RegressionFullFilePath = String.Empty;
    }
  }
}

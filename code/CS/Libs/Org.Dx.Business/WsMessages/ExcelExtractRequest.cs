using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;
using Org.Dx.Business;
using Org.WSO;
using Org.WSO.Transactions;

namespace Org.Dx.Business
{
  [XMap(XType = XType.Element)]
  [WCFTrans(Version = "1.0.0.0")]
  public class ExcelExtractRequest : TransactionBase
  {
    [XMap]
    public FileExtractMode FileExtractMode { get; set; }

    [XMap]
    public string FullPath { get; set; }

    [XMap]
    public string FileNamePrefix { get; set; }

    [XMap(XType = XType.Element, WrapperElement="WorksheetsToInclude", CollectionElements="WorksheetName")]
    public List<string> WorksheetsToInclude { get; set; }

    [XMap]
    public string MapName { get; set; }

    [XMap]
    public string FullMapPath { get; set; }

    public ExcelExtractRequest()
    {
      this.FileExtractMode = FileExtractMode.NotSet;
      this.FullPath = String.Empty;
      this.FileNamePrefix = String.Empty;
      this.WorksheetsToInclude = null;
      this.FullMapPath = String.Empty;
      this.MapName = String.Empty;
    }
  }
}

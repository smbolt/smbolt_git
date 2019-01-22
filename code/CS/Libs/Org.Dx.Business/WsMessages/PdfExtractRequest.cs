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
  public class PdfExtractRequest : TransactionBase
  {
    [XMap]
    public FileExtractMode FileExtractMode { get; set; }

    [XMap]
    public string FullPath { get; set; }

    [XMap]
    public string FileNamePrefix { get; set; }

    [XMap]
    public string MapName { get; set; }

    [XMap]
    public string FullMapPath { get; set; }

    public PdfExtractRequest()
    {
      this.FileExtractMode = FileExtractMode.NotSet;
      this.FileNamePrefix = String.Empty;
      this.FullPath = String.Empty;
      this.MapName = String.Empty;
      this.FullMapPath = String.Empty;
    }
  }
}

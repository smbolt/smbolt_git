using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;
using Org.WSO;
using Org.WSO.Transactions;

namespace Org.Software.Tasks.Transactions
{  
  public enum ResponseType
  {
    NotSet,
    RetryRequest,
    Ready,
    SegmentReturned,
    AcknowledgedComplete,
    AcknowledgedCancel
  }

  [XMap(XType = XType.Element)]
  [WCFTrans(Version = "1.0.0.0")]
  public class DownloadSoftwareResponse : TransactionBase
  {
    [XMap]
    public ResponseType ResponseType { get; set; }
    [XMap]
    public string UpgradeVersion { get; set; }
    [XMap]
    public string UpgradePlatformString { get; set; }
    [XMap]
    public int SegmentNumber { get; set; }
    [XMap]
    public int TotalSegments { get; set; }
    [XMap]
    public int RemainingSegments { get; set; }
    [XMap]
    public int TotalFileSize { get; set; }
    [XMap]
    public int SegmentSize { get; set; }
    [XMap]
    public string SegmentData { get; set; }

    public DownloadSoftwareResponse()
    {
      this.ResponseType = ResponseType.NotSet;
      this.UpgradeVersion = String.Empty;
      this.UpgradePlatformString = String.Empty;
      this.SegmentNumber = 0;
      this.TotalSegments = 0;
      this.RemainingSegments = 0;
      this.TotalFileSize = 0;
      this.SegmentSize = 0;
      this.SegmentData = String.Empty;
    }
  }
}

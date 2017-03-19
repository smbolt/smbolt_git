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
  public enum RequestType
  {
    NotSet,
    InitialRequest,
    GetNextSegment,
    CancelDownload,
    DownloadComplete
  }

  [XMap(XType = XType.Element)]
  [WCFTrans(Version = "1.0.0.0")]
  public class DownloadSoftwareRequest : TransactionBase
  {    
    [XMap]
    public int OrgId { get; set; }
    [XMap]
    public string DomainName { get; set; }
    [XMap]
    public string MachineName { get; set; }
    [XMap]
    public string UserName { get; set; }
    [XMap]
    public int ModuleCode { get; set; }
    [XMap]
    public string ModuleName { get; set; }
    [XMap]
    public string CurrentVersion { get; set; }
    [XMap]
    public string CurrentPlatformString { get; set; }
    [XMap]
    public string UpgradeVersion { get; set; }
    [XMap]
    public string UpgradePlatformString { get; set; }
    [XMap]
    public RequestType RequestType { get; set; }
    [XMap]
    public int SegmentNumber { get; set; }

    public DownloadSoftwareRequest()
    {
      this.OrgId = -1;
      this.DomainName = String.Empty;
      this.MachineName = String.Empty;
      this.UserName = String.Empty;
      this.ModuleCode = 0; 
      this.ModuleName = String.Empty;
      this.CurrentVersion = String.Empty;
      this.CurrentPlatformString = String.Empty;
      this.UpgradeVersion = String.Empty;
      this.UpgradePlatformString = String.Empty;
      this.RequestType = RequestType.NotSet;
      this.SegmentNumber = 0;
    }
  }
}

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
  [XMap(XType = XType.Element)]
  [WCFTrans(Version = "1.0.0.0")]
  public class CheckForUpdatesResponse : TransactionBase
  {
    [XMap]
    public bool UpgradeAvailable { get; set; }
    [XMap]
    public bool UpgradeRequired { get; set; }
    [XMap]
    public string CurrentVersion { get; set; }
    [XMap]
    public string UpgradeVersion { get; set; }
    [XMap]
    public string PlatformString { get; set; }

    public CheckForUpdatesResponse()
    {
      this.UpgradeAvailable = false;
      this.UpgradeRequired = false;
      this.CurrentVersion = String.Empty;
      this.UpgradeVersion = String.Empty;
      this.PlatformString = String.Empty;
    }
  }
}

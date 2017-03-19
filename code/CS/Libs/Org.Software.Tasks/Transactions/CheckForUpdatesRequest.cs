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
  public class CheckForUpdatesRequest : TransactionBase
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
    public string CurrentVersionExpanded { get { return Get_CurrentVersionExpanded(); } }
    [XMap]
    public string CurrentPlatformString { get; set; }

    public CheckForUpdatesRequest()
    {
      this.OrgId = -1;
      this.DomainName = String.Empty;
      this.MachineName = String.Empty;
      this.UserName = String.Empty;
      this.ModuleCode = 0; 
      this.ModuleName = String.Empty;
      this.CurrentVersion = String.Empty;
      this.CurrentPlatformString = String.Empty;
    }

    private string Get_CurrentVersionExpanded()
    {
      string ver = this.CurrentVersion;
      if (ver.IsBlank())
        return String.Empty;

      int[] versionTokens = ver.ToTokenArrayInt32(Constants.DotDelimiter); 

      string currentVersionExpanded = String.Empty;
      foreach(int versionToken in versionTokens)
      {
        if (currentVersionExpanded.IsBlank())
          currentVersionExpanded += versionToken.ToString("000000"); 
        else
          currentVersionExpanded += "." + versionToken.ToString("000000"); 
      }

      return currentVersionExpanded;
    }
  }
}

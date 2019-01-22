using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO.Transactions
{
  public enum GetConfigListCommand
  {
    NotSet,
    GetConfigList
  }

  [XMap(XType = XType.Element)]
  public class GetConfigListRequest : TransactionBase
  {
    [XMap]
    public GetConfigListCommand GetConfigListCommand {
      get;
      set;
    }

    [XMap]
    public string GetFilesFrom {
      get;
      set;
    }

    [XMap]
    public string SendFilesTo {
      get;
      set;
    }

    [XMap]
    public ConfigFileType ConfigType {
      get;
      set;
    }

    [XMap]
    public string ProfileName {
      get;
      set;
    }

    public GetConfigListRequest()
    {
      this.GetConfigListCommand = GetConfigListCommand.NotSet;
      this.GetFilesFrom = String.Empty;
      this.SendFilesTo = String.Empty;
      this.ConfigType = ConfigFileType.NotSet;
      this.ProfileName = String.Empty;
    }
  }
}
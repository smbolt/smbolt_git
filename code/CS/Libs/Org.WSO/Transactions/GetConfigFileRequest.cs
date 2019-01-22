using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO.Transactions
{
  public enum GetConfigFileCommand
  {
    NotSet,
    GetProfileList,
    GetConfigList,
    GetConfigFile
  }

  [XMap(XType = XType.Element)]
  public class GetConfigFileRequest : TransactionBase
  {
    [XMap]
    public GetConfigFileCommand GetConfigFileCommand {
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

    [XMap]
    public string FileName {
      get;
      set;
    }

    public GetConfigFileRequest()
    {
      this.GetConfigFileCommand = GetConfigFileCommand.NotSet;
      this.GetFilesFrom = String.Empty;
      this.SendFilesTo = String.Empty;
      this.ConfigType = ConfigFileType.NotSet;
      this.ProfileName = String.Empty;
      this.FileName = String.Empty;
    }
  }
}
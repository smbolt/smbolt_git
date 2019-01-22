using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS
{
  [XMap(XType = XType.Element, CollectionElements = "FxVersion", WrapperElement="FxVersionSet")]
  public class FxVersionSet : Dictionary<string, FxVersion> {  }

  [XMap(XType = XType.Element)]
  public class FxVersion
  {
    [XMap]
    public int FrameworkVersionId {
      get;
      set;
    }

    [XMap(IsKey = true)]
    public string FrameworkVersionString {
      get;
      set;
    }

    [XMap]
    public string Version {
      get;
      set;
    }

    [XMap]
    public string VersionNum {
      get;
      set;
    }

    [XMap]
    public string ServicePackString {
      get;
      set;
    }

    public string FxVersionKey {
      get {
        return Get_FxVersionKey();
      }
    }

    public FxVersion()
    {
      this.FrameworkVersionId = -1;
      this.FrameworkVersionString = String.Empty;
      this.Version = String.Empty;
      this.VersionNum = String.Empty;
      this.ServicePackString = String.Empty;
    }

    private string Get_FxVersionKey()
    {
      string key = this.FrameworkVersionString.Trim() + "|" +
                   this.Version + "|" +
                   this.VersionNum + "|" +
                   this.ServicePackString;

      return key;
    }
  }
}

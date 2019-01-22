using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class CO
  {
    [XMap(MyParent = true)]
    public COSet COSet {
      get;
      set;
    }

    public ProgramConfig ProgramConfig {
      get {
        return this.COSet == null ? null : this.COSet.ProgramConfig;
      }
    }

    [XMap(Name = "K", IsKey = true)]
    public string Key {
      get;
      set;
    }

    [XMap(XType = XType.Element, Name = "V", IsObject = true)]
    public object Value {
      get;
      set;
    }

    public CO()
    {
      this.Key = String.Empty;
      this.Value = null;
    }

    public CO(string key, object value)
    {
      this.Key = key;
      this.Value = value;
    }

    public CO(string key, object value, COSet coSet)
    {
      this.COSet = coSet;
      this.Key = key;
      this.Value = value;
    }
  }
}

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
  public class DI
  {
    [XMap(MyParent = true)]
    public ConfigDictionary ConfigDictionary {
      get;
      set;
    }

    public ProgramConfig ProgramConfig {
      get {
        return this.ConfigDictionarySet == null ? null : this.ConfigDictionarySet.ProgramConfig;
      }
    }
    public ConfigDictionarySet ConfigDictionarySet {
      get {
        return this.ConfigDictionary == null ? null : this.ConfigDictionary.ConfigDictionarySet;
      }
    }

    [XMap(Name = "K", IsExplicit = true)]
    public string Key {
      get;
      set;
    }

    [XMap(Name = "V", DefaultValue = "", IsExplicit = true)]
    public string Value {
      get;
      set;
    }

    public DI()
    {
      this.Key = String.Empty;
      this.Value = String.Empty;
    }

    public DI(string key, string value)
    {
      this.Key = key;
      this.Value = value;
    }

    public DI(string key, string value, ConfigDictionary configDictionary)
    {
      this.ConfigDictionary = configDictionary;
      this.Key = key;
      this.Value = value;
    }
  }
}

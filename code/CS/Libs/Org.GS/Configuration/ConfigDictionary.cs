using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "DI", XType = XType.Element)]
  public class ConfigDictionary : List<DI>
  {
    [XMap(MyParent = true)]
    public ConfigDictionarySet ConfigDictionarySet { get; set; }

    [XMap(IsKey = true)]
    public string Name { get; set; }


    [XParm(Name = "parent", ParmSource = XParmSource.Parent, AttrName = "", Required = false)]
    public ConfigDictionary(ConfigDictionarySet parent)
    {
      this.ConfigDictionarySet = parent;
      this.Name = String.Empty;
    }

    public ConfigDictionary() { }

    public ConfigDictionary(string name, ConfigDictionarySet parent)
    {
      this.ConfigDictionarySet = parent;
      this.Name = name;
    }
  }
}

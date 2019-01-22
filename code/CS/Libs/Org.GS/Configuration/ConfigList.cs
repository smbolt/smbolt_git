using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "LI", XType = XType.Element)]
  public class ConfigList : List<LI>
  {
    [XMap(MyParent = true)]
    public ConfigListSet ConfigListSet {
      get;
      set;
    }

    [XMap(IsKey = true)]
    public string Name {
      get;
      set;
    }


    [XParm(Name = "parent", ParmSource = XParmSource.Parent)]
    public ConfigList(ConfigListSet parent)
    {
      this.ConfigListSet = parent;
      this.Name = String.Empty;
    }

    public ConfigList() { }

    public ConfigList(string name, ConfigListSet parent)
    {
      this.ConfigListSet = parent;
      this.Name = name;
    }
  }
}

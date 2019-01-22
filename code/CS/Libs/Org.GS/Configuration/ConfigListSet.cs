using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "ConfigList", XType = XType.Element)]
  public class ConfigListSet : Dictionary<string, ConfigList>
  {      
    [XMap(MyParent = true)]
    public ProgramConfig ProgramConfig { get; set; }

    [XParm(Name = "parent", ParmSource = XParmSource.Parent)]
    public ConfigListSet(ProgramConfig parent)
    {
      this.ProgramConfig = parent;
    }
  }
}

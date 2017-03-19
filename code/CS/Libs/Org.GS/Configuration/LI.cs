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
  public class LI
  {      
    [XMap(MyParent = true)]
    public ConfigList ConfigList { get; set; }
    
    public ProgramConfig ProgramConfig { get { return this.ConfigListSet == null ? null : this.ConfigListSet.ProgramConfig; } }
    public ConfigListSet ConfigListSet { get { return this.ConfigList == null ? null : this.ConfigList.ConfigListSet; } }
        
    [XMap(Name = "V", DefaultValue = "", IsExplicit = true)]
    public string Value { get; set; }

    public LI()
    {
      this.Value = String.Empty;
    }

    public LI(string value)
    {
      this.Value = value;
    }

    public LI(string value, ConfigList configList)
    {
      this.ConfigList = configList;
      this.Value = value;
    }
  }
}

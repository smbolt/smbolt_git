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
  public class CI
  {      
    [XMap(MyParent = true)]
    public CIGroup CIGroup { get; set; }

    public ProgramConfig ProgramConfig { get { return this.CISet == null ? null : this.CISet.ProgramConfig; } }
    public CISet CISet { get { return this.CIGroup == null ? null : this.CIGroup.CISet; } }

    [XMap(Name="K", IsKey=true)]
    public string Key { get; set; }

    [XMap(Name="V", DefaultValue="", IsExplicit = true)]
    public string Value { get; set; }
        
    public CI()
    {
      this.Key = String.Empty;
      this.Value = String.Empty;
    }

    public CI(string key, string value)
    {
      this.Key = key;
      this.Value = value;
    }

    public CI(string key, string value, CIGroup ciGroup)
    {
      this.CIGroup = ciGroup;
      this.Key = key;
      this.Value = value;
    }
  }
}

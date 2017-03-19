using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class ConfigGroup
  {
    [XMap(IsKey = true)]
    public string GroupID { get; set; }

    [XMap]
    public string GroupName { get; set; }

    public ConfigGroup()
    {
      this.GroupID = String.Empty;
      this.GroupName = String.Empty;
    }
  }
}

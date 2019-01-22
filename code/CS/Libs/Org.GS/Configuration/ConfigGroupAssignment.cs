using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class ConfigGroupAssignment
  {
    [XMap]
    public string GroupID {
      get;
      set;
    }

    public ConfigGroupAssignment()
    {
      this.GroupID = String.Empty;
    }
  }
}

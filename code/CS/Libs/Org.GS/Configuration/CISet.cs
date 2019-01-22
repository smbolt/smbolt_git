using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements="CIGroup", XType=XType.Element)]
  public class CISet : Dictionary<string, CIGroup>
  {
    [XMap(MyParent = true)]
    public ProgramConfig ProgramConfig {
      get;
      set;
    }

    [XParm(Name = "parent", ParmSource = XParmSource.Parent)]
    public CISet(ProgramConfig parent)
    {
      this.ProgramConfig = parent;
    }
  }
}

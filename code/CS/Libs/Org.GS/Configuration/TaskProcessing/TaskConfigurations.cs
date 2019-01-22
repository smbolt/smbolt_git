using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements="TaskConfigSet")]
  public class TaskConfigurations : Dictionary<string, TaskConfigSet>
  {
    public ProgramConfig ProgramConfig {
      get;
      set;
    }

    [XParm(Name = "parent", ParmSource = XParmSource.Parent)]
    public TaskConfigurations(ProgramConfig parent)
    {
      this.ProgramConfig = parent;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Org.GS;

namespace Org.GS.Code
{
  [ObfuscationAttribute(Exclude = true)]
  public enum ExclusionControl
  {
    NotSet,
    IfIncludes,
    IfMatches
  }

  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class ExtensionExclusion
  {
    [XMap]
    public string Value {
      get;
      set;
    }

    [XMap(IsRequired = true)]
    public ExclusionControl ExclusionControl {
      get;
      set;
    }

    public ExtensionExclusion()
    {
      this.Value = String.Empty;
      this.ExclusionControl = ExclusionControl.NotSet;
    }
  }
}

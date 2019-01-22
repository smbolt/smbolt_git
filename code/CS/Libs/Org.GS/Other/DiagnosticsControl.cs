using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS.Configuration;

namespace Org.GS
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class DiagnosticsControl
  {
    [XMap]
    public bool RunFsActionSet {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public FSActionSet FSActionSet {
      get;
      set;
    }

    public DiagnosticsControl()
    {
      this.RunFsActionSet = false;
      this.FSActionSet = new FSActionSet(null);
    }
  }
}

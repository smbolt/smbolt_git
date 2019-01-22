using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "FSAction", XType = XType.Element)]
  public class FSActionGroup : Dictionary<string, FSAction>
  {
    [XMap(IsKey = true)]
    public string Name {
      get;
      set;
    }

    [XMap(DefaultValue = "True")]
    public bool IsActive {
      get;
      set;
    }

    [XMap]
    public string Src {
      get;
      set;
    }

    [XMap]
    public string Dst {
      get;
      set;
    }

    [XMap(DefaultValue = "Ignore")]
    public FailureAction FailureAction {
      get;
      set;
    }

    public bool ContinueProcessing {
      get;
      set;
    }

    [XMap(MyParent = true)]
    public FSActionSet FSActionSet {
      get;
      set;
    }

    [XParm(Name = "parent", ParmSource = XParmSource.Parent)]
    public FSActionGroup(FSActionSet parent)
    {
      this.FSActionSet = parent;
      this.Src = String.Empty;
      this.Dst = String.Empty;
      this.IsActive = true;
      this.FailureAction = FailureAction.Ignore;
      this.ContinueProcessing = true;
    }
  }
}

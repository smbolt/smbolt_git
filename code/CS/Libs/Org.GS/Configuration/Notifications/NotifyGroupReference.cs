using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Org.GS
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class NotifyGroupReference
  {
    [XMap(Name = "Name")]
    public string NotifyGroupName {
      get;
      set;
    }

    [XMap(Name = "IsActive")]
    public bool IsActive {
      get;
      set;
    }

    public NotifyGroupReference()
    {
      this.NotifyGroupName = String.Empty;
      this.IsActive = false;
    }
  }
}

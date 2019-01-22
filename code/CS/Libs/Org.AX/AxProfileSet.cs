using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.AX
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "AxProfile")]
  public class AxProfileSet : Dictionary<string, AxProfile>
  {
    [XMap(XType = XType.Element, WrapperElement = "VariableSet", CollectionElements = "Variable", UseKeyValue = true)]
    public VariableSet VariableSet {
      get;
      set;
    }

    public AxProfileSet()
    {
      this.VariableSet = new VariableSet();
    }

    public void AutoInit()
    {
      foreach (var profile in this.Values)
      {
        profile.AxProfileSet = this;
      }
    }
  }
}

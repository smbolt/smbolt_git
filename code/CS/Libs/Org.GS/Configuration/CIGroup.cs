using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "CI", XType = XType.Element)] 
  public class CIGroup : Dictionary<string, CI>
  {
    [XMap(MyParent = true)]
    public CISet CISet { get; set; }

    [XMap(IsKey=true)]
    public string Name { get; set; }


    [XParm(Name = "parent", ParmSource = XParmSource.Parent)]
    public CIGroup(CISet parent)
    {
      CISet = parent;
      this.Name = String.Empty;
    }

    public CIGroup(string name, CISet parent)
    {
      CISet = parent;
      this.Name = name;
    }
  }
}

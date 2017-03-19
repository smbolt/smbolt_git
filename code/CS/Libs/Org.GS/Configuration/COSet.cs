using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "CO", XType = XType.Element, WrapperElement = "COSet")]
  public class COSet : Dictionary<string, CO>
  {
    [XMap(MyParent = true)]
    public ProgramConfig ProgramConfig { get; set; }

    [XMap(IsKey = true)]
    public string Name { get; set; }


    [XParm(Name = "parent", ParmSource = XParmSource.Parent, AttrName = "", Required = false)]
    public COSet(ProgramConfig parent)
    {
      this.ProgramConfig = parent;
      this.Name = String.Empty;
    }

    public COSet() { }

    public COSet(string name, ProgramConfig parent)
    {
      this.ProgramConfig = parent;
      this.Name = name;
    }
  }
}

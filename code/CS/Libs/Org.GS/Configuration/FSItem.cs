using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)] 
  public class FSItem
  {
    [XMap(IsKey=true)]
    public string Name { get; set; }

    public FSItem()
    {
      this.Name = String.Empty;
    }
  }
}

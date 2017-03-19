using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements="ProgramConfig", XType = XType.Element)] 
  public class ProgramConfigSet : Dictionary<string, ProgramConfig>
  {
    public ProgramConfigSet()
    {
    }
  }
}

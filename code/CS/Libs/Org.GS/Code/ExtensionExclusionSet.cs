using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Code
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "Extension")] 
  public class ExtensionExclusionSet : List<ExtensionExclusion>
  {
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "LevelLimits", WrapperElement = "LevelLimitsSet")] 
  public class LevelLimitsSet : List<LevelLimits>
  {
  }
}

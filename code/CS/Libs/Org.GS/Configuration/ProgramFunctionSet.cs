using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "ProgramFunction")]
  public class ProgramFunctionSet : SortedList<int, ProgramFunction>
  {
  }
}

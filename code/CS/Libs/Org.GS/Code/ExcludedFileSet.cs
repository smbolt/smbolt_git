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
  [XMap(XType = XType.Element, WrapperElement="ExcludedFileSet", CollectionElements = "ExcludedFile", UseKeyValue = true)]
  public class ExcludedFileSet : List<String>
  {
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  [XMap(XType = XType.Element, CollectionElements="ExtractSection", KeyName="SectionName")]
  public class ExtractSectionSet : Dictionary<string, ExtractSection>
  {
  }
}

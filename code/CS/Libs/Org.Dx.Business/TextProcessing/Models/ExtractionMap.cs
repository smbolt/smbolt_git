using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  [XMap(XType = XType.Element)]
  public class ExtractionMap
  {
    [XMap]
    public ExtractionUnit ExtractionUnit { get; set; }

    [XMap]
    public string Prefix { get; set; }

    [XMap]
    public string Suffix { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "ExtractSection", WrapperElement="ExtractSectionSet")]
    public ExtractSectionSet ExtractSectionSet { get; set; }

    public ExtractionMap()
    {
      this.ExtractionUnit = ExtractionUnit.NotSet;
      this.Prefix = String.Empty;
      this.Suffix = String.Empty;
      this.ExtractSectionSet = new ExtractSectionSet();
    }
  }
}

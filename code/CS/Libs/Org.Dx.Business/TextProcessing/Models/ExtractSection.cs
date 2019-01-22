using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  [XMap(XType = XType.Element, KeyName="SectionName")]
  public class ExtractSection
  {
    [XMap(IsKey = true)]
    public string SectionName { get; set; }

    [XMap]
    public string Prefix { get; set; }

    [XMap]
    public string Suffix { get; set; }

    [XMap]
    public string Rectangle { get; set; }

    public ExtractSection()
    {
      this.SectionName = String.Empty;
      this.Prefix = String.Empty;
      this.Suffix = String.Empty;
      this.Rectangle = String.Empty;
    }

  }
}

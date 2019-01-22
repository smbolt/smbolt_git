using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "b", Abbr = "Bold", AutoMap = true)]
  public class Bold : DocumentElement
  {
    [Meta(XMatch = true)]
    public bool Val {
      get;
      set;
    }

    public Bold() { }

    public Bold(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      this.Val = xml.GetRequiredBooleanAttribute("val");
    }
  }
}
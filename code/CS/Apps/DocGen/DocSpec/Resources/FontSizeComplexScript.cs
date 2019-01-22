using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "szCs", Abbr = "FontSzCs", AutoMap = true)]
  public class FontSizeComplexScript : DocumentElement
  {
    [Meta(XMatch = true)]
    public string Val {
      get;
      set;
    }

    public FontSizeComplexScript() { }

    public FontSizeComplexScript(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      this.Val = xml.GetRequiredAttributeValue("val");
    }
  }
}

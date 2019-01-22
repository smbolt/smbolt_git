using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "lang", Abbr = "Lang", AutoMap = true)]
  public class Languages : DocumentElement
  {
    [Meta(XMatch = true)]
    public string Bidi {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public string EastAsia {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public string Val {
      get;
      set;
    }

    public Languages() { }

    public Languages(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      this.Bidi = xml.GetRequiredAttributeValue("bidi");
      this.EastAsia = xml.GetRequiredAttributeValue("eastAsia");
      this.Val = xml.GetRequiredAttributeValue("val");
    }
  }
}

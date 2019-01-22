using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName="aDocRes")]
  public class DocResource : DocumentElement
  {
    public StyleSet StyleSet {
      get;
      set;
    }
    public PropertyCache PropertyCache {
      get;
      set;
    }

    public DocResource(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      XElement stylesElement = null;

      if (xml.Name.LocalName == "styles")
        stylesElement = xml;
      else
        stylesElement = xml.GetElement("styles");


      this.StyleSet = new StyleSet(stylesElement, doc, this);
      this.PropertyCache = new PropertyCache(xml.GetElement("propCache"), doc, this);
    }
  }
}

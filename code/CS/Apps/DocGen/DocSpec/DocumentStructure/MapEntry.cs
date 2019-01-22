using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "aMapEntry", Abbr = "MapEntry")]
  public class MapEntry : DocumentElement
  {
    public MapEntry() { }

    public MapEntry(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      this.Tag = xml.GetRequiredAttributeValue("tag");

      this.LoadChildren(xml, doc, this);
    }

  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public class MapEntrySet : DocumentElement
  {
    public MapEntrySet() { }

    public MapEntrySet(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      this.LoadChildren(xml, doc, this);
    }

  }
}
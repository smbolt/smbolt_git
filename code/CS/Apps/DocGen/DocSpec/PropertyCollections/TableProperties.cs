using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "tblPr", Abbr = "TblPr", ParentSet = "C")]
  public class TableProperties : DocumentElement
  {
    public TableProperties() { }

    public TableProperties(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      IEnumerable<XElement> set = xml.Elements();
      foreach (XElement e in set)
        this.ChildElements.Add(DeFactory.CreateDeObject(this.ParentSet, e.Name.LocalName, e, doc, this));
    }
  }
}

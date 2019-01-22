using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "tblStyle", AutoMap = true, Abbr = "TblStyle")]
  public class TableStyle : DocumentElement
  {
    [Meta(XMatch = true)]
    public TableStyleValues Val {
      get;
      set;
    }

    public TableStyle() { }

    public TableStyle(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      this.Val = (TableStyleValues) xml.GetRequiredEnumAttribute("val", typeof(TableStyleValues));
    }
  }
}

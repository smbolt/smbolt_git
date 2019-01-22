using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "tblInd", Abbr = "TblIndent", AutoMap = true)]
  public class TableIndentation : DocumentElement
  {
    [Meta(XMatch = true)]
    public TableWidthUnitValues? Type {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public Int32? Width {
      get;
      set;
    }

    public TableIndentation() { }

    public TableIndentation(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      this.Type = (TableWidthUnitValues)xml.GetRequiredEnumAttribute("type", typeof(TableWidthUnitValues));
      this.Width = xml.GetRequiredAttributeInt32("w");
    }
  }
}
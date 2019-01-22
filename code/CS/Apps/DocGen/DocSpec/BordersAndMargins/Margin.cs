using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "aMar", AutoMap = true, Abbr = "Mgn")]
  public class Margin : DocumentElement
  {
    [Meta(XMatch = true, IsAttribute = true)]
    public string Width {
      get;
      set;
    }
    [Meta(XMatch = true, IsAttribute = true)]
    public TableWidthUnitValues Type {
      get;
      set;
    }

    public Margin() {}

    public Margin(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
      {
        this.Width = "0";
        this.Type = TableWidthUnitValues.Auto;
        return;
      }

      this.Width = xml.GetRequiredAttributeValue("w");
      this.Type = (TableWidthUnitValues)xml.GetRequiredEnumAttribute("type", typeof(TableWidthUnitValues));

      switch (xml.Name.LocalName)
      {
        case "top":
          this.OpenXmlClassName = "TopMargin";
          break;

        case "left":
          this.OpenXmlClassName = "TableCellLeftMargin";
          break;

        case "bottom":
          this.OpenXmlClassName = "BottomMargin";
          break;

        case "right":
          this.OpenXmlClassName = "TableCellRightMargin";
          break;

        case "start":
          this.OpenXmlClassName = "StartMargin";
          break;

        case "end":
          this.OpenXmlClassName = "EndMargin";
          break;
      }
    }

  }
}

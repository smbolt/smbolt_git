using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "tcPr", Abbr = "TCPr")]
  public class TableCellProperties : DocumentElement
  {
    public TableCellProperties() { }

    public TableCellProperties(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      IEnumerable<XElement> set = xml.Elements();
      foreach(XElement e in set)
      {
        string elementName = e.Name.LocalName;
        switch (elementName)
        {
          case "tcW":
            TableCellWidth tableCellWidth = new TableCellWidth(e, doc, this);
            this.ChildElements.Add(tableCellWidth);
            break;

          case "trHeight":
            TableRowHeight tableRowHeight = new TableRowHeight(e, doc, this);
            this.ChildElements.Add(tableRowHeight);
            break;

          case "tcBorders":
            TableCellBorders tableCellBorders = new TableCellBorders(e, doc, this);
            this.ChildElements.Add(tableCellBorders);
            break;
        }
      }
    }
  }
}

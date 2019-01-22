using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public class PropertyCache : Dictionary<string, DocumentElement>
  {
    public PropertyCache(XElement xml, Doc doc, DocumentElement parent)
    {
      this.Clear();

      if (xml == null)
        return;

      IEnumerable<XElement> set = xml.Elements();
      foreach (XElement e in set)
      {
        string elementName = e.Name.LocalName;

        switch (elementName)
        {
          case "tblPr":
            TableProperties tableProperties = new TableProperties(e, doc, null);
            if (this.ContainsKey(tableProperties.Name))
              throw new Exception("PropertyCache already contains a property object keyed with the name '" + tableProperties.Name + "'.");
            this.Add(tableProperties.Name, tableProperties);
            break;

          case "tcPr":
            TableCellProperties cellProperties = new TableCellProperties(e, doc, null);
            if (this.ContainsKey(cellProperties.Name))
              throw new Exception("PropertyCache already contains a property object keyed with the name '" + cellProperties.Name + "'.");
            this.Add(cellProperties.Name, cellProperties);
            break;
        }
      }
    }
  }
}

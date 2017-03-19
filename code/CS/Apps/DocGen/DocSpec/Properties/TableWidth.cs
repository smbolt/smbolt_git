using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "tblW", AutoMap = true, Abbr = "TblWth")]
    public class TableWidth : DocumentElement
    {
        [Meta(XMatch = true)]
        public TableWidthUnitValues Type { get; set; }
        [Meta(XMatch = true)]
        public string Width { get; set; }

        public TableWidth() { }

        public TableWidth(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
            {
                this.Type = TableWidthUnitValues.Nil;
                this.Width = "0";
                return;
            }

            this.Type = (TableWidthUnitValues)xml.GetRequiredEnumAttribute("type", typeof(TableWidthUnitValues));
            this.Width = xml.GetRequiredAttributeValue("w");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "trHeight", AutoMap = true, Abbr = "TRHgt")]
    public class TableRowHeight : DocumentElement
    {
        [Meta(XMatch = true, IsAttribute = true)]
        public HeightRuleValues HeightType { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public UInt32 Val { get; set; }

        public TableRowHeight() { }

        public TableRowHeight(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
            {
                this.HeightType = HeightRuleValues.Auto;
                this.Val = 0;
                return;
            }

            this.HeightType = (HeightRuleValues)xml.GetRequiredEnumAttribute("hRule", typeof(HeightRuleValues));
            this.Val = xml.GetUInt32Attribute("val");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "rStyle", Abbr = "RStyle")]
    public class RunStyle : DocumentElement
    {
        public string Val { get; set; }

        public RunStyle() { }

        public RunStyle(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
                return;

            this.Val = xml.GetRequiredAttributeValue("val");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "spacing", ChildOfSet = "B", Abbr = "Spc")]
    public class Spacing : DocumentElement
    {
        public int Val { get; set; }

        public Spacing() { }

        public Spacing(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
                return;

            this.Val = xml.GetRequiredIntegerAttribute("val");
        }
    }
}
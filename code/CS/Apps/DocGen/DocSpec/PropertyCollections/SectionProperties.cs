using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "sectPr", Abbr = "SectPr")]
    public class SectionProperties : DocumentElement 
    {
        public SectionProperties(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
                return;

            IEnumerable<XElement> props = xml.Elements();
            foreach (XElement prop in props)
                this.ChildElements.Add(DeFactory.CreateDeObject(this.ParentSet, prop.Name.LocalName, prop, doc, this));
        }
    }
}


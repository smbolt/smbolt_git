using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "tabs", Abbr = "Tabs", IsAttribute = true)]
    public class Tabs : DocumentElement
    {
        public Tabs() { }

        public Tabs(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            XNamespace ns = xml.Name.Namespace;
            IEnumerable<XElement> tabStops = xml.Elements(ns + "tab");

            foreach (XElement tabStop in tabStops)
                this.ChildElements.Add(new TabStop(tabStop, doc, this));
        }
    }
}
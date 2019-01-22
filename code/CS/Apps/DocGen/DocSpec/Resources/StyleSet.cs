using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "styleSet", Abbr = "StyleSet")]
    public class StyleSet : DocumentElement
    {
        public Dictionary<string, Style> Styles { get; set; }

        public StyleSet() { }

        public StyleSet(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            this.Styles = new Dictionary<string, Style>();

            if (xml == null)
                return;

            XNamespace ns = xml.Name.NamespaceName;

            XElement docDefaults = xml.Element(ns + "docDefaults");
            if (docDefaults != null)
            {
                Style s = new Style(docDefaults, doc, this);

                if (this.Styles.ContainsKey(s.StyleId))
                    throw new Exception("Duplicate styleId '" + s.StyleId + "' found when adding docDefault style.");

                this.Styles.Add(s.StyleId, s);
            }

            IEnumerable<XElement> set = xml.Elements(ns + "style");
            foreach (XElement e in set)
            {
                Style s = new Style(e, doc, this);

                if (this.Styles.ContainsKey(s.StyleId))
                    throw new Exception("Duplicate styleId '" + s.StyleId + "' found.");

                this.Styles.Add(s.StyleId, s);
            }
        }
    }
}

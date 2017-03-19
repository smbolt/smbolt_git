using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "aDocMap")]
    public class DocMap : DocumentElement
    {
        public MapEntrySet MapEntrySet { get; set; }
        public StyleSet StyleSet { get; set; }
        public PropertyCache PropertyCache { get; set; }

        public DocMap(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
                return;

            XElement stylesElement = null;

            if (xml.Name.LocalName == "styles")
                stylesElement = xml;
            else
                stylesElement = xml.GetElement("styles");

            // load styles and put them in the Doc's StyleSet, override as necessary
            this.StyleSet = new StyleSet(stylesElement, doc, this);
            foreach (Style s in this.StyleSet.Styles.Values)
            {
                if (this.Doc.DocResource.StyleSet.Styles.ContainsKey(s.StyleId))
                    this.Doc.DocResource.StyleSet.Styles[s.StyleId] =  s;
                else
                    this.Doc.DocResource.StyleSet.Styles.Add(s.StyleId, s);
            }

            // load properties and put them in the Doc's PropertyCache, override as necessary
            this.PropertyCache = new PropertyCache(xml.GetElement("propCache"), doc, this);
            foreach (KeyValuePair<string, DocumentElement> kvp in this.PropertyCache)
            {
                if (this.Doc.DocResource.PropertyCache.ContainsKey(kvp.Key))
                    this.Doc.DocResource.PropertyCache[kvp.Key] = kvp.Value;
                else
                    this.Doc.DocResource.PropertyCache.Add(kvp.Key, kvp.Value);
            }

            XElement mapEntries = xml.GetElement("mapEntries");
            if (mapEntries != null)
                this.MapEntrySet = new MapEntrySet(mapEntries, doc, parent);
            else
                this.MapEntrySet = new MapEntrySet();
        }
    }
}
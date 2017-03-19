using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "style", Abbr = "Style")]
    public class Style : DocumentElement
    {
        public StyleValues Type { get; set; }
        public bool Default { get; set; }
        public string BasedOn { get; set; }
        public string NextParagraphStyle { get; set; }
        public string UIPriority { get; set; }
        public bool SemiHidden { get; set; }
        public bool UnhideWhenUsed { get; set; }
        public bool PrimaryStyle { get; set; }

        public RunProperties RunProperties { get; set; }
        public ParagraphProperties ParagraphProperties { get; set; }
        public TableProperties TableProperties { get; set; }

        public Style() 
        {
 
        }

        public Style(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
                return;

            string elementName = xml.Name.LocalName;

            switch (elementName)
            {
                case "docDefaults":
                    this.Name = "DocDefault";
                    this.StyleId = "DocDefault";
                    this.Type = StyleValues.DocDefaults;
                    this.Default = false;
                    this.BasedOn = String.Empty;
                    this.NextParagraphStyle = "Normal";
                    this.PrimaryStyle = false;
                    this.UIPriority = "99";
                    this.SemiHidden = false;
                    this.UnhideWhenUsed = false;

                    this.RunProperties = new RunProperties(xml.GetElement("rPrDefault").GetElement("rPr"), doc, this);
                    this.ParagraphProperties = new ParagraphProperties(xml.GetElement("pPrDefault").GetElement("pPr"), doc, this);
                    this.TableProperties = null;
                    break;

                default:
                    this.Name = xml.GetRequiredElementAttributeValue("name", "val");
                    this.StyleId = xml.GetRequiredAttributeValue("styleId");
                    this.Type = (StyleValues)xml.GetRequiredEnumAttribute("type", typeof(StyleValues));

                    this.Default = false;
                    string defaultAttribute = xml.GetAttributeValue("default");
                    if (defaultAttribute != null)
                        if (defaultAttribute == "1" || defaultAttribute.ToLower() == "true")
                            this.Default = true;

                    this.BasedOn = xml.GetAttributeValue("basedOn"); 
                    this.NextParagraphStyle = xml.GetAttributeValue("next");
                    this.PrimaryStyle = xml.GetElement("qFormat") != null;
                    this.UIPriority = xml.GetElementAttributeValueOrBlank("uiPriority", "val");
                    this.SemiHidden = xml.GetElement("semiHidden") != null;
                    this.UnhideWhenUsed = xml.GetElement("unhideWhenUsed") != null;

                    this.RunProperties = new RunProperties(xml.GetElement("rPr"), doc, this);
                    this.ParagraphProperties = new ParagraphProperties(xml.GetElement("pPr"), doc, this);
                    this.TableProperties = new TableProperties(xml.GetElement("tblPr"), doc, this);

                    break;
            }
        }
    }
}

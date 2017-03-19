using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class BorderType : DocumentElement
    {
        [Meta(XMatch = true, IsAttribute = true)]
        public string Color { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public bool Frame { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public bool Shadow { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public UInt32 Size { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public UInt32 Space { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public string ThemeColor { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public string ThemeShade { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public string ThemeTint { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public BorderValues Val { get; set; }

        public BorderType() { }

        public BorderType(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            this.Val = BorderValues.Nil;
            this.Color = "auto";
            this.Size = 0U;
            this.Space = 0U;

            if (xml == null)
                return;

            this.Val = (BorderValues)xml.GetRequiredEnumAttribute("val", (typeof(BorderValues)));
            this.Color = xml.GetRequiredAttributeValue("color");
            this.Size = xml.GetRequiredAttributeUInt32("sz");
            this.Space = xml.GetRequiredAttributeUInt32("space");

            switch (xml.Name.LocalName)
            {
                case "top":
                    this.OpenXmlClassName = "TopBorder";
                    break;

                case "left":
                    this.OpenXmlClassName = "LeftBorder";
                    break;

                case "bottom":
                    this.OpenXmlClassName = "BottomBorder";
                    break;

                case "right":
                    this.OpenXmlClassName = "RightBorder";
                    break;

                case "start":
                    this.OpenXmlClassName = "StartBorder";
                    break;

                case "end":
                    this.OpenXmlClassName = "EndBorder";
                    break;

                case "insideH":
                    this.OpenXmlClassName = "InsideHorizontalBorder";
                    break;

                case "insideV":
                    this.OpenXmlClassName = "InsideVerticalBorder";
                    break;

                case "tl2br":
                    this.OpenXmlClassName = "TopLeftToBottomRightBorder";
                    break;

                case "tr2bl":
                    this.OpenXmlClassName = "TopRightToBottomLeftBorder";
                    break;
            }
        }

    }
}

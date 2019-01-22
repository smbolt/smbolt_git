using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "rFonts", Abbr = "RunFonts", AutoMap = true)]
    public class RunFonts : DocumentElement
    {
        [Meta(XMatch = true)]
        public string Ascii { get; set; }
        [Meta(XMatch = true)]
        public ThemeFontValues? AsciiTheme { get; set; }
        [Meta(XMatch = true)]
        public string ComplexScript { get; set; }
        [Meta(XMatch = true)]
        public ThemeFontValues? ComplexScriptTheme { get; set; }
        [Meta(XMatch = true)]
        public string EastAsia { get; set; }
        [Meta(XMatch = true)]
        public ThemeFontValues? EastAsiaTheme { get; set; }
        [Meta(XMatch = true)]
        public string HighAnsi { get; set; }
        [Meta(XMatch = true)]
        public ThemeFontValues? HighAnsiTheme { get; set; }
        [Meta(XMatch = true)]
        public FontTypeHintValues? Hint { get; set; }

        public RunFonts() { }

        public RunFonts(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
                return;

            this.Ascii = xml.GetAttributeValueOrNull("ascii");
            this.AsciiTheme = this.GetThemeFontValues(xml.GetAttributeValueOrNull("asciiTheme"));
            this.ComplexScript = xml.GetAttributeValueOrNull("cs");
            this.ComplexScriptTheme = this.GetThemeFontValues(xml.GetAttributeValueOrNull("cstheme"));
            this.EastAsia = xml.GetAttributeValueOrNull("eastAsia");
            this.EastAsiaTheme = this.GetThemeFontValues(xml.GetAttributeValueOrNull("eastAsiaTheme"));
            this.HighAnsi = xml.GetAttributeValueOrNull("hAnsi");
            this.HighAnsiTheme = this.GetThemeFontValues(xml.GetAttributeValueOrNull("hAnsiTheme"));
        }

        private ThemeFontValues? GetThemeFontValues(string themeFontValue)
        {
            switch (themeFontValue)
            {
                case "majorEastAsia": return ThemeFontValues.MajorEastAsia;
                case "majorBidi": return ThemeFontValues.MajorBidi;
                case "majorAscii": return ThemeFontValues.MajorAscii;
                case "majorHAnsi": return ThemeFontValues.MajorHighAnsi;
                case "minorEastAsia": return ThemeFontValues.MinorEastAsia;
                case "minorBidi": return ThemeFontValues.MinorBidi;
                case "minorAscii": return ThemeFontValues.MinorAscii;
                case "minorHAnsi": return ThemeFontValues.MinorHighAnsi;             
            }

            return null;
        }
    }
}
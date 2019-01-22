using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class AggregatedProperties
    {
        public List<DocumentElement> RawProperties { get; set; }

        public FontSize FontSize { get; set; }
        public Color Color { get; set; }
        public Bold Bold { get; set; }
        public Underline Underline { get; set; }
        public Italic Italic { get; set; }
        public FontSizeComplexScript FontSizeComplexScript { get; set; }
        public Justification Justification { get; set; }
        public Languages Languages { get; set; }
        public PageMargin PageMargin { get; set; }
        public PageSize PageSize { get; set; }
        public ParagraphStyleId ParagraphStyleId { get; set; }
        public RunFonts RunFonts { get; set; }
        public SpacingBetweenLines SpacingBetweenLines { get; set; }
        public TableBorders TableBorders { get; set; }
        public TableCellBorders TableCellBorders { get; set; }
        public TableCellMargin TableCellMargin { get; set; }
        public TableCellWidth TableCellWidth { get; set; }
        public TableLook TableLook { get; set; }
        public TableRowHeight TableRowHeight { get; set; }
        public TableStyle TableStyle { get; set; }
        public TableWidth TableWidth { get; set; }
        public Doc Doc { get; set; }
        public DocumentElement Parent { get; set; }

        public AggregatedProperties(DocumentElement parent, Doc doc)
        {
            this.Parent = parent;
            this.Doc = doc;
            this.RawProperties = new List<DocumentElement>();
        }

        public XElement GetXmlMap()
        {
            XElement e = new XElement("AggregatedProperties");

            List<PropertyInfo> piSet = this.GetType().GetProperties().ToList();
            foreach (PropertyInfo pi in piSet)
            {
                if (pi.PropertyType.IsSubclassOf(typeof(DocSpec.DocumentElement)))
                {
                    string name = pi.PropertyType.Name;
                    object val = pi.GetValue(this, null);
                    if (val != null)
                        e.Add(((DocumentElement)val).GetXmlMap(true));
                }
            }

            return e;
        }

        public Font GetFont()
        {
            string fontFace = "Calibri";
            float fontSize = 11.0F;
            FontStyle fontStyle = FontStyle.Regular;
            
            if (this.Bold != null)
                if (this.Bold.Val)
                    fontStyle = fontStyle | FontStyle.Bold;

            if (this.Italic != null)
                if (this.Italic.Val)
                    fontStyle = fontStyle | FontStyle.Italic;

            if (this.Underline != null)
                fontStyle = fontStyle | FontStyle.Underline;


            string pStyleId = "Default";
            if (this.ParagraphStyleId != null)
                pStyleId = this.ParagraphStyleId.Val;
            Style pStyle = this.Doc.DocResource.StyleSet.Styles[pStyleId];
            RunFonts rf = pStyle.RunProperties.ChildElements.OfType<RunFonts>().FirstOrDefault();
            if (rf != null)
                if (rf.Ascii.IsNotBlank())
                    fontFace = rf.Ascii;

            FontSize fSz = pStyle.RunProperties.ChildElements.OfType<FontSize>().FirstOrDefault();
            if (fSz != null)
                fontSize = fSz.PointSize;

            if (this.FontSize != null)
                fontSize = this.FontSize.PointSize;

            string fontSetKey = fontFace + "." + fontSize.ToString() + "." + fontStyle.ToString();

            if (this.Doc.FontSet.ContainsKey(fontSetKey))
                return this.Doc.FontSet[fontSetKey];

            float netFontSize = fontSize/ 0.96F;

            Font f = new Font(fontFace, netFontSize, fontStyle);
            this.Doc.FontSet.Add(fontSetKey, f);

            return f;
        }

        public System.Drawing.Color GetColor()
        {
            if (this.Color != null)
                return g.GetColorFromString(this.Color.Val);

            return System.Drawing.Color.Black;
        }

        public JustificationValues GetJustification()
        {
            if (this.Justification == null)
                return JustificationValues.Left;

            return this.Justification.Val;
        }
    }
}

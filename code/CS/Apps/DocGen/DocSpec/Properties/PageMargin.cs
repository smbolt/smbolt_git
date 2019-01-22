using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "pgMar", AutoMap = true, Abbr = "PgMgn")]
    public class PageMargin : DocumentElement
    {
        [Meta(XMatch = true, IsAttribute = true)] 
        public Int32 Top { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public Int32 Bottom { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public UInt32 Left { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public UInt32 Right { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public UInt32 Header { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public UInt32 Footer { get; set; }
        [Meta(XMatch = true, IsAttribute = true)]
        public UInt32 Gutter { get; set; }

        public PageMargin() { }

        public PageMargin(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
            {
                this.Top = 0;
                this.Bottom = 0;
                this.Left = 0;
                this.Right = 0;
                this.Header = 0;
                this.Footer = 0;
                this.Gutter = 0;
                return;
            }

            this.Top = xml.GetInt32Attribute("top");
            this.Bottom = xml.GetInt32Attribute("bottom");
            this.Left = xml.GetUInt32Attribute("left");
            this.Right = xml.GetUInt32Attribute("right");
            this.Header = xml.GetUInt32Attribute("header");
            this.Footer = xml.GetUInt32Attribute("footer");
            this.Gutter = xml.GetUInt32Attribute("gutter");
        }

        public PageLayout PageLayoutFromTwips(Size pageSize)
        {
            PageLayout layout = new PageLayout();

            int topMargin = Convert.ToInt32((float)this.Top / 1440 * 100);
            int leftMargin = Convert.ToInt32((float)this.Left / 1440 * 100);
            int bottomMargin = Convert.ToInt32((float)this.Bottom / 1440 * 100);
            int rightMargin = Convert.ToInt32((float)this.Right / 1440 * 100);
            int header = Convert.ToInt32((float)this.Header / 1440 * 100);
            int footer = Convert.ToInt32((float)this.Footer / 1440 * 100);

            int mainWidth = pageSize.Width - (leftMargin + rightMargin);
            int mainHeight = pageSize.Height - (topMargin + bottomMargin);
            int headerHeight = topMargin - header;
            int footerHeight = bottomMargin - footer;

            layout.Header = new Rectangle(leftMargin, header, mainWidth, headerHeight - 1);
            layout.Main = new Rectangle(leftMargin, topMargin, mainWidth, mainHeight);
            layout.Footer = new Rectangle(leftMargin, topMargin + mainHeight + 1, mainWidth, footerHeight - 1);

            return layout;
        }
    }
}

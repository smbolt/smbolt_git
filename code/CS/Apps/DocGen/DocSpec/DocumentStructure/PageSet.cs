using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Ds = Org.DocGen.DocSpec;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    public class PageSet : SortedList<int, Page>
    {
        public float Scale { get; set; }
        public Size CurrentPageSize { get; set; }
        public PageLayout CurrentLayout { get; set; }

        public PageSet(float scale)
        {
            this.Scale = scale;
            this.CurrentPageSize = new Size(Convert.ToInt32(this.Scale * 850), Convert.ToInt32(this.Scale * 1100));
        }

        public int AddSection(Ds.Section section)
        {
            this.CurrentPageSize = GetPageSize(section).ToSizeFromTwips();
            this.CurrentLayout = GetPageLayout(section);

            Page p = new Page(this.CurrentPageSize, this.Scale);
            p.PageNumber = this.Count + 1;
            p.PageLayout = this.CurrentLayout;
            this.Add(p.PageNumber, p);

            return p.PageNumber;
        }

        public Ds.PageSize GetPageSize(Ds.Section section)
        {
            Ds.PageSize pageSize = section.Properties.ChildElements.OfType<Ds.PageSize>().SingleOrDefault();

            if (pageSize == null)
            {
                pageSize = new PageSize();
                pageSize.Width = 12240;
                pageSize.Height = 15840;
                pageSize.Orient = PageOrientationValues.Portrait;
            }

            return pageSize;
        }

        public Ds.PageLayout GetPageLayout(Ds.Section section)
        {
            Ds.PageMargin marginSet = section.Properties.ChildElements.OfType<Ds.PageMargin>().SingleOrDefault();

            if (marginSet != null)
                return marginSet.PageLayoutFromTwips(this.CurrentPageSize);

            // Use default 1" margins and 1/2" header and footer
            PageLayout layout = new PageLayout();
            layout.Header = new Rectangle(100, 50, 650, 50);
            layout.Main = new Rectangle(100, 100, 650, 900);
            layout.Footer = new Rectangle(100, 1000, 650, 50);

            return layout;
        }
    }
}

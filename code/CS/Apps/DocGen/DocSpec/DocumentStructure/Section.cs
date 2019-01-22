using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Drawing;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "aSect", Abbr = "Sect")]
    public class Section : DocumentElement
    {
        public Section() { }

        public override void MapElement(Graphics g)
        {
            foreach (DocumentElement de in this.ChildElements)
            {
                de.MapElement(g);
                this.MergeMetrics(de.RawMetrics);
            }
        }

        public override void DrawElement(Graphics g)
        {
            this.TraceMetrics("SECT_001");

            foreach (DocumentElement de in this.ChildElements)
            {
                de.DrawElement(g);
                this.MergeMetrics(de.RawMetrics);
                this.TraceMetrics("SECT_002");
            }
        }

        public override SizeSpec GetSizeSpec()
        {
            PageSize pgSz = (PageSize)this.Properties.ChildElements.OfType<PageSize>().FirstOrDefault();
            if (pgSz == null)
                throw new Exception("No PageSize property found in section properties for section '" + this.Name + "'.");

            PageMargin pgMg = (PageMargin)this.Properties.ChildElements.OfType<PageMargin>().FirstOrDefault();
            if (pgMg == null)
                throw new Exception("No PageMargin property found in section properties for section '" + this.Name + "'.");

            Size pageSize = pgSz.ToSizeFromTwips();
            PageLayout pageLayout = pgMg.PageLayoutFromTwips(pageSize);

            this.RawMetrics.Offset = new Point(pageLayout.Main.Left, pageLayout.Main.Top);
            this.RawMetrics.MaxSize = new Size(pageLayout.Main.Width, 0);
            this.RawMetrics.TotalSize = new Size(0, 0);

            SpecUnit widthSpec = new SpecUnit(SpecUnitDimension.Width, this.RawMetrics.MaxSize.Width, CommonUnits.Inches, SizeControl.Exact);
            SpecUnit heightSpec = new SpecUnit(SpecUnitDimension.Height, this.RawMetrics.MaxSize.Height, CommonUnits.Inches, SizeControl.Auto);

            return new SizeSpec(widthSpec, heightSpec);
        }

        public override void SetSizeSpec()
        {
            PageSize pgSz = (PageSize)this.Properties.ChildElements.OfType<PageSize>().FirstOrDefault();
            if (pgSz == null)
                throw new Exception("No PageSize property found in section properties for section '" + this.Name + "'.");

            PageMargin pgMg = (PageMargin)this.Properties.ChildElements.OfType<PageMargin>().FirstOrDefault();
            if (pgMg == null)
                throw new Exception("No PageMargin property found in section properties for section '" + this.Name + "'.");

            Size pageSize = pgSz.ToSizeFromTwips();
            PageLayout pageLayout = pgMg.PageLayoutFromTwips(pageSize);

            this.RawMetrics.Offset = new Point(pageLayout.Main.Left, pageLayout.Main.Top);
            this.RawMetrics.MaxSize = new Size(pageLayout.Main.Width, 0);
            this.RawMetrics.TotalSize = new Size(0, 0);

            SpecUnit widthSpec = new SpecUnit(SpecUnitDimension.Width, this.RawMetrics.MaxSize.Width, CommonUnits.Inches, SizeControl.Exact);
            SpecUnit heightSpec = new SpecUnit(SpecUnitDimension.Height, this.RawMetrics.MaxSize.Height, CommonUnits.Inches, SizeControl.Auto);

            this.RawMetrics.SizeSpec = new SizeSpec(widthSpec, heightSpec);
        }
    }
}

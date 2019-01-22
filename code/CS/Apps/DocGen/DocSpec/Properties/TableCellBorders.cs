using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
    [Meta(OxName = "tcBorders", Abbr = "TCBdr")]
    public class TableCellBorders : DocumentElement
    {
        [Meta(XMatch = true)]
        public Border TopBorder { get; set; }
        [Meta(XMatch = true)]
        public Border LeftBorder { get; set; }
        [Meta(XMatch = true)]
        public Border BottomBorder { get; set; }
        [Meta(XMatch = true)]
        public Border RightBorder { get; set; }
        [Meta(XMatch = true)]
        public Border StartBorder { get; set; }
        [Meta(XMatch = true)]
        public Border EndBorder { get; set; }
        [Meta(XMatch = true)]
        public Border InsideHorizontalBorder { get; set; }
        [Meta(XMatch = true)]
        public Border InsideVerticalBorder { get; set; }
        [Meta(XMatch = true)]
        public Border TopLeftToBottomRightBorder { get; set; }
        [Meta(XMatch = true)]
        public Border TopRightToBottomLeftBorder { get; set; }
        public List<Border> UsedBorders
        {
            get { return GetUsedBorders(); }
        }

        public TableCellBorders() { }

        public TableCellBorders(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);

            if (xml == null)
                return;

            XElement topBorderElement = xml.GetElement("top");
            if (topBorderElement != null)
                this.TopBorder = new Border(topBorderElement, doc, this);

            XElement leftBorderElement = xml.GetElement("left");
            if (leftBorderElement != null)
                this.LeftBorder = new Border(leftBorderElement, doc, this);

            XElement bottomBorderElement = xml.GetElement("bottom");
            if (bottomBorderElement != null)
                this.BottomBorder = new Border(bottomBorderElement, doc, this);

            XElement rightBorderElement = xml.GetElement("right");
            if (rightBorderElement != null)
                this.RightBorder = new Border(rightBorderElement, doc, this);

            XElement startBorderElement = xml.GetElement("start");
            if (startBorderElement != null)
                this.StartBorder = new Border(startBorderElement, doc, this);

            XElement endBorderElement = xml.GetElement("end");
            if (endBorderElement != null)
                this.EndBorder = new Border(endBorderElement, doc, this);

            XElement ihBorderElement = xml.GetElement("insideH");
            if (ihBorderElement != null)
                this.InsideHorizontalBorder = new Border(ihBorderElement, doc, this);

            XElement ivBorderElement = xml.GetElement("insideV");
            if (ivBorderElement != null)
                this.InsideVerticalBorder = new Border(ivBorderElement, doc, this);

            XElement tlbrBorderElement = xml.GetElement("tl2br");
            if (tlbrBorderElement != null)
                this.TopLeftToBottomRightBorder = new Border(tlbrBorderElement, doc, this);

            XElement trblBorderElement = xml.GetElement("tr2bl");
            if (trblBorderElement != null)
                this.TopRightToBottomLeftBorder = new Border(trblBorderElement, doc, this);
        }

        public List<Border> GetUsedBorders()
        {
            List<Border> usedBorders = new List<Border>();

            if (this.TopBorder != null)
                usedBorders.Add(this.TopBorder);

            if (this.LeftBorder != null)
                usedBorders.Add(this.LeftBorder);

            if (this.BottomBorder != null)
                usedBorders.Add(this.BottomBorder);

            if (this.RightBorder != null)
                usedBorders.Add(this.RightBorder);

            if (this.StartBorder != null)
                usedBorders.Add(this.StartBorder);

            if (this.EndBorder != null)
                usedBorders.Add(this.EndBorder);

            if (this.InsideHorizontalBorder != null)
                usedBorders.Add(this.InsideHorizontalBorder);

            if (this.InsideVerticalBorder != null)
                usedBorders.Add(this.InsideVerticalBorder);

            if (this.TopLeftToBottomRightBorder != null)
                usedBorders.Add(this.TopLeftToBottomRightBorder);

            if (this.TopRightToBottomLeftBorder != null)
                usedBorders.Add(this.TopRightToBottomLeftBorder);

            return usedBorders;
        }
    }
}

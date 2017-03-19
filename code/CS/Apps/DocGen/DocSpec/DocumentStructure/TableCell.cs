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
    [Meta(OxName = "tc", Abbr = "TCell")]
    public class TableCell : DocumentElement
    {
        public TableCell() { }

        public TableCell(XElement xml, Doc doc, DocumentElement parent)
        {
            base.Initialize(xml, doc, parent);
            
            if (xml == null)
                return;

            string cellPropertiesName = xml.GetAttributeValue("tcPr");
            XElement cellPropertiesElement = xml.GetElement("tcPr");

            if (cellPropertiesName.IsNotBlank())
            {
                if (!this.Doc.DocResource.PropertyCache.ContainsKey(cellPropertiesName))
                    throw new Exception("Cached CellProperties named '" + cellPropertiesName + "' does not exist in the cache.");

                this.Properties = (TableCellProperties)this.Doc.DocResource.PropertyCache[cellPropertiesName];
                this.Properties.Depth = this.Depth + 1;
            }

            // need to try out the overlay... 
            if (cellPropertiesElement != null)
            {
                TableCellProperties cp = new TableCellProperties(cellPropertiesElement, doc, this);
                if (this.Properties == null)
                    this.Properties = cp;
                else
                    this.Properties.Overlay(cp);
            }   

            this.LoadChildren(xml, doc, this);
        }

        public override void Draw(Graphics g)
        {
            g.DrawString(this.DeType.ToString() + "->" + this.Name, this.Doc.Font13b, Brushes.Black, this.Doc.Point);
            this.Doc.Point = new Point(this.Doc.Point.X, this.Doc.Point.Y + 20);
        }

        public override void MapElement(Graphics g)
        {
            this.SetSizeSpec();
            this.RawMetrics.Offset = this.Parent.RawMetrics.CurrentHorizontalOffset;

            string tag = this.Tag;
            string name = this.Name;

            this.Parent.RawMetrics.WidthSpecUsed += this.RawMetrics.SizeSpec.Width.Val;

            if (this.RawMetrics.SizeSpec.Height.Val > this.Parent.RawMetrics.HeightSpecUsed)
                this.Parent.RawMetrics.HeightSpecUsed = this.RawMetrics.SizeSpec.Height.Val;
            
            foreach (DocumentElement de in this.ChildElements)
            {
                de.MapElement(g);
                this.RawMetrics.HeightSpecUsed += de.RawMetrics.HeightSpecUsed;
            }

            this.RawMetrics.TotalSize = new SizeF(this.RawMetrics.SizeSpec.Width.Val, this.RawMetrics.HeightSpecUsed);
            
            OccupiedRegion or = new OccupiedRegion(this.Doc.GetRegionName(this.Level, this.Name), 1, this.RawMetrics.GetRectangleF(), this);
            this.Doc.RegionSet.Add(or.RegionName, or);
        }

        public override void DrawElement(Graphics g)
        {
            //this.RawMetrics.SizeSpec = this.GetSizeSpec();
            //this.RawMetrics.Offset = this.Parent.RawMetrics.CurrentHorizontalOffset;

            string tag = this.Tag;
            string name = this.Name;

            //this.Parent.RawMetrics.WidthSpecUsed += this.RawMetrics.SizeSpec.Width.Val;

            //if (this.RawMetrics.SizeSpec.Height.Val > this.Parent.RawMetrics.HeightSpecUsed)
            //    this.Parent.RawMetrics.HeightSpecUsed = this.RawMetrics.SizeSpec.Height.Val;

            this.TraceMetrics("CELL_001");

            foreach (DocumentElement de in this.ChildElements)
            {
                de.DrawElement(g);
                //this.RawMetrics.HeightSpecUsed += de.RawMetrics.HeightSpecUsed;
                this.TraceMetrics("CELL_002");
            }

            //this.RawMetrics.TotalSize = new SizeF(this.RawMetrics.SizeSpec.Width.Val, this.RawMetrics.HeightSpecUsed);
            this.TraceMetrics("CELL_003");

            // temporary, conditional code to put borders on table cells?
            if (name == "profileHeader" || name == "competenciesHeader" || name == "experienceHeader")
            {
                TableCellBorders tableCellBorders = this.AggregatedProperties.TableCellBorders;
                if (tableCellBorders != null)
                {
                    SizeF cellSize = this.RawMetrics.TotalSize;
                    PointF cellOffset = this.RawMetrics.Offset;

                    if (tableCellBorders.TopBorder != null)
                    {
                        Border b = tableCellBorders.TopBorder;
                        float pixelSize = b.GetPixelSize();
                        System.Drawing.Color color = Org.GS.g.GetColorFromString(b.Color);
                        Pen p = new Pen(new SolidBrush(color), pixelSize);
                        g.DrawLine(p, cellOffset, cellOffset.ShiftRight(cellSize.Width));
                    }

                    if (tableCellBorders.BottomBorder != null)
                    {
                        Border b = tableCellBorders.BottomBorder;
                        float pixelSize = b.GetPixelSize();
                        System.Drawing.Color color = Org.GS.g.GetColorFromString(b.Color);
                        Pen p = new Pen(new SolidBrush(color), pixelSize);
                        g.DrawLine(p, cellOffset.ShiftDown(cellSize.Height), cellOffset.ShiftDown(cellSize.Height).ShiftRight(cellSize.Width));
                    }
                }
            }

            //OccupiedRegion or = new OccupiedRegion(this.Doc.GetRegionName(this.Level, this.Name), 1, this.RawMetrics.GetRectangleF(), this);
            //this.Doc.RegionSet.Add(or.RegionName, or);

            if (this.InDiagnosticsMode && this.DiagnosticsLevel != 90)
            {
                if (this.DiagnosticsLevel > 0)
                {
                    RectangleF rect = this.RawMetrics.UsedRectangle;
                    g.DrawRectangle(new Pen(Brushes.SteelBlue, 0.3F), rect.X, rect.Y, rect.Width, rect.Height);
                }

                if (this.DiagnosticsLevel > 1)
                {
                    string print = this.Name;
                    if (this.Tag.IsNotBlank())
                        print += " (" + this.Tag + ")";

                    Font f = new Font("Tahoma", 5.0F);
                    SizeF tSz = g.MeasureString(print, f);
                    PointF drawingPoint = new PointF(this.RawMetrics.Offset.X + this.RawMetrics.SizeSpec.Size.Width - tSz.Width, 
                                                     this.RawMetrics.Offset.Y + this.RawMetrics.SizeSpec.Size.Height - tSz.Height);
                    g.DrawString(print, f, Brushes.SteelBlue, drawingPoint);
                }
            }
        }

        public override void SetSizeSpec()
        {
            SizeSpec parentSizeSpec = this.Parent.RawMetrics.SizeSpec;
            SizeSpec sizeSpec = parentSizeSpec.Clone();

            if (this.Properties == null)
            {
                this.RawMetrics.SizeSpec = sizeSpec;
                return;
            }

            TableCellWidth tcw = (TableCellWidth)this.Properties.ChildElements.OfType<TableCellWidth>().FirstOrDefault();
            if (tcw != null)
            {
                float val = (float)decimal.Parse(tcw.Width);
                switch (tcw.Type)
                {
                    case TableWidthUnitValues.Auto:
                        throw new Exception("TableCell.GetSizeSpec for 'TableWidthUnitValues.Auto' is not yet implemented.");

                    case TableWidthUnitValues.Pct:
                        sizeSpec.Width.Val = Conv.FromPctToPixels(parentSizeSpec.Width.Val, tcw.Width);
                        sizeSpec.Width.IsSpecified = true;
                        break;

                    case TableWidthUnitValues.Nil:
                        throw new Exception("TableCell.GetSizeSpec for 'TableWidthUnitValues.Nil' is not yet implemented.");

                    case TableWidthUnitValues.Dxa:
                        sizeSpec.Width.Val = Conv.FromDxaToPixels(tcw.Width);
                        sizeSpec.Width.IsSpecified = true;
                        break;
                }
            }

            TableRowHeight trh = (TableRowHeight)this.Properties.ChildElements.OfType<TableRowHeight>().FirstOrDefault();
            if (trh != null)
            {
                float val = (float)trh.Val;
                switch (trh.HeightType)
                {
                    case HeightRuleValues.Auto:
                        sizeSpec.Height.Val = 0;
                        sizeSpec.Height.Units = CommonUnits.Dxa;
                        sizeSpec.Height.SizeControl = g.ToEnum<SizeControl>(trh.HeightType.ToString(), SizeControl.Auto);
                        sizeSpec.Height.IsSpecified = true;
                        break;

                    case HeightRuleValues.AtLeast:
                        sizeSpec.Height.Val = Conv.FromDxaToPixels(trh.Val);
                        sizeSpec.Height.Units = CommonUnits.Dxa;
                        sizeSpec.Height.SizeControl = g.ToEnum<SizeControl>(trh.HeightType.ToString(), SizeControl.AtLeast);
                        sizeSpec.Height.IsSpecified = true;
                        break;

                    case HeightRuleValues.Exact:
                        sizeSpec.Height.Val = Conv.FromDxaToPixels(trh.Val);
                        sizeSpec.Height.Units = CommonUnits.Dxa;
                        sizeSpec.Height.SizeControl = g.ToEnum<SizeControl>(trh.HeightType.ToString(), SizeControl.Auto);
                        sizeSpec.Height.IsSpecified = true;
                        break;
                }

            }
            
            this.RawMetrics.SizeSpec = sizeSpec;
        }

        public override SizeSpec GetSizeSpec()
        {
            SizeSpec parentSizeSpec = this.Parent.RawMetrics.SizeSpec;
            SizeSpec sizeSpec = parentSizeSpec.Clone();

            if (this.Properties == null)
                return sizeSpec;

            TableCellWidth tcw = (TableCellWidth)this.Properties.ChildElements.OfType<TableCellWidth>().FirstOrDefault();
            if (tcw != null)
            {
                float val = (float)decimal.Parse(tcw.Width);
                switch (tcw.Type)
                {
                    case TableWidthUnitValues.Auto:
                        throw new Exception("TableCell.GetSizeSpec for 'TableWidthUnitValues.Auto' is not yet implemented.");

                    case TableWidthUnitValues.Pct:
                        sizeSpec.Width.Val = Conv.FromPctToPixels(parentSizeSpec.Width.Val, tcw.Width);
                        sizeSpec.Width.IsSpecified = true;
                        break;

                    case TableWidthUnitValues.Nil:
                        throw new Exception("TableCell.GetSizeSpec for 'TableWidthUnitValues.Nil' is not yet implemented.");

                    case TableWidthUnitValues.Dxa:
                        sizeSpec.Width.Val = Conv.FromDxaToPixels(tcw.Width);
                        sizeSpec.Width.IsSpecified = true;
                        break;
                }
            }

            TableRowHeight trh = (TableRowHeight)this.Properties.ChildElements.OfType<TableRowHeight>().FirstOrDefault();
            if (trh != null)
            {
                float val = (float)trh.Val;
                switch (trh.HeightType)
                {
                    case HeightRuleValues.Auto:
                        sizeSpec.Height.Val = 0;
                        sizeSpec.Height.Units = CommonUnits.Dxa;
                        sizeSpec.Height.SizeControl = g.ToEnum<SizeControl>(trh.HeightType.ToString(), SizeControl.Auto);
                        sizeSpec.Height.IsSpecified = true;
                        break;

                    case HeightRuleValues.AtLeast:
                        sizeSpec.Height.Val = Conv.FromDxaToPixels(trh.Val);
                        sizeSpec.Height.Units = CommonUnits.Dxa;
                        sizeSpec.Height.SizeControl = g.ToEnum<SizeControl>(trh.HeightType.ToString(), SizeControl.AtLeast);
                        sizeSpec.Height.IsSpecified = true;
                        break;

                    case HeightRuleValues.Exact:
                        sizeSpec.Height.Val = Conv.FromDxaToPixels(trh.Val);
                        sizeSpec.Height.Units = CommonUnits.Dxa;
                        sizeSpec.Height.SizeControl = g.ToEnum<SizeControl>(trh.HeightType.ToString(), SizeControl.Auto);
                        sizeSpec.Height.IsSpecified = true;
                        break;
                }

            }
            
            return sizeSpec;
        }
    }
}

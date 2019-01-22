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
  [Meta(OxName = "tbl", Abbr = "Tbl")]
  public class Table : DocumentElement
  {
    public Table() { }

    public Table(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
      {
        this.Properties = null;
        return;
      }

      string tablePropertiesName = xml.GetAttributeValue("tblPr");
      XElement tablePropertiesElement = xml.GetElement("tblPr");

      if (tablePropertiesName.IsNotBlank())
      {
        if (!this.Doc.DocResource.PropertyCache.ContainsKey(tablePropertiesName))
          throw new Exception("Cached TableProperties named '" + tablePropertiesName + "' does not exist in the cache.");

        this.Properties = (TableProperties)this.Doc.DocResource.PropertyCache[tablePropertiesName];
        this.Properties.Depth = this.Depth + 1;
      }

      // need to try out the overlay...
      if (tablePropertiesElement != null)
      {
        TableProperties tp = new TableProperties(tablePropertiesElement, doc, this);
        if (this.Properties == null)
          this.Properties = tp;
        else
          this.Properties.Overlay(tp);
      }

      this.LoadChildren(xml, doc, this);
    }

    public override void MapElement(Graphics g)
    {
      this.SetSizeSpec();
      this.RawMetrics.Offset = this.Parent.RawMetrics.CurrentVerticalOffset;

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
      //this.RawMetrics.Offset = this.Parent.RawMetrics.CurrentVerticalOffset;

      this.TraceMetrics("TABL_001");

      foreach (DocumentElement de in this.ChildElements)
      {
        de.DrawElement(g);
        //this.RawMetrics.HeightSpecUsed += de.RawMetrics.HeightSpecUsed;
        this.TraceMetrics("TABL_002");
      }

      //this.RawMetrics.TotalSize = new SizeF(this.RawMetrics.SizeSpec.Width.Val, this.RawMetrics.HeightSpecUsed);

      this.TraceMetrics("TABL_003");

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

    public override void Draw(Graphics g)
    {
      g.DrawString(this.DeType.ToString() + "->" + this.Name, this.Doc.Font13b, Brushes.Black, this.Doc.Point);
      this.Doc.Point = new Point(this.Doc.Point.X, this.Doc.Point.Y + 20);
    }

    public override void SetSizeSpec()
    {
      SizeSpec parentSizeSpec = this.Parent.RawMetrics.SizeSpec;
      SizeSpec sizeSpec = parentSizeSpec.Clone();

      TableWidth tblW = (TableWidth)this.Properties.ChildElements.OfType<TableWidth>().FirstOrDefault();
      if (tblW != null)
      {
        switch (tblW.Type)
        {
          case TableWidthUnitValues.Auto:
            throw new Exception("Table.GetSizeSpec for 'TableWidthUnitValues.Auto' is not yet implemented.");

          case TableWidthUnitValues.Pct:
            sizeSpec.Width.Val = Conv.FromPctToPixels(parentSizeSpec.Width.Val, tblW.Width);
            sizeSpec.Width.SizeControl = SizeControl.Exact;
            sizeSpec.Width.IsSpecified = true;
            break;

          case TableWidthUnitValues.Nil:
            throw new Exception("Table.GetSizeSpec for 'TableWidthUnitValues.Nil' is not yet implemented.");

          case TableWidthUnitValues.Dxa:
            throw new Exception("Table.GetSizeSpec for 'TableWidthUnitValues.Dxa' is not yet implemented.");
        }
      }

      this.RawMetrics.SizeSpec = sizeSpec;
    }

    public override SizeSpec GetSizeSpec()
    {
      SizeSpec parentSizeSpec = this.Parent.RawMetrics.SizeSpec;
      SizeSpec sizeSpec = parentSizeSpec.Clone();

      TableWidth tblW = (TableWidth)this.Properties.ChildElements.OfType<TableWidth>().FirstOrDefault();
      if (tblW != null)
      {
        switch (tblW.Type)
        {
          case TableWidthUnitValues.Auto:
            throw new Exception("Table.GetSizeSpec for 'TableWidthUnitValues.Auto' is not yet implemented.");

          case TableWidthUnitValues.Pct:
            sizeSpec.Width.Val = Conv.FromPctToPixels(parentSizeSpec.Width.Val, tblW.Width);
            sizeSpec.Width.SizeControl = SizeControl.Exact;
            sizeSpec.Width.IsSpecified = true;
            break;

          case TableWidthUnitValues.Nil:
            throw new Exception("Table.GetSizeSpec for 'TableWidthUnitValues.Nil' is not yet implemented.");

          case TableWidthUnitValues.Dxa:
            throw new Exception("Table.GetSizeSpec for 'TableWidthUnitValues.Dxa' is not yet implemented.");
        }
      }

      return sizeSpec;
    }
  }
}

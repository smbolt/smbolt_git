using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "p", Abbr = "P")]
  public class Paragraph : DocumentElement
  {
    public Paragraph() { }

    public Paragraph(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      XElement pPrElement = xml.GetElement("pPr");

      if (pPrElement != null)
        this.Properties = new ParagraphProperties(pPrElement, doc, this);

      this.LoadChildren(xml, doc, this);
    }

    public override void Draw(Graphics g)
    {
      g.DrawString(this.DeType.ToString() + "->" + this.Name, this.Doc.Font13b, Brushes.Black, this.Doc.Point);
      this.Doc.Point = new Point(this.Doc.Point.X, this.Doc.Point.Y + 20);
    }

    public override void MapElement(Graphics g)
    {
      DocControl dc = this.Doc.DocPackage.DocControl;
      PrintControl pc = dc.PrintControl;

      this.RawMetrics.SizeSpec = this.Parent.RawMetrics.SizeSpec.Clone();
      this.RawMetrics.Offset = this.Parent.RawMetrics.Offset.ShiftDown(this.Parent.RawMetrics.HeightSpecUsed);

      JustificationValues justification = this.AggregatedProperties.GetJustification();

      List<Run> runs = this.ChildElements.OfType<Run>().ToList();

      if (runs.Count == 0)
      {
        Font font = this.AggregatedProperties.GetFont();
        SizeF tSz = g.MeasureString(" ", font);
        this.Parent.RawMetrics.HeightSpecUsed += tSz.Height;
        return;
      }

      foreach (Run r in runs)
      {
        RunKit rk = new RunKit(g, r.AggregatedProperties.GetFont(), r.AggregatedProperties.GetColor(), justification, this.RawMetrics.SizeSpec.Width.Val, this.RawMetrics.Offset.X, this.RawMetrics.Offset.Y,
                               pc.Scale, pc.WidthFactor, pc.SpaceWidthFactor, pc.LineFactor, this.InDiagnosticsMode, r.DiagnosticsLevel);

        rk.ScaleGraphics();

        if (r.IsEmpty)
          break;

        rk.RunText = r.Text;
        bool runFinished = false;
        int tPtr = 0;

        while (!runFinished)
        {
          if (tPtr > rk.Tokens.Length - 1)
          {
            runFinished = true;
            break;
          }

          string token = rk.Tokens[tPtr];
          float spaceWidth = rk.StandardSpaceWidth * rk.SpaceWidthFactor;
          if (tPtr == 0 || rk.SpaceRemaining == rk.Width)
            spaceWidth = 0F;

          SizeF tSz = g.MeasureString(token, rk.Font);
          float tokenWidth = spaceWidth + (tSz.Width - rk.WidthPaddingTrim);

          if (rk.SpaceRemaining > tokenWidth)
          {
            if (tSz.Height > rk.LineHeight)
              rk.LineHeight = tSz.Height;

            rk.LineTokens.Add(token);
            rk.SpaceRemaining -= tokenWidth;
            tPtr++;
          }
          else
          {
            if (rk.LineTokens.Count > 0)
              rk.MapLineOfText();
          }
        }

        if (rk.LineTokens.Count > 0)
          rk.MapLineOfText();

        this.RawMetrics.HeightSpecUsed += rk.TotalHeightUsed;

        rk.ResetGraphicsScale();
      }

      this.RawMetrics.TotalSize = new SizeF(this.RawMetrics.SizeSpec.Width.Val, this.RawMetrics.HeightSpecUsed);

      if (this.Doc.DrawingMode != DrawingMode.DocumentPortion)
      {
        OccupiedRegion or = new OccupiedRegion(this.Doc.GetRegionName(this.Level, this.Name), 1, this.RawMetrics.GetRectangleF(), this);
        this.Doc.RegionSet.Add(or.RegionName, or);
      }
    }

    public override void DrawElement(Graphics g)
    {
      DocControl dc = this.Doc.DocPackage.DocControl;
      PrintControl pc = dc.PrintControl;

      //this.RawMetrics.SizeSpec = this.Parent.RawMetrics.SizeSpec.Clone();
      //this.RawMetrics.Offset = this.Parent.RawMetrics.Offset.ShiftDown(this.Parent.RawMetrics.HeightSpecUsed);

      this.TraceMetrics("PARG_001");

      JustificationValues justification = this.AggregatedProperties.GetJustification();

      List<Run> runs = this.ChildElements.OfType<Run>().ToList();

      if (runs.Count == 0)
      {
        Font font = this.AggregatedProperties.GetFont();
        SizeF tSz = g.MeasureString(" ", font);
        //this.Parent.RawMetrics.HeightSpecUsed += tSz.Height;
        return;
      }

      foreach (Run r in runs)
      {
        RunKit rk = new RunKit(g, r.AggregatedProperties.GetFont(), r.AggregatedProperties.GetColor(), justification, this.RawMetrics.SizeSpec.Width.Val, this.RawMetrics.Offset.X, this.RawMetrics.Offset.Y,
                               pc.Scale, pc.WidthFactor, pc.SpaceWidthFactor, pc.LineFactor, this.InDiagnosticsMode, r.DiagnosticsLevel);

        this.TraceMetrics("PARG_002");

        rk.ScaleGraphics();

        if (r.IsEmpty)
          break;

        rk.RunText = r.Text;
        bool runFinished = false;
        int tPtr = 0;

        while (!runFinished)
        {
          if (tPtr > rk.Tokens.Length - 1)
          {
            runFinished = true;
            break;
          }

          string token = rk.Tokens[tPtr];
          float spaceWidth = rk.StandardSpaceWidth * rk.SpaceWidthFactor;
          if (tPtr == 0 || rk.SpaceRemaining == rk.Width)
            spaceWidth = 0F;

          SizeF tSz = g.MeasureString(token, rk.Font);
          float tokenWidth = spaceWidth + (tSz.Width - rk.WidthPaddingTrim);

          if (rk.SpaceRemaining > tokenWidth)
          {
            if (tSz.Height > rk.LineHeight)
              rk.LineHeight = tSz.Height;

            rk.LineTokens.Add(token);
            rk.SpaceRemaining -= tokenWidth;
            tPtr++;
          }
          else
          {
            if (rk.LineTokens.Count > 0)
              rk.DrawLineOfText();
          }
        }

        if (rk.LineTokens.Count > 0)
          rk.DrawLineOfText();

        //this.RawMetrics.HeightSpecUsed += rk.TotalHeightUsed;
        this.TraceMetrics("PARG_003");

        rk.ResetGraphicsScale();
      }

      //this.RawMetrics.TotalSize = new SizeF(this.RawMetrics.SizeSpec.Width.Val, this.RawMetrics.HeightSpecUsed);

      //if (this.Doc.DrawingMode != DrawingMode.DocumentPortion)
      //{
      //    OccupiedRegion or = new OccupiedRegion(this.Doc.GetRegionName(this.Level, this.Name), 1, this.RawMetrics.GetRectangleF(), this);
      //    this.Doc.RegionSet.Add(or.RegionName, or);
      //}

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
  }
}

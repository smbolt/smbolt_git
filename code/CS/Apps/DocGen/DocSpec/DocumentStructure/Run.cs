using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "r", Abbr = "R")]
  public class Run : DocumentElement
  {
    public override string Text {
      get {
        return Get_Text();
      }
    }
    public bool IsEmpty {
      get {
        return Get_IsEmpty();
      }
    }

    public Run() { }

    public Run(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      XElement rPrElement = xml.GetElement("rPr");

      if (rPrElement != null)
        this.Properties = new RunProperties(rPrElement, doc, this);

      this.LoadChildren(xml, doc, this);
    }

    public override void Draw(Graphics g)
    {
    }

    //public override void DrawElement(Graphics g)
    //{
    //    this.RawMetrics.SizeSpec = this.Parent.RawMetrics.SizeSpec.Clone();
    //    this.RawMetrics.Offset = this.Parent.RawMetrics.Offset;

    //    if (this.ChildElements == null)
    //        return;

    //    Text t = this.ChildElements.OfType<Text>().FirstOrDefault();
    //    string runText = t.Val;
    //    if (runText.IsBlank())
    //        return;

    //    Font f = this.AggregatedProperties.GetFont(this);

    //    SizeF sz = g.MeasureString(runText, f);
    //    g.DrawString(runText, f, Brushes.Black, this.RawMetrics.Offset);

    //    if (this.Doc.InDiagnosticsMode)
    //    {
    //        RectangleF r = new RectangleF(this.RawMetrics.Offset, sz);
    //        g.DrawRectangle(Pens.LightBlue, r.X, r.Y, r.Width, r.Height);
    //    }

    //    this.RawMetrics.HeightSpecUsed += sz.Height;
    //}

    private string Get_Text()
    {
      string text = String.Empty;

      Text t = this.ChildElements.OfType<Text>().FirstOrDefault();
      if (t != null)
        text = t.Val;

      return text;
    }

    private bool Get_IsEmpty()
    {
      Text t = this.ChildElements.OfType<Text>().FirstOrDefault();
      if (t == null)
        return true;

      return false;
    }
  }
}

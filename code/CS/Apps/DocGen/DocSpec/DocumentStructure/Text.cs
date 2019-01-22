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
  [Meta(OxName = "t", Abbr = "Text")]
  public class Text : DocumentElement
  {
    public string Val {
      get;
      set;
    }

    public Text() { }

    public Text(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      this.Val = xml.Value;
    }

    public override void Draw(Graphics g)
    {
      g.DrawString(this.DeType.ToString() + "->" + this.Name, this.Doc.Font13b, Brushes.Black, this.Doc.Point);
      this.Doc.Point = new Point(this.Doc.Point.X, this.Doc.Point.Y + 20);
    }

    public override void DrawElement(Graphics g)
    {
      string val = this.Val;
      Font f = this.Doc.GetFont(this.AggregatedProperties);


    }
  }
}
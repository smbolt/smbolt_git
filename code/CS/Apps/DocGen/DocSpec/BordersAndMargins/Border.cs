using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "aBdr", Abbr = "Bdr")]
  public class Border : BorderType
  {
    public Border() : base() {}
    public Border(XElement xml, Doc doc, DocumentElement parent) : base(xml, doc, parent) {}

    public float GetPointSize()
    {
      int rawSize = Convert.ToInt32(this.Size);
      float pointSize = (float)rawSize / 8.0F;
      return pointSize;
    }

    public float GetPixelSize()
    {
      float pointSize = this.GetPointSize();
      float inchSize = pointSize / 72;
      float pixelSize = inchSize * 100.0F;
      return pixelSize;
    }
  }
}

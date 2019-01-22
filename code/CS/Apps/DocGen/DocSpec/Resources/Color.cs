using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "color", Abbr = "Color", IsAttribute = true, AutoMap=true)]
  public class Color : DocumentElement
  {
    [Meta(XMatch = true)]
    public ThemeColorValues? ThemeColor {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public string ThemeShade {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public string ThemeTint {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public string Val {
      get;
      set;
    }

    public Color() { }

    public Color(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      string themeColor = xml.GetAttributeValueOrNull("themeColor");
      if (themeColor != null)
        this.ThemeColor = (ThemeColorValues)Enum.Parse(typeof(ThemeColorValues), themeColor);

      this.ThemeShade = xml.GetAttributeValueOrNull("themeShade");
      this.ThemeTint = xml.GetAttributeValueOrNull("themeTint");
      this.Val = xml.GetAttributeValueOrNull("val");
    }
  }
}
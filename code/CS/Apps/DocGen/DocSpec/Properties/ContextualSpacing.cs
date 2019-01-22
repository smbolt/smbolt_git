using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "contextualSpacing", Abbr = "ContextualSpacing", IsAttribute = true, AutoMap = true)]
  public class ContextualSpacing : DocumentElement
  {
    [Meta(XMatch = true)]
    public bool Val {
      get;
      set;
    }

    public ContextualSpacing() { }

    public ContextualSpacing(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      this.Val = true;
    }
  }
}
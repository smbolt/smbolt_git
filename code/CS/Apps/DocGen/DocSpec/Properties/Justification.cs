using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "jc", Abbr = "Just", AutoMap = true)]
  public class Justification : DocumentElement
  {
    [Meta(XMatch = true)]
    public JustificationValues Val {
      get;
      set;
    }

    public Justification() { }

    public Justification(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      this.Val = (JustificationValues)xml.GetRequiredEnumAttribute("val", typeof(JustificationValues));
    }
  }
}
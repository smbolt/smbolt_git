using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class Alias
  {
    public string Spec {
      get;
      set;
    }
    public string Value {
      get;
      set;
    }

    public Alias(XElement xml)
    {
      this.Spec = XmlHelper.GetAttributeValueAsString(xml, "Spec");
      this.Value = XmlHelper.GetAttributeValueAsString(xml, "Value");
    }
  }
}

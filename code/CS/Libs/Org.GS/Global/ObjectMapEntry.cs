using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class ObjectMapEntry
  {
    public string Spec { get; set; }
    public string Value { get; set; }

    public ObjectMapEntry(XElement entry)
    {
      this.Spec = XmlHelper.GetAttributeValueAsString(entry, "Spec");
      this.Value = XmlHelper.GetAttributeValueAsString(entry, "Value");

      //this.ProcessSpec();
    }
  }
}

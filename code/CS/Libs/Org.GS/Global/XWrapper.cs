using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class XWrapper
  {
    public int Key { get; set; }
    public XElement XElement { get; set; }

    public XWrapper(int key, XElement e)
    {
      this.Key = key;
      this.XElement = e;
    }
  }
}

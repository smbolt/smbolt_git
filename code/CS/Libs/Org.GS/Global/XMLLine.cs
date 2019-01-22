using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class XmlLine
  {
    public string Line {
      get;
      set;
    }
    public List<XmlLineError> XmlLineErrors {
      get;
      set;
    }

    public XmlLine(string line)
    {
      this.Line = line;
      this.XmlLineErrors = new List<XmlLineError>();
    }
  }
}

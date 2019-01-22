using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DxDocs
{
  public class DocParm
  {
    public string Name {
      get;
      set;
    }
    public string Value {
      get;
      set;
    }

    public DocParm()
    {
      this.Name = String.Empty;
      this.Value = String.Empty;
    }

    public DocParm(string name, string value)
    {
      this.Name = name;
      this.Value = value;
    }
  }
}

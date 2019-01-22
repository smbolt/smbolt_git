using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class Prox
  {
    public string Name {
      get;
      set;
    }
    public Type Type {
      get;
      set;
    }
    public object Value {
      get;
      set;
    }
    public bool IsNull {
      get {
        return this.Value == null;
      }
    }

    public Prox()
    {
      this.Name = String.Empty;
      this.Type = null;
      this.Value = null;
    }

    public int ToInt32()
    {
      return this.Value.ToInt32();
    }

    public override string ToString()
    {
      if (this.Value == null)
        return String.Empty;

      return this.Value.ToString();
    }
  }
}

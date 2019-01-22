using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Pdf
{
  public class PObjectSet : Dictionary<string, PObject>
  {
    public PObjectSetType pObjectSetType {
      get;
      set;
    }
    public PObject Parent {
      get;
      private set;
    }

    public PObjectSet(PObject parent)
    {
      this.Parent = parent;
      this.pObjectSetType = PObjectSetType.NotSet;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class HtoSet : List<Hto>
  {
    public HtoSourceObjectType HtoSourceObjectType {
      get;
      private set;
    }
    public Hto Parent {
      get;
      private set;
    }

    public HtoSet(Hto parent, HtoSourceObjectType htoSourceObjectType)
    {
      this.HtoSourceObjectType = htoSourceObjectType;
      this.Parent = parent;
    }

    public new void Add(Hto hto)
    {
      hto.Seq = this.Count;
      base.Add(hto);
    }
  }
}

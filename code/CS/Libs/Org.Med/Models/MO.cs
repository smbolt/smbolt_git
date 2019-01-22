using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Med.Models
{
  public class MO
  {
    public MOType MOType {
      get;
      set;
    }
    public MO Parent {
      get;
      set;
    }
    public MOSet MOSet {
      get;
      set;
    }

    public MO(MO parent = null)
    {
      this.MOType = MOType.NotSet;
      this.Parent = parent;
      this.MOSet = new MOSet();
    }
  }
}

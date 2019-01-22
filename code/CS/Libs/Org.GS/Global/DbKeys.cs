using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class DbKeys : Dictionary<string, object>
  {
    public Dictionary<string, DbKeys> ChildKeys {
      get;
      set;
    }
    public DbKeys()
    {
      this.ChildKeys = new Dictionary<string, DbKeys>();
    }
  }
}

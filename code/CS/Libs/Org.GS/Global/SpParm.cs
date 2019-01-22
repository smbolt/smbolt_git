using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class SpParm
  {
    public string ParmName {
      get;
      set;
    }
    public object ParmValue {
      get;
      set;
    }

    public SpParm(string parmName, object parmValue)
    {
      this.ParmName = parmName;
      this.ParmValue = parmValue;
    }
  }
}

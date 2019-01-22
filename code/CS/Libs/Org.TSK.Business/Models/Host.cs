using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.TSK.Business.Models
{
  public class Host
  {
    public int HostID {
      get;
      set;
    }
    public string HostName {
      get;
      set;
    }

    public Host()
    {
      this.HostID = 0;
      this.HostName = String.Empty;
    }
  }
}

using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DynamoDB
{
  public class DbParms
  {
    public bool UseLocalInstance {
      get;
      set;
    }
    public string Host {
      get;
      set;
    }
    public int Port {
      get;
      set;
    }
    public int TcpConnectWaitMilliseconds {
      get;
      set;
    }

    public string SerivceURL {
      get {
        return Get_ServiceURL();
      }
    }

    public DbParms()
    {
      this.TcpConnectWaitMilliseconds = 3000;
    }

    private string Get_ServiceURL()
    {
      if (this.Host.IsBlank())
        throw new Exception("The host name is null or blank.");

      if (this.Port == 0)
        throw new Exception("The port number is 0 (default integer).");

      return "http://" + this.Host.Trim() + ":" + this.Port.ToString();
    }
  }
}

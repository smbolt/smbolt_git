using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.WSO
{
  public class WsHost
  {
    public bool IsUsed {
      get;
      set;
    }
    public string DomainName {
      get;
      set;
    }
    public string ComputerName {
      get;
      set;
    }
    public string IPAddress {
      get;
      set;
    }
    public string UserName {
      get;
      set;
    }
    public string DomainAndComputer {
      get {
        return this.DomainName + @"\" + this.ComputerName;
      }
    }
    public string DomainAndUser {
      get {
        return this.DomainName + @"\" + this.UserName;
      }
    }

    public WsHost()
    {
      this.IsUsed = false;
      this.DomainName = String.Empty;
      this.ComputerName = String.Empty;
      this.IPAddress = String.Empty;
      this.UserName = String.Empty;
    }
  }
}

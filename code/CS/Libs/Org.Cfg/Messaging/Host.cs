using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Cfg.Messaging
{
  public class Host
  {
    public bool IsUsed { get; set; }
    public string DomainName { get; set; }
    public string ComputerName { get; set; }
    public string IPAddress { get; set; }
    public string UserName { get; set; }
    public string DomainAndComputer { get { return this.DomainName + @"\" + this.ComputerName; } }
    public string DomainAndUser { get { return this.DomainName + @"\" + this.UserName; } } 

    public Host()
    {
      this.IsUsed = false;
      this.DomainName = String.Empty;
      this.ComputerName = String.Empty;
      this.IPAddress = String.Empty;
      this.UserName = String.Empty;
    }
  }
}

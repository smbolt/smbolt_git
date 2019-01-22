using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Principal;
using Org.GS;
using Org.GS.Configuration;

namespace Org.GS.Security
{
  public class UserSession
  {
    public WindowsPrincipal WindowsPrincipal { get; set; }
    public bool IsAuthenticated { get { return Get_IsAuthenticated(); } }
    public string DomainUserName { get { return Get_DomainUserName(); } }
    public ConfigSecurity ConfigSecurity { get; set; }

    // constructor
    public UserSession()
    {
      this.WindowsPrincipal = null;
      this.ConfigSecurity = new ConfigSecurity();
    }

    private string Get_DomainUserName()
    {
      if (this.WindowsPrincipal == null)
        return String.Empty;

      if (this.WindowsPrincipal.Identity == null)
        return String.Empty;

      return this.WindowsPrincipal.Identity.Name;
    }

    private bool Get_IsAuthenticated()
    {
      if (this.WindowsPrincipal == null)
        return false;

      if (this.WindowsPrincipal.Identity == null)
        return false;

      return this.WindowsPrincipal.Identity.IsAuthenticated;
    }
  }
}

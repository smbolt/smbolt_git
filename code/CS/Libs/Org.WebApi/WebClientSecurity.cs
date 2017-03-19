using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.WebApi
{
  public class WebClientSecurity
  {
    public List<string> LoggedInUserGroups { get; set; }
    public List<string> LoggedInUserFunctions { get; set; }
    public string DomainUserName { get; set; }
    public List<WebClientSecurity> SecurityAll { get; set; }

    public WebClientSecurity()
    {
      this.LoggedInUserGroups = new List<string>();
      this.LoggedInUserFunctions = new List<string>();
      this.DomainUserName = String.Empty;
      this.SecurityAll = new List<WebClientSecurity>();
    }
  }
}

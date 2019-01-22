using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.ApiModels
{
  public class LoginResult
  {
    public bool isLoginResult = true;
    public int AccountId { get; set; }
    public string UserName { get; set; }
    public int OrgId { get; set; }
    public string Token { get; set; }
    public string TokenDebug { get; set; }
    public bool IsNewRegistration { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserDisplayName { get; set; }
  }
}

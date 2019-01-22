using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.ApiModels
{
  public class LoginModel : ApiModelBase
  {
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsNewRegistration { get; set; }

    public LoginModel()
    {
      this.UserName = String.Empty;
      this.Password = String.Empty;
      this.IsNewRegistration = false;
    }
  }
}

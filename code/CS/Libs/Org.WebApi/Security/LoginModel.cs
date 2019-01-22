using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.WebApi.Security
{
  public class LoginModel
  {
    public int ID { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Stamp { get; set; }

    public LoginModel()
    {
      this.ID = 0;
      this.UserName = String.Empty;
      this.Password = String.Empty;
      this.Stamp = String.Empty;
    }
  }
}

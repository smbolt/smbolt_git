using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.ApiModels
{
  public class PersonModel : ApiModelBase
  {
    public string FirstName { get; set; }
    public string Password { get; set; }
    public bool IsNewRegistration { get; set; }

    public PersonModel()
    {
      this.FirstName = String.Empty;
      this.Password = String.Empty;
      this.IsNewRegistration = false;
    }
  }
}

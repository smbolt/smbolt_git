using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.ApiModels
{
  public class PasswordResetModel : ApiModelBase
  {
    public int Option { get; set; }
    public string ResetData { get; set; }
    public string SecurityAnswer { get; set; }
  }
}

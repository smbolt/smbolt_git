using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.ApiModels
{
  public class AuthValidationModel : ApiModelBase
  {
    public bool IsAuthorized {
      get;
      set;
    }
    public int AuthReasonCode {
      get;
      set;
    }
  }
}

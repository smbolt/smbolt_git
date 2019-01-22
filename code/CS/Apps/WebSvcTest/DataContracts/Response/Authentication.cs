using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldingSystems.FieldVisor.API.DataContracts.Response
{
  public class Authentication : BaseResponse
  {
    public string AuthID {
      get;
      set;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Org.GS;

namespace Org.ApiClient
{
  public class TokenResponseWrapper
  {
    public string TokenRequestUrl {
      get;
      set;
    }
    public TokenResponseStatus TokenResponseStatus {
      get;
      set;
    }
    public TokenResponse TokenResponse {
      get;
      set;
    }

    public TokenResponseWrapper()
    {
      this.TokenResponseStatus = TokenResponseStatus.TokenNotRequested;

    }
  }
}

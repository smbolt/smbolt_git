using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.ApiClient
{
  public enum DiscoveryResponseStatus
  {
    DiscoveryNotAttempted,
    DiscoveryError,
    DiscoverySuccessful
  }

  public enum TokenResponseStatus
  {
    TokenNotRequested,
    TokenRequestError,
    TokenRequestSuccessful
  }
}

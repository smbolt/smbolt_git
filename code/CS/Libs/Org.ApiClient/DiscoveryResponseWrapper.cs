using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Org.GS;

namespace Org.ApiClient
{
  public class DiscoveryResponseWrapper
  {
    public string DiscoveryUrl {
      get;
      set;
    }
    public DiscoveryResponseStatus DiscoveryResponseStatus {
      get;
      set;
    }
    public DiscoveryResponse DiscoveryResponse {
      get;
      set;
    }

    public DiscoveryResponseWrapper()
    {
      this.DiscoveryResponseStatus = DiscoveryResponseStatus.DiscoveryNotAttempted;
    }
  }
}

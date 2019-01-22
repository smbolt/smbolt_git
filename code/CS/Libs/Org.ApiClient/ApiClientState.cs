using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Org.GS;

namespace Org.ApiClient
{
    public class ApiClientState
    {
        public DiscoveryResponseWrapper DiscoveryResponseWrapper { get; set; }
        public TokenResponseWrapper TokenResponseWrapper { get; set; }

        public ApiClientState()
        {
            this.DiscoveryResponseWrapper = new DiscoveryResponseWrapper();
            this.TokenResponseWrapper = new TokenResponseWrapper();

        }
    }
}

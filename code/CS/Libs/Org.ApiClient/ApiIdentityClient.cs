using System;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Org.ApiClient
{
    public class ApiIdentityClient : IDisposable
    {
        public async Task<DiscoveryResponse> DiscoverEndpoints(string url)
        {
            try
            {
                var discoveryResponse = await DiscoveryClient.GetAsync(url + "/.well-known/openid-configuration");
                return discoveryResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An exception occurred while attempting to discover the OAuth endpoints.", ex);
            }
        }


        public void Dispose()
        {

        }
    }
}

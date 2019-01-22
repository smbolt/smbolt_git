using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Org.ApiClient
{
    public class ApiTokenClient : IDisposable
    {
        private DiscoveryResponse _discoveryResponse;

        public ApiTokenClient(DiscoveryResponse discoveryResponse)
        {
            _discoveryResponse = discoveryResponse;
        }

        public async Task<TokenResponse> RequestToken(string grantType, string url, string resource, string client, string secret, string user = "", string password = "")
        {
            try
            {
                var tokenClient = new TokenClient(url, client, secret);
                TokenResponse tokenResponse = null;

                switch (grantType)
                {
                    case "ClientCredentials":
                        tokenResponse = await tokenClient.RequestClientCredentialsAsync(resource);
                        return tokenResponse;

                    case "ResourceOwnerPassword":
                        tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(user, password, resource);
                        return tokenResponse;

                    default:
                        throw new Exception("The RequestToken method is not yet implemented for grant type '" + grantType + "'.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An exception occurred while requesting a token.", ex);
            }
        }


        public void Dispose()
        {

        }
    }
}

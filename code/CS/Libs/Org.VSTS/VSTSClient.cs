using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Org.GS;


namespace Org.VSTS
{
  public class VSTSClient : IDisposable
  {
    private VssCredentials _vssCredentials;
    private string _personalAccessToken;
    private string _vssConnectionUri;
    private VssConnection _vssConnection;

    public VSTSClient(string vssConnectionUri, string personalAccessToken)
    {
      _personalAccessToken = personalAccessToken;
      _vssConnectionUri = vssConnectionUri;
      _vssConnection = Connect();
    }

    private VssConnection Connect()
    {
      try
      {
        _vssCredentials = new VssBasicCredential("stephen.m.bolt@gmail.com", "gen126@Msdn");
        var connection = new VssConnection(new Uri(_vssConnectionUri), _vssCredentials);
        return connection;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to establish a connection to VSTS at '" +
                            _vssConnectionUri + "'.", ex);
      }
    }

    public string GetAccounts()
    {
      try
      {
        var accountClient = _vssConnection.GetClient<Microsoft.VisualStudio.Services.Account.Client.AccountHttpClient>();
        var accounts = accountClient.GetAccount(String.Empty);



        return "Account list";
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get a list of Accounts.", ex);
      }
    }

    public string GetUsers()
    {
      try
      {

        using (var graphClient = new Microsoft.VisualStudio.Services.Organization.Client.OrganizationHttpClient(new Uri(_vssConnectionUri), _vssCredentials))
        {
          var accounts = graphClient.GetAccountsAsync().Result;


        }

        return "User list";
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get a list of Users.", ex);
      }
    }

    public async Task<string> GetWebApiToken()
    {
      try
      {
        using (var client = new System.Net.Http.HttpClient())
        {
          client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

          client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
              Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                  string.Format("{0}:{1}", "", _personalAccessToken))));

          using (System.Net.Http.HttpResponseMessage response = await client.GetAsync(
                   "https://gulfportenergy.visualstudio.com/DefaultCollection/_apis/projects"))
          {
            response.EnsureSuccessStatusCode();
            var resultValue = await response.Content.ReadAsStringAsync().ContinueWith(r =>
            {
              var result = r.Result;
              string jsonFmt = Newtonsoft.Json.Linq.JObject.Parse(result).ToString();
              return jsonFmt;
            });
            return resultValue.ToString();
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get a list of Users.", ex);
      }
    }

    public void Dispose()
    {

    }
  }
}

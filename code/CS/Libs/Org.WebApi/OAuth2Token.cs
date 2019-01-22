using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Org.GS;

namespace Org.WebApi
{
  public class OAuth2Token
  {
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string TokenType { get; set; }
    public string Appcp { get; set; }
    public string Apicp { get; set; }
    public string Subdomain { get; set; }
    public int ExpiresIn { get; set; }

    public OAuth2Token(JObject json)
    {
      if (json != null)
      {
        AccessToken = (string)json["access_token"];
        RefreshToken = (string)json["refresh_token"];
        TokenType = (string)json["token_type"];
        Appcp = (string)json["appcp"];
        Apicp = (string)json["apicp"];
        Subdomain = (string)json["subdomain"];
        ExpiresIn = (int)json["expires_in"];
      }
      else
      {
        AccessToken = "";
        RefreshToken = "";
        TokenType = "";
        Appcp = "";
        Apicp = "";
        Subdomain = "";
        ExpiresIn = 0;
      }
    }
  }
}
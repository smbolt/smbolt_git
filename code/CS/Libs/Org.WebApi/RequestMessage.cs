using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Org.WebApi
{
  public class RequestMessage
  {
    private HttpRequestMessage _httpRequestMessage;
    private Dictionary<string, string> _items;

    public string ClientIpAddress { get { return Get_ClientIpAddress(); } }
    public string AuthorizationToken { get { return Get_AuthorizationToken(); } }

    public RequestMessage(HttpRequestMessage httpRequestMessage)
    {
      _httpRequestMessage = httpRequestMessage;
      LoadItems();
    }

    public void LoadItems()
    {
      _items = new Dictionary<string, string>();

      var contextWrapper = _httpRequestMessage.Properties["MS_HttpContext"];
      if (contextWrapper != null)
      {
        var requestWrapper = ((System.Web.HttpContextWrapper)contextWrapper).Request;
        if (requestWrapper != null)
        {
          for (int i = 0; i < requestWrapper.Params.Count; i++)
          {
            string key = requestWrapper.Params.Keys[i];
            if (key != null)
            {
              string[] values = requestWrapper.Params.GetValues(i);
              if (!_items.ContainsKey(key) && values.Length > 0)
              {
                _items.Add(key, values[0]);
              }
            }
          }
        }
      }
    }

    private string Get_ClientIpAddress()
    {
      if (_items.ContainsKey("REMOTE_ADDR"))
        return _items["REMOTE_ADDR"];
      return String.Empty;
    }

    private string Get_AuthorizationToken()
    {
      if (_items.ContainsKey("HTTP_TOKEN"))
        return _items["HTTP_TOKEN"];
      return String.Empty;
    }
  }
}

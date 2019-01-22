using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.ApiHost
{
  public class WebHost : IDisposable
  {
    private string _url;
    private IDisposable _webApp;

    public WebHost(string url)
    {
      _url = url;
    }


    public void Start()
    {
      try
      {
        _webApp = WebApp.Start<Startup>(_url);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to start the WebApp on URL '" + _url + "'.", ex);
      }
    }

    public void Stop()
    {
      _webApp?.Dispose();
    }

    public void Dispose()
    {
    }
  }
}

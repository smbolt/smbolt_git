using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.ApiHost
{
  public class WebRequestHandler
  {
    private IEnumerable<Route> Routes {
      get;
      set;
    }

    public WebRequestHandler(IEnumerable<Route> routes)
    {
      this.Routes = routes;
    }

    public async Task<object> Invoke(IDictionary<string, object> env)
    {
      var handler = RequestFactory.Get(env, this.Routes);
      await handler.Handle();
      return Task.FromResult<object>(null);
    }
  }
}

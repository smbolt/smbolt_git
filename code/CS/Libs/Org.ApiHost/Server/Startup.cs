using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Org.ApiHost.Controllers;

namespace Org.ApiHost
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      var config = new HttpConfiguration();

      config.Routes.MapHttpRoute(
        name: "DefaultApi",
        routeTemplate: "api/{controller}/{id}",
        defaults: new { id = RouteParameter.Optional }
      );


      app.UseWebApi(config);

      app.Use(new Func<object, object>(
                x => new WebRequestHandler(new List<Route>
      {
        new Route("Home", typeof(HomeController))
      })
              ));
    }

  }
}

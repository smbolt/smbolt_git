using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Org.ApiHost;
using Org.ApiHost.Models;

namespace Org.ApiHost.Controllers
{
  public class HomeController
  {
    public IView Index()
    {
      return new View("Index", new Home { AppName = "WebHost" });
    }

    public IView All()
    {
      var result = new List<Home>();

      for (int i = 0; i < 100; i++)
      {
        result.Add(new Home { AppName = "WebHost_" + i.ToString() });
      }

      return new View("All", result);
    }
  }
}

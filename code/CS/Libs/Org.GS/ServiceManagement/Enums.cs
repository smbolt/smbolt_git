using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.ServiceManagement
{
  public enum ServiceType
  {
    WindowsService = 1,
    WCFWebService = 2,
    WebSite = 3,
    Unidentified = 9
  }

  public enum ServiceObjectType
  {
    NotSet,
    ServiceEnvironment,
    ServiceHost,
    ServiceSpec    
  }
}

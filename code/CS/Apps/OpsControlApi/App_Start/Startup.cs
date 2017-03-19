using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Org.DB;
using Org.OpsControlApi.Services;
using Org.GS.Security;
using Org.GS;

namespace Org.OpsControlApi
{
  public static class Startup
  {
    public static void Initialize()
    {
      g.TimeoutMinutes = WebConfigurationManager.AppSettings["timeOutMinutes"].ToInt32OrDefault(30);
      ConnStringManager.Load(WebConfigurationManager.AppSettings["connStringMap"]);

      string taskDbPrefix = g.CI("TasksDbPrefix");
      ScheduledTaskService.ConfigDbSpec = g.GetDbSpec(taskDbPrefix); 
    }
  }
}

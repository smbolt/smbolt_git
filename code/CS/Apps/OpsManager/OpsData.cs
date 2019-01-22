using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.Configuration;
using Org.TSK.Business.Models;
using tsk = Org.TSK.Business.Models;
using Org.WSO;

namespace Org.OpsManager
{
  public class OpsData
  {
    public Dictionary<int, string> AppLogEntities {
      get;
      set;
    }
    public Dictionary<int, string> AppLogModules {
      get;
      set;
    }
    public Dictionary<int, string> AppLogEvents {
      get;
      set;
    }
    public GridViewSet GridViewSet {
      get;
      set;
    }
    public ConfigDbSpec TasksDbSpec {
      get;
      set;
    }
    public ConfigDbSpec LoggingDbSpec {
      get;
      set;
    }
    public ConfigDbSpec NotifyDbSpec {
      get;
      set;
    }
    public ConfigWsSpec OpsMgmt01WsSpec {
      get;
      set;
    }
    public string Environment {
      get;
      set;
    }
    public ScheduledTask CurrentScheduledTask {
      get;
      set;
    }
    public tsk.TaskSchedule CurrentTaskSchedule {
      get;
      set;
    }
    public tsk.TaskScheduleElement CurrentScheduleElement {
      get;
      set;
    }
    public TaskParameter CurrentTaskParameter {
      get;
      set;
    }
    public TaskParameterSet CurrentTaskParameterSet {
      get;
      set;
    }

    public OpsData()
    {
      this.AppLogEntities = new Dictionary<int, string>();
      this.AppLogModules = new Dictionary<int, string>();
      this.AppLogEvents = new Dictionary<int, string>();
      this.GridViewSet = new GridViewSet();
      this.CurrentScheduledTask = new ScheduledTask();
      this.CurrentTaskSchedule = new tsk.TaskSchedule();
      this.CurrentScheduleElement = new tsk.TaskScheduleElement();
      this.CurrentTaskParameter = new TaskParameter();
      this.CurrentTaskParameterSet = new TaskParameterSet();
    }
  }
}

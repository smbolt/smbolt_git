using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.ServiceManagement;
using Org.GS;

namespace Org.TSK.Business.Models
{
  public class TaskService
  {
    public int TaskServiceID {
      get;
      set;
    }
    public int HostID {
      get;
      set;
    }
    public string HostName {
      get;
      set;
    }
    public int? ParentServiceID {
      get;
      set;
    }
    public string TaskServiceName {
      get;
      set;
    }
    public ServiceType ServiceType {
      get;
      set;
    }
    public WebServiceBinding WCFServiceBinding {
      get;
      set;
    }
    public string WCFServicePort {
      get;
      set;
    }
    public string WCFServiceName {
      get;
      set;
    }

    public TaskService()
    {
      this.TaskServiceID = 0;
      this.HostID = 0;
      this.HostName = String.Empty;
      this.ParentServiceID = null;
      this.TaskServiceName = String.Empty;
      this.ServiceType = ServiceType.Unidentified;
      this.WCFServiceBinding = WebServiceBinding.NotSet;
      this.WCFServicePort = String.Empty;
      this.WCFServiceName = String.Empty;
    }
  }
}

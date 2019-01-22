using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.WebApi.Models;

namespace Org.Ops.ApiModels
{
  public class ScheduledTask : ApiModelBase
  {
    public int ScheduledTaskId {
      get;
      set;
    }
    public string TaskName {
      get;
      set;
    }
    public int ProcessorTypeId {
      get;
      set;
    }
    public string ProcessorName {
      get;
      set;
    }
    public string ProcessorVersion {
      get;
      set;
    }
    public string AssemblyLocation {
      get;
      set;
    }
    public string StoredProcedureName {
      get;
      set;
    }
    public bool IsActive {
      get;
      set;
    }
    public bool IsLongRunning {
      get;
      set;
    }
    public bool TrackHistory {
      get;
      set;
    }
    public int? ActiveScheduleId {
      get;
      set;
    }
    public string CreatedBy {
      get;
      set;
    }
    public System.DateTime CreatedDate {
      get;
      set;
    }
    public string ModifiedBy {
      get;
      set;
    }
    public DateTime? ModifiedDate {
      get;
      set;
    }
  }
}

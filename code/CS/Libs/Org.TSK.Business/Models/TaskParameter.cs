using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.TSK.Business.Models
{
  public class TaskParameter
  {
    public int ParameterID {
      get;
      set;
    }
    public int? ScheduledTaskID {
      get;
      set;
    }
    public string ParameterSetName {
      get;
      set;
    }
    public string ParameterName {
      get;
      set;
    }
    public string ParameterValue {
      get;
      set;
    }
    public string DataType {
      get;
      set;
    }
    public string CreatedBy {
      get;
      set;
    }
    public DateTime CreatedDate {
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

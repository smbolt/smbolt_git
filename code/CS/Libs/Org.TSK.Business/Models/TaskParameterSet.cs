using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.TSK.Business.Models
{
  public class TaskParameterSet
  {
    public string ParameterSetName {
      get;
      set;
    }
    public List<TaskParameter> TaskParameters {
      get;
      set;
    }

    public TaskParameterSet()
    {
      this.TaskParameters = new List<TaskParameter>();
    }
  }
}

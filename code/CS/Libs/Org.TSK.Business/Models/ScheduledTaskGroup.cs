using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.TSK.Business.Models
{
  public class ScheduledTaskGroup
  {
    public int TaskGroupId {
      get;
      set;
    }
    public string TaskGroupName {
      get;
      set;
    }

    public ScheduledTaskGroup()
    {
      this.TaskGroupId = 0;
      this.TaskGroupName = String.Empty;
    }
  }
}

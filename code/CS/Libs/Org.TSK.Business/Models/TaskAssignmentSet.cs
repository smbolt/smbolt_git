using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.TSK.Business.Models
{
  public class TaskAssignmentSet : List<TaskAssignment>
  {
    public bool IsTaskAssigned(string taskName)
    {
      return this.Where(t => t.TaskName.ToUpper().Trim() == taskName.ToUpper().Trim()).Count() > 0;
    }
  }
}

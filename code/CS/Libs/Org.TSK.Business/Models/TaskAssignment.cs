using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TSK.Business.Models
{
  public class TaskAssignment
  {
    public int TaskServiceAssignmentID { get; set; }
    public int TaskServiceID { get; set; }
    public string TaskServiceName { get; set; }
    public int ServiceHostID { get; set; }
    public string HostName { get; set; }
    public int ScheduledTaskID { get; set; }
    public string TaskName { get; set; }
    public bool IsActive { get; set; }

    public TaskAssignment()
    {
      this.TaskServiceAssignmentID = 0;
      this.TaskServiceID = 0;
      this.TaskServiceName = String.Empty;
      this.ServiceHostID = 0;
      this.HostName = String.Empty;
      this.ScheduledTaskID = 0;
      this.TaskName = String.Empty;
      this.IsActive = false;
    }
  }
}

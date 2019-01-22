using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.TSK.Business.Models
{
  public class TaskSchedule
  {
    public int TaskScheduleId {
      get;
      set;
    }
    public int ScheduledTaskId {
      get;
      set;
    }
    public string ScheduleName {
      get;
      set;
    }
    public int? ScheduleNumber {
      get;
      set;
    }
    public bool IsActive {
      get;
      set;
    }
    public bool IsManaged {
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

    public List<TaskScheduleElement> TaskScheduleElements {
      get;
      set;
    }

    public TaskSchedule()
    {
      this.TaskScheduleElements = new List<TaskScheduleElement>();
    }
  }
}

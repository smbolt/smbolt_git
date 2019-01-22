//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Org.GS;
using Org.DB;
namespace Org.BMR.Data.Entities
{
  using System;
  using System.Collections.Generic;

  [DbMap(DbElement.Table, "Adsdi_Org", "", "TaskSchedule")]
  public partial class TaskSchedule
  {
    public TaskSchedule()
    {
      this.TaskScheduleElements = new HashSet<TaskScheduleElement>();
    }

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
    public Nullable<int> ScheduleNumber {
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
    public int CreatedBy {
      get;
      set;
    }
    public System.DateTime CreatedDate {
      get;
      set;
    }
    public int ModifiedBy {
      get;
      set;
    }
    public System.DateTime ModifiedDate {
      get;
      set;
    }

    public virtual ScheduledTask ScheduledTask {
      get;
      set;
    }
    public virtual ICollection<TaskScheduleElement> TaskScheduleElements {
      get;
      set;
    }
  }
}

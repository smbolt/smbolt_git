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
    
    [DbMap(DbElement.Table, "Adsdi_Org", "", "ScheduledTask")]
    public partial class ScheduledTask
    {
        public ScheduledTask()
        {
            this.ScheduledTaskParameters = new HashSet<ScheduledTaskParameter>();
            this.TaskSchedules = new HashSet<TaskSchedule>();
        }
    
        public int ScheduledTaskId { get; set; }
        public string TaskName { get; set; }
        public bool IsManaged { get; set; }
        public Nullable<int> TaskNumber { get; set; }
        public string AssemblyName { get; set; }
        public string AssemblyLocation { get; set; }
        public string ClassName { get; set; }
        public string StoredProcedureName { get; set; }
        public bool IsActive { get; set; }
        public bool IsLongRunning { get; set; }
        public Nullable<int> ActiveScheduleId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual ICollection<ScheduledTaskParameter> ScheduledTaskParameters { get; set; }
        public virtual ICollection<TaskSchedule> TaskSchedules { get; set; }
    }
}

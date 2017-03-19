using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;

namespace Org.TSK.Business.Models
{
	public class TaskScheduleExecutionType
	{
		public int TaskScheduleExecutionTypeId { get; set; }
		public string TaskExecutionTypeDesc { get; set; }
		public TaskExecutionType TaskExecutionType { get { return g.ToEnum<TaskExecutionType>(this.TaskScheduleExecutionTypeId, TaskExecutionType.NotSet); } }
	}
}

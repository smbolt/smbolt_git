using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.Configuration;
using Org.GS;

namespace Org.TSK.Business.Models
{
	public class TaskScheduleIntervalType
	{
		public int IntervalTypeId { get; set; }
		public string IntervalTypeDesc { get; set; }
		public IntervalType IntervalType { get { return g.ToEnum<IntervalType>(this.IntervalTypeId, IntervalType.NotSet); } }
	}
}

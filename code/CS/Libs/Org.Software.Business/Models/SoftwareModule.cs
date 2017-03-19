using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Software.Business.Models
{
	public class SoftwareModule
	{
		public int SoftwareModuleId { get; set; }
		public int SoftwareModuleCode { get; set; }
		public string SoftwareModuleName { get; set; }
		public int SoftwareModuleTypeId { get; set; }
		public int SoftwareStatusId { get; set; }
	}
}

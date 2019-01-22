using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Software.Business.Models
{
	public class FrameworkVersion
	{
		public int FrameworkVersionId { get; set; }
		public int SoftwareStatusId { get; set; }
		public string FrameworkVersionString { get; set; }
		public string Version { get; set; }
		public string VersionNum { get; set; }
		public string ServicePackString { get; set; }
	}
}

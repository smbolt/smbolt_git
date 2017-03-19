using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Software.Business.Models
{
	public class SoftwareVersion
	{
		public int SoftwareVersionId { get; set; }
		public int SoftwareStatusId { get; set; }
		public int SoftwareModuleId { get; set; }
		public string VersionValue { get; set; }
		public int SoftwarePlatformId { get; set; }
		public int RepositoryId { get; set; }
	}
}

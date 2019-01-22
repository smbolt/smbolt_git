using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Org.Cfg;
//using Org.Cfg.Messaging;

namespace Org.JsonTest
{
  public class GetAssemblyReportResponse
  {
    public string AssemblyReport { get; set; }

    public GetAssemblyReportResponse()
    {
      this.AssemblyReport = string.Empty;
    }
  }
}

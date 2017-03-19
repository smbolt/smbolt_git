using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Logging
{
  public class AppLogDetail
  {
    public int LogDetailId { get; set; }
    public int LogId { get; set; }
    public LogDetailType LogDetailType { get; set; }
    public int SetId { get; set; }
    public string LogDetail { get; set; }

    public AppLogDetail()
    {
      this.LogDetailId = 0;
      this.LogId = 0;
      this.SetId = 0;
      this.LogDetailType = LogDetailType.NotSet;
      this.LogDetail = String.Empty;
    }
  }
}

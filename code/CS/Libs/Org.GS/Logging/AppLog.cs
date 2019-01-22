using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Logging
{
  public class AppLog
  {
    public long LogId { get; set; }
    public DateTime LogDateTime { get; set; }
    public LogSeverity SeverityCode { get; set; }
    public string Message { get; set; }
    public int? ModuleId { get; set; }
    public int? EventCode { get; set; }
    public int? EntityId { get; set; }
    public int? RunId { get; set; }
    public string UserName { get; set; }
    public string ClientHost { get; set; }
    public string ClientIp { get; set; }
    public string ClientUser { get; set; }
    public string ClientApplication { get; set; }
    public string ClientApplicationVersion { get; set; }
    public string TransactionName { get; set; }
    public bool NotificationSent { get; set; }
    public AppLogDetailSet AppLogDetailSet { get; set; }

    public AppLog()
    {
      this.LogId = 0;
      this.LogDateTime = DateTime.MinValue;
      this.SeverityCode = LogSeverity.INFO;
      this.Message = String.Empty;
      this.AppLogDetailSet = new AppLogDetailSet();
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Cfg.Messaging
{
  public class MessageDateTime
  {
    public bool IsUsed {
      get;
      set;
    }
    public DateTime? DateTime {
      get;
      set;
    }
    public TimeZoneInfo TimeZoneInfo {
      get;
      set;
    }

    public MessageDateTime()
    {
      this.IsUsed = false;
      this.DateTime = (DateTime?) null;
      this.TimeZoneInfo = TimeZoneInfo.Utc;
    }

    public MessageDateTime(DateTime dateTime, TimeZoneInfo timeZoneInfo)
    {
      this.IsUsed = true;
      this.DateTime = dateTime;
      this.TimeZoneInfo = timeZoneInfo;
    }
  }
}

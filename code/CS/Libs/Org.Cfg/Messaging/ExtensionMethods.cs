using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Cfg.Messaging
{
  public static class ExtensionMethods
  {
    public static DateTime? ToLocalDateTime(this MessageDateTime mdt)
    {
      if (mdt == null || !mdt.DateTime.HasValue)
        return (DateTime?)null;           

      TimeZoneInfo tzLocal = TimeZoneInfo.Local;
      return TimeZoneInfo.ConvertTime(mdt.DateTime.Value, mdt.TimeZoneInfo, TimeZoneInfo.Local);
    }

    public static Message ToMessage(this JsonObject j)
    {
      var m = new Message();


      return m;
    }
  }
}

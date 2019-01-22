using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Logging
{
  public class LogDetail
  {
    public int LogDetailId {
      get;
      set;
    }
    public int LogId {
      get;
      set;
    }
    public int SetId {
      get;
      set;
    }
    public LogDetailType LogDetailType {
      get;
      set;
    }
    public string DetailData {
      get;
      set;
    }

    public LogDetail()
    {
      this.LogDetailId = 0;
      this.LogId = 0;
      this.SetId = 0;
      this.LogDetailType = LogDetailType.NotSet;
      this.DetailData = String.Empty;
    }
  }
}

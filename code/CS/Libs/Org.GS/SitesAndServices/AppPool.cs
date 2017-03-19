using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public enum AppPoolStatus
  {
    NotSet = -1,
    Starting = 0,
    Started = 1,
    Stopping = 2,
    Stopped = 3,
    Unknown = 99
  }
  
  public enum AppPoolCommand
  {
    Start,
    Stop
  }

  [Serializable()]
  [XMap(XType = XType.Element)]
  public class AppPool
  {
    [XMap]
    public string Name { get; set; }
    [XMap]
    public bool AutoStart { get; set; }
    [XMap]
    public bool Enable32BitAppOnWin64 { get; set; }
    [XMap]
    public AppPoolStatus AppPoolStatus { get; set; }

    public AppPool()
    {
      this.Name = String.Empty;
      this.AutoStart = true;
      this.Enable32BitAppOnWin64 = true;
      this.AppPoolStatus = GS.AppPoolStatus.NotSet;
    }
  }
}

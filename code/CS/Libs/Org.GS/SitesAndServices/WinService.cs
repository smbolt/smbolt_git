using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public enum WinServiceStatus
  {
    NotSet,
    Running,
    Starting,
    Started,
    Stopping,
    Stopped,
    Paused,
    Unknown
  }

  public enum WinServiceCommand
  {
    Start,
    Stop
  }

  [Serializable()]
  [XMap(XType = XType.Element)]
  public class WinService
  {
    [XMap]
    public string Name {
      get;
      set;
    }
    [XMap]
    public string MachineName {
      get;
      set;
    }
    [XMap]
    public WinServiceStatus WinServiceStatus {
      get;
      set;
    }
    [XMap]
    public bool CanPauseAndContinue {
      get;
      set;
    }
    [XMap]
    public bool CanStop {
      get;
      set;
    }


    public WinService()
    {
      this.Name = String.Empty;
      this.MachineName = String.Empty;
      this.WinServiceStatus = GS.WinServiceStatus.NotSet;
      this.CanPauseAndContinue = false;
      this.CanStop = false;
    }
  }
}

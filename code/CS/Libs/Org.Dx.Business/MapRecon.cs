using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class MapRecon
  {
    public string MapName { get; set; }
    public bool InMapsFolder { get; set; }
    public bool InOppositeEnvMapsFolder { get; set; }
    public MatchStatus MatchStatus { get; set; }
    public bool MapInDatabase { get; set; }
    public bool MapInOppositeDatabase { get; set; }
    public string TaskName { get; set; }
    public string OppositeTaskName { get; set; }
    public bool? TaskActive { get; set; }
    public bool? TaskActiveInOppositeEnv { get; set; }

    public MapRecon()
    {
      this.MapName = String.Empty;
      this.MatchStatus = MatchStatus.NotSet;
      this.TaskName = String.Empty;
      this.OppositeTaskName = String.Empty;
      this.TaskActive = null;
      this.TaskActiveInOppositeEnv = null;
    }
  }
}

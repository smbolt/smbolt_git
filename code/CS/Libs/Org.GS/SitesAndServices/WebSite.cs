using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public enum WebSiteStatus
  {
    NotSet = -1,
    Starting = 0,
    Started = 1,
    Stopping = 2,
    Stopped = 3,
    Unknown = 99
  }
  
  public enum SiteCommand
  {
    Start,
    Stop
  }

  [Serializable()]
  [XMap(XType = XType.Element)]
  public class WebSite
  {
    [XMap]
    public string Name { get; set; }
    [XMap]
    public int Id { get; set; }
    [XMap]
    public WebSiteStatus WebSiteStatus { get; set; }

    public WebSite()
    {
      this.Name = String.Empty;
      this.Id = 0;
      this.WebSiteStatus = GS.WebSiteStatus.NotSet;
    }
  }
}

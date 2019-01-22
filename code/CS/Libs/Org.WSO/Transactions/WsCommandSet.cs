using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element, CollectionElements = "WsCommand")]
  public class WsCommandSet : List<WsCommand>
  {
    [XMap(DefaultValue = "0")]
    public float DurationSeconds {
      get;
      set;
    }

    public WsCommandSet()
    {
      this.DurationSeconds = 0F;
    }

    public void SetDuration(DateTime beginDT, DateTime endDT)
    {
      TimeSpan tsDuration = endDT - beginDT;
      int totalSeconds = Convert.ToInt32(tsDuration.TotalSeconds);
      int milliseconds = tsDuration.Milliseconds;
      this.DurationSeconds = totalSeconds + (float)milliseconds / 1000;
    }
  }
}

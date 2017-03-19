using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  [XMap(XType = XType.Element, CollectionElements = "IpdxCommand")]
  public class IpdxCommandSet : List<IpdxCommand>
  {
    [XMap(DefaultValue = "0")]
    public float DurationSeconds { get { return Get_DurationSeconds(); } }

    public DateTime BeginDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public IpdxCommandSet()
    {
      this.BeginDateTime = DateTime.MinValue;
      this.EndDateTime = DateTime.MinValue;
    }

    public float Get_DurationSeconds()
    {
      TimeSpan tsDuration = this.EndDateTime - this.BeginDateTime;
      int totalSeconds = Convert.ToInt32(tsDuration.TotalSeconds);
      int milliseconds = tsDuration.Milliseconds;
      return totalSeconds + (float)milliseconds / 1000;
    }
  }
}

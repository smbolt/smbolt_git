using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Performance
{
  [XMap(XType=XType.Element, CollectionElements="PerfProfile")]
  public class PerfProfileSet : Dictionary<string, PerfProfile>
  {
  }
}

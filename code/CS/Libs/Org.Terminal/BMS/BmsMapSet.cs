using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Terminal.BMS
{
  public class BmsMapSet : Dictionary<string, BmsMap>
  {
    public Bms_DFHMSD Bms_DFHMSD { get; set; }

    public BmsMapSet()
    {
      this.Bms_DFHMSD = null;
    }
  }
}

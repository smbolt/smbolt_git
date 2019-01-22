using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Terminal.BMS
{
  public class BmsMap
  {
    public Bms_DFHMDI Bms_DFHMDI {
      get;
      set;
    }
    public string Name {
      get;
      set;
    }
    public List<Bms_DFHMDF> Bms_DFHMDF_Set {
      get;
      set;
    }

    public BmsMap()
    {
      this.Name = String.Empty;
      this.Bms_DFHMDI = null;
      this.Bms_DFHMDF_Set = new List<Bms_DFHMDF>();
    }
  }
}

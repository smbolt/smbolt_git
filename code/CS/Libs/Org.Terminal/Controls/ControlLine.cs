using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Terminal.Controls
{
  public class ControlLine : List<MFBase>
  {
    public bool IncludesHFlexControls {
      get {
        return Get_IncludesHFlexControls();
      }
    }
    public bool IncludesVFlexControls {
      get {
        return Get_IncludesVFlexControls();
      }
    }

    private bool Get_IncludesHFlexControls()
    {
      if (this.Count == 0)
        return false;

      foreach (MFBase control in this)
      {
        if (control.IsHFlexControl)
          return true;
      }

      return false;
    }

    private bool Get_IncludesVFlexControls()
    {
      if (this.Count == 0)
        return false;

      foreach (MFBase control in this)
      {
        if (control.IsVFlexControl)
          return true;
      }

      return false;
    }
  }
}

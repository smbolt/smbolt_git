using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Controls
{
  [XMap(XType=XType.Element, Name = "ControlSpec", ClassName = "ButtonSpec")]
  public class ButtonSpec : ControlSpecBase
  {
    [XMap]
    public override ControlType ControlType { get; set; }

    public ButtonSpec()
    {
      this.ControlType = ControlType.ButtonSpec;
    }
  }
}

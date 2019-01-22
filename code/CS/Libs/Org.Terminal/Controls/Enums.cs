using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Terminal.Controls
{
  public enum EventType
  {
    NotSet,
    KeyDown,
    KeyPress,
    KeyUp,
    Click,
    DoubleClick,
    Paint,
    Enter,
    Leave,
    MouseEnter,
    MouseMove,
    MouseLeave,
    CursorBlink,
    ScreenResize
  }

  public enum EventCommand
  {
    UpdateInfoPanel,
    NotSet
  }


}

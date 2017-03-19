using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.GS.UI
{
  public enum UIWindowState
  {
    Normal = 0,
    Minimized,
    Maximized
  }

  public enum StartPosition
  {
    CenterScreen = 0,
    Manual
  }

  public enum WindowType
  {
    Normal = 0,
    ToolWindow
  }

  public enum SizeMode
  {
    Literal = 0,
    PercentOfScreen
  }

  [XMap(XType = XType.Element)]
  public class UIState
  {
    [XMap(XType = XType.Element, CollectionElements = "UIWindow", WrapperElement = "UIWindowSet")]
    public UIWindowSet UIWindowSet { get; set; }

    public UIState()
    {
        this.UIWindowSet = new UIWindowSet();
    }
  }
}

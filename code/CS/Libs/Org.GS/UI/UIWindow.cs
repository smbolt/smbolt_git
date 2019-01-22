using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.GS.UI
{
  [XMap(XType = XType.Element)]
  public class UIWindow
  {
    [XMap(IsKey = true)]
    public string Name {
      get;
      set;
    }

    [XMap(IsRequired = true)]
    public string TypeName {
      get;
      set;
    }

    [XMap(DefaultValue = "True")]
    public bool IsActive {
      get;
      set;
    }

    [XMap]
    public string WindowTitle {
      get;
      set;
    }

    [XMap(DefaultValue = "Normal")]
    public UIWindowState UIWindowState {
      get;
      set;
    }

    [XMap]
    public StartPosition StartPosition {
      get;
      set;
    }

    [XMap(DefaultValue = "False")]
    public bool IsMainForm {
      get;
      set;
    }

    [XMap(DefaultValue = "True")]
    public bool IsDockable {
      get;
      set;
    }

    [XMap(DefaultValue = "Normal")]
    public WindowType WindowType {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public WindowLocation WindowLocation {
      get;
      set;
    }

    public UIWindow()
    {
      this.Name = String.Empty;
      this.TypeName = String.Empty;
      this.IsActive = true;
      this.WindowTitle = String.Empty;
      this.UIWindowState = UIWindowState.Normal;
      this.StartPosition = StartPosition.CenterScreen;
      this.IsMainForm = false;
      this.IsDockable = true;
      this.WindowType = WindowType.Normal;
      this.WindowLocation = new WindowLocation();
    }

    public void AutoInit()
    {
      if (this.WindowLocation.Size.Width == 0 && this.WindowLocation.Size.Height == 0)
      {
        this.WindowLocation.Size = new Size(100, 100);
      }
    }
  }
}

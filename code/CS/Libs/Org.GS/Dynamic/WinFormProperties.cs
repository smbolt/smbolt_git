using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Org.GS.Dynamic
{
  public class WinFormProperties
  {
    public bool IsFormTitleSpecified { get; set; }
    public string FormTitle { get; set; }

    public bool IsFormIconSpecified { get; set; }
    public Icon FormIcon { get; set; }

    public bool IsInitialFormSizeSpecified { get; set; }
    public Size InitialFormSize { get; set; }

    public WinFormProperties()
    {
      this.Initialize();
    }

    private void Initialize()
    {
      this.IsFormTitleSpecified = false;
      this.FormTitle = String.Empty;

      this.IsFormIconSpecified = false;
      this.FormIcon = null;

      this.IsInitialFormSizeSpecified = false;
      this.InitialFormSize = new Size();

    }
  }
}

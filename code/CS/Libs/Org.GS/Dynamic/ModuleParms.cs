using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Resources;
using Org.GS;

namespace Org.GS.Dynamic
{
  public class ModuleParms
  {
    public bool MainFormSizeSpecified { get; set; }
    public Size MainFormSize { get; set; }

    public ModuleParms(ResourceManager rm)
    {
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS.Dynamic
{
  public class ModuleSet : Dictionary<string, Module>
  {
    public string MainModule { get; set; }

    public ModuleSet()
    {
      this.MainModule = String.Empty;
    }
  }
}

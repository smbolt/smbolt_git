using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public class ProcessParms
  {
    public string ExecutablePath { get; set; }
    public string[] Args { get; set; }

    public ProcessParms()
    {
      this.ExecutablePath = String.Empty;
      this.Args = new string[] { };
    }
  }
}

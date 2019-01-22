using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.OrgScript.Compilation
{
  public class CompilerConfig
  {
    public int TargetRawLength { get; set; }
    public bool InDebugMode { get; set; }
    public bool RunParallel { get; set; }


    public CompilerConfig()
    {
      this.TargetRawLength = 500;
      this.InDebugMode = true;
      this.RunParallel = false;
    }
  }
}

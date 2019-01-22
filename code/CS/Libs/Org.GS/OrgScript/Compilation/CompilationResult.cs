using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.OrgScript.Compilation
{
  public class CompilationResult
  {
    public Executable Executable {
      get;
      private set;
    }
    public CompilerMessageSet CompilerMessageSet {
      get;
      private set;
    }

    public CompilationResult()
    {
      this.Executable = null;
      this.CompilerMessageSet = new CompilerMessageSet();
    }
  }
}

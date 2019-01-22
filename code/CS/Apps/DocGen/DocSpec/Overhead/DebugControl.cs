using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
  public class DebugControl
  {
    public bool InDiagnosticsMode {
      get;
      set;
    }
    public int DiagnosticsLevel {
      get;
      set;
    }
    public bool ShowScale {
      get;
      set;
    }
    public bool CreateMap {
      get;
      set;
    }
    public bool CreateXmlMap {
      get;
      set;
    }
    public bool IncludeProperties {
      get;
      set;
    }

    public DebugControl()
    {
      this.InDiagnosticsMode = false;
      this.DiagnosticsLevel = 0;
      this.ShowScale = false;
      this.CreateMap = false;
      this.CreateXmlMap = false;
      this.IncludeProperties = false;
    }
  }
}

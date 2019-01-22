using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GraphicReports
{
  public class ReportDiagnostics
  {
    public bool DiagnosticsActive {
      get;
      set;
    }
    public bool ShowMargins {
      get;
      set;
    }
    public bool ShowGrid {
      get;
      set;
    }
    public bool ShowDiagInfo {
      get;
      set;
    }

    public ReportDiagnostics()
    {
      this.DiagnosticsActive = false;
      this.ShowMargins = false;
      this.ShowGrid = false;
      this.ShowDiagInfo = false;
    }
  }
}

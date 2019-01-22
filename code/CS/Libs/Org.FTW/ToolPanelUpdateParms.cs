using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.FTW
{
  public class ToolPanelUpdateParms
  {
    public string ToolPanelName { get; set; }
    public string Command { get; set; }

    public ToolPanelUpdateParms()
    {
      this.ToolPanelName = String.Empty;
      this.Command = String.Empty;
    }
  }
}

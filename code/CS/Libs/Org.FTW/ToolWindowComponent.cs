using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.FTW.Forms;
using Org.FTW.ToolPanels;
using Org.GS;

namespace Org.FTW
{
  public class ToolWindowComponents
  {
    public string Name { get; set; }
    public frmToolWindowBase ToolWindow { get; set; }
    public ToolPanelBase ToolPanel { get; set; }
    public Panel DockedTarget { get; set; }
    public Panel FloatedTarget { get; set; }
  }
}

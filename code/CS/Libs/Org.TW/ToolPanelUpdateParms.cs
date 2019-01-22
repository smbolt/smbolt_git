using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Dx.Business.TextProcessing;
using Org.GS;

namespace Org.TW
{
  public class ToolPanelUpdateParms
  {
    public TextConfigType TextConfigType { get; set; }
    public string ConfigFileFullPath { get; set; }
    public string ToolPanelName { get; set; }
    public string Command { get; set; }
    public string ConfigFullXmlPath { get; set; }
    public bool SuppressSwitchToErrorTab { get; set; }

    public ToolPanelUpdateParms()
    {
      this.TextConfigType = TextConfigType.NotSet;
      this.ConfigFileFullPath = String.Empty;
      this.ToolPanelName = String.Empty;
      this.Command = String.Empty;
      this.ConfigFullXmlPath = String.Empty;
      this.SuppressSwitchToErrorTab = false;
    }
  }
}

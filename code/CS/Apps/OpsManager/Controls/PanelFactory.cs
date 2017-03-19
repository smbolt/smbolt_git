using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.OpsManager.Controls
{
  public class PanelFactory
  {
    
    public BasePanel CreatePanel(PanelData panelData, ChangeType changeType)
    {
      BasePanel panel;
      switch (panelData.NotifyType)
      {
        case NotifyType.NotifyConfigSet:
          panel = new NotifyConfigSetPanel(panelData, changeType);
          break;

        case NotifyType.NotifyConfig:
          panel = new NotifyConfigPanel(panelData, changeType);
          break;

        case NotifyType.NotifyEvent:
          panel = new NotifyEventPanel(panelData, changeType);
          break;

        case NotifyType.NotifyGroup:
          panel = new NotifyGroupPanel(panelData, changeType);
          break;

        case NotifyType.NotifyPerson:
          panel = new NotifyPersonPanel(panelData, changeType);
          break;

        default: 
          panel = new BasePanel();
          break;
      }

      return panel;
    }
  }
}

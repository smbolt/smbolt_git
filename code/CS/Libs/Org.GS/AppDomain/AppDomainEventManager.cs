using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.AppDomainManagement
{
  public class AppDomainEventManager : MarshalByRefObject
  {
    public event Action<NotifyMessage> NotifyMessage;
    public event Action<ProgressMessage> ProgressUpdate;

    public void PlugIn_ProgressUpdate(ProgressMessage progressMessage)
    {
      if (this.ProgressUpdate != null)
        this.ProgressUpdate(progressMessage);
    }

    public void PlugIn_NotifyMessage(NotifyMessage notifyMessage)
    {
      if (this.NotifyMessage != null)
        this.NotifyMessage(notifyMessage); 
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.AppDomainManagement;
using Org.GS;

namespace Org.GS.PlugIn
{
  public interface IPlugIn
  {
    int EntityId { get; }
    event Action<NotifyMessage> NotifyMessage;
    event Action<ProgressMessage> ProgressUpdate;
    TaskRequest TaskRequest { get; set; }
    string Identify();
    TaskResult ProcessTask();
  }
}

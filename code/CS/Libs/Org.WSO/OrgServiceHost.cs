using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Org.GS;
using Org.WSO.Transactions;

namespace Org.WSO
{
  public class OrgServiceHost : ServiceHost
  {
    public event Action<WsCommandSet> ServiceCommand;

    public OrgServiceHost(Type serviceType, params Uri[] baseAddresses)
      : base(serviceType, baseAddresses) { }

    public void FireServiceCommandEvent(WsCommandSet commandSet)
    {
      if (ServiceCommand == null)
        return;

      ServiceCommand(commandSet);
    }
  }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Org.GS;

namespace Org.WSO
{
  public class ServiceContext : IExtension<ServiceHostBase>
  {
    private static ServiceHostBase _serviceHostBase;
    private static ServiceBase _serviceBase;

    public ServiceBase ServiceBase
    {
      get {
        return _serviceBase;
      }
      set {
        _serviceBase = value;
      }
    }

    private ServiceState _serviceState;
    public ServiceState ServiceState
    {
      get {
        return _serviceState;
      }
    }

    public static ServiceContext GetCurrent(ServiceBase serviceBase)
    {
      _serviceBase = serviceBase;
      ServiceContext serviceContext = OperationContext.Current.InstanceContext.Host.Extensions.Find<ServiceContext>();

      if (serviceContext == null)
      {
        serviceContext = new ServiceContext();
        OperationContext.Current.InstanceContext.Host.Extensions.Add(serviceContext);
      }
      else
      {
        serviceContext.ServiceState.IsNew = false;
      }

      return serviceContext;
    }

    public ServiceContext()
    {
      _serviceState = new ServiceState(_serviceBase);
    }

    public void Attach(ServiceHostBase owner)
    {
      _serviceHostBase = owner;
    }

    public void Detach(ServiceHostBase owner)
    {
    }

    ~ServiceContext()
    {

    }

  }
}

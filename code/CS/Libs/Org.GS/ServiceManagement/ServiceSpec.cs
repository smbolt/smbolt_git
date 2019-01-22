using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS.Configuration;
using Org.GS;

namespace Org.GS.ServiceManagement
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class ServiceSpec : ServiceObject
  {
    public int TaskServiceID { get; set; }
    public int ServiceHostID { get; set; }
    public int? ParentServiceID { get; set; }

    [XMap(IsRequired = true, IsKey = true)]
    public string Name { get; set; }
    
    [XMap(IsRequired = true)]
    public ServiceType ServiceType { get; set; }

    [XMap(DefaultValue = "BasicHttp")]
    public WebServiceBinding WebServiceBinding { get; set; }

    [XMap]
    public string WsHost { get; set; }

    [XMap]
    public string WsPort { get; set; }

    [XMap]
    public string WsServiceName { get; set; }

    [XMap(XType = XType.Element, MyParent = true, Name="ParentServiceSpecSet")]
    public ServiceSpecSet ParentServiceSpecSet { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "ServiceSpec", WrapperElement = "ServiceSpecSet")]
    public ServiceSpecSet ServiceSpecSet { get; set; }

    public ServiceHost ServiceHost { get { return Get_ServiceHost(); } }
    public ServiceEnvironment ServiceEnvironment { get { return Get_ServiceEnvironment(); } }

    public ServiceSpec()
    {
      this.TaskServiceID = 0;
      this.ServiceHostID = 0; 
      this.ParentServiceID = null;
      base.ServiceObjectType = ServiceObjectType.ServiceSpec;
      this.Name = String.Empty;
      this.ServiceType = ServiceType.Unidentified;
      this.WebServiceBinding = WebServiceBinding.NotSet;
      this.WsHost = String.Empty;
      this.WsPort = String.Empty;
      this.ServiceSpecSet = new ServiceSpecSet();
    }

    private ServiceHost Get_ServiceHost()
    {
      if (this.ParentServiceSpecSet == null)
        throw new Exception("The ParentServiceSpecSet property of this ServiceSpec named '" + this.Name + "' is null, the ServiceHost cannot be determined.");

      if (this.ParentServiceSpecSet.ParentServiceHost == null)
        throw new Exception("The ParentServiceHost property of the ParentServiceSpecSet property of this ServiceSpec named '" + this.Name +
                            "' is null, the ServiceHost cannot be determined.");

      return this.ParentServiceSpecSet.ParentServiceHost;
    }

    private ServiceEnvironment Get_ServiceEnvironment()
    {
      var serviceHost = Get_ServiceHost();

      if (serviceHost.ParentServiceHostSet == null)
        throw new Exception("The ParentServiceHostSet property of this ServiceHost named '" + serviceHost.Name + "' is null, the ServiceEnvironment cannot be determined.");

      if (serviceHost.ParentServiceHostSet.ParentServiceEnvironment == null)
        throw new Exception("The ParentServiceEnvironment property of the ParentServiceHostSet property of the ServiceHost named '" + serviceHost.Name +
                            "' is null, the ServiceEnvironment cannot be determined.");

      return serviceHost.ParentServiceHostSet.ParentServiceEnvironment;
    }
  }
}

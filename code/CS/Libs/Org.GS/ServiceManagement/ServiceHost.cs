using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS.Network;
using Org.GS;

namespace Org.GS.ServiceManagement
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class ServiceHost : ServiceObject
  {
    public int HostID {
      get;
      set;
    }

    [XMap (IsRequired = true, IsKey = true)]
    public string Name {
      get;
      set;
    }

    private string _ipAddress;
    [XMap]
    public string IPAddress
    {
      get {
        return Get_IPAddress();
      }
      set {
        _ipAddress = value;
      }
    }

    private string _ipV4Address;
    [XMap]
    public string IPV4Address
    {
      get {
        return Get_IPV4Address();
      }
      set {
        _ipV4Address = value;
      }
    }

    [XMap (XType=XType.Element, CollectionElements = "ServiceSpec", WrapperElement = "ServiceSpecSet")]
    public ServiceSpecSet ServiceSpecSet {
      get;
      set;
    }

    [XMap(XType = XType.Element, MyParent = true, Name = "ParentServiceHostSet")]
    public ServiceHostSet ParentServiceHostSet {
      get;
      set;
    }

    public ServiceEnvironment ServiceEnvironment {
      get {
        return Get_ServiceEnvironment();
      }
    }

    public ServiceHost()
    {
      this.HostID = 0;
      base.ServiceObjectType = ServiceObjectType.ServiceHost;
      this.Name = String.Empty;
      this.IPAddress = String.Empty;
      this.ServiceSpecSet = new ServiceSpecSet();
    }

    public void AutoInit()
    {

    }

    public ServiceEnvironment Get_ServiceEnvironment()
    {
      if (this.ParentServiceHostSet == null)
        throw new Exception("The ParentServiceHostSet property of this ServiceHost object (name '" + this.Name + "') is null.  The ServiceEnvironment object " +
                            "cannot be located.");

      if (this.ParentServiceHostSet.ParentServiceEnvironment == null)
        throw new Exception("The ParentServiceHostEnvironment property of the ParentServiceHostSet property of this ServiceHost object (name '" + this.Name +
                            "') is null.  The ServiceEnvironment object cannot be located.");

      return this.ParentServiceHostSet.ParentServiceEnvironment;
    }

    public string Get_IPAddress()
    {
      if (_ipAddress.IsIPV4Address())
        return _ipAddress;

      _ipAddress = NetworkHelper.GetIPAddressFromHostName(this.Name);

      return _ipAddress;
    }

    public string Get_IPV4Address()
    {
      if (_ipV4Address.IsIPV4Address())
        return _ipV4Address;

      _ipV4Address = NetworkHelper.GetIPV4AddressFromHostName(this.Name);

      return _ipV4Address;
    }
  }
}

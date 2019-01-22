using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.GS.ServiceManagement
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class ServiceEnvironment : ServiceObject
  {
    [XMap(IsRequired = true, IsKey = true)]
    public string Name {
      get;
      set;
    }

    [XMap(DefaultValue = "False")]
    public bool IsProductionEnvironment {
      get;
      set;
    }

    [XMap(XType = XType.Element, CollectionElements = "ServiceHost", WrapperElement = "ServiceHostSet")]
    public ServiceHostSet ServiceHostSet {
      get;
      set;
    }

    public ServiceEnvironment()
    {
      base.ServiceObjectType = ServiceObjectType.ServiceEnvironment;
      this.Name = String.Empty;
      this.IsProductionEnvironment = false;
      this.ServiceHostSet = new ServiceHostSet();
    }
  }
}

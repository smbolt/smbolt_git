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
  [XMap(XType = XType.Element, CollectionElements = "ServiceHost", WrapperElement = "ServiceHostSet")]
  public class ServiceHostSet : Dictionary<string, ServiceHost>
  {
    [XMap(XType = XType.Element, MyParent = true, Name = "ParentServiceEnvironment")]
    public ServiceEnvironment ParentServiceEnvironment { get; set; }
  }
}

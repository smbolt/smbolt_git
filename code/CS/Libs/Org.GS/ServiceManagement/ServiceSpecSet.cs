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
  [XMap(XType = XType.Element, CollectionElements = "ServiceSpec", WrapperElement = "ServiceSpecSet")]
  public class ServiceSpecSet : SortedList<string, ServiceSpec>
  {
    [XMap]
    public string Name { get; set; }

    [XMap(XType = XType.Element, MyParent = true, Name = "ParentServiceHost")]
    public ServiceHost ParentServiceHost { get; set; }

    public ServiceSpecSet()
    {
      this.Name = String.Empty;
    }
  }
}

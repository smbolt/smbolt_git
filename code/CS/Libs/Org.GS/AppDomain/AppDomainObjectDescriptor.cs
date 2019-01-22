using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.AppDomainManagement
{
  public class AppDomainObjectDescriptor : MarshalByRefObject
  {
    public string AppDomainFriendlyName { get; private set; }
    public string ObjectRegistryName { get; private set; }
    public string ObjectAssemblyName { get; private set; }
    public string ObjectTypeName { get; private set; }
    public AppDomainSetup AppDomainSetup { get; private set; }
    public AppDomain AppDomain { get; set; }
    public object Object { get; set; }
    public ILease Lease { get; set; }

    public AppDomainObjectDescriptor(string appDomainFriendlyName, string objectRegistryName, string objectAssemblyName, string objectTypeName, AppDomainSetup appDomainSetup)
    {
      this.AppDomainFriendlyName = appDomainFriendlyName;
      this.ObjectRegistryName = objectRegistryName;
      this.ObjectAssemblyName = objectAssemblyName;
      this.ObjectTypeName = objectTypeName; 
      this.AppDomainSetup = appDomainSetup;
      this.AppDomain = null;
      this.Object = null;
      this.Lease = null;
    }

    public string ToReport()
    {
      return "AppDomainFriendlyName=" + (this.AppDomainFriendlyName.IsBlank() ? "blank or null" : this.AppDomainFriendlyName) + ", " +
             "ObjectRegistryName=" + (this.ObjectRegistryName.IsBlank() ? "blank or null" : this.ObjectRegistryName) + ", " +
             "ObjectAssemblyName=" + (this.ObjectAssemblyName.IsBlank() ? "blank or null" : this.ObjectAssemblyName) + ", " +
             "ObjectTypeName=" + (this.ObjectTypeName.IsBlank() ? "blank or null" : this.ObjectTypeName) + ", " +
             "AppDomainSetup.ApplicationBase=" + (this.AppDomainSetup == null || this.AppDomainSetup.ApplicationBase == null ? "blank or null" :
                this.AppDomainSetup.ApplicationBase) + ".";
    }
  }
}

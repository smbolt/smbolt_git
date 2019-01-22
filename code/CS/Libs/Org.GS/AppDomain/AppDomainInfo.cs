using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.GS.AppDomainManagement;

namespace Org.GS.AppDomainManagement
{
  public class AppDomainInfo
  {
    public string FriendlyName { get; set; }
    public AppDomain AppDomain { get; private set; }
    public AppDomainInfoSet AppDomainInfoSet { get; private set; }
    public AppDomainInfo Parent { get; private set; }
    public string Report { get { return Get_Report(); } }
    public SortedList<string, AppDomainObjectDescriptor> RegisteredObjects { get; set; }

    public AppDomainInfo(AppDomain appDomain, AppDomainInfo parent)
    {
      this.AppDomain = appDomain;
      this.FriendlyName = String.Empty;
      this.Parent = parent;
      this.AppDomainInfoSet = new AppDomainInfoSet();
      this.RegisteredObjects = new SortedList<string, AppDomainObjectDescriptor>();
    }

    private string Get_Report()
    {
      try
      {
        var sb = new StringBuilder();

        sb.Append("AppDomain         : " + this.FriendlyName + g.crlf);
        sb.Append("Parent            : " + (this.Parent == null ? "None" : this.Parent.FriendlyName) + g.crlf);
        sb.Append("Base Directory    : " + this.AppDomain.BaseDirectory + g.crlf);

        IList<Assembly> assemblies = null;

        if (!this.AppDomain.IsTheDefaultAppDomain())
        {
          using (var appDomainUtility = new AppDomainUtility())
            return appDomainUtility.GetAssemblyReport();
        }
        
        assemblies = this.AppDomain.GetAssemblies();

        if (assemblies == null)
        {
          sb.Append(g.crlf); 
          sb.Append("Need to get list of assemblies through the proxy - can't get references to the assembly in this, " +
                    "the parent AppDomain" + g.crlf);           
        }

        if (assemblies != null)
        {
          foreach (var assembly in assemblies)
          {
            string manifestModule = assembly.ManifestModule.Name;
            string fullName = assembly.FullName;
            string codeBase = assembly.CodeBase;
            string location = assembly.Location;
            string image = assembly.ImageRuntimeVersion;

            string results = "Manifest Module   : " + manifestModule + g.crlf +
                              "Full Name         : " + fullName + g.crlf +
                              "Code Base         : " + codeBase + g.crlf +
                              "Location          : " + location + g.crlf +
                              "Image             : " + image + g.crlf2;
            sb.Append(results);
          }
        }

        string report = sb.ToString();
        return report;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to build the AppDomainInfo.Report.", ex); 
      }
    }
  }
}

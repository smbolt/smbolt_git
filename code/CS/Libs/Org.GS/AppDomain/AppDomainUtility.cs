using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.AppDomainManagement
{
  public class AppDomainUtility : MarshalByRefObject, IAppDomainUtility, IDisposable
  {
    public string AppDomainFriendlyName {
      get {
        return AppDomain.CurrentDomain.FriendlyName;
      }
    }

    public AppDomainUtility()
    {
    }

    public string GetAssemblyReport()
    {
      try
      {
        StringBuilder sb = new StringBuilder();

        var currentAppDomain = AppDomain.CurrentDomain;

        string parentFriendlyName = currentAppDomain.GetData("ParentAppDomainFriendlyName") as String;
        if (parentFriendlyName.IsBlank())
          parentFriendlyName = "Unknown Parent";

        sb.Append("AppDomain         : " + currentAppDomain.FriendlyName + g.crlf);
        sb.Append("Parent            : " + parentFriendlyName + g.crlf);
        sb.Append("Base Directory    : " + currentAppDomain.BaseDirectory + g.crlf2);

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        if (assemblies != null)
        {
          foreach (var assembly in assemblies)
          {
            string manifestModule = assembly.ManifestModule.Name;
            string fullName = assembly.FullName;
            string codeBase = assembly.CodeBase;
            string location = assembly.Location;
            string image = assembly.ImageRuntimeVersion;

            string results =  "Manifest Module   : " + manifestModule + g.crlf +
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
        throw new Exception("An exception occurred while attempting to create the assembly report for the AppDomain named '" + AppDomain.CurrentDomain.FriendlyName + "'.", ex);
      }
    }

    public Assembly GetRootAssembly()
    {
      try
      {
        return AssemblyHelper.GetRootAssembly();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to return the Org Framework 'Root' assembly loaded in the current AppDomain " +
                            "named '" + AppDomain.CurrentDomain.FriendlyName + "'.", ex);
      }

    }

    public void Dispose() { }
  }
}

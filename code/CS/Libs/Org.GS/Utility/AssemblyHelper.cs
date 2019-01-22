using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Org.GS
{
  public class AssemblyHelper
  {
    public static bool IsInVisualStudioDesigner
    {
      get { return Get_IsInVisualStudioDesigner(); }
    }

    public static Assembly GetWebRootAssembly()
    {
      List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
      foreach (Assembly assembly in assemblies)
      {
        if (assembly.GetCustomAttributes(typeof(OrgWebRootAssembly), false).ToList().FirstOrDefault() != null)
          return assembly;
      }

      return null;
    }

    public static Assembly GetModuleAssembly()
    {
      List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
      foreach (Assembly assembly in assemblies)
      {
        if (assembly.GetCustomAttributes(typeof(OrgModuleAssembly), false).ToList().FirstOrDefault() != null)
          return assembly;
      }

      return null;
    }

    public static Assembly GetRootAssembly()
    {
      List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

      Assembly orgGsAssembly = null; 
      Assembly defaultAssembly = null;
      Assembly orgModuleAssembly = null;

      // Locate the assembly marked as the "OrgModuleAssembly" if there's one marked as such in the AppDomain
      // Otherwise, see if the Org.PlugIn assembly is loaded - when simple/base PlugIns are used from there.
      // Otherwise, see if the Org.GS assembly is loaded and use it by default - it has to be loaded to execute this code,
      // but a reference to it is needed and returned.
      foreach (Assembly assembly in assemblies)
      {
        if (assembly.GetCustomAttributes(typeof(OrgAssemblyTag), false).ToList().FirstOrDefault() != null)
        {
          if (assembly.FullName.Contains("Org.GS"))
          {
            orgGsAssembly = assembly;
          }
          else
          {
            if (assembly.GetCustomAttributes(typeof(OrgModuleAssembly), false).ToList().FirstOrDefault() != null)
            {
              if (assembly.FullName.Contains("Org.PlugIn"))
              {
                defaultAssembly = assembly;
              }
              else
              {
                orgModuleAssembly = assembly;
                break;
              }
            }
          }
        }
      }

      if (orgModuleAssembly != null)
        return orgModuleAssembly;

      if (defaultAssembly != null)
        return defaultAssembly;

      if (orgGsAssembly != null)
        return orgGsAssembly;

      return null;
    }

    public static Assembly GetControlLibraryAssembly()
    {
      List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
      foreach (Assembly assembly in assemblies)
      {
        if (assembly.GetCustomAttributes(typeof(OrgControlLibrary), false).ToList().FirstOrDefault() != null)
          return assembly;
      }

      return null;
    }

    public static string GetAssembliesInAppDomainString()
    {
      List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

      List<string> assemblyNames = new List<string>();
      foreach (Assembly assembly in assemblies)
      {
        string name = assembly.ManifestModule.Name;
        if (!assemblyNames.Contains(name))
          assemblyNames.Add(name);
      }

      assemblyNames.Sort();

      StringBuilder sb = new StringBuilder();

      foreach (string s in assemblyNames)
        sb.Append(s + Environment.NewLine);

      string results = sb.ToString();

      return results;
    }

    public static List<Assembly> GetAssembliesInAppDomain()
    {
      List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
      
      return assemblies;
    }

    private static bool Get_IsInVisualStudioDesigner()
    {
      List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
      int visualStudioDllCount = 0; 

      foreach (Assembly assembly in assemblies)
      {
        string name = assembly.ManifestModule.Name;
        if (name.StartsWith("Microsoft.VisualStudio"))
        {
          visualStudioDllCount++;
          if(visualStudioDllCount > 1)
            return true;
        }
      }

      return false;
    }

    public static string GetAssemblyReport(bool includeAllAssemblies)
    {
      var assemblies = AssemblyHelper.GetAssembliesInAppDomain();
      var selectedAssemblies = new List<Assembly>();

      foreach(var assembly in assemblies)
      {
        if (includeAllAssemblies)
        {
          if (assembly.ContainsCustomAttribute(typeof(OrgAssemblyTag)))
          {
            selectedAssemblies.Add(assembly);
          }
        }
        else
        {
          selectedAssemblies.Add(assembly);
        }
      }

      StringBuilder sb = new StringBuilder();
      string appDomainName = AppDomain.CurrentDomain.FriendlyName;

      sb.Append("AppDomain:        " + appDomainName + g.crlf2); 

      foreach (Assembly assembly in selectedAssemblies)
      {
        string manifestModule = assembly.ManifestModule.Name;
        string fullName = assembly.FullName;
        string codeBase = assembly.CodeBase;
        string location = assembly.Location;
        string image = assembly.ImageRuntimeVersion;

        string results =  "Manifest Module:  " + manifestModule + g.crlf + 
                          "Full Name:        " + fullName + g.crlf + 
                          "Code Base:        " + codeBase + g.crlf + 
                          "Location:         " + location + g.crlf + 
                          "Image:            " + image + g.crlf2; 
        sb.Append(results);
      }

      return sb.ToString();
    }
  }
}

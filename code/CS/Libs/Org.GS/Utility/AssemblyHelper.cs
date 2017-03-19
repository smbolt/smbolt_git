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

    public static string GetAssemblyReport(bool all)
    {
      List<Assembly> assemblies = new List<Assembly>();
      var orgAssemblies = AssemblyHelper.GetAssembliesInAppDomain();

      foreach(var orgAssembly in orgAssemblies)
      {
        if(!all)
        if (orgAssembly.ContainsCustomAttribute(typeof(OrgAssemblyTag)))
        {
          assemblies.Add(orgAssembly);
        }
        if(all)
        {
          assemblies.Add(orgAssembly);
        }
      }

      StringBuilder sb = new StringBuilder();

      foreach (Assembly assembly in assemblies)
      {
        string manifestModule = assembly.ManifestModule.Name;
        string fullName = assembly.FullName;
        //string codeBase = assembly.CodeBase;
        //string location = assembly.Location;
        string image = assembly.ImageRuntimeVersion;
        //string eCodeBase = assembly.EscapedCodeBase;

        string results =  "Manifest Module:  " + manifestModule + g.crlf + 
                          "Full Name:        " + fullName + g.crlf + 
                          //"Code Base:        " + codeBase + g.crlf + 
                          //"Location:         " + location + g.crlf + 
                          "Image:            " + image + g.crlf2; 
                          //"e Code Base:      " + eCodeBase + g.crlf2;
        sb.Append(results);
      }

      return sb.ToString();
    }
  }
}

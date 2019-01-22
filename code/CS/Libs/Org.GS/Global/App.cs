using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Org.GS
{
  public class App
  {
    private FileSystemUtility fsu;

    // assembly data
    public string _assemblyName {
      get;
      set;
    }
    public string _companyName {
      get;
      set;
    }
    public string _productName {
      get;
      set;
    }
    public Version _version {
      get;
      set;
    }
    public string _assemblyTitle {
      get;
      set;
    }
    public string _appDataPath {
      get;
      set;
    }

    // environment data
    public string _systemDriveLetter {
      get;
      set;
    }

    public App(Assembly assembly)
    {
      this.fsu = new FileSystemUtility();
      GetAssemblyData(assembly);
      GetEnvironmentData();
      GetFileSystemData();
    }

    private void GetAssemblyData(Assembly assembly)
    {
      _assemblyName = assembly.GetName().Name;
      _version = assembly.GetName().Version;

      object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
      if (attributes.Length > 0)
      {
        AssemblyCompanyAttribute companyAttribute = (AssemblyCompanyAttribute)attributes[0];
        _companyName = companyAttribute.Company;
      }
      else
        _companyName = "ORG";

      attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
      if (attributes.Length > 0)
      {
        AssemblyProductAttribute productAttribute = (AssemblyProductAttribute)attributes[0];
        _productName = productAttribute.Product;
      }
      else
        _companyName = "ProductName";

      attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
      if (attributes.Length > 0)
      {
        AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
        _assemblyTitle = titleAttribute.Title;
      }
      else
        _companyName = "Assembly Title";
    }

    private void GetEnvironmentData()
    {
      string systemDirectory = Environment.SystemDirectory;
      System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(systemDirectory);
      _systemDriveLetter = di.Root.ToString();
    }

    public void GetFileSystemData()
    {
      if (!Directory.Exists(this._systemDriveLetter))
        throw new Exception("System Drive Letter '" + this._systemDriveLetter + "' does not exist.");

      string drive = fsu.RemoveTrailingSlash(this._systemDriveLetter);
      string programDataPath = drive + @"\ProgramData";

      if (Directory.Exists(programDataPath))
      {
        try
        {
          fsu.AssertAccess(programDataPath, FileSystemAccess.CreateDirectory);
        }
        catch (Exception ex)
        {
          string message = ex.Message;
        }
      }
    }
  }
}

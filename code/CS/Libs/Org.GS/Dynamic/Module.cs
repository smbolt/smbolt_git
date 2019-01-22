using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Org.GS;

namespace Org.GS.Dynamic
{
  public class Module
  {
    public string ModuleName {
      get;
      set;
    }
    public string ModuleTitle {
      get;
      set;
    }
    public string ModuleVersion {
      get;
      set;
    }
    public string ModuleTitleDisplay {
      get {
        return this.ModuleTitle + " - " + this.ModuleVersion;
      }
    }
    public string ModulePath {
      get;
      set;
    }

    public string ModuleNameShort
    {
      get
      {
        if (this.ModuleName == null)
          return String.Empty;
        return this.ModuleName.Replace(".Module", String.Empty);
      }
    }

    public string ModuleFileName
    {
      get {
        return this.GetModuleFileName();
      }
    }

    public string ModuleKey
    {
      get {
        return this.GetModuleKey();
      }
    }

    public bool IsPdbFilePresent
    {
      get {
        return this.GetIsPdbFilePresent();
      }
    }

    public string FullPath
    {
      get {
        return this.GetFullPath();
      }
    }

    public Module()
    {
      this.ModuleName = String.Empty;
      this.ModuleVersion = String.Empty;
      this.ModulePath = String.Empty;
    }

    public Module(Assembly assembly)
    {
      this.ModuleName = "Module Name Missing";
      OrgModuleName moduleNameAttribute = (OrgModuleName)assembly.GetCustomAttributes(typeof(OrgModuleName), false).ToList().FirstOrDefault();
      if (moduleNameAttribute != null)
      {
        if (moduleNameAttribute.Value.IsNotBlank())
          this.ModuleName = moduleNameAttribute.Value.Trim();
      }

      this.ModuleTitle = "Module Title Missing";
      OrgModuleTitle moduleTitleAttribute = (OrgModuleTitle)assembly.GetCustomAttributes(typeof(OrgModuleTitle), false).ToList().FirstOrDefault();
      if (moduleTitleAttribute != null)
      {
        if (moduleTitleAttribute.Value.IsNotBlank())
          this.ModuleTitle = moduleTitleAttribute.Value.Trim();
      }

      this.ModulePath = Path.GetDirectoryName(assembly.Location);

      this.ModuleVersion = "0.0.0.0";
      OrgVersion OrgVersion = (OrgVersion)assembly.GetCustomAttributes(typeof(OrgVersion), false).ToList().FirstOrDefault();
      if (OrgVersion != null)
        if (OrgVersion.Value.IsNotBlank())
          this.ModuleVersion = OrgVersion.Value.Trim();
    }

    private string GetFullPath()
    {
      if (this.ModuleName == null || this.ModulePath == null)
        return String.Empty;

      return g.RemoveTrailingSlash(this.ModulePath.Trim()) + @"\" + this.ModuleFileName;
    }

    private bool GetIsPdbFilePresent()
    {
      string fullPath = this.GetFullPath();

      if (fullPath.IsBlank())
        return false;

      if (!fullPath.Contains(".dll"))
        return false;

      string pdbFullPath = fullPath.Replace(".dll", ".pdb");

      return File.Exists(pdbFullPath);
    }

    private string GetModuleKey()
    {
      string moduleKey = this.ModuleName.Replace(".dll", String.Empty);
      moduleKey += ":" + this.ModuleVersion;

      return moduleKey;
    }

    public string GetModuleFileName()
    {
      return this.ModuleName.Trim() + ".dll";
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Drawing;
using Org.MOD.Contracts;
using Org.GS;

namespace Org.MOD.Concrete
{
  public class ModuleBase : IModule
  {
    protected Assembly Assembly { get; set; }
    protected ResourceManager ResourceManager { get; set; }
    protected string ModuleName { get; private set; }
    protected string ModuleVersion { get; private set; }
    protected int ModuleCode { get; private set; }
    protected string ModuleConfigName { get; private set; }
    protected string CompanyName { get; private set; }
    protected string ModuleNameAndVersionString { get { return Get_ModuleNameAndVersionString(); } }
    protected string ModuleCopyrightString  { get { return Get_ModuleCopyrightString(); } }
   
    public ModuleBase(Assembly module)
    {
      Initialize(module);
    }

    public void Initialize(Assembly module)
    {
      this.Assembly = module;
      
      OrgModuleName orgModuleName = (OrgModuleName)this.Assembly.GetCustomAttributes(typeof(OrgModuleName), false).ToList().FirstOrDefault();
      if (orgModuleName == null)
        throw new Exception("OrgModuleName assembly attribute not found."); 
      this.ModuleName = orgModuleName.Value;
      
      OrgVersion orgVersion = (OrgVersion)this.Assembly.GetCustomAttributes(typeof(OrgVersion), false).ToList().FirstOrDefault();
      if (orgVersion == null)
        throw new Exception("OrgVersion assembly attribute not found."); 
      this.ModuleVersion = orgVersion.Value;
      
      OrgModuleCode orgModuleCode = (OrgModuleCode)this.Assembly.GetCustomAttributes(typeof(OrgModuleCode), false).ToList().FirstOrDefault();
      if (orgModuleCode == null)
        throw new Exception("OrgModuleCode assembly attribute not found."); 
      this.ModuleCode = orgModuleCode.Value; 
      
      OrgConfigName orgConfigName = (OrgConfigName)this.Assembly.GetCustomAttributes(typeof(OrgConfigName), false).ToList().FirstOrDefault();
      if (orgConfigName == null)
        throw new Exception("OrgConfigName assembly attribute not found."); 
      this.ModuleConfigName = orgConfigName.Value;      
      
      AssemblyCompanyAttribute assemblyCompanyAttribute = (AssemblyCompanyAttribute)this.Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false).ToList().FirstOrDefault();
      if (assemblyCompanyAttribute == null)
        throw new Exception("AssemblyCompanyAttribute assembly attribute not found."); 
      this.CompanyName = assemblyCompanyAttribute.Company;

      this.ResourceManager = new ResourceManager(this.ModuleName + ".Resource1", this.Assembly);
    }

    ~ModuleBase()
    {
      Dispose(false); 
    }

    public virtual void Run()
    {
      throw new Exception("The Run method must be overridden in the derived module."); 
    }

    private string Get_ModuleNameAndVersionString()
    {
      return this.ModuleName + " - " + this.ModuleVersion; 
    }

    private string Get_ModuleCopyrightString()
    {
      return DateTime.Now.Year.ToString() + " " + Constants.Copyright + " " + this.CompanyName;
    }

    public virtual void Dispose()
    {
      Dispose(true); 
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {

      }
    }
  }
}

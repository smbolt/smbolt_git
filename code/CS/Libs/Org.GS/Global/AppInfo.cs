﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using Org.GS;

namespace Org.GS
{
  public class AppInfo
  {
    private Assembly _assembly;

    public string Vendor { get; set; }
    public string AppName { get; set; }
    public string AppVersion { get; set; }
    public string AppTitle { get; set; }
    public int ModuleCode { get; set; }
    public string ModuleName { get; set; }
    public string ConfigName { get; set; }
    public string AppConfigSuffix { get; set; }
    public string OrgVersion { get; set; }
    public ApplicationType OrgApplicationType { get; set; }
    public LicenseScheme LicenseScheme { get; set; }
    public LicenseStatus LicenseStatus { get; set; }
    public int LicenseExpiringInterval { get; set; }
    public DateTime FreeUntil { get; set; }
    public DateTime FreeAfter { get; set; }
    public int LicenseRemainingDays { get; set; }

    private bool _missingOrgAttributes;
    public bool MissingOrgAttributes { get { return _missingOrgAttributes; } }
        
    public AppInfo()
    {
      _missingOrgAttributes = false;
            
      _assembly = Assembly.GetEntryAssembly();

      if (_assembly == null)
        _assembly = AssemblyHelper.GetWebRootAssembly();

      if (_assembly == null)
        _assembly = AssemblyHelper.GetControlLibraryAssembly();

      g.ModuleAssembly = AssemblyHelper.GetModuleAssembly();


      if (_assembly == null)
      {
        string assembliesInAppDomain = AssemblyHelper.GetAssembliesInAppDomainString();
        frmDisplayText fDisplayText = new frmDisplayText(assembliesInAppDomain);
        fDisplayText.ShowDialog();
        throw new Exception("Cannot find module assembly. Most likley cause is that web site or web service application main assembly does not have the " +
                            "'OrgWebRootAssembly' custom attribute assigned in AssemblyInfo.cs or for custom controls, that the custom control does not have " +
                            "the 'OrgControlLibrary' custom attribute assigned in AssemblyInfo.cs."+ g.crlf + "Assemblies in AppDomain" + g.crlf +
                            assembliesInAppDomain);
      }

      g.RootAssembly = _assembly;

      g.IsModuleHost = false;
      if (_assembly.GetCustomAttributes(typeof(OrgModuleHost), false).ToList().FirstOrDefault() != null)
        g.IsModuleHost = true;

      this.AppName = _assembly.GetName().Name;
      if (this.AppName.StartsWith("Org."))
        this.AppName = this.AppName.Replace("Org.", String.Empty); 

      Version v = _assembly.GetName().Version;
      this.AppVersion = v.Major.ToString().Trim() + "." + v.Minor.ToString().Trim() + "." + v.Build.ToString().Trim() + "." + v.Revision.ToString();
                        
      this.Vendor = "ORG";
      AssemblyCompanyAttribute vendorAttribute = (AssemblyCompanyAttribute) _assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false).ToList().FirstOrDefault();
      if (vendorAttribute != null)
        if (vendorAttribute.Company.IsNotBlank())
          this.Vendor = vendorAttribute.Company.Trim();

      this.AppTitle = "App Title";
      AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute) _assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).ToList().FirstOrDefault();
      if (titleAttribute != null)
        if (titleAttribute.Title.IsNotBlank())
          this.AppTitle = titleAttribute.Title.Trim();

      this.OrgVersion = "0.0.0.0";
      OrgVersion OrgVersion = (OrgVersion)_assembly.GetCustomAttributes(typeof(OrgVersion), false).ToList().FirstOrDefault();
      if (OrgVersion == null)
        _missingOrgAttributes = true;
      else
        if (OrgVersion.Value.IsBlank())
          _missingOrgAttributes = true;
        else
          this.OrgVersion = OrgVersion.Value.Trim();
            
      this.OrgApplicationType = ApplicationType.NotSet;
      OrgApplicationType OrgApplicationType = (OrgApplicationType)_assembly.GetCustomAttributes(typeof(OrgApplicationType), false).ToList().FirstOrDefault();
      if (OrgApplicationType == null)
        _missingOrgAttributes = true;
      else
        this.OrgApplicationType = OrgApplicationType.ApplicationType;

      this.ConfigName = this.AppName;
      OrgConfigName configNameAttribute = (OrgConfigName)_assembly.GetCustomAttributes(typeof(OrgConfigName), false).ToList().FirstOrDefault();
      if (configNameAttribute == null)
        _missingOrgAttributes = true;
      else
      {
        if (configNameAttribute.Value.IsNotBlank())
          this.ConfigName = configNameAttribute.Value.Trim();
      }

      this.ModuleName = this.AppName;
      OrgModuleName moduleNameAttribute = (OrgModuleName)_assembly.GetCustomAttributes(typeof(OrgModuleName), false).ToList().FirstOrDefault();
      if (moduleNameAttribute == null)
        _missingOrgAttributes = true;
      else
      {
        if (moduleNameAttribute.Value.IsNotBlank())
          this.ModuleName = moduleNameAttribute.Value.Trim();
      }

      this.ModuleCode = 999;
      OrgModuleCode moduleCodeAttribute = (OrgModuleCode)_assembly.GetCustomAttributes(typeof(OrgModuleCode), false).ToList().FirstOrDefault();
      if (moduleCodeAttribute == null)
        _missingOrgAttributes = true;
      else
        this.ModuleCode = moduleCodeAttribute.Value;
            
      this.FreeUntil = DateTime.MinValue;
      this.FreeAfter = DateTime.MinValue;
      this.LicenseScheme = LicenseScheme.None;
      this.LicenseStatus = LicenseStatus.None;
      this.LicenseExpiringInterval = 30;
      this.LicenseRemainingDays = 0; 

      OrgFreeUntil freeUntilAttribute = (OrgFreeUntil)_assembly.GetCustomAttributes(typeof(OrgFreeUntil), false).ToList().FirstOrDefault();
      if (freeUntilAttribute != null)
        this.FreeUntil = DateTime.Parse(freeUntilAttribute.Value);

      OrgFreeAfter freeAfterAttribute = (OrgFreeAfter)_assembly.GetCustomAttributes(typeof(OrgFreeAfter), false).ToList().FirstOrDefault();
      if (freeAfterAttribute != null)
        this.FreeAfter = DateTime.Parse(freeAfterAttribute.Value);

      OrgLicenseExpiringInterval licenseExpiringIntervalAttribute = 
          (OrgLicenseExpiringInterval)_assembly.GetCustomAttributes(typeof(OrgLicenseExpiringInterval), false).ToList().FirstOrDefault();
      if (licenseExpiringIntervalAttribute != null)
        this.LicenseExpiringInterval = licenseExpiringIntervalAttribute.Days;

      if (this.FreeUntil > DateTime.MinValue && this.FreeAfter > DateTime.MinValue)
        this.LicenseScheme = LicenseScheme.Simple1;

    }
  }
}
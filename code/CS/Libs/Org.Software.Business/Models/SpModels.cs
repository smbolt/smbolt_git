using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.DB;
using Org.GS;

namespace Org.Software.Business.Models
{
  [DbMap(DbElement.Model, "Org_Software", "dbo", "sp_GetSoftwareUpdatesForModuleVersion_Result")]
  public class SoftwareUpdatesForModuleVersion : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetSoftwareUpdatesForModuleVersion_Result", "SoftwareModuleCode", false, false)]
    public int SoftwareModuleCode { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetSoftwareUpdatesForModuleVersion_Result", "SoftwareModuleName", false, false)]
    public string SoftwareModuleName { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetSoftwareUpdatesForModuleVersion_Result", "SoftwarePlatformId", false, false)]
    public int SoftwarePlatformId { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetSoftwareUpdatesForModuleVersion_Result", "SoftwarePlatformString", false, false)]
    public string SoftwarePlatformString { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetSoftwareUpdatesForModuleVersion_Result", "PlatformDescription", false, false)]
    public string PlatformDescription { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetSoftwareUpdatesForModuleVersion_Result", "SoftwareVersionId", false, false)]
    public int SoftwareVersionId { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetSoftwareUpdatesForModuleVersion_Result", "SoftwareModuleId", false, false)]
    public int SoftwareModuleId { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetSoftwareUpdatesForModuleVersion_Result", "SoftwareVersion", false, false)]
    public string SoftwareVersion { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetSoftwareUpdatesForModuleVersion_Result", "RepositoryId", false, false)]
    public int RepositoryId { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetSoftwareUpdatesForModuleVersion_Result", "RepositoryRoot", false, false)]
    public string RepositoryRoot { get; set; }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result")]
  public class ModuleVersionForPlatform : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result", "SoftwareModuleName", false, false)]
    public string SoftwareModuleName { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result", "SoftwareModuleTypeName", false, false)]
    public string SoftwareModuleTypeName { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result", "SoftwareModuleStatus", false, false)]
    public int SoftwareModuleStatus { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result", "SoftwareVersion", false, false)]
    public string SoftwareVersion { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result", "SoftwareVersionStatus", false, false)]
    public int SoftwareVersionStatus { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result", "SoftwarePlatformId", false, false)]
    public int SoftwarePlatformId { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result", "SoftwarePlatformString", false, false)]
    public string SoftwarePlatformString { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result", "SoftwarePlatformStatus", false, false)]
    public int SoftwarePlatformStatus { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result", "RepositoryId", false, false)]
    public int RepositoryId { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result", "RepositoryRoot", false, false)]
    public string RepositoryRoot { get; set; }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "sp_GetModuleVersionForPlatform_Result", "RepositoryStatus", false, false)]
    public int RepositoryStatus { get; set; }
  }
}

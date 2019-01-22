using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.DB;
using Org.GS;

namespace Org.Software.Business.Models
{
  [DbMap(DbElement.Model, "Org_Software", "dbo", "AppLog")]
  public class AppLog : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLog", "LogId", false, true)]
    public long LogId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLog", "LogDateTime", false, false)]
    public DateTime LogDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLog", "SeverityCode", false, false)]
    public string SeverityCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLog", "Message", false, false)]
    public string Message {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLog", "ModuleCode", false, false)]
    public int ModuleCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLog", "EventCode", false, false)]
    public int EventCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLog", "OrgId", true, false)]
    public int? OrgId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "AppLogDetail")]
  public class AppLogDetail : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLogDetail", "LogDetailId", false, true)]
    public int LogDetailId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLogDetail", "LogId", false, false)]
    public long LogId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLogDetail", "AppLogDetailTypeCode", false, false)]
    public string AppLogDetailTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLogDetail", "LogDetail", false, false)]
    public string LogDetail {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "AppLogDetailType")]
  public class AppLogDetailType : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLogDetailType", "AppLogDetailTypeCode", false, true)]
    public string AppLogDetailTypeCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLogDetailType", "AppLogDetailTypeDesc", false, false)]
    public string AppLogDetailTypeDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "AppLogEvent")]
  public class AppLogEvent : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLogEvent", "EventCode", false, true)]
    public int EventCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLogEvent", "EventDesc", false, false)]
    public string EventDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "AppLogSeverity")]
  public class AppLogSeverity : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLogSeverity", "SeverityCode", false, true)]
    public string SeverityCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "AppLogSeverity", "SeverityDesc", false, false)]
    public string SeverityDesc {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "FrameworkVersion")]
  public class FrameworkVersion : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "FrameworkVersion", "FrameworkVersionId", false, true)]
    public int FrameworkVersionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "FrameworkVersion", "SoftwareStatusId", false, false)]
    public int SoftwareStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "FrameworkVersion", "FrameworkVersionString", false, false)]
    public string FrameworkVersionString {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "FrameworkVersion", "Version", false, false)]
    public string Version {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "FrameworkVersion", "VersionNum", false, false)]
    public string VersionNum {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "FrameworkVersion", "ServicePackString", false, false)]
    public string ServicePackString {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "Module")]
  public class Module : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "Module", "ModuleCode", false, true)]
    public int ModuleCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "Module", "ModuleName", false, false)]
    public string ModuleName {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "Organization")]
  public class Organization : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "Organization", "OrgId", false, true)]
    public int OrgId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "Organization", "ParentOrgId", true, false)]
    public int? ParentOrgId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "Organization", "OrgStatusId", false, false)]
    public int OrgStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "Organization", "OrgTypeId", false, false)]
    public int OrgTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "Organization", "OrgName", false, false)]
    public string OrgName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "Organization", "OrgDescription", true, false)]
    public string OrgDescription {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "OrgStatus")]
  public class OrgStatus : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "OrgStatus", "OrgStatusId", false, true)]
    public int OrgStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "OrgStatus", "OrgStatusAbbr", false, false)]
    public string OrgStatusAbbr {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "OrgStatus", "OrgStatusValue", false, false)]
    public string OrgStatusValue {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "OrgType")]
  public class OrgType : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "OrgType", "OrgTypeId", false, true)]
    public int OrgTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "OrgType", "OrgTypeValue", false, false)]
    public string OrgTypeValue {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "SoftwareModule")]
  public class SoftwareModule : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareModule", "SoftwareModuleId", false, true)]
    public int SoftwareModuleId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareModule", "SoftwareModuleCode", false, false)]
    public int SoftwareModuleCode {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareModule", "SoftwareModuleName", false, false)]
    public string SoftwareModuleName {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareModule", "SoftwareModuleTypeId", false, false)]
    public int SoftwareModuleTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareModule", "SoftwareStatusId", false, false)]
    public int SoftwareStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareModule", "CreatedDateTime", false, false)]
    public DateTime CreatedDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareModule", "CreatedAccountId", false, false)]
    public int CreatedAccountId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareModule", "ModifiedDateTime", true, false)]
    public DateTime? ModifiedDateTime {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareModule", "ModifiedAccountId", true, false)]
    public int? ModifiedAccountId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "SoftwareModuleType")]
  public class SoftwareModuleType : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareModuleType", "SoftwareModuleTypeId", false, true)]
    public int SoftwareModuleTypeId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareModuleType", "SoftwareModuleTypeName", false, false)]
    public string SoftwareModuleTypeName {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "SoftwarePlatform")]
  public class SoftwarePlatform : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwarePlatform", "SoftwarePlatformId", false, true)]
    public int SoftwarePlatformId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwarePlatform", "SoftwarePlatformString", false, false)]
    public string SoftwarePlatformString {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwarePlatform", "PlatformDescription", false, false)]
    public string PlatformDescription {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwarePlatform", "SoftwareStatusId", false, false)]
    public int SoftwareStatusId {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "SoftwareRepository")]
  public class SoftwareRepository : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareRepository", "RepositoryId", false, true)]
    public int RepositoryId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareRepository", "SoftwareStatusId", false, false)]
    public int SoftwareStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareRepository", "RepositoryRoot", false, false)]
    public string RepositoryRoot {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "SoftwareStatus")]
  public class SoftwareStatus : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareStatus", "SoftwareStatusId", false, true)]
    public int SoftwareStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareStatus", "SoftwareStatus", false, false)]
    public string SoftwareStatus1 {
      get;
      set;
    }
  }

  [DbMap(DbElement.Model, "Org_Software", "dbo", "SoftwareVersion")]
  public class SoftwareVersion : ModelBase
  {
    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareVersion", "SoftwareVersionId", false, true)]
    public int SoftwareVersionId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareVersion", "SoftwareStatusId", false, false)]
    public int SoftwareStatusId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareVersion", "SoftwareModuleId", false, false)]
    public int SoftwareModuleId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareVersion", "SoftwareVersion", false, false)]
    public string SoftwareVersion1 {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareVersion", "SoftwarePlatformId", false, false)]
    public int SoftwarePlatformId {
      get;
      set;
    }

    [DbMap(DbElement.Column, "Org_Software", "dbo", "SoftwareVersion", "RepositoryId", false, false)]
    public int RepositoryId {
      get;
      set;
    }
  }
}

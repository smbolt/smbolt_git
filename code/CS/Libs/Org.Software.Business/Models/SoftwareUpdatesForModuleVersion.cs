using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Software.Business.Models
{
  public class SoftwareUpdatesForModuleVersion
  {
    public int SoftwareModuleCode {
      get;
      set;
    }
    public string SoftwareModuleName {
      get;
      set;
    }
    public int SoftwarePlatformId {
      get;
      set;
    }
    public string SoftwarePlatformString {
      get;
      set;
    }
    public string PlatformDescription {
      get;
      set;
    }
    public int SoftwareVersionId {
      get;
      set;
    }
    public int SoftwareModuleId {
      get;
      set;
    }
    public string VersionValue {
      get;
      set;
    }
    public int RepositoryId {
      get;
      set;
    }
    public string RepositoryRoot {
      get;
      set;
    }
  }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Software.Business.Models
{
  public class ModuleVersionForPlatform
  {
    public string ModuleName {
      get;
      set;
    }
    public string ModuleTypeName {
      get;
      set;
    }
    public int ModuleStatus {
      get;
      set;
    }
    public string VersionValue {
      get;
      set;
    }
    public int VersionStatus {
      get;
      set;
    }
    public int PlatformId {
      get;
      set;
    }
    public string PlatformString {
      get;
      set;
    }
    public int PlatformStatus {
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
    public int RepositoryStatus {
      get;
      set;
    }
  }
}

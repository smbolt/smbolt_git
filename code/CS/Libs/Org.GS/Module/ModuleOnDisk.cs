using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class ModuleOnDisk
  {
    public string ModuleName {
      get;
      set;
    }
    public string ModuleVersion {
      get;
      set;
    }
    public string ModuleKey {
      get {
        return Get_ModuleKey();
      }
    }
    public string ModuleFolder {
      get;
      set;
    }

    public ModuleOnDisk()
    {
      this.ModuleName = String.Empty;
      this.ModuleVersion = String.Empty;
      this.ModuleFolder = String.Empty;
    }

    private string Get_ModuleKey()
    {
      string moduleName = this.ModuleName.IsNotBlank() ? this.ModuleName.Trim() : "ModuleNameMissing";
      string moduleVersion = this.ModuleVersion.IsNotBlank() ? this.ModuleVersion.Trim() : "ModuleVersionMissing";
      return moduleName + "_" + moduleVersion;
    }
  }
}

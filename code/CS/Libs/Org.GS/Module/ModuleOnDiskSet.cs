using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Org.GS
{
  public enum ModuleOnDiskSetType
  {
    NotModuleSet,
    OneLevel,
    TwoLevel
  }


  public class ModuleOnDiskSet : Dictionary<string, ModuleOnDisk>
  {
    private string _moduleName = String.Empty;

    public ModuleOnDiskSet(string moduleRootPath, string moduleName)
    {
      if (!Directory.Exists(moduleRootPath))
        return;

      if (moduleName.IsNotBlank())
        _moduleName = moduleName.Trim();

      List<string> moduleFolders = Directory.GetDirectories(moduleRootPath).ToList();

      foreach (string moduleFolder in moduleFolders)
      {
        string moduleFolderName = Path.GetFileName(moduleFolder);

        if (_moduleName.IsNotBlank() && moduleFolderName != _moduleName)
          continue;

        List<string> versionFolders = Directory.GetDirectories(moduleFolder).ToList();
        foreach (string versionFolder in versionFolders)
        {
          string versionFolderName = Path.GetFileName(versionFolder);
          var moduleOnDisk = new ModuleOnDisk();
          moduleOnDisk.ModuleName = moduleFolderName;
          moduleOnDisk.ModuleVersion = versionFolderName;
          moduleOnDisk.ModuleFolder = versionFolder; 

          if (!this.ContainsKey(moduleOnDisk.ModuleKey))
            this.Add(moduleOnDisk.ModuleKey, moduleOnDisk);
        }
      }
    }
  }
}

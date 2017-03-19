using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Org.GS;

namespace Org.GS.Dynamic
{
  public class ModuleManager
  {
    public Module EntryModule { get; set; } 
    public string RuntimePath { get; set; }
    public string RepositoryPath { get; set; }
    public ModuleSet ModulesInRepository { get; set; }

    private string _mainModule;
    public string MainModule 
    {
      get { return this._mainModule; }
      set
      {
          this._mainModule = value;
          MarkMainModule();
      }
    }

    public ModuleManager(string mainModule)
    {
      this.Initialize();
      this.MainModule = mainModule;
    }

    private void Initialize()
    {
      this.EntryModule = new Module(Assembly.GetEntryAssembly());
      this.RuntimePath = Path.GetDirectoryName(Application.ExecutablePath);
      this.RepositoryPath = g.GetAppPath() + @"\" + "Modules";
      this._mainModule = String.Empty;
      this.ModulesInRepository = GetModulesInRepository();
    }

    public ModuleSet GetModulesInRepository()
    {
      ModuleSet moduleSet = new ModuleSet();

      List<string> moduleFolders = Directory.GetDirectories(this.RepositoryPath).ToList();
      foreach(string moduleFolder in moduleFolders)
      {
        List<string> versionFolders = Directory.GetDirectories(moduleFolder).ToList();                
        foreach (string versionFolder in versionFolders)
        {
          int position = 0;
          string[] files = Directory.GetFiles(versionFolder, "*.Module.dll");
          Module module = new Module();
          string versionFolderName = Path.GetFileName(versionFolder);
          foreach (string file in files)
          {
            position = file.LastIndexOf(@"\");
            if (position != -1 && file.Length > position)
            {
              module.ModuleName = file.Substring(position + 1).Replace(".dll", String.Empty);
              module.ModulePath = versionFolder;
              module.ModuleVersion = versionFolderName;

              if (!moduleSet.ContainsKey(module.ModuleKey))
                moduleSet.Add(module.ModuleKey, module);
            }
          }
        }
      }

      return moduleSet; 
    }

    private void MarkMainModule()
    {
      if (!this.ModulesInRepository.ContainsKey(this._mainModule))
        throw new Exception("Invalid mainModule specified '" + this._mainModule + "' - not found in module repository.");

      this.ModulesInRepository.MainModule = this._mainModule;
    }      
  }
}

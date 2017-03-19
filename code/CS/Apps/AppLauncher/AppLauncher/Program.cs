using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Org.MOD.Contracts;
using Org.MOD.Concrete;
using Org.SoftwareUpdates;
using Org.GS;

namespace Org.AppLauncher
{
  static class Program
  {
    private static a a;
    private static bool _exitDueToStartupFailure = false;
    private static bool _launchModule = true;

    [ImportMany(typeof(IModule))]
    private static IEnumerable<Lazy<IModule, IModuleMetadata>> _modules;
    private static Dictionary<string, IModule> _availableModules;
    private static CompositionContainer _container;
    private static IModule _theModule = null;


    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      Initialize();

      if (g.GetCI("RunSoftwareUpdates").ToBoolean())
      {
        RunSoftwareUpdates();
      }

      if (!_launchModule)
        return; 

      LoadModule();

      if (_exitDueToStartupFailure)
      {
        MessageBox.Show("No module will be launched.  AppLauncher will close.", "AppLauncher - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      _theModule.Run();
    }

    private static void Initialize()
    {
      try
      {
        a = new a();
      }
      catch(Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to load the AppConfig file." + g.crlf2 + ex.ToReport(), "AppLauncher - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      g.AppConfig.ResetPcKeys();
    }

    private static void LoadModule()
    {
      string modulePath = g.GetCI("ModulePath"); 
      string moduleToRun = g.GetCI("ModuleToRun");
      string moduleVersion = g.GetCI("ModuleVersion");

      if (modulePath.IsBlank())
      {
        MessageBox.Show("A valid 'ModulePath' configuration must be supplied.", "AppLauncher - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _exitDueToStartupFailure = true;
        return;
      }

      bool useModuleSet = g.CI("UseModuleSet").ToBoolean();

      if (!Directory.Exists(modulePath))
      {
        MessageBox.Show("The path specified in the 'ModulePath' configuration is not valid." + g.crlf2 +
                        "Specified path is '" + modulePath + "'.", "AppLauncher - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _exitDueToStartupFailure = true;
        return;
      }

      var moduleSet = new ModuleOnDiskSet(modulePath, moduleToRun); 

      AggregateCatalog catalog = new AggregateCatalog();

      if (useModuleSet)
      {
        foreach (var moduleOnDisk in moduleSet.Values)
          catalog.Catalogs.Add(new DirectoryCatalog(moduleOnDisk.ModuleFolder));
      }
      else
      {
				catalog.Catalogs.Add(new DirectoryCatalog(modulePath));
      }

      _container = new CompositionContainer(catalog);

      try
      {
        _modules = _container.GetExports<IModule, IModuleMetadata>();
        LoadAvailableModules();
      }
      catch (CompositionException ex)
      {
        MessageBox.Show("An exception occurred attempting to locate MEF modules." + g.crlf2 + ex.ToReport(), "AppLauncher - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _exitDueToStartupFailure = true;
        return;
      }

      try
      {
        if (moduleToRun.IsBlank() || moduleVersion.IsBlank())
        {
          MessageBox.Show("Both the 'ModuleToRun' configuration and the 'ModuleVersion' configuration must be supplied." + g.crlf2 +
                          "Value for 'ModuleToRun' is '" + moduleToRun + "'" + g.crlf2 +
                          "Value supplied for 'ModuleVersion' is '" + moduleVersion + "'", "AppLauncher - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          _exitDueToStartupFailure = true;
          return;
        }

        _theModule = GetModule(moduleToRun, moduleVersion);
        if (_theModule == null)
        {
          MessageBox.Show("Module '" + moduleToRun + "', Version '" + moduleVersion + "' not found.", "AppLauncher - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          _exitDueToStartupFailure = true;
          return;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to load the following module:" + g.crlf2 +
                        "ModuleToRun: " + moduleToRun + g.crlf +
                        "Version: " + moduleVersion + g.crlf2 + "Exception:" + g.crlf + ex.ToReport(),
                        "AppLauncher - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _exitDueToStartupFailure = true;
        return;
      }
    }

    private static IModule GetModule(string moduleName, string moduleVersion)
    {
      if (_availableModules.ContainsKey(moduleName + "_" + moduleVersion))
        return _availableModules[moduleName + "_" + moduleVersion];

      return null;
    }

    private static void LoadAvailableModules()
    {      
      // Load available MEF modules in a general collection keyed with ModuleName_ModuleVersion.
      // Also place entry in the general collection keyed with ModuleName_LatestVersion.
      // In this way modules can be located by either specific version or by logical "LatestVersion" value.

      _availableModules = new Dictionary<string, IModule>();
      var moduleVersions = new Dictionary<string, SortedList<string, IModule>>();

      foreach (Lazy<IModule, IModuleMetadata> module in _modules)
      {
        string moduleKey = module.Metadata.Name + "_" + module.Metadata.Version; 
        if (_availableModules.ContainsKey(moduleKey))
        {
          throw new Exception("More than one module was found with module name '" +  module.Metadata.Name + "' and " + 
                              "module version '" + module.Metadata.Version + "'."); 
        }
        else
        {
          _availableModules.Add(moduleKey, module.Value);
          if (!moduleVersions.ContainsKey(module.Metadata.Name))
            moduleVersions.Add(module.Metadata.Name, new SortedList<string,IModule>()); 

          if (!moduleVersions[module.Metadata.Name].ContainsKey(module.Metadata.Version))
            moduleVersions[module.Metadata.Name].Add(module.Metadata.Version, module.Value); 
        }
      }

      foreach(string moduleName in moduleVersions.Keys)
      {
        _availableModules.Add(moduleName + "_" + "LatestVersion", moduleVersions[moduleName].Values.Last()); 
      }
    }

    private static void RunSoftwareUpdates()
    {
      using(frmUpdateManager fUpdateManager = new frmUpdateManager())
      {
        if (!fUpdateManager.IsDisposed)
        {
          fUpdateManager.ShowDialog();
          _launchModule = fUpdateManager.LaunchModule; 
        }
      }
    }

  }
}

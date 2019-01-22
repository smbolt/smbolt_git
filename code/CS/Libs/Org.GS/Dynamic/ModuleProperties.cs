using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS.Dynamic
{
  public class ModuleProperties
  {
    public bool ShowModuleExplorer {
      get;
      set;
    }
    public bool UseSplashScreen {
      get;
      set;
    }
    public string ModuleName {
      get;
      set;
    }
    public string ModuleVersion {
      get;
      set;
    }
    public string MainFormTitle {
      get;
      set;
    }

    public ModuleProperties()
    {
      this.Initialize();
    }

    private void Initialize()
    {
      this.ShowModuleExplorer = false;
      this.UseSplashScreen = false;
      this.ModuleName = "ModuleName.Module";
      this.ModuleVersion = "1.0.0.0";
      this.MainFormTitle = "ModuleBasedApplication";
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using Org.GS; 

namespace Org.MOD.Concrete
{
  public class WinAppModule : ModuleBase, IDisposable
  {
    public Image SplashImage { get; private set; }

    public WinAppModule(Assembly module) : base(module)
    {      
      this.SplashImage = (Bitmap)base.ResourceManager.GetObject("SplashImage");
      if (this.SplashImage == null)
        throw new Exception("The splash form image for module " + base.ModuleName + " is not located in the module resource file.");
    }

    ~WinAppModule()
    {
      this.Dispose();
    }    

    public override void Dispose()
    {
      base.Dispose();
      Dispose(true); 
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing); 

      if (disposing)
      {
        if (this.SplashImage != null)
        {
          this.SplashImage.Dispose();
        }
      }
    }
  }
}

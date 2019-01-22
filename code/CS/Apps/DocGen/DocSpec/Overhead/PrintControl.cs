using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
  public class PrintControl
  {
    public bool CreateDocument {
      get;
      set;
    }
    public float Scale {
      get;
      set;
    }
    public float WidthFactor {
      get;
      set;
    }
    public float SpaceWidthFactor {
      get;
      set;
    }
    public float LineFactor {
      get;
      set;
    }
    public int TextContrast {
      get;
      set;
    }

    public PrintControl()
    {
      this.CreateDocument = true;
      this.Scale = 1.0F;
      this.WidthFactor = 0.220F;
      this.SpaceWidthFactor = 0.150F;
      this.LineFactor = 0.975F;
      this.TextContrast = 3;
    }
  }
}

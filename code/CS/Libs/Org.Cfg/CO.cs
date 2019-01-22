using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Cfg
{
  public class CO
  {
    public virtual string CIType {
      get {
        throw new Exception("CIType property must be overriden in derived class.");
      }
    }
    public virtual string CIName {
      get;
      set;
    }
    public string SourceFileName {
      get;
      set;
    }
  }
}

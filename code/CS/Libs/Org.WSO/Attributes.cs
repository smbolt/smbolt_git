using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Org.GS;

namespace Org.WSO
{   
  [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = false)]
  public class WCFTrans : Attribute 
  {
    public string Version;

    [DebuggerStepThrough]
    public WCFTrans(string version = "1.0.0.0")  
    {
      this.Version = version;
    }
  }

}

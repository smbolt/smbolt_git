using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Security
{
  public static class AdSecurityManager
  {
    private static bool _isInitialized = Initialize();
    public static SecurityGroupSet SecurityGroupSet {
      get;
      set;
    }

    public static string FullControlGroup {
      get;
      set;
    }
    public static string ReadOnlyGroup {
      get;
      set;
    }

    public static bool Initialize()
    {
      if (_isInitialized)
        return true;
      SecurityGroupSet = new SecurityGroupSet();
      return true;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.GS;


namespace Org.GS.Binary
{
  public static class ExtensionMethods
  {
    //[DebuggerStepThrough]
    public static void ZeroInit(this byte[] a)
    {
      if (a == null)
        return;

      if (a.Length == 0)
        return;

      for (int i = 0; i < a.Length; i++)
        a[i] = 0x00; 
    }
  }
}

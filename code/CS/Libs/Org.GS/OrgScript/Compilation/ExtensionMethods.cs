using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.OrgScript.Compilation
{
  public static class ExtensionMethods
  {
    public static string Display(this SyntaxNodeType t)
    {
      if (t == SyntaxNodeType.Spaces)
        return "Spaces!";



      return t.ToString();
    }
  }
}

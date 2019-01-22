using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class FileSpec
  {
    [XMap]
    public string Name {
      get;
      set;
    }

    public FileSpec()
    {
      this.Name = String.Empty;
    }

    public FileSpec(string pattern)
    {
      this.Name = pattern;
    }
  }
}

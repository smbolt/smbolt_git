using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS 
{
  public class AssemblyMap 
  {
    public string AssemblyName { get; set; }
    public string CodeBase { get; set; }
    public string ImageRuntimeVersion { get; set; }
    public object ManifestModule { get; set; }
    public string FullName { get; set; }
    public object CustomAttributes { get; set; }

    public AssemblyMap()
    {
      this.AssemblyName = string.Empty;
      this.CodeBase = string.Empty;
      this.ImageRuntimeVersion = string.Empty;
      this.ManifestModule = string.Empty;
      this.FullName = string.Empty;
      this.CustomAttributes = string.Empty;
    }
  }
}

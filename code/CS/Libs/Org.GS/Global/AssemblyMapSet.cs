using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class AssemblyMapSet : Dictionary<string, AssemblyMap>
  {
    public string ToReport()
    {
      StringBuilder sb = new StringBuilder();

      foreach (var map in this.Values)
      {
        sb.Append("Assembly Name" + g.crlf);
        sb.Append("  " + map.AssemblyName + g.crlf);
        sb.Append("  " + map.CodeBase + g.crlf);
        sb.Append("  " + map.ImageRuntimeVersion + g.crlf);
        sb.Append("  " + map.ManifestModule + g.crlf);
        sb.Append("  " + map.FullName + g.crlf);
        sb.Append("  " + map.CustomAttributes + g.crlf);
        sb.Append("         " + g.crlf);
      }

      return sb.ToString();
    }
  }
}

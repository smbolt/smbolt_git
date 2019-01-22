using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public static class FileSystemExtensionMethods
  {
    public static string ToFileActionReport(this System.IO.FileInfo fi, bool isDryRun, string action, int level)
    {
      if (fi == null)
        return "FileInfo object is null";

      if (action.IsBlank())
        action = "*** ACTION IS BLANK ***";

      bool isDirectory = fi.Attributes.HasFlag(System.IO.FileAttributes.Directory);

      if (isDirectory)
      {
        return (isDryRun ? "*DRY-RUN* " : "") +
               action + " FLDR " +
               fi.FullName;
      }
      else
      {
        return (isDryRun ? "*DRY-RUN* " : "") +
               action + " FILE " +
               fi.Name + " (" + fi.Length.ToString("###,###,###,##0") + " bytes)";
      }
    }
  }
}

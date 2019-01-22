using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class FileCompareUtility
  {
    public static FileCompareResult CompareFiles(string scriptFile, string baseFilePath, string compareFilePath, string reportFile)
    {
      try
      {
        var result = new FileCompareResult();

        using (var fileCompareEngine = new FileCompareEngine())
        {
          return fileCompareEngine.CompareFiles(scriptFile, baseFilePath, compareFilePath, reportFile);
        }
      }

      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to compare the files '" + baseFilePath + "' and '" +
                            compareFilePath + "'.", ex);
      }
    }
  }
}

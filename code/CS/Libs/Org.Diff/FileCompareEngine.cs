using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Diff.DiffBuilder.Model;
using Org.Diff.DiffBuilder;
using Org.GS;

namespace Org.Diff
{
  public class FileCompareEngine : IDisposable
  {

    public FileCompareEngine()
    {
    }

    public FileCompareResult CompareFiles(FileCompareParms parms)
    {
      var fileCompareResult = new FileCompareResult();

      try
      {
        if (!File.Exists(parms.LeftPath))
          throw new Exception("The 'leftPath' parameter does not point to a valid file - parameter value is '" + parms.LeftPath + "'.");

        if (!File.Exists(parms.RightPath))
          throw new Exception("The 'rightPath' parameter does not point to a valid file - parameter value is '" + parms.RightPath + "'.");

        string file1 = File.ReadAllText(parms.LeftPath);
        string file2 = File.ReadAllText(parms.RightPath);

        IDiffBuilder diffBuilder = parms.FileCompareReportLayout == FileCompareReportLayout.Inline ?
                                (IDiffBuilder) new InlineDiffBuilder(new Differ()) :
                                (IDiffBuilder) new SideBySideDiffBuilder(new Differ());

        var diffModel = diffBuilder.BuildDiffModel(file1, file2, parms.CreateFileComparisionReport);

        fileCompareResult.FileCompareStatus = diffModel.FileCompareStatus;
        fileCompareResult.FileCompareReport = diffModel.FileCompareReport;

        return fileCompareResult;
      }
      catch (Exception ex)
      {
        fileCompareResult.FileCompareStatus = FileCompareStatus.CompareOperationFailed;
        fileCompareResult.Exception = ex;
        fileCompareResult.FileCompareReport = String.Empty;
        return fileCompareResult;
      }
    }

    public void Dispose()
    {
    }
  }
}

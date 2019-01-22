using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Diff
{
  public class FileCompareResult
  {
    public FileCompareStatus FileCompareStatus {
      get;
      set;
    }
    public string FileCompareReport {
      get;
      set;
    }
    public Exception Exception {
      get;
      set;
    }

    public FileCompareResult()
    {
      this.FileCompareStatus = FileCompareStatus.CompareOperationNotStarted;
      this.FileCompareReport = String.Empty;
      this.Exception = null;
    }
  }
}

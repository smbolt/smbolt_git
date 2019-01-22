using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class FileCompareResult
  {
    public bool FilesMatch { get; set; }
    public string ComparisionReportPath { get; set; }

    public FileCompareResult()
    {
      this.FilesMatch = false;
      this.ComparisionReportPath = String.Empty;
    }
  }
}

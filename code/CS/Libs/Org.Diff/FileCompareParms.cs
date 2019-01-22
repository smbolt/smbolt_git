using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Diff
{
  public class FileCompareParms
  {
    public string LeftPath { get; set; }
    public string RightPath { get; set; }
    public bool IgnoreWhiteSpace { get; set; }
    public bool IgnoreTextCase { get; set; }
    public bool CreateFileComparisionReport { get; set; }
    public FileCompareReportLayout FileCompareReportLayout { get; set; }

    public FileCompareParms()
    {
      this.LeftPath = String.Empty;
      this.RightPath = String.Empty;
      this.IgnoreWhiteSpace = false;
      this.IgnoreTextCase = false;
      this.CreateFileComparisionReport = true;
      this.FileCompareReportLayout = FileCompareReportLayout.Inline;
    }
  }
}

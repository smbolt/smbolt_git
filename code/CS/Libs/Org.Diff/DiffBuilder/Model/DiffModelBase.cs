using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Diff.DiffBuilder.Model
{
  public class DiffModelBase
  {
    public FileCompareStatus FileCompareStatus { get; set; }
    public string FileCompareReport { get; set; }

    public DiffModelBase()
    {
      this.FileCompareStatus = FileCompareStatus.CompareOperationNotStarted;
      this.FileCompareReport = String.Empty;
    }
  }
}

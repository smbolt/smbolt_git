using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Diff
{
  public enum FileCompareStatus
  {
    CompareOperationNotStarted,
    Matched,
    NotMatched,
    CompareOperationFailed
  }

  public enum FileCompareReportLayout
  {
    Inline,
    SideBySide,
  }
}

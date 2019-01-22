using System.Collections.Generic;

namespace Org.Diff.DiffBuilder.Model
{
  public class DiffPaneModel : DiffModelBase
  {
    public List<DiffPiece> Lines { get; }

    public DiffPaneModel()
    {
      base.FileCompareStatus = FileCompareStatus.CompareOperationNotStarted;

      Lines = new List<DiffPiece>();
    }
  }
}

namespace Org.Diff.DiffBuilder.Model
{
  /// <summary>
  /// A model which represents differences between to texts to be shown side by side
  /// </summary>
  public class SideBySideDiffModel : DiffModelBase
  {
    public DiffPaneModel OldText { get; }
    public DiffPaneModel NewText { get; }

    public SideBySideDiffModel()
    {
      base.FileCompareStatus = FileCompareStatus.CompareOperationNotStarted;

      OldText = new DiffPaneModel();
      NewText = new DiffPaneModel();
    }
  }
}

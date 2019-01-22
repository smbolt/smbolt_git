using Org.Diff.DiffBuilder.Model;

namespace Org.Diff.DiffBuilder
{
  public interface IDiffBuilder
  {
    DiffModelBase BuildDiffModel(string oldText, string newText, bool createFileCompareReport);
  }
}

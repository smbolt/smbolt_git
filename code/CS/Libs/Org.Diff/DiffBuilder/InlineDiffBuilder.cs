using System;
using System.Collections.Generic;
using System.Text;
using Org.Diff.DiffBuilder.Model;
using Org.Diff.Model;
using Org.GS;

namespace Org.Diff.DiffBuilder
{
  public class InlineDiffBuilder : IDiffBuilder
  {
    private readonly IDiffer differ;
    private bool _createDiffReport;

    public InlineDiffBuilder(IDiffer differ)
    {
      this.differ = differ ?? throw new ArgumentNullException(nameof(differ));
    }

    public DiffModelBase BuildDiffModel(string oldText, string newText, bool createDiffReport)
    {
      if (oldText == null) throw new ArgumentNullException(nameof(oldText));
      if (newText == null) throw new ArgumentNullException(nameof(newText));

      _createDiffReport = createDiffReport;

      var model = new DiffPaneModel();
      var diffResult = differ.CreateLineDiffs(oldText, newText, ignoreWhitespace: true);

      BuildDiffPieces(diffResult, model.Lines);

      if (_createDiffReport)
        PrepareResultsAndReport(model);
      else
        PrepareResults(model);

      return model;
    }

    private void PrepareResults(DiffPaneModel model)
    {
      model.FileCompareReport = String.Empty;
      int diffCount = 0;

      foreach (var line in model.Lines)
      {
        switch (line.Type)
        {
          case ChangeType.Inserted:
          case ChangeType.Deleted:
            diffCount++;
            break;
        }
      }

      model.FileCompareStatus = diffCount == 0 ? FileCompareStatus.Matched : FileCompareStatus.NotMatched;
    }

    private void PrepareResultsAndReport(DiffPaneModel model)
    {
      StringBuilder sb = new StringBuilder();
      int diffCount = 0;

      foreach (var line in model.Lines)
      {
        switch (line.Type)
        {
          case ChangeType.Inserted:
            diffCount++;
            sb.Append("[+] " + line.Text + g.crlf);
            break;

          case ChangeType.Deleted:
            diffCount++;
            sb.Append("[-] " + line.Text + g.crlf);
            break;

          default:
            sb.Append("    " + line.Text + g.crlf);
            break;
        }
      }

      model.FileCompareStatus = diffCount == 0 ? FileCompareStatus.Matched : FileCompareStatus.NotMatched;

      model.FileCompareReport = sb.ToString();
    }

    private static void BuildDiffPieces(DiffResult diffResult, List<DiffPiece> pieces)
    {
      int bPos = 0;

      foreach (var diffBlock in diffResult.DiffBlocks)
      {
        for (; bPos < diffBlock.InsertStartB; bPos++)
          pieces.Add(new DiffPiece(diffResult.PiecesNew[bPos], ChangeType.Unchanged, bPos + 1));

        int i = 0;
        for (; i < Math.Min(diffBlock.DeleteCountA, diffBlock.InsertCountB); i++)
          pieces.Add(new DiffPiece(diffResult.PiecesOld[i + diffBlock.DeleteStartA], ChangeType.Deleted));

        i = 0;
        for (; i < Math.Min(diffBlock.DeleteCountA, diffBlock.InsertCountB); i++)
        {
          pieces.Add(new DiffPiece(diffResult.PiecesNew[i + diffBlock.InsertStartB], ChangeType.Inserted, bPos + 1));
          bPos++;
        }

        if (diffBlock.DeleteCountA > diffBlock.InsertCountB)
        {
          for (; i < diffBlock.DeleteCountA; i++)
            pieces.Add(new DiffPiece(diffResult.PiecesOld[i + diffBlock.DeleteStartA], ChangeType.Deleted));
        }
        else
        {
          for (; i < diffBlock.InsertCountB; i++)
          {
            pieces.Add(new DiffPiece(diffResult.PiecesNew[i + diffBlock.InsertStartB], ChangeType.Inserted, bPos + 1));
            bPos++;
          }
        }
      }

      for (; bPos < diffResult.PiecesNew.Length; bPos++)
        pieces.Add(new DiffPiece(diffResult.PiecesNew[bPos], ChangeType.Unchanged, bPos + 1));
    }
  }
}

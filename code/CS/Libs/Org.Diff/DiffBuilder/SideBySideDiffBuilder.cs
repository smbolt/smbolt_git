using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.Diff.DiffBuilder.Model;
using Org.Diff.Model;
using Org.GS;

namespace Org.Diff.DiffBuilder
{
  public class SideBySideDiffBuilder : IDiffBuilder
  {
    private readonly IDiffer differ;

    private delegate void PieceBuilder(string oldText, string newText, List<DiffPiece> oldPieces, List<DiffPiece> newPieces);

    public static readonly char[] WordSeparaters = { ' ', '\t', '.', '(', ')', '{', '}', ',', '!' };

    public SideBySideDiffBuilder(IDiffer differ)
    {
      this.differ = differ ?? throw new ArgumentNullException(nameof(differ));
    }

    public DiffModelBase BuildDiffModel(string oldText, string newText, bool createDiffReport)
    {
      if (oldText.IsBlank())
        throw new ArgumentNullException(nameof(oldText));

      if (newText.IsBlank())
        throw new ArgumentNullException(nameof(newText));

      var model = BuildLineDiff(oldText, newText);

      if (createDiffReport)
        PrepareResultsAndReport(model);
      else
        PrepareResults(model);

      return model;
    }

    private void PrepareResults(SideBySideDiffModel model)
    {
      model.FileCompareReport = String.Empty;
      int diffCount = 0;

      foreach (var line in model.OldText.Lines)
      {
        switch (line.Type)
        {
          case ChangeType.Inserted:
          case ChangeType.Deleted:
            diffCount++;
            break;
        }
      }

      foreach (var line in model.NewText.Lines)
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

    private void PrepareResultsAndReport(SideBySideDiffModel model)
    {
      int diffCount = 0;

      StringBuilder sb = new StringBuilder();

      int longestLine = 0;
      for (int i = 0; i < model.OldText.Lines.Count; i++)
      {
        if (model.OldText.Lines.ElementAt(i).Text.Length > longestLine)
          longestLine = model.OldText.Lines.ElementAt(i).Text.Length;

        if (model.NewText.Lines.ElementAt(i).Text.Length > longestLine)
          longestLine = model.NewText.Lines.ElementAt(i).Text.Length;
      }

      for (int i = 0; i < model.OldText.Lines.Count; i++)
      {
        var leftLine = model.OldText.Lines.ElementAt(i);
        var rightLine = model.NewText.Lines.ElementAt(i);

        string leftInd = GetChangeIndicator(leftLine.Type);
        string rightInd = GetChangeIndicator(rightLine.Type);

        if (leftLine.Type != ChangeType.Unchanged || rightLine.Type != ChangeType.Unchanged)
          diffCount++;

        sb.Append(leftInd + leftLine.Text.PadTo(longestLine) + "      " + rightInd + rightLine.Text.PadTo(longestLine) + g.crlf);
      }

      model.FileCompareStatus = diffCount == 0 ? FileCompareStatus.Matched : FileCompareStatus.NotMatched;
      model.FileCompareReport = sb.ToString();
    }

    private string GetChangeIndicator(ChangeType changeType)
    {
      switch (changeType)
      {
        case ChangeType.Deleted:
          return "[-] ";
        case ChangeType.Inserted:
          return "[+] ";
        case ChangeType.Imaginary:
          return "[~] ";
        case ChangeType.Modified:
          return "[M] ";
      }

      return "    ";
    }

    private SideBySideDiffModel BuildLineDiff(string oldText, string newText)
    {
      var model = new SideBySideDiffModel();
      var diffResult = differ.CreateLineDiffs(oldText, newText, ignoreWhitespace: true);
      BuildDiffPieces(diffResult, model.OldText.Lines, model.NewText.Lines, BuildWordDiffPieces);
      return model;
    }

    private void BuildWordDiffPieces(string oldText, string newText, List<DiffPiece> oldPieces, List<DiffPiece> newPieces)
    {
      var diffResult = differ.CreateWordDiffs(oldText, newText, false, WordSeparaters);
      BuildDiffPieces(diffResult, oldPieces, newPieces, null);
    }

    private static void BuildDiffPieces(DiffResult diffResult, List<DiffPiece> oldPieces, List<DiffPiece> newPieces, PieceBuilder subPieceBuilder)
    {
      int aPos = 0;
      int bPos = 0;

      foreach (var diffBlock in diffResult.DiffBlocks)
      {
        while (bPos < diffBlock.InsertStartB && aPos < diffBlock.DeleteStartA)
        {
          oldPieces.Add(new DiffPiece(diffResult.PiecesOld[aPos], ChangeType.Unchanged, aPos + 1));
          newPieces.Add(new DiffPiece(diffResult.PiecesNew[bPos], ChangeType.Unchanged, bPos + 1));
          aPos++;
          bPos++;
        }

        int i = 0;
        for (; i < Math.Min(diffBlock.DeleteCountA, diffBlock.InsertCountB); i++)
        {
          var oldPiece = new DiffPiece(diffResult.PiecesOld[i + diffBlock.DeleteStartA], ChangeType.Deleted, aPos + 1);
          var newPiece = new DiffPiece(diffResult.PiecesNew[i + diffBlock.InsertStartB], ChangeType.Inserted, bPos + 1);

          if (subPieceBuilder != null)
          {
            subPieceBuilder(diffResult.PiecesOld[aPos], diffResult.PiecesNew[bPos], oldPiece.SubPieces, newPiece.SubPieces);
            newPiece.Type = oldPiece.Type = ChangeType.Modified;
          }

          oldPieces.Add(oldPiece);
          newPieces.Add(newPiece);
          aPos++;
          bPos++;
        }

        if (diffBlock.DeleteCountA > diffBlock.InsertCountB)
        {
          for (; i < diffBlock.DeleteCountA; i++)
          {
            oldPieces.Add(new DiffPiece(diffResult.PiecesOld[i + diffBlock.DeleteStartA], ChangeType.Deleted, aPos + 1));
            newPieces.Add(new DiffPiece());
            aPos++;
          }
        }
        else
        {
          for (; i < diffBlock.InsertCountB; i++)
          {
            newPieces.Add(new DiffPiece(diffResult.PiecesNew[i + diffBlock.InsertStartB], ChangeType.Inserted, bPos + 1));
            oldPieces.Add(new DiffPiece());
            bPos++;
          }
        }
      }

      while (bPos < diffResult.PiecesNew.Length && aPos < diffResult.PiecesOld.Length)
      {
        oldPieces.Add(new DiffPiece(diffResult.PiecesOld[aPos], ChangeType.Unchanged, aPos + 1));
        newPieces.Add(new DiffPiece(diffResult.PiecesNew[bPos], ChangeType.Unchanged, bPos + 1));
        aPos++;
        bPos++;
      }
    }
  }
}

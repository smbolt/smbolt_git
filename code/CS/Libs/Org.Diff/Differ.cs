using System;
using System.Collections.Generic;
using System.Text;
using Org.Diff.Model;
using Org.GS;
using Org.GS.Logging;

namespace Org.Diff
{
  public class Differ : IDiffer
  {
    //private static Logger _logger = new Logger();

    private Dictionary<string, int> _pieceHashes;

    private int[] _oldHashedPieces;
    private string[] _oldPieces;
    private string _oldRawData;
    private bool[] _oldMods;

    private int[] _newHashedPieces;

    private string[] _newPieces;
    private string _newRawData;
    private bool[] _newMods;

    public string Report {
      get {
        return Get_Report();
      }
    }

    private readonly string[] emptyStringArray = new string[0];

    public DiffResult CreateLineDiffs(string oldText, string newText, bool ignoreWhitespace)
    {
      return CreateLineDiffs(oldText, newText, ignoreWhitespace, false);
    }

    public DiffResult CreateLineDiffs(string oldText, string newText, bool ignoreWhitespace, bool ignoreCase)
    {
      if (oldText == null) throw new ArgumentNullException(nameof(oldText));
      if (newText == null) throw new ArgumentNullException(nameof(newText));


      return CreateCustomDiffs(oldText, newText, ignoreWhitespace, ignoreCase, str => str.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
    }

    public DiffResult CreateCharacterDiffs(string oldText, string newText, bool ignoreWhitespace)
    {
      return CreateCharacterDiffs(oldText, newText, ignoreWhitespace, false);
    }

    public DiffResult CreateCharacterDiffs(string oldText, string newText, bool ignoreWhitespace, bool ignoreCase)
    {
      if (oldText == null) throw new ArgumentNullException(nameof(oldText));
      if (newText == null) throw new ArgumentNullException(nameof(newText));


      return CreateCustomDiffs(
               oldText,
               newText,
               ignoreWhitespace,
               ignoreCase,
               str =>
      {
        var s = new string[str.Length];
        for (int i = 0; i < str.Length; i++) s[i] = str[i].ToString();
        return s;
      });
    }

    public DiffResult CreateWordDiffs(string oldText, string newText, bool ignoreWhitespace, char[] separators)
    {
      return CreateWordDiffs(oldText, newText, ignoreWhitespace, false, separators);
    }

    public DiffResult CreateWordDiffs(string oldText, string newText, bool ignoreWhitespace, bool ignoreCase, char[] separators)
    {
      if (oldText == null) throw new ArgumentNullException(nameof(oldText));
      if (newText == null) throw new ArgumentNullException(nameof(newText));


      return CreateCustomDiffs(
               oldText,
               newText,
               ignoreWhitespace,
               ignoreCase,
               str => SmartSplit(str, separators));
    }

    public DiffResult CreateCustomDiffs(string oldText, string newText, bool ignoreWhiteSpace, Func<string, string[]> chunker)
    {
      return CreateCustomDiffs(oldText, newText, ignoreWhiteSpace, false, chunker);
    }

    public DiffResult CreateCustomDiffs(string oldText, string newText, bool ignoreWhiteSpace, bool ignoreCase, Func<string, string[]> chunker)
    {
      if (oldText == null) throw new ArgumentNullException(nameof(oldText));
      if (newText == null) throw new ArgumentNullException(nameof(newText));
      if (chunker == null) throw new ArgumentNullException(nameof(chunker));

      var _oldPieceHash = new Dictionary<string, int>();
      var _newPieceHash = new Dictionary<string, int>();


      var lineDiffs = new List<DiffBlock>();

      _oldRawData = oldText;
      _newRawData = newText;

      _pieceHashes = new Dictionary<string, int>();

      BuildOldPieceHashes(ignoreWhiteSpace, ignoreCase, chunker);
      BuildNewPieceHashes(ignoreWhiteSpace, ignoreCase, chunker);

      BuildModificationData();

      //string report = "Old Hashed Pieces" + g.crlf +
      //                _oldHashedPieces.ToGrid(10, 6) + g.crlf2 +
      //                "Old Modifications" + g.crlf +
      //                _oldMods.ToGrid(10, 6) + g.crlf2 +
      //                "New Hashed Pieces" + g.crlf +
      //                _newHashedPieces.ToGrid(10, 6) + g.crlf2 +
      //                "New Modifications" + g.crlf +
      //                _newMods.ToGrid(10, 6);

      string report = this.Report;


      int piecesALength = _oldHashedPieces.Length;
      int piecesBLength = _newHashedPieces.Length;
      int posA = 0;
      int posB = 0;

      // loop through both sets of pieces
      // look for the modifications

      do
      {
        // advance past indices where A and B are the same (neither are modified)
        while (posA < piecesALength
               && posB < piecesBLength
               && !_oldMods[posA]
               && !_newMods[posB])
        {
          posA++;
          posB++;
        }

        int beginA = posA;
        int beginB = posB;

        for (; posA < piecesALength && _oldMods[posA]; posA++) ;

        for (; posB < piecesBLength && _newMods[posB]; posB++) ;

        int deleteCount = posA - beginA;
        int insertCount = posB - beginB;

        if (deleteCount > 0 || insertCount > 0)
        {
          lineDiffs.Add(new DiffBlock(beginA, deleteCount, beginB, insertCount));
        }
      } while (posA < piecesALength && posB < piecesBLength);

      return new DiffResult(_oldPieces, _newPieces, lineDiffs);
    }

    private string[] SmartSplit(string str, char[] delims)
    {
      var list = new List<string>();
      int begin = 0;
      for (int i = 0; i < str.Length; i++)
      {
        if (Array.IndexOf(delims, str[i]) != -1)
        {
          list.Add(str.Substring(begin, (i - begin)));
          list.Add(str.Substring(i, 1));
          begin = i + 1;
        }
        else if (i >= str.Length - 1)
        {
          list.Add(str.Substring(begin, (i + 1 - begin)));
        }
      }

      return list.ToArray();
    }

    /// <summary>
    /// Finds the middle snake and the minimum length of the edit script comparing string A and B
    /// </summary>
    /// <param name="A"></param>
    /// <param name="startA">Lower bound inclusive</param>
    /// <param name="endA">Upper bound exclusive</param>
    /// <param name="B"></param>
    /// <param name="startB">lower bound inclusive</param>
    /// <param name="endB">upper bound exclusive</param>
    /// <returns></returns>
    protected EditLengthResult CalculateEditLength(int[] A, int startA, int endA, int[] B, int startB, int endB)
    {
      int N = endA - startA;
      int M = endB - startB;
      int MAX = M + N + 1;

      var forwardDiagonal = new int[MAX + 1];
      var reverseDiagonal = new int[MAX + 1];
      return CalculateEditLength(A, startA, endA, B, startB, endB, forwardDiagonal, reverseDiagonal);
    }

    private EditLengthResult CalculateEditLength(int[] A, int startA, int endA, int[] B, int startB, int endB, int[] forwardDiagonal, int[] reverseDiagonal)
    {
      if (null == A) throw new ArgumentNullException(nameof(A));
      if (null == B) throw new ArgumentNullException(nameof(B));

      if (A.Length == 0 && B.Length == 0)
      {
        return new EditLengthResult();
      }

      int N = endA - startA;
      int M = endB - startB;
      int MAX = M + N + 1;
      int HALF = MAX / 2;
      int delta = N - M;
      bool deltaEven = delta % 2 == 0;
      forwardDiagonal[1 + HALF] = 0;
      reverseDiagonal[1 + HALF] = N + 1;

      //_logger.Log("Comparing strings");
      //_logger.Log(String.Format("\t{0} of length {1}", A, A.Length));
      //_logger.Log(String.Format("\t{0} of length {1}", B, B.Length));

      for (int D = 0; D <= HALF; D++)
      {
        //_logger.Log("\nSearching for a {0}-Path", D);
        // forward D-path
        //_logger.Log("\tSearching for forward path");
        Edit lastEdit;
        for (int k = -D; k <= D; k += 2)
        {
          //_logger.Log("\n\t\tSearching diagonal {0}", k);
          int kIndex = k + HALF;
          int x, y;
          if (k == -D || (k != D && forwardDiagonal[kIndex - 1] < forwardDiagonal[kIndex + 1]))
          {
            x = forwardDiagonal[kIndex + 1]; // y up    move down from previous diagonal
            lastEdit = Edit.InsertDown;
            //_logger.Log(String.Format("\t\tMoved down from diagonal {0} at ({1},{2}) to ", k + 1, x, (x - (k + 1))));
          }
          else
          {
            x = forwardDiagonal[kIndex - 1] + 1; // x up     move right from previous diagonal
            lastEdit = Edit.DeleteRight;
            //_logger.Log(String.Format("\t\tMoved right from diagonal {0} at ({1},{2}) to ", k - 1, x - 1, (x - 1 - (k - 1))));
          }
          y = x - k;
          int startX = x;
          int startY = y;
          //_logger.Log("({0},{1})", x, y);
          while (x < N && y < M && A[x + startA] == B[y + startB])
          {
            x += 1;
            y += 1;
          }
          //_logger.Log("\t\tFollowed snake to ({0},{1})", x, y);

          forwardDiagonal[kIndex] = x;

          if (!deltaEven && k - delta >= -D + 1 && k - delta <= D - 1)
          {
            int revKIndex = (k - delta) + HALF;
            int revX = reverseDiagonal[revKIndex];
            int revY = revX - k;
            if (revX <= x && revY <= y)
            {
              return new EditLengthResult
              {
                EditLength = 2 * D - 1,
                StartX = startX + startA,
                StartY = startY + startB,
                EndX = x + startA,
                EndY = y + startB,
                LastEdit = lastEdit
              };
            }
          }
        }

        // reverse D-path
        //_logger.Log("\n\tSearching for a reverse path");
        for (int k = -D; k <= D; k += 2)
        {
          //_logger.Log("\n\t\tSearching diagonal {0} ({1})", k, k + delta);
          int kIndex = k + HALF;
          int x, y;
          if (k == -D || (k != D && reverseDiagonal[kIndex + 1] <= reverseDiagonal[kIndex - 1]))
          {
            x = reverseDiagonal[kIndex + 1] - 1; // move left from k+1 diagonal
            lastEdit = Edit.DeleteLeft;
            //_logger.Log(String.Format("\t\tMoved left from diagonal {0} at ({1},{2}) to ", k + 1, x + 1, ((x + 1) - (k + 1 + delta))));
          }
          else
          {
            x = reverseDiagonal[kIndex - 1]; //move up from k-1 diagonal
            lastEdit = Edit.InsertUp;
            //_logger.Log(String.Format("\t\tMoved up from diagonal {0} at ({1},{2}) to ", k - 1, x, (x - (k - 1 + delta))));
          }
          y = x - (k + delta);

          int endX = x;
          int endY = y;

          //_logger.Log("({0},{1})", x, y);
          while (x > 0 && y > 0 && A[startA + x - 1] == B[startB + y - 1])
          {
            x -= 1;
            y -= 1;
          }

          //_logger.Log("\t\tFollowed snake to ({0},{1})", x, y);
          reverseDiagonal[kIndex] = x;

          if (deltaEven && k + delta >= -D && k + delta <= D)
          {
            int forKIndex = (k + delta) + HALF;
            int forX = forwardDiagonal[forKIndex];
            int forY = forX - (k + delta);
            if (forX >= x && forY >= y)
            {
              return new EditLengthResult
              {
                EditLength = 2 * D,
                StartX = x + startA,
                StartY = y + startB,
                EndX = endX + startA,
                EndY = endY + startB,
                LastEdit = lastEdit
              };
            }
          }
        }
      }

      throw new Exception("Should never get here");
    }

    protected void BuildModificationData()
    {
      int N = _oldHashedPieces.Length;
      int M = _newHashedPieces.Length;
      int MAX = M + N + 1;
      var forwardDiagonal = new int[MAX + 1];
      var reverseDiagonal = new int[MAX + 1];
      BuildModificationData(0, N, 0, M, forwardDiagonal, reverseDiagonal);
    }

    private void BuildModificationData(int startA, int endA, int startB, int endB, int[] forwardDiagonal, int[] reverseDiagonal)
    {
      while (startA < endA && startB < endB && _oldHashedPieces[startA].Equals(_newHashedPieces[startB]))
      {
        startA++;
        startB++;
      }

      while (startA < endA && startB < endB && _oldHashedPieces[endA - 1].Equals(_newHashedPieces[endB - 1]))
      {
        endA--;
        endB--;
      }

      int aLength = endA - startA;
      int bLength = endB - startB;

      if (aLength > 0 && bLength > 0)
      {
        EditLengthResult res = CalculateEditLength(_oldHashedPieces, startA, endA, _newHashedPieces, startB, endB, forwardDiagonal, reverseDiagonal);
        if (res.EditLength <= 0) return;

        if (res.LastEdit == Edit.DeleteRight && res.StartX - 1 > startA)
          _oldMods[--res.StartX] = true;
        else if (res.LastEdit == Edit.InsertDown && res.StartY - 1 > startB)
          _newMods[--res.StartY] = true;
        else if (res.LastEdit == Edit.DeleteLeft && res.EndX < endA)
          _oldMods[res.EndX++] = true;
        else if (res.LastEdit == Edit.InsertUp && res.EndY < endB)
          _newMods[res.EndY++] = true;

        BuildModificationData(startA, res.StartX, startB, res.StartY, forwardDiagonal, reverseDiagonal);

        BuildModificationData(res.EndX, endA, res.EndY, endB, forwardDiagonal, reverseDiagonal);
      }
      else if (aLength > 0)
      {
        for (int i = startA; i < endA; i++)
          _oldMods[i] = true;
      }
      else if (bLength > 0)
      {
        for (int i = startB; i < endB; i++)
          _newMods[i] = true;
      }
    }

    private void BuildOldPieceHashes(bool ignoreWhitespace, bool ignoreCase, Func<string, string[]> chunker)
    {
      var pieces = string.IsNullOrEmpty(_oldRawData)
                   ? emptyStringArray
                   : chunker(_oldRawData);

      _oldPieces = pieces;
      _oldHashedPieces = new int[pieces.Length];
      _oldMods = new bool[pieces.Length];

      for (int i = 0; i < pieces.Length; i++)
      {
        string piece = pieces[i];
        if (ignoreWhitespace) piece = piece.Trim();
        if (ignoreCase) piece = piece.ToUpperInvariant();

        if (_pieceHashes.ContainsKey(piece))
        {
          _oldHashedPieces[i] = _pieceHashes[piece];
        }
        else
        {
          _oldHashedPieces[i] = _pieceHashes.Count;
          _pieceHashes[piece] = _pieceHashes.Count;
        }
      }
    }

    private void BuildNewPieceHashes(bool ignoreWhitespace, bool ignoreCase, Func<string, string[]> chunker)
    {
      var pieces = string.IsNullOrEmpty(_newRawData)
                   ? emptyStringArray
                   : chunker(_newRawData);

      _newPieces = pieces;
      _newHashedPieces = new int[pieces.Length];
      _newMods = new bool[pieces.Length];

      for (int i = 0; i < pieces.Length; i++)
      {
        string piece = pieces[i];
        if (ignoreWhitespace) piece = piece.Trim();
        if (ignoreCase) piece = piece.ToUpperInvariant();

        if (_pieceHashes.ContainsKey(piece))
        {
          _newHashedPieces[i] = _pieceHashes[piece];
        }
        else
        {
          _newHashedPieces[i] = _pieceHashes.Count;
          _pieceHashes[piece] = _pieceHashes.Count;
        }
      }
    }

    private string Get_Report()
    {
      var sb = new StringBuilder();

      if (_oldHashedPieces == null || _oldHashedPieces.Length == 0)
        return "OLD HASHED PIECES IS NULL OR EMPTY";

      if (_newHashedPieces == null || _newHashedPieces.Length == 0)
        return "NEW HASHED PIECES IS NULL OR EMPTY";

      if (_oldMods == null || _oldMods.Length == 0)
        return "OLD MODS IS NULL OR EMPTY";

      if (_newMods == null || _newMods.Length == 0)
        return "NEW MODES IS NULL OR EMPTY";

      if (_oldMods.Length != _oldHashedPieces.Length)
        return "OLD MODS and OLD HASHED PIECES are of different lengths: " + _oldMods.Length.ToString() + " and " + _oldHashedPieces.Length.ToString() + " respectively.";

      if (_newMods.Length != _newHashedPieces.Length)
        return "NEW MODS and NEW HASHED PIECES are of different lengths: " + _newMods.Length.ToString() + " and " + _newHashedPieces.Length.ToString() + " respectively.";


      bool doneWithOld = false;
      bool doneWithNew = false;

      int oldIndex = 0;
      int newIndex = 0;

      while (!doneWithOld || !doneWithNew)
      {
        if (doneWithOld)
        {
          sb.Append(g.BlankString(30));
          if (doneWithNew)
          {
            sb.Append("  ");
            sb.Append("     ");
          }
          else
          {
            sb.Append(_newMods[newIndex] ? "M " : "  ");
            sb.Append(_newHashedPieces[newIndex].ToString("0000") + " ");
          }
          sb.Append("  ");
        }
        else
        {
          sb.Append(_oldHashedPieces[oldIndex].ToString("0000") + " ");
          sb.Append(_oldMods[oldIndex] ? "M " : "  ");
          if (doneWithNew)
          {
            sb.Append("  ");
            sb.Append("     ");
          }
          else
          {
            sb.Append(_newMods[newIndex] ? "M " : "  ");
            sb.Append(_newHashedPieces[newIndex].ToString("0000") + " ");
          }
          sb.Append("  ");
          sb.Append(_oldPieces[oldIndex].PadToLength(30));
        }

        sb.Append(" | ");

        if (doneWithNew)
        {
          sb.Append(g.BlankString(30));
        }
        else
        {
          sb.Append(_newPieces[newIndex].PadToLength(30));
        }

        oldIndex++;
        newIndex++;
        doneWithOld = oldIndex > _oldMods.Length - 1;
        doneWithNew = newIndex > _newMods.Length - 1;

        sb.Append(g.crlf);
      }

      string report = sb.ToString();
      return report;
    }
  }
}
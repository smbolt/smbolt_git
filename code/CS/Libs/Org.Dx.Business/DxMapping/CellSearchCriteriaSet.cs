using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  public class CellSearchCriteriaSet : List<CellSearchCriteria>
  {
    public bool UsesOrLogic { get; protected set; }
    public bool UsesAndLogic { get { return !this.UsesOrLogic; } }
    public string Comment { get; set; }
    public bool MatchByDefault { get; private set; }
    public Algorithm Algorithm { get; private set; }
    public bool FindFirst { get; set; }
    public bool FindLast { get; set; }
    public bool OrLastIndex { get; set; }
    public bool Random { get; set; }
    public int? MinOppositeIndex { get; set; }
    public int? MaxOppositeIndex { get; set; }
    public string Report { get { return Get_Report(); } }
    private string _rawSpec;
    private IndexType _indexType;
    private DxSearchTarget _dxSearchTarget;


    public CellSearchCriteriaSet()
    {
      Initialize();
    }

    public CellSearchCriteriaSet(string rawSpec, IndexType indexType, DxSearchTarget dxSearchTarget)
    {
      Initialize();
      _rawSpec = rawSpec;
      _indexType = indexType;
      _dxSearchTarget = dxSearchTarget;

      Parse();
    }

    public bool RowMatch(DxRow row)
    {
      try
      {
        if (!row.HasContent)
          return false;

        var consumedIndices = new List<int>();
        int minNextIndex = -1;

        bool anyMatchesFound = false;

        foreach (var cellSearchCriteria in this)
        {
          bool cellMatchesCriteria = false;

          if (cellSearchCriteria.ColIndexSpec.HasValue)
          {
            if (cellSearchCriteria.ColIndexSpec.Value >= minNextIndex && !consumedIndices.Contains(cellSearchCriteria.ColIndexSpec.Value))
            {
              if (this.InRange(cellSearchCriteria.ColIndexSpec.Value))
              {
                if (row.Cells[cellSearchCriteria.ColIndexSpec.Value] != null)
                {
                  var cell = row.Cells.Values.ElementAt(cellSearchCriteria.ColIndexSpec.Value);
                  cellMatchesCriteria = cell.MatchesCriteria(cellSearchCriteria.TextNodeSpec);

                  if (cellMatchesCriteria)
                  {
                    if (!consumedIndices.Contains(cellSearchCriteria.ColIndexSpec.Value))
                      consumedIndices.Add(cellSearchCriteria.ColIndexSpec.Value);

                    if (!this.Random && cellSearchCriteria.ColIndexSpec.Value > minNextIndex)
                      minNextIndex = cellSearchCriteria.ColIndexSpec.Value + 1;
                  }
                }
              }
            }
          }
          else
          {
            foreach (var c in row.Cells.Values)
            {
              if (c.ColumnIndex >= minNextIndex && !consumedIndices.Contains(c.ColumnIndex))
              {
                if (this.InRange(c.ColumnIndex))
                {
                  string cellValue = c.TextValue;
                  cellMatchesCriteria = c.MatchesCriteria(cellSearchCriteria.TextNodeSpec);

                  if (cellMatchesCriteria)
                  {
                    if (!consumedIndices.Contains(c.ColumnIndex))
                      consumedIndices.Add(c.ColumnIndex);

                    if (!this.Random && c.ColumnIndex > minNextIndex)
                      minNextIndex = c.ColumnIndex + 1;

                    break;
                  }
                }
              }
            }
          }

          if (cellMatchesCriteria)
            anyMatchesFound = true;

          if (this.Count == 1)
            return cellMatchesCriteria;

          if (cellMatchesCriteria && this.UsesOrLogic)
            return true;

          if (!cellMatchesCriteria && this.UsesAndLogic)
            return false;
        }

        return anyMatchesFound;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if the DxRow + '" + row.Report + "' matches " +
                            "the CellSearchCriteriaSet '" + this.Report + "'.", ex);
      }
    }

    public bool ColumnMatch(DxColumn col)
    {
      try
      {
        if (!col.HasContent)
          return false;

        var consumedIndices = new List<int>();
        int minNextIndex = -1;

        bool anyMatchesFound = false;

        foreach (var cellSearchCriteria in this)
        {
          bool cellMatchesCriteria = false;

          if (cellSearchCriteria.RowIndexSpec.HasValue)
          {
            if (cellSearchCriteria.RowIndexSpec.Value >= minNextIndex && !consumedIndices.Contains(cellSearchCriteria.RowIndexSpec.Value))
            {
              if (this.InRange(cellSearchCriteria.RowIndexSpec.Value))
              {
                if (col.Cells[cellSearchCriteria.RowIndexSpec.Value] != null)
                {
                  var cell = col.Cells.Values.ElementAt(cellSearchCriteria.RowIndexSpec.Value);
                  cellMatchesCriteria = cell.MatchesCriteria(cellSearchCriteria.TextNodeSpec);

                  if (cellMatchesCriteria)
                  {
                    if (!consumedIndices.Contains(cellSearchCriteria.RowIndexSpec.Value))
                      consumedIndices.Add(cellSearchCriteria.RowIndexSpec.Value);

                    if (!this.Random && cellSearchCriteria.RowIndexSpec.Value > minNextIndex)
                      minNextIndex = cellSearchCriteria.RowIndexSpec.Value + 1;
                  }
                }
              }
            }
          }
          else
          {
            foreach (var c in col.Cells.Values)
            {
              if (c.RowIndex >= minNextIndex && !consumedIndices.Contains(c.RowIndex))
              {

                if (this.InRange(c.RowIndex))
                {
                  string cellValue = c.TextValue;
                  cellMatchesCriteria = c.MatchesCriteria(cellSearchCriteria.TextNodeSpec);

                  if (cellMatchesCriteria)
                  {
                    if (!consumedIndices.Contains(c.RowIndex))
                      consumedIndices.Add(c.RowIndex);

                    if (!this.Random && c.RowIndex > minNextIndex)
                      minNextIndex = c.RowIndex + 1;

                    break;
                  }
                }
              }
            }
          }

          if (cellMatchesCriteria)
            anyMatchesFound = true;

          if (this.Count == 1)
            return cellMatchesCriteria;

          if (cellMatchesCriteria && this.UsesOrLogic)
            return true;

          if (!cellMatchesCriteria && this.UsesAndLogic)
            return false;
        }

        return anyMatchesFound;

      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if the DxColumn + '" + col.Report + "' matches " +
                            "the CellSearchCriteriaSet '" + this.Report + "'.", ex);
      }
    }

    private bool InRange(int index)
    {
      if (this.MinOppositeIndex.HasValue && index < this.MinOppositeIndex.Value)
        return false;

      if (this.MaxOppositeIndex.HasValue && index > this.MaxOppositeIndex.Value)
        return false;

      return true;
    }

    private void Initialize()
    {
      this.UsesOrLogic = false;
      this.Comment = String.Empty;
      this.MatchByDefault = false;
      this.Algorithm = null;
      this.FindFirst = false;
      this.FindLast = false;
      this.OrLastIndex = false;
      this.Random = false;
      this.MinOppositeIndex = null;
      this.MaxOppositeIndex = null;
      _rawSpec = String.Empty;
      _indexType = IndexType.NotUsed;
      _dxSearchTarget = DxSearchTarget.NotSet;
    }

    private void Parse()
    {
      if (_rawSpec.IsBlank())
        throw new Exception("The searchSpec is " + (this._rawSpec == null ? "null" : "blank") + ".");

      var cellSpecs = _rawSpec.Trim().Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();

      if (cellSpecs.Count == 1 && cellSpecs.First() == "*")
      {
        this.MatchByDefault = true;
        return;
      }

      foreach (var cellSpec in cellSpecs)
      {
        // peel off any cellSpecs beginning with a forward slash... 
        if (cellSpec.StartsWith("/"))
        {
          if (cellSpec.ToLower().StartsWith("/find-all:"))
          {
            string tokensToFind = cellSpec.Substring(10);
            string[] tokens = tokensToFind.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0)
              throw new Exception("The '/find-all:' cell search command does not include any tokens to find - found '" + cellSpec + "'.");

            foreach (string token in tokens)
            {
              var searchCriteria = new CellSearchCriteria(this, _dxSearchTarget, token);
              this.Add(searchCriteria);
            }
            continue;
          }

          // if a cellSpec starts with /comment, the whole spec is a comment - no parsing is done.
          if (cellSpec.ToLower().StartsWith("/comment:"))
          {
            //this.Comment = cellSpec.Substring(9).Trim();
            break;
          }

          string[] specTokens = cellSpec.Substring(1).Split(Constants.FSlashDelimiter, StringSplitOptions.RemoveEmptyEntries);
          foreach (string specToken in specTokens)
          {
            string[] token = specToken.Split(Constants.ColonDelimiter, StringSplitOptions.RemoveEmptyEntries);
            if (token.Length > 2)
              throw new Exception("The cell specification is invalid - any specification starting with '/' can only contain one or two tokens " +
                                  "delimited by a colon - found '" + specToken + "' which has " + token.Length.ToString() + " tokens.");

            switch (token[0].ToLower().Trim())
            {
              case "first":
                if (token.Length > 1)
                  throw new Exception("The '/first' cell specification cannot include a colon and second token - found '" + specToken + "'.");
                this.FindFirst = true;
                break;

              case "last":
                if (token.Length > 1)
                  throw new Exception("The '/first' cell specification cannot include a colon and second token - found '" + specToken + "'.");
                this.FindLast = true;
                break;

              case "orlastindex":
                if (token.Length > 1)
                  throw new Exception("The '/orLastIndex' cell specification cannot include a colon and second token - found '" + specToken + "'.");
                this.OrLastIndex = true;
                break;

              case "random":
                if (token.Length > 1)
                  throw new Exception("The '/random' cell specification cannot include a colon and second token - found '" + specToken + "'.");
                this.Random = true;
                break;


              case "comment":
                if (token.Length > 0)
                  this.Comment = this.Comment.IsBlank() ? token[1] : this.Comment += " " + token[1].Trim();
                break;

              case "algorithm":
                if (token.Length != 2)
                  throw new Exception("The '/algorithm' cell specification must include an algorithm specification after a colon, i.e. '/algorithm:[spec]'" +
                                      " - found '" + specToken + "'.");
                if (this.Algorithm != null)
                  throw new Exception("Multiple algorithms cannot occur with a cell specification - found '" + _rawSpec + "'.");

                this.Algorithm = new Algorithm(token[1]);

                // ensure appropriate usage of AlgorithmTypes
                if (_indexType == IndexType.RowIndex)
                {
                  if (this.Algorithm.AlgorithmType == AlgorithmType.RightMostNonBlankCell ||
                      this.Algorithm.AlgorithmType == AlgorithmType.LeftMostNonBlankCell)
                    throw new Exception("The Algorithm type '" + this.Algorithm.AlgorithmType.ToString() + "' cannot be " +
                                        "used with IndexLocators for index type '" + this._indexType.ToString() + "'.");
                }
                else
                {
                  if (this.Algorithm.AlgorithmType == AlgorithmType.TopMostNonBlankCell ||
                      this.Algorithm.AlgorithmType == AlgorithmType.BottomMostNonBlankCell)
                    throw new Exception("The Algorithm type '" + this.Algorithm.AlgorithmType.ToString() + "' cannot be " +
                                        "used with IndexLocators for index type '" + this._indexType.ToString() + "'.");
                }

                break;

              default:
                throw new Exception("The cell specification is invalid '" + specToken + "'.");
            }
          }

          continue;
        }

        var cellSearchCriteria = new CellSearchCriteria(this, _dxSearchTarget, cellSpec);
        this.Add(cellSearchCriteria);
      }

      if (this.Count > 0 && this.Algorithm != null)
        throw new Exception("The IndexLocator cannot have both an Algorithm and CellSearchCriteria items - " + this.Report + ".");

      if (this.Count == 0 && this.Algorithm == null)
        throw new Exception("There are zero CellSearchCriteria objects in the IndexLocator with value '" + this.Report + "'.");
    }

    private string Get_Report()
    {
      var sb = new StringBuilder();
      
      for (int i = 0; i < this.Count; i++)
      {
        sb.Append(i.ToString("00") + " - " + this[i].Report + g.crlf);
      }

      sb.Append("FindFirst        : " + this.FindFirst.ToString() + g.crlf +
                "FindLast         : " + this.FindLast.ToString() + g.crlf +
                "OrLastIndex      : " + this.OrLastIndex.ToString() + g.crlf +
                "Random           : " + this.Random.ToString() + g.crlf +
                "MinOppositeIndex : " + (this.MinOppositeIndex.HasValue ? this.MinOppositeIndex.Value.ToString() : "NULL") + g.crlf +
                "MaxOppositeIndex : " + (this.MaxOppositeIndex.HasValue ? this.MaxOppositeIndex.Value.ToString() : "NULL") + g.crlf +
                "Comment          : " + this.Comment);

      string report = sb.ToString();

      return report;
    }
  }


}

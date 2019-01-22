using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
 

namespace Org.Dx.Business 
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class DxFilter 
  {
    [XMap(DefaultValue = "Default")]
    public string Name { get; set; }

    [XMap(DefaultValue = "NotSet")]
    public FilterType FilterType { get; set; }

    [XMap(DefaultValue = "NotSet")]
    public FilterMethod FilterMethod { get; set; }

    [XMap]
    public string RowFilterCellValues { get; set; }

    [XMap]
    public string SheetFilterSheetNames { get; set; }

    [XMap]
    public string SheetFilterCellValues { get; set; }

    [XMap]
    public string FilterCriteria { get; set; }

    [XMap]
    public string BaseColSpec { get; set; }

    [XMap]
    public string BaseRowSpec { get; set; }

    public int? BaseColIndex { get; set; }
    public int? BaseRowIndex { get; set; }

    private string[] _types;
    public string[] Types { get { return _types; } }

    private string[] _sheetNameArray;
    public string[] SheetNameArray { get { return _sheetNameArray; } }

    private string[] _sheetFormatArray;
    public string[] SheetFormatArray { get { return _sheetFormatArray; } }

    public DxFilter()
    {
      _types = null;
    }

    public void AutoInit()
    {
      if (this.FilterCriteria.IsBlank())
        this.FilterCriteria = String.Empty;

      if (this.RowFilterCellValues.IsNotBlank())
        _types = this.RowFilterCellValues.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);
      else
        _types = new string[0];

      if (this.SheetFilterSheetNames.IsNotBlank())
        _sheetNameArray = this.SheetFilterSheetNames.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);
      else
        _sheetNameArray = new string[0]; 

      if (this.SheetFilterCellValues.IsNotBlank())
        _sheetFormatArray = this.SheetFilterCellValues.Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);
      else
        _sheetFormatArray = this.RowFilterCellValues.Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);

    }
    
    public bool MatchesWorksheet(DxRowSet dxRowSet)
    {
      try
      {
        this.BaseColIndex = EstablishBaseColumn(dxRowSet);
        this.BaseRowIndex = EstablishBaseRow(dxRowSet);

        if (this.BaseColSpec.IsNotBlank() && !this.BaseColIndex.HasValue)
          return false;

        if (this.BaseRowSpec.IsNotBlank() && !this.BaseRowIndex.HasValue)
          return false;

        if (!this.BaseColIndex.HasValue)
          this.BaseColIndex = 0;

        if (!this.BaseRowIndex.HasValue)
          this.BaseRowIndex = 0;

        var cellLocator = new DxCellLocator(this, this.FilterCriteria);

        if (cellLocator.CellSearchCriteriaSet.MatchByDefault)
          return true;

        // For tracking whether any cellSearchCriteria don't match
        bool allMatch = true;
        bool anyMatch = false;
        
        // Loop through all the cell matching criteria.
        foreach (var cellSearchCriteria in cellLocator.CellSearchCriteriaSet)
        {
          // Get one or more cells from the Worksheet (DxRowSet) as List<DxCell> based on the row and column specification.
          // These are the cells we're trying to match against.
          var cells = dxRowSet.GetCells(cellSearchCriteria.RowIndexSpec, cellSearchCriteria.ColIndexSpec);

          // When looping through the cells (there may be one, or whole rows, whole columns, etc. whatever is specified in the 
          // cellSearchCriteria object), we get a good match when any of the cells meets the criteria specified.  A true result,
          // stored in the 'match' variable, indicates that "this specific cellSearchCriteria object" is satisfied.  Don't confuse 
          // this any-match-will-do paradigm with the higher-level "cross-cellSearchCriteria AND/OR logic processing".
          bool match = false;
          foreach (var cell in cells)
          {
            match = cell.MatchesCriteria(cellSearchCriteria.TextNodeSpec);
            // any match satisfies this specific cellSearchCritieria object
            if (match)
              break;
          }
          
          // "Short Circuit" - If this criterion failed to match and we are using "AND" logic (requiring that all criteria match)
          // then there's no need to look further - we just return false.
          if (!match && cellLocator.CellSearchCriteriaSet.UsesAndLogic)
            return false;

          // "Short Circuit" - If we have a match and we're using "OR logic" at the high-level, 
          // then we've satisfied the high-level matching condition and simply return true;
          if (match && cellLocator.CellSearchCriteriaSet.UsesOrLogic)
            return true;

          // track the match for higher-level logic
          if (match)
            anyMatch = true;
          else
            allMatch = false;
        }

        if (cellLocator.CellSearchCriteriaSet.UsesAndLogic)
        {
          return allMatch;
        }
        
        // If we get here, we're looking for "any matches";
        return anyMatch;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if the DxRowSet named '" + dxRowSet.WorksheetName + "' " +
                            "matches the DxFilter named '" + this.Name + "'.", ex);
      }
    }

    private int? EstablishBaseRow(DxRowSet dxRowSet)
    {
      if (this.BaseRowSpec.IsBlank())
        return null;

      var rowIndexLocator = new IndexLocator(IndexType.RowIndex, this.BaseRowSpec);
      return dxRowSet.LocateRowIndex(rowIndexLocator);
    }

    private int? EstablishBaseColumn(DxRowSet dxRowSet)
    {
      if (this.BaseColSpec.IsBlank())
        return null;

      var columnIndexLocator = new IndexLocator(IndexType.ColumnIndex, this.BaseColSpec);
      return dxRowSet.LocateColumnIndex(columnIndexLocator);
    }

    public bool MatchesRow(DxRow row)
    {
      try
      {
        if (row == null || row.Cells.Count == 0)
          return false;

        var matchCriteria = new CellSearchCriteriaSet(this.FilterCriteria, IndexType.NotUsed, DxSearchTarget.NotSet);
        return matchCriteria.RowMatch(row);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if the row '" + row.Report + "' matches " +
                            "the DxFilter '" + /*this.Report  + */  "'.", ex);
      }
    }
  }
}

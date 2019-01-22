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
  [XMap(CollectionElements = "DxFilter", XType = XType.Element)]
  public class DxFilterSet : List<DxFilter>
  {
    public DxFilter SheetFilter { get { return this.Where(f => f.FilterType == FilterType.SheetFilter).FirstOrDefault(); } }

    public string Report { get { return Get_Report(); } }

    public void AutoInit()
    {
      if (this.Where(f => f.FilterType == FilterType.SheetFilter).Count() > 1)
        throw new Exception("Only one filter with FilterType 'SheetFilter' is allowed in a DxFilterSet.");
    }

    public bool MatchesRow(DxRow row)
    {
      try
      {
        if (this.Count == 0)
          return true;

        foreach (var dxFilter in this)
        {
          if (!dxFilter.MatchesRow(row))
            return false;
        }

        return true;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if the row + '" + row.Report + "' matches " +
                            "the DxFilterSet '" + this.Report + "'.", ex);
      }
    }


    public bool IncludeWorksheet(string wsName)
    {
      bool includeSheet = true;

      foreach (var filter in this.GetSheetFilters())
      {
        switch (filter.FilterMethod)
        {
          case FilterMethod.SheetFilterSheetNames:
            includeSheet = false;
            foreach (string filterSheetName in filter.SheetNameArray)
            {
              string sheetName = filterSheetName.Trim();
              bool negateAction = false;
              if (sheetName.StartsWith("!"))
              {
                negateAction = true;
                sheetName = sheetName.Substring(1); 
              }

              if (sheetName == "*")
              {
                if (negateAction)
                  includeSheet = false;
                else
                  includeSheet = true;
              }
              else
              {
                if (sheetName.ToLower() == wsName.ToLower())
                {
                  if (negateAction)
                    return false;
                  else
                    return true;
                }
              }
            }
            break; 

          case FilterMethod.SheetFilterCellValues:
            {
              foreach (var filterCellValue in filter.SheetFormatArray)
              {
                
              }


            }

            break; 
        }
      }

      return includeSheet;
    }

    public bool IncludeRow(DxCellSet cellSet)
    {
      try
      {
        if (cellSet == null)
          return false;

        bool includeRow = true;

        var filters = this.GetRowFilters();

        foreach (var filter in filters)
        {
          string[] filterTypes = filter.RowFilterCellValues.Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);
          switch (filter.FilterMethod)
          {
            case FilterMethod.RowFilterCellValues:

              foreach (var rowFilter in filterTypes)
              {
                string[] tokens = rowFilter.Split(Constants.CommaDelimiter);
                string compareValue = tokens[2].ToString();
                var srcCell = cellSet.Cells.Values.ElementAt(tokens[1].ToInt32());
                includeRow = srcCell.IncludeBasedOnValue(compareValue);
                if (!includeRow)
                  return includeRow;
              }

              break;
          }
        }

        return includeRow;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine whether a DxRow should be included.", ex); 
      }
    }

    public bool UseThisMap(DxMap map, DxRowSet srcRowSet)
    {
      try
      {
        foreach (var filter in this)
        {
          foreach (var filterValues in filter.SheetFormatArray)
          {
            string parmValue = String.Empty;

            string[] tokens = filterValues.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries).TrimArrayTokens();
            string compareValue = tokens[2].Trim();

            if (tokens[0].IsNotNumeric())
              throw new Exception("The specified filter '" + filterValues + "' contains a non-numeric value in the first token which is the row index.");
            int rowIndex = tokens[0].ToInt32();

            // get the row
            var row = srcRowSet.Rows.Values.ElementAt(rowIndex);

            if (tokens[1].ToInt32() > row.Cells.Count)
              return false;

            if (tokens[1].IsNotNumeric())
              throw new Exception("The specified filter '" + filterValues + "' contains a non-numeric value in the second token, which is the column index.");

            int colIndex = tokens[1].ToInt32();

            if (row.Cells.Count - 1 < colIndex)
              return false;

            // get the cell from the row
            var cell = row.Cells.ElementAt(colIndex);
            string cellValue = cell.Value.ValueOrDefault.DbToString();

            if (compareValue.Contains("@CONTAINS"))
            {
              if (cell.Value.ValueOrDefault == null)
                return false;
              parmValue = compareValue.Split('(', ')')[1];
              compareValue = compareValue.Replace("(" + parmValue + ")", "");
            }
            else
            {
              switch (compareValue)
              {
                case "@ISNOTBLANK@":
                  if (cellValue.IsBlank())
                    return false;
                  break;

                case "@ISBLANK@":
                  if (!cellValue.IsBlank())
                    return false;
                  break;

                case "@CONTAINS@":
                  if (!cellValue.Contains(parmValue))
                    return false;
                  break;

                default:
                  if (cellValue != compareValue)
                    return false;
                  break;
              }
            }
          }
        }

        return true;
      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred applying a map filter to a source row set.", ex);
      }
    }
    
    private DxFilterSet GetSheetFilters()
    {
      var filterSet = new DxFilterSet();

      foreach(var filter in this)
      {
        if (filter.FilterMethod.ToString().StartsWith("SheetFilter"))
          filterSet.Add(filter); 
      }

      return filterSet;
    }

    private DxFilterSet GetRowFilters()
    {
      var filterSet = new DxFilterSet();

      foreach(var filter in this)
      {
        if (filter.FilterMethod.ToString().StartsWith("RowFilter"))
          filterSet.Add(filter); 
      }

      return filterSet;
    }

    private string Get_Report()
    {
      return "DxFilterSet Report not yet implemented.";
    }
  }
}

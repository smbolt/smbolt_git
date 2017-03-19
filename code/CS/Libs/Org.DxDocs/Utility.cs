using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using Org.GS;
using Org.Dx.Business;
using Org.DxDocs;

namespace Org.DxDocs
{
  public class EndRowSearchParms
  {
    public string FileName { get; set; }
    public int StartColumn { get; set; }
    public int EndColumn { get; set; }
    public int StartRow { get; set; }
    public string EndRowSearchString { get; set; }
    public bool EndRowSearchCaseSensitive { get; set; }
    public int EndRowSearchColumn { get; set; }
    public int EndRowSearchStart { get; set; }
    public int EndRowSearchLimit { get; set; }

    public EndRowSearchParms()
    {
      this.FileName = String.Empty;
      this.StartColumn = 0;
      this.EndColumn = 0;
      this.StartRow = 0;
      this.EndRowSearchString = String.Empty;
      this.EndRowSearchCaseSensitive = false;
      this.EndRowSearchColumn = 0;
      this.EndRowSearchStart = 0;
      this.EndRowSearchLimit = 9999;    }
  }

  public class Utility
  {
    public static DxWorkbook GetWorkbook(ExcelExtractRequest request)
    {
      var dxWb = new DxWorkbook();

      try
      {
        using (var wb = new Workbook())
        {
          wb.LoadDocument(request.FullPath);

          dxWb.FilePath = request.FullPath;

          bool returnAllSheets = true;
          var wsList = new List<string>();
          if (request.WorksheetsToInclude != null && request.WorksheetsToInclude.Count > 0)
          {
            returnAllSheets = false;
            wsList = request.WorksheetsToInclude;
          }

          foreach (var ws in wb.Worksheets)
          {
            if (ws.Columns.LastUsedIndex == 0 && ws.Rows.LastUsedIndex == -1)
              continue;

            if (!returnAllSheets && !wsList.Contains(ws.Name))
              continue;

            int numberOfRows = ws.Rows.LastUsedIndex + 1;

            int numberOfColumns = ws.Columns.LastUsedIndex + 1;

            var dxWs = new DxWorksheet(numberOfRows, numberOfColumns);
            dxWs.LastUsedRowIndex = numberOfRows;
            dxWs.LastUsedColumnIndex = numberOfColumns;
            dxWs.WorksheetName = ws.Name;
            
            
            for(int r = 0; r < numberOfRows; r ++)
            {
              for(int c = 0; c < numberOfColumns; c++)
              {
                var cell = ws.Cells[r, c];

                var dxCell = new DxCell();

                if (cell.Value.IsDateTime)
                  dxCell.RawValue = cell.Value.DateTimeValue;
                else
                  if (cell.Value.IsNumeric)
                    dxCell.RawValue = cell.Value.NumericValue;
                  else
                    if (cell.Value.IsBoolean)
                      dxCell.RawValue = cell.Value.BooleanValue;
                    else
                      if (cell.Value.IsEmpty)
                        dxCell.RawValue = null;
                      else
                        dxCell.RawValue = cell.Value.TextValue;

                dxCell.RowIndex = cell.RowIndex;
                dxCell.ColumnIndex = cell.ColumnIndex;
                dxCell.IsBoolean = cell.Value.IsBoolean;
                dxCell.IsDateTime = cell.Value.IsDateTime;
                dxCell.IsEmpty = cell.Value.IsEmpty;
                dxCell.IsNumeric = cell.Value.IsNumeric;
                dxCell.IsText = cell.Value.IsText;

                dxWs.DxCells[r, c] = dxCell;
              }              
            }
           
            if (dxWb.ContainsKey(dxWs.WorksheetName))
              throw new Exception("The DxWorkbook already contains a worksheet named '" + dxWs.WorksheetName + "'."); 
            
            dxWb.Add(dxWs.WorksheetName, dxWs);
          }
        }
        return dxWb;
      }

      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to extract statement data from the Excel file.", ex);
      }
    }

    public static List<GridCellSet> GetGridCellSetList(EndRowSearchParms parms)
    {
      var gridCellSetList = new List<GridCellSet>();

      try
      {
        DateTime _holdProductionDate = DateTime.MinValue;

        using (var wb = new Workbook())
        {
          wb.LoadDocument(parms.FileName);

          foreach (var ws in wb.Worksheets)
          {
            int lastColumnUsed = ws.Columns.LastUsedIndex;
            int lastRowUsed = ws.Rows.LastUsedIndex;

            if (lastColumnUsed == 0 && lastRowUsed == -1)
              continue;

            var cell = ws.Cells[2, 0];
            var value = cell.Value;
            var stringValue = cell.Value.ToString();
            DateTime dateValue = cell.Value.DateTimeValue;

            int endRow = 0;

            endRow = Utility.FindRowByColValue(ws, parms.EndRowSearchColumn, parms.EndRowSearchString, parms.EndRowSearchCaseSensitive, parms.EndRowSearchStart, parms.EndRowSearchLimit);
            if (endRow == -1)
              throw new Exception("Unable to locate the vertical extent of the worksheet.");

            int rows = endRow - parms.StartRow;
            int cols = parms.EndColumn - parms.StartColumn + 1;

            var gc = new GridCellSet(ws, rows, cols, parms.StartRow, parms.StartColumn);

            gridCellSetList.Add(gc);
          }
        }

        return gridCellSetList;
      }

      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to extract statement data from the Excel file '" + parms.FileName + "'.", ex);
      }
    }


    public static int FindRowByColValue(Worksheet ws, int col, string value, bool caseSensitive, int rowStart, int rowLimit)
    {
      int row = rowStart;

      if (!caseSensitive)
        value = value.ToLower().Trim();
      else
        value = value.Trim();


      while (true)
      {
        string cellValue = ws.Cells[row, col].Value.ToString().Trim();
        if (caseSensitive)
        {
          if (cellValue == value)
            return row;
        }
        else
        {
          if (cellValue.ToLower() == value)
            return row;
        }

        row++;
        if (row > rowLimit)
          return -1;
      }
    }
  }
}

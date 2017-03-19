using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;
namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "DxWorksheet", XType = XType.Element)] 
  public class DxWorksheet : DxRowSet
  {
    [XMap(IsKey = true)]
    public string WorksheetName { get; set; }

    [XMap]
    public int LastUsedRowIndex { get; set; }

    [XMap]
    public int LastUsedColumnIndex { get; set; }

    public DxCell[,] DxCells { get; set; }

    public int Rows { get { return Get_RowCount(); } }
    public int LastRowIndex { get { return Get_LastRowIndex(); } }
    public int Cols { get { return Get_ColCount(); } }
    public int LastColIndex { get { return Get_LastColIndex(); } }


    [XMap(XType = XType.Element)]
    public XElement DxCellArray
    {
      get { return Get_DxCells(); }
      set { Set_DxCells(value); }
    }

    public DxWorksheet() {}

    public DxWorksheet(int rows, int cols)
    {
      this.WorksheetName = String.Empty;
      this.LastUsedRowIndex = 0;
      this.LastUsedColumnIndex = 0;
      this.DxCells = new DxCell[rows, cols];
    }

    public int FindFirstColWithValue(string value, int startCol, int endCol, int row, bool caseSensitive)
    {
      if (endCol > this.DxCells.GetLength(1))
        endCol = this.DxCells.GetLength(1); 

      for (int c = startCol; c < endCol; c++)
      {
        string text = this.DxCells[row, c].TextValue;

        if (caseSensitive)
        {
          if (text == value)
            return c;
				}
        else
        {
          if (text.ToLower() == value.ToLower())
            return c;
        }        
      }

      return -1;
    }

    public DxCell FindFirstColWithValue(int rowIndex, DxSearchCriteria searchCriteria)
    {      
      if (searchCriteria.StartIndex == -1)
        searchCriteria.StartIndex = 0;

      if (searchCriteria.EndIndex == -1)
        searchCriteria.EndIndex = this.LastRowIndex;

      var row = this.GetRow(rowIndex); 

      foreach (var cell in row.Values)
      {
        if (cell.Match(searchCriteria))
          return cell;
      }

      return null;
    }

    public int FindFirstRowWithValue(string value, int startRow, int endRow, int col, bool caseSensitive)
    {
      if (endRow > this.DxCells.GetLength(0))
        endRow = this.DxCells.GetLength(0); 

      for (int r = startRow; r < endRow; r++)
      {
        string text = this.DxCells[r, col].TextValue;

        if (caseSensitive)
        {
          if (text == value)
            return r;
				}
        else
        {
          if (text.ToLower() == value.ToLower())
            return r;        
        }
      }

      return -1;
    }

    public DxCell FindFirstCellInColumn(int colIndex, DxSearchCriteria searchCriteria)
    {
      if (searchCriteria.StartIndex == -1)
        searchCriteria.StartIndex = 0;

      if (searchCriteria.EndIndex == -1)
        searchCriteria.EndIndex = this.LastRowIndex;

      var col = this.GetColumn(colIndex);

      foreach (var cell in col.Values)
      {
        if (cell.Match(searchCriteria))
          return cell;
      }

      return null;
    }

    public DxCell FindLastCellInColumn(int colIndex, DxSearchCriteria searchCriteria)
    {
      if (searchCriteria.StartIndex == -1)
        searchCriteria.StartIndex = 0;

      if (searchCriteria.EndIndex == -1)
        searchCriteria.EndIndex = this.LastRowIndex;

      DxCell lastCell = null;

      var col = this.GetColumn(colIndex);

      foreach (var cell in col.Values)
      {
        if (cell.Match(searchCriteria))
          lastCell = cell;
      }

      return lastCell;
    }

    public int FindLastRowWithValue(string value, int startRow, int endRow, int col, bool caseSensitive)
    {
      int row = -1;

      for (int r = startRow; r < endRow; r++)
      {
        string text = this.DxCells[r, col].TextValue;

        if (caseSensitive)
        {
          if (text == value)
            row = r;

          else
            if (text.ToLower() == value.ToLower())
              row = r;
        }
      }

      return row;
    }

    public int FindRowWithValue(string value, int startRow, int col)
    {
      int row = -1;

      for (int r = startRow; r < this.LastRowIndex; r++)
      {
        string text = this.DxCells[r, col].TextValue;
        
        if (text == value)
        {
          row = r;
          break;
        }
      }

      return row;
    }

    public DxIndexSet GetRowIndexSet(int col, DxComparisonType comparisionType, DxTextCase textCase, string matchValue, int startRow, int endRow)
    {
      var indexSet = new DxIndexSet();

      if (endRow == -1)
        endRow = this.LastRowIndex;

      if (endRow > this.LastRowIndex)
        endRow = this.LastRowIndex;

      matchValue = matchValue.Trim();
      if (textCase == DxTextCase.NotCaseSensitive)
        matchValue = matchValue.ToLower();


      for (int r = startRow; r < endRow; r++)
      {
        var cell = this.DxCells[r, col];
        if (cell != null)
        {
          string cellTextValue = cell.TextValue.Trim();
          string cellCompareValue = cellTextValue;
          if (textCase == DxTextCase.NotCaseSensitive)
            cellCompareValue = cellTextValue.ToLower();

          switch (comparisionType)
          {

            case DxComparisonType.StartsWith:
              if (cellCompareValue.StartsWith(matchValue))
                indexSet.Add(r);
              break;

            case DxComparisonType.EndsWith:
              if (cellCompareValue.EndsWith(matchValue))
                indexSet.Add(r);
              break;

            case DxComparisonType.Contains:
              if (cellCompareValue.Contains(matchValue))
                indexSet.Add(r);
              break;

            default:
              if (cellCompareValue == matchValue)
                indexSet.Add(r);
              break;
          }
        }
      }

      return indexSet;
    }


		public DxIndexSet GetColIndexSet(int row, DxComparisonType comparisionType, DxTextCase textCase, string matchValue, int startCol, int endCol)
		{
			var indexSet = new DxIndexSet();

			if (endCol == -1)
				endCol = this.LastColIndex;

			if (endCol > this.LastColIndex)
				endCol = this.LastColIndex;

			matchValue = matchValue.Trim();
			if (textCase == DxTextCase.NotCaseSensitive)
				matchValue = matchValue.ToLower();


			for (int c = startCol; c < endCol; c++)
			{
				var cell = this.DxCells[row, c];
				if (cell != null)
				{
					string cellTextValue = cell.TextValue.Trim();
					string cellCompareValue = cellTextValue;
					if (textCase == DxTextCase.NotCaseSensitive)
						cellCompareValue = cellTextValue.ToLower();

					switch (comparisionType)
					{

						case DxComparisonType.StartsWith:
							if (cellCompareValue.StartsWith(matchValue))
								indexSet.Add(c);
							break;

						case DxComparisonType.EndsWith:
							if (cellCompareValue.EndsWith(matchValue))
								indexSet.Add(c);
							break;

						case DxComparisonType.Contains:
							if (cellCompareValue.Contains(matchValue))
								indexSet.Add(c);
							break;

						default:
							if (cellCompareValue == matchValue)
								indexSet.Add(c);
							break;
					}
				}
			}

			return indexSet;
		}


    public int Get_RowCount()
    {
      if (this.DxCells == null)
        return 0;

      return this.DxCells.GetLength(0);
    }

    public int Get_LastRowIndex()
    {
      int rowCount = Get_RowCount();
      if (rowCount > 0)
        return rowCount - 1;
      return -1;
    }

    public int Get_ColCount()
    {
      if (this.DxCells == null)
        return 0;

      return this.DxCells.GetLength(1);
    }

    public int Get_LastColIndex()
    {
      int colCount = Get_ColCount();
      if (colCount > 0)
        return colCount - 1;
      return -1;
    }

    public DxRow GetRow(int rowIndex)
    {
      return this.GetRow(rowIndex, 0, this.LastColIndex);
    }

    public DxRow GetRow(int rowIndex, int startColumn)
    {
      return this.GetRow(rowIndex, startColumn, this.LastColIndex);
    }

    public DxRow GetRow(int rowIndex, int startColumn, int endColumn)
    {
      var row = new DxRow();

      if (rowIndex < 0 || rowIndex > this.LastRowIndex)
        return row;

      if (startColumn < 0)
        startColumn = 0;

      if (endColumn > this.LastColIndex)
        endColumn = this.LastColIndex;

      if (endColumn < startColumn)
        return row;

      for (int c = startColumn; c < endColumn + 1; c++)
      {
        var cell = this.DxCells[rowIndex, c];
        if (!row.ContainsKey(cell.ColumnIndex))
          row.Add(cell.ColumnIndex, cell);
      }

      return row;
    }

    public DxRow GetRow(int rowIndex, DxIndexSet colIndices)
    {
      var allRows = this.GetRow(rowIndex);

      var rows = new DxRow();

      foreach (var kvpRow in allRows)
      {
        if (kvpRow.Key.In(colIndices))
          rows.Add(kvpRow.Value.ColumnIndex, kvpRow.Value);
      }

      return rows;
    }

    public DxColumn GetColumn(int colIndex)
    {
      return GetColumn(colIndex, 0, this.LastRowIndex);
    }

    public DxColumn GetColumn(int colIndex, int startRow)
    {
      return GetColumn(colIndex, startRow, this.LastRowIndex);
    }

    public DxColumn GetColumn(int colIndex, int startRow, int endRow)
    {
      var col = new DxColumn();

      if (colIndex < 0 || colIndex > this.LastColIndex)
        return col;

      if (startRow < 0)
        startRow = 0;

      if (endRow > this.LastRowIndex)
        endRow = this.LastRowIndex;

      if (endRow < startRow)
        return col;

      for (int r = startRow; r < endRow; r++)
      {
        var cell = this.DxCells[r, colIndex];
        if (!col.ContainsKey(cell.RowIndex))
          col.Add(cell.RowIndex, cell);
      }

      return col;
    }

    public DxColumn GetCol(int colIndex, DxIndexSet rowIndices)
    {
      var allCols = this.GetColumn(colIndex);

      var col = new DxColumn();

      foreach (var kvpCol in allCols)
      {
        if (kvpCol.Key.In(rowIndices))
          col.Add(kvpCol.Value.ColumnIndex, kvpCol.Value);
      }

      return col;
    }


    public string GetGrid(int cellWidth, int cellSpacing)
    {

      if (this.DxCells == null)
        return "DxCells is null";

      if (this.DxCells.Length == 0)
        return "DxCells contains no cells";

      StringBuilder sb = new StringBuilder();
      
      int rowCount = this.DxCells.GetLength(0);
      int colCount = this.DxCells.GetLength(1);

      for (int c = 0; c < colCount; c++)
      {
        if (c == 0)
          sb.Append("ROW#    ");
        sb.Append("Col-" + c.ToString("000").PadTo(cellWidth));
      }
      sb.Append(g.crlf); 

      for (int r = 0; r < rowCount; r++)
      {
        for (int c = 0; c < colCount; c++)
        {
          if (c == 0)
            sb.Append(r.ToString("00000") + "   "); 

          string cellText = this.DxCells[r,c].TextValue;
          sb.Append(cellText.PadTo(cellWidth) + g.BlankString(cellSpacing)); 
        }
        sb.Append(g.crlf); 
      }

      string grid = sb.ToString();
      return grid;
    }

    private XElement Get_DxCells()
    {
      var dxCells = new XElement("DxCellArray");
      dxCells.Add(new XAttribute("Columns", (this.LastUsedColumnIndex).ToString()));
      dxCells.Add(new XAttribute("Rows", (this.LastUsedRowIndex).ToString()));

      for (int r = 0; r < this.LastUsedRowIndex; r++)
      {
        for (int c = 0; c < this.LastUsedColumnIndex; c++)
        {
          var cell = this.DxCells[r, c];
          // Only store the xml if there is a value for the cell
          if (cell.RawValue != null)
          {
            var dxCell = new XElement("DxCell");
            dxCell.Add(new XAttribute("R", cell.RowIndex.ToString()));
            dxCell.Add(new XAttribute("C", cell.ColumnIndex.ToString()));
            dxCell.Add(new XAttribute("Types", cell.IsBoolean.ToString().Substring(0, 1) + cell.IsDateTime.ToString().Substring(0, 1) +
                                              cell.IsEmpty.ToString().Substring(0, 1) + cell.IsNumeric.ToString().Substring(0, 1) +
                                              cell.IsText.ToString().Substring(0, 1)));

            string rawValue = "@NULL@";
            if (cell.RawValue != null)
              rawValue = cell.RawValue.ToString();
            dxCell.Add(new XAttribute("RawValue", rawValue.ToString()));
            dxCells.Add(dxCell);
          }
        }
      }

      return dxCells; 
    }

    private void Set_DxCells(XElement dxCellArray)
    {
      int rows = dxCellArray.Attribute("Rows").Value.ToInt32();
      int cols = dxCellArray.Attribute("Columns").Value.ToInt32();

      this.DxCells = new DxCell[rows, cols];

      foreach (var cell in dxCellArray.Elements("DxCell"))
      {
        DxCell dxCell = new DxCell();
        dxCell.RowIndex = cell.Attribute("R").Value.ToInt32();
        dxCell.ColumnIndex = cell.Attribute("C").Value.ToInt32();
        dxCell.RawValue = cell.Attribute("RawValue").Value.Trim();
        if (dxCell.RawValue.ToString() == "@NULL@")
          dxCell.RawValue = null;
        string types = cell.Attribute("Types").Value;
        dxCell.IsBoolean = types.Substring(0, 1) == "T";
        dxCell.IsDateTime = types.Substring(1, 1) == "T";
        dxCell.IsEmpty = types.Substring(2, 1) == "T";
        dxCell.IsNumeric = types.Substring(3, 1) == "T";
        dxCell.IsText = types.Substring(4, 1) == "T";
        this.DxCells[dxCell.RowIndex, dxCell.ColumnIndex] = dxCell; 
      }

      // fill in any cells for which XML was not produced
      for (int r = 0; r < rows; r++)
      {
        for (int c = 0; c < cols; c++)
        {
          if (this.DxCells[r, c] == null)
          {
            var dxCell = new DxCell();
            dxCell.RowIndex = r;
            dxCell.ColumnIndex = c;
            dxCell.RawValue = null;
            dxCell.IsEmpty = true;
            this.DxCells[r, c] = dxCell;
          }
        }
      }

      RefreshDxRows();
    }

    private void RefreshDxRows()
    {
      this.Clear();

      for (int r = 0; r < this.DxCells.GetLength(0); r++)
      {
        var row = new DxRow();
        for (int c = 0; c < this.DxCells.GetLength(1); c++)
        {
          row.Add(c, this.DxCells[r, c]);
        }
        this.Add(r, row); 
      }
    }
  }
}

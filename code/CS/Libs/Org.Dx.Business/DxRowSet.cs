using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class DxRowSet : SortedList<int, DxRow>
  {
    public int LastRowIndex { get { return Get_LastRowIndex(); } }
    public int LastColIndex { get { return Get_LastColIndex(); } }
    public DxCell[,] DxCells { get; set; }

    public DxRowSet GetRowSetEndingWithColumnValue(int colIndex, DxSearchCriteria searchCriteria)
    {
      if (this.Count == 0)
        return null;

      if (searchCriteria.StartIndex >= this.Count)
        return null;

      if (colIndex >= this.First().Value.Count)
        return null;

      bool endingValueFound = false;


      var rs = new DxRowSet();

      for (int i = searchCriteria.StartIndex; i < this.Count; i++)
      {
        var row = this.ElementAt(i);
        var cell = row.Value.ElementAt(colIndex).Value;
        rs.Add(row.Key, row.Value);

        if (cell.Match(searchCriteria))
        {
          endingValueFound = true;
          break;
        }
      }    

      if (rs.Count == 0 || !endingValueFound)
        return null;

      return rs;
    }

    public DxRow FindFirstRow2 (int colIndex, DxSearchCriteria searchCriteria)
    {
      if (this.Count == 0)
        return null;

      if(searchCriteria.StartIndex == -1)
        searchCriteria.StartIndex = 0;

      if (colIndex >= this.First().Value.Count)
        return null;
      
      var r = new DxRow();

      for(int i = searchCriteria.StartIndex; i < this.Count; i++)
      {
        var row = this.ElementAt(i);
        var cell = row.Value.ElementAt(colIndex).Value;
        if(cell.Match(searchCriteria))
        {
          r.Add(row.Key, cell);
          return r;
        }
      }

      return null;
    }

    public DxRowSet FilterRowSetByColumnFunction(DxIndexSet indexSet, Func<DxCell, bool> func, bool requireAll)
    {
      if (this.Count == 0)
        return null;

      var rs = new DxRowSet();


      foreach (var rowKvp in this)
      {
        int trueCount = 0;
        var row = rowKvp.Value;
        foreach (var index in indexSet)
        {
          DxCell cell = row.Values.ElementAt(index);
          if (func(cell))
            trueCount++;
        }

        if (requireAll)
        {
          if (trueCount == indexSet.Count)
            rs.Add(rowKvp.Key, row);
        }
        else
        {
          if (trueCount > 0)
            rs.Add(rowKvp.Key, row);
        }
      }

      if (rs.Count == 0)
        return null;

      return rs;
    }

    public DxColumn GetColumn(int colIndex)
    {
      return GetColumn(colIndex, 0, this.LastRowIndex);
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

    public int Get_LastRowIndex()
    {
      int rowCount = Get_RowCount();
      if (rowCount > 0)
        return rowCount - 1;
      return -1;
    }

    public int Get_RowCount()
    {
      if (this.DxCells == null)
        return 0;

      return this.DxCells.GetLength(0);
    }

    public int Get_LastColIndex()
    {
      int colCount = Get_ColCount();
      if (colCount > 0)
        return colCount - 1;
      return -1;
    }

    public int Get_ColCount()
    {
      if (this.DxCells == null)
        return 0;

      return this.DxCells.GetLength(1);
    }

    public DxRow GetRow(int rowIndex)
    {
      return this.GetRow(rowIndex, 0, this.LastColIndex);
    }

    public DxRow GetRow(int rowIndex, int startColumn, int endColumn)
    {
      var row = new DxRow();

      if (rowIndex < 0 || rowIndex > endColumn)
        return row;

      if (startColumn < 0)
        startColumn = 0;

      if (endColumn == -1)
        return row;

      if (endColumn < startColumn)
        return row;

      for (int c = startColumn; c < endColumn; c++)
      {
        var cell = this.DxCells[rowIndex, c];
        if (!row.ContainsKey(cell.ColumnIndex))
          row.Add(cell.ColumnIndex, cell);
      }

      return row;
    }

    public DxRow GetRowWithValue(int rowIndex, int startColumn, int endColumn)
    {
      var row = new DxRow();

      if (rowIndex < 0 || rowIndex > endColumn)
        return row;

      if (startColumn < 0)
        startColumn = 0;

      if (endColumn == -1)
        return row;

      if (endColumn < startColumn)
        return row;

      for (int c = startColumn; c < endColumn; c++)
      {
        var cell = this.DxCells[rowIndex, c];
        if (!row.ContainsKey(cell.ColumnIndex))
          row.Add(cell.ColumnIndex, cell);
      }

      return row;
    }


  }
}

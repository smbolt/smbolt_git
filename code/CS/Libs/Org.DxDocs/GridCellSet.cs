using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using Org.GS;

namespace Org.DxDocs
{
  public class GridCellSet
  {
    private Worksheet _ws;
    public int RowCount {
      get;
      private set;
    }
    public int ColumnCount {
      get;
      private set;
    }
    private int _startRow;
    private int _endRow;
    private int _startCol;
    private int _endCol;

    public List<object[]> Rows
    {
      get {
        return Get_Rows();
      }
    }

    private object[,] _cells = null;

    public GridCellSet(Worksheet ws, int rows, int cols, int startRow, int startCol)
    {
      _ws = ws;
      this.RowCount = rows;
      this.ColumnCount = cols;
      _startRow = startRow;
      _startCol = startCol;
      _endRow = startRow + rows;
      _endCol = startCol + cols;

      _cells = new object[rows, cols];
      LoadValues();
    }

    public GridCell Cell(int row, int col)
    {
      if (this.RowCount <= row || this.ColumnCount <= col)
        return null;

      return new GridCell(row, col, _cells[row, col]);
    }

    private List<object[]> Get_Rows()
    {
      var rows = new List<object[]>();

      for (int r = 0; r < RowCount; r++)
      {
        var row = new object[this.ColumnCount];
        for (int c = 0; c < ColumnCount; c++)
        {
          row[c] = _cells[r, c];
        }
        rows.Add(row);
      }

      return rows;
    }

    private void LoadValues()
    {
      int gridRow = 0;
      int gridCol = 0;

      for (int r = _startRow; r < _endRow; r++)
      {
        for (int c = _startCol; c < _endCol; c++)
        {
          gridRow = r - _startRow;
          gridCol = c - _startCol;

          string value = _ws.Cells[r, c].Value.ToString().Trim();
          _cells[gridRow, gridCol] = value;
        }
      }
    }

    public int FindRowByColValue(int col, string value, bool caseSensitive, int rowStart, int rowLimit)
    {
      int row = rowStart;

      if (!caseSensitive)
        value = value.ToLower().Trim();
      else
        value = value.Trim();


      while (true)
      {
        string cellValue = this.Cell(row, col).Value.ToString().Trim();
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.Dx.Business.TextProcessing;
using Org.GS;

namespace Org.Dx.Business
{
  public class DxRowSet : Node
  {
    private SortedList<int, DxRow> _rows;
    public SortedList<int, DxRow> Rows
    {
      get
      {
        if (_rows == null)
          _rows = new SortedList<int, DxRow>();
        return _rows;
      }
    }

    private DxCell[,] _dxCells;
    public DxCell[,] DxCells
    {
      get
      {
        return _dxCells;
      }
      set
      {
        _dxCells = value;
      }
    }

    public List<DxCell> CellList {
      get {
        return Get_CellList();
      }
    }

    private DxColumnSet _dxColumnSet;
    public DxColumnSet DxColumnSet {
      get {
        return _dxColumnSet != null ? _dxColumnSet : Get_DxColumnSet();
      }
    }

    private int? _lastUsedRowIndex;
    public int LastUsedRowIndex
    {
      get {
        return Get_LastUsedRowIndex();
      }
      set {
        _lastUsedRowIndex = value;
      }
    }

    private int? _lastUsedColumnIndex;
    public int LastUsedColumnIndex
    {
      get {
        return Get_LastUsedColumnIndex();
      }
      set {
        _lastUsedColumnIndex = value;
      }
    }

    private int? _rowCount;
    public int RowCount
    {
      get {
        return Get_RowCount();
      }
      set {
        _rowCount = value;
      }
    }

    private int? _columnCount;
    public int ColumnCount
    {
      get {
        return Get_ColumnCount();
      }
      set {
        _columnCount = value;
      }
    }

    public virtual bool IsHidden {
      get;
      set;
    }

    public string WorksheetName {
      get;
      set;
    }
    public int SheetIndex {
      get {
        return Get_SheetIndex();
      }
    }

    public string Report {
      get {
        return Get_Report();
      }
    }

    public DxWorkbook DxWorkbook {
      get;
      set;
    }

    public string CellIndexReport {
      get {
        return Get_CellIndexReport();
      }
    }

    public DxRowSet()
    {
      this.DxObject = this;
      _rows = new SortedList<int, DxRow>();
    }

    protected void Initialize()
    {
      try
      {
        _dxColumnSet = null;

        if (this.Rows.Count > 0)
          return;

        var rowCount = this.DxCells.GetLength(0);
        var colCount = this.DxCells.GetLength(1);

        for (int r = 0; r < rowCount; r++)
        {
          for (int c = 0; c < colCount; c++)
          {
            var cell = this.DxCells[r, c];
            if (cell == null)
            {
              if (!this.Rows.ContainsKey(r))
              {
                this.Rows.Add(r, new DxRow(this));
              }
              this.Rows[r].Add(c, new DxCell());
            }
            else
            {
              if (!this.Rows.ContainsKey(r))
              {
                var dxRow = new DxRow(this);
                dxRow.SetRowIndex(cell.RowIndex);
                this.Rows.Add(r, dxRow);
              }
              this.Rows[r].Add(c, cell);
            }

            if (!this.Rows.ContainsKey(r))
            {
              this.Rows.Add(r, new DxRow(this));
            }
          }
        }

        this.EnsureParentage();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to initialize the worksheet (map DxCells into the RowSet).", ex);
      }
    }

    public DxRowSet GetRows(string value, int columnIndex = 0, bool caseSensitive = false)
    {
      var rowSet = new DxRowSet();

      foreach (var row in this.Rows.Values)
      {
        if (row.Cells.ContainsKey(columnIndex))
        {
          var dxCell = row.Cells[columnIndex];

          object valueObject = dxCell.ValueOrDefault;
          string cellValue = valueObject != null ? valueObject.ToString() : String.Empty;

          if (cellValue.Contains("[") && cellValue.Contains("]"))
            cellValue = cellValue.GetTextAfter(Constants.CloseBracket);

          if (cellValue.IsBlank())
            continue;

          if (caseSensitive)
          {
            if (value.Trim() == cellValue.Trim())
              rowSet.Rows.Add(row.RowIndex, row);
          }
          else
          {
            if (value.ToLower().Trim() == cellValue.ToLower().Trim())
              rowSet.Rows.Add(row.RowIndex, row);
          }
        }
      }

      rowSet.EnsureParentage();
      return rowSet;
    }

    public void SetCountsAndCollections()
    {
      this.LastUsedRowIndex = Get_LastUsedRowIndex();
      this.LastUsedColumnIndex = Get_LastUsedColumnIndex();
    }

    public int Get_LastUsedRowIndex()
    {
      if (_lastUsedRowIndex.HasValue)
        return _lastUsedRowIndex.Value;

      int lastUsedRowIndex = -1;
      int lastUsedColumnIndex = -1;

      foreach (var dxRow in this.Rows.Values)
      {
        if (dxRow.LastUsedColumnIndex > lastUsedColumnIndex)
          lastUsedColumnIndex = dxRow.LastUsedColumnIndex;

        if (dxRow.RowIndex > lastUsedRowIndex)
          lastUsedRowIndex = dxRow.RowIndex;
      }

      _lastUsedRowIndex = lastUsedRowIndex;
      _lastUsedColumnIndex = lastUsedColumnIndex;

      return lastUsedRowIndex;
    }

    public int Get_LastUsedColumnIndex()
    {
      if (_lastUsedColumnIndex.HasValue)
        return _lastUsedColumnIndex.Value;

      int lastUsedRowIndex = -1;
      int lastUsedColumnIndex = -1;

      foreach (var dxRow in this.Rows.Values)
      {
        if (dxRow.LastUsedColumnIndex > lastUsedColumnIndex)
          lastUsedColumnIndex = dxRow.LastUsedColumnIndex;

        if (dxRow.RowIndex > lastUsedRowIndex)
          lastUsedRowIndex = dxRow.RowIndex;
      }

      _lastUsedRowIndex = lastUsedRowIndex;
      _lastUsedColumnIndex = lastUsedColumnIndex;

      return lastUsedColumnIndex;
    }

    public int Get_ColumnCount()
    {
      if (_columnCount.HasValue)
        return _columnCount.Value;

      List<int> colIndices = new List<int>();

      foreach (var dxRow in this.Rows.Values)
      {
        foreach (var dxCell in dxRow.Cells.Values)
        {
          if (!colIndices.Contains(dxCell.ColumnIndex))
            colIndices.Add(dxCell.ColumnIndex);
        }
      }

      _columnCount = colIndices.Count;
      return colIndices.Count;
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
        searchCriteria.EndIndex = this.LastUsedRowIndex;

      var row = this.GetRow(rowIndex);

      foreach (var cell in row.Cells.Values)
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
        searchCriteria.EndIndex = this.LastUsedRowIndex;

      var col = this.GetColumn(colIndex);

      foreach (var cell in col.Cells.Values)
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
        searchCriteria.EndIndex = this.LastUsedRowIndex;

      DxCell lastCell = null;

      var col = this.GetColumn(colIndex);

      foreach (var cell in col.Cells.Values)
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

          else if (text.ToLower() == value.ToLower())
            row = r;
        }
      }

      return row;
    }

    public int FindRowWithValue(string value, int startRow, int col)
    {
      int row = -1;

      int lastUsedRowIndex = this.LastUsedRowIndex;

      for (int r = startRow; r < lastUsedRowIndex; r++)
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

    public DxRowSet GetRowSetEndingWithColumnValue(int colIndex, DxSearchCriteria searchCriteria)
    {
      if (this.Rows.Count == 0)
        return null;

      if (searchCriteria.StartIndex >= this.Rows.Count)
        return null;

      if (colIndex >= this.Rows.First().Value.Cells.Count)
        return null;

      bool endingValueFound = false;

      var rs = new DxRowSet();

      for (int i = searchCriteria.StartIndex; i < this.Rows.Count; i++)
      {
        var row = this.Rows.ElementAt(i);
        var cell = row.Value.Cells.ElementAt(colIndex).Value;
        rs.Rows.Add(row.Key, row.Value);

        if (cell.Match(searchCriteria))
        {
          endingValueFound = true;
          break;
        }
      }

      if (rs.Rows.Count == 0 || !endingValueFound)
        return null;

      rs.EnsureParentage();
      return rs;
    }

    public DxRow FindFirstRow2(int colIndex, DxSearchCriteria searchCriteria)
    {
      if (this.Rows.Count == 0)
        return null;

      if (searchCriteria.StartIndex == -1)
        searchCriteria.StartIndex = 0;

      if (colIndex >= this.Rows.First().Value.Cells.Count)
        return null;

      var r = new DxRow(this);

      for (int i = searchCriteria.StartIndex; i < this.Rows.Count; i++)
      {
        var row = this.Rows.ElementAt(i);
        var cell = row.Value.Cells.ElementAt(colIndex).Value;
        if (cell.Match(searchCriteria))
        {
          r.Add(row.Key, cell);
          return r;
        }
      }

      return null;
    }

    public DxRowSet FilterRowSetByColumnFunction(DxIndexSet indexSet, Func<DxCell, bool> func, bool requireAll)
    {
      if (this.Rows.Count == 0)
        return null;

      var rs = new DxRowSet();

      foreach (var rowKvp in this.Rows)
      {
        int trueCount = 0;
        var row = rowKvp.Value;
        foreach (var index in indexSet)
        {
          DxCell cell = row.Cells.Values.ElementAt(index);
          if (func(cell))
            trueCount++;
        }

        if (requireAll)
        {
          if (trueCount == indexSet.Count)
            rs.Rows.Add(rowKvp.Key, row);
        }
        else
        {
          if (trueCount > 0)
            rs.Rows.Add(rowKvp.Key, row);
        }
      }

      if (rs.Rows.Count == 0)
        return null;

      rs.EnsureParentage();

      return rs;
    }

    public DxColumn GetColumn(int colIndex)
    {
      return GetColumn(colIndex, 0, this.LastUsedRowIndex);
    }

    public DxColumn GetColumn(int colIndex, int startRow)
    {
      return GetColumn(colIndex, startRow, this.LastUsedRowIndex);
    }

    public DxColumn GetColumn(int colIndex, int startRow, int endRow)
    {
      var col = new DxColumn();

      if (colIndex < 0 || colIndex > this.LastUsedColumnIndex)
        return col;

      if (startRow < 0)
        startRow = 0;

      if (endRow > this.LastUsedRowIndex)
        endRow = this.LastUsedRowIndex;

      if (endRow < startRow)
        return col;

      for (int r = startRow; r < endRow; r++)
      {
        var cell = this.DxCells[r, colIndex];
        if (!col.Cells.ContainsKey(cell.RowIndex))
          col.Cells.Add(cell.RowIndex, cell);
      }

      return col;
    }

    public DxColumn GetColumn(int colIndex, DxIndexSet rowIndices)
    {
      var theColumn = this.GetColumn(colIndex);

      var col = new DxColumn();

      foreach (var kvpCell in theColumn.Cells)
      {
        if (kvpCell.Key.In(rowIndices))
          col.Cells.Add(kvpCell.Value.ColumnIndex, kvpCell.Value);
      }

      return col;
    }

    public int Get_RowCount()
    {
      if (this.DxCells == null)
        return 0;

      return this.DxCells.GetLength(0);
    }

    public int Get_SheetIndex()
    {
      if (this.DxWorkbook == null)
        throw new Exception("The DxRowSet cannot determine the SheetIndex when it does not have a reference to the DxWorkbook.");

      if (this.WorksheetName.IsBlank())
        throw new Exception("The DxRowSet cannot determine the SheetIndex when it does not have a non-blank WorksheetName.");

      if (!this.DxWorkbook.Keys.Contains(this.WorksheetName))
        throw new Exception("The DxWorkbook does not contain a DxWorksheet named '" + this.WorksheetName + "'.");

      for (int i = 0; i < this.DxWorkbook.Keys.Count; i++)
      {
        if (this.DxWorkbook.Keys.ElementAt(i) == this.WorksheetName)
          return i;
      }

      throw new Exception("Cannot locate the DxWorkbook Key (WorksheetName) index value for WorksheetName '" + this.WorksheetName + "'.");
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
      return this.GetRow(rowIndex, 0, this.LastUsedColumnIndex);
    }

    public DxRow GetRow(int rowIndex, int startColumn)
    {
      return this.GetRow(rowIndex, startColumn, this.LastUsedColumnIndex);
    }

    public DxRow GetRow(int rowIndex, int startColumn, int endColumn)
    {
      try
      {
        var row = new DxRow(this);

        if (rowIndex < 0 || rowIndex > _lastUsedRowIndex)
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
          if (!row.Cells.ContainsKey(cell.ColumnIndex))
            row.Add(cell.ColumnIndex, cell);
        }

        row.EnsureParentage();
        return row;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception has occurred while attempting to get a row from the DxRowSet for sheet named '" +
                            this.WorksheetName + " rowIndex = " + rowIndex.ToString() + " startColumn = " + startColumn.ToString() +
                            " endColumn = " + endColumn.ToString() + ".", ex);
      }
    }

    public DxRow GetRow(int rowIndex, DxIndexSet colIndices)
    {
      var baseRow = this.GetRow(rowIndex);

      var row = new DxRow(this);

      foreach (var kvpRow in baseRow.Cells)
      {
        if (kvpRow.Key.In(colIndices))
          row.Add(kvpRow.Value.ColumnIndex, kvpRow.Value);
      }

      row.EnsureParentage();
      return row;
    }

    public DxRow GetRowWithValue(int rowIndex, int startColumn, int endColumn)
    {
      var row = new DxRow(this);

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
        if (!row.Cells.ContainsKey(cell.ColumnIndex))
          row.Add(cell.ColumnIndex, cell);
      }

      row.EnsureParentage();
      return row;
    }

    public DxRowSet ExtractRowSet(DxMap map)
    {
      var extractedRowSet = new DxRowSet();

      switch (map.DxMapType)
      {
        case DxMapType.RowToRow:
          foreach (var extractedRow in extractedRowSet.Rows.Values)
          {
            var dstRow = new DxRow(this);
            foreach (var mapItem in map.DxMapItemSet.Values)
            {
              var extractedCell = extractedRow.Cells[mapItem.SrcCol];
              //object extractedValue = extractedRow.Value.

              //var dstCell = new DxCell();

            }
          }
          break;

        default:
          throw new Exception("The DxMapType '" + map.DxMapType.ToString() + "' is not yet implemented.");
      }

      extractedRowSet.EnsureParentage();
      return extractedRowSet;
    }

    public void AddCell(DxCell cell)
    {
      try
      {
        if (cell.RowIndex == 99999)
        {
          if (this.Rows.Count == 0)
            cell.RowIndex = 0;
          else
            cell.RowIndex = this.Rows.Keys.Max() + 1;
        }

        if (!this.Rows.ContainsKey(cell.RowIndex))
          this.Rows.Add(cell.RowIndex, new DxRow(this));

        if (_lastUsedRowIndex.HasValue)
        {
          if (_lastUsedRowIndex < cell.RowIndex)
            _lastUsedRowIndex = cell.RowIndex;
        }
        else
        {
          _lastUsedRowIndex = cell.RowIndex;
        }

        this.Rows[cell.RowIndex].Add(cell.ColumnIndex, cell);
        cell.Parent = this.Rows[cell.RowIndex];
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to add a cell to the DxRowSet at row " + cell.RowIndex.ToString() +
                            " column " + cell.ColumnIndex.ToString() + ".", ex);
      }
    }

    public DxIndexSet GetRowIndexSet(int col, DxComparisonType comparisionType, DxTextCase textCase, string matchValue, int startRow, int endRow)
    {
      var indexSet = new DxIndexSet();

      if (endRow == -1)
        endRow = this.LastUsedRowIndex;

      if (endRow > this.LastUsedRowIndex)
        endRow = this.LastUsedRowIndex;

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
        endCol = this.LastUsedColumnIndex;

      if (endCol > this.LastUsedColumnIndex)
        endCol = this.LastUsedColumnIndex;

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

          string cellText = this.DxCells[r, c].TextValue;
          sb.Append(cellText.PadTo(cellWidth) + g.BlankString(cellSpacing));
        }
        sb.Append(g.crlf);
      }

      string grid = sb.ToString();
      return grid;
    }

    protected XElement Get_DxCellArray()
    {
      int lastUsedRowIndex = this.LastUsedRowIndex;
      int lastUsedColumnIndex = this.LastUsedColumnIndex;

      var dxCellArray = new XElement("DxCellArray");
      dxCellArray.Add(new XAttribute("Columns", (lastUsedColumnIndex + 1).ToString()));
      dxCellArray.Add(new XAttribute("Rows", (lastUsedRowIndex + 1).ToString()));

      this.PopulateDxCellsCollection();


      for (int r = 0; r < lastUsedRowIndex + 1; r++)
      {
        for (int c = 0; c < lastUsedColumnIndex + 1; c++)
        {
          var cell = this.DxCells[r, c];
          if (cell != null && cell.RawValue != null)
          {
            var dxCell = new XElement("DxCell");
            dxCell.Add(new XAttribute("R", cell.RowIndex.ToString()));
            dxCell.Add(new XAttribute("C", cell.ColumnIndex.ToString()));
            if (cell.Name.IsNotBlank())
              dxCell.Add(new XAttribute("Name", cell.Name != null ? cell.Name.Trim() : String.Empty));
            string rawValue = "@NULL@";
            if (cell.RawValue != null)
              rawValue = cell.RawValue.ToString();
            dxCell.Add(new XAttribute("V", rawValue.ToString()));
            dxCellArray.Add(dxCell);
          }
        }
      }

      return dxCellArray;
    }

    protected void Set_DxCellArray(XElement dxCellArray)
    {
      int rows = dxCellArray.Attribute("Rows").Value.ToInt32();
      int cols = dxCellArray.Attribute("Columns").Value.ToInt32();

      this.DxCells = new DxCell[rows, cols];

      foreach (var cell in dxCellArray.Elements("DxCell"))
      {
        DxCell dxCell = new DxCell();
        dxCell.RowIndex = cell.Attribute("R").Value.ToInt32();
        dxCell.ColumnIndex = cell.Attribute("C").Value.ToInt32();
        dxCell.RawValue = cell.Attribute("V").Value.Trim();
        if (cell.Attribute("Name") != null)
          dxCell.Name = cell.Attribute("Name").Value.Trim();
        if (dxCell.RawValue.ToString() == "@NULL@")
          dxCell.RawValue = null;
        dxCell.AutoInit();
        this.DxCells[dxCell.RowIndex, dxCell.ColumnIndex] = dxCell;
      }

      for (int r = 0; r < rows; r++)
      {
        for (int c = 0; c < cols; c++)
        {
          if (this.DxCells[r, c] == null)
          {
            var dxCell = new DxCell();
            dxCell.Name = String.Empty;
            dxCell.RowIndex = r;
            dxCell.ColumnIndex = c;
            dxCell.RawValue = null;
            dxCell.IsDummyCell = true;
            this.DxCells[r, c] = dxCell;
          }
        }
      }

      RefreshDxRows();
    }

    private void RefreshDxRows()
    {
      this.Rows.Clear();

      for (int r = 0; r < this.DxCells.GetLength(0); r++)
      {
        var row = new DxRow(this);
        for (int c = 0; c < this.DxCells.GetLength(1); c++)
        {
          row.Add(c, this.DxCells[r, c]);
        }
        this.Rows.Add(r, row);
      }

      this.EnsureParentage();
    }

    private void PopulateDxCellsCollection()
    {
      this.DxCells = new DxCell[this.LastUsedRowIndex + 1, this.LastUsedColumnIndex + 1];

      foreach (var dxRow in this.Rows.Values)
      {
        foreach (var dxCell in dxRow.Cells.Values)
        {
          if (this.DxCells.GetLength(0) < dxCell.RowIndex + 1)
          {
            Debugger.Break();
          }
          else
          {
            if (this.DxCells.GetLength(1) < dxCell.ColumnIndex + 1)
            {
              Debugger.Break();
            }
          }

          this.DxCells[dxCell.RowIndex, dxCell.ColumnIndex] = dxCell;
        }
      }
    }

    public void PopulateDxCellsArray(bool reindexRows = false)
    {
      int rowIndex = 0;
      int colIndex = 0;

      try
      {
        this.DxCells = new DxCell[0, 0];

        if (this == null || this.Rows.Count == 0)
          return;

        if (reindexRows)
        {
          for (int r = 0; r < this.Rows.Count; r++)
          {
            rowIndex = r;
            var row = this.Rows.Values.ElementAt(r);
            for (int c = 0; c < row.Cells.Count; c++)
            {
              colIndex = c;
              var cell = row.Cells[c];
              cell.RowIndex = r;
            }
          }
        }

        int highestRowUsed = -1;
        int highestColUsed = -1;

        foreach (DxRow row in this.Rows.Values)
        {
          foreach (DxCell cell in row.Cells.Values)
          {
            if (cell.RowIndex > highestRowUsed)
              highestRowUsed = cell.RowIndex;
            if (cell.ColumnIndex > highestColUsed)
              highestColUsed = cell.ColumnIndex;
          }
        }

        this.DxCells = new DxCell[highestRowUsed + 1, highestColUsed + 1];

        foreach (DxRow row in this.Rows.Values)
        {
          foreach (DxCell cell in row.Cells.Values)
          {
            if (this.DxCells.GetLength(0) < cell.RowIndex + 1)
            {
              Debugger.Break();
            }

            if (this.DxCells.GetLength(1) < cell.ColumnIndex + 1)
            {
              Debugger.Break();
            }

            this.DxCells[cell.RowIndex, cell.ColumnIndex] = cell;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to populate the DxCells array for the DxRowSet (sheet) named '" + this.WorksheetName + "'.", ex);
      }
    }

    public int LocateRowIndex(string spec, RectangleSide side, int startRowIndex, int minColIndex, int maxColIndex)
    {
      if (spec.IsBlank())
      {
        if (side == RectangleSide.Top)
          return 0;

        return this.LastUsedRowIndex;
      }

      if (spec.IsInteger())
      {
        int rowIndex = spec.ToInt32();

        if (side == RectangleSide.Bottom && rowIndex > this.LastUsedRowIndex)
          throw new Exception("The row index literal specified in the Region spec '" + spec + "' is greater than the number of rows in the sheet named '" + this.WorksheetName + "'.");

        return rowIndex;
      }

      var indexLocator = new IndexLocator(IndexType.RowIndex, spec);
      indexLocator.CellSearchCriteriaSet.MinOppositeIndex = minColIndex;
      indexLocator.CellSearchCriteriaSet.MaxOppositeIndex = maxColIndex;

      if (indexLocator.CellSearchCriteriaSet.Algorithm != null)
        return this.ProcessAlgorithm(indexLocator);

      int endIndex = this.LastUsedRowIndex + 1;
      int increment = 1;

      if (indexLocator.CellSearchCriteriaSet.FindLast)
      {
        endIndex = startRowIndex - 1;
        startRowIndex = this.LastUsedRowIndex;
        increment = -1;
      }

      for (int r = startRowIndex; r != endIndex; r += increment)
      {
        var row = this.Rows.Values.ElementAt(r);
        if (indexLocator.RowMatch(row))
          return r;
      }

      if (indexLocator.CellSearchCriteriaSet.OrLastIndex)
        return this.LastUsedRowIndex;

      return -1;
    }

    public int LocateColumnIndex(string spec, RectangleSide side, int startColumnIndex, int minRowIndex, int maxRowIndex)
    {
      if (spec.IsBlank())
      {
        if (side == RectangleSide.Left)
          return 0;

        return this.LastUsedColumnIndex;
      }

      if (spec.IsInteger())
      {
        int columnIndex = spec.ToInt32();

        if (side == RectangleSide.Right && columnIndex > this.LastUsedColumnIndex)
          throw new Exception("The column index literal specified in the Region spec '" + spec + "' is greater than the number of columns in the sheet named '" + this.WorksheetName + "'.");

        return columnIndex;
      }

      var indexLocator = new IndexLocator(IndexType.ColumnIndex, spec);
      indexLocator.CellSearchCriteriaSet.MinOppositeIndex = minRowIndex;
      indexLocator.CellSearchCriteriaSet.MaxOppositeIndex = maxRowIndex;

      if (indexLocator.CellSearchCriteriaSet.Algorithm != null)
        return this.ProcessAlgorithm(indexLocator);


      int endIndex = this.LastUsedColumnIndex + 1;
      int increment = 1;

      if (indexLocator.CellSearchCriteriaSet.FindLast)
      {
        endIndex = startColumnIndex - 1;
        startColumnIndex = this.LastUsedColumnIndex;
        increment = -1;
      }

      for (int c = startColumnIndex; c != endIndex; c += increment)
      {
        var col = this.DxColumnSet.Columns.Values.ElementAt(c);
        if (indexLocator.ColumnMatch(col))
          return c;
      }

      return -1;
    }

    public int? LocateRowIndex(IndexLocator indexLocator)
    {
      for (int r = 0; r < this.Rows.Count; r++)
      {
        var dxRow = this.Rows.Values.ElementAt(r);

        bool match = dxRow.Match(indexLocator);

        if (match)
          return r;
      }

      return null;
    }

    public int? LocateColumnIndex(IndexLocator indexLocator)
    {
      int lastUsedColumnIndex = this.LastUsedColumnIndex;

      for (int c = 0; c < lastUsedColumnIndex; c++)
      {
        var dxColumn = this.GetColumn(c);

        bool match = dxColumn.Match(indexLocator);

        if (match)
          return c;
      }

      return null;
    }

    private int ProcessAlgorithm(IndexLocator indexLocator)
    {
      try
      {
        if (indexLocator.IndexType == IndexType.RowIndex)
        {
          switch (indexLocator.CellSearchCriteriaSet.Algorithm.AlgorithmType)
          {
            case AlgorithmType.TopMostNonBlankCell:
              return this.GetTopMostNonBlankCellIndex(indexLocator.CellSearchCriteriaSet.MinOppositeIndex, indexLocator.CellSearchCriteriaSet.MaxOppositeIndex);
            case AlgorithmType.BottomMostNonBlankCell:
              return this.GetBottomMostNonBlankCellIndex(indexLocator.CellSearchCriteriaSet.MinOppositeIndex, indexLocator.CellSearchCriteriaSet.MaxOppositeIndex);

            default:
              throw new Exception("The AlgorithmType '" + indexLocator.CellSearchCriteriaSet.Algorithm.AlgorithmType.ToString() + "' is not yet implemented.");
          }
        }
        else
        {
          switch (indexLocator.CellSearchCriteriaSet.Algorithm.AlgorithmType)
          {
            case AlgorithmType.RightMostNonBlankCell:
              return this.GetRightMostNonBlankCellIndex(indexLocator.CellSearchCriteriaSet.MinOppositeIndex, indexLocator.CellSearchCriteriaSet.MaxOppositeIndex);
            case AlgorithmType.LeftMostNonBlankCell:
              return this.GetLeftMostNonBlankCellIndex(indexLocator.CellSearchCriteriaSet.MinOppositeIndex, indexLocator.CellSearchCriteriaSet.MaxOppositeIndex);

            default:
              throw new Exception("The AlgorithmType '" + indexLocator.CellSearchCriteriaSet.Algorithm.AlgorithmType.ToString() + "' is not yet implemented.");
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to process the index locator algorithm '" + indexLocator.CellSearchCriteriaSet.Algorithm.Report + "'.", ex);

      }
    }

    public string Get_Report()
    {
      StringBuilder sb = new StringBuilder();
      StringBuilder sbHeader = new StringBuilder();

      sb.Append(g.crlf + "WORKSHEET NAME : " + this.WorksheetName + g.crlf);

      if (this.Rows.Values.Count == 0)
        sb.Append("The worksheet contains now rows.");

      for (int r = 0; r < this.Rows.Values.Count; r++)
      {
        int rowKey = this.Rows.Keys.ElementAt(r);
        var row = this.Rows.Values.ElementAt(r);

        StringBuilder sbTopGrid = new StringBuilder();
        StringBuilder sbBottomGrid = new StringBuilder();
        StringBuilder sbData = new StringBuilder();

        if (r == 0)
        {
          sbHeader.Append("   ROW   ");
          for (int i = 0; i < this.ColumnCount; i++)
          {
            sbHeader.Append("        " + i.ToString("00") + "         ");
          }

          sb.Append(sbHeader.ToString() + g.crlf);
        }

        var nextRow = r < this.Rows.Values.Count - 1 ? this.Rows.Values.ElementAt(r + 1) : null;

        for (var columnIndex = 0; columnIndex < this.LastUsedColumnIndex + 1; columnIndex++)
        {
          if (columnIndex == 0)
          {
            sbData.Append(("\xA6").ToString() + " " + rowKey.ToString("00000") + " " + ("\xA6").ToString());
            sbTopGrid.Append("+-------+");
            sbBottomGrid.Append("+-------+");
          }

          if (row.Cells.Count > columnIndex)
          {
            DxCell cell = row.Cells.ElementAt(columnIndex).Value;
            string cellIndices = cell.RowIndex.ToString() + ":" + cell.ColumnIndex.ToString();
            int cellIndicesLength = cellIndices.Length;

            sbTopGrid.Append("-" + cellIndices + new String('-', 17 - cellIndicesLength) + "+");
            sbBottomGrid.Append("------------------+");

            if (cell.RawValue == null)
            {
              sbData.Append(" NULL             " + ("\xA6").ToString());
            }
            else
            {
              if (cell.IsNumeric)
              {
                sbData.Append(" " + cell.RawValue.ToString().PadToJustifyRight(16).Substring(0, 16) + " " + ("\xA6").ToString());
              }
              else
              {
                string cellValue = cell.RawValue.ToString().Replace("\r\n", " ").Replace("\n", " ");
                sbData.Append(" " + cellValue.PadToLength(16).Substring(0, 16) + " " + ("\xA6").ToString());
              }
            }
          }
          else
          {
            sbTopGrid.Append("------------------+");
            sbBottomGrid.Append("------------------+");
            sbData.Append(" (NO CELL)        " + ("\xA6").ToString());
          }
        }

        sb.Append(sbTopGrid.ToString() + g.crlf);
        sb.Append(sbData.ToString() + g.crlf);

        if (r == this.Rows.Values.Count - 1)
          sb.Append(sbBottomGrid.ToString() + g.crlf);
      }

      string report = sb.ToString();

      return report;
    }

    public void Trim(DxRegion region)
    {
      try
      {
        region.SetRegionRectangle(this);

        this.Trim(region.TopIndex, region.BottomIndex);

        foreach (var row in this.Rows.Values)
        {
          row.Trim(region.LeftIndex, region.RightIndex);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to apply the region '" + region.Report + " to the DxRowSet named '" + this.WorksheetName + "'.", ex);
      }
    }

    public void Trim(int topIndex, int bottomIndex = -1)
    {
      try
      {
        if (topIndex < 0 || bottomIndex > this.Rows.Count - 1)
          throw new Exception("The parameters passed to the DxRowSet Trim method (top=" + topIndex.ToString() + ", bottom=" + bottomIndex.ToString() +
                              "are not valid to be applied to DxRowSet named '" + this.WorksheetName + "' with indices from 0 to " + (this.Rows.Count - 1).ToString() + ".");

        // first, ensure all row.RowIndex values correctly match the current physical row index
        for (int i = 0; i < this.Rows.Count - 1; i++)
        {
          this.Rows.Values.ElementAt(i).SetRowIndex(i);
        }

        if (bottomIndex == -1)
          bottomIndex = this.Rows.Count - 1;

        var tempRows = new List<DxRow>();

        for (int i = 0; i < this.Rows.Count - 1; i++)
        {
          if (i < topIndex || i > bottomIndex)
            continue;
          tempRows.Add(this.Rows.Values.ElementAt(i));
        }

        this.Rows.Clear();

        for (int i = 0; i < tempRows.Count; i++)
        {
          this.Rows.Add(i, tempRows.ElementAt(i));
          this.Rows.Values[i].SetRowIndex(i);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to trim rows below index " + topIndex.ToString() + " and after index " + bottomIndex.ToString() +
                            "from DxRowSet named '" + this.WorksheetName + "' which has indices from 0 to " + (this.Rows.Count - 1).ToString() + ".", ex);
      }
    }

    public void EnsureIntegrity()
    {
      try
      {
        foreach (var kvpRow in this.Rows)
        {
          kvpRow.Value.SetRowIndex(kvpRow.Key);
          kvpRow.Value.EnsureIntegrity();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the EnsureIntegrity method of the DxRowSet.", ex);
      }
    }

    private string Get_CellIndexReport()
    {
      var sb = new StringBuilder();

      for (int r = 0; r < this.Rows.Count; r++)
      {
        var row = this.Rows.Values.ElementAt(r);
        for (int c = 0; c < row.Cells.Count; c++)
        {
          var cell = row.Cells[c];
          sb.Append("PR:" + r.ToString("00000") + " LR:" + cell.RowIndex.ToString("00000") + " PC:" + c.ToString("00000") + " LC:" + cell.ColumnIndex.ToString("00000") + g.crlf);
        }
        sb.Append(g.crlf);
      }

      string report = sb.ToString().Trim();
      return report;
    }

    public List<DxCell> GetCells(int? rowIndex, int? colIndex)
    {
      var cells = new List<DxCell>();

      if (rowIndex.HasValue && colIndex.HasValue)
      {
        if (this.Rows.Count > rowIndex.Value)
        {
          var row = this.Rows.Values.ElementAt(rowIndex.Value);
          if (row.Cells.Count > colIndex.Value)
          {
            cells.Add(row.Cells.Values.ElementAt(colIndex.Value));
          }
        }
      }
      else
      {
        if (rowIndex.HasValue)
        {
          if (this.Rows.Count > rowIndex.Value)
          {
            var row = this.Rows.Values.ElementAt(rowIndex.Value);
            for (int c = 0; c < row.Cells.Count; c++)
            {
              cells.Add(row.Cells.Values.ElementAt(c));
            }
          }
        }
        else
        {
          if (colIndex.HasValue)
          {
            for (var r = 0; r < this.Rows.Count; r++)
            {
              var row = this.Rows.Values.ElementAt(rowIndex.Value);
              if (row.Cells.Count > colIndex.Value)
              {
                cells.Add(row.Cells.Values.ElementAt(colIndex.Value));
              }
            }
          }
          else
          {
            return this.CellList;
          }
        }
      }

      return cells;
    }

    private List<DxCell> Get_CellList()
    {
      var cells = new List<DxCell>();

      for (int r = 0; r < this.Rows.Count; r++)
      {
        var row = this.Rows.Values.ElementAt(r);
        for (int c = 0; c < row.Cells.Count; c++)
        {
          cells.Add(row.Cells.Values.ElementAt(c));
        }
      }

      return cells;
    }

    private DxColumnSet Get_DxColumnSet()
    {
      var colSet = new DxColumnSet();
      for (int c = 0; c < this.LastUsedColumnIndex + 1; c++)
      {
        var col = new DxColumn();
        for (int r = 0; r < this.LastUsedRowIndex + 1; r++)
        {
          var row = this.Rows[r];
          col.Cells.Add(r, row.Cells[c]);
        }
        colSet.Columns.Add(c, col);
      }

      _dxColumnSet = colSet;
      return colSet;
    }

    public SortedList<int, DxCell> GetCells()
    {
      var cells = new SortedList<int, DxCell>();

      foreach (var row in _rows.Values)
      {
        int seqCount = 0;
        foreach (var cell in row.Cells.Values)
        {
          if (seqCount == 0)
            cell.IsFirstInSequence = true;
          seqCount++;
          cells.Add(cells.Count, cell);
        }

        if (seqCount > 0)
          cells.Values.Last().IsLastInSequence = true;
      }

      if (cells.Count > 0)
      {
        cells.Values.First().IsFirstInSet = true;
        cells.Values.Last().IsLastInSet = true;
      }

      return cells;
    }

    public void EnsureParentage()
    {
      try
      {
        foreach (var row in this.Rows)
        {
          row.Value.Parent = this;
          row.Value.EnsureParentage();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to ensure the parentage of Dx objects.", ex);
      }
    }

    private int GetRightMostNonBlankCellIndex(int? minRowIndex, int? maxRowIndex)
    {
      for (int c = this.LastUsedColumnIndex; c > -1; c--)
      {
        var col = this.DxColumnSet.Columns.ElementAt(c);
        if (col.Value.HasContentInRange(minRowIndex, minRowIndex))
          return c;
      }

      return -1;
    }

    private int GetLeftMostNonBlankCellIndex(int? minRowIndex, int? maxRowIndex)
    {
      for (int c = 0; c < this.LastUsedColumnIndex + 1; c++)
      {
        var col = this.DxColumnSet.Columns.ElementAt(c);
        if (col.Value.HasContentInRange(minRowIndex, minRowIndex))
          return c;
      }

      return -1;
    }

    private int GetTopMostNonBlankCellIndex(int? minColIndex, int? maxColIndex)
    {
      for (int r = 0; r < this.LastUsedRowIndex + 1; r++)
      {
        var row = this.Rows.ElementAt(r);
        if (row.Value.HasContentInRange(minColIndex, maxColIndex))
          return r;
      }

      return -1;
    }

    private int GetBottomMostNonBlankCellIndex(int? minColIndex, int? maxColIndex)
    {
      for (int r = this.LastUsedRowIndex; r < -1; r--)
      {
        var row = this.Rows.ElementAt(r);
        if (row.Value.HasContentInRange(minColIndex, maxColIndex))
          return r;
      }

      return -1;
    }
  }
}

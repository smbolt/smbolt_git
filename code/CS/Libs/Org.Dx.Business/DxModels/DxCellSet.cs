using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  public class DxCellSet : Node
  {
    private int? _lastUsedColumnIndex;
    public int LastUsedColumnIndex {
      get {
        return _lastUsedColumnIndex.HasValue ? _lastUsedColumnIndex.Value : -1;
      }
    }

    private int? _rowIndex;
    public int RowIndex {
      get {
        return _rowIndex.HasValue ? _rowIndex.Value : -1;
      }
    }

    public DxRowSet DxRowSet {
      get;
      set;
    }
    public string RowValues {
      get {
        return Get_RowValues();
      }
    }

    public bool HasContent {
      get {
        return Get_HasContent();
      }
    }

    private Dictionary<string, DxCell> _namedCells;

    private SortedList<int, DxCell> _cells;
    public SortedList<int, DxCell> Cells
    {
      get
      {
        if (_cells == null)
          _cells = new SortedList<int, DxCell>();
        return _cells;
      }
    }

    public DxCellSet(DxRowSet dxRowSet)
    {
      this.DxObject = this;

      _cells = new SortedList<int, DxCell>();
      this.DxRowSet = dxRowSet;
    }

    public void Add(int columnIndex, DxCell dxCell)
    {
      dxCell.Parent = this;

      if (_cells.ContainsKey(columnIndex))
        throw new Exception("The column index " + columnIndex.ToString() + " already exists in the DxCellSet.");

      if (_rowIndex.HasValue && dxCell.RowIndex != _rowIndex.Value)
        throw new Exception("The row index of the cell being added which is '" + dxCell.RowIndex.ToString() + "' is not the same as the existing row index " +
                            "for this DxCellSet (row) which is '" + _rowIndex.Value.ToString() + "'.");

      dxCell.DxCellSet = this;

      if (!_rowIndex.HasValue)
        _rowIndex = dxCell.RowIndex;

      string cellName = dxCell.Name == null ? String.Empty : dxCell.Name.Trim();

      if (_namedCells == null)
        _namedCells = new Dictionary<string, DxCell>();

      if (cellName.IsNotBlank() && _namedCells.ContainsKey(cellName))
        throw new Exception("The DxCell name '" + cellName + "' already exists in the DxCellSet (row).");

      if (cellName.IsNotBlank())
        _namedCells.Add(cellName, dxCell);

      if (!_lastUsedColumnIndex.HasValue || _lastUsedColumnIndex.Value < dxCell.ColumnIndex)
        _lastUsedColumnIndex = dxCell.ColumnIndex;

      _cells.Add(columnIndex, dxCell);
    }

    private string Get_RowValues()
    {
      StringBuilder sb = new StringBuilder();

      if (_cells.Count == 0)
        return "No cells";

      sb.Append("Column     Value" + g.crlf2);

      foreach (var kvp in _cells)
      {
        sb.Append(kvp.Key.ToString("0000") + "       " + kvp.Value.TextValue + g.crlf);
      }

      return sb.ToString();
    }

    public bool HasContentInRange(int? minColIndex, int? maxColIndex)
    {
      if (_cells.Count == 0)
        return false;

      foreach (var cell in _cells.Values)
      {
        if ((minColIndex.HasValue && cell.ColumnIndex < minColIndex.Value) || (maxColIndex.HasValue && cell.ColumnIndex > maxColIndex.Value))
          continue;

        if (cell.ValueOrDefault.ObjectToTrimmedString().IsNotBlank())
          return true;
      }

      return false;
    }

    private bool Get_HasContent()
    {
      if (_cells.Count == 0)
        return false;

      foreach (var cell in _cells.Values)
      {
        if (cell.ValueOrDefault.ObjectToTrimmedString().IsNotBlank())
          return true;
      }

      return false;
    }

    public void Trim(int leftIndex, int rightIndex = -1)
    {
      try
      {
        // rightIndex is defaulted to -1 to indicate that no right bound was specified

        if (leftIndex < 0 || rightIndex > _cells.Count - 1)
          throw new Exception("The parameters passed to the DxCellSet Trim method (left=" + leftIndex.ToString() + ", right=" + rightIndex.ToString() +
                              "are not valid for the DxCellSet (row) which has indices from 0 to " + (_cells.Count - 1).ToString() + ".");

        // if rightIndex is not specified, set it to the last index in the row
        if (rightIndex == -1)
          rightIndex = _cells.Count - 1;

        // ensure that the column index values in the cells are correct
        for (int i = 0; i < _cells.Count - 1; i++)
        {
          _cells.Values.ElementAt(i).ColumnIndex = i;
        }

        var tempCells = new List<DxCell>();

        for (int i = 0; i < _cells.Count; i++)
        {
          if (i < leftIndex || i > rightIndex)
            continue;

          tempCells.Add(_cells.Values.ElementAt(i));
        }

        _cells.Clear();

        for (int i = 0; i < tempCells.Count; i++)
        {
          _cells.Add(i, tempCells.ElementAt(i));
          _cells.Values[i].ColumnIndex = i;
        }

        this.EnsureParentage();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to trim cells below columnIndex " + leftIndex.ToString() + " and " + rightIndex.ToString() +
                            " from a DxCellSet (row) with indices from 0 to " + (_cells.Count - 1).ToString() + ".", ex);
      }
    }

    public bool IncludeRow(DxCellSet cellSet)
    {
      var fs = new DxFilterSet();

      try
      {
        return fs.IncludeRow(cellSet);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to exclude a DxRow object based on its value and the specified DxFilterSet.", ex);
      }
    }

    public object CellValue(string name, Type type, bool isNullable = true)
    {
      DxCell cell = null;

      try
      {
        if (_namedCells == null)
          this.PopulateNamedCellsArray();

        string singleNodeName = String.Empty;
        if (name.Contains("."))
          singleNodeName = name.Split(Constants.PeriodDelimiter, StringSplitOptions.RemoveEmptyEntries).LastOrDefault().ObjectToTrimmedString();

        if (!_namedCells.ContainsKey(name))
        {
          if (singleNodeName.IsNotBlank() && _namedCells.ContainsKey(singleNodeName))
          {
            cell = _namedCells[singleNodeName];
          }
          else
          {
            if (!isNullable)
              throw new Exception("Required cell named '" + name + "' is not included in the DxCellSet.");
            else
              return null;
          }
        }

        if (cell == null)
          cell = _namedCells[name];

        string typeName = type.GetTypeName();

        switch (typeName)
        {
          case "String":
            return cell.ValueOrDefault.DbToString();
          case "DateTime":
            return cell.ValueOrDefault.DbToDateTime().Value;
          case "DateTime?":
            return cell.ValueOrDefault.DbToDateTime();
          case "Decimal":
            return cell.ValueOrDefault.DbToDecimal().Value;
          case "Decimal?":
            return cell.ValueOrDefault.DbToDecimal();
          case "Int32":
            return cell.ValueOrDefault.DbToInt32().Value;
          case "Int32?":
            return cell.ValueOrDefault.DbToInt32();
          default:
            throw new Exception("Data type '" + typeName + "' is not yet implemented in DxCellSet.CellValue.");
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get a strongly typed value from the cell - cell name is '" +
                            (cell.Name.IsNotBlank() ? cell.Name : "[cell name is null or blank]") + "'.", ex);
      }
    }

    public string GetTextValue(int columnIndex)
    {
      if (!_cells.ContainsKey(columnIndex))
        return String.Empty;

      return _cells[columnIndex].TextValue;
    }

    public int? GetInt32Value(int columnIndex)
    {
      if (!_cells.ContainsKey(columnIndex))
        return (int?)null;

      if (!this.IsNumeric(columnIndex))
        return (int?)null;

      return _cells[columnIndex].Int32Value;
    }

    public decimal? GetDecimalValue(int columnIndex)
    {
      if (!_cells.ContainsKey(columnIndex))
        return (decimal?)null;

      if (!this.IsNumeric(columnIndex))
        return (decimal?)null;

      return _cells[columnIndex].DecimalValue;
    }

    public bool? GetBooleanValue(int columnIndex)
    {
      if (!_cells.ContainsKey(columnIndex))
        return (bool?)null;

      if (!this.IsBoolean(columnIndex))
        return (bool?)null;

      return _cells[columnIndex].BooleanValue;
    }

    public DateTime? GetDateTimeValue(int columnIndex)
    {
      if (!_cells.ContainsKey(columnIndex))
        return (DateTime?)null;

      if (!this.IsBoolean(columnIndex))
        return (DateTime?)null;

      return _cells[columnIndex].DateTimeValue;
    }

    public bool IsBoolean(int columnIndex)
    {
      if (!_cells.ContainsKey(columnIndex))
        return false;

      return _cells[columnIndex].IsBoolean;
    }

    public bool IsDateTime(int columnIndex)
    {
      if (!_cells.ContainsKey(columnIndex))
        return false;

      return _cells[columnIndex].IsDateTime;
    }

    public bool IsEmpty(int columnIndex)
    {
      if (!_cells.ContainsKey(columnIndex))
        return false;

      return _cells[columnIndex].IsDateTime;
    }

    public bool IsNumeric(int columnIndex)
    {
      if (!_cells.ContainsKey(columnIndex))
        return false;

      return _cells[columnIndex].IsNumeric;
    }

    public bool IsText(int columnIndex)
    {
      if (!_cells.ContainsKey(columnIndex))
        return false;

      return _cells[columnIndex].IsText;
    }

    public bool IsNull(int columnIndex)
    {
      if (!_cells.ContainsKey(columnIndex))
        return false;

      return _cells[columnIndex].RawValue == null;
    }

    public DxCell GetNamedCell(string name)
    {
      name = name.Trim();

      if (_namedCells == null)
      {
        _namedCells = new Dictionary<string, DxCell>();
        foreach (var cell in _cells.Values)
        {
          if (cell.Name.IsNotBlank())
          {
            if (_namedCells.ContainsKey(cell.Name.Trim()))
              throw new Exception("The DxRow contains a duplicate name '" + cell.Name.Trim() + "'.");
            _namedCells.Add(cell.Name.Trim(), cell);
          }
        }
      }

      if (_namedCells.ContainsKey(name.Trim()))
        return _namedCells[name.Trim()];

      return null;
    }

    public void PopulateNamedCellsArray()
    {
      _namedCells = new Dictionary<string, DxCell>();
      foreach (var cell in _cells.Values)
      {
        if (cell.Name.IsBlank())
          continue;

        if (cell.Name.IsBlank())
          throw new Exception("A DxCell object was found which does not have a Name property - row index is '" + cell.RowIndex.ToString() +
                              "', column index is '" + cell.ColumnIndex.ToString());

        if (_namedCells.ContainsKey(cell.Name))
          throw new Exception("A DxCell object was found has a duplicate  Name property '" + cell.Name + "', - row index is '" + cell.RowIndex.ToString() +
                              "', column index is '" + cell.ColumnIndex.ToString());

        _namedCells.Add(cell.Name, cell);
      }
    }

    public bool ExcludeBasedOnFilterSet(DxFilterSet fs)
    {
      try
      {
        foreach (var cell in _cells.Values)
        {
          if (cell.ExcludeBaseOnFilter(fs))
            return true;
        }

        return false;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to exclude a DxRow object based on its value and the specified DxFilterSet.", ex);
      }
    }

    public void SetRowIndex(int rowIndex)
    {
      _rowIndex = rowIndex;
    }

    public void EnsureIntegrity()
    {
      try
      {
        foreach(var kvpCell in _cells)
        {

        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the EnsureIntegrity method of the DxCellSet.", ex);
      }
    }

    public bool Match(IndexLocator indexLocator)
    {
      bool allMatch = true;
      bool anyMatch = false;

      foreach (var cellSearchCriteria in indexLocator.CellSearchCriteriaSet)
      {
        bool match = false;

        if (cellSearchCriteria.ColIndexSpec.HasValue)
        {
          if (_cells.Count >= cellSearchCriteria.RowIndexSpec.Value)
          {
            var dxCell = _cells.Values.ElementAt(cellSearchCriteria.ColIndexSpec.Value);
            match = dxCell.MatchesCriteria(cellSearchCriteria.TextNodeSpec);
            if (match)
            {
              anyMatch = true;
              if (indexLocator.CellSearchCriteriaSet.UsesOrLogic)
                return true;
              break;
            }
            else
            {
              allMatch = false;
              if (indexLocator.CellSearchCriteriaSet.UsesAndLogic)
                return false;
            }
          }
          else
          {
            allMatch = false;
          }
        }
        else
        {
          for (int c = 0; c < _cells.Count; c++)
          {
            var dxCell = _cells.Values.ElementAt(c);
            match = dxCell.MatchesCriteria(cellSearchCriteria.TextNodeSpec);
            if (match)
              break;
          }

          if (match)
          {
            anyMatch = true;
            if (indexLocator.CellSearchCriteriaSet.UsesOrLogic)
              return true;
          }
          else
          {
            allMatch = false;
            if (indexLocator.CellSearchCriteriaSet.UsesAndLogic)
              return false;
          }
        }
      }

      if (indexLocator.CellSearchCriteriaSet.UsesAndLogic)
        return allMatch;

      return anyMatch;
    }

    public string Get_Report()
    {
      StringBuilder sb = new StringBuilder();
      StringBuilder sbHeader = new StringBuilder();
      StringBuilder sbGrid = new StringBuilder();
      StringBuilder sbData = new StringBuilder();

      // build row header
      sbHeader.Append("   ROW   ");
      for (int i = 0; i < _cells.Count; i++)
      {
        sbHeader.Append("        " + i.ToString("00") + "         ");
      }

      for (int i = 0; i < _cells.Count; i++)
      {
        if (i == 0)
          sbHeader.Append(g.crlf + "+-------+");
        sbHeader.Append("------------------+");
      }

      sb.Append(sbHeader.ToString() + g.crlf);

      // add row data
      int columnIndex = -1;
      foreach (var cell in _cells.Values)
      {
        columnIndex++;
        if (columnIndex == 0)
        {
          sbData.Append(("\xA6").ToString() + "  ROW  " + ("\xA6").ToString());
          sbGrid.Append("+-------+");
        }

        sbGrid.Append("------------------+");

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

      sb.Append(sbData.ToString() + g.crlf);
      sb.Append(sbGrid.ToString() + g.crlf);

      string report = sb.ToString();

      return report;
    }

    public string Get_VerticalReport()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Col   Value" + g.crlf);


      int columnIndex = -1;
      foreach (var cell in _cells.Values)
      {
        columnIndex++;
        sb.Append(columnIndex.ToString("000") + "  ");

        if (cell.RawValue == null)
        {
          sb.Append(" NULL             " + g.crlf);
        }
        else
        {
          if (cell.IsNumeric)
            sb.Append(" " + cell.RawValue.ToString().PadToJustifyRight(16).Substring(0, 16) + g.crlf);
          else
            sb.Append(" " + cell.RawValue.ToString().PadToLength(16).Substring(0, 16) + g.crlf);
        }
      }


      string report = sb.ToString();
      return report;
    }

    public SortedList<int, DxCell> GetCells()
    {
      var cells = new SortedList<int, DxCell>();

      foreach (var cell in _cells.Values)
      {
        cells.Add(cells.Count, cell);
      }

      if (cells.Count > 0)
      {
        cells.Values.First().IsFirstInSet = true;
        cells.Values.First().IsFirstInSequence = true;
        cells.Values.Last().IsLastInSet = true;
        cells.Values.Last().IsLastInSequence = true;
      }

      return cells;
    }

    public void EnsureParentage()
    {
      try
      {

      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to ensure parentage of all cells in a DxCellSet.", ex);
      }
    }
  }
}

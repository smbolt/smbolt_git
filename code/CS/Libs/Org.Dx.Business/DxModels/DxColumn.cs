using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  public class DxColumn : Node
  {
    private Dictionary<int, DxCell> _cells;
    public Dictionary<int, DxCell> Cells
    {
      get
      {
        if (_cells == null)
          _cells = new Dictionary<int, DxCell>();
        return _cells;
      }
    }

    public string Report {
      get {
        return Get_Report();
      }
    }
    public string ColumnValues {
      get {
        return Get_ColumnValues();
      }
    }
    public bool HasContent {
      get {
        return Get_HasContent();
      }
    }

    public DxColumn()
    {
      this.DxObject = this;
      _cells = new Dictionary<int, DxCell>();
    }

    private string Get_ColumnValues()
    {
      StringBuilder sb = new StringBuilder();

      if (_cells.Count == 0)
        return "No cells";

      sb.Append("Row     Value" + g.crlf2);

      foreach (var kvp in _cells)
      {
        sb.Append(kvp.Key.ToString("0000") + "       " + kvp.Value.TextValue + g.crlf);
      }

      return sb.ToString();
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

    public bool Match(IndexLocator indexLocator)
    {
      bool allMatch = true;
      bool anyMatch = false;

      foreach (var cellSearchCriteria in indexLocator.CellSearchCriteriaSet)
      {
        bool match = false;

        if (cellSearchCriteria.RowIndexSpec.HasValue)
        {
          if (_cells.Count >= cellSearchCriteria.RowIndexSpec.Value)
          {
            var dxCell = _cells.Values.ElementAt(cellSearchCriteria.RowIndexSpec.Value);
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
          for (int r = 0; r < _cells.Count; r++)
          {
            var dxCell = _cells.Values.ElementAt(r);
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

    private string Get_Report()
    {
      StringBuilder sb = new StringBuilder();

      if (_cells.Count == 0)
        return "No cells";

      sb.Append("Row     Value" + g.crlf2);

      foreach (var kvp in _cells)
      {
        sb.Append(kvp.Key.ToString("0000") + "       " + kvp.Value.TextValue + g.crlf);
      }

      return sb.ToString();
    }

    public bool HasContentInRange(int? minRowIndex, int? maxRowIndex)
    {
      if (_cells.Count == 0)
        return false;

      foreach (var cell in _cells.Values)
      {
        if ((minRowIndex.HasValue && cell.RowIndex < minRowIndex.Value) || (maxRowIndex.HasValue && cell.RowIndex > maxRowIndex.Value))
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

    public SortedList<int, DxCell> GetCells()
    {
      var cells = new SortedList<int, DxCell>();

      foreach (var cell in this.Cells.Values)
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
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class DxColumnSet : Node
  {
    private SortedList<int, DxColumn> _columns;
    public SortedList<int, DxColumn> Columns
    {
      get
      {
        if (_columns == null)
          _columns = new SortedList<int, DxColumn>();
        return _columns;
      }
    }

    public DxColumnSet()
    {
      this.DxObject = this;
      _columns = new SortedList<int, DxColumn>();
    }

    public SortedList<int, DxCell> GetCells()
    {
      var cells = new SortedList<int, DxCell>();

      foreach (var col in _columns.Values)
      {
        int seqCount = 0;
        foreach (var cell in col.Cells.Values)
        {
          if (seqCount == 0)
            cell.IsFirstInSequence = true;

          cells.Add(cells.Count, cell);
          seqCount++;
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
  }
}

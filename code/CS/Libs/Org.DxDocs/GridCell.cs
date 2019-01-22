using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using Org.GS;

namespace Org.DxDocs
{
  public class GridCell
  {
    public Cell Cell {
      get;
      set;
    }
    public int Row {
      get;
      private set;
    }
    public int Col {
      get;
      private set;
    }

    private object _value;
    public string Value {
      get {
        return Get_Value();
      }
    }

    public GridCell(int row, int col, object value)
    {
      this.Row = row;
      this.Col = col;
      _value = value;
    }

    private string Get_Value()
    {
      if (_value == null)
        return String.Empty;

      return _value.ToString().Trim();
    }
  }
}

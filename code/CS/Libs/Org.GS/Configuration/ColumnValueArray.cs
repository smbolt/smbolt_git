using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection; 
using Org.GS;

namespace Org.GS.Configuration
{
  public class ColumnValueArray
  {
    private object _rowObject;
    private GridView _gv;
    private string[] _columnValues;

    public ColumnValueArray(object o, GridView gv)
    {
      _rowObject = o;
      _gv = gv;
      _columnValues = new string[_gv.Count]; 
      PlaceValuesInColumns();
    }

    public string[] ToArray()
    {
      return _columnValues;
    }

    private void PlaceValuesInColumns()
    {
      int colIndex = 0;

      foreach (var gridCol in _gv)
      {
        string colValue = String.Empty;
        PropertyInfo pi = _rowObject.GetType().GetProperty(gridCol.ColumnName);
        if (pi != null)
        {
          object pv = pi.GetValue(_rowObject);
          if (pv != null)
            colValue = pv.ToString().Trim();
        }
        _columnValues[colIndex++] = colValue; 
      }
    }
  }
}

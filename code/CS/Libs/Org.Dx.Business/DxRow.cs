using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  public class DxRow : Dictionary<int, DxCell>
  {
    public string RowValues { get { return Get_RowValues(); } }    

    private string Get_RowValues()
    {
      StringBuilder sb = new StringBuilder();

      if (this.Count == 0)
        return "No cells";

      sb.Append("Column     Value" + g.crlf2); 

      foreach (var kvp in this)
      {
        sb.Append(kvp.Key.ToString("0000") + "       " + kvp.Value.TextValue + g.crlf);
      }

      return sb.ToString();
    }

    public string GetTextValue(int columnIndex)
    {
      if (!this.ContainsKey(columnIndex))
        return String.Empty;

      return this[columnIndex].TextValue;
    }

    public int? GetInt32Value(int columnIndex)
    {
      if (!this.ContainsKey(columnIndex))
        return (int?)null;

      if (!this.IsNumeric(columnIndex))
        return (int?)null;
      
      return this[columnIndex].Int32Value;
    }

    public decimal? GetDecimalValue(int columnIndex)
    {
      if (!this.ContainsKey(columnIndex))
        return (decimal?)null;

      if (!this.IsNumeric(columnIndex))
        return (decimal?)null;

      return this[columnIndex].DecimalValue;
    }

    public bool? GetBooleanValue(int columnIndex)
    {
      if (!this.ContainsKey(columnIndex))
        return (bool?)null;

      if (!this.IsBoolean(columnIndex))
        return (bool?)null;

      return this[columnIndex].BooleanValue;
    }

    public DateTime? GetDateTimeValue(int columnIndex)
    {
      if (!this.ContainsKey(columnIndex))
        return (DateTime?)null;

      if (!this.IsBoolean(columnIndex))
        return (DateTime?)null;

      return this[columnIndex].DateTimeValue;
    }

    public bool IsBoolean(int columnIndex)
    { 
      if (!this.ContainsKey(columnIndex))
        return false;

      return this[columnIndex].IsBoolean;
    } 

    public bool IsDateTime(int columnIndex)
    {
      if (!this.ContainsKey(columnIndex))
        return false;

      return this[columnIndex].IsDateTime;
    }

    public bool IsEmpty(int columnIndex)
    {
      if (!this.ContainsKey(columnIndex))
        return false;

      return this[columnIndex].IsDateTime;
    }

    public bool IsNumeric(int columnIndex)
    {
      if (!this.ContainsKey(columnIndex))
        return false;

      return this[columnIndex].IsNumeric;
    }

    public bool IsText(int columnIndex)
    {
      if (!this.ContainsKey(columnIndex))
        return false;

      return this[columnIndex].IsText;
    }

    public bool IsNull(int columnIndex)
    {
      if (!this.ContainsKey(columnIndex))
        return false;

      return this[columnIndex].RawValue == null;
    }
  }
}

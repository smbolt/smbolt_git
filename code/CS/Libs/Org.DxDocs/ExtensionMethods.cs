using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using Org.Dx.Business;

namespace Org.DxDocs
{
  public static class ExtensionMethods
  {
    public static DxCell ToDxCell(this Cell cell)
    {
      var dxCell = new DxCell();

      if (cell.Value.IsDateTime)
        dxCell.RawValue = cell.Value.DateTimeValue;
      else
        if (cell.Value.IsNumeric)
        dxCell.RawValue = cell.Value.NumericValue;
      else
          if (cell.Value.IsBoolean)
        dxCell.RawValue = cell.Value.BooleanValue;
      else
            if (cell.Value.IsEmpty)
        dxCell.RawValue = null;
      else
        dxCell.RawValue = cell.Value.TextValue;

      dxCell.RowIndex = cell.RowIndex;
      dxCell.ColumnIndex = cell.ColumnIndex;
      //dxCell.IsBoolean = cell.Value.IsBoolean;
      //dxCell.IsDateTime = cell.Value.IsDateTime;
      //dxCell.IsEmpty = cell.Value.IsEmpty;
      //dxCell.IsNumeric = cell.Value.IsNumeric;
      //dxCell.IsText = cell.Value.IsText;

      return dxCell;
    }
  }
}

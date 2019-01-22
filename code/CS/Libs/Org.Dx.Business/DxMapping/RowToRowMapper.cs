using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class RowToRowMapper : MapperBase
  {
    public RowToRowMapper(MapEngine mapEngine, DxWorksheet sourceWorksheet, DxMap dxMap)
      : base(mapEngine, sourceWorksheet, dxMap) { }

    public override List<DxRowSet> MapData(DxWorkbook targetWorkbook)
    {
      try
      {
        var targetWorksheets = new List<DxRowSet>();
        var targetWorksheet = new DxWorksheet(targetWorkbook);
        targetWorksheet.WorksheetName = base.SourceWorksheet.WorksheetName;

        int destRowIndex = 0;

        foreach (var srcRow in base.SourceWorksheet.Rows.Values)
        {
          if (!base.DxMap.DxFilterSet.MatchesRow(srcRow))
            continue;

          if (targetWorksheet.Rows.Count != 0)
            destRowIndex++;

          foreach (var mapItem in base.DxMap.DxMapItemSet.Values)
          {
            mapItem.Initialize();
            string report = mapItem.Report;

            if (mapItem.DebugBreak)
            {
              Debugger.Break();
              mapItem.DebugBreak = false;
            }

            if (mapItem.MapCommand == "SkipRest")
              break;

            mapItem.DxObject = srcRow;
            mapItem.SheetIndex = base.SourceWorksheet.SheetIndex;

            if (mapItem.MapCommand == "SkipRestOnCondition")
            {
              bool skipRest = mapItem.IncludeBasedOnCondition();
              if (skipRest)
                break;
            }

            if (!mapItem.IncludeBasedOnCondition())
              continue;

            string srcCellValue = mapItem.SrcCellValue;

            var dstCell = new DxCell();
            dstCell.RowIndex = -1;
            dstCell.ColumnIndex = -1;

            dstCell.DxMapItem = mapItem;

            if (mapItem.CreatesVariable)
            {
              string variableName = mapItem.Dest;
              MapEngine.AddVariable(mapItem, variableName, srcCellValue);
              continue;
            }

            dstCell.RawValue = srcCellValue;
            dstCell.RowIndex = destRowIndex;
            dstCell.ColumnIndex = mapItem.DestCol;

            dstCell.Name = mapItem.Name;
            if (dstCell.Name.Contains("["))
            {
              int pos = dstCell.Name.IndexOf("[");
              dstCell.Name = dstCell.Name.Substring(0, pos).Trim();
            }

            if (!targetWorksheet.Rows.ContainsKey(dstCell.RowIndex))
              targetWorksheet.Rows.Add(dstCell.RowIndex, new DxRow(targetWorksheet));

            DxRow dxRow = targetWorksheet.Rows[dstCell.RowIndex];

            dxRow.Add(dstCell.ColumnIndex, dstCell);
          }
        }

        if (targetWorksheet.Rows.Count > 0)
          targetWorksheets.Add(targetWorksheet);

        return targetWorksheets;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to map data from DxWorksheet named '" + base.SourceWorksheet.WorksheetName + "' " +
                            "using DxMap named '" + base.DxMap.Name, ex);
      }
    }
  }
}

﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class SheetToSheetMapper : MapperBase
  {
    public SheetToSheetMapper(MapEngine mapEngine, DxWorksheet sourceWorksheet, DxMap dxMap)
      : base(mapEngine, sourceWorksheet, dxMap) { }

    public override List<DxRowSet> MapData(DxWorkbook targetWorkbook)
    {
      try
      {
        var targetWorksheets = new List<DxRowSet>();
        var targetWorksheet = new DxWorksheet(targetWorkbook);
        targetWorksheet.WorksheetName = base.SourceWorksheet.WorksheetName;

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

          mapItem.DxObject = base.SourceWorksheet as DxRowSet;
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

          if (!mapItem.ProcessThisMapItem(srcCellValue))
            continue;

          var dstCell = new DxCell();
          dstCell.RowIndex = -1;
          dstCell.ColumnIndex = -1;

          dstCell.DxMapItem = mapItem;

          if (mapItem.CreatesVariable)
          {
            string variableName = mapItem.Dest.Trim();
            MapEngine.AddVariable(mapItem, variableName, srcCellValue);
            continue;
          }

          dstCell.RawValue = srcCellValue;
          dstCell.RowIndex = mapItem.DestRow;
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

          if (dxRow.Cells.ContainsKey(dstCell.ColumnIndex))
          {
            throw new Exception("The DxCell at row index " + dstCell.RowIndex.ToString() + " and column index " + dstCell.ColumnIndex.ToString() + " already exists " +
                                "in the destination RowSet named '" + targetWorksheet.WorksheetName + "'.");
          }
          dxRow.Add(dstCell.ColumnIndex, dstCell);
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

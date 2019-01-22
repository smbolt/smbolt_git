using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.Dx.Business.TextProcessing;
using Org.GS;

namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, WrapperElement = "DxProcessingRoutineSet")]
  public class DxProcessingRoutine
  {
    [XMap(IsKey = true, IsRequired = true)]
    public string Name {
      get;
      set;
    }

    [XMap]
    public string NewSheetIdentifier {
      get;
      set;
    }

    [XMap]
    public int ColumnIdentifier {
      get;
      set;
    }

    [XMap]
    public string IncludeSheetFilter {
      get;
      set;
    }

    [XMap(DefaultValue = "")]
    public string SplitSheetName {
      get;
      set;
    }

    [XMap]
    public bool DiscardFirst {
      get;
      set;
    }

    public DxWorkbook RunRoutine(DxWorkbook srcWb, DxMapSet mapSet)
    {
      if (mapSet.PreProcessingRoutine.IsBlank())
        return srcWb;

      var newWb = new DxWorkbook();
      newWb.FilePath = srcWb.FilePath;
      newWb.IsMapped = true;

      foreach (var processRoutine in mapSet.DxProcessingRoutineSet.Values)
      {
        switch (processRoutine.Name)
        {
          case "FilterSheets":



          case "SplitSheets":
            var newSrcWb = new DxWorkbook();
            var finalWs = new DxWorksheet(newSrcWb);
            int wsCounter = 0;
            foreach (var wrkSheet in srcWb)
            {
              var newSrcWs = new DxWorksheet(newSrcWb);
              finalWs = newSrcWs;
              newSrcWs.WorksheetName = "Sheet" + wsCounter.ToString();
              int rowCounter = 0;
              foreach (var row in wrkSheet.Value.Rows)
              {
                if (newSrcWs.Rows.Count == 0)
                {
                  newSrcWs.Rows.Add(rowCounter,row.Value);
                  rowCounter++;
                  continue;
                }
                if (processRoutine.ColumnIdentifier > row.Value.Cells.Count)
                  throw new Exception("The column number to be checked is greater than the number of columns in the row.");
                var cellValue = row.Value.Cells[processRoutine.ColumnIdentifier].ValueOrDefault.DbToString();
                if (cellValue.IsBlank() || !cellValue.StartsWith(processRoutine.NewSheetIdentifier))
                {
                  newSrcWs.Rows.Add(rowCounter,row.Value);
                  rowCounter++;
                  continue;
                }
                else
                {
                  newSrcWb.Add(newSrcWs.WorksheetName, newSrcWs);
                  wsCounter++;
                  rowCounter = 0;
                  newSrcWs = new DxWorksheet(newSrcWb);
                  finalWs = newSrcWs;
                  newSrcWs.WorksheetName = "Sheet" + wsCounter.ToString();
                  newSrcWs.Rows.Add(rowCounter,row.Value);
                  rowCounter++;
                  continue;
                }
              }
            }

            newSrcWb.AddSheet(finalWs);
            return newSrcWb;
        }
      }

      return srcWb;
    }
  }
}

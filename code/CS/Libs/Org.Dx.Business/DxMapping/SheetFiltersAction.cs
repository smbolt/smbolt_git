using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class FilterSheetsAction : DxActionBase, IDxAction
  {
    public FilterSheetsAction(MapEngine mapEngine, DxActionParms dxActionParms) : base(mapEngine, dxActionParms) { }

    public override DxWorkbook Execute(DxWorkbook srcWb, DxMapSet mapSet)
    {
      try
      {
        if (srcWb == null)
          throw new Exception("The source workbook is null.");

        base.SourceWorkbook = srcWb;
        base.DxMapSet = mapSet;

        var filter = base.DxMapSet.DxFilterSet.Where(f => f.Name == base.DxActionParms.FilterName).FirstOrDefault();

        if (filter == null)
          throw new Exception("DxFilter '" + base.DxActionParms.FilterName + "' not found.");

        base.TargetWorkbook = new DxWorkbook();
        base.TargetWorkbook.FilePath = base.SourceWorkbook.FilePath;

        foreach (var ws in base.SourceWorkbook.Values)
        {
          if (filter.MatchesWorksheet(ws))
            base.TargetWorkbook.AddSheet(ws);
        }

        base.FireOnNewWorkbookVersion(base.TargetWorkbook);

        return base.TargetWorkbook;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to execute a FilterSheetsAction.", ex);
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class DataMappingAction : DxActionBase, IDxAction
  {
    public DataMappingAction(MapEngine mapEngine, DxActionParms dxActionParms) : base(mapEngine, dxActionParms) { }

    public override DxWorkbook Execute(DxWorkbook srcWb, DxMapSet mapSet)
    {
      try
      {
        if (srcWb == null)
          throw new Exception("The source workbook is null.");

        base.SourceWorkbook = srcWb;
        base.DxMapSet = mapSet;
        base.TargetWorkbook = new DxWorkbook();
        base.TargetWorkbook.FilePath = base.SourceWorkbook.FilePath;

        foreach (var srcWs in srcWb.Values)
        {
          var map = base.DxMapSet.GetMap(srcWs);
          var mapper = DataMapperFactory.GetDataMapper(base.MapEngine, srcWs, map);
          var tgtRowSets = mapper.MapData(base.TargetWorkbook);

          foreach (var tgtRowSet in tgtRowSets)
          {
            var targetWs = new DxWorksheet(base.TargetWorkbook, tgtRowSet);
            targetWs.WorksheetName = "Sheet[" + base.TargetWorkbook.Count.ToString() + "]";
            targetWs.EnsureParentage();
            base.TargetWorkbook.AddSheet(targetWs);
          }
        }

        base.FireOnNewWorkbookVersion(base.TargetWorkbook);

        return base.TargetWorkbook;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to execute a DataMappingAction.", ex);
      }
    }
  }
}

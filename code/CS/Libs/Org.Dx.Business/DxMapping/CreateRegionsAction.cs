using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class CreateRegionsAction : DxActionBase, IDxAction
  {
    public CreateRegionsAction(MapEngine mapEngine, DxActionParms dxActionParms) : base(mapEngine, dxActionParms) { }

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


        foreach (var ws in base.SourceWorkbook.Values)
        {
          base.DxMapSet.DxRegionSet.NextStartRowIndex = 0;
          foreach (var regionName in base.DxActionParms.RegionNames)
          {
            var dxRegion = base.DxMapSet.DxRegionSet.Values.Where(r => r.Name == regionName).FirstOrDefault();

            if (dxRegion == null)
              throw new Exception("DxRegion '" + regionName + "' not found.");

            var regions = dxRegion.GetRegions(ws);

            foreach (var region in regions)
            {
              var regionWs = new DxWorksheet(base.TargetWorkbook, region);
              regionWs.WorksheetName = dxRegion.Name + "[" + base.TargetWorkbook.Count.ToString() + "]";
              regionWs.EnsureParentage();
              regionWs.SetNodeData(dxRegion.NodeData);
              base.TargetWorkbook.AddSheet(regionWs);
            }
          }
        }

        base.FireOnNewWorkbookVersion(base.TargetWorkbook);

        return base.TargetWorkbook;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to execute a CreateRegionsAction.", ex);
      }
    }
  }
}

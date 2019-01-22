using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class DataMapperFactory
  {
    public static IDataMapper GetDataMapper(MapEngine mapEngine, DxWorksheet sourceWorksheet, DxMap dxMap)
    {
      switch (dxMap.DxMapType)
      {
        case DxMapType.SheetToSheet:
          return new SheetToSheetMapper(mapEngine, sourceWorksheet, dxMap);

        case DxMapType.RowToSheet:
          return new RowToSheetMapper(mapEngine, sourceWorksheet, dxMap);

        case DxMapType.RowToRow:
          return new RowToRowMapper(mapEngine, sourceWorksheet, dxMap);

        default:
          throw new Exception("The map type '" + dxMap.DxMapType.ToString() + "' is not implemented in the DxMapperFactory.");
      }
    }
  }
}

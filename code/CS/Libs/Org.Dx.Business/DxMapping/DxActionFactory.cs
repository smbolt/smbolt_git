using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class DxActionFactory
  {
    public static IDxAction GetDxAction(MapEngine mapEngine, DxActionParms dxActionParms)
    {
      switch (dxActionParms.DxActionType)
      {
        case DxActionType.FilterSheetsAction:
          return new FilterSheetsAction(mapEngine, dxActionParms);

        case DxActionType.CreateRegionsAction:
          return new CreateRegionsAction(mapEngine, dxActionParms);

        case DxActionType.DataMappingAction:
          return new DataMappingAction(mapEngine, dxActionParms);

        default:
          throw new NotImplementedException("The DxActionType '" + dxActionParms.DxActionType.ToString() + "' is not implemented.");
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class MapperBase : IDataMapper  {

    public MapEngine MapEngine { get; set; }
    public DxWorksheet SourceWorksheet { get; set; }
    public DxMap DxMap { get; set; }

    public MapperBase(MapEngine mapEngine, DxWorksheet sourceWorksheet, DxMap dxMap)
    {
      this.MapEngine = mapEngine;
      this.SourceWorksheet = sourceWorksheet;
      this.DxMap = dxMap;
      this.DxMap.Initialize();
      MapEngine.LocalVariables.Clear();
    }

    public virtual List<DxRowSet> MapData(DxWorkbook targetWorkbook)
    {
      throw new NotImplementedException("The MapData method must be implemented in the derived class.");
    }
  }
}

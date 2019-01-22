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
  [XMap(XType = XType.Element, CollectionElements = "DxMap", WrapperElement = "DxMapSet")]
  public class DxMapSet : Dictionary<string, DxMap>
  {
    [XMap(IsRequired = true )]
    public string Name {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public ExtractionMap ExtractionMap {
      get;
      set;
    }

    [XMap(XType = XType.Element, WrapperElement = "DxCommandSet", CollectionElements = "DxCommand")]
    public DxCommandSet DxCommandSet {
      get;
      set;
    }

    [XMap]
    public string PreProcessingRoutine {
      get;
      set;
    }

    [XMap(XType = XType.Element, WrapperElement = "DxMapSet", CollectionElements = "DxProcessingRoutine")]
    public DxProcessingRoutineSet DxProcessingRoutineSet {
      get;
      set;
    }

    [XMap(XType = XType.Element, WrapperElement = "DxMapSet", CollectionElements = "DxRegion")]
    public DxRegionSet DxRegionSet {
      get;
      set;
    }

    [XMap]
    public string ColumnIndexMapName {
      get;
      set;
    }

    [XMap(XType = XType.Element, WrapperElement = "DxMapSet", CollectionElements = "DxFilterSet")]
    public DxFilterSet DxFilterSet {
      get;
      set;
    }

    [XMap]
    public string FullMapPath {
      get;
      set;
    }

    public DxMapType DxMapType {
      get {
        return Get_DxMapType();
      }
    }

    public DxMapSet()
    {
      this.DxCommandSet = new DxCommandSet();
      this.DxRegionSet = new DxRegionSet();
      this.DxProcessingRoutineSet = new DxProcessingRoutineSet();
      this.DxFilterSet = new DxFilterSet();
      this.FullMapPath = String.Empty;
    }

    public void AutoInit()
    {
      if (this.ColumnIndexMapName.IsBlank())
        this.ColumnIndexMapName = "ColumnIndexMap";

      foreach (var map in this.Values)
      {
        foreach (var mapItem in map.DxMapItemSet.Values)
        {
          mapItem.DxMap = map;
        }

        if (map.DxRegionSet != null)
        {
          foreach (var region in map.DxRegionSet.Values)
          {
            foreach (var regionRow in region.DxRegionRowSet.Values)
            {
              foreach (var mapItem in regionRow.Values)
              {
                mapItem.DxMap = map;
              }
            }
          }
        }
      }
    }

    public DxMap GetMap(DxRowSet srcRowSet)
    {
      try
      {
        if (srcRowSet.NodeData.ContainsKey("MapName"))
        {
          string mapName = srcRowSet.NodeData["MapName"].ToString();
          var map = this.Values.Where(m => m.Name == mapName).FirstOrDefault();
          if (map == null)
            throw new Exception("The MapName specified in the NodeData collection of the DxRowSet with SheetName '" +
                                srcRowSet.WorksheetName + "' does not exist. The MapName is '" + mapName + "'.");
          return map;
        }

        // if it's the only map, use it...
        if (this.Count == 1)
          return this.Values.First();

        // see if map can be matched based on DataSource
        foreach (var map in this.Values)
        {
          // if the map has a DataSource property tying it to a sheet name (typically a region name)
          if (map.DataSource.IsNotBlank())
          {
            string mapDataSource = map.DataSource;
            if (mapDataSource.EndsWith("*"))
            {
              mapDataSource = mapDataSource.Substring(0, mapDataSource.Length - 1);
              if (srcRowSet.WorksheetName.ToLower().Contains(mapDataSource.ToLower()))
                return map;
            }
            else
            {
              if (mapDataSource.ToLower() == srcRowSet.WorksheetName.ToLower())
                return map;
            }
          }
        }

        // attempt to match based on the maps FilterSet
        foreach (var map in this.Values)
        {
          if (map.DxFilterSet.Count == 0)
            continue;

          if (map.DxFilterSet.UseThisMap(map, srcRowSet))
            return map;
        }

        throw new Exception("No map could be found to match against DxRowSet with sheet name '" + srcRowSet.WorksheetName + "'.");

      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred attempting to get the map for a specific sheet.", ex);
      }
    }

    public void Validate()
    {
      try
      {

      }
      catch (Exception ex)
      {
        throw new Exception("An error occurred attempting to validate the Map Set. There are an invalid number of maps in the Map Set.", ex);
      }
    }

    private DxMapType Get_DxMapType()
    {
      var perUnitMaps = this.Values.Where(m => m.MapTiming == MapTiming.PerUnit);

      if (perUnitMaps.Count() == 0)
        return DxMapType.Invalid;

      return perUnitMaps.First().DxMapType;
    }

    private DxProcessingRoutineSet Get_DxProcessingRoutineSet()
    {
      var dxProcessingRoutineSet = new DxProcessingRoutineSet();

      return dxProcessingRoutineSet;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "DxMapItem", XType = XType.Element, WrapperElement = "DxMapItemSet")]
  public class DxMap
  {
    [XMap(IsRequired = true, IsKey = true)]
    public string Name {
      get;
      set;
    }

    [XMap(IsRequired = true)]
    public DxMapType DxMapType {
      get;
      set;
    }

    [XMap(DefaultValue = "PerUnit")]
    public MapTiming MapTiming {
      get;
      set;
    }

    [XMap]
    public string DataSource {
      get;
      set;
    }

    [XMap(DefaultValue = "None")]
    public SheetControl SheetControl {
      get;
      set;
    }

    [XMap(XType = XType.Element, WrapperElement = "DxFilterSet", CollectionElements = "DxFilter")]
    public DxFilterSet DxFilterSet {
      get;
      set;
    }

    [XMap(XType = XType.Element, WrapperElement = "DxMapItemSet", CollectionElements = "DxMapItem")]
    public DxMapItemSet DxMapItemSet {
      get;
      set;
    }

    [XMap(XType = XType.Element, WrapperElement = "DxRegionSet", CollectionElements = "DxRegion")]
    public DxRegionSet DxRegionSet {
      get;
      set;
    }

    public ColumnIndexMap ColumnIndexMap {
      get;
      set;
    }

    public DxMap()
    {
      this.DxFilterSet = new DxFilterSet();
      this.DxMapItemSet = new DxMapItemSet();
      this.DxRegionSet = new DxRegionSet();
      this.DataSource = String.Empty;
    }

    public DxRowSet FilterRowSet(DxRowSet rs)
    {
      try
      {
        var rowSet = new DxRowSet();
        rowSet.DxWorkbook = rs.DxWorkbook;

        foreach (var r in rs.Rows)
        {
          if (!r.Value.ExcludeBasedOnFilterSet(this.DxFilterSet))
          {
            rowSet.Rows.Add(r.Key, r.Value);
          }
        }

        rowSet.EnsureParentage();

        return rowSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to filter a DxRowSet.", ex);
      }
    }

    public List<DxRowSet> FilterWorkbook(DxWorkbook wb)
    {
      var rowSets = new List<DxRowSet>();
      var rowSet = new DxRowSet();

      return rowSets;
    }

    public void ValidateWorkbook(DxWorkbook wb)
    {
      try
      {
        // run validation based on map-based instructions
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to validate the DxWorkbook.", ex);
      }
    }

    public void Initialize()
    {
      foreach (var mapItem in this.DxMapItemSet.Values)
      {
        mapItem.Initialize();
      }
    }
  }
}


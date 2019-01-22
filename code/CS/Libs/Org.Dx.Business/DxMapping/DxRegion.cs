using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "DxRegionRow")]
  public class DxRegion
  {
    [XMap(IsKey =  true, IsRequired = true)]
    public string Name {
      get;
      set;
    }

    [XMap(DefaultValue = "99999")]
    public int RegionsPerSheet {
      get;
      set;
    }

    [XMap(DefaultValue = "Vertical")]
    public Direction Direction {
      get;
      set;
    }

    public DxRowSet DxRowSet {
      get;
      set;
    }

    [XMap(XType = XType.Element, CollectionElements = "DxMap")]
    public DxMapSet DxMapSet {
      get;
      set;
    }

    [XMap (DefaultValue="NotUsed")]
    public DxRegionExtractMethod DxRegionExtractMethod {
      get;
      set;
    }

    [XMap(XType = XType.Element, CollectionElements = "DxRegionRow", WrapperElement = "DxRegionRowSet")]
    public DxRegionRowSet DxRegionRowSet {
      get;
      set;
    }

    [XMap]
    public string TopLeftCell {
      get;
      set;
    }

    [XMap]
    public string TopRightCell {
      get;
      set;
    }

    [XMap]
    public string BottomRightCell {
      get;
      set;
    }

    public CellDefinition TopLeftCellDefinition {
      get;
      set;
    }
    public CellDefinition TopRightCellDefinition {
      get;
      set;
    }
    public CellDefinition BottomRightCellDefinition {
      get;
      set;
    }

    [XMap (DefaultValue = "True")]
    public bool Advance {
      get;
      set;
    }

    [XMap]
    public string TopRowSpec {
      get;
      set;
    }

    [XMap]
    public string BottomRowSpec {
      get;
      set;
    }

    [XMap]
    public string LeftColSpec {
      get;
      set;
    }

    [XMap]
    public string RightColSpec {
      get;
      set;
    }

    [XMap]
    public string NodeData {
      get;
      set;
    }

    [XMap(MyParent = true)]
    public DxRegionSet DxRegionSet {
      get;
      set;
    }

    public int TopIndex {
      get;
      private set;
    }
    public int BottomIndex {
      get;
      private set;
    }
    public int LeftIndex {
      get;
      private set;
    }
    public int RightIndex {
      get;
      private set;
    }

    public string Report {
      get {
        return Get_Report();
      }
    }

    [XMap]
    public string SheetSelect {
      get;
      set;
    }

    public MapEngine MapEngine {
      get;
      set;
    }

    public bool IndicesEstablished {
      get;
      set;
    }


    [XParm(Name = "parent", ParmSource = XParmSource.Parent)]
    public DxRegion(DxRegionSet parent)
    {
      this.DxRowSet = null;
      this.DxRegionSet = parent;
      this.DxRegionRowSet = new DxRegionRowSet();
      this.IndicesEstablished = false;
    }

    public void AutoInit()
    {
      if (this.DxRegionRowSet == null || this.DxRegionRowSet.Count == 0)
        return;

      foreach (var dxRegionRow in this.DxRegionRowSet.Values)
      {
        if (dxRegionRow.DxRegionExtractMethod == DxRegionExtractMethod.DefaultToRegion)
        {
          dxRegionRow.DxRegionExtractMethod = this.DxRegionExtractMethod;
        }
      }

      if (this.TopRowSpec.IsBlank() && this.BottomRowSpec.IsBlank() && this.LeftColSpec.IsBlank() && this.RightColSpec.IsBlank())
        throw new Exception("The specifications for the region boundaries are all blank.  At least one boundary must be defined.");
    }

    public List<DxRowSet> GetRegions(DxRowSet srcRowSet)
    {
      try
      {
        if (this.Direction == Direction.Horizontal)
          throw new Exception("Extracting DxRegions horizontally is not yet implemented.");

        var regions = new List<DxRowSet>();

        if (!this.Advance)
          this.DxRegionSet.NextStartRowIndex = 0;

        while (true)
        {
          this.TopIndex = srcRowSet.LocateRowIndex(this.TopRowSpec, RectangleSide.Top, this.DxRegionSet.NextStartRowIndex, 0, srcRowSet.LastUsedColumnIndex);

          if (this.TopIndex == -1)
            break;

          this.BottomIndex = srcRowSet.LocateRowIndex(this.BottomRowSpec, RectangleSide.Bottom, this.TopIndex, 0, srcRowSet.LastUsedColumnIndex);

          if (this.BottomIndex == -1)
            break;

          this.LeftIndex = srcRowSet.LocateColumnIndex(this.LeftColSpec, RectangleSide.Left, 0, this.TopIndex, this.BottomIndex);
          this.RightIndex = srcRowSet.LocateColumnIndex(this.RightColSpec, RectangleSide.Right, 0, this.TopIndex, this.BottomIndex);
          this.IndicesEstablished = true;

          ValidateIndices(srcRowSet);

          this.DxRegionSet.NextStartRowIndex = this.BottomIndex + 1;

          var tgtRowSet = new DxRowSet();

          for (int r = this.TopIndex; r < this.BottomIndex + 1; r++)
          {
            var srcRow = srcRowSet.Rows.Values[r];
            var tgtRow = new DxRow(tgtRowSet);
            for (int c = this.LeftIndex; c < this.RightIndex + 1; c++)
            {
              if (srcRow.Cells.ContainsKey(c))
              {
                var cell = srcRow.Cells[c].Clone(tgtRow);
                cell.RowIndex = tgtRowSet.Rows.Count;
                cell.ColumnIndex = tgtRow.Cells.Count;
                tgtRow.Add(cell.ColumnIndex, cell);
              }
            }

            if (tgtRow.Cells.Count > 0)
              tgtRowSet.Rows.Add(tgtRowSet.Rows.Count, tgtRow);
          }

          if (tgtRowSet.Rows.Count > 0)
            regions.Add(tgtRowSet);

          if (regions.Count >= this.RegionsPerSheet)
            break;
        }

        return regions;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to extract regions from a DxRowSet - region name is '" +
                            this.Name + ", DxRowSet name is '" + srcRowSet.WorksheetName + "'.", ex);
      }
    }

    public void SetRegionRectangle(DxRowSet rowSet)
    {
      try
      {
        this.TopIndex = rowSet.LocateRowIndex(this.TopRowSpec, RectangleSide.Top, 0, 0, rowSet.LastUsedColumnIndex);
        this.BottomIndex = rowSet.LocateRowIndex(this.BottomRowSpec, RectangleSide.Bottom, 0, 0, rowSet.LastUsedColumnIndex);
        this.LeftIndex = rowSet.LocateColumnIndex(this.LeftColSpec, RectangleSide.Left, 0, 0, rowSet.LastUsedRowIndex);
        this.RightIndex = rowSet.LocateColumnIndex(this.RightColSpec, RectangleSide.Right, 0, 0, rowSet.LastUsedRowIndex);
        this.IndicesEstablished = true;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine the RegionRectangle.", ex);
      }
    }


    private void ValidateIndices(DxRowSet srcRowSet)
    {
      if (this.TopIndex < 0 || this.TopIndex > srcRowSet.LastUsedRowIndex)
        throw new Exception("The DxRegion.TopIndex is invalid '" + this.TopIndex.ToString() + "'.  It must be greater than 0 and less than or equal to " +
                            "the source DxRowSet last row index which is '" + srcRowSet.LastUsedRowIndex.ToString() + "'. DxRegion name is '" + this.Name + "'.");

      if (this.BottomIndex > srcRowSet.LastUsedRowIndex)
        throw new Exception("The DxRegion.BottomIndex is invalid '" + this.BottomIndex.ToString() + "'.  It must be less than or equal to " +
                            "the source DxRowSet last row index which is '" + srcRowSet.LastUsedRowIndex.ToString() + "'. DxRegion name is '" + this.Name + "'.");

      if (this.TopIndex > this.BottomIndex)
        throw new Exception("The DxRegion.TopIndex '" + this.TopIndex.ToString() + "' cannot be greater than the DxRegion.BottomIndex '" +
                            this.BottomIndex.ToString() + "'. DxRegion name is '" + this.Name + "'.");

      if (this.LeftIndex < 0 || this.LeftIndex > srcRowSet.LastUsedColumnIndex)
        throw new Exception("The DxRegion.LeftIndex is invalid '" + this.LeftIndex.ToString() + "'.  It must be greater than 0 and less than or equal to " +
                            "the source DxRowSet last column index which is '" + srcRowSet.LastUsedColumnIndex.ToString() + "'. DxRegion name is '" + this.Name + "'.");

      if (this.RightIndex > srcRowSet.LastUsedColumnIndex)
        throw new Exception("The DxRegion.RightIndex is invalid '" + this.RightIndex.ToString() + "'.  It must be less than or equal to " +
                            "the source DxRowSet last column index which is '" + srcRowSet.LastUsedColumnIndex.ToString() + "'. DxRegion name is '" + this.Name + "'.");

      if (this.LeftIndex > this.RightIndex)
        throw new Exception("The DxRegion.LeftIndex '" + this.LeftIndex.ToString() + "' cannot be greater than the DxRegion.RightIndex '" +
                            this.RightIndex.ToString() + "'. DxRegion name is '" + this.Name + "'.");
    }

    private string Get_Report()
    {
      return this.Name;
    }
  }
}

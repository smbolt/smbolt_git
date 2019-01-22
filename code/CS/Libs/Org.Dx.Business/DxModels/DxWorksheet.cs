using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;
namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "DxWorksheet", XType = XType.Element)] 
  public class DxWorksheet : DxRowSet
  {
    [XMap(IsKey = true)]
    public new string WorksheetName
    {
      get { return base.WorksheetName; }
      set { base.WorksheetName = value; }
    }

    [XMap(DefaultValue = "False")]
    public override bool IsHidden
    {
      get { return base.IsHidden; }
      set { base.IsHidden = value; }
    }

    [XMap(XType = XType.Element)]
    public XElement DxCellArray
    {
      get 
      { 
        return Get_DxCellArray(); 
      }
      set 
      { 
        Set_DxCellArray(value);
        this.Initialize();
      }
    }
    
    [XParm(ParmSource = XParmSource.Parent, Name = "parent")]
    public DxWorksheet(DxWorkbook parent = null) 
    {
      this.DxObject = this;
      this.DxWorkbook = parent;
    }

    public DxWorksheet(DxWorkbook parent, string sheetName)
    {
      this.DxObject = this;
      this.DxWorkbook = parent;
      this.WorksheetName = sheetName;
    }

    public DxWorksheet(DxWorkbook parent, int rows, int cols)
    {
      this.DxObject = this;
      this.DxWorkbook = parent;
      this.WorksheetName = String.Empty;
      this.DxCells = new DxCell[rows, cols];
    }

    public DxWorksheet(DxWorkbook parent, DxRowSet dxRowSet)
    {
      this.DxObject = this;
      this.DxWorkbook = parent;

      for (int r = 0; r < dxRowSet.Rows.Count; r++)
      {
        int rowIndex = dxRowSet.Rows.Keys.ElementAt(r);
        var dxRow = dxRowSet.Rows.Values.ElementAt(r);
        this.Rows.Add(rowIndex, dxRow);
      }
    }
    
    public void AutoInit()
    {
      base.Initialize();
    }

    public DxCell GetCellByName(string cellName)
    {
      if (this.DxCells == null && this.Rows.Count == 0)
        return null;

      if (this.Rows.Count > 0)
      {
        for(int r = 0; r < this.Rows.Count; r++)
        {
          var row = this.Rows.ElementAt(r).Value; 
          for (int c = 0; c < row.Cells.Count; c++)
          {
            var cell = row.Cells.ElementAt(c).Value;
            if (cell.Name == cellName)
              return cell;
          }

        }
      }

      if (this.DxCells == null)
        return null;

      int rows = this.DxCells.GetLength(0);
      int cols = this.DxCells.GetLength(1);

      for (int r = 0; r < rows; r++)
      {
        for (int c = 0; c < cols; c++)
        {
          var cell = this.DxCells[r, c];
          if (cell != null)
          {
            if (cell.Name == cellName)
              return cell;
          }
        }
      }

      return null;
    }

    public void ReplaceWorksheet(DxRowSet rowSet)
    {
      this.Rows.Clear();
      
      foreach (var row in rowSet.Rows)
      {
        this.Rows.Add (row.Key, row.Value);
      }
    }

    public DxWorksheet Clone(DxWorkbook cloneWbParent)
    {
      try
      {
        var clone = new DxWorksheet(cloneWbParent, this.RowCount, this.ColumnCount);    

        clone.IsHidden = this.IsHidden;
        foreach (var kvpRow in this.Rows)
        {
          clone.Rows.Add(kvpRow.Key, kvpRow.Value.Clone(clone));
        }

        return clone;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a clone of a DxWorksheet - sheet name is '" + this.WorksheetName + "'.", ex);
      }
    }
  }
}

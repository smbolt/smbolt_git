using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Configuration
{
  [XMap(XType = XType.Element)]
  public class GridColumn
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap]
    public string Text { get; set; }

    [XMap]
    public string Tag { get; set; }

    [XMap(DefaultValue="True")]
    public bool Visible { get; set; }

    [XMap]
    public string ColumnName { get; set; }

    [XMap]
    public string Model { get; set; }

    [XMap]
    public string ModelKey { get; set; }

    [XMap]
    public bool ForeignEntryRequired { get; set; }

    [XMap]
    public int Width { get; set; }

    [XMap(DefaultValue="Left")]
    public string Align { get; set; }

    [XMap(DefaultValue="False")]
    public bool Fill { get; set; }

    [XMap(DefaultValue = "0")]
    public int MaxChars { get; set; }

    public string DataValue { get; set; }
    public string ToolTip { get; set; }
    public int WidthPixels { get; set; }
    public float WidthPct { get; set; }
    public bool IsLastVisibleColumn { get; set; }
    public int ColumnIndex { get; set; }
    public bool IsDbNullable { get; set; }
    public bool IsPrimaryKey { get; set; }

    public GridColumn()
    {
      this.Name = String.Empty;
      this.Text = String.Empty;
      this.Tag = String.Empty;
      this.Visible = true; 
      this.ColumnName = String.Empty;
      this.Model = String.Empty;
      this.ModelKey = String.Empty;
      this.ForeignEntryRequired = false;
      this.Width = 0;
      this.Align = String.Empty;
      this.Fill = false;
      this.MaxChars = 0;
      this.DataValue = String.Empty;
      this.ToolTip = String.Empty;
      this.WidthPixels = 0;
      this.WidthPct = 0F;
      this.IsLastVisibleColumn = false;
      this.ColumnIndex = -1; 
      this.IsDbNullable = false;
      this.IsPrimaryKey = false;
    }

    public GridColumn(GridColumn templateColumn)
    {
      this.Name = templateColumn.Name;
      this.Text = templateColumn.Text;
      this.Tag = templateColumn.Tag;
      this.ColumnName = templateColumn.ColumnName;
      this.Model = templateColumn.Model;
      this.ModelKey = templateColumn.ModelKey;
      this.ForeignEntryRequired = templateColumn.ForeignEntryRequired;
      this.Width = templateColumn.Width;
      this.Align = templateColumn.Align;
      this.Fill = templateColumn.Fill;
      this.MaxChars = templateColumn.MaxChars;
      this.DataValue = templateColumn.DataValue;
      this.WidthPixels = templateColumn.WidthPixels;
      this.WidthPct = templateColumn.WidthPct;
      this.ToolTip = templateColumn.ToolTip;
      this.IsLastVisibleColumn = templateColumn.IsLastVisibleColumn;
      this.ColumnIndex = -1; 
      this.IsDbNullable = false;
      this.IsPrimaryKey = false;
    }    

    public void AutoInit()
    {
      if (this.Text.IsNotBlank())
      {
        string text = this.Text.Replace(" ", String.Empty); 
        if (this.Name.IsBlank())
          this.Name = text;
        if (this.ColumnName.IsBlank())
          this.ColumnName = text;
        if (this.Tag.IsBlank())
          this.Tag = text; 
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;
using Org.GS;

namespace Org.TW.ToolPanels
{
  public partial class PropertiesPanel : ToolPanelBase
  {
    private Image _emptyCellImage;

    public PropertiesPanel()
    {
      InitializeComponent();
      InitializeForm();
    }

    public void LoadElement(object de)
    {
      gvProps.Rows.Clear();

      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Name", de.Name, false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Tag", de.Tag, false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Type", de.DeType.ToString(), false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "OxName", de.OxName, false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Depth", de.Depth.ToString(), false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Level", de.Level.ToString(), false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Content Query", de.ContentQuery, false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Content", de.ContentValue, false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Offset", "x: " + de.RawMetrics.Offset.X.ToString() + "  y: " + de.RawMetrics.Offset.Y.ToString(), false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Object Size", "w: " + de.RawMetrics.TotalSize.Width.ToString() + "  h: " + de.RawMetrics.TotalSize.Height.ToString(), false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Post Horz Offset", "x: " + de.RawMetrics.CurrentHorizontalOffset.X.ToString() + "  y: " + de.RawMetrics.CurrentHorizontalOffset.Y.ToString(), false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Post Vert Offset", "x: " + de.RawMetrics.CurrentVerticalOffset.X.ToString() + "  y: " + de.RawMetrics.CurrentVerticalOffset.Y.ToString(), false });



      //string parentName = "null";
      //string parentType = String.Empty;
      //if (de.Parent != null)
      //{
      //  parentName = de.Parent.Name;
      //  parentType = de.Parent.DeType.ToString();
      //}

      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Parent Name", parentName, false });
      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Parent Type", parentType, false });

      //gvProps.Rows.Add(new object[] { _emptyCellImage, "Child Elements", de.ChildElements.Count.ToString(), false });


    }

    private void InitializeForm()
    {
      ResourceManager resourceManager = new ResourceManager("Org.TW.Resource1", Assembly.GetExecutingAssembly());
      _emptyCellImage = (Bitmap)resourceManager.GetObject("EmptyCell");

      gvProps.Columns.Clear();

      DataGridViewColumn col = new DataGridViewImageColumn();
      col.Name = "Control";
      col.HeaderText = String.Empty;
      col.Width = 20;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvProps.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Name";
      col.HeaderText = "Name";
      col.Width = 150;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gvProps.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Value";
      col.HeaderText = "Value";
      col.Width = 150;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
      col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      gvProps.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Editable";
      col.HeaderText = String.Empty;
      col.Width = 0;
      col.Visible = false;
      gvProps.Columns.Add(col);
    }
  }
}

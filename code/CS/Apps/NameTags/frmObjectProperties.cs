using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Org.GS;

namespace NameTags
{
  public partial class frmObjectProperties : Form
  {
    public event Action<bool> DrawingObjectUpdated;

    DrawingObjectSet _objSet;
    bool IsComboBoxEventSuppressed = false;
    bool IsCellEditingInProgress = false;
    int colIndex = -1;
    int rowIndex = -1;


    public frmObjectProperties(DrawingObjectSet objSet)
    {
      InitializeComponent();
      _objSet = objSet;

      InitializeForm();
    }

    public void InitializeForm()
    {
      cboDrawingObjects.Items.Clear();

      foreach (KeyValuePair<int, DrawingObject> kvpObj in _objSet)
      {
        string objType = "Unknown Type";
        switch (kvpObj.Value.Type)
        {
          case Enums.ObjectType.TextObject: objType = "Text object"; break;
          case Enums.ObjectType.GraphicsObject: objType = "Graphics object"; break;
          case Enums.ObjectType.RectangleObject: objType = "Rectangle object"; break;
          case Enums.ObjectType.EllipseObject: objType = "Ellipse object"; break;
          case Enums.ObjectType.DiplomaPicture: objType = "Diploma picture"; break;
          default: objType = "Unknown object type"; break;
        }

        cboDrawingObjects.Items.Add(kvpObj.Value.Name +
            " - " + objType);
      }


      int objKey = -1;

      if (_objSet.SelectedObjectKeys.Length > 0)
        objKey = _objSet.SelectedObjectKeys[0];

      LoadDataGridView(objKey);
    }


    private void LoadDataGridView(int objKey)
    {
      dgvProperties.Rows.Clear();

      if (_objSet.Count < 1 || objKey == -1)
        return;

      DrawingObject obj = _objSet[objKey];

      IsComboBoxEventSuppressed = true;
      SetComboBoxValueByObjectName(obj.Name);
      IsComboBoxEventSuppressed = false;

      dgvProperties.Rows.Add(9);

      int rowIndex = 0;
      dgvProperties.Rows[rowIndex].Cells[0].Value = "Name";
      dgvProperties.Rows[rowIndex].Cells[1].Value = obj.Name;


      rowIndex++;
      dgvProperties.Rows[rowIndex].Cells[0].Value = "Type";
      switch (obj.Type)
      {
        case Enums.ObjectType.TextObject: dgvProperties.Rows[rowIndex].Cells[1].Value = "Text object"; break;
        case Enums.ObjectType.GraphicsObject: dgvProperties.Rows[rowIndex].Cells[1].Value = "Graphics object"; break;
        case Enums.ObjectType.RectangleObject: dgvProperties.Rows[rowIndex].Cells[1].Value = "Rectangle object"; break;
        case Enums.ObjectType.EllipseObject: dgvProperties.Rows[rowIndex].Cells[1].Value = "Ellipse object"; break;
        case Enums.ObjectType.DiplomaPicture: dgvProperties.Rows[rowIndex].Cells[1].Value = "Diploma picture"; break;
        default: dgvProperties.Rows[rowIndex].Cells[1].Value = "Unknown object type"; break;
      }

      rowIndex++;
      dgvProperties.Rows[rowIndex].Cells[0].Value = "Text";
      dgvProperties.Rows[rowIndex].Cells[1].Value = obj.Text;

      rowIndex++;
      dgvProperties.Rows[rowIndex].Cells[0].Value = "Graphics Path";
      dgvProperties.Rows[rowIndex].Cells[1].Value = obj.GraphicsPath;

      if (obj.TextFont != null)
      {
        rowIndex++;
        dgvProperties.Rows[rowIndex].Cells[0].Value = "Font Size";
        dgvProperties.Rows[rowIndex].Cells[1].Value = obj.TextFont.Size.ToString();
      }

      dgvProperties.Tag = objKey;
    }

    private void cboDrawingObjects_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (IsComboBoxEventSuppressed)
        return;

      char[] delim = new char[] { '-' };
      string[] s = cboDrawingObjects.Text.Split(delim);
      if (s.Length < 2)
        return;

      string objName = s[0].Trim();
            
      int objKey = GetObjectKeyByName(objName);

      _objSet.DeselectAll();
      _objSet[objKey].Select();

      LoadDataGridView(objKey);

      DrawingObjectUpdated?.Invoke(true);
    }

    private int GetObjectKeyByName(string objName)
    {
      foreach (KeyValuePair<int, DrawingObject> kvpObj in _objSet)
      {
        if (kvpObj.Value.Name == objName)
          return kvpObj.Key;
      }

      return -1;
    }

    private void SetComboBoxValueByObjectName(string objName)
    {
      char[] delim = new char[] { '-' };
    for (int i = 0; i < cboDrawingObjects.Items.Count; i++)
    {
        string[] s = cboDrawingObjects.Items[i].ToString().Trim().Split(delim);
        if (s.Length > 1)
        {
          if (s[0].Trim() == objName)
          {
            cboDrawingObjects.SelectedIndex = i;
            dgvProperties.Focus();
            dgvProperties.Select();
            return;
          }
        }
      }
    }

    private void dgvProperties_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      if (dgvProperties.Tag == null)
        return;

      string propertyName = dgvProperties.Rows[e.RowIndex].Cells[0].Value.ToString();
      string propertyValue = dgvProperties.Rows[e.RowIndex].Cells[1].Value.ToString();


      int objKey = Convert.ToInt32(dgvProperties.Tag);

      switch (propertyName)
      {
        case "Name": _objSet[objKey].Name = propertyValue; break;
        case "Text": _objSet[objKey].Text = propertyValue; break;
        case "Type": _objSet[objKey].Type = GetObjectType(propertyValue); break;
        case "Graphics Path": _objSet[objKey].GraphicsPath = propertyValue; break;
        case "Font Size": _objSet[objKey].SetFontSize(propertyValue.ToFloat()); break;
      }

      IsCellEditingInProgress = false;

      DrawingObjectUpdated?.Invoke(true);
    }

    private Enums.ObjectType GetObjectType(string typeString)
    {
      Enums.ObjectType objectType = Enums.ObjectType.TextObject;

      switch (typeString)
      {
        case "Text object": objectType = Enums.ObjectType.TextObject; break;
        case "Rectangle object": objectType = Enums.ObjectType.RectangleObject; break;
        case "Ellipse object": objectType = Enums.ObjectType.EllipseObject; break;
        case "Diploma picture": objectType = Enums.ObjectType.DiplomaPicture; break;
      }

      return objectType;
    }

    private void frmObjectProperties_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (IsCellEditingInProgress)
      {
        if (dgvProperties.Tag == null)
          return;

        string propertyName = dgvProperties.Rows[rowIndex].Cells[0].Value.ToString();
        string propertyValue = dgvProperties.Rows[rowIndex].Cells[1].Value.ToString();

        int objKey = Convert.ToInt32(dgvProperties.Tag);

        switch (propertyName)
        {
          case "Name": _objSet[objKey].Name = propertyValue; break;
          case "Text": _objSet[objKey].Text = propertyValue; break;
          case "Type": _objSet[objKey].Type = GetObjectType(propertyValue); break;
          case "Graphics Path": _objSet[objKey].GraphicsPath = propertyValue; break;
        }

        IsCellEditingInProgress = false;
      }
    }

    private void dgvProperties_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
      IsCellEditingInProgress = true;
      rowIndex = e.RowIndex;
      colIndex = e.ColumnIndex;
    }

  }
}
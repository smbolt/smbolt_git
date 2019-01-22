using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.FTW.ToolPanels;
using Org.Terminal.Screen;
using Org.Terminal.Controls;
using Org.Terminal.BMS;
using Org.GS;

namespace Org.EditorToolWindows
{
  public partial class ControlPanel : ToolPanelBase
  {
    private ScreenSpecSet _screenSpecSet;
    private ScreenSpec _selectedScreenSpec;
    private string _selectedScreenName;

    public ControlPanel()
    {
      InitializeComponent();
      InitializeControl();
    }


    public void MainEventHandler(MFEventArgs e)
    {
      MFContainer scr = null;

      switch (e.EventCommand)
      {
        case EventCommand.UpdateInfoPanel:
          if (e.EventType == EventType.ScreenResize)
          {
            if (e.MFContainer != null)
            {
              scr = e.MFContainer;
              lblScreenSize.Text = "W:" + scr.Size.Width.ToString("0000") + "  H:" + scr.Size.Height.ToString("0000");
              lblScreenLines.Text = scr.CapacityNumberOfLinesHigh.ToString("000");
              lblScreenCols.Text = scr.CapacityNumberOfCharsWide.ToString("000");
              lblPaddingValue.Text = "T:" + scr.TopPadding.ToString("00") + "  R:" + scr.RightPadding.ToString("00") +
                                   "  B:" + scr.BottomPadding.ToString("00") + "  L:" + scr.LeftPadding.ToString("00");
              lblClientSize.Text = "W:" + scr.Size.Width.ToString("0000") + "  H:" + scr.Size.Height.ToString("0000");

              string focusedControl = lblControlNameValue.Text.DbToString();

              if (scr.Controls.ContainsKey(focusedControl))
              {
                e.Sender = scr.Controls[focusedControl] as MFBase;
                if (e.Sender == null)
                  return;
              }
              else
              {
                return;
              }
            }
            else
            {
              return;
            }
          }

          var c = e.Sender as MFBase;

          lblControlNameValue.Text = c.Name;
          lblTagValue.Text = c.Tag.DbToString();

          lblOrigLocation.Text = "X:0000  Y:0000";
          lblCurrLocation.Text = "X:" + c.Location.X.ToString("0000") + "  Y:" + c.Location.Y.ToString("0000");
          lblOrigLine.Text = c.FieldSpec.OrigLine.ToString("000");
          lblCurrLine.Text = c.FieldSpec.CurrLine.ToString("000");
          lblOrigCol.Text = c.FieldSpec.OrigCol.ToString("000");
          lblCurrCol.Text = c.FieldSpec.CurrCol.ToString("000");
          lblOrigLth.Text = c.FieldSpec.OrigLth.ToString("000");
          lblCurrLth.Text = c.FieldSpec.CurrLth.ToString("000");
          lblOrigSize.Text = "W:0000  H:0000";
          lblCurrSize.Text = "W:" + c.Size.Width.ToString("0000") + "  H:" + c.Size.Height.ToString("0000");

          lblOrigValue.Text = c.FieldSpec.OrigValue;
          lblCurrValue.Text = c.FieldSpec.CurrValue;

          if (e.EventType == EventType.ScreenResize)
            return;

          scr = c.MFContainer;
          lblScreenSize.Text = "W:" + scr.Size.Width.ToString("0000") + "  H:" + scr.Size.Height.ToString("0000");
          lblScreenLines.Text = scr.CapacityNumberOfLinesHigh.ToString("000");
          lblScreenCols.Text = scr.CapacityNumberOfCharsWide.ToString("000");
          lblPaddingValue.Text = "T:" + scr.TopPadding.ToString("00") + "  R:" + scr.RightPadding.ToString("00") +
                               "  B:" + scr.BottomPadding.ToString("00") + "  L:" + scr.LeftPadding.ToString("00");
          lblClientSize.Text = "W:" + scr.Size.Width.ToString("0000") + "  H:" + scr.Size.Height.ToString("0000");

          Application.DoEvents();
          break;
      }
    }


    public void SetScreenSpec(ScreenSpecSet screenSpecSet, string selectedScreenName)
    {
      _screenSpecSet = screenSpecSet;
      _selectedScreenName = selectedScreenName;
      _selectedScreenSpec = _screenSpecSet[_selectedScreenName];
      LoadScreenSpec();
    }

    private void LoadScreenSpec()
    {
      cboScreenName.Items.Clear();
      foreach (var screen in _screenSpecSet.Values)
      {
        cboScreenName.Items.Add(screen.Name);
      }

      for (int i = 0; i < cboScreenName.Items.Count; i++)
      {
        if (cboScreenName.Items[i].ToString() == _selectedScreenName)
        {
          cboScreenName.SelectedIndex = i;
          break;
        }
      }

      gvFields.Rows.Clear();

      foreach (var field in _selectedScreenSpec.FieldSpecSet)
      {
        var fieldGrid = new string[6];
        fieldGrid[0] = field.Name;
        fieldGrid[1] = field.CurrLine.ToString();
        fieldGrid[2] = field.CurrCol.ToString();
        fieldGrid[3] = field.CurrLth.ToString();
        fieldGrid[4] = field.Init;
        fieldGrid[5] = field.CurrValue;
        gvFields.Rows.Add(fieldGrid);
      }
    }

    private void InitializeControl()
    {
      base.TopPanel.Visible = false;
      this.pnlTop.Visible = false;
      this.mnuMain.Visible = false;

      InitializeGrid();
    }


    private void InitializeGrid()
    {
      gvFields.Columns.Clear();

      DataGridViewColumn col = new DataGridViewTextBoxColumn();
      col.Name = "Name";
      col.HeaderText = "Name";
      col.Width = 100;
      gvFields.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Line";
      col.HeaderText = "Line";
      col.Width = 40;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvFields.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Col";
      col.HeaderText = "Col";
      col.Width = 40;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvFields.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Lth";
      col.HeaderText = "Lth";
      col.Width = 40;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvFields.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "InitValue";
      col.HeaderText = "InitValue";
      col.Width = 160;
      gvFields.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "CurrValue";
      col.HeaderText = "Curr Value";
      col.Width = 160;
      col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      gvFields.Columns.Add(col);
    }
  }
}

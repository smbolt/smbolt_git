using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;

namespace Org.TW.ToolPanels
{
  public partial class ControlPanel : ToolPanelBase
  {
    public bool PrintToImage
    {
      get {
        return ckPrintToImage.Checked;
      }
      set {
        ckPrintToImage.Checked = value;
      }
    }

    public bool CreateDocument
    {
      get {
        return ckCreateDocument.Checked;
      }
      set {
        ckCreateDocument.Checked = value;
      }
    }

    public bool ShowMap
    {
      get {
        return ckShowMap.Checked;
      }
      set {
        ckShowMap.Checked = value;
      }
    }

    public bool ShowXmlMap
    {
      get {
        return ckShowXmlMap.Checked;
      }
      set {
        ckShowXmlMap.Checked = value;
      }
    }

    public bool IncludeProperties
    {
      get {
        return ckIncludeProperties.Checked;
      }
      set {
        ckIncludeProperties.Checked = value;
      }
    }

    public bool DiagnosticsMode
    {
      get {
        return ckDiagnosticsMode.Checked;
      }
      set {
        ckDiagnosticsMode.Checked = value;
      }
    }

    public bool ShowScale
    {
      get {
        return ckShowScale.Checked;
      }
      set {
        ckShowScale.Checked = value;
      }
    }

    public int DiagnosticsLevel
    {
      get {
        return Convert.ToInt32(numDiagnosticsLevel.Value);
      }
      set {
        numDiagnosticsLevel.Value = value;
      }
    }

    public float LineFactor
    {
      get {
        return txtLineFactor.Text.ToFloat();
      }
      set {
        txtLineFactor.Text = value.ToString("0.000");
      }
    }

    public float WidthFactor
    {
      get {
        return txtWidthFactor.Text.ToFloat();
      }
      set {
        txtWidthFactor.Text = value.ToString("0.000");
      }
    }

    public float SpaceWidthFactor
    {
      get {
        return txtSpaceWidthFactor.Text.ToFloat();
      }
      set {
        txtSpaceWidthFactor.Text = value.ToString("0.000");
      }
    }

    public ControlPanel()
    {
      InitializeComponent();
    }

    public void ClearRegionList()
    {
      lstRegions.Items.Clear();
    }

    public void AddRegion(string regionName)
    {
      lstRegions.Items.Add(regionName);
    }
  }
}

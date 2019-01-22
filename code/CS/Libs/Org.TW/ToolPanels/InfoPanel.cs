using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.TW.ToolPanels
{
  public partial class InfoPanel : ToolPanelBase
  {
    public string Source
    {
      get {
        return lblSource.Text;
      }
      set {
        lblSource.Text = value;
      }
    }

    public string MousePos
    {
      get {
        return lblMousePosValue.Text;
      }
      set {
        lblMousePosValue.Text = value;
      }
    }

    public string MouseRaw
    {
      get {
        return lblMouseRawValue.Text;
      }
      set {
        lblMouseRawValue.Text = value;
      }
    }

    public string Color
    {
      get {
        return lblColorValue.Text;
      }
      set {
        lblColorValue.Text = value;
      }
    }

    public string ElementName
    {
      get {
        return lblNameValue.Text;
      }
      set {
        lblNameValue.Text = value;
      }
    }

    public string ElementTag
    {
      get {
        return lblTagValue.Text;
      }
      set {
        lblTagValue.Text = value;
      }
    }

    public string ElementType
    {
      get {
        return lblTypeValue.Text;
      }
      set {
        lblTypeValue.Text = value;
      }
    }

    public string Level
    {
      get {
        return lblLevelValue.Text;
      }
      set {
        lblLevelValue.Text = value;
      }
    }

    public string Offset
    {
      get {
        return lblOffsetValue.Text;
      }
      set {
        lblOffsetValue.Text = value;
      }
    }

    public string ElementSize
    {
      get {
        return lblSizeValue.Text;
      }
      set {
        lblSizeValue.Text = value;
      }
    }


    public InfoPanel()
    {
      InitializeComponent();
      InitializeControl();
    }

    private void InitializeControl()
    {
      this.Clear();
    }


    public void Clear()
    {
      lblSourceValue.Text = String.Empty;
      lblMousePosValue.Text = String.Empty;
      lblMouseRawValue.Text = String.Empty;
      lblColorValue.Text = String.Empty;
      lblNameValue.Text = String.Empty;
      lblTagValue.Text = String.Empty;
      lblLevelValue.Text = String.Empty;
      lblTypeValue.Text = String.Empty;
      lblOffsetValue.Text = String.Empty;
      lblSizeValue.Text = String.Empty;
    }
  }
}

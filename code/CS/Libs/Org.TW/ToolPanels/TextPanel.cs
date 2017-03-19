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
  public partial class TextPanel : ToolPanelBase
  {
    public TextPanel()
    {
        InitializeComponent();
    }

    public void SetText(string text)
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Action)(((() => this.txtCode.Text = text))));
      }
      else
      {
        this.txtCode.Text = text;
      }

      this.txtCode.SelectionStart = 0;
      this.txtCode.SelectionLength = 0;
    }

    public void Clear()
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Action)(((() => this.txtCode.Clear()))));
      }
      else
      {
        this.txtCode.Clear();
      }
    }

  }
}

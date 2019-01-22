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
  public partial class DebugPanel : ToolPanelBase
  {
    public DebugPanel()
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

    }

    public void AppendText(string text)
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Action)(((() => this.txtCode.Text += g.crlf + text))));
      }
      else
      {
        this.txtCode.Text += g.crlf + text;
      }

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

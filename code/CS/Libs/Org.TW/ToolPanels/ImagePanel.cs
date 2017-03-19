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
  public partial class ImagePanel : ToolPanelBase
  {
    public PictureBox PictureBox
    {
      get { return this.pbMain; }
    }

    public ImagePanel()
    {
      InitializeComponent();
    }
  }
}

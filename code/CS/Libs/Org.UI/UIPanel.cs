using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Org.GS;

namespace Org.UI
{
  public class UIPanel : UIBase
  {
    public UIPanel()
    {
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      //
      // UIPanel
      //
      this.Name = "UIPanel";
      this.ResumeLayout(false);
    }
  }
}

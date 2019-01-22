using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TW.ToolPanels;
using Org.TW;
using Org.GS.TextProcessing;
using Org.GS;

namespace Org.PdfExtractToolWindows
{
  public partial class TreeViewTextDocStructure : ToolPanelBase
  {
    public TreeViewTextDocStructure()
      : base("TreeViewTextDocStructure")
    {
      InitializeComponent();
      InitializeControl();
    }


    private void InitializeControl()
    {
    }
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.GS
{
  public partial class frmConfigData : Form
  {
    public frmConfigData(string configData)
    {
      InitializeComponent();
      txtOut.Text = configData;
      txtOut.SelectionStart = 0;
      txtOut.SelectionLength = 0;
    }

    private void mnuFileExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}

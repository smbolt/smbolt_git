using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.ControlTest
{
  public partial class frmConfig : Form
  {
    private string _filePath;
    public event Action<string> ConfigFormAction;

    public frmConfig(string filePath)
    {
      InitializeComponent();
      _filePath = filePath;
      InitializeForm();
    }

    private void InitializeForm()
    {
      fctxtMain.Text = File.ReadAllText(_filePath);
    }

    public string GetText()
    {
      return fctxtMain.Text;
    }

    private void btnHide_Click(object sender, EventArgs e)
    {
      this.Visible = false;
    }

    private void fctxtMain_CustomAction(object sender, FastColoredTextBoxNS.CustomActionEventArgs e)
    {
      switch(e.Action)
      {
        case FastColoredTextBoxNS.FCTBAction.CustomAction1:
          if (this.ConfigFormAction != null)
            this.ConfigFormAction("SaveAndDisplay");
          break;
      }
    }
  }
}

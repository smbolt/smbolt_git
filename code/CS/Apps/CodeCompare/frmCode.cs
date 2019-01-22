using System;
using System.Windows.Forms;
using Org.GS;

namespace Org.CodeCompare
{
  public partial class frmCode : Form
  {
    private string _fileName;

    public frmCode(string fileName)
    {
      InitializeComponent();

      _fileName = fileName;

      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      switch (sender.ActionTag())
      {
        case "Exit":
          this.Close();
          break;
      }
    }

    private void InitializeForm()
    {

    }
  }
}

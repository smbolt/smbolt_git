using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Math;

namespace MathWorkbench
{
  public partial class frmMain : Form
  {
    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Compute":
          Compute();
          break;

        case "Exit":
          Close();
          break;
      }
    }

    private void Compute()
    {
      var e = new Equation();
      e.Formula = txtEquation.Text;

      e.Variables.Add("a", "17");

      txtOut.Text = e.ComputeValue() + g.crlf2 + g.MemoryLog;

      //txtOut.Text = new Equation(txtEquation.Text).ComputeValue() + g.crlf2 + g.MemoryLog;
    }


    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        string errorMessage = ex.ToReport();

        if (errorMessage.Length > 10000)
          errorMessage = errorMessage.Substring(0, 10000);

        MessageBox.Show(errorMessage, "MathWorkbench - Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }


      try
      {
        this.SetInitialSizeAndLocation();

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 +
                        ex.ToReport(), "MathWorkbench - Initialization Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
      }
    }
  }
}

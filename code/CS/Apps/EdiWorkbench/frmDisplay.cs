using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Org.EdiWorkbench
{
  public partial class frmDisplay : Form
  {

      public frmDisplay()
      {
          InitializeComponent();
      }

      public void SetText(string text)
      {
          rtxtDisplay.Text = text;
          rtxtDisplay.SelectionStart = 0;
          rtxtDisplay.SelectionLength = 0;

          Application.DoEvents();
      }

      public void Clear()
      {
          rtxtDisplay.Clear();
      }
  }
}

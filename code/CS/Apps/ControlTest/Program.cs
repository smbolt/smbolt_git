using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;

namespace Org.ControlTest
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
      Application.Run(new frmMain());
    }

    

    static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
      MessageBox.Show("A last resort exception has been caught in the ControlTest program." + g.crlf2 + "Please see the message and error detail below." + 
          g.crlf2 + e.Exception.Message + 
          g.crlf2 + "Source:" + g.crlf + e.Exception.Source + g.crlf2 + 
          g.crlf2 + "Stack Trace:" + g.crlf + e.Exception.StackTrace,
          "ControlTest - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }
}

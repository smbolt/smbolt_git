using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Teleflora.Operations.MetricView
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    ///
    public static string[] args;

    [STAThread]
    static void Main(string[] Args)
    {
      args = Args;
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      frmMain f = new frmMain();
      f.SetArgs(args);
      Application.Run(f);
    }
  }
}
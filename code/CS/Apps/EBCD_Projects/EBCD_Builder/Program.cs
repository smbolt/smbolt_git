using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Adsdi.EBCD_Builder
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
            Application.Run(new Adsdi.EBCD_Builder.frmMain());
        }
    }
}

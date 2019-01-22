using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS.Logging;
using Org.GS;
using System.IO;
using Org.GX;
using Newtonsoft.Json.Linq;

namespace GxWorkbench
{
  public partial class frmMain : Form
  {
    private Logger _logger;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      switch (sender.ActionTag())
      {
        case "Go":
          Go();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void Go()
    {
      this.Cursor = Cursors.WaitCursor;

      txtOut.Text = String.Empty;
      Application.DoEvents();

      TaskResult taskResult = null;

      try
      {
        var gxJob = GetGxJobFromJsonFile();

        using (var gxEngine = new GxEngine(gxJob))
        {
          taskResult = gxEngine.RunJobAsync().Result;
        }

        txtOut.Text = taskResult.Report;

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;

        MessageBox.Show("An exception occurred during the initialization of the application." + g.crlf2 + ex.ToReport(),
                        "GxWorkbench - GxJob Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private GxJob GetGxJobFromJsonFile()
    {
      string fileName = g.CI("InputFile");

      try
      {
        using (var htoFactory = new HtoFactory())
        {
          string jobName = Path.GetFileNameWithoutExtension(fileName);
          var hto = htoFactory.CreateFromJsonFile(fileName);
          hto.Id = jobName;
          return new GxJob(hto);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create an Hto object from the JSON file '" + fileName + "'..", ex);
      }
    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the creation of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "GxWorkbench - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {
        _logger = new Logger();
        this.SetInitialSizeAndLocation();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application." + g.crlf2 + ex.ToReport(),
                        "GxWorkbench - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}

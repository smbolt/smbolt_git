using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using Org.DB;
using Org.GS;
using Gulfport.WellMaster.Data;
using Gulfport.BusinessObjects.WellMaster;
using GDB = Gulfport.WellMaster.Data.Entities;

namespace Org.DbDto
{
  public partial class frmMain : Form
  {
    private a a;
    private List<GDB.Well> _wells;

    public frmMain()
    {
      InitializeComponent();
      InitializeApplication();
    }

    private void Action(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "GetWells":
          GetWells();
          break;

        case "Exit":
          this.Close();
          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void GetWells()
    {
      try
      {
        DateTime dtBegin = DateTime.Now;
        List<Well> wells = null;

        using (var repository = new WellMasterRepository(g.CI("ConnectionStringName"), "GPMaster"))
        {
          wells = repository.Wells.ToList();
        }

        StringBuilder sb = new StringBuilder();
        foreach (var well in wells)
        {
          sb.Append(well.WellID.Value.ToString("000000") + " " + well.WellName + g.crlf);
        }

        TimeSpan ts = DateTime.Now - dtBegin;
        txtOut.Text = "Total milliseconds : " + ts.TotalMilliseconds.ToString() + "  Well count: " + wells.Count().ToString() +
                      g.crlf2 + sb.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during retrieval of wells." + g.crlf2 + ex.ToReport(),
                        "DbDto - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    private void InitializeApplication()
    {
      try
      {
        a = new a();
        g.ConnectionStringName = g.CI("ConnectionStringName");
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
                        "DbDto - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }
  }
}

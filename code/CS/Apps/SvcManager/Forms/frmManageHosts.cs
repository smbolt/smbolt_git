using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TSK.Business.Models;
using Org.TSK.Business;
using Org.GS.Configuration;
using Org.GS;

namespace Org.SvcManager
{
  public partial class frmManageHosts : Form
  {
    private string _env;
    private ConfigDbSpec _dbSpec;
    private HostSet _hostSet;

    public frmManageHosts(string env, ConfigDbSpec dbSpec)
    {
      InitializeComponent();

      _env = env;
      _dbSpec = dbSpec;

      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "AddNewHost":
          AddNewHost();
          break;

        case "Close":
          this.DialogResult = DialogResult.OK;
          this.Close();
          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void AddNewHost()
    {
      try
      {
        using (var fAddOrUpdateHost = new frmAddOrUpdateHost(true, _env, _dbSpec))
        {
          if (fAddOrUpdateHost.ShowDialog() == DialogResult.OK)
          {
            LoadGrid();
          }
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to retrieve the add a new host to the database." + g.crlf2 + ex.ToReport(),
                        "Manage Hosts - Error Adding New Host", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadGrid()
    {
      try
      {
        gvHosts.Rows.Clear();

        this.Cursor = Cursors.WaitCursor;

        using (var taskRepo = new TaskRepository(_dbSpec))
        {
          _hostSet = taskRepo.GetHostSet();
        }

        foreach (var host in _hostSet.Values)
        {
          gvHosts.Rows.Add(
            host.HostName,
            host.HostID);
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to retrieve the existing hosts and load them to the grid." + g.crlf2 + ex.ToReport(),
                        "Manage Hosts - Error Loading Existing Hosts", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeForm()
    {
      lblEnvironmentValue.Text = _env;
      InitializeGrid();
      LoadGrid();
    }


    private void InitializeGrid()
    {
      gvHosts.Columns.Clear();

      DataGridViewColumn col = new DataGridViewTextBoxColumn();
      col.Name = "HostName";
      col.HeaderText = "Host Name";
      col.Width = 200;
      col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      gvHosts.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "HostID";
      col.HeaderText = String.Empty;
      col.Width = 0;
      col.Visible = false;
      gvHosts.Columns.Add(col);
    }

  }
}

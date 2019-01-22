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
  public partial class frmAddOrUpdateHost : Form
  {
    private bool _newHost;
    private string _env;
    private ConfigDbSpec _dbSpec;
    private HostSet _hostSet;

    public frmAddOrUpdateHost(bool newHost, string env, ConfigDbSpec dbSpec)
    {
      InitializeComponent();

      _newHost = newHost;
      _env = env;
      _dbSpec = dbSpec;

      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "OK":
          if (!ValidateInput())
            return;

          if (_newHost)
            AddHostToDatabase();
          else
            UpdateHost();

          this.DialogResult = DialogResult.OK;
          this.Close();
          break;

        case "Cancel":
          this.DialogResult = DialogResult.Cancel;
          this.Close();
          break;
      }
    }

    private void AddHostToDatabase()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        var host = new Host();
        host.HostName = txtHostName.Text;

        using (var taskRepo = new TaskRepository(_dbSpec))
        {
          host.HostID = taskRepo.AddHost(host);
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        throw new Exception("An exception occurred when attempting to load the HostSet to the grid.", ex);
      }
    }

    private void UpdateHost()
    {

    }

    private HostSet GetHostSet()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        using (var taskRepo = new TaskRepository(_dbSpec))
        {
          this.Cursor = Cursors.Default;
          return taskRepo.GetHostSet();
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        throw new Exception("An exception occurred when attempting to retrieve the HostSet (list of existing hosts).", ex);
      }
    }

    private void InitializeForm()
    {
      this.Text = _newHost ? "Add New Host" : "Update Host";

      _hostSet = GetHostSet();
    }

    private void txtHostName_TextChanged(object sender, EventArgs e)
    {
      btnOK.Enabled = txtHostName.Text.IsNotBlank();
    }

    private bool ValidateInput()
    {
      if (txtHostName.Text.IsBlank())
        return false;

      if (_newHost)
      {
        foreach (var host in _hostSet)
        {
          if (txtHostName.Text.ToUpper().Trim() == host.Value.HostName.ToUpper())
          {
            MessageBox.Show("The host name '" + txtHostName.Text.Trim() + "' already exists in the database." + g.crlf2 +
                            "Please enter a different host name.", this.Text + " - Duplicate Host Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
          }
        }
      }

      return true;
    }
  }
}

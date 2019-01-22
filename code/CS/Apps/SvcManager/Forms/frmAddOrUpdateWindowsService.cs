using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TSK.Business;
using Org.TSK.Business.Models;
using Org.GS.ServiceManagement;
using Org.GS.Configuration;
using Org.GS;

namespace Org.SvcManager
{
  public partial class frmAddOrUpdateWindowsService : Form
  {
    private bool _isNew;
    private ServiceEnvironment _serviceEnvironment;
    private ServiceHost _serviceHost;
    private ServiceType _serviceType;
    private ConfigDbSpec _dbSpec;
    private ServiceSpecSet _existingServices;


    public frmAddOrUpdateWindowsService(ServiceEnvironment serviceEnvironment, ServiceHost serviceHost, ServiceType serviceType, ConfigDbSpec dbSpec, bool isNew)
    {
      InitializeComponent();

      _serviceEnvironment = serviceEnvironment;
      _serviceHost = serviceHost;
      _serviceType = serviceType;
      _dbSpec = dbSpec;
      _isNew = isNew;

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
          AddServiceToDatabase();
          this.DialogResult = DialogResult.OK;
          this.Close();
          break;

        case "Cancel":
          this.DialogResult = DialogResult.Cancel;
          this.Close();
          break;
      }
    }

    private void AddServiceToDatabase()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        var serviceSpec = new ServiceSpec();
        serviceSpec.Name = txtTaskServiceName.Text.Trim();
        serviceSpec.ServiceHostID = _serviceHost.HostID;
        serviceSpec.ParentServiceID = null;
        serviceSpec.ServiceType = _serviceType;

        switch (_serviceType)
        {
          case ServiceType.WindowsService:

            break;

          case ServiceType.WCFWebService:
            // add additional WCF fields after adding them to the UI.
            break;

          case ServiceType.WebSite:

            break;
        }

        using (var taskRepo = new TaskRepository(_dbSpec))
        {
          taskRepo.AddService(serviceSpec);
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to add the service to the database." + g.crlf2 + ex.ToReport(),
                        "Add Service - Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private ServiceSpecSet GetExistingServices()
    {
      try
      {
        using (var taskRepo = new TaskRepository(_dbSpec))
        {
          return taskRepo.GetServicesForHost(_serviceHost.HostID);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get a list of services for host '" + _serviceHost.Name + "' in the " +
                            _serviceEnvironment.Name + " environment.", ex);
      }
    }

    private void InitializeForm()
    {

      lblEnvironmentValue.Text = _serviceEnvironment.Name;
      lblHostValue.Text = _serviceHost.Name;
      lblServiceType.Text = _serviceType.ToString();

      try
      {
        _existingServices = GetExistingServices();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the form." + g.crlf2 + ex.ToReport(),
                        "Add Service - Form Initialization Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private bool ValidateInput()
    {
      string serviceType = "Windows Service";
      if (_serviceType == ServiceType.WCFWebService)
        serviceType = "WCF Web Service";
      if (_serviceType == ServiceType.WebSite)
        serviceType = "Web Site";

      if (txtTaskServiceName.Text.IsBlank())
      {
        MessageBox.Show("The " + serviceType + " name cannot be blank.", "Add " + serviceType, MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtTaskServiceName.Focus();
        txtTaskServiceName.SelectAll();
        return false;
      }

      foreach (var service in _existingServices.Values)
      {
        if (service.Name.ToUpper().Trim() == txtTaskServiceName.Text.Trim().ToUpper())
        {
          MessageBox.Show("The " + serviceType + " name already exists." + g.crlf2 + "Please enter a different name.",
                          "Add " + serviceType, MessageBoxButtons.OK, MessageBoxIcon.Error);
          txtTaskServiceName.Focus();
          txtTaskServiceName.SelectAll();
          return false;
        }
      }

      // add particular validation by type
      switch (_serviceType)
      {
        case ServiceType.WindowsService:
          break;

        case ServiceType.WCFWebService:
          break;

        case ServiceType.WebSite:
          break;
      }


      return true;
    }

    private void CriteriaChanged(object sender, EventArgs e)
    {
      btnOK.Enabled = CheckCriteria();
    }

    private bool CheckCriteria()
    {
      switch (_serviceType)
      {
        case ServiceType.WindowsService:
          if (txtTaskServiceName.Text.IsNotBlank())
            return true;
          break;


        case ServiceType.WCFWebService:

          break;


        case ServiceType.WebSite:

          break;
      }

      return false;
    }
  }
}

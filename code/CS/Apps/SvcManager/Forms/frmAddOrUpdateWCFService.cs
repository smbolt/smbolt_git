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
using Org.WSO.Transactions;
using Org.WSO;
using Org.WSC;
using Org.GS.ServiceManagement;
using Org.GS.Configuration;
using Org.GS.Network;
using Org.GS;

namespace Org.SvcManager
{
  public partial class frmAddOrUpdateWCFService : Form
  {
    private bool _isNew;
    private ServiceEnvironment _serviceEnvironment;
    private ServiceHost _serviceHost;
    private ServiceType _serviceType;
    private ServiceSpec _existingServiceSpec;
    private ServiceSpec _parentServiceSpec;
    private ConfigDbSpec _dbSpec;
    private WCFHostingModel _wcfHostingModel;
    private bool _initializationComplete;

    public frmAddOrUpdateWCFService(ServiceSpec existingServiceSpec, ServiceEnvironment serviceEnvironment, ServiceHost serviceHost, ServiceType serviceType, 
                                    WCFHostingModel wcfHostingModel, ConfigDbSpec dbSpec, ServiceSpec parentServiceSpec, bool isNew)
    {
      InitializeComponent();

      _existingServiceSpec = existingServiceSpec;
      _serviceEnvironment = serviceEnvironment;
      _serviceHost = serviceHost;
      _serviceType = serviceType;
      _wcfHostingModel = wcfHostingModel;
      _dbSpec = dbSpec;
      _parentServiceSpec = parentServiceSpec;
      _isNew = isNew;

      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "TestWebService":
          TestWebService();
          break;

        case "PingPort":
          PingPort();
          break;

        case "OK":
          if (!ValidateInput())
            return;
          UpdateDatabase();
          this.DialogResult = DialogResult.OK;
          this.Close();
          break;

        case "Cancel":
          this.DialogResult = DialogResult.Cancel;
          this.Close();
          break;
      }
    }

    private void UpdateDatabase()
    {
      if (!ValidateInput())
        return;

      string task = _isNew ? "add the web service" : "update the web service";
      string title = _isNew ? "Add Web Service" : "Update Web Service";

      this.Cursor = Cursors.WaitCursor;

      try
      {
        var serviceSpec = new ServiceSpec();
        if (!_isNew)
          serviceSpec.TaskServiceID = _existingServiceSpec.TaskServiceID;
        serviceSpec.ServiceHostID = _serviceHost.HostID;
        serviceSpec.ParentServiceID = _wcfHostingModel == WCFHostingModel.HostedInWindowsService && _parentServiceSpec != null ? 
                                      _parentServiceSpec.TaskServiceID : (int?) null;
        serviceSpec.Name = _wcfHostingModel == WCFHostingModel.HostedInWindowsService ? "MgmtWebSvc" : txtWCFServiceName.Text.Trim();
        serviceSpec.ServiceType = ServiceType.WCFWebService;
        serviceSpec.WebServiceBinding = g.ToEnum<WebServiceBinding>(cboWebServiceBinding.Text, WebServiceBinding.BasicHttp);
        serviceSpec.WsHost = _serviceHost.Name;
        serviceSpec.WsPort = txtPort.Text.Trim();
        serviceSpec.WsServiceName = _wcfHostingModel == WCFHostingModel.IIS ? txtWCFServiceName.Text.Trim() : null;

        using (var taskRepo = new TaskRepository(_dbSpec))
        {
          if (_isNew)
            serviceSpec.TaskServiceID = taskRepo.AddService(serviceSpec);
          else
            taskRepo.UpdateService(serviceSpec);
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to " + task + "." +
                         g.crlf2 + ex.ToReport(), title + " - Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void TestWebService()
    {
      if (!ValidateInput())
        return;

      this.Cursor = Cursors.WaitCursor;

      try
      {
        var configWsSpec = BuildConfigWsSpec();

        if (!configWsSpec.IsReadyToConnect())
          throw new Exception("ConfigWsSpec is not ready to connect to the web service."); 

        var wsParms = new WsParms("Ping", configWsSpec, false);
        var messageFactory = new MessageFactory();
        var requestMessage = messageFactory.CreateRequestMessage(wsParms); 
        var responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);

        this.Cursor = Cursors.Default;

        if (responseMessage.TransactionStatus == TransactionStatus.Success)
        {
          MessageBox.Show("The web service test was successful." + g.crlf2 + "",
                          "Test Web Service Connection - Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
          string errorMessage = String.Empty;

          if (responseMessage.TransactionName == "ErrorResponse")
          {
            using (var f = new ObjectFactory2())
            {
              var errorResponse = f.Deserialize(responseMessage.TransactionBody) as ErrorResponse;
              errorMessage = errorResponse.Message;
            }
          }

          MessageBox.Show("The web service test failed." + g.crlf2 + errorMessage,
                          "Test Web Service Connection - Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }        
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to test the Ping transaction in the WCF web service on host '" + _serviceHost + "'." + 
                         g.crlf2 + ex.ToReport(), "Test Web Service Connection - Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private ConfigWsSpec BuildConfigWsSpec()
    {
      var configWsSpec = new ConfigWsSpec();
      configWsSpec.WsHost = _serviceHost.IPV4Address;
      configWsSpec.WsPort = txtPort.Text.Trim();
      configWsSpec.WsBinding = g.ToEnum<WebServiceBinding>(cboWebServiceBinding.Text, WebServiceBinding.BasicHttp);
      configWsSpec.WsServiceName = txtWCFServiceName.Text.Trim();
      return configWsSpec;
    }

    private void PingPort()
    {
      try
      {
        if (!ValidateInput())
          return;

        this.Cursor = Cursors.WaitCursor;

        string ipAddress = _serviceHost.IPV4Address;
        int port = Int32.Parse(txtPort.Text.Trim());

        var taskResult = PortPing.PingPort(ipAddress, port);

        this.Cursor = Cursors.Default;

        if (taskResult.TaskResultStatus == TaskResultStatus.Success)
        {
          MessageBox.Show(taskResult.Message, "Port Ping - Host " + _serviceHost.Name + " was Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
          MessageBox.Show(taskResult.Message, "Port Ping - Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to execute the Port Ping of host '" + _serviceHost + "'." + g.crlf2 + ex.ToReport(),
                        "Port Ping - Exception Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }
    }

    private bool ValidateInput()
    {
      if (!txtPort.Text.IsInteger() || txtPort.Text.ToInt32() < 0 || txtPort.Text.ToInt32() > 65535)
      {
        EnableButtons(false);
        txtPort.SelectAll();
        txtPort.Focus();
        MessageBox.Show("The port must be a numeric value between 0 and 65536." + g.crlf2 + "Please enter a valid port.",
                        this.Text + " - Invalid Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      if (_wcfHostingModel == WCFHostingModel.IIS && txtWCFServiceName.Text.Trim().Contains(" "))
      {
        EnableButtons(false);
        txtWCFServiceName.SelectAll();
        txtWCFServiceName.Focus();
        MessageBox.Show("The WCF ServiceName cannot contain blank characters.",
                        this.Text + " - Invalid WCF Service Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      return true;
    }

    private void InitializeForm()
    {
      _initializationComplete = false;

      EnableButtons(false);

      lblEnvironmentValue.Text = _serviceEnvironment.Name;
      lblHostValue.Text = _serviceHost.Name;
      lblServiceTypeValue.Text = _serviceType.ToString();
      lblParentServiceValue.Text = _parentServiceSpec == null ? "None" : _parentServiceSpec.Name;

      if (_isNew)
      {
        this.Text = "Add WCF Service";

        switch (_serviceType)
        {
          case ServiceType.WindowsService:
            cboWebServiceBinding.SelectedIndex = -1;
            cboWebServiceBinding.Enabled = false;
            break;

          case ServiceType.WCFWebService:
            cboWebServiceBinding.SelectedIndex = 0;
            cboWebServiceBinding.Enabled = false;
            break;

          case ServiceType.WebSite:
            cboWebServiceBinding.SelectedIndex = -1;
            cboWebServiceBinding.Enabled = false;
            break;
        }
      }
      else
      {
        this.Text = "Update WCF Service";
        this.txtWCFServiceName.Text = _existingServiceSpec.WsServiceName;
        this.txtPort.Text = _existingServiceSpec.WsPort;

        switch (_serviceType)
        {
          case ServiceType.WCFWebService:

            for (int i = 0; i < cboWebServiceBinding.Items.Count; i++)
            {
              if (cboWebServiceBinding.Items[i].ToString() == _existingServiceSpec.WebServiceBinding.ToString())
              {
                cboWebServiceBinding.SelectedIndex = i;
                break;
              }
            }
            cboWebServiceBinding.Enabled = false;
            break;

          case ServiceType.WindowsService:
            cboWebServiceBinding.SelectedIndex = -1;
            cboWebServiceBinding.Enabled = false;
            break;

          case ServiceType.WebSite:
            cboWebServiceBinding.SelectedIndex = -1;
            cboWebServiceBinding.Enabled = false;
            break;
        }
      }

      if (_wcfHostingModel == WCFHostingModel.HostedInWindowsService)
      {
        lblWCFServiceName.Enabled = false;
        txtWCFServiceName.Enabled = false;
      }

      lblHostingModelValue.Text = _wcfHostingModel.ToString();

      _initializationComplete = true;
    }

    private void InputChanged(object sender, EventArgs e)
    {
      if (!_initializationComplete)
        return;

      EnableButtons(false);

      if (_wcfHostingModel == WCFHostingModel.HostedInWindowsService)
      {
        if (_isNew)
        {
          if (cboWebServiceBinding.Text.IsNotBlank() && txtPort.Text.IsNotBlank())
          {
            EnableButtons(true);
          }
        }
        else
        {
          if (cboWebServiceBinding.Text.Trim() != _existingServiceSpec.WebServiceBinding.ToString() || txtPort.Text.Trim() != _existingServiceSpec.WsPort.Trim())
          {
            EnableButtons(true);
          }
        }
      }
      else
      {
        if (_isNew)
        {
          if (txtWCFServiceName.Text.IsNotBlank() && cboWebServiceBinding.Text.IsNotBlank() && txtPort.Text.IsNotBlank())
          {
            EnableButtons(true);
          }
        }
        else
        {
          if (txtWCFServiceName.Text.Trim() != _existingServiceSpec.WsServiceName.Trim() ||
              cboWebServiceBinding.Text.Trim() != _existingServiceSpec.WebServiceBinding.ToString() ||
              txtPort.Text.Trim() != _existingServiceSpec.WsPort.Trim())
          {
            EnableButtons(true);
          }
        }
      }

      if (txtPort.Text.IsInteger())
      {
        btnPingPort.Enabled = true;
        if (cboWebServiceBinding.Text.IsNotBlank())
          btnTestWebService.Enabled = true;
      }

      
    }

    private void EnableButtons(bool enabled)
    {
      btnOK.Enabled = enabled;
      btnPingPort.Enabled = enabled;
      btnTestWebService.Enabled = enabled;
    }
  }
}

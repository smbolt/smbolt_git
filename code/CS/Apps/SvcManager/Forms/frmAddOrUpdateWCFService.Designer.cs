namespace Org.SvcManager
{
  partial class frmAddOrUpdateWCFService
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddOrUpdateWCFService));
      this.lblServiceTypeValue = new System.Windows.Forms.Label();
      this.lblHostValue = new System.Windows.Forms.Label();
      this.lblEnvironmentValue = new System.Windows.Forms.Label();
      this.lblServiceType = new System.Windows.Forms.Label();
      this.lblHost = new System.Windows.Forms.Label();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.txtWCFServiceName = new System.Windows.Forms.TextBox();
      this.lblWCFServiceName = new System.Windows.Forms.Label();
      this.lblPort = new System.Windows.Forms.Label();
      this.txtPort = new System.Windows.Forms.TextBox();
      this.lblHostingModel = new System.Windows.Forms.Label();
      this.lblHostingModelValue = new System.Windows.Forms.Label();
      this.cboWebServiceBinding = new System.Windows.Forms.ComboBox();
      this.lblWebServiceBinding = new System.Windows.Forms.Label();
      this.btnPingPort = new System.Windows.Forms.Button();
      this.btnTestWebService = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.lblParentService = new System.Windows.Forms.Label();
      this.lblParentServiceValue = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lblServiceTypeValue
      // 
      this.lblServiceTypeValue.AutoSize = true;
      this.lblServiceTypeValue.Location = new System.Drawing.Point(105, 56);
      this.lblServiceTypeValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblServiceTypeValue.Name = "lblServiceTypeValue";
      this.lblServiceTypeValue.Size = new System.Drawing.Size(90, 13);
      this.lblServiceTypeValue.TabIndex = 1;
      this.lblServiceTypeValue.Text = "Windows Service";
      // 
      // lblHostValue
      // 
      this.lblHostValue.AutoSize = true;
      this.lblHostValue.Location = new System.Drawing.Point(105, 34);
      this.lblHostValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblHostValue.Name = "lblHostValue";
      this.lblHostValue.Size = new System.Drawing.Size(68, 13);
      this.lblHostValue.TabIndex = 2;
      this.lblHostValue.Text = "HOSTNAME";
      // 
      // lblEnvironmentValue
      // 
      this.lblEnvironmentValue.AutoSize = true;
      this.lblEnvironmentValue.Location = new System.Drawing.Point(105, 13);
      this.lblEnvironmentValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblEnvironmentValue.Name = "lblEnvironmentValue";
      this.lblEnvironmentValue.Size = new System.Drawing.Size(29, 13);
      this.lblEnvironmentValue.TabIndex = 3;
      this.lblEnvironmentValue.Text = "Prod";
      // 
      // lblServiceType
      // 
      this.lblServiceType.AutoSize = true;
      this.lblServiceType.Location = new System.Drawing.Point(19, 56);
      this.lblServiceType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblServiceType.Name = "lblServiceType";
      this.lblServiceType.Size = new System.Drawing.Size(73, 13);
      this.lblServiceType.TabIndex = 4;
      this.lblServiceType.Text = "Service Type:";
      // 
      // lblHost
      // 
      this.lblHost.AutoSize = true;
      this.lblHost.Location = new System.Drawing.Point(19, 34);
      this.lblHost.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblHost.Name = "lblHost";
      this.lblHost.Size = new System.Drawing.Size(32, 13);
      this.lblHost.TabIndex = 5;
      this.lblHost.Text = "Host:";
      // 
      // lblEnvironment
      // 
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(19, 13);
      this.lblEnvironment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(69, 13);
      this.lblEnvironment.TabIndex = 6;
      this.lblEnvironment.Text = "Environment:";
      // 
      // txtWCFServiceName
      // 
      this.txtWCFServiceName.Location = new System.Drawing.Point(21, 154);
      this.txtWCFServiceName.Margin = new System.Windows.Forms.Padding(2);
      this.txtWCFServiceName.Name = "txtWCFServiceName";
      this.txtWCFServiceName.Size = new System.Drawing.Size(238, 20);
      this.txtWCFServiceName.TabIndex = 0;
      this.txtWCFServiceName.TextChanged += new System.EventHandler(this.InputChanged);
      // 
      // lblWCFServiceName
      // 
      this.lblWCFServiceName.AutoSize = true;
      this.lblWCFServiceName.Location = new System.Drawing.Point(19, 138);
      this.lblWCFServiceName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblWCFServiceName.Name = "lblWCFServiceName";
      this.lblWCFServiceName.Size = new System.Drawing.Size(198, 13);
      this.lblWCFServiceName.TabIndex = 7;
      this.lblWCFServiceName.Text = "WCF Service Name (when hosted in IIS)";
      // 
      // lblPort
      // 
      this.lblPort.AutoSize = true;
      this.lblPort.Location = new System.Drawing.Point(19, 228);
      this.lblPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblPort.Name = "lblPort";
      this.lblPort.Size = new System.Drawing.Size(26, 13);
      this.lblPort.TabIndex = 7;
      this.lblPort.Text = "Port";
      // 
      // txtPort
      // 
      this.txtPort.Location = new System.Drawing.Point(21, 244);
      this.txtPort.Margin = new System.Windows.Forms.Padding(2);
      this.txtPort.Name = "txtPort";
      this.txtPort.Size = new System.Drawing.Size(115, 20);
      this.txtPort.TabIndex = 2;
      this.txtPort.TextChanged += new System.EventHandler(this.InputChanged);
      // 
      // lblHostingModel
      // 
      this.lblHostingModel.AutoSize = true;
      this.lblHostingModel.Location = new System.Drawing.Point(19, 79);
      this.lblHostingModel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblHostingModel.Name = "lblHostingModel";
      this.lblHostingModel.Size = new System.Drawing.Size(78, 13);
      this.lblHostingModel.TabIndex = 4;
      this.lblHostingModel.Text = "Hosting Model:";
      // 
      // lblHostingModelValue
      // 
      this.lblHostingModelValue.AutoSize = true;
      this.lblHostingModelValue.Location = new System.Drawing.Point(105, 79);
      this.lblHostingModelValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblHostingModelValue.Name = "lblHostingModelValue";
      this.lblHostingModelValue.Size = new System.Drawing.Size(138, 13);
      this.lblHostingModelValue.TabIndex = 1;
      this.lblHostingModelValue.Text = "Hosted in Windows Service";
      // 
      // cboWebServiceBinding
      // 
      this.cboWebServiceBinding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboWebServiceBinding.FormattingEnabled = true;
      this.cboWebServiceBinding.Items.AddRange(new object[] {
            "BasicHttp",
            "WsHttp",
            "WsHttps",
            "NetTcp"});
      this.cboWebServiceBinding.Location = new System.Drawing.Point(22, 199);
      this.cboWebServiceBinding.Name = "cboWebServiceBinding";
      this.cboWebServiceBinding.Size = new System.Drawing.Size(237, 21);
      this.cboWebServiceBinding.TabIndex = 1;
      this.cboWebServiceBinding.SelectedIndexChanged += new System.EventHandler(this.InputChanged);
      // 
      // lblWebServiceBinding
      // 
      this.lblWebServiceBinding.AutoSize = true;
      this.lblWebServiceBinding.Location = new System.Drawing.Point(19, 183);
      this.lblWebServiceBinding.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblWebServiceBinding.Name = "lblWebServiceBinding";
      this.lblWebServiceBinding.Size = new System.Drawing.Size(107, 13);
      this.lblWebServiceBinding.TabIndex = 7;
      this.lblWebServiceBinding.Text = "Web Service Binding";
      // 
      // btnPingPort
      // 
      this.btnPingPort.Location = new System.Drawing.Point(144, 242);
      this.btnPingPort.Name = "btnPingPort";
      this.btnPingPort.Size = new System.Drawing.Size(115, 23);
      this.btnPingPort.TabIndex = 3;
      this.btnPingPort.Tag = "PingPort";
      this.btnPingPort.Text = "PingPort";
      this.btnPingPort.UseVisualStyleBackColor = true;
      this.btnPingPort.Click += new System.EventHandler(this.Action);
      // 
      // btnTestWebService
      // 
      this.btnTestWebService.Location = new System.Drawing.Point(21, 280);
      this.btnTestWebService.Name = "btnTestWebService";
      this.btnTestWebService.Size = new System.Drawing.Size(238, 23);
      this.btnTestWebService.TabIndex = 4;
      this.btnTestWebService.Tag = "TestWebService";
      this.btnTestWebService.Text = "Test Web Service (Ping)";
      this.btnTestWebService.UseVisualStyleBackColor = true;
      this.btnTestWebService.Click += new System.EventHandler(this.Action);
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(22, 319);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(114, 23);
      this.btnOK.TabIndex = 5;
      this.btnOK.Tag = "OK";
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.Action);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(144, 319);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(115, 23);
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      // 
      // lblParentService
      // 
      this.lblParentService.AutoSize = true;
      this.lblParentService.Location = new System.Drawing.Point(19, 101);
      this.lblParentService.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblParentService.Name = "lblParentService";
      this.lblParentService.Size = new System.Drawing.Size(80, 13);
      this.lblParentService.TabIndex = 4;
      this.lblParentService.Text = "Parent Service:";
      // 
      // lblParentServiceValue
      // 
      this.lblParentServiceValue.AutoSize = true;
      this.lblParentServiceValue.Location = new System.Drawing.Point(105, 101);
      this.lblParentServiceValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblParentServiceValue.Name = "lblParentServiceValue";
      this.lblParentServiceValue.Size = new System.Drawing.Size(33, 13);
      this.lblParentServiceValue.TabIndex = 1;
      this.lblParentServiceValue.Text = "None";
      // 
      // frmAddOrUpdateWCFService
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(278, 360);
      this.ControlBox = false;
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.btnTestWebService);
      this.Controls.Add(this.btnPingPort);
      this.Controls.Add(this.cboWebServiceBinding);
      this.Controls.Add(this.txtPort);
      this.Controls.Add(this.txtWCFServiceName);
      this.Controls.Add(this.lblPort);
      this.Controls.Add(this.lblWebServiceBinding);
      this.Controls.Add(this.lblWCFServiceName);
      this.Controls.Add(this.lblParentServiceValue);
      this.Controls.Add(this.lblHostingModelValue);
      this.Controls.Add(this.lblServiceTypeValue);
      this.Controls.Add(this.lblHostValue);
      this.Controls.Add(this.lblParentService);
      this.Controls.Add(this.lblEnvironmentValue);
      this.Controls.Add(this.lblHostingModel);
      this.Controls.Add(this.lblServiceType);
      this.Controls.Add(this.lblHost);
      this.Controls.Add(this.lblEnvironment);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAddOrUpdateWCFService";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Add WCF Service";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblServiceTypeValue;
    private System.Windows.Forms.Label lblHostValue;
    private System.Windows.Forms.Label lblEnvironmentValue;
    private System.Windows.Forms.Label lblServiceType;
    private System.Windows.Forms.Label lblHost;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.TextBox txtWCFServiceName;
    private System.Windows.Forms.Label lblWCFServiceName;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.TextBox txtPort;
    private System.Windows.Forms.Label lblHostingModel;
    private System.Windows.Forms.Label lblHostingModelValue;
    private System.Windows.Forms.ComboBox cboWebServiceBinding;
    private System.Windows.Forms.Label lblWebServiceBinding;
    private System.Windows.Forms.Button btnPingPort;
    private System.Windows.Forms.Button btnTestWebService;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label lblParentService;
    private System.Windows.Forms.Label lblParentServiceValue;
  }
}
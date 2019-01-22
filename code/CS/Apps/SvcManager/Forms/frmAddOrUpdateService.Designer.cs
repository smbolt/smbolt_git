namespace Org.SvcManager
{
  partial class frmAddOrUpdateTaskService
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddOrUpdateTaskService));
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.lblTaskServiceName = new System.Windows.Forms.Label();
      this.txtTaskServiceName = new System.Windows.Forms.TextBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.lblEnvironmentValue = new System.Windows.Forms.Label();
      this.lblHost = new System.Windows.Forms.Label();
      this.lblHostValue = new System.Windows.Forms.Label();
      this.lblServiceType = new System.Windows.Forms.Label();
      this.lblServiceTypeValue = new System.Windows.Forms.Label();
      this.SuspendLayout();
      //
      // lblEnvironment
      //
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(28, 27);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(102, 20);
      this.lblEnvironment.TabIndex = 0;
      this.lblEnvironment.Text = "Environment:";
      //
      // lblTaskServiceName
      //
      this.lblTaskServiceName.AutoSize = true;
      this.lblTaskServiceName.Location = new System.Drawing.Point(28, 151);
      this.lblTaskServiceName.Name = "lblTaskServiceName";
      this.lblTaskServiceName.Size = new System.Drawing.Size(145, 20);
      this.lblTaskServiceName.TabIndex = 0;
      this.lblTaskServiceName.Text = "Task Service Name";
      //
      // txtTaskServiceName
      //
      this.txtTaskServiceName.Location = new System.Drawing.Point(32, 175);
      this.txtTaskServiceName.Name = "txtTaskServiceName";
      this.txtTaskServiceName.Size = new System.Drawing.Size(386, 26);
      this.txtTaskServiceName.TabIndex = 2;
      this.txtTaskServiceName.TextChanged += new System.EventHandler(this.CriteriaChanged);
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(127, 313);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(190, 41);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      //
      // btnOK
      //
      this.btnOK.Location = new System.Drawing.Point(127, 258);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(190, 41);
      this.btnOK.TabIndex = 4;
      this.btnOK.Tag = "OK";
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.Action);
      //
      // lblEnvironmentValue
      //
      this.lblEnvironmentValue.AutoSize = true;
      this.lblEnvironmentValue.Location = new System.Drawing.Point(158, 27);
      this.lblEnvironmentValue.Name = "lblEnvironmentValue";
      this.lblEnvironmentValue.Size = new System.Drawing.Size(42, 20);
      this.lblEnvironmentValue.TabIndex = 0;
      this.lblEnvironmentValue.Text = "Prod";
      //
      // lblHost
      //
      this.lblHost.AutoSize = true;
      this.lblHost.Location = new System.Drawing.Point(28, 60);
      this.lblHost.Name = "lblHost";
      this.lblHost.Size = new System.Drawing.Size(47, 20);
      this.lblHost.TabIndex = 0;
      this.lblHost.Text = "Host:";
      //
      // lblHostValue
      //
      this.lblHostValue.AutoSize = true;
      this.lblHostValue.Location = new System.Drawing.Point(158, 60);
      this.lblHostValue.Name = "lblHostValue";
      this.lblHostValue.Size = new System.Drawing.Size(99, 20);
      this.lblHostValue.TabIndex = 0;
      this.lblHostValue.Text = "HOSTNAME";
      //
      // lblServiceType
      //
      this.lblServiceType.AutoSize = true;
      this.lblServiceType.Location = new System.Drawing.Point(28, 94);
      this.lblServiceType.Name = "lblServiceType";
      this.lblServiceType.Size = new System.Drawing.Size(103, 20);
      this.lblServiceType.TabIndex = 0;
      this.lblServiceType.Text = "Service Type:";
      //
      // lblServiceTypeValue
      //
      this.lblServiceTypeValue.AutoSize = true;
      this.lblServiceTypeValue.Location = new System.Drawing.Point(158, 94);
      this.lblServiceTypeValue.Name = "lblServiceTypeValue";
      this.lblServiceTypeValue.Size = new System.Drawing.Size(129, 20);
      this.lblServiceTypeValue.TabIndex = 0;
      this.lblServiceTypeValue.Text = "Windows Service";
      //
      // frmAddOrUpdateTaskService
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(450, 380);
      this.ControlBox = false;
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.txtTaskServiceName);
      this.Controls.Add(this.lblTaskServiceName);
      this.Controls.Add(this.lblServiceTypeValue);
      this.Controls.Add(this.lblHostValue);
      this.Controls.Add(this.lblEnvironmentValue);
      this.Controls.Add(this.lblServiceType);
      this.Controls.Add(this.lblHost);
      this.Controls.Add(this.lblEnvironment);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAddOrUpdateTaskService";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Add New Service";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.Label lblTaskServiceName;
    private System.Windows.Forms.TextBox txtTaskServiceName;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label lblEnvironmentValue;
    private System.Windows.Forms.Label lblHost;
    private System.Windows.Forms.Label lblHostValue;
    private System.Windows.Forms.Label lblServiceType;
    private System.Windows.Forms.Label lblServiceTypeValue;
  }
}
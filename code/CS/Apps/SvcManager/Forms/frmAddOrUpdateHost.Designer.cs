namespace Org.SvcManager
{
  partial class frmAddOrUpdateHost
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddOrUpdateHost));
      this.lblHostName = new System.Windows.Forms.Label();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.lblEnvironmentValue = new System.Windows.Forms.Label();
      this.txtHostName = new System.Windows.Forms.TextBox();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblHostName
      // 
      this.lblHostName.AutoSize = true;
      this.lblHostName.Location = new System.Drawing.Point(12, 58);
      this.lblHostName.Name = "lblHostName";
      this.lblHostName.Size = new System.Drawing.Size(89, 20);
      this.lblHostName.TabIndex = 0;
      this.lblHostName.Text = "Host Name";
      // 
      // lblEnvironment
      // 
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(12, 13);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(102, 20);
      this.lblEnvironment.TabIndex = 1;
      this.lblEnvironment.Text = "Environment:";
      // 
      // lblEnvironmentValue
      // 
      this.lblEnvironmentValue.AutoSize = true;
      this.lblEnvironmentValue.Location = new System.Drawing.Point(133, 13);
      this.lblEnvironmentValue.Name = "lblEnvironmentValue";
      this.lblEnvironmentValue.Size = new System.Drawing.Size(42, 20);
      this.lblEnvironmentValue.TabIndex = 2;
      this.lblEnvironmentValue.Text = "Prod";
      // 
      // txtHostName
      // 
      this.txtHostName.Location = new System.Drawing.Point(16, 92);
      this.txtHostName.Name = "txtHostName";
      this.txtHostName.Size = new System.Drawing.Size(386, 26);
      this.txtHostName.TabIndex = 3;
      this.txtHostName.TextChanged += new System.EventHandler(this.txtHostName_TextChanged);
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.Location = new System.Drawing.Point(114, 159);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(190, 41);
      this.btnOK.TabIndex = 6;
      this.btnOK.Tag = "OK";
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.Action);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(114, 211);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(190, 41);
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      // 
      // frmAddOrUpdateHost
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(421, 304);
      this.ControlBox = false;
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.txtHostName);
      this.Controls.Add(this.lblEnvironment);
      this.Controls.Add(this.lblEnvironmentValue);
      this.Controls.Add(this.lblHostName);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAddOrUpdateHost";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Add New Host";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblHostName;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.Label lblEnvironmentValue;
    private System.Windows.Forms.TextBox txtHostName;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
  }
}
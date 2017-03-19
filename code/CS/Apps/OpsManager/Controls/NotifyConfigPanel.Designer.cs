namespace Org.OpsManager.Controls
{
  partial class NotifyConfigPanel
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lblTreeNodePath = new System.Windows.Forms.Label();
      this.txtSupportPhone = new System.Windows.Forms.TextBox();
      this.txtSupportEmail = new System.Windows.Forms.TextBox();
      this.chkSendEmail = new System.Windows.Forms.CheckBox();
      this.chkSendSms = new System.Windows.Forms.CheckBox();
      this.chkIsActive = new System.Windows.Forms.CheckBox();
      this.lblSupportEmail = new System.Windows.Forms.Label();
      this.lblSupportPhone = new System.Windows.Forms.Label();
      this.lblName = new System.Windows.Forms.Label();
      this.txtName = new System.Windows.Forms.TextBox();
      this.btnSave = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblTreeNodePath
      // 
      this.lblTreeNodePath.AutoSize = true;
      this.lblTreeNodePath.Location = new System.Drawing.Point(20, 20);
      this.lblTreeNodePath.Name = "lblTreeNodePath";
      this.lblTreeNodePath.Size = new System.Drawing.Size(89, 13);
      this.lblTreeNodePath.TabIndex = 6;
      this.lblTreeNodePath.Text = "Tree Node Path: ";
      // 
      // txtSupportPhone
      // 
      this.txtSupportPhone.Location = new System.Drawing.Point(400, 66);
      this.txtSupportPhone.Name = "txtSupportPhone";
      this.txtSupportPhone.Size = new System.Drawing.Size(125, 20);
      this.txtSupportPhone.TabIndex = 2;
      this.txtSupportPhone.TextChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // txtSupportEmail
      // 
      this.txtSupportEmail.Location = new System.Drawing.Point(230, 66);
      this.txtSupportEmail.Name = "txtSupportEmail";
      this.txtSupportEmail.Size = new System.Drawing.Size(150, 20);
      this.txtSupportEmail.TabIndex = 1;
      this.txtSupportEmail.TextChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // chkSendEmail
      // 
      this.chkSendEmail.AutoSize = true;
      this.chkSendEmail.Location = new System.Drawing.Point(230, 92);
      this.chkSendEmail.Name = "chkSendEmail";
      this.chkSendEmail.Size = new System.Drawing.Size(79, 17);
      this.chkSendEmail.TabIndex = 4;
      this.chkSendEmail.Text = "Send Email";
      this.chkSendEmail.UseVisualStyleBackColor = true;
      this.chkSendEmail.CheckedChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // chkSendSms
      // 
      this.chkSendSms.AutoSize = true;
      this.chkSendSms.Location = new System.Drawing.Point(400, 92);
      this.chkSendSms.Name = "chkSendSms";
      this.chkSendSms.Size = new System.Drawing.Size(77, 17);
      this.chkSendSms.TabIndex = 5;
      this.chkSendSms.Text = "Send SMS";
      this.chkSendSms.UseVisualStyleBackColor = true;
      this.chkSendSms.CheckedChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // chkIsActive
      // 
      this.chkIsActive.AutoSize = true;
      this.chkIsActive.Location = new System.Drawing.Point(40, 92);
      this.chkIsActive.Name = "chkIsActive";
      this.chkIsActive.Size = new System.Drawing.Size(67, 17);
      this.chkIsActive.TabIndex = 3;
      this.chkIsActive.Text = "Is Active";
      this.chkIsActive.UseVisualStyleBackColor = true;
      this.chkIsActive.CheckedChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // lblSupportEmail
      // 
      this.lblSupportEmail.AutoSize = true;
      this.lblSupportEmail.Location = new System.Drawing.Point(230, 50);
      this.lblSupportEmail.Name = "lblSupportEmail";
      this.lblSupportEmail.Size = new System.Drawing.Size(72, 13);
      this.lblSupportEmail.TabIndex = 8;
      this.lblSupportEmail.Tag = "";
      this.lblSupportEmail.Text = "Support Email";
      // 
      // lblSupportPhone
      // 
      this.lblSupportPhone.AutoSize = true;
      this.lblSupportPhone.Location = new System.Drawing.Point(400, 50);
      this.lblSupportPhone.Name = "lblSupportPhone";
      this.lblSupportPhone.Size = new System.Drawing.Size(78, 13);
      this.lblSupportPhone.TabIndex = 9;
      this.lblSupportPhone.Tag = "";
      this.lblSupportPhone.Text = "Support Phone";
      // 
      // lblName
      // 
      this.lblName.AutoSize = true;
      this.lblName.Location = new System.Drawing.Point(40, 50);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(98, 13);
      this.lblName.TabIndex = 7;
      this.lblName.Tag = "";
      this.lblName.Text = "Notify Config Name";
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(40, 66);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(168, 20);
      this.txtName.TabIndex = 0;
      this.txtName.TextChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // btnSave
      // 
      this.btnSave.Enabled = false;
      this.btnSave.Location = new System.Drawing.Point(40, 130);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(100, 35);
      this.btnSave.TabIndex = 10;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Save);
      // 
      // NotifyConfigPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.lblTreeNodePath);
      this.Controls.Add(this.txtSupportPhone);
      this.Controls.Add(this.txtSupportEmail);
      this.Controls.Add(this.chkSendEmail);
      this.Controls.Add(this.chkSendSms);
      this.Controls.Add(this.chkIsActive);
      this.Controls.Add(this.lblSupportEmail);
      this.Controls.Add(this.lblSupportPhone);
      this.Controls.Add(this.lblName);
      this.Controls.Add(this.txtName);
      this.Name = "NotifyConfigPanel";
      this.Size = new System.Drawing.Size(1574, 964);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.Label lblSupportPhone;
    private System.Windows.Forms.Label lblSupportEmail;
    private System.Windows.Forms.CheckBox chkIsActive;
    private System.Windows.Forms.CheckBox chkSendSms;
    private System.Windows.Forms.CheckBox chkSendEmail;
    private System.Windows.Forms.TextBox txtSupportEmail;
    private System.Windows.Forms.TextBox txtSupportPhone;
    private System.Windows.Forms.Label lblTreeNodePath;
    private System.Windows.Forms.Button btnSave;
  }
}

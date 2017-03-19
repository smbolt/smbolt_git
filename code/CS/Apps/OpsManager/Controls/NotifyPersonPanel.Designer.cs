namespace Org.OpsManager.Controls
{
  partial class NotifyPersonPanel
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
      this.lblName = new System.Windows.Forms.Label();
      this.txtName = new System.Windows.Forms.TextBox();
      this.chkIsActive = new System.Windows.Forms.CheckBox();
      this.lblSmsNumber = new System.Windows.Forms.Label();
      this.lblEmailAddress = new System.Windows.Forms.Label();
      this.txtSmsNumber = new System.Windows.Forms.TextBox();
      this.txtEmailAddress = new System.Windows.Forms.TextBox();
      this.chkSmsActive = new System.Windows.Forms.CheckBox();
      this.chkEmailActive = new System.Windows.Forms.CheckBox();
      this.lblTreeNodePath = new System.Windows.Forms.Label();
      this.chkIsActiveInGroup = new System.Windows.Forms.CheckBox();
      this.lblDivider = new System.Windows.Forms.Label();
      this.btnSave = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblName
      // 
      this.lblName.AutoSize = true;
      this.lblName.Location = new System.Drawing.Point(40, 50);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(35, 13);
      this.lblName.TabIndex = 8;
      this.lblName.Text = "Name";
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(40, 66);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(168, 20);
      this.txtName.TabIndex = 0;
      this.txtName.TextChanged += new System.EventHandler(this.DirtyCheck);
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
      // lblSmsNumber
      // 
      this.lblSmsNumber.AutoSize = true;
      this.lblSmsNumber.Location = new System.Drawing.Point(397, 50);
      this.lblSmsNumber.Name = "lblSmsNumber";
      this.lblSmsNumber.Size = new System.Drawing.Size(70, 13);
      this.lblSmsNumber.TabIndex = 10;
      this.lblSmsNumber.Text = "SMS Number";
      // 
      // lblEmailAddress
      // 
      this.lblEmailAddress.AutoSize = true;
      this.lblEmailAddress.Location = new System.Drawing.Point(230, 50);
      this.lblEmailAddress.Name = "lblEmailAddress";
      this.lblEmailAddress.Size = new System.Drawing.Size(73, 13);
      this.lblEmailAddress.TabIndex = 9;
      this.lblEmailAddress.Text = "Email Address";
      // 
      // txtSmsNumber
      // 
      this.txtSmsNumber.Location = new System.Drawing.Point(400, 66);
      this.txtSmsNumber.Name = "txtSmsNumber";
      this.txtSmsNumber.Size = new System.Drawing.Size(125, 20);
      this.txtSmsNumber.TabIndex = 2;
      this.txtSmsNumber.TextChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // txtEmailAddress
      // 
      this.txtEmailAddress.Location = new System.Drawing.Point(230, 66);
      this.txtEmailAddress.Name = "txtEmailAddress";
      this.txtEmailAddress.Size = new System.Drawing.Size(150, 20);
      this.txtEmailAddress.TabIndex = 1;
      this.txtEmailAddress.TextChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // chkSmsActive
      // 
      this.chkSmsActive.AutoSize = true;
      this.chkSmsActive.Location = new System.Drawing.Point(400, 92);
      this.chkSmsActive.Name = "chkSmsActive";
      this.chkSmsActive.Size = new System.Drawing.Size(82, 17);
      this.chkSmsActive.TabIndex = 5;
      this.chkSmsActive.Text = "SMS Active";
      this.chkSmsActive.UseVisualStyleBackColor = true;
      this.chkSmsActive.CheckedChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // chkEmailActive
      // 
      this.chkEmailActive.AutoSize = true;
      this.chkEmailActive.Location = new System.Drawing.Point(230, 92);
      this.chkEmailActive.Name = "chkEmailActive";
      this.chkEmailActive.Size = new System.Drawing.Size(84, 17);
      this.chkEmailActive.TabIndex = 4;
      this.chkEmailActive.Text = "Email Active";
      this.chkEmailActive.UseVisualStyleBackColor = true;
      this.chkEmailActive.CheckedChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // lblTreeNodePath
      // 
      this.lblTreeNodePath.AutoSize = true;
      this.lblTreeNodePath.Location = new System.Drawing.Point(20, 20);
      this.lblTreeNodePath.Name = "lblTreeNodePath";
      this.lblTreeNodePath.Size = new System.Drawing.Size(89, 13);
      this.lblTreeNodePath.TabIndex = 7;
      this.lblTreeNodePath.Text = "Tree Node Path: ";
      // 
      // chkIsActiveInGroup
      // 
      this.chkIsActiveInGroup.AutoSize = true;
      this.chkIsActiveInGroup.Location = new System.Drawing.Point(40, 137);
      this.chkIsActiveInGroup.Name = "chkIsActiveInGroup";
      this.chkIsActiveInGroup.Size = new System.Drawing.Size(110, 17);
      this.chkIsActiveInGroup.TabIndex = 6;
      this.chkIsActiveInGroup.Text = "Is Active in Group";
      this.chkIsActiveInGroup.UseVisualStyleBackColor = true;
      this.chkIsActiveInGroup.CheckedChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // lblDivider
      // 
      this.lblDivider.AutoSize = true;
      this.lblDivider.Location = new System.Drawing.Point(37, 114);
      this.lblDivider.Name = "lblDivider";
      this.lblDivider.Size = new System.Drawing.Size(493, 13);
      this.lblDivider.TabIndex = 11;
      this.lblDivider.Text = "---------------------------------------------------------------------------------" +
    "--------------------------------------------------------------------------------" +
    "-";
      // 
      // btnSave
      // 
      this.btnSave.Enabled = false;
      this.btnSave.Location = new System.Drawing.Point(40, 170);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(100, 35);
      this.btnSave.TabIndex = 13;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Save);
      // 
      // NotifyPersonPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.lblDivider);
      this.Controls.Add(this.chkIsActiveInGroup);
      this.Controls.Add(this.lblTreeNodePath);
      this.Controls.Add(this.chkEmailActive);
      this.Controls.Add(this.chkSmsActive);
      this.Controls.Add(this.txtEmailAddress);
      this.Controls.Add(this.txtSmsNumber);
      this.Controls.Add(this.lblEmailAddress);
      this.Controls.Add(this.lblSmsNumber);
      this.Controls.Add(this.chkIsActive);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.lblName);
      this.Name = "NotifyPersonPanel";
      this.Size = new System.Drawing.Size(1574, 964);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.CheckBox chkIsActive;
    private System.Windows.Forms.Label lblSmsNumber;
    private System.Windows.Forms.Label lblEmailAddress;
    private System.Windows.Forms.TextBox txtSmsNumber;
    private System.Windows.Forms.TextBox txtEmailAddress;
    private System.Windows.Forms.CheckBox chkSmsActive;
    private System.Windows.Forms.CheckBox chkEmailActive;
    private System.Windows.Forms.Label lblTreeNodePath;
    private System.Windows.Forms.CheckBox chkIsActiveInGroup;
    private System.Windows.Forms.Label lblDivider;
    private System.Windows.Forms.Button btnSave;
  }
}

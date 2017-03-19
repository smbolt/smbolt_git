namespace Org.OpsManager.Controls
{
  partial class NotifyConfigSetPanel
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
      this.lblNotifyConfigSetName = new System.Windows.Forms.Label();
      this.chkIsActive = new System.Windows.Forms.CheckBox();
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
      this.lblTreeNodePath.TabIndex = 2;
      this.lblTreeNodePath.Text = "Tree Node Path: ";
      // 
      // lblNotifyConfigSetName
      // 
      this.lblNotifyConfigSetName.AutoSize = true;
      this.lblNotifyConfigSetName.Location = new System.Drawing.Point(40, 50);
      this.lblNotifyConfigSetName.Name = "lblNotifyConfigSetName";
      this.lblNotifyConfigSetName.Size = new System.Drawing.Size(117, 13);
      this.lblNotifyConfigSetName.TabIndex = 3;
      this.lblNotifyConfigSetName.Text = "Notify Config Set Name";
      // 
      // chkIsActive
      // 
      this.chkIsActive.AutoSize = true;
      this.chkIsActive.Location = new System.Drawing.Point(40, 92);
      this.chkIsActive.Name = "chkIsActive";
      this.chkIsActive.Size = new System.Drawing.Size(67, 17);
      this.chkIsActive.TabIndex = 1;
      this.chkIsActive.Text = "Is Active";
      this.chkIsActive.UseVisualStyleBackColor = true;
      this.chkIsActive.CheckedChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(40, 66);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(216, 20);
      this.txtName.TabIndex = 0;
      this.txtName.TextChanged += new System.EventHandler(this.DirtyCheck);
      // 
      // btnSave
      // 
      this.btnSave.Enabled = false;
      this.btnSave.Location = new System.Drawing.Point(40, 130);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(100, 35);
      this.btnSave.TabIndex = 4;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Save);
      // 
      // NotifyConfigSetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.lblTreeNodePath);
      this.Controls.Add(this.lblNotifyConfigSetName);
      this.Controls.Add(this.chkIsActive);
      this.Controls.Add(this.txtName);
      this.Name = "NotifyConfigSetPanel";
      this.Size = new System.Drawing.Size(1267, 758);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.CheckBox chkIsActive;
    private System.Windows.Forms.Label lblNotifyConfigSetName;
    private System.Windows.Forms.Label lblTreeNodePath;
    private System.Windows.Forms.Button btnSave;
  }
}

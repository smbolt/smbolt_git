namespace Org.OpsManager.Controls
{
  partial class NotifyGroupPanel
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
      this.txtName = new System.Windows.Forms.TextBox();
      this.lblName = new System.Windows.Forms.Label();
      this.chkIsActive = new System.Windows.Forms.CheckBox();
      this.chkIsActiveInEvent = new System.Windows.Forms.CheckBox();
      this.lblTreeNodePath = new System.Windows.Forms.Label();
      this.lblDivider = new System.Windows.Forms.Label();
      this.btnSave = new System.Windows.Forms.Button();
      this.SuspendLayout();
      //
      // txtName
      //
      this.txtName.Location = new System.Drawing.Point(40, 66);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(175, 20);
      this.txtName.TabIndex = 0;
      this.txtName.TextChanged += new System.EventHandler(this.DirtyCheck);
      //
      // lblName
      //
      this.lblName.AutoSize = true;
      this.lblName.Location = new System.Drawing.Point(40, 50);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(67, 13);
      this.lblName.TabIndex = 4;
      this.lblName.Text = "Group Name";
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
      // chkIsActiveInEvent
      //
      this.chkIsActiveInEvent.AutoSize = true;
      this.chkIsActiveInEvent.Location = new System.Drawing.Point(40, 137);
      this.chkIsActiveInEvent.Name = "chkIsActiveInEvent";
      this.chkIsActiveInEvent.Size = new System.Drawing.Size(109, 17);
      this.chkIsActiveInEvent.TabIndex = 2;
      this.chkIsActiveInEvent.Text = "Is Active in Event";
      this.chkIsActiveInEvent.UseVisualStyleBackColor = true;
      this.chkIsActiveInEvent.CheckedChanged += new System.EventHandler(this.DirtyCheck);
      //
      // lblTreeNodePath
      //
      this.lblTreeNodePath.AutoSize = true;
      this.lblTreeNodePath.Location = new System.Drawing.Point(20, 20);
      this.lblTreeNodePath.Name = "lblTreeNodePath";
      this.lblTreeNodePath.Size = new System.Drawing.Size(89, 13);
      this.lblTreeNodePath.TabIndex = 3;
      this.lblTreeNodePath.Text = "Tree Node Path: ";
      //
      // lblDivider
      //
      this.lblDivider.AutoSize = true;
      this.lblDivider.Location = new System.Drawing.Point(37, 114);
      this.lblDivider.Name = "lblDivider";
      this.lblDivider.Size = new System.Drawing.Size(181, 13);
      this.lblDivider.TabIndex = 5;
      this.lblDivider.Text = "----------------------------------------------------------";
      //
      // btnSave
      //
      this.btnSave.Enabled = false;
      this.btnSave.Location = new System.Drawing.Point(40, 170);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(100, 35);
      this.btnSave.TabIndex = 12;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Save);
      //
      // NotifyGroupPanel
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.lblDivider);
      this.Controls.Add(this.lblTreeNodePath);
      this.Controls.Add(this.chkIsActiveInEvent);
      this.Controls.Add(this.chkIsActive);
      this.Controls.Add(this.lblName);
      this.Controls.Add(this.txtName);
      this.Location = new System.Drawing.Point(20, 20);
      this.Name = "NotifyGroupPanel";
      this.Size = new System.Drawing.Size(1574, 964);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.CheckBox chkIsActive;
    private System.Windows.Forms.CheckBox chkIsActiveInEvent;
    private System.Windows.Forms.Label lblTreeNodePath;
    private System.Windows.Forms.Label lblDivider;
    private System.Windows.Forms.Button btnSave;
  }
}

namespace Org.OpsManager
{
  partial class frmAssignTaskGroup
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssignTaskGroup));
      this.lblScheduledTaskName = new System.Windows.Forms.Label();
      this.lblScheduledTaskNameValue = new System.Windows.Forms.Label();
      this.cboTaskGroups = new System.Windows.Forms.ComboBox();
      this.lblTaskGroup = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblScheduledTaskName
      // 
      this.lblScheduledTaskName.AutoSize = true;
      this.lblScheduledTaskName.Location = new System.Drawing.Point(12, 16);
      this.lblScheduledTaskName.Name = "lblScheduledTaskName";
      this.lblScheduledTaskName.Size = new System.Drawing.Size(116, 13);
      this.lblScheduledTaskName.TabIndex = 0;
      this.lblScheduledTaskName.Text = "Scheduled Task Name";
      // 
      // lblScheduledTaskNameValue
      // 
      this.lblScheduledTaskNameValue.AutoSize = true;
      this.lblScheduledTaskNameValue.Location = new System.Drawing.Point(145, 16);
      this.lblScheduledTaskNameValue.Name = "lblScheduledTaskNameValue";
      this.lblScheduledTaskNameValue.Size = new System.Drawing.Size(114, 13);
      this.lblScheduledTaskNameValue.TabIndex = 0;
      this.lblScheduledTaskNameValue.Text = "[scheduled task name]";
      // 
      // cboTaskGroups
      // 
      this.cboTaskGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTaskGroups.FormattingEnabled = true;
      this.cboTaskGroups.Location = new System.Drawing.Point(15, 70);
      this.cboTaskGroups.Name = "cboTaskGroups";
      this.cboTaskGroups.Size = new System.Drawing.Size(331, 21);
      this.cboTaskGroups.TabIndex = 1;
      this.cboTaskGroups.SelectedIndexChanged += new System.EventHandler(this.cboTaskGroups_SelectedIndexChanged);
      // 
      // lblTaskGroup
      // 
      this.lblTaskGroup.AutoSize = true;
      this.lblTaskGroup.Location = new System.Drawing.Point(12, 54);
      this.lblTaskGroup.Name = "lblTaskGroup";
      this.lblTaskGroup.Size = new System.Drawing.Size(96, 13);
      this.lblTaskGroup.TabIndex = 0;
      this.lblTaskGroup.Text = "Select Task Group";
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(15, 124);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(118, 23);
      this.btnOK.TabIndex = 2;
      this.btnOK.Tag = "OK";
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.Action);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(228, 124);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(118, 23);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      // 
      // frmAssignTaskGroup
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(358, 173);
      this.ControlBox = false;
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.cboTaskGroups);
      this.Controls.Add(this.lblScheduledTaskNameValue);
      this.Controls.Add(this.lblTaskGroup);
      this.Controls.Add(this.lblScheduledTaskName);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmAssignTaskGroup";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Assign Task Group for Scheduled Task";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblScheduledTaskName;
    private System.Windows.Forms.Label lblScheduledTaskNameValue;
    private System.Windows.Forms.ComboBox cboTaskGroups;
    private System.Windows.Forms.Label lblTaskGroup;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
  }
}
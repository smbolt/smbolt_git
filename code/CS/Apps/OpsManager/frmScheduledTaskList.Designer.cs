namespace Org.OpsManager
{
  partial class frmScheduledTaskList
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
      this.cboScheduledTasks = new System.Windows.Forms.ComboBox();
      this.ckCopySchedule = new System.Windows.Forms.CheckBox();
      this.ckCopyParameters = new System.Windows.Forms.CheckBox();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.lblScheduleTask = new System.Windows.Forms.Label();
      this.SuspendLayout();
      //
      // cboScheduledTasks
      //
      this.cboScheduledTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboScheduledTasks.FormattingEnabled = true;
      this.cboScheduledTasks.Location = new System.Drawing.Point(21, 32);
      this.cboScheduledTasks.Margin = new System.Windows.Forms.Padding(2);
      this.cboScheduledTasks.Name = "cboScheduledTasks";
      this.cboScheduledTasks.Size = new System.Drawing.Size(299, 21);
      this.cboScheduledTasks.TabIndex = 0;
      this.cboScheduledTasks.SelectedIndexChanged += new System.EventHandler(this.cboScheduledTasks_SelectedIndexChanged);
      //
      // ckCopySchedule
      //
      this.ckCopySchedule.AutoSize = true;
      this.ckCopySchedule.Checked = true;
      this.ckCopySchedule.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckCopySchedule.Location = new System.Drawing.Point(21, 62);
      this.ckCopySchedule.Margin = new System.Windows.Forms.Padding(2);
      this.ckCopySchedule.Name = "ckCopySchedule";
      this.ckCopySchedule.Size = new System.Drawing.Size(98, 17);
      this.ckCopySchedule.TabIndex = 1;
      this.ckCopySchedule.Text = "Copy Schedule";
      this.ckCopySchedule.UseVisualStyleBackColor = true;
      this.ckCopySchedule.CheckedChanged += new System.EventHandler(this.CheckedChanged);
      //
      // ckCopyParameters
      //
      this.ckCopyParameters.AutoSize = true;
      this.ckCopyParameters.Checked = true;
      this.ckCopyParameters.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckCopyParameters.Location = new System.Drawing.Point(21, 81);
      this.ckCopyParameters.Margin = new System.Windows.Forms.Padding(2);
      this.ckCopyParameters.Name = "ckCopyParameters";
      this.ckCopyParameters.Size = new System.Drawing.Size(106, 17);
      this.ckCopyParameters.TabIndex = 1;
      this.ckCopyParameters.Text = "Copy Parameters";
      this.ckCopyParameters.UseVisualStyleBackColor = true;
      this.ckCopyParameters.CheckedChanged += new System.EventHandler(this.CheckedChanged);
      //
      // btnOK
      //
      this.btnOK.Location = new System.Drawing.Point(21, 110);
      this.btnOK.Margin = new System.Windows.Forms.Padding(2);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(117, 23);
      this.btnOK.TabIndex = 2;
      this.btnOK.Tag = "OK";
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.Action);
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(201, 110);
      this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(117, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      //
      // lblScheduleTask
      //
      this.lblScheduleTask.AutoSize = true;
      this.lblScheduleTask.Location = new System.Drawing.Point(21, 15);
      this.lblScheduleTask.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblScheduleTask.Name = "lblScheduleTask";
      this.lblScheduleTask.Size = new System.Drawing.Size(139, 13);
      this.lblScheduleTask.TabIndex = 3;
      this.lblScheduleTask.Text = "Select the task to copy from";
      //
      // frmScheduledTaskList
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(341, 157);
      this.ControlBox = false;
      this.Controls.Add(this.lblScheduleTask);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.ckCopyParameters);
      this.Controls.Add(this.ckCopySchedule);
      this.Controls.Add(this.cboScheduledTasks);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "frmScheduledTaskList";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Copy Schedule and Parameters from Existing Task";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cboScheduledTasks;
    private System.Windows.Forms.CheckBox ckCopySchedule;
    private System.Windows.Forms.CheckBox ckCopyParameters;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label lblScheduleTask;
  }
}
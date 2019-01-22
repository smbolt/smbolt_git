namespace Org.OpsManager.Controls
{
  partial class NotifyEventPanel
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
      this.txtDefaultSubject = new System.Windows.Forms.TextBox();
      this.lblDefaultSubject = new System.Windows.Forms.Label();
      this.chkIsActive = new System.Windows.Forms.CheckBox();
      this.lblTreeNodePath = new System.Windows.Forms.Label();
      this.btnSave = new System.Windows.Forms.Button();
      this.SuspendLayout();
      //
      // txtName
      //
      this.txtName.Location = new System.Drawing.Point(40, 66);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(176, 20);
      this.txtName.TabIndex = 0;
      this.txtName.TextChanged += new System.EventHandler(this.DirtyCheck);
      //
      // lblName
      //
      this.lblName.AutoSize = true;
      this.lblName.Location = new System.Drawing.Point(40, 50);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(66, 13);
      this.lblName.TabIndex = 4;
      this.lblName.Text = "Event Name";
      //
      // txtDefaultSubject
      //
      this.txtDefaultSubject.Location = new System.Drawing.Point(235, 66);
      this.txtDefaultSubject.Name = "txtDefaultSubject";
      this.txtDefaultSubject.Size = new System.Drawing.Size(388, 20);
      this.txtDefaultSubject.TabIndex = 1;
      this.txtDefaultSubject.TextChanged += new System.EventHandler(this.DirtyCheck);
      //
      // lblDefaultSubject
      //
      this.lblDefaultSubject.AutoSize = true;
      this.lblDefaultSubject.Location = new System.Drawing.Point(235, 50);
      this.lblDefaultSubject.Name = "lblDefaultSubject";
      this.lblDefaultSubject.Size = new System.Drawing.Size(80, 13);
      this.lblDefaultSubject.TabIndex = 5;
      this.lblDefaultSubject.Text = "Default Subject";
      //
      // chkIsActive
      //
      this.chkIsActive.AutoSize = true;
      this.chkIsActive.Location = new System.Drawing.Point(40, 92);
      this.chkIsActive.Name = "chkIsActive";
      this.chkIsActive.Size = new System.Drawing.Size(67, 17);
      this.chkIsActive.TabIndex = 2;
      this.chkIsActive.Text = "Is Active";
      this.chkIsActive.UseVisualStyleBackColor = true;
      this.chkIsActive.CheckedChanged += new System.EventHandler(this.DirtyCheck);
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
      // btnSave
      //
      this.btnSave.Enabled = false;
      this.btnSave.Location = new System.Drawing.Point(40, 130);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(100, 35);
      this.btnSave.TabIndex = 11;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Save);
      //
      // NotifyEventPanel
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.lblTreeNodePath);
      this.Controls.Add(this.chkIsActive);
      this.Controls.Add(this.lblDefaultSubject);
      this.Controls.Add(this.txtDefaultSubject);
      this.Controls.Add(this.lblName);
      this.Controls.Add(this.txtName);
      this.Name = "NotifyEventPanel";
      this.Size = new System.Drawing.Size(1267, 758);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.TextBox txtDefaultSubject;
    private System.Windows.Forms.Label lblDefaultSubject;
    private System.Windows.Forms.CheckBox chkIsActive;
    private System.Windows.Forms.Label lblTreeNodePath;
    private System.Windows.Forms.Button btnSave;
  }
}

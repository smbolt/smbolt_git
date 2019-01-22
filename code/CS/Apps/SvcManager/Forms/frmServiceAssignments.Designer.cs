namespace Org.SvcManager
{
  partial class frmServiceAssignments
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServiceAssignments));
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.lblEnvironmentValue = new System.Windows.Forms.Label();
      this.lblHost = new System.Windows.Forms.Label();
      this.lblHostValue = new System.Windows.Forms.Label();
      this.lblServiceName = new System.Windows.Forms.Label();
      this.lblServiceNameValue = new System.Windows.Forms.Label();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.cboTaskGroup = new System.Windows.Forms.ComboBox();
      this.btnClose = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.lblTaskGroupFilter = new System.Windows.Forms.Label();
      this.gvTasks = new System.Windows.Forms.DataGridView();
      this.lblAssignmentSummary = new System.Windows.Forms.Label();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvTasks)).BeginInit();
      this.SuspendLayout();
      //
      // lblEnvironment
      //
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(9, 10);
      this.lblEnvironment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(69, 13);
      this.lblEnvironment.TabIndex = 0;
      this.lblEnvironment.Text = "Environment:";
      //
      // lblEnvironmentValue
      //
      this.lblEnvironmentValue.AutoSize = true;
      this.lblEnvironmentValue.Location = new System.Drawing.Point(91, 10);
      this.lblEnvironmentValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblEnvironmentValue.Name = "lblEnvironmentValue";
      this.lblEnvironmentValue.Size = new System.Drawing.Size(28, 13);
      this.lblEnvironmentValue.TabIndex = 0;
      this.lblEnvironmentValue.Text = "Test";
      //
      // lblHost
      //
      this.lblHost.AutoSize = true;
      this.lblHost.Location = new System.Drawing.Point(9, 27);
      this.lblHost.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblHost.Name = "lblHost";
      this.lblHost.Size = new System.Drawing.Size(32, 13);
      this.lblHost.TabIndex = 0;
      this.lblHost.Text = "Host:";
      //
      // lblHostValue
      //
      this.lblHostValue.AutoSize = true;
      this.lblHostValue.Location = new System.Drawing.Point(91, 27);
      this.lblHostValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblHostValue.Name = "lblHostValue";
      this.lblHostValue.Size = new System.Drawing.Size(85, 13);
      this.lblHostValue.TabIndex = 0;
      this.lblHostValue.Text = "OKC1EDW1001";
      //
      // lblServiceName
      //
      this.lblServiceName.AutoSize = true;
      this.lblServiceName.Location = new System.Drawing.Point(9, 44);
      this.lblServiceName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblServiceName.Name = "lblServiceName";
      this.lblServiceName.Size = new System.Drawing.Size(77, 13);
      this.lblServiceName.TabIndex = 0;
      this.lblServiceName.Text = "Service Name:";
      //
      // lblServiceNameValue
      //
      this.lblServiceNameValue.AutoSize = true;
      this.lblServiceNameValue.Location = new System.Drawing.Point(91, 44);
      this.lblServiceNameValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblServiceNameValue.Name = "lblServiceNameValue";
      this.lblServiceNameValue.Size = new System.Drawing.Size(94, 13);
      this.lblServiceNameValue.TabIndex = 0;
      this.lblServiceNameValue.Text = "GPTaskService01";
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.lblAssignmentSummary);
      this.pnlTop.Controls.Add(this.cboTaskGroup);
      this.pnlTop.Controls.Add(this.btnClose);
      this.pnlTop.Controls.Add(this.btnSave);
      this.pnlTop.Controls.Add(this.lblTaskGroupFilter);
      this.pnlTop.Controls.Add(this.lblEnvironment);
      this.pnlTop.Controls.Add(this.lblServiceNameValue);
      this.pnlTop.Controls.Add(this.lblHost);
      this.pnlTop.Controls.Add(this.lblHostValue);
      this.pnlTop.Controls.Add(this.lblServiceName);
      this.pnlTop.Controls.Add(this.lblEnvironmentValue);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(2);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(775, 94);
      this.pnlTop.TabIndex = 1;
      //
      // cboTaskGroup
      //
      this.cboTaskGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTaskGroup.FormattingEnabled = true;
      this.cboTaskGroup.Location = new System.Drawing.Point(217, 38);
      this.cboTaskGroup.Margin = new System.Windows.Forms.Padding(2);
      this.cboTaskGroup.Name = "cboTaskGroup";
      this.cboTaskGroup.Size = new System.Drawing.Size(256, 21);
      this.cboTaskGroup.TabIndex = 3;
      this.cboTaskGroup.SelectedIndexChanged += new System.EventHandler(this.cboTaskGroup_SelectedIndexChanged);
      //
      // btnClose
      //
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(641, 36);
      this.btnClose.Margin = new System.Windows.Forms.Padding(2);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(127, 27);
      this.btnClose.TabIndex = 1;
      this.btnClose.Tag = "Close";
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.Action);
      //
      // btnSave
      //
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(641, 6);
      this.btnSave.Margin = new System.Windows.Forms.Padding(2);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(127, 27);
      this.btnSave.TabIndex = 2;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Action);
      //
      // lblTaskGroupFilter
      //
      this.lblTaskGroupFilter.AutoSize = true;
      this.lblTaskGroupFilter.Location = new System.Drawing.Point(214, 23);
      this.lblTaskGroupFilter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblTaskGroupFilter.Name = "lblTaskGroupFilter";
      this.lblTaskGroupFilter.Size = new System.Drawing.Size(88, 13);
      this.lblTaskGroupFilter.TabIndex = 0;
      this.lblTaskGroupFilter.Text = "Task Group Filter";
      //
      // gvTasks
      //
      this.gvTasks.AllowUserToAddRows = false;
      this.gvTasks.AllowUserToDeleteRows = false;
      this.gvTasks.AllowUserToResizeRows = false;
      this.gvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvTasks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvTasks.Location = new System.Drawing.Point(0, 94);
      this.gvTasks.MultiSelect = false;
      this.gvTasks.Name = "gvTasks";
      this.gvTasks.RowHeadersVisible = false;
      this.gvTasks.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvTasks.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvTasks.RowTemplate.Height = 19;
      this.gvTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvTasks.Size = new System.Drawing.Size(775, 389);
      this.gvTasks.TabIndex = 6;
      this.gvTasks.Tag = "EditScheduledTask";
      this.gvTasks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTasks_CellContentClick);
      //
      // lblAssignmentSummary
      //
      this.lblAssignmentSummary.AutoSize = true;
      this.lblAssignmentSummary.Location = new System.Drawing.Point(13, 75);
      this.lblAssignmentSummary.Name = "lblAssignmentSummary";
      this.lblAssignmentSummary.Size = new System.Drawing.Size(169, 13);
      this.lblAssignmentSummary.TabIndex = 4;
      this.lblAssignmentSummary.Text = "0 tasks of 0 assigned (no updates)";
      //
      // frmServiceAssignments
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(775, 483);
      this.ControlBox = false;
      this.Controls.Add(this.gvTasks);
      this.Controls.Add(this.pnlTop);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(2);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmServiceAssignments";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Task Service Assignments";
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvTasks)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.Label lblEnvironmentValue;
    private System.Windows.Forms.Label lblHost;
    private System.Windows.Forms.Label lblHostValue;
    private System.Windows.Forms.Label lblServiceName;
    private System.Windows.Forms.Label lblServiceNameValue;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.DataGridView gvTasks;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.ComboBox cboTaskGroup;
    private System.Windows.Forms.Label lblTaskGroupFilter;
    private System.Windows.Forms.Label lblAssignmentSummary;
  }
}
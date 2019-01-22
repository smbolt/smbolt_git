namespace Org.OpsManager
{
  partial class frmTaskSchedule
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTaskSchedule));
      this.txtScheduleName = new System.Windows.Forms.TextBox();
      this.cbIsActive = new System.Windows.Forms.CheckBox();
      this.lblScheduleName = new System.Windows.Forms.Label();
      this.lblStatus = new System.Windows.Forms.Label();
      this.gvScheduleElements = new System.Windows.Forms.DataGridView();
      this.ctxMenuScheduleElements = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuScheduleElementsDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.btnNewScheduleElement = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.pnlBottom = new System.Windows.Forms.Panel();
      this.pnlTop = new System.Windows.Forms.Panel();
      ((System.ComponentModel.ISupportInitialize)(this.gvScheduleElements)).BeginInit();
      this.ctxMenuScheduleElements.SuspendLayout();
      this.pnlBottom.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      //
      // txtScheduleName
      //
      this.txtScheduleName.Location = new System.Drawing.Point(11, 23);
      this.txtScheduleName.Name = "txtScheduleName";
      this.txtScheduleName.Size = new System.Drawing.Size(191, 20);
      this.txtScheduleName.TabIndex = 2;
      this.txtScheduleName.Tag = "PropertyChange";
      this.txtScheduleName.TextChanged += new System.EventHandler(this.Action);
      //
      // cbIsActive
      //
      this.cbIsActive.AutoSize = true;
      this.cbIsActive.Location = new System.Drawing.Point(692, 25);
      this.cbIsActive.Name = "cbIsActive";
      this.cbIsActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.cbIsActive.Size = new System.Drawing.Size(67, 17);
      this.cbIsActive.TabIndex = 3;
      this.cbIsActive.Tag = "PropertyChange";
      this.cbIsActive.Text = "Is Active";
      this.cbIsActive.UseVisualStyleBackColor = true;
      this.cbIsActive.CheckedChanged += new System.EventHandler(this.Action);
      //
      // lblScheduleName
      //
      this.lblScheduleName.AutoSize = true;
      this.lblScheduleName.Location = new System.Drawing.Point(11, 7);
      this.lblScheduleName.Name = "lblScheduleName";
      this.lblScheduleName.Size = new System.Drawing.Size(83, 13);
      this.lblScheduleName.TabIndex = 6;
      this.lblScheduleName.Text = "Schedule Name";
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 266);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(784, 20);
      this.lblStatus.TabIndex = 17;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // gvScheduleElements
      //
      this.gvScheduleElements.AllowUserToAddRows = false;
      this.gvScheduleElements.AllowUserToDeleteRows = false;
      this.gvScheduleElements.AllowUserToResizeRows = false;
      this.gvScheduleElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvScheduleElements.ContextMenuStrip = this.ctxMenuScheduleElements;
      this.gvScheduleElements.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvScheduleElements.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvScheduleElements.Location = new System.Drawing.Point(0, 58);
      this.gvScheduleElements.MultiSelect = false;
      this.gvScheduleElements.Name = "gvScheduleElements";
      this.gvScheduleElements.RowHeadersVisible = false;
      this.gvScheduleElements.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvScheduleElements.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvScheduleElements.RowTemplate.Height = 19;
      this.gvScheduleElements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvScheduleElements.Size = new System.Drawing.Size(784, 168);
      this.gvScheduleElements.TabIndex = 18;
      this.gvScheduleElements.Tag = "EditScheduleElement";
      this.gvScheduleElements.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvScheduleElements_CellMouseUp);
      this.gvScheduleElements.DoubleClick += new System.EventHandler(this.Action);
      //
      // ctxMenuScheduleElements
      //
      this.ctxMenuScheduleElements.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMenuScheduleElementsDelete
      });
      this.ctxMenuScheduleElements.Name = "ctxMenuScheduleElements";
      this.ctxMenuScheduleElements.Size = new System.Drawing.Size(108, 26);
      this.ctxMenuScheduleElements.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuScheduleElements_Opening);
      //
      // ctxMenuScheduleElementsDelete
      //
      this.ctxMenuScheduleElementsDelete.Name = "ctxMenuScheduleElementsDelete";
      this.ctxMenuScheduleElementsDelete.Size = new System.Drawing.Size(107, 22);
      this.ctxMenuScheduleElementsDelete.Tag = "DeleteScheduleElement";
      this.ctxMenuScheduleElementsDelete.Text = "Delete";
      this.ctxMenuScheduleElementsDelete.Click += new System.EventHandler(this.Action);
      //
      // btnNewScheduleElement
      //
      this.btnNewScheduleElement.Location = new System.Drawing.Point(632, 6);
      this.btnNewScheduleElement.Name = "btnNewScheduleElement";
      this.btnNewScheduleElement.Size = new System.Drawing.Size(127, 25);
      this.btnNewScheduleElement.TabIndex = 6;
      this.btnNewScheduleElement.Tag = "NewScheduleElement";
      this.btnNewScheduleElement.Text = "New Schedule Element";
      this.btnNewScheduleElement.UseVisualStyleBackColor = true;
      this.btnNewScheduleElement.Click += new System.EventHandler(this.Action);
      //
      // btnSave
      //
      this.btnSave.Enabled = false;
      this.btnSave.Location = new System.Drawing.Point(3, 8);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(110, 25);
      this.btnSave.TabIndex = 4;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Action);
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(116, 8);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(110, 25);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      //
      // pnlBottom
      //
      this.pnlBottom.Controls.Add(this.btnSave);
      this.pnlBottom.Controls.Add(this.btnCancel);
      this.pnlBottom.Controls.Add(this.btnNewScheduleElement);
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 226);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(784, 40);
      this.pnlBottom.TabIndex = 26;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.lblScheduleName);
      this.pnlTop.Controls.Add(this.txtScheduleName);
      this.pnlTop.Controls.Add(this.cbIsActive);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(784, 58);
      this.pnlTop.TabIndex = 27;
      //
      // frmTaskSchedule
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 286);
      this.ControlBox = false;
      this.Controls.Add(this.gvScheduleElements);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.pnlBottom);
      this.Controls.Add(this.lblStatus);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmTaskSchedule";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Task Schedule";
      ((System.ComponentModel.ISupportInitialize)(this.gvScheduleElements)).EndInit();
      this.ctxMenuScheduleElements.ResumeLayout(false);
      this.pnlBottom.ResumeLayout(false);
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox txtScheduleName;
    private System.Windows.Forms.CheckBox cbIsActive;
    private System.Windows.Forms.Label lblScheduleName;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.DataGridView gvScheduleElements;
    private System.Windows.Forms.Button btnNewScheduleElement;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Panel pnlBottom;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.ContextMenuStrip ctxMenuScheduleElements;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuScheduleElementsDelete;
  }
}
namespace Org.OpsManager
{
  partial class frmParameterSet
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParameterSet));
      this.txtParameterSetName = new System.Windows.Forms.TextBox();
      this.lblParameterSetName = new System.Windows.Forms.Label();
      this.txtParameterValue = new System.Windows.Forms.TextBox();
      this.lblParameterName = new System.Windows.Forms.Label();
      this.lblParameterValue = new System.Windows.Forms.Label();
      this.lblDataType = new System.Windows.Forms.Label();
      this.txtDataType = new System.Windows.Forms.TextBox();
      this.txtParameterName = new System.Windows.Forms.TextBox();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.gvParametersInSet = new System.Windows.Forms.DataGridView();
      this.ctxMenuParametersInSet = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuParametersInSetEdit = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuParametersInSetRemove = new System.Windows.Forms.ToolStripMenuItem();
      this.btnAdd = new System.Windows.Forms.Button();
      this.pnlButtons = new System.Windows.Forms.Panel();
      this.btnClear = new System.Windows.Forms.Button();
      this.pnlParameterSet = new System.Windows.Forms.Panel();
      ((System.ComponentModel.ISupportInitialize)(this.gvParametersInSet)).BeginInit();
      this.ctxMenuParametersInSet.SuspendLayout();
      this.pnlButtons.SuspendLayout();
      this.pnlParameterSet.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtParameterSetName
      // 
      this.txtParameterSetName.Location = new System.Drawing.Point(165, 20);
      this.txtParameterSetName.Name = "txtParameterSetName";
      this.txtParameterSetName.Size = new System.Drawing.Size(234, 20);
      this.txtParameterSetName.TabIndex = 1;
      this.txtParameterSetName.Tag = "PropertyChange";
      this.txtParameterSetName.TextChanged += new System.EventHandler(this.Action);
      // 
      // lblParameterSetName
      // 
      this.lblParameterSetName.AutoSize = true;
      this.lblParameterSetName.Location = new System.Drawing.Point(51, 23);
      this.lblParameterSetName.Name = "lblParameterSetName";
      this.lblParameterSetName.Size = new System.Drawing.Size(108, 13);
      this.lblParameterSetName.TabIndex = 1;
      this.lblParameterSetName.Text = "Parameter Set Name:";
      // 
      // txtParameterValue
      // 
      this.txtParameterValue.Location = new System.Drawing.Point(165, 70);
      this.txtParameterValue.Name = "txtParameterValue";
      this.txtParameterValue.Size = new System.Drawing.Size(234, 20);
      this.txtParameterValue.TabIndex = 3;
      this.txtParameterValue.Tag = "PropertyChange";
      this.txtParameterValue.TextChanged += new System.EventHandler(this.Action);
      // 
      // lblParameterName
      // 
      this.lblParameterName.AutoSize = true;
      this.lblParameterName.Location = new System.Drawing.Point(70, 47);
      this.lblParameterName.Name = "lblParameterName";
      this.lblParameterName.Size = new System.Drawing.Size(89, 13);
      this.lblParameterName.TabIndex = 3;
      this.lblParameterName.Text = "Parameter Name:";
      this.lblParameterName.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // lblParameterValue
      // 
      this.lblParameterValue.AutoSize = true;
      this.lblParameterValue.Location = new System.Drawing.Point(71, 73);
      this.lblParameterValue.Name = "lblParameterValue";
      this.lblParameterValue.Size = new System.Drawing.Size(88, 13);
      this.lblParameterValue.TabIndex = 4;
      this.lblParameterValue.Text = "Parameter Value:";
      this.lblParameterValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // lblDataType
      // 
      this.lblDataType.AutoSize = true;
      this.lblDataType.Location = new System.Drawing.Point(99, 99);
      this.lblDataType.Name = "lblDataType";
      this.lblDataType.Size = new System.Drawing.Size(60, 13);
      this.lblDataType.TabIndex = 5;
      this.lblDataType.Text = "Data Type:";
      this.lblDataType.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // txtDataType
      // 
      this.txtDataType.Location = new System.Drawing.Point(165, 96);
      this.txtDataType.Name = "txtDataType";
      this.txtDataType.Size = new System.Drawing.Size(234, 20);
      this.txtDataType.TabIndex = 4;
      this.txtDataType.Tag = "PropertyChange";
      this.txtDataType.TextChanged += new System.EventHandler(this.Action);
      // 
      // txtParameterName
      // 
      this.txtParameterName.Location = new System.Drawing.Point(165, 44);
      this.txtParameterName.Name = "txtParameterName";
      this.txtParameterName.Size = new System.Drawing.Size(234, 20);
      this.txtParameterName.TabIndex = 2;
      this.txtParameterName.Tag = "PropertyChange";
      this.txtParameterName.TextChanged += new System.EventHandler(this.Action);
      // 
      // btnSave
      // 
      this.btnSave.Location = new System.Drawing.Point(3, 8);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(110, 25);
      this.btnSave.TabIndex = 7;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Action);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(119, 8);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(110, 25);
      this.btnCancel.TabIndex = 8;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 519);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(540, 20);
      this.lblStatus.TabIndex = 46;
      this.lblStatus.Text = "Status";
      // 
      // gvParametersInSet
      // 
      this.gvParametersInSet.AllowUserToAddRows = false;
      this.gvParametersInSet.AllowUserToDeleteRows = false;
      this.gvParametersInSet.AllowUserToResizeRows = false;
      this.gvParametersInSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvParametersInSet.ContextMenuStrip = this.ctxMenuParametersInSet;
      this.gvParametersInSet.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvParametersInSet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvParametersInSet.Location = new System.Drawing.Point(0, 166);
      this.gvParametersInSet.MultiSelect = false;
      this.gvParametersInSet.Name = "gvParametersInSet";
      this.gvParametersInSet.RowHeadersVisible = false;
      this.gvParametersInSet.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvParametersInSet.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvParametersInSet.RowTemplate.Height = 19;
      this.gvParametersInSet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvParametersInSet.Size = new System.Drawing.Size(540, 315);
      this.gvParametersInSet.TabIndex = 47;
      this.gvParametersInSet.Tag = "";
      // 
      // ctxMenuParametersInSet
      // 
      this.ctxMenuParametersInSet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuParametersInSetEdit,
            this.ctxMenuParametersInSetRemove});
      this.ctxMenuParametersInSet.Name = "ctxMenuParametersInSet";
      this.ctxMenuParametersInSet.Size = new System.Drawing.Size(118, 48);
      this.ctxMenuParametersInSet.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuParametersInSet_Opening);
      // 
      // ctxMenuParametersInSetEdit
      // 
      this.ctxMenuParametersInSetEdit.Name = "ctxMenuParametersInSetEdit";
      this.ctxMenuParametersInSetEdit.Size = new System.Drawing.Size(117, 22);
      this.ctxMenuParametersInSetEdit.Tag = "Edit";
      this.ctxMenuParametersInSetEdit.Text = "Edit";
      this.ctxMenuParametersInSetEdit.Click += new System.EventHandler(this.Action);
      // 
      // ctxMenuParametersInSetRemove
      // 
      this.ctxMenuParametersInSetRemove.Name = "ctxMenuParametersInSetRemove";
      this.ctxMenuParametersInSetRemove.Size = new System.Drawing.Size(117, 22);
      this.ctxMenuParametersInSetRemove.Tag = "Remove";
      this.ctxMenuParametersInSetRemove.Text = "Remove";
      this.ctxMenuParametersInSetRemove.Click += new System.EventHandler(this.Action);
      // 
      // btnAdd
      // 
      this.btnAdd.Enabled = false;
      this.btnAdd.Location = new System.Drawing.Point(203, 122);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(95, 23);
      this.btnAdd.TabIndex = 5;
      this.btnAdd.Tag = "Add";
      this.btnAdd.Text = "Add";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.Action);
      // 
      // pnlButtons
      // 
      this.pnlButtons.Controls.Add(this.btnSave);
      this.pnlButtons.Controls.Add(this.btnCancel);
      this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlButtons.Location = new System.Drawing.Point(0, 481);
      this.pnlButtons.Name = "pnlButtons";
      this.pnlButtons.Size = new System.Drawing.Size(540, 38);
      this.pnlButtons.TabIndex = 49;
      // 
      // btnClear
      // 
      this.btnClear.Location = new System.Drawing.Point(304, 122);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(95, 23);
      this.btnClear.TabIndex = 6;
      this.btnClear.Tag = "Clear";
      this.btnClear.Text = "Clear";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new System.EventHandler(this.Action);
      // 
      // pnlParameterSet
      // 
      this.pnlParameterSet.Controls.Add(this.txtParameterSetName);
      this.pnlParameterSet.Controls.Add(this.lblParameterSetName);
      this.pnlParameterSet.Controls.Add(this.txtParameterName);
      this.pnlParameterSet.Controls.Add(this.btnClear);
      this.pnlParameterSet.Controls.Add(this.txtParameterValue);
      this.pnlParameterSet.Controls.Add(this.txtDataType);
      this.pnlParameterSet.Controls.Add(this.lblParameterName);
      this.pnlParameterSet.Controls.Add(this.btnAdd);
      this.pnlParameterSet.Controls.Add(this.lblParameterValue);
      this.pnlParameterSet.Controls.Add(this.lblDataType);
      this.pnlParameterSet.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlParameterSet.Location = new System.Drawing.Point(0, 0);
      this.pnlParameterSet.Name = "pnlParameterSet";
      this.pnlParameterSet.Size = new System.Drawing.Size(540, 166);
      this.pnlParameterSet.TabIndex = 52;
      // 
      // frmParameterSet
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(540, 539);
      this.ControlBox = false;
      this.Controls.Add(this.gvParametersInSet);
      this.Controls.Add(this.pnlParameterSet);
      this.Controls.Add(this.pnlButtons);
      this.Controls.Add(this.lblStatus);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmParameterSet";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Parameter Set";
      ((System.ComponentModel.ISupportInitialize)(this.gvParametersInSet)).EndInit();
      this.ctxMenuParametersInSet.ResumeLayout(false);
      this.pnlButtons.ResumeLayout(false);
      this.pnlParameterSet.ResumeLayout(false);
      this.pnlParameterSet.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox txtParameterSetName;
    private System.Windows.Forms.Label lblParameterSetName;
    private System.Windows.Forms.TextBox txtParameterValue;
    private System.Windows.Forms.Label lblParameterName;
    private System.Windows.Forms.Label lblParameterValue;
    private System.Windows.Forms.Label lblDataType;
    private System.Windows.Forms.TextBox txtDataType;
    private System.Windows.Forms.TextBox txtParameterName;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.DataGridView gvParametersInSet;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Panel pnlButtons;
    private System.Windows.Forms.ContextMenuStrip ctxMenuParametersInSet;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuParametersInSetEdit;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuParametersInSetRemove;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Panel pnlParameterSet;
  }
}
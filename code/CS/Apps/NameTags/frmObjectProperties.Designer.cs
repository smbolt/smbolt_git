namespace NameTags
{
  partial class frmObjectProperties
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
      this.cboDrawingObjects = new System.Windows.Forms.ComboBox();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.lblPropertyName = new System.Windows.Forms.Label();
      this.lblPropertyDescription = new System.Windows.Forms.Label();
      this.dgvProperties = new System.Windows.Forms.DataGridView();
      this.PropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.PropertyValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).BeginInit();
      this.SuspendLayout();
      //
      // cboDrawingObjects
      //
      this.cboDrawingObjects.Dock = System.Windows.Forms.DockStyle.Top;
      this.cboDrawingObjects.FormattingEnabled = true;
      this.cboDrawingObjects.Location = new System.Drawing.Point(0, 0);
      this.cboDrawingObjects.Name = "cboDrawingObjects";
      this.cboDrawingObjects.Size = new System.Drawing.Size(399, 21);
      this.cboDrawingObjects.TabIndex = 0;
      this.cboDrawingObjects.SelectedIndexChanged += new System.EventHandler(this.cboDrawingObjects_SelectedIndexChanged);
      //
      // pnlTop
      //
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 21);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(399, 24);
      this.pnlTop.TabIndex = 1;
      //
      // splitterMain
      //
      this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.Location = new System.Drawing.Point(0, 45);
      this.splitterMain.Name = "splitterMain";
      this.splitterMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
      //
      // splitterMain.Panel1
      //
      this.splitterMain.Panel1.Controls.Add(this.dgvProperties);
      //
      // splitterMain.Panel2
      //
      this.splitterMain.Panel2.Controls.Add(this.lblPropertyDescription);
      this.splitterMain.Panel2.Controls.Add(this.lblPropertyName);
      this.splitterMain.Size = new System.Drawing.Size(399, 534);
      this.splitterMain.SplitterDistance = 471;
      this.splitterMain.TabIndex = 2;
      //
      // lblPropertyName
      //
      this.lblPropertyName.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblPropertyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPropertyName.Location = new System.Drawing.Point(0, 0);
      this.lblPropertyName.Name = "lblPropertyName";
      this.lblPropertyName.Size = new System.Drawing.Size(397, 19);
      this.lblPropertyName.TabIndex = 0;
      this.lblPropertyName.Text = "<property name>";
      //
      // lblPropertyDescription
      //
      this.lblPropertyDescription.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblPropertyDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPropertyDescription.Location = new System.Drawing.Point(0, 19);
      this.lblPropertyDescription.Name = "lblPropertyDescription";
      this.lblPropertyDescription.Size = new System.Drawing.Size(397, 38);
      this.lblPropertyDescription.TabIndex = 1;
      this.lblPropertyDescription.Text = "<property description>";
      //
      // dgvProperties
      //
      this.dgvProperties.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dgvProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
        this.PropertyName,
        this.PropertyValue
      });
      this.dgvProperties.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvProperties.Location = new System.Drawing.Point(0, 0);
      this.dgvProperties.Name = "dgvProperties";
      this.dgvProperties.RowHeadersVisible = false;
      this.dgvProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
      this.dgvProperties.Size = new System.Drawing.Size(397, 469);
      this.dgvProperties.TabIndex = 0;
      this.dgvProperties.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvProperties_CellBeginEdit);
      this.dgvProperties.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProperties_CellEndEdit);
      //
      // PropertyName
      //
      this.PropertyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.PropertyName.FillWeight = 120F;
      this.PropertyName.HeaderText = "Property";
      this.PropertyName.Name = "PropertyName";
      this.PropertyName.ReadOnly = true;
      this.PropertyName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      //
      // PropertyValue
      //
      this.PropertyValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.PropertyValue.FillWeight = 150F;
      this.PropertyValue.HeaderText = "Property Value";
      this.PropertyValue.Name = "PropertyValue";
      this.PropertyValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      //
      // frmObjectProperties
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(399, 579);
      this.Controls.Add(this.splitterMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.cboDrawingObjects);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "frmObjectProperties";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Drawing Object Properties";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmObjectProperties_FormClosing);
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      this.splitterMain.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cboDrawingObjects;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.Label lblPropertyDescription;
    private System.Windows.Forms.Label lblPropertyName;
    private System.Windows.Forms.DataGridView dgvProperties;
    private System.Windows.Forms.DataGridViewTextBoxColumn PropertyName;
    private System.Windows.Forms.DataGridViewTextBoxColumn PropertyValue;
  }
}
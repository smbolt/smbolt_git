namespace Org.TW.ToolPanels
{
  partial class PropertiesPanel
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
      this.pnlGrid = new System.Windows.Forms.Panel();
      this.gvProps = new System.Windows.Forms.DataGridView();
      this.pnlTopControl = new System.Windows.Forms.Panel();
      this.cboPropertyName = new System.Windows.Forms.ComboBox();
      this.pnlBottomControl = new System.Windows.Forms.Panel();
      this.pnlGrid.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvProps)).BeginInit();
      this.pnlTopControl.SuspendLayout();
      this.SuspendLayout();
      //
      // pnlGrid
      //
      this.pnlGrid.Controls.Add(this.gvProps);
      this.pnlGrid.Controls.Add(this.pnlTopControl);
      this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlGrid.Location = new System.Drawing.Point(0, 0);
      this.pnlGrid.Name = "pnlGrid";
      this.pnlGrid.Size = new System.Drawing.Size(345, 458);
      this.pnlGrid.TabIndex = 1;
      //
      // gvProps
      //
      this.gvProps.AllowUserToAddRows = false;
      this.gvProps.AllowUserToDeleteRows = false;
      this.gvProps.AllowUserToResizeRows = false;
      this.gvProps.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.gvProps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvProps.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvProps.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvProps.Location = new System.Drawing.Point(0, 23);
      this.gvProps.Name = "gvProps";
      this.gvProps.RowHeadersVisible = false;
      this.gvProps.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvProps.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DodgerBlue;
      this.gvProps.RowTemplate.Height = 19;
      this.gvProps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvProps.Size = new System.Drawing.Size(345, 435);
      this.gvProps.TabIndex = 4;
      //
      // pnlTopControl
      //
      this.pnlTopControl.Controls.Add(this.cboPropertyName);
      this.pnlTopControl.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTopControl.Location = new System.Drawing.Point(0, 0);
      this.pnlTopControl.Name = "pnlTopControl";
      this.pnlTopControl.Size = new System.Drawing.Size(345, 23);
      this.pnlTopControl.TabIndex = 5;
      //
      // cboPropertyName
      //
      this.cboPropertyName.Dock = System.Windows.Forms.DockStyle.Top;
      this.cboPropertyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboPropertyName.FormattingEnabled = true;
      this.cboPropertyName.Location = new System.Drawing.Point(0, 0);
      this.cboPropertyName.Name = "cboPropertyName";
      this.cboPropertyName.Size = new System.Drawing.Size(345, 21);
      this.cboPropertyName.TabIndex = 0;
      //
      // pnlBottomControl
      //
      this.pnlBottomControl.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottomControl.Location = new System.Drawing.Point(0, 458);
      this.pnlBottomControl.Name = "pnlBottomControl";
      this.pnlBottomControl.Size = new System.Drawing.Size(345, 52);
      this.pnlBottomControl.TabIndex = 2;
      //
      // PropertiesPanel
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pnlGrid);
      this.Controls.Add(this.pnlBottomControl);
      this.Name = "PropertiesPanel";
      this.Size = new System.Drawing.Size(345, 510);
      this.Tag = "ToolPanel_Properties";
      this.pnlGrid.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvProps)).EndInit();
      this.pnlTopControl.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlGrid;
    private System.Windows.Forms.Panel pnlBottomControl;
    private System.Windows.Forms.DataGridView gvProps;
    private System.Windows.Forms.Panel pnlTopControl;
    private System.Windows.Forms.ComboBox cboPropertyName;

  }
}

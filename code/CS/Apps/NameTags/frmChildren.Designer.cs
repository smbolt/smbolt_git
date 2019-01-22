namespace NameTags
{
  partial class frmChildren
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
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblStatus = new System.Windows.Forms.Label();
      this.gvChildren = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.gvChildren)).BeginInit();
      this.SuspendLayout();
      //
      // pnlTop
      //
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1219, 73);
      this.pnlTop.TabIndex = 0;
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 619);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1219, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // gvChildren
      //
      this.gvChildren.AllowUserToAddRows = false;
      this.gvChildren.AllowUserToDeleteRows = false;
      this.gvChildren.AllowUserToResizeRows = false;
      this.gvChildren.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.gvChildren.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvChildren.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvChildren.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvChildren.Location = new System.Drawing.Point(0, 73);
      this.gvChildren.MultiSelect = false;
      this.gvChildren.Name = "gvChildren";
      this.gvChildren.RowHeadersVisible = false;
      this.gvChildren.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvChildren.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvChildren.RowTemplate.Height = 19;
      this.gvChildren.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvChildren.Size = new System.Drawing.Size(1219, 546);
      this.gvChildren.TabIndex = 6;
      this.gvChildren.Tag = "EditScheduledTask";
      //
      // frmChildren
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1219, 642);
      this.ControlBox = false;
      this.Controls.Add(this.gvChildren);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Name = "frmChildren";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Children List";
      ((System.ComponentModel.ISupportInitialize)(this.gvChildren)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.DataGridView gvChildren;
  }
}
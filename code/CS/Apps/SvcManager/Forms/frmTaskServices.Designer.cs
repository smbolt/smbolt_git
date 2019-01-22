namespace Org.SvcManager
{
  partial class frmTaskServices
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTaskServices));
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnClose = new System.Windows.Forms.Button();
      this.cboEnvironment = new System.Windows.Forms.ComboBox();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.gvTaskServices = new System.Windows.Forms.DataGridView();
      this.btnAddNew = new System.Windows.Forms.Button();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvTaskServices)).BeginInit();
      this.SuspendLayout();
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.btnAddNew);
      this.pnlTop.Controls.Add(this.btnClose);
      this.pnlTop.Controls.Add(this.cboEnvironment);
      this.pnlTop.Controls.Add(this.lblEnvironment);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(879, 88);
      this.pnlTop.TabIndex = 0;
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(670, 24);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(190, 41);
      this.btnClose.TabIndex = 4;
      this.btnClose.Tag = "Close";
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.Action);
      // 
      // cboEnvironment
      // 
      this.cboEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboEnvironment.FormattingEnabled = true;
      this.cboEnvironment.Items.AddRange(new object[] {
            "Test",
            "Prod"});
      this.cboEnvironment.Location = new System.Drawing.Point(13, 37);
      this.cboEnvironment.Name = "cboEnvironment";
      this.cboEnvironment.Size = new System.Drawing.Size(262, 28);
      this.cboEnvironment.TabIndex = 1;
      this.cboEnvironment.SelectedIndexChanged += new System.EventHandler(this.cboEnvironment_SelectedIndexChanged);
      // 
      // lblEnvironment
      // 
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(13, 13);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(98, 20);
      this.lblEnvironment.TabIndex = 0;
      this.lblEnvironment.Text = "Environment";
      // 
      // gvTaskServices
      // 
      this.gvTaskServices.AllowUserToAddRows = false;
      this.gvTaskServices.AllowUserToDeleteRows = false;
      this.gvTaskServices.AllowUserToResizeRows = false;
      this.gvTaskServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvTaskServices.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvTaskServices.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvTaskServices.Location = new System.Drawing.Point(0, 88);
      this.gvTaskServices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.gvTaskServices.MultiSelect = false;
      this.gvTaskServices.Name = "gvTaskServices";
      this.gvTaskServices.RowHeadersVisible = false;
      this.gvTaskServices.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvTaskServices.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvTaskServices.RowTemplate.Height = 19;
      this.gvTaskServices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvTaskServices.Size = new System.Drawing.Size(879, 528);
      this.gvTaskServices.TabIndex = 7;
      this.gvTaskServices.Tag = "EditScheduledTask";
      // 
      // btnAddNew
      // 
      this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddNew.Location = new System.Drawing.Point(474, 24);
      this.btnAddNew.Name = "btnAddNew";
      this.btnAddNew.Size = new System.Drawing.Size(190, 41);
      this.btnAddNew.TabIndex = 4;
      this.btnAddNew.Tag = "AddNew";
      this.btnAddNew.Text = "Add New";
      this.btnAddNew.UseVisualStyleBackColor = true;
      this.btnAddNew.Click += new System.EventHandler(this.Action);
      // 
      // frmTaskServices
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(879, 616);
      this.ControlBox = false;
      this.Controls.Add(this.gvTaskServices);
      this.Controls.Add(this.pnlTop);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmTaskServices";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Manage Task Services";
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvTaskServices)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.ComboBox cboEnvironment;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.DataGridView gvTaskServices;
    private System.Windows.Forms.Button btnAddNew;
  }
}
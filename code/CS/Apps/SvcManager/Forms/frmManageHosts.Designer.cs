namespace Org.SvcManager
{
  partial class frmManageHosts
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageHosts));
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.lblEnvironmentValue = new System.Windows.Forms.Label();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnAddNewHost = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.gvHosts = new System.Windows.Forms.DataGridView();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvHosts)).BeginInit();
      this.SuspendLayout();
      //
      // lblEnvironment
      //
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(12, 16);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(102, 20);
      this.lblEnvironment.TabIndex = 0;
      this.lblEnvironment.Text = "Environment:";
      //
      // lblEnvironmentValue
      //
      this.lblEnvironmentValue.AutoSize = true;
      this.lblEnvironmentValue.Location = new System.Drawing.Point(133, 16);
      this.lblEnvironmentValue.Name = "lblEnvironmentValue";
      this.lblEnvironmentValue.Size = new System.Drawing.Size(42, 20);
      this.lblEnvironmentValue.TabIndex = 0;
      this.lblEnvironmentValue.Text = "Prod";
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.btnAddNewHost);
      this.pnlTop.Controls.Add(this.btnClose);
      this.pnlTop.Controls.Add(this.lblEnvironment);
      this.pnlTop.Controls.Add(this.lblEnvironmentValue);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(6, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(642, 127);
      this.pnlTop.TabIndex = 1;
      //
      // btnAddNewHost
      //
      this.btnAddNewHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddNewHost.Location = new System.Drawing.Point(440, 16);
      this.btnAddNewHost.Name = "btnAddNewHost";
      this.btnAddNewHost.Size = new System.Drawing.Size(190, 41);
      this.btnAddNewHost.TabIndex = 5;
      this.btnAddNewHost.Tag = "AddNewHost";
      this.btnAddNewHost.Text = "Add New Host";
      this.btnAddNewHost.UseVisualStyleBackColor = true;
      this.btnAddNewHost.Click += new System.EventHandler(this.Action);
      //
      // btnClose
      //
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(440, 68);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(190, 41);
      this.btnClose.TabIndex = 5;
      this.btnClose.Tag = "Close";
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.Action);
      //
      // gvHosts
      //
      this.gvHosts.AllowUserToAddRows = false;
      this.gvHosts.AllowUserToDeleteRows = false;
      this.gvHosts.AllowUserToResizeRows = false;
      this.gvHosts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvHosts.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvHosts.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.gvHosts.Location = new System.Drawing.Point(6, 127);
      this.gvHosts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.gvHosts.MultiSelect = false;
      this.gvHosts.Name = "gvHosts";
      this.gvHosts.RowHeadersVisible = false;
      this.gvHosts.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.gvHosts.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
      this.gvHosts.RowTemplate.Height = 19;
      this.gvHosts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvHosts.Size = new System.Drawing.Size(642, 283);
      this.gvHosts.TabIndex = 8;
      this.gvHosts.Tag = "EditScheduledTask";
      //
      // frmManageHosts
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(654, 416);
      this.ControlBox = false;
      this.Controls.Add(this.gvHosts);
      this.Controls.Add(this.pnlTop);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmManageHosts";
      this.Padding = new System.Windows.Forms.Padding(6, 0, 6, 6);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Manage Hosts";
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvHosts)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.Label lblEnvironmentValue;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.DataGridView gvHosts;
    private System.Windows.Forms.Button btnAddNewHost;
    private System.Windows.Forms.Button btnClose;
  }
}
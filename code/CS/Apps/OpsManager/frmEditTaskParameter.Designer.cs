namespace Org.OpsManager
{
  partial class frmEditTaskParameter
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditTaskParameter));
      this.txtParameterName = new System.Windows.Forms.TextBox();
      this.lblParameterName = new System.Windows.Forms.Label();
      this.lblParameterValue = new System.Windows.Forms.Label();
      this.lblDataType = new System.Windows.Forms.Label();
      this.txtParameterValue = new System.Windows.Forms.TextBox();
      this.txtDataType = new System.Windows.Forms.TextBox();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.pnlMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtParameterName
      // 
      this.txtParameterName.Location = new System.Drawing.Point(40, 28);
      this.txtParameterName.Name = "txtParameterName";
      this.txtParameterName.Size = new System.Drawing.Size(190, 20);
      this.txtParameterName.TabIndex = 1;
      this.txtParameterName.Tag = "PropertyChange";
      this.txtParameterName.TextChanged += new System.EventHandler(this.Action);
      // 
      // lblParameterName
      // 
      this.lblParameterName.AutoSize = true;
      this.lblParameterName.Location = new System.Drawing.Point(38, 12);
      this.lblParameterName.Name = "lblParameterName";
      this.lblParameterName.Size = new System.Drawing.Size(86, 13);
      this.lblParameterName.TabIndex = 1;
      this.lblParameterName.Text = "Parameter Name";
      // 
      // lblParameterValue
      // 
      this.lblParameterValue.AutoSize = true;
      this.lblParameterValue.Location = new System.Drawing.Point(38, 56);
      this.lblParameterValue.Name = "lblParameterValue";
      this.lblParameterValue.Size = new System.Drawing.Size(85, 13);
      this.lblParameterValue.TabIndex = 2;
      this.lblParameterValue.Text = "Parameter Value";
      // 
      // lblDataType
      // 
      this.lblDataType.AutoSize = true;
      this.lblDataType.Location = new System.Drawing.Point(38, 100);
      this.lblDataType.Name = "lblDataType";
      this.lblDataType.Size = new System.Drawing.Size(57, 13);
      this.lblDataType.TabIndex = 3;
      this.lblDataType.Text = "Data Type";
      // 
      // txtParameterValue
      // 
      this.txtParameterValue.Location = new System.Drawing.Point(40, 72);
      this.txtParameterValue.Name = "txtParameterValue";
      this.txtParameterValue.Size = new System.Drawing.Size(190, 20);
      this.txtParameterValue.TabIndex = 2;
      this.txtParameterValue.Tag = "PropertyChange";
      this.txtParameterValue.TextChanged += new System.EventHandler(this.Action);
      // 
      // txtDataType
      // 
      this.txtDataType.Location = new System.Drawing.Point(40, 116);
      this.txtDataType.Name = "txtDataType";
      this.txtDataType.Size = new System.Drawing.Size(190, 20);
      this.txtDataType.TabIndex = 3;
      this.txtDataType.Tag = "PropertyChange";
      this.txtDataType.TextChanged += new System.EventHandler(this.Action);
      // 
      // btnSave
      // 
      this.btnSave.Location = new System.Drawing.Point(40, 160);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(92, 23);
      this.btnSave.TabIndex = 4;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Action);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(138, 160);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(92, 23);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.lblParameterName);
      this.pnlMain.Controls.Add(this.txtParameterName);
      this.pnlMain.Controls.Add(this.btnCancel);
      this.pnlMain.Controls.Add(this.lblParameterValue);
      this.pnlMain.Controls.Add(this.btnSave);
      this.pnlMain.Controls.Add(this.txtParameterValue);
      this.pnlMain.Controls.Add(this.txtDataType);
      this.pnlMain.Controls.Add(this.lblDataType);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 0);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(274, 204);
      this.pnlMain.TabIndex = 9;
      // 
      // frmEditTaskParameter
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(274, 204);
      this.ControlBox = false;
      this.Controls.Add(this.pnlMain);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmEditTaskParameter";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Task Parameter";
      this.pnlMain.ResumeLayout(false);
      this.pnlMain.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox txtParameterName;
    private System.Windows.Forms.Label lblParameterName;
    private System.Windows.Forms.Label lblParameterValue;
    private System.Windows.Forms.Label lblDataType;
    private System.Windows.Forms.TextBox txtParameterValue;
    private System.Windows.Forms.TextBox txtDataType;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Panel pnlMain;
  }
}
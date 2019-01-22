namespace Org.OpsManager
{
  partial class frmIdentifier
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIdentifier));
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.txtDescription = new System.Windows.Forms.TextBox();
      this.txtIdentifierId = new System.Windows.Forms.TextBox();
      this.lblDescription = new System.Windows.Forms.Label();
      this.lblIdentifierID = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnSave
      // 
      this.btnSave.Enabled = false;
      this.btnSave.Location = new System.Drawing.Point(15, 94);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(110, 25);
      this.btnSave.TabIndex = 2;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.Action);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(131, 94);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(110, 25);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      // 
      // txtDescription
      // 
      this.txtDescription.Location = new System.Drawing.Point(131, 31);
      this.txtDescription.Multiline = true;
      this.txtDescription.Name = "txtDescription";
      this.txtDescription.Size = new System.Drawing.Size(464, 51);
      this.txtDescription.TabIndex = 1;
      this.txtDescription.Tag = "DirtyCheck";
      this.txtDescription.TextChanged += new System.EventHandler(this.Action);
      // 
      // txtIdentifierId
      // 
      this.txtIdentifierId.Location = new System.Drawing.Point(15, 31);
      this.txtIdentifierId.Name = "txtIdentifierId";
      this.txtIdentifierId.Size = new System.Drawing.Size(110, 20);
      this.txtIdentifierId.TabIndex = 0;
      this.txtIdentifierId.Tag = "DirtyCheck";
      this.txtIdentifierId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.txtIdentifierId.TextChanged += new System.EventHandler(this.Action);
      this.txtIdentifierId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IntegerOnly_KeyPress);
      // 
      // lblDescription
      // 
      this.lblDescription.AutoSize = true;
      this.lblDescription.Location = new System.Drawing.Point(128, 15);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new System.Drawing.Size(63, 13);
      this.lblDescription.TabIndex = 4;
      this.lblDescription.Text = "Description:";
      // 
      // lblIdentifierID
      // 
      this.lblIdentifierID.AutoSize = true;
      this.lblIdentifierID.Location = new System.Drawing.Point(15, 15);
      this.lblIdentifierID.Name = "lblIdentifierID";
      this.lblIdentifierID.Size = new System.Drawing.Size(64, 13);
      this.lblIdentifierID.TabIndex = 5;
      this.lblIdentifierID.Text = "Identifier ID:";
      // 
      // frmIdentifier
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(607, 129);
      this.ControlBox = false;
      this.Controls.Add(this.lblIdentifierID);
      this.Controls.Add(this.lblDescription);
      this.Controls.Add(this.txtIdentifierId);
      this.Controls.Add(this.txtDescription);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnSave);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmIdentifier";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Identifier";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.TextBox txtIdentifierId;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.Label lblIdentifierID;
  }
}
namespace Org.WinServiceHost
{
  partial class frmEnvironment
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnvironment));
      this.cboEnvironment = new System.Windows.Forms.ComboBox();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.lblPrompt = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // cboEnvironment
      // 
      this.cboEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboEnvironment.FormattingEnabled = true;
      this.cboEnvironment.Items.AddRange(new object[] {
            "PROD",
            "TEST"});
      this.cboEnvironment.Location = new System.Drawing.Point(52, 65);
      this.cboEnvironment.Name = "cboEnvironment";
      this.cboEnvironment.Size = new System.Drawing.Size(230, 21);
      this.cboEnvironment.TabIndex = 0;
      this.cboEnvironment.SelectedIndexChanged += new System.EventHandler(this.cboEnvironment_SelectedIndexChanged);
      // 
      // lblEnvironment
      // 
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(49, 46);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(66, 13);
      this.lblEnvironment.TabIndex = 1;
      this.lblEnvironment.Text = "Environment";
      // 
      // lblPrompt
      // 
      this.lblPrompt.AutoSize = true;
      this.lblPrompt.Location = new System.Drawing.Point(49, 17);
      this.lblPrompt.Name = "lblPrompt";
      this.lblPrompt.Size = new System.Drawing.Size(203, 13);
      this.lblPrompt.TabIndex = 1;
      this.lblPrompt.Text = "Select the environment you want to run in";
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(116, 104);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(104, 23);
      this.btnOK.TabIndex = 2;
      this.btnOK.Tag = "OK";
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.Action);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(116, 133);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(104, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      // 
      // frmEnvironment
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(347, 178);
      this.ControlBox = false;
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.lblPrompt);
      this.Controls.Add(this.lblEnvironment);
      this.Controls.Add(this.cboEnvironment);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmEnvironment";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "WinServiceHost - Environment Selection";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cboEnvironment;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.Label lblPrompt;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
  }
}
namespace Org.SF {
  partial class frmDbConfig {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.btnOK = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(314, 475);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(117, 32);
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // frmDbConfig
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(870, 564);
      this.Controls.Add(this.btnOK);
      this.Name = "frmDbConfig";
      this.Text = "frmDbConfig";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
  }
}
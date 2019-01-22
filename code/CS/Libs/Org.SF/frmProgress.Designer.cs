namespace Org.SF
{
  partial class frmProgress
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProgress));
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.lblProgress = new System.Windows.Forms.Label();
      this.lblDescription = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      //
      // progressBar
      //
      this.progressBar.Location = new System.Drawing.Point(15, 42);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(420, 23);
      this.progressBar.TabIndex = 0;
      //
      // lblProgress
      //
      this.lblProgress.Location = new System.Drawing.Point(15, 68);
      this.lblProgress.Name = "lblProgress";
      this.lblProgress.Size = new System.Drawing.Size(420, 19);
      this.lblProgress.TabIndex = 1;
      this.lblProgress.Text = "Message to indicate what specific action is running...";
      //
      // lblDescription
      //
      this.lblDescription.Location = new System.Drawing.Point(15, 21);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new System.Drawing.Size(417, 18);
      this.lblDescription.TabIndex = 1;
      this.lblDescription.Text = "General message to inform user concerning the process that is running...";
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(157, 96);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(135, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      //
      // frmProgress
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(453, 129);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.lblDescription);
      this.Controls.Add(this.lblProgress);
      this.Controls.Add(this.progressBar);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmProgress";
      this.ShowInTaskbar = false;
      this.Text = "Progress Form";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDataLoadProgress_FormClosing);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label lblProgress;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.Button btnCancel;
  }
}
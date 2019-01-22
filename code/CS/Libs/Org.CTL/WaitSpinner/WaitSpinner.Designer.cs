namespace Org.CTL
{
  partial class WaitSpinner
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
      this.components = new System.ComponentModel.Container();
      this.pnlImage = new System.Windows.Forms.Panel();
      this.pbMain = new System.Windows.Forms.PictureBox();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.pnlImage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
      this.SuspendLayout();
      //
      // pnlImage
      //
      this.pnlImage.Controls.Add(this.pbMain);
      this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlImage.Location = new System.Drawing.Point(0, 0);
      this.pnlImage.Name = "pnlImage";
      this.pnlImage.Size = new System.Drawing.Size(64, 64);
      this.pnlImage.TabIndex = 0;
      //
      // pbMain
      //
      this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pbMain.Location = new System.Drawing.Point(0, 0);
      this.pbMain.Name = "pbMain";
      this.pbMain.Size = new System.Drawing.Size(64, 64);
      this.pbMain.TabIndex = 0;
      this.pbMain.TabStop = false;
      //
      // timer1
      //
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      //
      // WaitSpinnerControl
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.pnlImage);
      this.Name = "WaitSpinnerControl";
      this.Size = new System.Drawing.Size(64, 64);
      this.Resize += new System.EventHandler(this.WaitSpinner_Resize);
      this.pnlImage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlImage;
    private System.Windows.Forms.PictureBox pbMain;
    private System.Windows.Forms.Timer timer1;
  }
}

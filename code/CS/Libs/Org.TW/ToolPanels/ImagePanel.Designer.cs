namespace Org.TW.ToolPanels
{
  partial class ImagePanel
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
      this.pnlImage = new System.Windows.Forms.Panel();
      this.pbMain = new System.Windows.Forms.PictureBox();
      this.pnlImage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
      this.SuspendLayout();
      // 
      // pnlImage
      // 
      this.pnlImage.AutoScroll = true;
      this.pnlImage.BackColor = System.Drawing.Color.Silver;
      this.pnlImage.Controls.Add(this.pbMain);
      this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlImage.Location = new System.Drawing.Point(0, 0);
      this.pnlImage.Name = "pnlImage";
      this.pnlImage.Size = new System.Drawing.Size(712, 596);
      this.pnlImage.TabIndex = 0;
      this.pnlImage.Tag = "";
      // 
      // pbMain
      // 
      this.pbMain.BackColor = System.Drawing.Color.White;
      this.pbMain.Location = new System.Drawing.Point(18, 16);
      this.pbMain.Name = "pbMain";
      this.pbMain.Size = new System.Drawing.Size(677, 561);
      this.pbMain.TabIndex = 0;
      this.pbMain.TabStop = false;
      // 
      // ImagePanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pnlImage);
      this.Name = "ImagePanel";
      this.Size = new System.Drawing.Size(712, 596);
      this.Tag = "ToolPanel_Image";
      this.pnlImage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlImage;
    private System.Windows.Forms.PictureBox pbMain;
  }
}

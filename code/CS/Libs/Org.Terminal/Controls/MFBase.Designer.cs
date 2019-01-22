namespace Org.Terminal.Controls
{
  partial class MFBase
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
      this.pbMain = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
      this.SuspendLayout();
      // 
      // pbMain
      // 
      this.pbMain.BackColor = System.Drawing.Color.Black;
      this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pbMain.Location = new System.Drawing.Point(0, 0);
      this.pbMain.Name = "pbMain";
      this.pbMain.Size = new System.Drawing.Size(367, 272);
      this.pbMain.TabIndex = 0;
      this.pbMain.TabStop = false;
      this.pbMain.SizeChanged += new System.EventHandler(this.pbMain_SizeChanged);
      this.pbMain.Click += new System.EventHandler(this.pbMain_Click);
      this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
      this.pbMain.MouseEnter += new System.EventHandler(this.pbMain_MouseEnter);
      this.pbMain.MouseLeave += new System.EventHandler(this.pbMain_MouseLeave);
      this.pbMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseMove);
      // 
      // MFBase
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.Controls.Add(this.pbMain);
      this.Name = "MFBase";
      this.Size = new System.Drawing.Size(367, 272);
      this.Enter += new System.EventHandler(this.MFBase_Enter);
      this.Leave += new System.EventHandler(this.MFBase_Leave);
      this.MouseEnter += new System.EventHandler(this.MFBase_MouseEnter);
      this.MouseLeave += new System.EventHandler(this.MFBase_MouseLeave);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MFBase_MouseMove);
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox pbMain;
  }
}

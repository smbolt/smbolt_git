namespace Org.TW.Forms
{
  partial class frmToolWindowBase
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmToolWindowBase));
      this.pnlMain = new System.Windows.Forms.Panel();
      this.SuspendLayout();
      //
      // pnlMain
      //
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 0);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(201, 338);
      this.pnlMain.TabIndex = 1;
      //
      // frmToolWindowBase
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(201, 338);
      this.Controls.Add(this.pnlMain);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(2);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmToolWindowBase";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Tools";
      this.Activated += new System.EventHandler(this.base_Activated);
      this.Deactivate += new System.EventHandler(this.base_Deactivate);
      this.ResizeEnd += new System.EventHandler(this.base_ResizeEnd);
      this.VisibleChanged += new System.EventHandler(this.base_VisibleChanged);
      this.Move += new System.EventHandler(this.base_LocationChanged);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlMain;
  }
}
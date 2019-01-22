namespace Org.TW.ToolPanels
{
  partial class ToolPanelBase
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
      this.pnlTopControl = new System.Windows.Forms.Panel();
      this.btnDockFloat = new System.Windows.Forms.Button();
      this.pnlTopControl.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlTopControl
      // 
      this.pnlTopControl.Controls.Add(this.btnDockFloat);
      this.pnlTopControl.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTopControl.Location = new System.Drawing.Point(0, 0);
      this.pnlTopControl.Name = "pnlTopControl";
      this.pnlTopControl.Padding = new System.Windows.Forms.Padding(3);
      this.pnlTopControl.Size = new System.Drawing.Size(241, 26);
      this.pnlTopControl.TabIndex = 0;
      // 
      // btnDockFloat
      // 
      this.btnDockFloat.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnDockFloat.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnDockFloat.Location = new System.Drawing.Point(196, 3);
      this.btnDockFloat.Name = "btnDockFloat";
      this.btnDockFloat.Size = new System.Drawing.Size(42, 20);
      this.btnDockFloat.TabIndex = 0;
      this.btnDockFloat.Tag = "TW_Dock";
      this.btnDockFloat.Text = "Dock";
      this.btnDockFloat.UseVisualStyleBackColor = true;
      // 
      // ToolPanelBase
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pnlTopControl);
      this.Name = "ToolPanelBase";
      this.Size = new System.Drawing.Size(241, 236);
      this.DockChanged += new System.EventHandler(this.ToolPanelBase_DockChanged);
      this.pnlTopControl.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlTopControl;
    private System.Windows.Forms.Button btnDockFloat;
  }
}

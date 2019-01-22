namespace Org.TW.ToolPanels
{
  partial class DebugPanel
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
      this.txtCode = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // txtCode
      // 
      this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCode.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtCode.Location = new System.Drawing.Point(0, 0);
      this.txtCode.Multiline = true;
      this.txtCode.Name = "txtCode";
      this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtCode.Size = new System.Drawing.Size(315, 150);
      this.txtCode.TabIndex = 0;
      this.txtCode.WordWrap = false;
      // 
      // DebugPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txtCode);
      this.Name = "DebugPanel";
      this.Size = new System.Drawing.Size(315, 150);
      this.Tag = "ToolPanel_Debug";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtCode;
  }
}

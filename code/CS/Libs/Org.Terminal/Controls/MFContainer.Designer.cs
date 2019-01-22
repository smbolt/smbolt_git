namespace Org.Terminal.Controls
{
  partial class MFContainer
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
      this.SuspendLayout();
      // 
      // MFContainer
      // 
      this.Click += new System.EventHandler(this.MFContainer_Click);
      this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.MFContainer_ControlAdded);
      this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.MFContainer_ControlRemoved);
      this.Resize += new System.EventHandler(this.MFContainer_Resize);
      this.ResumeLayout(false);

    }

    #endregion
  }
}

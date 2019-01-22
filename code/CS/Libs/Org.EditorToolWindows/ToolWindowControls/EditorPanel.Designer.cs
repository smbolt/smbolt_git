namespace Org.EditorToolWindows
{
  partial class EditorPanel
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
      this.pnlMain = new System.Windows.Forms.Panel();
      this.mfContainer = new Org.Terminal.Controls.MFContainer();
      this.pnlMain.SuspendLayout();
      this.SuspendLayout();
      //
      // pnlMain
      //
      this.pnlMain.Controls.Add(this.mfContainer);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 26);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(950, 678);
      this.pnlMain.TabIndex = 2;
      //
      // mfContainer
      //
      this.mfContainer.BackColor = System.Drawing.Color.Black;
      this.mfContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mfContainer.DrawMetrics = true;
      this.mfContainer.IsReadyForSizeManagement = false;
      this.mfContainer.Location = new System.Drawing.Point(0, 0);
      this.mfContainer.Name = "mfContainer";
      this.mfContainer.Size = new System.Drawing.Size(950, 678);
      this.mfContainer.TabIndex = 0;
      //
      // EditorPanel
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pnlMain);
      this.Name = "EditorPanel";
      this.Size = new System.Drawing.Size(950, 704);
      this.Tag = "ToolPanel_EditorPanel";
      this.Controls.SetChildIndex(this.pnlMain, 0);
      this.pnlMain.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlMain;
    private Org.Terminal.Controls.MFContainer mfContainer;
  }
}

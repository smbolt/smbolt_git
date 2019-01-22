namespace Org.PdfExtractToolWindows
{
  partial class PdfViewerControl
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
      this.dxPdfViewer = new DevExpress.XtraPdfViewer.PdfViewer();
      this.SuspendLayout();
      //
      // dxPdfViewer
      //
      this.dxPdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dxPdfViewer.Location = new System.Drawing.Point(0, 26);
      this.dxPdfViewer.Name = "dxPdfViewer";
      this.dxPdfViewer.Size = new System.Drawing.Size(485, 427);
      this.dxPdfViewer.TabIndex = 1;
      this.dxPdfViewer.Tag = "ToolPanel_PdfViewer";
      //
      // PdfViewer
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.dxPdfViewer);
      this.Name = "PdfViewer";
      this.Size = new System.Drawing.Size(485, 453);
      this.Tag = "ToolPanel_PdfViewer";
      this.Controls.SetChildIndex(this.dxPdfViewer, 0);
      this.ResumeLayout(false);

    }

    #endregion

    private DevExpress.XtraPdfViewer.PdfViewer dxPdfViewer;
  }
}

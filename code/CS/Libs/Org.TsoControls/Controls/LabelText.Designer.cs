namespace Org.TsoControls.Controls
{
  partial class LabelText
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
      this.lbl = new System.Windows.Forms.Label();
      this.SuspendLayout();
      //
      // lbl
      //
      this.lbl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbl.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl.ForeColor = System.Drawing.Color.White;
      this.lbl.Location = new System.Drawing.Point(0, 0);
      this.lbl.Name = "lbl";
      this.lbl.Size = new System.Drawing.Size(100, 23);
      this.lbl.TabIndex = 0;
      this.lbl.Text = "LABEL:";
      this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // LabelText
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.Controls.Add(this.lbl);
      this.DoubleBuffered = true;
      this.Name = "LabelText";
      this.Size = new System.Drawing.Size(100, 23);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lbl;
  }
}

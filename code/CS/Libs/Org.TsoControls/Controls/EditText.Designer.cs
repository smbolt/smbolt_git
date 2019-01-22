namespace Org.TsoControls.Controls
{
  partial class EditText
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
      this.txt = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      //
      // txt
      //
      this.txt.BackColor = System.Drawing.Color.Black;
      this.txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txt.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt.ForeColor = System.Drawing.SystemColors.MenuHighlight;
      this.txt.Location = new System.Drawing.Point(3, 0);
      this.txt.Multiline = false;
      this.txt.Name = "txt";
      this.txt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.txt.Size = new System.Drawing.Size(94, 23);
      this.txt.TabIndex = 0;
      this.txt.Text = "EDIT";
      this.txt.WordWrap = false;
      //
      // EditText
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.Controls.Add(this.txt);
      this.Name = "EditText";
      this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
      this.Size = new System.Drawing.Size(100, 23);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox txt;
  }
}

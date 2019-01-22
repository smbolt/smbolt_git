namespace Org.SF
{
  partial class frmTextDisplay
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTextDisplay));
      this.txtOut = new FastColoredTextBoxNS.FastColoredTextBox();
      ((System.ComponentModel.ISupportInitialize)(this.txtOut)).BeginInit();
      this.SuspendLayout();
      //
      // txtOut
      //
      this.txtOut.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''
      };
      this.txtOut.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtOut.BackBrush = null;
      this.txtOut.CharHeight = 13;
      this.txtOut.CharWidth = 7;
      this.txtOut.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtOut.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtOut.IsReplaceMode = false;
      this.txtOut.Location = new System.Drawing.Point(0, 0);
      this.txtOut.Name = "txtOut";
      this.txtOut.Paddings = new System.Windows.Forms.Padding(0);
      this.txtOut.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtOut.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtOut.ServiceColors")));
      this.txtOut.Size = new System.Drawing.Size(1255, 830);
      this.txtOut.TabIndex = 0;
      this.txtOut.Zoom = 100;
      //
      // frmTextDisplay
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1255, 830);
      this.Controls.Add(this.txtOut);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmTextDisplay";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Text Display";
      ((System.ComponentModel.ISupportInitialize)(this.txtOut)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private FastColoredTextBoxNS.FastColoredTextBox txtOut;
  }
}
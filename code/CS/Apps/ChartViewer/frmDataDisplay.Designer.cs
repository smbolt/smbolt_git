namespace Org.ChartViewer
{
  partial class frmDataDisplay
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataDisplay));
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnHide = new System.Windows.Forms.Button();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.ckCsv = new System.Windows.Forms.CheckBox();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.ckCsv);
      this.pnlTop.Controls.Add(this.btnHide);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1182, 48);
      this.pnlTop.TabIndex = 0;
      // 
      // btnHide
      // 
      this.btnHide.Location = new System.Drawing.Point(13, 12);
      this.btnHide.Name = "btnHide";
      this.btnHide.Size = new System.Drawing.Size(95, 26);
      this.btnHide.TabIndex = 0;
      this.btnHide.Text = "Hide";
      this.btnHide.UseVisualStyleBackColor = true;
      this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
      // 
      // txtOut
      // 
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Lucida Console", 8F);
      this.txtOut.Location = new System.Drawing.Point(0, 48);
      this.txtOut.MaxLength = 2147483646;
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1182, 633);
      this.txtOut.TabIndex = 1;
      this.txtOut.WordWrap = false;
      // 
      // ckCsv
      // 
      this.ckCsv.AutoSize = true;
      this.ckCsv.Location = new System.Drawing.Point(133, 17);
      this.ckCsv.Name = "ckCsv";
      this.ckCsv.Size = new System.Drawing.Size(91, 17);
      this.ckCsv.TabIndex = 1;
      this.ckCsv.Text = "Show as CSV";
      this.ckCsv.UseVisualStyleBackColor = true;
      this.ckCsv.CheckedChanged += new System.EventHandler(this.ckCsv_CheckedChanged);
      // 
      // frmDataDisplay
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1182, 681);
      this.ControlBox = false;
      this.Controls.Add(this.txtOut);
      this.Controls.Add(this.pnlTop);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmDataDisplay";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Forecast Data Viewer";
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnHide;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.CheckBox ckCsv;
  }
}
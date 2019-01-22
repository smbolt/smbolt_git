namespace Org.SF
{
  partial class frmSplash
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
      this.pnlMain = new System.Windows.Forms.Panel();
      this.pbMain = new System.Windows.Forms.PictureBox();
      this.lblMessage = new System.Windows.Forms.Label();
      this.lblCopyright = new System.Windows.Forms.Label();
      this.lblReset = new System.Windows.Forms.Label();
      this.lblVersion = new System.Windows.Forms.Label();
      this.pnlMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
      this.SuspendLayout();
      //
      // pnlMain
      //
      this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnlMain.Controls.Add(this.pbMain);
      this.pnlMain.Controls.Add(this.lblMessage);
      this.pnlMain.Controls.Add(this.lblCopyright);
      this.pnlMain.Controls.Add(this.lblReset);
      this.pnlMain.Controls.Add(this.lblVersion);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 0);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(500, 271);
      this.pnlMain.TabIndex = 10;
      this.pnlMain.UseWaitCursor = true;
      //
      // pbMain
      //
      this.pbMain.Location = new System.Drawing.Point(-1, -1);
      this.pbMain.Name = "pbMain";
      this.pbMain.Size = new System.Drawing.Size(500, 202);
      this.pbMain.TabIndex = 7;
      this.pbMain.TabStop = false;
      this.pbMain.UseWaitCursor = true;
      //
      // lblMessage
      //
      this.lblMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMessage.ForeColor = System.Drawing.Color.Black;
      this.lblMessage.Location = new System.Drawing.Point(11, 203);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(476, 22);
      this.lblMessage.TabIndex = 6;
      this.lblMessage.Text = "Message";
      this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lblMessage.UseWaitCursor = true;
      //
      // lblCopyright
      //
      this.lblCopyright.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCopyright.Location = new System.Drawing.Point(11, 225);
      this.lblCopyright.Name = "lblCopyright";
      this.lblCopyright.Size = new System.Drawing.Size(362, 22);
      this.lblCopyright.TabIndex = 4;
      this.lblCopyright.Text = "Copyright";
      this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lblCopyright.UseWaitCursor = true;
      //
      // lblReset
      //
      this.lblReset.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblReset.ForeColor = System.Drawing.Color.SteelBlue;
      this.lblReset.Location = new System.Drawing.Point(307, 249);
      this.lblReset.Name = "lblReset";
      this.lblReset.Size = new System.Drawing.Size(182, 17);
      this.lblReset.TabIndex = 2;
      this.lblReset.Text = "ESCAPED";
      this.lblReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lblReset.UseWaitCursor = true;
      //
      // lblVersion
      //
      this.lblVersion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblVersion.Location = new System.Drawing.Point(11, 247);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new System.Drawing.Size(187, 22);
      this.lblVersion.TabIndex = 3;
      this.lblVersion.Text = "Version";
      this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lblVersion.UseWaitCursor = true;
      //
      // frmSplash
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(500, 271);
      this.Controls.Add(this.pnlMain);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmSplash";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Load += new System.EventHandler(this.frmSplash_Load);
      this.Shown += new System.EventHandler(this.frmSplash_Shown);
      this.pnlMain.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlMain;
    internal System.Windows.Forms.PictureBox pbMain;
    internal System.Windows.Forms.Label lblMessage;
    internal System.Windows.Forms.Label lblCopyright;
    internal System.Windows.Forms.Label lblReset;
    internal System.Windows.Forms.Label lblVersion;
  }
}
namespace Org.ServiceAlert
{
  partial class frmMain
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.lblTitle = new System.Windows.Forms.Label();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.pbLogo = new System.Windows.Forms.PictureBox();
      this.pbError = new System.Windows.Forms.PictureBox();
      this.txtError = new System.Windows.Forms.TextBox();
      this.pnlBottomControls = new System.Windows.Forms.Panel();
      this.btnSendToSupport = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.lblMessage = new System.Windows.Forms.Label();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbError)).BeginInit();
      this.pnlBottomControls.SuspendLayout();
      this.SuspendLayout();
      //
      // lblTitle
      //
      this.lblTitle.AutoSize = true;
      this.lblTitle.Font = new System.Drawing.Font("Calibri", 22F, System.Drawing.FontStyle.Bold);
      this.lblTitle.ForeColor = System.Drawing.Color.White;
      this.lblTitle.Location = new System.Drawing.Point(60, 26);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(300, 37);
      this.lblTitle.TabIndex = 1;
      this.lblTitle.Text = "Windows Service Error";
      //
      // pnlTop
      //
      this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(88)))));
      this.pnlTop.Controls.Add(this.pbLogo);
      this.pnlTop.Controls.Add(this.pbError);
      this.pnlTop.Controls.Add(this.lblTitle);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(5, 4);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(650, 96);
      this.pnlTop.TabIndex = 2;
      //
      // pbLogo
      //
      this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.pbLogo.BackColor = System.Drawing.Color.White;
      this.pbLogo.Image = global::Org.ServiceAlert.Properties.Resources.ServiceAlertLogo;
      this.pbLogo.Location = new System.Drawing.Point(407, 3);
      this.pbLogo.Name = "pbLogo";
      this.pbLogo.Size = new System.Drawing.Size(240, 90);
      this.pbLogo.TabIndex = 2;
      this.pbLogo.TabStop = false;
      //
      // pbError
      //
      this.pbError.Image = global::Org.ServiceAlert.Properties.Resources.error2;
      this.pbError.Location = new System.Drawing.Point(11, 23);
      this.pbError.Name = "pbError";
      this.pbError.Size = new System.Drawing.Size(48, 48);
      this.pbError.TabIndex = 0;
      this.pbError.TabStop = false;
      //
      // txtError
      //
      this.txtError.BackColor = System.Drawing.Color.White;
      this.txtError.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtError.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtError.Location = new System.Drawing.Point(5, 128);
      this.txtError.Multiline = true;
      this.txtError.Name = "txtError";
      this.txtError.ReadOnly = true;
      this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtError.Size = new System.Drawing.Size(650, 179);
      this.txtError.TabIndex = 3;
      //
      // pnlBottomControls
      //
      this.pnlBottomControls.Controls.Add(this.btnSendToSupport);
      this.pnlBottomControls.Controls.Add(this.btnClose);
      this.pnlBottomControls.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottomControls.Location = new System.Drawing.Point(5, 307);
      this.pnlBottomControls.Name = "pnlBottomControls";
      this.pnlBottomControls.Size = new System.Drawing.Size(650, 35);
      this.pnlBottomControls.TabIndex = 4;
      //
      // btnSendToSupport
      //
      this.btnSendToSupport.Location = new System.Drawing.Point(0, 7);
      this.btnSendToSupport.Name = "btnSendToSupport";
      this.btnSendToSupport.Size = new System.Drawing.Size(160, 23);
      this.btnSendToSupport.TabIndex = 0;
      this.btnSendToSupport.Text = "Send to Support";
      this.btnSendToSupport.UseVisualStyleBackColor = true;
      this.btnSendToSupport.Click += new System.EventHandler(this.btnSendToAdsdi_Click);
      //
      // btnClose
      //
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(489, 7);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(160, 23);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      //
      // lblMessage
      //
      this.lblMessage.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblMessage.Location = new System.Drawing.Point(5, 100);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(650, 28);
      this.lblMessage.TabIndex = 5;
      this.lblMessage.Text = "An error has occurred with a Windows Service - please see below.";
      this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(660, 342);
      this.Controls.Add(this.txtError);
      this.Controls.Add(this.lblMessage);
      this.Controls.Add(this.pnlBottomControls);
      this.Controls.Add(this.pnlTop);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(5, 4, 5, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Windows Service Error Alerter";
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbError)).EndInit();
      this.pnlBottomControls.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pbError;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.TextBox txtError;
    private System.Windows.Forms.Panel pnlBottomControls;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnSendToSupport;
    private System.Windows.Forms.Label lblMessage;
    private System.Windows.Forms.PictureBox pbLogo;
  }
}


namespace Adsdi.EncryptedFileUtility
{
  partial class frmPassword
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPassword));
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.lblPasswordPrompt = new System.Windows.Forms.Label();
      this.lblPasswordInfo = new System.Windows.Forms.Label();
      this.pbLock = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pbLock)).BeginInit();
      this.SuspendLayout();
      //
      // btnCancel
      //
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(203, 88);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(85, 23);
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      //
      // btnOK
      //
      this.btnOK.Location = new System.Drawing.Point(109, 88);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(85, 23);
      this.btnOK.TabIndex = 6;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      //
      // txtPassword
      //
      this.txtPassword.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtPassword.Location = new System.Drawing.Point(109, 61);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(179, 21);
      this.txtPassword.TabIndex = 3;
      this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
      //
      // lblPasswordPrompt
      //
      this.lblPasswordPrompt.AutoSize = true;
      this.lblPasswordPrompt.Location = new System.Drawing.Point(106, 44);
      this.lblPasswordPrompt.Name = "lblPasswordPrompt";
      this.lblPasswordPrompt.Size = new System.Drawing.Size(114, 13);
      this.lblPasswordPrompt.TabIndex = 4;
      this.lblPasswordPrompt.Text = "Please enter password";
      //
      // lblPasswordInfo
      //
      this.lblPasswordInfo.AutoSize = true;
      this.lblPasswordInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblPasswordInfo.Location = new System.Drawing.Point(18, 16);
      this.lblPasswordInfo.Name = "lblPasswordInfo";
      this.lblPasswordInfo.Size = new System.Drawing.Size(285, 13);
      this.lblPasswordInfo.TabIndex = 5;
      this.lblPasswordInfo.Text = "A password is required to use this utility program.";
      //
      // pbLock
      //
      this.pbLock.Image = global::Adsdi.EncryptedFileUtility.Properties.Resources._lock;
      this.pbLock.Location = new System.Drawing.Point(34, 55);
      this.pbLock.Name = "pbLock";
      this.pbLock.Size = new System.Drawing.Size(48, 48);
      this.pbLock.TabIndex = 8;
      this.pbLock.TabStop = false;
      //
      // frmPassword
      //
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(340, 138);
      this.ControlBox = false;
      this.Controls.Add(this.pbLock);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.txtPassword);
      this.Controls.Add(this.lblPasswordPrompt);
      this.Controls.Add(this.lblPasswordInfo);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmPassword";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Please enter the required password";
      ((System.ComponentModel.ISupportInitialize)(this.pbLock)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Label lblPasswordPrompt;
    private System.Windows.Forms.Label lblPasswordInfo;
    private System.Windows.Forms.PictureBox pbLock;
  }
}
namespace Org.GS.Configuration
{
  partial class frmSecurityCache
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSecurityCache));
      this.lblUserName = new System.Windows.Forms.Label();
      this.lblUserNameValue = new System.Windows.Forms.Label();
      this.lblInfo = new System.Windows.Forms.Label();
      this.lbGroups = new System.Windows.Forms.CheckedListBox();
      this.lblSelectGroups = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.SuspendLayout();
      //
      // lblUserName
      //
      this.lblUserName.AutoSize = true;
      this.lblUserName.Location = new System.Drawing.Point(12, 71);
      this.lblUserName.Name = "lblUserName";
      this.lblUserName.Size = new System.Drawing.Size(60, 13);
      this.lblUserName.TabIndex = 0;
      this.lblUserName.Text = "User Name";
      //
      // lblUserNameValue
      //
      this.lblUserNameValue.BackColor = System.Drawing.Color.White;
      this.lblUserNameValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblUserNameValue.Location = new System.Drawing.Point(15, 87);
      this.lblUserNameValue.Name = "lblUserNameValue";
      this.lblUserNameValue.Size = new System.Drawing.Size(210, 23);
      this.lblUserNameValue.TabIndex = 1;
      this.lblUserNameValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lblInfo
      //
      this.lblInfo.Location = new System.Drawing.Point(12, 22);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new System.Drawing.Size(446, 45);
      this.lblInfo.TabIndex = 0;
      this.lblInfo.Text = "The temporary security cache will be built for the follwing user.  This cache wil" +
                          "l only be used in the event that Active Directory cannot be contacted by Patient" +
                          "Link.";
      this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // lbGroups
      //
      this.lbGroups.CheckOnClick = true;
      this.lbGroups.FormattingEnabled = true;
      this.lbGroups.Location = new System.Drawing.Point(15, 151);
      this.lbGroups.Name = "lbGroups";
      this.lbGroups.Size = new System.Drawing.Size(443, 229);
      this.lbGroups.TabIndex = 2;
      this.lbGroups.SelectedValueChanged += new System.EventHandler(this.lbGroups_SelectedValueChanged);
      //
      // lblSelectGroups
      //
      this.lblSelectGroups.AutoSize = true;
      this.lblSelectGroups.Location = new System.Drawing.Point(12, 132);
      this.lblSelectGroups.Name = "lblSelectGroups";
      this.lblSelectGroups.Size = new System.Drawing.Size(225, 13);
      this.lblSelectGroups.TabIndex = 0;
      this.lblSelectGroups.Text = "Select the desired ORG security groups.";
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(15, 387);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(127, 25);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      //
      // btnOK
      //
      this.btnOK.Location = new System.Drawing.Point(331, 387);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(127, 25);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      //
      // frmSecurityCache
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(475, 422);
      this.ControlBox = false;
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.lbGroups);
      this.Controls.Add(this.lblUserNameValue);
      this.Controls.Add(this.lblInfo);
      this.Controls.Add(this.lblSelectGroups);
      this.Controls.Add(this.lblUserName);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmSecurityCache";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Build Temporary Security Cache";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblUserName;
    private System.Windows.Forms.Label lblUserNameValue;
    private System.Windows.Forms.Label lblInfo;
    private System.Windows.Forms.CheckedListBox lbGroups;
    private System.Windows.Forms.Label lblSelectGroups;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
  }
}
namespace Org.GS
{
  partial class frmLocateAppConfig
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
      this.lblMessage = new System.Windows.Forms.Label();
      this.btnLocateAppConfig = new System.Windows.Forms.Button();
      this.btnUseDefaultAppConfig = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.lblLocatePrompt = new System.Windows.Forms.Label();
      this.lblUseInitAppConfig = new System.Windows.Forms.Label();
      this.lblCancel = new System.Windows.Forms.Label();
      this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
      this.SuspendLayout();
      //
      // lblMessage
      //
      this.lblMessage.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMessage.Location = new System.Drawing.Point(0, 0);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Padding = new System.Windows.Forms.Padding(8, 10, 8, 0);
      this.lblMessage.Size = new System.Drawing.Size(475, 33);
      this.lblMessage.TabIndex = 0;
      this.lblMessage.Text = "The application configuration file for this program is missing.";
      //
      // btnLocateAppConfig
      //
      this.btnLocateAppConfig.Location = new System.Drawing.Point(151, 77);
      this.btnLocateAppConfig.Name = "btnLocateAppConfig";
      this.btnLocateAppConfig.Size = new System.Drawing.Size(172, 23);
      this.btnLocateAppConfig.TabIndex = 1;
      this.btnLocateAppConfig.Tag = "LocateAppConfig";
      this.btnLocateAppConfig.Text = "Locate App Config File";
      this.btnLocateAppConfig.UseVisualStyleBackColor = true;
      this.btnLocateAppConfig.Click += new System.EventHandler(this.Action);
      //
      // btnUseDefaultAppConfig
      //
      this.btnUseDefaultAppConfig.Location = new System.Drawing.Point(151, 153);
      this.btnUseDefaultAppConfig.Name = "btnUseDefaultAppConfig";
      this.btnUseDefaultAppConfig.Size = new System.Drawing.Size(172, 23);
      this.btnUseDefaultAppConfig.TabIndex = 1;
      this.btnUseDefaultAppConfig.Tag = "UseDefaultAppConfig";
      this.btnUseDefaultAppConfig.Text = "Use Default App Config File";
      this.btnUseDefaultAppConfig.UseVisualStyleBackColor = true;
      this.btnUseDefaultAppConfig.Click += new System.EventHandler(this.Action);
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(151, 230);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(172, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.Action);
      //
      // lblLocatePrompt
      //
      this.lblLocatePrompt.AutoSize = true;
      this.lblLocatePrompt.Location = new System.Drawing.Point(13, 56);
      this.lblLocatePrompt.Name = "lblLocatePrompt";
      this.lblLocatePrompt.Size = new System.Drawing.Size(409, 13);
      this.lblLocatePrompt.TabIndex = 2;
      this.lblLocatePrompt.Text = "To attempt to locate an application configuration file for this program, click th" +
                                  "is button.";
      //
      // lblUseInitAppConfig
      //
      this.lblUseInitAppConfig.AutoSize = true;
      this.lblUseInitAppConfig.Location = new System.Drawing.Point(13, 131);
      this.lblUseInitAppConfig.Name = "lblUseInitAppConfig";
      this.lblUseInitAppConfig.Size = new System.Drawing.Size(413, 13);
      this.lblUseInitAppConfig.TabIndex = 2;
      this.lblUseInitAppConfig.Text = "To allow this program to create a default application configuration file, click t" +
                                      "his button.";
      //
      // lblCancel
      //
      this.lblCancel.AutoSize = true;
      this.lblCancel.Location = new System.Drawing.Point(13, 205);
      this.lblCancel.Name = "lblCancel";
      this.lblCancel.Size = new System.Drawing.Size(428, 13);
      this.lblCancel.TabIndex = 2;
      this.lblCancel.Text = "To close this dialog click this button.  It is possible that this program will no" +
                            "t be able to run.";
      //
      // dlgFileOpen
      //
      this.dlgFileOpen.Filter = "App Config Files (.xml)|*.xml*";
      this.dlgFileOpen.Title = "Locate Application Confiuration File";
      //
      // frmLocateAppConfig
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(475, 283);
      this.ControlBox = false;
      this.Controls.Add(this.lblCancel);
      this.Controls.Add(this.lblUseInitAppConfig);
      this.Controls.Add(this.lblLocatePrompt);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnUseDefaultAppConfig);
      this.Controls.Add(this.btnLocateAppConfig);
      this.Controls.Add(this.lblMessage);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "frmLocateAppConfig";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Location Application Configuration File";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblMessage;
    private System.Windows.Forms.Button btnLocateAppConfig;
    private System.Windows.Forms.Button btnUseDefaultAppConfig;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label lblLocatePrompt;
    private System.Windows.Forms.Label lblUseInitAppConfig;
    private System.Windows.Forms.Label lblCancel;
    private System.Windows.Forms.OpenFileDialog dlgFileOpen;
  }
}
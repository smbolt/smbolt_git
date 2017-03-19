namespace Org.SoftwareUpdates
{
  partial class frmUpdateError
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateError));
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblTitle = new System.Windows.Forms.Label();
      this.pbError = new System.Windows.Forms.PictureBox();
      this.pnlErrorMessage = new System.Windows.Forms.Panel();
      this.txtErrorMessage = new System.Windows.Forms.TextBox();
      this.lblErrorMessageHeader = new System.Windows.Forms.Label();
      this.pnlControl = new System.Windows.Forms.Panel();
      this.lblActionExplanation = new System.Windows.Forms.Label();
      this.lblAction = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.cboAction = new System.Windows.Forms.ComboBox();
      this.lblActionHeader = new System.Windows.Forms.Label();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbError)).BeginInit();
      this.pnlErrorMessage.SuspendLayout();
      this.pnlControl.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlTop
      // 
      this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(24)))), ((int)(((byte)(156)))));
      this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnlTop.Controls.Add(this.lblTitle);
      this.pnlTop.Controls.Add(this.pbError);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Padding = new System.Windows.Forms.Padding(8, 4, 4, 4);
      this.pnlTop.Size = new System.Drawing.Size(617, 56);
      this.pnlTop.TabIndex = 0;
      // 
      // lblTitle
      // 
      this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(24)))), ((int)(((byte)(156)))));
      this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTitle.ForeColor = System.Drawing.Color.White;
      this.lblTitle.Location = new System.Drawing.Point(56, 4);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
      this.lblTitle.Size = new System.Drawing.Size(555, 46);
      this.lblTitle.TabIndex = 1;
      this.lblTitle.Text = "Error message";
      this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pbError
      // 
      this.pbError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(24)))), ((int)(((byte)(156)))));
      this.pbError.Dock = System.Windows.Forms.DockStyle.Left;
      this.pbError.Image = global::Org.SoftwareUpdates.Resource1.error2;
      this.pbError.Location = new System.Drawing.Point(8, 4);
      this.pbError.Name = "pbError";
      this.pbError.Size = new System.Drawing.Size(48, 46);
      this.pbError.TabIndex = 0;
      this.pbError.TabStop = false;
      // 
      // pnlErrorMessage
      // 
      this.pnlErrorMessage.BackColor = System.Drawing.Color.White;
      this.pnlErrorMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pnlErrorMessage.Controls.Add(this.txtErrorMessage);
      this.pnlErrorMessage.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlErrorMessage.Location = new System.Drawing.Point(0, 80);
      this.pnlErrorMessage.Name = "pnlErrorMessage";
      this.pnlErrorMessage.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
      this.pnlErrorMessage.Size = new System.Drawing.Size(617, 131);
      this.pnlErrorMessage.TabIndex = 1;
      // 
      // txtErrorMessage
      // 
      this.txtErrorMessage.BackColor = System.Drawing.Color.White;
      this.txtErrorMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtErrorMessage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtErrorMessage.Location = new System.Drawing.Point(8, 4);
      this.txtErrorMessage.Multiline = true;
      this.txtErrorMessage.Name = "txtErrorMessage";
      this.txtErrorMessage.ReadOnly = true;
      this.txtErrorMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtErrorMessage.Size = new System.Drawing.Size(597, 119);
      this.txtErrorMessage.TabIndex = 99;
      this.txtErrorMessage.TabStop = false;
      // 
      // lblErrorMessageHeader
      // 
      this.lblErrorMessageHeader.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblErrorMessageHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblErrorMessageHeader.Location = new System.Drawing.Point(0, 56);
      this.lblErrorMessageHeader.Name = "lblErrorMessageHeader";
      this.lblErrorMessageHeader.Padding = new System.Windows.Forms.Padding(3, 0, 0, 2);
      this.lblErrorMessageHeader.Size = new System.Drawing.Size(617, 24);
      this.lblErrorMessageHeader.TabIndex = 0;
      this.lblErrorMessageHeader.Text = "Error Message";
      this.lblErrorMessageHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlControl
      // 
      this.pnlControl.Controls.Add(this.lblActionExplanation);
      this.pnlControl.Controls.Add(this.lblAction);
      this.pnlControl.Controls.Add(this.btnOK);
      this.pnlControl.Controls.Add(this.cboAction);
      this.pnlControl.Controls.Add(this.lblActionHeader);
      this.pnlControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlControl.Location = new System.Drawing.Point(0, 211);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new System.Windows.Forms.Padding(3);
      this.pnlControl.Size = new System.Drawing.Size(617, 117);
      this.pnlControl.TabIndex = 2;
      // 
      // lblActionExplanation
      // 
      this.lblActionExplanation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblActionExplanation.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblActionExplanation.Location = new System.Drawing.Point(3, 74);
      this.lblActionExplanation.Name = "lblActionExplanation";
      this.lblActionExplanation.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
      this.lblActionExplanation.Size = new System.Drawing.Size(611, 40);
      this.lblActionExplanation.TabIndex = 5;
      this.lblActionExplanation.Text = "Select an action from the drop-down list above to respond to the error.";
      // 
      // lblAction
      // 
      this.lblAction.AutoSize = true;
      this.lblAction.Location = new System.Drawing.Point(15, 30);
      this.lblAction.Name = "lblAction";
      this.lblAction.Size = new System.Drawing.Size(167, 13);
      this.lblAction.TabIndex = 4;
      this.lblAction.Text = "Select how to respond to the error";
      // 
      // btnOK
      // 
      this.btnOK.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnOK.Location = new System.Drawing.Point(263, 43);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(91, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // cboAction
      // 
      this.cboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboAction.FormattingEnabled = true;
      this.cboAction.Location = new System.Drawing.Point(10, 44);
      this.cboAction.Name = "cboAction";
      this.cboAction.Size = new System.Drawing.Size(247, 21);
      this.cboAction.TabIndex = 0;
      this.cboAction.SelectedIndexChanged += new System.EventHandler(this.cboAction_SelectedIndexChanged);
      // 
      // lblActionHeader
      // 
      this.lblActionHeader.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblActionHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblActionHeader.Location = new System.Drawing.Point(3, 3);
      this.lblActionHeader.Name = "lblActionHeader";
      this.lblActionHeader.Padding = new System.Windows.Forms.Padding(3, 0, 0, 2);
      this.lblActionHeader.Size = new System.Drawing.Size(611, 23);
      this.lblActionHeader.TabIndex = 1;
      this.lblActionHeader.Text = "Error response action";
      this.lblActionHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // frmUpdateError
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(617, 328);
      this.Controls.Add(this.pnlControl);
      this.Controls.Add(this.pnlErrorMessage);
      this.Controls.Add(this.lblErrorMessageHeader);
      this.Controls.Add(this.pnlTop);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmUpdateError";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Software Update - Error";
      this.pnlTop.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbError)).EndInit();
      this.pnlErrorMessage.ResumeLayout(false);
      this.pnlErrorMessage.PerformLayout();
      this.pnlControl.ResumeLayout(false);
      this.pnlControl.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.PictureBox pbError;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Panel pnlErrorMessage;
    private System.Windows.Forms.TextBox txtErrorMessage;
    private System.Windows.Forms.Label lblErrorMessageHeader;
    private System.Windows.Forms.Panel pnlControl;
    private System.Windows.Forms.Label lblActionExplanation;
    private System.Windows.Forms.Label lblAction;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.ComboBox cboAction;
    private System.Windows.Forms.Label lblActionHeader;
  }
}
namespace Org.TextUtility
{
  partial class frmSaveAs
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaveAs));
      this.lblNewFileName = new System.Windows.Forms.Label();
      this.txtNewFileName = new System.Windows.Forms.TextBox();
      this.cboExistingFiles = new System.Windows.Forms.ComboBox();
      this.lblExistingFiles = new System.Windows.Forms.Label();
      this.ckOverwriteExisting = new System.Windows.Forms.CheckBox();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      //
      // lblNewFileName
      //
      this.lblNewFileName.AutoSize = true;
      this.lblNewFileName.Location = new System.Drawing.Point(13, 16);
      this.lblNewFileName.Name = "lblNewFileName";
      this.lblNewFileName.Size = new System.Drawing.Size(107, 13);
      this.lblNewFileName.TabIndex = 0;
      this.lblNewFileName.Text = "Enter New File Name";
      //
      // txtNewFileName
      //
      this.txtNewFileName.Location = new System.Drawing.Point(16, 33);
      this.txtNewFileName.Name = "txtNewFileName";
      this.txtNewFileName.Size = new System.Drawing.Size(249, 20);
      this.txtNewFileName.TabIndex = 1;
      this.txtNewFileName.TextChanged += new System.EventHandler(this.txtNewFileName_TextChanged);
      //
      // cboExistingFiles
      //
      this.cboExistingFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboExistingFiles.FormattingEnabled = true;
      this.cboExistingFiles.Location = new System.Drawing.Point(16, 110);
      this.cboExistingFiles.Name = "cboExistingFiles";
      this.cboExistingFiles.Size = new System.Drawing.Size(249, 21);
      this.cboExistingFiles.TabIndex = 3;
      this.cboExistingFiles.SelectedIndexChanged += new System.EventHandler(this.cboExistingFiles_SelectedIndexChanged);
      //
      // lblExistingFiles
      //
      this.lblExistingFiles.AutoSize = true;
      this.lblExistingFiles.Location = new System.Drawing.Point(13, 94);
      this.lblExistingFiles.Name = "lblExistingFiles";
      this.lblExistingFiles.Size = new System.Drawing.Size(149, 13);
      this.lblExistingFiles.TabIndex = 0;
      this.lblExistingFiles.Text = "Select existing file to overwrite";
      //
      // ckOverwriteExisting
      //
      this.ckOverwriteExisting.AutoSize = true;
      this.ckOverwriteExisting.Location = new System.Drawing.Point(17, 71);
      this.ckOverwriteExisting.Name = "ckOverwriteExisting";
      this.ckOverwriteExisting.Size = new System.Drawing.Size(125, 17);
      this.ckOverwriteExisting.TabIndex = 4;
      this.ckOverwriteExisting.Text = "Overwrite existing file";
      this.ckOverwriteExisting.UseVisualStyleBackColor = true;
      this.ckOverwriteExisting.CheckedChanged += new System.EventHandler(this.ckOverwriteExisting_CheckedChanged);
      //
      // btnSave
      //
      this.btnSave.Location = new System.Drawing.Point(16, 155);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(113, 23);
      this.btnSave.TabIndex = 5;
      this.btnSave.Tag = "Save";
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      //
      // btnCancel
      //
      this.btnCancel.Location = new System.Drawing.Point(152, 155);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(113, 23);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      //
      // frmSaveAs
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(282, 193);
      this.ControlBox = false;
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.ckOverwriteExisting);
      this.Controls.Add(this.cboExistingFiles);
      this.Controls.Add(this.txtNewFileName);
      this.Controls.Add(this.lblExistingFiles);
      this.Controls.Add(this.lblNewFileName);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmSaveAs";
      this.Text = "Supply new file name";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblNewFileName;
    private System.Windows.Forms.TextBox txtNewFileName;
    private System.Windows.Forms.ComboBox cboExistingFiles;
    private System.Windows.Forms.Label lblExistingFiles;
    private System.Windows.Forms.CheckBox ckOverwriteExisting;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
  }
}
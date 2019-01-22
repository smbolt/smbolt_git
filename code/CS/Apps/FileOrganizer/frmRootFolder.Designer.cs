namespace Org.FileOrganizer {
  partial class frmRootFolder {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.lblRootFolderPath = new System.Windows.Forms.Label();
      this.txtRootFolderPath = new System.Windows.Forms.TextBox();
      this.txtRootFolderName = new System.Windows.Forms.TextBox();
      this.lblRootFolderName = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnBrowseForRootFolder = new System.Windows.Forms.Button();
      this.SuspendLayout();
      //
      // lblRootFolderPath
      //
      this.lblRootFolderPath.AutoSize = true;
      this.lblRootFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblRootFolderPath.Location = new System.Drawing.Point(62, 30);
      this.lblRootFolderPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblRootFolderPath.Name = "lblRootFolderPath";
      this.lblRootFolderPath.Size = new System.Drawing.Size(87, 13);
      this.lblRootFolderPath.TabIndex = 2;
      this.lblRootFolderPath.Text = "Root Folder Path";
      //
      // txtRootFolderPath
      //
      this.txtRootFolderPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtRootFolderPath.Location = new System.Drawing.Point(65, 61);
      this.txtRootFolderPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtRootFolderPath.Name = "txtRootFolderPath";
      this.txtRootFolderPath.Size = new System.Drawing.Size(577, 20);
      this.txtRootFolderPath.TabIndex = 3;
      //
      // txtRootFolderName
      //
      this.txtRootFolderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtRootFolderName.Location = new System.Drawing.Point(65, 132);
      this.txtRootFolderName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.txtRootFolderName.Name = "txtRootFolderName";
      this.txtRootFolderName.Size = new System.Drawing.Size(577, 20);
      this.txtRootFolderName.TabIndex = 5;
      //
      // lblRootFolderName
      //
      this.lblRootFolderName.AutoSize = true;
      this.lblRootFolderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblRootFolderName.Location = new System.Drawing.Point(62, 102);
      this.lblRootFolderName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblRootFolderName.Name = "lblRootFolderName";
      this.lblRootFolderName.Size = new System.Drawing.Size(93, 13);
      this.lblRootFolderName.TabIndex = 4;
      this.lblRootFolderName.Text = "Root Folder Name";
      //
      // btnCancel
      //
      this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnCancel.Location = new System.Drawing.Point(422, 195);
      this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(220, 52);
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Tag = "Cancel";
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      //
      // btnOK
      //
      this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnOK.Location = new System.Drawing.Point(65, 195);
      this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(220, 52);
      this.btnOK.TabIndex = 6;
      this.btnOK.Tag = "OK";
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.Action);
      //
      // btnBrowseForRootFolder
      //
      this.btnBrowseForRootFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnBrowseForRootFolder.Location = new System.Drawing.Point(473, 21);
      this.btnBrowseForRootFolder.Name = "btnBrowseForRootFolder";
      this.btnBrowseForRootFolder.Size = new System.Drawing.Size(169, 32);
      this.btnBrowseForRootFolder.TabIndex = 8;
      this.btnBrowseForRootFolder.Tag = "BrowseForRootFolder";
      this.btnBrowseForRootFolder.Text = "Browse";
      this.btnBrowseForRootFolder.UseVisualStyleBackColor = true;
      this.btnBrowseForRootFolder.Click += new System.EventHandler(this.btnBrowseForRootFolder_Click);
      //
      // frmRootFolder
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(723, 297);
      this.ControlBox = false;
      this.Controls.Add(this.btnBrowseForRootFolder);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.txtRootFolderName);
      this.Controls.Add(this.lblRootFolderName);
      this.Controls.Add(this.txtRootFolderPath);
      this.Controls.Add(this.lblRootFolderPath);
      this.Name = "frmRootFolder";
      this.Text = "Create New Root Folder";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblRootFolderPath;
    private System.Windows.Forms.TextBox txtRootFolderPath;
    private System.Windows.Forms.TextBox txtRootFolderName;
    private System.Windows.Forms.Label lblRootFolderName;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnBrowseForRootFolder;

  }
}
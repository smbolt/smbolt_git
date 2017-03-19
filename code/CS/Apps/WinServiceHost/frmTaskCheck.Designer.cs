namespace Org.WinServiceHost
{
    partial class frmTaskCheck
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTasks = new System.Windows.Forms.Label();
            this.gvTasks = new System.Windows.Forms.DataGridView();
            this.cboProfile = new System.Windows.Forms.ComboBox();
            this.lblProfileName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 270);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 28);
            this.btnOK.TabIndex = 1;
            this.btnOK.Tag = "OK";
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.Action);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(370, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Tag = "Cancel";
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Action);
            // 
            // lblTasks
            // 
            this.lblTasks.AutoSize = true;
            this.lblTasks.Location = new System.Drawing.Point(12, 53);
            this.lblTasks.Name = "lblTasks";
            this.lblTasks.Size = new System.Drawing.Size(90, 13);
            this.lblTasks.TabIndex = 2;
            this.lblTasks.Text = "Configured Tasks";
            // 
            // gvTasks
            // 
            this.gvTasks.AllowUserToAddRows = false;
            this.gvTasks.AllowUserToDeleteRows = false;
            this.gvTasks.AllowUserToResizeColumns = false;
            this.gvTasks.AllowUserToResizeRows = false;
            this.gvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTasks.Location = new System.Drawing.Point(12, 71);
            this.gvTasks.Name = "gvTasks";
            this.gvTasks.RowHeadersVisible = false;
            this.gvTasks.RowTemplate.Height = 18;
            this.gvTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvTasks.Size = new System.Drawing.Size(468, 193);
            this.gvTasks.TabIndex = 3;
            // 
            // cboProfile
            // 
            this.cboProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProfile.FormattingEnabled = true;
            this.cboProfile.Location = new System.Drawing.Point(12, 22);
            this.cboProfile.Name = "cboProfile";
            this.cboProfile.Size = new System.Drawing.Size(171, 21);
            this.cboProfile.TabIndex = 4;
            // 
            // lblProfileName
            // 
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(12, 6);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(36, 13);
            this.lblProfileName.TabIndex = 2;
            this.lblProfileName.Text = "Profile";
            // 
            // frmTaskCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 304);
            this.ControlBox = false;
            this.Controls.Add(this.cboProfile);
            this.Controls.Add(this.gvTasks);
            this.Controls.Add(this.lblProfileName);
            this.Controls.Add(this.lblTasks);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmTaskCheck";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "$AppName - Task Management";
            ((System.ComponentModel.ISupportInitialize)(this.gvTasks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTasks;
        private System.Windows.Forms.DataGridView gvTasks;
        private System.Windows.Forms.ComboBox cboProfile;
        private System.Windows.Forms.Label lblProfileName;
    }
}
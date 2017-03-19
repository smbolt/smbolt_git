namespace Org.CodePusher
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
      this.btnPushCode = new System.Windows.Forms.Button();
      this.lblSource = new System.Windows.Forms.Label();
      this.lblDestination = new System.Windows.Forms.Label();
      this.txtSource = new System.Windows.Forms.TextBox();
      this.txtDestination = new System.Windows.Forms.TextBox();
      this.txtResults = new System.Windows.Forms.TextBox();
      this.lblResults = new System.Windows.Forms.Label();
      this.ckReportOnly = new System.Windows.Forms.CheckBox();
      this.cboProfile = new System.Windows.Forms.ComboBox();
      this.lblProfile = new System.Windows.Forms.Label();
      this.lblStatus = new System.Windows.Forms.Label();
      this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblMappingControlElement = new System.Windows.Forms.Label();
      this.btnReloadCodePusherConfig = new System.Windows.Forms.Button();
      this.cboMappingControlElements = new System.Windows.Forms.ComboBox();
      this.ckClearDestination = new System.Windows.Forms.CheckBox();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.pnlTop.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnPushCode
      // 
      this.btnPushCode.Location = new System.Drawing.Point(862, 29);
      this.btnPushCode.Name = "btnPushCode";
      this.btnPushCode.Size = new System.Drawing.Size(138, 21);
      this.btnPushCode.TabIndex = 0;
      this.btnPushCode.Tag = "PushCode";
      this.btnPushCode.Text = "Push Code";
      this.btnPushCode.UseVisualStyleBackColor = true;
      this.btnPushCode.Click += new System.EventHandler(this.Action);
      // 
      // lblSource
      // 
      this.lblSource.AutoSize = true;
      this.lblSource.Location = new System.Drawing.Point(9, 59);
      this.lblSource.Name = "lblSource";
      this.lblSource.Size = new System.Drawing.Size(41, 13);
      this.lblSource.TabIndex = 1;
      this.lblSource.Text = "Source";
      // 
      // lblDestination
      // 
      this.lblDestination.AutoSize = true;
      this.lblDestination.Location = new System.Drawing.Point(9, 104);
      this.lblDestination.Name = "lblDestination";
      this.lblDestination.Size = new System.Drawing.Size(60, 13);
      this.lblDestination.TabIndex = 1;
      this.lblDestination.Text = "Destination";
      // 
      // txtSource
      // 
      this.txtSource.Location = new System.Drawing.Point(9, 75);
      this.txtSource.Name = "txtSource";
      this.txtSource.Size = new System.Drawing.Size(666, 20);
      this.txtSource.TabIndex = 2;
      // 
      // txtDestination
      // 
      this.txtDestination.Location = new System.Drawing.Point(9, 120);
      this.txtDestination.Name = "txtDestination";
      this.txtDestination.Size = new System.Drawing.Size(666, 20);
      this.txtDestination.TabIndex = 2;
      // 
      // txtResults
      // 
      this.txtResults.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtResults.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtResults.Location = new System.Drawing.Point(0, 13);
      this.txtResults.Multiline = true;
      this.txtResults.Name = "txtResults";
      this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtResults.Size = new System.Drawing.Size(1032, 534);
      this.txtResults.TabIndex = 3;
      // 
      // lblResults
      // 
      this.lblResults.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblResults.Location = new System.Drawing.Point(0, 0);
      this.lblResults.Name = "lblResults";
      this.lblResults.Size = new System.Drawing.Size(1032, 13);
      this.lblResults.TabIndex = 1;
      this.lblResults.Text = "Results";
      // 
      // ckReportOnly
      // 
      this.ckReportOnly.AutoSize = true;
      this.ckReportOnly.Location = new System.Drawing.Point(885, 99);
      this.ckReportOnly.Name = "ckReportOnly";
      this.ckReportOnly.Size = new System.Drawing.Size(82, 17);
      this.ckReportOnly.TabIndex = 4;
      this.ckReportOnly.Text = "Report Only";
      this.ckReportOnly.UseVisualStyleBackColor = true;
      // 
      // cboProfile
      // 
      this.cboProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboProfile.FormattingEnabled = true;
      this.cboProfile.Location = new System.Drawing.Point(9, 29);
      this.cboProfile.Name = "cboProfile";
      this.cboProfile.Size = new System.Drawing.Size(353, 21);
      this.cboProfile.TabIndex = 5;
      this.cboProfile.SelectedIndexChanged += new System.EventHandler(this.cboProfile_SelectedIndexChanged);
      // 
      // lblProfile
      // 
      this.lblProfile.AutoSize = true;
      this.lblProfile.Location = new System.Drawing.Point(9, 13);
      this.lblProfile.Name = "lblProfile";
      this.lblProfile.Size = new System.Drawing.Size(36, 13);
      this.lblProfile.TabIndex = 1;
      this.lblProfile.Text = "Profile";
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 710);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1032, 21);
      this.lblStatus.TabIndex = 6;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.lblMappingControlElement);
      this.pnlTop.Controls.Add(this.lblProfile);
      this.pnlTop.Controls.Add(this.btnReloadCodePusherConfig);
      this.pnlTop.Controls.Add(this.btnPushCode);
      this.pnlTop.Controls.Add(this.lblSource);
      this.pnlTop.Controls.Add(this.cboMappingControlElements);
      this.pnlTop.Controls.Add(this.cboProfile);
      this.pnlTop.Controls.Add(this.lblDestination);
      this.pnlTop.Controls.Add(this.txtSource);
      this.pnlTop.Controls.Add(this.txtDestination);
      this.pnlTop.Controls.Add(this.ckClearDestination);
      this.pnlTop.Controls.Add(this.ckReportOnly);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1032, 163);
      this.pnlTop.TabIndex = 8;
      // 
      // lblMappingControlElement
      // 
      this.lblMappingControlElement.AutoSize = true;
      this.lblMappingControlElement.Location = new System.Drawing.Point(368, 13);
      this.lblMappingControlElement.Name = "lblMappingControlElement";
      this.lblMappingControlElement.Size = new System.Drawing.Size(125, 13);
      this.lblMappingControlElement.TabIndex = 1;
      this.lblMappingControlElement.Text = "Mapping Control Element";
      // 
      // btnReloadCodePusherConfig
      // 
      this.btnReloadCodePusherConfig.Location = new System.Drawing.Point(862, 56);
      this.btnReloadCodePusherConfig.Name = "btnReloadCodePusherConfig";
      this.btnReloadCodePusherConfig.Size = new System.Drawing.Size(138, 21);
      this.btnReloadCodePusherConfig.TabIndex = 0;
      this.btnReloadCodePusherConfig.Tag = "ReloadCodePusherConfig";
      this.btnReloadCodePusherConfig.Text = "Reload Code Pusher Config";
      this.btnReloadCodePusherConfig.UseVisualStyleBackColor = true;
      this.btnReloadCodePusherConfig.Click += new System.EventHandler(this.Action);
      // 
      // cboMappingControlElements
      // 
      this.cboMappingControlElements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboMappingControlElements.FormattingEnabled = true;
      this.cboMappingControlElements.Location = new System.Drawing.Point(368, 29);
      this.cboMappingControlElements.Name = "cboMappingControlElements";
      this.cboMappingControlElements.Size = new System.Drawing.Size(307, 21);
      this.cboMappingControlElements.TabIndex = 5;
      this.cboMappingControlElements.SelectedIndexChanged += new System.EventHandler(this.cboMappingControlElements_SelectedIndexChanged);
      // 
      // ckClearDestination
      // 
      this.ckClearDestination.AutoSize = true;
      this.ckClearDestination.Location = new System.Drawing.Point(681, 123);
      this.ckClearDestination.Name = "ckClearDestination";
      this.ckClearDestination.Size = new System.Drawing.Size(138, 17);
      this.ckClearDestination.TabIndex = 4;
      this.ckClearDestination.Text = "Clear Files Before Move";
      this.ckClearDestination.UseVisualStyleBackColor = true;
      this.ckClearDestination.CheckedChanged += new System.EventHandler(this.ckClearDestination_CheckedChanged);
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.txtResults);
      this.pnlMain.Controls.Add(this.lblResults);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 163);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(1032, 547);
      this.pnlMain.TabIndex = 9;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1032, 731);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "ADSDI - Code Pusher";
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      this.pnlMain.PerformLayout();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPushCode;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.CheckBox ckReportOnly;
        private System.Windows.Forms.ComboBox cboProfile;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.OpenFileDialog dlgFileOpen;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblMappingControlElement;
        private System.Windows.Forms.ComboBox cboMappingControlElements;
        private System.Windows.Forms.CheckBox ckClearDestination;
        private System.Windows.Forms.Button btnReloadCodePusherConfig;
    }
}


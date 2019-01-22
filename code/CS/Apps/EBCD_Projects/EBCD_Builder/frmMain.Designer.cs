namespace Adsdi.EBCD_Builder
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
            this.components = new System.ComponentModel.Container();
            this.btnBuildEBCD = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnGetEBCD = new System.Windows.Forms.Button();
            this.lblObjectName = new System.Windows.Forms.Label();
            this.txtObjectName = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblBuildDate = new System.Windows.Forms.Label();
            this.lblBuildDateValue = new System.Windows.Forms.Label();
            this.gbIdentification = new System.Windows.Forms.GroupBox();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtEBCDValue = new System.Windows.Forms.TextBox();
            this.lblEBCDValue = new System.Windows.Forms.Label();
            this.txtEBCDKey = new System.Windows.Forms.TextBox();
            this.lblEBCDKey = new System.Windows.Forms.Label();
            this.lvMain = new System.Windows.Forms.ListView();
            this.chKey = new System.Windows.Forms.ColumnHeader();
            this.chValue = new System.Windows.Forms.ColumnHeader();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.ctxMnuLVMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMnuLVMainDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.gbIdentification.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.gbMain.SuspendLayout();
            this.ctxMnuLVMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBuildEBCD
            // 
            this.btnBuildEBCD.Location = new System.Drawing.Point(14, 7);
            this.btnBuildEBCD.Name = "btnBuildEBCD";
            this.btnBuildEBCD.Size = new System.Drawing.Size(98, 23);
            this.btnBuildEBCD.TabIndex = 91;
            this.btnBuildEBCD.Text = "Build EBCD";
            this.btnBuildEBCD.UseVisualStyleBackColor = true;
            this.btnBuildEBCD.Click += new System.EventHandler(this.btnBuildEBCD_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(118, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(98, 23);
            this.btnClear.TabIndex = 93;
            this.btnClear.Text = "Clear Form";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnGetEBCD
            // 
            this.btnGetEBCD.Location = new System.Drawing.Point(222, 7);
            this.btnGetEBCD.Name = "btnGetEBCD";
            this.btnGetEBCD.Size = new System.Drawing.Size(98, 23);
            this.btnGetEBCD.TabIndex = 92;
            this.btnGetEBCD.Text = "Get EBCD";
            this.btnGetEBCD.UseVisualStyleBackColor = true;
            this.btnGetEBCD.Click += new System.EventHandler(this.btnGetEBCD_Click);
            // 
            // lblObjectName
            // 
            this.lblObjectName.AutoSize = true;
            this.lblObjectName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObjectName.Location = new System.Drawing.Point(19, 28);
            this.lblObjectName.Name = "lblObjectName";
            this.lblObjectName.Size = new System.Drawing.Size(73, 13);
            this.lblObjectName.TabIndex = 10;
            this.lblObjectName.Text = "Object Name:";
            // 
            // txtObjectName
            // 
            this.txtObjectName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjectName.Location = new System.Drawing.Point(22, 44);
            this.txtObjectName.Name = "txtObjectName";
            this.txtObjectName.Size = new System.Drawing.Size(144, 21);
            this.txtObjectName.TabIndex = 1;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(175, 28);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(46, 13);
            this.lblVersion.TabIndex = 10;
            this.lblVersion.Text = "Version:";
            // 
            // txtVersion
            // 
            this.txtVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVersion.Location = new System.Drawing.Point(178, 44);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(144, 21);
            this.txtVersion.TabIndex = 2;
            // 
            // lblBuildDate
            // 
            this.lblBuildDate.AutoSize = true;
            this.lblBuildDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuildDate.Location = new System.Drawing.Point(565, 28);
            this.lblBuildDate.Name = "lblBuildDate";
            this.lblBuildDate.Size = new System.Drawing.Size(59, 13);
            this.lblBuildDate.TabIndex = 10;
            this.lblBuildDate.Text = "Build Date:";
            // 
            // lblBuildDateValue
            // 
            this.lblBuildDateValue.BackColor = System.Drawing.SystemColors.Control;
            this.lblBuildDateValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuildDateValue.Location = new System.Drawing.Point(566, 44);
            this.lblBuildDateValue.Name = "lblBuildDateValue";
            this.lblBuildDateValue.Size = new System.Drawing.Size(174, 21);
            this.lblBuildDateValue.TabIndex = 10;
            this.lblBuildDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbIdentification
            // 
            this.gbIdentification.Controls.Add(this.lblObjectName);
            this.gbIdentification.Controls.Add(this.txtKey);
            this.gbIdentification.Controls.Add(this.txtVersion);
            this.gbIdentification.Controls.Add(this.lblKey);
            this.gbIdentification.Controls.Add(this.lblVersion);
            this.gbIdentification.Controls.Add(this.txtObjectName);
            this.gbIdentification.Controls.Add(this.lblBuildDate);
            this.gbIdentification.Controls.Add(this.lblBuildDateValue);
            this.gbIdentification.Location = new System.Drawing.Point(14, 18);
            this.gbIdentification.Name = "gbIdentification";
            this.gbIdentification.Size = new System.Drawing.Size(967, 129);
            this.gbIdentification.TabIndex = 0;
            this.gbIdentification.TabStop = false;
            this.gbIdentification.Text = "EBCD Identification:";
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(993, 24);
            this.mnuMain.TabIndex = 13;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(103, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnBuildEBCD);
            this.pnlToolbar.Controls.Add(this.btnGetEBCD);
            this.pnlToolbar.Controls.Add(this.btnClear);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 24);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(993, 37);
            this.pnlToolbar.TabIndex = 90;
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(0, 638);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(993, 24);
            this.pnlStatus.TabIndex = 15;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(993, 24);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gbMain);
            this.pnlMain.Controls.Add(this.gbIdentification);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 61);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(993, 577);
            this.pnlMain.TabIndex = 16;
            // 
            // gbMain
            // 
            this.gbMain.Controls.Add(this.btnUpdate);
            this.gbMain.Controls.Add(this.txtEBCDValue);
            this.gbMain.Controls.Add(this.lblEBCDValue);
            this.gbMain.Controls.Add(this.txtEBCDKey);
            this.gbMain.Controls.Add(this.lblEBCDKey);
            this.gbMain.Controls.Add(this.lvMain);
            this.gbMain.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMain.Location = new System.Drawing.Point(14, 164);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(967, 410);
            this.gbMain.TabIndex = 10;
            this.gbMain.TabStop = false;
            this.gbMain.Text = "EBCD Data:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(831, 41);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(118, 21);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtEBCDValue
            // 
            this.txtEBCDValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEBCDValue.Location = new System.Drawing.Point(397, 41);
            this.txtEBCDValue.Name = "txtEBCDValue";
            this.txtEBCDValue.Size = new System.Drawing.Size(418, 21);
            this.txtEBCDValue.TabIndex = 12;
            // 
            // lblEBCDValue
            // 
            this.lblEBCDValue.AutoSize = true;
            this.lblEBCDValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEBCDValue.Location = new System.Drawing.Point(394, 25);
            this.lblEBCDValue.Name = "lblEBCDValue";
            this.lblEBCDValue.Size = new System.Drawing.Size(62, 13);
            this.lblEBCDValue.TabIndex = 1;
            this.lblEBCDValue.Text = "EBCD Value";
            // 
            // txtEBCDKey
            // 
            this.txtEBCDKey.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEBCDKey.Location = new System.Drawing.Point(30, 41);
            this.txtEBCDKey.Name = "txtEBCDKey";
            this.txtEBCDKey.Size = new System.Drawing.Size(349, 21);
            this.txtEBCDKey.TabIndex = 11;
            // 
            // lblEBCDKey
            // 
            this.lblEBCDKey.AutoSize = true;
            this.lblEBCDKey.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEBCDKey.Location = new System.Drawing.Point(27, 24);
            this.lblEBCDKey.Name = "lblEBCDKey";
            this.lblEBCDKey.Size = new System.Drawing.Size(54, 13);
            this.lblEBCDKey.TabIndex = 1;
            this.lblEBCDKey.Text = "EBCD Key";
            // 
            // lvMain
            // 
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chKey,
            this.chValue});
            this.lvMain.ContextMenuStrip = this.ctxMnuLVMain;
            this.lvMain.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMain.FullRowSelect = true;
            this.lvMain.GridLines = true;
            this.lvMain.Location = new System.Drawing.Point(22, 81);
            this.lvMain.MultiSelect = false;
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(927, 306);
            this.lvMain.TabIndex = 20;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMain_SelectedIndexChanged);
            // 
            // chKey
            // 
            this.chKey.Text = "EBCD Key";
            this.chKey.Width = 300;
            // 
            // chValue
            // 
            this.chValue.Text = "Value";
            this.chValue.Width = 600;
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKey.Location = new System.Drawing.Point(19, 74);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(83, 13);
            this.lblKey.TabIndex = 10;
            this.lblKey.Text = "Encryption Key:";
            // 
            // txtKey
            // 
            this.txtKey.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKey.Location = new System.Drawing.Point(22, 90);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(300, 21);
            this.txtKey.TabIndex = 4;
            // 
            // ctxMnuLVMain
            // 
            this.ctxMnuLVMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMnuLVMainDelete});
            this.ctxMnuLVMain.Name = "ctxMnuLVMain";
            this.ctxMnuLVMain.Size = new System.Drawing.Size(117, 26);
            this.ctxMnuLVMain.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMnuLVMain_Opening);
            // 
            // ctxMnuLVMainDelete
            // 
            this.ctxMnuLVMainDelete.Name = "ctxMnuLVMainDelete";
            this.ctxMnuLVMainDelete.Size = new System.Drawing.Size(152, 22);
            this.ctxMnuLVMainDelete.Text = "Delete";
            this.ctxMnuLVMainDelete.Click += new System.EventHandler(this.ctxMnuLVMainDelete_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 662);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.mnuMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EBCD Builder (Encrypted Binary Control Data)";
            this.gbIdentification.ResumeLayout(false);
            this.gbIdentification.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlStatus.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            this.ctxMnuLVMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuildEBCD;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnGetEBCD;
        private System.Windows.Forms.Label lblObjectName;
        private System.Windows.Forms.TextBox txtObjectName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblBuildDate;
        private System.Windows.Forms.Label lblBuildDateValue;
        private System.Windows.Forms.GroupBox gbIdentification;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox gbMain;
        private System.Windows.Forms.TextBox txtEBCDKey;
        private System.Windows.Forms.Label lblEBCDKey;
        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.ColumnHeader chKey;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.TextBox txtEBCDValue;
        private System.Windows.Forms.Label lblEBCDValue;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.ContextMenuStrip ctxMnuLVMain;
        private System.Windows.Forms.ToolStripMenuItem ctxMnuLVMainDelete;
    }
}


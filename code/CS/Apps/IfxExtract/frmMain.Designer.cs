namespace Org.IfxExtract
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.mnuMain = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.lblStatus = new System.Windows.Forms.Label();
			this.pnlTop = new System.Windows.Forms.Panel();
			this.btnWriteLog = new System.Windows.Forms.Button();
			this.btnGetIfxData = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txtOut = new FastColoredTextBoxNS.FastColoredTextBox();
			this.btnAddModule = new System.Windows.Forms.Button();
			this.mnuMain.SuspendLayout();
			this.pnlTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtOut)).BeginInit();
			this.SuspendLayout();
			// 
			// mnuMain
			// 
			this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
			this.mnuMain.Location = new System.Drawing.Point(0, 0);
			this.mnuMain.Name = "mnuMain";
			this.mnuMain.Size = new System.Drawing.Size(1162, 24);
			this.mnuMain.TabIndex = 0;
			this.mnuMain.Text = "menuStrip1";
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(37, 20);
			this.mnuFile.Text = "&File";
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Name = "mnuFileExit";
			this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
			this.mnuFileExit.Tag = "Exit";
			this.mnuFileExit.Text = "E&xit";
			this.mnuFileExit.Click += new System.EventHandler(this.Action);
			// 
			// lblStatus
			// 
			this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblStatus.Location = new System.Drawing.Point(0, 677);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.lblStatus.Size = new System.Drawing.Size(1162, 23);
			this.lblStatus.TabIndex = 1;
			this.lblStatus.Text = "Status";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlTop
			// 
			this.pnlTop.Controls.Add(this.btnAddModule);
			this.pnlTop.Controls.Add(this.btnWriteLog);
			this.pnlTop.Controls.Add(this.btnGetIfxData);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 24);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(1162, 56);
			this.pnlTop.TabIndex = 2;
			// 
			// btnWriteLog
			// 
			this.btnWriteLog.Location = new System.Drawing.Point(159, 18);
			this.btnWriteLog.Name = "btnWriteLog";
			this.btnWriteLog.Size = new System.Drawing.Size(141, 23);
			this.btnWriteLog.TabIndex = 0;
			this.btnWriteLog.Tag = "WriteLog";
			this.btnWriteLog.Text = "Write Log";
			this.btnWriteLog.UseVisualStyleBackColor = true;
			this.btnWriteLog.Click += new System.EventHandler(this.Action);
			// 
			// btnGetIfxData
			// 
			this.btnGetIfxData.Location = new System.Drawing.Point(12, 18);
			this.btnGetIfxData.Name = "btnGetIfxData";
			this.btnGetIfxData.Size = new System.Drawing.Size(141, 23);
			this.btnGetIfxData.TabIndex = 0;
			this.btnGetIfxData.Tag = "GetIfxData";
			this.btnGetIfxData.Text = "Get IFX Data";
			this.btnGetIfxData.UseVisualStyleBackColor = true;
			this.btnGetIfxData.Click += new System.EventHandler(this.Action);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 80);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.txtOut);
			this.splitContainer1.Size = new System.Drawing.Size(1162, 597);
			this.splitContainer1.SplitterDistance = 427;
			this.splitContainer1.TabIndex = 3;
			// 
			// txtOut
			// 
			this.txtOut.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
			this.txtOut.AutoScrollMinSize = new System.Drawing.Size(25, 13);
			this.txtOut.BackBrush = null;
			this.txtOut.CharHeight = 13;
			this.txtOut.CharWidth = 7;
			this.txtOut.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtOut.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOut.Font = new System.Drawing.Font("Courier New", 9F);
			this.txtOut.IsReplaceMode = false;
			this.txtOut.Location = new System.Drawing.Point(0, 0);
			this.txtOut.Name = "txtOut";
			this.txtOut.Paddings = new System.Windows.Forms.Padding(0);
			this.txtOut.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.txtOut.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtOut.ServiceColors")));
			this.txtOut.Size = new System.Drawing.Size(1162, 166);
			this.txtOut.TabIndex = 0;
			this.txtOut.Zoom = 100;
			// 
			// btnAddModule
			// 
			this.btnAddModule.Location = new System.Drawing.Point(306, 18);
			this.btnAddModule.Name = "btnAddModule";
			this.btnAddModule.Size = new System.Drawing.Size(141, 23);
			this.btnAddModule.TabIndex = 0;
			this.btnAddModule.Tag = "AddModule";
			this.btnAddModule.Text = "Add Module";
			this.btnAddModule.UseVisualStyleBackColor = true;
			this.btnAddModule.Click += new System.EventHandler(this.Action);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1162, 700);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.pnlTop);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.mnuMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mnuMain;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Informix Data Extract";
			this.mnuMain.ResumeLayout(false);
			this.mnuMain.PerformLayout();
			this.pnlTop.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtOut)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mnuMain;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button btnGetIfxData;
		private FastColoredTextBoxNS.FastColoredTextBox txtOut;
		private System.Windows.Forms.Button btnWriteLog;
		private System.Windows.Forms.Button btnAddModule;
	}
}


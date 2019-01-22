namespace Org.EbcdicConverter
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
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.lblStatus = new System.Windows.Forms.Label();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.lblFileName = new System.Windows.Forms.Label();
      this.txtFileName = new System.Windows.Forms.TextBox();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageCombined = new System.Windows.Forms.TabPage();
      this.txtCombined = new System.Windows.Forms.TextBox();
      this.pnlCombinedTop = new System.Windows.Forms.Panel();
      this.tabPageAscii = new System.Windows.Forms.TabPage();
      this.txtAscii = new System.Windows.Forms.TextBox();
      this.pnlAsciiTop = new System.Windows.Forms.Panel();
      this.tabPageEbcdic = new System.Windows.Forms.TabPage();
      this.txtEbcdic = new System.Windows.Forms.TextBox();
      this.pnlEbcdicTop = new System.Windows.Forms.Panel();
      this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
      this.cboCommand = new System.Windows.Forms.ComboBox();
      this.btnRunCommand = new System.Windows.Forms.Button();
      this.lblCommands = new System.Windows.Forms.Label();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageCombined.SuspendLayout();
      this.tabPageAscii.SuspendLayout();
      this.tabPageEbcdic.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.ImageScalingSize = new System.Drawing.Size(32, 32);
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
      this.mnuMain.Size = new System.Drawing.Size(1135, 24);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFileExit
      });
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 22);
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
      this.lblStatus.Location = new System.Drawing.Point(0, 727);
      this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1135, 21);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.cboCommand);
      this.pnlTop.Controls.Add(this.btnRunCommand);
      this.pnlTop.Controls.Add(this.btnBrowse);
      this.pnlTop.Controls.Add(this.lblCommands);
      this.pnlTop.Controls.Add(this.lblFileName);
      this.pnlTop.Controls.Add(this.txtFileName);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(2);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1135, 94);
      this.pnlTop.TabIndex = 2;
      //
      // btnBrowse
      //
      this.btnBrowse.Location = new System.Drawing.Point(666, 20);
      this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(77, 22);
      this.btnBrowse.TabIndex = 2;
      this.btnBrowse.Tag = "Browse";
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.Action);
      //
      // lblFileName
      //
      this.lblFileName.AutoSize = true;
      this.lblFileName.Location = new System.Drawing.Point(10, 7);
      this.lblFileName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblFileName.Name = "lblFileName";
      this.lblFileName.Size = new System.Drawing.Size(54, 13);
      this.lblFileName.TabIndex = 1;
      this.lblFileName.Text = "File Name";
      //
      // txtFileName
      //
      this.txtFileName.Location = new System.Drawing.Point(12, 21);
      this.txtFileName.Margin = new System.Windows.Forms.Padding(2);
      this.txtFileName.Name = "txtFileName";
      this.txtFileName.Size = new System.Drawing.Size(650, 20);
      this.txtFileName.TabIndex = 0;
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageCombined);
      this.tabMain.Controls.Add(this.tabPageAscii);
      this.tabMain.Controls.Add(this.tabPageEbcdic);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(145, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 118);
      this.tabMain.Margin = new System.Windows.Forms.Padding(2);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1135, 609);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 3;
      //
      // tabPageCombined
      //
      this.tabPageCombined.Controls.Add(this.txtCombined);
      this.tabPageCombined.Controls.Add(this.pnlCombinedTop);
      this.tabPageCombined.Location = new System.Drawing.Point(4, 22);
      this.tabPageCombined.Margin = new System.Windows.Forms.Padding(2);
      this.tabPageCombined.Name = "tabPageCombined";
      this.tabPageCombined.Padding = new System.Windows.Forms.Padding(2);
      this.tabPageCombined.Size = new System.Drawing.Size(1127, 583);
      this.tabPageCombined.TabIndex = 0;
      this.tabPageCombined.Text = "Combined";
      this.tabPageCombined.UseVisualStyleBackColor = true;
      //
      // txtCombined
      //
      this.txtCombined.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtCombined.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCombined.Font = new System.Drawing.Font("Lucida Console", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtCombined.Location = new System.Drawing.Point(2, 54);
      this.txtCombined.Margin = new System.Windows.Forms.Padding(2);
      this.txtCombined.Multiline = true;
      this.txtCombined.Name = "txtCombined";
      this.txtCombined.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtCombined.Size = new System.Drawing.Size(1123, 527);
      this.txtCombined.TabIndex = 1;
      this.txtCombined.WordWrap = false;
      //
      // pnlCombinedTop
      //
      this.pnlCombinedTop.BackColor = System.Drawing.SystemColors.Control;
      this.pnlCombinedTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlCombinedTop.Location = new System.Drawing.Point(2, 2);
      this.pnlCombinedTop.Margin = new System.Windows.Forms.Padding(2);
      this.pnlCombinedTop.Name = "pnlCombinedTop";
      this.pnlCombinedTop.Size = new System.Drawing.Size(1123, 52);
      this.pnlCombinedTop.TabIndex = 0;
      //
      // tabPageAscii
      //
      this.tabPageAscii.Controls.Add(this.txtAscii);
      this.tabPageAscii.Controls.Add(this.pnlAsciiTop);
      this.tabPageAscii.Location = new System.Drawing.Point(4, 22);
      this.tabPageAscii.Margin = new System.Windows.Forms.Padding(2);
      this.tabPageAscii.Name = "tabPageAscii";
      this.tabPageAscii.Padding = new System.Windows.Forms.Padding(2);
      this.tabPageAscii.Size = new System.Drawing.Size(1127, 608);
      this.tabPageAscii.TabIndex = 1;
      this.tabPageAscii.Text = "ASCII";
      this.tabPageAscii.UseVisualStyleBackColor = true;
      //
      // txtAscii
      //
      this.txtAscii.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtAscii.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtAscii.Font = new System.Drawing.Font("Lucida Console", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtAscii.Location = new System.Drawing.Point(2, 54);
      this.txtAscii.Margin = new System.Windows.Forms.Padding(2);
      this.txtAscii.Multiline = true;
      this.txtAscii.Name = "txtAscii";
      this.txtAscii.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtAscii.Size = new System.Drawing.Size(1123, 552);
      this.txtAscii.TabIndex = 2;
      this.txtAscii.WordWrap = false;
      //
      // pnlAsciiTop
      //
      this.pnlAsciiTop.BackColor = System.Drawing.SystemColors.Control;
      this.pnlAsciiTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlAsciiTop.Location = new System.Drawing.Point(2, 2);
      this.pnlAsciiTop.Margin = new System.Windows.Forms.Padding(2);
      this.pnlAsciiTop.Name = "pnlAsciiTop";
      this.pnlAsciiTop.Size = new System.Drawing.Size(1123, 52);
      this.pnlAsciiTop.TabIndex = 1;
      //
      // tabPageEbcdic
      //
      this.tabPageEbcdic.Controls.Add(this.txtEbcdic);
      this.tabPageEbcdic.Controls.Add(this.pnlEbcdicTop);
      this.tabPageEbcdic.Location = new System.Drawing.Point(4, 22);
      this.tabPageEbcdic.Margin = new System.Windows.Forms.Padding(2);
      this.tabPageEbcdic.Name = "tabPageEbcdic";
      this.tabPageEbcdic.Size = new System.Drawing.Size(1127, 608);
      this.tabPageEbcdic.TabIndex = 2;
      this.tabPageEbcdic.Text = "EBCDIC";
      this.tabPageEbcdic.UseVisualStyleBackColor = true;
      //
      // txtEbcdic
      //
      this.txtEbcdic.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtEbcdic.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtEbcdic.Font = new System.Drawing.Font("Lucida Console", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtEbcdic.Location = new System.Drawing.Point(0, 52);
      this.txtEbcdic.Margin = new System.Windows.Forms.Padding(2);
      this.txtEbcdic.Multiline = true;
      this.txtEbcdic.Name = "txtEbcdic";
      this.txtEbcdic.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtEbcdic.Size = new System.Drawing.Size(1127, 556);
      this.txtEbcdic.TabIndex = 2;
      this.txtEbcdic.WordWrap = false;
      //
      // pnlEbcdicTop
      //
      this.pnlEbcdicTop.BackColor = System.Drawing.SystemColors.Control;
      this.pnlEbcdicTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlEbcdicTop.Location = new System.Drawing.Point(0, 0);
      this.pnlEbcdicTop.Margin = new System.Windows.Forms.Padding(2);
      this.pnlEbcdicTop.Name = "pnlEbcdicTop";
      this.pnlEbcdicTop.Size = new System.Drawing.Size(1127, 52);
      this.pnlEbcdicTop.TabIndex = 1;
      //
      // dlgFileOpen
      //
      this.dlgFileOpen.Title = "Locate file to open...";
      //
      // cboCommand
      //
      this.cboCommand.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboCommand.FormattingEnabled = true;
      this.cboCommand.Location = new System.Drawing.Point(12, 59);
      this.cboCommand.Name = "cboCommand";
      this.cboCommand.Size = new System.Drawing.Size(650, 22);
      this.cboCommand.TabIndex = 3;
      this.cboCommand.SelectedIndexChanged += new System.EventHandler(this.cboCommand_SelectedIndexChanged);
      //
      // btnRunCommand
      //
      this.btnRunCommand.Location = new System.Drawing.Point(666, 59);
      this.btnRunCommand.Margin = new System.Windows.Forms.Padding(2);
      this.btnRunCommand.Name = "btnRunCommand";
      this.btnRunCommand.Size = new System.Drawing.Size(77, 22);
      this.btnRunCommand.TabIndex = 2;
      this.btnRunCommand.Tag = "RunCommand";
      this.btnRunCommand.Text = "Run";
      this.btnRunCommand.UseVisualStyleBackColor = true;
      this.btnRunCommand.Click += new System.EventHandler(this.Action);
      //
      // lblCommands
      //
      this.lblCommands.AutoSize = true;
      this.lblCommands.Location = new System.Drawing.Point(11, 45);
      this.lblCommands.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblCommands.Name = "lblCommands";
      this.lblCommands.Size = new System.Drawing.Size(59, 13);
      this.lblCommands.TabIndex = 1;
      this.lblCommands.Text = "Commands";
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1135, 748);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "EBCDIC / ASCII Converter";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabPageCombined.ResumeLayout(false);
      this.tabPageCombined.PerformLayout();
      this.tabPageAscii.ResumeLayout(false);
      this.tabPageAscii.PerformLayout();
      this.tabPageEbcdic.ResumeLayout(false);
      this.tabPageEbcdic.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageCombined;
    private System.Windows.Forms.TabPage tabPageAscii;
    private System.Windows.Forms.TabPage tabPageEbcdic;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.Label lblFileName;
    private System.Windows.Forms.TextBox txtFileName;
    private System.Windows.Forms.TextBox txtCombined;
    private System.Windows.Forms.Panel pnlCombinedTop;
    private System.Windows.Forms.TextBox txtAscii;
    private System.Windows.Forms.Panel pnlAsciiTop;
    private System.Windows.Forms.TextBox txtEbcdic;
    private System.Windows.Forms.Panel pnlEbcdicTop;
    private System.Windows.Forms.OpenFileDialog dlgFileOpen;
    private System.Windows.Forms.ComboBox cboCommand;
    private System.Windows.Forms.Button btnRunCommand;
    private System.Windows.Forms.Label lblCommands;
  }
}


namespace SystemChecker
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
      this.btnClose = new System.Windows.Forms.Button();
      this.pnlBottom = new System.Windows.Forms.Panel();
      this.btnCopyToClipboard = new System.Windows.Forms.Button();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblDescription = new System.Windows.Forms.Label();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptionsRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.pnlBottom.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.mnuMain.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.SuspendLayout();
      //
      // btnClose
      //
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnClose.Location = new System.Drawing.Point(457, 9);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(107, 28);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      //
      // pnlBottom
      //
      this.pnlBottom.BackColor = System.Drawing.Color.LightSteelBlue;
      this.pnlBottom.Controls.Add(this.btnCopyToClipboard);
      this.pnlBottom.Controls.Add(this.btnClose);
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 343);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(572, 45);
      this.pnlBottom.TabIndex = 1;
      //
      // btnCopyToClipboard
      //
      this.btnCopyToClipboard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnCopyToClipboard.Location = new System.Drawing.Point(8, 9);
      this.btnCopyToClipboard.Name = "btnCopyToClipboard";
      this.btnCopyToClipboard.Size = new System.Drawing.Size(146, 28);
      this.btnCopyToClipboard.TabIndex = 0;
      this.btnCopyToClipboard.Text = "Copy to Clipboard";
      this.btnCopyToClipboard.UseVisualStyleBackColor = true;
      this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
      //
      // pnlTop
      //
      this.pnlTop.BackColor = System.Drawing.Color.LightSteelBlue;
      this.pnlTop.Controls.Add(this.lblDescription);
      this.pnlTop.Controls.Add(this.mnuMain);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(572, 72);
      this.pnlTop.TabIndex = 2;
      //
      // lblDescription
      //
      this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDescription.Location = new System.Drawing.Point(0, 24);
      this.lblDescription.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Padding = new System.Windows.Forms.Padding(5, 5, 5, 7);
      this.lblDescription.Size = new System.Drawing.Size(572, 48);
      this.lblDescription.TabIndex = 0;
      this.lblDescription.Text = "System Checker gathers information about this computer for the purpose of determi" +
                                 "ning software compatibility.";
      this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtOut
      //
      this.txtOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Lucida Console", 8F);
      this.txtOut.Location = new System.Drawing.Point(6, 6);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ReadOnly = true;
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(560, 259);
      this.txtOut.TabIndex = 3;
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile,
        this.mnuOptions
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(572, 24);
      this.mnuMain.TabIndex = 1;
      this.mnuMain.Text = "menuStrip1";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFileExit
      });
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "&File";
      //
      // mnuFileExit
      //
      this.mnuFileExit.Name = "mnuFileExit";
      this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
      //
      // mnuOptions
      //
      this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuOptionsRefresh
      });
      this.mnuOptions.Name = "mnuOptions";
      this.mnuOptions.Size = new System.Drawing.Size(61, 20);
      this.mnuOptions.Text = "&Options";
      //
      // mnuOptionsRefresh
      //
      this.mnuOptionsRefresh.Name = "mnuOptionsRefresh";
      this.mnuOptionsRefresh.Size = new System.Drawing.Size(152, 22);
      this.mnuOptionsRefresh.Text = "&Refresh";
      this.mnuOptionsRefresh.Click += new System.EventHandler(this.mnuOptionsRefresh_Click);
      //
      // pnlMain
      //
      this.pnlMain.Controls.Add(this.txtOut);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 72);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Padding = new System.Windows.Forms.Padding(6);
      this.pnlMain.Size = new System.Drawing.Size(572, 271);
      this.pnlMain.TabIndex = 6;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(572, 388);
      this.Controls.Add(this.pnlMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.pnlBottom);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "System Checker - 1.0";
      this.pnlBottom.ResumeLayout(false);
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      this.pnlMain.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Panel pnlBottom;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.Button btnCopyToClipboard;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.ToolStripMenuItem mnuOptions;
    private System.Windows.Forms.ToolStripMenuItem mnuOptionsRefresh;
    private System.Windows.Forms.Panel pnlMain;
  }
}


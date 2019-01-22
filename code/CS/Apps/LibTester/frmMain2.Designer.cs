namespace Org.LibTester
{
  partial class frmMain2
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
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.lblStatus = new System.Windows.Forms.Label();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageOutput = new System.Windows.Forms.TabPage();
      this.btnParseReport = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageOutput.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1115, 24);
      this.mnuMain.TabIndex = 0;
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
      this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
      this.mnuFileExit.Tag = "Exit";
      this.mnuFileExit.Text = "E&xit";
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.btnParseReport);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1115, 70);
      this.pnlTop.TabIndex = 1;
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 660);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1115, 19);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtOut
      //
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(3, 3);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1101, 534);
      this.txtOut.TabIndex = 3;
      this.txtOut.WordWrap = false;
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageOutput);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(120, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 94);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1115, 566);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 4;
      //
      // tabPageOutput
      //
      this.tabPageOutput.Controls.Add(this.txtOut);
      this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
      this.tabPageOutput.Name = "tabPageOutput";
      this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageOutput.Size = new System.Drawing.Size(1107, 540);
      this.tabPageOutput.TabIndex = 1;
      this.tabPageOutput.Text = "Output";
      this.tabPageOutput.UseVisualStyleBackColor = true;
      //
      // btnParseReport
      //
      this.btnParseReport.Location = new System.Drawing.Point(42, 17);
      this.btnParseReport.Name = "btnParseReport";
      this.btnParseReport.Size = new System.Drawing.Size(111, 38);
      this.btnParseReport.TabIndex = 0;
      this.btnParseReport.Tag = "ParseReport";
      this.btnParseReport.Text = "Parse Report";
      this.btnParseReport.UseVisualStyleBackColor = true;
      this.btnParseReport.Click += new System.EventHandler(this.Action);
      //
      // frmMain2
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1115, 679);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain2";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Library Tester";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageOutput.ResumeLayout(false);
      this.tabPageOutput.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageOutput;
    private System.Windows.Forms.Button btnParseReport;
  }
}


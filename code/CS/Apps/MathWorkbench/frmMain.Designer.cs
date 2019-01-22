namespace MathWorkbench
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
      this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnCompute = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.txtEquation = new System.Windows.Forms.TextBox();
      this.lblEquation = new System.Windows.Forms.Label();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
      this.mnuMain.Size = new System.Drawing.Size(1214, 24);
      this.mnuMain.TabIndex = 0;
      this.mnuMain.Text = "menuStrip1";
      //
      // mnuFile
      //
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuExit
      });
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 22);
      this.mnuFile.Text = "&File";
      //
      // mnuExit
      //
      this.mnuExit.Name = "mnuExit";
      this.mnuExit.Size = new System.Drawing.Size(92, 22);
      this.mnuExit.Tag = "Exit";
      this.mnuExit.Text = "E&xit";
      this.mnuExit.Click += new System.EventHandler(this.Action);
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.lblEquation);
      this.pnlTop.Controls.Add(this.txtEquation);
      this.pnlTop.Controls.Add(this.btnCompute);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1214, 153);
      this.pnlTop.TabIndex = 1;
      //
      // btnCompute
      //
      this.btnCompute.Location = new System.Drawing.Point(12, 119);
      this.btnCompute.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.btnCompute.Name = "btnCompute";
      this.btnCompute.Size = new System.Drawing.Size(93, 26);
      this.btnCompute.TabIndex = 0;
      this.btnCompute.Tag = "Compute";
      this.btnCompute.Text = "Compute";
      this.btnCompute.UseVisualStyleBackColor = true;
      this.btnCompute.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 615);
      this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(1214, 27);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtOut
      //
      this.txtOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(0, 177);
      this.txtOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1214, 438);
      this.txtOut.TabIndex = 3;
      //
      // txtEquation
      //
      this.txtEquation.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtEquation.Location = new System.Drawing.Point(12, 23);
      this.txtEquation.Multiline = true;
      this.txtEquation.Name = "txtEquation";
      this.txtEquation.Size = new System.Drawing.Size(842, 90);
      this.txtEquation.TabIndex = 1;
      //
      // lblEquation
      //
      this.lblEquation.AutoSize = true;
      this.lblEquation.Location = new System.Drawing.Point(13, 4);
      this.lblEquation.Name = "lblEquation";
      this.lblEquation.Size = new System.Drawing.Size(49, 13);
      this.lblEquation.TabIndex = 2;
      this.lblEquation.Text = "Equation";
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1214, 642);
      this.Controls.Add(this.txtOut);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "MathWorkbench - v1.0.0.0";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnCompute;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.Label lblEquation;
    private System.Windows.Forms.TextBox txtEquation;
  }
}


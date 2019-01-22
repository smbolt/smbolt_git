namespace Org.MEFTest
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
      this.btnRunTaskProcessor = new System.Windows.Forms.Button();
      this.pnlTop = new System.Windows.Forms.Panel();
      this.ckWrapText = new System.Windows.Forms.CheckBox();
      this.ckUseLocalWebService = new System.Windows.Forms.CheckBox();
      this.lblEnvironment = new System.Windows.Forms.Label();
      this.lblTaskProcessor = new System.Windows.Forms.Label();
      this.cboEnvironment = new System.Windows.Forms.ComboBox();
      this.cboTaskToRun = new System.Windows.Forms.ComboBox();
      this.btnClearOutput = new System.Windows.Forms.Button();
      this.btnCancelTask = new System.Windows.Forms.Button();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.btnGetTypesFromTestAssemblies = new System.Windows.Forms.Button();
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
      this.mnuMain.Size = new System.Drawing.Size(1120, 24);
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
      this.mnuFileExit.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 625);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1120, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // btnRunTaskProcessor
      //
      this.btnRunTaskProcessor.Location = new System.Drawing.Point(13, 23);
      this.btnRunTaskProcessor.Name = "btnRunTaskProcessor";
      this.btnRunTaskProcessor.Size = new System.Drawing.Size(125, 23);
      this.btnRunTaskProcessor.TabIndex = 2;
      this.btnRunTaskProcessor.Tag = "RunTaskProcessor";
      this.btnRunTaskProcessor.Text = "Run Task Processor";
      this.btnRunTaskProcessor.UseVisualStyleBackColor = true;
      this.btnRunTaskProcessor.Click += new System.EventHandler(this.Action);
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.ckWrapText);
      this.pnlTop.Controls.Add(this.ckUseLocalWebService);
      this.pnlTop.Controls.Add(this.lblEnvironment);
      this.pnlTop.Controls.Add(this.lblTaskProcessor);
      this.pnlTop.Controls.Add(this.cboEnvironment);
      this.pnlTop.Controls.Add(this.cboTaskToRun);
      this.pnlTop.Controls.Add(this.btnClearOutput);
      this.pnlTop.Controls.Add(this.btnCancelTask);
      this.pnlTop.Controls.Add(this.btnGetTypesFromTestAssemblies);
      this.pnlTop.Controls.Add(this.btnRunTaskProcessor);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1120, 88);
      this.pnlTop.TabIndex = 3;
      //
      // ckWrapText
      //
      this.ckWrapText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ckWrapText.AutoSize = true;
      this.ckWrapText.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ckWrapText.Checked = true;
      this.ckWrapText.CheckState = System.Windows.Forms.CheckState.Checked;
      this.ckWrapText.Location = new System.Drawing.Point(1032, 35);
      this.ckWrapText.Name = "ckWrapText";
      this.ckWrapText.Size = new System.Drawing.Size(76, 17);
      this.ckWrapText.TabIndex = 7;
      this.ckWrapText.Tag = "WrapText";
      this.ckWrapText.Text = "Wrap Text";
      this.ckWrapText.UseVisualStyleBackColor = true;
      this.ckWrapText.CheckedChanged += new System.EventHandler(this.Action);
      //
      // ckUseLocalWebService
      //
      this.ckUseLocalWebService.AutoSize = true;
      this.ckUseLocalWebService.Location = new System.Drawing.Point(621, 27);
      this.ckUseLocalWebService.Name = "ckUseLocalWebService";
      this.ckUseLocalWebService.Size = new System.Drawing.Size(252, 17);
      this.ckUseLocalWebService.TabIndex = 7;
      this.ckUseLocalWebService.Text = "Use Local Web Service (Excel to DxWorkbook)";
      this.ckUseLocalWebService.UseVisualStyleBackColor = true;
      //
      // lblEnvironment
      //
      this.lblEnvironment.AutoSize = true;
      this.lblEnvironment.Location = new System.Drawing.Point(497, 8);
      this.lblEnvironment.Name = "lblEnvironment";
      this.lblEnvironment.Size = new System.Drawing.Size(99, 13);
      this.lblEnvironment.TabIndex = 6;
      this.lblEnvironment.Text = "Select Environment";
      //
      // lblTaskProcessor
      //
      this.lblTaskProcessor.AutoSize = true;
      this.lblTaskProcessor.Location = new System.Drawing.Point(149, 8);
      this.lblTaskProcessor.Name = "lblTaskProcessor";
      this.lblTaskProcessor.Size = new System.Drawing.Size(114, 13);
      this.lblTaskProcessor.TabIndex = 5;
      this.lblTaskProcessor.Text = "Select Task Processor";
      //
      // cboEnvironment
      //
      this.cboEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboEnvironment.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cboEnvironment.FormattingEnabled = true;
      this.cboEnvironment.Items.AddRange(new object[] {
        "Test",
        "Prod"
      });
      this.cboEnvironment.Location = new System.Drawing.Point(500, 24);
      this.cboEnvironment.Name = "cboEnvironment";
      this.cboEnvironment.Size = new System.Drawing.Size(111, 21);
      this.cboEnvironment.TabIndex = 4;
      this.cboEnvironment.Tag = "cboEnvironment";
      this.cboEnvironment.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
      //
      // cboTaskToRun
      //
      this.cboTaskToRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTaskToRun.FormattingEnabled = true;
      this.cboTaskToRun.Location = new System.Drawing.Point(150, 24);
      this.cboTaskToRun.Name = "cboTaskToRun";
      this.cboTaskToRun.Size = new System.Drawing.Size(336, 21);
      this.cboTaskToRun.TabIndex = 3;
      this.cboTaskToRun.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
      //
      // btnClearOutput
      //
      this.btnClearOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClearOutput.Location = new System.Drawing.Point(1014, 8);
      this.btnClearOutput.Name = "btnClearOutput";
      this.btnClearOutput.Size = new System.Drawing.Size(94, 23);
      this.btnClearOutput.TabIndex = 2;
      this.btnClearOutput.Tag = "ClearOutput";
      this.btnClearOutput.Text = "Clear Output";
      this.btnClearOutput.UseVisualStyleBackColor = true;
      this.btnClearOutput.Click += new System.EventHandler(this.Action);
      //
      // btnCancelTask
      //
      this.btnCancelTask.Location = new System.Drawing.Point(13, 51);
      this.btnCancelTask.Name = "btnCancelTask";
      this.btnCancelTask.Size = new System.Drawing.Size(125, 23);
      this.btnCancelTask.TabIndex = 2;
      this.btnCancelTask.Tag = "CancelTask";
      this.btnCancelTask.Text = "Cancel Task";
      this.btnCancelTask.UseVisualStyleBackColor = true;
      this.btnCancelTask.Click += new System.EventHandler(this.Action);
      //
      // txtOut
      //
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(0, 112);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1120, 513);
      this.txtOut.TabIndex = 4;
      this.txtOut.WordWrap = false;
      //
      // btnGetTypesFromTestAssemblies
      //
      this.btnGetTypesFromTestAssemblies.Location = new System.Drawing.Point(150, 51);
      this.btnGetTypesFromTestAssemblies.Name = "btnGetTypesFromTestAssemblies";
      this.btnGetTypesFromTestAssemblies.Size = new System.Drawing.Size(125, 23);
      this.btnGetTypesFromTestAssemblies.TabIndex = 2;
      this.btnGetTypesFromTestAssemblies.Tag = "GetTypesFromTestAssemblies";
      this.btnGetTypesFromTestAssemblies.Text = "Run Task Processor";
      this.btnGetTypesFromTestAssemblies.UseVisualStyleBackColor = true;
      this.btnGetTypesFromTestAssemblies.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1120, 648);
      this.Controls.Add(this.txtOut);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "MEF Tester";
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
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button btnRunTaskProcessor;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.ComboBox cboTaskToRun;
    private System.Windows.Forms.Button btnCancelTask;
    private System.Windows.Forms.ComboBox cboEnvironment;
    private System.Windows.Forms.CheckBox ckUseLocalWebService;
    private System.Windows.Forms.Label lblEnvironment;
    private System.Windows.Forms.Label lblTaskProcessor;
    private System.Windows.Forms.Button btnClearOutput;
    private System.Windows.Forms.CheckBox ckWrapText;
    private System.Windows.Forms.Button btnGetTypesFromTestAssemblies;
  }
}


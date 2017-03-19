namespace Org.NotificationTest
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
      this.pnlTopControl = new System.Windows.Forms.Panel();
      this.ckFromDatabase = new System.Windows.Forms.CheckBox();
      this.ckSynchronous = new System.Windows.Forms.CheckBox();
      this.ckThrowExceptions = new System.Windows.Forms.CheckBox();
      this.lblTaskResult = new System.Windows.Forms.Label();
      this.lblNotifyConfigSetName = new System.Windows.Forms.Label();
      this.lblTaskName = new System.Windows.Forms.Label();
      this.cboEventType = new System.Windows.Forms.ComboBox();
      this.cboTaskResult = new System.Windows.Forms.ComboBox();
      this.cboNotifyConfigSets = new System.Windows.Forms.ComboBox();
      this.cboTaskName = new System.Windows.Forms.ComboBox();
      this.btnReloadConfigs = new System.Windows.Forms.Button();
      this.btnTestNotification = new System.Windows.Forms.Button();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.mnuMain.SuspendLayout();
      this.pnlTopControl.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1408, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(0, 724);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1408, 21);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlTopControl
      // 
      this.pnlTopControl.Controls.Add(this.ckFromDatabase);
      this.pnlTopControl.Controls.Add(this.ckSynchronous);
      this.pnlTopControl.Controls.Add(this.ckThrowExceptions);
      this.pnlTopControl.Controls.Add(this.lblTaskResult);
      this.pnlTopControl.Controls.Add(this.lblNotifyConfigSetName);
      this.pnlTopControl.Controls.Add(this.lblTaskName);
      this.pnlTopControl.Controls.Add(this.cboEventType);
      this.pnlTopControl.Controls.Add(this.cboTaskResult);
      this.pnlTopControl.Controls.Add(this.cboNotifyConfigSets);
      this.pnlTopControl.Controls.Add(this.cboTaskName);
      this.pnlTopControl.Controls.Add(this.btnReloadConfigs);
      this.pnlTopControl.Controls.Add(this.btnTestNotification);
      this.pnlTopControl.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTopControl.Location = new System.Drawing.Point(0, 24);
      this.pnlTopControl.Name = "pnlTopControl";
      this.pnlTopControl.Size = new System.Drawing.Size(1408, 107);
      this.pnlTopControl.TabIndex = 2;
      // 
      // ckFromDatabase
      // 
      this.ckFromDatabase.AutoSize = true;
      this.ckFromDatabase.Location = new System.Drawing.Point(1256, 33);
      this.ckFromDatabase.Name = "ckFromDatabase";
      this.ckFromDatabase.Size = new System.Drawing.Size(98, 17);
      this.ckFromDatabase.TabIndex = 4;
      this.ckFromDatabase.Text = "From Database";
      this.ckFromDatabase.UseVisualStyleBackColor = true;
      // 
      // ckSynchronous
      // 
      this.ckSynchronous.AutoSize = true;
      this.ckSynchronous.Location = new System.Drawing.Point(927, 41);
      this.ckSynchronous.Name = "ckSynchronous";
      this.ckSynchronous.Size = new System.Drawing.Size(88, 17);
      this.ckSynchronous.TabIndex = 3;
      this.ckSynchronous.Text = "Synchronous";
      this.ckSynchronous.UseVisualStyleBackColor = true;
      // 
      // ckThrowExceptions
      // 
      this.ckThrowExceptions.AutoSize = true;
      this.ckThrowExceptions.Location = new System.Drawing.Point(927, 22);
      this.ckThrowExceptions.Name = "ckThrowExceptions";
      this.ckThrowExceptions.Size = new System.Drawing.Size(111, 17);
      this.ckThrowExceptions.TabIndex = 3;
      this.ckThrowExceptions.Text = "Throw Exceptions";
      this.ckThrowExceptions.UseVisualStyleBackColor = true;
      // 
      // lblTaskResult
      // 
      this.lblTaskResult.AutoSize = true;
      this.lblTaskResult.Location = new System.Drawing.Point(461, 7);
      this.lblTaskResult.Name = "lblTaskResult";
      this.lblTaskResult.Size = new System.Drawing.Size(61, 13);
      this.lblTaskResult.TabIndex = 2;
      this.lblTaskResult.Text = "TaskResult";
      // 
      // lblNotifyConfigSetName
      // 
      this.lblNotifyConfigSetName.AutoSize = true;
      this.lblNotifyConfigSetName.Location = new System.Drawing.Point(9, 6);
      this.lblNotifyConfigSetName.Name = "lblNotifyConfigSetName";
      this.lblNotifyConfigSetName.Size = new System.Drawing.Size(80, 13);
      this.lblNotifyConfigSetName.TabIndex = 2;
      this.lblNotifyConfigSetName.Text = "NotifyConfigSet";
      // 
      // lblTaskName
      // 
      this.lblTaskName.AutoSize = true;
      this.lblTaskName.Location = new System.Drawing.Point(237, 7);
      this.lblTaskName.Name = "lblTaskName";
      this.lblTaskName.Size = new System.Drawing.Size(62, 13);
      this.lblTaskName.TabIndex = 2;
      this.lblTaskName.Text = "Task Name";
      // 
      // cboEventType
      // 
      this.cboEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboEventType.FormattingEnabled = true;
      this.cboEventType.Items.AddRange(new object[] {
            "NotificationRequested",
            "NotificationCompleted",
            "NotificationFailed"});
      this.cboEventType.Location = new System.Drawing.Point(749, 50);
      this.cboEventType.Name = "cboEventType";
      this.cboEventType.Size = new System.Drawing.Size(171, 21);
      this.cboEventType.TabIndex = 1;
      // 
      // cboTaskResult
      // 
      this.cboTaskResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTaskResult.FormattingEnabled = true;
      this.cboTaskResult.Items.AddRange(new object[] {
            "Success",
            "Warning",
            "Failed",
            "Canceled"});
      this.cboTaskResult.Location = new System.Drawing.Point(464, 23);
      this.cboTaskResult.Name = "cboTaskResult";
      this.cboTaskResult.Size = new System.Drawing.Size(211, 21);
      this.cboTaskResult.TabIndex = 1;
      // 
      // cboNotifyConfigSets
      // 
      this.cboNotifyConfigSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboNotifyConfigSets.FormattingEnabled = true;
      this.cboNotifyConfigSets.Location = new System.Drawing.Point(12, 23);
      this.cboNotifyConfigSets.Name = "cboNotifyConfigSets";
      this.cboNotifyConfigSets.Size = new System.Drawing.Size(211, 21);
      this.cboNotifyConfigSets.TabIndex = 1;
      this.cboNotifyConfigSets.SelectedIndexChanged += new System.EventHandler(this.cboNotifyConfigSets_SelectedIndexChanged);
      // 
      // cboTaskName
      // 
      this.cboTaskName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTaskName.FormattingEnabled = true;
      this.cboTaskName.Location = new System.Drawing.Point(240, 23);
      this.cboTaskName.Name = "cboTaskName";
      this.cboTaskName.Size = new System.Drawing.Size(211, 21);
      this.cboTaskName.TabIndex = 1;
      // 
      // btnReloadConfigs
      // 
      this.btnReloadConfigs.Location = new System.Drawing.Point(1248, 8);
      this.btnReloadConfigs.Name = "btnReloadConfigs";
      this.btnReloadConfigs.Size = new System.Drawing.Size(132, 23);
      this.btnReloadConfigs.TabIndex = 0;
      this.btnReloadConfigs.Tag = "ReloadConfigs";
      this.btnReloadConfigs.Text = "Reload Configs";
      this.btnReloadConfigs.UseVisualStyleBackColor = true;
      this.btnReloadConfigs.Click += new System.EventHandler(this.Action);
      // 
      // btnTestNotification
      // 
      this.btnTestNotification.Location = new System.Drawing.Point(749, 21);
      this.btnTestNotification.Name = "btnTestNotification";
      this.btnTestNotification.Size = new System.Drawing.Size(171, 23);
      this.btnTestNotification.TabIndex = 0;
      this.btnTestNotification.Tag = "TestNotification";
      this.btnTestNotification.Text = "Test Notification";
      this.btnTestNotification.UseVisualStyleBackColor = true;
      this.btnTestNotification.Click += new System.EventHandler(this.Action);
      // 
      // txtOut
      // 
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(0, 131);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(1408, 593);
      this.txtOut.TabIndex = 4;
      this.txtOut.WordWrap = false;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1408, 745);
      this.Controls.Add(this.txtOut);
      this.Controls.Add(this.pnlTopControl);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Notification Test";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTopControl.ResumeLayout(false);
      this.pnlTopControl.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTopControl;
    private System.Windows.Forms.Button btnTestNotification;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.ComboBox cboTaskResult;
    private System.Windows.Forms.ComboBox cboTaskName;
    private System.Windows.Forms.CheckBox ckSynchronous;
    private System.Windows.Forms.CheckBox ckThrowExceptions;
    private System.Windows.Forms.Label lblTaskResult;
    private System.Windows.Forms.Label lblTaskName;
    private System.Windows.Forms.Label lblNotifyConfigSetName;
    private System.Windows.Forms.ComboBox cboNotifyConfigSets;
    private System.Windows.Forms.CheckBox ckFromDatabase;
    private System.Windows.Forms.Button btnReloadConfigs;
    private System.Windows.Forms.ComboBox cboEventType;
  }
}


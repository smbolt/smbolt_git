namespace Org.JsonTest
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
      this.pnlTop = new System.Windows.Forms.Panel();
      this.btnLoadAppConfig = new System.Windows.Forms.Button();
      this.btnDeserializeObject = new System.Windows.Forms.Button();
      this.btnSerializeObject = new System.Windows.Forms.Button();
      this.btnBuildCommandSet = new System.Windows.Forms.Button();
      this.btnRun = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageRawText = new System.Windows.Forms.TabPage();
      this.txtRaw = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tabPageJson = new System.Windows.Forms.TabPage();
      this.txtJson = new FastColoredTextBoxNS.FastColoredTextBox();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageRawText.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtRaw)).BeginInit();
      this.tabPageJson.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtJson)).BeginInit();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1344, 24);
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
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.btnLoadAppConfig);
      this.pnlTop.Controls.Add(this.btnDeserializeObject);
      this.pnlTop.Controls.Add(this.btnSerializeObject);
      this.pnlTop.Controls.Add(this.btnBuildCommandSet);
      this.pnlTop.Controls.Add(this.btnRun);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1344, 73);
      this.pnlTop.TabIndex = 1;
      // 
      // btnLoadAppConfig
      // 
      this.btnLoadAppConfig.Location = new System.Drawing.Point(495, 8);
      this.btnLoadAppConfig.Name = "btnLoadAppConfig";
      this.btnLoadAppConfig.Size = new System.Drawing.Size(128, 23);
      this.btnLoadAppConfig.TabIndex = 0;
      this.btnLoadAppConfig.Tag = "LoadAppConfig";
      this.btnLoadAppConfig.Text = "Load AppConfig";
      this.btnLoadAppConfig.UseVisualStyleBackColor = true;
      this.btnLoadAppConfig.Click += new System.EventHandler(this.Action);
      // 
      // btnDeserializeObject
      // 
      this.btnDeserializeObject.Location = new System.Drawing.Point(361, 8);
      this.btnDeserializeObject.Name = "btnDeserializeObject";
      this.btnDeserializeObject.Size = new System.Drawing.Size(128, 23);
      this.btnDeserializeObject.TabIndex = 0;
      this.btnDeserializeObject.Tag = "DeserializeObject";
      this.btnDeserializeObject.Text = "Deserialize Object";
      this.btnDeserializeObject.UseVisualStyleBackColor = true;
      this.btnDeserializeObject.Click += new System.EventHandler(this.Action);
      // 
      // btnSerializeObject
      // 
      this.btnSerializeObject.Location = new System.Drawing.Point(228, 8);
      this.btnSerializeObject.Name = "btnSerializeObject";
      this.btnSerializeObject.Size = new System.Drawing.Size(128, 23);
      this.btnSerializeObject.TabIndex = 0;
      this.btnSerializeObject.Tag = "SerializeObject";
      this.btnSerializeObject.Text = "Serialize Object";
      this.btnSerializeObject.UseVisualStyleBackColor = true;
      this.btnSerializeObject.Click += new System.EventHandler(this.Action);
      // 
      // btnBuildCommandSet
      // 
      this.btnBuildCommandSet.Location = new System.Drawing.Point(13, 37);
      this.btnBuildCommandSet.Name = "btnBuildCommandSet";
      this.btnBuildCommandSet.Size = new System.Drawing.Size(128, 23);
      this.btnBuildCommandSet.TabIndex = 0;
      this.btnBuildCommandSet.Tag = "BuildCommandSet";
      this.btnBuildCommandSet.Text = "Build Command Set";
      this.btnBuildCommandSet.UseVisualStyleBackColor = true;
      this.btnBuildCommandSet.Click += new System.EventHandler(this.Action);
      // 
      // btnRun
      // 
      this.btnRun.Location = new System.Drawing.Point(13, 8);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(128, 23);
      this.btnRun.TabIndex = 0;
      this.btnRun.Tag = "BuildMessage";
      this.btnRun.Text = "BuildMessage";
      this.btnRun.UseVisualStyleBackColor = true;
      this.btnRun.Click += new System.EventHandler(this.Action);
      // 
      // lblStatus
      // 
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 658);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1344, 21);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tabPageRawText);
      this.tabMain.Controls.Add(this.tabPageJson);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(125, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 97);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1344, 561);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 3;
      // 
      // tabPageRawText
      // 
      this.tabPageRawText.Controls.Add(this.txtRaw);
      this.tabPageRawText.Location = new System.Drawing.Point(4, 22);
      this.tabPageRawText.Name = "tabPageRawText";
      this.tabPageRawText.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
      this.tabPageRawText.Size = new System.Drawing.Size(1336, 535);
      this.tabPageRawText.TabIndex = 0;
      this.tabPageRawText.Text = "Raw Text";
      this.tabPageRawText.UseVisualStyleBackColor = true;
      // 
      // txtRaw
      // 
      this.txtRaw.AutoCompleteBracketsList = new char[] {
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
      this.txtRaw.AutoScrollMinSize = new System.Drawing.Size(25, 13);
      this.txtRaw.BackBrush = null;
      this.txtRaw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtRaw.CharHeight = 13;
      this.txtRaw.CharWidth = 7;
      this.txtRaw.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtRaw.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtRaw.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtRaw.Font = new System.Drawing.Font("Courier New", 9F);
      this.txtRaw.IsReplaceMode = false;
      this.txtRaw.Location = new System.Drawing.Point(3, 3);
      this.txtRaw.Name = "txtRaw";
      this.txtRaw.Paddings = new System.Windows.Forms.Padding(0);
      this.txtRaw.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtRaw.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtRaw.ServiceColors")));
      this.txtRaw.Size = new System.Drawing.Size(1330, 529);
      this.txtRaw.TabIndex = 0;
      this.txtRaw.Zoom = 100;
      // 
      // tabPageJson
      // 
      this.tabPageJson.Controls.Add(this.txtJson);
      this.tabPageJson.Location = new System.Drawing.Point(4, 22);
      this.tabPageJson.Name = "tabPageJson";
      this.tabPageJson.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
      this.tabPageJson.Size = new System.Drawing.Size(1336, 535);
      this.tabPageJson.TabIndex = 1;
      this.tabPageJson.Text = "Json ";
      this.tabPageJson.UseVisualStyleBackColor = true;
      // 
      // txtJson
      // 
      this.txtJson.AutoCompleteBracketsList = new char[] {
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
      this.txtJson.AutoScrollMinSize = new System.Drawing.Size(27, 14);
      this.txtJson.BackBrush = null;
      this.txtJson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtJson.CharHeight = 14;
      this.txtJson.CharWidth = 8;
      this.txtJson.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.txtJson.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.txtJson.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtJson.Font = new System.Drawing.Font("Courier New", 9.75F);
      this.txtJson.IsReplaceMode = false;
      this.txtJson.Location = new System.Drawing.Point(3, 3);
      this.txtJson.Name = "txtJson";
      this.txtJson.Paddings = new System.Windows.Forms.Padding(0);
      this.txtJson.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.txtJson.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtJson.ServiceColors")));
      this.txtJson.Size = new System.Drawing.Size(1330, 529);
      this.txtJson.TabIndex = 1;
      this.txtJson.Zoom = 100;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1344, 679);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Json Test";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPageRawText.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtRaw)).EndInit();
      this.tabPageJson.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.txtJson)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Button btnRun;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageRawText;
    private FastColoredTextBoxNS.FastColoredTextBox txtRaw;
    private System.Windows.Forms.TabPage tabPageJson;
    private FastColoredTextBoxNS.FastColoredTextBox txtJson;
		private System.Windows.Forms.Button btnDeserializeObject;
		private System.Windows.Forms.Button btnSerializeObject;
		private System.Windows.Forms.Button btnLoadAppConfig;
    private System.Windows.Forms.Button btnBuildCommandSet;
  }
}


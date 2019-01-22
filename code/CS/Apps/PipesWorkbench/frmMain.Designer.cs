namespace Org.PipesWorkbench
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
      this.pnlSupervisor = new System.Windows.Forms.Panel();
      this.lblExistingNamedPipes = new System.Windows.Forms.Label();
      this.lblExistingNamedPipesValue = new System.Windows.Forms.Label();
      this.btnSendMessage = new System.Windows.Forms.Button();
      this.btnCloseNamedPipeClient = new System.Windows.Forms.Button();
      this.btnGetExistingNamedPipes = new System.Windows.Forms.Button();
      this.btnCreateNamedPipeClient = new System.Windows.Forms.Button();
      this.btnCreateNamedPipeServer = new System.Windows.Forms.Button();
      this.txtServersReport = new System.Windows.Forms.TextBox();
      this.btnCreateNamedPipe = new System.Windows.Forms.Button();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPageNamePipeServers = new System.Windows.Forms.TabPage();
      this.tabPageNamedPipeClients = new System.Windows.Forms.TabPage();
      this.pnlServersTop = new System.Windows.Forms.Panel();
      this.pnlClientTop = new System.Windows.Forms.Panel();
      this.pnlToolBar = new System.Windows.Forms.Panel();
      this.txtClientsReport = new System.Windows.Forms.TextBox();
      this.cboNamedPipeServers = new System.Windows.Forms.ComboBox();
      this.lblNamedPipeServers = new System.Windows.Forms.Label();
      this.cboNamedPipes = new System.Windows.Forms.ComboBox();
      this.lblNamedPipes = new System.Windows.Forms.Label();
      this.btnListen = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.btnDeleteNamedPipeServer = new System.Windows.Forms.Button();
      this.mnuMain.SuspendLayout();
      this.pnlSupervisor.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPageNamePipeServers.SuspendLayout();
      this.tabPageNamedPipeClients.SuspendLayout();
      this.pnlServersTop.SuspendLayout();
      this.pnlClientTop.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(1237, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(0, 741);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1237, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlSupervisor
      //
      this.pnlSupervisor.Controls.Add(this.lblExistingNamedPipes);
      this.pnlSupervisor.Controls.Add(this.lblExistingNamedPipesValue);
      this.pnlSupervisor.Controls.Add(this.btnGetExistingNamedPipes);
      this.pnlSupervisor.Controls.Add(this.pnlToolBar);
      this.pnlSupervisor.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlSupervisor.Location = new System.Drawing.Point(0, 24);
      this.pnlSupervisor.Name = "pnlSupervisor";
      this.pnlSupervisor.Size = new System.Drawing.Size(1237, 94);
      this.pnlSupervisor.TabIndex = 2;
      //
      // lblExistingNamedPipes
      //
      this.lblExistingNamedPipes.AutoSize = true;
      this.lblExistingNamedPipes.Location = new System.Drawing.Point(204, 9);
      this.lblExistingNamedPipes.Name = "lblExistingNamedPipes";
      this.lblExistingNamedPipes.Size = new System.Drawing.Size(149, 13);
      this.lblExistingNamedPipes.TabIndex = 2;
      this.lblExistingNamedPipes.Text = "Existing Named Pipes (filtered)";
      //
      // lblExistingNamedPipesValue
      //
      this.lblExistingNamedPipesValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblExistingNamedPipesValue.Location = new System.Drawing.Point(204, 25);
      this.lblExistingNamedPipesValue.Name = "lblExistingNamedPipesValue";
      this.lblExistingNamedPipesValue.Size = new System.Drawing.Size(262, 33);
      this.lblExistingNamedPipesValue.TabIndex = 1;
      //
      // btnSendMessage
      //
      this.btnSendMessage.Location = new System.Drawing.Point(15, 47);
      this.btnSendMessage.Name = "btnSendMessage";
      this.btnSendMessage.Size = new System.Drawing.Size(160, 23);
      this.btnSendMessage.TabIndex = 0;
      this.btnSendMessage.Tag = "SendMessage";
      this.btnSendMessage.Text = "Send Message";
      this.btnSendMessage.UseVisualStyleBackColor = true;
      this.btnSendMessage.Click += new System.EventHandler(this.Action);
      //
      // btnCloseNamedPipeClient
      //
      this.btnCloseNamedPipeClient.Location = new System.Drawing.Point(15, 76);
      this.btnCloseNamedPipeClient.Name = "btnCloseNamedPipeClient";
      this.btnCloseNamedPipeClient.Size = new System.Drawing.Size(160, 23);
      this.btnCloseNamedPipeClient.TabIndex = 0;
      this.btnCloseNamedPipeClient.Tag = "CloseNamedPipeClient";
      this.btnCloseNamedPipeClient.Text = "Close Named Pipe Client";
      this.btnCloseNamedPipeClient.UseVisualStyleBackColor = true;
      this.btnCloseNamedPipeClient.Click += new System.EventHandler(this.Action);
      //
      // btnGetExistingNamedPipes
      //
      this.btnGetExistingNamedPipes.Location = new System.Drawing.Point(12, 20);
      this.btnGetExistingNamedPipes.Name = "btnGetExistingNamedPipes";
      this.btnGetExistingNamedPipes.Size = new System.Drawing.Size(160, 23);
      this.btnGetExistingNamedPipes.TabIndex = 0;
      this.btnGetExistingNamedPipes.Tag = "GetExistingNamedPipes";
      this.btnGetExistingNamedPipes.Text = "Get Existing Named Pipes";
      this.btnGetExistingNamedPipes.UseVisualStyleBackColor = true;
      this.btnGetExistingNamedPipes.Click += new System.EventHandler(this.Action);
      //
      // btnCreateNamedPipeClient
      //
      this.btnCreateNamedPipeClient.Location = new System.Drawing.Point(15, 18);
      this.btnCreateNamedPipeClient.Name = "btnCreateNamedPipeClient";
      this.btnCreateNamedPipeClient.Size = new System.Drawing.Size(160, 23);
      this.btnCreateNamedPipeClient.TabIndex = 0;
      this.btnCreateNamedPipeClient.Tag = "CreateNamedPipeClient";
      this.btnCreateNamedPipeClient.Text = "Create Named Pipe Client";
      this.btnCreateNamedPipeClient.UseVisualStyleBackColor = true;
      this.btnCreateNamedPipeClient.Click += new System.EventHandler(this.Action);
      //
      // btnCreateNamedPipeServer
      //
      this.btnCreateNamedPipeServer.Location = new System.Drawing.Point(255, 34);
      this.btnCreateNamedPipeServer.Name = "btnCreateNamedPipeServer";
      this.btnCreateNamedPipeServer.Size = new System.Drawing.Size(104, 23);
      this.btnCreateNamedPipeServer.TabIndex = 0;
      this.btnCreateNamedPipeServer.Tag = "CreateNamedPipeServer";
      this.btnCreateNamedPipeServer.Text = "Create Server";
      this.btnCreateNamedPipeServer.UseVisualStyleBackColor = true;
      this.btnCreateNamedPipeServer.Click += new System.EventHandler(this.Action);
      //
      // txtServersReport
      //
      this.txtServersReport.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtServersReport.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtServersReport.Location = new System.Drawing.Point(3, 148);
      this.txtServersReport.Multiline = true;
      this.txtServersReport.Name = "txtServersReport";
      this.txtServersReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtServersReport.Size = new System.Drawing.Size(1223, 446);
      this.txtServersReport.TabIndex = 1;
      this.txtServersReport.WordWrap = false;
      //
      // btnCreateNamedPipe
      //
      this.btnCreateNamedPipe.Location = new System.Drawing.Point(255, 79);
      this.btnCreateNamedPipe.Name = "btnCreateNamedPipe";
      this.btnCreateNamedPipe.Size = new System.Drawing.Size(104, 23);
      this.btnCreateNamedPipe.TabIndex = 0;
      this.btnCreateNamedPipe.Tag = "CreateNamedPipe";
      this.btnCreateNamedPipe.Text = "Create Pipe";
      this.btnCreateNamedPipe.UseVisualStyleBackColor = true;
      this.btnCreateNamedPipe.Click += new System.EventHandler(this.Action);
      //
      // tabMain
      //
      this.tabMain.Controls.Add(this.tabPageNamePipeServers);
      this.tabMain.Controls.Add(this.tabPageNamedPipeClients);
      this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabMain.ItemSize = new System.Drawing.Size(125, 18);
      this.tabMain.Location = new System.Drawing.Point(0, 118);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1237, 623);
      this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabMain.TabIndex = 4;
      //
      // tabPageNamePipeServers
      //
      this.tabPageNamePipeServers.Controls.Add(this.txtServersReport);
      this.tabPageNamePipeServers.Controls.Add(this.pnlServersTop);
      this.tabPageNamePipeServers.Location = new System.Drawing.Point(4, 22);
      this.tabPageNamePipeServers.Name = "tabPageNamePipeServers";
      this.tabPageNamePipeServers.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageNamePipeServers.Size = new System.Drawing.Size(1229, 597);
      this.tabPageNamePipeServers.TabIndex = 0;
      this.tabPageNamePipeServers.Text = "Named Pipe Servers";
      this.tabPageNamePipeServers.UseVisualStyleBackColor = true;
      //
      // tabPageNamedPipeClients
      //
      this.tabPageNamedPipeClients.Controls.Add(this.txtClientsReport);
      this.tabPageNamedPipeClients.Controls.Add(this.pnlClientTop);
      this.tabPageNamedPipeClients.Location = new System.Drawing.Point(4, 22);
      this.tabPageNamedPipeClients.Name = "tabPageNamedPipeClients";
      this.tabPageNamedPipeClients.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageNamedPipeClients.Size = new System.Drawing.Size(1229, 597);
      this.tabPageNamedPipeClients.TabIndex = 1;
      this.tabPageNamedPipeClients.Text = "Named Pipe Clients";
      this.tabPageNamedPipeClients.UseVisualStyleBackColor = true;
      //
      // pnlServersTop
      //
      this.pnlServersTop.BackColor = System.Drawing.SystemColors.Control;
      this.pnlServersTop.Controls.Add(this.lblNamedPipes);
      this.pnlServersTop.Controls.Add(this.lblNamedPipeServers);
      this.pnlServersTop.Controls.Add(this.cboNamedPipes);
      this.pnlServersTop.Controls.Add(this.cboNamedPipeServers);
      this.pnlServersTop.Controls.Add(this.btnDeleteNamedPipeServer);
      this.pnlServersTop.Controls.Add(this.btnCreateNamedPipeServer);
      this.pnlServersTop.Controls.Add(this.button1);
      this.pnlServersTop.Controls.Add(this.btnListen);
      this.pnlServersTop.Controls.Add(this.btnCreateNamedPipe);
      this.pnlServersTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlServersTop.Location = new System.Drawing.Point(3, 3);
      this.pnlServersTop.Name = "pnlServersTop";
      this.pnlServersTop.Size = new System.Drawing.Size(1223, 145);
      this.pnlServersTop.TabIndex = 0;
      //
      // pnlClientTop
      //
      this.pnlClientTop.BackColor = System.Drawing.SystemColors.Control;
      this.pnlClientTop.Controls.Add(this.btnCreateNamedPipeClient);
      this.pnlClientTop.Controls.Add(this.btnCloseNamedPipeClient);
      this.pnlClientTop.Controls.Add(this.btnSendMessage);
      this.pnlClientTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlClientTop.Location = new System.Drawing.Point(3, 3);
      this.pnlClientTop.Name = "pnlClientTop";
      this.pnlClientTop.Size = new System.Drawing.Size(1223, 145);
      this.pnlClientTop.TabIndex = 0;
      //
      // pnlToolBar
      //
      this.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlToolBar.Location = new System.Drawing.Point(0, 0);
      this.pnlToolBar.Name = "pnlToolBar";
      this.pnlToolBar.Size = new System.Drawing.Size(1237, 0);
      this.pnlToolBar.TabIndex = 3;
      this.pnlToolBar.Visible = false;
      //
      // txtClientsReport
      //
      this.txtClientsReport.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtClientsReport.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtClientsReport.Location = new System.Drawing.Point(3, 148);
      this.txtClientsReport.Multiline = true;
      this.txtClientsReport.Name = "txtClientsReport";
      this.txtClientsReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtClientsReport.Size = new System.Drawing.Size(1223, 446);
      this.txtClientsReport.TabIndex = 2;
      this.txtClientsReport.WordWrap = false;
      //
      // cboNamedPipeServers
      //
      this.cboNamedPipeServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboNamedPipeServers.FormattingEnabled = true;
      this.cboNamedPipeServers.Location = new System.Drawing.Point(16, 35);
      this.cboNamedPipeServers.Name = "cboNamedPipeServers";
      this.cboNamedPipeServers.Size = new System.Drawing.Size(233, 21);
      this.cboNamedPipeServers.TabIndex = 1;
      //
      // lblNamedPipeServers
      //
      this.lblNamedPipeServers.AutoSize = true;
      this.lblNamedPipeServers.Location = new System.Drawing.Point(13, 19);
      this.lblNamedPipeServers.Name = "lblNamedPipeServers";
      this.lblNamedPipeServers.Size = new System.Drawing.Size(104, 13);
      this.lblNamedPipeServers.TabIndex = 2;
      this.lblNamedPipeServers.Text = "Named Pipe Servers";
      //
      // cboNamedPipes
      //
      this.cboNamedPipes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboNamedPipes.FormattingEnabled = true;
      this.cboNamedPipes.Location = new System.Drawing.Point(16, 80);
      this.cboNamedPipes.Name = "cboNamedPipes";
      this.cboNamedPipes.Size = new System.Drawing.Size(233, 21);
      this.cboNamedPipes.TabIndex = 1;
      //
      // lblNamedPipes
      //
      this.lblNamedPipes.AutoSize = true;
      this.lblNamedPipes.Location = new System.Drawing.Point(13, 64);
      this.lblNamedPipes.Name = "lblNamedPipes";
      this.lblNamedPipes.Size = new System.Drawing.Size(70, 13);
      this.lblNamedPipes.TabIndex = 2;
      this.lblNamedPipes.Text = "Named Pipes";
      //
      // btnListen
      //
      this.btnListen.Location = new System.Drawing.Point(363, 79);
      this.btnListen.Name = "btnListen";
      this.btnListen.Size = new System.Drawing.Size(104, 23);
      this.btnListen.TabIndex = 0;
      this.btnListen.Tag = "StartListening";
      this.btnListen.Text = "Start Listening";
      this.btnListen.UseVisualStyleBackColor = true;
      this.btnListen.Click += new System.EventHandler(this.Action);
      //
      // button1
      //
      this.button1.Location = new System.Drawing.Point(470, 79);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(104, 23);
      this.button1.TabIndex = 0;
      this.button1.Tag = "StopListening";
      this.button1.Text = "Stop Listening";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.Action);
      //
      // btnDeleteNamedPipeServer
      //
      this.btnDeleteNamedPipeServer.Location = new System.Drawing.Point(363, 34);
      this.btnDeleteNamedPipeServer.Name = "btnDeleteNamedPipeServer";
      this.btnDeleteNamedPipeServer.Size = new System.Drawing.Size(104, 23);
      this.btnDeleteNamedPipeServer.TabIndex = 0;
      this.btnDeleteNamedPipeServer.Tag = "DeleteNamedPipeServer";
      this.btnDeleteNamedPipeServer.Text = "Delete Server";
      this.btnDeleteNamedPipeServer.UseVisualStyleBackColor = true;
      this.btnDeleteNamedPipeServer.Click += new System.EventHandler(this.Action);
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1237, 764);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.pnlSupervisor);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Pipes Workbench - v1.0.0.0";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlSupervisor.ResumeLayout(false);
      this.pnlSupervisor.PerformLayout();
      this.tabMain.ResumeLayout(false);
      this.tabPageNamePipeServers.ResumeLayout(false);
      this.tabPageNamePipeServers.PerformLayout();
      this.tabPageNamedPipeClients.ResumeLayout(false);
      this.tabPageNamedPipeClients.PerformLayout();
      this.pnlServersTop.ResumeLayout(false);
      this.pnlServersTop.PerformLayout();
      this.pnlClientTop.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlSupervisor;
    private System.Windows.Forms.Button btnCreateNamedPipeServer;
    private System.Windows.Forms.Button btnSendMessage;
    private System.Windows.Forms.Button btnCloseNamedPipeClient;
    private System.Windows.Forms.Button btnCreateNamedPipeClient;
    private System.Windows.Forms.Button btnGetExistingNamedPipes;
    private System.Windows.Forms.TextBox txtServersReport;
    private System.Windows.Forms.Label lblExistingNamedPipes;
    private System.Windows.Forms.Label lblExistingNamedPipesValue;
    private System.Windows.Forms.Button btnCreateNamedPipe;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPageNamePipeServers;
    private System.Windows.Forms.Panel pnlServersTop;
    private System.Windows.Forms.TabPage tabPageNamedPipeClients;
    private System.Windows.Forms.Panel pnlToolBar;
    private System.Windows.Forms.TextBox txtClientsReport;
    private System.Windows.Forms.Panel pnlClientTop;
    private System.Windows.Forms.Label lblNamedPipes;
    private System.Windows.Forms.Label lblNamedPipeServers;
    private System.Windows.Forms.ComboBox cboNamedPipes;
    private System.Windows.Forms.ComboBox cboNamedPipeServers;
    private System.Windows.Forms.Button btnDeleteNamedPipeServer;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button btnListen;
  }
}


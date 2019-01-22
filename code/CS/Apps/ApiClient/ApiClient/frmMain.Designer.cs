namespace ApiClient
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
      this.gbAuthorization = new System.Windows.Forms.GroupBox();
      this.lblGrantType = new System.Windows.Forms.Label();
      this.cboGrantType = new System.Windows.Forms.ComboBox();
      this.cboApiResource = new System.Windows.Forms.ComboBox();
      this.lblApiResource = new System.Windows.Forms.Label();
      this.gbApiInteraction = new System.Windows.Forms.GroupBox();
      this.lblApiRequest = new System.Windows.Forms.Label();
      this.cboApiRequest = new System.Windows.Forms.ComboBox();
      this.btnRunApiRequest = new System.Windows.Forms.Button();
      this.toolStripMain = new System.Windows.Forms.ToolStrip();
      this.tbConfigure = new System.Windows.Forms.ToolStripButton();
      this.tbtnDiscover = new System.Windows.Forms.ToolStripButton();
      this.tbtnLogin = new System.Windows.Forms.ToolStripButton();
      this.tbtnGetToken = new System.Windows.Forms.ToolStripButton();
      this.tbtnClear = new System.Windows.Forms.ToolStripButton();
      this.splitMain = new System.Windows.Forms.SplitContainer();
      this.tvMain = new System.Windows.Forms.TreeView();
      this.tvImageList = new System.Windows.Forms.ImageList(this.components);
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.cboClient = new System.Windows.Forms.ComboBox();
      this.lblClients = new System.Windows.Forms.Label();
      this.cboClientSecret = new System.Windows.Forms.ComboBox();
      this.lblClientSecret = new System.Windows.Forms.Label();
      this.pnlToolbar = new System.Windows.Forms.Panel();
      this.cboUser = new System.Windows.Forms.ComboBox();
      this.cboPassword = new System.Windows.Forms.ComboBox();
      this.lblUser = new System.Windows.Forms.Label();
      this.lblPassword = new System.Windows.Forms.Label();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.gbAuthorization.SuspendLayout();
      this.gbApiInteraction.SuspendLayout();
      this.toolStripMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
      this.splitMain.Panel1.SuspendLayout();
      this.splitMain.Panel2.SuspendLayout();
      this.splitMain.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.pnlToolbar.SuspendLayout();
      this.SuspendLayout();
      //
      // mnuMain
      //
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile
      });
      this.mnuMain.Location = new System.Drawing.Point(4, 0);
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
      this.mnuMain.Size = new System.Drawing.Size(1134, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(4, 771);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(1134, 20);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.gbAuthorization);
      this.pnlTop.Controls.Add(this.gbApiInteraction);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(4, 81);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1134, 207);
      this.pnlTop.TabIndex = 7;
      //
      // gbAuthorization
      //
      this.gbAuthorization.Controls.Add(this.lblPassword);
      this.gbAuthorization.Controls.Add(this.lblClientSecret);
      this.gbAuthorization.Controls.Add(this.lblUser);
      this.gbAuthorization.Controls.Add(this.lblClients);
      this.gbAuthorization.Controls.Add(this.cboPassword);
      this.gbAuthorization.Controls.Add(this.cboClientSecret);
      this.gbAuthorization.Controls.Add(this.cboUser);
      this.gbAuthorization.Controls.Add(this.cboClient);
      this.gbAuthorization.Controls.Add(this.lblGrantType);
      this.gbAuthorization.Controls.Add(this.cboGrantType);
      this.gbAuthorization.Controls.Add(this.cboApiResource);
      this.gbAuthorization.Controls.Add(this.lblApiResource);
      this.gbAuthorization.Location = new System.Drawing.Point(10, 6);
      this.gbAuthorization.Name = "gbAuthorization";
      this.gbAuthorization.Size = new System.Drawing.Size(490, 190);
      this.gbAuthorization.TabIndex = 6;
      this.gbAuthorization.TabStop = false;
      this.gbAuthorization.Text = "Authorization Configurations";
      //
      // lblGrantType
      //
      this.lblGrantType.AutoSize = true;
      this.lblGrantType.Location = new System.Drawing.Point(24, 29);
      this.lblGrantType.Name = "lblGrantType";
      this.lblGrantType.Size = new System.Drawing.Size(93, 13);
      this.lblGrantType.TabIndex = 3;
      this.lblGrantType.Text = "Select Grant Type";
      //
      // cboGrantType
      //
      this.cboGrantType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboGrantType.FormattingEnabled = true;
      this.cboGrantType.Location = new System.Drawing.Point(24, 44);
      this.cboGrantType.Name = "cboGrantType";
      this.cboGrantType.Size = new System.Drawing.Size(147, 21);
      this.cboGrantType.TabIndex = 2;
      this.cboGrantType.SelectedIndexChanged += new System.EventHandler(this.cboGrantType_SelectedIndexChanged);
      //
      // cboApiResource
      //
      this.cboApiResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboApiResource.FormattingEnabled = true;
      this.cboApiResource.Items.AddRange(new object[] {
        "HTTP",
        "HTTPS"
      });
      this.cboApiResource.Location = new System.Drawing.Point(183, 44);
      this.cboApiResource.Name = "cboApiResource";
      this.cboApiResource.Size = new System.Drawing.Size(145, 21);
      this.cboApiResource.TabIndex = 2;
      this.cboApiResource.SelectedIndexChanged += new System.EventHandler(this.cboApiRequest_SelectedIndexChanged);
      //
      // lblApiResource
      //
      this.lblApiResource.AutoSize = true;
      this.lblApiResource.Location = new System.Drawing.Point(183, 29);
      this.lblApiResource.Name = "lblApiResource";
      this.lblApiResource.Size = new System.Drawing.Size(106, 13);
      this.lblApiResource.TabIndex = 3;
      this.lblApiResource.Text = "Select API Resource";
      //
      // gbApiInteraction
      //
      this.gbApiInteraction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                      | System.Windows.Forms.AnchorStyles.Right)));
      this.gbApiInteraction.Controls.Add(this.lblApiRequest);
      this.gbApiInteraction.Controls.Add(this.cboApiRequest);
      this.gbApiInteraction.Controls.Add(this.btnRunApiRequest);
      this.gbApiInteraction.Location = new System.Drawing.Point(511, 6);
      this.gbApiInteraction.Name = "gbApiInteraction";
      this.gbApiInteraction.Size = new System.Drawing.Size(615, 190);
      this.gbApiInteraction.TabIndex = 5;
      this.gbApiInteraction.TabStop = false;
      this.gbApiInteraction.Text = "API Interaction";
      //
      // lblApiRequest
      //
      this.lblApiRequest.AutoSize = true;
      this.lblApiRequest.Location = new System.Drawing.Point(24, 25);
      this.lblApiRequest.Name = "lblApiRequest";
      this.lblApiRequest.Size = new System.Drawing.Size(100, 13);
      this.lblApiRequest.TabIndex = 3;
      this.lblApiRequest.Text = "Select API Request";
      //
      // cboApiRequest
      //
      this.cboApiRequest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboApiRequest.FormattingEnabled = true;
      this.cboApiRequest.Location = new System.Drawing.Point(24, 44);
      this.cboApiRequest.Name = "cboApiRequest";
      this.cboApiRequest.Size = new System.Drawing.Size(307, 21);
      this.cboApiRequest.TabIndex = 2;
      this.cboApiRequest.SelectedIndexChanged += new System.EventHandler(this.cboApiRequest_SelectedIndexChanged);
      //
      // btnRunApiRequest
      //
      this.btnRunApiRequest.Location = new System.Drawing.Point(337, 43);
      this.btnRunApiRequest.Name = "btnRunApiRequest";
      this.btnRunApiRequest.Size = new System.Drawing.Size(115, 23);
      this.btnRunApiRequest.TabIndex = 0;
      this.btnRunApiRequest.Tag = "RunApiRequest";
      this.btnRunApiRequest.Text = "Run API Request";
      this.btnRunApiRequest.UseVisualStyleBackColor = true;
      this.btnRunApiRequest.Click += new System.EventHandler(this.Action);
      //
      // toolStripMain
      //
      this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.tbConfigure,
        this.tbtnDiscover,
        this.tbtnLogin,
        this.tbtnGetToken,
        this.tbtnClear
      });
      this.toolStripMain.Location = new System.Drawing.Point(0, 0);
      this.toolStripMain.Name = "toolStripMain";
      this.toolStripMain.Size = new System.Drawing.Size(1134, 57);
      this.toolStripMain.TabIndex = 1;
      this.toolStripMain.Text = "toolStrip1";
      //
      // tbConfigure
      //
      this.tbConfigure.AutoSize = false;
      this.tbConfigure.Image = ((System.Drawing.Image)(resources.GetObject("tbConfigure.Image")));
      this.tbConfigure.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
      this.tbConfigure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbConfigure.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbConfigure.Name = "tbConfigure";
      this.tbConfigure.Size = new System.Drawing.Size(64, 54);
      this.tbConfigure.Tag = "Configure";
      this.tbConfigure.Text = "Configure";
      this.tbConfigure.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.tbConfigure.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.tbConfigure.Click += new System.EventHandler(this.Action);
      //
      // tbtnDiscover
      //
      this.tbtnDiscover.AutoSize = false;
      this.tbtnDiscover.Image = ((System.Drawing.Image)(resources.GetObject("tbtnDiscover.Image")));
      this.tbtnDiscover.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
      this.tbtnDiscover.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnDiscover.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnDiscover.Name = "tbtnDiscover";
      this.tbtnDiscover.Size = new System.Drawing.Size(64, 54);
      this.tbtnDiscover.Tag = "Discover";
      this.tbtnDiscover.Text = "Discover";
      this.tbtnDiscover.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.tbtnDiscover.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.tbtnDiscover.Click += new System.EventHandler(this.Action);
      //
      // tbtnLogin
      //
      this.tbtnLogin.AutoSize = false;
      this.tbtnLogin.Image = global::ApiClient.Properties.Resources.login_big;
      this.tbtnLogin.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
      this.tbtnLogin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnLogin.Name = "tbtnLogin";
      this.tbtnLogin.Size = new System.Drawing.Size(64, 54);
      this.tbtnLogin.Tag = "Login";
      this.tbtnLogin.Text = "Login";
      this.tbtnLogin.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.tbtnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.tbtnLogin.Click += new System.EventHandler(this.Action);
      //
      // tbtnGetToken
      //
      this.tbtnGetToken.AutoSize = false;
      this.tbtnGetToken.Image = global::ApiClient.Properties.Resources.auth_token_big;
      this.tbtnGetToken.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
      this.tbtnGetToken.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnGetToken.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnGetToken.Name = "tbtnGetToken";
      this.tbtnGetToken.Size = new System.Drawing.Size(64, 54);
      this.tbtnGetToken.Tag = "GetToken";
      this.tbtnGetToken.Text = "Get Token";
      this.tbtnGetToken.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.tbtnGetToken.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.tbtnGetToken.Click += new System.EventHandler(this.Action);
      //
      // tbtnClear
      //
      this.tbtnClear.AutoSize = false;
      this.tbtnClear.Image = global::ApiClient.Properties.Resources.clear_big;
      this.tbtnClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
      this.tbtnClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.tbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbtnClear.Name = "tbtnClear";
      this.tbtnClear.Size = new System.Drawing.Size(64, 54);
      this.tbtnClear.Tag = "Clear";
      this.tbtnClear.Text = "Clear";
      this.tbtnClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      this.tbtnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.tbtnClear.Click += new System.EventHandler(this.Action);
      //
      // splitMain
      //
      this.splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitMain.Location = new System.Drawing.Point(4, 288);
      this.splitMain.Name = "splitMain";
      //
      // splitMain.Panel1
      //
      this.splitMain.Panel1.Controls.Add(this.tvMain);
      //
      // splitMain.Panel2
      //
      this.splitMain.Panel2.Controls.Add(this.tabMain);
      this.splitMain.Size = new System.Drawing.Size(1134, 483);
      this.splitMain.SplitterDistance = 212;
      this.splitMain.SplitterWidth = 3;
      this.splitMain.TabIndex = 3;
      //
      // tvMain
      //
      this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMain.ImageIndex = 0;
      this.tvMain.ImageList = this.tvImageList;
      this.tvMain.Location = new System.Drawing.Point(0, 0);
      this.tvMain.Name = "tvMain";
      this.tvMain.SelectedImageIndex = 0;
      this.tvMain.Size = new System.Drawing.Size(210, 481);
      this.tvMain.TabIndex = 0;
      //
      // tvImageList
      //
      this.tvImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.tvImageList.ImageSize = new System.Drawing.Size(16, 16);
      this.tvImageList.TransparentColor = System.Drawing.Color.Transparent;
      //
      // tabMain
      //
      this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                             | System.Windows.Forms.AnchorStyles.Left)
                             | System.Windows.Forms.AnchorStyles.Right)));
      this.tabMain.Controls.Add(this.tabPage1);
      this.tabMain.Controls.Add(this.tabPage2);
      this.tabMain.ItemSize = new System.Drawing.Size(1, 1);
      this.tabMain.Location = new System.Drawing.Point(-4, -4);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(930, 489);
      this.tabMain.TabIndex = 4;
      //
      // tabPage1
      //
      this.tabPage1.Controls.Add(this.txtOut);
      this.tabPage1.Location = new System.Drawing.Point(4, 5);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(922, 480);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.UseVisualStyleBackColor = true;
      //
      // txtOut
      //
      this.txtOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(3, 3);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(916, 474);
      this.txtOut.TabIndex = 1;
      this.txtOut.WordWrap = false;
      //
      // tabPage2
      //
      this.tabPage2.Location = new System.Drawing.Point(4, 5);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(922, 524);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.UseVisualStyleBackColor = true;
      //
      // cboClient
      //
      this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboClient.FormattingEnabled = true;
      this.cboClient.Location = new System.Drawing.Point(24, 91);
      this.cboClient.Name = "cboClient";
      this.cboClient.Size = new System.Drawing.Size(147, 21);
      this.cboClient.TabIndex = 2;
      this.cboClient.SelectedIndexChanged += new System.EventHandler(this.cboClients_SelectedIndexChanged);
      //
      // lblClients
      //
      this.lblClients.AutoSize = true;
      this.lblClients.Location = new System.Drawing.Point(24, 76);
      this.lblClients.Name = "lblClients";
      this.lblClients.Size = new System.Drawing.Size(66, 13);
      this.lblClients.TabIndex = 3;
      this.lblClients.Text = "Select Client";
      //
      // cboClientSecret
      //
      this.cboClientSecret.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboClientSecret.FormattingEnabled = true;
      this.cboClientSecret.Location = new System.Drawing.Point(181, 91);
      this.cboClientSecret.Name = "cboClientSecret";
      this.cboClientSecret.Size = new System.Drawing.Size(147, 21);
      this.cboClientSecret.TabIndex = 2;
      //
      // lblClientSecret
      //
      this.lblClientSecret.AutoSize = true;
      this.lblClientSecret.Location = new System.Drawing.Point(181, 76);
      this.lblClientSecret.Name = "lblClientSecret";
      this.lblClientSecret.Size = new System.Drawing.Size(100, 13);
      this.lblClientSecret.TabIndex = 3;
      this.lblClientSecret.Text = "Select Client Secret";
      //
      // pnlToolbar
      //
      this.pnlToolbar.AutoSize = true;
      this.pnlToolbar.Controls.Add(this.toolStripMain);
      this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlToolbar.Location = new System.Drawing.Point(4, 24);
      this.pnlToolbar.Name = "pnlToolbar";
      this.pnlToolbar.Size = new System.Drawing.Size(1134, 57);
      this.pnlToolbar.TabIndex = 8;
      //
      // cboUser
      //
      this.cboUser.FormattingEnabled = true;
      this.cboUser.Location = new System.Drawing.Point(24, 140);
      this.cboUser.Name = "cboUser";
      this.cboUser.Size = new System.Drawing.Size(147, 21);
      this.cboUser.TabIndex = 2;
      this.cboUser.SelectedIndexChanged += new System.EventHandler(this.cboUser_SelectedIndexChanged);
      //
      // cboPassword
      //
      this.cboPassword.FormattingEnabled = true;
      this.cboPassword.Location = new System.Drawing.Point(181, 140);
      this.cboPassword.Name = "cboPassword";
      this.cboPassword.Size = new System.Drawing.Size(147, 21);
      this.cboPassword.TabIndex = 2;
      //
      // lblUser
      //
      this.lblUser.AutoSize = true;
      this.lblUser.Location = new System.Drawing.Point(24, 125);
      this.lblUser.Name = "lblUser";
      this.lblUser.Size = new System.Drawing.Size(62, 13);
      this.lblUser.TabIndex = 3;
      this.lblUser.Text = "Select User";
      //
      // lblPassword
      //
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(181, 125);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(86, 13);
      this.lblPassword.TabIndex = 3;
      this.lblPassword.Text = "Select Password";
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1142, 791);
      this.Controls.Add(this.splitMain);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.pnlToolbar);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "API Client";
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.gbAuthorization.ResumeLayout(false);
      this.gbAuthorization.PerformLayout();
      this.gbApiInteraction.ResumeLayout(false);
      this.gbApiInteraction.PerformLayout();
      this.toolStripMain.ResumeLayout(false);
      this.toolStripMain.PerformLayout();
      this.splitMain.Panel1.ResumeLayout(false);
      this.splitMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
      this.splitMain.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.pnlToolbar.ResumeLayout(false);
      this.pnlToolbar.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.SplitContainer splitMain;
    private System.Windows.Forms.Button btnRunApiRequest;
    private System.Windows.Forms.Label lblApiRequest;
    private System.Windows.Forms.ComboBox cboApiRequest;
    private System.Windows.Forms.Label lblGrantType;
    private System.Windows.Forms.ComboBox cboGrantType;
    private System.Windows.Forms.Label lblApiResource;
    private System.Windows.Forms.ComboBox cboApiResource;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.GroupBox gbAuthorization;
    private System.Windows.Forms.GroupBox gbApiInteraction;
    private System.Windows.Forms.TreeView tvMain;
    private System.Windows.Forms.ImageList tvImageList;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.ToolStrip toolStripMain;
    private System.Windows.Forms.ToolStripButton tbConfigure;
    private System.Windows.Forms.ToolStripButton tbtnDiscover;
    private System.Windows.Forms.ToolStripButton tbtnLogin;
    private System.Windows.Forms.ToolStripButton tbtnGetToken;
    private System.Windows.Forms.ToolStripButton tbtnClear;
    private System.Windows.Forms.Label lblClients;
    private System.Windows.Forms.ComboBox cboClient;
    private System.Windows.Forms.Label lblClientSecret;
    private System.Windows.Forms.ComboBox cboClientSecret;
    private System.Windows.Forms.Panel pnlToolbar;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Label lblUser;
    private System.Windows.Forms.ComboBox cboPassword;
    private System.Windows.Forms.ComboBox cboUser;
  }
}


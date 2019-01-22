namespace Org.WSC
{
  partial class frmWebServiceClient
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWebServiceClient));
      this.pnlTop = new System.Windows.Forms.Panel();
      this.ckClearDisplay = new System.Windows.Forms.CheckBox();
      this.cboWebServiceHost = new System.Windows.Forms.ComboBox();
      this.lblWebService = new System.Windows.Forms.Label();
      this.btnPingPort = new System.Windows.Forms.Button();
      this.txtPort = new System.Windows.Forms.TextBox();
      this.lblPort = new System.Windows.Forms.Label();
      this.lblSelectTransaction = new System.Windows.Forms.Label();
      this.lblWebServiceHost = new System.Windows.Forms.Label();
      this.lblParameters = new System.Windows.Forms.Label();
      this.cboWebService = new System.Windows.Forms.ComboBox();
      this.cboTransaction = new System.Windows.Forms.ComboBox();
      this.txtParms1 = new System.Windows.Forms.TextBox();
      this.btnSendMessage = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.txtOut = new System.Windows.Forms.TextBox();
      this.btnClose = new System.Windows.Forms.Button();
      this.pnlTop.SuspendLayout();
      this.SuspendLayout();
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.ckClearDisplay);
      this.pnlTop.Controls.Add(this.cboWebServiceHost);
      this.pnlTop.Controls.Add(this.lblWebService);
      this.pnlTop.Controls.Add(this.btnPingPort);
      this.pnlTop.Controls.Add(this.txtPort);
      this.pnlTop.Controls.Add(this.lblPort);
      this.pnlTop.Controls.Add(this.lblSelectTransaction);
      this.pnlTop.Controls.Add(this.lblWebServiceHost);
      this.pnlTop.Controls.Add(this.lblParameters);
      this.pnlTop.Controls.Add(this.cboWebService);
      this.pnlTop.Controls.Add(this.cboTransaction);
      this.pnlTop.Controls.Add(this.txtParms1);
      this.pnlTop.Controls.Add(this.btnClose);
      this.pnlTop.Controls.Add(this.btnSendMessage);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(997, 138);
      this.pnlTop.TabIndex = 0;
      //
      // ckClearDisplay
      //
      this.ckClearDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ckClearDisplay.AutoSize = true;
      this.ckClearDisplay.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ckClearDisplay.Location = new System.Drawing.Point(839, 8);
      this.ckClearDisplay.Name = "ckClearDisplay";
      this.ckClearDisplay.Size = new System.Drawing.Size(146, 17);
      this.ckClearDisplay.TabIndex = 23;
      this.ckClearDisplay.Text = "Clear display on each call";
      this.ckClearDisplay.UseVisualStyleBackColor = true;
      //
      // cboWebServiceHost
      //
      this.cboWebServiceHost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboWebServiceHost.FormattingEnabled = true;
      this.cboWebServiceHost.Location = new System.Drawing.Point(15, 30);
      this.cboWebServiceHost.Name = "cboWebServiceHost";
      this.cboWebServiceHost.Size = new System.Drawing.Size(155, 21);
      this.cboWebServiceHost.TabIndex = 16;
      //
      // lblWebService
      //
      this.lblWebService.AutoSize = true;
      this.lblWebService.Location = new System.Drawing.Point(174, 13);
      this.lblWebService.Name = "lblWebService";
      this.lblWebService.Size = new System.Drawing.Size(102, 13);
      this.lblWebService.TabIndex = 11;
      this.lblWebService.Text = "Select Web Service";
      //
      // btnPingPort
      //
      this.btnPingPort.Location = new System.Drawing.Point(661, 58);
      this.btnPingPort.Name = "btnPingPort";
      this.btnPingPort.Size = new System.Drawing.Size(121, 23);
      this.btnPingPort.TabIndex = 22;
      this.btnPingPort.Tag = "PingPort";
      this.btnPingPort.Text = "Ping Port";
      this.btnPingPort.UseVisualStyleBackColor = true;
      this.btnPingPort.Click += new System.EventHandler(this.Action);
      //
      // txtPort
      //
      this.txtPort.Location = new System.Drawing.Point(569, 30);
      this.txtPort.Name = "txtPort";
      this.txtPort.Size = new System.Drawing.Size(86, 20);
      this.txtPort.TabIndex = 21;
      //
      // lblPort
      //
      this.lblPort.AutoSize = true;
      this.lblPort.Location = new System.Drawing.Point(567, 13);
      this.lblPort.Name = "lblPort";
      this.lblPort.Size = new System.Drawing.Size(26, 13);
      this.lblPort.TabIndex = 12;
      this.lblPort.Text = "Port";
      //
      // lblSelectTransaction
      //
      this.lblSelectTransaction.AutoSize = true;
      this.lblSelectTransaction.Location = new System.Drawing.Point(335, 13);
      this.lblSelectTransaction.Name = "lblSelectTransaction";
      this.lblSelectTransaction.Size = new System.Drawing.Size(96, 13);
      this.lblSelectTransaction.TabIndex = 13;
      this.lblSelectTransaction.Text = "Select Transaction";
      //
      // lblWebServiceHost
      //
      this.lblWebServiceHost.AutoSize = true;
      this.lblWebServiceHost.Location = new System.Drawing.Point(12, 13);
      this.lblWebServiceHost.Name = "lblWebServiceHost";
      this.lblWebServiceHost.Size = new System.Drawing.Size(62, 13);
      this.lblWebServiceHost.TabIndex = 14;
      this.lblWebServiceHost.Text = "Select Host";
      //
      // lblParameters
      //
      this.lblParameters.AutoSize = true;
      this.lblParameters.Location = new System.Drawing.Point(14, 59);
      this.lblParameters.Name = "lblParameters";
      this.lblParameters.Size = new System.Drawing.Size(60, 13);
      this.lblParameters.TabIndex = 15;
      this.lblParameters.Text = "Parameters";
      //
      // cboWebService
      //
      this.cboWebService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboWebService.FormattingEnabled = true;
      this.cboWebService.Location = new System.Drawing.Point(177, 30);
      this.cboWebService.Name = "cboWebService";
      this.cboWebService.Size = new System.Drawing.Size(155, 21);
      this.cboWebService.TabIndex = 17;
      //
      // cboTransaction
      //
      this.cboTransaction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboTransaction.FormattingEnabled = true;
      this.cboTransaction.Location = new System.Drawing.Point(342, 30);
      this.cboTransaction.Name = "cboTransaction";
      this.cboTransaction.Size = new System.Drawing.Size(214, 21);
      this.cboTransaction.TabIndex = 18;
      //
      // txtParms1
      //
      this.txtParms1.Location = new System.Drawing.Point(15, 75);
      this.txtParms1.Multiline = true;
      this.txtParms1.Name = "txtParms1";
      this.txtParms1.Size = new System.Drawing.Size(541, 50);
      this.txtParms1.TabIndex = 19;
      //
      // btnSendMessage
      //
      this.btnSendMessage.Location = new System.Drawing.Point(661, 29);
      this.btnSendMessage.Name = "btnSendMessage";
      this.btnSendMessage.Size = new System.Drawing.Size(121, 23);
      this.btnSendMessage.TabIndex = 20;
      this.btnSendMessage.Tag = "SendMessage";
      this.btnSendMessage.Text = "Send Message";
      this.btnSendMessage.UseVisualStyleBackColor = true;
      this.btnSendMessage.Click += new System.EventHandler(this.Action);
      //
      // lblStatus
      //
      this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblStatus.Location = new System.Drawing.Point(0, 679);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(997, 23);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // txtOut
      //
      this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtOut.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtOut.Location = new System.Drawing.Point(0, 138);
      this.txtOut.Multiline = true;
      this.txtOut.Name = "txtOut";
      this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtOut.Size = new System.Drawing.Size(997, 541);
      this.txtOut.TabIndex = 2;
      this.txtOut.WordWrap = false;
      //
      // btnClose
      //
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(864, 98);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(121, 27);
      this.btnClose.TabIndex = 20;
      this.btnClose.Tag = "Close";
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.Action);
      //
      // frmWebServiceClient
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(997, 702);
      this.Controls.Add(this.txtOut);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.pnlTop);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmWebServiceClient";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Web Service Client";
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.CheckBox ckClearDisplay;
    private System.Windows.Forms.ComboBox cboWebServiceHost;
    private System.Windows.Forms.Label lblWebService;
    private System.Windows.Forms.Button btnPingPort;
    private System.Windows.Forms.TextBox txtPort;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.Label lblSelectTransaction;
    private System.Windows.Forms.Label lblWebServiceHost;
    private System.Windows.Forms.Label lblParameters;
    private System.Windows.Forms.ComboBox cboWebService;
    private System.Windows.Forms.ComboBox cboTransaction;
    private System.Windows.Forms.TextBox txtParms1;
    private System.Windows.Forms.Button btnSendMessage;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TextBox txtOut;
    private System.Windows.Forms.Button btnClose;
  }
}
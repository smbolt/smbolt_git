namespace Org.PdfExplorer
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
      this.ckOnlyImages = new System.Windows.Forms.CheckBox();
      this.ckOmitAnnotations = new System.Windows.Forms.CheckBox();
      this.btnOpenDocument = new System.Windows.Forms.Button();
      this.btnSwitchView = new System.Windows.Forms.Button();
      this.splitterMain = new System.Windows.Forms.SplitContainer();
      this.tvMain = new System.Windows.Forms.TreeView();
      this.ctxMenuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.ctxMenuTreeViewExpandAll = new System.Windows.Forms.ToolStripMenuItem();
      this.ctxMenuTreeViewCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
      this.pnlTvBottom = new System.Windows.Forms.Panel();
      this.pnlTvTop = new System.Windows.Forms.Panel();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tabPagePdfImage = new System.Windows.Forms.TabPage();
      this.tabPagePdfStructure = new System.Windows.Forms.TabPage();
      this.txtPdfStructure = new System.Windows.Forms.TextBox();
      this.pnlBackground = new System.Windows.Forms.Panel();
      this.txtObjectNumberBreak = new System.Windows.Forms.TextBox();
      this.lblDebugBreak = new System.Windows.Forms.Label();
      this.pnlPdfImageShadow = new System.Windows.Forms.Panel();
      this.pbPdfImage = new System.Windows.Forms.PictureBox();
      this.mnuMain.SuspendLayout();
      this.pnlTop.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).BeginInit();
      this.splitterMain.Panel1.SuspendLayout();
      this.splitterMain.Panel2.SuspendLayout();
      this.splitterMain.SuspendLayout();
      this.ctxMenuTreeView.SuspendLayout();
      this.tabMain.SuspendLayout();
      this.tabPagePdfImage.SuspendLayout();
      this.tabPagePdfStructure.SuspendLayout();
      this.pnlBackground.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbPdfImage)).BeginInit();
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
      this.mnuMain.Size = new System.Drawing.Size(1324, 24);
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
      this.lblStatus.Location = new System.Drawing.Point(0, 777);
      this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
      this.lblStatus.Size = new System.Drawing.Size(1324, 15);
      this.lblStatus.TabIndex = 1;
      this.lblStatus.Text = "Status";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      //
      // pnlTop
      //
      this.pnlTop.Controls.Add(this.txtObjectNumberBreak);
      this.pnlTop.Controls.Add(this.lblDebugBreak);
      this.pnlTop.Controls.Add(this.ckOnlyImages);
      this.pnlTop.Controls.Add(this.ckOmitAnnotations);
      this.pnlTop.Controls.Add(this.btnOpenDocument);
      this.pnlTop.Controls.Add(this.btnSwitchView);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 24);
      this.pnlTop.Margin = new System.Windows.Forms.Padding(2);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(1324, 37);
      this.pnlTop.TabIndex = 2;
      //
      // ckOnlyImages
      //
      this.ckOnlyImages.AutoSize = true;
      this.ckOnlyImages.Location = new System.Drawing.Point(441, 9);
      this.ckOnlyImages.Name = "ckOnlyImages";
      this.ckOnlyImages.Size = new System.Drawing.Size(84, 17);
      this.ckOnlyImages.TabIndex = 1;
      this.ckOnlyImages.Tag = "FilterObjects";
      this.ckOnlyImages.Text = "Only Images";
      this.ckOnlyImages.UseVisualStyleBackColor = true;
      this.ckOnlyImages.CheckedChanged += new System.EventHandler(this.Action);
      //
      // ckOmitAnnotations
      //
      this.ckOmitAnnotations.AutoSize = true;
      this.ckOmitAnnotations.Location = new System.Drawing.Point(332, 9);
      this.ckOmitAnnotations.Name = "ckOmitAnnotations";
      this.ckOmitAnnotations.Size = new System.Drawing.Size(103, 17);
      this.ckOmitAnnotations.TabIndex = 1;
      this.ckOmitAnnotations.Tag = "FilterObjects";
      this.ckOmitAnnotations.Text = "OmitAnnotations";
      this.ckOmitAnnotations.UseVisualStyleBackColor = true;
      this.ckOmitAnnotations.CheckedChanged += new System.EventHandler(this.Action);
      //
      // btnOpenDocument
      //
      this.btnOpenDocument.Location = new System.Drawing.Point(8, 5);
      this.btnOpenDocument.Margin = new System.Windows.Forms.Padding(2);
      this.btnOpenDocument.Name = "btnOpenDocument";
      this.btnOpenDocument.Size = new System.Drawing.Size(145, 24);
      this.btnOpenDocument.TabIndex = 0;
      this.btnOpenDocument.Tag = "OpenDocument";
      this.btnOpenDocument.Text = "Open Document";
      this.btnOpenDocument.UseVisualStyleBackColor = true;
      this.btnOpenDocument.Click += new System.EventHandler(this.Action);
      //
      // btnSwitchView
      //
      this.btnSwitchView.Location = new System.Drawing.Point(234, 5);
      this.btnSwitchView.Margin = new System.Windows.Forms.Padding(2);
      this.btnSwitchView.Name = "btnSwitchView";
      this.btnSwitchView.Size = new System.Drawing.Size(75, 24);
      this.btnSwitchView.TabIndex = 0;
      this.btnSwitchView.Tag = "SwitchView";
      this.btnSwitchView.Text = "Switch View";
      this.btnSwitchView.UseVisualStyleBackColor = true;
      this.btnSwitchView.Click += new System.EventHandler(this.Action);
      //
      // splitterMain
      //
      this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitterMain.Location = new System.Drawing.Point(1, 0);
      this.splitterMain.Margin = new System.Windows.Forms.Padding(2);
      this.splitterMain.Name = "splitterMain";
      //
      // splitterMain.Panel1
      //
      this.splitterMain.Panel1.Controls.Add(this.tvMain);
      this.splitterMain.Panel1.Controls.Add(this.pnlTvBottom);
      this.splitterMain.Panel1.Controls.Add(this.pnlTvTop);
      //
      // splitterMain.Panel2
      //
      this.splitterMain.Panel2.Controls.Add(this.tabMain);
      this.splitterMain.Size = new System.Drawing.Size(1322, 716);
      this.splitterMain.SplitterDistance = 400;
      this.splitterMain.SplitterWidth = 2;
      this.splitterMain.TabIndex = 3;
      //
      // tvMain
      //
      this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tvMain.ContextMenuStrip = this.ctxMenuTreeView;
      this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvMain.Location = new System.Drawing.Point(0, 30);
      this.tvMain.Margin = new System.Windows.Forms.Padding(2);
      this.tvMain.Name = "tvMain";
      this.tvMain.Size = new System.Drawing.Size(398, 654);
      this.tvMain.TabIndex = 2;
      this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
      //
      // ctxMenuTreeView
      //
      this.ctxMenuTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.ctxMenuTreeViewExpandAll,
        this.ctxMenuTreeViewCollapseAll
      });
      this.ctxMenuTreeView.Name = "ctxMenuTreeView";
      this.ctxMenuTreeView.Size = new System.Drawing.Size(137, 48);
      //
      // ctxMenuTreeViewExpandAll
      //
      this.ctxMenuTreeViewExpandAll.Name = "ctxMenuTreeViewExpandAll";
      this.ctxMenuTreeViewExpandAll.Size = new System.Drawing.Size(136, 22);
      this.ctxMenuTreeViewExpandAll.Tag = "ExpandAll";
      this.ctxMenuTreeViewExpandAll.Text = "Expand All";
      this.ctxMenuTreeViewExpandAll.Click += new System.EventHandler(this.Action);
      //
      // ctxMenuTreeViewCollapseAll
      //
      this.ctxMenuTreeViewCollapseAll.Name = "ctxMenuTreeViewCollapseAll";
      this.ctxMenuTreeViewCollapseAll.Size = new System.Drawing.Size(136, 22);
      this.ctxMenuTreeViewCollapseAll.Tag = "CollapseAll";
      this.ctxMenuTreeViewCollapseAll.Text = "Collapse All";
      this.ctxMenuTreeViewCollapseAll.Click += new System.EventHandler(this.Action);
      //
      // pnlTvBottom
      //
      this.pnlTvBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlTvBottom.Location = new System.Drawing.Point(0, 684);
      this.pnlTvBottom.Margin = new System.Windows.Forms.Padding(2);
      this.pnlTvBottom.Name = "pnlTvBottom";
      this.pnlTvBottom.Size = new System.Drawing.Size(398, 30);
      this.pnlTvBottom.TabIndex = 1;
      //
      // pnlTvTop
      //
      this.pnlTvTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTvTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTvTop.Margin = new System.Windows.Forms.Padding(2);
      this.pnlTvTop.Name = "pnlTvTop";
      this.pnlTvTop.Size = new System.Drawing.Size(398, 30);
      this.pnlTvTop.TabIndex = 0;
      //
      // tabMain
      //
      this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                             | System.Windows.Forms.AnchorStyles.Left)
                             | System.Windows.Forms.AnchorStyles.Right)));
      this.tabMain.Controls.Add(this.tabPagePdfImage);
      this.tabMain.Controls.Add(this.tabPagePdfStructure);
      this.tabMain.ItemSize = new System.Drawing.Size(1, 1);
      this.tabMain.Location = new System.Drawing.Point(-5, -5);
      this.tabMain.Margin = new System.Windows.Forms.Padding(2);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(924, 714);
      this.tabMain.TabIndex = 2;
      //
      // tabPagePdfImage
      //
      this.tabPagePdfImage.AutoScroll = true;
      this.tabPagePdfImage.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.tabPagePdfImage.Controls.Add(this.pbPdfImage);
      this.tabPagePdfImage.Controls.Add(this.pnlPdfImageShadow);
      this.tabPagePdfImage.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tabPagePdfImage.Location = new System.Drawing.Point(4, 5);
      this.tabPagePdfImage.Margin = new System.Windows.Forms.Padding(2);
      this.tabPagePdfImage.Name = "tabPagePdfImage";
      this.tabPagePdfImage.Padding = new System.Windows.Forms.Padding(2);
      this.tabPagePdfImage.Size = new System.Drawing.Size(916, 705);
      this.tabPagePdfImage.TabIndex = 0;
      //
      // tabPagePdfStructure
      //
      this.tabPagePdfStructure.Controls.Add(this.txtPdfStructure);
      this.tabPagePdfStructure.Location = new System.Drawing.Point(4, 5);
      this.tabPagePdfStructure.Margin = new System.Windows.Forms.Padding(2);
      this.tabPagePdfStructure.Name = "tabPagePdfStructure";
      this.tabPagePdfStructure.Padding = new System.Windows.Forms.Padding(4, 2, 0, 0);
      this.tabPagePdfStructure.Size = new System.Drawing.Size(916, 705);
      this.tabPagePdfStructure.TabIndex = 1;
      this.tabPagePdfStructure.UseVisualStyleBackColor = true;
      //
      // txtPdfStructure
      //
      this.txtPdfStructure.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtPdfStructure.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtPdfStructure.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtPdfStructure.Location = new System.Drawing.Point(4, 2);
      this.txtPdfStructure.Margin = new System.Windows.Forms.Padding(2);
      this.txtPdfStructure.Multiline = true;
      this.txtPdfStructure.Name = "txtPdfStructure";
      this.txtPdfStructure.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtPdfStructure.Size = new System.Drawing.Size(912, 703);
      this.txtPdfStructure.TabIndex = 0;
      this.txtPdfStructure.WordWrap = false;
      //
      // pnlBackground
      //
      this.pnlBackground.Controls.Add(this.splitterMain);
      this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlBackground.Location = new System.Drawing.Point(0, 61);
      this.pnlBackground.Margin = new System.Windows.Forms.Padding(2);
      this.pnlBackground.Name = "pnlBackground";
      this.pnlBackground.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.pnlBackground.Size = new System.Drawing.Size(1324, 716);
      this.pnlBackground.TabIndex = 4;
      //
      // txtObjectNumberBreak
      //
      this.txtObjectNumberBreak.Location = new System.Drawing.Point(613, 8);
      this.txtObjectNumberBreak.Name = "txtObjectNumberBreak";
      this.txtObjectNumberBreak.Size = new System.Drawing.Size(72, 20);
      this.txtObjectNumberBreak.TabIndex = 2;
      //
      // lblDebugBreak
      //
      this.lblDebugBreak.AutoSize = true;
      this.lblDebugBreak.Location = new System.Drawing.Point(579, 11);
      this.lblDebugBreak.Name = "lblDebugBreak";
      this.lblDebugBreak.Size = new System.Drawing.Size(35, 13);
      this.lblDebugBreak.TabIndex = 3;
      this.lblDebugBreak.Text = "Break";
      //
      // pnlPdfImageShadow
      //
      this.pnlPdfImageShadow.BackColor = System.Drawing.Color.Black;
      this.pnlPdfImageShadow.Location = new System.Drawing.Point(10, 10);
      this.pnlPdfImageShadow.Name = "pnlPdfImageShadow";
      this.pnlPdfImageShadow.Size = new System.Drawing.Size(400, 400);
      this.pnlPdfImageShadow.TabIndex = 0;
      //
      // pbPdfImage
      //
      this.pbPdfImage.BackColor = System.Drawing.Color.White;
      this.pbPdfImage.Location = new System.Drawing.Point(8, 8);
      this.pbPdfImage.Name = "pbPdfImage";
      this.pbPdfImage.Size = new System.Drawing.Size(400, 400);
      this.pbPdfImage.TabIndex = 0;
      this.pbPdfImage.TabStop = false;
      //
      // frmMain
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1324, 792);
      this.Controls.Add(this.pnlBackground);
      this.Controls.Add(this.pnlTop);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mnuMain;
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "PDF Explorer";
      this.Shown += new System.EventHandler(this.frmMain_Shown);
      this.ResizeEnd += new System.EventHandler(this.frmMain_ResizeEnd);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.splitterMain.Panel1.ResumeLayout(false);
      this.splitterMain.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitterMain)).EndInit();
      this.splitterMain.ResumeLayout(false);
      this.ctxMenuTreeView.ResumeLayout(false);
      this.tabMain.ResumeLayout(false);
      this.tabPagePdfImage.ResumeLayout(false);
      this.tabPagePdfStructure.ResumeLayout(false);
      this.tabPagePdfStructure.PerformLayout();
      this.pnlBackground.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbPdfImage)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.SplitContainer splitterMain;
    private System.Windows.Forms.TreeView tvMain;
    private System.Windows.Forms.Panel pnlTvBottom;
    private System.Windows.Forms.Panel pnlTvTop;
    private System.Windows.Forms.Button btnSwitchView;
    private System.Windows.Forms.TabControl tabMain;
    private System.Windows.Forms.TabPage tabPagePdfImage;
    private System.Windows.Forms.TabPage tabPagePdfStructure;
    private System.Windows.Forms.TextBox txtPdfStructure;
    private System.Windows.Forms.Panel pnlBackground;
    private System.Windows.Forms.Button btnOpenDocument;
    private System.Windows.Forms.ContextMenuStrip ctxMenuTreeView;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuTreeViewExpandAll;
    private System.Windows.Forms.ToolStripMenuItem ctxMenuTreeViewCollapseAll;
    private System.Windows.Forms.CheckBox ckOmitAnnotations;
    private System.Windows.Forms.CheckBox ckOnlyImages;
    private System.Windows.Forms.TextBox txtObjectNumberBreak;
    private System.Windows.Forms.Label lblDebugBreak;
    private System.Windows.Forms.PictureBox pbPdfImage;
    private System.Windows.Forms.Panel pnlPdfImageShadow;
  }
}


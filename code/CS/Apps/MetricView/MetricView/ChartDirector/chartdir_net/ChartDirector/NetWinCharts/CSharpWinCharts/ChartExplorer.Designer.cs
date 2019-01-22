namespace CSharpChartExplorer
{
    partial class ChartExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartExplorer));
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.statusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.BackPB = new System.Windows.Forms.ToolBarButton();
            this.ForwardPB = new System.Windows.Forms.ToolBarButton();
            this.PreviousPB = new System.Windows.Forms.ToolBarButton();
            this.NextPB = new System.Windows.Forms.ToolBarButton();
            this.ViewSourcePB = new System.Windows.Forms.ToolBarButton();
            this.HelpPB = new System.Windows.Forms.ToolBarButton();
            this.toolBarImageList = new System.Windows.Forms.ImageList(this.components);
            this.treeView = new System.Windows.Forms.TreeView();
            this.treeViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.splitter = new System.Windows.Forms.Splitter();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.chartViewer8 = new ChartDirector.WinChartViewer();
            this.chartViewer7 = new ChartDirector.WinChartViewer();
            this.chartViewer6 = new ChartDirector.WinChartViewer();
            this.chartViewer5 = new ChartDirector.WinChartViewer();
            this.chartViewer4 = new ChartDirector.WinChartViewer();
            this.chartViewer3 = new ChartDirector.WinChartViewer();
            this.chartViewer2 = new ChartDirector.WinChartViewer();
            this.line = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.chartViewer1 = new ChartDirector.WinChartViewer();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
            this.rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 485);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(842, 21);
            this.statusBar.TabIndex = 3;
            this.statusBar.Text = "statusBar";
            // 
            // statusBarPanel
            // 
            this.statusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel.Name = "statusBarPanel";
            this.statusBarPanel.Text = " Please select chart to view";
            this.statusBarPanel.Width = 825;
            // 
            // toolBar
            // 
            this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.BackPB,
            this.ForwardPB,
            this.PreviousPB,
            this.NextPB,
            this.ViewSourcePB,
            this.HelpPB});
            this.toolBar.ButtonSize = new System.Drawing.Size(60, 50);
            this.toolBar.Divider = false;
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.toolBarImageList;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(842, 45);
            this.toolBar.TabIndex = 4;
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // BackPB
            // 
            this.BackPB.Enabled = false;
            this.BackPB.ImageIndex = 0;
            this.BackPB.Name = "BackPB";
            this.BackPB.Text = "Back";
            // 
            // ForwardPB
            // 
            this.ForwardPB.Enabled = false;
            this.ForwardPB.ImageIndex = 1;
            this.ForwardPB.Name = "ForwardPB";
            this.ForwardPB.Text = "Forward";
            // 
            // PreviousPB
            // 
            this.PreviousPB.ImageIndex = 2;
            this.PreviousPB.Name = "PreviousPB";
            this.PreviousPB.Text = "Previous";
            // 
            // NextPB
            // 
            this.NextPB.ImageIndex = 3;
            this.NextPB.Name = "NextPB";
            this.NextPB.Text = "Next";
            // 
            // ViewSourcePB
            // 
            this.ViewSourcePB.ImageIndex = 4;
            this.ViewSourcePB.Name = "ViewSourcePB";
            this.ViewSourcePB.Text = "View Code";
            // 
            // HelpPB
            // 
            this.HelpPB.ImageIndex = 5;
            this.HelpPB.Name = "HelpPB";
            this.HelpPB.Text = "View Doc";
            // 
            // toolBarImageList
            // 
            this.toolBarImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarImageList.ImageStream")));
            this.toolBarImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.toolBarImageList.Images.SetKeyName(0, "");
            this.toolBarImageList.Images.SetKeyName(1, "");
            this.toolBarImageList.Images.SetKeyName(2, "");
            this.toolBarImageList.Images.SetKeyName(3, "");
            this.toolBarImageList.Images.SetKeyName(4, "");
            this.toolBarImageList.Images.SetKeyName(5, "");
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView.HotTracking = true;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.treeViewImageList;
            this.treeView.ItemHeight = 16;
            this.treeView.Location = new System.Drawing.Point(0, 45);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(210, 440);
            this.treeView.TabIndex = 5;
            this.treeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeCollapse);
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // treeViewImageList
            // 
            this.treeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImageList.ImageStream")));
            this.treeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeViewImageList.Images.SetKeyName(0, "");
            this.treeViewImageList.Images.SetKeyName(1, "");
            this.treeViewImageList.Images.SetKeyName(2, "");
            this.treeViewImageList.Images.SetKeyName(3, "");
            this.treeViewImageList.Images.SetKeyName(4, "");
            this.treeViewImageList.Images.SetKeyName(5, "");
            this.treeViewImageList.Images.SetKeyName(6, "");
            this.treeViewImageList.Images.SetKeyName(7, "");
            this.treeViewImageList.Images.SetKeyName(8, "");
            this.treeViewImageList.Images.SetKeyName(9, "");
            this.treeViewImageList.Images.SetKeyName(10, "");
            this.treeViewImageList.Images.SetKeyName(11, "");
            this.treeViewImageList.Images.SetKeyName(12, "");
            this.treeViewImageList.Images.SetKeyName(13, "");
            this.treeViewImageList.Images.SetKeyName(14, "");
            this.treeViewImageList.Images.SetKeyName(15, "");
            this.treeViewImageList.Images.SetKeyName(16, "");
            this.treeViewImageList.Images.SetKeyName(17, "");
            this.treeViewImageList.Images.SetKeyName(18, "");
            this.treeViewImageList.Images.SetKeyName(19, "");
            this.treeViewImageList.Images.SetKeyName(20, "");
            this.treeViewImageList.Images.SetKeyName(21, "linearmetericon.png");
            this.treeViewImageList.Images.SetKeyName(22, "barmetericon.png");
            // 
            // splitter
            // 
            this.splitter.Location = new System.Drawing.Point(210, 45);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(3, 440);
            this.splitter.TabIndex = 6;
            this.splitter.TabStop = false;
            // 
            // rightPanel
            // 
            this.rightPanel.AutoScroll = true;
            this.rightPanel.BackColor = System.Drawing.SystemColors.Window;
            this.rightPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rightPanel.Controls.Add(this.chartViewer8);
            this.rightPanel.Controls.Add(this.chartViewer7);
            this.rightPanel.Controls.Add(this.chartViewer6);
            this.rightPanel.Controls.Add(this.chartViewer5);
            this.rightPanel.Controls.Add(this.chartViewer4);
            this.rightPanel.Controls.Add(this.chartViewer3);
            this.rightPanel.Controls.Add(this.chartViewer2);
            this.rightPanel.Controls.Add(this.line);
            this.rightPanel.Controls.Add(this.title);
            this.rightPanel.Controls.Add(this.chartViewer1);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(213, 45);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(629, 440);
            this.rightPanel.TabIndex = 7;
            this.rightPanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.rightPanel_Layout);
            // 
            // chartViewer8
            // 
            this.chartViewer8.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer8.Location = new System.Drawing.Point(368, 144);
            this.chartViewer8.Name = "chartViewer8";
            this.chartViewer8.Size = new System.Drawing.Size(112, 98);
            this.chartViewer8.TabIndex = 9;
            this.chartViewer8.TabStop = false;
            this.chartViewer8.ClickHotSpot += new ChartDirector.WinHotSpotEventHandler(this.chartViewer_ClickHotSpot);
            // 
            // chartViewer7
            // 
            this.chartViewer7.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer7.Location = new System.Drawing.Point(248, 144);
            this.chartViewer7.Name = "chartViewer7";
            this.chartViewer7.Size = new System.Drawing.Size(112, 98);
            this.chartViewer7.TabIndex = 8;
            this.chartViewer7.TabStop = false;
            this.chartViewer7.ClickHotSpot += new ChartDirector.WinHotSpotEventHandler(this.chartViewer_ClickHotSpot);
            // 
            // chartViewer6
            // 
            this.chartViewer6.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer6.Location = new System.Drawing.Point(128, 144);
            this.chartViewer6.Name = "chartViewer6";
            this.chartViewer6.Size = new System.Drawing.Size(112, 98);
            this.chartViewer6.TabIndex = 7;
            this.chartViewer6.TabStop = false;
            this.chartViewer6.ClickHotSpot += new ChartDirector.WinHotSpotEventHandler(this.chartViewer_ClickHotSpot);
            // 
            // chartViewer5
            // 
            this.chartViewer5.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer5.Location = new System.Drawing.Point(6, 144);
            this.chartViewer5.Name = "chartViewer5";
            this.chartViewer5.Size = new System.Drawing.Size(112, 98);
            this.chartViewer5.TabIndex = 6;
            this.chartViewer5.TabStop = false;
            this.chartViewer5.ClickHotSpot += new ChartDirector.WinHotSpotEventHandler(this.chartViewer_ClickHotSpot);
            // 
            // chartViewer4
            // 
            this.chartViewer4.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer4.Location = new System.Drawing.Point(368, 38);
            this.chartViewer4.Name = "chartViewer4";
            this.chartViewer4.Size = new System.Drawing.Size(112, 98);
            this.chartViewer4.TabIndex = 5;
            this.chartViewer4.TabStop = false;
            this.chartViewer4.ClickHotSpot += new ChartDirector.WinHotSpotEventHandler(this.chartViewer_ClickHotSpot);
            // 
            // chartViewer3
            // 
            this.chartViewer3.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer3.Location = new System.Drawing.Point(248, 38);
            this.chartViewer3.Name = "chartViewer3";
            this.chartViewer3.Size = new System.Drawing.Size(112, 98);
            this.chartViewer3.TabIndex = 4;
            this.chartViewer3.TabStop = false;
            this.chartViewer3.ClickHotSpot += new ChartDirector.WinHotSpotEventHandler(this.chartViewer_ClickHotSpot);
            // 
            // chartViewer2
            // 
            this.chartViewer2.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer2.Location = new System.Drawing.Point(128, 38);
            this.chartViewer2.Name = "chartViewer2";
            this.chartViewer2.Size = new System.Drawing.Size(112, 98);
            this.chartViewer2.TabIndex = 3;
            this.chartViewer2.TabStop = false;
            this.chartViewer2.ClickHotSpot += new ChartDirector.WinHotSpotEventHandler(this.chartViewer_ClickHotSpot);
            // 
            // line
            // 
            this.line.BackColor = System.Drawing.Color.DarkBlue;
            this.line.Dock = System.Windows.Forms.DockStyle.Top;
            this.line.Location = new System.Drawing.Point(0, 29);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(625, 3);
            this.line.TabIndex = 2;
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Dock = System.Windows.Forms.DockStyle.Top;
            this.title.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(494, 29);
            this.title.TabIndex = 1;
            this.title.Text = "Up to 8 charts in each demo module";
            // 
            // chartViewer1
            // 
            this.chartViewer1.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer1.Location = new System.Drawing.Point(8, 38);
            this.chartViewer1.Name = "chartViewer1";
            this.chartViewer1.Size = new System.Drawing.Size(112, 98);
            this.chartViewer1.TabIndex = 0;
            this.chartViewer1.TabStop = false;
            this.chartViewer1.ClickHotSpot += new ChartDirector.WinHotSpotEventHandler(this.chartViewer_ClickHotSpot);
            // 
            // ChartExplorer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(842, 506);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.statusBar);
            this.Name = "ChartExplorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChartDirector Sample Charts";
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ImageList toolBarImageList;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.StatusBarPanel statusBarPanel;
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.ImageList treeViewImageList;
		private System.Windows.Forms.ToolBarButton BackPB;
		private System.Windows.Forms.ToolBarButton ForwardPB;
		private System.Windows.Forms.ToolBarButton PreviousPB;
		private System.Windows.Forms.ToolBarButton NextPB;
		private System.Windows.Forms.ToolBarButton ViewSourcePB;
		private System.Windows.Forms.ToolBarButton HelpPB;
		private System.Windows.Forms.Splitter splitter;
		private System.Windows.Forms.Panel rightPanel;
		private System.Windows.Forms.Label title;
		private System.Windows.Forms.Label line;
		private ChartDirector.WinChartViewer chartViewer1;
		private ChartDirector.WinChartViewer chartViewer2;
		private ChartDirector.WinChartViewer chartViewer3;
		private ChartDirector.WinChartViewer chartViewer4;
		private ChartDirector.WinChartViewer chartViewer5;
		private ChartDirector.WinChartViewer chartViewer6;
		private ChartDirector.WinChartViewer chartViewer7;
		private ChartDirector.WinChartViewer chartViewer8;
	}
}
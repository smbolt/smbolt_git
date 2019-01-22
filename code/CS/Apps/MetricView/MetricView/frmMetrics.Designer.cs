namespace Teleflora.Operations.MetricView
{
    partial class frmMetrics
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
            this.sbMain = new System.Windows.Forms.StatusStrip();
            this.splitterMain = new System.Windows.Forms.SplitContainer();
            this.lvServers = new System.Windows.Forms.ListView();
            this.pnlServersCriteriaBottom = new System.Windows.Forms.Panel();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.lblServersIncludedStatus = new System.Windows.Forms.Label();
            this.pnlServersDateTime = new System.Windows.Forms.Panel();
            this.btnGetMetrics = new System.Windows.Forms.Button();
            this.lblServersIncluded = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblServersToDateTime = new System.Windows.Forms.Label();
            this.lblServersFromDateTime = new System.Windows.Forms.Label();
            this.lblServerCriteria = new System.Windows.Forms.Label();
            this.pnlMetricsDetail = new System.Windows.Forms.Panel();
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.lblMetricDetail = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabMetricList = new System.Windows.Forms.TabPage();
            this.tabMetricDetail = new System.Windows.Forms.TabPage();
            this.lvMetrics = new System.Windows.Forms.ListView();
            this.pnlMetricsTop = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMetrics = new System.Windows.Forms.Label();
            this.lblMetricDescription = new System.Windows.Forms.Label();
            this.pnlDetailTop = new System.Windows.Forms.Panel();
            this.pnlDetailMain = new System.Windows.Forms.Panel();
            this.pbMetricDetail = new System.Windows.Forms.PictureBox();
            this.lblMetricDetailHeader = new System.Windows.Forms.Label();
            this.mnuMain.SuspendLayout();
            this.sbMain.SuspendLayout();
            this.splitterMain.Panel1.SuspendLayout();
            this.splitterMain.Panel2.SuspendLayout();
            this.splitterMain.SuspendLayout();
            this.pnlServersCriteriaBottom.SuspendLayout();
            this.pnlServersDateTime.SuspendLayout();
            this.pnlMetricsDetail.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabMetricList.SuspendLayout();
            this.tabMetricDetail.SuspendLayout();
            this.pnlMetricsTop.SuspendLayout();
            this.pnlDetailTop.SuspendLayout();
            this.pnlDetailMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMetricDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.mnuMain.Location = new System.Drawing.Point(2, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1123, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(103, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // sbMain
            // 
            this.sbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.sbMain.Location = new System.Drawing.Point(2, 769);
            this.sbMain.Name = "sbMain";
            this.sbMain.Size = new System.Drawing.Size(1123, 22);
            this.sbMain.TabIndex = 1;
            this.sbMain.Text = "statusStrip1";
            // 
            // splitterMain
            // 
            this.splitterMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitterMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitterMain.IsSplitterFixed = true;
            this.splitterMain.Location = new System.Drawing.Point(2, 24);
            this.splitterMain.Name = "splitterMain";
            // 
            // splitterMain.Panel1
            // 
            this.splitterMain.Panel1.Controls.Add(this.lvServers);
            this.splitterMain.Panel1.Controls.Add(this.pnlServersCriteriaBottom);
            this.splitterMain.Panel1.Controls.Add(this.pnlServersDateTime);
            this.splitterMain.Panel1.Controls.Add(this.lblServerCriteria);
            this.splitterMain.Panel1.Padding = new System.Windows.Forms.Padding(6, 2, 6, 0);
            // 
            // splitterMain.Panel2
            // 
            this.splitterMain.Panel2.Controls.Add(this.pnlMetricsTop);
            this.splitterMain.Panel2.Controls.Add(this.tabMain);
            this.splitterMain.Panel2.Controls.Add(this.pnlMetricsDetail);
            this.splitterMain.Size = new System.Drawing.Size(1123, 745);
            this.splitterMain.SplitterDistance = 266;
            this.splitterMain.TabIndex = 2;
            // 
            // lvServers
            // 
            this.lvServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvServers.FullRowSelect = true;
            this.lvServers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvServers.HideSelection = false;
            this.lvServers.Location = new System.Drawing.Point(6, 121);
            this.lvServers.MultiSelect = false;
            this.lvServers.Name = "lvServers";
            this.lvServers.Size = new System.Drawing.Size(250, 560);
            this.lvServers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvServers.TabIndex = 20;
            this.lvServers.UseCompatibleStateImageBehavior = false;
            this.lvServers.View = System.Windows.Forms.View.Details;
            this.lvServers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvServers_ItemChecked);
            // 
            // pnlServersCriteriaBottom
            // 
            this.pnlServersCriteriaBottom.Controls.Add(this.btnClearAll);
            this.pnlServersCriteriaBottom.Controls.Add(this.btnCheckAll);
            this.pnlServersCriteriaBottom.Controls.Add(this.lblServersIncludedStatus);
            this.pnlServersCriteriaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlServersCriteriaBottom.Location = new System.Drawing.Point(6, 681);
            this.pnlServersCriteriaBottom.Name = "pnlServersCriteriaBottom";
            this.pnlServersCriteriaBottom.Size = new System.Drawing.Size(250, 60);
            this.pnlServersCriteriaBottom.TabIndex = 9;
            // 
            // btnClearAll
            // 
            this.btnClearAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAll.Location = new System.Drawing.Point(130, 27);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(120, 24);
            this.btnClearAll.TabIndex = 31;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckAll.Location = new System.Drawing.Point(0, 27);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(120, 24);
            this.btnCheckAll.TabIndex = 30;
            this.btnCheckAll.Text = "Check All";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // lblServersIncludedStatus
            // 
            this.lblServersIncludedStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblServersIncludedStatus.Location = new System.Drawing.Point(0, 0);
            this.lblServersIncludedStatus.Name = "lblServersIncludedStatus";
            this.lblServersIncludedStatus.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lblServersIncludedStatus.Size = new System.Drawing.Size(250, 28);
            this.lblServersIncludedStatus.TabIndex = 6;
            this.lblServersIncludedStatus.Text = "0 Servers Included";
            // 
            // pnlServersDateTime
            // 
            this.pnlServersDateTime.Controls.Add(this.btnGetMetrics);
            this.pnlServersDateTime.Controls.Add(this.lblServersIncluded);
            this.pnlServersDateTime.Controls.Add(this.dtpToDate);
            this.pnlServersDateTime.Controls.Add(this.dtpFromDate);
            this.pnlServersDateTime.Controls.Add(this.lblServersToDateTime);
            this.pnlServersDateTime.Controls.Add(this.lblServersFromDateTime);
            this.pnlServersDateTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlServersDateTime.Location = new System.Drawing.Point(6, 27);
            this.pnlServersDateTime.Name = "pnlServersDateTime";
            this.pnlServersDateTime.Size = new System.Drawing.Size(250, 94);
            this.pnlServersDateTime.TabIndex = 1;
            // 
            // btnGetMetrics
            // 
            this.btnGetMetrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetMetrics.Location = new System.Drawing.Point(130, 60);
            this.btnGetMetrics.Name = "btnGetMetrics";
            this.btnGetMetrics.Size = new System.Drawing.Size(120, 24);
            this.btnGetMetrics.TabIndex = 15;
            this.btnGetMetrics.Text = "Get Metrics";
            this.btnGetMetrics.UseVisualStyleBackColor = true;
            this.btnGetMetrics.Click += new System.EventHandler(this.btnGetMetrics_Click);
            // 
            // lblServersIncluded
            // 
            this.lblServersIncluded.AutoSize = true;
            this.lblServersIncluded.Location = new System.Drawing.Point(3, 71);
            this.lblServersIncluded.Name = "lblServersIncluded";
            this.lblServersIncluded.Size = new System.Drawing.Size(90, 13);
            this.lblServersIncluded.TabIndex = 2;
            this.lblServersIncluded.Text = "Included Servers:";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "yyyy-MM-dd HH:mm ";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(132, 29);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(120, 20);
            this.dtpToDate.TabIndex = 11;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "yyyy-MM-dd HH:mm ";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(6, 29);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(120, 20);
            this.dtpFromDate.TabIndex = 10;
            // 
            // lblServersToDateTime
            // 
            this.lblServersToDateTime.AutoSize = true;
            this.lblServersToDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServersToDateTime.Location = new System.Drawing.Point(129, 13);
            this.lblServersToDateTime.Name = "lblServersToDateTime";
            this.lblServersToDateTime.Size = new System.Drawing.Size(77, 13);
            this.lblServersToDateTime.TabIndex = 0;
            this.lblServersToDateTime.Text = "To Date/Time:";
            this.lblServersToDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServersFromDateTime
            // 
            this.lblServersFromDateTime.AutoSize = true;
            this.lblServersFromDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServersFromDateTime.Location = new System.Drawing.Point(3, 13);
            this.lblServersFromDateTime.Name = "lblServersFromDateTime";
            this.lblServersFromDateTime.Size = new System.Drawing.Size(87, 13);
            this.lblServersFromDateTime.TabIndex = 0;
            this.lblServersFromDateTime.Text = "From Date/Time:";
            this.lblServersFromDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServerCriteria
            // 
            this.lblServerCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblServerCriteria.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerCriteria.Location = new System.Drawing.Point(6, 2);
            this.lblServerCriteria.Name = "lblServerCriteria";
            this.lblServerCriteria.Size = new System.Drawing.Size(250, 25);
            this.lblServerCriteria.TabIndex = 1;
            this.lblServerCriteria.Text = "Select Metrics Criteria";
            this.lblServerCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMetricsDetail
            // 
            this.pnlMetricsDetail.Controls.Add(this.txtDetail);
            this.pnlMetricsDetail.Controls.Add(this.lblMetricDetail);
            this.pnlMetricsDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMetricsDetail.Location = new System.Drawing.Point(0, 619);
            this.pnlMetricsDetail.Name = "pnlMetricsDetail";
            this.pnlMetricsDetail.Size = new System.Drawing.Size(849, 122);
            this.pnlMetricsDetail.TabIndex = 4;
            // 
            // txtDetail
            // 
            this.txtDetail.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetail.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetail.Location = new System.Drawing.Point(0, 24);
            this.txtDetail.Multiline = true;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.ReadOnly = true;
            this.txtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtDetail.Size = new System.Drawing.Size(849, 98);
            this.txtDetail.TabIndex = 2;
            this.txtDetail.TabStop = false;
            // 
            // lblMetricDetail
            // 
            this.lblMetricDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMetricDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetricDetail.Location = new System.Drawing.Point(0, 0);
            this.lblMetricDetail.Name = "lblMetricDetail";
            this.lblMetricDetail.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblMetricDetail.Size = new System.Drawing.Size(849, 24);
            this.lblMetricDetail.TabIndex = 1;
            this.lblMetricDetail.Text = "Metric Detail";
            this.lblMetricDetail.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tabMain
            // 
            this.tabMain.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabMain.Controls.Add(this.tabMetricList);
            this.tabMain.Controls.Add(this.tabMetricDetail);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.ItemSize = new System.Drawing.Size(5, 49);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(849, 619);
            this.tabMain.TabIndex = 6;
            // 
            // tabMetricList
            // 
            this.tabMetricList.Controls.Add(this.lvMetrics);
            this.tabMetricList.Location = new System.Drawing.Point(4, 53);
            this.tabMetricList.Name = "tabMetricList";
            this.tabMetricList.Padding = new System.Windows.Forms.Padding(3);
            this.tabMetricList.Size = new System.Drawing.Size(841, 562);
            this.tabMetricList.TabIndex = 0;
            this.tabMetricList.Tag = "METRIC_LIST";
            this.tabMetricList.UseVisualStyleBackColor = true;
            // 
            // tabMetricDetail
            // 
            this.tabMetricDetail.Controls.Add(this.pnlDetailMain);
            this.tabMetricDetail.Controls.Add(this.pnlDetailTop);
            this.tabMetricDetail.Location = new System.Drawing.Point(4, 53);
            this.tabMetricDetail.Name = "tabMetricDetail";
            this.tabMetricDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabMetricDetail.Size = new System.Drawing.Size(841, 562);
            this.tabMetricDetail.TabIndex = 1;
            this.tabMetricDetail.Tag = "METRIC_DETAIL";
            this.tabMetricDetail.UseVisualStyleBackColor = true;
            // 
            // lvMetrics
            // 
            this.lvMetrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMetrics.FullRowSelect = true;
            this.lvMetrics.GridLines = true;
            this.lvMetrics.Location = new System.Drawing.Point(3, 3);
            this.lvMetrics.MultiSelect = false;
            this.lvMetrics.Name = "lvMetrics";
            this.lvMetrics.Size = new System.Drawing.Size(835, 556);
            this.lvMetrics.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvMetrics.TabIndex = 6;
            this.lvMetrics.UseCompatibleStateImageBehavior = false;
            this.lvMetrics.View = System.Windows.Forms.View.Details;
            this.lvMetrics.DoubleClick += new System.EventHandler(this.lvMetrics_DoubleClick);
            // 
            // pnlMetricsTop
            // 
            this.pnlMetricsTop.Controls.Add(this.lblMetricDescription);
            this.pnlMetricsTop.Controls.Add(this.lblMetrics);
            this.pnlMetricsTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMetricsTop.Location = new System.Drawing.Point(0, 0);
            this.pnlMetricsTop.Name = "pnlMetricsTop";
            this.pnlMetricsTop.Size = new System.Drawing.Size(849, 52);
            this.pnlMetricsTop.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(38, 17);
            this.lblStatus.Text = "Status";
            // 
            // lblMetrics
            // 
            this.lblMetrics.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMetrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetrics.Location = new System.Drawing.Point(0, 0);
            this.lblMetrics.Name = "lblMetrics";
            this.lblMetrics.Size = new System.Drawing.Size(849, 25);
            this.lblMetrics.TabIndex = 2;
            this.lblMetrics.Text = "Metrics List";
            this.lblMetrics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMetricDescription
            // 
            this.lblMetricDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMetricDescription.Location = new System.Drawing.Point(0, 25);
            this.lblMetricDescription.Name = "lblMetricDescription";
            this.lblMetricDescription.Size = new System.Drawing.Size(849, 16);
            this.lblMetricDescription.TabIndex = 3;
            this.lblMetricDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDetailTop
            // 
            this.pnlDetailTop.Controls.Add(this.lblMetricDetailHeader);
            this.pnlDetailTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetailTop.Location = new System.Drawing.Point(3, 3);
            this.pnlDetailTop.Name = "pnlDetailTop";
            this.pnlDetailTop.Size = new System.Drawing.Size(835, 67);
            this.pnlDetailTop.TabIndex = 0;
            // 
            // pnlDetailMain
            // 
            this.pnlDetailMain.AutoScroll = true;
            this.pnlDetailMain.Controls.Add(this.pbMetricDetail);
            this.pnlDetailMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetailMain.Location = new System.Drawing.Point(3, 70);
            this.pnlDetailMain.Name = "pnlDetailMain";
            this.pnlDetailMain.Size = new System.Drawing.Size(835, 489);
            this.pnlDetailMain.TabIndex = 1;
            // 
            // pbMetricDetail
            // 
            this.pbMetricDetail.BackColor = System.Drawing.Color.White;
            this.pbMetricDetail.Location = new System.Drawing.Point(0, 0);
            this.pbMetricDetail.Name = "pbMetricDetail";
            this.pbMetricDetail.Size = new System.Drawing.Size(829, 480);
            this.pbMetricDetail.TabIndex = 0;
            this.pbMetricDetail.TabStop = false;
            this.pbMetricDetail.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMetricDetail_Paint);
            // 
            // lblMetricDetailHeader
            // 
            this.lblMetricDetailHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMetricDetailHeader.Location = new System.Drawing.Point(0, 0);
            this.lblMetricDetailHeader.Name = "lblMetricDetailHeader";
            this.lblMetricDetailHeader.Size = new System.Drawing.Size(835, 67);
            this.lblMetricDetailHeader.TabIndex = 0;
            // 
            // frmMetrics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 791);
            this.Controls.Add(this.splitterMain);
            this.Controls.Add(this.sbMain);
            this.Controls.Add(this.mnuMain);
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMetrics";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metrics";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMetrics_KeyDown);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.sbMain.ResumeLayout(false);
            this.sbMain.PerformLayout();
            this.splitterMain.Panel1.ResumeLayout(false);
            this.splitterMain.Panel2.ResumeLayout(false);
            this.splitterMain.ResumeLayout(false);
            this.pnlServersCriteriaBottom.ResumeLayout(false);
            this.pnlServersDateTime.ResumeLayout(false);
            this.pnlServersDateTime.PerformLayout();
            this.pnlMetricsDetail.ResumeLayout(false);
            this.pnlMetricsDetail.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabMetricList.ResumeLayout(false);
            this.tabMetricDetail.ResumeLayout(false);
            this.pnlMetricsTop.ResumeLayout(false);
            this.pnlDetailTop.ResumeLayout(false);
            this.pnlDetailMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMetricDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.StatusStrip sbMain;
        private System.Windows.Forms.SplitContainer splitterMain;
        private System.Windows.Forms.Label lblServerCriteria;
        private System.Windows.Forms.Panel pnlServersDateTime;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblServersToDateTime;
        private System.Windows.Forms.Label lblServersFromDateTime;
        private System.Windows.Forms.Label lblServersIncluded;
        private System.Windows.Forms.Label lblServersIncludedStatus;
        private System.Windows.Forms.Button btnGetMetrics;
        private System.Windows.Forms.ListView lvServers;
        private System.Windows.Forms.Panel pnlServersCriteriaBottom;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Panel pnlMetricsDetail;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.Label lblMetricDetail;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabMetricList;
        private System.Windows.Forms.ListView lvMetrics;
        private System.Windows.Forms.TabPage tabMetricDetail;
        private System.Windows.Forms.Panel pnlMetricsTop;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Label lblMetricDescription;
        private System.Windows.Forms.Label lblMetrics;
        private System.Windows.Forms.Panel pnlDetailMain;
        private System.Windows.Forms.PictureBox pbMetricDetail;
        private System.Windows.Forms.Panel pnlDetailTop;
        private System.Windows.Forms.Label lblMetricDetailHeader;

    }
}
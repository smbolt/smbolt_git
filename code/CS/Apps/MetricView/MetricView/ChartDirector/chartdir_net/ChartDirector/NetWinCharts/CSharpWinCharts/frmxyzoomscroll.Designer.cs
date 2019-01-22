namespace CSharpChartExplorer
{
    partial class FrmXYZoomScroll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmXYZoomScroll));
            this.leftPanel = new System.Windows.Forms.Panel();
            this.savePB = new System.Windows.Forms.Button();
            this.viewPortControl1 = new ChartDirector.WinViewPortControl(this.components);
            this.winChartViewer1 = new ChartDirector.WinChartViewer();
            this.zoomLevelLabel = new System.Windows.Forms.Label();
            this.separator = new System.Windows.Forms.Label();
            this.pointerPB = new System.Windows.Forms.RadioButton();
            this.zoomInPB = new System.Windows.Forms.RadioButton();
            this.zoomOutPB = new System.Windows.Forms.RadioButton();
            this.zoomBar = new System.Windows.Forms.TrackBar();
            this.topLabel = new System.Windows.Forms.Label();
            this.leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewPortControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomBar)).BeginInit();
            this.SuspendLayout();
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.LightGray;
            this.leftPanel.Controls.Add(this.savePB);
            this.leftPanel.Controls.Add(this.viewPortControl1);
            this.leftPanel.Controls.Add(this.zoomLevelLabel);
            this.leftPanel.Controls.Add(this.separator);
            this.leftPanel.Controls.Add(this.pointerPB);
            this.leftPanel.Controls.Add(this.zoomInPB);
            this.leftPanel.Controls.Add(this.zoomOutPB);
            this.leftPanel.Controls.Add(this.zoomBar);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftPanel.Location = new System.Drawing.Point(0, 24);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(120, 482);
            this.leftPanel.TabIndex = 20;
            // 
            // savePB
            // 
            this.savePB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savePB.Image = ((System.Drawing.Image)(resources.GetObject("savePB.Image")));
            this.savePB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.savePB.Location = new System.Drawing.Point(0, 112);
            this.savePB.Name = "savePB";
            this.savePB.Size = new System.Drawing.Size(120, 28);
            this.savePB.TabIndex = 3;
            this.savePB.Text = "      Save";
            this.savePB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.savePB.Click += new System.EventHandler(this.savePB_Click);
            // 
            // viewPortControl1
            // 
            this.viewPortControl1.Location = new System.Drawing.Point(5, 331);
            this.viewPortControl1.Name = "viewPortControl1";
            this.viewPortControl1.SelectionBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.viewPortControl1.Size = new System.Drawing.Size(110, 110);
            this.viewPortControl1.TabIndex = 34;
            this.viewPortControl1.TabStop = false;
            this.viewPortControl1.Viewer = this.winChartViewer1;
            this.viewPortControl1.ViewPortBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.viewPortControl1.ViewPortExternalColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // winChartViewer1
            // 
            this.winChartViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winChartViewer1.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.winChartViewer1.Location = new System.Drawing.Point(120, 24);
            this.winChartViewer1.MouseUsage = ChartDirector.WinChartMouseUsage.ScrollOnDrag;
            this.winChartViewer1.MouseWheelZoomRatio = 1.1;
            this.winChartViewer1.Name = "winChartViewer1";
            this.winChartViewer1.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.winChartViewer1.Size = new System.Drawing.Size(500, 482);
            this.winChartViewer1.TabIndex = 19;
            this.winChartViewer1.TabStop = false;
            this.winChartViewer1.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical;
            this.winChartViewer1.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.winChartViewer1_ViewPortChanged);
            this.winChartViewer1.ClickHotSpot += new ChartDirector.WinHotSpotEventHandler(this.winChartViewer1_ClickHotSpot);
            this.winChartViewer1.MouseMovePlotArea += new System.Windows.Forms.MouseEventHandler(this.winChartViewer1_MouseMovePlotArea);
            // 
            // zoomLevelLabel
            // 
            this.zoomLevelLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomLevelLabel.Location = new System.Drawing.Point(23, 212);
            this.zoomLevelLabel.Name = "zoomLevelLabel";
            this.zoomLevelLabel.Size = new System.Drawing.Size(80, 16);
            this.zoomLevelLabel.TabIndex = 33;
            this.zoomLevelLabel.Text = "Zoom Level";
            // 
            // separator
            // 
            this.separator.BackColor = System.Drawing.Color.Black;
            this.separator.Dock = System.Windows.Forms.DockStyle.Right;
            this.separator.Location = new System.Drawing.Point(119, 0);
            this.separator.Name = "separator";
            this.separator.Size = new System.Drawing.Size(1, 482);
            this.separator.TabIndex = 32;
            // 
            // pointerPB
            // 
            this.pointerPB.Appearance = System.Windows.Forms.Appearance.Button;
            this.pointerPB.Checked = true;
            this.pointerPB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pointerPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pointerPB.Image = ((System.Drawing.Image)(resources.GetObject("pointerPB.Image")));
            this.pointerPB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pointerPB.Location = new System.Drawing.Point(0, 0);
            this.pointerPB.Name = "pointerPB";
            this.pointerPB.Size = new System.Drawing.Size(120, 29);
            this.pointerPB.TabIndex = 0;
            this.pointerPB.TabStop = true;
            this.pointerPB.Text = "      Pointer";
            this.pointerPB.CheckedChanged += new System.EventHandler(this.pointerPB_CheckedChanged);
            // 
            // zoomInPB
            // 
            this.zoomInPB.Appearance = System.Windows.Forms.Appearance.Button;
            this.zoomInPB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.zoomInPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomInPB.Image = ((System.Drawing.Image)(resources.GetObject("zoomInPB.Image")));
            this.zoomInPB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.zoomInPB.Location = new System.Drawing.Point(0, 28);
            this.zoomInPB.Name = "zoomInPB";
            this.zoomInPB.Size = new System.Drawing.Size(120, 29);
            this.zoomInPB.TabIndex = 1;
            this.zoomInPB.TabStop = true;
            this.zoomInPB.Text = "      Zoom In";
            this.zoomInPB.CheckedChanged += new System.EventHandler(this.zoomInPB_CheckedChanged);
            // 
            // zoomOutPB
            // 
            this.zoomOutPB.Appearance = System.Windows.Forms.Appearance.Button;
            this.zoomOutPB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.zoomOutPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomOutPB.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutPB.Image")));
            this.zoomOutPB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.zoomOutPB.Location = new System.Drawing.Point(0, 56);
            this.zoomOutPB.Name = "zoomOutPB";
            this.zoomOutPB.Size = new System.Drawing.Size(120, 28);
            this.zoomOutPB.TabIndex = 2;
            this.zoomOutPB.TabStop = true;
            this.zoomOutPB.Text = "      Zoom Out";
            this.zoomOutPB.CheckedChanged += new System.EventHandler(this.zoomOutPB_CheckedChanged);
            // 
            // zoomBar
            // 
            this.zoomBar.Location = new System.Drawing.Point(0, 227);
            this.zoomBar.Maximum = 100;
            this.zoomBar.Minimum = 1;
            this.zoomBar.Name = "zoomBar";
            this.zoomBar.Size = new System.Drawing.Size(120, 45);
            this.zoomBar.TabIndex = 4;
            this.zoomBar.TabStop = false;
            this.zoomBar.TickFrequency = 10;
            this.zoomBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.zoomBar.Value = 1;
            this.zoomBar.ValueChanged += new System.EventHandler(this.zoomBar_ValueChanged);
            // 
            // topLabel
            // 
            this.topLabel.BackColor = System.Drawing.Color.Navy;
            this.topLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topLabel.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topLabel.ForeColor = System.Drawing.Color.Yellow;
            this.topLabel.Location = new System.Drawing.Point(0, 0);
            this.topLabel.Name = "topLabel";
            this.topLabel.Size = new System.Drawing.Size(620, 24);
            this.topLabel.TabIndex = 21;
            this.topLabel.Text = "Advanced Software Engineering";
            this.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmXYZoomScroll
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(620, 506);
            this.Controls.Add(this.winChartViewer1);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.topLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmXYZoomScroll";
            this.Text = "XY Zooming and Scrolling";
            this.Load += new System.EventHandler(this.FrmXYZoomScroll_Load);
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewPortControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ChartDirector.WinChartViewer winChartViewer1;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Label zoomLevelLabel;
        private System.Windows.Forms.Label separator;
        private System.Windows.Forms.RadioButton pointerPB;
        private System.Windows.Forms.RadioButton zoomInPB;
        private System.Windows.Forms.RadioButton zoomOutPB;
        private System.Windows.Forms.TrackBar zoomBar;
        private System.Windows.Forms.Label topLabel;
        private ChartDirector.WinViewPortControl viewPortControl1;
        private System.Windows.Forms.Button savePB;
    }
}
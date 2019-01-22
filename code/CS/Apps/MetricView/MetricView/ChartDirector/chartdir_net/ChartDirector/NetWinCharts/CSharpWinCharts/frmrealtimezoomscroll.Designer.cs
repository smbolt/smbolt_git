namespace CSharpChartExplorer
{
    partial class FrmRealTimeZoomScroll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRealTimeZoomScroll));
            this.winChartViewer1 = new ChartDirector.WinChartViewer();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.savePB = new System.Windows.Forms.Button();
            this.pointerPB = new System.Windows.Forms.RadioButton();
            this.zoomInPB = new System.Windows.Forms.RadioButton();
            this.zoomOutPB = new System.Windows.Forms.RadioButton();
            this.samplePeriod = new System.Windows.Forms.NumericUpDown();
            this.valueC = new System.Windows.Forms.Label();
            this.valueB = new System.Windows.Forms.Label();
            this.valueA = new System.Windows.Forms.Label();
            this.valueCLabel = new System.Windows.Forms.Label();
            this.valueBLabel = new System.Windows.Forms.Label();
            this.valueALabel = new System.Windows.Forms.Label();
            this.simulatedMachineLabel = new System.Windows.Forms.Label();
            this.separator = new System.Windows.Forms.Label();
            this.updatePeriodLabel = new System.Windows.Forms.Label();
            this.topLabel = new System.Windows.Forms.Label();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.chartUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.dataRateTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer1)).BeginInit();
            this.leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.samplePeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // winChartViewer1
            // 
            this.winChartViewer1.Location = new System.Drawing.Point(124, 27);
            this.winChartViewer1.Name = "winChartViewer1";
            this.winChartViewer1.Size = new System.Drawing.Size(640, 350);
            this.winChartViewer1.TabIndex = 27;
            this.winChartViewer1.TabStop = false;
            this.winChartViewer1.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.winChartViewer1_ViewPortChanged);
            this.winChartViewer1.MouseMovePlotArea += new System.Windows.Forms.MouseEventHandler(this.winChartViewer1_MouseMovePlotArea);
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.leftPanel.Controls.Add(this.savePB);
            this.leftPanel.Controls.Add(this.pointerPB);
            this.leftPanel.Controls.Add(this.zoomInPB);
            this.leftPanel.Controls.Add(this.zoomOutPB);
            this.leftPanel.Controls.Add(this.samplePeriod);
            this.leftPanel.Controls.Add(this.valueC);
            this.leftPanel.Controls.Add(this.valueB);
            this.leftPanel.Controls.Add(this.valueA);
            this.leftPanel.Controls.Add(this.valueCLabel);
            this.leftPanel.Controls.Add(this.valueBLabel);
            this.leftPanel.Controls.Add(this.valueALabel);
            this.leftPanel.Controls.Add(this.simulatedMachineLabel);
            this.leftPanel.Controls.Add(this.separator);
            this.leftPanel.Controls.Add(this.updatePeriodLabel);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 24);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(120, 371);
            this.leftPanel.TabIndex = 25;
            // 
            // savePB
            // 
            this.savePB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savePB.Image = ((System.Drawing.Image)(resources.GetObject("savePB.Image")));
            this.savePB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.savePB.Location = new System.Drawing.Point(0, 109);
            this.savePB.Name = "savePB";
            this.savePB.Size = new System.Drawing.Size(120, 28);
            this.savePB.TabIndex = 3;
            this.savePB.Text = "      Save";
            this.savePB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.savePB.Click += new System.EventHandler(this.savePB_Click);
            // 
            // pointerPB
            // 
            this.pointerPB.Appearance = System.Windows.Forms.Appearance.Button;
            this.pointerPB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pointerPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pointerPB.Image = ((System.Drawing.Image)(resources.GetObject("pointerPB.Image")));
            this.pointerPB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pointerPB.Location = new System.Drawing.Point(0, -1);
            this.pointerPB.Name = "pointerPB";
            this.pointerPB.Size = new System.Drawing.Size(120, 28);
            this.pointerPB.TabIndex = 0;
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
            this.zoomInPB.Location = new System.Drawing.Point(0, 26);
            this.zoomInPB.Name = "zoomInPB";
            this.zoomInPB.Size = new System.Drawing.Size(120, 28);
            this.zoomInPB.TabIndex = 1;
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
            this.zoomOutPB.Location = new System.Drawing.Point(0, 53);
            this.zoomOutPB.Name = "zoomOutPB";
            this.zoomOutPB.Size = new System.Drawing.Size(120, 28);
            this.zoomOutPB.TabIndex = 2;
            this.zoomOutPB.Text = "      Zoom Out";
            this.zoomOutPB.CheckedChanged += new System.EventHandler(this.zoomOutPB_CheckedChanged);
            // 
            // samplePeriod
            // 
            this.samplePeriod.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.samplePeriod.Location = new System.Drawing.Point(5, 190);
            this.samplePeriod.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.samplePeriod.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.samplePeriod.Name = "samplePeriod";
            this.samplePeriod.Size = new System.Drawing.Size(112, 20);
            this.samplePeriod.TabIndex = 4;
            this.samplePeriod.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.samplePeriod.ValueChanged += new System.EventHandler(this.samplePeriod_ValueChanged);
            // 
            // valueC
            // 
            this.valueC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.valueC.Location = new System.Drawing.Point(56, 328);
            this.valueC.Name = "valueC";
            this.valueC.Size = new System.Drawing.Size(60, 22);
            this.valueC.TabIndex = 45;
            this.valueC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueB
            // 
            this.valueB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.valueB.Location = new System.Drawing.Point(56, 306);
            this.valueB.Name = "valueB";
            this.valueB.Size = new System.Drawing.Size(60, 22);
            this.valueB.TabIndex = 44;
            this.valueB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueA
            // 
            this.valueA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.valueA.Location = new System.Drawing.Point(56, 285);
            this.valueA.Name = "valueA";
            this.valueA.Size = new System.Drawing.Size(60, 22);
            this.valueA.TabIndex = 43;
            this.valueA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueCLabel
            // 
            this.valueCLabel.Location = new System.Drawing.Point(4, 328);
            this.valueCLabel.Name = "valueCLabel";
            this.valueCLabel.Size = new System.Drawing.Size(48, 22);
            this.valueCLabel.TabIndex = 42;
            this.valueCLabel.Text = "Gamma";
            this.valueCLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueBLabel
            // 
            this.valueBLabel.Location = new System.Drawing.Point(4, 306);
            this.valueBLabel.Name = "valueBLabel";
            this.valueBLabel.Size = new System.Drawing.Size(48, 22);
            this.valueBLabel.TabIndex = 41;
            this.valueBLabel.Text = "Beta";
            this.valueBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueALabel
            // 
            this.valueALabel.Location = new System.Drawing.Point(4, 285);
            this.valueALabel.Name = "valueALabel";
            this.valueALabel.Size = new System.Drawing.Size(48, 22);
            this.valueALabel.TabIndex = 40;
            this.valueALabel.Text = "Alpha";
            this.valueALabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // simulatedMachineLabel
            // 
            this.simulatedMachineLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simulatedMachineLabel.Location = new System.Drawing.Point(4, 266);
            this.simulatedMachineLabel.Name = "simulatedMachineLabel";
            this.simulatedMachineLabel.Size = new System.Drawing.Size(112, 17);
            this.simulatedMachineLabel.TabIndex = 38;
            this.simulatedMachineLabel.Text = "Simulated Machine";
            // 
            // separator
            // 
            this.separator.BackColor = System.Drawing.Color.Black;
            this.separator.Dock = System.Windows.Forms.DockStyle.Right;
            this.separator.Location = new System.Drawing.Point(119, 0);
            this.separator.Name = "separator";
            this.separator.Size = new System.Drawing.Size(1, 371);
            this.separator.TabIndex = 31;
            // 
            // updatePeriodLabel
            // 
            this.updatePeriodLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updatePeriodLabel.Location = new System.Drawing.Point(5, 173);
            this.updatePeriodLabel.Name = "updatePeriodLabel";
            this.updatePeriodLabel.Size = new System.Drawing.Size(112, 17);
            this.updatePeriodLabel.TabIndex = 1;
            this.updatePeriodLabel.Text = "Update Period (ms)";
            // 
            // topLabel
            // 
            this.topLabel.BackColor = System.Drawing.Color.Navy;
            this.topLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topLabel.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topLabel.ForeColor = System.Drawing.Color.Yellow;
            this.topLabel.Location = new System.Drawing.Point(0, 0);
            this.topLabel.Name = "topLabel";
            this.topLabel.Size = new System.Drawing.Size(768, 24);
            this.topLabel.TabIndex = 26;
            this.topLabel.Text = "Advanced Software Engineering";
            this.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.BackColor = System.Drawing.Color.White;
            this.hScrollBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hScrollBar1.Location = new System.Drawing.Point(120, 379);
            this.hScrollBar1.Maximum = 1000000000;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(648, 16);
            this.hScrollBar1.TabIndex = 5;
            this.hScrollBar1.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged);
            // 
            // chartUpdateTimer
            // 
            this.chartUpdateTimer.Enabled = true;
            this.chartUpdateTimer.Interval = 250;
            this.chartUpdateTimer.Tick += new System.EventHandler(this.chartUpdateTimer_Tick);
            // 
            // dataRateTimer
            // 
            this.dataRateTimer.Enabled = true;
            this.dataRateTimer.Interval = 250;
            this.dataRateTimer.Tick += new System.EventHandler(this.dataRateTimer_Tick);
            // 
            // FrmRealTimeZoomScroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(768, 395);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.winChartViewer1);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.topLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmRealTimeZoomScroll";
            this.Text = "Realtime Chart with Zoom/Scroll and Track Line";
            this.Load += new System.EventHandler(this.FrmRealtimeZoomScroll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer1)).EndInit();
            this.leftPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.samplePeriod)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ChartDirector.WinChartViewer winChartViewer1;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.NumericUpDown samplePeriod;
        private System.Windows.Forms.Label valueC;
        private System.Windows.Forms.Label valueB;
        private System.Windows.Forms.Label valueA;
        private System.Windows.Forms.Label valueCLabel;
        private System.Windows.Forms.Label valueBLabel;
        private System.Windows.Forms.Label valueALabel;
        private System.Windows.Forms.Label simulatedMachineLabel;
        private System.Windows.Forms.Label separator;
        private System.Windows.Forms.Label updatePeriodLabel;
        private System.Windows.Forms.Label topLabel;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.RadioButton pointerPB;
        private System.Windows.Forms.RadioButton zoomInPB;
        private System.Windows.Forms.RadioButton zoomOutPB;
        internal System.Windows.Forms.Timer chartUpdateTimer;
        internal System.Windows.Forms.Timer dataRateTimer;
        private System.Windows.Forms.Button savePB;
    }
}
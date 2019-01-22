namespace CSharpChartExplorer
{
    partial class FrmRealTimeDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRealTimeDemo));
            this.winChartViewer1 = new ChartDirector.WinChartViewer();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.samplePeriod = new System.Windows.Forms.NumericUpDown();
            this.valueC = new System.Windows.Forms.Label();
            this.valueB = new System.Windows.Forms.Label();
            this.valueA = new System.Windows.Forms.Label();
            this.valueCLabel = new System.Windows.Forms.Label();
            this.valueBLabel = new System.Windows.Forms.Label();
            this.valueALabel = new System.Windows.Forms.Label();
            this.simulatedMachineLabel = new System.Windows.Forms.Label();
            this.freezePB = new System.Windows.Forms.RadioButton();
            this.runPB = new System.Windows.Forms.RadioButton();
            this.separator = new System.Windows.Forms.Label();
            this.updatePeriodLabel = new System.Windows.Forms.Label();
            this.topLabel = new System.Windows.Forms.Label();
            this.dataRateTimer = new System.Windows.Forms.Timer(this.components);
            this.chartUpdateTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer1)).BeginInit();
            this.leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.samplePeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // winChartViewer1
            // 
            this.winChartViewer1.Location = new System.Drawing.Point(128, 32);
            this.winChartViewer1.Name = "winChartViewer1";
            this.winChartViewer1.Size = new System.Drawing.Size(600, 270);
            this.winChartViewer1.TabIndex = 24;
            this.winChartViewer1.TabStop = false;
            this.winChartViewer1.ViewPortChanged += new ChartDirector.WinViewPortEventHandler(this.winChartViewer1_ViewPortChanged);
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.leftPanel.Controls.Add(this.samplePeriod);
            this.leftPanel.Controls.Add(this.valueC);
            this.leftPanel.Controls.Add(this.valueB);
            this.leftPanel.Controls.Add(this.valueA);
            this.leftPanel.Controls.Add(this.valueCLabel);
            this.leftPanel.Controls.Add(this.valueBLabel);
            this.leftPanel.Controls.Add(this.valueALabel);
            this.leftPanel.Controls.Add(this.simulatedMachineLabel);
            this.leftPanel.Controls.Add(this.freezePB);
            this.leftPanel.Controls.Add(this.runPB);
            this.leftPanel.Controls.Add(this.separator);
            this.leftPanel.Controls.Add(this.updatePeriodLabel);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 24);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(120, 286);
            this.leftPanel.TabIndex = 22;
            // 
            // samplePeriod
            // 
            this.samplePeriod.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.samplePeriod.Location = new System.Drawing.Point(4, 99);
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
            this.samplePeriod.TabIndex = 2;
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
            this.valueC.Location = new System.Drawing.Point(56, 241);
            this.valueC.Name = "valueC";
            this.valueC.Size = new System.Drawing.Size(60, 22);
            this.valueC.TabIndex = 45;
            this.valueC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueB
            // 
            this.valueB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.valueB.Location = new System.Drawing.Point(56, 219);
            this.valueB.Name = "valueB";
            this.valueB.Size = new System.Drawing.Size(60, 22);
            this.valueB.TabIndex = 44;
            this.valueB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueA
            // 
            this.valueA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.valueA.Location = new System.Drawing.Point(56, 198);
            this.valueA.Name = "valueA";
            this.valueA.Size = new System.Drawing.Size(60, 22);
            this.valueA.TabIndex = 43;
            this.valueA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueCLabel
            // 
            this.valueCLabel.Location = new System.Drawing.Point(4, 241);
            this.valueCLabel.Name = "valueCLabel";
            this.valueCLabel.Size = new System.Drawing.Size(48, 22);
            this.valueCLabel.TabIndex = 42;
            this.valueCLabel.Text = "Gamma";
            this.valueCLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueBLabel
            // 
            this.valueBLabel.Location = new System.Drawing.Point(4, 219);
            this.valueBLabel.Name = "valueBLabel";
            this.valueBLabel.Size = new System.Drawing.Size(48, 22);
            this.valueBLabel.TabIndex = 41;
            this.valueBLabel.Text = "Beta";
            this.valueBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueALabel
            // 
            this.valueALabel.Location = new System.Drawing.Point(4, 198);
            this.valueALabel.Name = "valueALabel";
            this.valueALabel.Size = new System.Drawing.Size(48, 22);
            this.valueALabel.TabIndex = 40;
            this.valueALabel.Text = "Alpha";
            this.valueALabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // simulatedMachineLabel
            // 
            this.simulatedMachineLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simulatedMachineLabel.Location = new System.Drawing.Point(4, 180);
            this.simulatedMachineLabel.Name = "simulatedMachineLabel";
            this.simulatedMachineLabel.Size = new System.Drawing.Size(112, 17);
            this.simulatedMachineLabel.TabIndex = 38;
            this.simulatedMachineLabel.Text = "Simulated Machine";
            // 
            // freezePB
            // 
            this.freezePB.Appearance = System.Windows.Forms.Appearance.Button;
            this.freezePB.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.freezePB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.freezePB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.freezePB.Image = ((System.Drawing.Image)(resources.GetObject("freezePB.Image")));
            this.freezePB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.freezePB.Location = new System.Drawing.Point(0, 27);
            this.freezePB.Name = "freezePB";
            this.freezePB.Size = new System.Drawing.Size(120, 28);
            this.freezePB.TabIndex = 1;
            this.freezePB.Text = "       Freeze Chart";
            // 
            // runPB
            // 
            this.runPB.Appearance = System.Windows.Forms.Appearance.Button;
            this.runPB.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.runPB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.runPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runPB.Image = ((System.Drawing.Image)(resources.GetObject("runPB.Image")));
            this.runPB.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.runPB.Location = new System.Drawing.Point(0, 0);
            this.runPB.Name = "runPB";
            this.runPB.Size = new System.Drawing.Size(120, 28);
            this.runPB.TabIndex = 0;
            this.runPB.Text = "       Run Chart";
            this.runPB.CheckedChanged += new System.EventHandler(this.runPB_CheckedChanged);
            // 
            // separator
            // 
            this.separator.BackColor = System.Drawing.Color.Black;
            this.separator.Dock = System.Windows.Forms.DockStyle.Right;
            this.separator.Location = new System.Drawing.Point(119, 0);
            this.separator.Name = "separator";
            this.separator.Size = new System.Drawing.Size(1, 286);
            this.separator.TabIndex = 31;
            // 
            // updatePeriodLabel
            // 
            this.updatePeriodLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updatePeriodLabel.Location = new System.Drawing.Point(4, 82);
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
            this.topLabel.Size = new System.Drawing.Size(734, 24);
            this.topLabel.TabIndex = 23;
            this.topLabel.Text = "Advanced Software Engineering";
            this.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataRateTimer
            // 
            this.dataRateTimer.Tick += new System.EventHandler(this.dataRateTimer_Tick);
            // 
            // chartUpdateTimer
            // 
            this.chartUpdateTimer.Tick += new System.EventHandler(this.chartUpdateTimer_Tick);
            // 
            // FrmRealTimeDemo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(734, 310);
            this.Controls.Add(this.winChartViewer1);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.topLabel);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmRealTimeDemo";
            this.Text = "Simple Realtime Chart";
            this.Load += new System.EventHandler(this.FrmRealTimeDemo_Load);
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
        private System.Windows.Forms.RadioButton freezePB;
        private System.Windows.Forms.RadioButton runPB;
        private System.Windows.Forms.Label separator;
        private System.Windows.Forms.Label updatePeriodLabel;
        private System.Windows.Forms.Label topLabel;
        private System.Windows.Forms.Timer dataRateTimer;
        private System.Windows.Forms.Timer chartUpdateTimer;
    }
}
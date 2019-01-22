namespace CSharpChartExplorer
{
    partial class FrmFinanceDemo
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
            this.rightPanel = new System.Windows.Forms.Panel();
            this.winChartViewer1 = new ChartDirector.WinChartViewer();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.compareWith = new System.Windows.Forms.TextBox();
            this.compareWithLabel = new System.Windows.Forms.Label();
            this.tickerSymbol = new System.Windows.Forms.TextBox();
            this.percentageScale = new System.Windows.Forms.CheckBox();
            this.parabolicSAR = new System.Windows.Forms.CheckBox();
            this.separator = new System.Windows.Forms.Label();
            this.tickerSymbolLabel = new System.Windows.Forms.Label();
            this.movAvg2 = new System.Windows.Forms.TextBox();
            this.avgType2 = new System.Windows.Forms.ComboBox();
            this.movAvg1 = new System.Windows.Forms.TextBox();
            this.avgType1 = new System.Windows.Forms.ComboBox();
            this.indicator4 = new System.Windows.Forms.ComboBox();
            this.indicator3 = new System.Windows.Forms.ComboBox();
            this.indicator2 = new System.Windows.Forms.ComboBox();
            this.indicator1 = new System.Windows.Forms.ComboBox();
            this.priceBand = new System.Windows.Forms.ComboBox();
            this.chartType = new System.Windows.Forms.ComboBox();
            this.indicatorLabel = new System.Windows.Forms.Label();
            this.movAvgLabel = new System.Windows.Forms.Label();
            this.priceBandLabel = new System.Windows.Forms.Label();
            this.chartTypeLabel = new System.Windows.Forms.Label();
            this.logScale = new System.Windows.Forms.CheckBox();
            this.volumeBars = new System.Windows.Forms.CheckBox();
            this.chartSize = new System.Windows.Forms.ComboBox();
            this.chartSizeLabel = new System.Windows.Forms.Label();
            this.timeRange = new System.Windows.Forms.ComboBox();
            this.timePeriodLabel = new System.Windows.Forms.Label();
            this.topLabel = new System.Windows.Forms.Label();
            this.rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer1)).BeginInit();
            this.leftPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightPanel
            // 
            this.rightPanel.AutoScroll = true;
            this.rightPanel.BackColor = System.Drawing.Color.White;
            this.rightPanel.Controls.Add(this.winChartViewer1);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(160, 24);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(792, 538);
            this.rightPanel.TabIndex = 6;
            // 
            // winChartViewer1
            // 
            this.winChartViewer1.Location = new System.Drawing.Point(6, 16);
            this.winChartViewer1.Name = "winChartViewer1";
            this.winChartViewer1.Size = new System.Drawing.Size(780, 470);
            this.winChartViewer1.TabIndex = 0;
            this.winChartViewer1.TabStop = false;
            // 
            // leftPanel
            // 
            this.leftPanel.AutoScroll = true;
            this.leftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(221)))), ((int)(((byte)(255)))));
            this.leftPanel.Controls.Add(this.compareWith);
            this.leftPanel.Controls.Add(this.compareWithLabel);
            this.leftPanel.Controls.Add(this.tickerSymbol);
            this.leftPanel.Controls.Add(this.percentageScale);
            this.leftPanel.Controls.Add(this.parabolicSAR);
            this.leftPanel.Controls.Add(this.separator);
            this.leftPanel.Controls.Add(this.tickerSymbolLabel);
            this.leftPanel.Controls.Add(this.movAvg2);
            this.leftPanel.Controls.Add(this.avgType2);
            this.leftPanel.Controls.Add(this.movAvg1);
            this.leftPanel.Controls.Add(this.avgType1);
            this.leftPanel.Controls.Add(this.indicator4);
            this.leftPanel.Controls.Add(this.indicator3);
            this.leftPanel.Controls.Add(this.indicator2);
            this.leftPanel.Controls.Add(this.indicator1);
            this.leftPanel.Controls.Add(this.priceBand);
            this.leftPanel.Controls.Add(this.chartType);
            this.leftPanel.Controls.Add(this.indicatorLabel);
            this.leftPanel.Controls.Add(this.movAvgLabel);
            this.leftPanel.Controls.Add(this.priceBandLabel);
            this.leftPanel.Controls.Add(this.chartTypeLabel);
            this.leftPanel.Controls.Add(this.logScale);
            this.leftPanel.Controls.Add(this.volumeBars);
            this.leftPanel.Controls.Add(this.chartSize);
            this.leftPanel.Controls.Add(this.chartSizeLabel);
            this.leftPanel.Controls.Add(this.timeRange);
            this.leftPanel.Controls.Add(this.timePeriodLabel);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 24);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(160, 538);
            this.leftPanel.TabIndex = 4;
            // 
            // compareWith
            // 
            this.compareWith.Location = new System.Drawing.Point(8, 64);
            this.compareWith.Name = "compareWith";
            this.compareWith.Size = new System.Drawing.Size(128, 20);
            this.compareWith.TabIndex = 1;
            this.compareWith.Leave += new System.EventHandler(this.compareWith_Leave);
            this.compareWith.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.compareWith_KeyPress);
            // 
            // compareWithLabel
            // 
            this.compareWithLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compareWithLabel.Location = new System.Drawing.Point(8, 48);
            this.compareWithLabel.Name = "compareWithLabel";
            this.compareWithLabel.Size = new System.Drawing.Size(100, 16);
            this.compareWithLabel.TabIndex = 21;
            this.compareWithLabel.Text = "Compare With";
            // 
            // tickerSymbol
            // 
            this.tickerSymbol.Location = new System.Drawing.Point(8, 24);
            this.tickerSymbol.Name = "tickerSymbol";
            this.tickerSymbol.Size = new System.Drawing.Size(128, 20);
            this.tickerSymbol.TabIndex = 0;
            this.tickerSymbol.Text = "ASE.SYMBOL";
            this.tickerSymbol.Leave += new System.EventHandler(this.tickerSymbol_Leave);
            this.tickerSymbol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tickerSymbol_KeyPress);
            // 
            // percentageScale
            // 
            this.percentageScale.Location = new System.Drawing.Point(8, 236);
            this.percentageScale.Name = "percentageScale";
            this.percentageScale.Size = new System.Drawing.Size(128, 20);
            this.percentageScale.TabIndex = 7;
            this.percentageScale.Text = "Percentage Scale";
            this.percentageScale.CheckedChanged += new System.EventHandler(this.selectionChanged);
            // 
            // parabolicSAR
            // 
            this.parabolicSAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(221)))), ((int)(((byte)(255)))));
            this.parabolicSAR.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parabolicSAR.Location = new System.Drawing.Point(8, 196);
            this.parabolicSAR.Name = "parabolicSAR";
            this.parabolicSAR.Size = new System.Drawing.Size(128, 20);
            this.parabolicSAR.TabIndex = 5;
            this.parabolicSAR.Text = "Parabolic SAR";
            this.parabolicSAR.UseVisualStyleBackColor = false;
            this.parabolicSAR.CheckedChanged += new System.EventHandler(this.selectionChanged);
            // 
            // separator
            // 
            this.separator.BackColor = System.Drawing.Color.Black;
            this.separator.Dock = System.Windows.Forms.DockStyle.Right;
            this.separator.Location = new System.Drawing.Point(159, 0);
            this.separator.Name = "separator";
            this.separator.Size = new System.Drawing.Size(1, 538);
            this.separator.TabIndex = 19;
            // 
            // tickerSymbolLabel
            // 
            this.tickerSymbolLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tickerSymbolLabel.Location = new System.Drawing.Point(8, 8);
            this.tickerSymbolLabel.Name = "tickerSymbolLabel";
            this.tickerSymbolLabel.Size = new System.Drawing.Size(100, 16);
            this.tickerSymbolLabel.TabIndex = 18;
            this.tickerSymbolLabel.Text = "Ticker Symbol";
            // 
            // movAvg2
            // 
            this.movAvg2.Location = new System.Drawing.Point(96, 388);
            this.movAvg2.Name = "movAvg2";
            this.movAvg2.Size = new System.Drawing.Size(40, 20);
            this.movAvg2.TabIndex = 13;
            this.movAvg2.Text = "25";
            this.movAvg2.TextChanged += new System.EventHandler(this.selectionChanged);
            // 
            // avgType2
            // 
            this.avgType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.avgType2.Location = new System.Drawing.Point(8, 388);
            this.avgType2.Name = "avgType2";
            this.avgType2.Size = new System.Drawing.Size(88, 22);
            this.avgType2.TabIndex = 12;
            this.avgType2.SelectedIndexChanged += new System.EventHandler(this.selectionChanged);
            // 
            // movAvg1
            // 
            this.movAvg1.Location = new System.Drawing.Point(96, 364);
            this.movAvg1.Name = "movAvg1";
            this.movAvg1.Size = new System.Drawing.Size(40, 20);
            this.movAvg1.TabIndex = 11;
            this.movAvg1.Text = "10";
            this.movAvg1.TextChanged += new System.EventHandler(this.selectionChanged);
            // 
            // avgType1
            // 
            this.avgType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.avgType1.Location = new System.Drawing.Point(8, 364);
            this.avgType1.Name = "avgType1";
            this.avgType1.Size = new System.Drawing.Size(88, 22);
            this.avgType1.TabIndex = 10;
            this.avgType1.SelectedIndexChanged += new System.EventHandler(this.selectionChanged);
            // 
            // indicator4
            // 
            this.indicator4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.indicator4.Location = new System.Drawing.Point(8, 504);
            this.indicator4.Name = "indicator4";
            this.indicator4.Size = new System.Drawing.Size(130, 22);
            this.indicator4.TabIndex = 17;
            this.indicator4.SelectedIndexChanged += new System.EventHandler(this.selectionChanged);
            // 
            // indicator3
            // 
            this.indicator3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.indicator3.Location = new System.Drawing.Point(8, 480);
            this.indicator3.Name = "indicator3";
            this.indicator3.Size = new System.Drawing.Size(130, 22);
            this.indicator3.TabIndex = 16;
            this.indicator3.SelectedIndexChanged += new System.EventHandler(this.selectionChanged);
            // 
            // indicator2
            // 
            this.indicator2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.indicator2.Location = new System.Drawing.Point(8, 456);
            this.indicator2.Name = "indicator2";
            this.indicator2.Size = new System.Drawing.Size(130, 22);
            this.indicator2.TabIndex = 15;
            this.indicator2.SelectedIndexChanged += new System.EventHandler(this.selectionChanged);
            // 
            // indicator1
            // 
            this.indicator1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.indicator1.Location = new System.Drawing.Point(8, 432);
            this.indicator1.Name = "indicator1";
            this.indicator1.Size = new System.Drawing.Size(130, 22);
            this.indicator1.TabIndex = 14;
            this.indicator1.SelectedIndexChanged += new System.EventHandler(this.selectionChanged);
            // 
            // priceBand
            // 
            this.priceBand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.priceBand.Location = new System.Drawing.Point(8, 320);
            this.priceBand.Name = "priceBand";
            this.priceBand.Size = new System.Drawing.Size(130, 22);
            this.priceBand.TabIndex = 9;
            this.priceBand.SelectedIndexChanged += new System.EventHandler(this.selectionChanged);
            // 
            // chartType
            // 
            this.chartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chartType.Location = new System.Drawing.Point(8, 276);
            this.chartType.Name = "chartType";
            this.chartType.Size = new System.Drawing.Size(130, 22);
            this.chartType.TabIndex = 8;
            this.chartType.SelectedIndexChanged += new System.EventHandler(this.selectionChanged);
            // 
            // indicatorLabel
            // 
            this.indicatorLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.indicatorLabel.Location = new System.Drawing.Point(8, 416);
            this.indicatorLabel.Name = "indicatorLabel";
            this.indicatorLabel.Size = new System.Drawing.Size(128, 16);
            this.indicatorLabel.TabIndex = 13;
            this.indicatorLabel.Text = "Technical Indicators";
            // 
            // movAvgLabel
            // 
            this.movAvgLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.movAvgLabel.Location = new System.Drawing.Point(8, 348);
            this.movAvgLabel.Name = "movAvgLabel";
            this.movAvgLabel.Size = new System.Drawing.Size(100, 16);
            this.movAvgLabel.TabIndex = 12;
            this.movAvgLabel.Text = "Moving Averages";
            // 
            // priceBandLabel
            // 
            this.priceBandLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceBandLabel.Location = new System.Drawing.Point(8, 304);
            this.priceBandLabel.Name = "priceBandLabel";
            this.priceBandLabel.Size = new System.Drawing.Size(100, 16);
            this.priceBandLabel.TabIndex = 11;
            this.priceBandLabel.Text = "Price Band";
            // 
            // chartTypeLabel
            // 
            this.chartTypeLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartTypeLabel.Location = new System.Drawing.Point(8, 260);
            this.chartTypeLabel.Name = "chartTypeLabel";
            this.chartTypeLabel.Size = new System.Drawing.Size(100, 16);
            this.chartTypeLabel.TabIndex = 10;
            this.chartTypeLabel.Text = "Chart Type";
            // 
            // logScale
            // 
            this.logScale.Location = new System.Drawing.Point(8, 216);
            this.logScale.Name = "logScale";
            this.logScale.Size = new System.Drawing.Size(128, 20);
            this.logScale.TabIndex = 6;
            this.logScale.Text = "Log Scale";
            this.logScale.CheckedChanged += new System.EventHandler(this.selectionChanged);
            // 
            // volumeBars
            // 
            this.volumeBars.Checked = true;
            this.volumeBars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.volumeBars.Location = new System.Drawing.Point(8, 176);
            this.volumeBars.Name = "volumeBars";
            this.volumeBars.Size = new System.Drawing.Size(128, 20);
            this.volumeBars.TabIndex = 4;
            this.volumeBars.Text = "Show Volume Bars";
            this.volumeBars.CheckedChanged += new System.EventHandler(this.selectionChanged);
            // 
            // chartSize
            // 
            this.chartSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chartSize.Location = new System.Drawing.Point(8, 148);
            this.chartSize.Name = "chartSize";
            this.chartSize.Size = new System.Drawing.Size(130, 22);
            this.chartSize.TabIndex = 3;
            this.chartSize.SelectedIndexChanged += new System.EventHandler(this.selectionChanged);
            // 
            // chartSizeLabel
            // 
            this.chartSizeLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartSizeLabel.Location = new System.Drawing.Point(8, 132);
            this.chartSizeLabel.Name = "chartSizeLabel";
            this.chartSizeLabel.Size = new System.Drawing.Size(100, 16);
            this.chartSizeLabel.TabIndex = 2;
            this.chartSizeLabel.Text = "Chart Size";
            // 
            // timeRange
            // 
            this.timeRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeRange.Location = new System.Drawing.Point(8, 104);
            this.timeRange.MaxDropDownItems = 20;
            this.timeRange.Name = "timeRange";
            this.timeRange.Size = new System.Drawing.Size(130, 22);
            this.timeRange.TabIndex = 2;
            this.timeRange.SelectedIndexChanged += new System.EventHandler(this.selectionChanged);
            // 
            // timePeriodLabel
            // 
            this.timePeriodLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timePeriodLabel.Location = new System.Drawing.Point(8, 88);
            this.timePeriodLabel.Name = "timePeriodLabel";
            this.timePeriodLabel.Size = new System.Drawing.Size(100, 16);
            this.timePeriodLabel.TabIndex = 0;
            this.timePeriodLabel.Text = "Time Period";
            // 
            // topLabel
            // 
            this.topLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(136)))));
            this.topLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topLabel.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topLabel.ForeColor = System.Drawing.Color.Yellow;
            this.topLabel.Location = new System.Drawing.Point(0, 0);
            this.topLabel.Name = "topLabel";
            this.topLabel.Size = new System.Drawing.Size(952, 24);
            this.topLabel.TabIndex = 5;
            this.topLabel.Text = "Advanced Software Engineering";
            this.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmFinanceDemo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(952, 562);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.topLabel);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmFinanceDemo";
            this.Text = "ChartDirector Financial Chart Demonstration";
            this.Load += new System.EventHandler(this.FrmFinanceDemo_Load);
            this.rightPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewer1)).EndInit();
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel rightPanel;
        private ChartDirector.WinChartViewer winChartViewer1;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.TextBox compareWith;
        private System.Windows.Forms.Label compareWithLabel;
        private System.Windows.Forms.TextBox tickerSymbol;
        private System.Windows.Forms.CheckBox percentageScale;
        private System.Windows.Forms.CheckBox parabolicSAR;
        private System.Windows.Forms.Label separator;
        private System.Windows.Forms.Label tickerSymbolLabel;
        private System.Windows.Forms.TextBox movAvg2;
        private System.Windows.Forms.ComboBox avgType2;
        private System.Windows.Forms.TextBox movAvg1;
        private System.Windows.Forms.ComboBox avgType1;
        private System.Windows.Forms.ComboBox indicator4;
        private System.Windows.Forms.ComboBox indicator3;
        private System.Windows.Forms.ComboBox indicator2;
        private System.Windows.Forms.ComboBox indicator1;
        private System.Windows.Forms.ComboBox priceBand;
        private System.Windows.Forms.ComboBox chartType;
        private System.Windows.Forms.Label indicatorLabel;
        private System.Windows.Forms.Label movAvgLabel;
        private System.Windows.Forms.Label priceBandLabel;
        private System.Windows.Forms.Label chartTypeLabel;
        private System.Windows.Forms.CheckBox logScale;
        private System.Windows.Forms.CheckBox volumeBars;
        private System.Windows.Forms.ComboBox chartSize;
        private System.Windows.Forms.Label chartSizeLabel;
        private System.Windows.Forms.ComboBox timeRange;
        private System.Windows.Forms.Label timePeriodLabel;
        private System.Windows.Forms.Label topLabel;

    }
}
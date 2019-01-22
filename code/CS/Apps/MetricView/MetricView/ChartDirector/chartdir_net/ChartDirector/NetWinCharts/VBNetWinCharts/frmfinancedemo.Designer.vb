<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFinanceDemo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.rightPanel = New System.Windows.Forms.Panel
        Me.winChartViewer1 = New ChartDirector.WinChartViewer
        Me.leftPanel = New System.Windows.Forms.Panel
        Me.compareWith = New System.Windows.Forms.TextBox
        Me.compareWithLabel = New System.Windows.Forms.Label
        Me.tickerSymbol = New System.Windows.Forms.TextBox
        Me.percentageScale = New System.Windows.Forms.CheckBox
        Me.parabolicSAR = New System.Windows.Forms.CheckBox
        Me.separator = New System.Windows.Forms.Label
        Me.tickerSymbolLabel = New System.Windows.Forms.Label
        Me.movAvg2 = New System.Windows.Forms.TextBox
        Me.avgType2 = New System.Windows.Forms.ComboBox
        Me.movAvg1 = New System.Windows.Forms.TextBox
        Me.avgType1 = New System.Windows.Forms.ComboBox
        Me.indicator4 = New System.Windows.Forms.ComboBox
        Me.indicator3 = New System.Windows.Forms.ComboBox
        Me.indicator2 = New System.Windows.Forms.ComboBox
        Me.indicator1 = New System.Windows.Forms.ComboBox
        Me.priceBand = New System.Windows.Forms.ComboBox
        Me.chartType = New System.Windows.Forms.ComboBox
        Me.indicatorLabel = New System.Windows.Forms.Label
        Me.movAvgLabel = New System.Windows.Forms.Label
        Me.priceBandLabel = New System.Windows.Forms.Label
        Me.chartTypeLabel = New System.Windows.Forms.Label
        Me.logScale = New System.Windows.Forms.CheckBox
        Me.volumeBars = New System.Windows.Forms.CheckBox
        Me.chartSize = New System.Windows.Forms.ComboBox
        Me.chartSizeLabel = New System.Windows.Forms.Label
        Me.timeRange = New System.Windows.Forms.ComboBox
        Me.timePeriodLabel = New System.Windows.Forms.Label
        Me.topLabel = New System.Windows.Forms.Label
        Me.rightPanel.SuspendLayout()
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.leftPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'rightPanel
        '
        Me.rightPanel.AutoScroll = True
        Me.rightPanel.BackColor = System.Drawing.Color.White
        Me.rightPanel.Controls.Add(Me.winChartViewer1)
        Me.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rightPanel.Location = New System.Drawing.Point(160, 24)
        Me.rightPanel.Name = "rightPanel"
        Me.rightPanel.Size = New System.Drawing.Size(792, 538)
        Me.rightPanel.TabIndex = 9
        '
        'winChartViewer1
        '
        Me.winChartViewer1.Location = New System.Drawing.Point(6, 16)
        Me.winChartViewer1.Name = "winChartViewer1"
        Me.winChartViewer1.Size = New System.Drawing.Size(780, 470)
        Me.winChartViewer1.TabIndex = 0
        Me.winChartViewer1.TabStop = False
        '
        'leftPanel
        '
        Me.leftPanel.AutoScroll = True
        Me.leftPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.leftPanel.Controls.Add(Me.compareWith)
        Me.leftPanel.Controls.Add(Me.compareWithLabel)
        Me.leftPanel.Controls.Add(Me.tickerSymbol)
        Me.leftPanel.Controls.Add(Me.percentageScale)
        Me.leftPanel.Controls.Add(Me.parabolicSAR)
        Me.leftPanel.Controls.Add(Me.separator)
        Me.leftPanel.Controls.Add(Me.tickerSymbolLabel)
        Me.leftPanel.Controls.Add(Me.movAvg2)
        Me.leftPanel.Controls.Add(Me.avgType2)
        Me.leftPanel.Controls.Add(Me.movAvg1)
        Me.leftPanel.Controls.Add(Me.avgType1)
        Me.leftPanel.Controls.Add(Me.indicator4)
        Me.leftPanel.Controls.Add(Me.indicator3)
        Me.leftPanel.Controls.Add(Me.indicator2)
        Me.leftPanel.Controls.Add(Me.indicator1)
        Me.leftPanel.Controls.Add(Me.priceBand)
        Me.leftPanel.Controls.Add(Me.chartType)
        Me.leftPanel.Controls.Add(Me.indicatorLabel)
        Me.leftPanel.Controls.Add(Me.movAvgLabel)
        Me.leftPanel.Controls.Add(Me.priceBandLabel)
        Me.leftPanel.Controls.Add(Me.chartTypeLabel)
        Me.leftPanel.Controls.Add(Me.logScale)
        Me.leftPanel.Controls.Add(Me.volumeBars)
        Me.leftPanel.Controls.Add(Me.chartSize)
        Me.leftPanel.Controls.Add(Me.chartSizeLabel)
        Me.leftPanel.Controls.Add(Me.timeRange)
        Me.leftPanel.Controls.Add(Me.timePeriodLabel)
        Me.leftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.leftPanel.Location = New System.Drawing.Point(0, 24)
        Me.leftPanel.Name = "leftPanel"
        Me.leftPanel.Size = New System.Drawing.Size(160, 538)
        Me.leftPanel.TabIndex = 7
        '
        'compareWith
        '
        Me.compareWith.Location = New System.Drawing.Point(8, 64)
        Me.compareWith.Name = "compareWith"
        Me.compareWith.Size = New System.Drawing.Size(128, 20)
        Me.compareWith.TabIndex = 1
        '
        'compareWithLabel
        '
        Me.compareWithLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.compareWithLabel.Location = New System.Drawing.Point(8, 48)
        Me.compareWithLabel.Name = "compareWithLabel"
        Me.compareWithLabel.Size = New System.Drawing.Size(100, 16)
        Me.compareWithLabel.TabIndex = 21
        Me.compareWithLabel.Text = "Compare With"
        '
        'tickerSymbol
        '
        Me.tickerSymbol.Location = New System.Drawing.Point(8, 24)
        Me.tickerSymbol.Name = "tickerSymbol"
        Me.tickerSymbol.Size = New System.Drawing.Size(128, 20)
        Me.tickerSymbol.TabIndex = 0
        Me.tickerSymbol.Text = "ASE.SYMBOL"
        '
        'percentageScale
        '
        Me.percentageScale.Location = New System.Drawing.Point(8, 236)
        Me.percentageScale.Name = "percentageScale"
        Me.percentageScale.Size = New System.Drawing.Size(128, 20)
        Me.percentageScale.TabIndex = 7
        Me.percentageScale.Text = "Percentage Scale"
        '
        'parabolicSAR
        '
        Me.parabolicSAR.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.parabolicSAR.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.parabolicSAR.Location = New System.Drawing.Point(8, 196)
        Me.parabolicSAR.Name = "parabolicSAR"
        Me.parabolicSAR.Size = New System.Drawing.Size(128, 20)
        Me.parabolicSAR.TabIndex = 5
        Me.parabolicSAR.Text = "Parabolic SAR"
        Me.parabolicSAR.UseVisualStyleBackColor = False
        '
        'separator
        '
        Me.separator.BackColor = System.Drawing.Color.Black
        Me.separator.Dock = System.Windows.Forms.DockStyle.Right
        Me.separator.Location = New System.Drawing.Point(159, 0)
        Me.separator.Name = "separator"
        Me.separator.Size = New System.Drawing.Size(1, 538)
        Me.separator.TabIndex = 19
        '
        'tickerSymbolLabel
        '
        Me.tickerSymbolLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tickerSymbolLabel.Location = New System.Drawing.Point(8, 8)
        Me.tickerSymbolLabel.Name = "tickerSymbolLabel"
        Me.tickerSymbolLabel.Size = New System.Drawing.Size(100, 16)
        Me.tickerSymbolLabel.TabIndex = 18
        Me.tickerSymbolLabel.Text = "Ticker Symbol"
        '
        'movAvg2
        '
        Me.movAvg2.Location = New System.Drawing.Point(96, 388)
        Me.movAvg2.Name = "movAvg2"
        Me.movAvg2.Size = New System.Drawing.Size(40, 20)
        Me.movAvg2.TabIndex = 13
        Me.movAvg2.Text = "25"
        '
        'avgType2
        '
        Me.avgType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.avgType2.Location = New System.Drawing.Point(8, 388)
        Me.avgType2.Name = "avgType2"
        Me.avgType2.Size = New System.Drawing.Size(88, 22)
        Me.avgType2.TabIndex = 12
        '
        'movAvg1
        '
        Me.movAvg1.Location = New System.Drawing.Point(96, 364)
        Me.movAvg1.Name = "movAvg1"
        Me.movAvg1.Size = New System.Drawing.Size(40, 20)
        Me.movAvg1.TabIndex = 11
        Me.movAvg1.Text = "10"
        '
        'avgType1
        '
        Me.avgType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.avgType1.Location = New System.Drawing.Point(8, 364)
        Me.avgType1.Name = "avgType1"
        Me.avgType1.Size = New System.Drawing.Size(88, 22)
        Me.avgType1.TabIndex = 10
        '
        'indicator4
        '
        Me.indicator4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.indicator4.Location = New System.Drawing.Point(8, 504)
        Me.indicator4.Name = "indicator4"
        Me.indicator4.Size = New System.Drawing.Size(130, 22)
        Me.indicator4.TabIndex = 17
        '
        'indicator3
        '
        Me.indicator3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.indicator3.Location = New System.Drawing.Point(8, 480)
        Me.indicator3.Name = "indicator3"
        Me.indicator3.Size = New System.Drawing.Size(130, 22)
        Me.indicator3.TabIndex = 16
        '
        'indicator2
        '
        Me.indicator2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.indicator2.Location = New System.Drawing.Point(8, 456)
        Me.indicator2.Name = "indicator2"
        Me.indicator2.Size = New System.Drawing.Size(130, 22)
        Me.indicator2.TabIndex = 15
        '
        'indicator1
        '
        Me.indicator1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.indicator1.Location = New System.Drawing.Point(8, 432)
        Me.indicator1.Name = "indicator1"
        Me.indicator1.Size = New System.Drawing.Size(130, 22)
        Me.indicator1.TabIndex = 14
        '
        'priceBand
        '
        Me.priceBand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.priceBand.Location = New System.Drawing.Point(8, 320)
        Me.priceBand.Name = "priceBand"
        Me.priceBand.Size = New System.Drawing.Size(130, 22)
        Me.priceBand.TabIndex = 9
        '
        'chartType
        '
        Me.chartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.chartType.Location = New System.Drawing.Point(8, 276)
        Me.chartType.Name = "chartType"
        Me.chartType.Size = New System.Drawing.Size(130, 22)
        Me.chartType.TabIndex = 8
        '
        'indicatorLabel
        '
        Me.indicatorLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.indicatorLabel.Location = New System.Drawing.Point(8, 416)
        Me.indicatorLabel.Name = "indicatorLabel"
        Me.indicatorLabel.Size = New System.Drawing.Size(128, 16)
        Me.indicatorLabel.TabIndex = 13
        Me.indicatorLabel.Text = "Technical Indicators"
        '
        'movAvgLabel
        '
        Me.movAvgLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.movAvgLabel.Location = New System.Drawing.Point(8, 348)
        Me.movAvgLabel.Name = "movAvgLabel"
        Me.movAvgLabel.Size = New System.Drawing.Size(100, 16)
        Me.movAvgLabel.TabIndex = 12
        Me.movAvgLabel.Text = "Moving Averages"
        '
        'priceBandLabel
        '
        Me.priceBandLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.priceBandLabel.Location = New System.Drawing.Point(8, 304)
        Me.priceBandLabel.Name = "priceBandLabel"
        Me.priceBandLabel.Size = New System.Drawing.Size(100, 16)
        Me.priceBandLabel.TabIndex = 11
        Me.priceBandLabel.Text = "Price Band"
        '
        'chartTypeLabel
        '
        Me.chartTypeLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chartTypeLabel.Location = New System.Drawing.Point(8, 260)
        Me.chartTypeLabel.Name = "chartTypeLabel"
        Me.chartTypeLabel.Size = New System.Drawing.Size(100, 16)
        Me.chartTypeLabel.TabIndex = 10
        Me.chartTypeLabel.Text = "Chart Type"
        '
        'logScale
        '
        Me.logScale.Location = New System.Drawing.Point(8, 216)
        Me.logScale.Name = "logScale"
        Me.logScale.Size = New System.Drawing.Size(128, 20)
        Me.logScale.TabIndex = 6
        Me.logScale.Text = "Log Scale"
        '
        'volumeBars
        '
        Me.volumeBars.Checked = True
        Me.volumeBars.CheckState = System.Windows.Forms.CheckState.Checked
        Me.volumeBars.Location = New System.Drawing.Point(8, 176)
        Me.volumeBars.Name = "volumeBars"
        Me.volumeBars.Size = New System.Drawing.Size(128, 20)
        Me.volumeBars.TabIndex = 4
        Me.volumeBars.Text = "Show Volume Bars"
        '
        'chartSize
        '
        Me.chartSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.chartSize.Location = New System.Drawing.Point(8, 148)
        Me.chartSize.Name = "chartSize"
        Me.chartSize.Size = New System.Drawing.Size(130, 22)
        Me.chartSize.TabIndex = 3
        '
        'chartSizeLabel
        '
        Me.chartSizeLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chartSizeLabel.Location = New System.Drawing.Point(8, 132)
        Me.chartSizeLabel.Name = "chartSizeLabel"
        Me.chartSizeLabel.Size = New System.Drawing.Size(100, 16)
        Me.chartSizeLabel.TabIndex = 2
        Me.chartSizeLabel.Text = "Chart Size"
        '
        'timeRange
        '
        Me.timeRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.timeRange.Location = New System.Drawing.Point(8, 104)
        Me.timeRange.MaxDropDownItems = 20
        Me.timeRange.Name = "timeRange"
        Me.timeRange.Size = New System.Drawing.Size(130, 22)
        Me.timeRange.TabIndex = 2
        '
        'timePeriodLabel
        '
        Me.timePeriodLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.timePeriodLabel.Location = New System.Drawing.Point(8, 88)
        Me.timePeriodLabel.Name = "timePeriodLabel"
        Me.timePeriodLabel.Size = New System.Drawing.Size(100, 16)
        Me.timePeriodLabel.TabIndex = 0
        Me.timePeriodLabel.Text = "Time Period"
        '
        'topLabel
        '
        Me.topLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(136, Byte), Integer))
        Me.topLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.topLabel.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.topLabel.ForeColor = System.Drawing.Color.Yellow
        Me.topLabel.Location = New System.Drawing.Point(0, 0)
        Me.topLabel.Name = "topLabel"
        Me.topLabel.Size = New System.Drawing.Size(952, 24)
        Me.topLabel.TabIndex = 8
        Me.topLabel.Text = "Advanced Software Engineering"
        Me.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmFinanceDemo
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(952, 562)
        Me.Controls.Add(Me.rightPanel)
        Me.Controls.Add(Me.leftPanel)
        Me.Controls.Add(Me.topLabel)
        Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FrmFinanceDemo"
        Me.Text = "ChartDirector Financial Chart Demonstration"
        Me.rightPanel.ResumeLayout(False)
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.leftPanel.ResumeLayout(False)
        Me.leftPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents rightPanel As System.Windows.Forms.Panel
    Private WithEvents winChartViewer1 As ChartDirector.WinChartViewer
    Private WithEvents leftPanel As System.Windows.Forms.Panel
    Private WithEvents compareWith As System.Windows.Forms.TextBox
    Private WithEvents compareWithLabel As System.Windows.Forms.Label
    Private WithEvents tickerSymbol As System.Windows.Forms.TextBox
    Private WithEvents percentageScale As System.Windows.Forms.CheckBox
    Private WithEvents parabolicSAR As System.Windows.Forms.CheckBox
    Private WithEvents separator As System.Windows.Forms.Label
    Private WithEvents tickerSymbolLabel As System.Windows.Forms.Label
    Private WithEvents movAvg2 As System.Windows.Forms.TextBox
    Private WithEvents avgType2 As System.Windows.Forms.ComboBox
    Private WithEvents movAvg1 As System.Windows.Forms.TextBox
    Private WithEvents avgType1 As System.Windows.Forms.ComboBox
    Private WithEvents indicator4 As System.Windows.Forms.ComboBox
    Private WithEvents indicator3 As System.Windows.Forms.ComboBox
    Private WithEvents indicator2 As System.Windows.Forms.ComboBox
    Private WithEvents indicator1 As System.Windows.Forms.ComboBox
    Private WithEvents priceBand As System.Windows.Forms.ComboBox
    Private WithEvents chartType As System.Windows.Forms.ComboBox
    Private WithEvents indicatorLabel As System.Windows.Forms.Label
    Private WithEvents movAvgLabel As System.Windows.Forms.Label
    Private WithEvents priceBandLabel As System.Windows.Forms.Label
    Private WithEvents chartTypeLabel As System.Windows.Forms.Label
    Private WithEvents logScale As System.Windows.Forms.CheckBox
    Private WithEvents volumeBars As System.Windows.Forms.CheckBox
    Private WithEvents chartSize As System.Windows.Forms.ComboBox
    Private WithEvents chartSizeLabel As System.Windows.Forms.Label
    Private WithEvents timeRange As System.Windows.Forms.ComboBox
    Private WithEvents timePeriodLabel As System.Windows.Forms.Label
    Private WithEvents topLabel As System.Windows.Forms.Label
End Class

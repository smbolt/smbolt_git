<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRealTimeZoomScroll
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRealTimeZoomScroll))
        Me.winChartViewer1 = New ChartDirector.WinChartViewer
        Me.topLabel = New System.Windows.Forms.Label
        Me.dataRateTimer = New System.Windows.Forms.Timer(Me.components)
        Me.chartUpdateTimer = New System.Windows.Forms.Timer(Me.components)
        Me.hScrollBar1 = New System.Windows.Forms.HScrollBar
        Me.leftPanel = New System.Windows.Forms.Panel
        Me.savePB = New System.Windows.Forms.Button
        Me.pointerPB = New System.Windows.Forms.RadioButton
        Me.zoomInPB = New System.Windows.Forms.RadioButton
        Me.zoomOutPB = New System.Windows.Forms.RadioButton
        Me.samplePeriod = New System.Windows.Forms.NumericUpDown
        Me.valueC = New System.Windows.Forms.Label
        Me.valueB = New System.Windows.Forms.Label
        Me.valueA = New System.Windows.Forms.Label
        Me.valueCLabel = New System.Windows.Forms.Label
        Me.valueBLabel = New System.Windows.Forms.Label
        Me.valueALabel = New System.Windows.Forms.Label
        Me.simulatedMachineLabel = New System.Windows.Forms.Label
        Me.separator = New System.Windows.Forms.Label
        Me.updatePeriodLabel = New System.Windows.Forms.Label
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.leftPanel.SuspendLayout()
        CType(Me.samplePeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'winChartViewer1
        '
        Me.winChartViewer1.Location = New System.Drawing.Point(124, 27)
        Me.winChartViewer1.Name = "winChartViewer1"
        Me.winChartViewer1.Size = New System.Drawing.Size(640, 350)
        Me.winChartViewer1.TabIndex = 27
        Me.winChartViewer1.TabStop = False
        '
        'topLabel
        '
        Me.topLabel.BackColor = System.Drawing.Color.Navy
        Me.topLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.topLabel.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.topLabel.ForeColor = System.Drawing.Color.Yellow
        Me.topLabel.Location = New System.Drawing.Point(0, 0)
        Me.topLabel.Name = "topLabel"
        Me.topLabel.Size = New System.Drawing.Size(768, 24)
        Me.topLabel.TabIndex = 26
        Me.topLabel.Text = "Advanced Software Engineering"
        Me.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dataRateTimer
        '
        '
        'chartUpdateTimer
        '
        '
        'hScrollBar1
        '
        Me.hScrollBar1.BackColor = System.Drawing.Color.White
        Me.hScrollBar1.Cursor = System.Windows.Forms.Cursors.Default
        Me.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hScrollBar1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hScrollBar1.Location = New System.Drawing.Point(120, 379)
        Me.hScrollBar1.Maximum = 1000000000
        Me.hScrollBar1.Name = "hScrollBar1"
        Me.hScrollBar1.Size = New System.Drawing.Size(648, 16)
        Me.hScrollBar1.TabIndex = 28
        '
        'leftPanel
        '
        Me.leftPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.leftPanel.Controls.Add(Me.savePB)
        Me.leftPanel.Controls.Add(Me.pointerPB)
        Me.leftPanel.Controls.Add(Me.zoomInPB)
        Me.leftPanel.Controls.Add(Me.zoomOutPB)
        Me.leftPanel.Controls.Add(Me.samplePeriod)
        Me.leftPanel.Controls.Add(Me.valueC)
        Me.leftPanel.Controls.Add(Me.valueB)
        Me.leftPanel.Controls.Add(Me.valueA)
        Me.leftPanel.Controls.Add(Me.valueCLabel)
        Me.leftPanel.Controls.Add(Me.valueBLabel)
        Me.leftPanel.Controls.Add(Me.valueALabel)
        Me.leftPanel.Controls.Add(Me.simulatedMachineLabel)
        Me.leftPanel.Controls.Add(Me.separator)
        Me.leftPanel.Controls.Add(Me.updatePeriodLabel)
        Me.leftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.leftPanel.Location = New System.Drawing.Point(0, 24)
        Me.leftPanel.Name = "leftPanel"
        Me.leftPanel.Size = New System.Drawing.Size(120, 371)
        Me.leftPanel.TabIndex = 29
        '
        'savePB
        '
        Me.savePB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.savePB.Image = CType(resources.GetObject("savePB.Image"), System.Drawing.Image)
        Me.savePB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.savePB.Location = New System.Drawing.Point(0, 109)
        Me.savePB.Name = "savePB"
        Me.savePB.Size = New System.Drawing.Size(120, 28)
        Me.savePB.TabIndex = 3
        Me.savePB.Text = "      Save"
        Me.savePB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pointerPB
        '
        Me.pointerPB.Appearance = System.Windows.Forms.Appearance.Button
        Me.pointerPB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pointerPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.pointerPB.Image = CType(resources.GetObject("pointerPB.Image"), System.Drawing.Image)
        Me.pointerPB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.pointerPB.Location = New System.Drawing.Point(0, -1)
        Me.pointerPB.Name = "pointerPB"
        Me.pointerPB.Size = New System.Drawing.Size(120, 28)
        Me.pointerPB.TabIndex = 0
        Me.pointerPB.Text = "      Pointer"
        '
        'zoomInPB
        '
        Me.zoomInPB.Appearance = System.Windows.Forms.Appearance.Button
        Me.zoomInPB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.zoomInPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.zoomInPB.Image = CType(resources.GetObject("zoomInPB.Image"), System.Drawing.Image)
        Me.zoomInPB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.zoomInPB.Location = New System.Drawing.Point(0, 26)
        Me.zoomInPB.Name = "zoomInPB"
        Me.zoomInPB.Size = New System.Drawing.Size(120, 28)
        Me.zoomInPB.TabIndex = 1
        Me.zoomInPB.Text = "      Zoom In"
        '
        'zoomOutPB
        '
        Me.zoomOutPB.Appearance = System.Windows.Forms.Appearance.Button
        Me.zoomOutPB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.zoomOutPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.zoomOutPB.Image = CType(resources.GetObject("zoomOutPB.Image"), System.Drawing.Image)
        Me.zoomOutPB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.zoomOutPB.Location = New System.Drawing.Point(0, 53)
        Me.zoomOutPB.Name = "zoomOutPB"
        Me.zoomOutPB.Size = New System.Drawing.Size(120, 28)
        Me.zoomOutPB.TabIndex = 2
        Me.zoomOutPB.Text = "      Zoom Out"
        '
        'samplePeriod
        '
        Me.samplePeriod.Increment = New Decimal(New Integer() {250, 0, 0, 0})
        Me.samplePeriod.Location = New System.Drawing.Point(5, 190)
        Me.samplePeriod.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
        Me.samplePeriod.Minimum = New Decimal(New Integer() {250, 0, 0, 0})
        Me.samplePeriod.Name = "samplePeriod"
        Me.samplePeriod.Size = New System.Drawing.Size(112, 20)
        Me.samplePeriod.TabIndex = 4
        Me.samplePeriod.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'valueC
        '
        Me.valueC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.valueC.Location = New System.Drawing.Point(56, 328)
        Me.valueC.Name = "valueC"
        Me.valueC.Size = New System.Drawing.Size(60, 22)
        Me.valueC.TabIndex = 45
        Me.valueC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'valueB
        '
        Me.valueB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.valueB.Location = New System.Drawing.Point(56, 306)
        Me.valueB.Name = "valueB"
        Me.valueB.Size = New System.Drawing.Size(60, 22)
        Me.valueB.TabIndex = 44
        Me.valueB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'valueA
        '
        Me.valueA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.valueA.Location = New System.Drawing.Point(56, 285)
        Me.valueA.Name = "valueA"
        Me.valueA.Size = New System.Drawing.Size(60, 22)
        Me.valueA.TabIndex = 43
        Me.valueA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'valueCLabel
        '
        Me.valueCLabel.Location = New System.Drawing.Point(4, 328)
        Me.valueCLabel.Name = "valueCLabel"
        Me.valueCLabel.Size = New System.Drawing.Size(48, 22)
        Me.valueCLabel.TabIndex = 42
        Me.valueCLabel.Text = "Gamma"
        Me.valueCLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'valueBLabel
        '
        Me.valueBLabel.Location = New System.Drawing.Point(4, 306)
        Me.valueBLabel.Name = "valueBLabel"
        Me.valueBLabel.Size = New System.Drawing.Size(48, 22)
        Me.valueBLabel.TabIndex = 41
        Me.valueBLabel.Text = "Beta"
        Me.valueBLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'valueALabel
        '
        Me.valueALabel.Location = New System.Drawing.Point(4, 285)
        Me.valueALabel.Name = "valueALabel"
        Me.valueALabel.Size = New System.Drawing.Size(48, 22)
        Me.valueALabel.TabIndex = 40
        Me.valueALabel.Text = "Alpha"
        Me.valueALabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'simulatedMachineLabel
        '
        Me.simulatedMachineLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.simulatedMachineLabel.Location = New System.Drawing.Point(4, 266)
        Me.simulatedMachineLabel.Name = "simulatedMachineLabel"
        Me.simulatedMachineLabel.Size = New System.Drawing.Size(112, 17)
        Me.simulatedMachineLabel.TabIndex = 38
        Me.simulatedMachineLabel.Text = "Simulated Machine"
        '
        'separator
        '
        Me.separator.BackColor = System.Drawing.Color.Black
        Me.separator.Dock = System.Windows.Forms.DockStyle.Right
        Me.separator.Location = New System.Drawing.Point(119, 0)
        Me.separator.Name = "separator"
        Me.separator.Size = New System.Drawing.Size(1, 371)
        Me.separator.TabIndex = 31
        '
        'updatePeriodLabel
        '
        Me.updatePeriodLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.updatePeriodLabel.Location = New System.Drawing.Point(5, 173)
        Me.updatePeriodLabel.Name = "updatePeriodLabel"
        Me.updatePeriodLabel.Size = New System.Drawing.Size(112, 17)
        Me.updatePeriodLabel.TabIndex = 1
        Me.updatePeriodLabel.Text = "Update Period (ms)"
        '
        'FrmRealTimeZoomScroll
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(768, 395)
        Me.Controls.Add(Me.hScrollBar1)
        Me.Controls.Add(Me.winChartViewer1)
        Me.Controls.Add(Me.leftPanel)
        Me.Controls.Add(Me.topLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "FrmRealTimeZoomScroll"
        Me.Text = "Realtime Chart with Zoom/Scroll and Track Line"
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.leftPanel.ResumeLayout(False)
        CType(Me.samplePeriod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents winChartViewer1 As ChartDirector.WinChartViewer
    Private WithEvents topLabel As System.Windows.Forms.Label
    Private WithEvents dataRateTimer As System.Windows.Forms.Timer
    Private WithEvents chartUpdateTimer As System.Windows.Forms.Timer
    Private WithEvents hScrollBar1 As System.Windows.Forms.HScrollBar
    Private WithEvents leftPanel As System.Windows.Forms.Panel
    Private WithEvents savePB As System.Windows.Forms.Button
    Private WithEvents pointerPB As System.Windows.Forms.RadioButton
    Private WithEvents zoomInPB As System.Windows.Forms.RadioButton
    Private WithEvents zoomOutPB As System.Windows.Forms.RadioButton
    Private WithEvents samplePeriod As System.Windows.Forms.NumericUpDown
    Private WithEvents valueC As System.Windows.Forms.Label
    Private WithEvents valueB As System.Windows.Forms.Label
    Private WithEvents valueA As System.Windows.Forms.Label
    Private WithEvents valueCLabel As System.Windows.Forms.Label
    Private WithEvents valueBLabel As System.Windows.Forms.Label
    Private WithEvents valueALabel As System.Windows.Forms.Label
    Private WithEvents simulatedMachineLabel As System.Windows.Forms.Label
    Private WithEvents separator As System.Windows.Forms.Label
    Private WithEvents updatePeriodLabel As System.Windows.Forms.Label
End Class

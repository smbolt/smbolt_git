<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmZoomScrollTrack2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmZoomScrollTrack2))
        Me.winChartViewer1 = New ChartDirector.WinChartViewer
        Me.hScrollBar1 = New System.Windows.Forms.HScrollBar
        Me.leftPanel = New System.Windows.Forms.Panel
        Me.endDateCtrl = New System.Windows.Forms.DateTimePicker
        Me.endDateLabel = New System.Windows.Forms.Label
        Me.separatorLine = New System.Windows.Forms.Label
        Me.pointerPB = New System.Windows.Forms.RadioButton
        Me.zoomInPB = New System.Windows.Forms.RadioButton
        Me.zoomOutPB = New System.Windows.Forms.RadioButton
        Me.startDateCtrl = New System.Windows.Forms.DateTimePicker
        Me.startDateLabel = New System.Windows.Forms.Label
        Me.topLabel = New System.Windows.Forms.Label
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.leftPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'winChartViewer1
        '
        Me.winChartViewer1.HotSpotCursor = System.Windows.Forms.Cursors.Hand
        Me.winChartViewer1.Location = New System.Drawing.Point(128, 32)
        Me.winChartViewer1.Name = "winChartViewer1"
        Me.winChartViewer1.Size = New System.Drawing.Size(640, 350)
        Me.winChartViewer1.TabIndex = 27
        Me.winChartViewer1.TabStop = False
        '
        'hScrollBar1
        '
        Me.hScrollBar1.BackColor = System.Drawing.Color.White
        Me.hScrollBar1.Cursor = System.Windows.Forms.Cursors.Default
        Me.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hScrollBar1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hScrollBar1.Location = New System.Drawing.Point(120, 387)
        Me.hScrollBar1.Maximum = 1000000000
        Me.hScrollBar1.Name = "hScrollBar1"
        Me.hScrollBar1.Size = New System.Drawing.Size(656, 16)
        Me.hScrollBar1.TabIndex = 24
        '
        'leftPanel
        '
        Me.leftPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.leftPanel.Controls.Add(Me.endDateCtrl)
        Me.leftPanel.Controls.Add(Me.endDateLabel)
        Me.leftPanel.Controls.Add(Me.separatorLine)
        Me.leftPanel.Controls.Add(Me.pointerPB)
        Me.leftPanel.Controls.Add(Me.zoomInPB)
        Me.leftPanel.Controls.Add(Me.zoomOutPB)
        Me.leftPanel.Controls.Add(Me.startDateCtrl)
        Me.leftPanel.Controls.Add(Me.startDateLabel)
        Me.leftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.leftPanel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.leftPanel.Location = New System.Drawing.Point(0, 24)
        Me.leftPanel.Name = "leftPanel"
        Me.leftPanel.Size = New System.Drawing.Size(120, 379)
        Me.leftPanel.TabIndex = 26
        '
        'endDateCtrl
        '
        Me.endDateCtrl.CustomFormat = ""
        Me.endDateCtrl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.endDateCtrl.Location = New System.Drawing.Point(4, 281)
        Me.endDateCtrl.Name = "endDateCtrl"
        Me.endDateCtrl.Size = New System.Drawing.Size(112, 20)
        Me.endDateCtrl.TabIndex = 4
        Me.endDateCtrl.Value = New Date(2000, 1, 1, 0, 0, 0, 0)
        '
        'endDateLabel
        '
        Me.endDateLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endDateLabel.Location = New System.Drawing.Point(4, 265)
        Me.endDateLabel.Name = "endDateLabel"
        Me.endDateLabel.Size = New System.Drawing.Size(72, 16)
        Me.endDateLabel.TabIndex = 32
        Me.endDateLabel.Text = "End Date"
        '
        'separatorLine
        '
        Me.separatorLine.BackColor = System.Drawing.Color.Black
        Me.separatorLine.Dock = System.Windows.Forms.DockStyle.Right
        Me.separatorLine.Location = New System.Drawing.Point(119, 0)
        Me.separatorLine.Name = "separatorLine"
        Me.separatorLine.Size = New System.Drawing.Size(1, 379)
        Me.separatorLine.TabIndex = 31
        '
        'pointerPB
        '
        Me.pointerPB.Appearance = System.Windows.Forms.Appearance.Button
        Me.pointerPB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pointerPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.pointerPB.Image = CType(resources.GetObject("pointerPB.Image"), System.Drawing.Image)
        Me.pointerPB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.pointerPB.Location = New System.Drawing.Point(0, 0)
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
        Me.zoomInPB.Location = New System.Drawing.Point(0, 27)
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
        Me.zoomOutPB.Location = New System.Drawing.Point(0, 54)
        Me.zoomOutPB.Name = "zoomOutPB"
        Me.zoomOutPB.Size = New System.Drawing.Size(120, 28)
        Me.zoomOutPB.TabIndex = 2
        Me.zoomOutPB.Text = "      Zoom Out"
        '
        'startDateCtrl
        '
        Me.startDateCtrl.CustomFormat = ""
        Me.startDateCtrl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.startDateCtrl.Location = New System.Drawing.Point(4, 227)
        Me.startDateCtrl.Name = "startDateCtrl"
        Me.startDateCtrl.Size = New System.Drawing.Size(112, 20)
        Me.startDateCtrl.TabIndex = 3
        Me.startDateCtrl.Value = New Date(2000, 1, 1, 0, 0, 0, 0)
        '
        'startDateLabel
        '
        Me.startDateLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startDateLabel.Location = New System.Drawing.Point(4, 211)
        Me.startDateLabel.Name = "startDateLabel"
        Me.startDateLabel.Size = New System.Drawing.Size(72, 16)
        Me.startDateLabel.TabIndex = 1
        Me.startDateLabel.Text = "Start Date"
        '
        'topLabel
        '
        Me.topLabel.BackColor = System.Drawing.Color.Navy
        Me.topLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.topLabel.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.topLabel.ForeColor = System.Drawing.Color.Yellow
        Me.topLabel.Location = New System.Drawing.Point(0, 0)
        Me.topLabel.Name = "topLabel"
        Me.topLabel.Size = New System.Drawing.Size(776, 24)
        Me.topLabel.TabIndex = 25
        Me.topLabel.Text = "Advanced Software Engineering"
        Me.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmZoomScrollTrack2
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(776, 403)
        Me.Controls.Add(Me.winChartViewer1)
        Me.Controls.Add(Me.hScrollBar1)
        Me.Controls.Add(Me.leftPanel)
        Me.Controls.Add(Me.topLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "FrmZoomScrollTrack2"
        Me.Text = "Zooming and Scrolling with Track Line (2)"
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.leftPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents winChartViewer1 As ChartDirector.WinChartViewer
    Private WithEvents hScrollBar1 As System.Windows.Forms.HScrollBar
    Private WithEvents leftPanel As System.Windows.Forms.Panel
    Private WithEvents endDateCtrl As System.Windows.Forms.DateTimePicker
    Private WithEvents endDateLabel As System.Windows.Forms.Label
    Private WithEvents separatorLine As System.Windows.Forms.Label
    Private WithEvents pointerPB As System.Windows.Forms.RadioButton
    Private WithEvents zoomInPB As System.Windows.Forms.RadioButton
    Private WithEvents zoomOutPB As System.Windows.Forms.RadioButton
    Private WithEvents startDateCtrl As System.Windows.Forms.DateTimePicker
    Private WithEvents startDateLabel As System.Windows.Forms.Label
    Private WithEvents topLabel As System.Windows.Forms.Label
End Class

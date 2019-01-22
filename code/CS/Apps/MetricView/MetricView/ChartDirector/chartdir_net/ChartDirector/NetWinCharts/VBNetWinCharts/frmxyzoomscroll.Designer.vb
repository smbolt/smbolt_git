<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmXYZoomScroll
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmXYZoomScroll))
        Me.winChartViewer1 = New ChartDirector.WinChartViewer
        Me.leftPanel = New System.Windows.Forms.Panel
        Me.viewPortControl1 = New ChartDirector.WinViewPortControl(Me.components)
        Me.savePB = New System.Windows.Forms.Button
        Me.zoomLevelLabel = New System.Windows.Forms.Label
        Me.separator = New System.Windows.Forms.Label
        Me.pointerPB = New System.Windows.Forms.RadioButton
        Me.zoomInPB = New System.Windows.Forms.RadioButton
        Me.zoomOutPB = New System.Windows.Forms.RadioButton
        Me.zoomBar = New System.Windows.Forms.TrackBar
        Me.topLabel = New System.Windows.Forms.Label
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.leftPanel.SuspendLayout()
        CType(Me.viewPortControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.zoomBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'winChartViewer1
        '
        Me.winChartViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.winChartViewer1.HotSpotCursor = System.Windows.Forms.Cursors.Hand
        Me.winChartViewer1.Location = New System.Drawing.Point(120, 24)
        Me.winChartViewer1.MouseUsage = ChartDirector.WinChartMouseUsage.ScrollOnDrag
        Me.winChartViewer1.MouseWheelZoomRatio = 1.1
        Me.winChartViewer1.Name = "winChartViewer1"
        Me.winChartViewer1.ScrollDirection = ChartDirector.WinChartDirection.HorizontalVertical
        Me.winChartViewer1.Size = New System.Drawing.Size(500, 482)
        Me.winChartViewer1.TabIndex = 22
        Me.winChartViewer1.TabStop = False
        Me.winChartViewer1.ZoomDirection = ChartDirector.WinChartDirection.HorizontalVertical
        '
        'leftPanel
        '
        Me.leftPanel.BackColor = System.Drawing.Color.LightGray
        Me.leftPanel.Controls.Add(Me.viewPortControl1)
        Me.leftPanel.Controls.Add(Me.savePB)
        Me.leftPanel.Controls.Add(Me.zoomLevelLabel)
        Me.leftPanel.Controls.Add(Me.separator)
        Me.leftPanel.Controls.Add(Me.pointerPB)
        Me.leftPanel.Controls.Add(Me.zoomInPB)
        Me.leftPanel.Controls.Add(Me.zoomOutPB)
        Me.leftPanel.Controls.Add(Me.zoomBar)
        Me.leftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.leftPanel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.leftPanel.Location = New System.Drawing.Point(0, 24)
        Me.leftPanel.Name = "leftPanel"
        Me.leftPanel.Size = New System.Drawing.Size(120, 482)
        Me.leftPanel.TabIndex = 23
        '
        'viewPortControl1
        '
        Me.viewPortControl1.Location = New System.Drawing.Point(5, 331)
        Me.viewPortControl1.Name = "viewPortControl1"
        Me.viewPortControl1.SelectionBorderColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.viewPortControl1.Size = New System.Drawing.Size(110, 110)
        Me.viewPortControl1.TabIndex = 37
        Me.viewPortControl1.TabStop = False
        Me.viewPortControl1.Viewer = Me.winChartViewer1
        Me.viewPortControl1.ViewPortBorderColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.viewPortControl1.ViewPortExternalColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'savePB
        '
        Me.savePB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.savePB.Image = CType(resources.GetObject("savePB.Image"), System.Drawing.Image)
        Me.savePB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.savePB.Location = New System.Drawing.Point(0, 112)
        Me.savePB.Name = "savePB"
        Me.savePB.Size = New System.Drawing.Size(120, 28)
        Me.savePB.TabIndex = 3
        Me.savePB.Text = "      Save"
        Me.savePB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'zoomLevelLabel
        '
        Me.zoomLevelLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.zoomLevelLabel.Location = New System.Drawing.Point(23, 212)
        Me.zoomLevelLabel.Name = "zoomLevelLabel"
        Me.zoomLevelLabel.Size = New System.Drawing.Size(80, 16)
        Me.zoomLevelLabel.TabIndex = 33
        Me.zoomLevelLabel.Text = "Zoom Level"
        '
        'separator
        '
        Me.separator.BackColor = System.Drawing.Color.Black
        Me.separator.Dock = System.Windows.Forms.DockStyle.Right
        Me.separator.Location = New System.Drawing.Point(119, 0)
        Me.separator.Name = "separator"
        Me.separator.Size = New System.Drawing.Size(1, 482)
        Me.separator.TabIndex = 32
        '
        'pointerPB
        '
        Me.pointerPB.Appearance = System.Windows.Forms.Appearance.Button
        Me.pointerPB.Checked = True
        Me.pointerPB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pointerPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.pointerPB.Image = CType(resources.GetObject("pointerPB.Image"), System.Drawing.Image)
        Me.pointerPB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.pointerPB.Location = New System.Drawing.Point(0, 0)
        Me.pointerPB.Name = "pointerPB"
        Me.pointerPB.Size = New System.Drawing.Size(120, 29)
        Me.pointerPB.TabIndex = 0
        Me.pointerPB.TabStop = True
        Me.pointerPB.Text = "      Pointer"
        '
        'zoomInPB
        '
        Me.zoomInPB.Appearance = System.Windows.Forms.Appearance.Button
        Me.zoomInPB.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.zoomInPB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.zoomInPB.Image = CType(resources.GetObject("zoomInPB.Image"), System.Drawing.Image)
        Me.zoomInPB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.zoomInPB.Location = New System.Drawing.Point(0, 28)
        Me.zoomInPB.Name = "zoomInPB"
        Me.zoomInPB.Size = New System.Drawing.Size(120, 29)
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
        Me.zoomOutPB.Location = New System.Drawing.Point(0, 56)
        Me.zoomOutPB.Name = "zoomOutPB"
        Me.zoomOutPB.Size = New System.Drawing.Size(120, 28)
        Me.zoomOutPB.TabIndex = 2
        Me.zoomOutPB.Text = "      Zoom Out"
        '
        'zoomBar
        '
        Me.zoomBar.Location = New System.Drawing.Point(0, 227)
        Me.zoomBar.Maximum = 100
        Me.zoomBar.Minimum = 1
        Me.zoomBar.Name = "zoomBar"
        Me.zoomBar.Size = New System.Drawing.Size(120, 45)
        Me.zoomBar.TabIndex = 4
        Me.zoomBar.TabStop = False
        Me.zoomBar.TickFrequency = 10
        Me.zoomBar.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.zoomBar.Value = 1
        '
        'topLabel
        '
        Me.topLabel.BackColor = System.Drawing.Color.Navy
        Me.topLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.topLabel.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.topLabel.ForeColor = System.Drawing.Color.Yellow
        Me.topLabel.Location = New System.Drawing.Point(0, 0)
        Me.topLabel.Name = "topLabel"
        Me.topLabel.Size = New System.Drawing.Size(620, 24)
        Me.topLabel.TabIndex = 24
        Me.topLabel.Text = "Advanced Software Engineering"
        Me.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmXYZoomScroll
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(620, 506)
        Me.Controls.Add(Me.winChartViewer1)
        Me.Controls.Add(Me.leftPanel)
        Me.Controls.Add(Me.topLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "FrmXYZoomScroll"
        Me.Text = "XY Zooming and Scrolling"
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.leftPanel.ResumeLayout(False)
        Me.leftPanel.PerformLayout()
        CType(Me.viewPortControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.zoomBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents winChartViewer1 As ChartDirector.WinChartViewer
    Private WithEvents leftPanel As System.Windows.Forms.Panel
    Private WithEvents zoomLevelLabel As System.Windows.Forms.Label
    Private WithEvents separator As System.Windows.Forms.Label
    Private WithEvents pointerPB As System.Windows.Forms.RadioButton
    Private WithEvents zoomInPB As System.Windows.Forms.RadioButton
    Private WithEvents zoomOutPB As System.Windows.Forms.RadioButton
    Private WithEvents zoomBar As System.Windows.Forms.TrackBar
    Private WithEvents topLabel As System.Windows.Forms.Label
    Private WithEvents savePB As System.Windows.Forms.Button
    Private WithEvents viewPortControl1 As ChartDirector.WinViewPortControl
End Class

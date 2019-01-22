<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmViewPortControlDemo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmViewPortControlDemo))
        Me.winChartViewer1 = New ChartDirector.WinChartViewer
        Me.leftPanel = New System.Windows.Forms.Panel
        Me.separatorLine = New System.Windows.Forms.Label
        Me.pointerPB = New System.Windows.Forms.RadioButton
        Me.zoomInPB = New System.Windows.Forms.RadioButton
        Me.zoomOutPB = New System.Windows.Forms.RadioButton
        Me.topLabel = New System.Windows.Forms.Label
        Me.savePB = New System.Windows.Forms.Button
        Me.viewPortControl1 = New ChartDirector.WinViewPortControl(Me.components)
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.leftPanel.SuspendLayout()
        CType(Me.viewPortControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'winChartViewer1
        '
        Me.winChartViewer1.Location = New System.Drawing.Point(128, 32)
        Me.winChartViewer1.Name = "winChartViewer1"
        Me.winChartViewer1.Size = New System.Drawing.Size(640, 400)
        Me.winChartViewer1.TabIndex = 25
        Me.winChartViewer1.TabStop = False
        '
        'leftPanel
        '
        Me.leftPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.leftPanel.Controls.Add(Me.savePB)
        Me.leftPanel.Controls.Add(Me.separatorLine)
        Me.leftPanel.Controls.Add(Me.pointerPB)
        Me.leftPanel.Controls.Add(Me.zoomInPB)
        Me.leftPanel.Controls.Add(Me.zoomOutPB)
        Me.leftPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.leftPanel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.leftPanel.Location = New System.Drawing.Point(0, 24)
        Me.leftPanel.Name = "leftPanel"
        Me.leftPanel.Size = New System.Drawing.Size(120, 486)
        Me.leftPanel.TabIndex = 24
        '
        'separatorLine
        '
        Me.separatorLine.BackColor = System.Drawing.Color.Black
        Me.separatorLine.Dock = System.Windows.Forms.DockStyle.Right
        Me.separatorLine.Location = New System.Drawing.Point(119, 0)
        Me.separatorLine.Name = "separatorLine"
        Me.separatorLine.Size = New System.Drawing.Size(1, 486)
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
        'topLabel
        '
        Me.topLabel.BackColor = System.Drawing.Color.Navy
        Me.topLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.topLabel.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.topLabel.ForeColor = System.Drawing.Color.Yellow
        Me.topLabel.Location = New System.Drawing.Point(0, 0)
        Me.topLabel.Name = "topLabel"
        Me.topLabel.Size = New System.Drawing.Size(775, 24)
        Me.topLabel.TabIndex = 23
        Me.topLabel.Text = "Advanced Software Engineering"
        Me.topLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'savePB
        '
        Me.savePB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.savePB.Image = CType(resources.GetObject("savePB.Image"), System.Drawing.Image)
        Me.savePB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.savePB.Location = New System.Drawing.Point(0, 109)
        Me.savePB.Name = "savePB"
        Me.savePB.Size = New System.Drawing.Size(120, 28)
        Me.savePB.TabIndex = 37
        Me.savePB.Text = "      Save"
        Me.savePB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'viewPortControl1
        '
        Me.viewPortControl1.Location = New System.Drawing.Point(128, 438)
        Me.viewPortControl1.Name = "viewPortControl1"
        Me.viewPortControl1.Size = New System.Drawing.Size(640, 60)
        Me.viewPortControl1.TabIndex = 26
        Me.viewPortControl1.TabStop = False
        Me.viewPortControl1.Viewer = Me.winChartViewer1
        '
        'FrmViewPortControlDemo
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(775, 510)
        Me.Controls.Add(Me.viewPortControl1)
        Me.Controls.Add(Me.winChartViewer1)
        Me.Controls.Add(Me.leftPanel)
        Me.Controls.Add(Me.topLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "FrmViewPortControlDemo"
        Me.Text = "Zooming and Scrolling with Viewport Control"
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.leftPanel.ResumeLayout(False)
        CType(Me.viewPortControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents winChartViewer1 As ChartDirector.WinChartViewer
    Private WithEvents leftPanel As System.Windows.Forms.Panel
    Private WithEvents separatorLine As System.Windows.Forms.Label
    Private WithEvents pointerPB As System.Windows.Forms.RadioButton
    Private WithEvents zoomInPB As System.Windows.Forms.RadioButton
    Private WithEvents zoomOutPB As System.Windows.Forms.RadioButton
    Private WithEvents topLabel As System.Windows.Forms.Label
    Private WithEvents savePB As System.Windows.Forms.Button
    Private WithEvents viewPortControl1 As ChartDirector.WinViewPortControl
End Class

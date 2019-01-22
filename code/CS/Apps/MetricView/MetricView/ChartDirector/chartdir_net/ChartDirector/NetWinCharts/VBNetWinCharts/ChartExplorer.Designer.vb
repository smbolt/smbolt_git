<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChartExplorer
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChartExplorer))
        Me.ToolBar = New System.Windows.Forms.ToolBar()
        Me.BackPB = New System.Windows.Forms.ToolBarButton()
        Me.ForwardPB = New System.Windows.Forms.ToolBarButton()
        Me.PreviousPB = New System.Windows.Forms.ToolBarButton()
        Me.NextPB = New System.Windows.Forms.ToolBarButton()
        Me.ViewSourcePB = New System.Windows.Forms.ToolBarButton()
        Me.HelpPB = New System.Windows.Forms.ToolBarButton()
        Me.toolBarImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.treeViewImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.treeView = New System.Windows.Forms.TreeView()
        Me.splitter = New System.Windows.Forms.Splitter()
        Me.rightPanel = New System.Windows.Forms.Panel()
        Me.chartViewer8 = New ChartDirector.WinChartViewer()
        Me.chartViewer7 = New ChartDirector.WinChartViewer()
        Me.chartViewer6 = New ChartDirector.WinChartViewer()
        Me.chartViewer5 = New ChartDirector.WinChartViewer()
        Me.chartViewer4 = New ChartDirector.WinChartViewer()
        Me.chartViewer3 = New ChartDirector.WinChartViewer()
        Me.chartViewer2 = New ChartDirector.WinChartViewer()
        Me.chartViewer1 = New ChartDirector.WinChartViewer()
        Me.line = New System.Windows.Forms.Label()
        Me.title = New System.Windows.Forms.Label()
        Me.statusBar = New System.Windows.Forms.StatusBar()
        Me.statusBarPanel = New System.Windows.Forms.StatusBarPanel()
        Me.rightPanel.SuspendLayout()
        CType(Me.chartViewer8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartViewer7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartViewer6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartViewer5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartViewer4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartViewer3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartViewer2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.statusBarPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BackPB, Me.ForwardPB, Me.PreviousPB, Me.NextPB, Me.ViewSourcePB, Me.HelpPB})
        Me.ToolBar.ButtonSize = New System.Drawing.Size(60, 50)
        Me.ToolBar.Divider = False
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.ImageList = Me.toolBarImageList
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(842, 45)
        Me.ToolBar.TabIndex = 0
        '
        'BackPB
        '
        Me.BackPB.Enabled = False
        Me.BackPB.ImageIndex = 0
        Me.BackPB.Name = "BackPB"
        Me.BackPB.Text = "Back"
        '
        'ForwardPB
        '
        Me.ForwardPB.Enabled = False
        Me.ForwardPB.ImageIndex = 1
        Me.ForwardPB.Name = "ForwardPB"
        Me.ForwardPB.Text = "Forward"
        '
        'PreviousPB
        '
        Me.PreviousPB.ImageIndex = 2
        Me.PreviousPB.Name = "PreviousPB"
        Me.PreviousPB.Text = "Previous"
        '
        'NextPB
        '
        Me.NextPB.ImageIndex = 3
        Me.NextPB.Name = "NextPB"
        Me.NextPB.Text = "Next"
        '
        'ViewSourcePB
        '
        Me.ViewSourcePB.ImageIndex = 4
        Me.ViewSourcePB.Name = "ViewSourcePB"
        Me.ViewSourcePB.Text = "View Code"
        '
        'HelpPB
        '
        Me.HelpPB.ImageIndex = 5
        Me.HelpPB.Name = "HelpPB"
        Me.HelpPB.Text = "View Doc"
        '
        'toolBarImageList
        '
        Me.toolBarImageList.ImageStream = CType(resources.GetObject("toolBarImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.toolBarImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.toolBarImageList.Images.SetKeyName(0, "")
        Me.toolBarImageList.Images.SetKeyName(1, "")
        Me.toolBarImageList.Images.SetKeyName(2, "")
        Me.toolBarImageList.Images.SetKeyName(3, "")
        Me.toolBarImageList.Images.SetKeyName(4, "")
        Me.toolBarImageList.Images.SetKeyName(5, "")
        '
        'treeViewImageList
        '
        Me.treeViewImageList.ImageStream = CType(resources.GetObject("treeViewImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.treeViewImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.treeViewImageList.Images.SetKeyName(0, "")
        Me.treeViewImageList.Images.SetKeyName(1, "")
        Me.treeViewImageList.Images.SetKeyName(2, "")
        Me.treeViewImageList.Images.SetKeyName(3, "")
        Me.treeViewImageList.Images.SetKeyName(4, "")
        Me.treeViewImageList.Images.SetKeyName(5, "")
        Me.treeViewImageList.Images.SetKeyName(6, "")
        Me.treeViewImageList.Images.SetKeyName(7, "")
        Me.treeViewImageList.Images.SetKeyName(8, "")
        Me.treeViewImageList.Images.SetKeyName(9, "")
        Me.treeViewImageList.Images.SetKeyName(10, "")
        Me.treeViewImageList.Images.SetKeyName(11, "")
        Me.treeViewImageList.Images.SetKeyName(12, "")
        Me.treeViewImageList.Images.SetKeyName(13, "")
        Me.treeViewImageList.Images.SetKeyName(14, "")
        Me.treeViewImageList.Images.SetKeyName(15, "")
        Me.treeViewImageList.Images.SetKeyName(16, "")
        Me.treeViewImageList.Images.SetKeyName(17, "")
        Me.treeViewImageList.Images.SetKeyName(18, "")
        Me.treeViewImageList.Images.SetKeyName(19, "")
        Me.treeViewImageList.Images.SetKeyName(20, "")
        Me.treeViewImageList.Images.SetKeyName(21, "linearmetericon.png")
        Me.treeViewImageList.Images.SetKeyName(22, "barmetericon.png")
        '
        'treeView
        '
        Me.treeView.Dock = System.Windows.Forms.DockStyle.Left
        Me.treeView.HotTracking = True
        Me.treeView.ImageIndex = 0
        Me.treeView.ImageList = Me.treeViewImageList
        Me.treeView.Location = New System.Drawing.Point(0, 45)
        Me.treeView.Name = "treeView"
        Me.treeView.SelectedImageIndex = 0
        Me.treeView.Size = New System.Drawing.Size(210, 440)
        Me.treeView.TabIndex = 1
        '
        'splitter
        '
        Me.splitter.Location = New System.Drawing.Point(210, 45)
        Me.splitter.Name = "splitter"
        Me.splitter.Size = New System.Drawing.Size(3, 440)
        Me.splitter.TabIndex = 2
        Me.splitter.TabStop = False
        '
        'rightPanel
        '
        Me.rightPanel.AutoScroll = True
        Me.rightPanel.BackColor = System.Drawing.SystemColors.Window
        Me.rightPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.rightPanel.Controls.Add(Me.chartViewer8)
        Me.rightPanel.Controls.Add(Me.chartViewer7)
        Me.rightPanel.Controls.Add(Me.chartViewer6)
        Me.rightPanel.Controls.Add(Me.chartViewer5)
        Me.rightPanel.Controls.Add(Me.chartViewer4)
        Me.rightPanel.Controls.Add(Me.chartViewer3)
        Me.rightPanel.Controls.Add(Me.chartViewer2)
        Me.rightPanel.Controls.Add(Me.chartViewer1)
        Me.rightPanel.Controls.Add(Me.line)
        Me.rightPanel.Controls.Add(Me.title)
        Me.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rightPanel.Location = New System.Drawing.Point(213, 45)
        Me.rightPanel.Name = "rightPanel"
        Me.rightPanel.Size = New System.Drawing.Size(629, 440)
        Me.rightPanel.TabIndex = 3
        '
        'chartViewer8
        '
        Me.chartViewer8.HotSpotCursor = System.Windows.Forms.Cursors.Hand
        Me.chartViewer8.Location = New System.Drawing.Point(368, 144)
        Me.chartViewer8.Name = "chartViewer8"
        Me.chartViewer8.Size = New System.Drawing.Size(112, 98)
        Me.chartViewer8.TabIndex = 9
        Me.chartViewer8.TabStop = False
        '
        'chartViewer7
        '
        Me.chartViewer7.HotSpotCursor = System.Windows.Forms.Cursors.Hand
        Me.chartViewer7.Location = New System.Drawing.Point(248, 144)
        Me.chartViewer7.Name = "chartViewer7"
        Me.chartViewer7.Size = New System.Drawing.Size(112, 98)
        Me.chartViewer7.TabIndex = 8
        Me.chartViewer7.TabStop = False
        '
        'chartViewer6
        '
        Me.chartViewer6.HotSpotCursor = System.Windows.Forms.Cursors.Hand
        Me.chartViewer6.Location = New System.Drawing.Point(128, 144)
        Me.chartViewer6.Name = "chartViewer6"
        Me.chartViewer6.Size = New System.Drawing.Size(112, 98)
        Me.chartViewer6.TabIndex = 7
        Me.chartViewer6.TabStop = False
        '
        'chartViewer5
        '
        Me.chartViewer5.HotSpotCursor = System.Windows.Forms.Cursors.Hand
        Me.chartViewer5.Location = New System.Drawing.Point(6, 144)
        Me.chartViewer5.Name = "chartViewer5"
        Me.chartViewer5.Size = New System.Drawing.Size(112, 98)
        Me.chartViewer5.TabIndex = 6
        Me.chartViewer5.TabStop = False
        '
        'chartViewer4
        '
        Me.chartViewer4.HotSpotCursor = System.Windows.Forms.Cursors.Hand
        Me.chartViewer4.Location = New System.Drawing.Point(368, 38)
        Me.chartViewer4.Name = "chartViewer4"
        Me.chartViewer4.Size = New System.Drawing.Size(112, 98)
        Me.chartViewer4.TabIndex = 5
        Me.chartViewer4.TabStop = False
        '
        'chartViewer3
        '
        Me.chartViewer3.HotSpotCursor = System.Windows.Forms.Cursors.Hand
        Me.chartViewer3.Location = New System.Drawing.Point(248, 38)
        Me.chartViewer3.Name = "chartViewer3"
        Me.chartViewer3.Size = New System.Drawing.Size(112, 98)
        Me.chartViewer3.TabIndex = 4
        Me.chartViewer3.TabStop = False
        '
        'chartViewer2
        '
        Me.chartViewer2.HotSpotCursor = System.Windows.Forms.Cursors.Hand
        Me.chartViewer2.Location = New System.Drawing.Point(128, 38)
        Me.chartViewer2.Name = "chartViewer2"
        Me.chartViewer2.Size = New System.Drawing.Size(112, 98)
        Me.chartViewer2.TabIndex = 3
        Me.chartViewer2.TabStop = False
        '
        'chartViewer1
        '
        Me.chartViewer1.HotSpotCursor = System.Windows.Forms.Cursors.Hand
        Me.chartViewer1.Location = New System.Drawing.Point(8, 38)
        Me.chartViewer1.Name = "chartViewer1"
        Me.chartViewer1.Size = New System.Drawing.Size(112, 98)
        Me.chartViewer1.TabIndex = 0
        Me.chartViewer1.TabStop = False
        '
        'line
        '
        Me.line.BackColor = System.Drawing.Color.DarkBlue
        Me.line.Dock = System.Windows.Forms.DockStyle.Top
        Me.line.Location = New System.Drawing.Point(0, 29)
        Me.line.Name = "line"
        Me.line.Size = New System.Drawing.Size(625, 3)
        Me.line.TabIndex = 12
        '
        'title
        '
        Me.title.AutoSize = True
        Me.title.Dock = System.Windows.Forms.DockStyle.Top
        Me.title.Font = New System.Drawing.Font("Verdana", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.title.Location = New System.Drawing.Point(0, 0)
        Me.title.Name = "title"
        Me.title.Size = New System.Drawing.Size(494, 29)
        Me.title.TabIndex = 10
        Me.title.Text = "Up to 8 charts in each demo module"
        '
        'statusBar
        '
        Me.statusBar.Location = New System.Drawing.Point(0, 485)
        Me.statusBar.Name = "statusBar"
        Me.statusBar.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel})
        Me.statusBar.ShowPanels = True
        Me.statusBar.Size = New System.Drawing.Size(842, 21)
        Me.statusBar.TabIndex = 4
        Me.statusBar.Text = "statusBar"
        '
        'statusBarPanel
        '
        Me.statusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.statusBarPanel.Name = "statusBarPanel"
        Me.statusBarPanel.Text = " Please select chart to view"
        Me.statusBarPanel.Width = 825
        '
        'ChartExplorer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(842, 506)
        Me.Controls.Add(Me.rightPanel)
        Me.Controls.Add(Me.splitter)
        Me.Controls.Add(Me.treeView)
        Me.Controls.Add(Me.ToolBar)
        Me.Controls.Add(Me.statusBar)
        Me.Name = "ChartExplorer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ChartDirector Sample Charts"
        Me.rightPanel.ResumeLayout(False)
        Me.rightPanel.PerformLayout()
        CType(Me.chartViewer8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartViewer7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartViewer6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartViewer5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartViewer4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartViewer3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartViewer2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.statusBarPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents toolBarImageList As System.Windows.Forms.ImageList
    Private WithEvents treeViewImageList As System.Windows.Forms.ImageList
    Private WithEvents ToolBar As System.Windows.Forms.ToolBar
    Private WithEvents BackPB As System.Windows.Forms.ToolBarButton
    Private WithEvents ForwardPB As System.Windows.Forms.ToolBarButton
    Private WithEvents PreviousPB As System.Windows.Forms.ToolBarButton
    Private WithEvents NextPB As System.Windows.Forms.ToolBarButton
    Friend WithEvents HelpPB As System.Windows.Forms.ToolBarButton
    Private WithEvents treeView As System.Windows.Forms.TreeView
    Friend WithEvents splitter As System.Windows.Forms.Splitter
    Friend WithEvents rightPanel As System.Windows.Forms.Panel
    Private WithEvents title As System.Windows.Forms.Label
    Private WithEvents line As System.Windows.Forms.Label
    Private WithEvents chartViewer7 As ChartDirector.WinChartViewer
    Private WithEvents chartViewer6 As ChartDirector.WinChartViewer
    Private WithEvents chartViewer5 As ChartDirector.WinChartViewer
    Private WithEvents chartViewer4 As ChartDirector.WinChartViewer
    Private WithEvents chartViewer3 As ChartDirector.WinChartViewer
    Private WithEvents chartViewer2 As ChartDirector.WinChartViewer
    Private WithEvents chartViewer8 As ChartDirector.WinChartViewer
    Private WithEvents ViewSourcePB As System.Windows.Forms.ToolBarButton
    Private WithEvents statusBar As System.Windows.Forms.StatusBar
    Private WithEvents statusBarPanel As System.Windows.Forms.StatusBarPanel
    Private WithEvents chartViewer1 As ChartDirector.WinChartViewer
End Class

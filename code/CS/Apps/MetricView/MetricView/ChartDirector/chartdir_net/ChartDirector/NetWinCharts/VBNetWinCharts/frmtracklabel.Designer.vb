<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTrackLabel
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
        Me.winChartViewer1 = New ChartDirector.WinChartViewer
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'winChartViewer1
        '
        Me.winChartViewer1.Location = New System.Drawing.Point(8, 8)
        Me.winChartViewer1.Name = "winChartViewer1"
        Me.winChartViewer1.Size = New System.Drawing.Size(640, 400)
        Me.winChartViewer1.TabIndex = 0
        Me.winChartViewer1.TabStop = False
        '
        'FrmTrackLabel
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(656, 416)
        Me.Controls.Add(Me.winChartViewer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "FrmTrackLabel"
        Me.Text = "Track Line with Data Labels"
        CType(Me.winChartViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents winChartViewer1 As ChartDirector.WinChartViewer
End Class

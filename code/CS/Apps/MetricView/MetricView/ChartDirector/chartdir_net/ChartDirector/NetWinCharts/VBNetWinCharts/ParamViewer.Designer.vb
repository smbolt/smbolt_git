<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParamViewer
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
        Me.label = New System.Windows.Forms.Label
        Me.OKPB = New System.Windows.Forms.Button
        Me.listView = New System.Windows.Forms.ListView
        Me.Key = New System.Windows.Forms.ColumnHeader
        Me.Value = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'label
        '
        Me.label.Location = New System.Drawing.Point(8, 7)
        Me.label.Name = "label"
        Me.label.Size = New System.Drawing.Size(304, 42)
        Me.label.TabIndex = 5
        Me.label.Text = "This is to demonstrate that ChartDirector charts are clickable. In this demo prog" & _
            "ram, we just display the information provided to the ClickHotSpot event handler." & _
            ""
        '
        'OKPB
        '
        Me.OKPB.Location = New System.Drawing.Point(124, 232)
        Me.OKPB.Name = "OKPB"
        Me.OKPB.Size = New System.Drawing.Size(72, 21)
        Me.OKPB.TabIndex = 4
        Me.OKPB.Text = "OK"
        '
        'listView
        '
        Me.listView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Key, Me.Value})
        Me.listView.GridLines = True
        Me.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.listView.Location = New System.Drawing.Point(8, 52)
        Me.listView.Name = "listView"
        Me.listView.Size = New System.Drawing.Size(304, 174)
        Me.listView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.listView.TabIndex = 3
        Me.listView.UseCompatibleStateImageBehavior = False
        Me.listView.View = System.Windows.Forms.View.Details
        '
        'Key
        '
        Me.Key.Text = "Key"
        Me.Key.Width = 80
        '
        'Value
        '
        Me.Value.Text = "Value"
        Me.Value.Width = 220
        '
        'ParamViewer
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(320, 261)
        Me.Controls.Add(Me.label)
        Me.Controls.Add(Me.OKPB)
        Me.Controls.Add(Me.listView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "ParamViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hot Spot Parameters"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Key As System.Windows.Forms.ColumnHeader
    Friend WithEvents Value As System.Windows.Forms.ColumnHeader
    Private WithEvents label As System.Windows.Forms.Label
    Private WithEvents OKPB As System.Windows.Forms.Button
    Private WithEvents listView As System.Windows.Forms.ListView
End Class

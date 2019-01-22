Public Class ParamViewer
    Inherits System.Windows.Forms.Form

    ' <summary>
    ' ParamViewer Constructor
    ' </summary>
    Public Sub Display(ByVal sender As Object, ByVal e As ChartDirector.WinHotSpotEventArgs)
        ' Add the name of the ChartViewer control that is being clicked
        listView.Items.Add(New ListViewItem(New String() {"source", sender.Name}))

        ' List out the parameters of the hot spot
        Dim key As DictionaryEntry
        For Each key In e.GetAttrValues()
            listView.Items.Add(New ListViewItem(New String() {key.Key, key.Value}))
        Next

        ' Display the form
        ShowDialog()
    End Sub

    ' <summary>
    ' Handler for the OK button
    ' </summary>
    Private Sub OKPB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKPB.Click
        ' Just close the Form
        Close()
    End Sub
    
End Class

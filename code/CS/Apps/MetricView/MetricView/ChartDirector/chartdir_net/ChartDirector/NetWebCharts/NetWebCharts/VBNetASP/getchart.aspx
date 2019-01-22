<%@ Page Language="VB" Debug="true" %>
<%
Dim filename As String = Request("filename")
Dim image() As Byte = Session(Request("img"))
Dim stype As String = Request("stype")

Dim contentType As String = "text/html; charset=utf-8"
If UBound(image) >= 3 Then
	Dim c0 As Integer = image(0)
	Dim c1 As Integer = image(1)
	Dim c2 As Integer = image(2)
	Dim c3 As Integer = image(3)
	If c0 = &H47 And c1 = &H49 Then
		contentType = "image/gif"
	ElseIf c1 = &H50 And c2 = &H4e Then
		contentType = "image/png"
	ElseIf c0 = &H42 And c1 = &H4d  Then
		contentType = "image/bmp"
	ElseIf c0 = &Hff And c1 = &Hd8 Then
		contentType = "image/jpeg"
	ElseIf c0 = 0 And c1 = 0 Then
		contentType = "image/vnd.wap.wbmp"
	ElseIf ".svg" = stype Then
		contentType = "image/svg+xml"
	ElseIf c0 = &H25 And c1 = &H50 And c2 = &H44 And c3 = &H46 Then
		contentType = "application/pdf"
	End If
	If c0 = &H1f And c1 = &H8b Then
		Response.AddHeader("Content-Encoding", "gzip")
	End If
End If

If filename <> "" Then
	Response.AddHeader("Content-Disposition", "inline; filename=" & filename)
End If

Response.ContentType = contentType
Response.BinaryWrite(image)
%>

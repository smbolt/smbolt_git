<%@ Language="VB" Debug="true" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View Source Code</title>
</head>
<body style="margin:5px 0px 0px 5px">
<%
Dim myPath As String = Server.MapPath(Request("file"))
Dim myFile As System.IO.StreamReader = System.IO.File.OpenText(myPath)
%>
<p style="margin-bottom:5px; font-size:15pt; font-family:verdana;"><b><%=myPath%></b></p>
<a href="javascript:history.go(-1);" style="font-size:10pt; font-family:verdana;">Back to Chart Graphics</a>
<%="<xmp>"%>
<%=myFile.ReadToEnd()%>
<%
myFile.Close()
%>
<%="</xmp>"%>
</body>
</html>

<%@ Language="C#" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ChartDirector Information</title>
</head>
<body style="margin:5px 0px 0px 5px">
<div style="font-family:verdana; font-weight:bold; font-size:18pt;">
ChartDirector Information
</div>
<hr style="border:solid 1px #000080" />
<div style="font-family:verdana; font-size:10pt;">
<ul style="margin-top:0; list-style:square; font-family:verdana; font-size:10pt;">
<li>Description : <%=Chart.getDescription()%><br /><br /></li>
<li>Version : <%=(Chart.getVersion() & 0xff000000) / 0x1000000%>.<%=(Chart.getVersion() & 0xff0000) / 0x10000%>.<%=Chart.getVersion() & 0xffff%><br /><br /></li>
<li>Copyright : <%=Chart.getCopyright()%><br /><br /></li>
<li>Boot Log : <br /><ul><li><%=Chart.getBootLog().Replace("\n", "</li><li>")%></li></ul><br /></li>
<li>Font Loading Test : <br /><ul><li><%=Chart.libgTTFTest().Replace("\n", "</li><li>")%></li></ul></li>
</ul>
</div>
</body>
</html>

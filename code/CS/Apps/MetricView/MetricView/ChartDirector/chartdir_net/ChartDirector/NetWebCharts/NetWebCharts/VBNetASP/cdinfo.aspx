<%@ Language="VB" Debug="true" %>
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
<li>Version : <%=(Chart.getVersion() And &Hff000000&) / &H1000000&%>.<%=(Chart.getVersion() And &Hff0000&) / &H10000&%>.<%=Chart.getVersion() And &Hffff&%></li><br /><br />
<li>Copyright : <%=Chart.getCopyright()%><br /><br /></li>
<li>Boot Log : <br /><ul><li><%=Replace(Chart.getBootLog(), Chr(10), "</li><li>")%></li></ul><br /></li>
<li>Font Loading Test : <br /><ul><li><%=Replace(Chart.libgTTFTest(), Chr(10), "</li><li>")%></li></ul></li>
</ul>
</div>
</body>
</html>

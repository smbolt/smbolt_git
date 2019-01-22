<%@ Language="VB" Debug="true" %>
<%
Response.ContentType = "image/png"
Response.BinaryWrite(Session(Request("img")))
%>

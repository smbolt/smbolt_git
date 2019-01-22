<%@ Language="C#" Debug="true" %>
<%
Response.ContentType = "image/png";
Response.BinaryWrite((byte[])Session[Request["img"]]);
%>

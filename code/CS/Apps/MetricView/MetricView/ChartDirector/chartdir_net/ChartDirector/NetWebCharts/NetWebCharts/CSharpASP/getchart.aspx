<%@ Language="C#" Debug="true" %>
<%
string filename = (string)Request["filename"];
byte[] image = (byte[])Session[Request["img"]];
string stype = (string)Request["stype"];

string contentType = "text/html; charset=utf-8";
if ((null != image) && (4 <= image.Length))
{
	int c0 = image[0];
	int c1 = image[1];
	int c2 = image[2];
	int c3 = image[3];
	if ((c0 == 0x47) && (c1 == 0x49))
		contentType = "image/gif";
	else if ((c1 == 0x50) && (c2 == 0x4e))
		contentType = "image/png";
	else if ((c0 == 0x42) && (c1 == 0x4d)) 
		contentType = "image/bmp";
	else if ((c0 == 0xff) && (c1 == 0xd8))
		contentType = "image/jpeg";
	else if ((c0 == 0) && (c1 == 0))
		contentType = "image/vnd.wap.wbmp";
	else if ((null != stype) && (".svg" == stype))
		contentType = "image/svg+xml";
	else if ((c0 == '%') && (c1 == 'P') && (c2 == 'D') && (c3 == 'F'))
		contentType = "application/pdf";
	if ((c0 == 0x1f) && (c1 == 0x8b))
		Response.AddHeader("Content-Encoding", "gzip");
}

if ((null != filename) && ("" != filename))
	Response.AddHeader("Content-Disposition", "inline; filename=" + filename);

Response.ContentType = contentType;
Response.BinaryWrite(image);
%>
